using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class FetchData : System.Web.UI.Page
    {
        ibuckethead obj = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                //GridView1.DataBind();
                BtnExecute_Click(sender, e);
            }
        }

        protected void BtnExecute_Click(object sender, EventArgs e)
        {
            string userinput = lblselect.Text + " " + txtsearch.Text.ToString().ToLower();
            try
            {


                //only accept the alphabets and numbers , rest will be replaced by blank    
                userinput = Regex.Replace(userinput, "[^A-Za-z0-9$*()=' ]", "");
                //Response.Write(userinput);

                if (userinput.Contains("delete") || userinput.Contains("insert") || userinput.Contains("update") || userinput.Contains("drop") || userinput.Contains("truncate"))
                {
                    Response.End();
                }
                else
                {
                    SqlDataSource1.SelectCommand = userinput;
                    GridView1.DataBind();

                    GridView1.AllowPaging = false;
                    GridView1.DataBind();
                    lblcount.Text = "Record Found : " + GridView1.Rows.Count.ToString();

                    GridView1.AllowPaging = true;
                    GridView1.DataBind();
                }
            }
            catch
            {
                Response.Write(userinput);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }

        protected void btnExtract_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                GridView1.DataBind();


                GridView1.GridLines = GridLines.Both;
                GridView1.HorizontalAlign = HorizontalAlign.Center;
                Response.AddHeader("content-disposition", "attachment;filename=Data.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                GridView1.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }
        }
    }
}