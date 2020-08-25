using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Mphasis_webapp.bank
{
    public partial class ScheduledReport : System.Web.UI.Page
    {
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetATM(string prefixText)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = "select distinct siteid from ATMs where ltrim(rtrim(siteid)) like @SearchText + '%' and bankid='TAMILNAD MERCANTILE BANK LIMITED' ";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);
                    List<string> CountryNames = new List<string>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CountryNames.Add(dt.Rows[i][0].ToString());
                    }
                    conn.Close();
                    return CountryNames;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_go.Click += btn_go_Click;
            btnexcel.Click += btnexcel_Click;

            if (Page.IsPostBack)
            {
                try
                {
                    btn_go_Click(sender, e);
                }
                catch
                {
                }
            }
            defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
        }

        public void btn_go_Click(object sender, EventArgs e)
        {
            string query = "";

            string atm = "";
            if (txt_atmid.Text.Trim().Length == 0)
            { atm = "%"; }
            else { atm = txt_atmid.Text.Trim(); }

            string refno = "";
            if (txt_refno.Text.Trim().Length == 0)
            { refno = "%"; }
            else { refno = txt_refno.Text.Trim(); }

            if (dd_status.SelectedIndex == 0)
            {
                query = "select distinct userid as [USER ID], s.atmid as [SITE ID],convert(varchar(10),convert(date,date),103) as [DATE],visitstatus from scheduler s join atms a on a.siteid=s.atmid  where Convert(date,date) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' and s.atmid like '" + atm + "' and  isnull(refno,'NA') like '" + refno + "' and a.bankid='TAMILNAD MERCANTILE BANK LIMITED' order by date";
            }
            else if (dd_status.SelectedIndex == 1)
            {
                query = "select distinct userid as [USER ID], s.atmid as [SITE ID],convert(varchar(10),convert(date,date),103) as [DATE],visitstatus from scheduler s join atms a on a.siteid=s.atmid where visitstatus='NOT VISITED' and Convert(date,date) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' and s.atmid like '" + atm + "' and  isnull(refno,'NA') like '" + refno + "' and a.bankid='TAMILNAD MERCANTILE BANK LIMITED' order by date";
            }
            else
            {
                query = "select distinct userid as [USER ID], s.atmid as [SITE ID],convert(varchar(10),convert(date,date),103) as [DATE],visitstatus from scheduler s join atms a on a.siteid=s.atmid where visitstatus='VISITED' and Convert(date,date) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' and s.atmid like '" + atm + "' and  isnull(refno,'NA') like '" + refno + "' and a.bankid='TAMILNAD MERCANTILE BANK LIMITED' order by date";
            }

            sql.SelectCommand = query;
            sql.DataBind();

            GridView1.DataSourceID = sql.ID;
            GridView1.DataBind();
        }

        void btnexcel_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                //Page_Load(sender, e);

                GridView1.AllowPaging = false;
                GridView1.AllowSorting = false;
                GridView1.DataBind();
                //GridView1.Columns[1].Visible = false;
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
                Response.AddHeader("content-disposition", "attachment;filename=SchedulerDetails.xls");
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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_go_Click(sender, e);
        }
    }
}