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
    public partial class AuditForAPeriod : System.Web.UI.Page
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
            try
            {
                CalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                CalendarExtender2.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            }
            catch (Exception ex) { }
            //CalendarExtender1.EndDate = toDate;
            //CalendarExtender2.EndDate = toDate;
            if (!Page.IsPostBack)
            {
                dd_bank.DataBind();

                dd_bank.Items.Add("ALL");
                dd_bank.Items.FindByText("ALL").Value = "%";
                dd_bank.Items.FindByValue("%").Selected = true;

                dd_cust.DataBind();

                dd_cust.Items.Add("ALL");
                dd_cust.Items.FindByText("ALL").Value = "%";
                dd_cust.Items.FindByValue("%").Selected = true;

                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);
            }

            if (Page.IsPostBack)
            {
                if (Request.QueryString["export"] == "true")
                {
                    btn_Update_Click(sender, e);
                    GridView1.AllowPaging = false;
                    GridView1.DataBind();
                    Response.AddHeader("content-disposition", "attachment;filename=AuditReport.xls");
                    Response.Charset = String.Empty;
                    Response.ContentType = "application/vnd.xls";
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView1.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();
                    Response.Redirect("auditforaperiod.aspx");
                }
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            ImageButton2.Visible = true;
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
                Label3.Text = bucket.CountRows(GridView1, Label3); // GridView1.Rows.Count.ToString() + " records matching your criteria.";
            }
            /*------------------------------------------------------------------------------------------------*/
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

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            string sql = @"SELECT '' as [REPORT],'' as [DOWNLOAD],c.Vid AS [VISIT ID],c.ATMID as [ATM], a.Location, a. Bankid as [Bank], a.Client as [Client], 
                         convert(varchar(10),convert(date,vdate),103)+' '+vtime as [Audit Date TIme],dbo.udf_GetNumeric(version) as [VERSION] from DR_CTP c, atms a where c.atmid=a.atmid AND a.client like '" + dd_cust.SelectedItem.Value.ToString() + @"' 
                         and a.bankid like '" + dd_bank.SelectedItem.Value.ToString() +
                        "' and convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + "'";

            if (txtuser.Text != "")
            {
                sql += " and a.state in (" + txtuser.Text + ") ";
            }
            // Response.Write(sql);
            /*------------------------------------------------------------------------------------------------*/
            /* Bind query to grid view
            /*------------------------------------------------------------------------------------------------*/
            bucket.BindGrid(GridView1, sql);
            return sql;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
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
                                ws.Cells[1, 1, 50, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
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
                                Response.AddHeader("content-disposition", "attachment;  filename=AuditForAPeroidReport.xlsx");
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.BinaryWrite(reportData);
                                Response.End();

                            }
                        }
                    }
                }
            }
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
            btn_Update_Click(sender, e);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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

    }
}
