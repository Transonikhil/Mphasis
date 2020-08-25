using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Mphasis_webapp.Excel
{
    public partial class MapAtmsToUsers : System.Web.UI.Page
    {
        ibuckethead2 bucket = new ibuckethead2();
        CommonClass obj = new CommonClass();
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            //txt_user.Text = Request.QueryString["user"];

            fte6.InvalidChars = @"`~!@#$%^&()+{}:<>?[]-=\;'./""";
            fte7.InvalidChars = @"`~!@#$%^&()+{}:<>?[]-=\;'./""";

            if (!IsPostBack)
            {
                //getatms();
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            Label1.Visible = true;
            lbl_atmnotFound.Text = "";
            lbl_atmnotFound1.Text = "";

            string a = "";
            string user = "";
            string atm = "";
            string str = "";

            if ((!String.IsNullOrEmpty(txt_Add.Text.Trim())) && (!String.IsNullOrEmpty(txt_Delete.Text.Trim())))
            {
                foreach (var y in txt_Delete.Text.Split(','))
                {
                    user = y.ToString().Trim();
                    a = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION; IF EXISTS (SELECT userid FROM Users where userid like '" + user +
                        "' and (status<>'DEL' or datastatus<>'DEL')) Begin UPDATE users set status='MOD',datastatus='MOD' where userid like '" + user + "'  END COMMIT TRANSACTION";
                    // Response.Write(a);
                    if (obj.NonExecuteQuery(a) != -1)
                    {
                        foreach (var x in txt_Add.Text.Split(','))
                        {
                            atm = x.ToString().Trim();
                            str = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION; IF EXISTS (SELECT atmid FROM atms where atmid like '" + atm +
                                "' and atmstatus='Active' )  BEGIN IF EXISTS (SELECT userid FROM usermap where atmid like '" + atm + "' and userid like '" + user + "') BEGIN UPDATE usermap set status='MOD',serverstatus='MOD' where userid like '" + user +
                                "' and atmid like '" + atm + "' END ELSE BEGIN Insert into Usermap (userid,atmid,status,serverstatus) values ('" + user + "','" + atm +
                                "','CRE','CRE')  END END COMMIT TRANSACTION";
                            //Response.Write(str);
                            if (obj.NonExecuteQuery(str) == -1)
                            {
                                lbl_atmnotFound.Text += x.ToString().Trim() + ",";
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        lbl_atmnotFound1.Text += y.ToString().Trim() + ",";
                    }
                }
                lbl_atmnotFound.Text = lbl_atmnotFound.Text.TrimEnd(',');
                lbl_atmnotFound1.Text = lbl_atmnotFound1.Text.TrimEnd(',');

                if (lbl_atmnotFound.Text.Trim() != "")
                {
                    lbl_atmnotFound.Text = "INVALID ATMS(ADDITION) : " + lbl_atmnotFound.Text;
                }

                if (lbl_atmnotFound1.Text.Trim() != "")
                {
                    lbl_atmnotFound1.Text = "INVALID USER(S) : " + lbl_atmnotFound1.Text;
                }
            }
            txt_Add.Text = "";
            txt_Delete.Text = "";
        }
        protected void btn_Submit0_Click(object sender, EventArgs e)
        {

        }
    }
}