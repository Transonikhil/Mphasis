using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RCM
{
    public partial class TodaysAudits : System.Web.UI.Page
    {

        string month = System.DateTime.Now.Month.ToString("d2");
        string year = System.DateTime.Now.Year.ToString();

        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        DateTime toDate = DateTime.Now;
        int cnt = 0;
        string state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string year = "";
            if (System.DateTime.Now.ToString("MM") == "01")
            {
                year = System.DateTime.Now.AddYears(-1).ToString("yyyy");
            }
            else
            {
                year = System.DateTime.Now.ToString("yyyy");
            }


            //CalendarExtender1.StartDate = Convert.ToDateTime(System.DateTime.Now.AddMonths(-1).ToString("MM") + "/" + "01/" + year);
            //CalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            //CalendarExtender2.StartDate = Convert.ToDateTime(System.DateTime.Now.AddMonths(-1).ToString("MM") + "/" + "01/" + year);
            //CalendarExtender2.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            if (Page.IsPostBack)
            {

            }
            else
            {
                txt_frmDate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = System.DateTime.Now.ToString("MM'/'dd'/'yyyy");

                string state = "SELECT  distinct state,state as STATE  FROM [atms] where rcm = '" + Session["sess_username"].ToString() + "'";
                obj.BindListboxWithValue(ddstate, state);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Visible = false;
            }
        }
        protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            //e.Command.Parameters["@atmid"].Value = dd_atm.SelectedValue.ToString();
            //e.Command.Parameters["@from"].Value = txt_frmDate.Text;
            //e.Command.Parameters["@to"].Value = txt_toDate.Text;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Button1_Click(sender, e);
            GridView1.AllowPaging = false;
            GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment;filename=MasterReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            if (string.IsNullOrEmpty(txt_frmDate.Text.Trim()))
            {
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }
            string q = "";
            #region Code to fetch data from DR_CTP
            if (txtuser.Text == "")
            {
                q = @"Select c.vid,a.siteid,a.Location, a. Bankid as [Bank], a.Client as [Client], convert(varchar(10),convert(date,vdate),103) +' '+vtime as [Audit Date TIme] 
                from DR_CTP c, ATMs a where c.atmid=a.atmid AND (c.ATMID like '" + dd_atm.Text + "' or a.siteid like '" + dd_atm.Text + "') and a.RCM like '" + Session["sess_username"] + "' and Convert(date,vdate) between '" + txt_frmDate.Text +
                    "' and '" + txt_toDate.Text + "'";
            }
            else
            {
                q = @"Select c.vid,a.siteid,a.Location, a. Bankid as [Bank], a.Client as [Client], convert(varchar(10),convert(date,vdate),103) +' '+vtime as [Audit Date TIme] 
                from DR_CTP c, ATMs a where c.atmid=a.atmid AND (c.ATMID like '" + dd_atm.Text + "' or a.siteid like '" + dd_atm.Text + "') and a.RCM like '" + Session["sess_username"] + "'  and a.state in (" + txtuser.Text + @")  and Convert(date,vdate) between '" + txt_frmDate.Text +
                    "' and '" + txt_toDate.Text + "'";
            }
            bucket.BindGrid(GridView1, q);
            #endregion
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
    }
}