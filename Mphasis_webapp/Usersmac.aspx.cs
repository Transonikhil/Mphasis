using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace Mphasis_webapp
{
    public partial class Usersmac : System.Web.UI.Page
    {
        CommonClass obj = new CommonClass();
        ibuckethead bucket = new ibuckethead();
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                Label1.ForeColor = Color.Green;
                Label1.Text = bucket.CountRows(GridView1, Label1);
            }
            else
            {
                Label1.ForeColor = Color.Red;
                Label1.Text = "No records found pertaining to your search. Please select other search criteria.";
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RESET")
            {
                string insert1 = @"insert into MacResetLog ([userid],[MacAddress],[Updatedby]) SELECT [USERID],[MacAddress],'" + Session["sess_userid"] + "'  FROM users  where userid='" + hfdeletevid.Value + "'";
                obj.NonExecuteQuery(insert1);

                string Update = @"Update users set MacAddress = null,DeviceID=null,IMEI=null where userid = ('" + hfdeletevid.Value + "')";
                obj.NonExecuteQuery(Update);
                Response.Redirect("Usersmac.aspx");
            }
        }

    }
}