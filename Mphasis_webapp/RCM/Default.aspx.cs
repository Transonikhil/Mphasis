using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RCM
{
    public partial class Default : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            ibuckethead bucket = new ibuckethead();
            bucket.ExecuteQuery("Delete from DR_CTP where atmid='null' and vdate='null'");
            string q1 = "select distinct COUNT(atmid) as 'atmid' from ATMs where atmstatus <> 'Inactive' and RCM like '" + Session["sess_username"] + "'";
            string[] a1 = bucket.verifyReader(q1, "atmid");
            //Response.Write(q1);
            lbl_siteassigned.Text = a1[0];
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("FieldTracker.aspx?Offline=True");
        }

    }
}