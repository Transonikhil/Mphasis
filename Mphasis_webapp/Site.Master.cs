using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!HttpContext.Current.Request.Url.AbsolutePath.ToString().Contains("Login.aspx"))
                {
                    if (Session["sess_userid"] == null || Session["sess_role"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else if (Session["sess_role"].ToString() == "admin")
                    {

                        AccordionPane1.Visible = true;
                        AccordionPane2.Visible = true;
                        AccordionPane3.Visible = true;
                        AccordionPane4.Visible = true;
                        pane_rcm.Visible = false;
                        pane_rm.Visible = false;
                        pane_ch.Visible = false;
                        pane_bank.Visible = false;
                    }
                    else if (Session["sess_role"].ToString() == "RCM")
                    {

                        AccordionPane1.Visible = false;
                        AccordionPane2.Visible = false;
                        AccordionPane3.Visible = false;
                        AccordionPane4.Visible = false;
                        pane_rcm.Visible = true;
                        pane_rm.Visible = false;
                        pane_ch.Visible = false;
                        pane_bank.Visible = false;
                    }
                    else if (Session["sess_role"].ToString() == "RM")
                    {
                        AccordionPane1.Visible = false;
                        AccordionPane2.Visible = false;
                        AccordionPane3.Visible = false;
                        AccordionPane4.Visible = false;
                        pane_rcm.Visible = false;
                        pane_rm.Visible = true;
                        pane_ch.Visible = false;
                        pane_bank.Visible = false;

                    }
                    else if (Session["sess_role"].ToString() == "CH")
                    {
                        AccordionPane1.Visible = false;
                        AccordionPane2.Visible = false;
                        AccordionPane3.Visible = false;
                        AccordionPane4.Visible = false;
                        pane_rcm.Visible = false;
                        pane_rm.Visible = false;
                        pane_ch.Visible = true;
                        pane_bank.Visible = false;

                    }
                    else if (Session["sess_role"].ToString() == "BANK")
                    {
                        AccordionPane1.Visible = false;
                        AccordionPane2.Visible = false;
                        AccordionPane3.Visible = false;
                        AccordionPane4.Visible = false;
                        pane_rcm.Visible = false;
                        pane_rm.Visible = false;
                        pane_ch.Visible = false;
                        pane_bank.Visible = true;
                        Stateorbankdashboard.Visible = false;
                        Button6.Visible = false;
                    }
                }
            }

            catch (Exception ee)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Session["sess_role"].ToString() == "admin")
            {
                Response.Redirect("Default.aspx");
            }
            else if (Session["sess_role"].ToString() == "RCM")
            {
                Response.Redirect("~/RCM/Default.aspx");
            }
            else if (Session["sess_role"].ToString() == "RM")
            {
                Response.Redirect("~/RM/Default.aspx");
            }
            else if (Session["sess_role"].ToString() == "CH")
            {
                Response.Redirect("~/CH/Default.aspx");
            }
            else if (Session["sess_role"].ToString() == "BANK")
            {
                Response.Redirect("~/BANK/Default.aspx");
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {

            if (Session["sess_role"].ToString() == "admin")
            {
                Response.Redirect("~/FieldTracker.aspx?Offline=True");
            }
            else if (Session["sess_role"].ToString() == "RM")
            {
                Response.Redirect("~/RM/FieldTracker.aspx?offline=True");
            }
            else if (Session["sess_role"].ToString() == "CH")
            {
                Response.Redirect("~/CH/FieldTracker.aspx?offline=True");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["sess_role"].ToString() == "admin")
            {
                Response.Redirect("DashBoardBankstate.aspx");
            }
            else if (Session["sess_role"].ToString() == "RCM")
            {
                Response.Redirect("~/RCM/DashBoardBankstateRCM.aspx");
            }
            else if (Session["sess_role"].ToString() == "RM")
            {
                Response.Redirect("~/RM/DashBoardBankstateRM.aspx");
            }
            else if (Session["sess_role"].ToString() == "CH")
            {
                Response.Redirect("~/CH/DashBoardBankstateCH.aspx");
            }

        }
    }
}