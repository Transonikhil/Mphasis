using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.SqlClient;

using System.Data.SqlClient;
using System.Configuration;

namespace Mphasis_webapp
{
    public partial class YesterdaysReport : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        ApplyRate app = new ApplyRate();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                app.getsitedetails();
                app.calctot();
            }

            timer_Tick(sender, e);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink report = new HyperLink();
                report.Text = e.Row.Cells[2].Text;
                report.NavigateUrl = "DetailView.aspx?site=" + e.Row.Cells[0].Text;
                e.Row.Cells[2].Controls.Add(report);
            }
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            //restart:;

            string bind = "";

            bind = @"Select p.siteid as [SITE ID],sitename as [SITE NAME],COUNT(p.siteid) as [NO. OF VISITS],SUM(totamt) as [TOTAL AMOUNT] from Parking_Master p inner join Site s on
                p.siteid=s.siteid where CONVERT(date,vdate)=CONVERT(date,getdate() - 1) group by p.siteid,s.sitename order by p.siteid asc,s.sitename asc";

            //Response.Write(bind);
            bucket.BindGrid(bind, "X", GridView1, this.Page);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                Label3.ForeColor = Color.Green;
                Label3.Text = bucket.CountRows(GridView1, Label3);
            }
            else
            {
                Label3.ForeColor = Color.Red;
                Label3.Text = "No records found";
                //goto restart;
            }

            //timer.Enabled = false;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            timer_Tick(sender, e);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            timer_Tick(sender, e);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void btn_rep_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
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
                Response.AddHeader("content-disposition", "attachment;filename=MasterReport-Parking.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                GridView1.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
                Response.Redirect("Search.aspx");
            }
        }
    }
}