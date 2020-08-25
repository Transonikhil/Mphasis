<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FetchData.aspx.cs" Inherits="Mphasis_webapp.FetchData" %>

<!DOCTYPE html>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function ShowModal() {

            $find('pop').show();
        }


	  </script>
</head>
<body style="font-family: Cambria, Cochin, Georgia, Times">

    <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <table id="Table1" style="width: 100%; text-align: center" runat="server">
                <tr>
                    <td>QUERY : <asp:Label ID="lblselect" runat="server" Text="Select "></asp:Label>
                        <asp:TextBox ID="txtsearch" runat="server" Width="500px"></asp:TextBox>
                        <asp:Button ID="BtnExecute" runat="server" Text="EXECUTE" Style="cursor: pointer" Font-Names="Cambria, Cochin, Georgia, Times" BorderWidth="0px" BackColor="#6699ff" Height="23px" Width="100px" Font-Size="15px" ForeColor="White" CausesValidation="False" EnableViewState="False" UseSubmitBehavior="False" ClientIDMode="Static"  OnClick="BtnExecute_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 30px">
        </div>
        <div id="Div1" style="width:1250px;overflow-x: scroll; overflow-y: scroll" runat="server">
            <table id="Table2" style="width: 100%; text-align: center" runat="server">
                <tr>
                    <td>
                        <asp:Label ID="lblcount" runat="server" Text=""></asp:Label> <asp:Button ID="btnExtract" runat="server" Text="EXTRACT" Style="cursor: pointer" Font-Names="Cambria, Cochin, Georgia, Times" BorderWidth="0px" BackColor="#6699ff" Height="23px" Width="100px" Font-Size="15px" ForeColor="White" CausesValidation="False" EnableViewState="False" UseSubmitBehavior="False"  OnClick="btnExtract_Click" />
                        
                    </td>
                </tr>
                <tr >
                    <td>
                        <asp:GridView ID="GridView1" runat="server" allowpaging="true" page-size="10" EnableSortingAndPagingCallbacks="true" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            
        </div>
        <div style="text-align:center">
         
    </div>
    </form>
</body>
</html>
