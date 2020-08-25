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

namespace Mphasis_webapp.MappingBackUp
{
    public partial class MapAtmsToUsers : System.Web.UI.Page
    {
        CommonClass obj = new CommonClass();
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);

        public string clean(string a)
        {
            return Regex.Replace(a, "a-zA-Z0-9,", "", RegexOptions.Compiled);
        }

        //public void getatms()
        //{
        //    txt_Add.Text = "";
        //    txt_Role.Text = "";

        //    string a = txt_user.Text = Request.QueryString["user"];

        //    try
        //    {
        //        string str = "SELECT ATMID from UserMap where (status<>'DEL' or serverstatus<>'DEL') and userid like @user";

        //        SqlCommand cmd = new SqlCommand(str, cn);
        //        cmd.Parameters.AddWithValue("@user", a.Trim());
        //        cn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            txt_Role.Text += dr[0].ToString().Trim() + ", ";
        //        }

        //        dr.Close();
        //    }
        //    catch (Exception ee)
        //    {
        //        Response.Write(ee.Message);
        //    }
        //    finally
        //    {
        //        cn.Close();
        //    }


        //    //SqlDataReader dr = null;
        //    //dr=cmd.ExecuteReader();
        //    //string str = "SELECT ATMID from UserMap where (status<>'DEL' or serverstatus<>'DEL') and userid like @user";
        //    //SqlCommand cmd = new SqlCommand(str, cn);
        //    //cmd.Parameters.AddWithValue("user", a.Trim());
        //    //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //DataSet ds = new DataSet();
        //    //da.Fill(ds, "ATMS");
        //    //foreach (DataRow dr in ds.Tables[0].Rows)
        //    //{
        //    //    txt_Role.Text += dr[0].ToString().Trim() + ", ";
        //    //}

        //    txt_Role.Text = txt_Role.Text.TrimEnd(' ').TrimEnd(',');
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            {

                //txt_user.Text = Request.QueryString["user"];

                fte6.InvalidChars = @"`~!@#$%^&*()_+{}:<>?[]-=\;'./""";
                fte7.InvalidChars = @"`~!@#$%^&*()_+{}:<>?[]-=\;'./""";

                if (!IsPostBack)
                {
                    //getatms();
                }
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
            string upd = "";
            string str = "";

            if ((!String.IsNullOrEmpty(txt_Add.Text.Trim())) && (!String.IsNullOrEmpty(txt_Delete.Text.Trim())))
            {
                //if (txt_Add.Text.Contains(","))
                //{
                foreach (var y in txt_Delete.Text.Split(','))
                {
                    user = clean(y.ToString().Trim());
                    a = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                        IF EXISTS (SELECT userid FROM Users where userid like '" + user + "' and (status<>'DEL'))" +
                        "Begin UPDATE users set datastatus='MOD' where userid like '" + user + "'  END COMMIT TRANSACTION";
                    //Response.Write(a);
                    if (obj.NonExecuteQuery(a) != -1)
                    {
                        foreach (var x in txt_Add.Text.Split(','))
                        {
                            atm = clean(x.ToString().Trim());
                            str = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                                IF EXISTS (SELECT atmid FROM atms where atmid like '" + atm + "')  BEGIN IF EXISTS (SELECT userid FROM usermap where atmid like '" + atm + "' and userid like '" + user +
                                "') BEGIN UPDATE usermap set status='MOD',serverstatus='MOD' where userid like '" + user + "' and atmid like '" + atm +
                                "' END ELSE BEGIN Insert into Usermap (userid,atmid,status,serverstatus) values ('" + user + "','" + atm + "','CRE','CRE');UPDATE atms set atmstatus='Active' where atmid like '" + atm + "' END END COMMIT TRANSACTION";
                            //Response.Write(str);
                            if (obj.NonExecuteQuery(str) != -1)
                            {

                            }
                            else
                            {
                                lbl_atmnotFound.Text += x.ToString().Trim() + ",";
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

            #region extra
            //        if (!String.IsNullOrEmpty(txt_Delete.Text.Trim()))
            //        {
            //            if (txt_Delete.Text.Contains(","))
            //            {
            //                foreach (var x in txt_Delete.Text.Split(','))
            //                {
            //                    atm = clean(x.ToString().Trim());
            //                    str = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
            //                                IF EXISTS (SELECT atmid FROM atms where atmid like '" + atm + "') BEGIN IF EXISTS (SELECT userid FROM usermap where atmid like '" + atm + "' and userid like '" + a +
            //                                    "') BEGIN UPDATE usermap set status='DEL',serverstatus='DEL' where userid like '" + a + "' and atmid like '" + atm +
            //                                    "' END END COMMIT TRANSACTION";

            //                    if (obj.NonExecuteQuery(str) == -1)
            //                    {
            //                        lbl_atmnotFound1.Text += x.ToString().Trim() + ",";
            //                    }
            //                }
            //            }

            //            else
            //            {
            //                atm = clean(txt_Delete.Text.Trim());
            //                str = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
            //                                IF EXISTS (SELECT atmid FROM atms where atmid like '" + atm + "') BEGIN IF EXISTS (SELECT userid FROM usermap where atmid like '" + atm + "' and userid like '" + a +
            //                                    "') BEGIN UPDATE usermap set status='DEL',serverstatus='DEL' where userid like '" + a + "' and atmid like '" + atm +
            //                                    "' END END COMMIT TRANSACTION";


            //                if (obj.NonExecuteQuery(str) == -1)
            //                {
            //                    lbl_atmnotFound1.Text = txt_Delete.Text.Trim();
            //                }
            //            }

            //            lbl_atmnotFound1.Text = lbl_atmnotFound1.Text.TrimEnd(',');

            //            if (lbl_atmnotFound1.Text.Trim() != "")
            //            {
            //                lbl_atmnotFound1.Text = "INVALID ATMS(DELETION) : " + lbl_atmnotFound1.Text;
            //                upd = "Update Users set status='MOD' where userid like '" + a.Trim() + "'";
            //                obj.NonExecuteQuery(upd);
            //            }
            //            else
            //            {
            //                upd = "Update Users set status='MOD' where userid like '" + a.Trim() + "'";
            //                obj.NonExecuteQuery(upd);
            //            }

            //            //Response.Write("<script>alert('ATMID is removed..')</script>"); 
            //        }

            #endregion

            txt_Add.Text = "";
            txt_Delete.Text = "";

            // Response.Redirect("MAPATMS.aspx?user=" + txt_user.Text);
            //getatms();
        }

        protected void btn_Submit0_Click(object sender, EventArgs e)
        {

        }
    }
}