using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class ChronicleIssues1 : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {

            defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            #region Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                //DropDownList1.DataBind();

                //DropDownList1.Items.Add("ALL");
                //DropDownList1.Items.FindByText("ALL").Value = "%";
                //DropDownList1.Items.FindByValue("%").Selected = true;

                DropDownList2.DataBind();

                DropDownList2.Items.Add("ALL");
                DropDownList2.Items.FindByText("ALL").Value = "%";
                DropDownList2.Items.FindByValue("%").Selected = true;
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]

        public static List<string> SearchCustomers(string prefixText, int count)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select distinct atmid from atms where " +
                    "atmid like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> customers = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["atmid"].ToString());
                        }
                    }
                    conn.Close();
                    return customers;
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            #region Code to fetch data from DR_CTP
            string sql = @"Select i.atmid as [ATMID],addressline1 as [ADDRESS],bankid as [BANK],DDocketNumber as [TICKET NUMBER],
                    CONVERT(varchar(10),OpenDate,103) + ' ' + CONVERT(varchar(10),OpenDate,108) as [CALL OPEN DATE]
                    from IncidentsNew1 i inner join atms a on a.atmid=i.ATMID where FaultID like '" + DropDownList2.SelectedValue + "' and i.atmid like '" + txtAtm.Text +
                        "' and convert(date,opendate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' and i.atmid in " +
                        "(Select atmid from IncidentsNew1 where FaultID like '" + DropDownList2.SelectedValue + "' group by atmid having COUNT(atmid) >= 2)";
            bucket.BindGrid(GridView1, sql);
            //Response.Write(sql);

            /*------------------------------------------------------------------------------------------------*/
            /* If no rows returned display null error or fetch count
            /*------------------------------------------------------------------------------------------------*/
            if (GridView1.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
                Label3.Visible = false;
                ImageButton1.Visible = false;
                div1.Visible = true;
            }
            else
            {
                div1.Visible = true;
                ImageButton1.Visible = true;
                Label3.Visible = true;
                Label1.Visible = false;
                Label3.Text = bucket.CountRows(GridView1, Label3);
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            btn_search_Click(sender, e);
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment;filename=MasterReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
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
                //e.Row.Cells[i].Text=e.Row.Cells[i].Text.Replace("E|N","No-");
                //}
            }
        }
    }
}