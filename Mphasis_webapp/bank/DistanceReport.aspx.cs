using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mphasis_webapp.bank
{
    public partial class DistanceReport : System.Web.UI.Page
    {
        ibuckethead bucket = new ibuckethead();
        CommonClass obj = new CommonClass();
        string rows;
        string mapwithanimation = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            defaultCalendarExtender.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            //defaultCalendarExtender1.EndDate = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MM/yyyy"));

            txt_frmDate.Attributes.Add("readonly", "readonly");
            //txt_toDate.Attributes.Add("readonly", "readonly");

            #region Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            /* Add 'ALL' to userid dropdown list
            /*------------------------------------------------------------------------------------------------*/
            if (!Page.IsPostBack)
            {
                chk123.Visible = false;
                Session["sess_x"] = "";
                //DropDownList1.DataBind();

                //DropDownList1.Items.Add("ALL");
                //DropDownList1.Items.FindByText("ALL").Value = "%";
                //DropDownList1.Items.FindByValue("%").Selected = true;
                string state = "SELECT  distinct state,state as STATE FROM [atms] where CH = '" + Session["sess_username"].ToString() + "'";
                obj.BindListboxWithValue(ddstate, state);
            }
            else
            {
                chk123.Visible = true;
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static ArrayList PopulateUser(string state)
        {
            ArrayList list = new ArrayList();
            String strConnString = ConfigurationManager
                .ConnectionStrings["SQLConnstr"].ConnectionString;

            string strQuery = " select distinct u.userid from users u " +
                              " join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role in ('AO','CM','DE') " +
                            " and a.CH like '" + HttpContext.Current.Session["sess_username"].ToString() + "'";
            if (!string.IsNullOrWhiteSpace(state))
            {
                strQuery += " and u.state in(" + state + ")";
            }
            strQuery += "  order by userid";

            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strQuery;

                        cmd.Connection = con;
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            list.Add(new ListItem(sdr[0].ToString()));
                        }
                        con.Close();
                        return list;
                    }
                    catch (Exception ex) { }
                    return list;
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            #region Code to fetch data from DR_CTP
            string users = Request.Form[DropDownList1.UniqueID];
            hdfUsers.Value = users;
            if (users == "All")
            {
                users = "%";
            }
            string sql = "";
            if (txtuser.Text == "")
            {
                sql = @"SELECT distinct(case when d.Siteid='NA' then Vid else d.Siteid end) as [SITE ID], d.USERID AS [USER ID],
                case when d.Siteid='NA' then d.address  else a.addressline1 end  AS LOCATION,a.city as CITY,case when d.Siteid='NA' then '-'  else a.bankid end AS [BANK NAME], 
                convert(varchar(10),convert(date,ldate),103) AS [DATE OF VISIT], d.ltime AS [TIME OF VISIT],DistanceTraveled as [DISTANCE  IN KM]
                from Distance d left outer join atms a on d.siteid=a.siteid where d.userid like '" + users + "' and CH like '" + Session["sess_username"] + "' and convert(date,ldate) = '" +
                        txt_frmDate.Text + "' order by d.ltime desc";
            }
            else
            {
                sql = @"SELECT distinct(case when d.Siteid='NA' then Vid else d.Siteid end) as [SITE ID], d.USERID AS [USER ID],
                       case when d.Siteid='NA' then d.address  else a.addressline1 end  AS LOCATION,a.city as CITY,case when d.Siteid='NA' then '-'  else a.bankid end AS [BANK NAME], 
                       convert(varchar(10),convert(date,ldate),103) AS [DATE OF VISIT], d.ltime AS [TIME OF VISIT],DistanceTraveled as [DISTANCE  IN KM]
                       from Distance d left outer join atms a on d.siteid=a.siteid where d.userid like '" + users +
                                "' and CH like '" + Session["sess_username"] + "' and a.state in (" + txtuser.Text + @") and convert(date,ldate) = '" +
                             txt_frmDate.Text + "' order by d.ltime desc";
            }
            bucket.BindGrid(GridView1, sql);
            //Response.Write(sql);

            /*------------------------------------------------------------------------------------------------*/
            /* If no rows returned display null error or fetch count
            /*------------------------------------------------------------------------------------------------*/
            if (GridView1.Rows.Count.Equals(0))
            {
                Label1.Visible = true;
                Label3.Visible = false;
                ImageButton1.Visible = false;
                div1.Visible = true;
                chk123.Visible = false;
            }
            else
            {
                div1.Visible = true;
                ImageButton1.Visible = true;
                Label3.Visible = true;
                Label1.Visible = false;
                Label3.Text = bucket.CountRows(GridView1, Label3);
            }
            /*------------------------------------------------------------------------------------------------*/
            #endregion

            // getmap();
            drawmap(users);
        }
        #region start
        public void getmap()
        {
            //Response.Write(chk123.Items.Count);
            //for (int i = 1; i < chk123.Items.Count; i++)
            //{
            //  chk123.Items.RemoveAt(i);
            //}
            try
            {
                if (chk123.Items.Count >= 1)
                {
                    chk123.Items.RemoveAt(1);
                }

            }
            catch
            {

            }

            chk123.Items.Add(DropDownList1.SelectedValue);

            string x =

            @"<script>
        function initialize() {
            var myLatlng = new google.maps.LatLng(-25.363882, 131.044922);
            var mapOptions = {
                zoom: 4,
                center: myLatlng
            }
            //var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

            var locations = [" + getlatlong() + "];" +

                "var map = new google.maps.Map(document.getElementById('map-canvas'), {" +
                  "    zoom: 10," +
                    "center: new google.maps.LatLng(19.40, 72.86)," +
                    "mapTypeId: google.maps.MapTypeId.ROADMAP" +
               " });" +

                @"var infowindow = new google.maps.InfoWindow();

            var marker, i;

            for (i = 0; i < locations.length; i++) {
                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                    map: map
                });

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        infowindow.setContent(locations[i][0]);
                        infowindow.open(map, marker);
                    }
                })(marker, i));
            }
        }

        google.maps.event.addDomListener(window, 'load', initialize);

    </script>";

            Session["sess_x"] = x;
        }

        public string getlatlong()
        {
            string latlong = ""; int i = 1;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnstr"].ConnectionString);
            SqlCommand cnCommand = cn.CreateCommand();
            SqlDataReader reader = default(SqlDataReader);
            try
            {
                cnCommand.CommandText = @"Select '[' + char(39) + case when d.siteid='NA' then vid else d.siteid end  + char(39) + ',' + lat + ',' + long + ',' from distance d left outer join atms a " +
                                        "on a.siteid=d.siteid where userid='" + DropDownList1.SelectedValue +
                                        "'  and ldate='" + txt_frmDate.Text + "' order by ldate,ltime";
                //cnCommand.CommandText = @"Select '{""lat"": ' + lat + ',""lng"": ' + long + '},' from Wheeltracker where serialnumber='ahmedabad01'";
                cn.Open();
                reader = cnCommand.ExecuteReader();

                while (reader.Read())
                {
                    latlong += reader[0].ToString() + i + "],";
                    i++;
                }
                //Response.Write(cnCommand.CommandText);
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

            return latlong.TrimEnd(',');
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            btn_search_Click(sender, e);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            btn_search_Click(sender, e);
            //GridView1.AllowPaging = false;
            // GridView1.DataBind();
            Response.AddHeader("content-disposition", "attachment;filename=DistanceReport.xls");
            Response.Charset = String.Empty;
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Response.Write("9");
                //for (int i = 0; i < 45; i++)
                //{
                //    e.Row.Cells[i].Text=e.Row.Cells[i].Text.Replace("E|N","No-");
                //}
            }
        }
        #endregion

        public void drawmap(string users)
        {
            mapwithanimation = "";
            string sb = "";
            try
            {


                DataTable dt = new DataTable();
                // Query to get the user longlat of User
                ibuckethead bucket = new ibuckethead();
                if (Request.QueryString["userid"] != "")
                {
                    dt = bucket.BindoboutGrid("Select case when d.siteid='NA' then vid else d.siteid end , lat,long from distance d left outer join atms a on a.siteid=d.siteid where userid='" + users + "'  and ldate=Convert(date,'" + txt_frmDate.Text + "') and lat <> '0' and long <> '0' order by ltime");

                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //GOOGLE MAP Specific Code will come here      
                string usercoor = "";
                string path = "";
                if (dt.Rows.Count > 0)
                {
                    //  usercoor += "['" + dt.Rows[0][0].ToString() + "'," + dt.Rows[0][1].ToString() + "," + dt.Rows[0][2].ToString() + "],";
                    // usercoor += "['" + dt.Rows[0][0].ToString() + "'," + dt.Rows[dt.Rows.Count - 1][1].ToString() + "," + dt.Rows[dt.Rows.Count - 1][2].ToString() + "]";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        path += "{ lat: " + dt.Rows[i][1].ToString() + ", lng: " + dt.Rows[i][2].ToString() + "},";
                        usercoor += "['" + dt.Rows[i][0].ToString() + "'," + dt.Rows[i][1].ToString() + "," + dt.Rows[i][2].ToString() + "],";
                    }
                }

                mapwithanimation = @"<script>

try{
function initMap() {
            
            var userCoor = [ " + usercoor.TrimEnd(',') + @"];

            var map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: " + dt.Rows[0][1].ToString() + @", lng: " + dt.Rows[0][2].ToString() + @" },
                zoom: 12,
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
           
          if(userCoor[i][0]== 'PUNCHIN'){// if(i==0){
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(userCoor[i][1], userCoor[i][2]),
                map: map,
                title: '',
                icon: new google.maps.MarkerImage('./Image/green-dot.png')
            });
            }
             else if(userCoor[i][0] == 'PUNCHOUT'){ //if(i == userCoor.length - 1){
             var marker = new google.maps.Marker({
                position: new google.maps.LatLng(userCoor[i][1], userCoor[i][2]),
                map: map,
                title: '',
                icon: new google.maps.MarkerImage('./Image/red-dot.png')
            });
           }
            else {
             var marker = new google.maps.Marker({
                position: new google.maps.LatLng(userCoor[i][1], userCoor[i][2]),
                map: map,
                title: '',
                icon: new google.maps.MarkerImage('./Image/blue-dot.png')
            });
           }
        
            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                                       
                    var latlng = new google.maps.LatLng(userCoor[i][1],userCoor[i][2]);
                    var geocoder = geocoder = new google.maps.Geocoder();
                    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                     if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) { 
                        var contentString = ' ';                     
                        contentString = '<p> Siteid : ' + userCoor[i][0] +' <br />' +' Address : ' + results[1].formatted_address +'</p>';
                       // contentString = results[1].formatted_address;
                        infowindow.setContent(contentString);//userCoor[i][0]
                        infowindow.open(map, marker);
                            }
                        }   
                     });                   
                }
            })(marker, i));

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
    }
}