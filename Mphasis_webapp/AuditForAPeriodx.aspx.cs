using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class AuditForAPeriodx : System.Web.UI.Page
    {
        string month = System.DateTime.Now.Month.ToString("d2");
        string year = System.DateTime.Now.Year.ToString();

        ibuckethead bucket = new ibuckethead();

        DateTime toDate = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            CalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            CalendarExtender2.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            //CalendarExtender1.EndDate = toDate;
            //CalendarExtender2.EndDate = toDate;

            if (Page.IsPostBack)
            {
                if (Request.QueryString["export"] == "true")
                {
                    btn_Update_Click(sender, e);
                    GridView1.AllowPaging = false;
                    GridView1.DataBind();
                    Response.AddHeader("content-disposition", "attachment;filename=AuditReport.xls");
                    Response.Charset = String.Empty;
                    Response.ContentType = "application/vnd.xls";
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView1.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();
                    Response.Redirect("auditforaperiod.aspx");
                }
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");


            string sql =
                    @"Select c.vid,c.ATMID as [ATM], a.Location, a. Bankid as [Bank], a.Client as [Client], 
                    replace(substring(vid,len(userid)+2,len(vid)),'_',' ') as [Audit Date TIme] from DR_CTP c, atms a where c.atmid=a.atmid AND a.client='" + dd_cust.SelectedItem.Text.ToString() + @"' 
                    and a.bankid='" + dd_bank.SelectedItem.Text.ToString() + "' and convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + "'";

            /*------------------------------------------------------------------------------------------------*/
            /* Bind query to grid view
            /*------------------------------------------------------------------------------------------------*/
            bucket.BindGrid(GridView1, sql);
            /*------------------------------------------------------------------------------------------------*/


            /*------------------------------------------------------------------------------------------------*/
            /* If no rows returned display null error or fetch count
            /*------------------------------------------------------------------------------------------------*/
            if (GridView1.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
                Label3.Visible = false;
            }
            else
            {
                Label3.Visible = true;
                Label1.Visible = false;
                Label3.Text = bucket.CountRows(GridView1, Label3); // GridView1.Rows.Count.ToString() + " records matching your criteria.";
            }
            /*------------------------------------------------------------------------------------------------*/
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_Update_Click(sender, e);
        }
    }
}