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
    public partial class UserMasterReport : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        int cnt = 0;
        string role = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            }
            catch (Exception ex) { }
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

                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }

        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state, string role)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all SELECT  distinct userid as userid,userid FROM [users] where role like '" + role + "'";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and state in(" + state + ")";
            }
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
            if (ddrole.SelectedValue == "%")
            {
                role = "role in ('AO','DE','CM')";
            }
            else
            {
                role = "role like '" + ddrole.SelectedValue + "'";
            }
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;
            if (users == "All")
            {
                users = "%";
            }
            #region Code to fetch data from DR_CTP
            string sql = "";
            //        if (txtuser.Text == "")
            //        {
            //            sql = @"SELECT d.Vid AS [VISIT ID],d.USERID AS [USER ID],d.ATMID AS [ATM ID],a.SiteID as [Site ID],a.onoffsite as [SITE TYPE],a.addressline1 AS LOCATION,a.state AS [STATE],a.bankid AS [BANK NAME], a.client AS MSP, 
            //                    convert(varchar(10),convert(date,vdate),101) AS [DATE OF VISIT], d.vtime AS [TIME OF VISIT],visittype as [VISIT TYPE],visitremark as [VISIT REMARK] ,Q1 as [ATM MACHINE WORKING FINE?],
            //                    Q2 as [CARETAKER AVAILABLE?],Q3 as [CARETAKER NAME],Q4 as [CARETAKER NUMBER],Q5 as [CLEANING DONE REGULARLY?], Q6 as [How Severe is the upkeep issue], Q7 as [FLOORING PROPER?], Q8 as [DUST BIN OK?],
            //                    Q9 as [BACKROOM OK?], Q10 as [WRITING LEDGE AND VMS PROPER?],Q41 as [Signage & Lollipop Cleaned or not?], Q11 as [FIRE EXTINGUISHER OK?], Q12 as [IS RNM OK?], Q13 as [How Severe is the RnM issue],
            //                    Q14 as [LIGHTS OK?],Q15 as [No. Of CFL Working],Q16 as [GLOW SIGN PROPER?], Q17 as [DOOR WORKING PROPERLY?], Q18 as [WALLS PROPER?], Q19 as [CEILING PROPER?],
            //                    Q20 as [DOOR MAT AVAILABLE?], Q21 as [IS AC Installed at Site], Q22 as [AC WORKING PROPERLY?], Q23 as [AC Connected with timer?], Q24 as [AC connected with meter?],
            //                    Q25 as [UPS AND BATTERIES WORKING?], Q26 as [CAMERAS AVAILABLE AT SITE?], Q27 as [Signage & Lollipop is working?], Q28 as [ANY ISSUE AFFECTNG THE TRANSACTIONS?],
            //                    Q29 as [Feedback from Neighboring Shops/ LL], Q30 as [VSAT Ballasting], Q31 as [Mandatory Notices], Q32 as [Electricity Bill Payment], Q33 as [Any New Bills at Site],
            //                    Q34 as [Submeter Reading], Q35 as [Any Power Theft Noticed], Q36 as [Multimeter Reading of Earthing], Q37 as [Is the Visit along with PM Engineer and CRA],
            //                    Q38 as [If Yes: PM Docket No:], Q39 as [If Yes: Is PM Done properly], Q40 as [Cash Tallied with Admin Balance, Machine Counter and Physical Couting],Q42 as [Raw Power Status],Q43 as [Multimeter P N Reading],Q44 as [Multimeter P E Reading],Q45 as [Multimeter N E Reading],Q46 as [UPS P N Reading],Q47 as [UPS P E Reading],Q48 as [UPS N E Reading],Q49 as [Power availability in a day (no of Hrs)],Q50 as [Frequency of power failure in a day],Q51 as [Is the ODU-IDU Connection done as per requirement], Q52 as [Other ATMs nearby (range within 500 meters)],Q53 as [Stabilizer available],Q54 as [Isolation available],Q55 as [Monkey cage available], d.lat as[LATITUDE], d.lon as[LONGITUDE]
            //                    from dr_ctp d inner join atms a on d.atmid=a.atmid join users u on u.userid=d.userid where d.userid like '" + users +
            //                    "' and " + role + " and convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "'";
            //        }
            //        else
            //        {
            //            sql = @"SELECT d.Vid AS [VISIT ID],d.USERID AS [USER ID],d.ATMID AS [ATM ID],a.SiteID as [Site ID],a.onoffsite as [SITE TYPE],a.addressline1 AS LOCATION,a.state AS [STATE],a.bankid AS [BANK NAME], a.client AS MSP, 
            //                    convert(varchar(10),convert(date,vdate),101) AS [DATE OF VISIT], d.vtime AS [TIME OF VISIT],visittype as [VISIT TYPE],visitremark as [VISIT REMARK], Q1 as [ATM MACHINE WORKING FINE?],
            //                    Q2 as [CARETAKER AVAILABLE?],Q3 as [CARETAKER NAME],Q4 as [CARETAKER NUMBER],Q5 as [CLEANING DONE REGULARLY?], Q6 as [How Severe is the upkeep issue], Q7 as [FLOORING PROPER?], Q8 as [DUST BIN OK?],
            //                    Q9 as [BACKROOM OK?], Q10 as [WRITING LEDGE AND VMS PROPER?],Q41 as [Signage & Lollipop Cleaned or not?], Q11 as [FIRE EXTINGUISHER OK?], Q12 as [IS RNM OK?], Q13 as [How Severe is the RnM issue],
            //                    Q14 as [LIGHTS OK?],Q15 as [No. Of CFL Working],Q16 as [GLOW SIGN PROPER?], Q17 as [DOOR WORKING PROPERLY?], Q18 as [WALLS PROPER?], Q19 as [CEILING PROPER?],
            //                    Q20 as [DOOR MAT AVAILABLE?], Q21 as [IS AC Installed at Site], Q22 as [AC WORKING PROPERLY?], Q23 as [AC Connected with timer?], Q24 as [AC connected with meter?],
            //                    Q25 as [UPS AND BATTERIES WORKING?], Q26 as [CAMERAS AVAILABLE AT SITE?], Q27 as [Signage & Lollipop is working?], Q28 as [ANY ISSUE AFFECTNG THE TRANSACTIONS?],
            //                    Q29 as [Feedback from Neighboring Shops/ LL], Q30 as [VSAT Ballasting], Q31 as [Mandatory Notices], Q32 as [Electricity Bill Payment], Q33 as [Any New Bills at Site],
            //                    Q34 as [Submeter Reading], Q35 as [Any Power Theft Noticed], Q36 as [Multimeter Reading of Earthing], Q37 as [Is the Visit along with PM Engineer and CRA],
            //                    Q38 as [If Yes: PM Docket No:], Q39 as [If Yes: Is PM Done properly], Q40 as [Cash Tallied with Admin Balance, Machine Counter and Physical Couting],Q42 as [Raw Power Status],Q43 as [P N Reading],Q44 as [P E Reading],Q45 as [N E Reading],Q46 as [P N Reading],Q47 as [P E Reading],Q48 as [N E Reading],Q49 as [Power availability in a day (no of Hrs)],Q50 as [Frequency of power failure in a day],Q51 as [Is the ODU-IDU Connection done as per requirement], Q52 as [Other ATMs nearby (range within 500 meters)],Q53 as [Stabilizer available],Q54 as [Isolation available],Q55 as [Monkey cage available], d.lat as[LATITUDE], d.lon as[LONGITUDE]
            //                    from dr_ctp d inner join atms a on d.atmid=a.atmid join users u on u.userid=d.userid where d.userid like '" + users +
            //                    "' and " + role + " and a.state in (" + txtuser.Text + ") and convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "'";
            //        }
            if (txtuser.Text == "")
            {
                sql = @"SELECT b.Vid AS [VISIT ID],b.reason as [REASON OF VISIT],b.remarks as [REMARKS],
            convert(varchar(10),convert(date,b.fromdate),103)+' '+convert(varchar(10),convert(time,b.fromdate),108) as [START TIME],
            convert(varchar(10),convert(date,b.todate),103)+' '+convert(varchar(10),convert(time,b.todate),108) as [END TIME],b.enteredby as [USER], 
            convert(varchar(10),convert(date,b.enteredon),103)+' '+convert(varchar(10),convert(time,b.enteredon),108) as [ENTERED ON] from activity b join users a on b.enteredby = a.userid 
            where b.enteredby like '" + users + "'and " + role + "  and convert(date,fromdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' ";
            }
            else
            {
                sql = @"SELECT b.Vid AS [VISIT ID],b.reason as [REASON OF VISIT],
            b.remarks as [REMARKS],convert(varchar(10),convert(date,b.fromdate),103)+' '+convert(varchar(10),convert(time,b.fromdate),108) as [START TIME],
            convert(varchar(10),convert(date,b.todate),103)+' '+convert(varchar(10),convert(time,b.todate),108) as [END TIME],b.enteredby as [USER], 
            convert(varchar(10),convert(date,b.enteredon),103)+' '+convert(varchar(10),convert(time,b.enteredon),108) as [ENTERED ON] 
            from activity b join users a on b.enteredby = a.userid 
            where b.enteredby like '" + users + "'and " + role + " and a.state in (" + txtuser.Text + ") and convert(date,fromdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' ";
            }

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
            Response.AddHeader("content-disposition", "attachment;filename=UserMasterReport.xls");
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

                //for (int i = 11; i < 52; i++)
                //{

                //    if (e.Row.Cells[i].Text.Contains("E|N"))
                //    {
                //        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|N", "No-");
                //    }
                //    else if (e.Row.Cells[i].Text.Contains("E|Y"))
                //    {
                //        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|Y", "Yes-");
                //    }
                //    else if (e.Row.Cells[i].Text.ToUpper().Contains("Y-"))
                //    {
                //        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("Y-", "Yes-");
                //    }
                //    else if (e.Row.Cells[i].Text.ToUpper().Contains("N-"))
                //    {
                //        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("N-", "No-");
                //    }
                //    else if (e.Row.Cells[i].Text.ToUpper() == "Y")
                //    {
                //        e.Row.Cells[i].Text = "Yes";
                //    }
                //    else if (e.Row.Cells[i].Text.ToUpper() == "N")
                //    {
                //        e.Row.Cells[i].Text = "No";
                //    }
                // }


            }
        }

        protected void ddrole_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList1.DataBind();

            DropDownList1.Items.Add("ALL");
            DropDownList1.Items.FindByText("ALL").Value = "%";
            DropDownList1.Items.FindByValue("%").Selected = true;
        }
    }
}