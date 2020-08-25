using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OboutInc.ImageZoom;
using System.Data;

namespace Mphasis_webapp.bank
{
    public partial class MainPage : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();

        string virtualpath = ConfigurationManager.AppSettings["virtualpath"];
        string physicalpath = ConfigurationManager.AppSettings["physicalpath"];


        private int CurrentPage = 1;
        private int ItemsPerPage = 20;

        //public void show_available_images(int noofImage, string VID)
        //{
        //    string[] year = { "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025" };
        //    string[] month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    string[] monthcount = { "01_2014", "02_2014", "03_2014", "04_2014", "05_2014", "06_2014", "07_2014", "08_2014", "09_2014", "10_2014", "11_2014", "12_2014" };



        //    for (int i = 0; i < year.Length; i++)
        //    {
        //        if (VID.Contains(year[i]))
        //        {
        //            for (int j = 0; j < month.Length; j++)
        //            {
        //                if (VID.Contains(month[j]))
        //                {
        //                    string folderpath = null;
        //                    folderpath = ""+ virtualpath +"" + year[i] + "/" + month[j] + "/";

        //                    int k = 0;
        //                    int l = 0;
        //                    ImageButton[] z = new ImageButton[23];
        //                    z[0] = ImageButton1; z[1] = ImageButton2; z[2] = ImageButton3; z[3] = ImageButton4; z[4] = ImageButton5;
        //                    z[5] = ImageButton6; z[6] = ImageButton7; z[7] = ImageButton8; z[8] = ImageButton9; z[9] = ImageButton10;
        //                    z[10] = ImageButton11; z[11] = ImageButton12; z[12] = ImageButton13; z[13] = ImageButton14; z[14] = ImageButton15;
        //                    z[15] = ImageButton16; z[16] = ImageButton17; z[17] = ImageButton18; z[18] = ImageButton19; z[19] = ImageButton20;
        //                    z[20] = ImageButton21; z[21] = ImageButton22; z[22] = ImageButton23;


        //                    try
        //                    {
        //                        while (k < noofImage)
        //                        {
        //                            try
        //                            {
        //                                if (File.Exists(""+ physicalpath +"" + year[i] + "\\" + month[j] + "\\Photo" + VID + "_" + l + ".jpg") == true)
        //                                {
        //                                    z[k].Visible = true;
        //                                    z[k].ImageUrl = folderpath + "Photo" + VID + "_" + l + ".jpg";

        //                                }
        //                                else
        //                                {
        //                                    z[k].Visible = false;
        //                                }

        //                            }
        //                            catch (Exception ex)
        //                            {

        //                            }
        //                            k++;
        //                            l++;
        //                        }
        //                        if (File.Exists(""+ physicalpath +"" + year[i] + "\\" + month[j] + "\\S" + VID + ".jpg") == true)
        //                        {
        //                            z[22].Visible = true;
        //                            z[22].ImageUrl = folderpath + "S" + VID + ".jpg";

        //                        }
        //                        else
        //                        {
        //                            z[22].Visible = false;
        //                        }

        //                    }
        //                    catch { }

        //                  //  Image1.ImageUrl = folderpath + "S" + VID + ".jpg";
        //                }
        //            }
        //        }
        //    }
        //}

        public void show_available_images(int noofImage, string VID)
        {
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
                            folderpath = "" + virtualpath + "" + year[x] + "/" + month[j] + "/";

                            int i = 0;

                            int TotalRows = 0;
                            DataTable dt = new DataTable();
                            string path = "";
                            DataRow newRow = null;

                            dt.Columns.Add("FilePath", typeof(string));
                            try
                            {
                                while (i < noofImage)
                                {
                                    if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\Photo" + VID + "_" + i + ".jpg") == true)
                                    {

                                        newRow = dt.NewRow();
                                        path = folderpath + "Photo" + VID + "_" + i + ".jpg";
                                        dt.Rows.Add(path);
                                    }
                                    i++;
                                }
                                if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\S" + VID + ".jpg") == true)
                                {
                                    ImageButton23.Visible = true;
                                    ImageButton23.ImageUrl = folderpath + "S" + VID + ".jpg";

                                }
                                else
                                {
                                    ImageButton23.Visible = false;
                                }

                            }
                            catch { }

                            try
                            {

                                DataList1.DataSource = dt;
                                DataList1.DataBind();

                                TotalRows = 20;
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message);
                            }
                            finally
                            {

                            }
                            //  Image1.ImageUrl = folderpath + "SIGN_" + VID + ".png";
                        }
                    }
                }
            }
        }


        public void beautify(string Name, Label l)
        {
            try
            {
                if (Name.Substring(0, 1) == "Y")
                {
                    l.Text = "Yes";
                    l.ForeColor = System.Drawing.Color.Blue;
                }

                else if (Name == "N/A")
                {
                    l.Text = "N/A";
                    l.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Name == "N")
                {
                    l.Text = "No";
                    l.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Name == "NA")
                {
                    l.Text = "NA";
                    l.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Name.StartsWith("N-"))
                {
                    l.ForeColor = System.Drawing.Color.Blue;
                    l.Text = "No-" + Name.Replace("N-", "");
                }
                else if (Name.StartsWith("Y-"))
                {
                    l.ForeColor = System.Drawing.Color.Blue;
                    l.Text = "Yes-" + Name.Replace("Y-", "");
                }
                else if (Name.Contains("E|Y"))
                {
                    l.ForeColor = System.Drawing.Color.Red;
                    l.Text = "Yes-" + Name.Replace("E|Y", "");
                }
                else if (Name.Contains("E|N"))
                {
                    l.ForeColor = System.Drawing.Color.Red;
                    l.Text = "No-" + Name.Replace("E|N", "");
                }
                else if (Name.Contains("E|Poor"))
                {
                    l.ForeColor = System.Drawing.Color.Red;
                    l.Text = "POOR-" + Name.Replace("E|Poor", "");
                }
                else if (Name.Contains("E|POOR"))
                {
                    l.ForeColor = System.Drawing.Color.Red;
                    l.Text = "POOR-" + Name.Replace("E|POOR", "");
                }
                else
                {
                    l.Text = Name;
                    l.ForeColor = System.Drawing.Color.Blue;
                }
            }
            catch
            {
                l.Text = Name;
                l.ForeColor = System.Drawing.Color.Blue;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["auditid"] != "")
                {
                    string querystring = Request.QueryString["auditid"]; int qno = 0;

                    string q1 = @"SELECT a.atmid,a.bankid,a.addressline1,city,a.region,a.state,a.pin,'' as [ZONE],vid,vdate,vtime,d.userid FROM Atms a INNER JOIN DR_CTP d ON a.atmid = d.ATMID
                            where vid='" + querystring + "'";
                    string[] c1 = { "ATMID", "bankid", "Addressline1", "City", "Region", "STATE", "PIN", "ZONE", "vid", "vdate", "userid", "vtime" };
                    string[] a1 = bucket.xread(q1, c1);

                    lbl_atmID.Text = a1[0]; lbl_bankid.Text = a1[1]; lbl_add.Text = a1[2]; lbl_city.Text = a1[3]; lbl_reg.Text = a1[4]; lbl_state.Text = a1[5]; lbl_pin.Text = a1[6];
                    lbl_zone.Text = a1[7]; lbl_vid.Text = a1[8]; lbl_vdate.Text = a1[9]; lbl_visitedby.Text = a1[10]; lbl_vtime.Text = a1[11];

                    #region add questions

                    lbl_q1.Text = "1. ATM MACHINE WORKING FINE?";
                    lbl_q2.Text = "2. CARETAKER AVAILABLE?";
                    lbl_q3.Text = "I. CARETAKER NAME?";
                    lbl_q4.Text = "II. CARETAKER NUMBER?";
                    lbl_q5.Text = "3. CLEANING DONE REGULARLY?";
                    lbl_q6.Text = "I. HOW SEVERE IS THE UPKEEP ISSUE";
                    lbl_q7.Text = "II. FLOORING PROPER?";
                    lbl_q8.Text = "III. DUST BIN OK?";
                    lbl_q9.Text = "IV. BACKROOM OK?";
                    lbl_q10.Text = "V WRITING LEDGE AND VMS PROPER?";
                    lbl_q11.Text = "4. FIRE EXTINGUISHER OK?";
                    lbl_q12.Text = "5. IS RNM OK?";
                    lbl_q13.Text = "I HOW SEVERE IS THE RNM ISSUE";
                    //lbl_q14.Text = "5.2 FLOORING PROPER?";
                    lbl_q14.Text = "II. LIGHTS OK?";
                    lbl_q15.Text = "III. No. Of CFL Working";
                    lbl_q16.Text = "IV. GLOW SIGN PROPER?";
                    lbl_q17.Text = "V. DOOR WORKING PROPERLY?";
                    lbl_q18.Text = "VI. WALLS PROPER?";
                    lbl_q19.Text = "VII. CEILING PROPER?";
                    lbl_q20.Text = "6. DOOR MAT AVAILABLE?";
                    lbl_q21.Text = "7. IS AC INSTALLED AT SITE";
                    lbl_q22.Text = "I. AC WORKING PROPERLY?";
                    lbl_q23.Text = "II. AC CONNECTED WITH TIMER?";
                    lbl_q24.Text = "III. AC CONNECTED WITH METER?";
                    lbl_q25.Text = "8. UPS AND BATTERIES WORKING?";
                    lbl_q26.Text = "9. CAMERAS AVAILABLE AT SITE?";
                    lbl_q27.Text = "10. SIGNAGE & LOLLIPOP IS WORKING?";
                    lbl_q28.Text = "11. ANY ISSUE AFFECTNG THE TRANSACTIONS?";
                    lbl_q29.Text = "12. FEEDBACK FROM NEIGHBORING SHOPS/ LL";
                    lbl_q30.Text = "13. VSAT BALLASTING";
                    lbl_q31.Text = "14. MANDATORY NOTICES";
                    lbl_q32.Text = "15. ELECTRICITY BILL PAYMENT";
                    lbl_q33.Text = "I. ANY NEW BILLS AT SITE";
                    lbl_q34.Text = "II. SUBMETER READING";
                    lbl_q35.Text = "16. ANY POWER THEFT NOTICED";
                    lbl_q36.Text = "17. MULTIMETER READING OF EARTHING";
                    lbl_q37.Text = "18. IS THE VISIT ALONG WITH PM ENGINEER AND CRA?";
                    lbl_q38.Text = "I. PM DOCKET NO:";
                    lbl_q39.Text = "II. IS PM DONE PROPERLY?";
                    lbl_q40.Text = "19. CASH TALLIED WITH ADMIN BALANCE, MACHINE COUNTER AND PHYSICAL COUTING";
                    //lbl_q42.Text = "20. ANY OTHER ISSUES NOTICED?";

                    #endregion

                    string q = "select Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,ltrim(rtrim(pix)) as 'pix' from dr_ctp where vid='" + querystring + "'";
                    string[] c2 = { "Q1", "Q2", "Q3", "Q4", "Q5", "Q6", "Q7", "Q8", "Q9", "Q10", "Q11", "Q12", "Q13", "Q14", "Q15", "Q16", "Q17", "Q18", "Q19", "Q20", "Q21", "Q22", "Q23", "Q24", "Q25", "Q26", "Q27", "Q28", "Q29", "Q30", "Q31", "Q32", "Q33", "Q34", "Q35", "Q36", "Q37", "Q38", "Q39", "Q40", "Q41", "Q42", "Q43", "Q44", "Q45", "Q46", "Q47", "Q48", "Q49", "Q50", "pix" };
                    string[] a = bucket.xread(q, c2);

                    Label[] l = new Label[50];
                    l[0] = Label1; l[1] = Label2; l[2] = Label3; l[3] = Label4; l[4] = Label5; l[5] = Label6; l[6] = Label7; l[7] = Label8; l[8] = Label9; l[9] = Label10;
                    l[10] = Label11; l[11] = Label12; l[12] = Label13; l[13] = Label14; l[14] = Label15; l[15] = Label16; l[16] = Label17; l[17] = Label18; l[18] = Label19;
                    l[19] = Label20; l[20] = Label21; l[21] = Label22; l[22] = Label23; l[23] = Label24; l[24] = Label25; l[25] = Label26; l[26] = Label27; l[27] = Label28;
                    l[28] = Label29; l[29] = Label30; l[30] = Label31; l[31] = Label32; l[32] = Label33; l[33] = Label34; l[34] = Label35; l[35] = Label36; l[36] = Label37;
                    l[37] = Label38; l[38] = Label39; l[39] = Label40; l[40] = Label41; l[41] = Label42; l[42] = Label43; l[43] = Label44; l[44] = Label45; l[45] = Label46;
                    l[46] = Label47; l[47] = Label48; l[48] = Label49; l[49] = Label50;

                    int i = 0;

                    while (i < 40)
                    {
                        try
                        {
                            beautify(a[i].Trim(), l[i]);
                        }
                        catch { }
                        i++;
                    }

                    string bind = "Select vid as [VISIT ID],siteid as [SITE ID],userid as [USER ID],type as [ASSET],scanstatus as [STATUS] from Scan where vid='" + querystring + "'";
                    bucket.BindGrid(GridView1, bind);

                    show_available_images(Convert.ToInt32(a[50]) - 1, lbl_vid.Text);
                }
            }
        }
    }
}