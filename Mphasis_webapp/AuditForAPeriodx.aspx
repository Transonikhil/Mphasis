<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuditForAPeriodx.aspx.cs" Inherits="Mphasis_webapp.AuditForAPeriodx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
     <script type="text/javascript">
     
        function windowOnLoad() {
            document.getElementById('txt_frmDate').readOnly = true;
            document.getElementById('txt_toDate').readOnly = true;
        }

        window.onload = windowOnLoad;
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;"><strong>Customer Wise - Bank Wise Audit Reports For A Period</strong></div>
    <br />
            <br />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
             <div style="font-size:x-large;font-family:Cambria, Cochin, Georgia, Times; font-variant:small-caps; width:100%">
                 <table width="100%">
                     <tr>
                         <td class="auto-style1">Select Customer: </td>
                         <td class="auto-style1">
                             <asp:DropDownList ID="dd_cust" runat="server" Width="320px" DataSourceID="SqlDataSource2" DataTextField="client" DataValueField="client">
                             </asp:DropDownList>
                             <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select distinct client from atms"></asp:SqlDataSource>
                             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select distinct bankid from atms"></asp:SqlDataSource>
                         </td>
                         <td class="auto-style1">Select Bank: </td>
                         <td class="auto-style1">
                             <asp:DropDownList ID="dd_bank" runat="server" Width="320px" DataSourceID="SqlDataSource1" DataTextField="bankid" DataValueField="bankid">
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td>From Date: </td>
                         <td>
                             <asp:TextBox ID="txt_frmDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                             <ajaxToolkit:RoundedCornersExtender ID="txt_frmdate_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_frmdate">
                             </ajaxToolkit:RoundedCornersExtender>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_frmdate" />
                         </td>
                         <td>To Date: </td>
                         <td>
                             <asp:TextBox ID="txt_toDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                             <ajaxToolkit:RoundedCornersExtender ID="txt_todate_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_todate">
                             </ajaxToolkit:RoundedCornersExtender>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_todate" />
                         </td>
                     </tr>
                     <tr>
                         <td></td>
                         <td style="padding-top:0.5em;">
                             <asp:Button ID="btn_Update" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search" OnClick="btn_Update_Click" />
                             <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageUrl="~/Image/EXCEL.ICO" PostBackUrl="~/AuditForAPeriod.aspx?export=true" Width="20px" />
                         </td>
                     </tr>
                 </table>
                 &nbsp;&nbsp;&nbsp;
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
                 <div align="center">
                     <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="GridView1_PageIndexChanging"
                AllowPaging ="true" PageSize = "12" 
                CellPadding="4" Font-Size="10pt" 
                    ForeColor="#333333" GridLines="None" AllowSorting="False" Width="909px" 
                    style="margin-top: 0px">
                         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                         <EditRowStyle BackColor="#999999" />
                         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                         <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                         <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" 
                        VerticalAlign="Middle" />
                         <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                         <SortedAscendingCellStyle BackColor="#E9E7E2" />
                         <SortedAscendingHeaderStyle BackColor="#506C8C" />
                         <SortedDescendingCellStyle BackColor="#FFFDF8" />
                         <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                         <Columns>
                             <asp:HyperLinkField DataTextField="" DataNavigateUrlFields="VID" 
                            DataNavigateUrlFormatString="Report.aspx?auditid={0}&dnld_excel=Y" Text="Excel" 
                            HeaderText="Report" />
                             <asp:HyperLinkField DataTextField="" DataNavigateUrlFields="VID" 
                            DataNavigateUrlFormatString="photos.aspx?auditid={0}&dnld_pdf=Y" Text="Photos" 
                            HeaderText="Download" />
                             <asp:HyperLinkField DataTextField="VID" DataNavigateUrlFields="VID" 
                            DataNavigateUrlFormatString="MainPage1.aspx?auditid={0}" Text="Visit ID" 
                            HeaderText="Visit ID" />
                         </Columns>
                     </asp:GridView>
                 </div>
                 <br />
                 <hr />
             </div>
         </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
