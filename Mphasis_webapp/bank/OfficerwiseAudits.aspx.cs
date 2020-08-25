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

namespace Mphasis_webapp.bank
{
    public partial class OfficerwiseAudits : System.Web.UI.Page
    {
        string month = System.DateTime.Now.Month.ToString("d2");
        string year = System.DateTime.Now.Year.ToString();

        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        DateTime toDate = DateTime.Now;
        int cnt = 0;
        string state = "";
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list and keep focus on current month and year on first page load,
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
                DropDownList1.Items.FindByText("ALL").Value = "%";
                DropDownList1.Items.FindByValue("%").Selected = true;

                string state = "SELECT  distinct state,state as STATE  FROM [atms] where bankid='TAMILNAD MERCANTILE BANK LIMITED'";
                obj.BindListboxWithValue(ddstate, state);
            }
            /*------------------------------------------------------------------------------------------------*/
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all select distinct u.userid,u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role in ('DE','CM','AO') " +
                            " and a.bankid='TAMILNAD MERCANTILE BANK LIMITED'";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and u.state in(" + state + ")";
            }
            //strQuery += "  order by userid";

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
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                // txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }


            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");

            string sql = "";
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;
            if (users == "All")
            {
                users = "%";
            }
            try
            {
                if (txtuser.Text == "")
                {
                    sql = @"Select '' as [Report],'' as [Download],'' as [Visit ID],c.vid,c.ATMID as [ATM], a.Location, a. Bankid as [Bank], a.Client as [Client],  
                convert(varchar(12),Convert(date,vdate),103) as [Audit Date],vtime as [Audit Time] from DR_CTP c, ATMs a where 
                c.atmid=a.atmid and Convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text +
                     "'and a.bankid='TAMILNAD MERCANTILE BANK LIMITED' and userid like '" + users + "' order by Convert(date,vdate) desc,vtime desc";
                }
                else
                {
                    sql = @"Select '' as [Report],'' as [Download],'' as [Visit ID],c.vid,c.ATMID as [ATM], a.Location, a. Bankid as [Bank], a.Client as [Client],  
                vdate as [Audit Date],vtime as [Audit Time] from DR_CTP c, ATMs a where 
                c.atmid=a.atmid and Convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text +
                    "'and a.bankid='TAMILNAD MERCANTILE BANK LIMITED' and a.state in (" + txtuser.Text + ")  and userid like '" + users + "' order by Convert(date,vdate) desc,vtime desc";

                }
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
            Response.AddHeader("content-disposition", "attachment;filename=MasterReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            btn_search_Click(sender, e);

            GridView1.AllowPaging = false;
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
                HyperLink hp = new HyperLink();
                hp.Text = "EXCEL";
                hp.NavigateUrl = "Report.aspx?auditid=" + e.Row.Cells[3].Text + "&dnld_excel=Y";
                e.Row.Cells[0].Controls.Add(hp);

                HyperLink hp1 = new HyperLink();
                hp1.Text = "PHOTOS";
                hp1.NavigateUrl = "photos.aspx?auditid=" + e.Row.Cells[3].Text + "&dnld_pdf=Y";
                e.Row.Cells[1].Controls.Add(hp1);

                HyperLink hp2 = new HyperLink();
                hp2.Text = "View Report";
                hp2.NavigateUrl = "MainPage1.aspx?auditid=" + e.Row.Cells[3].Text + "";
                e.Row.Cells[2].Controls.Add(hp2);
            }
        }
    }
}