using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RCM
{
    public partial class LocatorOne : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        string Strofficer;
        string sql = "";
        string uid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Server.ScriptTimeout = 50000;
            defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            //defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            //string update = "update location set L_date = Convert(varchar(15),Convert(date,getdate()),101) , l_time = Convert(char(8),Convert(time,getdate()),109) where isdate(l_date) <> 1 ;" +
            //                "delete from locationHist where isdate(l_date) <> 1";
            //bucket.ExecuteQuery(update);

            if (!Page.IsPostBack)
            {
                Session["sess_x"] = "";
                ddofficer.DataBind();
                uid = Request.QueryString["userid"].ToString();
                txt_frmDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                ddofficer.Items.Add(new ListItem("ALL", "ALL"));
                ddofficer.SelectedValue = uid;
                lblusername.Text = "Location of " + uid;

                if (ddofficer.SelectedValue == "ALL")
                {
                    sql = "select l.userid,l_long,l_lat, case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                          ", case when rem_battery <=5 then 'Batterylow' else " +
                        "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end as [status] " +
                        ", replace(Convert(varchar(15),Convert(date,l_date),106),' ','-') ,Convert(char(8),Convert(time,l_time,109)) " +
                        " from location l, users u WHERE l.userid=u.userid and CONVERT(date, l_date) = CONVERT(date, getdate(), 101) and l_lat <> '' and l_lat is not null and RcM='" + Session["sess_username"] + "'order by userid , L_time desc";
                    //sql = "SELECT * ,case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                    //    ",case when rem_battery <=5 then 'Batterylow' else "+
                    //    "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end "+
                    //    "FROM(SELECT   locationHist.*, ROW_NUMBER() OVER (PARTITION BY l_date, userid ORDER BY CONVERT(date, l_date) desc) AS RN " +
                    //    "FROM locationHist WHERE CONVERT(date, l_date) = CONVERT(date, getdate(), 101)) AS t " +
                    //    "WHERE RN = 1";
                }
                else
                {
                    sql = "select Top 1 userid,l_long,l_lat, case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                            " , case when rem_battery <=5 then 'Batterylow' else " +
                            "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end as [status] " +
                            ", replace(Convert(varchar(15),Convert(date,l_date),106),' ','-') , Convert(char(8),Convert(time,l_time,109))" +
                            "from location where userid='" + uid + "' and CONVERT(date, l_date) = CONVERT(date, getdate(), 101) and l_lat <> '' and l_lat is not null order by userid , L_time desc";

                    //sql = "SELECT Top 1 * ,case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                    //    ",case when rem_battery <=5 then 'Batterylow' else " +
                    //    "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end " +
                    //    "FROM(SELECT   location.*, ROW_NUMBER() OVER (PARTITION BY l_date, userid ORDER BY CONVERT(date, l_date) desc) AS RN " +
                    //    "FROM location where userid='" + uid + "' and l_date=(select MAX(L_date) from locationHist " +
                    //     "where userid='" + uid + "')) AS t " +
                    //    "WHERE RN = 1";

                }
            }
            else
            {
                uid = ddofficer.SelectedValue;
                lblusername.Text = "Location of " + uid;

                if (ddofficer.SelectedValue == "ALL")
                {
                    //sql = "select top 5 *, case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                    //    ",case when rem_battery <=5 then 'Batterylow' else " +
                    //    "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end " +    
                    //    "from locationHist where l_date='" + txt_frmDate.Text + "'";
                    //Response.Write(txt_frmDate.Text + System.DateTime.Today.ToString("MM'/'dd'/'yyyy"));
                    if (txt_frmDate.Text == System.DateTime.Today.ToString("MM'/'dd'/'yyyy"))
                    {
                        sql = "SELECT userid,l_long,l_lat,case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                            ", case when rem_battery <=5 then 'Batterylow' else " +
                            "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end as [status] " +
                            ", replace(Convert(varchar(15),Convert(date,l_date),106),' ','-') , Convert(char(8),Convert(time,l_time,109))" +
                            "FROM(SELECT   location.*, ROW_NUMBER() OVER (PARTITION BY l_date, userid ORDER BY CONVERT(date, l_date) desc, L_time desc) AS RN " +
                            "FROM location  WHERE  l_date = '" + txt_frmDate.Text + "' and l_lat <> '' and l_lat is not null) AS t " +
                            "WHERE RN = 1 and userid in (select userid from users where RCM = '" + Session["sess_username"] + "') order by userid ";
                    }
                    else
                    {
                        sql = "SELECT userid,l_long,l_lat,case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                            ", case when rem_battery <=5 then 'Batterylow' else " +
                            "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end as [status] " +
                            ", replace(Convert(varchar(15),Convert(date,l_date),106),' ','-') , Convert(char(8),Convert(time,l_time,109))" +
                            "FROM(SELECT   locationHist.*, ROW_NUMBER() OVER (PARTITION BY l_date, userid ORDER BY CONVERT(date, l_date) desc, L_time desc) AS RN " +
                            "FROM locationHist WHERE l_date = '" + txt_frmDate.Text + "' and l_lat <> '' and l_lat is not null) AS t " +
                            "WHERE RN = 1 and userid in (select userid from users where RCM = '" + Session["sess_username"] + "') order by userid";
                    }
                }
                else
                {
                    //sql = "select Top 1  *, case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                    //",case when rem_battery <=5 then 'Batterylow' else " +
                    //" case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end "+
                    //"from locationHist where userid='" + uid + "' and (l_date='" + txt_frmDate.Text + "')";

                    if (txt_frmDate.Text == System.DateTime.Today.ToString("MM'/'dd'/'yyyy"))
                    {
                        sql = "SELECT Top 1 userid,l_long,l_lat, case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                       ",case when rem_battery <=5 then 'Batterylow' else " +
                       "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end as [status] " +
                       ", replace(Convert(varchar(15),Convert(date,l_date),106),' ','-') , Convert(char(8),Convert(time,l_time,109))" +
                       "FROM(SELECT   location.*, ROW_NUMBER() OVER (PARTITION BY l_date, userid ORDER BY CONVERT(date, l_date) desc, L_time desc) AS RN " +
                       "FROM location where userid='" + uid + "' and (l_date='" + txt_frmDate.Text + "') and l_lat <> '' and l_lat is not null) AS t " +
                       "WHERE RN = 1";
                        //sql = "select Top 1  userid,l_long,l_lat, case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                        //",case when rem_battery <=5 then 'Batterylow' else " +
                        //" case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end " +
                        //", replace(Convert(varchar(15),Convert(date,l_date),106),' ','-') " +
                        //"from location where userid='" + uid + "' and (l_date='" + txt_frmDate.Text + "')";
                    }
                    else
                    {
                        sql = "SELECT Top 1 userid,l_long,l_lat, case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1 " +
                       ",case when rem_battery <=5 then 'Batterylow' else " +
                       "case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l_date,'/','-') + ' ' + l_time) < -60 then 'Offline' else 'Online' end end as [status] " +
                       ", replace(Convert(varchar(15),Convert(date,l_date),106),' ','-') , Convert(char(8),Convert(time,l_time,109))" +
                       "FROM(SELECT   locationHist.*, ROW_NUMBER() OVER (PARTITION BY l_date, userid ORDER BY CONVERT(date, l_date) desc, L_time desc) AS RN " +
                       "FROM locationHist where userid='" + uid + "' and (l_date='" + txt_frmDate.Text + "') and l_lat <> '' and l_lat is not null) AS t " +
                       "WHERE RN = 1";
                    }

                }
            }
            //Response.Write(sql);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sql);
            dt = bucket.GetData(cmd);

            string x = "";
            string markers = "";
            string zoomstr = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if (dt.Rows[i][4].ToString() == "Offline")
                    //{
                    //    markers += "var r = GetResults(" + dt.Rows[i][2].ToString() + ", " + dt.Rows[i][1].ToString() + ");" + "\n" +
                    //    "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                    //    "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                    //    "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : '+ r.features[0].properties.label +'<br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                    //        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                    //    "{ externalGraphic: 'Image/man-red.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                    //    "vectorLayer.addFeatures(feature);" + "\n";
                    //}
                    //else if (dt.Rows[i][4].ToString() == "Batterylow")
                    //{
                    //    markers += "var r = GetResults(" + dt.Rows[i][2].ToString() + ", " + dt.Rows[i][1].ToString() + ");" + "\n" +
                    //    "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                    //    "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                    //    "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : '+ r.features[0].properties.label +'<br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                    //        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                    //    "{ externalGraphic: 'Image/man-orange.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                    //    "vectorLayer.addFeatures(feature);" + "\n";
                    //}
                    //else
                    //{
                    //    markers += "var r = GetResults(" + dt.Rows[i][2].ToString() + ", " + dt.Rows[i][1].ToString() + ");" + "\n" +
                    //    "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                    //    "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                    //    "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : '+ r.features[0].properties.label +'<br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                    //        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                    //    "{ externalGraphic: 'Image/man-green.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                    //    "vectorLayer.addFeatures(feature);" + "\n";
                    //}

                    if (dt.Rows[i][4].ToString() == "Offline")
                    {
                        markers += "var r = GetResults(" + dt.Rows[i][2].ToString() + ", " + dt.Rows[i][1].ToString() + ");" + "\n" +
                        "try {" +
                        "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                        "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                        "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : '+ r.features[0].properties.label +'<br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                        "{ externalGraphic: 'Image/man-red.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                        "vectorLayer.addFeatures(feature);" + "\n" +
                        "} catch(ee){" +
                         "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                        "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                        "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : NA <br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                        "{ externalGraphic: 'Image/man-red.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                        "vectorLayer.addFeatures(feature);}" + "\n";
                    }
                    else if (dt.Rows[i][4].ToString() == "Batterylow")
                    {
                        markers += "var r = GetResults(" + dt.Rows[i][2].ToString() + ", " + dt.Rows[i][1].ToString() + ");" + "\n" +
                        "try {" +
                        "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                        "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                        "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : '+ r.features[0].properties.label +'<br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                        "{ externalGraphic: 'Image/man-orange.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                        "vectorLayer.addFeatures(feature);" + "\n" +
                        "} catch(ee){" +
                         "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                        "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                        "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : NA <br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                        "{ externalGraphic: 'Image/man-orange.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                        "vectorLayer.addFeatures(feature);}" + "\n";
                    }
                    else
                    {
                        markers += "var r = GetResults(" + dt.Rows[i][2].ToString() + ", " + dt.Rows[i][1].ToString() + ");" + "\n" +
                        "try {" +
                        "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                        "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                        "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : '+ r.features[0].properties.label +'<br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                        "{ externalGraphic: 'Image/man-green.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                        "vectorLayer.addFeatures(feature);" + "\n" +
                        "} catch(ee){" +
                         "var feature = new OpenLayers.Feature.Vector(" + "\n" +
                        "new OpenLayers.Geometry.Point(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo)," +
                        "{ description: 'UserID : " + dt.Rows[i][0].ToString() + " <br/> Location : NA <br/>Battery : " + dt.Rows[i][3].ToString() + "%<br/>Date : " + dt.Rows[i][5].ToString() + " " + dt.Rows[i][6].ToString() + "'}," +
                        //"{ description: 'Location : ' + '" + bucket.filterText(dt.Rows[i][5].ToString().TrimStart(',')) + "' }," +
                        "{ externalGraphic: 'Image/man-green.png', graphicHeight: 35, graphicWidth: 35, graphicXOffset: -12, graphicYOffset: -25 });" + "\n" +
                        "vectorLayer.addFeatures(feature);}" + "\n";
                    }
                    if (ddofficer.SelectedValue == "ALL")
                    {
                        zoomstr = "var lonLat = new OpenLayers.LonLat(77.2658031,23.1996633).transform(epsg4326, projectTo);" + "\n" +
                                    "var zoom = 4;" + "\n";
                    }
                    else
                    {
                        zoomstr = "var lonLat = new OpenLayers.LonLat(" + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][2].ToString() + ").transform(epsg4326, projectTo);" + "\n" +
                                    "var zoom = 17;" + "\n";
                    }
                }
                Label1.Visible = false;
            }
            else
            {
                Label1.Visible = true;
            }
            x = @" <script src=" + Convert.ToChar(34) + "http://openlayers.org/api/OpenLayers.js" + Convert.ToChar(34) + "></script> <script src=" + Convert.ToChar(34) + "https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js" + Convert.ToChar(34) + "></script>" +
              "<script>" + "\n" +
            "function GetResults(lat, long) {" + "\n" +
               "var jsonObjectInstance = $.parseJSON($.ajax({" + "\n" +//Transovative && Transo@123 mapzen // transovativesup2506@gmail.com && Trans123
                  " url: 'https://search.mapzen.com/v1/reverse?api_key=search-BST68nm&point.lat='+lat+'&point.lon='+long+'&size=1'," + "\n" +
                   " async: false," + "\n" +
                    "dataType: 'json'" + "\n" +
                "}).responseText); return jsonObjectInstance" + "\n" +
            "}" + "\n" +

            "map = new OpenLayers.Map(" + Convert.ToChar(34) + "basicMap" + Convert.ToChar(34) + ");" + "\n" +
            "map.addLayer(new OpenLayers.Layer.OSM());" + "\n" +

            "epsg4326 = new OpenLayers.Projection(" + Convert.ToChar(34) + "EPSG:4326" + Convert.ToChar(34) + ");" + "\n" + //WGS 1984 projection
            "projectTo = map.getProjectionObject(); " + "\n" +//The map projection (Spherical Mercator)

            //"var lonLat = new OpenLayers.LonLat(77.2658031,23.1996633).transform(epsg4326, projectTo);" + "\n" +
            //"var zoom = 4;" + "\n" +
            zoomstr +
            "map.setCenter(lonLat, zoom);" + "\n" +

            "var vectorLayer = new OpenLayers.Layer.Vector(" + Convert.ToChar(34) + "Overlay" + Convert.ToChar(34) + ");" + "\n" +

            markers +

            "map.addLayer(vectorLayer);" + "\n" +
            //Add a selector control to the vectorLayer with popup functions
            "var controls = {" + "\n" +
                "selector: new OpenLayers.Control.SelectFeature(vectorLayer, { onSelect: createPopup, onUnselect: destroyPopup })" + "\n" +
            "};" + "\n" +

            "function createPopup(feature) {" + "\n" +
                "feature.popup = new OpenLayers.Popup.FramedCloud(" + Convert.ToChar(34) + "pop" + Convert.ToChar(34) + "," + "\n" +
                    "feature.geometry.getBounds().getCenterLonLat()," + "\n" +
                    "null," + "\n" +
                    "'<div class=" + Convert.ToChar(34) + "markerContent" + Convert.ToChar(34) + ">' + feature.attributes.description + '</div>'," + "\n" +
                    "null," + "\n" +
                    "true," + "\n" +
                    "function () { controls['selector'].unselectAll(); }" + "\n" +
                ");" + "\n" +
                //feature.popup.closeOnMove = true;
                "map.addPopup(feature.popup);" + "\n" +
            "}" + "\n" +

            "function destroyPopup(feature) {" + "\n" +
                "feature.popup.destroy();" + "\n" +
                "feature.popup = null;" + "\n" +
            "}" + "\n" +

            "map.addControl(controls['selector']);" + "\n" +
            "controls['selector'].activate();" + "\n" +

        "</script>";

            Session["sess_x"] = x;
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {

        }

    }

}