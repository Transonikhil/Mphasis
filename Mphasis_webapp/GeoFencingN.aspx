<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GeoFencingN.aspx.cs" Inherits="Mphasis_webapp.GeoFencingN" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps;
        font-size: xx-large; font-weight: bolder;">
        <strong>Officer Wise Geofencing</strong>
    </div>
    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; width: 100%;
        font-variant: small-caps">
        <table width="90%">
            <tr>
                <td>
                    Select Officer:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="320px" DataSourceID="SqlDataSource1"
                        DataTextField="userid" DataValueField="userid">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="SELECT distinct [userid] FROM [users] where userid&lt;&gt;'admin' order by userid">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_frmdate" Height="20px" Width="320px" runat="server"></asp:TextBox>
                    <ajaxtoolkit:roundedcornersextender id="txt_frmdate_RoundedCornersExtender" runat="server"
                        enabled="True" radius="10" targetcontrolid="txt_frmdate">
                                </ajaxtoolkit:roundedcornersextender>
                               <%--  <ajaxToolkit:calendarextender id="defaultCalendarExtender" runat="server" format="MM/dd/yyyy"
                targetcontrolid="txt_frmDate" />--%>
                                
                </td>
                <td>
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_todate" Height="20px" Width="320px" runat="server"></asp:TextBox>
                    <ajaxtoolkit:roundedcornersextender id="txt_todate_RoundedCornersExtender" runat="server"
                        enabled="True" radius="10" targetcontrolid="txt_todate">
                                </ajaxtoolkit:roundedcornersextender>
                                <%-- <ajaxToolkit:calendarextender id="defaultCalendarExtender1" runat="server" format="MM/dd/yyyy"
                targetcontrolid="txt_toDate" />--%>
                             
                </td>
            </tr>
            
            
                <td>
                </td>
                <td style="padding-top: 0.5em">
                    <asp:Button ID="Button1" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                        runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search" />
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" Width="20px" Style="text-align: left"
                        ImageUrl="~/Image/EXCEL.ICO" OnClick="ImageButton1_Click" />
                        
                </td>
            </tr>
        </table>
        <div align="center">
            <ajaxToolkit:calendarextender id="defaultCalendarExtender" runat="server" format="MM/dd/yyyy"
                targetcontrolid="txt_frmDate" />
            <ajaxToolkit:calendarextender id="defaultCalendarExtender1" runat="server" format="MM/dd/yyyy"
                targetcontrolid="txt_toDate" />
        </div>
        <div align="center">
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="No records found pertaining to your search. Please select other search criteria."
                Visible="False"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Green" Visible="False"></asp:Label>
        </div>
        <div align="center">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="12" CellPadding="4"
                Font-Size="10pt" ForeColor="#333333" GridLines="None" AllowSorting="false" Width="909px"
                Style="margin-top: 0px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <sortedascendingcellstyle backcolor="#E9E7E2" />
                <sortedascendingheaderstyle backcolor="#506C8C" />
                <sorteddescendingcellstyle backcolor="#FFFDF8" />
                <sorteddescendingheaderstyle backcolor="#6F8DAE" />
            </asp:GridView>
        </div>
        <hr />
    </div>
</asp:Content>
