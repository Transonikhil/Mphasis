using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RCM
{
    public partial class DashBoardBankstateRCM : System.Web.UI.Page
    {
        int cnt = 0;
        string state = "";
        int cnt1 = 0;
        string bank = "";
        CommonClass obj = new CommonClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            ibuckethead bucket = new ibuckethead();


            if (!IsPostBack)
            {
                fillddstate();
                fillddbank();

            }
        }

        public void fillddstate()
        {
            string state = "SELECT  distinct state,state as STATE  FROM [atms] where rcm = '" + Session["sess_username"].ToString() + "'";
            obj.BindListboxWithValue(ddstate, state);
            if (txtuser.Text != "")
            {
                SqlDataSource2.SelectCommand = "select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP d inner join atms a on a.atmid = d.ATMID  where   convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -6, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) and a.state in (" + txtuser.Text + ") and a.rcm = '" + Session["sess_username"].ToString() + "' group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)";

            }
            else
            {
                SqlDataSource2.SelectCommand = "select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP d inner join atms a on a.atmid = d.ATMID  where   convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -6, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) and a.rcm = '" + Session["sess_username"].ToString() + "' group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)";
            }

        }

        public void fillddbank()
        {
            string state = "SELECT  distinct bankid,bankid as Bank  FROM [atms] where rcm = '" + Session["sess_username"].ToString() + "'";
            obj.BindListboxWithValue(ddbankstate, state);
            if (txtbankstate.Text != "")
            {
                SqlDataSource1.SelectCommand = "select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP d inner join atms a on a.atmid = d.ATMID where   convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -6, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) and a.bankid in (" + txtbankstate.Text + ") and a.rcm = '" + Session["sess_username"].ToString() + "' group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)";
            }
            else
            {
                SqlDataSource1.SelectCommand = "select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP d inner join atms a on a.atmid = d.ATMID where   convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -6, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) and a.rcm = '" + Session["sess_username"].ToString() + "' group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)";
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("FieldTracker.aspx?Offline=True");
        }
        protected void btn_searchState_Click(object sender, EventArgs e)
        {
            if (txtuser.Text != "")
            {
                SqlDataSource2.SelectCommand = "select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP d inner join atms a on a.atmid = d.ATMID  where   convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -6, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) and a.state in (" + txtuser.Text + ") and a.rcm = '" + Session["sess_username"].ToString() + "'  group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)";

            }
            else
            {
                SqlDataSource2.SelectCommand = "select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP d inner join atms a on a.atmid = d.ATMID  where   convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -6, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) and a.rcm = '" + Session["sess_username"].ToString() + "' group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)";
            }
            //chkBank.Items.Clear();
            fillddbank();
        }
        protected void btn_search_bank_Click(object sender, EventArgs e)
        {
            if (txtbankstate.Text != "")
            {
                SqlDataSource1.SelectCommand = "select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP d inner join atms a on a.atmid = d.ATMID where   convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -6, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) and a.bankid in (" + txtbankstate.Text + ") group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)";
            }
            else
            {
                SqlDataSource1.SelectCommand = "select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP d inner join atms a on a.atmid = d.ATMID where   convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -6, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)";
            }
            //chkstate.Items.Clear();
            fillddstate();
        }
    }
}