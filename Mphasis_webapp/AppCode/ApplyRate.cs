using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

public class ApplyRate
{
    CommonClass obj = new CommonClass();

    public void getsitedetails()
    {
        string upd = @"Update Parking_SubMaster set ptime=vtime,pdate=vdate,siteid=p.siteid from Parking_Master p join Parking_SubMaster s on p.vid=s.vid
                    where s.siteid is null;If Exists (Select regno from Parking_SubMaster where charge is null and CONVERT(date,pdate)=CONVERT(date,getdate())) 
                    If Exists (Select regno from Parking_SubMaster where charge is null and CONVERT(date,pdate)=CONVERT(date,getdate())
                    group by regno,siteid having COUNT(regno)=1) Update Parking_SubMaster set charge=rate,hour=r.hour
                    from Parking_SubMaster p join RateCard r on p.siteid=r.siteid and p.vehicletype=r.vehicletype and r.hour=1
                    where charge is null and CONVERT(date,pdate)=CONVERT(date,getdate()) and regno in (Select regno from Parking_SubMaster where charge is null and 
                    CONVERT(date,pdate)=CONVERT(date,getdate()) group by regno,siteid having COUNT(regno)=1)";
        obj.NonExecuteQuery(upd);

        string updcharge = "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        SqlCommand cnCommand = cn.CreateCommand();
        SqlDataReader reader = default(SqlDataReader);
        try
        {
            cnCommand.CommandText = @";with t1 as (SELECT [regno],[pdate],[ptime],siteid FROM (SELECT Parking_SubMaster.*, ROW_NUMBER() OVER (PARTITION BY regno ORDER BY ptime asc)
                                    AS RN FROM Parking_SubMaster WHERE regno in (Select regno from Parking_SubMaster where CONVERT(date,pdate)=CONVERT(date,getdate())
                                    group by regno,siteid having COUNT(regno)>1)) AS t WHERE RN = 1), 
                                    t2 as (SELECT [regno],[pdate],[ptime],siteid FROM (SELECT Parking_SubMaster.*, ROW_NUMBER() OVER (PARTITION BY regno ORDER BY ptime desc)
                                    AS RN FROM Parking_SubMaster WHERE regno in (Select regno from Parking_SubMaster where CONVERT(date,pdate)=CONVERT(date,getdate())
                                    group by regno,siteid having COUNT(regno)>1)) AS t WHERE RN = 1)

                                    select t1.regno,DATEDIFF(hh,t1.ptime,t2.ptime),t1.siteid,t2.ptime
                                    from t1 full outer join t2 on t1.regno = t2.regno";
            cn.Open();
            reader = cnCommand.ExecuteReader();

            while (reader.Read())
            {
                updcharge = @"Update Parking_SubMaster set hour=" + reader[1] + ",charge=rate from Parking_SubMaster p join RateCard r on p.siteid=r.siteid and " +
                            "p.vehicletype=r.vehicletype and r.hour=" + reader[1] + " where CONVERT(date,pdate)=CONVERT(date,getdate()) and ptime='" +
                            reader[3].ToString() + "' and regno='" + reader[0].ToString() + "' and p.siteid='" + reader[2].ToString() +
                            "';Update Parking_SubMaster set hour=0,charge=0 where regno='" + reader[0].ToString() + 
                            "' and CONVERT(date,pdate)=CONVERT(date,getdate()) and siteid='" + reader[2].ToString() + "' and ptime <>'" + reader[3].ToString() + "'";
                obj.NonExecuteQuery(updcharge);
            }
            reader.Close();
            cn.Close();
        }

        catch (Exception ee)
        {
            
        }

        finally
        {
            reader.Close();
            cn.Close();
            cn.Dispose();
        }
    }

    public void calctot()
    {
        string updcharge = "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
        SqlCommand cnCommand = cn.CreateCommand();
        SqlDataReader reader = default(SqlDataReader);
        try
        {
            cnCommand.CommandText = @"Select vid,sum(charge) from Parking_SubMaster where CONVERT(date,[pdate])=CONVERT(date,getdate()) group by vid";
            cn.Open();
            reader = cnCommand.ExecuteReader();

            while (reader.Read())
            {
                updcharge = @"Update Parking_Master set totamt=" + reader[1] + " where vid='" + reader[0].ToString() + "'";
                obj.NonExecuteQuery(updcharge);
            }
            reader.Close();
            cn.Close();
        }

        catch (Exception ee)
        {

        }

        finally
        {
            reader.Close();
            cn.Close();
            cn.Dispose();
        }
    }
}