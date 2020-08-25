<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSiteUploadV6.aspx.cs" Inherits="Mphasis_webapp.RM.NewSiteUploadV6" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <script type="text/javascript" lang="javascript">
        function ShowModal() {

            $find('pop').show();
        }
    </script>

    <script>
        function write_to_excel() {
            window.location.href = "DownloadLog.aspx";
            debugger;
            setTimeout(function () {
                //window.location.href = "UploadTarget.aspx";
                document.getElementById("lblmsg").innerText = "";

            }, 1000);
        }
    </script>

    <style type="text/css">
        .lbl
        {
            text-align: right;
            font-size: 12px;
            font-family: Arial;
            background-color: #D9D9D9;
        }
        .Button
        {
            background-color: #6666FF;
            font-weight: bold;
            color: white;
            -webkit-border-radius: 20px;
            border-radius: 5px;
            cursor: pointer;
        }
        .confirm-dialog
        {
            margin: 0px auto;
            width: 329px;
            padding-top: 14px;
            position: relative;
            border: orange solid 1px;
            background-color: white;
            top: 9px;
            left: 0px;
            height: 80px;
        }
        .confirm-dialog .inner
        {
            padding-right: 20px;
            padding-left: 20px;
            padding-bottom: 11px;
            float: left;
            margin: 0px 0px -20px 0px;
            width: 290px;
            padding-top: 0px;
        }
        .confirm-dialog H2
        {
            font-weight: bold;
            font-size: 1.25em;
            color: #000000;
            text-align: center;
        }
        .confirm-dialog input
        {
        }
        .confirm-dialog .base
        {
            padding-bottom: 4px;
            margin-left: -11px;
            margin-right: -11px;
            padding-top: 4px;
            text-align: center;
        }
        .modalBackground
        {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .autocomplete
        {
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
        #imgclose
        {
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div style="text-align: center">
        <p style="color: #003366; font-variant: small-caps; font-size: x-large; font-weight: bolder;
            text-align: left">
            ADD NEW SCHEDULER</p>
        <table style="width: 100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid; border-width: thin; text-align: center">
                    <span style="color: Red">*</span>Attach Excel file :
                    <asp:FileUpload ID="fileuploadExcel" runat="server" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSend" runat="server" Text="Import" OnClick="btnUpload_Click" CssClass="Button"
                        ClientIDMode="Static"  />
                    <asp:Button ID="btn_Submit0" runat="server" Style="display: none" />
                    &nbsp;&nbsp; Select Month :
                    <asp:DropDownList runat="server" ID="ddlmonth">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lnkformat" Text="Site Upload Excel Format" OnClick="lnkformat_Click"></asp:LinkButton>
                </td>
            </tr>
            
            <tr>
                <td colspan="4" style="text-align:center;color:Green;font-weight:bold">
                <br />
                    <asp:Label runat="server" ID="lblmsg" ClientIDMode="Static"></asp:Label><br />
                </td>
            </tr>
            
            <tr style="text-align: center">
                <td colspan="4">
                    <fieldset>
                        <legend>Log</legend>
                        <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lblsucc" runat="server" ForeColor="Green"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="true"
                            AllowPaging="false" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <%--<asp:BoundField DataField="USERID" HeaderText="USERID" />
                          <asp:BoundField DataField="SITEID" HeaderText="SITEID" />
                           <asp:BoundField DataField="DATE" HeaderText="DATE" />
                             <asp:BoundField DataField="TYPE" HeaderText="TYPE" />  --%>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" Font-Bold="true" />
                            <PagerStyle HorizontalAlign="Center" BackColor="#8DB4E2" />
                            <RowStyle HorizontalAlign="Center" ForeColor="#333333" />
                            <AlternatingRowStyle HorizontalAlign="Center" />
                            <sortedascendingcellstyle backcolor="#E9E7E2" />
                            <sortedascendingheaderstyle backcolor="#506C8C" />
                            <sorteddescendingcellstyle backcolor="#FFFDF8" />
                            <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                        </asp:GridView>
                        <asp:GridView ID="GridView2" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
                            EnableViewState="False" AllowPaging="false" OnPageIndexChanging="GridView1_PageIndexChanging">
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
                            <sortedascendingcellstyle backcolor="#E9E7E2" />
                            <sortedascendingheaderstyle backcolor="#506C8C" />
                            <sorteddescendingcellstyle backcolor="#FFFDF8" />
                            <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                        </asp:GridView>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
    </div>
    <div id="mod" runat="server" style="text-align: center;">
        <p style="color: #003366; font-variant: small-caps; font-size: x-large; font-weight: bolder;
            text-align: left">
            DELETE SCHEDULER</p>
        <table style="width: 100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid; border-width: thin; text-align: center">
                    <span style="color: Red">*</span>Attach Excel file :
                    <asp:FileUpload ID="mod_fileuploadExcel" runat="server" />
                    &nbsp;&nbsp;
                    <asp:Button ID="mod_btnSend" runat="server" Text="Import" OnClick="mod_btnUpload_Click"
                        CssClass="Button" ClientIDMode="Static"/>
                    <asp:Button ID="mod_btn_Submit0" runat="server" Style="display: none" />
                    &nbsp;&nbsp; Modify Scheduler for the Month
                    <asp:DropDownList ID="dd_month" runat="server">
                    </asp:DropDownList>
                    <asp:LinkButton runat="server" ID="mod_lnkformat" Text=" Go" OnClick="mod_lnkformat_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr style="text-align: left">
                <td colspan="4">
                    <fieldset>
                        <legend>Log</legend>
                        <asp:Label ID="mod_lblerror" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="mod_lblsucc" runat="server" ForeColor="Green"></asp:Label>
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="grid_mod" runat="server" Style="display: none" AllowPaging="false"
        AllowSorting="false">
    </asp:GridView>
    <asp:SqlDataSource ID="sql_mod" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
        runat="server"></asp:SqlDataSource>
    <div>
        <asp:ModalPopupExtender ID="m2" BehaviorID="pop" TargetControlID="btn_Submit0" PopupControlID="pnl"
            runat="server" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnl" runat="server" CssClass="confirm-dialog">
            <div>
                <h2>
                    Server Is Busy. Please Upload Your Scheduler Later...
                </h2>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
