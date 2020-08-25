using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.bank
{
    public partial class PhotoDownload : System.Web.UI.Page
    {

        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        int cnt = 0;
        string role = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM'/'dd'/'yyyy"));
                defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM'/'dd'/'yyyy"));
            }
            catch (Exception ex) { }
            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            #region Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
                DropDownList1.Items.FindByText("ALL").Value = "%";
                DropDownList1.Items.FindByValue("%").Selected = true;

                string state = "SELECT  distinct state,state as STATE  FROM [atms] where bankid='TAMILNAD MERCANTILE BANK LIMITED' ";
                obj.BindListboxWithValue(ddstate, state);
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }


        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state, string role)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all SELECT  distinct userid as userid,userid FROM [usermap] where atmid in (select atmid from atms where bankid='TAMILNAD MERCANTILE BANK LIMITED') ";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and state in(" + state + ")";
            }
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strQuery;

                        cmd.Connection = con;
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            list.Add(new ListItem(sdr[0].ToString()));
                        }
                        con.Close();
                        return list;
                    }
                    catch (Exception ex) { }
                    return list;
                }
            }
        }


        protected void btn_search_Click(object sender, EventArgs e)
        {
            //if (ddrole.SelectedValue == "%")
            //{
            //    role = "u.role in ('AO','DE','CM')";
            //}
            //else
            //{
            //    role = "u.role like '" + ddrole.SelectedValue + "'";
            //}

            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;

            if (users == "All")
            {
                users = "%";
            }

            // if (ddrole.SelectedValue == "%")
            //{
            //    Response.Redirect("BulkDownload.aspx?from=" + txt_frmDate.Text + "&&to=" + txt_toDate.Text + "&&role=%&&state=" + txtuser.Text + "&&user=" + users);
            //}
            //else
            //{
            Response.Redirect("BulkDownload.aspx?from=" + txt_frmDate.Text + "&&to=" + txt_toDate.Text + "&&state=" + txtuser.Text + "&&user=" + users);
            // }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            btn_search_Click(sender, e);
            //GridView1.AllowPaging = false;
            //GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment;filename=MasterReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            //GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Response.Write("9");
                //for (int i = 0; i < 45; i++)
                //{
                //    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|N", "No-");
                //}

                for (int i = 11; i < 52; i++)
                {

                    if (e.Row.Cells[i].Text.Contains("E|N"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|N", "No-");
                    }
                    else if (e.Row.Cells[i].Text.Contains("E|Y"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|Y", "Yes-");
                    }
                    else if (e.Row.Cells[i].Text.ToUpper().Contains("Y-"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("Y-", "Yes-");
                    }
                    else if (e.Row.Cells[i].Text.ToUpper().Contains("N-"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("N-", "No-");
                    }
                    else if (e.Row.Cells[i].Text.ToUpper() == "Y")
                    {
                        e.Row.Cells[i].Text = "Yes";
                    }
                    else if (e.Row.Cells[i].Text.ToUpper() == "N")
                    {
                        e.Row.Cells[i].Text = "No";
                    }
                }
            }
        }

        protected void ddrole_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList1.DataBind();

            DropDownList1.Items.Add("ALL");
            DropDownList1.Items.FindByText("ALL").Value = "%";
            DropDownList1.Items.FindByValue("%").Selected = true;
        }

    }
}