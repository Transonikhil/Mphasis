<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocatorN_Poly_1.aspx.cs" Inherits="Mphasis_webapp.RM.LocatorN_Poly_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        .style1
        {
            text-align: center;
        }
        #legend
        {
            font-family: Cambria, Cochin, Georgia, Times;
            background: #fff; /*padding: 10px;
    margin: 10px;*/
            border: 3px solid #000;
            text-align: left;
        }
        #legend img
        {
            vertical-align: middle;
        }
    </style>

    <script type="text/javascript">

        function windowOnLoad() {
            document.getElementById('txt_frmDate').readOnly = true;
            //document.getElementById('txt_toDate').readOnly = true;
        }

        window.onload = windowOnLoad;
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

    <script src="dist/js/jquery.min.js"></script>

    <script type="text/javascript">
        function PopulateControl(list, control) {
            debugger;
            control.empty();
            if (list.length > 0) {
                control.removeAttr("disabled");
                $.each(list, function() {
                    control.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
                $(function() {
                    $('select option').filter(function() {
                        return ($(this).val().trim() == "" && $(this).text().trim() == "");
                    }).remove();
                });
            }
            else {
                $(function() {
                    $('select option').filter(function() {
                        return ($(this).val().trim() == "" && $(this).text().trim() == "");
                    }).remove();
                });
            }
            var users = $('#<%=hdnlocation.ClientID%>').val();
            if (users != "") {
                $('#<%=ddofficer.ClientID %>').val(users);
                $('#<%=hdfUsers.ClientID%>').val('');
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <link rel="stylesheet" href="../js/ol.css" type="text/css" />
    <!-- The line below is only needed for old environments like Internet Explorer and Android 4.x -->

    <script src="https://cdn.polyfill.io/v2/polyfill.min.js?features=requestAnimationFrame,Element.prototype.classList,URL"></script>

    <script src="../js/ol.js"></script>

    <style>
        #loading-image
        {
            position: absolute;
            left: 43%;
            top: 40%;
            z-index: 1055;
        }
        #geo-marker
        {
            width: 10px;
            height: 10px;
            border: 1px solid #088;
            border-radius: 5px;
            background-color: #0b968f;
            opacity: 0.8;
        }
        .popover
        {
            width: 300px;
        }
        .popover
        {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 1060;
            display: none;
            max-width: 276px;
            padding: 1px;
            font-family: "Helvetica Neue" ,Helvetica,Arial,sans-serif;
            font-size: 14px;
            font-style: normal;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: left;
            text-align: start;
            text-decoration: none;
            text-shadow: none;
            text-transform: none;
            letter-spacing: normal;
            word-break: normal;
            word-spacing: normal;
            word-wrap: normal;
            white-space: normal;
            background-color: #fff;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            border: 1px solid #ccc;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 6px;
            -webkit-box-shadow: 0 5px 10px rgba(0,0,0,.2);
            box-shadow: 0 5px 10px rgba(0,0,0,.2);
            line-break: auto;
        }
        .popover-content
        {
            padding: 9px 14px;
        }
        .popover.top > .arrow
        {
            bottom: -11px;
            left: 50%;
            margin-left: -11px;
            border-top-color: #999;
            border-top-color: rgba(0,0,0,.25);
            border-bottom-width: 0;
        }
        .popover > .arrow
        {
            border-width: 11px;
        }
        .popover > .arrow, .popover > .arrow:after
        {
            position: absolute;
            display: block;
            width: 0;
            height: 0;
            border-color: transparent;
            border-style: solid;
        }
        .popover.top > .arrow:after
        {
            bottom: 1px;
            margin-left: -10px;
            content: " ";
            border-top-color: #fff;
            border-bottom-width: 0;
        }
        .popover > .arrow:after
        {
            content: "";
            border-width: 10px;
        }
        .popover > .arrow, .popover > .arrow:after
        {
            position: absolute;
            display: block;
            width: 0;
            height: 0;
            border-color: transparent;
            border-style: solid;
        }
    </style>
    <style>
        html, body, #mymap
        {
            width: 100%;
            height: 100%;
        }
        #geo-marker
        {
            width: 10px;
            height: 10px;
            border: 1px solid #088;
            border-radius: 5px;
            background-color: #0b968f;
            opacity: 0.8;
        }
        .popover
        {
            width: 300px;
        }
    </style>
    <style>
        .ol-popup
        {
            position: absolute;
            background-color: white;
            -webkit-filter: drop-shadow(0 1px 4px rgba(0,0,0,0.2));
            filter: drop-shadow(0 1px 4px rgba(0,0,0,0.2));
            padding: 15px;
            border-radius: 10px;
            border: 1px solid #cccccc;
            bottom: 12px;
            left: -50px;
            min-width: 380px;
            text-align: left;
        }
        .ol-popup:after, .ol-popup:before
        {
            top: 100%;
            border: solid transparent;
            content: " ";
            height: 0;
            width: 0;
            position: absolute;
            pointer-events: none;
        }
        .ol-popup:after
        {
            border-top-color: white;
            border-width: 10px;
            left: 48px;
            margin-left: -10px;
        }
        .ol-popup:before
        {
            border-top-color: #cccccc;
            border-width: 11px;
            left: 48px;
            margin-left: -11px;
        }
        .ol-popup-closer
        {
            text-decoration: none;
            position: absolute;
            top: 2px;
            right: 8px;
        }
        .ol-popup-closer:after
        {
            content: "✖";
            cursor: pointer;
        }
    </style>
    <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps;
        font-size: xx-large; font-weight: bolder;" class="animated fadeInLeft">
        View Entire Field Force
    </div>
    <br />
    <asp:HiddenField ID="hdnoff" runat="server" />
    <asp:HiddenField ID="hdnlocation" runat="server" />
    <asp:HiddenField ID="hdfUsers" runat="server" />
    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; text-align: left;">
        <div>
            <table width="100%">
                <tr>
                    <%-- <td>Select Region :</td>
                    <td>
                        <asp:DropDownList ID="ddBranch" Width="200px" runat="server"
                            DataSourceID="SqlDataSource2" onchange="PopulateLocation();" DataTextField="txt" DataValueField="val">
                        </asp:DropDownList>
                    </td>
                     <td>View :
                    </td>
                    <td style="display:none">
                        <asp:DropDownList ID="ddview" onchange="return hideshow();" runat="server" Width="200px">
                            <asp:ListItem Value="1" Text="Officers"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Sites"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Officers and Sites"></asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                </tr>
                <tr>
                    <td id="tdo1">
                        Select Officer :
                    </td>
                    <td id="tdo2">
                        <%--<asp:DropDownList ID="ddofficer" runat="server" Width="200px">
                            <asp:ListItem Value="%">ALL</asp:ListItem>
                        </asp:DropDownList>--%>
                      <asp:DropDownList ID="ddofficer" DataValueField="userid" DataTextField="username"
                    DataSourceID="SqlDataSource1"
                    runat="server">
                </asp:DropDownList>
                    <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                    SelectCommand="SELECT [userid],[username] FROM [users] where userid<>'admin' and status <> 'DEL' order by userid"></asp:SqlDataSource>
                    
                    --%>
                    
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                    SelectCommand="select '%' as userid ,'ALL' as username union all SELECT [userid],[username] FROM [users] where userid<>'admin' and role in ('AO','DE') and RM like @rm ">
                    <SelectParameters><asp:SessionParameter Name="rm" DefaultValue="%" SessionField="Sess_username" /></SelectParameters>
                </asp:SqlDataSource>
                <br />
                    </td>
                    <%-- <td id="tds1">Site Type :
                    </td>
                    <td id="tds2">
                        <asp:DropDownList ID="ddsitytype" runat="server" Width="200px">
                            <asp:ListItem Value="%" Text="ALL"></asp:ListItem>
                            <asp:ListItem Value="CMS" Text="CMS"></asp:ListItem>
                            <asp:ListItem Value="NON CMS" Text="NON-CMS"></asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                    <td id="tds1">
                        Date :
                    </td>
                    <td id="tds2">
                        <asp:TextBox ID="txt_frmDate" Height="20px" Width="138px" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <ajaxtoolkit:calendarextender id="defaultCalendarExtender" runat="server" format="MM/dd/yyyy"
                            targetcontrolid="txt_frmDate" />
                    </td>
                    <td>
                        <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" OnClick="btn_search_Click1"
                            Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" BorderWidth="0px"
                            BackColor="#003366" ForeColor="White" Text="Locate" />
                        <asp:Button ID="btn_play" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                            runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Track Movement"
                            Visible="false" />
                    </td>
                </tr>
            </table>
            <%-- <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"></asp:SqlDataSource>--%>
            <br />
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="No records found pertaining to your search. Please select other search criteria."
                Visible="False"></asp:Label>
           <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                SelectCommand="SELECT distinct [userid] FROM [users] where userid<>'admin' order by userid">
            </asp:SqlDataSource>--%>
        </div>
        <br />
        <div align="center">
            <%--<script type="text/javascript" src="js/jquery.js"></script>
<script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAS-EOAynYI4wmkqHVwxyNgzD6FChbFCEs&callback=initMap"
  type="text/javascript"></script>  --%>
            <%--<div id="legend"><h3 style="text-align:center;">Legend</h3></div>--%>
            <div id="legend" style="top: 400px; z-index: 1051; position: absolute; right: 0px;
                font-size: 11px;">
                <h3 style="text-align: center;">
                    Legend</h3>
                <div>
                    <img src="./Image/man-green.png">
                    User (online)
                    <br>
                    <br>
                </div>
                <div>
                    <img src="./Image/man-red.png">
                    User (Offline)
                    <br>
                    <br>
                </div>
                <div>
                    <img src="./Image/man-orange.png">
                    User (Battery Low)
                    <br>
                    <br>
                </div>
            </div>
            <div id="mapstyle" style="font-size: 12px; font-family: Helvetica Neue,Helvetica,Arial,sans-serif;
                line-height: 1.42857143; color: #333;">
                <div id="geo-marker">
                </div>
                <div id="popup" class="ol-popup">
                    <a href="#" id="popup-closer" class="ol-popup-closer"></a>
                    <div id="popup-content">
                    </div>
                </div>
                <div id="popupinfo">
                </div>
                <div id="map" runat="server" clientidmode="Static" style="height: 500px;">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
