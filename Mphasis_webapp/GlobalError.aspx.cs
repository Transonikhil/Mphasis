using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class GlobalError : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //HttpContext ctx = HttpContext.Current;
                try
                {
                    HttpContext.Current.Session["sess_userid"].ToString();

                    head.Text = "Something Went Wrong";
                    Exception err = Server.GetLastError();

                    if (err != null)
                    {
                        if (err.InnerException != null)
                        {
                            lbl_err.Text = err.InnerException.Source + "-" + err.InnerException.Message;
                            bucket.X(lbl_err.Text + "-" + Request.Url.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    //              Response.Write(ex.Message);
                    head.Text = "Session Expired";
                    Exception err = Server.GetLastError();
                    // lbl_err.Text = err.InnerException.Source + "-" + err.InnerException.Message;

                    if (err != null)
                    {
                        if (err.InnerException != null)
                        {
                            lbl_err.Text = err.InnerException.Source + "-" + err.InnerException.Message;
                            bucket.X(lbl_err.Text + "-" + Request.Url.ToString());
                        }
                    }
                }
            }
        }

        protected void lb_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();

            Response.Redirect("Login.aspx", false);
        }

    }
}