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

namespace Mphasis_webapp.UploadFormat
{
    public partial class DeleteUser : System.Web.UI.Page
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
                GridView1.DataBind();
                GridView2.DataBind();

            }

            add.Columns.Add("UserID", typeof(string));
            add.Columns.Add("Status", typeof(string));

            notadd.Columns.Add("UserID", typeof(string));
            notadd.Columns.Add("Status", typeof(string));





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
                                var AtmId = ws.Cells[rowNum, 1, rowNum, 1].Value;
                                var Status = ws.Cells[rowNum, 2, rowNum, 2].Value;
                                if (AtmId == null)
                                {
                                    //Response.Write("File Empty");
                                    break;
                                }
                                else
                                {

                                    string[] columns = { "userid" };
                                    string query = "SELECT [userid] FROM [Users]  WHERE [userid] = '" + AtmId.ToString().Trim() + "'";
                                    string[] value = bucket.xread(query, columns);
                                    string val = "";
                                    try { value[0].ToString(); }
                                    catch (Exception er)
                                    { val = "1"; }
                                    if (val != "1")
                                    {
                                        string updatestatus = @"update Users set Status='" + Status.ToString().Trim().ToUpper() + "',datastatus='" + Status.ToString().Trim().ToUpper() + "' where userid='" + AtmId.ToString().Trim() + "'";
                                        int i = obj.NonExecuteQuery(updatestatus);

                                        if (i > 0)
                                        {
                                            string q1 = "Delete from Location where userid='" + AtmId.ToString().Trim() + "'";
                                            obj.NonExecuteQuery(q1);
                                            string q2 = "Delete from LocationHist where userid='" + AtmId.ToString().Trim() + "'";
                                            obj.NonExecuteQuery(q2);
                                            string q3 = "Delete from Config where userid='" + AtmId.ToString().Trim() + "'";
                                            obj.NonExecuteQuery(q3);
                                            string q4 = "Delete from UserMap where userid='" + AtmId.ToString().Trim() + "'";
                                            obj.NonExecuteQuery(q4);
                                            string q5 = "Delete from version where userid='" + AtmId.ToString().Trim() + "'";
                                            obj.NonExecuteQuery(q5);
                                            string q6 = "Delete from users where userid='" + AtmId.ToString().Trim() + "'";
                                            obj.NonExecuteQuery(q6);
                                            add.Rows.Add(AtmId.ToString().Trim(), Status.ToString().Trim().ToUpper());
                                        }
                                        else
                                        {
                                            notadd.Rows.Add(AtmId.ToString().Trim(), Status.ToString().Trim().ToUpper());
                                        }
                                        //Response.Write(query);

                                    }
                                    else
                                    {
                                        notadd.Rows.Add(AtmId.ToString().Trim(), Status.ToString().Trim().ToUpper());
                                    }

                                }

                            }


                            Session["idadd"] = null;
                            Session["notidadd"] = null;



                            lblerror.Visible = true;
                            lblerror.Text = "IMPORT COMPLETE";
                            lblerror.ForeColor = Color.Green;
                            Session["idadd"] = add;
                            Session["notidadd"] = notadd;
                            GridView1.DataSource = add;
                            GridView1.DataBind();
                            GridView2.DataSource = notadd;
                            GridView2.DataBind();
                            lbladdcount.Text = GridView1.Rows.Count.ToString();
                            lblnotadd.Text = GridView2.Rows.Count.ToString();
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
            string XlsPath = Server.MapPath(@"~/UploadFormat/DeleteUser.xlsx");
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
                Response.AddHeader("content-disposition", "attachment;filename=DeleteUser.xlsx");
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
    }
}