<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LocatorOne.aspx.cs" Inherits="Mphasis_webapp.CH.LocatorOne" EnableEventValidation="false" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times;font-size: xx-large; font-weight: bolder;">
       <asp:Label ID="lblusername" runat="server"></asp:Label>
    </div>

    <div align="left">
        <br />
    </div>

    <div style="font-size:x-large;font-family: Cambria, Cochin, Georgia, Times; text-align:left;">
        <%--  <asp:UpdatePanel ID="u" runat="server">
            <ContentTemplate>--%>
        <table width="100%" cellpadding="0" cellspacing="0" style="">
            <div >
                Select Officer : <asp:DropDownList ID="ddofficer" DataValueField="userid" DataTextField="userid" DataSourceID="SqlDataSource1"
                    runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp; 
                 Date :        
                &nbsp;&nbsp;       
          <asp:TextBox ID="txt_frmDate" Height="20px" Width="138px" runat="server" ClientIDMode="Static"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy"
                    TargetControlID="txt_frmDate" />
      
                <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" OnClick="btn_search_Click" Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Locate" />

                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                    SelectCommand="SELECT distinct [userid] FROM [users] where userid<>'admin' and  CH like @rm order by userid">
                    <SelectParameters><asp:SessionParameter Name="rm" DefaultValue="%" SessionField="Sess_username" /></SelectParameters>
                </asp:SqlDataSource>
                <br />


            </div>
        </table>

    </div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr style="font-size: x-large;">
                <td>
                <asp:Label ID="Label1" runat="server" ForeColor="Red"
                    Text="No records found pertaining to your search. Please select other search criteria."
                    Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>

                <td style="width: 100%;" valign="top">

                    <body >
                        &nbsp;&nbsp;                       
                        <div>
                            <%--  <div id="map-canvas" style="width: 450px; height: 400px; float: left;">
                                <% Response.Write(Session["sess_x"]);%>
                            </div>--%>
                            
                            <div id="basicMap" style="width: 1100px; height: 400px;text-align:center ">
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
</asp:Content>
