using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;

namespace Mphasis_webapp.CH
{
    public partial class CallEntry : System.Web.UI.Page
    {
        CommonClass obj = new CommonClass();
        string datecheck = "yes";
        string checkdetails = "yes";
        string custoreach = "";
        string virtualpath = ConfigurationManager.AppSettings["virtualpath"];
        string physicalpath = ConfigurationManager.AppSettings["physicalpath"];
        protected void Page_Load(object sender, EventArgs e)
        {
            try { Session["sess_userid"].ToString(); }
            catch { Response.Redirect("~/Login.aspx"); }

            string docketnum = Request.QueryString["doc"];
            string updatedwntimeonload = @"Update IncidentsNew1 set downtime=cast((cast(cast(getdate() as float) - cast(opendate as float) as int) * 24) + datepart(hh, getdate() - opendate) as varchar(10)) 
                                                                + ':' + right('0' + cast(datepart(mi, getdate() - opendate) as varchar(2)), 2) where (callstatus='OPEN' or callstatus='DISPATCHED' or callstatus='RE-OPEN') and ddocketnumber='" + docketnum + "'";
            obj.NonExecuteQuery(updatedwntimeonload);

            //btn_reopen.Click += btn_reopen_Click;
            //btn_reviewd.Click += btn_reviewd_Click;
            btn_upload.Click += btn_upload_Click;

            lbl_user.Text = Session["sess_userid"].ToString();

            Session["sess_Date"] = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");



            #region callinfo
            SqlDataReader myreader = default(SqlDataReader);
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
            if (!IsPostBack)
            {
                string query = @"Select top(1) I.DDocketNumber as DDocket, CONVERT(varchar, I.OpenDate, 103) as [Login Date],CONVERT(varchar, I.OpenDate, 108) as [Login Time],
                            a.ATMID as ATM,a.location,a.addressLine1 as [address],a.BankId as Bank,a.Client as [CustomerName],a.state as [Branch Name],
                            f.FaultCode as [Call Problem],Convert(varchar,Convert(int,Replace(f.TAT,':00',''))/24 )+ ' days', a.RCM as [Custodian 1],
                            a.project as [Custodian 2 No.],a.region as [Distance of SIte],I.Remark as [Remark],I.IncidentOpenBy as [Reported By],
                            I.Downtime as [Elapse Time],I.callstatus as [callstatus],I.subcalltype as [subcalltype],CONVERT(varchar, I.DispatchDate, 103) as [Dispatch Date],
                            CONVERT(varchar, I.DispatchDate, 108) as [Dispatch Time],CONVERT(varchar, I.closeDate, 103) as [close Date],CONVERT(varchar, I.closeDate, 108) as [close Time],
                            f.fpriority as [servicetype],f.faultdescription as [fdesc],a.city as [Distance],CONVERT(varchar, I.ResolvedDate, 103) as [Resolved Date],
                            CONVERT(varchar, I.ResolvedDate, 108) as [Resolved Time],I.resolvedBy as [ResolvedBy],Convert(varchar,Convert(date,I.followupenteredtime)),CONVERT(varchar, I.followupenteredtime, 108),I.DocketNumber as Docket
                            from IncidentsNew1 as I left outer join ATMs as a on a.atmid=I.ATMID left outer join faultcode as f on f.faultid = i.faultid 
                            where I.ddocketnumber='" + docketnum + "'";
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cn.Open();
                    myreader = cmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        lbldocket.Text = docketnum;
                        lblopendate.Text = myreader[1].ToString().Trim();
                        lblopentime.Text = myreader[2].ToString().Trim();
                        lblatmid.Text = myreader[3].ToString().Trim();
                        lblloc.Text = myreader[4].ToString().Trim();
                        lbladd.Text = myreader[5].ToString().Trim();
                        lblbank.Text = myreader[6].ToString().Trim();
                        lblclient.Text = myreader[7].ToString().Trim();
                        //lblonoff.Text = myreader[8].ToString().Trim();
                        lblbranch.Text = myreader[8].ToString().Trim();
                        lblprblmcode.Text = myreader[9].ToString().Trim();
                        lbltat.Text = myreader[10].ToString().Trim();
                        lblname1.Text = myreader[11].ToString().Trim();
                        // lblname2.Text = myreader[1].ToString().Trim();
                        lblnum1.Text = myreader[12].ToString().Trim();
                        lblnum2.Text = myreader[13].ToString().Trim();
                        lblremark.Text = myreader[14].ToString().Trim();
                        lbldocketcreatedby.Text = myreader[15].ToString().Trim();
                        string[] splitString = myreader[16].ToString().Split(':');
                        lbldowntime.Text = (Convert.ToInt32(splitString[0].ToString()) / 24).ToString() + " day(s)";
                        lblcallstatus.Text = lblsubcalltype.Text = myreader[18].ToString().Trim();
                        lbldispatchdate.Text = myreader[19].ToString().Trim();
                        txtcustreach.Text = myreader[20].ToString().Trim();
                        lblclosedate.Text = myreader[21].ToString().Trim();
                        lblclosetime.Text = myreader[22].ToString().Trim();
                        lblservicetype.Text = myreader[23].ToString().Trim();
                        lblprblmtype.Text = myreader[24].ToString().Trim();
                        lbldist.Text = myreader[25].ToString().Trim();
                        lblresdate.Text = myreader[26].ToString().Trim();
                        txtrestype.Text = myreader[27].ToString().Trim();
                        txtresolvedby.Text = myreader[28].ToString().Trim();
                        lblbankdocket.Text = "NA";
                        txttravelexpense.Text = "";
                        txttravelremark.Text = "";

                        DateTime dt = DateTime.Now;
                        txtfollowup.Text = dt.ToString("yyyy'-'MM'-'dd");
                        MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                        if (dt.ToString("tt") == "AM")
                        {
                            am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                        }
                        else
                        {
                            am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                        }

                        txtvtime.SetTime(Convert.ToInt32(dt.ToString("hh")), Convert.ToInt32(dt.ToString("mm")), Convert.ToInt32(dt.ToString("ss")), am_pm);

                        #region chk followupdate
                        try
                        {
                            if (lblcallstatus.Text == "RESOLVED" && (lblresdate.Text == "" || txtrestype.Text == ""))
                            {
                                string up = "update IncidentsNew1 set ResolvedDate=(select top 1 remarkdate from Remarks where DocketNumber='" + docketnum + "' order by Priority desc ) where DDocketNumber='" + docketnum + "'";
                                obj.NonExecuteQuery(up);
                                string[] up1 = obj.verifyReader("select  CONVERT(varchar, ResolvedDate, 103) as [ResolvedDate],CONVERT(varchar, ResolvedDate, 108) as [ResolvedTime] from  IncidentsNew1 where DDocketNumber='" + docketnum + "'", "ResolvedDate", "ResolvedTime");
                                lblresdate.Text = up1[0];
                                txtrestype.Text = up1[1];
                            }
                            else if (lblcallstatus.Text == "DISPATCHED" && (lbldispatchdate.Text == "" || txtcustreach.Text == ""))
                            {
                                string up = "update IncidentsNew1 set DispatchDate=(select top 1 remarkdate from Remarks where DocketNumber='" + docketnum + "' order by Priority desc ) where DDocketNumber='" + docketnum + "'";
                                obj.NonExecuteQuery(up);
                                string[] up1 = obj.verifyReader("select  CONVERT(varchar, DispatchDate, 103) as [DispatchDate],CONVERT(varchar, DispatchDate, 108) as [Dispatchtime] from  IncidentsNew1 where DDocketNumber='" + docketnum + "'", "DispatchDate", "Dispatchtime");
                                lbldispatchdate.Text = up1[0];
                                txtcustreach.Text = up1[1];
                            }

                        }
                        catch (Exception ex)
                        { }
                        #endregion


                        #region chk call status
                        if (lbldocket.Text.Trim().Contains('T'))
                        {
                            txtdisploc.Text = "CONSOLE";
                        }
                        else
                        {
                            txtdisploc.Text = "TAB";
                        }

                        ddsubcall.Items.Clear();

                        if (lblcallstatus.Text == "OPEN")
                        {
                            ddsubcall.DataBind();
                            ddsubcall.Items.Add("DISPATCHED");

                            lbldt.Text = lblopendate.Text.Substring(3, 2) + '-' + lblopendate.Text.Substring(0, 2) + '-' + lblopendate.Text.Substring(6, 4) + ' ' + lblopentime.Text;

                            FileUpload1.Attributes.Add("style", "display:none");
                            // btn_upload.Attributes.Add("style", "display:none");
                            //txtfollowup.Attributes.Add("style", "display:inline-block");
                            // btn_review.Visible = false;
                        }
                        else if (lblcallstatus.Text == "DISPATCHED")
                        {
                            ddsubcall.DataBind();
                            ddsubcall.Items.Add("RESOLVED");

                            lbldt.Text = lbldispatchdate.Text.Substring(3, 2) + '-' + lbldispatchdate.Text.Substring(0, 2) + '-' + lbldispatchdate.Text.Substring(6, 4) + ' ' + txtcustreach.Text;

                            FileUpload1.Attributes.Add("style", "display:inline-block");
                            //btn_upload.Attributes.Add("style", "display:none");
                            //txtfollowup.Attributes.Add("style", "display:none");
                            // btn_review.Visible = false;
                        }
                        else if (lblcallstatus.Text == "RESOLVED")
                        {
                            ddsubcall.DataBind();
                            ddsubcall.Items.Add("CLOSE");

                            lbldt.Text = lblresdate.Text.Substring(3, 2) + '-' + lblresdate.Text.Substring(0, 2) + '-' + lblresdate.Text.Substring(6, 4) + ' ' + txtrestype.Text;

                            FileUpload1.Attributes.Add("style", "display:inline-block");
                            // btn_upload.Attributes.Add("style", "display:none");
                            //txtfollowup.Attributes.Add("style", "display:none");
                            // btn_review.Visible = true;
                        }
                        else if (lblcallstatus.Text == "CLOSE")
                        {
                            // btnjust.Visible = false;
                            ddsubcall.Visible = false;
                            //ddsubcall.Items.Add("RE-OPEN");

                            FileUpload1.Attributes.Add("style", "display:none");
                            //btn_upload.Attributes.Add("style", "display:none");
                            txtfollowup.Attributes.Add("style", "display:none");
                            txtvtime.Attributes.Add("style", "display:none");
                            txtrem.Attributes.Add("style", "display:none");
                            btnupdate.Attributes.Add("style", "display:none");
                            // btn_review.Visible = false;
                        }
                        else if (lblcallstatus.Text == "RE-OPEN")
                        {
                            ddsubcall.DataBind();
                            ddsubcall.Items.Add("DISPATCHED");

                            if (myreader[29].ToString().Trim() == "")
                            {
                                string up = "update IncidentsNew1 set FollowUpEnteredTime=e.FollowUpDate from IncidentsNew1 i join Remarks e on i.DDocketNumber=e.DocketNumber where i.DDocketNumber='" + lbldocket.Text + "'";
                                obj.NonExecuteQuery(up);
                                string[] ab = obj.verifyReader("select Convert(varchar,Convert(date,I.followupenteredtime)) as date,CONVERT(varchar, I.followupenteredtime, 108) as time from Incidentnew1 where ddocketnumber='" + lbldocket.Text + "'", "date", "time");
                                lbldt.Text = ab[0].ToString().Trim() + ' ' + ab[1].ToString().Trim();

                            }
                            else
                            {
                                lbldt.Text = myreader[29].ToString().Trim() + ' ' + myreader[30].ToString().Trim();
                            }

                            FileUpload1.Attributes.Add("style", "display:none");
                            //  btn_upload.Attributes.Add("style", "display:none");
                            //txtfollowup.Attributes.Add("style", "display:inline-block");
                            // btn_review.Visible = false;
                        }
                        #endregion
                    }
                }

                catch (Exception ee)
                {
                    Label1.Text = ee.Message;
                }

                finally
                {
                    myreader.Close();
                    cn.Close();
                    cn.Dispose();
                }

                if (lblcallstatus.Text != "RESOLVED")
                {
                    // btn_review.Visible = false;
                }
            }
            #endregion

        }

        void btn_upload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string path = "" + physicalpath + "";
                string filename = Path.GetFileName(FileUpload1.FileName);
                long fsize = FileUpload1.FileContent.Length;
                FileUpload1.SaveAs(path + Request.QueryString["doc"] + "$" + filename);
                string upload = "Insert into Activity_Log(vid,AttachedFile,Remarks,Size,AttachedDate,AttachedBy) values ('" + Request.QueryString["doc"] + "','" + filename.Trim() + "',NULL,'" + fsize.ToString() + "','" + System.DateTime.Now.ToString() + "','" + Session["sess_userid"] + "')";
                obj.NonExecuteQuery(upload);
                GridView1.DataBind();
            }
            else
            {

            }
        }

        public string getRemarks()
        {
            string remarks = "";
            string docketnum = Request.QueryString["doc"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            SqlCommand cmd = con.CreateCommand();
            SqlDataReader reader = default(SqlDataReader);
            try
            {
                cmd.CommandText = "Select case when isdate(r.FollowUpDate)=1 then CONVERT(varchar, r.FollowUpdate, 103) else '' end,case when isdate(r.FollowUpDate)=1 then CONVERT(varchar, r.FollowUpdate, 108) else '' end,UpdatedBy,Comments,r.SubCallType,CONVERT(varchar(10),Remarkdate,105) + ' ' + CONVERT(varchar(10),RemarkDate,8) from Remarks r join IncidentsNew1 i on i.DDocketNumber=r.DocketNumber where  r.DocketNumber='" + docketnum + "' and (r.RemarkDate=i.DispatchDate or r.RemarkDate=i.ResolvedDate) order by Priority desc";
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string rdate = reader.GetString(0).Trim();
                    string rtime = reader.GetString(1).Trim();
                    string updatedby = reader.GetString(2).Trim();
                    string comments = reader.GetString(3).Trim();
                    string calltype = reader.GetString(4).Trim();
                    string followup = reader.GetString(5).Trim();

                    remarks += "<tr><td class='border'>" + rdate + "</td><td class='border'>" + rtime + "</td><td class='border'>" + updatedby + "</td><td class='border'>" + comments +
                                       "</td><td class='border'>" + calltype + "</td><td style='border-bottom:#000000 solid 1px;font-size:12px;font-family:Arial;height:20px'>" + followup + "</td>";
                }
                reader.Close();
                con.Close();
            }

            catch (Exception ee)
            {
                Response.Write("" + ee.Message.ToString());
            }

            finally
            {
                reader.Close();
                con.Close();
                con.Dispose();
            }
            return remarks;
        }

        public int priority()
        {
            int num = 0;
            string docketnum = Request.QueryString["doc"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            SqlCommand cmd = con.CreateCommand();
            SqlDataReader reader = default(SqlDataReader);
            try
            {
                cmd.CommandText = "SELECT TOP 1 priority from Remarks where docketnumber='" + docketnum.Trim() + "' order by priority desc";
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    num = reader.GetInt32(0);
                }

                reader.Close();
                con.Close();
            }

            catch (Exception ee)
            {

            }

            finally
            {
                reader.Close();
                con.Close();
                con.Dispose();
            }
            return num;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hp = new HyperLink();
                hp.Text = e.Row.Cells[0].Text;
                hp.NavigateUrl = "" + virtualpath + "" + Request.QueryString["doc"] + "$" + e.Row.Cells[0].Text;
                e.Row.Cells[0].Controls.Add(hp);
            }
        }

        public void movedata()
        {
            string docketnum = Request.QueryString["doc"];
            if (ddsubcall.SelectedValue.Trim() == "CLOSED")
            {

                string complete = @"UPDATE IncidentsNew1 SET callpriority=1,callstatus='CLOSE',CallClosedBy='" + Session["sess_userid"] + "'," +
                                            "closedate = (SELECT RemarkDate FROM Remarks inner join IncidentsNew1 on " +
                                            "Remarks.DocketNumber=IncidentsNew1.DDocketNumber WHERE IncidentsNew1.DDocketNumber='" + docketnum.Trim() + "' and Remarks.SubCallType='CLOSED') " +
                                            "WHERE EXISTS (SELECT Remarks.DocketNumber FROM Remarks WHERE IncidentsNew1.DDocketNumber='" + docketnum.Trim() + "') ";
                obj.NonExecuteQuery(complete);

            }
            else if (ddsubcall.SelectedValue.Trim() == "RESOLVED")
            {

                string complete = @"UPDATE IncidentsNew1 SET callpriority=1,callstatus='RESOLVED',ResolvedBy='" + Session["sess_userid"] + "'," +
                                            "ResolvedDate = (SELECT RemarkDate FROM Remarks inner join IncidentsNew1 on " +
                                            "Remarks.DocketNumber=IncidentsNew1.DDocketNumber WHERE IncidentsNew1.DDocketNumber='" + docketnum.Trim() + "' and Remarks.SubCallType='RESOLVED') " +
                                            "WHERE EXISTS (SELECT Remarks.DocketNumber FROM Remarks WHERE IncidentsNew1.DDocketNumber='" + docketnum.Trim() + "') ";
                obj.NonExecuteQuery(complete);

            }
            else if (ddsubcall.SelectedValue.Trim() == "DISPATCHED")
            {
                string strupd = @"UPDATE IncidentsNew1 SET callpriority=2,callstatus='DISPATCHED',
                                    Downtime=cast((cast(cast(getdate() as float) - cast(opendate as float) as int) * 24) + datepart(hh, getdate() - opendate) as varchar(10)) + ':' + 
                                    right('0' + cast(datepart(mi, getdate() - opendate) as varchar(2)), 2),DispatchDate = (SELECT RemarkDate FROM Remarks inner join IncidentsNew1 on 
                                    Remarks.DocketNumber=IncidentsNew1.DDocketNumber WHERE IncidentsNew1.DDocketNumber='" + docketnum.Trim() + "' and Remarks.SubCallType='DISPATCHED') " +
                                        " WHERE EXISTS (SELECT Remarks.DocketNumber FROM Remarks WHERE IncidentsNew1.DDocketNumber='" + docketnum.Trim() + "') ";
                obj.NonExecuteQuery(strupd);
            }
            else
            {
                string updatedowntime = @"UPDATE IncidentsNew1 SET 
                                                    Downtime=cast((cast(cast(getdate() as float) - cast(opendate as float) as int) * 24) + datepart(hh, getdate() - opendate) as varchar(10)) + ':' + 
                                                    right('0' + cast(datepart(mi, getdate() - opendate) as varchar(2)), 2)
                                                    WHERE DDocketNumber='" + docketnum.Trim() + "')";
                obj.NonExecuteQuery(updatedowntime);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string followupenteredtime = System.DateTime.Now.ToString("yyyy'-'MM'-'dd HH:mm:ss"); bool chkdate = true;
            DateTime time = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", txtvtime.Hour, txtvtime.Minute, txtvtime.Second, txtvtime.AmPm));

            string mystring = ""; string updatesubcalltype = ""; string q = ""; DateTime? fdate = null; DateTime? odate = null; DateTime nw = DateTime.Now;
            string docketnum = Request.QueryString["doc"];
            int prioritynum = priority();
            prioritynum = prioritynum + 1;

            string followupdate = txtfollowup.Text + " " + time.ToString("HH:mm:ss");


            try
            {
                fdate = DateTime.Parse(followupdate); odate = DateTime.Parse(lbldt.Text);

            }
            catch (Exception ee)
            {
                // Response.Write(ee.Message);
                // chkdate = false;
            }

            if (chkdate == true)
            {
                try
                {
                    if (fdate > odate && fdate < nw)
                    {
                        if (txtrem.Text.Trim() != "" && txtrem.Text.Trim() != null)
                        {
                            if (ddsubcall.SelectedValue == "DISPATCHED")
                            {
                                mystring = @"Insert Into Remarks (DocketNumber,RemarkDate,UpdatedBy,Comments,SubCallType,FollowUpDate,Priority) VALUES ('" +
                                docketnum.Trim() + "','" + followupdate.Trim() + "','" + Session["sess_userid"] + "','" + txtrem.Text.Trim().Replace("'", "") +
                                "','" + ddsubcall.SelectedValue.Trim() + "','" + followupenteredtime + "'," + prioritynum + ")";

                                if (obj.NonExecuteQuery(mystring) != 0)
                                {
                                    updatesubcalltype = @"Update IncidentsNew1 set UpdateRemark='" + txtrem.Text.Trim().Replace("'", "") +
                                    "',followupdate='" + followupdate.Trim() +
                                    "',FollowUpEnteredTime='" + followupenteredtime + "',CallStatus='" + ddsubcall.SelectedValue.Trim() +
                                    "',subcalltype='" + ddsubcall.SelectedValue.Trim() + "',Dispatchdate='" + followupdate.Trim() + "',DispatchBy='" +
                                    Session["sess_userid"] + "',callpriority='3' where ddocketnumber='" + docketnum.Trim() + "'";
                                    obj.NonExecuteQuery(updatesubcalltype);
                                    Response.Write(updatesubcalltype);

                                    q = "IF (NOT EXISTS(SELECT userid FROM TicketUpdates WHERE docketnumber='" + docketnum.Trim() + "'))" +
                                    "BEGIN INSERT INTO TicketUpdates(docketnumber,userid,UpdatedBy,UpdatedOn,Status) Select '" +
                                    docketnum.Trim() + "',userid,'" + Session["sess_userid"] + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','MOD' from usermap " +
                                    " where atmid='" + lblatmid.Text.Trim() + "' END ELSE BEGIN " +
                                    "UPDATE TicketUpdates SET status = 'MOD',UpdatedBy='" + Session["sess_userid"] + "',UpdatedOn='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                    "' where docketnumber='" + docketnum.Trim() + "' END";
                                    obj.NonExecuteQuery(q);
                                }
                            }
                            else if (ddsubcall.SelectedValue == "RESOLVED")
                            {
                                //if (FileUpload1.HasFile)
                                //{
                                mystring = @"Insert Into Remarks (DocketNumber,RemarkDate,UpdatedBy,Comments,SubCallType,FollowUpDate,Priority) VALUES ('" +
                                docketnum.Trim() + "','" + followupdate.Trim() + "','" + Session["sess_userid"] + "','" + txtrem.Text.Trim().Replace("'", "") +
                                "','" + ddsubcall.SelectedValue.Trim() + "','" + followupenteredtime + "'," + prioritynum + ")";

                                if (obj.NonExecuteQuery(mystring) != 0)
                                {
                                    updatesubcalltype = @"Update IncidentsNew1 set UpdateRemark='" + txtrem.Text.Trim().Replace("'", "") +
                                    "',followupdate='" + followupdate.Trim() +
                                    "',FollowUpEnteredTime='" + followupenteredtime + "',CallStatus='" + ddsubcall.SelectedValue.Trim() +
                                    "',subcalltype='" + ddsubcall.SelectedValue.Trim() + "',ResolvedDate='" + followupdate.Trim() + "',ResolvedBy='" +
                                    Session["sess_userid"] + "',callpriority='2' where ddocketnumber='" + docketnum.Trim() + "'";
                                    obj.NonExecuteQuery(updatesubcalltype);

                                    q = "IF (NOT EXISTS(SELECT userid FROM TicketUpdates WHERE docketnumber='" + docketnum.Trim() + "'))" +
                                    "BEGIN INSERT INTO TicketUpdates(docketnumber,userid,UpdatedBy,UpdatedOn,Status) Select '" +
                                    docketnum.Trim() + "',userid,'" + Session["sess_userid"] + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','MOD' from usermap " +
                                    " where atmid='" + lblatmid.Text.Trim() + "' END ELSE BEGIN " +
                                    "UPDATE TicketUpdates SET status = 'MOD',UpdatedBy='" + Session["sess_userid"] + "',UpdatedOn='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                    "' where docketnumber='" + docketnum.Trim() + "' END";
                                    obj.NonExecuteQuery(q);

                                    btn_upload_Click(sender, e);
                                }
                                // }
                                else
                                {
                                    Response.Write("<script>alert('Please Select Photo in Proper Format')</script>");
                                }

                            }
                            else if (ddsubcall.SelectedValue == "CLOSE")
                            {
                                mystring = @"Insert Into Remarks (DocketNumber,RemarkDate,UpdatedBy,Comments,SubCallType,FollowUpDate,Priority) VALUES ('" +
                                docketnum.Trim() + "','" + followupdate.Trim() + "','" + Session["sess_userid"] + "','" + txtrem.Text.Trim().Replace("'", "") +
                                "','" + ddsubcall.SelectedValue.Trim() + "','" + followupenteredtime + "'," + prioritynum + ")";

                                if (obj.NonExecuteQuery(mystring) != 0)
                                {
                                    updatesubcalltype = @"Update IncidentsNew1 set UpdateRemark='" + txtrem.Text.Trim().Replace("'", "") +
                                    "',followupdate='" + followupdate.Trim() +
                                    "',FollowUpEnteredTime='" + followupenteredtime + "',CallStatus='" + ddsubcall.SelectedValue.Trim() +
                                    "',subcalltype='" + ddsubcall.SelectedValue.Trim() + "',CloseDate='" + followupdate.Trim() + "',CallClosedBy='" +
                                    Session["sess_userid"] + "',callpriority='1' where ddocketnumber='" + docketnum.Trim() + "'";
                                    obj.NonExecuteQuery(updatesubcalltype);

                                    q = "IF (NOT EXISTS(SELECT userid FROM TicketUpdates WHERE docketnumber='" + docketnum.Trim() + "'))" +
                                    "BEGIN INSERT INTO TicketUpdates(docketnumber,userid,UpdatedBy,UpdatedOn,Status) Select '" +
                                    docketnum.Trim() + "',userid,'" + Session["sess_userid"] + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','MOD' from usermap " +
                                    " where atmid='" + lblatmid.Text.Trim() + "' END ELSE BEGIN " +
                                    "UPDATE TicketUpdates SET status = 'MOD',UpdatedBy='" + Session["sess_userid"] + "',UpdatedOn='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                    "' where docketnumber='" + docketnum.Trim() + "' END";
                                    obj.NonExecuteQuery(q);
                                    btn_upload_Click(sender, e);
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('No details updated')</script>");
                            }
                            //else if(ddsubcall.SelectedValue == "RE-OPEN")
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "document.getElementById('div_review').style.display='block';", true);
                            //}
                        }

                        Response.Redirect("CallEntry.aspx?doc=" + docketnum, false);
                    }
                    else
                    {
                        Response.Write("<script>alert('" + ddsubcall.SelectedValue + " date should be greater than " + lblcallstatus.Text + " date and not greater than current time.')</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }

            }
            else
            {
                Response.Write("<script>alert('Date entered not in correct format')</script>");
            }
        }

        protected void btn_Submit0_Click(object sender, EventArgs e)
        {

        }

        protected void btncontinue_Click(object sender, EventArgs e)
        {
            int prioritynum = priority();
            prioritynum = prioritynum + 1;
            string mystring = "";
            string docketnum = Request.QueryString["doc"];

            if (txtrem.Text.Trim() != "" && txtrem.Text.Trim() != null)
            {
                try
                {
                    string followupenteredtime = System.DateTime.Now.ToString("yyyy'-'MM'-'dd HH:mm:ss");
                    mystring = "Insert Into Remarks (DocketNumber,RemarkDate,UpdatedBy,Comments,SubCallType,Priority) VALUES (" + "'" + docketnum.Trim() + "'," + "'"
                    + followupenteredtime + "'," + "'" + Session["sess_userid"] + "'," + "'" + txtrem.Text.Trim().Replace("'", "") + "','" +
                    ddsubcall.SelectedValue.Trim() + "'," + prioritynum + ")";
                    obj.NonExecuteQuery(mystring);
                    string updatesubcalltype = "Update IncidentsNew1 set UpdateRemark='" + txtrem.Text.Trim().Replace("'", "") + "'," + " subcalltype='" + ddsubcall.SelectedValue.Trim() + "',FollowUpEnteredTime='" + followupenteredtime + "' where ddocketnumber='" + docketnum.Trim() + "'";
                    obj.NonExecuteQuery(updatesubcalltype);

                    string q = "IF (NOT EXISTS(SELECT userid FROM TicketUpdates WHERE docketnumber='" + docketnum.Trim() + "'))" +
                        "BEGIN INSERT INTO TicketUpdates(docketnumber,userid,UpdatedBy,UpdatedOn,Status) Select '" +
                        docketnum.Trim() + "',userid,'" + Session["sess_userid"] + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','MOD' from usermap " +
                        " where atmid='" + lblatmid.Text.Trim() + "' END ELSE BEGIN " +
                        "UPDATE TicketUpdates SET status = 'MOD',UpdatedBy='" + Session["sess_userid"] + "',UpdatedOn='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                        "' where docketnumber='" + docketnum.Trim() + "' END";

                    obj.NonExecuteQuery(q);

                    movedata();
                    txtrem.Text = "";
                    txtfollowup.Text = "";

                    if (FileUpload1.HasFile)
                    {
                        string path = "" + physicalpath + "";
                        string filename = Path.GetFileName(FileUpload1.FileName);
                        long fsize = FileUpload1.FileContent.Length;
                        FileUpload1.SaveAs(path + Request.QueryString["doc"] + "$" + filename);
                        string upload = "Insert into Activity_Log(vid,AttachedFile,Remarks,Size,AttachedDate,AttachedBy) values ('" + Request.QueryString["doc"] + "','" + filename.Trim() + "',NULL,'" + fsize.ToString() + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Session["sess_userid"] + "')";
                        obj.NonExecuteQuery(upload);
                        GridView1.DataBind();
                    }

                    Response.Redirect("CallEntry.aspx?doc=" + docketnum, false);
                }

                catch (Exception ee)
                {
                    string errors = ee.Message;
                    Response.Write("" + errors);
                }
            }
            else
            {

            }
        }

        protected void btncontinue1_Click(object sender, EventArgs e)
        {
        }

        void btn_reviewd_Click(object sender, EventArgs e)
        {
            int prioritynum = priority();

            //string query = "update incidentsNew1 set SubCallType='CLOSE',callstatus='CLOSE',r_date='" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "',r_by='" + Session["sess_userid"] + "',r_status='CLOSE',r_comments='" + txt_rcomment.Text + "' where ddocketnumber='" + Request.QueryString["doc"] + "' ";

            string query2 = "insert into reviews (docketno,rdate,rby,rcomments,rstatus) values ('" + Request.QueryString["doc"] + "','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','" + Session["sess_userid"] + "','" + txt_rcomment.Text + "','CLOSE')";

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            cn.Open();
            //SqlCommand cmd = new SqlCommand(query, cn);
            SqlCommand cmdz = new SqlCommand(query2, cn);

            //Response.Write(query + "<br/>"+query2);

            //cmd.ExecuteNonQuery();
            cmdz.ExecuteNonQuery();

            cn.Close();
            cn.Dispose();

            string followupenteredtime = System.DateTime.Now.ToString("yyyy'-'MM'-'dd HH:mm:ss");
            string mystring = "Insert Into Remarks (DocketNumber,RemarkDate,UpdatedBy,Comments,SubCallType,Priority,followupdate) VALUES (" + "'" + Request.QueryString["doc"] + "'," + "'"
            + System.DateTime.Now.ToString("yyyy'-'MM'-'dd HH:mm:ss") + "'," + "'" + Session["sess_userid"] + "'," + "'" + txt_rcomment.Text.Trim().Replace("'", "") + "','CLOSE'," + (prioritynum + 1) + ",'" + followupenteredtime + "')";
            obj.NonExecuteQuery(mystring);
            string updatesubcalltype = "Update IncidentsNew1 set UpdateRemark='" + txt_rcomment.Text.Trim().Replace("'", "") +
                                        "',subcalltype='CLOSE',callstatus='CLOSE',r_date='" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") +
                                        "',r_by='" + Session["sess_userid"] + "',r_status='CLOSE',r_comments='" + txt_rcomment.Text + "',FollowUpEnteredTime='" +
                                        followupenteredtime + "',followupdate='" + followupenteredtime +
                                        "',closedate='" + followupenteredtime + "',callclosedby='" + Session["sess_userid"] + "' where ddocketnumber='" + Request.QueryString["doc"] + "'";
            obj.NonExecuteQuery(updatesubcalltype);

            Response.Redirect("CallEntry.aspx?doc=" + Request.QueryString["doc"], false);
        }

        void btn_reopen_Click(object sender, EventArgs e)
        {
            int prioritynum = priority();
            string query = "update incidentsNew1 set SubCallType='RE-OPEN', callstatus='RE-OPEN',r_date='" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "',r_by='" + Session["sess_userid"] + "',r_status='RE_OPEN',r_comments='" + txt_rcomment.Text + "' where ddocketnumber='" + Request.QueryString["doc"] + "' ";

            string query2 = "insert into reviews (docketno,rdate,rby,rcomments,rstatus) values ('" + Request.QueryString["doc"] + "','" + System.DateTime.Now.ToString("MM'/'dd'/'yyyy") + "','" + Session["sess_userid"] + "','" + txt_rcomment.Text + "','RE_OPEN')";

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlCommand cmdz = new SqlCommand(query2, cn);

            cmd.ExecuteNonQuery();
            cmdz.ExecuteNonQuery();

            cn.Close();
            cn.Dispose();

            string followupenteredtime = System.DateTime.Now.ToString("yyyy'-'MM'-'dd HH:mm:ss");
            string mystring = "Insert Into Remarks (DocketNumber,RemarkDate,UpdatedBy,Comments,SubCallType,Priority) VALUES (" + "'" + Request.QueryString["doc"] + "'," + "'"
            + System.DateTime.Now.ToString("yyyy'-'MM'-'dd HH:mm:ss") + "','" + Session["sess_userid"] + "','" + txt_rcomment.Text.Trim().Replace("'", "") + "','RE-OPEN'," + (prioritynum + 1) + ",'" + followupenteredtime + "')";
            obj.NonExecuteQuery(mystring);
            string updatesubcalltype = "Update IncidentsNew1 set UpdateRemark='" + txt_rcomment.Text.Trim().Replace("'", "") + "'," + " subcalltype='RE-OPEN',FollowUpEnteredTime='" + followupenteredtime + "' where ddocketnumber='" + Request.QueryString["doc"] + "'";
            obj.NonExecuteQuery(updatesubcalltype);

            Response.Redirect("CallEntry.aspx?doc=" + Request.QueryString["doc"], false);
        }

        protected void btnjust_Click(object sender, EventArgs e)
        {
            // Response.Redirect("Justification.aspx?doc=" + Request.QueryString["doc"], false);
        }
    }
}
//select Remark,cast(
//    (cast(cast(getdate() as float) - cast(opendate as float) as int) * 24) /* hours over 24 */
//    + datepart(hh, getdate() - opendate) /* hours */
//    as varchar(10))
//+ ':' + right('0' + cast(datepart(mi, getdate() - opendate) as varchar(2)), 2) from IncidentsNew1 order by docketnumber desc/* minutes */