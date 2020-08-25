using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.CH
{
    public partial class IncidentMasterReport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        CommonClass obj = new CommonClass();
        string status = "";
        ibuckethead bucket = new ibuckethead();
        bool checkpage = true;

        string callstatus = "";
        int cnt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            //Label1.Visible = false;
            string updatedwntimeonload = @"Update IncidentsNew1 set downtime=cast((cast(cast(getdate() as float) - cast(opendate as float) as int) * 24) + datepart(hh, getdate() - opendate) as varchar(10)) 
                                        + ':' + right('0' + cast(datepart(mi, getdate() - opendate) as varchar(2)), 2) where (callstatus='OPEN' or callstatus='DISPATCHED' or callstatus='RE-OPEN')";
            obj.NonExecuteQuery(updatedwntimeonload);




            string upddwntime = @"Update IncidentsNew1 set Downtime=(Select CONVERT(varchar(5),datediff(s,opendate,resolveddate)/3600) + ':' + CONVERT(varchar(5),datediff(s,opendate,resolveddate)/3600%60)),DTStatus='SYN' where (CallStatus='RESOLVED' or CallStatus='CLOSE')
                            and DTStatus='CRE'";
            obj.NonExecuteQuery(upddwntime);


            //update query to replace '-' in downtime when it is calculated in negative
            //downtime is calculated in negative when opendate from application is greater than the current date

            string upddwntimenew = @"Update IncidentsNew1 set Downtime=REPLACE(downtime,'-','') where downtime like '%-%'";
            obj.NonExecuteQuery(upddwntimenew);



            if (!Page.IsPostBack)
            {
                //status = "";
                chkAll.Checked = true;


                for (int i = 0; i < ddcalltype.Items.Count; i++)
                {
                    ddcalltype.Items[i].Selected = true;

                }



                ////ddcalltype1.Text = status.Remove("'");
                ////Response.Write(ddcalltype1.Text);
                lblTotalSelectedEmailCount.Text = ddcalltype.Items.Count.ToString() + " item(s) selected";

                //StartDate_CalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                //EndDate_CalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                string sessrole = "";
                try
                {
                    sessrole = Session["sess_role"].ToString();
                }
                catch (Exception ee)
                {
                    Response.Redirect("~/Login.aspx");
                }

                dd_faultC.Items.Add("ALL");
                dd_faultC.Items.FindByText("ALL").Value = "%";
                string query1x = "Select distinct [faultcode] FROM [faultcode]";
                obj.BindDropDown(dd_faultC, query1x);

                try
                {
                    if (Request.QueryString["fault"].ToString() != "")
                    {
                        dd_faultC.Items.FindByText(Request.QueryString["fault"].ToString()).Selected = true;

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
                    dd_faultC.Items.FindByValue("%").Selected = true;
                }

                //ddbank.DataBind();
                ddbank.Items.Add("ALL");
                ddbank.Items.FindByText("ALL").Value = "%";
                ddbank.Items.FindByValue("%").Selected = true;
                string query1 = "Select distinct ltrim(rtrim(bankid)) FROM [atms] where CH like '" + Session["sess_username"] + "'";
                obj.BindDropDown(ddbank, query1);

                ddagency.Items.Add("ALL");
                ddagency.Items.FindByText("ALL").Value = "%";
                ddagency.Items.FindByValue("%").Selected = true;
                string query9 = "Select distinct client FROM [atms] where CH like '" + Session["sess_username"] + "' ";
                obj.BindDropDown(ddagency, query9);

                //ddcity.DataBind();
                //ddcity.Items.Add("ALL");
                //ddcity.Items.FindByText("ALL").Value = "%";
                //ddcity.Items.FindByValue("%").Selected = true;
                string query3 = "Select distinct [username] as [RCM] FROM [Users] where username like '" + Session["sess_username"] + "'";
                obj.BindDropDown(ddcity, query3);

                ddzone.DataBind();
                ddzone.Items.Add("ALL");
                ddzone.Items.FindByText("ALL").Value = "%";
                ddzone.Items.FindByValue("%").Selected = true;

                string qzone = "Select distinct [state] as [state] FROM [ATMS] where CH like '" + Session["sess_username"] + "'";
                obj.BindDropDown(ddzone, qzone);


                ddproject.Items.Add("ALL");
                ddproject.Items.FindByText("ALL").Value = "%";
                ddproject.Items.FindByValue("%").Selected = true;
                string queryx = "Select distinct project FROM [ATMs]";
                obj.BindDropDown(ddproject, queryx);

                //ddcalltype.DataBind();
                //ddcalltype.Items.Add("ALL");
                //ddcalltype.Items.FindByText("ALL").Value = "%";
                //ddcalltype.Items.FindByValue("%").Selected = true;
                ////ddcalltype.Items.Add("NO ISSUES");
                ////ddcalltype.Items.FindByText("NO ISSUES").Value = "NO ISSUES";
                //string query4 = "Select distinct ltrim(rtrim(callstatus)) as [callstatus] FROM [IncidentsNew1] order by callstatus asc";
                //obj.BindDropDown(ddcalltype, query4);

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
                    //Label3.Text = "01/09/2014";
                    Label3.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
                }

                string[] words = Label3.Text.Split('/');
                Label5.Text = words[1] + '/' + words[0] + '/' + words[2];
                string[] words1 = Label4.Text.Split('/');
                Label6.Text = words1[1] + '/' + words1[0] + '/' + words1[2];

                Hid1.Value = "%";
                Hid2.Value = "%";

                SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("callstatus in (@callstatus)", "Callstatus like '%'");
                SqlDataSource1.DataBind();


                GridView1.AllowPaging = true;
                GridView1.AllowSorting = false;

                GridView1.DataSourceID = "SqlDataSource1";
                GridView1.DataBind();
            }
            else
            {
                for (int i = 0; i < ddcalltype.Items.Count; i++)
                {
                    if (ddcalltype.Items[i].Selected == true)
                    {
                        status += ddcalltype.Items[i].Text + "','";
                        cnt++;
                    }
                }

                if (cnt == 0)
                {
                    status = "";
                }

                lblTotalSelectedEmailCount.Text = cnt.ToString() + " item(s) selected";

                if (status.Length > 3)
                {
                    status = status.Remove(status.Length - 3);
                }
                if (txtEndDate.Text == "")
                {
                    Label4.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
                }
                else
                {
                    Label4.Text = txtEndDate.Text;
                }

                if (txtStartDate.Text == "")
                {
                    //Label3.Text = "01/09/2014";
                    Label3.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
                }
                else
                {
                    Label3.Text = txtStartDate.Text;
                }
                // ddcalltype1.Text = status;

                SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand.ToString().Replace("callstatus in (@callstatus)", "callstatus in ('" + status + "')");

                SqlDataSource1.DataBind();
                GridView1.DataSourceID = "SqlDataSource1";
                GridView1.DataBind();
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
                if (e.Row.Cells[0].Text.Contains('M') || e.Row.Cells[0].Text.Contains('T'))
                {
                    hp.NavigateUrl = "CallEntry.aspx?doc=" + docket;
                    e.Row.Cells[0].Controls.Add(hp);
                }

                if (e.Row.Cells[0].Text.Contains('M') || e.Row.Cells[0].Text.Contains('-'))
                {
                    e.Row.Cells[11].Text = "TAB";
                }
                else
                {
                    e.Row.Cells[11].Text = "CONSOLE";
                }

                //con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
                SqlDataReader dr = default(SqlDataReader);
                try
                {
                    con.Open();
                    string query = @"Select subcalltype,Comments,CONVERT(VARCHAR(10), RemarkDate, 105) + ' '+ CONVERT(VARCHAR(5), RemarkDate, 108),UpdatedBy,srno
                                    from Remarks where docketnumber='" + docket + "' order by srno asc";
                    SqlCommand cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[0].ToString().Trim() == "CLOSED")
                        {
                            callstatus = "CLOSE";
                            e.Row.Cells[16].Text = dr[3].ToString().Trim();
                            e.Row.Cells[17].Text = dr[2].ToString().Trim();
                        }
                        else if (dr[0].ToString().Trim() == "RESOLVED")
                        {
                            callstatus = "RESOLVED";
                            e.Row.Cells[16].Text = dr[3].ToString().Trim();
                            e.Row.Cells[17].Text = dr[2].ToString().Trim();
                        }
                        else if (dr[0].ToString().Trim() == "DISPATCHED")
                        {
                            callstatus = "DISPATCHED";
                        }

                        e.Row.Cells[19].Text = dr[1].ToString().Trim();
                        e.Row.Cells[18].Text = dr[2].ToString().Trim();
                    }

                    if (callstatus == "")
                    {
                        callstatus = "OPEN";
                        e.Row.Cells[19].Text = "";
                        e.Row.Cells[18].Text = "";
                    }
                }

                catch (Exception ee)
                {
                }

                finally
                {
                    dr.Close();
                    con.Close();
                }

                string downtime = e.Row.Cells[8].Text.ToString();
                string elapsetime = e.Row.Cells[9].Text.ToString();
                string dt = downtime.Replace(":", "");
                string et = elapsetime.Replace(":", "");

                //Response.Write(dt); Response.Write(et);

                if (callstatus == "DISPATCHED" || callstatus == "OPEN" || callstatus == "RE-OPEN")
                {
                    //e.Row.Cells[20].Text = "No Attachment available";
                    try
                    {
                        if (Convert.ToInt32(dt) > Convert.ToInt32(et))
                        {
                            for (int i = 0; i < 22; i++)
                            {

                                e.Row.Cells[i].BackColor = Color.FromName("#FD7A77");
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 22; i++)
                            {
                                e.Row.Cells[i].BackColor = Color.FromName("#FFFF99");
                            }
                        }
                    }
                    catch { }
                }
                else if (callstatus == "RESOLVED")
                {
                    for (int i = 0; i < 22; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromName("#C4D79B");
                    }
                }
                else if (callstatus == "CLOSE")
                {
                    HyperLink hp1 = new HyperLink();
                    hp1.Text = "Download";
                    // hp1.NavigateUrl = "AttachDwnld.aspx?auditid=" + docket;
                    e.Row.Cells[20].Controls.Add(hp1);

                    for (int i = 0; i < 22; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromName("#C4D79B");
                    }
                    //e.Row.Cells[12].Text = "-";
                    //e.Row.BackColor = Color.FromName("#C4D79B");
                }

                string[] splitString = e.Row.Cells[8].Text.Split(':');
                e.Row.Cells[8].Text = (Convert.ToInt32(splitString[0].Trim()) / 24).ToString() + " day(s)";
                e.Row.Cells[9].Text = (Convert.ToInt32(e.Row.Cells[9].Text.Replace(":00", "")) / 24).ToString() + " day(s)";

                //string a = "select CT,HK,RMA,AC,UPS from atms where atmid='"+e.Row.Cells[1].Text+"'";
                string Agency = "";
                try
                {
                    Agency = e.Row.Cells[7].Text.Substring(e.Row.Cells[7].Text.IndexOf("/") + 1);
                    e.Row.Cells[7].Text = e.Row.Cells[7].Text.Remove(e.Row.Cells[7].Text.IndexOf("/"));
                }
                catch (Exception ex)
                { }
                //try
                //{
                //    string[] r = e.Row.Cells[20].Text.ToString().Split('/');
                //    //e.Row.Cells[20].Text = r[0];
                //    if (Agency == "CT Agency")
                //    {
                //        e.Row.Cells[20].Text = r[0];
                //    }
                //    else if (Agency == "HK Agency")
                //    {

                //        e.Row.Cells[20].Text = r[1];
                //    }

                //    else if (Agency == "AC Agency")
                //    {
                //        e.Row.Cells[20].Text = r[3];
                //    }
                //    else if (Agency == "UPS Agency")
                //    {
                //        e.Row.Cells[20].Text = r[4];
                //    }
                //    else if (Agency == "EBILL Agency")
                //    {
                //        e.Row.Cells[20].Text = r[5];
                //    }
                //    else if (Agency == "ATM Agency")
                //    {
                //        e.Row.Cells[20].Text = r[6];
                //    }
                //    else if (Agency == "RM Agency")
                //    {
                //        e.Row.Cells[20].Text = r[2];
                //    }
                //}
                //catch (Exception ex)
                //{ }
            }

            //if (IsPostBack)
            //{
            //Response.Write("1");
            //        if (checkpage == true)
            //        {
            //            //Response.Write("2");
            //            if (e.Row.RowIndex == -1 && e.Row.RowType == DataControlRowType.Header)
            //            {
            //                //Response.Write("3");
            //                if ((ddcalltype.SelectedItem.Text == "ALL" || ddcalltype.SelectedItem.Text == "NO ISSUES") && dd_faultC.SelectedItem.Text == "ALL" && dddowntime.SelectedItem.Text == "ALL")
            //                {
            //                    SqlDataReader myreader = default(SqlDataReader);
            //                    int x = 0; string chck = "";
            //                    string[] words = Label3.Text.Split('/');
            //                    string s1 = words[1] + '/' + words[0] + '/' + words[2];
            //                    string[] words1 = Label4.Text.Split('/');
            //                    string s2 = words1[1] + '/' + words1[0] + '/' + words1[2];

            //                    if (txtCriteria1.Text == "")
            //                    {
            //                        chck = "%";
            //                    }
            //                    else
            //                    {
            //                        chck = txtCriteria1.Text;
            //                    }


            //                    string str = @"Select distinct d.atmid,ax.bankid,ax.rcm,ax.state,d.userid,Convert(varchar(10),Convert(datetime,d.vdate),105) + ' ' + vtime,d.vid,d.srno from dr_ctp d inner join atms ax on d.ATMID=ax.atmid where d.atmid not in 
            //                    (Select distinct i.ATMID from IncidentsNew1 i inner join atms a on i.ATMID=a.atmid
            //                    where a.RCM like '" + ddcity.SelectedValue + "' and a.branchid like '" + ddproject.SelectedValue + "' and a.state like '" + ddzone.SelectedValue + "' and a.bankid like '" + ddbank.SelectedValue +
            //                            "' and CONVERT(date,OpenDate,103) between CONVERT(date,'" + Label3.Text + "',103) and CONVERT(date,'" + Label3.Text +
            //                            "',103)) and convert(date,vdate) between '" + s1 + "' and '" + s2 + "' and ax.RCM like '" + ddcity.SelectedValue +
            //                            "' and ax.state like '" + ddzone.SelectedValue + "' and ax.branchid like '" + ddproject.SelectedValue +
            //                            "' and ax.bankid like '" + ddbank.SelectedValue + "' and (d.atmid like '" + chck + "' or d.vid like '" + chck + "') order by d.srno asc";
            //                    //Response.Write(str);
            //                    try
            //                    {
            //                        SqlCommand cmd = new SqlCommand(str, con);
            //                        con.Open();
            //                        myreader = cmd.ExecuteReader();
            //                        while (myreader.Read())
            //                        {
            //                            GridViewRow gvRow = new GridViewRow(x, x, DataControlRowType.DataRow, DataControlRowState.Insert);

            //                            for (int i = 0; i < e.Row.Cells.Count; i++)
            //                            {
            //                                TableCell tCell = new TableCell();
            //                                if (i == 0)
            //                                {
            //                                    tCell.Text = myreader[6].ToString();
            //                                }
            //                                else if (i == 1)
            //                                {
            //                                    tCell.Text = myreader[0].ToString();
            //                                }
            //                                else if (i == 2)
            //                                {
            //                                    tCell.Text = myreader[1].ToString();
            //                                }
            //                                else if (i == 3)
            //                                {
            //                                    tCell.Text = myreader[2].ToString();
            //                                }
            //                                else if (i == 4)
            //                                {
            //                                    tCell.Text = myreader[3].ToString();
            //                                }
            //                                else if (i == 5)
            //                                {
            //                                    tCell.Text = "NO ISSUES";
            //                                }
            //                                else if (i == 8 || i == 9)
            //                                {
            //                                    tCell.Text = "0 day(s)";
            //                                }
            //                                else if (i == 12)
            //                                {
            //                                    tCell.Text = myreader[4].ToString();
            //                                }
            //                                else if (i == 13)
            //                                {
            //                                    tCell.Text = myreader[5].ToString();
            //                                }
            //                                else if (i == 11)
            //                                {
            //                                    if (myreader["vid"].ToString().Contains("-"))
            //                                    {
            //                                        tCell.Text = "TAB";
            //                                    }
            //                                    else
            //                                    {
            //                                        tCell.Text = "CONSOLE";
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    tCell.Text = "NA";
            //                                }

            //                                gvRow.Cells.Add(tCell);
            //                                Table tbl = e.Row.Parent as Table;
            //                                tbl.Rows.Add(gvRow);
            //                            }
            //                            x++;
            //                        }
            //                        myreader.Close();
            //                        con.Close();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        Response.Write(ex.Message);
            //                    }
            //                    finally
            //                    {
            //                        myreader.Close();
            //                        con.Close();
            //                    }
            //                }
            //            }
            //        }
            ////}
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            btnreport.Visible = true;
            //checkpage = true;
            //if(ddcalltype.SelectedValue=="%")
            //{
            //    Hid1.Value = "%"; Hid2.Value = "%";
            //}
            //else if (ddcalltype.SelectedValue == "NO ISSUES")
            //{
            //    Hid1.Value = "0"; Hid2.Value = "%";
            //}
            //else
            //{
            //    Hid1.Value = "%"; Hid2.Value = "xyz";
            //}

            //if (dddowntime.SelectedValue == "11999")
            //{
            //    Label2.Text = "000";
            //}
            //else if (dddowntime.SelectedValue == "23999")
            //{
            //    Label2.Text = "12000";
            //}
            //else if (dddowntime.SelectedValue == "35999")
            //{
            //    Label2.Text = "24000";
            //}
            //else if (dddowntime.SelectedValue == "47999")
            //{
            //    Label2.Text = "36000";
            //}
            //else if (dddowntime.SelectedValue == "71999")
            //{
            //    Label2.Text = "48000";
            //}
            //else if (dddowntime.SelectedValue == "119999")
            //{
            //    Label2.Text = "72000";
            //}
            //else if (dddowntime.SelectedValue == "120000")
            //{
            //    Label2.Text = "000";
            //}

            //if (txtEndDate.Text == "")
            //{
            //    Label4.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
            //}
            //else
            //{
            //    Label4.Text = txtEndDate.Text.Trim();
            //}

            //if (txtStartDate.Text == "")
            //{
            //    Label3.Text = System.DateTime.Today.ToString("dd'/'MM'/'yyyy");
            //}
            //else
            //{
            //    Label3.Text = txtStartDate.Text.Trim();
            //}

            //string[] words = Label3.Text.Split('/');
            //Label5.Text = words[1] + '/' + words[0] + '/' + words[2];
            //string[] words1 = Label4.Text.Split('/');
            //Label6.Text = words1[1] + '/' + words1[0] + '/' + words1[2];

            ////GridView1.AllowSorting = true;
            //GridView1.DataSourceID = "SqlDataSource1";
            //GridView1.DataBind();
        }

        protected void btnreport_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                //Page_Load(sender, e);
                GridView1.AllowPaging = false;
                //GridView1.Columns[0].Visible = false;
                GridView1.Columns[19].Visible = false;
                GridView1.AllowSorting = false;
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
            string queryx = "Select distinct project FROM [ATMs] where bankid like '" + ddbank.SelectedValue + "'";
            obj.BindDropDown(ddproject, queryx);

            ddbank.Items.Clear();
            ddbank.Items.Add("ALL");
            ddbank.Items.FindByText("ALL").Value = "%";
            ddbank.Items.FindByValue("%").Selected = true;
            string query1 = "Select distinct ltrim(rtrim(Bankid)) FROM [ATMs] where CH like '" + ddcity.SelectedValue + "' and project like '" + ddproject.SelectedValue + "'";
            obj.BindDropDown(ddbank, query1);

            //GridView1.DataSourceID = "SqlDataSource1";
            //GridView1.DataBind();

        }

        protected void ddbank_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddbank.Items.Clear();
            ddbank.Items.Add("ALL");
            ddbank.Items.FindByText("ALL").Value = "%";
            ddbank.Items.FindByValue("%").Selected = true;
            string query1 = "Select distinct ltrim(rtrim(Bankid)) FROM [ATMs] where RCM like '" + ddcity.SelectedValue + "' and project='" + ddproject.SelectedValue + "'";
            obj.BindDropDown(ddbank, query1);

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
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.DataBind();
        }
    }
}