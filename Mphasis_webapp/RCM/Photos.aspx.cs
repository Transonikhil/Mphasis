using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSharpCode.SharpZipLib.Zip;
using System.Data.SqlClient;
using System.Configuration;

namespace Mphasis_webapp.RCM
{
    public partial class Photos : System.Web.UI.Page
    {
        string virtualpath = ConfigurationManager.AppSettings["virtualpath"];
        string physicalpath = ConfigurationManager.AppSettings["physicalpath"];

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            string pixd = "";
            string atm = "";
            string vdate = "";
            string bankname = "";
            string file_name = "";
            string strAuditId = Request.QueryString["auditid"].Trim();

            SqlCommand cnCommand = cn.CreateCommand();
            SqlDataReader reader = default(SqlDataReader);
            try
            {
                cnCommand.CommandText = @"SELECT pix,a.atmid,vdate,bankid from DR_CTP d inner join ATMS a on d.atmid=a.atmid where vid='" + strAuditId.Trim() + "'";
                cn.Open();
                reader = cnCommand.ExecuteReader();
                while (reader.Read())
                {
                    pixd = reader[0].ToString().Trim();
                    atm = reader[1].ToString().Trim();
                    vdate = reader[2].ToString().Trim();
                    bankname = reader[3].ToString().Trim();
                }
                reader.Close();
            }

            catch (Exception ee)
            {

            }

            finally
            {
                reader.Close();
                cn.Close();
            }

            string foldername = bankname + "-" + atm + " " + vdate.Replace("/", "_");

            Response.AddHeader("Content-Disposition", "attachment; filename=" + foldername + ".zip");
            Response.ContentType = "application/zip";
            int i = 0;
            using (var zipStream = new ZipOutputStream(Response.OutputStream))
            {
                while (i < (Convert.ToInt32(pixd) - 1))
                {
                    try
                    {

                        string[] year = {
    "2014",
    "2015",
    "2016",
    "2017",
    "2018",
    "2019",
    "2020",
    "2021",
    "2022",
    "2023",
    "2024",
    "2025"
};
                        string[] month = {
    "Jan",
    "Feb",
    "Mar",
    "Apr",
    "May",
    "Jun",
    "Jul",
    "Aug",
    "Sep",
    "Oct",
    "Nov",
    "Dec"
};

                        string[] monthcount = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };


                        for (int k = 0; k <= year.Length - 1; k++)
                        {
                            if (strAuditId.Contains(year[k]))
                            {
                                for (int j = 0; j <= month.Length - 1; j++)
                                {
                                    if (strAuditId.Contains(month[j]))
                                    {
                                        file_name = "" + physicalpath + "" + year[k] + "\\" + month[j] + "\\";


                                    }
                                    else if (strAuditId.Contains(monthcount[j] + "_" + year[i]))
                                    {
                                        file_name = "" + physicalpath + "" + year[k] + "\\Others\\";
                                    }
                                }
                            }
                        }



                        string filename = "Photo" + strAuditId + "_" + i.ToString() + ".jpg";

                        string localPath = new Uri(Path.Combine(file_name, filename)).LocalPath;


                        //Response.Write(localPath);
                        byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(file_name, filename));

                        var fileEntry = new ZipEntry(Path.GetFileName(localPath))
                        {
                            Size = fileBytes.Length
                        };



                        zipStream.PutNextEntry(fileEntry);
                        zipStream.Write(fileBytes, 0, fileBytes.Length);




                    }
                    catch { }
                    i++;
                }

                try
                {
                    string filename = "S" + strAuditId + ".jpg";

                    string localPath = new Uri(Path.Combine(file_name, filename)).LocalPath;


                    //Response.Write(localPath);
                    byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(file_name, filename));

                    var fileEntry = new ZipEntry(Path.GetFileName(localPath))
                    {
                        Size = fileBytes.Length
                    };



                    zipStream.PutNextEntry(fileEntry);
                    zipStream.Write(fileBytes, 0, fileBytes.Length);

                }
                catch (Exception ex)
                {

                }

                try
                {
                    string VID = strAuditId;
                    string[] year = { "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025" };
                    string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                    string[] monthcount = { "01_2014", "02_2014", "03_2014", "04_2014", "05_2014", "06_2014", "07_2014", "08_2014", "09_2014", "10_2014", "11_2014", "12_2014" };

                    for (int x = 0; x < year.Length; x++)
                    {
                        if (VID.Contains(year[x]))
                        {
                            for (int j = 0; j < month.Length; j++)
                            {
                                if (bucket.returnmonth(VID) == month[j])
                                {
                                    // noofImage = 10;
                                    string folderpath = null;
                                    //folderpath = ""+ virtualpath +"" + year[x] + "/" + month[j] + "/";
                                    string filename = "";
                                    int p = 1;
                                    string path = "";
                                    try
                                    {
                                        while (p < 70)
                                        {
                                            //Response.Write(""+ physicalpath +"" + year[x] + "\\" + month[j] + "\\PhotoQ" + i + "_" + VID + ".jpg");
                                            if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\PhotoQ" + p + "_" + VID + ".jpg") == true)
                                            {
                                                folderpath = "" + physicalpath + "" + year[x] + "\\" + month[j] + "\\";
                                                filename = "PhotoQ" + p + "_" + VID + ".jpg";

                                                string localPath = new Uri(Path.Combine(folderpath, filename)).LocalPath;


                                                //Response.Write(localPath);
                                                byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(folderpath, filename));

                                                var fileEntry = new ZipEntry(Path.GetFileName(localPath))
                                                {
                                                    Size = fileBytes.Length
                                                };
                                                zipStream.PutNextEntry(fileEntry);
                                                zipStream.Write(fileBytes, 0, fileBytes.Length);
                                            }

                                            p++;
                                        }
                                        zipStream.Flush();
                                        zipStream.Close();
                                    }

                                    catch { }


                                    //  Image1.ImageUrl = folderpath + "SIGN_" + VID + ".png";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                zipStream.Flush();
                zipStream.Close();
            }

            //show_CheckList_images(50, strAuditId);
        }

        //    public void show_CheckList_images(int noofImage, string VID)
        //    {
        //        string[] year = { "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025" };
        //        string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //        string[] monthcount = { "01_2014", "02_2014", "03_2014", "04_2014", "05_2014", "06_2014", "07_2014", "08_2014", "09_2014", "10_2014", "11_2014", "12_2014" };

        //        for (int x = 0; x < year.Length; x++)
        //        {
        //            if (VID.Contains(year[x]))
        //            {
        //                for (int j = 0; j < month.Length; j++)
        //                {
        //                    if (bucket.returnmonth(VID) == month[j])
        //                    {
        //                        // noofImage = 10;
        //                        string folderpath = null;
        //                        // folderpath = ""+ virtualpath +"" + year[x] + "/" + month[j] + "/";
        //                        string filename = "";
        //                        int p = 1;
        //                        string path = "";
        //                        using (var zipStream = new ZipOutputStream(Response.OutputStream))
        //                        {

        //                            try
        //                            {
        //                                while (p < noofImage)
        //                                {
        //                                    //Response.Write(""+ physicalpath +"" + year[x] + "\\" + month[j] + "\\PhotoQ" + i + "_" + VID + ".jpg");
        //                                    if (File.Exists(""+ physicalpath +"" + year[x] + "\\" + month[j] + "\\PhotoQ" + p + "_" + VID + ".jpg") == true)
        //                                    {
        //                                        folderpath = ""+ physicalpath +"" + year[x] + "\\" + month[j] + "\\";
        //                                        filename = "PhotoQ" + p + "_" + VID + ".jpg";

        //                                        string localPath = new Uri(Path.Combine(folderpath, filename)).LocalPath;


        //                                        //Response.Write(localPath);
        //                                        byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(folderpath, filename));

        //                                        var fileEntry = new ZipEntry(Path.GetFileName(localPath))
        //                                        {
        //                                            Size = fileBytes.Length
        //                                        };
        //                                        zipStream.PutNextEntry(fileEntry);
        //                                        zipStream.Write(fileBytes, 0, fileBytes.Length);
        //                                    }

        //                                    p++;
        //                                }
        //                                zipStream.Flush();
        //                                zipStream.Close();
        //                            }

        //                            catch { }

        //                        }
        //                        //  Image1.ImageUrl = folderpath + "SIGN_" + VID + ".png";
        //                    }
        //                }
        //            }
        //        }
        //    }

    }
}