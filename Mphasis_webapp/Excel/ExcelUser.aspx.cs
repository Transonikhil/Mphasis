using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.Excel
{
    public partial class ExcelUser : System.Web.UI.Page
    {
        ibuckethead2 bucket = new ibuckethead2();
        CommonClass obj = new CommonClass();
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        DataTable add = new DataTable();
        DataTable notadd = new DataTable();
        DataTable already = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["idadd"] = null;
                Session["notidadd"] = null;
                Session["already"] = null;
                GridView1.DataBind();
                GridView2.DataBind();
                GridView3.DataBind();
            }

            add.Columns.Add("Userid", typeof(string));
            add.Columns.Add("Remark", typeof(string));

            notadd.Columns.Add("Userid", typeof(string));
            notadd.Columns.Add("Remark", typeof(string));

            already.Columns.Add("Userid", typeof(string));
            already.Columns.Add("Remark", typeof(string));

            if (IsPostBack)
            {
                if (Session["idadd"] != null)
                {
                    add.Rows.Clear();
                    add = (DataTable)Session["idadd"];
                    Session["idadd"] = null;
                }
                if (Session["notidadd"] != null)
                {
                    notadd.Rows.Clear();
                    notadd = (DataTable)Session["notidadd"];
                    Session["notidadd"] = null;
                }
                if (Session["already"] != null)
                {
                    already.Rows.Clear();
                    already = (DataTable)Session["already"];
                    Session["already"] = null;
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileuploadExcel.HasFile)
            {
                if (System.IO.Path.GetExtension(fileuploadExcel.FileName) == ".xls" || System.IO.Path.GetExtension(fileuploadExcel.FileName) == ".xlsx")
                {
                    try
                    {
                        using (var excel = new ExcelPackage(fileuploadExcel.PostedFile.InputStream))
                        {
                            var ws = excel.Workbook.Worksheets.First();

                            int j = 0;
                            for (int rowNum = 2; rowNum <= 10000; rowNum++)
                            {
                                cn.Close();
                                var userid = ws.Cells[rowNum, 1, rowNum, 1].Value;
                                var password = ws.Cells[rowNum, 2, rowNum, 2].Value;
                                var role = ws.Cells[rowNum, 3, rowNum, 3].Value;
                                var CM = ws.Cells[rowNum, 4, rowNum, 4].Value;
                                var RCM = ws.Cells[rowNum, 5, rowNum, 5].Value;
                                var username = ws.Cells[rowNum, 6, rowNum, 6].Value;
                                var Emailid = ws.Cells[rowNum, 7, rowNum, 7].Value;
                                var Mobile = ws.Cells[rowNum, 8, rowNum, 8].Value;
                                var state = ws.Cells[rowNum, 9, rowNum, 9].Value;
                                var RM = ws.Cells[rowNum, 10, rowNum, 10].Value;
                                var CH = ws.Cells[rowNum, 11, rowNum, 11].Value;
                                //var mobile = ws.Cells[rowNum, 12, rowNum, 12].Value;

                                if (userid == null)
                                {
                                    //Response.Write("File Empty");
                                    break;
                                }
                                else
                                {
                                    string[] columns = { "User" };
                                    string query = " SELECT [userid] FROM [users]  WHERE [userid] = '" + userid.ToString().Trim() + "'";
                                    string[] value = bucket.xread(query, columns);
                                    string val = "";
                                    try { value[0].ToString(); }
                                    catch (Exception er)
                                    { val = "1"; }
                                    if (val == "1")
                                    {
                                        string insertuser = @"insert into users([userid],[password],[role],[RCM],[status],[datastatus],[username],[emailid],[Mobile],[state],[RM],[CH],[CM])values ('" + userid.ToString().Trim().ToLower() + "','" + password.ToString().Trim() + "','" + role.ToString().Trim().ToUpper() + "','" + RCM.ToString().Trim().ToUpper() + "','CRE','CRE','" + username.ToString().Trim().ToUpper() + "','" + Emailid.ToString().Trim() + "','" + Mobile.ToString().Trim() + "','" + state.ToString().Trim().ToUpper() + "','" + RM.ToString().Trim().ToUpper() + "','" + CH.ToString().Trim().ToUpper() + "','" + CM.ToString().Trim().ToUpper() + "')";
                                        int i = obj.NonExecuteQuery(insertuser);

                                        string insum = "insert into usermap select '" + userid.ToString().Trim() + "',atmid,'CRE','CRE' from atms where state='" + state.ToString().Trim() + "' and atmstatus<>'Inactive'";
                                        int p = obj.NonExecuteQuery(insum);
                                        Response.Write(insum);

                                        if (i > 0)
                                        {
                                            string location = @"insert into location ([userid],[L_date],[L_time],[L_long],[L_lat],[Rem_Battery],[B_date],[B_time]) values ('" + userid.ToString().Trim() + "','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','" + System.DateTime.Now.ToString("hh:mm:ss") + "','0.0','0.0','0','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','" + System.DateTime.Now.ToString("hh:mm:ss") + "')";
                                            int k = obj.NonExecuteQuery(location);

                                            if (k > 0 && i > 0)
                                            {
                                                string config = @"insert into [config] ([userid]) values ('" + userid.ToString().Trim() + "')";
                                                int l = obj.NonExecuteQuery(config);

                                                if (l > 0 && k > 0 && i > 0)
                                                {
                                                    string Version = @"insert into [Version] ([userid]) values ('" + userid.ToString().Trim() + "')";
                                                    int m = obj.NonExecuteQuery(Version);

                                                    if (m > 0 && l > 0 && k > 0 && i > 0)
                                                    {

                                                        add.Rows.Add(userid.ToString().Trim(), "Added");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            notadd.Rows.Add(userid.ToString().Trim(), "Not Added");
                                        }
                                        //Response.Write(query);
                                    }
                                    else
                                    {
                                        already.Rows.Add(userid.ToString().Trim(), "already Exist");
                                    }
                                }
                            }

                            Session["idadd"] = null;
                            Session["notidadd"] = null;
                            Session["already"] = null;

                            lblerror.Visible = true;
                            lblerror.Text = "IMPORT COMPLETE";
                            lblerror.ForeColor = Color.Green;
                            Session["idadd"] = add;
                            Session["notidadd"] = notadd;
                            Session["already"] = already;
                            GridView1.DataSource = add;
                            GridView1.DataBind();
                            GridView2.DataSource = notadd;
                            GridView2.DataBind();
                            GridView3.DataSource = already;
                            GridView3.DataBind();
                            lbladdcount.Text = GridView1.Rows.Count.ToString();
                            lblnotadd.Text = GridView2.Rows.Count.ToString();
                            lblalreadycount.Text = GridView3.Rows.Count.ToString();
                        }
                    }

                    catch (Exception ex)
                    {
                        lblerror.Text = ex.Message.ToString();
                        lblerror.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblerror.Visible = true;
                    lblerror.Text = "Select Excel File Only";
                    lblerror.ForeColor = Color.Red;
                }
            }
            else
            {
                lblerror.Visible = true;
                lblerror.Text = "Select File To Import";
                lblerror.ForeColor = Color.Red;
            }
        }

        protected void lnkformat_Click(object sender, EventArgs e)
        {
            string XlsPath = Server.MapPath(@"~/Excel/AddUser.xlsx");
            FileInfo fileDet = new System.IO.FileInfo(XlsPath);
            Response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.UTF8;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
            Response.AddHeader("Content-Length", fileDet.Length.ToString());
            Response.ContentType = "application/ms-excel";
            Response.WriteFile(fileDet.FullName);
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }

        protected void lnkaddexport_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.DataSource = add;
                GridView1.DataBind();
                GridView1.AllowPaging = false;
                GridView1.AllowSorting = false;
                GridView1.GridLines = GridLines.Both;
                GridView1.HorizontalAlign = HorizontalAlign.Center;
                Response.AddHeader("content-disposition", "attachment;filename=AddAtm.xlsx");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                GridView1.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        protected void lnknotadd_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.DataSource = notadd;
                GridView2.DataBind();
                GridView2.AllowPaging = false;
                GridView2.AllowSorting = false;
                GridView2.GridLines = GridLines.Both;
                GridView2.HorizontalAlign = HorizontalAlign.Center;
                Response.AddHeader("content-disposition", "attachment;filename=NotAddAtm.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                GridView2.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void lnkalready_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView3.DataSource = already;
                GridView3.DataBind();
                GridView3.AllowPaging = false;
                GridView3.AllowSorting = false;
                GridView3.GridLines = GridLines.Both;
                GridView3.HorizontalAlign = HorizontalAlign.Center;
                Response.AddHeader("content-disposition", "attachment;filename=AlreadyAtm.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/ms-excel";
                GridView3.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
}