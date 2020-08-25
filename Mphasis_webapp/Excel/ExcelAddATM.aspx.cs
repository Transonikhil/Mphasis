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
    public partial class ExcelAddATM : System.Web.UI.Page
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
            add.Columns.Add("Atmid", typeof(string));
            add.Columns.Add("Remark", typeof(string));
            //add.Columns.Add("CE", typeof(string));

            notadd.Columns.Add("Atmid", typeof(string));
            notadd.Columns.Add("Remark", typeof(string));
            //notadd.Columns.Add("CE", typeof(string));

            already.Columns.Add("Atmid", typeof(string));
            already.Columns.Add("Remark", typeof(string));
            //already.Columns.Add("CE", typeof(string));

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
                                var atmid = ws.Cells[rowNum, 1, rowNum, 1].Value;
                                var bankid = ws.Cells[rowNum, 2, rowNum, 2].Value;
                                var siteid = ws.Cells[rowNum, 3, rowNum, 3].Value;
                                var client = ws.Cells[rowNum, 4, rowNum, 4].Value;
                                var add1 = ws.Cells[rowNum, 5, rowNum, 5].Value;
                                var city = ws.Cells[rowNum, 6, rowNum, 6].Value;
                                var pin = ws.Cells[rowNum, 7, rowNum, 7].Value;
                                var state = ws.Cells[rowNum, 8, rowNum, 8].Value;
                                var onoffsite = ws.Cells[rowNum, 9, rowNum, 9].Value;
                                var region = ws.Cells[rowNum, 10, rowNum, 10].Value;
                                var CE = ws.Cells[rowNum, 11, rowNum, 11].Value;
                                var CM = ws.Cells[rowNum, 12, rowNum, 12].Value;
                                var RCM = ws.Cells[rowNum, 13, rowNum, 13].Value;
                                var DE = ws.Cells[rowNum, 14, rowNum, 14].Value;
                                var RM = ws.Cells[rowNum, 15, rowNum, 15].Value;
                                var CH = ws.Cells[rowNum, 16, rowNum, 16].Value;
                                //var Status = ws.Cells[rowNum, 16, rowNum, 16].Value;
                                //var scope = ws.Cells[rowNum, 17, rowNum, 17].Value;
                                //var alias = ws.Cells[rowNum, 18, rowNum, 18].Value;
                                //var atmtype = ws.Cells[rowNum, 19, rowNum, 19].Value;
                                //var agency = ws.Cells[rowNum, 20, rowNum, 20].Value;
                                //var vendor = ws.Cells[rowNum, 21, rowNum, 21].Value;
                                //var TOS = ws.Cells[rowNum, 22, rowNum, 22].Value;

                                if (atmid == null)
                                {
                                    //Response.Write("File Empty");
                                    break;
                                }
                                else
                                {
                                    string[] columns = { "AtmId" };
                                    string query = " SELECT [atmid] FROM [atms]  WHERE [atmid] = '" + atmid.ToString().Trim() + "'";
                                    string[] value = bucket.xread(query, columns);
                                    string val = "";
                                    try
                                    {
                                        value[0].ToString();
                                    }
                                    catch (Exception er)
                                    {
                                        val = "1";
                                    }

                                    if (val == "1")
                                    {
                                        add1 = add1.ToString().Replace(@"'", " ");
                                        add1 = add1.ToString().Replace(@";", " ");
                                        add1 = add1.ToString().Replace(@"-", " ");
                                        add1 = add1.ToString().Replace(@"\", " ");
                                        add1 = add1.ToString().Replace(@"/", " ");
                                        add1 = add1.ToString().Replace(@"$", " ");
                                        add1 = add1.ToString().Replace(@"#", " ");
                                        add1 = add1.ToString().Replace(@"@", " ");
                                        add1 = add1.ToString().Replace(@"%", " ");
                                        add1 = add1.ToString().Replace(@"&", " ");
                                        add1 = add1.ToString().Replace(@"*", " ");
                                        add1 = add1.ToString().Replace(@"(", " ");
                                        add1 = add1.ToString().Replace(@")", " ");
                                        add1 = add1.ToString().Replace(@",", " ");
                                        add1 = add1.ToString().Replace(@".", " ");
                                        add1 = add1.ToString().Replace(@":", " ");
                                        add1 = add1.ToString().Replace(@"+", " ");
                                        add1 = add1.ToString().Replace(@"{", " ");
                                        add1 = add1.ToString().Replace(@"}", " ");
                                        add1 = add1.ToString().Replace(@"[", " ");
                                        add1 = add1.ToString().Replace(@"]", " ");
                                        add1 = add1.ToString().Replace(@"  ", " ");
                                        add1 = add1.ToString().Replace(@"  ", " ");
                                        add1 = add1.ToString().Replace(@"  ", " ");

                                        string location;
                                        int length = add1.ToString().Length;
                                        if (length > 300)
                                        {
                                            add1 = add1.ToString().Substring(0, 300);
                                            location = add1.ToString().Substring(0, 30);
                                        }

                                        if (length > 30)
                                        {
                                            location = add1.ToString().Substring(0, 30);
                                        }
                                        else
                                        {
                                            location = add1.ToString().Trim();
                                        }


                                        string insert = @"insert into [ATMs]  ([atmid],[location],[bankid],[siteid],[client],[atmstatus],[addressline1],[addressline2],[city],[pin],[state],[project],[onoffsite],[region],[CE],[CM],[RCM],[DE],[RM],[CH]) values('"
                                            + atmid.ToString().Trim().ToUpper() + "','" + location.ToString().Trim().ToUpper() + "','" + bankid.ToString().Trim().ToUpper() + "','" + siteid.ToString().Trim().ToUpper() + "','" + client.ToString().Trim().ToUpper() + "','Active','"
                                            + add1.ToString().Trim().ToUpper() + "','" + location.ToString().Trim().ToUpper() + "','" + city.ToString().Trim().ToUpper() + "','" + pin.ToString().Trim() + "','" + state.ToString().Trim().ToUpper() + "','NA','" + onoffsite.ToString().Trim().ToUpper() + "','"
                                            + region.ToString().Trim().ToUpper() + "','" + CE.ToString().Trim().ToUpper() + "','" + CM.ToString().Trim().ToUpper() + "','" + RCM.ToString().Trim().ToUpper() + "','" + DE.ToString().Trim().ToUpper() + "','" + RM.ToString().Trim().ToUpper() + "','" + CH.ToString().Trim().ToUpper() + "')";
                                        int i = obj.NonExecuteQuery(insert);


                                        if (i > 0)
                                        {
                                            string insce = "insert into usermap select userid,'" + atmid.ToString().Trim().ToUpper() + "','CRE','CRE' from users where state='" + state.ToString().Trim() + "' and role in('AO','CM','RCM','DE','RM','CH') and status<>'DEL'";
                                            int p = obj.NonExecuteQuery(insce);
                                            string inscm = "update users set status='MOD', datastatus='MOD' where userid in(select userid from usermap where atmid='" + atmid.ToString().Trim().ToUpper() + "'and status<>'DEL')";
                                            int s = obj.NonExecuteQuery(inscm);

                                            if (p > 0 && s > 0)
                                            {
                                                add.Rows.Add(atmid.ToString().Trim().ToUpper(), "Added and mapped");
                                            }
                                            else
                                            {
                                                notadd.Rows.Add(atmid.ToString().Trim().ToUpper(), "Added but Not mapped");
                                            }
                                            //Response.Write(insce);
                                            //Response.Write(inscm);
                                        }
                                        else
                                        {
                                            notadd.Rows.Add(atmid.ToString().Trim().ToUpper(), "Not added");
                                        }
                                        //Response.Write(insert);
                                    }
                                    else
                                    {
                                        already.Rows.Add(atmid.ToString().Trim().ToUpper(), "Already Exist");
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
            string XlsPath = Server.MapPath(@"~/Excel/AddATM.xlsx");
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