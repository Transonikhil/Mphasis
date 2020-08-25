using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class ViewATM : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();

        public void checkErr(Panel p, TextBox t)
        {
            if (t.Text == "NA")
            {
                p.Visible = true;
                t.Focus();
                t.BorderColor = System.Drawing.Color.Red;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["atmid"] != "")
                {
                    string querystring = Request.QueryString["atmid"];

                    string q = "select * from atms where atmid='" + querystring + "'";
                    string[] a = bucket.verifyReader(q, "atmid", "location", "bankid", "sitenumber", "client", "status", "addressline1", "addressline2", "city", "pin", "state");

                    txt_atm.Text = a[0]; txt_location.Text = a[1]; txt_bank.Text = a[2]; txt_sitenumber.Text = a[3]; txt_customer.Text = a[4]; txt_addressline1.Text = a[6];
                    txt_addressline2.Text = a[7]; txt_City.Text = a[8]; txt_Pin.Text = a[9]; txt_state.Text = a[10];

                    user_err.Visible = false;
                    user_err0.Visible = false;
                    user_err1.Visible = false;
                    user_err2.Visible = false;

                    if (a[5] == "Active")
                    {
                        rdb_active.Checked = true;
                        glory(rdb_active);
                        glory(rdb_inactive);
                    }
                    else if (a[5] == "Inactive")
                    {
                        rdb_inactive.Checked = true;
                        glory(rdb_active);
                        glory(rdb_inactive);
                    }
                }
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            string atm = bucket.cleanText(txt_atm);
            string location = bucket.cleanText(txt_location);
            string addressLine1 = bucket.cleanText(txt_addressline1);
            string addressLine2 = bucket.cleanText(txt_addressline2);
            string city = bucket.cleanText(txt_City); ;
            string pinCode = bucket.cleanText(txt_Pin);
            string state = bucket.cleanText(txt_state);
            string bank = bucket.cleanText(txt_bank);
            string customer = bucket.cleanText(txt_customer);
            string siteNumber = bucket.cleanText(txt_sitenumber);

            string status = null;
            if (rdb_active.Checked)
            {
                status = "Active";
            }
            else if (rdb_inactive.Checked)
            {
                status = "Inactive";
            }

            if (atm == "NA")
            {
                checkErr(user_err, txt_atm);
            }
            else if (location == "NA")
            {
                checkErr(user_err0, txt_location);
            }
            else if (bank == "NA")
            {
                checkErr(user_err1, txt_bank);
            }
            else if (customer == "NA")
            {
                checkErr(user_err2, txt_customer);
            }
            else
            {
                string q = "Update ATMs set atmid='" + atm + "', location='" + location + "', bankid='" + bank + "', sitenumber='" + siteNumber + "', client='" + customer + "', status='" + status + "', addressline1='" + addressLine1 + "', addressline2='" + addressLine2 + "', city='" + city + "', pin='" + pinCode + "', state='" + state + "' where atmid='" + Request.QueryString["atmid"] + "'";

                if (bucket.ExecuteQuery(q) == "Success")
                {
                    Response.Redirect("Atms.aspx");
                }
                else
                {
                    Response.Write("Error");
                }
            }
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
    }
}