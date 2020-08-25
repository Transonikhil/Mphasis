using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;

namespace Mphasis_webapp.bank
{
    public partial class BulkDownload : System.Web.UI.Page
    {
        CommonClass obj = new CommonClass();
        SqlConnection con = new SqlConnection();

        SqlConnection conx = new SqlConnection();
        SqlConnection conn = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string from = Request.QueryString["from"];
                string to = Request.QueryString["to"];
                string role, state, vids, yr, mon, folderpath;
                //string atmid = Request.QueryString["atmid"];
                string user = Request.QueryString["user"];
                string fname = Session["sess_userid"] + "_" + System.DateTime.Now.ToString("ddMMyyhhmmss");


                //if (Request.QueryString["role"] == "%")
                //{
                //    role = "u.role in ('AO','DE','CM')";
                //}
                //else
                //{
                //    role = "u.role like '" + Request.QueryString["role"] + "'";
                //}

                if (Request.QueryString["state"] == "")
                {
                    state = "a.state like '%'";
                }
                else
                {
                    state = "a.state in (" + Request.QueryString["state"] + ")";
                }

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
                string query = @"select vid,d.atmid from dr_ctp d inner join atms a on d.atmid=a.atmid join users u on u.userid=d.userid where 
                            convert(date,vdate) between  '" + from + "' and '" + to + "' and " + state + " and a.bankid='TAMILNAD MERCANTILE BANK LIMITED' and  d.userid like '" + user + "'";
                // Response.Write(query);
                SqlCommand cmd1 = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    string path = Server.MapPath("~/BulkDownload/" + fname + "/" + dr[1].ToString() + "/" + dr[0].ToString());
                    //System.IO.Directory.CreateDirectory(Server.MapPath("~/BulkDownload/" + fname));

                    if (!Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }

                    vids = dr[0].ToString(); yr = getvalue(vids, 2); mon = getvalue(vids, 1);
                    folderpath = yr + @"\" + mon + @"\";


                    for (int i = 1; i <= 41; i++)
                    {
                        /*Add Files*/

                        try
                        {
                            if (File.Exists("C:\\Development\\CommonStorage\\Mphasis\\TransientStorage\\" + folderpath.Replace(@"\\", @"\\") + "PhotoQ" + i.ToString() + "_" + vids + ".jpg") == true)
                            {
                                File.Copy(@"C:\Development\CommonStorage\Mphasis\TransientStorage\" + folderpath + "PhotoQ" + i.ToString() + "_" + vids + ".jpg",
                                          @"C:\Development\v6_Mphasis\BulkDownload\" + fname + @"\" + dr[1].ToString() + @"\" + vids + @"\" + "PhotoQ" + i.ToString() + "_" + vids + ".jpg");
                            }
                        }
                        catch (Exception er)
                        {
                            //  Response.Write(er.Message.ToString());
                        }

                    }

                    for (int i = 0; i <= 5; i++)
                    {
                        /*Add Files*/

                        try
                        {
                            if (File.Exists("C:\\Development\\CommonStorage\\Mphasis\\TransientStorage\\" + folderpath.Replace(@"\\", @"\\") + "Photo" + vids + "_" + i.ToString() + ".jpg") == true)
                            {
                                File.Copy(@"C:\Development\CommonStorage\Mphasis\TransientStorage\" + folderpath + "Photo" + vids + "_" + i.ToString() + ".jpg",
                                          @"C:\Development\v6_Mphasis\BulkDownload\" + fname + @"\" + dr[1].ToString() + @"\" + vids + @"\" + "Photo" + vids + "_" + i.ToString() + ".jpg");
                            }
                        }
                        catch (Exception er)
                        {
                            //  Response.Write(er.Message.ToString());
                        }

                    }
                }

                conn.Close();
                /*Add Zip*/

                using (ZipFile zip = new ZipFile())
                {
                    try
                    {
                        zip.AddDirectory(@"C:\Development\v6_Mphasis\BulkDownload\" + fname);



                        //user = Session["sess_userid"].ToString() + "_" + System.DateTime.Now.ToString("hhmmss");
                        zip.Save(@"C:\Development\v6_Mphasis\BulkDownload\" + fname + ".zip");

                        /*Delete Folder*/
                        var dir = new DirectoryInfo(@"C:\Development\v6_Mphasis\BulkDownload\" + fname);
                        dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                        dir.Delete(true);

                        System.IO.FileInfo file = new System.IO.FileInfo(@"C:\Development\v6_Mphasis\BulkDownload\" + fname + ".zip");
                        Response.ClearContent();
                        Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + fname + ".zip", file.Name));
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "application/zip";
                        this.EnableViewState = false;
                        Response.TransmitFile(file.FullName);
                        Response.Flush();

                        File.Delete(@"C:\Development\v6_Mphasis\BulkDownload\" + fname + ".zip");

                        Response.End();
                    }


                    catch { }
                }
            }
        }

        public string getvalue(string fname, int i)
        {
            DateTime dt = DateTime.Now;
            try
            {
                return (fname.Split('-').Last()).Split('_')[i];
            }
            catch (Exception e)
            {
                if (i == 0)
                {
                    return dt.ToString("dd");
                }
                else if (i == 1)
                {
                    return dt.ToString("MMM");
                }
                else if (i == 2)
                {
                    return dt.ToString("yyyy");
                }
                else
                {
                    return "01";
                }
            }
        }

    }
}