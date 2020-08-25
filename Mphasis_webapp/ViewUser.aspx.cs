using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class ViewUser : System.Web.UI.Page
    {
        public void checkErr(Panel p, TextBox t)
        {
            if (t.Text == "NA")
            {
                t.Text = "";
                p.Visible = true;
                t.Focus();
            }
            else
            {
                p.Visible = false;
            }
        }

        public void glory(RadioButton r)
        {
            if (r.Checked == true)
            {
                r.Font.Bold = true;
                r.ForeColor = System.Drawing.Color.DarkSlateBlue;
            }
            else
            {
                r.Font.Bold = false;
                r.ForeColor = System.Drawing.Color.Gray;
            }
        }

        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Code to Fetch Everything on Looad
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["userid"] != "")
                {
                    string querystring = Request.QueryString["userid"];
                    string q = "select * from users where userid='" + querystring + "'";
                    string[] a = bucket.verifyReader(q, "userid", "password", "role", "ao", "oc", "fc", "om", "status");

                    txt_user.Text = a[0]; txt_password.Text = a[1]; txt_Role.Text = a[2]; txt_AO.Text = a[3]; txt_oc.Text = a[4]; txt_fc.Text = a[5]; txt_OM.Text = a[6];

                    if (a[7] == "Active")
                    {
                        rdb_active.Checked = true;
                        glory(rdb_active);
                        glory(rdb_inactive);
                    }
                    else if (a[7] == "Inactive")
                    {
                        rdb_active.Checked = true;
                        glory(rdb_active);
                        glory(rdb_inactive);
                    }
                }
            }
            #endregion
        }

        protected void rdb_active_CheckedChanged(object sender, EventArgs e)
        {
            glory(rdb_active);
            glory(rdb_inactive);
        }
        protected void rdb_inactive_CheckedChanged(object sender, EventArgs e)
        {
            glory(rdb_active);
            glory(rdb_inactive);
        }
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            string userid = bucket.cleanText(txt_user);
            string password = bucket.cleanText(txt_password);
            string role = bucket.cleanText(txt_Role);
            string ao = bucket.cleanText(txt_AO);
            string oc = bucket.cleanText(txt_oc);
            string fc = bucket.cleanText(txt_fc);
            string om = bucket.cleanText(txt_OM);
            string status = null;

            if (rdb_active.Checked)
            {
                status = "Active";
            }
            else if (rdb_inactive.Checked)
            {
                status = "Inactive";
            }

            if (userid == "NA")
            {
                checkErr(user_err, txt_user);
            }
            else if (password == "NA")
            {
                checkErr(Panel1, txt_password);
            }
            else if (role == "NA")
            {
                checkErr(Panel2, txt_Role);
            }
            else if (ao == "NA")
            {
                checkErr(Panel3, txt_AO);
            }
            else if (oc == "NA")
            {
                checkErr(Panel4, txt_oc);
            }
            else if (fc == "NA")
            {
                checkErr(Panel5, txt_fc);
            }
            else if (om == "NA")
            {
                checkErr(Panel6, txt_OM);
            }
            else
            {
                Response.Write("Reached.");

                string q1 = "update users set userid='" + userid + "', password='" + password + "', role='" + role + "', ao='" + ao + "', oc='" + oc + "', fc='" + fc + "', om='" + om + "', status='" + status + "' where userid='" + Request.QueryString["userid"] + "'";

                if (bucket.ExecuteQuery(q1) == "Success")
                {
                    Response.Redirect("Users.aspx");
                }
                else
                {
                    Response.Write("Error");
                }
            }
        }
    }
}