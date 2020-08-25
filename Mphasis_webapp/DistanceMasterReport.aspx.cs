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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;

namespace Mphasis_webapp
{
    public partial class DistanceMasterReport : System.Web.UI.Page
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
                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);
            }
            else
            {

            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }

        [System.Web.Services.WebMethod]

        public static ArrayList PopulateUser(string state)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all SELECT  distinct userid as userid,userid FROM [users] ";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " where state in(" + state + ")";
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

            ImageButton1.Visible = true;
            string sql = GetQuery();
            if (GridView1.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
                Label3.Visible = false;
                div1.Visible = true;
                //chk123.Visible = false;
            }
            else
            {
                div1.Visible = true;
                Label3.Visible = true;
                Label1.Visible = false;
                Label3.Text = bucket.CountRows(GridView1, Label3);
            }


        }

        protected string GetQuery()
        {
            #region Code to fetch data from DR_CTP

            string sql = "";
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;
            if (users == "All")
            {
                users = "%";
            }
            if (txtuser.Text != "")
            {
                //            sql = @"Select x.Userid,[DistanceTraveled],case when [Sites Audited] is null then '0' else [Sites Audited] end as [Sites Audited],convert(varchar(10),convert(date,Ldate),103) as [Date] from
                //                    (SELECT  userid, DistanceTraveled,ldate
                //                    FROM(SELECT   Distance.*, ROW_NUMBER() OVER (PARTITION BY ldate, userid ORDER BY CONVERT(date, ldate) desc, ltime desc) AS RN 
                //                    FROM Distance 
                //                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                //                    and userid in (select userid from users where state in (" + txtuser.Text + @") and userid like '"+users+@"')
                //                    ) AS t 
                //                    WHERE RN = 1 ) x
                //                    left outer join 
                //                    (Select count(vid) as 'Sites Audited',userid,vdate from dr_ctp dc inner join atms a on a.ATMID = dc.ATMID
                //                    where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "' and a.state in (" + txtuser.Text + @")
                //                    group by userid,vdate) y
                //                    on x.userid=y.userid and x.ldate=y.vdate
                //                    order by convert(date,ldate) asc,userid asc";

                sql = @"Select x.Userid,[DistanceTraveled],case when [Sites Audited] is null then '0' else [Sites Audited] end as [Sites Audited],convert(varchar(10),convert(date,Ldate),103) as [Date] from
                    (Select userid,sum(convert(float,distancetraveled)) [DistanceTraveled],ldate from
                    (
                    Select userid,sum(convert(float,distancetraveled)) [DistanceTraveled],ldate from Distance where 
                    userid in (select userid from users where state in (" + txtuser.Text + @") and userid like '" + users + @"')
                    and convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                    and vid='punchout' group by userid,ldate
                    union all
                    SELECT  userid, case when vid='PUNCHOUT' then '0' else DistanceTraveled end [DistanceTraveled],ldate
                    FROM(SELECT   Distance.*, ROW_NUMBER() OVER (PARTITION BY ldate, userid ORDER BY CONVERT(date, ldate) desc, ltime desc) AS RN 
                    FROM Distance 
                    where userid in (select userid from users where state in (" + txtuser.Text + @") and userid like '" + users + @"')
                    and convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"') tbl1
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
            else
            {
                //            sql = @"Select x.Userid,[DistanceTraveled],case when [Sites Audited] is null then '0' else [Sites Audited] end as [Sites Audited],convert(varchar(10),convert(date,Ldate),103) as [Date] from
                //                    (SELECT  userid, DistanceTraveled,ldate
                //                    FROM(SELECT   Distance.*, ROW_NUMBER() OVER (PARTITION BY ldate, userid ORDER BY CONVERT(date, ldate) desc, ltime desc) AS RN 
                //                    FROM Distance 
                //                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                //                    and userid in (select userid from users where userid like '" + users + @"')
                //                    ) AS t 
                //                    WHERE RN = 1 ) x
                //                    left outer join 
                //                    (Select count(vid) as 'Sites Audited',userid,vdate from dr_ctp dc inner join atms a on a.ATMID = dc.ATMID
                //                    where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                //                    group by userid,vdate) y
                //                    on x.userid=y.userid and x.ldate=y.vdate
                //                    order by convert(date,ldate) asc,userid asc";

                sql = @"Select x.Userid,[DistanceTraveled],case when [Sites Audited] is null then '0' else [Sites Audited] end as [Sites Audited],convert(varchar(10),convert(date,Ldate),103) as [Date] from
                    (Select userid,sum(convert(float,distancetraveled)) [DistanceTraveled],ldate from
                    (
                    Select userid,sum(convert(float,distancetraveled)) [DistanceTraveled],ldate from Distance where convert(date,Ldate) between '" +
                        txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                    and userid in (select userid from users where userid like '" + users + @"')
                    and vid='punchout' group by userid,ldate
                    union all
                    SELECT  userid, case when vid='PUNCHOUT' then '0' else DistanceTraveled end [DistanceTraveled],ldate
                    FROM(SELECT   Distance.*, ROW_NUMBER() OVER (PARTITION BY ldate, userid ORDER BY CONVERT(date, ldate) desc, ltime desc) AS RN 
                    FROM Distance 
                    where convert(date,Ldate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                    and userid in (select userid from users where userid like '" + users + @"')) tbl1
                    where rn=1
                    ) tbl
                    group by userid ,ldate
                    ) AS x
                    left outer join 
                    (Select count(vid) as 'Sites Audited',userid,vdate from dr_ctp dc inner join atms a on a.ATMID = dc.ATMID
                    where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' 
                    group by userid,vdate) y
                    on x.userid=y.userid and x.ldate=y.vdate
                    order by convert(date,ldate) asc,userid asc";
            }

            //       string sql = @"SELECT distinct(case when d.Siteid='NA' then Vid else d.Siteid end) as [SITE ID], d.USERID AS [USER ID],
            //                       case when d.Siteid='NA' then d.address  else a.addressline1 end  AS LOCATION,a.city as CITY,case when d.Siteid='NA' then '-'  else a.bankid end AS [BANK NAME], 
            //                       convert(varchar(10),convert(date,ldate),103) AS [DATE OF VISIT], d.ltime AS [TIME OF VISIT],DistanceTraveled as [DISTANCE  IN KM]
            //                       from Distance d left outer join atms a on d.siteid=a.atmid join users u on d.userid=u.userid where d.userid like '" + DropDownList1.SelectedValue + "' and " + role + " and convert(date,ldate) = '" +
            //                       txt_frmDate.Text + "' order by d.ltime desc";

            bucket.BindGrid(GridView1, sql);
            return sql;
            #endregion
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //Response.AddHeader("content-disposition", "attachment;filename=DistanceMasterReport.xls");
            //Response.Charset = String.Empty;
            //Response.ContentType = "application/vnd.xls";
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            btn_search_Click(sender, e);

            string constr = ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(GetQuery()))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                          
                            using (ExcelPackage pck = new ExcelPackage())
                            {
                                ExcelWorksheet ws = CreateSheet(pck, "Report", 1, true);
                                //ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                                ws.Cells["A1"].LoadFromDataTable(dt, true);
                                var start = ws.Dimension.Start;
                                var end = ws.Dimension.End;
                                ws.Cells[1, 1, 50, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();


                                for (var j = 0; j < dt.Columns.Count; j++)
                                {
                                    ws.Cells[1, j + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    ws.Cells[1, j + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkBlue);
                                    ws.Cells[1, j + 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                    ws.Cells[1, j + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    ws.Cells[1, j + 1].Style.Border.BorderAround(ExcelBorderStyle.Medium, System.Drawing.Color.Black);

                                }

                                byte[] reportData = pck.GetAsByteArray();
                                Response.Clear();
                                Response.AddHeader("content-disposition", "attachment;  filename=DistanceMasterReport.xlsx");
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.BinaryWrite(reportData);
                                Response.End();

                            }
                        }
                    }
                }
            }

                //GridView1.AllowPaging = false;
                //GridView1.AllowSorting = false;
                //GridView1.Columns[0].Visible = false;
                //GridView1.Columns[1].Visible = false;
                //GridView1.Columns[2].Visible = false;
                //GridView1.DataBind();

                //GridView1.RenderControl(hw);
                //Response.Write(sw.ToString());
                //Response.End();

        }

        public ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName, int sheetNumber, bool gridlines)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[sheetNumber];
            ws.Name = sheetName; //Setting Sheet's name
            ws.View.ShowGridLines = gridlines;
            return ws;
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