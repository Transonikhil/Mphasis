using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.RM
{
    public partial class LocatorN_Poly_1 : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txt_frmDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");

                if (Session["sess_role"].ToString().ToUpper() == "ADMIN")
                {

                    //ddofficer.Databind();


                }

                createOSMMap();
            }
        }

        #region map commented
        //    public void createMap()
        //    {
        //        var officername = "";
        //        //DropDownList ddoff = this.Master.FindControl("MainContent").FindControl("ddofficer") as DropDownList;



        //        hdnlocation.Value = Request.Form[ddofficer.UniqueID];
        //        if (hdnlocation.Value == null || hdnlocation.Value == "")
        //        {
        //            officername = "%";
        //        }
        //        else
        //        {
        //            officername = hdnlocation.Value;
        //        }




        //        string qry = @"select l.userid,L_lat,L_Long,
        //                            case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1,
        //                            case when l.rem_battery <=5 then 'Batterylow' else
        //                            case when DATEDIFF(MINUTE,replace(l.b_date,'/','-') + ' ' + l.l_time,CONVERT(VARCHAR(19), GETDATE(),120)) > 60 then
        //                            'Offline' else 'Online' end end as [Status],
        //                            l_Date,l_time,
        //                            u.mobile,username 
        //                            from location l 
        //                            left outer join users u 
        //                            on l.userid = u.userid join regionmap r on l.userid=r.userid
        //                            where r.region like '" + ddBranch.SelectedValue + "' and u.userid like '" + officername + @"' and (l_lat<>'0') and (l_long<> '0')	and u.status <> 'DEL'";

        //        qry += @"select top 3 l.userid,L_lat,L_Long,
        //                case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1,
        //                case when l.rem_battery <=5 then 'Batterylow' else
        //                case when DATEDIFF(MINUTE,replace(l.b_date,'/','-') + ' ' + l.l_time,CONVERT(VARCHAR(19), GETDATE(),120)) > 60 then
        //                'Offline' else 'Online' end end as [Status],
        //                l_Date,l_time,
        //                u.mobile,username,(select top 1 checkinimg from punchinout where userid=u.userid order by srno desc) as [img],
        //                (select top 1 DatePart(YYYY, Convert(date,punchintime,106)) from  punchinout where userid=u.userid order by srno desc) as [year],
        //                (select top 1 convert(char(3),punchintime,0) from  punchinout where userid=u.userid order by srno desc) as [mon] 
        //                from location l 
        //                left outer join users u 
        //                on l.userid = u.userid join regionmap r on l.userid=r.userid and status <> 'DEL'";

        //        qry = @"select distinct l.userid,L_lat,L_Long,
        //                case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1,
        //                case when l.rem_battery <=5 then 'Batterylow' else
        //                case when DATEDIFF(MINUTE,replace(l.b_date,'/','-') + ' ' + l.l_time,CONVERT(VARCHAR(19), GETDATE(),120)) > 60 then
        //                'Offline' else 'Online' end end as [Status],
        //                l_Date,l_time,
        //                isnull(u.mobile,'NA'),username, (select top 1 DatePart(YYYY, Convert(date,punchintime,106)) from  punchinout where userid=u.userid order by srno desc) as [year],
        //                (select top 1 convert(char(3),punchintime,0) from  punchinout where userid=u.userid order by srno desc) as [mon],              
        //                (select top 1 checkinimg from punchinout where userid=u.userid order by srno desc) as [img]               
        //                from location l 
        //                inner join users u 
        //                on l.userid = u.userid join regionmap r on l.userid=r.userid
        //                where r.region like '" + ddBranch.SelectedValue + @"' and u.userid like '%" + officername + @"%' and (l_lat<>'0') and ( l_long<> '0')  and l_lat is not null and l_long is not null	and u.status <> 'DEL'
        //                union all
        //                select distinct u.unitid,unitlat,unitlong,u.unitname,case when type='CMS' then 'cms' else 'noncms' end as type, location,region,'','','','',''
        //                from atms u 
        //				where unitlat is not null and unitlat not like '' and unitlat <> '0.0' and unitlat <> '0' and unitlat <> 'NULL' and u.region like '" + ddBranch.SelectedValue + @"' and u.status <> 'Inactive'";
        //        //Response.Write(qry);
        //        SqlCommand cmd = new SqlCommand(qry);
        //        DataTable dt = bucket.GetData(cmd);

        //        string userpropic = "";
        //        string markers = "";
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            //if (File.Exists(@"C:\Development\CommonStorage\Boparias\TransientStorage\" + dt.Rows[i][9].ToString() + "\\" + dt.Rows[i][10].ToString() + "\\" + dt.Rows[i][11].ToString()) == true)
        //            //if(File.Exists(@"C:\Development\CommonStorage\Boparias\TransientStorage\2017\Nov\"+dt.Rows[i][11].ToString()))

        //            if (File.Exists(@"C:\Development\CommonStorage\Boparias\TransientStorage\Profilepics\\" + dt.Rows[i][0].ToString() + ".jpg") == true)
        //            {
        //                userpropic = "https://s7.transovative.com//Boparias_uploader/TransientStorage/Profilepics/" + dt.Rows[i][0].ToString() + ".jpg";
        //            }
        //            else if (File.Exists(@"C:\Development\CommonStorage\Boparias\TransientStorage\" + dt.Rows[i][9].ToString() + "\\" + dt.Rows[i][10].ToString() + "\\" + dt.Rows[i][11].ToString()) == true)
        //            {
        //                userpropic = "https://s7.transovative.com//Boparias_uploader/TransientStorage/" + dt.Rows[i][9].ToString() + "/" + dt.Rows[i][10].ToString() + "/" + dt.Rows[i][11].ToString();
        //            }
        //            else
        //            {
        //                userpropic = "https://s7.transovative.com//Boparias_uploader/TransientStorage/ProfilePics/NAImage.jpg";

        //            }


        //            //  markers += "{ " + "\"title\"" + ": '" + dt.Rows[i][0].ToString().Trim().Replace("'", "") + "' ," + "\"lat\"" + ": '" + dt.Rows[i][1].ToString().Trim() + "' ," + "\"lng\"" + ": '" + dt.Rows[i][2].ToString().Trim() + "' ," + "\"description\"" + ": 'Name : " + dt.Rows[i][8] + " <br/> Mobile No : " + dt.Rows[i][7] + " <br/> Date Time : " + dt.Rows[i][5].ToString().Trim().Replace("'", "") + " - " + dt.Rows[i][6].ToString().Trim().Replace("'", "") + " <br/> Status : " + dt.Rows[i][4] + " <br/> Battery Remaining : " + dt.Rows[i][3] + "'  ," + "\"type\"" + ": '" + dt.Rows[i][4] + "'," + "\"unitdescp\"" + ": 'Unit Name : " + dt.Rows[i][3].ToString().Trim().Replace("'", "") + " <br/> Location : " + dt.Rows[i][5].ToString().Trim().Replace("'", "") + " <br/> Region : " + dt.Rows[i][6] + " <br/> Site Type : " +dt.Rows[i][4].ToString().ToUpper().Replace("NONCMS","NON-CMS")+"'},";
        //            markers += "{ " + "\"title\"" + ": '" + dt.Rows[i][0].ToString().Trim().Replace("'", "") + "' ," + "\"lat\"" + ": '" + dt.Rows[i][1].ToString().Trim() + "' ," + "\"lng\"" + ": '" + dt.Rows[i][2].ToString().Trim() + "' ," + "\"description\"" + ": 'Name : " + dt.Rows[i][8] + " <br/> Mobile No : " + dt.Rows[i][7] + " <br/> Date Time : " + dt.Rows[i][5].ToString().Trim().Replace("'", "") + " - " + dt.Rows[i][6].ToString().Trim().Replace("'", "") + " <br/> Status : " + dt.Rows[i][4] + " <br/> Battery Remaining : " + dt.Rows[i][3] + "'  ," + "\"type\"" + ": '" + dt.Rows[i][4] + "'," + "\"unitdescp\"" + ": 'Unit Name : " + dt.Rows[i][3].ToString().Trim().Replace("'", "") + " <br/> Location : " + dt.Rows[i][5].ToString().Trim().Replace("'", "") + " <br/> Region : " + dt.Rows[i][6] + " <br/> Site Type : " + dt.Rows[i][4].ToString().ToUpper().Replace("NONCMS", "NON-CMS") + "'," + "\"monn\"" + ": '" + userpropic + "'},";

        //            //Response.Write(markers+"<br/>");
        //        }
        //        //  Response.Write(markers);

        //        //        string query = @"select distinct u.unitid,u.unitname, location,region,lat,lon from unit u join 
        //        //                    dailyvisit d on u.unitid=d.unitid where lat is not null and lat not like '' and region like '"+ddBranch.SelectedValue+@"'
        //        //                    union all
        //        //                    select distinct u.unitid,u.unitname, location,region,lat,lon from unit u  join cmsvisit c on
        //        //                    u.unitid=c.unitid where lat is not null and lat not like '' and region like '"+ddBranch.SelectedValue+"'";

        //        //        SqlCommand cmdd = new SqlCommand(query);
        //        //        DataTable dtt = bucket.GetData(cmdd);

        //        //        string unit = "";
        //        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        //        {
        //        //            unit += "{ " + "\"lat\"" + ": '" + dt.Rows[i][4] + "' ," + "\"lng\"" + ": '" + dt.Rows[i][5] + "' ," + "\"description\"" + ": 'Unit Name : " + dt.Rows[i][1] + " <br/> Location : " + dt.Rows[i][2] + " <br/> Region : " + dt.Rows[i][3] + "' },";
        //        //        }

        //        // Response.Write(markers);
        //        //var units = [" + unit.TrimEnd(',') + @"];

        //        string map = @"
        //<script type='text/javascript'>
        //
        //
        //var markers = [ " + markers.TrimEnd(',') + @" ];
        //window.onload = function () {
        // 
        //
        //var iconBase = 'http://maps.google.com/mapfiles/ms/icons/';
        //        var icons = {
        //          online: {
        //            name: ' User (online)   ',
        //            icon: './Image/man-green.png'
        //          },
        //          offline: {
        //            name: ' User (Offline)  ',
        //            icon: './Image/man-red.png'
        //          },
        //          batterylow: {
        //            name: ' User (Battery Low)  ',
        //            icon: './Image/man-orange.png'
        //          },
        //          noncms: {
        //           name: '  NON CMS    ',
        //            icon: iconBase + 'purple-dot.png'
        //          },
        //       cms: {
        //           name: '  CMS    ',
        //            icon: iconBase + 'orange-dot.png'
        //          }
        //        };
        //
        //
        //
        //    var mapOptions = {
        //        center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
        //        zoom: 8,
        //        mapTypeId: google.maps.MapTypeId.ROADMAP
        //    };
        //
        //    var infoWindow = new google.maps.InfoWindow();
        //    var latlngbounds = new google.maps.LatLngBounds();
        //    var map = new google.maps.Map(document.getElementById('dvMap'), mapOptions);
        //
        //var legend = document.getElementById('legend');
        //        for (var key in icons) {
        //          var type = icons[key];
        //          var name = type.name;
        //          var icon = type.icon;
        //          var div = document.createElement('div');
        //          div.innerHTML = '<img src=' + icon + '> ' + name+'<br/><br/>';
        //          legend.appendChild(div);
        //        };
        //
        //map.controls[google.maps.ControlPosition.RIGHT_BOTTOM].push
        //(legend);
        //    var i = 0;
        //    var interval = setInterval(function () {
        //         
        //        var data = markers[i]
        //        var myLatlng = new google.maps.LatLng(data.lat, data.lng);
        //        var icon = '';
        //        switch (data.type) {
        //            case'Offline':
        //                icon ='./Image/man-red';            
        //                break;            
        //            case 'Batterylow':
        //                icon = './Image/man-orange';
        //                break;
        //            case 'Online':
        //                icon = './Image/man-green';
        //                break;
        //            case 'noncms':
        //                icon = 'http://maps.google.com/mapfiles/ms/icons/purple-dot';
        //                break;
        //            case 'cms':
        //                icon = 'http://maps.google.com/mapfiles/ms/icons/orange-dot';
        //                break;
        //        }
        //       //icon = 'http://maps.google.com/mapfiles/ms/icons/' + icon + '.png';
        //        // icon = '../Image/' +icon+'.png';
        //            icon = icon+'.png';
        //if(icon=='http://maps.google.com/mapfiles/ms/icons/purple-dot.png' || icon=='http://maps.google.com/mapfiles/ms/icons/orange-dot.png')
        //{
        //
        //        var marker = new google.maps.Marker({
        //            position: myLatlng,
        //            map: map,
        //            title: data.type,
        //            animation: google.maps.Animation.DROP,
        //            icon: new google.maps.MarkerImage(icon,
        //            new google.maps.Size(20, 20), /* size is determined at runtime */
        //            null, /* origin is 0,0 */
        //            null, /* anchor is bottom center of the scaled image */
        //            new google.maps.Size(20, 20))                      
        //        });
        //}
        //else{
        //var marker = new google.maps.Marker({
        //            position: myLatlng,
        //            map: map,
        //            title: data.type,
        //            animation: google.maps.Animation.DROP,
        //            icon: new google.maps.MarkerImage(icon,
        //            new google.maps.Size(60, 60), /* size is determined at runtime */
        //            null, /* origin is 0,0 */
        //            null, /* anchor is bottom center of the scaled image */
        //            new google.maps.Size(60, 60))           
        //            
        //        });
        //}
        //
        //
        //
        //        (function (marker, data) {
        //            google.maps.event.addListener(marker, 'click', function (e) {
        //                var geocoder = geocoder = new google.maps.Geocoder();
        //                geocoder.geocode({ 'latLng': myLatlng }, function (results, status) {
        //                if (status == google.maps.GeocoderStatus.OK) {
        //                    if (results[1]) { 
        //                    var contentString = ' ';
        //
        //switch (data.type) {
        //            case'Offline':
        //
        //                    contentString = '<table><tr><td><img src=' + data.monn + ' width=100px height=100px border=2></td><td>&nbsp;</td><td> UserID : ' + data.title +' <br />' + data.description +' <br />' +' Address : ' + results[1].formatted_address +'</td></tr></table>';
        //
        //                break;            
        //            case 'Batterylow':
        //                    contentString = '<table><tr><td><img src=' + data.monn + ' width=100px height=100px border=2></td><td>&nbsp;</td><td> UserID : ' + data.title +' <br />' + data.description +' <br />' +' Address : ' + results[1].formatted_address +'</td></tr></table>';
        //                break;
        //            case 'Online':
        //                    contentString = '<table><tr><td><img src=' + data.monn + ' width=100px height=100px border=2></td><td>&nbsp;</td><td> UserID : ' + data.title +' <br />' + data.description +' <br />' +' Address : ' + results[1].formatted_address +'</td></tr></table>';
        //                break;
        //            case 'cms':
        //                    contentString = '<p> Unit ID : ' + data.title +' <br /> ' + data.unitdescp +'</p>';
        //                break;
        //            case 'noncms':
        //                    contentString = '<p> Unit ID : ' + data.title +' <br /> ' + data.unitdescp +'</p>';
        //                break;
        //        }
        //                    infoWindow.setContent(contentString);//data.description
        //                    infoWindow.open(map, marker);
        //                    }
        //                  }
        //                });
        //            });
        //        })(marker, data); 
        //
        //        latlngbounds.extend(marker.position);
        //        i++;
        //        if (i == markers.length) {
        //            clearInterval(interval);
        //            var bounds = new google.maps.LatLngBounds();
        //            map.setCenter(latlngbounds.getCenter());
        //            map.fitBounds(latlngbounds);
        //        }
        //
        //
        //
        //    }, 20); 
        //
        //
        ////setlegends();
        // 
        //}
        //
        //
        //
        //
        //
        //
        //</script>";
        //        //Response.Write();
        //        //ddofficer.Items.Clear();


        //        //ddofficer.Items.Add("All");
        //        //ddofficer.Items.FindByText("All").Value = "%";

        //        //PopulateDropDownList(PopulateOfficer(ddBranch.SelectedValue), ddofficer);
        //        //ddofficer.Items.FindByValue(officername).Selected = true;

        //        //containt.InnerHtml = map;
        //    }
        #endregion

        private void PopulateDropDownList(ArrayList list, DropDownList ddl)
        {
            //ddl.DataSource = list;
            //ddl.DataTextField = "Text";
            //ddl.DataValueField = "Value";
            //ddl.DataBind();
        }


        //[System.Web.Services.WebMethod]
        //public static ArrayList PopulateOfficer(string category)
        //{
        //    ArrayList list = new ArrayList();
        //    String strConnString = ConfigurationManager
        //        .ConnectionStrings["SQLConnstr"].ConnectionString;
        //    String strQuery ="";
        //    if(HttpContext.Current.Session["sess_role"].ToString().ToUpper()=="ADMIN")
        //    {
        //        strQuery = "select userid from users where status <> 'DEL' and role in ('Supervisor','HK','ACCOUNT') and region like @br";

        //    }
        //    else if (HttpContext.Current.Session["sess_role"].ToString().ToUpper() == "RM")
        //    {
        //        strQuery = "select userid from users where status <> 'DEL' and role in ('Supervisor','HK','ACCOUNT') and region like '"+HttpContext.Current.Session["sess_region"].ToString()+"'";

        //    }

        //    using (SqlConnection con = new SqlConnection(strConnString))
        //    {

        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@br", br);
        //            cmd.CommandText = strQuery;

        //            cmd.Connection = con;
        //            con.Open();
        //            SqlDataReader sdr = cmd.ExecuteReader();
        //            while (sdr.Read())
        //            {
        //                list.Add(new ListItem(
        //               sdr["userid"].ToString(),
        //               sdr["userid"].ToString()
        //                ));
        //            }
        //            con.Close();
        //            return list;
        //        }
        //    }
        //}

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateLocation(string category)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            //String strQuery = @"select Convert(varchar(50),subcatid) as subcatid ,subcategoryname from subcategory where Convert(varchar(50),zoneid) = '" + shift.Split('|')[0] + "' and Convert(varchar(50),shiftid)= '" + shift.Split('|')[1] + "' and Convert(varchar(50),catid) = '" + category + "'";

            String strQuery = @"select distinct userid as val, upper(username) as txt  FROM [users] WHERE auditing='Yes'  and 
                            userid in (select userid from regionmap where region like '" + category + @"')
                            and status<>'DEL' order by txt";

            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strQuery;
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    int c = 0;
                    while (sdr.Read())
                    {
                        list.Add(new System.Web.UI.WebControls.ListItem(
                       sdr["txt"].ToString(),
                       sdr["val"].ToString()
                        ));
                        c++;
                    }

                    if (c > 1 || c == 0)
                    {
                        list.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "%"));
                    }

                    con.Close();
                    return list;
                }
            }
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            // createMap();
            createOSMMap();
        }
        protected void btn_search_Click1(object sender, EventArgs e)
        {
            //createMap();
            createOSMMap();
        }

        public string drawmap()
        {
            string mapstr = "";
            try
            {
                ibuckethead bucket = new ibuckethead();
                string rows;
                string qry = @"select u.username,l.l_lat,l.l_long,l.Rem_Battery, case when l.rem_battery <=5 then 'Batterylow' else 
                    case when DATEDIFF(MINUTE,CONVERT(VARCHAR(19), GETDATE(),120),replace(l.b_date,'/','-') + ' ' + l.l_time) < -60 then 'Offline' 
                    else 'Online' end end as[Status],substring(b_date,4,3) + substring(b_date,1,2)+ substring(b_date,6,5) + ' ' + b_time as [Last Updated On]
                    from location l inner join users u on l.userid = u.userid where l_lat <>'0' and l_long <>'0'";
                if (Session["sess_role"].ToString() == "AO")
                {
                    qry += " and u.userid in (select userid from  AreaUserMap where area in (select area from AreaUserMap where userid='" + Session["sess_userid"].ToString() + "'))";
                }
                else if (Session["sess_role"].ToString() == "BM")
                {
                    qry += " and u.Branch in (Select Branch from users where userid = '" + Session["sess_userid"].ToString() + "')";
                }
                else if (Session["sess_role"].ToString() == "REGION")
                {
                    qry += " and u.REGION in (Select REGION from users where userid = '" + Session["sess_userid"].ToString() + "')";
                }
                SqlCommand cmd = new SqlCommand(qry);
                DataTable dt = bucket.GetData(cmd);

                string usercoor = "";
                string marker = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //usercoor += "ol.proj.transform([" + dt.Rows[i][4].ToString() + "," + dt.Rows[i][3].ToString() + "], 'EPSG:4326','EPSG:3857'),";
                        usercoor += "ol.proj.transform([" + dt.Rows[i][2].ToString() + "," + dt.Rows[i][1].ToString() + "], 'EPSG:4326','EPSG:3857'),";

                        marker += @"var iconFeature" + i + @" = new ol.Feature({
                        geometry: new ol.geom.Point(loc[" + i + @"]),
                        name: '" + dt.Rows[i][0].ToString() + @"',
                        LastUpdatedOn: '" + dt.Rows[i][5].ToString() + @"',
                        rainfall: 501
                    });

                    iconFeature" + i + @".setStyle(new ol.style.Style({
                        image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
                            //  color: '#8959A8',
                            //crossOrigin: 'anonymous',
                            anchor: [0.5, 30],
                            anchorXUnits: 'fraction',
                            anchorYUnits: 'pixels',                    ";

                        if (dt.Rows[i][4].ToString() == "Batterylow")
                        {
                            marker += "src: '../Image/man-orange.png'";
                        }
                        else if (dt.Rows[i][4].ToString() == "Offline")
                        {
                            marker += "src: '../Image/man-red.png'";
                        }
                        else if (dt.Rows[i][4].ToString() == "Online")
                        {
                            marker += "src: '../Image/man-green.png'";
                        }
                        marker += @" }))
                    }));

                    iconFeatures.push(iconFeature" + i + @");";
                    }
                }

                mapstr = @"<script>
    setTimeout(function(){  
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
                        }),
                        zIndex: 2
                    })
                ],
                updateWhileAnimating: true
            });

            var loc = [" + usercoor + @"]; 
            var map = new ol.Map({
                target: 'map',
                view: new ol.View({
                    center: loc[0],
                    zoom: 16,
                    minZoom: 2,
                    maxZoom: 20
                }),
                layers: [
                    new ol.layer.Tile({
                        source: new ol.source.OSM(),
                        opacity: 0.6
                    })
                ],
                overlays: [overlay]
            });
            
                var view = map.getView();
                var extent = ol.extent.boundingExtent([" + usercoor + @"]);
                var size = map.getSize();
                view.fit(extent, size);
                if (view.getZoom() > 16) {
                    view.setZoom(9);
                }              

            var iconFeatures = [];
            " + marker + @"

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
                var feature = map.forEachFeatureAtPixel(evt.pixel,
                    function (feature) {
                        return feature;
                    });
                if (feature) { 
                    var coordinates = feature.getGeometry().getCoordinates();
                    var longlat = ol.proj.transform(coordinates, 'EPSG:3857', 'EPSG:4326');
                    popup.setPosition(coordinates);
                    var sid = GetResults(longlat[1], longlat[0]);  
                      //var sid = '';
                     content.innerHTML = '<p>User Name : '+ feature.get('name')+ '<br/>Last Updated On : '+feature.get('LastUpdatedOn')+'<br/>'+'Address : '+ sid.display_name+'</p>';
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

        }, 300);

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
                mapstr = "No details found.." + @"<script> var container = document.getElementById('popup');
                var geo = document.getElementById('geo-marker');
                container.style.display = 'none';  geo.style.display = 'none';
                </script>";
            }
            return mapstr;
            // map.InnerHtml = mapstr;
        }

        public void createOSMMap()
        {
            var officername = "";

            string strmap = "";
            try
            {
                string s1 = "", s2 = "";

                hdnlocation.Value = Request.Form[ddofficer.UniqueID];
                if (hdnlocation.Value == null || hdnlocation.Value == "" || ddofficer.SelectedValue == "%")
                {
                    officername = "%";
                    s1 = @"select   distinct l.userid,L_lat,L_Long,
                case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1,
                case when l.rem_battery <=5 then 'Batterylow' else
                case when DATEDIFF(MINUTE,replace(l.b_date,'/','-') + ' ' + l.l_time,CONVERT(VARCHAR(19), GETDATE(),120)) > 60 then
                'Offline' else 'Online' end end as [Status],
                l_Date,l_time,
                isnull(u.mobile,'NA'),username
                from location l 
                inner join users u 
                on l.userid = u.userid where    u.userid in (select userid from users where userid<>'admin' and role in ('AO','DE') and RM like '" + Session["sess_username"] + "') and (l_lat<>'0') and  l_date = '" + txt_frmDate.Text + "' and ( l_long<> '0')  and l_lat is not null and l_long is not null	and u.status <> 'DEL'";

                }
                else
                {
                    officername = hdnlocation.Value;
                    s1 = @"select   distinct l.userid,L_lat,L_Long,
                case when rem_battery>0 then rem_battery when rem_battery<0 then '0' end as rem_battery1,
                case when l.rem_battery <=5 then 'Batterylow' else
                case when DATEDIFF(MINUTE,replace(l.b_date,'/','-') + ' ' + l.l_time,CONVERT(VARCHAR(19), GETDATE(),120)) > 60 then
                'Offline' else 'Online' end end as [Status],
                l_Date,l_time,
                isnull(u.mobile,'NA'),username
                from location l 
                inner join users u 
                on l.userid = u.userid where    u.userid like '%" + officername + @"%' and (l_lat<>'0') and  l_date = '" + txt_frmDate.Text + "' and ( l_long<> '0')  and l_lat is not null and l_long is not null	and u.status <> 'DEL'";

                }




                //            s2 = @"select distinct u.unitid,LTRIM(RTRIM(unitlat)),LTRIM(RTRIM(unitlong)),u.unitname,case when type='CMS' then 'CMS' else 'NONCMS' end as type, location,region,'','','','',''
                //                    from unit u 
                //				    where unitlat is not null and  unitlat not like '' and unitlat <> '0.0' and unitlat <> '0'  and unitlat<> '1..' and unitlat <> 'NULL' and u.status <> 'Inactive'";

                string qry = @"";


                qry = s1;
                //Response.Write(qry);
                SqlCommand cmd = new SqlCommand(qry);
                DataTable dt = bucket.GetData(cmd);

                //string userpropic = "";
                string markers = "";
                string usercoords = "";
                string usercoor = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //  markers += "{ " + "\"title\"" + ": '" + dt.Rows[i][0].ToString().Trim().Replace("'", "") + "' ," + "\"lat\"" + ": '" + dt.Rows[i][1].ToString().Trim() + "' ," + "\"lng\"" + ": '" + dt.Rows[i][2].ToString().Trim() + "' ," + "\"description\"" + ": 'Name : " + dt.Rows[i][8] + " <br/> Mobile No : " + dt.Rows[i][7] + " <br/> Date Time : " + dt.Rows[i][5].ToString().Trim().Replace("'", "") + " - " + dt.Rows[i][6].ToString().Trim().Replace("'", "") + " <br/> Status : " + dt.Rows[i][4] + " <br/> Battery Remaining : " + dt.Rows[i][3] + "'  ," + "\"type\"" + ": '" + dt.Rows[i][4] + "'," + "\"unitdescp\"" + ": 'Unit Name : " + dt.Rows[i][3].ToString().Trim().Replace("'", "") + " <br/> Location : " + dt.Rows[i][5].ToString().Trim().Replace("'", "") + " <br/> Region : " + dt.Rows[i][6] + " <br/> Site Type : " +dt.Rows[i][4].ToString().ToUpper().Replace("NONCMS","NON-CMS")+"'},";
                    markers += "{ " + "\"title\"" + ": '" + dt.Rows[i][0].ToString().Trim().Replace("'", "") + @"' ,
                           " + "\"lat\"" + ": '" + dt.Rows[i][1].ToString().Trim() + "' ," + "\"lng\"" + ": '" + dt.Rows[i][2].ToString().Trim() + @"' ,
                 'Name : " + dt.Rows[i][8] + " <br/> Mobile No : " + dt.Rows[i][7] + " <br/> Date Time : "
                      + dt.Rows[i][5].ToString().Trim().Replace("'", "") + " - " + dt.Rows[i][6].ToString().Trim().Replace("'", "") +
                      " <br/> Status : " + dt.Rows[i][4] + " <br/> Battery Remaining : " + dt.Rows[i][3] + "'  ," + "\"type\"" + ": '"
                      + dt.Rows[i][4] + @"' <br/>}";

                    usercoords += "ol.proj.transform([" + dt.Rows[i][2].ToString() + "," + dt.Rows[i][1].ToString() + "], 'EPSG:4326','EPSG:3857'),";
                    usercoor += "['" + dt.Rows[i][0].ToString().Trim().Replace("'", "") + "'," + dt.Rows[i][1].ToString() + "," + dt.Rows[i][2].ToString() + ",'" +
                        dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString().ToUpper().Replace("NONCMS", "NON-CMS") + "','" +
                        dt.Rows[i][5].ToString().Trim().Replace("'", "") + "','" +
                        dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt.Rows[i][8].ToString() + "'],";
                    //Response.Write(usercoor+"<br/>");
                }

                strmap = @"<script>
   // setTimeout(function(){ 
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

            var loc = [" + usercoords + @"]; 
            var map = new ol.Map({
                target: 'map',
                view: new ol.View({
                    center: loc[0],
                    zoom: 16,
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
            
            
                var view = map.getView();
                var extent = ol.extent.boundingExtent([" + usercoords + @"]);
                var size = map.getSize();
                view.fit(extent, size);
                if (view.getZoom() > 16) {
                    view.setZoom(9);
                }            

            var userCoor = [ " + usercoor.TrimEnd(',') + @"];
            var iconFeatures = [];
            var j=0;
            var iconBase = 'http://maps.google.com/mapfiles/ms/icons/';
          for (i = 0; i < userCoor.length; i++) {
            //debugger;
     if(userCoor[i][4]== 'ONLINE')
            {
            var iconFeature = new ol.Feature({
                geometry: new ol.geom.Point(loc[i]),//ol.proj.transform([-72.0704, 46.678], 'EPSG:4326','EPSG:3857')),
                name: '<table><tr><td> User ID : '+userCoor[i][0]+'<br/>Name : '+userCoor[i][8]+'<br/>Mobile No : '+userCoor[i][7]+'<br/>Date Time : '+userCoor[i][5]+' - '+userCoor[i][6]+'<br/>Status : '+userCoor[i][4]+'<br/>Battery Remaining : '+userCoor[i][3]+'%</td></tr></table>',
                type:'user',                
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
                    src: './Image/man-green.png'
                }))
            }));
            iconFeatures.push(iconFeature);
            }
             else if(userCoor[i][4] == 'OFFLINE')
            {
            var iconFeature = new ol.Feature({
                geometry: new ol.geom.Point(loc[i]),//ol.proj.transform([-72.0704, 46.678], 'EPSG:4326','EPSG:3857')),
                name: '<table><tr><td> User ID : '+userCoor[i][0]+'<br/>Name : '+userCoor[i][8]+'<br/>Mobile No : '+userCoor[i][7]+'<br/>Date Time : '+userCoor[i][5]+' - '+userCoor[i][6]+'<br/>Status : '+userCoor[i][4]+'<br/>Battery Remaining : '+userCoor[i][3]+'%</td></tr></table>',
                type:'user',                
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
                    
                    src: './Image/man-red.png'
                }))
            }));
            iconFeatures.push(iconFeature);
            }
            else if(userCoor[i][4] == 'BATTERYLOW'){ 
            var iconFeature = new ol.Feature({
                geometry: new ol.geom.Point(loc[i]),//ol.proj.transform([-72.0704, 46.678], 'EPSG:4326','EPSG:3857')),
                name: '<table><tr><td> User ID : '+userCoor[i][0]+'<br/>Name : '+userCoor[i][8]+'<br/>Mobile No : '+userCoor[i][7]+'<br/>Date Time : '+userCoor[i][5]+' - '+userCoor[i][6]+'<br/>Status : '+userCoor[i][4]+'<br/>Battery Remaining : '+userCoor[i][3]+'%</td></tr></table>',
                type:'user',                            
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
                    src: './Image/man-orange.png'
                }))
            }));
            iconFeatures.push(iconFeature);
           }
             
           
            }
debugger;
//            var iconFeature1 = new ol.Feature({
//                geometry: new ol.geom.Point(loc[1]),
//                name: 'Null Island Two',
//                population: 4001,
//                rainfall: 501
//            });
//
//            iconFeature1.setStyle(new ol.style.Style({
//                image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
//                    //  color: '#8959A8',
//                    //crossOrigin: 'anonymous',
//                    anchor: [0.5, 30],
//                    anchorXUnits: 'fraction',
//                    anchorYUnits: 'pixels',
//                    src: 'http://maps.google.com/mapfiles/ms/icons/red-dot.png'
//                }))
//            }));
//
//            iconFeatures.push(iconFeature1);


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
                var feature = map.forEachFeatureAtPixel(evt.pixel,
                    function (feature) {
                        return feature;
                    });
                if (feature) {
                    var coordinates = feature.getGeometry().getCoordinates();
                    var longlat = ol.proj.transform(coordinates, 'EPSG:3857', 'EPSG:4326');
                    popup.setPosition(coordinates);
                    if(feature.get('type')=='atm')
                    {
                    content.innerHTML = '<p>'+ feature.O.name+ '</p>';
                    }
                    else if(feature.get('type')=='user')
                    {
                    var sid = GetResults(longlat[1], longlat[0]);
                    //var sid='brijesh';
                    content.innerHTML = '<p>'+ feature.get('name')+ '<br/>'+'Address : '+ sid.display_name+'</p>';
                    }
//                    var sid = GetResults(longlat[1], longlat[0]);
//                    content.innerHTML = feature.O.name+ 'Address : '+ sid.display_name;
                     overlay.setPosition(coordinates);
                } else { 
                     overlay.setPosition(undefined);
                     closer.blur();
                }
//                    $(element).popover({
//                        'placement': 'top',
//                        'html': true,
//                        'content': 'Siteid : '+feature.O.name+'<br/>Address : '+ sid.display_name //feature.get('name')
//                    });
//                    $(element).popover('show');
//                } else {
//                    $(element).popover('destroy');
//                }
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


                    //}, 300);

             function GetResults(lat, long) {
                var jsonObjectInstance = $.parseJSON($.ajax({
                    url: 'https://locationiq.org/v1/reverse.php?format=json&key=9c73f8c9ed5d47&lat=' + lat + '&lon=' + long,     
                    async: false,
                    dataType: 'json'
                }).responseText); return jsonObjectInstance
            } 
        </script>";

                if (dt.Rows.Count <= 0)
                {
                    strmap = @"No details found.." + @"<script> var container = document.getElementById('popup');
                var geo = document.getElementById('geo-marker');
                container.style.display = 'none';  geo.style.display = 'none';
                </script>";
                }
            }
            catch (Exception)
            {
                strmap = "No details found.." + @"<script> var container = document.getElementById('popup');
                var geo = document.getElementById('geo-marker');
                container.style.display = 'none';  geo.style.display = 'none';
                </script>";
            }
            map.InnerHtml = strmap;
        }
    }
}