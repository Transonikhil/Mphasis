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

namespace Mphasis_webapp.CH
{
    public partial class Report : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        CommonClass1 obj = new CommonClass1();
        string auditid = "";
        string atmid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateReport();
            auditid = Request.QueryString["auditid"];
            atmid = Request.QueryString["atmid"];
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
                    string[] ver = obj.verifyReader("select dbo.udf_GetNumeric(REPLACE(version,'_','')) as version from DR_CTP where vid='" + Request.QueryString["auditid"] + "'", "version");

                    if (Convert.ToDecimal(ver[0]) < Convert.ToDecimal("2.4"))
                    {
                        CreateHeader(ws1);
                        CreateData(ws1);
                    }
                    else if (Convert.ToDecimal(ver[0]) >= Convert.ToDecimal("2.4") && Convert.ToDecimal(ver[0]) <= Convert.ToDecimal("2.7"))
                    {
                        CreateHeader1(ws1);
                        CreateData1(ws1);
                    }
                    else if (Convert.ToDecimal(ver[0]) > Convert.ToDecimal("2.7") && Convert.ToDecimal(ver[0]) < Convert.ToDecimal("3.2"))
                    {
                        CreateHeader2(ws1);
                        CreateData2(ws1);
                    }
                    else if (Convert.ToDecimal(ver[0]) >= Convert.ToDecimal("3.2") && Convert.ToDecimal(ver[0]) < Convert.ToDecimal("3.3"))
                    {
                        CreateHeader3(ws1);
                        CreateData3(ws1);
                    }
                    else if (Convert.ToDecimal(ver[0]) >= Convert.ToDecimal("3.3") && Convert.ToDecimal(ver[0]) < Convert.ToDecimal("3.7"))
                    {
                        CreateHeader4(ws1);
                        CreateData4(ws1);
                    }
                    else if (Convert.ToDecimal(ver[0]) >= Convert.ToDecimal("3.7"))
                    {
                        CreateHeader5(ws1);
                        CreateData5(ws1);
                    }
                    ws1.View.ZoomScale = 100;
                    #endregion

                    string header = Request.QueryString["auditid"] + ".xlsx";
                    try
                    {
                        byte[] reportData = p.GetAsByteArray();
                        Response.Clear();
                        Response.AddHeader("content-disposition", "attachment;  filename=" + header);
                        //Response.AddHeader("content-disposition", "attachment;  filename=" + Request.QueryString["atmid"] + ".xlsx");
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

                a = cnCommand.CommandText = @"SELECT Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,
               a.atmid,a.bankid,a.location,a.city,convert(varchar(10),convert(date,vdate),103) as vdate,userid,vid
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
                    string q41 = reader[40].ToString().Trim();
                    string q42 = reader[41].ToString().Trim();


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

                    checkyesno(ws, q41, 23, 3);    //SIGNAGE & LOLLIPOP CLEAN OR NOT

                    checkyesno(ws, q11, 24, 3);
                    checkyesno(ws, q12, 25, 3);
                    checkyesno(ws, q13, 26, 3);
                    checkyesno(ws, q14, 27, 3);
                    checkyesno(ws, q15, 28, 3);
                    checkyesno(ws, q16, 29, 3);
                    checkyesno(ws, q17, 30, 3);
                    checkyesno(ws, q18, 31, 3);
                    checkyesno(ws, q19, 32, 3);
                    checkyesno(ws, q20, 33, 3);
                    checkyesno(ws, q21, 34, 3);
                    checkyesno(ws, q22, 35, 3);
                    checkyesno(ws, q23, 36, 3);
                    checkyesno(ws, q24, 37, 3);
                    checkyesno(ws, q25, 38, 3);
                    checkyesno(ws, q26, 39, 3);
                    checkyesno(ws, q27, 40, 3);
                    checkyesno(ws, q28, 41, 3);
                    checkyesno(ws, q29, 42, 3);
                    checkyesno(ws, q30, 43, 3);
                    checkyesno(ws, q31, 44, 3);
                    checkyesno(ws, q32, 45, 3);
                    checkyesno(ws, q33, 46, 3);
                    checkyesno(ws, q34, 47, 3);
                    checkyesno(ws, q35, 48, 3);
                    checkyesno(ws, q36, 49, 3);
                    checkyesno(ws, q37, 50, 3);
                    checkyesno(ws, q38, 51, 3);
                    checkyesno(ws, q39, 52, 3);
                    checkyesno(ws, q40, 53, 3);
                    //checkyesno(ws, q41, 5, 3);
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

        private void CreateData1(ExcelWorksheet ws)
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

                a = cnCommand.CommandText = @"SELECT Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,
            Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,
            Q55,Q56,Q57,remark,
               a.atmid,a.bankid,a.location,a.city,convert(varchar(10),convert(date,vdate),103) as vdate,userid,vid
            from DR_CTP d join atms a on a.atmid = d.ATMID where Vid ='" + auditid + "' ";

                cn.Open();
                reader = cnCommand.ExecuteReader();
                // obj.changeformat(ws, f, a, 2, 4, 2, 4);
                while (reader.Read())
                {
                    sr = sr + 1;

                    string q1 = reader[0].ToString().Trim();
                    string q2 = reader[1].ToString().Trim();
                    string q3 = reader[2].ToString().Trim();
                    string q4 = reader[3].ToString().Trim();
                    string q5 = reader[4].ToString().Trim();
                    string q6 = reader[5].ToString().Trim();
                    string q7 = reader[6].ToString().Trim();
                    string q8 = reader[7].ToString().Trim();
                    string q9 = reader[8].ToString().Trim();
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
                    string q41 = reader[40].ToString().Trim();
                    string q42 = reader[41].ToString().Trim();
                    string q43 = reader[42].ToString().Trim();
                    string q44 = reader[43].ToString().Trim();
                    string q45 = reader[44].ToString().Trim();
                    string q46 = reader[45].ToString().Trim();
                    string q47 = reader[46].ToString().Trim();
                    string q48 = reader[47].ToString().Trim();
                    string q49 = reader[48].ToString().Trim();
                    string q50 = reader[49].ToString().Trim();
                    string q51 = reader[50].ToString().Trim();
                    string q52 = reader[51].ToString().Trim();
                    string q53 = reader[52].ToString().Trim();
                    string q54 = reader[53].ToString().Trim();
                    string q55 = reader[54].ToString().Trim();
                    string q56 = reader[56].ToString().Trim();
                    string q57 = reader[57].ToString().Trim();
                    string q58 = reader[55].ToString().Trim();


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
                    // checkyesno(ws, q6, 18, 3);
                    checkyesno(ws, q7, 18, 3);
                    checkyesno(ws, q8, 19, 3);
                    checkyesno(ws, q9, 20, 3);
                    checkyesno(ws, q10, 21, 3);

                    checkyesno(ws, q41, 22, 3);    //SIGNAGE & LOLLIPOP CLEAN OR NOT

                    checkyesno(ws, q58, 23, 3);
                    checkyesno(ws, q11, 24, 3);

                    checkyesno(ws, q12, 25, 3);
                    // checkyesno(ws, q13, 25, 3);
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
                    // checkyesno(ws, q36, 49, 3);
                    checkyesno(ws, q37, 48, 3);
                    checkyesno(ws, q38, 49, 3);
                    checkyesno(ws, q39, 50, 3);
                    checkyesno(ws, q40, 51, 3);
                    // checkyesno(ws, q41, 55, 3);
                    checkyesno(ws, q42, 52, 3);
                    //checkyesno(ws, q43, 53, 3);
                    checkyesno(ws, q43, 54, 3);
                    checkyesno(ws, q44, 55, 3);
                    checkyesno(ws, q45, 56, 3);
                    // checkyesno(ws, q47, 57, 3);


                    checkyesno(ws, q46, 58, 3);
                    checkyesno(ws, q47, 59, 3);
                    checkyesno(ws, q48, 60, 3);

                    checkyesno(ws, q49, 61, 3);

                    checkyesno(ws, q50, 62, 3);
                    checkyesno(ws, q51, 63, 3);
                    checkyesno(ws, q52, 64, 3);

                    checkyesno(ws, q53, 65, 3);
                    checkyesno(ws, q54, 66, 3);
                    checkyesno(ws, q55, 67, 3);

                    //remarks
                    checkyesno(ws, q57, 68, 3);

                    ws.Row(69).Height = 90;
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
                    AddImage(ws, 69, 3, 69, 3, 150, 120, getimage(auditid, ""), "agscbr1", 935, 90);

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                int c = 1;
                for (int s = 5; s <= 36; s += 2)
                {
                    try
                    {
                        ws.Column(s).Width = 20;
                        AddImage(ws, 4, s, 9, s, 140, 114, getimage(auditid, c.ToString()), "m" + c, 935, 90);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    c++;
                }

                int rc = 13;
                for (int s = 1; s <= 80; s++)
                {
                    if (s >= 12)
                    {

                        if (s == 13)
                        {
                            if (chkquestimage(auditid, (s + 1).ToString()) != "")
                            {
                                ws.Row(rc).Height = 71;
                            }
                            //rc++;
                        }
                        else
                        {
                            if (chkquestimage(auditid, s.ToString()) != "")
                            {
                                ws.Row(rc).Height = 71;
                            }
                            rc++;
                        }
                    }
                    else if (s != 6 && s != 11 && s != 12)
                    {
                        if (chkquestimage(auditid, s.ToString()) != "")
                            ws.Row(rc).Height = 71;
                        rc++;
                    }
                    else if (s == 11)
                    {
                        if (chkquestimage(auditid, "41") != "")
                            ws.Row(rc).Height = 71;
                        rc += 3;
                    }



                }

                try
                {
                    AddImage(ws, 13, 5, 13, 5, 140, 90, getquestimage(auditid, "1"), "i" + 13, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 14, 5, 14, 5, 140, 90, getquestimage(auditid, "2"), "i" + 14, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 15, 5, 15, 5, 140, 90, getquestimage(auditid, "3"), "i" + 15, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 16, 5, 16, 5, 140, 90, getquestimage(auditid, "4"), "i" + 16, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 17, 5, 17, 5, 140, 90, getquestimage(auditid, "5"), "i" + 17, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 5, 18, 5, 140, 90, getquestimage(auditid, "6"), "i" + 18, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 19, 5, 19, 5, 140, 90, getquestimage(auditid, "8"), "i" + 19, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 20, 5, 20, 5, 140, 90, getquestimage(auditid, "9"), "i" + 20, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 21, 5, 21, 5, 140, 90, getquestimage(auditid, "10"), "i" + 21, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 22, 5, 22, 5, 140, 90, getquestimage(auditid, "41"), "i" + 22, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 23, 5, 22, 5, 140, 90, getquestimage(auditid, "11"), "is" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 5, 25, 5, 140, 90, getquestimage(auditid, "12"), "i" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 26, 5, 26, 5, 140, 90, getquestimage(auditid, "14"), "i" + 24, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 27, 5, 27, 5, 140, 90, getquestimage(auditid, "15"), "i" + 25, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 28, 5, 28, 5, 140, 90, getquestimage(auditid, "16"), "i" + 26, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 29, 5, 29, 5, 140, 90, getquestimage(auditid, "17"), "i" + 27, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                //try
                //{
                //    AddImage(ws, 30, 5, 30, 5, 140, 90, getquestimage(auditid, "18"), "i" + 21, 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}

                rc = 30;
                for (int s = 18; s <= 80; s++)
                {
                    try
                    {
                        if (rc == 48)
                        {
                            //AddImage(ws, 53, 5, 53, 5, 140, 90, getquestimage(auditid, "36"), "imgs" + s, 935, 90);
                        }
                        else if (rc == 53)
                        {
                            AddImage(ws, 53, 5, 53, 5, 140, 90, getquestimage(auditid, "36"), "imgs" + s, 935, 90);
                        }
                        else if (rc != 41)
                        {
                            AddImage(ws, rc, 5, rc, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    rc++;
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

                //try
                //{
                //    AddImage(ws, 4, 5, 9, 5, 140, 114, getimage(auditid, "1"), "m1", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 4, 7, 9, 7, 140, 114, getimage(auditid, "2"), "m2", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 4, 9, 9, 9, 140, 114, getimage(auditid, "3"), "m3", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 4, 11, 9, 11, 140, 114, getimage(auditid, "4"), "m4", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 11, 5, 16, 5, 140, 114, getimage(auditid, "5"), "m5", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 11, 7, 16, 7, 140, 114, getimage(auditid, "6"), "m6", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 11, 9, 16, 9, 140, 114, getimage(auditid, "7"), "m7", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 11, 11, 16, 11, 140, 114, getimage(auditid, "8"), "m8", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 18, 5, 23, 5, 140, 114, getimage(auditid, "9"), "m9", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 18, 7, 23, 7, 140, 114, getimage(auditid, "10"), "m10", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 18, 9, 23, 9, 140, 114, getimage(auditid, "11"), "m11", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 18, 11, 23, 11, 140, 114, getimage(auditid, "12"), "m12", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 25, 5, 30, 5, 140, 114, getimage(auditid, "13"), "m13", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 25, 7, 30, 7, 140, 114, getimage(auditid, "14"), "m14", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 25, 9, 30, 9, 140, 114, getimage(auditid, "15"), "m15", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 25, 11, 30, 11, 140, 114, getimage(auditid, "16"), "m16", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
                //try
                //{
                //    AddImage(ws, 32, 5, 37, 5, 140, 114, getimage(auditid, "17"), "m17", 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}
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
                //else if (a.Substring(0, 3).ToString() == "NA-")
                //{
                //    ws.Cells[i, j].Value = "NA";

                //}
                else if (a == "N")
                {
                    //ws.Cells[i, j].Value = "No"+a;
                    ws.Cells[i, j].Value = a.Replace("N", "No");
                }

                else if (a.Substring(0, 2).ToString() == "Y-")
                {
                    if (i == 46)
                    {
                        ws.Cells[i, j].Value = a.Replace("Y-", "Available-");
                    }
                    else
                    {
                        ws.Cells[i, j].Value = a.Replace("Y-", "Yes-");
                    }// ws.Cells[k, l].Value = a.Replace("NO", ""); ;
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
                    if (i == 46)
                    {
                        ws.Cells[i, j].Value = a.Replace("N-", "Not Available-");
                    }
                    else
                    {
                        ws.Cells[i, j].Value = a.Replace("N-", "No-");
                    }
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
                else if (a.ToString().Equals("E|N"))
                {
                    ws.Cells[i, j].Value = a.Replace("E|N", "No");
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
            obj.changeformat(ws, font, "", 23, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "4", 24, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "5", 25, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 26, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 27, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 28, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 29, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 30, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "", 31, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 32, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "6", 33, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "7", 34, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 35, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 36, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 37, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "8", 38, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "9", 39, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "10", 40, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "11", 41, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 42, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "12", 43, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "13", 44, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "14", 45, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "15", 46, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 47, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 48, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "16", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "17", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 52, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
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
            obj.changeformat(ws, font, "VI. Signage & Lollypop Cleaned or not?", 23, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "FIRE EXTINGUISHER OK?", 24, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS RNM OK?", 25, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. How Severe is the RnM issue", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "II. Flooring Proper?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Lights OK?", 27, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. No. Of CFL Working?", 28, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IV. Glow Sign Proper?", 29, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "V. Door Working Properly?", 30, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VI. Walls Proper?", 31, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VII. Ceiling Proper?", 32, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "DOOR MAT AVAILABLE?", 33, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS AC Installed at Site", 34, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. AC Working Properly?", 35, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. AC Connected with timer?", 36, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. AC connected with meter?", 37, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS AND BATTERIES WORKING?", 38, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CAMERAS AVAILABLE AT SITE?", 39, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollipop is working?", 40, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ANY ISSUE AFFECTNG THE TRANSACTIONS?", 41, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Feedback from Neighboring Shops/ LL", 42, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VSAT Ballasting", 43, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Mandatory Notices", 44, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Electricity Bill Payment", 45, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Any New Bills at Site", 46, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Submeter Reading", 47, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Any Power Theft Noticed", 48, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Multimeter Reading of Earthing", 49, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the Visit along with PM Engineer and CRA", 50, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. If Yes: PM Docket No:", 51, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. If Yes: Is PM Done properly", 52, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. Cash Tallied with Admin Balance, Machine Counter and Physical Counting", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Any Other Issues Noticed?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Auditor Selfie", 55, 1, 55, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Sign : ", 56, 1, 56, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            ws.Column(3).Width = 18.19;
            ws.Column(4).Width = 2.21;
        }

        private void CreateHeader1(ExcelWorksheet ws)
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
            obj.changeformat(ws, font, "4", 18, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "5", 19, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "6", 20, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "7", 21, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "8", 22, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "9", 23, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 24, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "10", 25, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "11", 26, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "12", 27, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "13", 28, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "14", 29, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "15", 30, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "16", 31, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "17", 32, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "18", 33, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 34, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 35, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 36, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "19", 37, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "20", 38, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "21", 39, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "22", 40, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 41, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "23", 42, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "24", 43, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "25", 44, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 45, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 46, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "26", 47, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "27", 48, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "28", 52, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "29", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 54, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "", 55, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 56, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "30", 57, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 58, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 59, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 60, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "31", 61, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "32", 62, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "33", 63, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "34", 64, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "35", 65, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "36", 66, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "37", 67, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "38", 68, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "39", 69, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "37", 67, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "20", 54, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Description", 12, 2, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Remarks", 12, 3, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Atm machine working fine?", 13, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "caretaker available?", 14, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Name :", 15, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Contact Number :", 16, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Cleaning Done Regularly?", 17, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Flooring Proper?", 18, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Dust Bin OK?", 19, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Backroom OK?", 20, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Writing Ledge and Vms Proper?", 21, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollypop Cleaned or not?", 22, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Fire Extinguisher Available ?", 23, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Is The Fire Extinguisher Expired?", 24, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is RNM Ok?", 25, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Lights Ok?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "No. Of CFL Working?", 27, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "II. Flooring Proper?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Glow Sign Proper?", 28, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Working Properly?", 29, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Walls Proper?", 30, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Ceiling Proper?", 31, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Mat Available?", 32, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS AC Installed at Site", 33, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. AC Working Properly?", 34, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. AC Connected with timer?", 35, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. AC connected with meter?", 36, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS AND BATTERIES Working?", 37, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CAMERAS AVAILABLE AT SITE?", 38, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollipop is working?", 39, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ANY ISSUE AFFECTNG THE TRANSACTIONS?", 40, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Feedback from Neighboring Shops/ LL", 41, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VSAT Ballasting", 42, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Mandatory Notices", 43, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Electricity Bill Payment", 44, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Any New Bills at Site", 45, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Submeter Reading", 46, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Any Power Theft Noticed", 47, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the Visit along with PM Engineer and CRA", 48, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. If Yes: PM Docket No:", 49, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. If Yes: Is PM Done properly", 50, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. Cash Tallied with Admin Balance, Machine Counter and Physical Counting", 51, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Raw Power Status: ", 52, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.P N Reading:", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II.P E Reading:", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III.N E Reading:", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS Power (Volt):", 57, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.P N Reading:", 58, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II.P E Reading:", 59, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III.N E Reading:", 60, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Power availability in a day (no of Hrs):", 61, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Frequency of power failure in a day:", 62, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the ODU-IDU Connection done as per requirement?", 63, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other ATMs nearby (range within 500 meters) ?", 64, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Stabilizer available ?", 65, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Isolation available ?", 66, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Monkey cage available ?", 67, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Other Remarks", 68, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Any Other Issues Noticed?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Auditor Selfie", 69, 1, 69, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Sign : ", 56, 1, 56, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            ws.Column(3).Width = 18.19;
            ws.Column(4).Width = 2.21;
        }
        private void CreateHeader2(ExcelWorksheet ws)
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
            obj.changeformat(ws, font, "4", 18, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "5", 19, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "6", 20, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "7", 21, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "8", 22, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "9", 23, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 24, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "10", 25, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "11", 26, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "12", 27, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "13", 28, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "14", 29, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "15", 30, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "16", 31, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "17", 32, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "18", 33, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 34, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 35, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 36, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "19", 37, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "20", 38, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "21", 39, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "22", 40, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 41, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "23", 42, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "24", 43, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "25", 44, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 45, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 46, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "26", 47, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "27", 48, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "28", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "", 52, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "29", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "30", 54, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "31", 55, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "32", 56, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 57, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 58, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 59, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "33", 60, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "34", 61, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "35", 62, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "36", 63, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "37", 64, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "38", 65, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "39", 66, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "40", 67, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "41", 68, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "42", 69, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "43", 70, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "44", 71, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "37", 67, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "20", 54, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Description", 12, 2, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Remarks", 12, 3, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Atm machine working fine?", 13, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "caretaker available?", 14, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Name :", 15, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Contact Number :", 16, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Cleaning Done Regularly?", 17, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Flooring Proper?", 18, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Dust Bin OK?", 19, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Backroom OK?", 20, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Writing Ledge and Vms Proper?", 21, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollypop Cleaned or not?", 22, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Fire Extinguisher Available ?", 23, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Is The Fire Extinguisher Expired?", 24, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is RNM Ok?", 25, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Lights Ok?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "No. Of CFL Working?", 27, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "II. Flooring Proper?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Glow Sign Proper?", 28, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Working Properly?", 29, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Walls Proper?", 30, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Ceiling Proper?", 31, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Mat Available?", 32, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS AC Installed at Site", 33, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. AC Working Properly?", 34, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. AC Connected with timer?", 35, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. AC connected with meter?", 36, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS AND BATTERIES Working?", 37, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CAMERAS AVAILABLE AT SITE?", 38, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollipop is working?", 39, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ANY ISSUE AFFECTNG THE TRANSACTIONS?", 40, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Feedback from Neighboring Shops/ LL", 41, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VSAT Ballasting", 42, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Mandatory Notices", 43, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Electricity Bill Payment", 44, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Any New Bills at Site", 45, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Submeter Reading", 46, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Any Power Theft Noticed", 47, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 48, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin); obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is the Visit along with PM Engineer and CRA", 49, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. If Yes: PM Docket No:", 50, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. If Yes: Is PM Done properly", 51, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. Cash Tallied with Admin Balance, Machine Counter and Physical Counting", 52, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Housekeeping Done On Site?", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Deep Cleaning Done On Site?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Raw Power Status: ", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "I.P N Reading:", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "II.P E Reading:", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "III.N E Reading:", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS Power (Volt):", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.P N Reading:", 57, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II.P E Reading:", 58, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III.N E Reading:", 59, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Power availability in a day (no of Hrs):", 60, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Frequency of power failure in a day:", 61, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the ODU-IDU Connection done as per requirement?", 62, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other ATMs nearby (range within 500 meters) ?", 63, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Stabilizer available ?", 64, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Isolation available ?", 65, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Monkey cage available ?", 66, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is CAM1 Working?", 67, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is CAM2 Working?", 68, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the Image getting stored?", 69, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is EJ getting pulled?", 70, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other Remarks", 71, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Any Other Issues Noticed?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Auditor Selfie", 72, 1, 72, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Sign : ", 56, 1, 56, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            ws.Column(3).Width = 18.19;
            ws.Column(4).Width = 2.21;
        }
        private void CreateData2(ExcelWorksheet ws)
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

                a = cnCommand.CommandText = @"SELECT Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,
            Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,
            Q55,Q56,Q57,remark,Q58,
               a.atmid,a.bankid,a.location,a.city,convert(varchar(10),convert(date,vdate),103) as vdate,userid,vid,ISNULL(Q59,'NA') as Q59,ISNULL(Q60,'NA') as Q60,ISNULL(Q61,'NA') as Q61,ISNULL(Q62,'NA') as Q62
            from DR_CTP d join atms a on a.atmid = d.ATMID where Vid ='" + auditid + "' ";

                cn.Open();
                reader = cnCommand.ExecuteReader();
                // obj.changeformat(ws, f, a, 2, 4, 2, 4);
                while (reader.Read())
                {
                    sr = sr + 1;

                    string q1 = reader[0].ToString().Trim();
                    string q2 = reader[1].ToString().Trim();
                    string q3 = reader[2].ToString().Trim();
                    string q4 = reader[3].ToString().Trim();
                    string q5 = reader[4].ToString().Trim();
                    string q6 = reader[5].ToString().Trim();
                    string q7 = reader[6].ToString().Trim();
                    string q8 = reader[7].ToString().Trim();
                    string q9 = reader[8].ToString().Trim();
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
                    string q41 = reader[40].ToString().Trim();
                    string q42 = reader[41].ToString().Trim();
                    string q43 = reader[42].ToString().Trim();
                    string q44 = reader[43].ToString().Trim();
                    string q45 = reader[44].ToString().Trim();
                    string q46 = reader[45].ToString().Trim();
                    string q47 = reader[46].ToString().Trim();
                    string q48 = reader[47].ToString().Trim();
                    string q49 = reader[48].ToString().Trim();
                    string q50 = reader[49].ToString().Trim();
                    string q51 = reader[50].ToString().Trim();
                    string q52 = reader[51].ToString().Trim();
                    string q53 = reader[52].ToString().Trim();
                    string q54 = reader[53].ToString().Trim();
                    string q55 = reader[54].ToString().Trim();
                    string q56 = reader[55].ToString().Trim();
                    string q57 = reader[56].ToString().Trim();
                    string q58 = reader[57].ToString().Trim();
                    string q59 = reader[58].ToString().Trim();

                    string q60 = reader["Q59"].ToString().Trim();
                    string q61 = reader["Q60"].ToString().Trim();
                    string q62 = reader["Q61"].ToString().Trim();
                    string q63 = reader["Q62"].ToString().Trim();

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
                    // checkyesno(ws, q6, 18, 3);
                    checkyesno(ws, q7, 18, 3);
                    checkyesno(ws, q8, 19, 3);
                    checkyesno(ws, q9, 20, 3);
                    checkyesno(ws, q10, 21, 3);

                    checkyesno(ws, q41, 22, 3);    //SIGNAGE & LOLLIPOP CLEAN OR NOT

                    checkyesno(ws, q56, 23, 3); //fire extin
                    checkyesno(ws, q11, 24, 3);

                    checkyesno(ws, q12, 25, 3);
                    // checkyesno(ws, q13, 25, 3);
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
                    // checkyesno(ws, q36, 49, 3);
                    checkyesno(ws, q36, 48, 3);
                    checkyesno(ws, q37, 49, 3);
                    checkyesno(ws, q38, 50, 3);
                    checkyesno(ws, q39, 51, 3);
                    // checkyesno(ws, q41, 55, 3);
                    checkyesno(ws, q40, 52, 3);//

                    checkyesno(ws, q57, 53, 3);
                    checkyesno(ws, q59, 54, 3);

                    checkyesno(ws, q42, 55, 3);
                    //  checkyesno(ws, q43, 57, 3);
                    // checkyesno(ws, q44, 58, 3);
                    //checkyesno(ws, q45, 59, 3);
                    checkyesno(ws, q46, 57, 3);
                    checkyesno(ws, q47, 58, 3);
                    checkyesno(ws, q48, 59, 3);
                    checkyesno(ws, q49, 60, 3);
                    checkyesno(ws, q50, 61, 3);
                    checkyesno(ws, q51, 62, 3);
                    checkyesno(ws, q52, 63, 3);
                    checkyesno(ws, q53, 64, 3);
                    checkyesno(ws, q54, 65, 3);
                    checkyesno(ws, q55, 66, 3);

                    checkyesno(ws, q60, 67, 3);
                    checkyesno(ws, q61, 68, 3);
                    checkyesno(ws, q62, 69, 3);
                    checkyesno(ws, q63, 70, 3);
                    checkyesno(ws, q58, 71, 3);




                    ws.Row(72).Height = 90;
                    //ws.Row(56).Height = 40.50;


                }
                ws.Column(3).Width = 36;
                //ws.Column(5).Width = 20;
                //ws.Column(7).Width = 20;
                //ws.Column(9).Width = 20;
                //ws.Column(11).Width = 20;

                reader.Close();
                cn.Close();

                try
                {
                    AddImage(ws, 72, 3, 72, 3, 150, 120, getimage(auditid, ""), "agscbr1", 935, 90);

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

                int c = 1;
                for (int s = 5; s <= 36; s += 2)
                {
                    try
                    {
                        ws.Column(s).Width = 20;
                        AddImage(ws, 4, s, 9, s, 140, 114, getimage(auditid, c.ToString()), "m" + c, 935, 90);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    c++;
                }

                int rc = 13;
                for (int s = 1; s <= 80; s++)
                {
                    if (s >= 12)
                    {

                        if (s == 13)
                        {
                            if (chkquestimage(auditid, (s + 1).ToString()) != "")
                            {
                                ws.Row(rc).Height = 71;
                            }
                            //rc++;
                        }
                        else
                        {
                            if (chkquestimage(auditid, s.ToString()) != "")
                            {
                                ws.Row(rc).Height = 71;
                            }
                            rc++;
                        }
                    }
                    else if (s != 6 && s != 11 && s != 12)
                    {
                        if (chkquestimage(auditid, s.ToString()) != "")
                            ws.Row(rc).Height = 71;
                        rc++;
                    }
                    else if (s == 11)
                    {
                        if (chkquestimage(auditid, "41") != "")
                            ws.Row(rc).Height = 71;
                        rc += 3;
                    }



                }

                try
                {
                    AddImage(ws, 13, 5, 13, 5, 140, 90, getquestimage(auditid, "1"), "i" + 13, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 14, 5, 14, 5, 140, 90, getquestimage(auditid, "2"), "i" + 14, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 15, 5, 15, 5, 140, 90, getquestimage(auditid, "3"), "i" + 15, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 16, 5, 16, 5, 140, 90, getquestimage(auditid, "4"), "i" + 16, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 17, 5, 17, 5, 140, 90, getquestimage(auditid, "5"), "i" + 17, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 5, 18, 5, 140, 90, getquestimage(auditid, "6"), "i" + 18, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 19, 5, 19, 5, 140, 90, getquestimage(auditid, "8"), "i" + 19, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 20, 5, 20, 5, 140, 90, getquestimage(auditid, "9"), "i" + 20, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 21, 5, 21, 5, 140, 90, getquestimage(auditid, "10"), "i" + 21, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 22, 5, 22, 5, 140, 90, getquestimage(auditid, "41"), "i" + 22, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 23, 5, 22, 5, 140, 90, getquestimage(auditid, "11"), "is" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 5, 25, 5, 140, 90, getquestimage(auditid, "12"), "i" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 26, 5, 26, 5, 140, 90, getquestimage(auditid, "14"), "i" + 24, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 27, 5, 27, 5, 140, 90, getquestimage(auditid, "15"), "i" + 25, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 28, 5, 28, 5, 140, 90, getquestimage(auditid, "16"), "i" + 26, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 29, 5, 29, 5, 140, 90, getquestimage(auditid, "17"), "i" + 27, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                //try
                //{
                //    AddImage(ws, 30, 5, 30, 5, 140, 90, getquestimage(auditid, "18"), "i" + 21, 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}

                rc = 30;
                for (int s = 18; s <= 80; s++)
                {
                    try
                    {
                        if (rc != 41)
                            AddImage(ws, rc, 5, rc, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    rc++;
                }

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





        private void CreateHeader3(ExcelWorksheet ws)
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
            obj.changeformat(ws, font, "4", 18, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "5", 19, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "6", 20, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "7", 21, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "8", 22, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "9", 23, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 24, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "10", 25, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "11", 26, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "12", 27, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "13", 28, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "14", 29, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "15", 30, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "16", 31, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "17", 32, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "18", 33, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 34, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 35, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 36, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "19", 37, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "20", 38, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "21", 39, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "22", 40, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 41, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "23", 42, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "24", 43, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "25", 44, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 45, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 46, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "26", 47, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "27", 48, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "28", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "", 52, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "29", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "30", 54, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "31", 55, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "32", 56, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 57, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 58, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 59, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "33", 60, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "34", 61, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "35", 62, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "36", 63, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "37", 64, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "38", 65, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "39", 66, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "40", 67, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "41", 68, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "42", 69, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "43", 70, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "44", 71, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "45", 72, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "46", 73, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "47", 74, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Description", 12, 2, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Remarks", 12, 3, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Atm machine working fine?", 13, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "caretaker available?", 14, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Name :", 15, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Contact Number :", 16, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Cleaning Done Regularly?", 17, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Flooring Proper?", 18, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Dust Bin OK?", 19, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Backroom OK?", 20, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Writing Ledge and Vms Proper?", 21, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollypop Cleaned or not?", 22, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Fire Extinguisher Available ?", 23, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Is The Fire Extinguisher Expired?", 24, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is RNM Ok?", 25, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Lights Ok?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "No. Of CFL Working?", 27, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "II. Flooring Proper?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Glow Sign Proper?", 28, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Working Properly?", 29, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Walls Proper?", 30, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Ceiling Proper?", 31, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Mat Available?", 32, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS AC Installed at Site", 33, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. AC Working Properly?", 34, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. AC Connected with timer?", 35, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. AC connected with meter?", 36, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS AND BATTERIES Working?", 37, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CAMERAS AVAILABLE AT SITE?", 38, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollipop is working?", 39, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ANY ISSUE AFFECTNG THE TRANSACTIONS?", 40, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Feedback from Neighboring Shops/ LL", 41, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VSAT Ballasting", 42, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Mandatory Notices", 43, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Electricity Bill Payment", 44, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Any New Bills at Site", 45, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Submeter Reading", 46, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Any Power Theft Noticed", 47, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 48, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin); obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is the Visit along with PM Engineer and CRA", 49, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. If Yes: PM Docket No:", 50, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. If Yes: Is PM Done properly", 51, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. Cash Tallied with Admin Balance, Machine Counter and Physical Counting", 52, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Housekeeping Done On Site?", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Deep Cleaning Done On Site?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Raw Power Status: ", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "I.P N Reading:", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "II.P E Reading:", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "III.N E Reading:", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS Power (Volt):", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.P N Reading:", 57, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II.P E Reading:", 58, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III.N E Reading:", 59, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Power availability in a day (no of Hrs):", 60, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Frequency of power failure in a day:", 61, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the ODU-IDU Connection done as per requirement?", 62, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other ATMs nearby (range within 500 meters) ?", 63, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Stabilizer available ?", 64, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Isolation available ?", 65, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Monkey cage available ?", 66, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is CAM1 Working?", 67, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is CAM2 Working?", 68, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the Image getting stored?", 69, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is EJ getting pulled?", 70, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other Remarks", 71, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the atm  power switch available inside back room ", 72, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Heigth of ATM Room", 73, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is Ramp Available ", 74, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            // obj.changeformat(ws, font, "Any Other Issues Noticed?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Auditor Selfie", 75, 1, 75, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Sign : ", 56, 1, 56, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            ws.Column(3).Width = 18.19;
            ws.Column(4).Width = 2.21;
        }
        private void CreateData3(ExcelWorksheet ws)
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

                a = cnCommand.CommandText = @"SELECT Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,
            Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,
            Q55,Q56,Q57,remark,Q58,
               a.atmid,a.bankid,a.location,a.city,convert(varchar(10),convert(date,vdate),103) as vdate,userid,vid,ISNULL(Q59,'NA') as Q59,ISNULL(Q60,'NA') as Q60,
            ISNULL(Q61,'NA') as Q61,ISNULL(Q62,'NA') as Q62,isnull(Q63,'NA') as Q63,isnull(Q64,'NA') as Q64,isnull(Q65,'NA') as Q65
            from DR_CTP d join atms a on a.atmid = d.ATMID where Vid ='" + auditid + "' ";

                cn.Open();
                reader = cnCommand.ExecuteReader();
                // obj.changeformat(ws, f, a, 2, 4, 2, 4);
                while (reader.Read())
                {
                    sr = sr + 1;
                    string q1 = reader[0].ToString().Trim();
                    string q2 = reader[1].ToString().Trim();
                    string q3 = reader[2].ToString().Trim();
                    string q4 = reader[3].ToString().Trim();
                    string q5 = reader[4].ToString().Trim();
                    string q6 = reader[5].ToString().Trim();
                    string q7 = reader[6].ToString().Trim();
                    string q8 = reader[7].ToString().Trim();
                    string q9 = reader[8].ToString().Trim();
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
                    string q41 = reader[40].ToString().Trim();
                    string q42 = reader[41].ToString().Trim();
                    string q43 = reader[42].ToString().Trim();
                    string q44 = reader[43].ToString().Trim();
                    string q45 = reader[44].ToString().Trim();
                    string q46 = reader[45].ToString().Trim();
                    string q47 = reader[46].ToString().Trim();
                    string q48 = reader[47].ToString().Trim();
                    string q49 = reader[48].ToString().Trim();
                    string q50 = reader[49].ToString().Trim();
                    string q51 = reader[50].ToString().Trim();
                    string q52 = reader[51].ToString().Trim();
                    string q53 = reader[52].ToString().Trim();
                    string q54 = reader[53].ToString().Trim();
                    string q55 = reader[54].ToString().Trim();
                    string q56 = reader[55].ToString().Trim();
                    string q57 = reader[56].ToString().Trim();
                    string q58 = reader[57].ToString().Trim();
                    string q59 = reader[58].ToString().Trim();

                    string q60 = reader["Q59"].ToString().Trim();
                    string q61 = reader["Q60"].ToString().Trim();
                    string q62 = reader["Q61"].ToString().Trim();
                    string q63 = reader["Q62"].ToString().Trim();
                    string q64 = reader["Q63"].ToString().Trim();
                    string q65 = reader["Q64"].ToString().Trim();
                    string q66 = reader["Q65"].ToString().Trim();


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
                    // checkyesno(ws, q6, 18, 3);
                    checkyesno(ws, q7, 18, 3);
                    checkyesno(ws, q8, 19, 3);
                    checkyesno(ws, q9, 20, 3);
                    checkyesno(ws, q10, 21, 3);

                    checkyesno(ws, q41, 22, 3);    //SIGNAGE & LOLLIPOP CLEAN OR NOT

                    checkyesno(ws, q56, 23, 3); //fire extin
                    checkyesno(ws, q11, 24, 3);

                    checkyesno(ws, q12, 25, 3);
                    // checkyesno(ws, q13, 25, 3);
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
                    // checkyesno(ws, q36, 49, 3);
                    checkyesno(ws, q36, 48, 3);
                    checkyesno(ws, q37, 49, 3);
                    checkyesno(ws, q38, 50, 3);
                    checkyesno(ws, q39, 51, 3);
                    // checkyesno(ws, q41, 55, 3);
                    checkyesno(ws, q40, 52, 3);//

                    checkyesno(ws, q57, 53, 3);
                    checkyesno(ws, q59, 54, 3);

                    checkyesno(ws, q42, 55, 3);
                    //  checkyesno(ws, q43, 57, 3);
                    // checkyesno(ws, q44, 58, 3);
                    //checkyesno(ws, q45, 59, 3);
                    checkyesno(ws, q46, 57, 3);
                    checkyesno(ws, q47, 58, 3);
                    checkyesno(ws, q48, 59, 3);
                    checkyesno(ws, q49, 60, 3);
                    checkyesno(ws, q50, 61, 3);
                    checkyesno(ws, q51, 62, 3);
                    checkyesno(ws, q52, 63, 3);
                    checkyesno(ws, q53, 64, 3);
                    checkyesno(ws, q54, 65, 3);
                    checkyesno(ws, q55, 66, 3);

                    checkyesno(ws, q60, 67, 3);
                    checkyesno(ws, q61, 68, 3);
                    checkyesno(ws, q62, 69, 3);
                    checkyesno(ws, q63, 70, 3);
                    checkyesno(ws, q58, 71, 3);
                    checkyesno(ws, q64, 72, 3);
                    checkyesno(ws, q65, 73, 3);
                    checkyesno(ws, q66, 74, 3);

                    ws.Row(75).Height = 90;
                    //ws.Row(56).Height = 40.50;


                }
                ws.Column(3).Width = 36;

                reader.Close();
                cn.Close();

                try
                {
                    AddImage(ws, 75, 3, 75, 3, 150, 100, getimage(auditid, ""), "agscbr1", 935, 90);

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

                int c = 1;
                for (int s = 5; s <= 36; s += 2)
                {
                    try
                    {
                        ws.Column(s).Width = 20;
                        AddImage(ws, 4, s, 9, s, 140, 114, getimage(auditid, c.ToString()), "m" + c, 935, 90);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    c++;
                }

                int rc = 13;
                for (int s = 1; s <= 80; s++)
                {
                    if (s >= 12)
                    {

                        if (s == 13)
                        {
                            if (chkquestimage(auditid, (s + 1).ToString()) != "")
                            {
                                ws.Row(rc).Height = 71;
                            }
                            //rc++;
                        }
                        else
                        {
                            if (rc == 72)
                            {
                                if (rc == 72 && chkquestimage(auditid, "64") != "")
                                {
                                    ws.Row(rc).Height = 71;
                                }
                            }
                            else if (rc == 66)
                            {
                                if (rc == 66 && chkquestimage(auditid, "55") != "")
                                {
                                    ws.Row(rc).Height = 71;
                                }
                            }
                            else if (chkquestimage(auditid, s.ToString()) != "" && rc != 67)
                            {
                                ws.Row(rc).Height = 71;
                            }
                            rc++;
                        }
                    }
                    else if (s != 6 && s != 11 && s != 12)
                    {
                        if (chkquestimage(auditid, s.ToString()) != "")
                            ws.Row(rc).Height = 71;
                        rc++;
                    }
                    //else if ()
                    else if (s == 11)
                    {
                        if (chkquestimage(auditid, "41") != "")
                            ws.Row(rc).Height = 71;
                        rc += 3;
                    }



                }

                try
                {
                    AddImage(ws, 13, 5, 13, 5, 140, 90, getquestimage(auditid, "1"), "i" + 13, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 14, 5, 14, 5, 140, 90, getquestimage(auditid, "2"), "i" + 14, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 15, 5, 15, 5, 140, 90, getquestimage(auditid, "3"), "i" + 15, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 16, 5, 16, 5, 140, 90, getquestimage(auditid, "4"), "i" + 16, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 17, 5, 17, 5, 140, 90, getquestimage(auditid, "5"), "i" + 17, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 5, 18, 5, 140, 90, getquestimage(auditid, "6"), "i" + 18, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 19, 5, 19, 5, 140, 90, getquestimage(auditid, "8"), "i" + 19, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 20, 5, 20, 5, 140, 90, getquestimage(auditid, "9"), "i" + 20, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 21, 5, 21, 5, 140, 90, getquestimage(auditid, "10"), "i" + 21, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 22, 5, 22, 5, 140, 90, getquestimage(auditid, "41"), "i" + 22, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 23, 5, 22, 5, 140, 90, getquestimage(auditid, "11"), "is" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 5, 25, 5, 140, 90, getquestimage(auditid, "12"), "i" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 26, 5, 26, 5, 140, 90, getquestimage(auditid, "14"), "i" + 24, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 27, 5, 27, 5, 140, 90, getquestimage(auditid, "15"), "i" + 25, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 28, 5, 28, 5, 140, 90, getquestimage(auditid, "16"), "i" + 26, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 29, 5, 29, 5, 140, 90, getquestimage(auditid, "17"), "i" + 27, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                //try
                //{
                //    AddImage(ws, 30, 5, 30, 5, 140, 90, getquestimage(auditid, "18"), "i" + 21, 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}

                rc = 30;
                for (int s = 18; s <= 80; s++)
                {
                    try
                    {
                        if (rc == 76)
                        {
                            AddImage(ws, rc - 4, 5, rc - 4, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                        else if (rc == 67)
                        {
                            AddImage(ws, rc - 1, 5, rc - 1, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                        else if (rc != 41)
                        {
                            AddImage(ws, rc, 5, rc, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    rc++;
                }

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

        private void CreateHeader4(ExcelWorksheet ws)
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
            obj.changeformat(ws, font, "4", 18, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "5", 19, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "6", 20, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "7", 21, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "8", 22, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "9", 23, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 24, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "10", 25, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "11", 26, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "12", 27, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "13", 28, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "14", 29, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "15", 30, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "16", 31, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "17", 32, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "18", 33, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 34, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 35, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 36, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "19", 37, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "20", 38, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "21", 39, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "22", 40, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 41, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "23", 42, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "24", 43, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "25", 44, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 45, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 46, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "26", 47, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "27", 48, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "28", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "", 52, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "29", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "30", 54, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "31", 55, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "32", 56, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 57, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 58, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 59, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "33", 60, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "34", 61, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "35", 62, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "36", 63, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "37", 64, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "38", 65, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "39", 66, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "40", 67, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "41", 68, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "42", 69, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "43", 70, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "44", 71, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "45", 72, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "46", 73, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "47", 74, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "48", 75, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);


            obj.changeformat(ws, font, "Description", 12, 2, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Remarks", 12, 3, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Atm machine working fine?", 13, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "caretaker available?", 14, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Name :", 15, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Contact Number :", 16, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Cleaning Done Regularly?", 17, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Flooring Proper?", 18, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Dust Bin OK?", 19, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Backroom OK?", 20, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Writing Ledge and Vms Proper?", 21, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollypop Cleaned or not?", 22, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Fire Extinguisher Available ?", 23, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Is The Fire Extinguisher Expired?", 24, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is RNM Ok?", 25, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Lights Ok?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "No. Of CFL Working?", 27, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "II. Flooring Proper?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Glow Sign Proper?", 28, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Working Properly?", 29, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Walls Proper?", 30, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Ceiling Proper?", 31, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Mat Available?", 32, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS AC Installed at Site", 33, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. AC Working Properly?", 34, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. AC Connected with timer?", 35, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. AC connected with meter?", 36, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS AND BATTERIES Working?", 37, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CAMERAS AVAILABLE AT SITE?", 38, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollipop is working?", 39, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ANY ISSUE AFFECTNG THE TRANSACTIONS?", 40, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Feedback from Neighboring Shops/ LL", 41, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VSAT Ballasting", 42, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Mandatory Notices", 43, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Electricity Bill Payment", 44, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Any New Bills at Site", 45, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Submeter Reading", 46, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Any Power Theft Noticed", 47, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 48, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin); obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is the Visit along with PM Engineer and CRA", 49, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. If Yes: PM Docket No:", 50, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. If Yes: Is PM Done properly", 51, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. Cash Tallied with Admin Balance, Machine Counter and Physical Counting", 52, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Housekeeping Done On Site?", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Deep Cleaning Done On Site?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Raw Power Status: ", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "I.P N Reading:", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "II.P E Reading:", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "III.N E Reading:", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS Power (Volt):", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.P N Reading:", 57, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II.P E Reading:", 58, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III.N E Reading:", 59, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Power availability in a day (no of Hrs):", 60, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Frequency of power failure in a day:", 61, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the ODU-IDU Connection done as per requirement?", 62, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other ATMs nearby (range within 500 meters) ?", 63, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Stabilizer available ?", 64, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Isolation available ?", 65, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Monkey cage available ?", 66, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is CAM1 Working?", 67, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is CAM2 Working?", 68, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the Image getting stored?", 69, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is EJ getting pulled?", 70, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other Remark", 71, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the atm  power switch available inside back room ", 72, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Heigth of ATM Room", 73, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is Ramp Available ", 74, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is LAN routing proper ?", 75, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            // obj.changeformat(ws, font, "Any Other Issues Noticed?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Auditor Selfie", 76, 1, 76, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Sign : ", 56, 1, 56, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            ws.Column(3).Width = 18.19;
            ws.Column(4).Width = 2.21;
        }
        private void CreateData4(ExcelWorksheet ws)
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

                a = cnCommand.CommandText = @"SELECT Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,
            Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,
            Q55,Q56,Q57,remark,Q58,
               a.atmid,a.bankid,a.location,a.city,convert(varchar(10),convert(date,vdate),103) as vdate,userid,vid,ISNULL(Q59,'NA') as Q59,ISNULL(Q60,'NA') as Q60,
            ISNULL(Q61,'NA') as Q61,ISNULL(Q62,'NA') as Q62,isnull(Q63,'NA') as Q63,isnull(Q64,'NA') as Q64,isnull(Q65,'NA') as Q65 ,isnull(Q66,'NA') as Q66
            from DR_CTP d join atms a on a.atmid = d.ATMID where Vid ='" + auditid + "' ";

                cn.Open();
                reader = cnCommand.ExecuteReader();
                // obj.changeformat(ws, f, a, 2, 4, 2, 4);
                while (reader.Read())
                {
                    sr = sr + 1;
                    string q1 = reader[0].ToString().Trim();
                    string q2 = reader[1].ToString().Trim();
                    string q3 = reader[2].ToString().Trim();
                    string q4 = reader[3].ToString().Trim();
                    string q5 = reader[4].ToString().Trim();
                    string q6 = reader[5].ToString().Trim();
                    string q7 = reader[6].ToString().Trim();
                    string q8 = reader[7].ToString().Trim();
                    string q9 = reader[8].ToString().Trim();
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
                    string q41 = reader[40].ToString().Trim();
                    string q42 = reader[41].ToString().Trim();
                    string q43 = reader[42].ToString().Trim();
                    string q44 = reader[43].ToString().Trim();
                    string q45 = reader[44].ToString().Trim();
                    string q46 = reader[45].ToString().Trim();
                    string q47 = reader[46].ToString().Trim();
                    string q48 = reader[47].ToString().Trim();
                    string q49 = reader[48].ToString().Trim();
                    string q50 = reader[49].ToString().Trim();
                    string q51 = reader[50].ToString().Trim();
                    string q52 = reader[51].ToString().Trim();
                    string q53 = reader[52].ToString().Trim();
                    string q54 = reader[53].ToString().Trim();
                    string q55 = reader[54].ToString().Trim();
                    string q56 = reader[55].ToString().Trim();
                    string q57 = reader[56].ToString().Trim();
                    string q58 = reader[57].ToString().Trim();
                    string q59 = reader[58].ToString().Trim();

                    string q60 = reader["Q59"].ToString().Trim();
                    string q61 = reader["Q60"].ToString().Trim();
                    string q62 = reader["Q61"].ToString().Trim();
                    string q63 = reader["Q62"].ToString().Trim();
                    string q64 = reader["Q63"].ToString().Trim();
                    string q65 = reader["Q64"].ToString().Trim();
                    string q66 = reader["Q65"].ToString().Trim();
                    string q67 = reader["Q66"].ToString().Trim();

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
                    // checkyesno(ws, q6, 18, 3);
                    checkyesno(ws, q7, 18, 3);
                    checkyesno(ws, q8, 19, 3);
                    checkyesno(ws, q9, 20, 3);
                    checkyesno(ws, q10, 21, 3);

                    checkyesno(ws, q41, 22, 3);    //SIGNAGE & LOLLIPOP CLEAN OR NOT

                    checkyesno(ws, q56, 23, 3); //fire extin
                    checkyesno(ws, q11, 24, 3);

                    checkyesno(ws, q12, 25, 3);
                    // checkyesno(ws, q13, 25, 3);
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
                    // checkyesno(ws, q36, 49, 3);
                    checkyesno(ws, q36, 48, 3);
                    checkyesno(ws, q37, 49, 3);
                    checkyesno(ws, q38, 50, 3);
                    checkyesno(ws, q39, 51, 3);
                    // checkyesno(ws, q41, 55, 3);
                    checkyesno(ws, q40, 52, 3);//

                    checkyesno(ws, q57, 53, 3);
                    checkyesno(ws, q59, 54, 3);

                    checkyesno(ws, q42, 55, 3);
                    //  checkyesno(ws, q43, 57, 3);
                    // checkyesno(ws, q44, 58, 3);
                    //checkyesno(ws, q45, 59, 3);
                    checkyesno(ws, q46, 57, 3);
                    checkyesno(ws, q47, 58, 3);
                    checkyesno(ws, q48, 59, 3);
                    checkyesno(ws, q49, 60, 3);
                    checkyesno(ws, q50, 61, 3);
                    checkyesno(ws, q51, 62, 3);
                    checkyesno(ws, q52, 63, 3);
                    checkyesno(ws, q53, 64, 3);
                    checkyesno(ws, q54, 65, 3);
                    checkyesno(ws, q55, 66, 3);

                    checkyesno(ws, q60, 67, 3);
                    checkyesno(ws, q61, 68, 3);
                    checkyesno(ws, q62, 69, 3);
                    checkyesno(ws, q63, 70, 3);
                    checkyesno(ws, q58, 71, 3);
                    checkyesno(ws, q64, 72, 3);
                    checkyesno(ws, q65, 73, 3);
                    checkyesno(ws, q66, 74, 3);
                    checkyesno(ws, q67, 75, 3);

                    ws.Row(76).Height = 90;
                    //ws.Row(56).Height = 40.50;


                }
                ws.Column(3).Width = 36;

                reader.Close();
                cn.Close();

                try
                {
                    AddImage(ws, 76, 3, 76, 3, 150, 100, getimage(auditid, ""), "agscbr1", 935, 90);

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

                int c = 1;
                for (int s = 5; s <= 36; s += 2)
                {
                    try
                    {
                        ws.Column(s).Width = 20;
                        AddImage(ws, 4, s, 9, s, 140, 114, getimage(auditid, c.ToString()), "m" + c, 935, 90);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    c++;
                }

                int rc = 13;
                for (int s = 1; s <= 80; s++)
                {
                    if (s >= 12)
                    {

                        if (s == 13)
                        {
                            if (chkquestimage(auditid, (s + 1).ToString()) != "")
                            {
                                ws.Row(rc).Height = 71;
                            }
                            //rc++;
                        }
                        else
                        {
                            if (rc == 72)
                            {
                                if (rc == 72 && chkquestimage(auditid, "64") != "")
                                {
                                    ws.Row(rc).Height = 71;
                                }
                            }
                            else if (rc == 66)
                            {
                                if (rc == 66 && chkquestimage(auditid, "55") != "")
                                {
                                    ws.Row(rc).Height = 71;
                                }
                            }
                            else if (chkquestimage(auditid, s.ToString()) != "" && rc != 67)
                            {
                                ws.Row(rc).Height = 71;
                            }
                            rc++;
                        }
                    }
                    else if (s != 6 && s != 11 && s != 12)
                    {
                        if (chkquestimage(auditid, s.ToString()) != "")
                            ws.Row(rc).Height = 71;
                        rc++;
                    }
                    //else if ()
                    else if (s == 11)
                    {
                        if (chkquestimage(auditid, "41") != "")
                            ws.Row(rc).Height = 71;
                        rc += 3;
                    }



                }

                try
                {
                    AddImage(ws, 13, 5, 13, 5, 140, 90, getquestimage(auditid, "1"), "i" + 13, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 14, 5, 14, 5, 140, 90, getquestimage(auditid, "2"), "i" + 14, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 15, 5, 15, 5, 140, 90, getquestimage(auditid, "3"), "i" + 15, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 16, 5, 16, 5, 140, 90, getquestimage(auditid, "4"), "i" + 16, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 17, 5, 17, 5, 140, 90, getquestimage(auditid, "5"), "i" + 17, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 5, 18, 5, 140, 90, getquestimage(auditid, "6"), "i" + 18, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 19, 5, 19, 5, 140, 90, getquestimage(auditid, "8"), "i" + 19, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 20, 5, 20, 5, 140, 90, getquestimage(auditid, "9"), "i" + 20, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 21, 5, 21, 5, 140, 90, getquestimage(auditid, "10"), "i" + 21, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 22, 5, 22, 5, 140, 90, getquestimage(auditid, "41"), "i" + 22, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 23, 5, 22, 5, 140, 90, getquestimage(auditid, "11"), "is" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 5, 25, 5, 140, 90, getquestimage(auditid, "12"), "i" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 26, 5, 26, 5, 140, 90, getquestimage(auditid, "14"), "i" + 24, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 27, 5, 27, 5, 140, 90, getquestimage(auditid, "15"), "i" + 25, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 28, 5, 28, 5, 140, 90, getquestimage(auditid, "16"), "i" + 26, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 29, 5, 29, 5, 140, 90, getquestimage(auditid, "17"), "i" + 27, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                //try
                //{
                //    AddImage(ws, 30, 5, 30, 5, 140, 90, getquestimage(auditid, "18"), "i" + 21, 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}

                rc = 30;
                for (int s = 18; s <= 80; s++)
                {
                    try
                    {
                        if (rc == 76)
                        {
                            AddImage(ws, rc - 4, 5, rc - 4, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                        else if (rc == 67)
                        {
                            AddImage(ws, rc - 1, 5, rc - 1, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                        else if (rc != 41)
                        {
                            AddImage(ws, rc, 5, rc, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    rc++;
                }

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

        private void CreateHeader5(ExcelWorksheet ws)
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
            obj.changeformat(ws, font, "4", 18, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "5", 19, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "6", 20, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "7", 21, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "8", 22, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "9", 23, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 24, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "10", 25, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "11", 26, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "12", 27, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "13", 28, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "14", 29, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "15", 30, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "16", 31, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "17", 32, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "18", 33, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 34, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 35, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 36, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "19", 37, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "20", 38, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "21", 39, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "22", 40, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 41, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "23", 42, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "24", 43, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "25", 44, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 45, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 46, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "26", 47, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "27", 48, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "28", 49, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 50, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 51, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "", 52, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "29", 53, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "30", 54, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "31", 55, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "32", 56, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 57, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 58, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "", 59, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "33", 60, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "34", 61, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "35", 62, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "36", 63, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "37", 64, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "38", 65, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "39", 66, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "40", 67, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "41", 68, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "42", 69, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "43", 70, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "44", 71, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "45", 72, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "46", 73, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "47", 74, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "48", 75, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "49", 76, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "50", 77, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "51", 78, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "52", 79, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "53", 80, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "54", 81, 1, null, null, null, null, true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Description", 12, 2, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Remarks", 12, 3, null, null, null, Color.FromArgb(252, 213, 180), true, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Center, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Atm machine working fine?", 13, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "caretaker available?", 14, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Name :", 15, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Contact Number :", 16, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Cleaning Done Regularly?", 17, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Flooring Proper?", 18, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Dust Bin OK?", 19, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Backroom OK?", 20, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Writing Ledge and Vms Proper?", 21, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollypop Cleaned or not?", 22, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Fire Extinguisher Available ?", 23, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Is The Fire Extinguisher Expired?", 24, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is RNM Ok?", 25, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Lights Ok?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "No. Of CFL Working?", 27, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "II. Flooring Proper?", 26, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Glow Sign Proper?", 28, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Working Properly?", 29, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Walls Proper?", 30, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Ceiling Proper?", 31, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Door Mat Available?", 32, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "IS AC Installed at Site", 33, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. AC Working Properly?", 34, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. AC Connected with timer?", 35, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. AC connected with meter?", 36, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS AND BATTERIES Working?", 37, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "CAMERAS AVAILABLE AT SITE?", 38, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Signage & Lollipop is working?", 39, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "ANY ISSUE AFFECTNG THE TRANSACTIONS?", 40, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.Feedback from Neighboring Shops/ LL", 41, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "VSAT Ballasting", 42, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Mandatory Notices", 43, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Electricity Bill Payment", 44, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. Any New Bills at Site", 45, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. Submeter Reading", 46, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Any Power Theft Noticed", 47, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 48, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin); obj.changeformat(ws, font, "Multimeter Reading of Earthing:", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is the Visit along with PM Engineer and CRA", 49, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I. If Yes: PM Docket No:", 50, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II. If Yes: Is PM Done properly", 51, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III. Cash Tallied with Admin Balance, Machine Counter and Physical Counting", 52, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Housekeeping Done On Site?", 53, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Deep Cleaning Done On Site?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Raw Power Status: ", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "I.P N Reading:", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "II.P E Reading:", 55, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            //obj.changeformat(ws, font, "III.N E Reading:", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "UPS Power (Volt):", 56, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "I.P N Reading:", 57, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "II.P E Reading:", 58, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "III.N E Reading:", 59, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Power availability in a day (no of Hrs):", 60, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Frequency of power failure in a day:", 61, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the ODU-IDU Connection done as per requirement?", 62, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other ATMs nearby (range within 500 meters) ?", 63, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Stabilizer available ?", 64, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Isolation available ?", 65, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Monkey cage available ?", 66, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            obj.changeformat(ws, font, "Is CAM1 Working?", 67, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is CAM2 Working?", 68, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the Image getting stored?", 69, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is EJ getting pulled?", 70, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Other Remark", 71, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is the atm  power switch available inside back room ", 72, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Heigth of ATM Room", 73, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is Ramp Available ", 74, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is LAN routing proper ?", 75, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Space availability in ATM backroom for 2 units with dimensions 2X2 each ?", 76, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Space availability in ATM lobby for 2 units with size dimensions 2X2 each ??", 77, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Network feasibility Voice ?", 78, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Network feasibility Data ?", 79, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Is shutter open close activity happening ?", 80, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            obj.changeformat(ws, font, "Shutter open/Close activity happening daily ?", 81, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);

            // obj.changeformat(ws, font, "Any Other Issues Noticed?", 54, 2, null, null, null, null, false, false, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            obj.changeformat(ws, font, "Auditor Selfie", 82, 1, 82, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);
            // obj.changeformat(ws, font, "Sign : ", 56, 1, 56, 2, null, null, false, true, false, false, ExcelVerticalAlignment.Center, ExcelHorizontalAlignment.Left, ExcelBorderStyle.Thin);



            ws.Column(3).Width = 18.19;
            ws.Column(4).Width = 2.21;
        }
        private void CreateData5(ExcelWorksheet ws)
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

                a = cnCommand.CommandText = @"SELECT Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,
            Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,
            Q55,Q56,Q57,remark,Q58,
               a.atmid,a.bankid,a.location,a.city,convert(varchar(10),convert(date,vdate),103) as vdate,userid,vid,ISNULL(Q59,'NA') as Q59,ISNULL(Q60,'NA') as Q60,
            ISNULL(Q61,'NA') as Q61,ISNULL(Q62,'NA') as Q62,isnull(Q63,'NA') as Q63,isnull(Q64,'NA') as Q64,isnull(Q65,'NA') as Q65 ,isnull(Q66,'NA') as Q66,isnull(Q67,'NA') as Q67,isnull(Q68,'NA') as Q68,isnull(Q69,'NA') as Q69,isnull(Q70,'NA') as Q70,isnull(Q71,'NA') as Q71,isnull(Q72,'NA') as Q72
            from DR_CTP d join atms a on a.atmid = d.ATMID where Vid ='" + auditid + "' ";

                cn.Open();
                reader = cnCommand.ExecuteReader();
                // obj.changeformat(ws, f, a, 2, 4, 2, 4);
                while (reader.Read())
                {
                    sr = sr + 1;
                    string q1 = reader[0].ToString().Trim();
                    string q2 = reader[1].ToString().Trim();
                    string q3 = reader[2].ToString().Trim();
                    string q4 = reader[3].ToString().Trim();
                    string q5 = reader[4].ToString().Trim();
                    string q6 = reader[5].ToString().Trim();
                    string q7 = reader[6].ToString().Trim();
                    string q8 = reader[7].ToString().Trim();
                    string q9 = reader[8].ToString().Trim();
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
                    string q41 = reader[40].ToString().Trim();
                    string q42 = reader[41].ToString().Trim();
                    string q43 = reader[42].ToString().Trim();
                    string q44 = reader[43].ToString().Trim();
                    string q45 = reader[44].ToString().Trim();
                    string q46 = reader[45].ToString().Trim();
                    string q47 = reader[46].ToString().Trim();
                    string q48 = reader[47].ToString().Trim();
                    string q49 = reader[48].ToString().Trim();
                    string q50 = reader[49].ToString().Trim();
                    string q51 = reader[50].ToString().Trim();
                    string q52 = reader[51].ToString().Trim();
                    string q53 = reader[52].ToString().Trim();
                    string q54 = reader[53].ToString().Trim();
                    string q55 = reader[54].ToString().Trim();
                    string q56 = reader[55].ToString().Trim();
                    string q57 = reader[56].ToString().Trim();
                    string q58 = reader[57].ToString().Trim();
                    string q59 = reader[58].ToString().Trim();

                    string q60 = reader["Q59"].ToString().Trim();
                    string q61 = reader["Q60"].ToString().Trim();
                    string q62 = reader["Q61"].ToString().Trim();
                    string q63 = reader["Q62"].ToString().Trim();
                    string q64 = reader["Q63"].ToString().Trim();
                    string q65 = reader["Q64"].ToString().Trim();
                    string q66 = reader["Q65"].ToString().Trim();
                    string q67 = reader["Q66"].ToString().Trim();
                    string q68 = reader["Q67"].ToString().Trim();
                    string q69 = reader["Q68"].ToString().Trim();
                    string q70 = reader["Q69"].ToString().Trim();
                    string q71 = reader["Q70"].ToString().Trim();
                    string q72 = reader["Q71"].ToString().Trim();
                    string q73 = reader["Q72"].ToString().Trim();

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
                    // checkyesno(ws, q6, 18, 3);
                    checkyesno(ws, q7, 18, 3);
                    checkyesno(ws, q8, 19, 3);
                    checkyesno(ws, q9, 20, 3);
                    checkyesno(ws, q10, 21, 3);

                    checkyesno(ws, q41, 22, 3);    //SIGNAGE & LOLLIPOP CLEAN OR NOT

                    checkyesno(ws, q56, 23, 3); //fire extin
                    checkyesno(ws, q11, 24, 3);

                    checkyesno(ws, q12, 25, 3);
                    // checkyesno(ws, q13, 25, 3);
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
                    // checkyesno(ws, q36, 49, 3);
                    checkyesno(ws, q36, 48, 3);
                    checkyesno(ws, q37, 49, 3);
                    checkyesno(ws, q38, 50, 3);
                    checkyesno(ws, q39, 51, 3);
                    // checkyesno(ws, q41, 55, 3);
                    checkyesno(ws, q40, 52, 3);//

                    checkyesno(ws, q57, 53, 3);
                    checkyesno(ws, q59, 54, 3);

                    checkyesno(ws, q42, 55, 3);
                    //  checkyesno(ws, q43, 57, 3);
                    // checkyesno(ws, q44, 58, 3);
                    //checkyesno(ws, q45, 59, 3);
                    checkyesno(ws, q46, 57, 3);
                    checkyesno(ws, q47, 58, 3);
                    checkyesno(ws, q48, 59, 3);
                    checkyesno(ws, q49, 60, 3);
                    checkyesno(ws, q50, 61, 3);
                    checkyesno(ws, q51, 62, 3);
                    checkyesno(ws, q52, 63, 3);
                    checkyesno(ws, q53, 64, 3);
                    checkyesno(ws, q54, 65, 3);
                    checkyesno(ws, q55, 66, 3);

                    checkyesno(ws, q60, 67, 3);
                    checkyesno(ws, q61, 68, 3);
                    checkyesno(ws, q62, 69, 3);
                    checkyesno(ws, q63, 70, 3);
                    checkyesno(ws, q58, 71, 3);
                    checkyesno(ws, q64, 72, 3);
                    checkyesno(ws, q65, 73, 3);
                    checkyesno(ws, q66, 74, 3);
                    checkyesno(ws, q67, 75, 3);
                    checkyesno(ws, q68, 76, 3);
                    checkyesno(ws, q69, 77, 3);
                    checkyesno(ws, q70, 78, 3);
                    checkyesno(ws, q71, 79, 3);
                    checkyesno(ws, q72, 80, 3);
                    checkyesno(ws, q73, 81, 3);

                    ws.Row(82).Height = 90;
                    //ws.Row(56).Height = 40.50;


                }
                ws.Column(3).Width = 36;

                reader.Close();
                cn.Close();

                try
                {
                    AddImage(ws, 82, 3, 82, 3, 150, 100, getimage(auditid, ""), "agscbr1", 935, 90);

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

                int c = 1;
                for (int s = 5; s <= 36; s += 2)
                {
                    try
                    {
                        ws.Column(s).Width = 20;
                        AddImage(ws, 4, s, 9, s, 140, 114, getimage(auditid, c.ToString()), "m" + c, 935, 90);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    c++;
                }

                int rc = 13;
                for (int s = 1; s <= 80; s++)
                {
                    if (s >= 12)
                    {

                        if (s == 13)
                        {
                            if (chkquestimage(auditid, (s + 1).ToString()) != "")
                            {
                                ws.Row(rc).Height = 71;
                            }
                            //rc++;
                        }
                        else
                        {
                            if (rc == 72)
                            {
                                if (rc == 72 && chkquestimage(auditid, "64") != "")
                                {
                                    ws.Row(rc).Height = 71;
                                }
                            }
                            if (rc == 76)
                            {
                                if (rc == 76 && chkquestimage(auditid, "67") != "")
                                {
                                    ws.Row(rc).Height = 71;
                                }
                            }
                            if (rc == 77)
                            {
                                if (rc == 77 && chkquestimage(auditid, "68") != "")
                                {
                                    ws.Row(rc).Height = 71;
                                }
                            }
                            else if (rc == 66)
                            {
                                if (rc == 66 && chkquestimage(auditid, "55") != "")
                                {
                                    ws.Row(rc).Height = 71;
                                }
                            }
                            else if (chkquestimage(auditid, s.ToString()) != "" && rc != 67 && s != 67 && s != 68)
                            {
                                ws.Row(rc).Height = 71;
                            }
                            rc++;
                        }
                    }
                    else if (s != 6 && s != 11 && s != 12)
                    {
                        if (chkquestimage(auditid, s.ToString()) != "")
                            ws.Row(rc).Height = 71;
                        rc++;
                    }
                    //else if ()
                    else if (s == 11)
                    {
                        if (chkquestimage(auditid, "41") != "")
                            ws.Row(rc).Height = 71;
                        rc += 3;
                    }



                }

                try
                {
                    AddImage(ws, 13, 5, 13, 5, 140, 90, getquestimage(auditid, "1"), "i" + 13, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 14, 5, 14, 5, 140, 90, getquestimage(auditid, "2"), "i" + 14, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 15, 5, 15, 5, 140, 90, getquestimage(auditid, "3"), "i" + 15, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 16, 5, 16, 5, 140, 90, getquestimage(auditid, "4"), "i" + 16, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                try
                {
                    AddImage(ws, 17, 5, 17, 5, 140, 90, getquestimage(auditid, "5"), "i" + 17, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 18, 5, 18, 5, 140, 90, getquestimage(auditid, "6"), "i" + 18, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 19, 5, 19, 5, 140, 90, getquestimage(auditid, "8"), "i" + 19, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 20, 5, 20, 5, 140, 90, getquestimage(auditid, "9"), "i" + 20, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 21, 5, 21, 5, 140, 90, getquestimage(auditid, "10"), "i" + 21, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 22, 5, 22, 5, 140, 90, getquestimage(auditid, "41"), "i" + 22, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 23, 5, 22, 5, 140, 90, getquestimage(auditid, "11"), "is" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 25, 5, 25, 5, 140, 90, getquestimage(auditid, "12"), "i" + 23, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 26, 5, 26, 5, 140, 90, getquestimage(auditid, "14"), "i" + 24, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 27, 5, 27, 5, 140, 90, getquestimage(auditid, "15"), "i" + 25, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 28, 5, 28, 5, 140, 90, getquestimage(auditid, "16"), "i" + 26, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                try
                {
                    AddImage(ws, 29, 5, 29, 5, 140, 90, getquestimage(auditid, "17"), "i" + 27, 935, 90);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                //try
                //{
                //    AddImage(ws, 30, 5, 30, 5, 140, 90, getquestimage(auditid, "18"), "i" + 21, 935, 90);
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}

                rc = 30;
                for (int s = 18; s <= 80; s++)
                {
                    try
                    {
                        if (rc == 76)
                        {
                            AddImage(ws, rc - 4, 5, rc - 4, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                        else if (rc == 67)
                        {
                            AddImage(ws, rc - 1, 5, rc - 1, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                        if (s == 67)
                        {
                            if (chkquestimage(auditid, s.ToString()) != "")
                            {
                                // ws.Row(76).Height = 81;
                                //  ws.Row(76).Height = 71;
                                AddImage(ws, 76, 5, 76, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                            }
                        }
                        else if (s == 68)
                        {
                            if (chkquestimage(auditid, s.ToString()) != "")
                            {
                                // ws.Row(77).Height = 81;
                                // ws.Row(77).Height = 71;
                                AddImage(ws, 77, 5, 77, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                            }
                        }
                        else if (rc != 41)
                        {
                            AddImage(ws, rc, 5, rc, 5, 140, 90, getquestimage(auditid, s.ToString()), "imgs" + s, 935, 90);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    rc++;
                }

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

        public string getquestimage(string VID, string imgno)
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
                                img = folderpath + "PhotoQ" + imgno + "_" + VID + ".jpg";
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

        public string chkquestimage(string VID, string imgno)
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
                            if (File.Exists(folderpath + "PhotoQ" + imgno + "_" + VID + ".jpg"))
                            {
                                try
                                {
                                    img = "1";
                                }
                                catch (Exception ex)
                                {
                                    ex.Message.ToString();
                                }
                            }
                        }
                    }
                }
            }
            return img;
        }
    }
}