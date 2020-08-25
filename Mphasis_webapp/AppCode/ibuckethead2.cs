using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;

public class ibuckethead2
{
    public string cleanText(TextBox t)
    {
        if (t.Text == "" || t.Text == null) { t.Text = ""; } else { t.Text = t.Text.Trim().Replace("'", ""); } return t.Text;
    }
	public void bindGrid(string query,string dsID, GridView g, Page p)
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
    }
	public void BindDropDown(DropDownList dd, string query)
    {
        string connnecstring = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(connnecstring);
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, cn);
        string value = ""; int i = 0;
        cn.Open();
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            value = value + reader[0] + ",";
        }
        reader.Close(); reader.Dispose(); cn.Close(); cn.Dispose();

        string[] split = Regex.Split(value, ",");
        while (i < (split.Length - 1))
        {
            dd.Items.Add(split[i]);
            i++;
        }
    }
    
	public string CountRows(GridView g,Label l)
    {
        g.AllowPaging = false;
        g.DataBind();
        l.Text = g.Rows.Count.ToString() + " Records matching your criteria.";
        g.AllowPaging = true;
        g.DataBind();
        return l.Text;
    }
	
    public void RunProcedure(string Name)
    {
        string connnecstring;
        connnecstring = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        System.Data.SqlClient.SqlConnection PubsConn = new System.Data.SqlClient.SqlConnection(connnecstring);
        System.Data.SqlClient.SqlCommand CMDproc = new System.Data.SqlClient.SqlCommand();
        System.Data.SqlClient.SqlDataReader reader;
        CMDproc.CommandType = System.Data.CommandType.StoredProcedure;
        CMDproc.CommandText = Name;
        CMDproc.Connection = PubsConn;
        PubsConn.Open();
        reader = CMDproc.ExecuteReader();
        PubsConn.Close();
        CMDproc.Dispose();
        reader.Close();
        reader.Dispose();
        PubsConn.Dispose();
    }

    public void BindGrid(GridView g, string query)
    {
        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
        cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;

        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query);
        
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = cn;
        try
        {
            cn.Open();
            sda.SelectCommand = cmd;
			try
			{
            sda.Fill(dt);
			}
			catch
			{
			g.DataSource = dt;
            g.AllowPaging = true;
            g.DataBind();
            cn.Close();
            cn.Dispose();
			}
            g.DataSource = dt;
            g.AllowPaging = true;
            g.DataBind();
            cn.Close();
            cn.Dispose();
        }
        finally
        {
            cn.Close();
            sda.Dispose();
            cn.Dispose();
        }
    }

    public System.Data.SqlClient.SqlDataReader GetDate(string myquery)
    {
        System.Data.SqlClient.SqlConnection mycon = new System.Data.SqlClient.SqlConnection();
        System.Data.SqlClient.SqlCommand comUserSelect = default(System.Data.SqlClient.SqlCommand);
        System.Data.SqlClient.SqlDataReader myReader = default(System.Data.SqlClient.SqlDataReader);
        string connnecstring = null;
        connnecstring = global::System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        mycon = new System.Data.SqlClient.SqlConnection(connnecstring);
        comUserSelect = new System.Data.SqlClient.SqlCommand(myquery, mycon);
        mycon.Open();
        myReader = comUserSelect.ExecuteReader();
        return myReader;
    }

    public string ExecuteQuery(string query)
    {
        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
        cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, cn);
        try { cn.Open(); cmd.ExecuteNonQuery(); cn.Close(); cn.Dispose(); return "Success"; }
        catch (System.Data.SqlClient.SqlException sql) { return sql.Message.ToString(); }
        catch (Exception ex) { return ex.Message.ToString(); }
        finally { cn.Close(); cn.Dispose(); }
    }
    public string[] xread(string query, string[] columns) /*For Select*/
    {
        string[] x = new string[columns.Length];
        var cn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        var cmd = new System.Data.SqlClient.SqlCommand(query, cn);

        cn.Open();
        var reader = cmd.ExecuteReader();
        int i = 0;
        while (reader.Read())
        {
            try
            {
                for (i = 0; i < columns.Length; i++)
                {
                    x[i] = reader[columns[i].ToString()].ToString().Trim();
                }
            }
            catch
            {
                x[i] = "N/A";
            }
        }
        reader.Close(); cmd.Dispose(); cn.Close(); cn.Dispose();
        return x;
    }
    public string[] yread(string query, string[] columns) /*For Select*/
    {
        string[] y = new string[columns.Length];

        var cn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        var cmd = new System.Data.SqlClient.SqlCommand(query, cn);

        cn.Open();
        var reader = cmd.ExecuteReader();
        int i = 0;
        while (reader.Read())
        {
            try
            {
                if (i > columns.Length)
                {
                    break;
                }
                y[i] = reader[0].ToString().Trim();
                i++;
            }
            catch
            {
                y[i] = "N/A";
            }
        }
        reader.Close(); cmd.Dispose(); cn.Close(); cn.Dispose();
        return y;
    }

    public string[] verifyReader(string query, string ColumnNumber1, string ColumnNumber2 = "0", string ColumnNumber3 = "0", string ColumnNumber4 = "0", string ColumnNumber5 = "0", string ColumnNumber6 = "0", string ColumnNumber7 = "0", string ColumnNumber8 = "0", string ColumnNumber9 = "0", string ColumnNumber10 = "0", string ColumnNumber11 = "0", string ColumnNumber12 = "0", string ColumnNumber13 = "0", string ColumnNumber14 = "0", string ColumnNumber15 = "0", string ColumnNumber16 = "0", string ColumnNumber17 = "0", string ColumnNumber18 = "0", string ColumnNumber19 = "0", string ColumnNumber20 = "0", string ColumnNumber21="0", string ColumnNumber22 = "0", string ColumnNumber23 = "0", string ColumnNumber24 = "0", string ColumnNumber25 = "0", string ColumnNumber26 = "0", string ColumnNumber27 = "0", string ColumnNumber28 = "0", string ColumnNumber29 = "0", string ColumnNumber30 = "0",  string ColumnNumber31 = "0", string ColumnNumber32 = "0", string ColumnNumber33 = "0", string ColumnNumber34 = "0", string ColumnNumber35 = "0", string ColumnNumber36 = "0", string ColumnNumber37 = "0", string ColumnNumber38 = "0", string ColumnNumber39 = "0", string ColumnNumber40 = "0",string ColumnNumber41 = "0", string ColumnNumber42 = "0", string ColumnNumber43 = "0", string ColumnNumber44 = "0",string ColumnNumber45 = "0", string ColumnNumber46 = "0", string ColumnNumber47 = "0", string ColumnNumber48 = "0", string ColumnNumber49 = "0", string ColumnNumber50 = "0")
    {
        int i=0;
        string[] x = new string[50];
        string[] ColumnArray = new string[50];

        ColumnArray[0] = ColumnNumber1; ColumnArray[1] = ColumnNumber2; ColumnArray[2] = ColumnNumber3; ColumnArray[3] = ColumnNumber4; ColumnArray[4] = ColumnNumber5; ColumnArray[5] = ColumnNumber6; ColumnArray[6] = ColumnNumber7; ColumnArray[7] = ColumnNumber8; ColumnArray[8] = ColumnNumber9; ColumnArray[9] = ColumnNumber10; ColumnArray[10] = ColumnNumber11; ColumnArray[11] = ColumnNumber12; ColumnArray[12] = ColumnNumber13; ColumnArray[13] = ColumnNumber14; ColumnArray[14] = ColumnNumber15; ColumnArray[15] = ColumnNumber16; ColumnArray[16] = ColumnNumber17; ColumnArray[17] = ColumnNumber18; ColumnArray[18] = ColumnNumber19; ColumnArray[19] = ColumnNumber20; ColumnArray[20] = ColumnNumber21; ColumnArray[21] = ColumnNumber22; ColumnArray[22] = ColumnNumber23; ColumnArray[23] = ColumnNumber24; ColumnArray[24] = ColumnNumber25; ColumnArray[25] = ColumnNumber26; ColumnArray[26] = ColumnNumber27; ColumnArray[27] = ColumnNumber28; ColumnArray[28] = ColumnNumber29; ColumnArray[29] = ColumnNumber30; ColumnArray[30] = ColumnNumber31; ColumnArray[31] = ColumnNumber32; ColumnArray[32] = ColumnNumber33; ColumnArray[33] = ColumnNumber34; ColumnArray[34] = ColumnNumber35; ColumnArray[35] = ColumnNumber36; ColumnArray[36] = ColumnNumber37; ColumnArray[37] = ColumnNumber38; ColumnArray[38] = ColumnNumber39; ColumnArray[39] = ColumnNumber40; ColumnArray[40] = ColumnNumber41; ColumnArray[41] = ColumnNumber42; ColumnArray[42] = ColumnNumber43; ColumnArray[43] = ColumnNumber44; ColumnArray[44] = ColumnNumber45; ColumnArray[45] = ColumnNumber46; ColumnArray[46] = ColumnNumber47; ColumnArray[47] = ColumnNumber48; ColumnArray[48] = ColumnNumber49; ColumnArray[49] = ColumnNumber50;


        System.Data.SqlClient.SqlDataReader reader;
        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
        cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, cn);

        cn.Open();
        reader = cmd.ExecuteReader();
        try{while (reader.Read()){try{while (i < 50){try{x[i] = reader[ColumnArray[i].ToString()].ToString().Trim();}catch{if (ColumnArray[i].ToString() != "0"){x[i] = "0";}else{reader.Close();cn.Close();cn.Dispose();break;}}i++;}reader.Close();cn.Close();cn.Dispose();return x;}catch{reader.Close();cn.Close();cn.Dispose();return x;}}return x;}
        catch{reader.Close();cn.Close();cn.Dispose();return x;}
    }

    public System.Data.DataTable GetData( System.Data.SqlClient.SqlCommand cmd)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        string strConnString = global::System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        
        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();

        con.ConnectionString = global::System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
        
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
        finally
        {
            con.Close();
            sda.Dispose();
            con.Dispose();
        }
    }
}