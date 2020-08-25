using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class FieldTrackerCadbury : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            //SqlConnection con=new SqlConnection();

            //SqlDataCommand cmd=

            string sql1 = "update location set L_date='07/14/2014',L_time='12:54' where ISDATE(L_date)<>1";
            bucket.ExecuteQuery(sql1);

            try
            {
                if (Session["sess_role"].ToString().Contains("RO"))
                {
                    Response.Redirect("CurrentAuditRO.aspx");
                }
                else
                {
                }
            }
            catch { Response.Redirect("login.aspx"); }

            string qOnline = "select count(*) as 'online' from location l,users u where l.userid=u.userid and u.oc='cad' and l_date<>'' and Rem_Battery>5 and DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(L_date,'/','-') + ' ' + l_time) > -60";
            string[] aonline = bucket.verifyReader(qOnline, "online");
            lbl_Online.Text = aonline[0];

            string qOffline = "select count(*) as 'offline' from location l,users u where l.userid=u.userid and u.oc='cad' and l_date<>'' and Rem_Battery>5 and DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(L_date,'/','-') + ' ' + l_time) < -60";
            string[] aOffline = bucket.verifyReader(qOffline, "offline");
            lbl_Offline.Text = aOffline[0];

            string qBatteryLow = "select count(*) as 'BL' from location l,users u where l.userid=u.userid and u.oc='cad' and l_date<>'' and Rem_Battery<=5";
            string[] aBatteryLow = bucket.verifyReader(qBatteryLow, "BL");
            lbl_Battery.Text = aBatteryLow[0];

            if (Request.QueryString.ToString() != "")
            {
                if (Session["role"].ToString().Contains("Cadbury"))
                {
                    string sqlOffline = null;
                    if (Request.QueryString["Online"] == "True")
                    {
                        sqlOffline = "select l.userid as [User], case when rem_battery>0 then ltrim(rtrim(rem_battery)) + '%' when rem_battery<0 then '0%' end as [Battery Remaining],'LocatorOne.aspx?userid=' + l.userid as [View On Map], substring(L_Date,4,3) + substring(L_Date,1,2)+ substring(L_Date,6,5) + ' ' + L_time as [Last Updated On] from location L join users u on l.userid=u.userid where u.oc='cad' and l_date<>'' and Rem_Battery>5 and DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(L_date,'/','-') + ' ' + l_time) > -60";
                    }
                    else if (Request.QueryString["Offline"] == "True")
                    {
                        sqlOffline = "select l.userid as [User], case when rem_battery>0 then ltrim(rtrim(rem_battery)) + '%' when rem_battery<0 then '0%' end as [Battery Remaining],'LocatorOne.aspx?userid=' + l.userid as [View On Map], substring(L_Date,4,3) + substring(L_Date,1,2)+ substring(L_Date,6,5) + ' ' + L_time as [Last Updated On] from location L join users u on l.userid=u.userid where u.oc='cad' and l_date<>'' and Rem_Battery>5 and DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(L_date,'/','-') + ' ' + l_time) < -60";
                    }
                    else if (Request.QueryString["Battery"] == "True")
                    {
                        sqlOffline = "select l.userid as [User],case when rem_battery>0 then ltrim(rtrim(rem_battery)) + '%' when rem_battery<0 then '0%' end as [Battery Remaining],'LocatorOne.aspx?userid=' + l.userid as [View On Map], substring(L_Date,4,3) + substring(L_Date,1,2)+ substring(L_Date,6,5) + ' ' + L_time as [Last Updated On] from location L join users u on l.userid=u.userid where u.oc='cad' and l_date<>'' and Rem_Battery<=5 ";
                    }
                    // Response.Write(sqlOffline);
                    bucket.BindGrid(GridView1, sqlOffline);
                    GridView1.AllowPaging = false;
                    GridView1.DataBind();

                }
            }
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Visible = false;
            }
        }
        protected void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("LocatorN_Poly.aspx");
        }
        protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[3].Visible = false;
            }
            catch { }
        }
    }
}