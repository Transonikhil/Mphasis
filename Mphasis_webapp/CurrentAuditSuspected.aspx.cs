using System;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class CurrentAuditSuspected : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = "delete from DR_ctp where isdate(vdate)<>1";
            bucket.ExecuteQuery(query);

            txtfromdate_CalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            txttodate_CalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            txtfromdate_CalendarExtender.StartDate = Convert.ToDateTime("11/09/2014");
            txttodate_CalendarExtender.StartDate = Convert.ToDateTime("11/09/2014");

            txtfromdate.Attributes.Add("Readonly", "true");
            txttodate.Attributes.Add("Readonly", "true");

            if (!IsPostBack)
            {
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
            }
            timer_Tick(sender, e);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink vid = new HyperLink();
                vid.Text = "VIEW";
                vid.NavigateUrl = "CurrentAudit.aspx?userid=" + e.Row.Cells[0].Text.Trim() + "&&vdate=" + e.Row.Cells[1].Text.Trim();
                e.Row.Cells[3].Controls.Add(vid);
            }
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(10000);
            //restart:;
            //bucket.ExecuteQuery("Delete from DR_Branch where atmid='null' and vdate='null'");
            string bind = "";
            if (Session["sess_role"].ToString().Contains("AO"))
            {
                //bind = "select * from currentview where vid like '%" + Session["sess_userid"] + "%'  order by [AUDIT DATE] desc, [AUDIT TIME] desc";
            }
            else
            {
                bind = @"Select userid as [USERID],COUNT(userid) as [COUNT],vdate as [VISIT DATE] from DR_CTP where lat='0' and lon='0' and CONVERT(date,vdate) between '" + txtfromdate.Text +
                        "' and '" + txttodate.Text + "' group by userid,vdate";
            }
            bucket.BindGrid(GridView1, bind);
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                Label3.Visible = true;
                Label3.Text = bucket.CountRows(GridView1, Label3);
                timer.Enabled = false;
            }
            else
            {
                Label3.Visible = true;
                Label3.Text = "No Records Found";
                Label3.ForeColor = System.Drawing.Color.Red;
                timer.Enabled = false;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            timer_Tick(sender, e);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            timer_Tick(sender, e);
        }
    }
}