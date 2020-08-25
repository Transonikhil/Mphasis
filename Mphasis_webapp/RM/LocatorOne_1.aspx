<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocatorOne_1.aspx.cs" Inherits="Mphasis_webapp.RM.LocatorOne_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <script type="text/javascript">

        function windowOnLoad() {
            document.getElementById('txt_frmDate').readOnly = true;
            //document.getElementById('txt_toDate').readOnly = true;
        }

        window.onload = windowOnLoad;
    </script>
    <%--    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true"></script>
    --%>
    <%-- <script src="https://maps.googleapis.com/maps/api/js?&callback=initMap&signed_in=true" async defer> </script>--%>


    <link rel="stylesheet" href=../js/ol.css" type="text/css">
    <!-- The line below is only needed for old environments like Internet Explorer and Android 4.x -->
    <script src="https://cdn.polyfill.io/v2/polyfill.min.js?features=requestAnimationFrame,Element.prototype.classList,URL"></script>

    <script src="../js/ol.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style>
        #loading-image {
            position: absolute;
            left: 43%;
            top: 40%;
            z-index: 1055;
        }

        #geo-marker {
            width: 10px;
            height: 10px;
            border: 1px solid #088;
            border-radius: 5px;
            background-color: #0b968f;
            opacity: 0.8;
        }

        .popover {
            width: 300px;
        }

        .popover {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 1060;
            display: none;
            max-width: 276px;
            padding: 1px;
            font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
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

        .popover-content {
            padding: 9px 14px;
        }

        .popover.top > .arrow {
            bottom: -11px;
            left: 50%;
            margin-left: -11px;
            border-top-color: #999;
            border-top-color: rgba(0,0,0,.25);
            border-bottom-width: 0;
        }

        .popover > .arrow {
            border-width: 11px;
        }

            .popover > .arrow, .popover > .arrow:after {
                position: absolute;
                display: block;
                width: 0;
                height: 0;
                border-color: transparent;
                border-style: solid;
            }

        .popover.top > .arrow:after {
            bottom: 1px;
            margin-left: -10px;
            content: " ";
            border-top-color: #fff;
            border-bottom-width: 0;
        }

        .popover > .arrow:after {
            content: "";
            border-width: 10px;
        }

        .popover > .arrow, .popover > .arrow:after {
            position: absolute;
            display: block;
            width: 0;
            height: 0;
            border-color: transparent;
            border-style: solid;
        }
    </style>
    <style type="text/css">
        html,
        body,
        #mymap {
            width: 100%;
            height: 100%;
        }

        #geo-marker {
            width: 10px;
            height: 10px;
            border: 1px solid #088;
            border-radius: 5px;
            background-color: #0b968f;
            opacity: 0.8;
        }

        .popover {
            width: 300px;
        }
    </style>
    <style type="text/css">
        .ol-popup {
            position: absolute;
            background-color: white;
            -webkit-filter: drop-shadow(0 1px 4px rgba(0,0,0,0.2));
            filter: drop-shadow(0 1px 4px rgba(0,0,0,0.2));
            padding: 15px;
            border-radius: 10px;
            border: 1px solid #cccccc;
            bottom: 12px;
            left: -50px;
            min-width: 280px;
            text-align: left;
        }

            .ol-popup:after, .ol-popup:before {
                top: 100%;
                border: solid transparent;
                content: " ";
                height: 0;
                width: 0;
                position: absolute;
                pointer-events: none;
            }

            .ol-popup:after {
                border-top-color: white;
                border-width: 10px;
                left: 48px;
                margin-left: -10px;
            }

            .ol-popup:before {
                border-top-color: #cccccc;
                border-width: 11px;
                left: 48px;
                margin-left: -11px;
            }

        .ol-popup-closer {
            text-decoration: none;
            position: absolute;
            top: 2px;
            right: 8px;
        }

            .ol-popup-closer:after {
                content: "✖";
                cursor: pointer;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <table>
        <tr>
            <td style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-size: x-large; font-weight: bolder;">
                <asp:Label ID="lblusername" runat="server"></asp:Label>
            </td>
            <td style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-size: x-large; font-weight: bolder; margin-left: 245px; float: right">
                <asp:Label ID="lblDateTime" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; text-align: left;">
        <%--  <asp:UpdatePanel ID="u" runat="server">
            <ContentTemplate>--%>
        <table width="100%" cellpadding="0" cellspacing="0">
            <div class="style1">
                Select Engineer :
                <asp:DropDownList ID="ddofficer" DataValueField="userid" DataTextField="username"
                    DataSourceID="SqlDataSource1"
                    runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp; 
                 Date :        
                &nbsp;&nbsp;       
          <asp:TextBox ID="txt_frmDate" Height="20px" Width="138px" runat="server" ClientIDMode="Static"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy"
                    TargetControlID="txt_frmDate" />


                <asp:Label ID="Label1" runat="server" ForeColor="Red"
                    Text="No records found pertaining to your search. Please select other search criteria."
                    Visible="False"></asp:Label>
                &nbsp;&nbsp;  
             From Time :
        &nbsp;&nbsp;       
      <asp:DropDownList ID="ddFromTime" CssClass="btn btn-default dropdown-toggle" AppendDataBoundItems="true" runat="server">
          <asp:ListItem>00:00</asp:ListItem>
          <asp:ListItem>00:30</asp:ListItem>
          <asp:ListItem>01:00</asp:ListItem>
          <asp:ListItem>01:30</asp:ListItem>
          <asp:ListItem>02:00</asp:ListItem>
          <asp:ListItem>02:30</asp:ListItem>
          <asp:ListItem>03:00</asp:ListItem>
          <asp:ListItem>03:30</asp:ListItem>
          <asp:ListItem>04:00</asp:ListItem>
          <asp:ListItem>04:30</asp:ListItem>
          <asp:ListItem>05:00</asp:ListItem>
          <asp:ListItem>05:30</asp:ListItem>
          <asp:ListItem>06:00</asp:ListItem>
          <asp:ListItem>06:30</asp:ListItem>
          <asp:ListItem>07:00</asp:ListItem>
          <asp:ListItem>07:30</asp:ListItem>
          <asp:ListItem>08:00</asp:ListItem>
          <asp:ListItem>08:30</asp:ListItem>
          <asp:ListItem>09:00</asp:ListItem>
          <asp:ListItem>09:30</asp:ListItem>
          <asp:ListItem>10:00</asp:ListItem>
          <asp:ListItem>10:30</asp:ListItem>
          <asp:ListItem>11:00</asp:ListItem>
          <asp:ListItem>11:30</asp:ListItem>
          <asp:ListItem>12:00</asp:ListItem>
          <asp:ListItem>12:30</asp:ListItem>
          <asp:ListItem>13:00</asp:ListItem>
          <asp:ListItem>13:30</asp:ListItem>
          <asp:ListItem>14:00</asp:ListItem>
          <asp:ListItem>14:30</asp:ListItem>
          <asp:ListItem>15:00</asp:ListItem>
          <asp:ListItem>15:30</asp:ListItem>
          <asp:ListItem>16:00</asp:ListItem>
          <asp:ListItem>16:30</asp:ListItem>
          <asp:ListItem>17:00</asp:ListItem>
          <asp:ListItem>17:30</asp:ListItem>
          <asp:ListItem>18:00</asp:ListItem>
          <asp:ListItem>18:30</asp:ListItem>
          <asp:ListItem>19:00</asp:ListItem>
          <asp:ListItem>19:30</asp:ListItem>
          <asp:ListItem>20:00</asp:ListItem>
          <asp:ListItem>20:30</asp:ListItem>
          <asp:ListItem>21:00</asp:ListItem>
          <asp:ListItem>21:30</asp:ListItem>
          <asp:ListItem>22:00</asp:ListItem>
          <asp:ListItem>22:30</asp:ListItem>
          <asp:ListItem>23:00</asp:ListItem>
          <asp:ListItem>23:30</asp:ListItem>
      </asp:DropDownList>

                &nbsp;&nbsp;
        To Time :        
        &nbsp;&nbsp;
         <asp:DropDownList ID="ddToTime" CssClass="btn btn-default dropdown-toggle" AppendDataBoundItems="true" runat="server">
             <asp:ListItem>00:30</asp:ListItem>
             <asp:ListItem>01:00</asp:ListItem>
             <asp:ListItem>01:30</asp:ListItem>
             <asp:ListItem>02:00</asp:ListItem>
             <asp:ListItem>02:30</asp:ListItem>
             <asp:ListItem>03:00</asp:ListItem>
             <asp:ListItem>03:30</asp:ListItem>
             <asp:ListItem>04:00</asp:ListItem>
             <asp:ListItem>04:30</asp:ListItem>
             <asp:ListItem>05:00</asp:ListItem>
             <asp:ListItem>05:30</asp:ListItem>
             <asp:ListItem>06:00</asp:ListItem>
             <asp:ListItem>06:30</asp:ListItem>
             <asp:ListItem>07:00</asp:ListItem>
             <asp:ListItem>07:30</asp:ListItem>
             <asp:ListItem>08:00</asp:ListItem>
             <asp:ListItem>08:30</asp:ListItem>
             <asp:ListItem>09:00</asp:ListItem>
             <asp:ListItem>09:30</asp:ListItem>
             <asp:ListItem>10:00</asp:ListItem>
             <asp:ListItem>10:30</asp:ListItem>
             <asp:ListItem>11:00</asp:ListItem>
             <asp:ListItem>11:30</asp:ListItem>
             <asp:ListItem>12:00</asp:ListItem>
             <asp:ListItem>12:30</asp:ListItem>
             <asp:ListItem>13:00</asp:ListItem>
             <asp:ListItem>13:30</asp:ListItem>
             <asp:ListItem>14:00</asp:ListItem>
             <asp:ListItem>14:30</asp:ListItem>
             <asp:ListItem>15:00</asp:ListItem>
             <asp:ListItem>15:30</asp:ListItem>
             <asp:ListItem>16:00</asp:ListItem>
             <asp:ListItem>16:30</asp:ListItem>
             <asp:ListItem>17:00</asp:ListItem>
             <asp:ListItem>17:30</asp:ListItem>
             <asp:ListItem>18:00</asp:ListItem>
             <asp:ListItem>18:30</asp:ListItem>
             <asp:ListItem>19:00</asp:ListItem>
             <asp:ListItem>19:30</asp:ListItem>
             <asp:ListItem>20:00</asp:ListItem>
             <asp:ListItem>20:30</asp:ListItem>
             <asp:ListItem>21:00</asp:ListItem>
             <asp:ListItem>21:30</asp:ListItem>
             <asp:ListItem>22:00</asp:ListItem>
             <asp:ListItem>22:30</asp:ListItem>
             <asp:ListItem>23:00</asp:ListItem>
             <asp:ListItem>23:30</asp:ListItem>
             <asp:ListItem>23:59</asp:ListItem>
         </asp:DropDownList>
                <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" OnClick="btn_search_Click" Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Locate" />

                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                    
                    SelectCommand="SELECT [userid],[username] FROM [users] where userid<>'admin' and  status <> 'DEL'  and   role in ('AO','DE') and RM like @rm order by userid">
                    
                                        <SelectParameters><asp:SessionParameter Name="rm" DefaultValue="%" SessionField="Sess_username" /></SelectParameters>
                    </asp:SqlDataSource>
                <br />


            </div>
        </table>

   <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 100%;" valign="top">
                    <body>
                        &nbsp;&nbsp;
                        <div style="font-size:12px;font-family:Helvetica Neue,Helvetica,Arial,sans-serif;line-height:1.42857143;color:#333;">
                            <%--  <div id="map-canvas" style="width: 450px; height: 400px; float: left;">
                                <% Response.Write(Session["sess_x"]);%>
                            </div>--%><%--
                            <script src="https://maps.googleapis.com/maps/api/js?&callback=initMap&signed_in=true" async defer> </script>--%>

                           <%-- <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAS-EOAynYI4wmkqHVwxyNgzD6FChbFCEs&callback=initMap"
                                type="text/javascript"></script>--%>
                             <div id="geo-marker"></div>
            <div id="popup" class="ol-popup">
                <a href="#" id="popup-closer" class="ol-popup-closer"></a>
                <div id="popup-content"></div>
            </div>
            <div id="popupinfo"></div>
                            <div id="map" style="width: 950px; height: 400px; text-align: center">
                                <% Response.Write(Session["sess_x"]);%>
                            </div>
                        </div>
                    </body>
                </td>
            </tr>
        </table>

        <%--</ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="ImageButton1" />
             </Triggers>
    </asp:UpdatePanel>--%>
        <hr />
    </div>
</asp:Content>
