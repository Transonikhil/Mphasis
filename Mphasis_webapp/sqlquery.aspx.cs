using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

namespace Mphasis_webapp
{
    public partial class sqlquery : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            string build = "";
            string query = "select Vid, ATMID, USERID, CT_name, OM_name, FC_name, OC_name, AO_name, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, synctime, pix, version, vdate, vtime from dr_ctp where vdate like '09/%%/2014'";
            cn.Open();
            SqlCommand cmd = new SqlCommand(query, cn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                build += "insert into dr_ctp values('" + reader[0] + "', '" + reader[1] + "', '" + reader[2] + "', '" + reader[3] + "', '" + reader[4] + "', '" + reader[5] + "', '" + reader[6] + "', '" + reader[7] + "', '" + reader[8] + "', '" + reader[9] + "', '" + reader[10] + "', '" + reader[11] + "', '" + reader[12] + "', '" + reader[13] + "', '" + reader[14] + "', '" + reader[15] + "', '" + reader[16] + "', '" + reader[17] + "', '" + reader[18] + "', '" + reader[19] + "', '" + reader[20] + "', '" + reader[21] + "', '" + reader[22] + "', '" + reader[23] + "', " +
                "'" + reader[24] + "', '" + reader[25] + "', '" + reader[26] + "', '" + reader[27] + "')" + "<br/>";
                //Response.Write(build);
            }
            reader.Close();
            cn.Close();
            cn.Dispose();

            StreamWriter file2 = new StreamWriter(@"c:\file.txt");
            file2.WriteLine(build);
            file2.Close();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {

        }

    }
}