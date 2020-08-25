using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.CH
{
    public partial class AuditForAPeriod : System.Web.UI.Page
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

            //CalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            //CalendarExtender2.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            //CalendarExtender1.EndDate = toDate;
            //CalendarExtender2.EndDate = toDate;
            if (!Page.IsPostBack)
            {
                dd_bank.DataBind();

                dd_bank.Items.Add("ALL");
                dd_bank.Items.FindByText("ALL").Value = "%";
                dd_bank.Items.FindByValue("%").Selected = true;

                dd_cust.DataBind();

                dd_cust.Items.Add("ALL");
                dd_cust.Items.FindByText("ALL").Value = "%";
                dd_cust.Items.FindByValue("%").Selected = true;

                string state = "SELECT  distinct state,state as STATE  FROM [atms] where CH = '" + Session["sess_username"].ToString() + "'";
                obj.BindListboxWithValue(ddstate, state);
            }

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select 'All' as userid,'%' union all select distinct u.userid,u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role in ('DE','CM','AO','CH','RM') " +
                            " and a.CH like '" + HttpContext.Current.Session["sess_username"].ToString() + "'";
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

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_toDate.Text.Trim()))
            {
                txt_toDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            if (string.IsNullOrEmpty(txt_frmDate.Text.Trim()))
            {
                txt_frmDate.Text = DateTime.Now.ToString("MM'/'dd'/'yyyy");
            }

            txt_frmDate.Attributes.Add("readonly", "readonly");
            txt_toDate.Attributes.Add("readonly", "readonly");


            string sql = "";
            if (txtuser.Text == "")
            {
                sql = @"Select c.vid,c.ATMID as [ATM], a.Location, a. Bankid as [Bank], a.Client as [Client], 
                    convert(varchar(12),Convert(date,vdate),103)+' '+vtime as [Audit Date TIme] from DR_CTP c, atms a where c.atmid=a.atmid AND a.client like '" + dd_cust.SelectedItem.Value.ToString() + @"' 
                    and a.bankid like '" + dd_bank.SelectedItem.Value.ToString() + "' and a.CH like '" + Session["sess_username"] + "' and convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + "'";
            }
            else
            {
                sql = @"Select c.vid,c.ATMID as [ATM], a.Location, a. Bankid as [Bank], a.Client as [Client], 
                    convert(varchar(12),Convert(date,vdate),103)+' '+vtime as [Audit Date TIme] from DR_CTP c, atms a where c.atmid=a.atmid AND a.client like '" + dd_cust.SelectedItem.Value.ToString() + @"' 
                    and a.bankid like '" + dd_bank.SelectedItem.Value.ToString() + "'  and a.state in (" + txtuser.Text + ") and a.CH like '" + Session["sess_username"] + "' and convert(date,vdate) between '" + txt_frmDate.Text + "' AND '" + txt_toDate.Text + "'";
            }
            // Response.Write(sql);
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
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_Update_Click(sender, e);
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

            Response.AddHeader("content-disposition", "attachment;filename=AuditReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            btn_Update_Click(sender, e);

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
    }
}