using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OboutInc.ImageZoom;
using System.Data;
using System.Configuration;

namespace Mphasis_webapp
{
    /*---------------------------------*/
    /*03 July 2015
     * New Questions added.
    /*---------------------------------*/

    public partial class MainPage1 : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();

        private int CurrentPage = 1;
        private int ItemsPerPage = 20;

        string virtualpath = ConfigurationManager.AppSettings["virtualpath"];
        string physicalpath = ConfigurationManager.AppSettings["physicalpath"];

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
        public void beautify(string Name, Label l, bool resp)
        {
            try
            {
                if (Name.StartsWith("Y-"))
                {
                    l.ForeColor = System.Drawing.Color.Blue;
                    if (resp == true)
                    {
                        l.Text = "Available-" + Name.Replace("Y-", "");
                    }
                    else
                    {
                        l.Text = "Yes-" + Name.Replace("Y-", "");
                    }
                }
                else
                if (Name.Substring(0, 1) == "Y")
                {
                    l.Text = "Yes" + Name.Replace("Y", " ");

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
                    if (resp == true)
                    {
                        l.ForeColor = System.Drawing.Color.Red;
                        l.Text = "Not Available-" + Name.Replace("N-", "");
                    }
                    else
                    {
                        l.Text = "No-" + Name.Replace("N-", "");
                    }


                }
                else if (Name.Equals("E|Y"))
                {
                    l.ForeColor = System.Drawing.Color.Red;
                    l.Text = "Yes" + Name.Replace("E|Y", "");
                }
                else if (Name.Contains("E|Y"))
                {
                    l.ForeColor = System.Drawing.Color.Red;
                    l.Text = "Yes-" + Name.Replace("E|Y", "");
                }
                else if (Name.Equals("E|N"))
                {
                    l.ForeColor = System.Drawing.Color.Red;
                    l.Text = "No" + Name.Replace("E|N", "");
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
            string ver = @"select dbo.udf_GetNumeric(REPLACE(version,'_','')) as version from DR_CTP where vid='" + Request.QueryString["auditid"] + "'";
            string[] v = { "version" };
            string[] v1 = bucket.xread(ver, v);

            if (Convert.ToDecimal(v1[0]) < Convert.ToDecimal("2.6"))
            {
                lbl43.Visible = false;
                lbl44.Visible = false;
                lbl45.Visible = false;
                lbl46.Visible = false;
                lbl47.Visible = false;
                lbl48.Visible = false;
                lbl49.Visible = false;
                lbl50.Visible = false;
                lbl51.Visible = false;
                lbl52.Visible = false;
                lbl53.Visible = false;
                lbl54.Visible = false;
                lbl55.Visible = false;
                lbl56.Visible = false;
                lbl57.Visible = false;
                lbl58.Visible = false;
                //fire ext expired
                lbl59.Visible = true;
                lbl60.Visible = false;
                lbl11.Visible = false;

                lblHK.Visible = false;
                lblDClean.Visible = false;
                //Label43.Visible = false;
                //Label44.Visible = false;
                //Label45.Visible = false;
                //Label46.Visible = false;
                //Label47.Visible = false;
                //Label48.Visible = false;
                //Label49.Visible = false;
                //Label50.Visible = false;
                //Label51.Visible = false;
                //Label52.Visible = false;
                //Label53.Visible = false;
                //Label54.Visible = false;
                //Label55.Visible = false;
                //Label56.Visible = false;
                //Label57.Visible = false;
                //Label58.Visible = false;
            }
            else if ((Convert.ToDecimal(v1[0]) == Convert.ToDecimal("2.6")) || (Convert.ToDecimal(v1[0]) == Convert.ToDecimal("2.7")))
            {
                lbl36.Visible = false;
                lblHK.Visible = false;
                lblDClean.Visible = false;
                // Label36.Visible = false;
            }
            else
            {
                lbl36.Visible = true;
                lbl44.Visible = false;
                lbl45.Visible = false;
                lbl46.Visible = false;
                lbl47.Visible = false;
                lblHK.Visible = true;
                lblDClean.Visible = true;
            }

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["auditid"] != "")
                {
                    string querystring = Request.QueryString["auditid"]; int qno = 0;

                    string q1 = @"SELECT a.atmid,a.bankid,a.addressline1,city,a.region,a.state,a.pin,'' as [ZONE],vid,convert(varchar(10),convert(date,vdate),103) as vdate,vtime,d.userid,d.visittype,case when d.visitremark is null   then 'NA' else d.visitremark end as visitremark,dbo.udf_GetNumeric(REPLACE(version,'_','')) as version FROM Atms a INNER JOIN DR_CTP d ON a.atmid = d.ATMID
                            where vid='" + querystring + "'";
                    string[] c1 = { "ATMID", "bankid", "Addressline1", "City", "Region", "STATE", "PIN", "ZONE", "vid", "vdate", "userid", "vtime", "visittype", "visitremark", "version" };
                    string[] a1 = bucket.xread(q1, c1);

                    lbl_atmID.Text = a1[0]; lbl_bankid.Text = a1[1]; lbl_add.Text = a1[2]; lbl_city.Text = a1[3]; lbl_reg.Text = a1[4]; lbl_state.Text = a1[5]; lbl_pin.Text = a1[6];
                    lbl_zone.Text = a1[7]; lbl_vid.Text = a1[8]; lbl_vdate.Text = a1[9]; lbl_visitedby.Text = a1[10]; lbl_vtime.Text = a1[11]; lbl_typ.Text = a1[12]; lbl_rmk.Text = a1[13];

                    #region add questions

                    lbl_q1.Text = "1. ATM MACHINE WORKING FINE?";
                    lbl_q2.Text = "2. CARETAKER AVAILABLE?";
                    lbl_q3.Text = " I. CARETAKER NAME?";
                    lbl_q4.Text = " II. CARETAKER NUMBER?";
                    lbl_q5.Text = "3. CLEANING DONE REGULARLY?";
                    //lbl_q6.Text = " I. HOW SEVERE IS THE UPKEEP ISSUE";
                    lbl_q7.Text = "4. FLOORING PROPER?";
                    lbl_q8.Text = "5. DUST BIN OK?";
                    lbl_q9.Text = "6. BACKROOM OK?";
                    lbl_q10.Text = "7. WRITING LEDGE AND VMS PROPER?";

                    lbl_q42.Text = "8. SIGNAGE & LOLLIPOP CLEANED OR NOT?";

                    lbl_q11.Text = "9.FIRE EXTINGUISHER AVAILABLE ? ";

                    if (Convert.ToDecimal(a1[14]) < Convert.ToDecimal("2.6"))
                    {
                        lbl_q59.Text = "9.IS THE FIRE EXTINGUISHER EXPIRED?";
                    }
                    else
                    {
                        lbl_q59.Text = "I.IS THE FIRE EXTINGUISHER EXPIRED?";
                    }
                    lbl_q12.Text = "10. IS RNM OK?";
                    //lbl_q13.Text = " I. HOW SEVERE IS THE RNM ISSUE";


                    //lbl_q14.Text = "5.2 FLOORING PROPER?";
                    lbl_q14.Text = "11. LIGHTS OK?";
                    lbl_q15.Text = "12. No. Of CFL Working";
                    lbl_q16.Text = "13. GLOW SIGN PROPER?";
                    lbl_q17.Text = "14. DOOR WORKING PROPERLY?";
                    lbl_q18.Text = "15. WALLS PROPER?";
                    lbl_q19.Text = "16. CEILING PROPER?";
                    lbl_q20.Text = "17. DOOR MAT AVAILABLE?";
                    lbl_q21.Text = "18. IS AC INSTALLED AT SITE";
                    lbl_q22.Text = " I. AC WORKING PROPERLY?";
                    lbl_q23.Text = " II. AC CONNECTED WITH TIMER?";
                    lbl_q24.Text = " III. AC CONNECTED WITH METER?";
                    lbl_q25.Text = "19. UPS AND BATTERIES WORKING?";
                    lbl_q26.Text = "20. CAMERAS AVAILABLE AT SITE?";
                    lbl_q27.Text = "21. SIGNAGE & LOLLIPOP IS WORKING?";
                    lbl_q28.Text = "22. ANY ISSUE AFFECTNG THE TRANSACTIONS?";
                    lbl_q29.Text = "  I. FEEDBACK FROM NEIGHBORING SHOPS/ LL";
                    lbl_q30.Text = "23. VSAT BALLASTING";
                    lbl_q31.Text = "24. MANDATORY NOTICES";
                    lbl_q32.Text = "25. ELECTRICITY BILL PAYMENT";
                    lbl_q33.Text = "I. ANY NEW BILLS AT SITE";
                    lbl_q34.Text = "II. SUBMETER READING";
                    lbl_q35.Text = "26. ANY POWER THEFT NOTICED";
                    lbl_q36.Text = "27. MULTIMETER READING OF EARTHING";

                    if (Convert.ToDecimal(a1[14]) <= Convert.ToDecimal("2.7"))
                    {
                        lbl_q37.Text = "27. IS THE VISIT ALONG WITH PM ENGINEER AND CRA?";
                    }
                    else
                    {
                        lbl_q37.Text = "28. IS THE VISIT ALONG WITH PM ENGINEER AND CRA?";
                    }
                    lbl_q38.Text = "  I. PM DOCKET NO:";
                    lbl_q39.Text = "  II. IS PM DONE PROPERLY?";
                    lbl_q40.Text = "  III. CASH TALLIED WITH ADMIN BALANCE, MACHINE COUNTER AND PHYSICAL COUTING";

                    //new 2 ques from ver2.8 onwards
                    lbl_q61.Text = "29. HOUSEKEEPING DONE ON SITE?";
                    lbl_q62.Text = "30. DEEP CLEANING DONE ON SITE?";


                    if (Convert.ToDecimal(a1[14]) <= Convert.ToDecimal("2.7"))
                    {
                        lbl_q43.Text = " 28. RAW POWER STATUS: ";
                        lbl_q44.Text = " 29.MULTIMETER READING OF EARTHING: ";
                        lbl_q45.Text = " I.P N READING: ";
                        lbl_q46.Text = " II.P E READING: ";
                        lbl_q47.Text = " III.N E READING: ";
                        lbl_q48.Text = " 30.UPS POWER (VOLT): ";
                        lbl_q49.Text = " I.P N READING: ";
                        lbl_q50.Text = " II.P E READING: ";
                        lbl_q51.Text = " II.N E READING: ";
                        lbl_q52.Text = "31.POWER AVAILABILITY IN A DAY (NO OF HRS)";
                        lbl_q53.Text = "32.FREQUENCY OF POWER FAILURE IN A DAY";
                        lbl_q54.Text = "33.IS THE ODU-IDU CONNECTION DONE AS PER REQUIREMENT?";
                        lbl_q55.Text = "34.OTHER ATMS NEARBY (RANGE WITHIN 500 METERS) ?";
                        lbl_q56.Text = "35.STABILIZER AVAILABLE ?";
                        lbl_q57.Text = "36.ISOLATION AVAILABLE ?";
                        lbl_q58.Text = "37.MONKEY CAGE AVAILABLE ?";

                        lbl_qu59.Text = "38.IS CAM1 WORKING?";
                        lbl_qu60.Text = "39.IS CAM2 WORKING?";
                        lbl_qu61.Text = "40.IS THE IMAGE GETTING STORED?";
                        lbl_qu62.Text = "41.IS EJ GETTING PULLED?";
                        lbl_q60.Text = "42.OTHER REMARK ?";

                    }
                    else
                    {
                        lbl_q43.Text = " 31. RAW POWER STATUS: ";
                        #region notiuseincurver
                        lbl_q44.Text = " 32.MULTIMETER READING OF EARTHING: ";
                        lbl_q45.Text = " I.P N READING: ";
                        lbl_q46.Text = " II.P E READING: ";
                        lbl_q47.Text = " III.N E READING: ";
                        #endregion

                        lbl_q48.Text = " 32.UPS POWER (VOLT): ";
                        lbl_q49.Text = " I.P N READING: ";
                        lbl_q50.Text = " II.P E READING: ";
                        lbl_q51.Text = " II.N E READING: ";
                        lbl_q52.Text = "33.POWER AVAILABILITY IN A DAY (NO OF HRS)";
                        lbl_q53.Text = "34.FREQUENCY OF POWER FAILURE IN A DAY";
                        lbl_q54.Text = "35.IS THE ODU-IDU CONNECTION DONE AS PER REQUIREMENT?";
                        lbl_q55.Text = "36.OTHER ATMS NEARBY (RANGE WITHIN 500 METERS) ?";
                        lbl_q56.Text = "37.STABILIZER AVAILABLE ?";
                        lbl_q57.Text = "38.ISOLATION AVAILABLE ?";
                        lbl_q58.Text = "39.MONKEY CAGE AVAILABLE ?";

                        lbl_qu59.Text = "40.IS CAM1 WORKING?";
                        lbl_qu60.Text = "41.IS CAM2 WORKING?";
                        lbl_qu61.Text = "42.IS THE IMAGE GETTING STORED?";
                        lbl_qu62.Text = "43.IS EJ GETTING PULLED?";
                        lbl_q60.Text = "44.OTHER REMARK ?";



                    }

                    if (Convert.ToDecimal(a1[14]) >= Convert.ToDecimal(3.2))
                    {
                        lbl63.Visible = true;
                        lbl64.Visible = true;
                        lbl65.Visible = true;

                        lbl_q63.Text = "45.IS ATM POWER SWITCH AVAILABLE INSIDE BACK ROOM ?";
                        lbl_q64.Text = "46.HEIGHT OF THE ATM ROOM ?";
                        lbl_q65.Text = "47.IS RAMP AVAILABLE ?";

                    }

                    if (Convert.ToDecimal(a1[14]) >= Convert.ToDecimal(3.3))
                    {
                        lbl66.Visible = true;
                        lbl_q66.Text = "48.IS LAN ROUTING PROPER ?";
                    }

                    if (Convert.ToDecimal(a1[14]) >= Convert.ToDecimal(3.7))
                    {
                        lbl67.Visible = true;
                        lbl68.Visible = true;
                        lbl69.Visible = true;
                        lbl70.Visible = true;
                        lbl71.Visible = true;
                        lbl72.Visible = true;

                        lbl_q67.Text = "49.Space availability in ATM backroom for 2 units with dimensions 2X2 each ?";
                        lbl_q68.Text = "50.Space availability in ATM lobby for 2 units with size dimensions 2X2 each ?";
                        lbl_q69.Text = "51.Network feasibility Voice ?";
                        lbl_q70.Text = "52.Network feasibility Data ?";
                        lbl_q71.Text = "53.Is shutter open close activity happening ?";
                        lbl_q72.Text = "54.Shutter open/Close activity happening daily ?";

                    }
                    //lbl_q42.Text = "20. ANY OTHER ISSUES NOTICED?";


                    #endregion

                    string q = "select Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15,Q16,Q17,Q18,Q19,Q20,Q21,Q22,Q23,Q24,Q25,Q26,Q27,Q28,Q29,Q30,Q31,Q32,Q33,Q34,Q35,Q36,Q37,Q38,Q39,Q40,Q41,Q42,Q43,Q44,Q45,Q46,Q47,Q48,Q49,Q50,Q51,Q52,Q53,Q54,Q55,Q56,ltrim(rtrim(pix)) as 'pix',Q57,remark,Q58,isnull(Q59,'NA') as Q59,isnull(Q60,'NA') as Q60,isnull(Q61,'NA') as Q61,isnull(Q62,'NA') as Q62,isnull(Q63,'NA') as Q63,isnull(Q64,'NA') as Q64,isnull(Q65,'NA') as Q65,isnull(Q66,'NA') as Q66,isnull(Q67,'NA') as Q67,isnull(Q68,'NA') as Q68,isnull(Q69,'NA') as Q69,isnull(Q70,'NA') as Q70,isnull(Q71,'NA') as Q71,isnull(Q72,'NA') as Q72 from dr_ctp where vid='" + querystring + "'";
                    string[] c2 = { "Q1", "Q2", "Q3", "Q4", "Q5", "Q6", "Q7", "Q8", "Q9", "Q10", "Q11", "Q41", "Q12", "Q13", "Q14", "Q15", "Q16", "Q17", "Q18", "Q19", "Q20", "Q21", "Q22", "Q23", "Q24", "Q25", "Q26", "Q27", "Q28", "Q29", "Q30", "Q31", "Q32", "Q33", "Q34", "Q35", "Q36", "Q37", "Q38", "Q39", "Q40", "Q42", "Q43", "Q44", "Q45", "Q46", "Q47", "Q48", "Q49", "Q50", "Q51", "Q52", "Q53", "Q54", "Q55", "Q56", "pix", "Q57", "remark", "Q58", "Q59", "Q60", "Q61", "Q62", "Q63", "Q64", "Q65", "Q66", "Q67", "Q68", "Q69", "Q70", "Q71", "Q72" };
                    string[] a = bucket.xread(q, c2);

                    Label[] l = new Label[74];
                    l[0] = Label1; l[1] = Label2; l[2] = Label3; l[3] = Label4; l[4] = Label5;
                    //l[5] = Label6;                
                    l[6] = Label7;
                    l[7] = Label8; l[8] = Label9; l[9] = Label10;

                    //fire ext expired

                    l[10] = Label59;

                    l[11] = Label42; //Signage & Lollypop Cleaned or not?

                    l[12] = Label12;
                    //l[13] = Label13;
                    l[14] = Label14;
                    l[15] = Label15;
                    l[16] = Label16;
                    l[17] = Label17;
                    l[18] = Label18;
                    l[19] = Label19;
                    l[20] = Label20;
                    l[21] = Label21;
                    l[22] = Label22;
                    l[23] = Label23;
                    l[24] = Label24;
                    l[25] = Label25;
                    l[26] = Label26;
                    l[27] = Label27;
                    l[28] = Label28;
                    l[29] = Label29;
                    l[30] = Label30;
                    l[31] = Label31;
                    l[32] = Label32;
                    l[33] = Label33;
                    l[34] = Label34;
                    l[35] = Label35;
                    l[36] = Label36;
                    l[37] = Label37;
                    l[38] = Label38;
                    l[39] = Label39;
                    l[40] = Label40;
                    l[41] = Label41;
                    l[41] = Label43;
                    l[42] = Label45;
                    l[43] = Label46;
                    l[44] = Label47;
                    // l[46] = Label48;
                    l[45] = Label49;
                    l[46] = Label50;
                    l[47] = Label51;
                    l[48] = Label52;
                    l[49] = Label53;
                    l[50] = Label54;
                    l[51] = Label55;
                    l[52] = Label56;
                    l[53] = Label57;
                    l[54] = Label58;

                    //fire ext available
                    l[55] = Label11;
                    l[58] = Label60;
                    l[57] = Label61;
                    l[59] = Label62;

                    l[60] = lbl_an59;
                    l[61] = lbl_an60;
                    l[62] = lbl_an61;
                    l[63] = lbl_an62;
                    l[64] = Label63;
                    l[65] = Label64;
                    l[66] = Label65;
                    l[67] = Label66;

                    l[68] = Label67;
                    l[69] = Label68;
                    l[70] = Label69;
                    l[71] = Label70;
                    l[72] = Label71;
                    l[73] = Label72;
                    //l[11] = Label12; l[12] = Label13; l[13] = Label14; l[14] = Label15; l[15] = Label16; l[16] = Label17; l[17] = Label18; l[18] = Label19;
                    //l[19] = Label20; l[20] = Label21; l[21] = Label22; l[22] = Label23; l[23] = Label24; l[24] = Label25; l[25] = Label26; l[26] = Label27; l[27] = Label28; 
                    //l[28] = Label29; l[29] = Label30; l[30] = Label31; l[31] = Label32; l[32] = Label33; l[33] = Label34; l[34] = Label35; l[35] = Label36; l[36] = Label37; 
                    //l[37] = Label38; l[38] = Label39; l[39] = Label40; l[40] = Label41;  l[42] = Label43; l[43] = Label44; l[44] = Label45; l[45] = Label46; 
                    //l[46] = Label47; l[47] = Label48; l[48] = Label49; l[49] = Label50;

                    int i = 0;

                    while (i < 74)
                    {
                        try
                        {
                            if (i == 34)
                            {
                                beautify(a[i].Trim(), l[i], true);
                            }
                            else
                            {
                                beautify(a[i].Trim(), l[i], false);
                            }
                        }
                        catch { }
                        i++;
                    }

                    string bind = "Select siteid as [SITE ID],userid as [USER ID],type as [ASSET],srno as [SRNO],make as [MAKE],scanstatus as [STATUS] from Scan where vid='" + querystring + "'";
                    bucket.BindGrid(GridView1, bind);

                    show_available_images(Convert.ToInt32(a[56]) - 1, lbl_vid.Text);
                    show_CheckList_images(65, lbl_vid.Text);
                }
            }
        }

        public void show_CheckList_images(int noofImage, string VID)
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

                            int i = 1;

                            int TotalRows = 0;
                            string path = "";
                            ImageButton[] z = new ImageButton[60];
                            z[1] = imgQ1; z[2] = imgQ2; z[3] = imgQ3; z[4] = imgQ4; z[5] = imgQ5; z[6] = imgQ6;
                            z[7] = imgQ7; z[8] = imgQ8; z[9] = imgQ9; z[10] = imgQ10; z[11] = imgQ11;
                            z[12] = imgQ12; z[13] = imgQ13; z[14] = imgQ14; z[15] = imgQ15; z[16] = imgQ16;
                            z[17] = imgQ17; z[18] = imgQ18; z[19] = imgQ19; z[20] = imgQ20; z[21] = imgQ21;
                            z[22] = imgQ22; z[23] = imgQ23; z[24] = imgQ24; z[25] = imgQ25; z[26] = imgQ26;
                            z[27] = imgQ27; z[28] = imgQ28; z[29] = imgQ29; z[30] = imgQ30; z[31] = imgQ31;
                            z[32] = imgQ32; z[33] = imgQ33; z[34] = imgQ34; z[35] = imgQ35; z[36] = imgQ36;
                            z[37] = imgQ37; z[38] = imgQ38; z[39] = imgQ39; z[40] = imgQ40; z[41] = imgQ41;
                            z[42] = imgQ42; z[43] = imgQ43; z[44] = imgQ44; z[45] = imgQ45; z[46] = imgQ46;
                            z[47] = imgQ47; z[48] = imgQ48; z[49] = imgQ49; z[50] = imgQ50;
                            z[51] = imgQ51; z[52] = imgQ52; z[53] = imgQ53; z[54] = imgQ54;
                            z[55] = imgQ58; z[56] = imgQ63; z[57] = imgQ67; z[58] = imgQ68;
                            //z[56] = imgQ56; z[57] = imgQ57; z[58] = imgQ58; 


                            try
                            {
                                while (i < noofImage)
                                {
                                    if (i == 6 || i == 13)
                                    {
                                        //   continue;
                                    }
                                    else
                                    {
                                        if (i == 11)
                                        {
                                            //Response.Write("else "+ physicalpath +"" + year[x] + "\\" + month[j] + "\\PhotoQ" + i + "_" + VID + ".jpg");
                                            if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\PhotoQ41_" + VID + ".jpg") == true)

                                            {
                                                z[42].Visible = true;
                                                z[42].ImageUrl = folderpath + "PhotoQ41_" + VID + ".jpg";
                                            }
                                            else
                                            {
                                                z[42].Visible = false;
                                                z[42].ImageUrl = "" + virtualpath + "/Image/NaImage.jpg";
                                            }
                                        }
                                        else if (i == 36 || i == 44)
                                        {
                                            //Response.Write(""+ physicalpath +"" + year[x] + "\\" + month[j] + "\\PhotoQ" + i + "_" + VID + ".jpg");
                                            if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\PhotoQ36_" + VID + ".jpg") == true)
                                            {
                                                z[i].Visible = true;
                                                z[i].ImageUrl = folderpath + "PhotoQ36_" + VID + ".jpg";
                                            }
                                        }
                                        else if (i == 56)
                                        {
                                            if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\PhotoQ64_" + VID + ".jpg") == true)
                                            {
                                                z[i].Visible = true;
                                                z[i].ImageUrl = folderpath + "PhotoQ64_" + VID + ".jpg";
                                            }
                                            else
                                            {
                                                z[i].Visible = false;
                                            }
                                        }
                                        else if (i == 57)
                                        {
                                            if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\PhotoQ67_" + VID + ".jpg") == true)
                                            {
                                                z[i].Visible = true;
                                                z[i].ImageUrl = folderpath + "PhotoQ67_" + VID + ".jpg";
                                            }
                                            else
                                            {
                                                z[i].Visible = false;
                                            }
                                        }
                                        else if (i == 58)
                                        {
                                            if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\PhotoQ68_" + VID + ".jpg") == true)
                                            {
                                                z[i].Visible = true;
                                                z[i].ImageUrl = folderpath + "PhotoQ68_" + VID + ".jpg";
                                            }
                                            else
                                            {
                                                z[i].Visible = false;
                                            }
                                        }
                                        else if (i != 42)
                                        {
                                            //Response.Write(""+ physicalpath +"" + year[x] + "\\" + month[j] + "\\PhotoQ" + i + "_" + VID + ".jpg");
                                            if (File.Exists("" + physicalpath + "" + year[x] + "\\" + month[j] + "\\PhotoQ" + i + "_" + VID + ".jpg") == true)


                                            {
                                                z[i].Visible = true;
                                                z[i].ImageUrl = folderpath + "PhotoQ" + i + "_" + VID + ".jpg";
                                            }
                                            else
                                            {
                                                z[i].Visible = false;
                                                z[i].ImageUrl = "" + virtualpath + "/Image/NaImage.jpg";
                                            }
                                        }

                                    }
                                    i++;
                                }

                            }
                            catch { }

                            try
                            {
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

    }
}