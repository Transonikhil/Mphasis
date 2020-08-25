using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class CurrentAudit : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = "delete from DR_ctp where isdate(vdate)<>1";
            bucket.ExecuteQuery(query);
        }
        
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink report = new HyperLink();
                report.Text = "Excel";
                report.NavigateUrl = "Report.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&dnld_excel=Y";

                e.Row.Cells[0].Controls.Add(report);

                HyperLink download = new HyperLink();
                download.Text = "Photo";
                download.NavigateUrl = "photos.aspx?auditid=" + e.Row.Cells[2].Text.Trim() + "&dnld_pdf=Y";

                e.Row.Cells[1].Controls.Add(download);

                HyperLink vid = new HyperLink();
                vid.Text = e.Row.Cells[2].Text;
                vid.NavigateUrl = "MainPage.aspx?auditid=" + vid.Text;

                e.Row.Cells[2].Controls.Add(vid);
            }
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            //System.Threading.Thread.Sleep(10000);
            //restart:;
            //bucket.ExecuteQuery("Delete from DR_Branch where atmid='null' and vdate='null'");
            string bind = "";

            try
            {
                if (Request.QueryString["userid"].ToString() != "")
                {
                    try
                    {
                        if (Request.QueryString["dist"].ToString() == "yes")
                        {
                            bind = @"select VID,ATMID,substring(vdate,4,3) + substring(vdate,1,2)+ substring(vdate,6,5) as [AUDIT DATE], 
                            vtime as [AUDIT TIME] from DR_CTP where isdate(vdate)=1 and vdate like 
                            '[0-1][0-9]/[0-3][0-9]/[1-2][0-9][0-9][0-9]' and userid like '" + Request.QueryString["userid"].ToString() +
                                    "' and vdate='" + Request.QueryString["vdate"].ToString() + "' and  distance like '%.%' " +
                                    " and Convert(int,LEFT(distance, CHARINDEX('.', distance) - 1)) > 1000 order by srno desc";
                            // "select * from currentview order by [AUDIT DATE] desc, [AUDIT TIME] desc";
                        }
                        else if (Request.QueryString["dist"].ToString() == "no")
                        {
                            bind = @"select VID,ATMID,substring(vdate,4,3) + substring(vdate,1,2)+ substring(vdate,6,5) as [AUDIT DATE], 
                            vtime as [AUDIT TIME] from DR_CTP where isdate(vdate)=1 and vdate like 
                            '[0-1][0-9]/[0-3][0-9]/[1-2][0-9][0-9][0-9]' and userid like '" + Request.QueryString["userid"].ToString() +
                                    "' and vdate='" + Request.QueryString["vdate"].ToString() +
                                    "' and DATEDIFF(MI,chkdate,CONVERT(datetime,(vdate + ' ' + vtime))) > 30 order by srno desc";
                            // "select * from currentview order by [AUDIT DATE] desc, [AUDIT TIME] desc";
                        }
                    }
                    catch (Exception ee)
                    {
                        bind = @"select VID,ATMID,substring(vdate,4,3) + substring(vdate,1,2)+ substring(vdate,6,5) as [AUDIT DATE], 
                            vtime as [AUDIT TIME] from DR_CTP where isdate(vdate)=1 and vdate like 
                            '[0-1][0-9]/[0-3][0-9]/[1-2][0-9][0-9][0-9]' and userid like '%" + Session["sess_userid"] + "%' and vdate='" + Request.QueryString["vdate"].ToString() + "' and lat='0' and lon='0' order by srno desc";
                    }
                }
            }
            catch (Exception ex)
            {
                if (Session["role"].ToString() == "AO")
                {
                    bind = "select * from currentview where vid like '%" + Session["sess_userid"] + "%'  order by [AUDIT DATE] desc, [AUDIT TIME] desc";
                }
                else
                {
                    bind = "select VID,ATMID,substring(vdate,4,3) + substring(vdate,1,2)+ substring(vdate,6,5) as [AUDIT DATE], vtime as [AUDIT TIME] from DR_CTP where isdate(vdate)=1 and vdate like '[0-1][0-9]/[0-3][0-9]/[1-2][0-9][0-9][0-9]' and vdate=convert(date,GETDATE(),103)  order by srno desc";// "select * from currentview order by [AUDIT DATE] desc, [AUDIT TIME] desc";
                }
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
                timer.Enabled = false;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            timer_Tick(sender, e);
        }
    }
}