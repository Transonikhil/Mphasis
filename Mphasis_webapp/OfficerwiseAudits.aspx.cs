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
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace Mphasis_webapp
{
    public partial class OfficerwiseAudits : System.Web.UI.Page
    {
        string month = System.DateTime.Now.Month.ToString("d2");
        string year = System.DateTime.Now.Year.ToString();
        string role = "";
        int cnt = 0;
        string state = "";
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        DateTime toDate = DateTime.Now;

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            }
            catch (Exception ex) { }
            // defaultCalendarExtender.EndDate = toDate;
            //defaultCalendarExtender1.EndDate = toDate;

            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list and keep focus on current month and year on first page load,
            /*-----------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
                DropDownList1.Items.FindByText("ALL").Value = "%";
                DropDownList1.Items.FindByValue("%").Selected = true;

                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);
            }

            //GridView1.DataSource = GetProducts();
            //GridView1.DataBind();
            /*------------------------------------------------------------------------------------------------*/
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

            ImageButton1.Visible = true;
            string sql = GetQuery();
            if (GridView1.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
                Label3.Visible = false;
            }
            else
            {
                Label3.Visible = true;
                Label1.Visible = false;
                Label3.Text = bucket.CountRows(GridView1, Label3);// GridView1.Rows.Count.ToString() + " records matching your criteria.";
            }

        }

        protected string GetQuery()
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                //  txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                // txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }
            if (ddrole.SelectedValue == "%")
            {
                role = "u.role in ('AO','DE','CM','RM','CH')";
            }
            else
            {
                role = "u.role like '" + ddrole.SelectedValue + "'";
            }

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;
            if (users == "All")
            {
                users = "%";
            }
            string sql = "";



            if (txtuser.Text == "")
            {
                sql = @"SELECT '' as [Report],'' as [DOWNLOAD],c.Vid AS [VISIT ID],c.ATMID as [ATM], a.Location, a. Bankid as [Bank], a.Client as [Client],  
                          convert(varchar(10),convert(date,vdate),103) as [Audit Date],vtime as [Audit Time], dbo.udf_GetNumeric(version) as [VERSION] from DR_CTP c, ATMs a, Users u where 
                          c.atmid=a.atmid and c.userid=u.userid and Convert(date,vdate) between '" + txt_frmDate.Text +
                     "' AND '" + txt_toDate.Text + "' and " + role + " and c.userid like '" + users +
                     "' order by Convert(date,vdate) desc,vtime desc";
            }
            else
            {
                sql = @"SELECT '' as [Report],'' as [DOWNLOAD],c.Vid AS [VISIT ID],c.ATMID as [ATM], a.Location, a. Bankid as [Bank], a.Client as [Client],  
                          convert(varchar(10),convert(date,vdate),103) as [Audit Date],vtime as [Audit Time],dbo.udf_GetNumeric(version) as [VERSION] from DR_CTP c, ATMs a, Users u where 
                          c.atmid=a.atmid and c.userid=u.userid and Convert(date,vdate) between '" + txt_frmDate.Text +
                      "' AND '" + txt_toDate.Text + "' and " + role + " and c.userid like '" + users +
                      "' and a.state in (" + txtuser.Text + ") order by Convert(date,vdate) desc,vtime desc";
            }

            //Response.Write(sql);
            /*------------------------------------------------------------------------------------------------*/
            /* Bind query to grid view
            /*------------------------------------------------------------------------------------------------*/
            bucket.BindGrid(GridView1, sql);
            return sql;
        }
    
            
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //Response.AddHeader("content-disposition", "attachment;filename=OfficerWiseReport.xls");
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
                            dt.Columns.Remove("Report");
                            dt.Columns.Remove("Download");

                            using (ExcelPackage pck = new ExcelPackage())
                            {
                                ExcelWorksheet ws = CreateSheet(pck, "Report", 1, true);
                                //ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                                ws.Cells["A1"].LoadFromDataTable(dt, true);
                                var start = ws.Dimension.Start;
                                var end = ws.Dimension.End;
                                ws.Cells[1, 1, 50, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
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
                                Response.AddHeader("content-disposition", "attachment;  filename=OfficerwiseAuditReport.xlsx");
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
            ////GridView1.Columns[0].Visible = false;
            ////GridView1.Columns[1].Visible = false;
            ////GridView1.Columns[2].Visible = false;
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //HyperLink hp = new HyperLink();
            //    //hp.Text = "EXCEL";
            //    //hp.NavigateUrl = "Report.aspx?auditid=" + e.Row.Cells[3].Text + "&dnld_excel=Y";
            //    //e.Row.Cells[0].Controls.Add(hp);

            //    //HyperLink hp1 = new HyperLink();
            //    //hp1.Text = "PHOTOS";
            //    //hp1.NavigateUrl = "photos.aspx?auditid=" + e.Row.Cells[3].Text + "&dnld_pdf=Y";
            //    //e.Row.Cells[1].Controls.Add(hp1);

            //    //HyperLink hp2 = new HyperLink();
            //    //hp2.Text = "View Report";
            //    //hp2.NavigateUrl = "MainPage1.aspx?auditid=" + e.Row.Cells[3].Text + "";
            //    //e.Row.Cells[2].Controls.Add(hp2);
            //}



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double version = Convert.ToDouble(e.Row.Cells[9].Text);

                HyperLink report = new HyperLink();
                report.Text = "Download";
                report.NavigateUrl = "Report.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&atmid=" + e.Row.Cells[4].Text.Trim() + "&dnld_excel=Y";

                e.Row.Cells[0].Controls.Add(report);

                HyperLink download = new HyperLink();
                download.Text = "Download";
                download.NavigateUrl = "photos.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&dnld_pdf=Y";

                e.Row.Cells[1].Controls.Add(download);

                HyperLink vid = new HyperLink();
                vid.Text = e.Row.Cells[2].Text;
                if (version > 3.9)
                {
                    vid.NavigateUrl = "http://s5.transovative.com/Mphasis/MainPageV2.aspx?auditid=" + vid.Text;
                }
                else
                {
                    vid.NavigateUrl = "http://s5.transovative.com/Mphasis/MainPage1.aspx?auditid=" + vid.Text;
                }

                e.Row.Cells[2].Controls.Add(vid);

            }
                
            

            if (obj.GetPostBackControlId(this.Page) == "ImageButton1")
            {
                if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                }
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