using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class SchedulerDashboard : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        int cnt = 0;
        string role = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM'/'dd'/'yyyy"));
                defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM'/'dd'/'yyyy"));
            }
            catch (Exception ex) { }
            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            #region Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
                DropDownList1.Items.FindByText("ALL").Value = "%";
                DropDownList1.Items.FindByValue("%").Selected = true;

                string state = "SELECT  distinct state,state as STATE  FROM [atms]";
                obj.BindListboxWithValue(ddstate, state);

                txt_frmDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");

                btn_search_Click(sender, e);
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }


        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state, string role)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all SELECT  distinct userid as userid,userid FROM [users] where role like '" + role + "'";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and state in(" + state + ")";
            }
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strQuery;

                        cmd.Connection = con;
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            list.Add(new ListItem(sdr[0].ToString()));
                        }
                        con.Close();
                        return list;
                    }
                    catch (Exception ex) { }
                    return list;
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (ddrole.SelectedValue == "%")
            {
                role = "u.role in ('AO','DE','CM','CH')";
            }
            else
            {
                role = "u.role like '" + ddrole.SelectedValue + "'";
            }

            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;

            if (users == "All" || users == null)
            {
                users = "%";
            }

            if (txtuser.Text == "")
            {
                SqlDataSource2.SelectCommand = @"Select count(*) as 'count','Total Scheduled' as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                     and s.userid like '" + users + @"'
                    union all
                    Select count(*) as 'count','Not Visited' as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                     and s.userid like '" + users + @"' and s.visitstatus='Not Visited'
                    union all
                    Select count(*) as 'count','Visited' as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                     and s.userid like '" + users + @"' and s.visitstatus='Visited'
                    union all
                    Select count(*) as 'count','Additional Visit' as [Criteria] from dr_ctp s inner join atms a on s.siteid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[vdate]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                     and s.userid like '" + users + @"' and s.siteid not in 
                    (Select s.AtmID as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                     and s.userid like '" + users + @"')
                    union all
                    Select sum([count]),'Grand Total' as [Criteria] from
                    (Select count(*) as 'count' from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                     and s.userid like '" + users + @"' union all 
                    Select count(*) as 'count' from dr_ctp s inner join atms a on s.siteid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[vdate]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                     and s.userid like '" + users + @"' and s.siteid not in 
                    (Select s.AtmID as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                     and s.userid like '" + users + @"')) L";
            }
            else
            {
                SqlDataSource2.SelectCommand = @"Select count(*) as 'count','Total Scheduled' as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                    and a.state in (" + txtuser.Text + @") and s.userid like '" + users + @"'
                    union all
                    Select count(*) as 'count','Not Visited' as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                    and a.state in (" + txtuser.Text + @") and s.userid like '" + users + @"' and s.visitstatus='Not Visited'
                    union all
                    Select count(*) as 'count','Visited' as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                    and a.state in (" + txtuser.Text + @") and s.userid like '" + users + @"' and s.visitstatus='Visited'
                    union all
                    Select count(*) as 'count','Additional Visit' as [Criteria] from dr_ctp s inner join atms a on s.siteid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[vdate]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                    and a.state in (" + txtuser.Text + @") and s.userid like '" + users + @"' and s.siteid not in 
                    (Select s.AtmID as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                    and a.state in (" + txtuser.Text + @") and s.userid like '" + users + @"')
                    union all
                    Select sum([count]),'Grand Total' as [Criteria] from
                    (Select count(*) as 'count' from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                    and a.state in (" + txtuser.Text + @") and s.userid like '" + users + @"' union all 
                    Select count(*) as 'count' from dr_ctp s inner join atms a on s.siteid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[vdate]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                    and a.state in (" + txtuser.Text + @") and s.userid like '" + users + @"' and s.siteid not in 
                    (Select s.AtmID as [Criteria] from Scheduler s inner join atms a on s.atmid=a.siteid inner join Users u on u.userid=s.userid 
                    where convert(date,[date]) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"' and " + role + @"
                    and a.state in (" + txtuser.Text + @") and s.userid like '" + users + @"')) L";
            }

            //SqlDataSource2.DataBind();
            //Chart3.DataSourceID = "SqlDataSource2";
            //Chart3.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            btn_search_Click(sender, e);
            //GridView1.AllowPaging = false;
            //GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment;filename=MasterReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            dwn.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Response.Write("9");
                //for (int i = 0; i < 45; i++)
                //{
                //    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|N", "No-");
                //}

                for (int i = 11; i < 52; i++)
                {

                    if (e.Row.Cells[i].Text.Contains("E|N"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|N", "No-");
                    }
                    else if (e.Row.Cells[i].Text.Contains("E|Y"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("E|Y", "Yes-");
                    }
                    else if (e.Row.Cells[i].Text.ToUpper().Contains("Y-"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("Y-", "Yes-");
                    }
                    else if (e.Row.Cells[i].Text.ToUpper().Contains("N-"))
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("N-", "No-");
                    }
                    else if (e.Row.Cells[i].Text.ToUpper() == "Y")
                    {
                        e.Row.Cells[i].Text = "Yes";
                    }
                    else if (e.Row.Cells[i].Text.ToUpper() == "N")
                    {
                        e.Row.Cells[i].Text = "No";
                    }
                }
            }
        }

        protected void ddrole_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList1.DataBind();

            DropDownList1.Items.Add("ALL");
            DropDownList1.Items.FindByText("ALL").Value = "%";
            DropDownList1.Items.FindByValue("%").Selected = true;
        }

    }
}