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
    public partial class DistanceMasterReportRM : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        string rows;
        string mapwithanimation = "";
        string role = "";
        int cnt = 0;
        string state = "";
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

            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            #region Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                Session["sess_x"] = "";
                string state = "SELECT  distinct state,state as STATE FROM [atms] where rm = '" + Session["sess_username"].ToString() + "'";
                obj.BindListboxWithValue(ddstate, state);
            }
            else
            {

            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all select u.userid,u.userid from users u " +
                              " where role in ('AO','CM','DE') " +
                            " and u.RM like '" + HttpContext.Current.Session["sess_username"].ToString() + "'";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and u.state in(" + state + ")";
            }
            // strQuery += "  order by userid";

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
            string sql = "";

            #region Code to fetch data from DR_CTP
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;
            if (users == "All")
            {
                users = "%";
            }
            if (txtuser.Text == "")
            {
                //            sql = @"Select x.Userid,[DistanceTraveled],case when [Sites Audited] is null then '0' else [Sites Audited] end as [Sites Audited],Ldate as [Date] from
                //                    (SELECT  userid, DistanceTraveled,ldate
                //                    FROM(SELECT   Distance.*, ROW_NUMBER() OVER (PARTITION BY ldate, userid ORDER BY CONVERT(date, ldate) desc, ltime desc) AS RN 
                //                    FROM Distance 
                //                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text +
                //                    @"' and userid in (select userid from users where  RM like '" + Session["sess_username"] + @"' and userid like '"+users+@"')
                //                    ) AS t 
                //                    WHERE RN = 1 ) x
                //                    left outer join 
                //                    (Select count(vid) as 'Sites Audited',userid,vdate from dr_ctp dc inner join atms a on a.ATMID = dc.ATMID
                //                    where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                //                    group by userid,vdate) y
                //                    on x.userid=y.userid and x.ldate=y.vdate
                //                    order by convert(date,ldate) asc,userid asc";

                sql = @"Select x.Userid,[DistanceTraveled],case when [Sites Audited] is null then '0' else [Sites Audited] end as [Sites Audited],Ldate as [Date] from
                    (Select userid,sum(convert(float,distancetraveled)) [DistanceTraveled],ldate from
                    (
                    Select userid,sum(convert(float,distancetraveled)) [DistanceTraveled],ldate from Distance  
                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text +
                        @"' and userid in (select userid from users where  RM like '" + Session["sess_username"] + @"' and userid like '" + users + @"')
                    and vid='punchout' group by userid,ldate
                    union all
                    SELECT  userid, case when vid='PUNCHOUT' then '0' else DistanceTraveled end [DistanceTraveled],ldate
                    FROM(SELECT   Distance.*, ROW_NUMBER() OVER (PARTITION BY ldate, userid ORDER BY CONVERT(date, ldate) desc, ltime desc) AS RN 
                    FROM Distance 
                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text +
                        @"' and userid in (select userid from users where  RM like '" + Session["sess_username"] + @"' and userid like '" + users + @"')) tbl1
                    where rn=1
                    ) tbl
                    group by userid ,ldate ) x
                    left outer join 
                    (Select count(vid) as 'Sites Audited',userid,vdate from dr_ctp dc inner join atms a on a.ATMID = dc.ATMID
                    where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                    group by userid,vdate) y
                    on x.userid=y.userid and x.ldate=y.vdate
                    order by convert(date,ldate) asc,userid asc";
            }
            else
            {
                //            sql = @"Select x.Userid,[DistanceTraveled],case when [Sites Audited] is null then '0' else [Sites Audited] end as [Sites Audited],Ldate as [Date] from
                //                    (SELECT  userid, DistanceTraveled,ldate
                //                    FROM(SELECT   Distance.*, ROW_NUMBER() OVER (PARTITION BY ldate, userid ORDER BY CONVERT(date, ldate) desc, ltime desc) AS RN 
                //                    FROM Distance 
                //                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text +
                //                    @"' and userid in (select userid from users where state in (" + txtuser.Text + @") and RM like '" + Session["sess_username"] + @"' and userid like '" + users + @"')
                //                    ) AS t 
                //                    WHERE RN = 1 ) x
                //                    left outer join 
                //                    (Select count(vid) as 'Sites Audited',userid,vdate from dr_ctp dc inner join atms a on a.ATMID = dc.ATMID
                //                    where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' and a.state in (" + txtuser.Text + @")
                //                    group by userid,vdate) y
                //                    on x.userid=y.userid and x.ldate=y.vdate
                //                    order by convert(date,ldate) asc,userid asc";

                sql = @"Select x.Userid,[DistanceTraveled],case when [Sites Audited] is null then '0' else [Sites Audited] end as [Sites Audited],Ldate as [Date] from
                    (Select userid,sum(convert(float,distancetraveled)) [DistanceTraveled],ldate from
                    (
                    Select userid,sum(convert(float,distancetraveled)) [DistanceTraveled],ldate from Distance  
                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text +
                        @"' and userid in (select userid from users where state in (" + txtuser.Text + @") and RM like '" + Session["sess_username"] + @"' and userid like '" + users + @"')
                    and vid='punchout' group by userid,ldate
                    union all
                    SELECT  userid, case when vid='PUNCHOUT' then '0' else DistanceTraveled end [DistanceTraveled],ldate
                    FROM(SELECT   Distance.*, ROW_NUMBER() OVER (PARTITION BY ldate, userid ORDER BY CONVERT(date, ldate) desc, ltime desc) AS RN 
                    FROM Distance 
                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text +
                        @"' and userid in (select userid from users where state in (" + txtuser.Text + @") and RM like '" + Session["sess_username"] + @"' and userid like '" + users + @"')) tbl1
                    where rn=1
                    ) tbl
                    group by userid ,ldate ) x
                    left outer join 
                    (Select count(vid) as 'Sites Audited',userid,vdate from dr_ctp dc inner join atms a on a.ATMID = dc.ATMID
                    where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' and a.state in (" + txtuser.Text + @")
                    group by userid,vdate) y
                    on x.userid=y.userid and x.ldate=y.vdate
                    order by convert(date,ldate) asc,userid asc";
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
                //chk123.Visible = false;
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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=DistanceMasterReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            btn_search_Click(sender, e);

            GridView1.AllowPaging = false;
            GridView1.AllowSorting = false;
            //GridView1.Columns[0].Visible = false;
            //GridView1.Columns[1].Visible = false;
            //GridView1.Columns[2].Visible = false;
            GridView1.DataBind();

            GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HyperLink hp = new HyperLink();
                //hp.Text = "EXCEL";
                //hp.NavigateUrl = "Report.aspx?auditid=" + e.Row.Cells[3].Text + "&dnld_excel=Y";
                //e.Row.Cells[0].Controls.Add(hp);

                //HyperLink hp1 = new HyperLink();
                //hp1.Text = "PHOTOS";
                //hp1.NavigateUrl = "photos.aspx?auditid=" + e.Row.Cells[3].Text + "&dnld_pdf=Y";
                //e.Row.Cells[1].Controls.Add(hp1);

                //HyperLink hp2 = new HyperLink();
                //hp2.Text = "View Report";
                //hp2.NavigateUrl = "MainPage1.aspx?auditid=" + e.Row.Cells[3].Text + "";
                //e.Row.Cells[2].Controls.Add(hp2);
            }
        }
    }
}