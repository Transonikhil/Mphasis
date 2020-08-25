<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XCount.aspx.cs" Inherits="Mphasis_webapp.XCount" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
        <div align="center">
            <table cellpadding="0" cellspacing="0" width="40%">
                <tr>
                    <td>Date :
                        <asp:TextBox ID="txtfromdate" runat="server" Width="175px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="MM/dd/yyyy" Enabled="True" TargetControlID="txtfromdate">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="Button" Width="133px" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div align="center">
            <asp:SqlDataSource runat="server"  ID="sql" />
            <br />
            <asp:GridView ID="GridView1" ShowFooter="true" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
				
				
				
            </asp:GridView>
        </div>
    </form>
</body>
</html>
