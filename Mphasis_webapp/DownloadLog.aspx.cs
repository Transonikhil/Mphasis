using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class DownloadLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["datatable"];

            if (dt.Rows.Count > 0)
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    GridView2.DataSource = dt;
                    GridView2.AllowPaging = false;

                    GridView2.AllowSorting = false;

                    GridView2.DataBind();
                    GridView2.GridLines = GridLines.Both;
                    GridView2.HorizontalAlign = HorizontalAlign.Center;
                    Response.AddHeader("content-disposition", "attachment;filename=SchedularIssue.xls");
                    Response.Charset = String.Empty;
                    Response.ContentType = "application/vnd.xls";
                    GridView2.RenderControl(hw);

                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }
            else
            {
                dt.Rows.Add("Schedular Uploaded");
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    GridView2.DataSource = dt;
                    GridView2.AllowPaging = false;

                    GridView2.AllowSorting = false;

                    GridView2.DataBind();
                    GridView2.GridLines = GridLines.Both;
                    GridView2.HorizontalAlign = HorizontalAlign.Center;
                    Response.AddHeader("content-disposition", "attachment;filename=Log.xls");
                    Response.Charset = String.Empty;
                    Response.ContentType = "application/vnd.xls";
                    GridView2.RenderControl(hw);

                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

    }
}