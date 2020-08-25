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
    public partial class UnAudited : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        DateTime toDate = DateTime.Now;
        CommonClass obj = new CommonClass();
        string role = "";
        int cnt = 0;
        string state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // defaultCalendarExtender.StartDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-(Convert.ToDouble((DateTime.Now.Date.Day)))).ToString("MM/dd/yyyy"));
            //defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            // defaultCalendarExtender1.StartDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-(Convert.ToDouble((DateTime.Now.Date.Day)))).ToString("MM/dd/yyyy"));
            //defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));


            if (!Page.IsPostBack)
            {
                txt_frmDate.Text = DateTime.Now.Date.AddDays((-(Convert.ToDouble((DateTime.Now.Date.Day)))) + 1).ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
                DropDownList1.Items.FindByText("ALL").Value = "%";
                DropDownList1.Items.FindByValue("%").Selected = true;

                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);
            }
            else
            {

            }
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
            //Response.Write(q);
            ImageButton2.Visible = true;
            string q = GetQuery();
            if (grid_unaudit.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
                Label3.Visible = false;
            }
            else
            {
                Label3.Visible = true;
                Label1.Visible = false;
                Label3.Text = bucket.CountRows(grid_unaudit, Label3);
            }



        }

        protected string GetQuery()
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
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

            string q = "";
            if (txtuser.Text == "")
            {
                q = @"select distinct a.atmid as [Atmid] ,a.location as Location,a.bankid as [Bank],region as [Region],a.siteid,a.onoffsite as [Site Type],a.state 
                     from atms a,Users u
                     where atmstatus <> 'Inactive' and u.userid like '" + users +
                              "' and " + role + " and a.atmid not in (select distinct atmid from dr_ctp where convert(date,vdate) between '" + txt_frmDate.Text +
                              "' and '" + txt_toDate.Text + "')";
            }
            else
            {
                q = @"select distinct a.atmid as [Atmid] ,a.location as Location,a.bankid as [Bank],region as [Region],a.siteid,a.onoffsite as [Site Type],a.state 
                     from atms a,Users u
                     where atmstatus <> 'Inactive' and u.userid like '" + users +
                             "' and " + role + " and a.atmid not in (select distinct atmid from dr_ctp where convert(date,vdate) between '" + txt_frmDate.Text +
                             "' and '" + txt_toDate.Text + "') and a.state in (" + txtuser.Text + ")";
            }

            bucket.BindGrid(grid_unaudit, q);
            return q;
        }
        protected void btn_search_Click1(object sender, EventArgs e)
        {
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
                                ws.Cells[1, 1, 12500, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
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
                                Response.AddHeader("content-disposition", "attachment;  filename=UnAuditedSitesReport.xlsx");
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.BinaryWrite(reportData);
                                Response.End();

                            }
                        }
                    }
                }
            }
            //grid_unaudit.AllowPaging = false;

            //grid_unaudit.DataBind();
            //grid_unaudit.GridLines = GridLines.Both;
            //Response.AddHeader("content-disposition", "attachment;filename=Unaudited.xls");
            //Response.Charset = String.Empty;
            //Response.ContentType = "application/vnd.xls";
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            //grid_unaudit.RenderControl(hw);
            //Response.Write(sw.ToString());
            //Response.End();
            //Response.Redirect("Unaudited.aspx");
        }

        public ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName, int sheetNumber, bool gridlines)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[sheetNumber];
            ws.Name = sheetName; //Setting Sheet's name
            ws.View.ShowGridLines = gridlines;
            return ws;
        }


        protected void grid_unaudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_unaudit.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
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