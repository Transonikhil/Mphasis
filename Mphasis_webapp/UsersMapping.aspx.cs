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

namespace Mphasis_webapp
{
    public partial class UsersMapping : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        int cnt = 0;
        string role = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            //    defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            //}
            //catch (Exception ex) { }

            //txt_frmDate.Attributes.Add("readonly", "readonly");
            //txt_toDate.Attributes.Add("readonly", "readonly");

            #region Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {

                //  obj.NonExecuteQuery("update DR_CTP set version='MP_V2.4' where version='MP_V2.5'");
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
                DropDownList1.Items.FindByText("ALL").Value = "%";
                DropDownList1.Items.FindByValue("%").Selected = true;

                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);

                //txt_frmDate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");
                //txt_toDate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");
                btn_search_Click(sender, e);
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

            string strQuery = "";
            if (HttpContext.Current.Session["sess_role"].ToString() == "CH")
            {
                strQuery = "select 'All' as userid,'%' union all select distinct u.userid,u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role like '" + role + "' " +
                            " and a.CH like '" + HttpContext.Current.Session["sess_username"].ToString() + "' and u.status <> 'DEL'";
            }
            else
                if (HttpContext.Current.Session["sess_role"].ToString() == "RM")
            {
                strQuery = "select 'All' as userid,'%' union all select distinct u.userid,u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role like '" + role + "' " +
                            " and a.rm like '" + HttpContext.Current.Session["sess_username"].ToString() + "' and u.status <> 'DEL'";
            }
            else
            {
                strQuery = " select 'All' as userid,'%' union all SELECT  distinct userid as userid,userid FROM [users] where role like '" + role + "' and status <> 'DEL'";
            }


            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and state in(" + state + ")";
            }

            //Response.Write(strQuery);
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
            if (ddrole.SelectedValue == "%")
            {
                role = "u.role in ('AO','DE','CM','CH','RM')";
            }
            else
            {
                role = "u.role like '" + ddrole.SelectedValue + "'";
            }
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;

            if (users == "All" || users == null)
            {
                users = "%";
            }

            #region Code to fetch data from DR_CTP
            string sql = "";
            if (txtuser.Text == "")
            {
                sql = @"SELECT distinct u.Userid as [USER ID],role, a.ce, a.cm, u.username as [Username], a.atmid as [ATM ID], a.bankid as [BANK], a.addressline1 as [ADDRESS],
                    a.city as [CITY], a.state as [STATE] 
                    from usermap um join users u
                    on u.userid=um.userid join atms a
                    on a.atmid=um.atmid
                    where um.status <> 'DEL' and
                    u.userid like '" + users +
                        "' and " + role + "  order by u.username asc";
            }
            else
            {
                sql = @"SELECT distinct u.Userid as [USER ID],role, a.ce, a.cm, u.username as [Username], a.atmid as [ATM ID], a.bankid as [BANK], a.addressline1 as [ADDRESS],
                    a.city as [CITY], a.state as [STATE] 
                    from usermap um join users u
                    on u.userid=um.userid join atms a
                    on a.atmid=um.atmid
                    where um.status <> 'DEL' and
                    u.userid like '" + users +
                        "' and " + role + " and a.state in (" + txtuser.Text + ") order by u.username asc";
            }

            if (Session["sess_role"].ToString() == "CH")
            {
                sql = sql.Replace("and " + role, "and " + role + " and a.ch ='" + Session["sess_username"] + "'");
            }
            if (Session["sess_role"].ToString() == "RM")
            {
                sql = sql.Replace("and " + role, "and " + role + " and a.rm ='" + Session["sess_username"] + "'");
            }

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
                //HyperLink report = new HyperLink();
                //report.Text = "Download";
                //report.NavigateUrl = "Report.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&atmid=" + e.Row.Cells[4].Text.Trim() + "&dnld_excel=Y";

                //e.Row.Cells[0].Controls.Add(report);

                //HyperLink download = new HyperLink();
                //download.Text = "Download";
                //download.NavigateUrl = "photos.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&dnld_pdf=Y";

                //e.Row.Cells[1].Controls.Add(download);

                //HyperLink vid = new HyperLink();
                //vid.Text = e.Row.Cells[2].Text;
                //vid.NavigateUrl = "MainPage1.aspx?auditid=" + vid.Text;

                //e.Row.Cells[2].Controls.Add(vid);
            }

            //if (IsPostBack && obj.GetPostBackControlId(this.Page) != "btn_search")
            //{
            //    if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        e.Row.Cells[0].Visible = false;
            //        e.Row.Cells[1].Visible = false;
            //    }
            //}
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