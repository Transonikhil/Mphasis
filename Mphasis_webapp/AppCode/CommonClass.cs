using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for CommonClass
/// </summary>
public class CommonClass
{
    SqlConnection mycon = new SqlConnection();
    SqlCommand comUserSelect = default(SqlCommand);
    SqlDataReader myReader = default(SqlDataReader);

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
			//try
		//	{
            sda.Fill(dt);
		//	}
			//catch{}
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

        public void BindListboxWithValue(ListBox dd, string query)
        {
            dd.Items.Clear();
            string connnecstring = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(connnecstring);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, cn);
            string value = ""; int i = 0;
            cn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                value = value + reader[0] + ":" + reader[1] + ",";
            }
            reader.Close(); reader.Dispose(); cn.Close(); cn.Dispose();

            string[] split = Regex.Split(value, ",");
            while (i < (split.Length - 1))
            {
                string[] split1 = Regex.Split(split[i], ":");
                dd.Items.Add(new ListItem(split1[0], split1[1]));
                i++;
            }
        }

    public string getdate(string chkdate)
    {
        string dt = "";

        try
        {
            dt = chkdate.Substring(3, 2) + "/" + chkdate.Substring(0, 2) + "/" + chkdate.Substring(6, 4);
        }
        catch(Exception ex)
        {

        }

        return dt;
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
	
    public SqlDataReader Connect(string myquery)
    {
        string connnectstring = ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        mycon = new SqlConnection(connnectstring);
        comUserSelect = new SqlCommand(myquery, mycon);
        try
        {
            mycon.Open();
            myReader = comUserSelect.ExecuteReader();
            return myReader;
        }

        catch (Exception ee)
        {
            ee.Message.ToString();
        }

        return myReader;
    }

    public int NonExecuteQuery(string myquery)
    {
        int rslt = 0;
        string connnecstring = null;
        connnecstring = global::System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        mycon = new SqlConnection(connnecstring);
        comUserSelect = new SqlCommand(myquery, mycon);
        try
        {
            mycon.Open();
            rslt = comUserSelect.ExecuteNonQuery();
            mycon.Close();
            return rslt;
        }

        catch (Exception ee)
        {
            ee.Message.ToString();
        }

        finally
        {
            mycon.Close();
        }
        return rslt;
    }
	
	public string[] verifyReader(string query, string ColumnNumber1, string ColumnNumber2 = "0", string ColumnNumber3 = "0", string ColumnNumber4 = "0", string ColumnNumber5 = "0", string ColumnNumber6 = "0", string ColumnNumber7 = "0", string ColumnNumber8 = "0", string ColumnNumber9 = "0", string ColumnNumber10 = "0", string ColumnNumber11 = "0", string ColumnNumber12 = "0", string ColumnNumber13 = "0", string ColumnNumber14 = "0", string ColumnNumber15 = "0", string ColumnNumber16 = "0", string ColumnNumber17 = "0", string ColumnNumber18 = "0", string ColumnNumber19 = "0", string ColumnNumber20 = "0", string ColumnNumber21="0", string ColumnNumber22 = "0", string ColumnNumber23 = "0", string ColumnNumber24 = "0", string ColumnNumber25 = "0", string ColumnNumber26 = "0", string ColumnNumber27 = "0", string ColumnNumber28 = "0", string ColumnNumber29 = "0", string ColumnNumber30 = "0",  string ColumnNumber31 = "0", string ColumnNumber32 = "0", string ColumnNumber33 = "0", string ColumnNumber34 = "0", string ColumnNumber35 = "0", string ColumnNumber36 = "0", string ColumnNumber37 = "0", string ColumnNumber38 = "0", string ColumnNumber39 = "0", string ColumnNumber40 = "0",string ColumnNumber41 = "0", string ColumnNumber42 = "0", string ColumnNumber43 = "0", string ColumnNumber44 = "0",string ColumnNumber45 = "0", string ColumnNumber46 = "0", string ColumnNumber47 = "0", string ColumnNumber48 = "0", string ColumnNumber49 = "0", string ColumnNumber50 = "0",string ColumnNumber51="0",string ColumnNumber52="0",string ColumnNumber53="0",string ColumnNumber54="0",string ColumnNumber55="0",string ColumnNumber56="0",string ColumnNumber57="0",string ColumnNumber58="0",string ColumnNumber59="0",string ColumnNumber60="0",string ColumnNumber61="0",string ColumnNumber62="0",string ColumnNumber63="0",string ColumnNumber64="0",string ColumnNumber65="0",string ColumnNumber66="0",string ColumnNumber67="0",string ColumnNumber68="0",string ColumnNumber69="0",string ColumnNumber70="0",string ColumnNumber71="0",string ColumnNumber72="0",string ColumnNumber73="0",string ColumnNumber74="0",string ColumnNumber75="0",string ColumnNumber76="0",string ColumnNumber77="0",string ColumnNumber78="0",string ColumnNumber79="0",string ColumnNumber80="0",string ColumnNumber81="0",string ColumnNumber82="0",string ColumnNumber83="0",string ColumnNumber84="0",string ColumnNumber85="0",string ColumnNumber86="0",string ColumnNumber87="0",string ColumnNumber88="0",string ColumnNumber89="0",string ColumnNumber90="0",string ColumnNumber91="0",string ColumnNumber92="0",string ColumnNumber93="0",string ColumnNumber94="0",string ColumnNumber95="0",string ColumnNumber96="0",string ColumnNumber97="0",string ColumnNumber98="0",string ColumnNumber99="0",string ColumnNumber100="0")
    {
        int i=0;
        string[] x = new string[100];
        string[] ColumnArray = new string[100];

        ColumnArray[0] = ColumnNumber1; ColumnArray[1] = ColumnNumber2; ColumnArray[2] = ColumnNumber3; ColumnArray[3] = ColumnNumber4; ColumnArray[4] = ColumnNumber5; ColumnArray[5] = ColumnNumber6; ColumnArray[6] = ColumnNumber7; ColumnArray[7] = ColumnNumber8; ColumnArray[8] = ColumnNumber9; ColumnArray[9] = ColumnNumber10; ColumnArray[10] = ColumnNumber11; ColumnArray[11] = ColumnNumber12; ColumnArray[12] = ColumnNumber13; ColumnArray[13] = ColumnNumber14; ColumnArray[14] = ColumnNumber15; ColumnArray[15] = ColumnNumber16; ColumnArray[16] = ColumnNumber17; ColumnArray[17] = ColumnNumber18; ColumnArray[18] = ColumnNumber19; ColumnArray[19] = ColumnNumber20; ColumnArray[20] = ColumnNumber21; ColumnArray[21] = ColumnNumber22; ColumnArray[22] = ColumnNumber23; ColumnArray[23] = ColumnNumber24; ColumnArray[24] = ColumnNumber25; ColumnArray[25] = ColumnNumber26; ColumnArray[26] = ColumnNumber27; ColumnArray[27] = ColumnNumber28; ColumnArray[28] = ColumnNumber29; ColumnArray[29] = ColumnNumber30; ColumnArray[30] = ColumnNumber31; ColumnArray[31] = ColumnNumber32; ColumnArray[32] = ColumnNumber33; ColumnArray[33] = ColumnNumber34; ColumnArray[34] = ColumnNumber35; ColumnArray[35] = ColumnNumber36; ColumnArray[36] = ColumnNumber37; ColumnArray[37] = ColumnNumber38; ColumnArray[38] = ColumnNumber39; ColumnArray[39] = ColumnNumber40; ColumnArray[40] = ColumnNumber41; ColumnArray[41] = ColumnNumber42; ColumnArray[42] = ColumnNumber43; ColumnArray[43] = ColumnNumber44; ColumnArray[44] = ColumnNumber45; ColumnArray[45] = ColumnNumber46; ColumnArray[46] = ColumnNumber47; ColumnArray[47] = ColumnNumber48; ColumnArray[48] = ColumnNumber49; ColumnArray[49] = ColumnNumber50;ColumnArray [50]=ColumnNumber51;ColumnArray [51]=ColumnNumber52;ColumnArray [52]=ColumnNumber53;ColumnArray [53]=ColumnNumber54;ColumnArray [54]=ColumnNumber55;ColumnArray [55]=ColumnNumber56;ColumnArray [56]=ColumnNumber57;ColumnArray [57]=ColumnNumber58;ColumnArray [58]=ColumnNumber59;ColumnArray [59]=ColumnNumber60;ColumnArray [60]=ColumnNumber61;ColumnArray [61]=ColumnNumber62;ColumnArray [62]=ColumnNumber63;ColumnArray [63]=ColumnNumber64;ColumnArray [64]=ColumnNumber65;ColumnArray [65]=ColumnNumber66;ColumnArray [66]=ColumnNumber67;ColumnArray [67]=ColumnNumber68;ColumnArray [68]=ColumnNumber69;ColumnArray [69]=ColumnNumber70;ColumnArray [70]=ColumnNumber71;ColumnArray [71]=ColumnNumber72;ColumnArray [72]=ColumnNumber73;ColumnArray [73]=ColumnNumber74;ColumnArray [74]=ColumnNumber75;ColumnArray [75]=ColumnNumber76;ColumnArray [76]=ColumnNumber77;ColumnArray [77]=ColumnNumber78;ColumnArray [78]=ColumnNumber79;ColumnArray [79]=ColumnNumber80;ColumnArray [80]=ColumnNumber81;ColumnArray [81]=ColumnNumber82;ColumnArray [82]=ColumnNumber83;ColumnArray [83]=ColumnNumber84;ColumnArray [84]=ColumnNumber85;ColumnArray [85]=ColumnNumber86;ColumnArray [86]=ColumnNumber87;ColumnArray [87]=ColumnNumber88;ColumnArray [88]=ColumnNumber89;ColumnArray [89]=ColumnNumber90;ColumnArray [90]=ColumnNumber91;ColumnArray [91]=ColumnNumber92;ColumnArray [92]=ColumnNumber93;ColumnArray [93]=ColumnNumber94;ColumnArray [94]=ColumnNumber95;ColumnArray [95]=ColumnNumber96;ColumnArray [96]=ColumnNumber97;ColumnArray [97]=ColumnNumber98;ColumnArray [98]=ColumnNumber99;ColumnArray [99]=ColumnNumber100;



        System.Data.SqlClient.SqlDataReader reader;
        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
        cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString;
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, cn);

        cn.Open();

        try
        {
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    while (i < 100)
                    {
                        try
                        {
                            x[i] = reader[ColumnArray[i].ToString()].ToString().Trim();
                        }
                        catch
                        {
                            if (ColumnArray[i].ToString() != "0")
                            {
                                x[i] = "0";
                            }
                            else
                            {
                                reader.Close();
                                cn.Close();
                                cn.Dispose();
                                break;
                            }
                        }
                        i++;
                    }
                    reader.Close();
                    cn.Close();
                    cn.Dispose();
                    return x;
                }
                catch
                {
                    reader.Close();
                    cn.Close();
                    cn.Dispose();
                    return x;
                }
            }
            return x;
        }
        catch
        {
            cn.Close();
            cn.Dispose();
            return x;
        }
        finally
        {
            cn.Close();
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

    public string GetPostBackControlId(Page page)
    {
        if (!page.IsPostBack)
            return string.Empty;

        Control control = null;
        // first we will check the "__EVENTTARGET" because if post back made by the controls
        // which used "_doPostBack" function also available in Request.Form collection.
        string controlName = page.Request.Params["__EVENTTARGET"];
        if (!String.IsNullOrEmpty(controlName))
        {
            control = page.FindControl(controlName);
        }
        else
        {
            // if __EVENTTARGET is null, the control is a button type and we need to
            // iterate over the form collection to find it

            // ReSharper disable TooWideLocalVariableScope
            string controlId;
            Control foundControl;
            // ReSharper restore TooWideLocalVariableScope

            foreach (string ctl in page.Request.Form)
            {
                // handle ImageButton they having an additional "quasi-property" 
                // in their Id which identifies mouse x and y coordinates
                if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                {
                    controlId = ctl.Substring(0, ctl.Length - 2);
                    foundControl = page.FindControl(controlId);
                }
                else
                {
                    foundControl = page.FindControl(ctl);
                }

                if (!(foundControl is Button || foundControl is ImageButton)) continue;

                control = foundControl;
                break;
            }
        }

        return control == null ? String.Empty : control.ID;
    }

     public System.Data.DataTable sdatatable(string query)
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
        catch { }
        cn.Close();
        cn.Dispose();
    }
    finally
    {
        cn.Close();
        sda.Dispose();
        cn.Dispose();
    }
    return dt;
}
}