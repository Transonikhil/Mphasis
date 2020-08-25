using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RM
{
    public partial class CurrentAudit1 : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        int cnt = 0;
        string state = "";
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
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
                DropDownList1.Items.FindByText("ALL").Value = "%";
                DropDownList1.Items.FindByValue("%").Selected = true;
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion

            if (!Page.IsPostBack)
            {
                string state = "SELECT  distinct state,state as STATE  FROM [atms] where rm = '" + Session["sess_username"].ToString() + "'";
                obj.BindListboxWithValue(ddstate, state);

                txt_frmDate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");
                btn_search_Click(sender, e);
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all select distinct u.userid,u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role in ('DE','CM','AO') " +
                            " and a.RM like '" + HttpContext.Current.Session["sess_username"].ToString() + "' and u.status <> 'DEL' and u.userid not like '%test%'";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and u.state in(" + state + ")";
            }
            //strQuery += "  order by userid";

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

            //For retrieving selected values of state
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;
            if (users == "All" || users == null)
            {
                users = "%";
            }

            #region Code to fetch
            string sql = "";
            if (txtuser.Text == "")
            {
                #region Code to fetch data from DR_CTP
                sql = @"SELECT '' as [Excel],'' as [Photos],d.Vid AS [VISIT ID],d.USERID AS [USER ID],d.ATMID AS [ATM ID],a.SiteID as [Site ID],
                    a.addressline1 AS LOCATION,a.state AS [STATE],a.bankid AS [BANK NAME], a.client AS MSP, 
                    convert(varchar(10),convert(date,vdate),103) AS [DATE OF VISIT], d.vtime AS [TIME OF VISIT]
                    from dr_ctp d inner join atms a on d.atmid=a.atmid where d.userid like '" + users +
                        "' and RM like '" + Session["sess_username"] + "' and convert(date,vdate) between '" + txt_frmDate.Text +
                        "' and '" + txt_toDate.Text + "'  order by convert(date,vdate) desc,convert(time,vtime) desc";
            }
            else
            {
                sql = @"SELECT '' as [Excel],'' as [Photos],d.Vid AS [VISIT ID],d.USERID AS [USER ID],d.ATMID AS [ATM ID],a.SiteID as [Site ID],
                    a.addressline1 AS LOCATION,a.state AS [STATE],a.bankid AS [BANK NAME], a.client AS MSP, 
                    convert(varchar(10),convert(date,vdate),103) AS [DATE OF VISIT], d.vtime AS [TIME OF VISIT]
                    from dr_ctp d inner join atms a on d.atmid=a.atmid where d.userid like '" + users + "' and RM like '" +
                        Session["sess_username"] + "' and convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text +
                        "' and a.state in (" + txtuser.Text + ")  order by convert(date,vdate) desc,convert(time,vtime) desc";

            }

            #endregion

            bucket.BindGrid(GridView1, sql);
            // Response.Write(sql);

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
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                btn_search_Click(sender, e);
                GridView1.DataBind();
                GridView1.AllowPaging = false;
                GridView1.AllowSorting = false;
                GridView1.DataBind();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        List<Control> controls = new List<Control>();
                        foreach (Control control in cell.Controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    controls.Add(control);
                                    break;
                                case "TextBox":
                                    controls.Add(control);
                                    break;
                                case "LinkButton":
                                    controls.Add(control);
                                    break;
                                case "CheckBox":
                                    controls.Add(control);
                                    break;
                                case "RadioButton":
                                    controls.Add(control);
                                    break;
                                case "Image":
                                    controls.Add(control);
                                    break;
                            }
                        }
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                    break;
                                case "TextBox":
                                    cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                    break;
                                case "LinkButton":
                                    cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                    break;
                                case "CheckBox":
                                    cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                    break;
                                case "RadioButton":
                                    cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                    break;
                                case "Image":
                                    cell.Controls.Add(new Literal { Text = (control as System.Web.UI.WebControls.Image).AlternateText = "" });
                                    break;
                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }
                GridView1.GridLines = GridLines.Both;
                Response.AddHeader("content-disposition", "attachment;filename=LatestAudits.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                GridView1.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
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
                HyperLink report = new HyperLink();
                report.Text = "Download";
                report.NavigateUrl = "Report.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&&atmid=" + e.Row.Cells[4].Text.Trim() + "&dnld_excel=Y";

                e.Row.Cells[0].Controls.Add(report);

                HyperLink download = new HyperLink();
                download.Text = "Download";
                download.NavigateUrl = "photos.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&dnld_pdf=Y";

                e.Row.Cells[1].Controls.Add(download);

                HyperLink vid = new HyperLink();
                vid.Text = e.Row.Cells[2].Text;
                vid.NavigateUrl = "MainPage1.aspx?auditid=" + vid.Text;

                e.Row.Cells[2].Controls.Add(vid);
            }
            //IsPostBack && obj.GetPostBackControlId(this.Page) != "btn_search"
            if (obj.GetPostBackControlId(this.Page) == "ImageButton1")
            {
                if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                }
            }
        }
    }
}