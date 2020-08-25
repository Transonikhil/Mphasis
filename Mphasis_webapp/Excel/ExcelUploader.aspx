<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelUploader.aspx.cs" Inherits="Mphasis_webapp.Excel.ExcelUploader" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Excel/ExcelUser.aspx">ADD USER</asp:HyperLink>
        &nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Excel/ExcelAddATM.aspx">ADD ATM</asp:HyperLink>
        &nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Excel/ExcelMapAtms.aspx">MAP ATM</asp:HyperLink>
        &nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Excel/MapAtmsToUsers.aspx">MULTIPLE MAP</asp:HyperLink>
        &nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Excel/DeleteATM.aspx">Inactive ATM</asp:HyperLink>
        &nbsp;&nbsp;&nbsp;        
        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Excel/DeleteMap.aspx">DeleteMap</asp:HyperLink>
        &nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Excel/DeleteUser.aspx">Deactive Users</asp:HyperLink>

       </div>
    </form>
</body>
</html>
