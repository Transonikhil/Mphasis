using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Mphasis_webapp
{
    public partial class Login : System.Web.UI.Page
    {
        CommonClass obj = new CommonClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string closecall = @"Insert into Remarks (DocketNumber,RemarkDate,
                                UpdatedBy,SubCallType,Priority,Comments) 
                                Select ddocketnumber,dateadd(dd,15,resolveddate),'admin','CLOSED',3,'Auto-Closure' from IncidentsNew1 where DATEDIFF
                                (dd,Resolveddate,GETDATE())>15 and CallStatus='RESOLVED';Update IncidentsNew1 set 
                                CallStatus='CLOSE',SubCallType='CLOSE',closedate=dateadd(dd,15,resolveddate),CallClosedBy='admin',UpdateRemark='Auto-Closure' 
                                where DATEDIFF(dd,Resolveddate,GETDATE())>15 and CallStatus='RESOLVED'; update Distance set DistanceTraveled=cast(convert(float,DistanceTraveled)/1000 as decimal(10,2)) where LEN(DistanceTraveled)>6";
                obj.NonExecuteQuery(closecall);
            }
        }

        protected void btn_go_Click(object sender, EventArgs e)
        {
            ibuckethead bucket = new ibuckethead();

            string username = bucket.cleanText(txt_login);
            string pwd = bucket.cleanText(txt_pwd);

            string q1 = ""; string[] a1;
            Session["sess_Date"] = "";

            q1 = "Select userid,password,role,username,status from users where userid='" + username + "'";
            a1 = bucket.verifyReader(q1, "userid", "password", "role", "username", "status");
            if (a1[4].Trim() == "DEL")
            {
                Response.Write("<script>alert('User deactivated')</script>");
            }
            else if (username != "" || pwd != "")
            {
                try
                {
                    if (a1[0].Trim() == username && a1[1].Trim() == pwd)
                    {
                        Session["sess_userid"] = a1[0];
                        Session["sess_role"] = a1[2];

                        if (Session["sess_role"].ToString() == "admin")
                        {
                            Session["sess_userid"] = a1[0];
                            Session["sess_role"] = a1[2];
                            Session["sess_username"] = a1[3];
                            Response.Redirect("FieldTracker.aspx?Offline=True");
                        }
                        else if (Session["sess_role"].ToString() == "RCM")
                        {
                            Session["sess_userid"] = a1[0];
                            Session["sess_role"] = a1[2];
                            Session["sess_username"] = a1[3];
                            Response.Redirect("RCM/FieldTracker.aspx?Offline=True");
                        }
                        else if (Session["sess_role"].ToString() == "RM")
                        {
                            Session["sess_userid"] = a1[0];
                            Session["sess_role"] = a1[2];
                            Session["sess_username"] = a1[3];
                            Response.Redirect("RM/FieldTracker.aspx?Offline=True");
                        }
                        else if (Session["sess_role"].ToString() == "CH")
                        {
                            Session["sess_userid"] = a1[0];
                            Session["sess_role"] = a1[2];
                            Session["sess_username"] = a1[3];
                            Response.Redirect("CH/FieldTracker.aspx?Offline=True");
                        }
                        else if (Session["sess_role"].ToString() == "BANK")
                        {
                            Session["sess_userid"] = a1[0];
                            Session["sess_role"] = a1[2];
                            Session["sess_username"] = a1[3];
                            Response.Redirect("bank/currentaudit1.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('Login Restricted.')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid userid or password.')</script>");
                    }
                }
                catch
                {
                    Response.Write("<script>alert('Invalid userid or password.')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid userid or password.')</script>");
            }
        }

    }
}