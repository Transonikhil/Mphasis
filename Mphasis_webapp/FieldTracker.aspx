<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FieldTracker.aspx.cs" Inherits="Mphasis_webapp.FieldTracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .style3
        {
            width: 100%;
            vertical-align:central;
        }
        .style8
        {
            text-align: center;
            width: 221px;
            height: 123px;
            vertical-align:central;
        }
        .style9
        {
            font-size: small;
            font-family: Arial;
        }
        .style10
        {
            height: 123px;
        }
        .auto-style2 {
            height: 123px;
            text-align: left;
            padding-left:2em;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Panel ID="master_panel" runat="server" CssClass="animated fadeInLeft">
    <div>
    <table class="style3">
        <tr>
            <td class="style8" valign="middle">
            
            <asp:Panel ID="Panel2" runat="server" BackImageUrl="~/Image/GreenBall.Jpg" Height="128px" Width="128px" Font-Bold="True" ForeColor="White" HorizontalAlign="Center">
                    <br />
                    <br />
                <div style="padding-top:0.7em;padding-right:0.5em">
                    <asp:Label ID="lbl_Online" runat="server" Font-Size="XX-Large" 
                        style="font-family: Arial" Text="18"></asp:Label>
                    </div>
                </asp:Panel>
            
                </td>
            <td class="style8">
                <asp:Panel ID="Panel1" runat="server" BackImageUrl="~/Image/RedBall.Jpg" Height="128px" Width="128px" Font-Bold="True" ForeColor="White">
                    <br />
                    <br />
                    <div style="padding-top:0.7em;padding-right:0.5em">
                    <asp:Label ID="lbl_Offline" runat="server" Font-Size="XX-Large" 
                        style="font-family: Arial" Text="18"></asp:Label>
                        </div>
                </asp:Panel>
            </td>
            <td class="style8">
                <asp:Panel ID="Panel3" runat="server" BackImageUrl="~/Image/BlueBall.Jpg" Height="128px" Width="128px" Font-Bold="True" ForeColor="White">
                    <br />
                    <br />
                    <div style="padding-top:0.7em;padding-right:0.5em">
                    <asp:Label ID="lbl_Battery" runat="server" Font-Size="XX-Large" 
                        style="font-family: Arial" Text="18"></asp:Label>
                        </div>
                </asp:Panel>                </td>
            <td class="auto-style2">
                <span class="style9"><strong>
                <asp:ImageButton ID="ImageButton2" runat="server" Height="114px" ImageUrl="~/Image/map.jpg" style="text-align: center; margin-left: 0px" Width="126px" PostBackURL="LocatorN_poly_1.aspx?userid=ALL" />
                <br />
                </strong></span> </td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>
                <a href="FieldTracker.aspx?online=True">Online</a></strong></td>
            <td>
                <span class="style9"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="FieldTracker.aspx?Offline=True">Offline</a></strong></span>
            </td>
            <td>
                <span class="style9"><strong><a href="FieldTracker.aspx?Battery=True">Battery Low / Down</a></strong></span></td>
            <td>
                <span class="style9"><strong>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong>
                <asp:Label ID="Label1" runat="server" Text="View Entire Field Force"></asp:Label>
                </span> </td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        <table class="style3">
        <tr>
            <td colspan="4">
                <div style="font-size:large;font-family:Cambria, Cochin, Georgia, Times; font-variant:small-caps">
                <asp:GridView ID="GridView1" runat="server" 
                CellPadding="4" BorderColor="#003366" BorderWidth="2px"
                    ForeColor="#333333" GridLines="None" Width="909px" 
                    style="margin-top: 0px" onrowdatabound="GridView1_RowDataBound1" onpageindexchanging="GridView1_PageIndexChanging">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="View On map" 
                            HeaderText="View On Map" Text="Map" />
                    </Columns>
                    </asp:GridView>&nbsp;
                    </div>
                    </td>
            
        </tr>
        </table>
</div>
</asp:Panel>

</asp:Content>
