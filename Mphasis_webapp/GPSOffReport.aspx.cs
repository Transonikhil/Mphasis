using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace Mphasis_webapp
{
    public partial class GPSOffReport : System.Web.UI.Page
    {
        string month = System.DateTime.Now.Month.ToString("d2");
        string year = System.DateTime.Now.Year.ToString();

        ibuckethead bucket = new ibuckethead();

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            //defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list and keep focus on current month and year on first page load,
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");

                sql_client.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;


                SqlConnection con = new SqlConnection(sql_client.ConnectionString);


                string sql = "select '%' as userid,  'ALL' as username union all select userid, username from users where role in ('AO', 'CH', 'DE', 'RM')  and status <> 'DEL'";

                if (Session["sess_role"].ToString() == "RM")
                {

                    sql = @"select 'All' as username,'%' as userid union all select distinct u.username,u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role in ('DE','CM','AO') " +
                            " and a.RM like '" + Session["sess_username"] + "' and u.status <> 'DEL'";
                }


                if (Session["sess_role"].ToString() == "CH")
                {
                    sql = " select 'All' as username,'%' as 'userid' union all select distinct u.username,u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role in ('DE','CM','AO','CH','RM') " +
                            " and a.CH like '" + Session["sess_username"] + "' and u.status <> 'DEL'";
                }

                con.Open();
                //SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dd_officer.DataSource = dt;
                dd_officer.DataTextField = "username";
                dd_officer.DataValueField = "userid";
                dd_officer.DataBind();


                dd_client_SelectedIndexChanged(null, null);
                btn_search_Click(null, null);

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
                    imgexcel.Style.Add("display", "none");

                }
                else
                {
                    Label3.Visible = true;
                    Label1.Visible = false;
                    Label3.Text = bucket.CountRows(GridView1, Label3);// GridView1.Rows.Count.ToString() + " records matching your criteria.";
                    imgexcel.Style.Add("display", "");

                }
                /*------------------------------------------------------------------------------------------------*/
            
        }

        protected string GetQuery()
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            hdnlocation.Value = Request.Form[dd_officer.UniqueID];
            string user = "";
            
            
                string sql = "";

                if (hdnlocation.Value == "")
                {
                    user = "%";
                }
                else
                {
                    user = hdnlocation.Value;
                }

                if (user == "%")
                {
                    sql = @"select i.userid as [User ID], u.username as [Username],
                        convert(varchar(11),convert(date,convert(varchar(10),[datetime])),106) as [Date], 
                        convert(varchar(8),convert(time,[datetime])) as [Time],
                        type as [Type],logstatus as [Status], convert(varchar(11),serverdatetime,106) + ' ' + convert(varchar(5),convert(time,[serverdatetime])) [Uploaded On Server]
                        from internet i join users u on i.userid=u.userid where 
                        internetstatus is null and convert(date,convert(varchar(10),[datetime]))
                        between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + @"' and  i.userid like '%'  order by i.userid asc,serverdatetime asc";
                }
                else
                {
                    sql = @"select i.userid as [User ID], u.username as [Username],
                        convert(varchar(11),convert(date,[datetime]),106) as [Date],
                        convert(varchar(8),convert(time,[datetime])) as [Time],type as [Type],logstatus as [Status],
                        convert(varchar(11),serverdatetime,106) + ' ' + convert(varchar(5),convert(time,[serverdatetime])) [Uploaded On Server] 
                        from internet i join users u on i.userid=u.userid where 
                        internetstatus is null and convert(date,convert(varchar(10),[datetime]))
                        between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + @"' and  i.userid like '" + user + "'  order by i.userid asc,serverdatetime asc";

                    //convert(varchar(11),convert(date,[datetime],103),101)
                    //                        between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + @"' and  i.userid like '" + user + "' and (internetstatus='OFF' or gpsstatus='OFF') order by i.userid asc";

                }

                if (Session["sess_role"].ToString() == "RM")
                {
                    sql = sql.Replace("i.userid like '" + user + "'", "(u.RM = '" + Session["sess_username"] + "' or u.userid= '" + Session["sess_userid"] + "') ");
                }

                if (Session["sess_role"].ToString() == "CH")
                {
                    sql = sql.Replace("i.userid like '" + user + "'", "(u.CH = '" + Session["sess_username"] + "' and u.userid like '" + user + "') and u.role in ('DE','CM','AO','CH')");
                }



                /*------------------------------------------------------------------------------------------------*/
                /* Bind query to grid view
                /*------------------------------------------------------------------------------------------------*/
                bucket.BindGrid(GridView1, sql);
                return sql;
            }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //Response.AddHeader("content-disposition", "attachment;filename=UserwiseReport.xls");
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
                                ws.Cells[1, 1, 200, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
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
                                Response.AddHeader("content-disposition", "attachment;  filename=MannualInternetAndGPSOffReport.xlsx");
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.BinaryWrite(reportData);
                                Response.End();

                            }
                        }
                    }
                }
            }

            //GridView1.AllowPaging = false;
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateLocation(string category)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            //String strQuery = @"select Convert(varchar(50),subcatid) as subcatid ,subcategoryname from subcategory where Convert(varchar(50),zoneid) = '" + shift.Split('|')[0] + "' and Convert(varchar(50),shiftid)= '" + shift.Split('|')[1] + "' and Convert(varchar(50),catid) = '" + category + "'";

            String strQuery = @"select distinct userid as val, upper(username) as txt  FROM [users] WHERE (role in ('Supervisor','HK','ACCOUNT','NR'))  and 
                            userid in (select userid from regionmap where region like '" + category + @"')
                             and status<>'DEL' order by txt";

            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strQuery;
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    int c = 0;
                    while (sdr.Read())
                    {
                        list.Add(new System.Web.UI.WebControls.ListItem(
                       sdr["txt"].ToString(),
                       sdr["val"].ToString()
                        ));
                        c++;
                    }

                    if (c > 1 || c == 0)
                    {
                        list.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "%"));
                    }

                    con.Close();
                    return list;
                }
            }
        }

        protected void exel_Click(object sender, EventArgs e)
        {
            // hdnlocation.Value = Request.Form[dd_officer.UniqueID];

            btn_search_Click(sender, e);

            GridView1.AllowPaging = false;
            GridView1.DataBind();


            // GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment;filename=InternetGPSOffReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void dd_client_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    string role = "";




            //    String strConnString = ConfigurationManager
            //        .ConnectionStrings["SQLConnstr"].ConnectionString;
            //    SqlConnection con= new SqlConnection(strConnString);
            //    string sql = "select '%' as userid,  'ALL' as username union all select userid, username from users where role like '"+dd_role.SelectedValue+"'  and status <> 'DEL'";

            //    if (dd_role.SelectedValue == "%")
            //    {
            //        role = "role in ('AO', 'CH', 'DE', 'RM')";

            //        sql = sql.Replace("role like '" + dd_role.SelectedValue + "'", role);
            //    }

            //    if (Session["sess_role"] == "RM")
            //    {
            //        sql = sql.Replace("status <> 'DEL'", "status <> 'DEL' and rm = '" + Session["sess_username"] + "'"); 
            //    }
            //    if (Session["sess_role"] == "CH")
            //    {
            //        sql = sql.Replace("status <> 'DEL'", "status <> 'DEL' and ch = '" + Session["sess_username"] + "'");
            //    }

            //    con.Open();
            //    //SqlCommand cmd = new SqlCommand(sql, con);
            //    SqlDataAdapter da = new SqlDataAdapter(sql, con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    dd_officer.DataSource = dt;
            //    dd_officer.DataTextField = "username";
            //    dd_officer.DataValueField = "userid";
            //    dd_officer.DataBind();

        }
    }
}