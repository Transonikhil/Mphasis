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
    public partial class DeleteMap : System.Web.UI.Page
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
            add.Columns.Add("atmid", typeof(string));

            notadd.Columns.Add("Userid", typeof(string));
            notadd.Columns.Add("atmid", typeof(string));
            notadd.Columns.Add("Remark", typeof(string));

            already.Columns.Add("Userid", typeof(string));
            already.Columns.Add("atmid", typeof(string));


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

                            for (int rowNum = 2; rowNum <= 10000; rowNum++)
                            {
                                cn.Close();
                                var userid = ws.Cells[rowNum, 1, rowNum, 1].Value;
                                var atmid = ws.Cells[rowNum, 2, rowNum, 2].Value;
                                var descp = ws.Cells[rowNum, 3, rowNum, 3].Value;

                                if (atmid == null || userid == null)
                                {
                                    //Response.Write("File Empty");
                                    break;
                                }
                                else
                                {
                                    SqlCommand cmd = new SqlCommand(" SELECT atmid FROM [atms]  WHERE [atmid] = '" + atmid.ToString().Trim() + "'", cn);
                                    cn.Open();
                                    SqlDataReader dr = cmd.ExecuteReader();

                                    if (dr.Read())
                                    {
                                        cn.Close();
                                        SqlCommand cmd2 = new SqlCommand(" SELECT userid FROM [users]  WHERE [userid] = '" + userid.ToString().Trim() + "'", cn);
                                        cn.Open();
                                        SqlDataReader dr2 = cmd2.ExecuteReader();

                                        if (dr2.Read())
                                        {
                                            cn.Close();
                                            SqlCommand cmd1 = new SqlCommand(" SELECT status FROM [usermap]  WHERE [atmid]='" + atmid.ToString().Trim() + "' AND [userid] = '" + userid.ToString().Trim() + "'", cn);
                                            cn.Open();
                                            SqlDataReader dr1 = cmd1.ExecuteReader();
                                            if (dr1.Read())
                                            {
                                                string status = dr1[0].ToString();
                                                if (status != "DEL")
                                                {
                                                    string updtum = "update [usermap] set [status]='DEL', [serverstatus]='DEL' WHERE [atmid]='" + atmid.ToString().Trim() + "' AND [userid] = '" + userid.ToString().Trim() + "'";
                                                    string upusr = "update [users] set status='MOD', datastatus='MOD'  where userid='" + userid.ToString().Trim() + "'";
                                                    int p = obj.NonExecuteQuery(updtum);
                                                    int q = obj.NonExecuteQuery(upusr);
                                                    if (p > 0 && q > 0)
                                                    {
                                                        add.Rows.Add(userid.ToString().Trim(), atmid.ToString().Trim());
                                                    }
                                                }
                                                else
                                                {
                                                    cn.Close();
                                                    already.Rows.Add(userid.ToString().Trim(), atmid.ToString().Trim());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            cn.Close();
                                            notadd.Rows.Add(userid.ToString().Trim(), atmid.ToString().Trim(), "userid does not exist");
                                        }
                                    }
                                    else
                                    {
                                        cn.Close();
                                        notadd.Rows.Add(userid.ToString().Trim(), atmid.ToString().Trim(), "atmid does not exist");
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
                            lblalradyadd.Text = GridView3.Rows.Count.ToString();
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
            string XlsPath = Server.MapPath(@"~/Excel/UserMap.xlsx");
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
                Response.AddHeader("content-disposition", "attachment;filename=AddAtm.xls");
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

        protected void lnkalradyadd_Click(object sender, EventArgs e)
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
                Response.AddHeader("content-disposition", "attachment;filename=alreadyAddAtm.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                GridView3.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

}