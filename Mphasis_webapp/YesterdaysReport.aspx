<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="YesterdaysReport.aspx.cs" Inherits="Mphasis_webapp.YesterdaysReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script type="text/javascript">

        function windowOnLoad() {
            document.getElementById('txtfromdate').readOnly = true;
            document.getElementById('txttodate').readOnly = true;
        }

        window.onload = windowOnLoad;


    </script>

    <style type="text/css">
        .auto-style1 {
            height: 39px;
        }

        .inputCheckboxList {
            font-family: Verdana;
            font-size: 12px;
            padding: 1px;
            border: solid 1px Transparent;
            width: 100%;
        }

        .scrollableDiv {
            position: relative;
            z-index: 10;
            padding-right: 0px;
            padding-left: 0px;
            padding-bottom: 0px;
            padding-top: 0px;
            margin: 0px;
            overflow: auto;
            border: solid 1px #ADACAC;
            height: 150px;
            width: 232px;
        }

        .checkboxlistHeader {
            background-color: #efefef;
            text-align: left;
            padding: 4px;
            overflow: auto;
            border-top: solid 1px #ADACAC;
            border-right: solid 1px #ADADAC;
            border-left: solid 1px #ADACAC;
            width: 242px;
            font-family: Verdana;
            font-size: 12px;
            font-weight: bold;
        }

        .alert {
            font-family: verdana;
            font-size: 13px;
            font-weight: normal;
            color: Red;
        }

        .auto-style3 {
            height: 53px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div style="color: #003366; font-variant: small-caps; font-size: x-large; font-weight: bolder;">
        YESTERDAY'S REPORT
    </div>
    <br />
    <div style="font-size: x-large; font-family: Arial; font-variant: small-caps; width: 100%" align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <br />
                <asp:Image ID="Image1" runat="server" ImageUrl="~/image/ajax-loader.gif" /><br>
                <asp:Label runat="server" ForeColor="#003366" Text="This may take a while, please wait..."></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <%--<asp:UpdatePanel runat="server" ID="up" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
            <asp:Timer ID="timer" runat="server" Interval="500" OnTick="timer_Tick"></asp:Timer>--%>

    <br />
    <div align="right">
        <asp:Label ID="Label3" runat="server" Style="font-size: x-large"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>

    <div align="center">
        <br />
        <asp:GridView ID="GridView1" runat="server"
            AllowPaging="True" PageSize="10" EnableSortingAndPagingCallbacks="true"
            CellPadding="4"
            ForeColor="#333333" GridLines="None" Width="909px"
            Style="margin-top: 0px" AutoGenerateColumns="true" BorderWidth="2px"
            BorderColor="#003366" OnRowDataBound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center"
                VerticalAlign="Middle" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            <Columns>
                <%--<asp:HyperLinkField HeaderText="Project" />--%>
            </Columns>
        </asp:GridView>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
