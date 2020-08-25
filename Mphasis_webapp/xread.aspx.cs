using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp
{
    public partial class xread : System.Web.UI.Page
    {
        public string[] xread1(string query, string[] columns)
        {
            string[] x = new string[columns.Length];
            System.Data.SqlClient.SqlDataReader reader;
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, cn);

            cn.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    for (int i = 0; i < columns.Length; i++)
                    {
                        x[i] = reader[columns[i].ToString()].ToString().Trim();
                    }
                }
                catch
                {
                    reader.Close(); cmd.Dispose(); cn.Close(); cn.Dispose(); return x;
                }
            }
            reader.Close(); cmd.Dispose(); cn.Close(); cn.Dispose();
            return x;
        }
        /*public void bindGrid(string query,string dsID, GridView g, Page p)
        {
            SqlDataSource sql = new SqlDataSource(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString, query);
            sql.ID = dsID;
            sql.DataBind();
            sql.Dispose();
            p.Controls.Add(sql);
            g.DataSourceID = dsID;
            g.AllowPaging = true;
            g.AllowSorting = true;
            g.DataBind();
        }*/
        protected void Page_Load(object sender, EventArgs e)
        {
            ibuckethead bucket = new ibuckethead();
            bucket.BindGrid("select atmid from atms", "atmsid", GridView1, this.Page);
        }
    }
}