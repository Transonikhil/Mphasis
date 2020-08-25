<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DistanceMasterReportCH.aspx.cs" Inherits="Mphasis_webapp.bank.DistanceMasterReportCH" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <script type="text/javascript">

        function windowOnLoad() {
            // document.getElementById('txt_frmDate').readOnly = true;
            //document.getElementById('txt_toDate').readOnly = true;
        }

        window.onload = windowOnLoad;
    </script>
    <%--    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true"></script>
    --%>
    <%-- <script src="https://maps.googleapis.com/maps/api/js?&callback=initMap&signed_in=true" async defer> </script>--%>
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.2.4.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            PopulateUser();
        });
        var pageUrl = '<%=HttpContext.Current.Request.Url.AbsolutePath%>';
        function PopulateUser() {
            $('#<%=DropDownList1.ClientID %>').empty().append('<option selected="selected" value="0">Loading...</option>');
            $('#<%=DropDownList1.ClientID %>').attr("disabled", true);
            var val1 = $('#<%=txtuser.ClientID %>').val();
            val1 = JSON.stringify(val1);
            try {
                $.ajax({
                    type: "POST",
                    url: pageUrl + '/PopulateUser',
                    data: '{state: ' + val1 + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnUserPopulated,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            } catch (er) {
                alert(er.Message);
            }
            return false;
        }
        function OnUserPopulated(response) {
            PopulateControl(response.d, $("#<%=DropDownList1.ClientID %>"));
        }
        function PopulateControl(list, control) {
            if (list.length > 0) {
                control.removeAttr("disabled");
                //control.empty().append('<option selected="selected" value="0">ALL</option>');
                control.empty();
                $.each(list, function () {
                    control.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
                $(function () {
                    $('select option').filter(function () {
                        return ($(this).val().trim() == "" && $(this).text().trim() == "");
                    }).remove();
                });
            }
            else {
                //control.empty().append('<option selected="selected" value="0">ALL<option>');
                control.empty();
                $(function () {
                    $('select option').filter(function () {
                        return ($(this).val().trim() == "" && $(this).text().trim() == "");
                    }).remove();
                });
            }
            var users = $('#hdfUsers').val();
            if (users != "") {
                $('#<%=DropDownList1.ClientID %>').val(users);
                $('#hdfUsers').val('');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:HiddenField ID="hdfUsers" ClientIDMode="Static" runat="server" />
    <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps; font-size: xx-large; font-weight: bolder;">
        Distance Master Report
    </div>

    <div align="left">
        <br />
    </div>

    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps; width: 100%">
        <%--  <asp:UpdatePanel ID="u" runat="server">
            <ContentTemplate>--%>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="auto-style1">From Date:
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txt_frmDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server"
                        Enabled="True" Radius="10" TargetControlID="txt_frmdate">
                    </ajaxToolkit:RoundedCornersExtender>
                    <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy"
                        TargetControlID="txt_frmDate" />
                </td>
                <td class="auto-style1">To Date:
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txt_toDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:RoundedCornersExtender ID="txt_todate_RoundedCornersExtender" runat="server"
                        Enabled="True" Radius="10" TargetControlID="txt_todate">
                    </ajaxToolkit:RoundedCornersExtender>
                    <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender1" runat="server" Format="MM/dd/yyyy"
                        TargetControlID="txt_toDate" />
                </td>
            </tr>
            <tr>
                <br />
                <td>Select State:
                </td>
                <td>
                    <asp:ListBox ID="ddstate" runat="server" SelectionMode="Multiple"
                        CssClass="form-control" onchange="chngUser(this.value)" ClientIDMode="Static"
                        DataTextField="username" DataValueField="userid" Width="319px"></asp:ListBox>
                    <asp:TextBox ID="txtuser" runat="server" CssClass="form-control" Style="display: none;" ClientIDMode="Static" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </td>
                <td>Select Officer:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="320px"
                        DataTextField="userid" DataValueField="userid">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <%-- <td>For Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_frmDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:RoundedCornersExtender ID="txt_frmdate_RoundedCornersExtender" runat="server"
                        Enabled="True" Radius="10" TargetControlID="txt_frmdate" ClientIDMode="Static">
                    </ajaxToolkit:RoundedCornersExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_frmDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy"
                        TargetControlID="txt_frmDate" />
                </td>--%>
                <%--<td>
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_toDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxtoolkit:roundedcornersextender id="txt_todate_RoundedCornersExtender" runat="server"
                        enabled="True" radius="10" targetcontrolid="txt_todate" clientidmode="Static">
                                            </ajaxtoolkit:roundedcornersextender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_toDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>--%>

                <td>
                    <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                        runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search"
                        OnClick="btn_search_Click" ClientIDMode="Static" />
                </td>
            </tr>
        </table>

        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 50%; text-align: right">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="No records found pertaining to your search criteria."
                        Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="Label3" runat="server" ForeColor="Green" Visible="False"></asp:Label>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" ImageUrl="~/Image/EXCEL.ICO"
                        OnClick="ImageButton1_Click" Visible="false" Style="text-align: left" Width="20px" />

                </td>
                <td style="width: 5%; text-align: center"></td>
                <td style="width: 45%; text-align: center"></td>
            </tr>
        </table>


        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 100%" valign="top">
                    <div align="center" runat="server" id="div1" style="overflow: auto; font-size: 13px">
                        <br />
                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Style="margin-top: 0px"
                            BorderColor="#003366" BorderWidth="2px" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowDataBound="GridView1_RowDataBound" AllowPaging="false" Width="942px">
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
                </td>
            </tr>
        </table>

        <%--</ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="ImageButton1" />
             </Triggers>
    </asp:UpdatePanel>--%>
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
            PopulateUser();
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
            height: 45px;
        }
    </style>
</asp:Content>
