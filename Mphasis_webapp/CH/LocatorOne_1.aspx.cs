using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.CH
{
    public partial class LocatorOne_1 : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        string rows;
        string mapwithanimation = "";
        string uid = "";
        CommonClass obj = new CommonClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Server.ScriptTimeout = 50000;
            // defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            //defaultCalendarExtender.StartDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(-(Convert.ToDouble((DateTime.Now.Date.Day)))).ToString("MM/dd/yyyy"));
            // defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            obj.NonExecuteQuery("Update locationhist set l_date=convert(date,locdoa),locupdated='YES' where isdate(l_date)<>1");

            if (!Page.IsPostBack)
            {
                if (Session["sess_role"].ToString().ToUpper().Trim() == "ADMIN")
                {
                    SqlDataSource1.SelectCommand = "SELECT [userid],[username] FROM [users] where userid<>'admin' and status <> 'DEL' order by userid";
                }
                //else if (Session["sess_role"].ToString().ToUpper().Trim() == "CM")
                //{
                //    SqlDataSource1.SelectCommand = "SELECT [userid],[username] FROM [users] where userid<>'admin' and status <> 'DEL' and role in ('CE') and cm like '" + Session["sess_username"] + "'  order by userid";
                //}
                //else if (Session["sess_role"].ToString().ToUpper().Trim() == "RM")
                //{
                //    SqlDataSource1.SelectCommand = "SELECT [userid],[username] FROM [users] where userid<>'admin' and status <> 'DEL' and role in ('CE,CM') and rm like '" + Session["sess_username"] + "' order by userid";
                //}
                Session["sess_x"] = ""; ddofficer.DataBind();
                uid = Request.QueryString["userid"].ToString();
                txt_frmDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                ddFromTime.SelectedValue = "00:00";
                ddToTime.SelectedValue = "23:59";
                ddofficer.DataBind();
                ddofficer.SelectedValue = uid;

                //lblusername.Text = "Location of " + ddofficer.SelectedItem.Text;
                // drawmap();
                drawosmmap();
            }
            else
            {
                uid = ddofficer.SelectedValue;
                lblusername.Text = "Location of " + ddofficer.SelectedItem.Text;
            }

            #region addheading
            DataTable dt = new DataTable();
            dt = obj.sdatatable(@"select Convert(varchar(11),Convert(date,l_date),106) +' ' + l_time as [locdate],username
                                    from users u join location l on u.userid=l.userid where u.userid='" + uid + "'");

            if (dt.Rows.Count > 0)
            {
                try
                {
                    lblusername.Text = "Location of " + dt.Rows[0][1].ToString();
                    if (dt.Rows[0][0].ToString() != "")
                        lblDateTime.Text = "Location Last Updated On : " + dt.Rows[0][0].ToString();
                    else
                        lblDateTime.Text = "Last Location Not Available";
                }
                catch
                {
                    lblDateTime.Text = "Last Location Not Available";
                }
            }
            else
            {
                lblDateTime.Text = "Last Location Not Available";
            }
            #endregion
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            // drawmap();
            drawosmmap();
        }

        public void drawmap()
        {
            mapwithanimation = "";
            try
            {
                DataTable dt = new DataTable();
                ibuckethead bucket = new ibuckethead();
                SqlCommand cmd = new SqlCommand(@"SELECT STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT
						'{ lat: '  + l_lat + ', lng:'+ l_long+'},' as[Data],l_time
						from [location30days]
						 where l_date ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						 and CONVERT(time, replace(l_time,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'						 
                            ) alldata order by CONVERT(time, replace(l_time,' ','')) asc
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            ),
 STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT top 1
						' User ID : ' +userid +'<br/>Engineer Name : '+ username +'<br/> Mobile No : '+ contactno   as[Data],l_time
						from [location30days]
						 where l_date ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						 and CONVERT(time, replace(l_time,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'						 
                            ) alldata order by CONVERT(time, replace(l_time,' ','')) asc
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            ),
 STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT top 1
						'[' + l_lat + ','+ l_long+'],' as[Data],l_time
						from [location30days]
						 where l_date ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						 and CONVERT(time, replace(l_time,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'		order by CONVERT(time, replace(l_time,' ','')) asc	 
                            ) alldata 
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            ),
STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT top 1
						'[' + l_lat + ','+ l_long+'],' as[Data],l_time
						from [location30days]
						where l_date ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						 and CONVERT(time, replace(l_time,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'		order by CONVERT(time, replace(l_time,' ','')) desc				 
                            ) alldata 
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            ),

STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT top 1000
                        '[' + lat + ','+ lon+ ', '+CHAR(39)+' PM Date Time : '+Convert(varchar(10),Convert(date,vdate),103)+' '+vtime+'<br/>ATMID : '+ATMID+ CHAR(39)+'],' as[Data],vtime,vdate,ATMID
						from DR_CTP
						where vdate ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						and CONVERT(time, replace(vtime,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'		
						 order by CONVERT(time, replace(vtime,' ','')) asc				 
                            ) alldata 
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            )");

                //     Response.Write(cmd.CommandText);
                cmd.CommandTimeout = 999999;
                dt = bucket.GetData(cmd);


                string usercoor = "";
                string lat = "";
                string lng = "";
                string path = "";
                string uadd = "";
                string audits = "";
                if (dt.Rows.Count > 0)
                {
                    path += dt.Rows[0][0].ToString();

                    string[] parts = dt.Rows[0][2].ToString().Split(',');
                    lat = parts[0].Replace('[', ' ');
                    lng = parts[1].Replace(']', ' ');
                    usercoor += dt.Rows[0][2].ToString() + dt.Rows[0][3].ToString();
                    uadd = dt.Rows[0][1].ToString();

                    try
                    {
                        audits += dt.Rows[0][4].ToString();
                    }
                    catch (Exception ex) { }
                }

                mapwithanimation = @"<script>
try{ 
function initMap() {          
            var userCoor = [ " + usercoor.TrimEnd(',') + @"];
            var auditCoor = [" + audits.TrimEnd(',') + @"]
            var uaddress = '" + uadd + @"';
            var map = new google.maps.Map(document.getElementById('map'), {

                 center: { lat: " + lat + @" ,lng: " + lng + @"},
                zoom: 10,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            // Define the symbol, using one of the predefined paths ('CIRCLE')
            // supplied by the Google Maps JavaScript API.
            var a= 0;
           
           
            
                var lineSymbol = {
                path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW,
                scale: 2,
                strokeColor: '#0000FF'
                };
            
            // Create the polyline and add the symbol to it via the 'icons' property.
            var line = new google.maps.Polyline({
                path: [ " + path.TrimEnd(',') + @" ],
               // path: [ { lat: 18.45202353, lng: 73.81140579 }, { lat: 23.0921453, lng: 72.5901526 }],
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],
                strokeColor: '#FF0000',
                map: map
            });

            
            
            var infowindow = new google.maps.InfoWindow({
                //content: contentString
            });

            for (i = 0; i < userCoor.length; i++) {
            if(i==0){
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(userCoor[i][0], userCoor[i][1]),
                map: map,
                
                title: '',
                icon: new google.maps.MarkerImage('http://maps.google.com/mapfiles/ms/icons/green-dot.png')
            });
            }
            else{
             var marker = new google.maps.Marker({
                position: new google.maps.LatLng(userCoor[i][0], userCoor[i][1]),
                map: map,
                title: '',
                icon: new google.maps.MarkerImage('http://maps.google.com/mapfiles/ms/icons/red-dot.png')
            });
           }           
            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                                       
                    var latlng = new google.maps.LatLng(userCoor[i][0],userCoor[i][1]);
                    var geocoder = geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                     if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) { 
                        var contentString = ' ';  
                        contentString = uaddress +'<p> Address : ' + results[1].formatted_address +'</p>';// results[1].formatted_address;
                        infowindow.setContent(contentString);//userCoor[i][3]
                        infowindow.open(map, marker);
                            }
                        }   
                     });                   
                }
            })(marker, i));

        }

        for (i = 0; i < auditCoor.length; i++) {
if(auditCoor.length != 0){
            
            var marker1 = new google.maps.Marker({
                position: new google.maps.LatLng(auditCoor[i][0], auditCoor[i][1]),
                map: map,
                title: '',
                icon: new google.maps.MarkerImage('http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld='+(i+1)+'|8E67FD|FFFFFF')
                });
            }
            google.maps.event.addListener(marker1, 'click', (function (marker, i) {
                return function () {
                                       
                    var latlng = new google.maps.LatLng(auditCoor[i][0],auditCoor[i][1]);
                    var geocoder = geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                     if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) { 
                        var contentString = ' ';  
                        contentString = uaddress + '<br/>' + auditCoor[i][2] + '<p> Address : ' + results[1].formatted_address +'</p>';// results[1].formatted_address;
                        infowindow.setContent(contentString);//userCoor[i][3]
                        infowindow.open(map, marker);
                            }
                        }   
                     });                   
                }
            })(marker1, i));
}

            animateCircle(line);
        }

        // Use the DOM setInterval() function to change the offset of the symbol
        // at fixed intervals.
        function animateCircle(line) {
            var count = 0;
            window.setInterval(function () {
                count = (count + 1) % 200;

                var icons = line.get('icons');
                icons[0].offset = (count / 2) + '%';
                line.set('icons', icons);
            }, 200);
        }
}

catch(ee)
{
    alert(ee);
}
</script>";
            }
            catch (Exception)
            {
                mapwithanimation = "No details found..";
            }

            Session["sess_x"] = mapwithanimation;
        }

        public void drawosmmap()
        {
            mapwithanimation = "";
            try
            {
                DataTable dt = new DataTable();
                ibuckethead bucket = new ibuckethead();

                SqlCommand cmd = new SqlCommand(@"SELECT STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT
						'ol.proj.transform(['  + l_long + ','+ l_lat+'], &apos;EPSG:4326&apos;,&apos;EPSG:3857&apos;),' as[Data],l_time
						from [location30days]
						 where l_date ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						 and CONVERT(time, replace(l_time,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'						 
                            ) alldata order by CONVERT(time, replace(l_time,' ','')) asc
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            ),
 STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT top 1
						' User ID : ' +userid +'<br/>User Name : '+ username +'<br/> Mobile No : '+ mobile   as[Data],l_time
						from [location30days]
						 where l_date ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						 and CONVERT(time, replace(l_time,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'						 
                            ) alldata order by CONVERT(time, replace(l_time,' ','')) asc
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            ),
 STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT top 1
						'ol.proj.transform(['  + l_long + ','+ l_lat+'], &apos;EPSG:4326&apos;,&apos;EPSG:3857&apos;),' as[Data],l_time
						from [location30days]
						 where l_date ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						 and CONVERT(time, replace(l_time,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'		order by CONVERT(time, replace(l_time,' ','')) asc	 
                            ) alldata 
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            ),
STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (						
						
						SELECT top 1
						'ol.proj.transform(['  + l_long + ','+ l_lat+'], &apos;EPSG:4326&apos;,&apos;EPSG:3857&apos;),' as[Data],l_time
						from [location30days]
						where l_date ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						 and CONVERT(time, replace(l_time,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'		order by CONVERT(time, replace(l_time,' ','')) desc				 
                            ) alldata 
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            ),
STUFF(
                 (
                      SELECT  CAST([Data] AS XML)
                      FROM
                      (										
						SELECT top 1000
                        '[' + lat + ','+ lon+ ', '+CHAR(39)+'PM Date Time : '+Convert(varchar(10),Convert(date,vdate),103)+' '+vtime+'<br/>ATMID : '+ATMID+ CHAR(39)+']|' as[Data],vtime,vdate,ATMID
						from DR_CTP
						where vdate ='" + txt_frmDate.Text + @"' and userid = '" + uid + @"'
						and CONVERT(time, replace(vtime,' ','')) between '" + ddFromTime.SelectedValue + "' and '" + ddToTime.SelectedValue + @"'		
						 order by CONVERT(time, replace(vtime,' ','')) asc				 
                            ) alldata 
                        FOR XML PATH('')
                    )
                ,   1
                ,   0
                ,   ''
            )");
                //      Response.Write(cmd.CommandText); 
                cmd.CommandTimeout = 999999;
                dt = bucket.GetData(cmd);

                string dailyvisit = "";
                DataTable dtcheckin = new DataTable();
                // dtcheckin = bucket.BindoboutGrid(dailyvisit, out rows);

                string path = "";
                string uadd = "";
                string uaddlast = "";

                string incoords = "";
                string outcoords = "";
                string daudits = ""; string auditcoords = "";
                if (dt.Rows.Count > 0)
                {
                    path += dt.Rows[0][0].ToString();

                    string[] inparts = dt.Rows[0][2].ToString().Split(',');
                    incoords = inparts[0].Replace('[', ' ') + inparts[1].Replace(']', ' ');
                    incoords = dt.Rows[0][2].ToString();
                    outcoords = dt.Rows[0][3].ToString();
                    uadd = dt.Rows[0][1].ToString();
                    uaddlast = dt.Rows[0][1].ToString();

                    // daudits = dt.Rows[0][4].ToString();
                    //auditcoords += "ol.proj.transform([" + dtcheckin.Rows[i][4].ToString() + ", " + dtcheckin.Rows[i][3].ToString() + "], 'EPSG:4326','EPSG:3857'),";//"[" + dt.Rows[i][3].ToString() + ", " + dt.Rows[i][4].ToString() + "],";

                    try
                    {
                        string[] inparts1 = new string[] { };
                        //string[] inparts1 = dt.Rows[0][4].ToString().Split(',');
                        string[] inparts2 = dt.Rows[0][4].ToString().Split('|');

                        for (int i = 0; i < inparts2.Length; i++)
                        {
                            inparts1 = inparts2[i].Split(',');

                            auditcoords += "ol.proj.transform([" + inparts1[1].Replace('[', ' ') + ", " + inparts1[0].Replace('[', ' ') + "], 'EPSG:4326','EPSG:3857'),";//"[" + dt.Rows[i][3].ToString() + ", " + dt.Rows[i][4].ToString() + "],";

                            //daudits += inparts1[2].Replace(']', ' ');

                            daudits += "[" + inparts1[2] + ",";
                        }
                    }
                    catch (Exception ex) { }
                }

                mapwithanimation = @"<script>
        try{ 
       setTimeout(function(){ 
        debugger;
            var
                sourceFeatures = new ol.source.Vector(),
                layerFeatures = new ol.layer.Vector({
                    source: sourceFeatures
                });

            var container = document.getElementById('popup');
            var content = document.getElementById('popup-content');
            var closer = document.getElementById('popup-closer');

            var overlay = new ol.Overlay(/** @type {olx.OverlayOptions} */({
                element: container,
                autoPan: true,
                autoPanAnimation: {
                    duration: 250
                }
            }));

            closer.onclick = function () {
                document.getElementById('popup-content').innerHTML = '';
                overlay.setPosition(undefined);
                closer.blur();
                return false;
            };

            var lineString = new ol.geom.LineString([]);

            var layerRoute = new ol.layer.Vector({
                source: new ol.source.Vector({
                    features: [
                        new ol.Feature({ geometry: lineString })
                    ]
                }),
                style: [
                    new ol.style.Style({
                        stroke: new ol.style.Stroke({
                            width: 3, color: 'rgba(255, 0, 0, 1)'
                            //lineDash: [.1, 5]
                        }),
                        zIndex: 2
                    })
                ],
                updateWhileAnimating: true
            });

            var loc = [" + incoords + @"]; 
            var map = new ol.Map({
                target: 'map',
                view: new ol.View({
                    center: loc[0],
                    zoom: 4,
                    minZoom: 2,
                    maxZoom: 20
                }),
                layers: [
                    new ol.layer.Tile({
                        source: new ol.source.OSM(),
                        opacity: 0.6
                    }),
                    layerRoute, layerFeatures
                ],
                overlays: [overlay]
            });
            
            var iconFeatures = [];
            
                var view = map.getView();
                var extent = ol.extent.boundingExtent([" + path.TrimEnd(',') + @"]);
                var size = map.getSize();
                view.fit(extent, size);
                if (view.getZoom() > 16) {
                    view.setZoom(9);
                }            

            var CheckInloc=[" + incoords.TrimEnd(',') + @"]; 
           
            var CheckIniconFeature = new ol.Feature({
                geometry: new ol.geom.Point(CheckInloc[0]),//ol.proj.transform([-72.0704, 46.678], 'EPSG:4326','EPSG:3857')),
                name:  '" + uadd + @"',
                type:'user',
                population: 4000,
                rainfall: 500
            });
            CheckIniconFeature.setStyle(new ol.style.Style({
                image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
                    //color: '#8959A8',
                    //crossOrigin: 'anonymous',
                    anchor: [0.5, 30],
                    anchorXUnits: 'fraction',
                    anchorYUnits: 'pixels',
                    src: 'http://maps.google.com/mapfiles/ms/icons/green-dot.png'
                }))
            }));
            iconFeatures.push(CheckIniconFeature);
            
            var CheckOutloc=[" + outcoords.TrimEnd(',') + @"]; 
            var CheckouticonFeature = new ol.Feature({
                geometry: new ol.geom.Point(CheckOutloc[0]),//ol.proj.transform([-72.0704, 46.678], 'EPSG:4326','EPSG:3857')),
                name: '" + uaddlast + @"',
                type:'user',
                population: 4000,
                rainfall: 500
            });
            CheckouticonFeature.setStyle(new ol.style.Style({
                image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
                    //color: '#8959A8',
                    //crossOrigin: 'anonymous',
                    anchor: [0.5, 30],
                    anchorXUnits: 'fraction',
                    anchorYUnits: 'pixels',
                    src: 'http://maps.google.com/mapfiles/ms/icons/red-dot.png'
                }))
            }));
            iconFeatures.push(CheckouticonFeature);            
                     
            var auditCoor = [ " + daudits.TrimEnd(',') + @"];
           
            var j=1;
            var loc1 = [" + auditcoords.TrimEnd(',') + @"]; 

          for (i = 0; i < auditCoor.length; i++) {
            debugger;
            
            var iconFeature = new ol.Feature({
                geometry: new ol.geom.Point(loc1[i]),//ol.proj.transform([-72.0704, 46.678], 'EPSG:4326','EPSG:3857')),
                name: auditCoor[i],
                type:'audit',
                population: 4000,
                rainfall: 500
            });

            iconFeature.setStyle(new ol.style.Style({
                            image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
                                //color: '#8959A8',
                                //crossOrigin: 'anonymous',
                                anchor: [0.5, 30],
                                anchorXUnits: 'fraction',
                                anchorYUnits: 'pixels',
                                src: 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld='+(j)+'|8E67FD|FFFFFF'
                            }))
                        }));
            iconFeatures.push(iconFeature);
            j++;           
            }

            var vectorSource = new ol.source.Vector({
                features: iconFeatures //add an array of features
            });

            var iconStyle = new ol.style.Style({
                image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
                    anchor: [0.5, 46],
                    anchorXUnits: 'fraction',
                    anchorYUnits: 'pixels',
                    opacity: 0.75,
                    src: 'http://openlayers.org/en/v3.9.0/examples/data/icon.png',
                }))
            });

            var vectorLayer = new ol.layer.Vector({
                source: vectorSource,
                //style: iconStyle
            });

            map.addOverlay(vectorLayer);

            var markerEl = document.getElementById('geo-marker');
            var marker = new ol.Overlay({
                positioning: 'center-center',
                offset: [5, 0],
                element: markerEl,
                stopEvent: false
            });

            var element = document.getElementById('popupinfo');
            var popup = new ol.Overlay({
                element: element,
                positioning: 'bottom-center',
                stopEvent: false,
                offset: [9, -21]
            });
            map.addOverlay(popup);

            map.on('click', function (evt) {
            debugger;
                var feature = map.forEachFeatureAtPixel(evt.pixel,
                    function (feature) {
                        return feature;
                    });
                if (feature) {
                    var coordinates = feature.getGeometry().getCoordinates();
                    var longlat = ol.proj.transform(coordinates, 'EPSG:3857', 'EPSG:4326');
                    popup.setPosition(coordinates);
                    
                    var sid = GetResults(longlat[1], longlat[0]);
                    //var sid='brijesh';
                    content.innerHTML = '<p>'+ feature.get('name')+ '<br/>'+'Address : '+ sid.display_name+'</p>';
                    
                     overlay.setPosition(coordinates);
                } else { 
                     overlay.setPosition(undefined);
                     closer.blur();
                }

            });

            map.addOverlay(marker);

            var fill = new ol.style.Fill({ color: 'rgba(255,255,255,1)' }),
                stroke = new ol.style.Stroke({ color: 'rgba(0,0,0,1)' }),
                style1 = [
                    new ol.style.Style({
                        image: new ol.style.Circle({
                            radius: 6, fill: fill, stroke: stroke
                        }),
                        zIndex: 4
                    })
                ];

            //a simulated path
            var path = [
                 " + path.TrimEnd(',') + @"
            ];

            var feature_start = new ol.Feature({
                geometry: new ol.geom.Point(path[0])
            }),
                feature_end = new ol.Feature({
                    geometry: new ol.geom.Point(path[path.length - 1])
                });

            feature_start.setStyle(style1);
            feature_end.setStyle(style1);
            sourceFeatures.addFeatures([feature_start, feature_end]);

            lineString.setCoordinates(path);  

            //fire the animation
            map.once('postcompose', function (event) {
                interval = setInterval(animation, 100);
            });

            var i = 0, interval;
            var animation = function () {

                if (i == path.length) {
                    i = 0;
                }

                marker.setPosition(path[i]);
                i++;
            };
        }, 300);
}
catch(ee)
{
    alert(ee);
}
                    
function GetResults(lat, long) {
                var jsonObjectInstance = $.parseJSON($.ajax({
                    url: 'https://locationiq.org/v1/reverse.php?format=json&key=9c73f8c9ed5d47&lat=' + lat + '&lon=' + long,     
                    async: false,
                    dataType: 'json'
                }).responseText); return jsonObjectInstance
            } 

</script>";
            }
            catch (Exception)
            {
                mapwithanimation = "<div style='font-size:x-large;text-align:center;font-family:Cambria, Cochin, Georgia, Times;color:#696969;'>No details found..</div>" + @"<script> var container = document.getElementById('popup');
                var geo = document.getElementById('geo-marker');
                container.style.display = 'none';  geo.style.display = 'none';
                </script>";
            }
            //   map.InnerHtml = mapwithanimation;
            Session["sess_x"] = mapwithanimation;
        }
    }
}