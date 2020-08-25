using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.bank
{
    public partial class AreaOfficerWiseSummary : System.Web.UI.Page
    {
        string month = System.DateTime.Now.Month.ToString("d2");
        string year = System.DateTime.Now.Year.ToString();
        string role = "";
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
                string sql = "";
                if (txtuser.Text != "")
                {
                    sql = @"select u.userid as [area officer],username as [User Name],COUNT(d.vid) as [visit] ,convert(varchar(10),convert(date,vdate),103) as [Audit Date]
                            from DR_CTP d, users u where  d.USERID=u.userid and Convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + @"' 
                            and state in (" + txtuser.Text + @") and CH = '" + Session["sess_username"].ToString() + @"'
                            group by u.userid ,vdate,username
                            order by CONVERT(date, vdate) desc";
                }
                else
                {
                    sql = @"select u.userid as [area officer],username as [User Name],COUNT(d.vid) as [visit] ,convert(varchar(10),convert(date,vdate),103) as [Audit Date]
                            from DR_CTP d, users u where d.USERID=u.userid and Convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + @"' 
                            and state is not null and CH = '" + Session["sess_username"].ToString() + @"'
                            group by u.userid ,vdate,username
                            order by CONVERT(date, vdate) desc";
                }
                //Response.Write(sql);

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
            Response.AddHeader("content-disposition", "attachment;filename=Areaofficersummary.xls");
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

            }
        }
    }
}