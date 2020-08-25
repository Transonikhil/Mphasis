using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class UnAuditedRO : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        DateTime toDate = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            defaultCalendarExtender.StartDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-(Convert.ToDouble((DateTime.Now.Date.Day)))).ToString("MM/dd/yyyy"));
            defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            defaultCalendarExtender1.StartDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-(Convert.ToDouble((DateTime.Now.Date.Day)))).ToString("MM/dd/yyyy"));
            defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            txt_frmDate.Text = DateTime.Now.Date.AddDays((-(Convert.ToDouble((DateTime.Now.Date.Day)))) + 1).ToString("MM/dd/yyyy");
            txt_toDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            string q = "";

            if (Request.QueryString["type"] == "ctp")
            {
                q = "select distinct Replace(atmid,'*BR*','') as [Atmid] ,location,bankid as [Bank] from atms where status <> 'Inactive' and bankid like '%ICICI%' and region='" + Session["sess_userid"].ToString().Trim() + "' and atmid not like '%*BR*%' and atmid not in (select distinct atmid from current_dr_ctp where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "') ";

            }
            else if (Request.QueryString["type"] == "hdfc")
            {
                q = "select distinct Replace(atmid,'HDFC','') as [Atmid] ,location,bankid as [Bank] from atms where status <> 'Inactive' and bankid like 'HDFC' and region='" + Session["sess_userid"].ToString().Trim() + "' and atmid not in (select distinct atmid from current_dr_ctp where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "') ";
            }
            else if (Request.QueryString["type"] == "bom")
            {
                q = "select distinct Replace(atmid,'*BR*','') as [Atmid] ,location,bankid as [Bank] from atms where status <> 'Inactive' and atmid like '%*BR*%' and atmid not like '%HDFC%' and region='" + Session["sess_userid"].ToString().Trim() + "' and bankid like 'BANK OF MAHARASHTRA' and atmid not in (select distinct atmid from dr_branchcurmonth where convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + "') ";
            }

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