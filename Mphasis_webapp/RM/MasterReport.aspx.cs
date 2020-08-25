using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RM
{
    public partial class MasterReport : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        int cnt = 0;
        string state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            #region Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
                DropDownList1.Items.FindByText("ALL").Value = "%";
                DropDownList1.Items.FindByValue("%").Selected = true;
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion

            if (!Page.IsPostBack)
            {
                string state = "SELECT  distinct state,state as STATE  FROM [atms] where rm = '" + Session["sess_username"].ToString() + "'";
                obj.BindListboxWithValue(ddstate, state);
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all select distinct u.userid,u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role in ('DE','CM','AO') " +
                            " and a.RM like '" + HttpContext.Current.Session["sess_username"].ToString() + "'";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and u.state in(" + state + ")";
            }
            //strQuery += "  order by userid";

            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strQuery;

                        cmd.Connection = con;
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            list.Add(new ListItem(sdr[0].ToString()));
                        }
                        con.Close();
                        return list;
                    }
                    catch (Exception ex) { }
                    return list;
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {

            //For retrieving selected values of state
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;
            if (users == "All")
            {
                users = "%";
            }
            #region Code to fetch
            string sql = "";
            if (txtuser.Text == "")
            {
                #region Code to fetch data from DR_CTP
                //            sql = @"SELECT d.Vid AS [VISIT ID],d.USERID AS [USER ID],d.ATMID AS [ATM ID],a.SiteID as [Site ID],a.onoffsite as [SITE TYPE],a.addressline1 AS LOCATION,a.state AS [STATE],a.bankid AS [BANK NAME], a.client AS MSP, 
                //                    convert(varchar(10),convert(date,vdate),103) AS [DATE OF VISIT], d.vtime AS [TIME OF VISIT],visittype as [VISIT TYPE],visitremark as [VISIT REMARK] ,Q1 as [ATM MACHINE WORKING FINE?],
                //                    Q2 as [CARETAKER AVAILABLE?],Q3 as [CARETAKER NAME],Q4 as [CARETAKER NUMBER],Q5 as [CLEANING DONE REGULARLY?], Q6 as [How Severe is the upkeep issue], Q7 as [FLOORING PROPER?], Q8 as [DUST BIN OK?],
                //                    Q9 as [BACKROOM OK?], Q10 as [WRITING LEDGE AND VMS PROPER?],Q41 as [Signage & Lollipop Cleaned or not?], Q56 as [FIRE EXTINGUISHER AVAILABLE?],Q11 as [IS THE FIRE EXTINGUISHER EXPIRED], Q12 as [IS RNM OK?], Q13 as [How Severe is the RnM issue],
                //                    Q14 as [LIGHTS OK?],Q15 as [No. Of CFL Working],Q16 as [GLOW SIGN PROPER?], Q17 as [DOOR WORKING PROPERLY?], Q18 as [WALLS PROPER?], Q19 as [CEILING PROPER?],
                //                    Q20 as [DOOR MAT AVAILABLE?], Q21 as [IS AC Installed at Site], Q22 as [AC WORKING PROPERLY?], Q23 as [AC Connected with timer?], Q24 as [AC connected with meter?],
                //                    Q25 as [UPS AND BATTERIES WORKING?], Q26 as [CAMERAS AVAILABLE AT SITE?], Q27 as [Signage & Lollipop is working?], Q28 as [ANY ISSUE AFFECTNG THE TRANSACTIONS?],
                //                    Q29 as [Feedback from Neighboring Shops/ LL], Q30 as [VSAT Ballasting], Q31 as [Mandatory Notices], Q32 as [Electricity Bill Payment], Q33 as [Any New Bills at Site],
                //                    Q34 as [Submeter Reading], Q35 as [Any Power Theft Noticed], Q36 as [Multimeter Reading of Earthing], Q37 as [Is the Visit along with PM Engineer and CRA],
                //                    Q38 as [If Yes: PM Docket No:], Q39 as [If Yes: Is PM Done properly], Q40 as [Cash Tallied with Admin Balance, Machine Counter and Physical Couting],q57 as [Housekeeping done on site?],Q58 as [Deep cleaning done on site?],Q42 as [Raw Power Status],
                //                    Q43 as [Multimeter P N Reading],Q44 as [Multimeter P E Reading],Q45 as [Multimeter N E Reading],
                //                    Q46 as [UPS P N Reading],Q47 as [UPS P E Reading],Q48 as [UPS N E Reading],Q49 as [Power availability in a day (no of Hrs)],Q50 as [Frequency of power failure in a day],Q51 as [Is the ODU-IDU Connection done as per requirement], 
                //                    Q52 as [Other ATMs nearby (range within 500 meters)],Q53 as [Stabilizer available],Q54 as [Isolation available],Q55 as [Monkey cage available],ISNULL(Q59,'NA') as [Is CAM1 Working?],ISNULL(Q60,'NA') as [Is CAM2 Working?],ISNULL(Q61,'NA') as [Is the Image getting stored?],ISNULL(Q62,'NA') as [Is EJ getting pulled?],remark as [OTHER REMARK],
                //					[AC],[ATM],[BAT1],[BAT2],[BAT3],[MODEM],[UPS], d.lat as[LATITUDE], d.lon as[LONGITUDE]
                //                    from dr_ctp d inner join atms a on d.atmid=a.atmid join users u on u.userid=d.userid
                //					left outer join 
                //					(SELECT vid,[AC],[ATM],[BAT1],[BAT2],[BAT3],[MODEM],[UPS] from 
                //					(
                //					select vid,srno, type
                //					from scan
                //					) x
                //					pivot 
                //					(
                //					max(srno)
                //					for type in ([AC],[ATM],[BAT1],[BAT2],[BAT3],[MODEM],[UPS])
                //					) p)
                //					s on d.vid=s.vid
                //                    where d.userid like '" + users + "' and a.RM like '" + Session["sess_username"] + "' and convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "'  order by convert(date,vdate) desc,convert(time,vtime) desc";

                sql = @"SELECT d.Vid AS [VISIT ID],d.USERID AS [USER ID],d.ATMID AS [ATM ID],a.SiteID as [Site ID],a.onoffsite as [SITE TYPE],a.addressline1 AS LOCATION,a.state AS [STATE],a.bankid AS [BANK NAME], a.client AS MSP, 
                convert(varchar(10),convert(date,vdate),103) AS [DATE OF VISIT], d.vtime AS [TIME OF VISIT],visittype as [VISIT TYPE],visitremark as [VISIT REMARK] ,Q1 as [ATM MACHINE WORKING FINE?],
                Q2 as [CARETAKER AVAILABLE?],Q3 as [CARETAKER NAME],Q4 as [CARETAKER NUMBER],Q5 as [CLEANING DONE REGULARLY?], Q6 as [How Severe is the upkeep issue], Q7 as [FLOORING PROPER?], Q8 as [DUST BIN OK?],
                Q9 as [BACKROOM OK?], Q10 as [WRITING LEDGE AND VMS PROPER?],Q41 as [Signage & Lollipop Cleaned or not?], Q56 as [FIRE EXTINGUISHER AVAILABLE?],Q11 as [IS THE FIRE EXTINGUISHER EXPIRED], Q12 as [IS RNM OK?], Q13 as [How Severe is the RnM issue],
                Q14 as [LIGHTS OK?],Q15 as [No. Of CFL Working],Q16 as [GLOW SIGN PROPER?], Q17 as [DOOR WORKING PROPERLY?], Q18 as [WALLS PROPER?], Q19 as [CEILING PROPER?],
                Q20 as [DOOR MAT AVAILABLE?], Q21 as [IS AC Installed at Site], Q22 as [AC WORKING PROPERLY?], Q23 as [AC Connected with timer?], Q24 as [AC connected with meter?],
                Q25 as [UPS AND BATTERIES WORKING?], Q26 as [CAMERAS AVAILABLE AT SITE?], Q27 as [Signage & Lollipop is working?], Q28 as [ANY ISSUE AFFECTNG THE TRANSACTIONS?],
                Q29 as [Feedback from Neighboring Shops/ LL], Q30 as [VSAT Ballasting], Q31 as [Mandatory Notices], Q32 as [Electricity Bill Payment], Q33 as [Any New Bills at Site],
                Q34 as [Submeter Reading], Q35 as [Any Power Theft Noticed], Q36 as [Multimeter Reading of Earthing], Q37 as [Is the Visit along with PM Engineer and CRA],
                Q38 as [If Yes: PM Docket No:], Q39 as [If Yes: Is PM Done properly], Q40 as [Cash Tallied with Admin Balance, Machine Counter and Physical Couting],q57 as [Housekeeping done on site?],Q58 as [Deep cleaning done on site?],Q42 as [Raw Power Status],
                Q43 as [Multimeter P N Reading],Q44 as [Multimeter P E Reading],Q45 as [Multimeter N E Reading],
                Q46 as [UPS P N Reading],Q47 as [UPS P E Reading],Q48 as [UPS N E Reading],Q49 as [Power availability in a day (no of Hrs)],Q50 as [Frequency of power failure in a day],Q51 as [Is the ODU-IDU Connection done as per requirement], 
                 Q52 as [Other ATMs nearby (range within 500 meters)],Q53 as [Stabilizer available],Q54 as [Isolation available],Q55 as [Monkey cage available],ISNULL(Q59,'NA') as [Is CAM1 Working?],ISNULL(Q60,'NA') as [Is CAM2 Working?],ISNULL(Q61,'NA') as [Is the Image getting stored?],ISNULL(Q62,'NA') as [Is EJ getting pulled?],
                isnull(Q63,'NA') as [IS ATM POWER SWITCH AVAILABLE INSIDE BACK ROOM ?],isnull(Q64,'NA')as [HEIGHT OF THE ATM ROOM ?],isnull(Q65,'NA') as [IS RAMP AVAILABLE ? ], isnull(Q66,'NA') as [IS LAN ROUTING PROPER ? ],isnull(Q67,'NA') as [Space availability in ATM backroom for 2 units with dimensions 2X2 each ? ],
                isnull(Q68,'NA') as [Space availability in ATM lobby for 2 units with size dimensions 2X2 each ? ],isnull(Q69,'NA') as [Network feasibility Voice ? ],isnull(Q70,'NA') as [Network feasibility Data ? ],isnull(Q71,'NA') as [Is shutter open close activity happening ? ],isnull(Q72,'NA') as [Shutter open/Close activity happening daily ? ],remark as [OTHER REMARK],
                [AC SRNO],[AC MAKE],[ATM SRNO],[ATM MAKE],[BAT1 SRNO],[BAT1 MAKE],
                [BAT2 SRNO],[BAT2 MAKE],[BAT3 SRNO],[BAT3 MAKE],[MODEM SRNO],[MODEM MAKE],[UPS SRNO],[UPS MAKE], d.lat as[LATITUDE], d.lon as[LONGITUDE]
                from dr_ctp d inner join atms a on d.atmid=a.atmid join users u on u.userid=d.userid
                left outer join 
                (select vid,[AC_SRNO] as [AC SRNO],[AC_MAKE] as [AC MAKE],[ATM_SRNO] as [ATM SRNO],[ATM_MAKE] as [ATM MAKE],
                [BAT1_SRNO] as [BAT1 SRNO],[BAT1_MAKE] as [BAT1 MAKE],[BAT2_SRNO] as [BAT2 SRNO],[BAT2_MAKE] as [BAT2 MAKE],
                [BAT3_SRNO] as [BAT3 SRNO],[BAT3_MAKE] as [BAT3 MAKE],[MODEM_SRNO] as [MODEM SRNO],[MODEM_MAKE] as [MODEM MAKE],
                [UPS_SRNO] as [UPS SRNO],[UPS_MAKE] as [UPS MAKE] from 
                (SELECT vid,type+'_'+col AS col,value FROM
                 (
                        SELECT vid,srno as [Srno],make AS [Make],
                        type FROM scan GROUP BY vid,srno,make,type) rt
                        unpivot ( value FOR col in (srno,make))unpiv )tp
                        pivot ( MAX(value) FOR col in ([AC_SRNO],[AC_MAKE],[ATM_SRNO],[ATM_MAKE],[BAT1_SRNO],[BAT1_MAKE],
                        [BAT2_SRNO],[BAT2_MAKE],[BAT3_SRNO],[BAT3_MAKE],[MODEM_SRNO],[MODEM_MAKE],[UPS_SRNO],[UPS_MAKE])) piv
                  ) s on d.vid=s.vid
                    where d.userid like '" + users + "' and a.RM like '" + Session["sess_username"] + "' and convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "'  order by convert(date,vdate) desc,convert(time,vtime) desc";
            }
            else
            {
                sql = @"SELECT d.Vid AS [VISIT ID],d.USERID AS [USER ID],d.ATMID AS [ATM ID],a.SiteID as [Site ID],a.onoffsite as [SITE TYPE],a.addressline1 AS LOCATION,a.state AS [STATE],a.bankid AS [BANK NAME], a.client AS MSP, 
                convert(varchar(10),convert(date,vdate),103) AS [DATE OF VISIT], d.vtime AS [TIME OF VISIT],visittype as [VISIT TYPE],visitremark as [VISIT REMARK] ,Q1 as [ATM MACHINE WORKING FINE?],
                Q2 as [CARETAKER AVAILABLE?],Q3 as [CARETAKER NAME],Q4 as [CARETAKER NUMBER],Q5 as [CLEANING DONE REGULARLY?], Q6 as [How Severe is the upkeep issue], Q7 as [FLOORING PROPER?], Q8 as [DUST BIN OK?],
                Q9 as [BACKROOM OK?], Q10 as [WRITING LEDGE AND VMS PROPER?],Q41 as [Signage & Lollipop Cleaned or not?], Q56 as [FIRE EXTINGUISHER AVAILABLE?],Q11 as [IS THE FIRE EXTINGUISHER EXPIRED], Q12 as [IS RNM OK?], Q13 as [How Severe is the RnM issue],
                Q14 as [LIGHTS OK?],Q15 as [No. Of CFL Working],Q16 as [GLOW SIGN PROPER?], Q17 as [DOOR WORKING PROPERLY?], Q18 as [WALLS PROPER?], Q19 as [CEILING PROPER?],
                Q20 as [DOOR MAT AVAILABLE?], Q21 as [IS AC Installed at Site], Q22 as [AC WORKING PROPERLY?], Q23 as [AC Connected with timer?], Q24 as [AC connected with meter?],
                Q25 as [UPS AND BATTERIES WORKING?], Q26 as [CAMERAS AVAILABLE AT SITE?], Q27 as [Signage & Lollipop is working?], Q28 as [ANY ISSUE AFFECTNG THE TRANSACTIONS?],
                Q29 as [Feedback from Neighboring Shops/ LL], Q30 as [VSAT Ballasting], Q31 as [Mandatory Notices], Q32 as [Electricity Bill Payment], Q33 as [Any New Bills at Site],
                Q34 as [Submeter Reading], Q35 as [Any Power Theft Noticed], Q36 as [Multimeter Reading of Earthing], Q37 as [Is the Visit along with PM Engineer and CRA],
                Q38 as [If Yes: PM Docket No:], Q39 as [If Yes: Is PM Done properly], Q40 as [Cash Tallied with Admin Balance, Machine Counter and Physical Couting],q57 as [Housekeeping done on site?],Q58 as [Deep cleaning done on site?],Q42 as [Raw Power Status],
                Q43 as [Multimeter P N Reading],Q44 as [Multimeter P E Reading],Q45 as [Multimeter N E Reading],
                Q46 as [UPS P N Reading],Q47 as [UPS P E Reading],Q48 as [UPS N E Reading],Q49 as [Power availability in a day (no of Hrs)],Q50 as [Frequency of power failure in a day],Q51 as [Is the ODU-IDU Connection done as per requirement], 
                 Q52 as [Other ATMs nearby (range within 500 meters)],Q53 as [Stabilizer available],Q54 as [Isolation available],Q55 as [Monkey cage available],ISNULL(Q59,'NA') as [Is CAM1 Working?],ISNULL(Q60,'NA') as [Is CAM2 Working?],ISNULL(Q61,'NA') as [Is the Image getting stored?],ISNULL(Q62,'NA') as [Is EJ getting pulled?],
                isnull(Q63,'NA') as [IS ATM POWER SWITCH AVAILABLE INSIDE BACK ROOM ?],isnull(Q64,'NA')as [HEIGHT OF THE ATM ROOM ?],isnull(Q65,'NA') as [IS RAMP AVAILABLE ? ], isnull(Q66,'NA') as [IS LAN ROUTING PROPER ? ],isnull(Q67,'NA') as [Space availability in ATM backroom for 2 units with dimensions 2X2 each ? ],
                isnull(Q68,'NA') as [Space availability in ATM lobby for 2 units with size dimensions 2X2 each ? ],isnull(Q69,'NA') as [Network feasibility Voice ? ],isnull(Q70,'NA') as [Network feasibility Data ? ],isnull(Q71,'NA') as [Is shutter open close activity happening ? ],isnull(Q72,'NA') as [Shutter open/Close activity happening daily ? ],remark as [OTHER REMARK],
                [AC SRNO],[AC MAKE],[ATM SRNO],[ATM MAKE],[BAT1 SRNO],[BAT1 MAKE],
                [BAT2 SRNO],[BAT2 MAKE],[BAT3 SRNO],[BAT3 MAKE],[MODEM SRNO],[MODEM MAKE],[UPS SRNO],[UPS MAKE], d.lat as[LATITUDE], d.lon as[LONGITUDE]
                from dr_ctp d inner join atms a on d.atmid=a.atmid join users u on u.userid=d.userid
                left outer join 
                (select vid,[AC_SRNO] as [AC SRNO],[AC_MAKE] as [AC MAKE],[ATM_SRNO] as [ATM SRNO],[ATM_MAKE] as [ATM MAKE],
                [BAT1_SRNO] as [BAT1 SRNO],[BAT1_MAKE] as [BAT1 MAKE],[BAT2_SRNO] as [BAT2 SRNO],[BAT2_MAKE] as [BAT2 MAKE],
                [BAT3_SRNO] as [BAT3 SRNO],[BAT3_MAKE] as [BAT3 MAKE],[MODEM_SRNO] as [MODEM SRNO],[MODEM_MAKE] as [MODEM MAKE],
                [UPS_SRNO] as [UPS SRNO],[UPS_MAKE] as [UPS MAKE] from 
                (SELECT vid,type+'_'+col AS col,value FROM
                 (
                        SELECT vid,srno as [Srno],make AS [Make],
                        type FROM scan GROUP BY vid,srno,make,type) rt
                        unpivot ( value FOR col in (srno,make))unpiv )tp
                        pivot ( MAX(value) FOR col in ([AC_SRNO],[AC_MAKE],[ATM_SRNO],[ATM_MAKE],[BAT1_SRNO],[BAT1_MAKE],
                        [BAT2_SRNO],[BAT2_MAKE],[BAT3_SRNO],[BAT3_MAKE],[MODEM_SRNO],[MODEM_MAKE],[UPS_SRNO],[UPS_MAKE])) piv
                  ) s on d.vid=s.vid
                    where d.userid like '" + users + "' and a.RM like '" + Session["sess_username"] + "' and convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' and a.state in (" + txtuser.Text + ")  order by convert(date,vdate) desc,convert(time,vtime) desc";

            }

            #endregion
            bucket.BindGrid(GridView1, sql);
            //Response.Write(sql);

            /*------------------------------------------------------------------------------------------------*/
            /* If no rows returned display null error or fetch count
            /*------------------------------------------------------------------------------------------------*/
            if (GridView1.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
                Label3.Visible = false;
                ImageButton1.Visible = false;
                div1.Visible = true;
            }
            else
            {
                div1.Visible = true;
                ImageButton1.Visible = true;
                Label3.Visible = true;
                Label1.Visible = false;
                Label3.Text = bucket.CountRows(GridView1, Label3);
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            btn_search_Click(sender, e);
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment;filename=MasterReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Response.Write("9");
                //for (int i = 0; i < 45; i++)
                //{
                //    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|N", "No-");
                //}

                for (int i = 11; i < 94; i++)
                {

                    if (e.Row.Cells[i].Text.Equals("E|N"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|N", "No");
                    }
                    else if (e.Row.Cells[i].Text.Contains("E|N"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|N", "No-");
                    }
                    else if (e.Row.Cells[i].Text.Contains("E|Y"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|Y", "Yes");
                    }
                    else if (e.Row.Cells[i].Text.ToUpper().Contains("Y-"))
                    {
                        if (i == 48)
                        {
                            e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("Y-", "Available-");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("Y-", "Yes-");
                        }

                    }
                    else if (e.Row.Cells[i].Text.ToUpper().Contains("N-"))
                    {
                        if (i == 48)
                        {
                            e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("N-", "Not Available-");
                        }
                        else
                        {
                            e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("N-", "No-");
                        }

                    }
                    else if (e.Row.Cells[i].Text.ToUpper() == "Y")
                    {
                        e.Row.Cells[i].Text = "Yes";
                    }
                    else if (e.Row.Cells[i].Text.ToUpper() == "N")
                    {
                        e.Row.Cells[i].Text = "No";
                    }
                }

            }
        }
    }
}