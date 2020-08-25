<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelAddATM.aspx.cs" Inherits="Mphasis_webapp.Excel.ExcelAddATM" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" lang="javascript">
        function ShowModal() {

            $find('pop').show();
        }
    </script>
    <style type="text/css">
        .lbl {
            text-align: right;
            font-size: 12px;
            font-family: Arial;
            background-color: #D9D9D9;
        }

        .Button {
            background-color: #6666FF;
            font-weight: bold;
            color: white;
            -webkit-border-radius: 20px;
            border-radius: 5px;
            cursor: pointer;
        }

        .confirm-dialog {
            MARGIN: 0px auto;
            WIDTH: 329px;
            PADDING-TOP: 14px;
            POSITION: relative;
            border: orange solid 1px;
            background-color: white;
            top: 9px;
            left: 0px;
            height: auto;
        }

            .confirm-dialog .inner {
                PADDING-RIGHT: 20px;
                PADDING-LEFT: 20px;
                PADDING-BOTTOM: 11px;
                FLOAT: left;
                MARGIN: 0px 0px -20px 0px;
                WIDTH: 290px;
                PADDING-TOP: 0px;
            }

            .confirm-dialog H2 {
                FONT-WEIGHT: bold;
                FONT-SIZE: 1.25em;
                COLOR: #000000;
                TEXT-ALIGN: center;
            }

            .confirm-dialog input {
            }

            .confirm-dialog .base {
                PADDING-BOTTOM: 4px;
                MARGIN-LEFT: -11px;
                MARGIN-RIGHT: -11px;
                PADDING-TOP: 4px;
                TEXT-ALIGN: center;
            }

        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .autocomplete {
            margin: 0px;
            background-color: White;
            cursor: default;
            overflow-y: auto;
            overflow-x: hidden;
            max-height: 180px;
            max-width: 163px;
            border: goldenrod solid 1px;
            text-align: left;
            z-index: 10000;
        }

        #imgclose {
            height: 31px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="text-align: center">
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="ExcelUploader.aspx">HOME</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4" style="border-style: solid; border-width: thin; text-align: center">
                        <span style="color: Red">*</span>Attach Excel file :
                    <asp:FileUpload ID="fileuploadExcel" runat="server" />
                        &nbsp;&nbsp;
                    <asp:Button ID="btnSend" runat="server" Text="Import" OnClick="btnUpload_Click" CssClass="Button" ClientIDMode="Static" OnClientClick="ShowModal()" />
                        <asp:Button ID="btn_Submit0" runat="server" Style="display: none" />
                        &nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lnkformat" Text="Atm Id Upload Excel Format" OnClick="lnkformat_Click"></asp:LinkButton>
                    </td>

                </tr>
                <tr style="text-align: center">
                    <td colspan="4">
                        <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 15px">
        </div>
        <div style="width: 100%">
            <table style="width: 100%; text-align: center; font-size: small">



                <tr>
                    
                    <td>
                        <div style="height: 500px; width:400px;overflow-x: scroll; overflow-y: scroll">

                            <table>
                                <tr id="countAtmId" runat="server">
                                    <td>
                                        <asp:Label runat="server" ID="lblnum"  Font-Size="15px" Font-Bold="True">Number Of ATM ID Uploaded :</asp:Label>
                                        <asp:Label runat="server" ID="lbladdcount" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="lnkaddexport" runat="server" OnClick="lnkaddexport_Click">Export</asp:LinkButton>
                                    </td>
                                </tr>

                            </table>
                            <asp:GridView ID="GridView1" Width="100%" runat="server" CssClass="alignment" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" GridLines="None" CellSpacing="1">
                                <EmptyDataTemplate>
                                    ATM ID Uploaded
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#33276A" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                        </div>
                    </td>
                    <td  >
                         <div style="height: 500px;width:400px; overflow-x: scroll; overflow-y: scroll">

                            <table>
                                <tr id="Tr1" runat="server">



                                    <td>
                                        <asp:Label runat="server" ID="Label1"  Font-Size="15px" Font-Bold="True">Number Of ATM ID Not Uploaded :</asp:Label>
                                        <asp:Label runat="server" ID="lblnotadd" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                                     <td>
                                        <asp:LinkButton ID="lnknotadd" runat="server" OnClick="lnknotadd_Click">Export</asp:LinkButton>
                                    </td>
                                </tr>

                            </table>
                            <asp:GridView ID="GridView2" Width="100%" runat="server" CssClass="alignment" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" GridLines="None" CellSpacing="1">
                                <EmptyDataTemplate>
                                    ATM ID Not Uploaded
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#33276A" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
                        </div>
                    </td>
                    <td >
                         <div style="height: 500px;width:400px; overflow-x: scroll; overflow-y: scroll">

                            <table>
                                <tr id="Tr2" runat="server">
                                    <td>
                                        <asp:Label runat="server" ID="Label3"  Font-Size="15px" Font-Bold="True">Number Of ATM ID Already Exists :</asp:Label>
                                        <asp:Label runat="server" ID="lblalreadycount" Font-Size="15px" Font-Bold="True"></asp:Label></td>
                                     <td>
                                        <asp:LinkButton ID="lnkalready" runat="server" OnClick="lnkalready_Click">Export</asp:LinkButton>
                                    </td>
                                </tr>

                            </table>
                            <asp:GridView ID="GridView3" Width="100%" runat="server" CssClass="alignment" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" GridLines="None" CellSpacing="1">
                                <EmptyDataTemplate>
                                    ATM ID Already Exists
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#33276A" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server"></asp:SqlDataSource>
                        </div>
                    </td>
                </tr>

            </table>

        </div>
        <div>
            <asp:ModalPopupExtender ID="m2" BehaviorID="pop" TargetControlID="btn_Submit0" PopupControlID="pnl" runat="server" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnl" runat="server" CssClass="confirm-dialog">


                <div>
                    <h2>Please Wait...
                        <br />
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/ajax-loader.gif" /></h2>
                </div>

            </asp:Panel>
        </div>
    </form>
</body>
</html>

