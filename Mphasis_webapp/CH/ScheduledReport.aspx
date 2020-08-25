<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ScheduledReport.aspx.cs" Inherits="Mphasis_webapp.CH.ScheduledReport" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <style type="text/css">
        .auto-style1 {
            text-align: right;
        }
    </style>
    <link href="../Styles/auto_complete.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div style="color: #003366; font-family: Arial; font-size: x-large; font-weight: bolder;">
        SCHEDULE REPORT<br />
    </div>
    <br />
    <div align="center">
        <table width="80%">
            <tr>
                <td class="auto-style1">SITE ID:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txt_atmid" />
                    <asp:AutoCompleteExtender ID="txt_site_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                        Enabled="True" ServiceMethod="GetATM" ServicePath="" CompletionListCssClass="autocomplete"
                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass=".AutoExtenderList"
                        TargetControlID="txt_atmid" UseContextKey="True">
                    </asp:AutoCompleteExtender>
                </td>
                <td class="auto-style1">SCHEDULE REFERENCE NO.:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txt_refno" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">FROM DATE:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txt_frmDate" />
                </td>
                <td class="auto-style1">To DATE:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txt_toDate" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right">STATUS:
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="dd_status">
                        <asp:ListItem Text="ALL" Value="%" />
                        <asp:ListItem Text="NOT VISITED" Value="IS NULL" />
                        <asp:ListItem Text="VISITED" Value="IS NOT NULL" />
                    </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Button Text="Submit" runat="server" ID="btn_go" />
                    <asp:Button Text="Excel" runat="server" ID="btnexcel" />
                </td>
            </tr>
        </table>
        <br />
        <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_frmDate" />
        <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_toDate" />

        <div>
            <div style="width: 1000px; overflow: auto; font-size: medium">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Style="margin-top: 0px" 
                    Width="909px" BorderColor="#003366" BorderWidth="2px" EnableSortingAndPagingCallbacks="true" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" PageSize="20">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
        <asp:SqlDataSource runat="server" ID="sql" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" />
    </div>
</asp:Content>
