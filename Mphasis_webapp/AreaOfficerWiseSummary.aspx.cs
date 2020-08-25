using System;
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
    public partial class AreaOfficerWiseSummary : System.Web.UI.Page
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
                //defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                //defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            }
            catch (Exception ex) { }
            // defaultCalendarExtender.EndDate = toDate;
            //defaultCalendarExtender1.EndDate = toDate;

            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list and keep focus on current month and year on first page load,
            /*-----------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);
            }
            /*------------------------------------------------------------------------------------------------*/
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
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");


           
            
                string sql = "";
                if (txtuser.Text != "")
                {
                    sql = @"select u.userid as [area officer],username as [User Name],COUNT(d.vid) as [visit] ,convert(varchar(10),convert(date,vdate),103) as vdate
                            from DR_CTP d, users u where d.USERID=u.userid and Convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + @"' 
                            and state in (" + txtuser.Text + @")
                            group by u.userid ,vdate,username
                            order by CONVERT(date, vdate) desc";
                }
                else
                {
                    sql = @"select u.userid as [area officer],username as [User Name],COUNT(d.vid) as [visit] ,convert(varchar(10),convert(date,vdate),103) as vdate
                            from DR_CTP d, users u where d.USERID=u.userid and Convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + @"' 
                            and state is not null
                            group by u.userid ,vdate,username
                            order by CONVERT(date, vdate) desc";
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
                           
                            using (ExcelPackage pck = new ExcelPackage())
                            {
                                ExcelWorksheet ws = CreateSheet(pck, "Report", 1, true);
                                //ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                                ws.Cells["A1"].LoadFromDataTable(dt, true);
                                var start = ws.Dimension.Start;
                                var end = ws.Dimension.End;
                                ws.Cells[1, 1, 30, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
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
                                Response.AddHeader("content-disposition", "attachment;  filename=AreaOfficerwiseSummeryReport.xlsx");
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
    }
}