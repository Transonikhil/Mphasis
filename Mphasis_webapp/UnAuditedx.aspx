<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnAuditedx.aspx.cs" Inherits="Mphasis_webapp.UnAuditedx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
     
        function windowOnLoad() {
            document.getElementById('txt_frmDate').readOnly = true;
            document.getElementById('txt_toDate').readOnly = true;
        }

        window.onload = windowOnLoad;
 </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;">
        unaudited sites for current month
         </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="font-size:x-large;font-family:Cambria, Cochin, Georgia, Times; font-variant:small-caps">
                <table>
                    <tr>
                        <td>From Date: </td>
                        <td>
                            <asp:TextBox ID="txt_frmDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:RoundedCornersExtender ID="txt_frmdate_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_frmdate">
                            </ajaxToolkit:RoundedCornersExtender>
                            <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_frmDate" />
                        </td>
                        <td>To Date: </td>
                        <td>
                            <asp:TextBox ID="txt_toDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:RoundedCornersExtender ID="txt_todate_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_todate">
                            </ajaxToolkit:RoundedCornersExtender>
                            <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_toDate" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="padding-top:0.5em">
                            <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search" OnClick="btn_search_Click" />
                            <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageUrl="~/Image/EXCEL.ICO" OnClick="btn_search_Click1" style="text-align: left" Width="20px" />
                        </td>
                    </tr>
                </table>
                <div align="center">
            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                <asp:Image ID="Image1" runat="server" ImageUrl="image/ajax-loader.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            </div>
                <div align="center">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                            Text="No records found pertaining to your search. Please select other search criteria." 
                            Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="Label3" runat="server" ForeColor="Green" 
                                                Visible="False"></asp:Label>
                </div>
                <br />
                <div align="center" style="font-size:large;font-family:Cambria, Cochin, Georgia, Times; font-variant:small-caps">
                
                    <asp:GridView ID="grid_unaudit" runat="server" AutoGenerateColumns="true" OnPageIndexChanging="grid_unaudit_PageIndexChanging"
                            CellPadding="4" ForeColor="#333333" 
                            GridLines="None" BorderColor="#003366" BorderWidth="3px"  >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    
                    
                    
                </div>
                <hr />
            </div>
        </ContentTemplate>
		<Triggers>
                 <asp:PostBackTrigger ControlID="ImageButton2" />
             </Triggers>
    </asp:UpdatePanel>
</asp:Content>
