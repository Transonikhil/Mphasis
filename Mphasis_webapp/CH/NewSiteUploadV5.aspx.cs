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
using System.Diagnostics;
using OfficeOpenXml.Style;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace Mphasis_webapp.CH
{
    public partial class NewSiteUploadV5 : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        DataTable add = new DataTable();
        DataTable issue = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            obj.NonExecuteQuery("Delete from tempscheduler where refno like '" + Session["sess_userid"] + "%'");

            if (IsPostBack)
            {
                GridView1.DataBind();
                GridView2.DataBind();
            }
            else
            {
                //  obj.NonExecuteQuery("Delete from tempscheduler where refno like '" + Session["sess_userid"] + "%'");
                DateTime now = DateTime.Now;

                if (now.ToString("MM") != "12")
                {
                    ddlmonth.Items.Add(new ListItem(now.ToString("MMM"), now.ToString("MM")));
                    ddlmonth.Items.Add(new ListItem(now.AddMonths(1).ToString("MMM"), now.AddMonths(1).ToString("MM")));
                }
                else
                {
                    ddlmonth.Items.Add(new ListItem(now.ToString("MMM"), now.ToString("MM")));
                }

                dd_month.Items.Add(new ListItem(now.ToString("MMM"), now.ToString("MM")));
                //Response.Write((Convert.ToDateTime("10/07/2015") - Convert.ToDateTime("02/07/2015")).Days);
            }

            if (Session["sess_role"] == "CMM")
            {
                mod.Attributes.Add("style", "display:none");
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string refno = Session["sess_userid"] + System.DateTime.Now.ToString("yyyyMMddHHmmss") + System.DateTime.Now.Millisecond;
            add.Rows.Clear();
            issue.Rows.Clear();

            if (fileuploadExcel.HasFile)
            {
                if (System.IO.Path.GetExtension(fileuploadExcel.FileName) == ".xls" || System.IO.Path.GetExtension(fileuploadExcel.FileName) == ".xlsx")
                {
                    try
                    {
                        using (var excel = new ExcelPackage(fileuploadExcel.PostedFile.InputStream))
                        {
                            var ws = excel.Workbook.Worksheets.First();

                            add.Columns.AddRange(new DataColumn[8] { new DataColumn("USERID", typeof(string)),
                        new DataColumn("Atmid", typeof(string)),
                        new DataColumn("DATE",typeof(string)),
                        new DataColumn("STATUS",typeof(string)),
                        new DataColumn("UPLOADDATE", typeof(string)),
                        new DataColumn("REFNO",typeof(string)),
                        new DataColumn("TYPE", typeof(string)),
                        new DataColumn("DESCP", typeof(string)) });

                            issue.Columns.Add("USERID", typeof(string));
                            issue.Columns.Add("Atmid", typeof(string));
                            issue.Columns.Add("DATE", typeof(string));
                            issue.Columns.Add("ISSUE", typeof(string));

                            for (int rowNum = 2; rowNum <= 10000; rowNum++)
                            {
                                try
                                {
                                    var userid = ws.Cells[rowNum, 1, rowNum, 1].Value;
                                    var Atmid = ws.Cells[rowNum, 2, rowNum, 2].Value;
                                    var _date = ws.Cells[rowNum, 3, rowNum, 3].Value;
                                    var decp = ws.Cells[rowNum, 4, rowNum, 4].Value;
                                    string dd = "";

                                    if (userid == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (_date.ToString().Length < 2)
                                        {
                                            _date = "0" + _date;
                                            dd = "0" + _date;
                                        }
                                        else
                                        {
                                            dd = _date.ToString();
                                        }

                                        _date = ddlmonth.SelectedValue + "/" + _date + "/" + DateTime.Now.Year;

                                        try
                                        {
                                            if (Convert.ToInt32(dd) > DateTime.DaysInMonth(Convert.ToInt32(DateTime.Now.ToString("yyyy")), Convert.ToInt32(ddlmonth.SelectedValue)))
                                            {
                                                add.Rows.Add(userid, Atmid, _date, "DEL", DateTime.Now.ToString("MM'/'dd'/'yyyy"), refno, decp, "Invalid Date");
                                            }
                                            else
                                            {
                                                add.Rows.Add(userid, Atmid, _date, "CRE", DateTime.Now.ToString("MM'/'dd'/'yyyy"), refno, decp, "");
                                            }
                                        }
                                        catch
                                        {
                                            add.Rows.Add(userid, Atmid, "", "DEL", DateTime.Now.ToString("MM'/'dd'/'yyyy"), refno, decp, "INVALID DATE");
                                        }


                                        string[] c2 = { "userid" };
                                        string user = "Select userid from users where userid = '" + userid.ToString().Trim() + "' and CH like '" + Session["Sess_username"] + "'";
                                        string[] vr = bucket.xread(user, c2);
                                        try { vr[0].ToString(); }

                                        catch (Exception er)
                                        {
                                            add.Rows.Add(userid, Atmid, "", "DEL", DateTime.Now.ToString("MM'/'dd'/'yyyy"), refno, decp, "Invalid Userid");
                                        }


                                        string[] c3 = { "atmid" };
                                        string atm = "Select atmid from atms where atmid like '" + Atmid.ToString().Trim() + "' and CH like '" + Session["Sess_username"] + "'";
                                        string[] vr1 = bucket.xread(atm, c3);
                                        try { vr1[0].ToString(); }

                                        catch (Exception er)
                                        {
                                            add.Rows.Add(userid, Atmid, "", "DEL", DateTime.Now.ToString("MM'/'dd'/'yyyy"), refno, decp, "Invalid Atmid");
                                        }


                                        //add.Rows.Add(userid,Atmid,);

                                        #region unwanted
                                        //string val = "";
                                        //string[] columns = { "userid" };
                                        //string useridck = "Select userid from Users where userid='" + userid.ToString().Trim() + "'";
                                        //string[] value = bucket.xread(useridck, columns);

                                        //try
                                        //{
                                        //    value[0].ToString();
                                        //    val = "OK";
                                        //    string[] columns1 = { "Atmid" };
                                        //    string Atmidck = "Select Atmid from Atms where Atmid='" + Atmid.ToString().Trim() + "' and atmStatus<>'Inactive'";

                                        //    string[] value1 = bucket.xread(Atmidck, columns1);
                                        //    try
                                        //    {
                                        //        value1[0].ToString();
                                        //        val = "OK";

                                        //        string[] columns2 = { "userid", "Atmid" };
                                        //        string mapck = "Select Atmid,userid from usermap where Atmid='" + Atmid.ToString().Trim() + "' and userid='" + userid.ToString().Trim() + "'";
                                        //        string[] value2 = bucket.xread(mapck, columns2);
                                        //        try
                                        //        {
                                        //            value2[0].ToString();
                                        //            val = "OK";

                                        //            try
                                        //            {
                                        //                if (Convert.ToInt32(_date) > DateTime.DaysInMonth(Convert.ToInt32(DateTime.Now.ToString("yyyy")), Convert.ToInt32(ddlmonth.SelectedValue)))
                                        //                {
                                        //                    val = "Date Issue";
                                        //                    issue.Rows.Add(userid, Atmid, "Date entered is incorrect. Total number of days for the selected month is " + Convert.ToInt32(ddlmonth.SelectedValue).ToString());
                                        //                }
                                        //                else
                                        //                {
                                        //                    string[] columns3 = { "sdate" };
                                        //                    string que = @"select convert(date,[date]) as [sdate] from scheduler where Atmid='" + Atmid.ToString().Trim() + "' and datepart(MM,convert(date,[date])='" + ddlmonth.SelectedValue + "' order by convert(date,[date]) desc";
                                        //                    string[] value3 = bucket.xread(que, columns3);

                                        //                    try
                                        //                    {
                                        //                        if(Convert.ToInt32((Convert.ToDateTime("07/07/2015") - Convert.ToDateTime("01/07/2015")).Days.ToString().Replace("-","")) >7)
                                        //                        {

                                        //                        }
                                        //                    }
                                        //                    catch
                                        //                    {
                                        //                        val = "OK";
                                        //                    }
                                        //                }
                                        //            }
                                        //            catch
                                        //            {
                                        //                val = "Date Issue";
                                        //                issue.Rows.Add(userid, Atmid, "Date Not In Proper Format");
                                        //            }

                                        //        }
                                        //        catch
                                        //        {
                                        //            val = "NO MAP";
                                        //            issue.Rows.Add(userid, Atmid, "SITE ID IS NOT MAPPED TO PARTICULAR USER");
                                        //        }
                                        //    }
                                        //    catch
                                        //    {
                                        //        val = "NO SITE";
                                        //        issue.Rows.Add(userid, Atmid, "Atmid DOES NOT EXISTS");
                                        //    }

                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    val = "NO USER";
                                        //    issue.Rows.Add(userid, Atmid, "USER ID DOES NOT EXISTS");
                                        //}


                                        //if (val == "OK")
                                        //{

                                        //    #region else
                                        //    try
                                        //    {
                                        //        var ci = new CultureInfo("en-US");
                                        //        var formatx = new[] { "M-d-yyyy", "dd-MM-yyyy", "MM-dd-yyyy", "M.d.yyyy", "dd.MM.yyyy", "MM.dd.yyyy", "MMM-dd-yyyy" }
                                        //                .Union(ci.DateTimeFormat.GetAllDateTimePatterns()).ToArray();
                                        //        string datevalue = "";
                                        //        string vdate = CleanDate(_date.ToString(), userid.ToString(), Atmid.ToString());
                                        //        try
                                        //        {
                                        //            datevalue = DateTime.ParseExact(vdate.ToString(), formatx, ci, DateTimeStyles.AssumeLocal).ToString("MM'/'dd'/'yyyy");

                                        //        }
                                        //        catch
                                        //        {

                                        //            datevalue = DateTime.FromOADate(Convert.ToDouble(vdate)).ToString("MM'/'dd'/'yyyy");
                                        //        }
                                        //        string chck = "If exists(Select userid from UserMap where userid='" + userid + "' and Atmid='" + Atmid + "' and status<>'DEL') begin Update usermap set descp=null where userid='" + userid + "' and Atmid='" + Atmid + "' end";

                                        //        int i = obj.NonExecuteQuery(chck);
                                        //        if (i > 0)
                                        //        {

                                        //            if (type.ToString().ToUpper().Trim() == "DAILY")
                                        //            {
                                        //                //Response.Write("DAILY");
                                        //                string temp_date = datevalue.ToString();

                                        //                string f_query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + datevalue + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','DAILY','DAILY')";
                                        //                string show = "";

                                        //                cn.Open();
                                        //                SqlCommand cmdx = new SqlCommand(f_query, cn);
                                        //                cmdx.ExecuteNonQuery();
                                        //                //createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                while ((DateTime.ParseExact(temp_date.ToString(), formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(1).ToString("MM'/'dd'/'yyyy").Substring(0, 2) == datevalue.ToString().Substring(0, 2))
                                        //                {
                                        //                    //temp_date = show;

                                        //                    if ((DateTime.ParseExact(temp_date.ToString(), formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(1).DayOfWeek.ToString() != DayOfWeek.Sunday.ToString())
                                        //                    {
                                        //                        show = (DateTime.ParseExact(temp_date, formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(1).ToString("MM'/'dd'/'yyyy");
                                        //                        string query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + show + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','DAILY','DAILY')";
                                        //                        temp_date = show;
                                        //                        SqlCommand cmdy = new SqlCommand(query, cn);
                                        //                        cmdy.ExecuteNonQuery();
                                        //                       // createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                        //Response.Write(query);
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        temp_date = (DateTime.ParseExact(temp_date, formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(1).ToString("MM'/'dd'/'yyyy");
                                        //                    }
                                        //                }
                                        //                cn.Close();
                                        //                add.Rows.Add(userid.ToString().Trim(), Atmid.ToString().Trim(), _date, type);
                                        //                //createTicket("SITE SCHEDULE", Atmid);
                                        //                //Response.Write("<script>alert('Schedule Created Successfuly.')</script>");
                                        //            }

                                        //            else if (type.ToString().ToUpper().Trim() == "WEEKLY")
                                        //            {
                                        //                //Response.Write("WEEKLY");
                                        //                string temp_date = datevalue.ToString();

                                        //                string f_query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + datevalue.ToString() + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','WEEKLY','WEEKLY')";
                                        //                string show = "";

                                        //                cn.Open();
                                        //                SqlCommand cmdx = new SqlCommand(f_query, cn);
                                        //                cmdx.ExecuteNonQuery();
                                        //                //createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                while ((DateTime.ParseExact(temp_date.ToString(), formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(7).ToString("MM'/'dd'/'yyyy").Substring(0, 2) == datevalue.ToString().Substring(0, 2))
                                        //                {

                                        //                    if ((DateTime.ParseExact(temp_date.ToString(), formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(7).DayOfWeek.ToString() != "Sunday")
                                        //                    {
                                        //                        show = (DateTime.ParseExact(temp_date, formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(7).ToString("MM'/'dd'/'yyyy");
                                        //                        string query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + show + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','WEEKLY','WEEKLY')";

                                        //                        temp_date = show;
                                        //                        SqlCommand cmdy = new SqlCommand(query, cn);
                                        //                        cmdy.ExecuteNonQuery();
                                        //                        //createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                    }
                                        //                    else
                                        //                    { temp_date = (DateTime.ParseExact(temp_date, formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(7).ToString("MM'/'dd'/'yyyy"); }
                                        //                }
                                        //                cn.Close();
                                        //                add.Rows.Add(userid.ToString().Trim(), Atmid.ToString().Trim(), _date, type);

                                        //                //Response.Write("<script>alert('Schedule Created Successfuly.')</script>");
                                        //            }

                                        //            else if (type.ToString().ToUpper().Trim() == "FORTHNIGHTLY" || type.ToString().ToUpper().Trim() == "FORTNIGHTLY")
                                        //            {
                                        //                //Response.Write("FTNLY");
                                        //                string temp_date = datevalue.ToString();

                                        //                string f_query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + datevalue.ToString() + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','FORTHNIGHTLY','FORTHNIGHTLY')";
                                        //                string show = "";
                                        //                cn.Open();
                                        //                SqlCommand cmdx = new SqlCommand(f_query, cn);
                                        //                cmdx.ExecuteNonQuery();
                                        //                //createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                //createTicket("SITE SCHEDULE", Atmid);
                                        //                while ((DateTime.ParseExact(temp_date.ToString(), formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(15).ToString("MM'/'dd'/'yyyy").Substring(0, 2) == datevalue.ToString().Substring(0, 2))
                                        //                {

                                        //                    if ((DateTime.ParseExact(temp_date.ToString(), formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(15).DayOfWeek.ToString() != "Sunday")
                                        //                    {
                                        //                        show = (DateTime.ParseExact(temp_date, formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(15).ToString("MM'/'dd'/'yyyy");
                                        //                        string query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + show + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','FORTHNIGHTLY','FORTHNIGHTLY')";

                                        //                        temp_date = show;
                                        //                        SqlCommand cmdy = new SqlCommand(query, cn);
                                        //                        cmdy.ExecuteNonQuery();
                                        //                        //createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                    }
                                        //                    else
                                        //                    { temp_date = (DateTime.ParseExact(temp_date, formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(15).ToString("MM'/'dd'/'yyyy"); }
                                        //                }
                                        //                cn.Close();
                                        //                add.Rows.Add(userid.ToString().Trim(), Atmid.ToString().Trim(), _date, type);

                                        //                //Response.Write("<script>alert('Schedule Created Successfuly.')</script>");
                                        //            }
                                        //            else if (type.ToString().ToUpper().Trim() == "MONTHLY")
                                        //            {
                                        //                //Response.Write("Monthly");
                                        //                string f_query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type) values ('" + userid + "','" + Atmid + "','" + datevalue.ToString() + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','" + decp + "')";
                                        //                cn.Open();
                                        //                SqlCommand cmdx = new SqlCommand(f_query, cn);
                                        //                cmdx.ExecuteNonQuery();
                                        //                cn.Close();
                                        //                //createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), datevalue.ToString(), userid.ToString());
                                        //                add.Rows.Add(userid.ToString().Trim(), Atmid.ToString().Trim(), _date, type);
                                        //                //createTicket("SITE SCHEDULE", Atmid);
                                        //                //Response.Write("<script>alert('Schedule Created Successfuly.')</script>");
                                        //            }
                                        //            else if (type.ToString().ToUpper().Trim() == "BIWEEKLY")
                                        //            {
                                        //                //Response.Write("WEEKLY");
                                        //                string temp_date = datevalue.ToString();

                                        //                string f_query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + datevalue.ToString() + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','BIWEEKLY','BIWEEKLY')";
                                        //                string show = "";

                                        //                cn.Open();
                                        //                SqlCommand cmdx = new SqlCommand(f_query, cn);
                                        //                cmdx.ExecuteNonQuery();
                                        //                //createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                while ((DateTime.ParseExact(temp_date.ToString(), formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(3).ToString("MM'/'dd'/'yyyy").Substring(0, 2) == datevalue.ToString().Substring(0, 2))
                                        //                {

                                        //                    if ((DateTime.ParseExact(temp_date.ToString(), formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(3).DayOfWeek.ToString() != "Sunday")
                                        //                    {
                                        //                        show = (DateTime.ParseExact(temp_date, formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(3).ToString("MM'/'dd'/'yyyy");
                                        //                        string query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + show + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','BIWEEKLY','BIWEEKLY')";

                                        //                        temp_date = show;
                                        //                        SqlCommand cmdy = new SqlCommand(query, cn);
                                        //                        cmdy.ExecuteNonQuery();
                                        //                       // createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        show = temp_date = (DateTime.ParseExact(temp_date, formatx, new CultureInfo("en-US"), DateTimeStyles.None)).AddDays(4).ToString("MM'/'dd'/'yyyy");

                                        //                        string query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type,stype) values ('" + userid + "','" + Atmid + "','" + show + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','WEEKLY','WEEKLY')";


                                        //                        SqlCommand cmdy = new SqlCommand(query, cn);
                                        //                        cmdy.ExecuteNonQuery();
                                        //                       // createTicket("SITE SCHEDULE", Atmid.ToString().Trim(), temp_date, userid.ToString());
                                        //                    }
                                        //                }
                                        //                cn.Close();
                                        //                add.Rows.Add(userid.ToString().Trim(), Atmid.ToString().Trim(), _date, type);
                                        //                //createTicket("SITE SCHEDULE", Atmid);
                                        //                //Response.Write("<script>alert('Schedule Created Successfuly.')</script>");
                                        //            }

                                        //            else
                                        //            {
                                        //                flag = false;
                                        //                lblerror.Visible = true;

                                        //                //lblerror.Text += "<br/>" + ".Userid: " + userid + " or Atmid: " + Atmid + " was not found in the db or date format was incorrect or SITE not mapped to respective user";
                                        //                //Response.Write("<script>alert()</script>");
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            flag = false;
                                        //            //lblerror.Text += "<br/>" + ".Userid: " + userid + " or Atmid: " + Atmid + " was not found in the db or date format was incorrect or SITE not mapped to respective user";
                                        //        }
                                        //    }
                                        //    catch(Exception ex )
                                        //    {
                                        //        //Response.Write(ex.Message.ToString());
                                        //        flag = false; //lblerror.Text += "<br/>" + ".Userid: " + userid + " or Atmid: " + Atmid + " was not found in the db or date format was incorrect or SITE not mapped to respective user";
                                        //        issue.Rows.Add(userid, Atmid, "Already Scheduled");
                                        //    }
                                        //    #endregion
                                        //}
                                        #endregion
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }

                        if (add.Rows.Count > 0)
                        {
                            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString))
                            {
                                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                                {
                                    //Set the database table name
                                    sqlBulkCopy.DestinationTableName = "dbo.tempscheduler";

                                    #region optional
                                    //[OPTIONAL]: Map the DataTable columns with that of the database table

                                    //sqlBulkCopy.ColumnMappings.Add("USERID", "userid");
                                    //sqlBulkCopy.ColumnMappings.Add("Atmid", "Atmid");
                                    //sqlBulkCopy.ColumnMappings.Add("DATE", "date");
                                    //sqlBulkCopy.ColumnMappings.Add("UPLOADDATE", "uploaddate");
                                    //sqlBulkCopy.ColumnMappings.Add("REFNO", "refno");
                                    //sqlBulkCopy.ColumnMappings.Add("TYPE", "type");
                                    //sqlBulkCopy.ColumnMappings.Add("STATUS", "status");
                                    //sqlBulkCopy.ColumnMappings.Add("DESCP", "descp");
                                    #endregion

                                    con.Open();
                                    sqlBulkCopy.WriteToServer(add);
                                    con.Close();
                                }
                            }
                            string chk = "";
                            // chk = @"If exists (Select Atmid from Scheduler where datepart(mm,[date])='" + ddlmonth.SelectedValue + "' and Atmid in (Select Atmid from " +
                            //              "tempscheduler where refno='" + refno + "' and DATEPART(mm,date)='" + ddlmonth.SelectedValue + "')) begin Update tempscheduler set " +
                            //              "status='DEL',descp='Site already scheduled' where refno='" + refno + "' and Atmid in (Select Atmid from scheduler where " +
                            //              "DATEPART(mm,date)='" + ddlmonth.SelectedValue + "') end";


                            chk = @"If exists (Select Atmid from Scheduler where datepart(mm,[date])='" + ddlmonth.SelectedValue + "' and Atmid in (Select Atmid from " +
                                  "tempScheduler)) begin Update tempScheduler set " +
                                     "status='DEL',descp='Site already scheduled' where refno='" + refno + "' and Atmid in (Select Atmid from Scheduler where " +
                                   "DATEPART(mm,date)='" + ddlmonth.SelectedValue + "' and DATEPART(yyyy,date)=datepart(yyyy,getDate())) end";


                            if (obj.NonExecuteQuery(chk) > 0)
                            {

                            }
                            else
                            {
                                //                            string del = @"Update tempscheduler set status='DEL' where Atmid in (Select Atmid from tempscheduler group by Atmid having COUNT(Atmid)>4)
                                //                                     and status<>'DEL' and refno='" + refno + "';Update tempscheduler set status='DEL',descp='Invalid Atmid' where Atmid not in " +
                                //                                         "(Select distinct Atmid from Atms) and refno='" + refno + "' and status <> 'DEL';Update tempscheduler set status='DEL',descp='Invalid userid'" +
                                //                                         " where userid not in (Select distinct userid from Users where status<>'DEL') and refno='" + refno + "'  and status <> 'DEL';" +
                                //                                         "Update tempscheduler set status='DEL',descp='Invalid date' where isdate([date])<>1 and refno='" + refno + "' and status <> 'DEL'";


                                string del = @"Update tempScheduler set status='DEL',descp='Site ID scheduled for more than 4 times' where Atmid in (Select Atmid from tempScheduler group by Atmid having COUNT(Atmid)>4)
                                   and status<>'DEL' and refno='" + refno + "';Update tempScheduler set status='DEL',descp='Invalid Site ID' where Atmid not in " +
                                              "(Select distinct SiteID from Atms) and refno='" + refno + "' and status <> 'DEL';Update tempScheduler set status='DEL',descp='Invalid userid'" +
                                             " where userid not in (Select distinct userid from Users where status<>'DEL') and refno='" + refno + "'  and status <> 'DEL';" +
                                            "Update tempScheduler set status='DEL',descp='Invalid date' where isdate([date])<>1 and refno='" + refno + "' and status <> 'DEL'";







                                obj.NonExecuteQuery(del);

                                chkmapping(refno);
                                chkdate(refno);
                                chkdaterestriction(refno);
                            }

                            chkvalid(refno);
                        }
                        else
                        {
                            lblerror.Text = "No records found on excel";
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


            if (issue.Rows.Count > 0)
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    GridView2.DataSource = issue;
                    GridView2.AllowPaging = false;

                    GridView2.AllowSorting = false;

                    GridView2.DataBind();



                    GridView2.GridLines = GridLines.Both;
                    GridView2.HorizontalAlign = HorizontalAlign.Center;
                    Response.AddHeader("content-disposition", "attachment;filename=SchedularIssue.xls");
                    Response.Charset = String.Empty;
                    Response.ContentType = "application/vnd.xls";
                    GridView2.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }
        }

        public void chkmapping(string vid)
        {
            string query = "";
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            SqlCommand cnCommand = cn.CreateCommand();
            SqlDataReader reader = default(SqlDataReader);
            try
            {
                cnCommand.CommandText = @"Select distinct userid,SiteID from TempScheduler t,atms a where refno='" + vid.ToString().Trim() + "' and a.atmid=t.atmid and status<>'DEL'";
                cnCommand.CommandTimeout = 500;
                cn.Open();
                reader = cnCommand.ExecuteReader();

                while (reader.Read())
                {
                    query = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                        IF NOT EXISTS (Select Siteid from UserMap u,atms a where u.atmid=a.atmid and siteid ='" + reader[1].ToString() + "' and userid='" + reader[0].ToString() +
                            "') BEGIN update TempScheduler set status='DEL',descp='Site not mapped' where Atmid ='" + reader[1].ToString() + "' and userid='" + reader[0].ToString() +
                            "' and refno='" + vid.ToString() + "' END COMMIT TRANSACTION";
                    obj.NonExecuteQuery(query);
                }

                reader.Close();
                cn.Close();
            }

            catch (Exception ee)
            {
                Response.Write(ee.Message);
            }

            finally
            {
                reader.Close();
                cn.Close();
                cn.Dispose();
            }
        }

        public void chkdate(string vid)
        {
            string query = "";

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            SqlCommand cnCommand = cn.CreateCommand();
            cnCommand.CommandTimeout = 500;
            SqlDataReader reader = default(SqlDataReader);
            try
            {
                string h = cnCommand.CommandText = @";with t1 as (SELECT Atmid,[date] FROM (SELECT tempscheduler.*, ROW_NUMBER() OVER (PARTITION BY Atmid ORDER BY CONVERT(date, [date]) asc)
                                    AS RN FROM tempscheduler WHERE CONVERT(date, uploaddate)<= CONVERT(date, getdate(), 101) and refno='" + vid.ToString() + "' and status <> 'DEL') AS t WHERE RN = 1), " +
                                           "t2 as (SELECT Atmid,vdate from (SELECT DR_CTP.*, ROW_NUMBER() OVER (PARTITION BY atmid ORDER BY Atmid asc,CONVERT(date, vdate) desc) " +
                                           "AS RN FROM DR_CTP WHERE CONVERT(date, vdate)<= CONVERT(date, getdate(), 101) and Atmid in (Select distinct Atmid from tempscheduler where " +
                                           " refno='" + vid.ToString() + "' and status <> 'DEL')) AS t WHERE RN = 1) select t1.Atmid,case when t2.vdate is null then 8 else Datediff(dd,t2.vdate,t1.date) end,t2.vdate " +
                                           "from t1 full outer join t2 on t1.Atmid = t2.Atmid";
                cn.Open();
                reader = cnCommand.ExecuteReader();
                //Response.Write(h);
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader[1].ToString()) <= 7)
                    {
                        query = @"update TempScheduler set status='DEL',descp='7 day clause failed.Last audit was on " + reader[2].ToString() + "' where Atmid='" +
                                reader[0].ToString() + "' and refno='" + vid.ToString() + "' END COMMIT TRANSACTION";
                        obj.NonExecuteQuery(query);
                    }
                }

                reader.Close();
                cn.Close();
            }

            catch (Exception ee)
            {
                Response.Write(ee.Message);
            }

            finally
            {
                reader.Close();
                cn.Close();
                cn.Dispose();
            }
        }

        public void chkdaterestriction(string vid)
        {
            string query = "";
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            SqlCommand cnCommand = cn.CreateCommand();
            cnCommand.CommandTimeout = 500;
            SqlDataReader reader = default(SqlDataReader);
            try
            {
                cnCommand.CommandText = @";with t1 as (SELECT Atmid,[date] FROM (SELECT tempscheduler.*, ROW_NUMBER() OVER (PARTITION BY Atmid ORDER BY CONVERT(date, [date]) asc)
                                    AS RN FROM tempscheduler WHERE CONVERT(date, uploaddate)<= CONVERT(date, getdate(), 101) and refno='" + vid.ToString() + "' and status <> 'DEL')  AS t WHERE RN = 1)," +
                                        " t2 as (SELECT Atmid,[date] FROM (SELECT tempscheduler.*, ROW_NUMBER() OVER (PARTITION BY Atmid ORDER BY CONVERT(date, [date]) asc) " +
                                        "AS RN FROM tempscheduler WHERE CONVERT(date, uploaddate)<= CONVERT(date, getdate(), 101) and refno='" + vid.ToString() + "' and status <> 'DEL')  AS t WHERE RN = 2), " +
                                        "t3 as (SELECT Atmid,[date] FROM (SELECT tempscheduler.*, ROW_NUMBER() OVER (PARTITION BY Atmid ORDER BY CONVERT(date, [date]) asc) " +
                                        "AS RN FROM tempscheduler WHERE CONVERT(date, uploaddate)<= CONVERT(date, getdate(), 101) and refno='" + vid.ToString() + "' and status <> 'DEL')  AS t WHERE RN = 3), " +
                                        "t4 as (SELECT Atmid,[date] FROM (SELECT tempscheduler.*, ROW_NUMBER() OVER (PARTITION BY Atmid ORDER BY CONVERT(date, [date]) asc) " +
                                        "AS RN FROM tempscheduler WHERE CONVERT(date, uploaddate)<= CONVERT(date, getdate(), 101) and refno='" + vid.ToString() + "' and status <> 'DEL')  AS t WHERE RN = 4) " +
                                        "select t1.Atmid,t1.date as [first audit],t2.date as [second audit],t3.date as [third audit],t4.date as [fourth audit], " +
                                        "case when DATEDIFF(dd,t1.date,t2.date) is null then 8 else DATEDIFF(dd,t1.date,t2.date) end, " +
                                        "case when DATEDIFF(dd,t2.date,t3.date) is null then 8 else DATEDIFF(dd,t2.date,t3.date) end, " +
                                        "case when DATEDIFF(dd,t3.date,t4.date) is null then 8 else DATEDIFF(dd,t3.date,t4.date) end " +
                                        "from t1 full outer join t2 on t1.Atmid = t2.Atmid full outer join t3 on t1.Atmid=t3.Atmid full outer join t4 on t1.Atmid=t4.Atmid";
                cn.Open();
                reader = cnCommand.ExecuteReader();

                while (reader.Read())
                {
                    if (Convert.ToInt32(reader[5].ToString()) <= 7)
                    {
                        query = "Update TempScheduler set status='DEL',descp='7 day clause failed. Last audit scheduled is on " + reader[1].ToString() + "' where Atmid='" +
                                reader[0].ToString() + "' and date='" + reader[2].ToString() + "' and refno='" + vid + "'";
                        obj.NonExecuteQuery(query);

                        if (Convert.ToInt32(reader[6].ToString()) <= 7)
                        {
                            query = "Update TempScheduler set status='DEL',descp='7 day clause failed. Last audit scheduled is on " + reader[2].ToString() + "' where Atmid='" +
                                    reader[0].ToString() + "' and date='" + reader[3].ToString() + "' and refno='" + vid + "'";
                            obj.NonExecuteQuery(query);
                        }

                        if (Convert.ToInt32(reader[7].ToString()) <= 7)
                        {
                            query = "Update TempScheduler set status='DEL',descp='7 day clause failed. Last audit scheduled is on " + reader[3].ToString() + "' where Atmid='" +
                                    reader[0].ToString() + "' and date='" + reader[4].ToString() + "' and refno='" + vid + "'";
                            obj.NonExecuteQuery(query);
                        }
                    }
                    else if (Convert.ToInt32(reader[6].ToString()) <= 7)
                    {
                        query = "Update TempScheduler set status='DEL',descp='7 day clause failed. Last audit scheduled is on " + reader[2].ToString() + "' where Atmid='" +
                                reader[0].ToString() + "' and date='" + reader[3].ToString() + "' and refno='" + vid + "'";
                        obj.NonExecuteQuery(query);

                        if (Convert.ToInt32(reader[7].ToString()) <= 7)
                        {
                            query = "Update TempScheduler set status='DEL',descp='7 day clause failed. Last audit scheduled is on " + reader[3].ToString() + "' where Atmid='" +
                                    reader[0].ToString() + "' and date='" + reader[4].ToString() + "' and refno='" + vid + "'";
                            obj.NonExecuteQuery(query);
                        }
                    }
                    else if (Convert.ToInt32(reader[7].ToString()) <= 7)
                    {
                        query = "Update TempScheduler set status='DEL',descp='7 day clause failed. Last audit scheduled is on " + reader[3].ToString() + "' where Atmid='" +
                                reader[0].ToString() + "' and date='" + reader[4].ToString() + "' and refno='" + vid + "'";
                        obj.NonExecuteQuery(query);
                    }
                }

                reader.Close();
                cn.Close();
            }

            catch (Exception ee)
            {
                Response.Write(ee.Message);
            }

            finally
            {
                reader.Close();
                cn.Close();
                cn.Dispose();
            }
        }

        public void chkvalid(string vid)
        {
            int cnt = 0; string query = "";
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            SqlCommand cnCommand = cn.CreateCommand();
            cnCommand.CommandTimeout = 500;
            SqlDataReader reader = default(SqlDataReader);
            try
            {
                // cnCommand.CommandText = @"Select userid as [USERID],Atmid as [Atmid],datepart(dd,date) as [DATE],descp as [DESCP] from TempScheduler where refno='" + vid.ToString().Trim() + "' and status='DEL'";
                cnCommand.CommandText = @"Select userid as [USERID],Atmid as [Atmid],[date] as [DATE],descp as [DESCP] from TempScheduler where refno='" + vid.ToString().Trim() + "' and status='DEL'";
                cn.Open();
                reader = cnCommand.ExecuteReader();

                while (reader.Read())
                {
                    cnt++;
                    issue.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                }

                reader.Close();
                cn.Close();
            }

            catch (Exception ee)
            {
                Response.Write(ee.Message);
            }

            finally
            {
                reader.Close();
                cn.Close();
                cn.Dispose();

                if (cnt == 0)
                {
                    issue.Rows.Add("Records uploaded", "", "", "");
                    query = @"insert into Scheduler(userid,Atmid,date,status,serverstatus,visitstatus,uploaddate,refno,type) 
                        select userid,Atmid,date,'CRE','CRE','NOT VISITED',uploaddate,refno,type from TempScheduler where refno='" + vid + "'";
                    obj.NonExecuteQuery(query);
                }

                obj.NonExecuteQuery("Delete from TempScheduler where refno='" + vid + "'");
            }
        }

        protected void mod_btnUpload_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            string date = System.DateTime.Now.ToString("dd'-'MM'-'yyyy hh:mm");
            if (mod_fileuploadExcel.HasFile)
            {
                if (System.IO.Path.GetExtension(mod_fileuploadExcel.FileName) == ".xls" || System.IO.Path.GetExtension(mod_fileuploadExcel.FileName) == ".xlsx")
                {
                    try
                    {

                        using (var excel = new ExcelPackage(mod_fileuploadExcel.PostedFile.InputStream))
                        {

                            var ws = excel.Workbook.Worksheets.First();

                            add.Columns.Add("USERD", typeof(string));

                            add.Columns.Add("Atmid", typeof(string));
                            add.Columns.Add("DATE OF AUDIT", typeof(string));
                            add.Columns.Add("type", typeof(string));

                            issue.Columns.Add("USERID", typeof(string));
                            issue.Columns.Add("Atmid", typeof(string));
                            issue.Columns.Add("DATE", typeof(string));
                            issue.Columns.Add("ISSUE", typeof(string));


                            int j = 0;
                            for (int rowNum = 2; rowNum <= 10000; rowNum++)
                            {
                                cn.Close();
                                var userid = ws.Cells[rowNum, 1, rowNum, 1].Value;
                                var Atmid = ws.Cells[rowNum, 2, rowNum, 2].Value;
                                var _date = ws.Cells[rowNum, 3, rowNum, 3].Value;
                                var type = ws.Cells[rowNum, 4, rowNum, 4].Value;


                                if (userid == null)
                                {
                                    break;
                                }
                                else
                                {
                                    string val = "";
                                    string checkuser =
                                    @"if exists(select x.Atmid,z.userid from (select Atmid from atms where Atmid='" + Atmid.ToString() + "') as x,(SELECT userid FROM users where userid='" + userid.ToString() + @"') as z)
                                select 1 as 'status'
                                else select 0 as 'status'";
                                    //"select count(*) from users where userid='" + newuserid + "'";
                                    cn.Open();
                                    SqlCommand cmd = new SqlCommand(checkuser, cn);
                                    var reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        if (Convert.ToInt32(reader[0].ToString()) != 0)
                                        {
                                            val = "OK";

                                        }
                                        else
                                        {
                                            val = "Issue";
                                            issue.Rows.Add(userid, Atmid, _date, "USERID OR Atmid DOES NOT EXISTS.");


                                        }
                                    }
                                    reader.Close();
                                    cn.Close();

                                    if (val == "OK")
                                    {
                                        if (type.ToString().Trim() == "ADD")
                                        {


                                            string[] columns2 = { "Count" };
                                            string mapck = "Select Count(*) as [Count] from Scheduler where status<>'DEL' and serverstatus<>'DEL' and  Atmid='" + Atmid.ToString().Trim() + "' and userid='" + userid.ToString().Trim() + "' and datepart(mm,date)='" + dd_month.SelectedValue + "'";
                                            string[] value2 = bucket.xread(mapck, columns2);
                                            try
                                            {
                                                int count = Convert.ToInt32(value2[0]);
                                                if (count >= 4)
                                                {

                                                    issue.Rows.Add(userid, Atmid, _date, "Only 4 Audits Allowed for a Month.");

                                                }
                                                else
                                                {

                                                    try
                                                    {
                                                        int datevalue = Convert.ToInt32(_date);
                                                        int datevalue1 = Convert.ToInt32(System.DateTime.Now.ToString("dd"));
                                                        if (datevalue >= datevalue1)
                                                        {
                                                            if (datevalue < 31)
                                                            {
                                                                string previous = "";
                                                                string next = "";
                                                                string _date1 = datevalue.ToString();
                                                                if (datevalue < 9)
                                                                {
                                                                    _date1 = "0" + datevalue.ToString();
                                                                }
                                                                string _month = dd_month.SelectedValue;
                                                                if (Convert.ToInt32(dd_month.SelectedValue) > 9)
                                                                {
                                                                    _month = "0" + dd_month.SelectedValue;
                                                                }

                                                                string[] c = { "previous" };
                                                                string m = "select  top 1 convert(VARCHAR(10),vdate,103) as [previous]  from scheduler where convert(varcHAR(10),vdate,103) <'" + _month + "/" + _date1 + "/" + System.DateTime.Now.ToString("yyyy") + "' and Atmid='" + Atmid.ToString().Trim() + "' and userid='" + userid.ToString().Trim() + "' and datepart(mm,vdate)='" + dd_month.SelectedValue + "' order by vdate desc";
                                                                string[] v = bucket.xread(m, c);
                                                                try
                                                                {
                                                                    previous = v[0].ToString();
                                                                }
                                                                catch { }

                                                                string[] c1 = { "next" };
                                                                string m1 = "select  top 1 convert(VARCHAR(10),vdate,103) as [next]  from scheduler where convert(varcHAR(10),vdate,103) > '" + _month + "/" + _date1 + "/" + System.DateTime.Now.ToString("yyyy") + "' and Atmid='" + Atmid.ToString().Trim() + "' and userid='" + userid.ToString().Trim() + "' and datepart(mm,vdate)='" + dd_month.SelectedValue + "' order by vdate asc";
                                                                string[] v1 = bucket.xread(m1, c1);
                                                                try
                                                                {
                                                                    next = v1[0].ToString();
                                                                }
                                                                catch { }



                                                                int previousvalue = 0;
                                                                int nextvalue = 0;
                                                                if (next == "")
                                                                { }
                                                                else
                                                                {
                                                                    string[] c11 = { "nextcount" };
                                                                    string m11 = "select  DATEDIFF(DD,'" + dd_month.SelectedValue + "/" + _date1 + "/" + System.DateTime.Now.ToString("yyyy") + "','" + next + "') as [nextcount]  from scheduler";
                                                                    string[] v11 = bucket.xread(m11, c11);
                                                                    try
                                                                    {
                                                                        nextvalue = Convert.ToInt32(v11[0]);
                                                                    }
                                                                    catch { }
                                                                }
                                                                if (previous == "")
                                                                { }
                                                                else
                                                                {
                                                                    string[] c11x = { "previouscount" };
                                                                    string m11x = "select  DATEDIFF(DD,'" + previous + "','" + dd_month.SelectedValue + "/" + _date1 + "/" + System.DateTime.Now.ToString("yyyy") + "') as [previouscount]";
                                                                    string[] v11x = bucket.xread(m11x, c11x);
                                                                    try
                                                                    {

                                                                        previousvalue = Convert.ToInt32(v11x[0]);
                                                                    }
                                                                    catch { }
                                                                }

                                                                if (previousvalue < 0 && nextvalue < 0)
                                                                {
                                                                    string query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type) values ('" + userid.ToString() + "','" + Atmid.ToString() + "','" + dd_month.SelectedValue + "/" + _date1 + "/" + System.DateTime.Now.ToString("yyyy") + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','WEEKLY')";


                                                                    int done = obj.NonExecuteQuery(query);
                                                                    if (done > 0)
                                                                    {
                                                                        mod_lblerror.Visible = true;
                                                                        mod_lblerror.Text = "Upload Succesfull";
                                                                        mod_lblerror.ForeColor = Color.Green;
                                                                    }
                                                                    else
                                                                    {

                                                                        issue.Rows.Add(userid, Atmid, _date, "Scheduler Already Exits.");
                                                                    }
                                                                }
                                                                else if (previousvalue > 7 && nextvalue > 7)
                                                                {
                                                                    string query = "insert into scheduler (userid, Atmid, date,visitstatus,uploaddate,refno,type) values ('" + userid.ToString() + "','" + Atmid.ToString() + "','" + dd_month.SelectedValue + "/" + _date1 + "/" + System.DateTime.Now.ToString("yyyy") + "','NOT VISITED','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','s" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy'-'hh:mm:ss") + "','WEEKLY')";


                                                                    int done = obj.NonExecuteQuery(query);
                                                                    if (done > 0)
                                                                    {
                                                                        mod_lblerror.Visible = true;
                                                                        mod_lblerror.Text = "Upload Succesfull";
                                                                        mod_lblerror.ForeColor = Color.Green;
                                                                    }
                                                                    else
                                                                    {

                                                                        issue.Rows.Add(userid, Atmid, _date, "Scheduler Already Exits.");
                                                                    }
                                                                }
                                                                else
                                                                {

                                                                    issue.Rows.Add(userid, Atmid, _date, "Difference between Schedule Date should be greater than 7.");
                                                                }

                                                            }

                                                            else
                                                            {

                                                                issue.Rows.Add(userid, Atmid, _date, "Incorrrect Day.");
                                                            }


                                                        }
                                                        else
                                                        {

                                                            issue.Rows.Add(userid, Atmid, _date, "Day cannot be less then Today.");
                                                        }

                                                    }
                                                    catch
                                                    {

                                                        issue.Rows.Add(userid, Atmid, _date, "Enter Only Day.");

                                                    }
                                                }
                                            }
                                            catch { }


                                        }
                                        else if (type.ToString().Trim() == "DEL")
                                        {

                                            string _month = dd_month.SelectedValue;
                                            if (Convert.ToInt32(dd_month.SelectedValue) < 9)
                                            {
                                                _month = "0" + dd_month.ToString();
                                            }
                                            string _date1 = _date.ToString();
                                            if (Convert.ToInt32(_date) < 9)
                                            {
                                                _date1 = "0" + _date.ToString();
                                            }
                                            string userMap = "update scheduler set status='DEL',serverstatus='DEL' where Atmid='" + Atmid + "' and date='" + _month + "/" + _date1 + "/" + System.DateTime.Now.ToString("yyyy") + "'"; //"insert into scheduler (userid, Atmid, date,status,serverstatus) values ('" + userid + "','" + Atmid + "','" + _date + "','CRE','CRE')";
                                            cn.Open();
                                            SqlCommand cmd2 = new SqlCommand(userMap, cn);
                                            cmd2.ExecuteNonQuery();
                                            cn.Close();


                                        }
                                        else
                                        {
                                            issue.Rows.Add(userid, Atmid, _date, "Unable to Add/Delete.");
                                        }

                                    }

                                }

                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        mod_lblerror.Text = ex.Message.ToString();
                        mod_lblerror.ForeColor = Color.Red;
                    }
                    if (issue.Rows.Count > 0)
                    {
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GridView2.DataSource = issue;
                            GridView2.AllowPaging = false;

                            GridView2.AllowSorting = false;

                            GridView2.DataBind();
                            GridView2.GridLines = GridLines.Both;
                            GridView2.HorizontalAlign = HorizontalAlign.Center;
                            Response.AddHeader("content-disposition", "attachment;filename=SchedularIssue.xls");
                            Response.Charset = String.Empty;
                            Response.ContentType = "application/vnd.xls";
                            GridView2.RenderControl(hw);
                            Response.Write(sw.ToString());
                            Response.Flush();
                            Response.End();

                        }
                    }
                }
                else
                {
                    mod_lblerror.Visible = true;
                    mod_lblerror.Text = "Select Excel File Only";
                    mod_lblerror.ForeColor = Color.Red;
                }

            }
            else
            {

                mod_lblerror.Visible = true;
                mod_lblerror.Text = "Select File To Import";
                mod_lblerror.ForeColor = Color.Red;
            }
        }

        protected void lnkformat_Click(object sender, EventArgs e)
        {
            string XlsPath = Server.MapPath(@"~/Uploadformat/Upload.xlsx");
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

        private void GenerateReport()
        {
            using (ExcelPackage p = new ExcelPackage())
            {
                try
                {
                    #region firstsheet
                    //Create a sheet
                    ExcelWorksheet ws = CreateSheet(p, "Schedular");
                    CreateData(ws);
                    ws.View.ZoomScale = 100; // Set the zoom size of worksheet
                    #endregion

                    //Generate A File with name
                    string file = "Schedular.xlsx";
                    try
                    {
                        byte[] reportData = p.GetAsByteArray();
                        Response.Clear();
                        Response.AddHeader("content-disposition", "attachment;  filename=" + file);
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

                    //These lines will open it in Excel
                    ProcessStartInfo pi = new ProcessStartInfo(file);
                    Process.Start(pi);

                    p.Dispose();
                }

                catch (Exception ee)
                {
                    //Label1.Text = ee.Message.ToString();
                }

                finally
                {
                    p.Dispose();
                }
            }
        }

        private ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[1];
            ws.Name = sheetName; //Setting Sheet's name
            ws.View.ShowGridLines = false;
            //ws.View.FreezePanes(1, 11);
            return ws;
        }

        private void colvalues(ExcelWorksheet ws, string a, int i, int j, bool x, bool y)
        {
            ws.Cells[i, j].Value = a;
            ws.Cells[i, j].Style.Border.Left.Style = ws.Cells[i, j].Style.Border.Bottom.Style = ws.Cells[i, j].Style.Border.Right.Style = ws.Cells[i, j].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[i, j].Style.Font.SetFromFont(new Font("Calibri", 11));
            ws.Cells[i, j].Style.Fill.PatternType = ExcelFillStyle.Solid;
            if (y == false)
            {
                ws.Cells[i, j].Style.Fill.BackgroundColor.SetColor(Color.White);
            }
            else
            {
                ws.Cells[i, j].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(197, 190, 151));
            }
            ws.Cells[i, j].Style.Font.Bold = x;
            ws.Cells[i, j].Style.WrapText = true;
            ws.Cells[i, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[i, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void CreateData(ExcelWorksheet ws)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            //ws.Column(1).AutoFit();
            //ws.Column(2).AutoFit();
            //ws.Column(3).AutoFit();
            ws.Column(1).Width = 20;
            ws.Column(2).Width = 15;
            ws.Column(3).Width = 35;
            ws.Column(4).Width = 20;
            ws.Column(5).Width = 20;

            colvalues(ws, "Userid", 1, 1, true, true);
            colvalues(ws, "Atmid", 1, 2, true, true);
            colvalues(ws, "Date(MM/DD/YYYY)", 1, 3, true, true);
            colvalues(ws, "DESCP(SV/JV/PIP)", 1, 4, true, true);
            colvalues(ws, "TYPE(DAILY/WEEKLY/FORTHNIGHTLY)", 1, 5, true, true);
            colvalues(ws, "DESCP", 2, 7, true, true);
            colvalues(ws, "Short Form", 2, 8, true, true);
            colvalues(ws, "Site Visit", 3, 7, true, true);
            colvalues(ws, "	SV", 3, 8, true, true);
            colvalues(ws, "Joint Visit", 4, 7, true, true);
            colvalues(ws, "JV", 4, 8, true, true);
            colvalues(ws, "PIP", 5, 7, true, true);
            colvalues(ws, "PIP", 5, 8, true, true);
            colvalues(ws, "Feasibility Survey", 6, 7, true, true);
            colvalues(ws, "FS", 6, 8, true, true);
            colvalues(ws, "Branch Meeting", 7, 7, true, true);
            colvalues(ws, "BM", 7, 8, true, true);



            int srno = 0; int i = 2;

            string query = "Select Atmid from SITEs where SITEstatus='Active' and Region like '%' and branchid<>'Mphasis'";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                cn.Open();
                SqlDataReader reader = default(SqlDataReader);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    colvalues(ws, "", i, 1, true, false);
                    colvalues(ws, reader[0].ToString().Trim(), i, 2, true, false);
                    colvalues(ws, "", i, 3, true, false);
                    colvalues(ws, "", i, 4, true, false);
                    colvalues(ws, "", i, 5, true, false);
                    srno++; i++;
                }

                reader.Close();
                cn.Close();
            }

            catch (Exception ee)
            {
                //Response.Write(ee.Message);
            }

            finally
            {
                cn.Close();
                if (srno == 0)
                {
                    ws.Cells[2, 1, 2, 3].Merge = true;
                    colvalues(ws, "No records found", 2, 1, false, false);
                }
            }
        }

        protected void mod_lnkformat_Click(object sender, EventArgs e)
        {
            string query = "select USERID,Atmid,DATEPART(D,date) as [DATE OF AUDIT],'ADD/DEL' as [type] from scheduler where datepart(mm,date)='" + dd_month.SelectedValue + "' and datepart(yyyy,date)=datepart(yyyy,getdate())";
            sql_mod.SelectCommand = query;
            sql_mod.DataBind();

            grid_mod.DataSourceID = sql_mod.ID;
            grid_mod.DataBind();

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                Response.AddHeader("content-disposition", "attachment;filename=ModifyScheduler.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                grid_mod.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

            //string XlsPath = Server.MapPath(@"~/Uploadformat/Format2.xlsx");
            //FileInfo fileDet = new System.IO.FileInfo(XlsPath);
            //Response.Clear();
            //Response.Charset = "UTF-8";
            //Response.ContentEncoding = Encoding.UTF8;
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileDet.Name));
            //Response.AddHeader("Content-Length", fileDet.Length.ToString());
            //Response.ContentType = "application/ms-excel";
            //Response.WriteFile(fileDet.FullName);
            //Response.End();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.DataBind();
        }

        protected string CleanDate(string date, string userid, string Atmid)
        {
            string[] _date = date.Split(new Char[] { '-', '/', '.', ' ' });

            List<string> month = new List<string>();
            foreach (ListItem sd in ddlmonth.Items)
            {
                month.Add(sd.Text);
            }


            if (_date.Count() > 2)
            {
                for (int i = 0; i <= _date.Length; i++)
                {

                    if (date.Contains(ddlmonth.SelectedItem.ToString()))
                    {
                        string val = _date[i];
                        string val1 = ddlmonth.SelectedValue;
                        date = date.Replace(_date[i], val1);
                        date = date.Replace("-", "/").Replace(".", "/");
                        break;
                    }
                }
            }


            return date;
        }

        protected void btnExtract_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.AllowPaging = false;


                GridView2.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public void createTicket(string faultdesc, string Atmid, string date, string userid)
        {
            string DocketNumCount = "";
            string[] vdatetime = date.Split(new Char[] { '/' });
            date = vdatetime[2] + "-" + vdatetime[0] + "-" + vdatetime[1];

            string[] c = { "count" };
            string dockquery = "Select max(DocketNumber) as count from IncidentsNew1";
            string[] v1 = bucket.xread(dockquery, c);
            string va = "";
            try { v1[0].ToString(); }
            catch (Exception er)
            { va = "1"; }
            if (va != "1")
            {
                DocketNumCount = Convert.ToString(Convert.ToInt32(v1[0].ToString()) + 1);
            }
            string msec = System.DateTime.Now.Millisecond.ToString();
            string DocketNum = "T" + System.DateTime.Now.ToString("MMddyyyy") + DocketNumCount + msec;
            string[] c1 = { "faultid" };
            string fault = "Select faultid from faultcode where faultdescription='" + faultdesc.ToString().Trim() + "'";
            string[] v = bucket.xread(fault, c1);
            string value = "";
            try { v[0].ToString(); }
            catch (Exception er)
            { value = "1"; }
            if (value != "1")
            {

                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("declare @docket varchar(50),@Atmid varchar(50),@faultid varchar(10),@calldesc varchar(50),@remark varchar(50),@opendate varchar(50),@userid varchar(50)");
                    sb.AppendLine("select @docket='" + DocketNum.Trim() + "',@faultid='" + v[0].ToString() + "',@Atmid='" + Atmid.Trim().Replace("'", "") + "',@calldesc='" + faultdesc.Trim().Replace("'", "") + "',@remark='',@opendate='" + date + "',@userid='" + userid + "'");
                    sb.AppendLine(@"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                                                IF EXISTS (select Atmid from SITEs where Atmid like @Atmid)
                                                BEGIN IF EXISTS (select faultdescription from faultcode where faultdescription like @calldesc)
                                                BEGIN IF NOT EXISTS (Select I.DDocketNumber from incidentsnew1 i where I.CallStatus!='CLOSE' and ltrim(rtrim(I.Atmid))=@Atmid and ltrim(rtrim(faultid))= @faultid and OpenDate=@opendate and Incidentopenby=@userid)
                                                BEGIN Insert Into IncidentsNew1 (DDOCKETNUMBER,Atmid,faultid,OpenDate,CallStatus,Remark,IncidentOpenBy,CallPriority,ivdate) VALUES (@docket,@Atmid,@faultid,'" +
                     date + "','OPEN',@remark," + "'" + userid + "',3,'" + DateTime.Now.ToString() + "') END END END COMMIT TRANSACTION");
                    // Response.Write(sb.ToString());
                    int i = obj.NonExecuteQuery(sb.ToString());

                    if (i > 0)
                    {

                    }
                    else
                    {

                    }
                }


                catch (Exception ee)
                {
                    string a = ee.Message;
                    //Response.Write("" + a);
                }
            }
        }
    }
}