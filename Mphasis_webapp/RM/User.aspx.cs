using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RM
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            GridView1.DataBind();
            if (GridView1.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            // btn_search_Click(sender, e);
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment;filename=Users.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}