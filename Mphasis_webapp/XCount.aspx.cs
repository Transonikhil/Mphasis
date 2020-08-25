using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace Mphasis_webapp
{
    public partial class XCount : System.Web.UI.Page
    {
        int xtotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.RowDataBound += GridView1_RowDataBound;

        }

        void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                xtotal += Convert.ToInt32(e.Row.Cells[1].Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = xtotal.ToString();
                e.Row.Cells[2].Text = " : ) ";
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT 'dr_ctp' as 'Table Name',count(*) as '# of Reports',vdate as 'Date' from dr_ctp where vdate = '" + txtfromdate.Text + "' group by vdate union all " +
                "SELECT 'BRANCH',count(*),vdate from dr_Branch where vdate = '" + txtfromdate.Text + "' group by vdate ";

            //Response.Write(query); Response.End();

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            sql.ConnectionString = cn.ConnectionString;
            sql.SelectCommand = query;
            sql.DataBind();

            GridView1.DataSourceID = sql.ID;
            GridView1.DataBind();
        }
    }
}