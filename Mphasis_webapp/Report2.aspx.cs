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
    public partial class Report2 : System.Web.UI.Page
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
            ws.Column(1).Width = 10;
            ws.Column(2).Width = 50;
            try
            {

                a = cnCommand.CommandText = @"SELECT Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,
               a.atmid,a.bankid,a.location,a.city,vdate,userid,vid
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
                    string q11 = reader[10].ToString().Trim();
                    string q12 = reader[11].ToString().Trim();
                    string q13 = reader[12].ToString().Trim();
                    string q14 = reader[13].ToString().Trim();
                    string q15 = reader[14].ToString().Trim();
                    string q16 = reader[15].ToString().Trim();
                    string q17 = reader[16].ToString().Trim();
                    string q18 = reader[17].ToString().Trim();
                    string q19 = reader[18].ToString().Trim();
                    string q20 = reader[19].ToString().Trim();
                    string q21 = reader[20].ToString().Trim();
                    string q22 = reader[21].ToString().Trim();
                    string q23 = reader[22].ToString().Trim();
                    string q24 = reader[23].ToString().Trim();
                    string q25 = reader[24].ToString().Trim();
                    string q26 = reader[25].ToString().Trim();
                    string q27 = reader[26].ToString().Trim();
                    string q28 = reader[27].ToString().Trim();
                    string q29 = reader[28].ToString().Trim();
                    string q30 = reader[29].ToString().Trim();
                    string q31 = reader[30].ToString().Trim();
                    string q32 = reader[31].ToString().Trim();
                    string q33 = reader[32].ToString().Trim();
                    string q34 = reader[33].ToString().Trim();
                    string q35 = reader[34].ToString().Trim();
                    string q36 = reader[35].ToString().Trim();
                    string q37 = reader[36].ToString().Trim();
                    string q38 = reader[37].ToString().Trim();
                    string q39 = reader[38].ToString().Trim();
                    string q40 = reader[39].ToString().Trim();
                    //string q41 = reader[40].ToString().Trim();
                    //string q42 = reader[41].ToString().Trim();


                    obj.changeformat(ws, f, "", 6, 3, 6, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 7, 3, 7, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 8, 3, 8, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 9, 3, 9, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 10, 3, 10, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "", 11, 3, 11, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "Bank Name : " + reader["bankid"].ToString(), 5, 1, 5, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);


                    obj.changeformat(ws, f, "ATM ID : " + reader["atmid"].ToString(), 6, 1, 6, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "Location : " + reader["location"].ToString(), 7, 1, 7, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "City : " + reader["city"].ToString(), 8, 1, 8, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "VID : " + reader["vid"].ToString(), 9, 1, 9, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "Auditor Name : " + reader["USERID"].ToString(), 10, 1, 10, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
                    obj.changeformat(ws, f, "Date & Time of Audit : " + reader["vdate"].ToString(), 11, 1, 11, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

                    //obj.changeformat(ws, f, reader[47].ToString().Trim(), 55, 3, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

                    checkyesno(ws, q1, 13, 3);
                    checkyesno(ws, q2, 14, 3);
                    checkyesno(ws, q3, 15, 3);
                    checkyesno(ws, q4, 16, 3);
                    checkyesno(ws, q5, 17, 3);
                    checkyesno(ws, q6, 18, 3);
                    checkyesno(ws, q7, 19, 3);
                    checkyesno(ws, q8, 20, 3);
                    checkyesno(ws, q9, 21, 3);
                    checkyesno(ws, q10, 22, 3);
                    checkyesno(ws, q11, 23, 3);
                    checkyesno(ws, q12, 24, 3);
                    checkyesno(ws, q13, 25, 3);
                    checkyesno(ws, q14, 26, 3);
                    checkyesno(ws, q15, 27, 3);
                    checkyesno(ws, q16, 28, 3);
                    checkyesno(ws, q17, 29, 3);
                    checkyesno(ws, q18, 30, 3);
                    checkyesno(ws, q19, 31, 3);
                    checkyesno(ws, q20, 32, 3);
                    checkyesno(ws, q21, 33, 3);
                    checkyesno(ws, q22, 34, 3);
                    checkyesno(ws, q23, 35, 3);
                    checkyesno(ws, q24, 36, 3);
                    checkyesno(ws, q25, 37, 3);
                    checkyesno(ws, q26, 38, 3);
                    checkyesno(ws, q27, 39, 3);
                    checkyesno(ws, q28, 40, 3);
                    checkyesno(ws, q29, 41, 3);
                    checkyesno(ws, q30, 42, 3);
                    checkyesno(ws, q31, 43, 3);
                    checkyesno(ws, q32, 44, 3);
                    checkyesno(ws, q33, 45, 3);
                    checkyesno(ws, q34, 46, 3);
                    checkyesno(ws, q35, 47, 3);
                    checkyesno(ws, q36, 48, 3);
                    checkyesno(ws, q37, 49, 3);
                    checkyesno(ws, q38, 50, 3);
                    checkyesno(ws, q39, 51, 3);
                    checkyesno(ws, q40, 52, 3);
                    //checkyesno(ws, q41, 53, 3);
                    // checkyesno(ws, q42, 54, 3);


                    ws.Row(55).Height = 90;
                    //ws.Row(56).Height = 40.50;


                }
                ws.Column(3).Width = 36;
                ws.Column(5).Width = 20;
                ws.Column(7).Width = 20;
                ws.Column(9).Width = 20;
                ws.Column(11).Width = 20;

                reader.Close();
                cn.Close();

                try
                {
                    AddImage(ws, 55, 3, 55, 3, 150, 120, getimage(auditid, ""), "agscbr1", 935, 90);

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }


                //for (int i = 4; i <= 21; i = i + 5)
                //{
                //    for (int j = 5; j <= 21; j = j + 2)
                //    {

                //    }
                //}

                //AddImage(ws, 4, 5, 9, 5, 140, 114, getimage(auditid, "0"), "m1", 935, 90);

                //AddImage(ws, 4, 5, 9, 5, 140, 114, getimage(auditid, "1"), "m1", 935, 90);
                //AddImage(ws, 4, 7, 9, 7, 140, 114, getimage(auditid, "2"), "m2", 935, 90);

                try
                {
                    AddImage(ws, 4, 5, 9, 5, 140, 114, getimage(auditid, "1"), "m1", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 4, 7, 9, 7, 140, 114, getimage(auditid, "2"), "m2", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 4, 9, 9, 9, 140, 114, getimage(auditid, "3"), "m3", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 4, 11, 9, 11, 140, 114, getimage(auditid, "4"), "m4", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 11, 5, 16, 5, 140, 114, getimage(auditid, "5"), "m5", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 11, 7, 16, 7, 140, 114, getimage(auditid, "6"), "m6", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 11, 9, 16, 9, 140, 114, getimage(auditid, "7"), "m7", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 11, 11, 16, 11, 140, 114, getimage(auditid, "8"), "m8", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 5, 23, 5, 140, 114, getimage(auditid, "9"), "m9", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 7, 23, 7, 140, 114, getimage(auditid, "10"), "m10", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 9, 23, 9, 140, 114, getimage(auditid, "11"), "m11", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 11, 23, 11, 140, 114, getimage(auditid, "12"), "m12", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 5, 30, 5, 140, 114, getimage(auditid, "13"), "m13", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 7, 30, 7, 140, 114, getimage(auditid, "14"), "m14", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 9, 30, 9, 140, 114, getimage(auditid, "15"), "m15", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 11, 30, 11, 140, 114, getimage(auditid, "16"), "m16", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 32, 5, 37, 5, 140, 114, getimage(auditid, "17"), "m17", 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                //AddImage(ws, 32, 7, 37, 7, 140, 114, getimage(auditid, "17"), "m18", 935, 90);
                //AddImage(ws, 32, 9, 37, 9, 140, 114, getimage(auditid, "18"), "m19", 935, 90);
                //AddImage(ws, 32, 11, 37, 11, 140, 114, getimage(auditid, "19"), "m20", 935, 90);

                //AddImage(ws, 39, 5, 44, 5, 140, 114, getimage(auditid, "20"), "m21", 935, 90);
                //AddImage(ws, 39, 7, 44, 7, 140, 114, getimage(auditid, "21"), "m22", 935, 90);
                //AddImage(ws, 39, 9, 44, 9, 140, 114, getimage(auditid, "22"), "m23", 935, 90);
                //AddImage(ws, 39, 11, 44, 11, 140, 114, getimage(auditid, "23"), "m24", 935, 90);


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
                else if (a.StartsWith("N-"))
                {
                    //ws.Cells[i, j].Value = "No"+a;
                    ws.Cells[i, j].Value = a.Replace("N-", "No-");
                }
                else if (a.StartsWith("Y-"))
                {
                    ws.Cells[i, j].Value = a.Replace("Y-", "Yes-");
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
            obj.changeformat(ws, font, "", 4, 1, 4, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 4, 3, 4, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            //obj.changeformat(ws, font, "Bank Name :", 5, 1, 5, 2, null, Color.FromArgb(218, 238, 243), true, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 5, 3, 5, 3, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "SrNo", 12, 1, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "1", 13, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "2", 14, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 15, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 16, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "3", 17, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 18, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 19, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 20, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 21, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 22, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "4", 23, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "5", 24, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 25, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 26, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 27, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 28, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 29, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "", 30, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 31, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "6", 32, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "7", 33, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 34, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 35, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 36, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "8", 37, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "9", 38, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "10", 39, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "11", 40, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "12", 41, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "13", 42, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "14", 43, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "15", 44, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 45, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 46, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "16", 47, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "17", 48, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "18", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "19", 52, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "20", 54, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Description", 12, 2, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Remarks", 12, 3, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "ATM MACHINE WORKING FINE?", 13, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CARETAKER AVAILABLE?", 14, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. NAME :", 15, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. CONTACT NUMBER :", 16, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CLEANING DONE REGULARLY?", 17, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. How Severe is the upkeep issue?", 18, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Flooring Proper?", 19, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. Dust Bin OK?", 20, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IV. Backroom OK?", 21, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "V. Writing Ledge and Vms Proper?", 22, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "FIRE EXTINGUISHER OK?", 23, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS RNM OK?", 24, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. How Severe is the RnM issue", 25, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "II. Flooring Proper?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Lights OK?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. No. Of CFL Working?", 27, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IV. Glow Sign Proper?", 28, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "V. Door Working Properly?", 29, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VI. Walls Proper?", 30, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VII. Ceiling Proper?", 31, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "DOOR MAT AVAILABLE?", 32, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS AC Installed at Site", 33, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. AC Working Properly?", 34, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. AC Connected with timer?", 35, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. AC connected with meter?", 36, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS AND BATTERIES WORKING?", 37, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CAMERAS AVAILABLE AT SITE?", 38, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollipop is working?", 39, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ANY ISSUE AFFECTNG THE TRANSACTIONS?", 40, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Feedback from Neighboring Shops/ LL", 41, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VSAT Ballasting", 42, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Mandatory Notices", 43, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Electricity Bill Payment", 44, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Any New Bills at Site", 45, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Submeter Reading", 46, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Any Power Theft Noticed", 47, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Multimeter Reading of Earthing", 48, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the Visit along with PM Engineer and CRA", 49, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. If Yes: PM Docket No:", 50, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. If Yes: Is PM Done properly", 51, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Cash Tallied with Admin Balance, Machine Counter and Physical Counting", 52, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Any Other Issues Noticed?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Auditor Selfie", 55, 1, 55, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Sign : ", 56, 1, 56, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



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
                try
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
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();

                }
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

                            folderpath = "C:\\Development\\CommonStorage\\Mphasis\\TransientStorage\\" + year[i] + "\\" + month[j] + "\\";
                            int k = 0;

                            try
                            {
                                if (imgno == "")
                                {
                                    img = folderpath + "S" + VID + ".jpg";
                                }
                                else
                                {
                                    img = folderpath + "Photo" + VID + "_" + imgno + ".jpg";

                                }
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }
                        }
                    }
                }
            }
            return img;
        }
    }

}