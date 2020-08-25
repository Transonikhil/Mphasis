using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml;

namespace Mphasis_webapp
{
    public partial class Memo : System.Web.UI.Page
    {
        CommonClass obj = new CommonClass();
        ibuckethead bucket = new ibuckethead();
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        List<string> userlist = new List<string>();
        // string date = System.DateTime.Now.ToShortDateString();
        string date = DateTime.Now.ToString("MM/dd/yyyy");
        string time = System.DateTime.Now.ToShortTimeString();
        string users = "";
        public string clean(string a)
        {
            return Regex.Replace(a, "a-zA-Z0-9,", "", RegexOptions.Compiled);
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUsers(string category, string roles)
        {

            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            String strQuery = "";
            string sesrole = HttpContext.Current.Session["sess_role"].ToString();

            strQuery = @"select distinct userid as val, upper(username) as txt  FROM [users] WHERE (role in ('AO','DE','CM','CH','RM') and role like '" + roles + @"')  and 
                           state like '" + category + @"' ";

            if (sesrole == "RCM")
            {
                strQuery += " and RCM like '" + HttpContext.Current.Session["sess_username"].ToString() + "'";
            }
            else if (sesrole == "RM")
            {
                strQuery += " and RM like '" + HttpContext.Current.Session["sess_username"].ToString() + "'";
            }
            else if (sesrole == "CH")
            {
                strQuery += " and CH like '" + HttpContext.Current.Session["sess_username"].ToString() + "'";
            }
            strQuery += " and status<>'DEL' order by txt";


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

                    if (c > 1)
                    {
                        list.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "--Select--"));
                        list.Insert(1, new System.Web.UI.WebControls.ListItem("ALL", "%"));
                    }
                    else if (c == 0)
                    {
                        list.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "--Select--"));
                    }
                    else if (c == 1)
                    {
                        list.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "--Select--"));
                    }

                    con.Close();
                    return list;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fte6.InvalidChars = @"`~!@#$%^&*()_+{}:<>?[]-=\;'./""";
            fte7.InvalidChars = @"`~!@#$%^&*()_+{}:<>?[]-=\;'/""";

            if (!Page.IsPostBack)
            {
                //ddlist.DataBind();
                //ddlist.Items.Add("All Users");
                //ddlist.Items.Add("--Select--");
                //ddlist.Items.FindByValue("--Select--").Selected = true;


                //SqlDataSource2.SelectCommand = "Select USERID,MEMOREMARK,SentOn,SEENDATE,SentBY,STATUS from Memo where CONVERT(DATE,convert(varchar(11),SENTON),103)='" + date + "'";
                //GridView1.DataBind();
                if (String.IsNullOrEmpty(txtdate.Text.Trim()))
                {
                    txtdate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");
                }

                search_Click(sender, e);

            }
            //ddUser.DataBind();
            calextDate.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            search_Click(sender, e);
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            hdnUsers.Value = Request.Form[ddUser.UniqueID];

            string a = txtremark.Text;
            string userid = "";
            string upd = "";
            string str = "";


            if (!String.IsNullOrEmpty(txtuserid.Text.Trim()) && !String.IsNullOrEmpty(txtremark.Text.Trim()))
            {
                if (txtuserid.Text.Contains(","))
                {
                    foreach (var x in txtuserid.Text.Split(','))
                    {
                        str = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                            IF EXISTS (SELECT userid FROM Users where userid like '" + x.Trim() + "' and role in ('AO','CM','DE','CH','RM')) BEGIN Insert into Memo (userid,MemoRemark,SentOn,SentBy) values ('" + x.Trim() + "','" + txtremark.Text + "',getDate(),'" + Session["sess_userid"] + "') END  COMMIT TRANSACTION";

                        if (obj.NonExecuteQuery(str) == -1)
                        {
                            // lblDiv.Style.Value = "display:block";
                            lbl_usernotFound.Text += x.ToString().Trim() + ",";
                        }
                        else
                        {
                            users += x.ToString().Trim() + ",";
                        }
                        users = users.Remove(users.LastIndexOf(","));
                    }
                }
                else
                {
                    str = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                        IF EXISTS (SELECT userid FROM Users where userid like '" + txtuserid.Text.Trim() + "' and role in ('AO','CM','DE','CH','RM')) BEGIN Insert into Memo (userid,MemoRemark,SentOn,SentBy) values ('" + txtuserid.Text.Trim() + "','" + txtremark.Text + "',getdate(),'" + Session["sess_userid"] + "') END  COMMIT TRANSACTION";

                    if (obj.NonExecuteQuery(str) == -1)
                    {
                        //lblDiv.Style.Value = "display:block";
                        lbl_usernotFound.Text = txtuserid.Text.ToString().Trim();
                    }
                    else
                    {
                        users = userid.ToString().Trim();
                    }
                }
                //Response.Write(str);

                lbl_usernotFound.Text = lbl_usernotFound.Text.TrimEnd(',');

                if (lbl_usernotFound.Text.Trim() != "")
                {
                    // lblDiv.Style.Value = "display:block";
                    lbl_usernotFound.Text = "Invalid User(s) : " + lbl_usernotFound.Text;
                    //upd = "Update Users set status='MOD',datastatus='MOD' where userid like '" + a.Trim() + "'";
                    //obj.NonExecuteQuery(upd);
                }
                else
                {

                    Response.Write("<script type='text/javascript'>alert('Memo Remark Successfully Sent to Selected Users')</script>");
                    txtuserid.Text = "";
                    txtremark.Text = "";
                    ddlState.Items.Clear();
                    ddlState.DataBind();
                    hdnUsers.Value = "";
                    // dduser.DataBind();
                }
            }
            //if ((users != "")&&(txtremarkark.Text!=""))
            //{
            //    SqlDataSource2.SelectCommand = "Select USERID,MEMOREMARK,SENDDATE,SENDTIME,SEENDATE,SEENTIME,STATUS from Memo where SendDate='" + date + "' and SendTime='" + time + "'";
            //    GridView1.DataBind();
            //    txtuseridid.Text = "";
            //    txtremarkark.Text = "";
            //   // ddlist.Items.FindByValue("--Select--").Selected = true;
            //    Response.Write("<script type='text/javascript'>alert('Memo Remark Successfully Sent to below Users:- "+users+"')</script>");
            //}
            //if (lbl_usernotFound.Text != "")
            //{
            //    lbl_usernotFound.Text = "Invalid Users " + lbl_usernotFound.Text;
            //}
            search_Click(sender, e);
        }
        protected void OnDataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //for (int i = 0; i < GridView1.Columns.Count; i++)
            //{
            //int i = 0;
            TableHeaderCell cell = new TableHeaderCell();
            cell.ColumnSpan = 9;
            TextBox txtSearch = new TextBox();
            txtSearch.Attributes["placeholder"] = GridView1.Columns[0].HeaderText;
            txtSearch.CssClass = "search_textbox";
            Label lbl = new Label();
            lbl.Text = "ENTER USERID : ";
            cell.Controls.Add(lbl);
            cell.Controls.Add(txtSearch);
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.Height = 50;
            //cell.BorderStyle = BorderStyle.None;
            //cell.BorderWidth = 0;
            //cell.Attributes.Add("style", "border:0px solid transparent");
            row.Controls.Add(cell);
            //}
            if (GridView1.Rows.Count.Equals(0))
            {
            }
            else
            {
                GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
            }

        }
        protected void ddlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlist.SelectedItem.Text == "All Users")
            //{
            //    cn.Open();
            //    string users = "select userid from users where role='AO' and status<>'DEL'";
            //    SqlCommand cmd = new SqlCommand(users, cn);
            //    SqlDataReader reader = default(SqlDataReader);
            //    reader = cmd.ExecuteReader();
            //    string p = "";

            //    while (reader.Read())
            //    {

            //        p += reader[0].ToString() + ",";
            //    }
            //    txtuseridid.Text = p;
            //    reader.Close();
            //    cn.Close();
            //}
            //else
            //{
            //    cn.Open();
            //    string users = "select userid from users where AO='" + ddlist.SelectedValue + "' and status<>'DEL' and userid<>'" + ddlist.SelectedValue + "'";
            //    SqlCommand cmd = new SqlCommand(users, cn);
            //    SqlDataReader reader = default(SqlDataReader);
            //    reader = cmd.ExecuteReader();
            //    string p = "";

            //    while (reader.Read())
            //    {

            //        p += reader[0].ToString() + ",";
            //    }
            //    txtuseridid.Text = p;
            //    reader.Close();
            //    cn.Close();
            //}
        }
        protected void search_Click(object sender, EventArgs e)
        {
            string sql = @"Select USERID,MEMOREMARK,Convert(varchar(10),SENTON,103) as [Sent Date],Convert(varchar(8),SENTON,108) as [Sent Time],
        case when SEENDATE is null then 'NA' else Convert(varchar(10),SEENDATE,103)+' '+Convert(varchar(8),SEENDATE,108) end as [Seen Date],SENTBY as [Sent by],STATUS as [Status] 
        from Memo where CONVERT(DATE,convert(varchar(11),SENTON),103)='" + txtdate.Text + "' and Status='" + ddstatus.SelectedValue + "'";
            // GridView1.DataBind();
            bucket.BindGrid(GridView1, sql);
            if (GridView1.Rows.Count.Equals(0))
            {
                lblNoRecord.Visible = true;
                lblSuccess.Visible = false;
                ImageButton1.Visible = false;
            }
            else
            {

                ImageButton1.Visible = true;
                lblSuccess.Visible = true;
                lblNoRecord.Visible = false;
                lblSuccess.Text = bucket.CountRows(GridView1, lblSuccess);
            }

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            #region unused
            //    Response.AddHeader("content-disposition", "attachment;filename=MemoReport.xls");
            //    Response.Charset = String.Empty;
            //    Response.ContentType = "application/vnd.xls";
            //    System.IO.StringWriter sw = new System.IO.StringWriter();
            //    System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    search_Click(sender, e);

            //    GridView1.AllowPaging = false;
            //    GridView1.DataBind();

            //    GridView1.RenderControl(hw);
            //    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //    Response.Output.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");// add this line to fix characterset in arabic
            //    Response.Write(style);
            //    Response.Write(sw.ToString());
            //    Response.End();
            #endregion
            using (ExcelPackage p = new ExcelPackage())
            {
                try
                {

                    ExcelWorksheet ws1 = CreateSheet(p, "Report");

                    #region Firstsheet

                    CreateHeader(ws1);
                    CreateData(ws1);
                    ws1.View.ZoomScale = 100;
                    #endregion


                    try
                    {
                        byte[] reportData = p.GetAsByteArray();
                        Response.Clear();
                        Response.AddHeader("content-disposition", "attachment;  filename=MemoReport.xlsx");
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.BinaryWrite(reportData);
                        Response.End();

                    }

                    catch (Exception ex)
                    {
                        if (!(ex is System.Threading.ThreadAbortException))
                        {
                            //Other error handling code here
                        }
                    }

                    p.Dispose();
                }

                catch (Exception ee)
                {
                    //lblexception.Text = ee.Message.ToString();
                }

                finally
                {
                    p.Dispose();
                }
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //Response.Write("9");
            //    for (int i = 0; i < 7; i++)
            //    {
            //        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("&nbsp;", "NA");
            //    }
            //}
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtuserid.Text = "";
            hdnUsers.Value = Request.Form[ddUser.UniqueID];

            if (hdnUsers.Value != "")
            {
                if (hdnUsers.Value == "%")
                {
                    string sql = "Select userid from users where state like '" + ddlState.SelectedValue + "' and userid like '" + hdnUsers.Value + "' and role like (case when role like '%' then role in ('AO','CM','DE','CH','RM') else role end) and status<>'DEL'";
                    DataTable dt = new DataTable();
                    dt = obj.sdatatable(sql);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][0].ToString() != "%")
                        {
                            txtuserid.Text += dt.Rows[i][0].ToString() + ", ";
                        }
                    }

                    txtuserid.Text = txtuserid.Text.TrimEnd(' ').TrimEnd(',');
                }
                else
                {
                    txtuserid.Text = hdnUsers.Value;
                }
            }
            else
            {
                txtuserid.Text = "";
            }
        }
        protected void btnpost_Click(object sender, EventArgs e)
        {
            txtuserid.Text = "";
            hdnUsers.Value = Request.Form[ddUser.UniqueID];

            if (hdnUsers.Value != "" && hdnUsers.Value != "--Select--")
            {
                if (hdnUsers.Value == "%")
                {
                    string sql = "Select userid from users where state like '" + ddlState.SelectedValue + "' and userid like '" + hdnUsers.Value +
                        "' and (role in ('AO','CM','DE','CH','RM') and role like '" + ddrole.SelectedValue + "') and status<>'DEL'";
                    DataTable dt = new DataTable();
                    dt = obj.sdatatable(sql);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][0].ToString() != "%")
                        {
                            txtuserid.Text += dt.Rows[i][0].ToString() + ", ";
                        }
                    }

                    txtuserid.Text = txtuserid.Text.TrimEnd(' ').TrimEnd(',');
                }
                else
                {
                    txtuserid.Text = hdnUsers.Value;
                }
            }
            else
            {
                txtuserid.Text = "";
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            search_Click(sender, e);
        }
        private void checkyesno(ExcelWorksheet ws, string a, int i, int j)
        {
            try
            {
                a = a.ToUpper();

                if (a == "N")
                {
                    ws.Cells[i, j].Value = "No";
                }
                else if (a == "Y")
                {
                    ws.Cells[i, j].Value = "Yes";
                }
                else if (a.StartsWith("Y|"))
                {
                    ws.Cells[i, j].Value = a.Replace("Y|", "Yes");
                }
                else if (a.Contains("E|Y"))
                {
                    ws.Cells[i, j].Value = a.Replace("E|Y", "Yes");
                }
                else if (a.Contains("E|Not Good"))
                {
                    ws.Cells[i, j].Value = a.Replace("E|Not Good", "Not Good-");
                }
                else if (a.StartsWith("E|N"))
                {
                    ws.Cells[i, j].Value = a.Replace("E|N", "No-");
                }
                else if (a.StartsWith("N|"))
                {
                    ws.Cells[i, j].Value = a.Replace("N|", "No");
                }
                else
                {
                    ws.Cells[i, j].Value = a;
                }
            }
            catch (Exception ee)
            {
                ws.Cells[i, j].Value = ee.Message;
            }
            ws.Cells[i, j].Style.Border.Left.Style = ws.Cells[i, j].Style.Border.Bottom.Style = ws.Cells[i, j].Style.Border.Right.Style = ws.Cells[i, j].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[i, j].Style.Font.SetFromFont(new Font("Calibri", 9));
            ws.Cells[i, j].Style.WrapText = true;
            ws.Cells[i, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[i, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
        private void CreateHeader(ExcelWorksheet ws)
        {

            ws.Row(1).Height = 42;

            ws.Cells.Style.Font.Name = "Calibri";
            ws.Cells.Style.Font.Size = 9;
            ws.Cells.Style.Font.Bold = true;
            Font font = new Font("Calibri", 9);
            Font font1 = new Font("Calibri", 10);
            Font header = new Font("Calibri", 14);
            Font headersmall = new Font("Calibri", 12);
            Font headermid = new Font("Calibri", 11);

            int col = 1;

            changeformat(ws, font, "USER ID", 1, col++, null, null, Color.White, Color.FromArgb(0, 51, 102), true);
            changeformat(ws, font, "MEMO REMARK", 1, col++, null, null, Color.White, Color.FromArgb(0, 51, 102), true);
            changeformat(ws, font1, "SEND DATE", 1, col++, null, null, Color.White, Color.FromArgb(0, 51, 102), true);
            changeformat(ws, font1, "SEND TIME", 1, col++, null, null, Color.White, Color.FromArgb(0, 51, 102), true);
            changeformat(ws, font1, "SEEN DATE", 1, col++, null, null, Color.White, Color.FromArgb(0, 51, 102), true);
            changeformat(ws, font1, "SENT BY", 1, col++, null, null, Color.White, Color.FromArgb(0, 51, 102), true);
            changeformat(ws, font1, "STATUS", 1, col++, null, null, Color.White, Color.FromArgb(0, 51, 102), true);

        }
        private void CreateData(ExcelWorksheet ws)
        {
            int sr = 0;
            string a = "";
            Font font = new Font("Calibri", 9);
            Font font1 = new Font("Calibri", 10);
            Font header = new Font("Calibri", 14);
            Font headersmall = new Font("Calibri", 9);
            Font headermid = new Font("Calibri", 11);

            try
            {
                string sql = @"Select USERID,MEMOREMARK,Convert(varchar(10),SENTON,103) as [Sent Date],Convert(varchar(8),SENTON,108) as [Sent Time],
        case when SEENDATE is null then 'NA' else Convert(varchar(10),SEENDATE,103)+' '+Convert(varchar(8),SEENDATE,108) end as [Seen Date],SENTBY as [Sent by],STATUS as [Status] 
        from Memo where CONVERT(DATE,convert(varchar(11),SENTON),103)='" + txtdate.Text + "' and Status='" + ddstatus.SelectedValue + "' ";

                DataTable dt = new DataTable();
                dt = obj.sdatatable(sql);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            checkyesno(ws, dt.Rows[i][j].ToString(), i + 2, j + 1);
                        }
                    }
                }

            }

            catch (Exception ee)
            {

            }

            finally
            {

            }

        }
        public void changeformat(ExcelWorksheet ws, Font font, string data, int rownum1, int colnum1, int? rownum2 = null, int? colnum2 = null, Color? fontcolor = null, Color? bgcolor = null, bool bold = false, ExcelVerticalAlignment vertical = ExcelVerticalAlignment.Center, ExcelHorizontalAlignment horizontal = ExcelHorizontalAlignment.Center, ExcelBorderStyle borderstyle = ExcelBorderStyle.Thin, int? txtrotate = null)
        {
            int cellAmount;
            int row2 = rownum2.HasValue ? rownum2.Value : rownum1;
            int col2 = colnum2.HasValue ? colnum2.Value : colnum1;
            int rotate = txtrotate.HasValue ? txtrotate.Value : 0;
            try
            {
                if (Int32.TryParse(data, out cellAmount))
                {

                    ws.Cells[rownum1, colnum1, row2, col2].Value = cellAmount;
                }
                else
                {
                    ws.Cells[rownum1, colnum1, row2, col2].Value = data;
                }
                ws.Cells[rownum1, colnum1, row2, col2].Style.TextRotation = rotate;

                ws.Cells[rownum1, colnum1, row2, col2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[rownum1, colnum1, row2, col2].Style.Fill.BackgroundColor.SetColor(bgcolor.HasValue ? bgcolor.Value : Color.White);
                ws.Cells[rownum1, colnum1, row2, col2].Style.Font.SetFromFont(font);
                ws.Cells[rownum1, colnum1, row2, col2].Style.Font.Color.SetColor(fontcolor.HasValue ? fontcolor.Value : Color.Black);
                ws.Cells[rownum1, colnum1, row2, col2].Style.Font.Bold = bold;
                ws.Cells[rownum1, colnum1, row2, col2].Style.WrapText = true;
                ws.Cells[rownum1, colnum1, row2, col2].Style.VerticalAlignment = vertical;
                ws.Cells[rownum1, colnum1, row2, col2].Style.HorizontalAlignment = horizontal;
                ws.Cells[rownum1, colnum1, row2, col2].Style.Border.Left.Style = ws.Cells[rownum1, colnum1, row2, col2].Style.Border.Bottom.Style = ws.Cells[rownum1, colnum1, row2, col2].Style.Border.Right.Style = ws.Cells[rownum1, colnum1, row2, col2].Style.Border.Top.Style = borderstyle;
            }
            catch (Exception ee)
            {
                ws.Cells[rownum1, colnum1, row2, col2].Value = ee.Message;
            }
        }
        private ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[1];
            ws.Name = sheetName; //Setting Sheet's name
            ws.View.ShowGridLines = false;
            return ws;
        }
    }
}