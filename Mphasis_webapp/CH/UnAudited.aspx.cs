using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.CH
{
    public partial class UnAudited : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        DateTime toDate = DateTime.Now;
        int cnt = 0;
        string state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //defaultCalendarExtender.StartDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-(Convert.ToDouble((DateTime.Now.Date.Day)))).ToString("MM/dd/yyyy"));
            //defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            //defaultCalendarExtender1.StartDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-(Convert.ToDouble((DateTime.Now.Date.Day)))).ToString("MM/dd/yyyy"));
            //defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            txt_frmDate.Text = DateTime.Now.Date.AddDays((-(Convert.ToDouble((DateTime.Now.Date.Day)))) + 1).ToString("MM'/'dd'/'yyyy");
            txt_toDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");

            if (!IsPostBack)
            {
                string state = "SELECT  distinct state,state as STATE  FROM [atms] where CH = '" + Session["sess_username"].ToString() + "'";
                obj.BindListboxWithValue(ddstate, state);
            }


        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            if (string.IsNullOrEmpty(txt_frmDate.Text.Trim()))
            {
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }
            string q = "";
            if (txtuser.Text == "")
            {
                q = @"select distinct a.atmid as [Atmid] ,a.location as Location,a.bankid as [Bank],region as [Region],u.username,a.siteid,a.onoffsite as [Site Type],a.state 
                     from atms a inner join Users u on u.ch=a.ch 
                     where  atmstatus <> 'Inactive' and u.CH like '" + Session["sess_username"] +
                       "'and a.atmid not in (select distinct atmid from dr_ctp where convert(date,vdate) between '" + txt_frmDate.Text +
                       "' and '" + txt_toDate.Text + "') ";
            }
            else
            {
                q = @"select distinct a.atmid as [Atmid] ,a.location as Location,a.bankid as [Bank],region as [Region],u.username,a.siteid,a.onoffsite as [Site Type],a.state 
                from atms a inner join Users u on u.ch=a.ch 
                where  atmstatus <> 'Inactive' and u.CH like '" + Session["sess_username"] +
                       "'and a.atmid not in (select distinct atmid from dr_ctp where convert(date,vdate) between '" + txt_frmDate.Text +
                       "' and '" + txt_toDate.Text + "') and a.state in (" + txtuser.Text + ")";
            }

            //        q = @"select distinct a.atmid as [Atmid] ,a.location as Location,a.bankid as [Bank],region as [Region],u.username,a.siteid,a.onoffsite as [Site Type],a.state 
            //                     from atms a,Users u,Usermap um 
            //                     where a.atmid=um.atmid and um.userid=u.userid and atmstatus <> 'DEL' and u.CH like '" + Session["sess_username"] +
            //                  "'and a.atmid not in (select distinct atmid from dr_ctp where convert(date,vdate) between '" + txt_frmDate.Text +
            //                  "' and '" + txt_toDate.Text + "') and a.state in (" + txtuser.Text + ")";

            bucket.BindGrid(grid_unaudit, q);

            //Response.Write(q);

            if (grid_unaudit.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
                Label3.Visible = false;
            }
            else
            {
                Label3.Visible = true;
                Label1.Visible = false;
                Label3.Text = bucket.CountRows(grid_unaudit, Label3);
            }



        }

        protected void btn_search_Click1(object sender, EventArgs e)
        {
            btn_search_Click(sender, e);
            grid_unaudit.AllowPaging = false;

            grid_unaudit.DataBind();
            grid_unaudit.GridLines = GridLines.Both;
            Response.AddHeader("content-disposition", "attachment;filename=Unaudited.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            grid_unaudit.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
            Response.Redirect("Unaudited.aspx");
        }

        protected void grid_unaudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_unaudit.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}