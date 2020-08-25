using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace Mphasis_webapp
{
    public partial class TodaysAudits : System.Web.UI.Page
    {
        string month = System.DateTime.Now.Month.ToString("d2");
        string year = System.DateTime.Now.Year.ToString();
        int cnt = 0;
        string state = "";
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        DateTime toDate = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            string year = "";
            if (System.DateTime.Now.ToString("MM") == "01")
            {
                year = System.DateTime.Now.AddYears(-1).ToString("yyyy");
            }
            else
            {
                year = System.DateTime.Now.ToString("yyyy");
            }


            //CalendarExtender1.StartDate = Convert.ToDateTime(System.DateTime.Now.AddMonths(-1).ToString("MM") + "/" + "01/" + year);
            //CalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            //CalendarExtender2.StartDate = Convert.ToDateTime(System.DateTime.Now.AddMonths(-1).ToString("MM") + "/" + "01/" + year);
            //CalendarExtender2.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            if (Page.IsPostBack)
            {

            }
            else
            {
                txt_frmDate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");
                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double version = Convert.ToDouble(e.Row.Cells[8].Text);

                HyperLink report = new HyperLink();
                report.Text = "Download";
                report.NavigateUrl = "http://s5.transovative.com/Mphasis/Report.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&atmid=" + e.Row.Cells[4].Text.Trim() + "&dnld_excel=Y";

                e.Row.Cells[0].Controls.Add(report);

                HyperLink download = new HyperLink();
                download.Text = "Download";
                download.NavigateUrl = "http://s5.transovative.com/Mphasis/photos.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&dnld_pdf=Y";

                e.Row.Cells[1].Controls.Add(download);

                HyperLink vid = new HyperLink();
                vid.Text = e.Row.Cells[2].Text;
                if (version > 3.9)
                {
                    vid.NavigateUrl = "http://s5.transovative.com/mphasiswebapp/MainPageV2.aspx?auditid=" + vid.Text;
                }
                else
                {
                    vid.NavigateUrl = "http://s5.transovative.com/mphasiswebapp/MainPage1.aspx?auditid=" + vid.Text;
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
        protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            //e.Command.Parameters["@atmid"].Value = dd_atm.SelectedValue.ToString();
            //e.Command.Parameters["@from"].Value = txt_frmDate.Text;
            //e.Command.Parameters["@to"].Value = txt_toDate.Text;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //Response.AddHeader("content-disposition", "attachment;filename=MasterReport.xls");
            //Response.Charset = String.Empty;
            //Response.ContentType = "application/vnd.xls";

            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            Button1_Click(sender, e);
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
                                ws.Cells[1, 1, 100, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
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
                                Response.AddHeader("content-disposition", "attachment;  filename=ATMwise-AuditReportForAPeriod.xlsx");
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.BinaryWrite(reportData);
                                Response.End();

                            }
                        }
                    }
                }
            }

            //GridView1.AllowPaging = false;
            //GridView1.RenderControl(hw);
            //GridView1.DataBind();

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            ImageButton1.Visible = true;
            string q = GetQuery();
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
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            if (string.IsNullOrEmpty(txt_frmDate.Text.Trim()))
            {
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }



            string q = "";

            if (txtuser.Text != "")
            {
                q = @"SELECT '' as [REPORT],'' as [DOWNLOAD],c.Vid AS [VISIT ID],a.siteid,a.Location, a. Bankid as [Bank], a.Client as [Client], convert(varchar(10),convert(date,vdate),103)+' '+vtime as [Audit Date TIme] ,dbo.udf_GetNumeric(version) as [VERSION]
                from DR_CTP c, ATMs a where c.atmid=a.atmid AND (c.ATMID like '" + dd_atm.Text + "' or a.siteid like '" + dd_atm.Text + "') and Convert(date,vdate) between '" + txt_frmDate.Text +
                    "' and '" + txt_toDate.Text + "' and a.state in (" + txtuser.Text + ")";
            }
            else
            {
                q = @"SELECT '' as [REPORT],'' as [DOWNLOAD],c.Vid AS [VISIT ID],a.siteid,a.Location, a. Bankid as [Bank], a.Client as [Client], convert(varchar(10),convert(date,vdate),103)+' '+vtime as [Audit Date TIme] ,dbo.udf_GetNumeric(version) as [VERSION]
                from DR_CTP c, ATMs a where c.atmid=a.atmid AND (c.ATMID like '" + dd_atm.Text + "' or a.siteid like '" + dd_atm.Text + "') and Convert(date,vdate) between '" + txt_frmDate.Text +
                    "' and '" + txt_toDate.Text + "'";
            }

            bucket.BindGrid(GridView1, q);
            return q;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = @"select distinct atmid from ATMs where " + @"ltrim(rtrim(atmid)) like @SearchText + '%'
                                   union all
                                   select distinct siteid from ATMs where " + "ltrim(rtrim(siteid)) like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);
                    List<string> CountryNames = new List<string>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CountryNames.Add(dt.Rows[i][0].ToString());
                    }
                    conn.Close();
                    return CountryNames;
                }
            }
        }
    }
}