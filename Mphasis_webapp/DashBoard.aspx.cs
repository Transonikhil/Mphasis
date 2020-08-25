using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Mphasis_webapp
{
    public partial class DashBoard : System.Web.UI.Page
    {
        CommonClass cls = new CommonClass();
        ibuckethead bucket = new ibuckethead();

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            string exe = "Delete from dr_ctp where isdate(vdate)<>1";
            bucket.ExecuteQuery(exe);

            if (Page.IsPostBack)
            {
                //UpdatePanel1.Update();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["request"] == "ctp")
            {
                Button4.ForeColor = System.Drawing.Color.Black;
                Button4.BackColor = System.Drawing.Color.White;
                string[] q = bucket.verifyReader("select distinct COUNT(atmid) as 'a' from ATMs where status <> 'inactive' and atmid not like '%*BR*%' and atmid not like '%HDFC%' and bankid like 'ICICI'", "a");
                lbl_siteassigned.Text = q[0];
            }
            else if (Request.QueryString["request"] == "hdfc")
            {
                Button1.ForeColor = System.Drawing.Color.Black;
                Button1.BackColor = System.Drawing.Color.White;
                string[] q = bucket.verifyReader("select distinct COUNT(atmid) as 'a' from ATMs where status <> 'inactive'  and atmid like '%HDFC%'", "a");
                lbl_siteassigned.Text = q[0];
            }
            else if (Request.QueryString["request"] == "axis")
            {
                Button2.ForeColor = System.Drawing.Color.Black;
                Button2.BackColor = System.Drawing.Color.White;
                string[] q = bucket.verifyReader("select distinct COUNT(atmid) as 'a' from ATMs where bankid='BANK OF MAHARASHTRA' and status <> 'inactive' and atmid like '%*BR*%' and atmid not like '%HDFC%' and bankid='BANK OF MAHARASHTRA'", "a");
                lbl_siteassigned.Text = q[0];
            }

            //graph();
            oldnewversion();
            if (Page.IsPostBack)
            {
                // timer.Enabled = true;

            }
        }

        //public void graph()
        //{
        //    if (Request.QueryString["request"] == "ctp")
        //    {
        //        piechartctp();
        //        barchatctp();
        //    }
        //    else
        //    {
        //        Response.Redirect("Default.aspx?request=ctp");
        //    }
        //    //timer.Enabled = false;
        //}

        public string piechart()
        {
            int flag = 0;
            string sql = "";
            if (Request.QueryString["request"] == "ctp")
            {
                Button4.ForeColor = System.Drawing.Color.Black;
                Button4.BackColor = System.Drawing.Color.White;
                string[] q = bucket.verifyReader("select distinct COUNT(atmid) as 'a' from ATMs where status <> 'inactive' and atmid not like '%*BR*%' and atmid not like '%HDFC%' and bankid like 'ICICI'", "a");
                lbl_siteassigned.Text = "5";// q[0];

                sql =
                @"
            select count(atmid) as Audited from atms 
            where atmid in (select atmid from current_dr_ctp) and status<>'Inactive' and atmid not like '%*BR*%' and atmid not like '%HDFC%' and bankid like 'ICICI'
            union all
            select count(atmid) from atms 
            where atmid not in (select atmid from current_dr_ctp) and status<>'Inactive' and atmid not like '%*BR*%' and atmid not like '%HDFC%' and bankid like 'ICICI'
            ";
                flag = 1;
            }
            else
            if (Request.QueryString["request"] == "hdfc")
            {
                Button1.ForeColor = System.Drawing.Color.Black;
                Button1.BackColor = System.Drawing.Color.White;
                //Chart4.Visible = false; Label1.Visible = false;            
                string[] q = bucket.verifyReader("select distinct COUNT(atmid) as 'a' from ATMs where status <> 'inactive'  and atmid like '%HDFC%'", "a");
                lbl_siteassigned.Text = q[0];
                sql =
               @"
            select count(atmid) as Audited from atms 
            where atmid in (select distinct atmid from current_dr_ctp where atmid like '%HDFC%') and status<>'Inactive'
            and bankid='HDFC'
            union all
            select count(atmid) from atms 
            where atmid not in (select distinct atmid from current_dr_ctp  where atmid like '%HDFC%') and status<>'Inactive' and bankid='HDFC'
            ";
                flag = 1;
            }
            else if (Request.QueryString["request"] == "axis")
            {
                Button2.ForeColor = System.Drawing.Color.Black;
                Button2.BackColor = System.Drawing.Color.White;
                string[] q = bucket.verifyReader("select distinct COUNT(atmid) as 'a' from ATMs where bankid='BANK OF MAHARASHTRA' and status <> 'inactive' and atmid like '%*BR*%' and atmid not like '%HDFC%' and bankid='BANK OF MAHARASHTRA'", "a");
                lbl_siteassigned.Text = q[0];
                //  Chart4.Visible = false; Label1.Visible = false;           

                sql =
               @"
            select count(atmid) as Audited from atms 
            where atmid in (select atmid from DR_BRANCH) and status<>'Inactive' and atmid like '%*BR*%' and atmid not like '%HDFC%'
            and bankid='BANK OF MAHARASHTRA'
            union all
            select count(atmid) from atms 
            where atmid not in (select atmid from DR_BRANCH) and status<>'Inactive' and atmid like '%*BR*%' and atmid not like '%HDFC%'
            and bankid='BANK OF MAHARASHTRA'
            ";
                flag = 1;
            }
            else
            {
                Response.Redirect("Dashboard.aspx?request=ctp");
            }
            SqlCommand cmd1 = new SqlCommand(sql);
            DataTable dt1 = bucket.GetData(cmd1);
            cmd1.CommandTimeout = 200;
            string pie = "";
            if (flag == 1 && dt1.Rows.Count > 0)
            {
                pie = @"<script src=""js/highcharts.js""></script>
   
        <script type=""text/javascript"">
		    $(function () {

		        $(document).ready(function () {

		            // Build the chart
		            $('#container').highcharts({
		                chart: {
		                    plotBackgroundColor: null,
		                    plotBorderWidth: null,
		                    plotShadow: false
		                },
		                title: {
		                    text: ''
		                },
		                tooltip: {
		                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		                },
		                plotOptions: {
		                    pie: {
		                        allowPointSelect: true,
		                        cursor: 'pointer',
		                        dataLabels: {
		                            enabled: false
		                        },
		                        showInLegend: true
		                    }
		                },
		                series: [{
		                    type: 'pie',
		                    name: 'Audits',
		                    data: [                   
                   
                    ['Audited', " + Convert.ToInt32(dt1.Rows[0][0]) + @"],
                    ['Pending'," + Convert.ToInt32(dt1.Rows[1][0]) + @"]
                ]
		                }]
		            });
		        });

		    });
		</script>";
            }
            return pie;
        }

        public string barchat()
        {
            string bar = "";
            int flag = 0;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
            DataTable dt = new DataTable();
            string sql = "";
            string data = "";
            if (Request.QueryString["request"] == "ctp")
            {
                Button4.ForeColor = System.Drawing.Color.Black;
                Button4.BackColor = System.Drawing.Color.White;

                sql =
                @"
            select vdate as [vdate],COUNT(*) as [visit] from dr_ctp d join atms a on d.ATMID=a.atmid where
            convert(date,vdate) between Convert(varchar,GETDATE()-6,101) and Convert(varchar,GETDATE(),101) 
            and status<>'Inactive' and a.atmid not like '%*BR*%' and a.atmid not like '%HDFC%'
            group by vdate order by vdate
            ";
                flag = 1;
            }
            else
            if (Request.QueryString["request"] == "hdfc")
            {
                Button1.ForeColor = System.Drawing.Color.Black;
                Button1.BackColor = System.Drawing.Color.White;
                //  Chart4.Visible = false; Label1.Visible = false;

                sql =
                 @"
            select vdate as [vdate],COUNT(*) as [visit] from current_dr_ctp d join atms a on d.ATMID=a.atmid where bankid='HDFC'
            and convert(date,vdate) between Convert(varchar,GETDATE()-7,101) and Convert(varchar,GETDATE(),101) 
            and status<>'Inactive'
            group by vdate order by vdate
            ";
                flag = 1;
            }
            else if (Request.QueryString["request"] == "axis")
            {
                Button2.ForeColor = System.Drawing.Color.Black;
                Button2.BackColor = System.Drawing.Color.White;

                //Chart4.Visible = false; Label1.Visible = false;            

                sql =
              @"
            select vdate as [vdate],COUNT(*) as [visit] from DR_BRANCH d join atms a on d.ATMID=a.atmid where bankid='BANK OF MAHARASHTRA'
            and convert(date,vdate) between Convert(varchar,GETDATE()-7,101) and Convert(varchar,GETDATE(),101) 
            and status<>'Inactive' and a.atmid like '%*BR*%' and a.atmid not like '%HDFC%'
            group by vdate order by vdate
            ";
                flag = 1;
            }
            if (flag == 1)
            {

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 200;
                cn.Open();
                SqlDataAdapter a = new SqlDataAdapter(cmd);
                a.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data += "['" + dt.Rows[i][0].ToString().Split(' ')[0] + "'," + dt.Rows[i][1] + "],";
                }
                Label1.Visible = false;

                //  Chart4.Visible = true;
                Label1.Visible = true;
                bar = @"

<script type=""text/javascript"">
$(function () {
    $('#Columndig').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: ''
        },
       
        xAxis: {
            type: 'category',
            labels: {
                rotation: -45,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: ''
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            pointFormat: 'Audit completed: <b>{point.y:.f} </b>'
        },
        series: [{
            name: 'Audit',
            data: [" + data +
                   @"],
            dataLabels: {
                enabled: true,
                rotation: -0.1,
                color: '#FFFFFF',
                align: 'right',
                format: '{point.y:.f}', // one decimal
                y: 15, // 10 pixels down from the top
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        }]
    });
});
		</script>";
            }

            return bar;
        }


        public string AreaOfficerWiseSummary()
        {

            int flag = 0;
            string column = "";
            string sql = "";
            string data = "";
            if (Request.QueryString["request"] == "ctp")
            {
                Button4.ForeColor = System.Drawing.Color.Black;
                Button4.BackColor = System.Drawing.Color.White;
                string[] q = bucket.verifyReader("select distinct COUNT(atmid) as 'a' from ATMs where status <> 'inactive' and atmid not like '%*BR*%' and atmid not like '%HDFC%' and bankid like 'ICICI'", "a");
                //  lbl_siteassigned.Text = q[0];

                sql =
                @"select u.AO as [area officer],COUNT(d.vid) as [visit] from DR_CTP d, users u where d.USERID=u.userid and convert(date,d.vdate ) 
                            between CONVERT(date,getdate() -30) and CONVERT(date,GETDATE()) group by u.ao union all select distinct ao, '0' from users where AO<>'AREA OFFICER' 
                            order by visit desc";
                flag = 1;
            }
            if (flag == 1)
            {
                SqlCommand cmd1 = new SqlCommand(sql);
                cmd1.CommandTimeout = 200;
                DataTable dt1 = bucket.GetData(cmd1);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    data += "['" + dt1.Rows[i][0].ToString().Split(' ')[0] + "'," + dt1.Rows[i][1] + "],";
                }
                column = @"

<script type=""text/javascript"">
$(function () {
    $('#ColumndigforOfficer').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: ''
        },
       
        xAxis: {
            type: 'category',
            labels: {
                rotation: -45,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Number of Audits Completed'
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            pointFormat: 'Audit completed: <b>{point.y:.f} </b>'
        },
        series: [{
            name: 'Audit',
            data: [" + data +


                  @"],
            dataLabels: {
                enabled: true,
                rotation: -0.1,
                color: '#FFFFFF',
                align: 'center',
                format: '{point.y:.f}', // one decimal
                y: 15, // 10 pixels down from the top
                style: {
                    fontSize: '11px',
                    fontFamily: 'Arial, Helvetica, sans-serif'
                }
            }
        }]
    });
});
		</script>";
            }
            return column;
        }

        protected void hpl_Click(object sender, EventArgs e)
        {
            string query = "update users set oldnewversion = 0 where userid = '" + Session["sess_userid"].ToString() + "'";
            cls.NonExecuteQuery(query);
            Response.Redirect("Default.aspx");
        }

        public void oldnewversion()
        {
            SqlConnection cn = new SqlConnection();
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                int choice = 0;
                string query = "select oldnewversion from users where userid = '" + Session["sess_userid"].ToString() + "'";
                cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandTimeout = 200;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    choice = Convert.ToInt32(dr[0]);
                }
                if (choice == 1)
                {
                    hpl.Text = "Switch to old version";
                    // hpl.PostBackUrl = "Default.aspx";
                }
                cn.Close(); cn.Dispose();
            }
            catch (Exception e)
            {

            }
            finally
            {
                dr.Close();
                cn.Close(); cn.Dispose();

            }
        }


    }
}