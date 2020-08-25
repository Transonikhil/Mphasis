<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DistanceReport.aspx.cs" Inherits="Mphasis_webapp.bank.DistanceReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <script type="text/javascript">

        function windowOnLoad() {
            document.getElementById('txt_frmDate').readOnly = true;
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
        Distance Report
    </div>

    <div align="left">
        <br />
    </div>

    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps; width: 100%">
        <%--  <asp:UpdatePanel ID="u" runat="server">
            <ContentTemplate>--%>
        <table width="100%" cellpadding="0" cellspacing="0">

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

                <td>
                    <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                        runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search"
                        OnClick="btn_search_Click" ClientIDMode="Static" />
                </td>
            </tr>
            <tr>
                <td>For Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_frmDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:RoundedCornersExtender ID="txt_frmdate_RoundedCornersExtender" runat="server"
                        Enabled="True" Radius="10" TargetControlID="txt_frmdate" ClientIDMode="Static">
                    </ajaxToolkit:RoundedCornersExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_frmDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy"
                        TargetControlID="txt_frmDate" />
                </td>
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
                <td style="width: 50%" valign="top">
                    <div align="center" runat="server" id="div1" style="overflow: auto; font-size: 13px">
                        <br />
                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Style="margin-top: 0px"
                            BorderColor="#003366" BorderWidth="2px" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowDataBound="GridView1_RowDataBound" AllowPaging="false">
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

                <td style="width: 5%"></td>

                <td style="width: 45%;" valign="top">

                    <body ng-app="app" ng-controller="Controller">
                        &nbsp;&nbsp;<select runat="server" ng-change="showGPSTracking()" ng-model="user" ng-init="init()" id="chk123" style="height: 20px; width: 320px; display: none">
                            <option selected="selected" value="" class="">Reselect user to view route</option>
                        </select>
                        <ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server"
                            Enabled="True" Radius="10" TargetControlID="chk123" ClientIDMode="Static">
                        </ajaxToolkit:RoundedCornersExtender>
                        <br />
                        <br />
                        <div>
                            <%--  <div id="map-canvas" style="width: 450px; height: 400px; float: left;">
                                <% Response.Write(Session["sess_x"]);%>
                            </div>--%>

                            <div id="map" style="width: 450px; height: 400px; float: left;">
                                <% Response.Write(Session["sess_x"]);%>
                            </div>
                        </div>
                    </body>
                </td>
            </tr>
        </table>
        <script src="https://maps.googleapis.com/maps/api/js?&callback=initMap&signed_in=true" async defer> </script>
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
</asp:Content>
