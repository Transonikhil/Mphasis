<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuditForAPeriod.aspx.cs" Inherits="Mphasis_webapp.RCM.AuditForAPeriod" %>

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
     <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps; font-size: xx-large; font-weight: bolder;"><strong>Customer Wise - Bank Wise Audit Reports For A Period</strong></div>
    <br />
    <br />

    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps; width: 100%">
        <table width="100%">
            <tr>
                <td class="auto-style1">Select Bank: </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="dd_bank" runat="server" Width="320px" DataSourceID="SqlDataSource1" DataTextField="bankid" DataValueField="bankid">
                    </asp:DropDownList>
                </td>
                <td class="auto-style1">Select Vendor: </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="dd_cust" runat="server" Width="320px" DataSourceID="SqlDataSource2" DataTextField="client" DataValueField="client">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select distinct client from atms"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select distinct bankid from atms"></asp:SqlDataSource>
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
                <td>Select State:
                </td>
                <td>
                    <asp:ListBox ID="ddstate" runat="server" SelectionMode="Multiple"
                        CssClass="form-control" onchange="chngUser(this.value)" ClientIDMode="Static"
                        DataTextField="username" DataValueField="userid" Width="319px"></asp:ListBox>
                    <asp:TextBox ID="txtuser" runat="server" CssClass="form-control" Style="display: none;" ClientIDMode="Static" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="padding-top: 0.5em;">
                    <asp:Button ID="btn_Update" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search" OnClick="btn_Update_Click" />
                    <asp:ImageButton ID="ImageButton2" runat="server" Height="20px" ImageUrl="~/Image/EXCEL.ICO" OnClick="ImageButton2_Click" Width="20px" />
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
                AllowPaging="true" PageSize="12"
                CellPadding="4" Font-Size="10pt"
                ForeColor="#333333" GridLines="None" AllowSorting="False" Width="909px"
                Style="margin-top: 0px">
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
        function OnSubComponentPopulated(response) {

        }

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
    </style>
</asp:Content>
