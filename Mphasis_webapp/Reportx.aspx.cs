using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

namespace Mphasis_webapp
{
    public partial class Reportx : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        CommonClass1 obj = new CommonClass1();
        string auditid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateReport();
            auditid = Request.QueryString["auditid"];
        }

        private void GenerateReport()
        {
            using (ExcelPackage p = new ExcelPackage())
            {
                try
                {


                    #region Firstsheet
                    ExcelWorksheet ws1 = obj.CreateSheet(p, "Report", 1, true);
                    //Merging cells and create a center heading for our table

                    CreateHeader(ws1);
                    CreateData(ws1);
                    ws1.View.ZoomScale = 100;
                    #endregion


                    //AddImage(ws, 83, 7, 83, 7, 130, 30, getimage(auditid, "2"), "agscbr2", 935, 460);

                    //AddImage(ws1, 82, 7, 82, 7, 130, 30, getimage(auditid, "1"), "agsaudit1", 565, 560);
                    //AddImage(ws1, 83, 7, 83, 7, 130, 30, getimage(auditid, "2"), "agsaudit2", 600, 560);

                    string header = Request.QueryString["auditid"] + ".xlsx";
                    try
                    {
                        byte[] reportData = p.GetAsByteArray();
                        Response.Clear();
                        //Response.AddHeader("content-disposition", "attachment;  filename=" + header);
                        Response.AddHeader("content-disposition", "attachment;  filename=AuditReport.xlsx");
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.BinaryWrite(reportData);
                        Response.End();
                    }

                    catch (Exception ex)
                    {
                        if (!(ex is System.Threading.ThreadAbortException))
                        {
                            //Other error handling code here
                        }
                    }

                    //These lines will open it in Excel
                    //ProcessStartInfo pi = new ProcessStartInfo(file);
                    //Process.Start(pi);

                    p.Dispose();
                }

                catch (Exception ee)
                {
                    lblexception.Text = ee.Message.ToString();
                }

                finally
                {
                    p.Dispose();
                }
            }
        }

        private void CreateData(ExcelWorksheet ws)
        {
            auditid = Request.QueryString["auditid"];
            string excel = "";
            string status = "true";

            int sr = 0;
            string a = "";
            Font f = new Font("Cambria", 11);
            Font f1 = new Font("Cambria", 10);
            SqlCommand cnCommand = cn.CreateCommand();
            SqlDataReader reader = default(SqlDataReader);
            try
            {

                a = cnCommand.CommandText = @"SELECT Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,a.atmid,a.bankid,a.location,vdate,userid
            from DR_CTP d join atms a on a.atmid = d.ATMID where Vid ='" + auditid + "' ";

                cn.Open();
                reader = cnCommand.ExecuteReader();
                // obj.changeformat(ws, f, a, 2, 4, 2, 4);
                while (reader.Read())
                {
                    sr = sr + 1;

                    string q1 = reader[0].ToString().Trim(); string q2 = reader[1].ToString().Trim();
                    string q3 = reader[2].ToString().Trim(); string q4 = reader[3].ToString().Trim();
                    string q5 = reader[4].ToString().Trim();
                    string q6 = reader[5].ToString().Trim(); string q7 = reader[6].ToString().Trim();
                    string q8 = reader[7].ToString().Trim(); string q9 = reader[8].ToString().Trim();
                    string q10 = reader[9].ToString().Trim();
                    string q11 = reader[10].ToString().Trim(); string q12 = reader[11].ToString().Trim();
                    string q13 = reader[12].ToString().Trim(); string q14 = reader[13].ToString().Trim();
                    string q15 = reader[14].ToString().Trim(); string q16 = reader[15].ToString().Trim();
                    string q17 = reader[16].ToString().Trim();

                    obj.changeformat(ws, f, "", 6, 3, 6, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 7, 3, 7, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 8, 3, 8, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 9, 3, 9, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 10, 3, 10, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "Bank Name : " + reader["bankid"].ToString(), 5, 1, 5, 2, null, Color.FromArgb(218, 238, 243), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);


                    obj.changeformat(ws, f, "CIT Agency Name : Modern", 6, 1, 6, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "Route Name/No : ", 7, 1, 7, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "Auditor Name : " + reader["USERID"].ToString(), 8, 1, 8, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "ATM ID : " + reader["atmid"].ToString(), 9, 1, 9, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "Date & Time of Audit : " + reader["vdate"].ToString(), 10, 1, 10, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, reader[21].ToString().Trim(), 30, 3, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

                    checkyesno(ws, q1, 12, 3);
                    checkyesno(ws, q2, 13, 3);
                    checkyesno(ws, q3, 14, 3);
                    checkyesno(ws, q4, 15, 3);
                    checkyesno(ws, q5, 16, 3);
                    checkyesno(ws, q6, 17, 3);
                    checkyesno(ws, q7, 18, 3);
                    checkyesno(ws, q8, 19, 3);
                    checkyesno(ws, q9, 20, 3);
                    checkyesno(ws, q10, 21, 3);
                    checkyesno(ws, q11, 22, 3);
                    checkyesno(ws, q12, 23, 3);
                    checkyesno(ws, q13, 24, 3);
                    checkyesno(ws, q14, 25, 3);
                    checkyesno(ws, q15, 26, 3);
                    checkyesno(ws, q16, 27, 3);
                    checkyesno(ws, q17, 28, 3);


                    ws.Row(30).Height = 25;
                    ws.Row(31).Height = 40.50;


                }
                ws.Column(3).Width = 36;
                ws.Column(5).Width = 17.58;
                reader.Close();
                cn.Close();
                AddImage(ws, 31, 3, 31, 3, 150, 50, getimage(auditid, ""), "agscbr1", 935, 90);

                AddImage(ws, 4, 5, 7, 5, 125, 82, getimage(auditid, "0"), "m1", 935, 90);
                AddImage(ws, 9, 5, 12, 5, 125, 85, getimage(auditid, "1"), "m2", 935, 90);
                AddImage(ws, 14, 5, 18, 5, 125, 95, getimage(auditid, "2"), "m3", 935, 90);
                AddImage(ws, 20, 5, 24, 5, 125, 95, getimage(auditid, "3"), "m4", 935, 90);
                AddImage(ws, 26, 5, 30, 5, 125, 110, getimage(auditid, "4"), "m5", 935, 90);


            }

            catch (Exception ee)
            {
                lblexception.Text = a;
            }

            finally
            {
                if (sr < 1)
                {
                    lblexception.Text = "No data found";
                }
                else
                {
                    lblexception.Text = "";
                }
                reader.Close();
                cn.Close();
            }


        }

        private void checkyesno(ExcelWorksheet ws, string a, int i, int j)
        {

            try
            {
                if (a == "NA")
                {
                    ws.Cells[i, j].Value = a;
                    // ws.Cells[k, l].Value = a.Replace("NA","");
                }
                else if (a == "No" || a == "NO")
                {
                    ws.Cells[i, j].Value = a;
                    // ws.Cells[k, l].Value = a.Replace("NO", ""); ;
                }
                else if (a == "Y-")
                {
                    ws.Cells[i, j].Value = a.Replace("Y-", "Yes-");
                    // ws.Cells[k, l].Value = a.Replace("NO", ""); ;
                }
                else if (a == "Y")
                {
                    ws.Cells[i, j].Value = a.Replace("Y", "Yes");
                    // ws.Cells[k, l].Value = a.Replace("NO", ""); ;
                }
                else if (a.Substring(0, 3).ToString() == "NA-")
                {
                    ws.Cells[i, j].Value = "NA";

                }
                else if (a.Substring(0, 2).ToString() == "Y-")
                {
                    ws.Cells[i, j].Value = a.Replace("Y-", "Yes-");
                    // ws.Cells[k, l].Value = a.Replace("NO", ""); ;
                }
                else if (a.Substring(0, 3).ToString() == "No-")
                {
                    // string v = a.Substring(0, 3);
                    ws.Cells[i, j].Value = "No";
                    // ws.Cells[k, l].Value = a.Replace("No-", "");
                }
                else if (a.Substring(0, 3).ToString() == "NO-")
                {
                    ws.Cells[i, j].Value = "No";
                }
                else if (a.Substring(0, 3).ToString() == "Yes")
                {
                    ws.Cells[i, j].Value = "Yes";
                    // ws.Cells[k, l].Value = a.Replace("Yes", "");
                }
                else if (a.Substring(0, 3).ToString() == "YES")
                {
                    ws.Cells[i, j].Value = "Yes";
                }
                else if (a.Substring(0, 3).ToString() == "E|N")
                {
                    ws.Cells[i, j].Value = a.Replace("E|N", "No-");
                }
                else if (a.Substring(0, 3).ToString() == "E|Y")
                {
                    ws.Cells[i, j].Value = a.Replace("E|Y", "Yes-");
                }
                else
                {
                    ws.Cells[i, j].Value = a;
                }
            }
            catch (Exception ee)
            {
                ws.Cells[i, j].Value = a;
            }



            ws.Cells[i, j].Style.Border.Left.Style = ws.Cells[i, j].Style.Border.Bottom.Style = ws.Cells[i, j].Style.Border.Right.Style = ws.Cells[i, j].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[i, j].Style.Font.SetFromFont(new Font("Cambria", 11));
            ws.Cells[i, j].Style.WrapText = true;
            //ws.Cells[i, j, k, l].Merge = true;

            ws.Cells[i, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[i, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }


        private void CreateHeader(ExcelWorksheet ws)
        {


            ws.Row(1).Height = 16.50;
            ws.Row(2).Height = 16.50;
            //ws.Cells[2, 2, 61, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            //ws.Cells[2, 4, 61, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            //ws.Cells[2, 2, 2, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            //ws.Cells[61, 2, 61, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // string path = Server.MapPath("~/Image/agsprocess.png");
            // AddImage(ws, 2, 1, 2, 1, 51, 23, path, "ags", 23, 0);

            ws.Cells.Style.Font.Name = "Cambria";
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Bold = true;
            Font font = new Font("Cambria", 11);
            Font font1 = new Font("Cambria", 10);
            Font header = new Font("Cambria", 16);

            //fore,back,bold,merge,italic,underline,rotate
            obj.changeformat(ws, header, "Audit Report", 2, 1, 3, 3, null, null, true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Medium);
            ws.Cells[2, 2, 2, 6].Style.Font.Name = "MS UI Gothic";
            ws.Cells[2, 2, 2, 6].Style.Font.Size = 16;
            //  ws.Cells[2, 2, 2, 6].Style.Font.Bold = true;
            obj.changeformat(ws, font, "Name of The Auditing Firm - Modern", 4, 1, 4, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 4, 3, 4, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            //obj.changeformat(ws, font, "Bank Name :", 5, 1, 5, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 5, 3, 5, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "CIT Agency Name", 6, 1, 6, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Route Name/No-", 7, 1, 7, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Auditor Name", 8, 1, 8, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ATM ID", 9, 1, 9, 2, null, Color.FromArgb(218, 238, 243), true, false, true, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Date & Time of Audit", 10, 1, 10, 2, null, Color.FromArgb(218, 238, 243), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "SrNo", 11, 1, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "1", 12, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "2", 13, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "3", 14, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "4", 15, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "5", 16, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "6", 17, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "7", 18, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "8", 19, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "9", 20, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "10", 21, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "11", 22, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "12", 23, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "13", 24, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "14", 25, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "15", 26, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "16", 27, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "17", 28, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Description", 11, 2, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Remarks", 11, 3, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "HOUSEKEEPING", 12, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "AC WORKING", 13, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ATM WORKING ", 14, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "SIGNAGE WORKING", 15, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "EXTINGUISHER. AVAILABLE", 16, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "BATTERIES", 17, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "U.P.S", 18, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "SKIMMING DEVICE FOUND", 19, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CDB AVAILABLE", 20, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "BACKROOM LIGHT", 21, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "LOBBY LIGHT", 22, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "DUSTBIN", 23, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "DOOR MAT", 24, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Main Door", 25, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Backroom Door", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Lollypop", 27, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Any Incident", 28, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Auditor Name", 30, 1, 30, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Sign : ", 31, 1, 31, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);


            ws.Column(1).Width = 7.57;
            ws.Column(2).Width = 65.43;
            ws.Column(3).Width = 18.19;
            ws.Column(4).Width = 2.21;
        }

        private static void AddImage(ExcelWorksheet ws, int frmrowIndex, int frmcolumnIndex, int torowIndex, int tocolumnIndex, int setwidth, int setheight, string filePath, string imgname, int postop, int posleft)
        {
            //How to Add a Image using EPPlus
            Bitmap image = new Bitmap(filePath);
            ExcelPicture picture = null;
            if (image != null)
            {
                picture = ws.Drawings.AddPicture(imgname, image);
                picture.From.Row = frmrowIndex - 1;
                picture.To.Row = torowIndex;
                picture.From.Column = frmcolumnIndex - 1;
                picture.To.Column = tocolumnIndex;
                ws.Cells[frmrowIndex, frmcolumnIndex, torowIndex, tocolumnIndex].Merge = true;
                ws.Cells[frmrowIndex, frmcolumnIndex, torowIndex, tocolumnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[frmrowIndex, frmcolumnIndex, torowIndex, tocolumnIndex].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                picture.From.ColumnOff = Pixel2MTU(2); //Two pixel space for better alignment
                picture.From.RowOff = Pixel2MTU(2);//Two pixel space for better alignment
                picture.To.ColumnOff = Pixel2MTU(2); //Two pixel space for better alignment
                picture.To.RowOff = Pixel2MTU(2);//Two pixel space for better alignment
                picture.SetSize(setwidth, setheight);
                ws.Cells[frmrowIndex, frmcolumnIndex, torowIndex, tocolumnIndex].Style.Border.Left.Style = ws.Cells[frmrowIndex, frmcolumnIndex, torowIndex, tocolumnIndex].Style.Border.Bottom.Style = ws.Cells[frmrowIndex, frmcolumnIndex, torowIndex, tocolumnIndex].Style.Border.Right.Style = ws.Cells[frmrowIndex, frmcolumnIndex, torowIndex, tocolumnIndex].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                //picture.SetPosition(postop, posleft);
            }
        }

        public static int Pixel2MTU(int pixels)
        {
            int mtus = pixels * 9525;
            return mtus;
        }

        public string getimage(string VID, string imgno)
        {
            string img = "";
            string[] year = { "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025" };
            string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            for (int i = 0; i < year.Length; i++)
            {
                if (VID.Contains(year[i]))
                {
                    for (int j = 0; j < month.Length; j++)
                    {
                        if (VID.Contains(month[j]))
                        {
                            string folderpath = null;
                            // folderpath = "/TransientStorage/" + year[i] + "/" + month[j] + "/";
                            folderpath = @"C:\\Development\\CommonStorage\\Modern\\TransientStorage\\" + year[i] + "\\" + month[j] + "\\";
                            int k = 0;

                            try
                            {
                                if (imgno == "")
                                {
                                    img = folderpath + "SIGN_" + VID + ".png";
                                }
                                else
                                {
                                    img = folderpath + "Photo" + VID + "_" + imgno + ".jpg";
                                }
                            }
                            catch (Exception ex)
                            {
                                //mergecells(ws, ex.Message.ToString(), 82, 7, 82, 7);
                            }
                        }
                    }
                }
            }
            return img;
        }
    }
}