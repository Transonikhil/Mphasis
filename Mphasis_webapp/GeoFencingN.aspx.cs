using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class GeoFencingN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            //{
            //    txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            //}


            //txt_frmDate.Attributes.Add("readonly", "readonly");
            //txt_toDate.Attributes.Add("readonly", "readonly");


        }

    }
}