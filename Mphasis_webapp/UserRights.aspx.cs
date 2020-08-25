using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class UserRights : System.Web.UI.Page
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
    }
}