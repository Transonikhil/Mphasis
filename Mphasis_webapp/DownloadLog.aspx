<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadLog.aspx.cs" Inherits="Mphasis_webapp.DownloadLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div id="dvData">
            <asp:GridView ID="GridView2" CssClass="table2excel" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
                EnableViewState="False" AllowPaging="True">
                <Columns>
                    <asp:BoundField DataField="USERID" HeaderText="USERID" />
                    <asp:BoundField DataField="SITEID" HeaderText="SITEID" />
                    <asp:BoundField DataField="DATE" HeaderText="DATE" />
                    <asp:BoundField DataField="ISSUE" HeaderText="ISSUE" />
                </Columns>
                <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" Font-Bold="true" />
                <PagerStyle HorizontalAlign="Center" BackColor="#8DB4E2" />
                <RowStyle HorizontalAlign="Center" ForeColor="#333333" />
                <AlternatingRowStyle HorizontalAlign="Center" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
