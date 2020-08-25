<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoardBankstateRM.aspx.cs" Inherits="Mphasis_webapp.RM.DashBoardBankstateRM" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .style3 {
            width: 80%;
        }

        .style5 {
            text-align: right;
        }

        .style6 {
            color: #800000;
            font-size: medium;
        }

        .style7 {
            font-size: x-small;
        }

        .style8 {
            color: #800000;
            text-align: center;
        }

        .style9 {
            text-align: center;
        }

        .style10 {
            font-size: medium;
        }

        .styleall {
            font-size: x-large;
            font-family: Cambria, Cochin, Georgia, Times;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="white_panel" runat="server" BackColor="White" Width="90%" BorderWidth="5" BorderColor="#003366" HorizontalAlign="Center">
        <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; width: 100%" align="center">

            <br />
            <table>
                <tr>
                    <td>Select State:
                    </td>
                    <td>
                        <asp:ListBox ID="ddstate" runat="server" SelectionMode="Multiple"
                            CssClass="form-control" onchange="chngUser(this.value)" ClientIDMode="Static"
                            DataTextField="username" DataValueField="userid" Width="319px"></asp:ListBox>
                        <asp:TextBox ID="txtuser" runat="server" CssClass="form-control" Style="display: none;" ClientIDMode="Static" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </td>
                    <td style="padding-top: 0.5em">
                        <asp:Button ID="btn_searchState" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                            runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search"
                            OnClick="btn_searchState_Click" />
                    </td>
                </tr>
            </table>
            <table class="styleall" width="80%">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Chart ID="Chart3" runat="server" Width="853px" Palette="Bright" DataSourceID="SqlDataSource2">
                            <Series>
                                <asp:Series Name="Series1" XValueMember="vdate" YValueMembers="visit">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Number of Audits Completed"></AxisY>
                                    <AxisX Title="" IntervalType="Auto" Interval="1"></AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="style8">
                        <strong>State Wise Audit Summary of Last 7 Days</strong></td>
                </tr>
                <tr>
                    <td colspan="2" class="style8">____________________________________________________________________________________
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>Select Bank:
                    </td>
                    <td>
                        <asp:ListBox ID="ddbankstate" runat="server" SelectionMode="Multiple"
                            CssClass="form-control" onchange="chngbankstate(this.value)" ClientIDMode="Static"
                            DataTextField="username" DataValueField="userid" Width="319px"></asp:ListBox>
                        <asp:TextBox ID="txtbankstate" runat="server" CssClass="form-control" Style="display: none;" ClientIDMode="Static" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </td>
                   <td style="padding-top: 0.5em">
                        <asp:Button ID="btn_search_bank" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                            runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search"
                            OnClick="btn_search_bank_Click" />
                    </td>
                </tr>
            </table>
            <table class="styleall" width="80%">
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Chart ID="Chart1" runat="server" Width="853px" Palette="Bright" DataSourceID="SqlDataSource1">
                            <Series>
                                <asp:Series Name="Series1" XValueMember="vdate" YValueMembers="visit">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY Title="Number of Audits Completed"></AxisY>
                                    <AxisX Title="" IntervalType="Auto" Interval="1"></AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="style8">
                        <strong>Bank Wise Audit Summary of Last 7 Days</strong></td>
                </tr>
                <tr>
                    <td colspan="2" class="style8">____________________________________________________________________________________
                    </td>
                </tr>

                <tr>
                    <td colspan="2" class="style5"><span class="style7">Powered By </span><a href="http://transovative.com/" title="Transformation Through Innovation">
                        <span class="style7">Transovative.com</span></a>&nbsp;</td>
                </tr>
            </table>
        </div>
    </asp:Panel>
  <script type="text/javascript" src="https://code.jquery.com/jquery-2.2.4.min.js"></script>
 <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.2/themes/redmond/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
    <link href="../DropDownCss/pqselect.dev.css" rel="stylesheet" />
    <script src="../DropDownCss/pqselect.dev.js"></script>
    <script type="text/javascript">

        $(function () {

            //initialize the pqSelect widget.
            $("#ddstate").pqSelect({
                multiplePlaceholder: 'All State',
                checkbox: true //adds checkbox to options    
            }).on("change", function (evt) {
                var val = $(this).val();
                //PopulateErrorDesc();
            }).pqSelect('close');
        });
       

    </script>
    <script type="text/javascript">

        function chngUser(ddlVal) {
            //debugger;
            var strDiv = "";

            if (ddlVal == "%") {
                $("#<%= ddstate.ClientID%> > option").each(function () {
                    var str = this.value;
                    if (str.toString().trim() != "" && str.toString().trim() != "%")
                        strDiv += "," + str.toString();
                });
                strDiv = strDiv.substring(1, strDiv.length);
            }
            else {
                var x = document.getElementById("<%= ddstate.ClientID%>");
                for (var i = 0; i < x.options.length; i++) {
                    if (x.options[i].selected) {
                        //alert(x.options[i].selected);
                        strDiv += "'" + x.options[i].value + "',";
                    }
                }
            }
            $('#<%= txtuser.ClientID%>').val(strDiv.substring(0, strDiv.length - 1));
            
        }
    </script>



    <script type="text/javascript">

        $(function () {

            //initialize the pqSelect widget.
            $("#ddbankstate").pqSelect({
                multiplePlaceholder: 'All State',
                checkbox: true //adds checkbox to options    
            }).on("change", function (evt) {
                var val = $(this).val();
                //PopulateErrorDesc();
            }).pqSelect('close');
        });
        function OnSubComponentPopulated(response) {
          
        }

    </script>
    <script type="text/javascript">

        function chngbankstate(ddlVal) {
            //debugger;
            var strDiv = "";

            if (ddlVal == "%") {
                $("#<%= ddbankstate.ClientID%> > option").each(function () {
                    var str = this.value;
                    if (str.toString().trim() != "" && str.toString().trim() != "%")
                        strDiv += "," + str.toString();
                });
                strDiv = strDiv.substring(1, strDiv.length);
            }
            else {
                var x = document.getElementById("<%= ddbankstate.ClientID%>");
                for (var i = 0; i < x.options.length; i++) {
                    if (x.options[i].selected) {
                        //alert(x.options[i].selected);
                        strDiv += "'" + x.options[i].value + "',";
                    }
                }
            }
            $('#<%= txtbankstate.ClientID%>').val(strDiv.substring(0, strDiv.length - 1));
            
        }
    </script>
    <style type="text/css">
        .inputCheckboxList {
            font-family: Verdana;
            font-size: 10px;
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
            height: 125px;
            width: 220px;
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
            font-size: 7px;
            font-weight: bold;
        }

        .alert {
            font-family: verdana;
            font-size: 7px;
            font-weight: normal;
            color: Red;
        }

        .style1 {
            height: 41px;
        }

        .style2 {
            height: 41px;
            text-align: center;
        }

        .style3 {
            text-align: center;
        }

        .style4 {
            height: 41px;
            text-align: left;
            width: 6%;
        }

        .style5 {
            width: 6%;
        }

        .style7 {
            width: 14%;
        }

        .style10 {
            width: 96%;
        }

        .style11 {
            width: 1%;
        }

        .style14 {
            width: 16%;
        }

        .style19 {
            width: 15%;
            height: 48px;
        }

        .style20 {
            width: 1%;
            height: 48px;
        }

        .style21 {
            width: 25%;
            height: 48px;
        }

        .style22 {
            width: 6%;
            height: 48px;
        }

        .style23 {
            width: 14%;
            height: 48px;
        }

        .style24 {
            width: 16%;
            height: 48px;
        }

        .style25 {
            width: 6%;
            height: 41px;
        }

        .style26 {
            width: 14%;
            height: 41px;
        }

        .style27 {
            width: 16%;
            height: 41px;
        }

        .style28 {
            color: #000000;
        }

        .style29 {
            width: 196px;
            height: 24px;
        }

        .style30 {
            height: 18px;
        }

        .auto-style1 {
            width: 166px;
        }

        .auto-style2 {
            width: 336px;
        }
    </style>
</asp:Content>
