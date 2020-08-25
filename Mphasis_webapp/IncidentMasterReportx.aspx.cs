using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Obout.ComboBox;

namespace Mphasis_webapp
{
    public partial class IncidentMasterReportx : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        CommonClass obj = new CommonClass();
        ibuckethead bucket = new ibuckethead();
        bool checkpage = true;
        SqlConnection conn = new SqlConnection();
        SqlConnection con1 = new SqlConnection();
        string callstatus = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Label1.Visible = false;
            //CreateACvpr();
            //CreateSRMvpr();
            string updatedwntimeonload = @"Update IncidentsNew1 set Downtime=(Select CONVERT(varchar(5),datediff(s,DispatchDate,resolveddate)/3600) + ':' + 
                                        CONVERT(varchar(5),datediff(s,DispatchDate,resolveddate)/3600%60)),DTStatus='SYN' where (CallStatus='RESOLVED' or CallStatus='CLOSE')
                                        and DTStatus<>'SYN';Update IncidentsNew1 set Downtime=(Select CONVERT(varchar(5),datediff(s,Opendate,getdate())/3600) + ':' + 
                                        CONVERT(varchar(5),datediff(s,Opendate,getdate())/3600%60)) where (CallStatus='DISPATCHED' or CallStatus='OPEN')";
            obj.NonExecuteQuery(updatedwntimeonload);


            if (!Page.IsPostBack)
            {
                string sessrole = "";
                try
                {
                    sessrole = Session["sess_role"].ToString();
                }
                catch (Exception ee)
                {
                    Response.Redirect("~/Login.aspx");
                }

                //dd_faultC.Items.Add("ALL");
                //dd_faultC.Items.FindByText("ALL").Value = "%";
                //string query1x = "Select distinct [faultcode] FROM [faultcode]";
                //obj.BindDropDown(dd_faultC, query1x);

                try
                {
                    if (Request.QueryString["fault"].ToString() != "")
                    {
                        //dd_faultC.Items.FindByText(Request.QueryString["fault"].ToString()).Selected = true;

                        if (Request.QueryString["TAT"].ToString() == "true")
                        {
                            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("where", "where CONVERT(int,REPLACE(Downtime,':',''))<CONVERT(int,REPLACE(f.TAT,':','')) and ");
                            SqlDataSource1.DataBind();
                        }
                        else if (Request.QueryString["TAT"].ToString() == "false")
                        {
                            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("where", "where CONVERT(int,REPLACE(Downtime,':',''))>CONVERT(int,REPLACE(f.TAT,':','')) and ");
                            SqlDataSource1.DataBind();
                        }
                        else if (Request.QueryString["TAT"].ToString() == "both")
                        {

                        }

                    }
                }
                catch
                {
                    //dd_faultC.Items.FindByValue("%").Selected = true;
                }

                //ddbank.DataBind();

                string query1 = "";


                query1 = "Select distinct ltrim(rtrim(BankName)) FROM [BankMap]";


                obj.BindDropDown(ddbank, query1);

                if (ddbank.Items.Count > 1)
                {
                    ddbank.Items.Add("ALL");
                    ddbank.Items.FindByText("ALL").Value = "%";
                    ddbank.Items.FindByValue("%").Selected = true;
                }

                //ddcity.DataBind();


                string query3 = "";

                ddcity.Items.Add("ALL");
                ddcity.Items.FindByText("ALL").Value = "%";
                ddcity.Items.FindByValue("%").Selected = true;
                query3 = "Select distinct [username] as [RCM] FROM [Users] where role='RM'";
                obj.BindDropDown(ddcity, query3);




                ddzone.DataBind();
                ddzone.Items.Add("ALL");
                ddzone.Items.FindByText("ALL").Value = "%";
                ddzone.Items.FindByValue("%").Selected = true;

                ddproject.Items.Add("ALL");
                ddproject.Items.FindByText("ALL").Value = "%";
                ddproject.Items.FindByValue("%").Selected = true;
                string queryx = "Select distinct branchid FROM [ATMs]";
                obj.BindDropDown(ddproject, queryx);

                //ddcalltype.DataBind();
                ddcalltype.Items.Add("ALL");
                ddcalltype.Items.FindByText("ALL").Value = "%";
                ddcalltype.Items.FindByValue("%").Selected = true;
                //ddcalltype.Items.Add("NO ISSUES");
                //ddcalltype.Items.FindByText("NO ISSUES").Value = "NO ISSUES";
                string query4 = "Select distinct ltrim(rtrim(callstatus)) as [callstatus] FROM [IncidentsNew1] order by callstatus asc";
                obj.BindDropDown(ddcalltype, query4);

                //dddowntime.DataBind();
                dddowntime.Items.Add("ALL");
                dddowntime.Items.FindByText("ALL").Value = "120000";
                dddowntime.Items.Add("0-5 Days");
                dddowntime.Items.FindByText("0-5 Days").Value = "11999";
                dddowntime.Items.Add("6-10 Days");
                dddowntime.Items.FindByText("6-10 Days").Value = "23999";
                dddowntime.Items.Add("11-15 Days");
                dddowntime.Items.FindByText("11-15 Days").Value = "35999";
                dddowntime.Items.Add("16-20 Days");
                dddowntime.Items.FindByText("16-20 Days").Value = "47999";
                dddowntime.Items.Add("21-30 Days");
                dddowntime.Items.FindByText("21-30 Days").Value = "71999";
                dddowntime.Items.Add(">30 Days");
                dddowntime.Items.FindByText(">30 Days").Value = "119999";
                dddowntime.Items.FindByValue("120000").Selected = true;
                Label2.Text = "000";
                //Label5.Text = "CLOSE";
                //Label6.Text = "RESOLVED";

                if (txtEndDate.Text == "")
                {
                    Label4.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
                }

                if (txtStartDate.Text == "")
                {
                    Label3.Text = "01/11/2014";
                    //Label3.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
                }

                string[] words = Label3.Text.Split('/');
                Label5.Text = words[1] + '/' + words[0] + '/' + words[2];
                string[] words1 = Label4.Text.Split('/');
                Label6.Text = words1[1] + '/' + words1[0] + '/' + words1[2];

                Hid1.Value = "%";
                Hid2.Value = "%";

                GridView1.AllowPaging = true;
                GridView1.AllowSorting = false;



                SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("faultcode in (@fault)", "faultcode like '%'").Replace("and a.bankname in (Select bankname from bankmap where userid = @user)", "");
                SqlDataSource1.DataBind();
                // Response.Write(SqlDataSource1.SelectCommand);

                GridView1.DataSourceID = "SqlDataSource1";
                GridView1.DataBind();
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = false;
            }

            if (IsPostBack)
            {
                StringBuilder orderedItems = new StringBuilder();
                StringBuilder order = new StringBuilder();
                order.Append("'");
                foreach (ComboBoxItem item in ComboBox1.Items)
                {
                    CheckBox checkbox = item.FindControl("CheckBox1") as CheckBox;
                    if (checkbox.Checked)
                    {
                        if (orderedItems.Length > 0)
                        {
                            orderedItems.Append(",");

                        }
                        orderedItems.Append(item.Value);

                    }

                }
                faulthf.Value = "'" + orderedItems.ToString().Replace(",", "','") + "'";
                //Response.Write(faulthf.Value);
                // Response.Write(SqlDataSource1.SelectCommand);

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Response.Write("1");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                con.Close();
                string docket = e.Row.Cells[0].Text;
                HyperLink hp = new HyperLink();
                hp.Text = docket;
                hp.NavigateUrl = "CallEntry.aspx?doc=" + docket;
                e.Row.Cells[0].Controls.Add(hp);

                if (e.Row.Cells[0].Text.Contains('T'))
                {
                    e.Row.Cells[11].Text = "CONSOLE";
                }
                else
                {
                    e.Row.Cells[11].Text = "TAB";
                }

                string downtime = e.Row.Cells[8].Text.ToString();
                string elapsetime = e.Row.Cells[9].Text.ToString();
                string dt = downtime.Replace(":", "");
                string et = elapsetime.Replace(":", "");
                callstatus = e.Row.Cells[5].Text.ToString();
                //Response.Write(dt); Response.Write(et);

                if (callstatus == "DISPATCHED" || callstatus == "OPEN" || callstatus == "RE-OPEN")
                {
                    e.Row.Cells[20].Text = "No Attachment available";
                    try
                    {
                        if (Convert.ToInt32(dt) > Convert.ToInt32(et))
                        {
                            for (int i = 0; i < 21; i++)
                            {

                                e.Row.Cells[i].BackColor = Color.FromName("#FD7A77");
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 21; i++)
                            {
                                e.Row.Cells[i].BackColor = Color.FromName("#FFFF99");
                            }
                        }
                    }
                    catch { }
                }
                else if (callstatus == "RESOLVED")
                {
                    HyperLink hp1 = new HyperLink();
                    hp1.Text = "Download";
                    hp1.NavigateUrl = "AttachDwnld.aspx?auditid=" + docket;
                    e.Row.Cells[20].Controls.Add(hp1);

                    for (int i = 0; i < 21; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromName("#C4D79B");
                    }
                }
                else if (callstatus == "CLOSE")
                {
                    for (int i = 0; i < 21; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromName("#C4D79B");
                    }
                    //e.Row.Cells[12].Text = "-";
                    //e.Row.BackColor = Color.FromName("#C4D79B");
                }

                string[] splitString = e.Row.Cells[8].Text.Split(':');
                e.Row.Cells[8].Text = (Convert.ToInt32(splitString[0].Trim()) / 24).ToString() + " day(s)";
                e.Row.Cells[9].Text = (Convert.ToInt32(e.Row.Cells[9].Text.Replace(":00", "")) / 24).ToString() + " day(s)";
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            btnreport.Visible = true;
            //checkpage = true;
            if (ddcalltype.SelectedValue == "%")
            {
                Hid1.Value = "%"; Hid2.Value = "%";
            }
            else if (ddcalltype.SelectedValue == "NO ISSUES")
            {
                Hid1.Value = "0"; Hid2.Value = "%";
            }
            else
            {
                Hid1.Value = "%"; Hid2.Value = "xyz";
            }

            if (dddowntime.SelectedValue == "11999")
            {
                Label2.Text = "000";
            }
            else if (dddowntime.SelectedValue == "23999")
            {
                Label2.Text = "12000";
            }
            else if (dddowntime.SelectedValue == "35999")
            {
                Label2.Text = "24000";
            }
            else if (dddowntime.SelectedValue == "47999")
            {
                Label2.Text = "36000";
            }
            else if (dddowntime.SelectedValue == "71999")
            {
                Label2.Text = "48000";
            }
            else if (dddowntime.SelectedValue == "119999")
            {
                Label2.Text = "72000";
            }
            else if (dddowntime.SelectedValue == "120000")
            {
                Label2.Text = "000";
            }

            if (txtEndDate.Text == "")
            {
                Label4.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
            }
            else
            {
                Label4.Text = txtEndDate.Text.Trim();
            }

            if (txtStartDate.Text == "")
            {
                Label3.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
            }
            else
            {
                Label3.Text = txtStartDate.Text.Trim();
            }

            string[] words = Label3.Text.Split('/');
            Label5.Text = words[1] + '/' + words[0] + '/' + words[2];
            string[] words1 = Label4.Text.Split('/');
            Label6.Text = words1[1] + '/' + words1[0] + '/' + words1[2];

            if (Session["sess_role"] == "RM")
            {
                SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("and a.bankname in (Select bankname from bankmap where userid = @user)", "");
                SqlDataSource1.DataBind();
            }

            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("faultcode in (@fault)", "faultcode in (" + faulthf.Value + ")");
            SqlDataSource1.DataBind();

            //Response.Write(SqlDataSource1.SelectCommand);

            //GridView1.AllowSorting = true;
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.DataBind();
            GridView1.Columns[16].Visible = false;
            GridView1.Columns[17].Visible = false;
        }

        protected void btnreport_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                //Page_Load(sender, e);
                GridView1.AllowPaging = false;
                GridView1.Columns[18].Visible = false;
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.AllowSorting = false;
                SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("faultcode in (@fault)", "faultcode in (" + faulthf.Value + ")");
                SqlDataSource1.DataBind();

                GridView1.DataSourceID = "SqlDataSource1";
                GridView1.DataBind();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        List<Control> controls = new List<Control>();
                        foreach (Control control in cell.Controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    controls.Add(control);
                                    break;
                                case "TextBox":
                                    controls.Add(control);
                                    break;
                                case "LinkButton":
                                    controls.Add(control);
                                    break;
                                case "CheckBox":
                                    controls.Add(control);
                                    break;
                                case "RadioButton":
                                    controls.Add(control);
                                    break;
                                case "Image":
                                    controls.Add(control);
                                    break;
                            }
                        }
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                    break;
                                case "TextBox":
                                    cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                    break;
                                case "LinkButton":
                                    cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                    break;
                                case "CheckBox":
                                    cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                    break;
                                case "RadioButton":
                                    cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                    break;
                                case "Image":
                                    cell.Controls.Add(new Literal { Text = (control as System.Web.UI.WebControls.Image).AlternateText = "" });
                                    break;
                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }
                GridView1.GridLines = GridLines.Both;
                Response.AddHeader("content-disposition", "attachment;filename=IncidentMasterReport.xls");
                Response.Charset = String.Empty;
                Response.ContentType = "application/vnd.xls";
                GridView1.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
                Response.Redirect("Search.aspx");
            }
        }

        protected void ddzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddzone.DataBind();
            ddzone.Items.Add("ALL");
            ddzone.Items.FindByText("ALL").Value = "%";
            ddzone.Items.FindByValue("%").Selected = true;

            //ddproject.DataBind();
            ddproject.Items.Clear();
            ddproject.Items.Add("ALL");
            ddproject.Items.FindByText("ALL").Value = "%";
            ddproject.Items.FindByValue("%").Selected = true;
            string queryx = "Select distinct branchid FROM [ATMs] where bankid like '" + ddbank.SelectedValue + "'";
            obj.BindDropDown(ddproject, queryx);

            ddbank.Items.Clear();
            string query1 = "Select distinct ltrim(rtrim(BankName)) FROM [BankMap] where userid='" + Session["sess_userid"] + "'";
            obj.BindDropDown(ddbank, query1);

            if (ddbank.Items.Count > 1)
            {
                ddbank.Items.Add("ALL");
                ddbank.Items.FindByText("ALL").Value = "%";
                ddbank.Items.FindByValue("%").Selected = true;
            }
        }

        protected void ddbank_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query1 = "Select distinct ltrim(rtrim(BankName)) FROM [BankMap] where userid='" + Session["sess_userid"] + "'";
            obj.BindDropDown(ddbank, query1);

            if (ddbank.Items.Count > 1)
            {
                ddbank.Items.Add("ALL");
                ddbank.Items.FindByText("ALL").Value = "%";
                ddbank.Items.FindByValue("%").Selected = true;
            }

            //ddproject.Items.Clear();
            //ddproject.Items.Add("ALL");
            //ddproject.Items.FindByText("ALL").Value = "%";
            //ddproject.Items.FindByValue("%").Selected = true;
            //string query1 = "Select distinct branchid FROM [ATMs] where bankid like '" + ddbank.SelectedValue + "'";
            //obj.BindDropDown(ddproject, query1);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //if (GridView1.PageIndex == 1)
            //{
            //    checkpage = true;
            //}
            //else
            //{
            //    checkpage = false;
            //}
            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("faultcode in (@fault)", "faultcode like '%'").Replace("and a.bankname in (Select bankname from bankmap where userid = @user)", "");
            SqlDataSource1.DataBind();
            // Response.Write(SqlDataSource1.SelectCommand);
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.DataBind();
        }
    }
}