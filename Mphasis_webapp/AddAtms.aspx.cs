using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class AddAtms : System.Web.UI.Page
    {
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
                rdb_active.Checked = true;
                glory(rdb_active);

                user_err.Visible = false;
                user_err0.Visible = false;
                user_err1.Visible = false;
                user_err2.Visible = false;
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
        protected void btn_Update_Click(object sender, EventArgs e)
        {
            ibuckethead bucket = new ibuckethead();

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
                string q = @"insert into ATMs(atmid, location, bankid, sitenumber, client, status, addressline1, addressline2, city, pin, state) values
                        ('" + atm + "','" + location + "','" + bank + "','" + siteNumber + "','" + customer + "','" + status + "','" + addressLine1 + "','" + addressLine2 + "','" + city + "','" + pinCode + "','" + state + "')";

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
    }
}