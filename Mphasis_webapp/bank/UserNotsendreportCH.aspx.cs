using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.bank
{
    public partial class UserNotsendreportCH : System.Web.UI.Page
    {
        string month = System.DateTime.Now.Month.ToString("d2");
        string year = System.DateTime.Now.Year.ToString();

        int cnt = 0;
        string state = "";
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        DateTime toDate = DateTime.Now;

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            }
            catch (Exception ex) { }
            // defaultCalendarExtender.EndDate = toDate;
            //defaultCalendarExtender1.EndDate = toDate;

            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list and keep focus on current month and year on first page load,
            /*-----------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                string state = "SELECT  distinct state,state as STATE  FROM [atms] where CH = '" + Session["sess_username"].ToString() + "'";
                obj.BindListboxWithValue(ddstate, state);
            }
            /*------------------------------------------------------------------------------------------------*/
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            try
            {
                string q = "";
                if (txtuser.Text == "")
                {
                    q = @"select x.userid as[USERID],username as [USERNAME],role as [ROLE],rcm as [RCM],state as [STATE],convert(varchar(10),convert(date,vdate),103) as [LAST VISIT DATE],vtime as [LAST VISIT TIME] from
                        (SELECT userid,atmid,vdate,vtime
                        FROM  (SELECT DR_CTP.*, ROW_NUMBER() OVER (PARTITION BY userid
                        ORDER BY CONVERT(date, vdate) desc, vtime DESC) AS RN
                        FROM  DR_CTP
                        ) AS t
                        WHERE     RN = 1)	x
                        inner join 
                        (Select userid,role,username,rcm,state from users where CH like '" + Session["sess_username"].ToString() + @"' and
                         userid not in 
                        (Select userid from dr_ctp where Convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"') and role <> 'admin'
                        ) y 
                        on x.userid=y.userid";
                }
                else
                {
                    q = @"select x.userid as[USERID],username as [USERNAME],role as [ROLE],rcm as [RCM],state as [STATE],convert(varchar(10),convert(date,vdate),103) as [LAST VISIT DATE],vtime as [LAST VISIT TIME] from
                        (SELECT userid,atmid,vdate,vtime
                        FROM  (SELECT DR_CTP.*, ROW_NUMBER() OVER (PARTITION BY userid
                        ORDER BY CONVERT(date, vdate) desc, vtime DESC) AS RN
                        FROM  DR_CTP
                        ) AS t
                        WHERE     RN = 1)	x
                        inner join 
                        (Select userid,role,username,rcm,state from users where state in (" + txtuser.Text + @") and CH like '" + Session["sess_username"].ToString() + @"' and
                         userid not in 
                        (Select userid from dr_ctp where Convert(date,vdate) between '" + txt_frmDate.Text + "' and '" + txt_toDate.Text + @"') and role <> 'admin'
                        ) y 
                        on x.userid=y.userid";
                }
                //Response.Write(sql);
                /*------------------------------------------------------------------------------------------------*/
                /* Bind query to grid view
                /*------------------------------------------------------------------------------------------------*/
                bucket.BindGrid(GridView1, q);
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
                    Label3.Text = bucket.CountRows(GridView1, Label3);// GridView1.Rows.Count.ToString() + " records matching your criteria.";
                }
                /*------------------------------------------------------------------------------------------------*/
            }
            catch
            {
                Response.Write("<script>alert('Fields can not be left blank')</script>");
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=OfficerWiseReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            btn_search_Click(sender, e);

            GridView1.AllowPaging = false;
            GridView1.AllowSorting = false;
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
            GridView1.Columns[2].Visible = false;
            GridView1.DataBind();

            GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HyperLink hp = new HyperLink();
                //hp.Text = "EXCEL";
                //hp.NavigateUrl = "Report.aspx?auditid=" + e.Row.Cells[3].Text + "&dnld_excel=Y";
                //e.Row.Cells[0].Controls.Add(hp);

                //HyperLink hp1 = new HyperLink();
                //hp1.Text = "PHOTOS";
                //hp1.NavigateUrl = "photos.aspx?auditid=" + e.Row.Cells[3].Text + "&dnld_pdf=Y";
                //e.Row.Cells[1].Controls.Add(hp1);

                //HyperLink hp2 = new HyperLink();
                //hp2.Text = "View Report";
                //hp2.NavigateUrl = "MainPage1.aspx?auditid=" + e.Row.Cells[3].Text + "";
                //e.Row.Cells[2].Controls.Add(hp2);
            }
        }
    }
}