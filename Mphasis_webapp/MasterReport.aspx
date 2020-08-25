<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterReport.aspx.cs" Inherits="Mphasis_webapp.MasterReport1" EnableEventValidation="false" %>

<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="System.IO" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <script type="text/javascript">

        function windowOnLoad() {
            document.getElementById('txt_frmDate').readOnly = true;
            document.getElementById('txt_toDate').readOnly = true;
        }

        window.onload = windowOnLoad;
    </script>
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
            var val2 = $('#<%=ddrole.ClientID %>').val();
            val2 = JSON.stringify(val2);
            try {
                $.ajax({
                    type: "POST",
                    url: pageUrl + '/PopulateUser',
                    data: '{state: ' + val1 + ',role: ' + val2 + '}',
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
        .form-control {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:HiddenField ID="hdfUsers" ClientIDMode="Static" runat="server" />
    <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps; font-size: xx-large; font-weight: bolder;">
        Master Report
        <br />
    </div>
    <div align="left">
        <br />
    </div>
    <br />
    <br />
    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps; width: 100%">
        <%--  <asp:UpdatePanel ID="u" runat="server">
            <ContentTemplate>--%>
        <table width="90%">
            <tr>
                <td>Select Role:
                </td>
                <td>
                    <asp:DropDownList ID="ddrole" runat="server" onchange="PopulateUser();" ClientIDMode="Static" Width="320px">
                        <asp:ListItem Value="%" Selected="True">ALL</asp:ListItem>
                        <asp:ListItem Value="AO">CE</asp:ListItem>
                        <asp:ListItem>DE</asp:ListItem>
                        <asp:ListItem>CM</asp:ListItem>
                        <asp:ListItem>RM</asp:ListItem>
                        <asp:ListItem>CH</asp:ListItem>
                    </asp:DropDownList>
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
                <td>Select Officer:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="320px" DataSourceID="SqlDataSource1"
                        DataTextField="userid" DataValueField="userid">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="select distinct u.userid from users u join usermap us on u.userid=us.userid join atms a on us.atmid=a.atmid where role like(case  when role= '%' then (select userid from users where role in('AO','DE')) else @role end) order by u.userid">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddrole" DefaultValue="%" Name="role" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>From Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_frmDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:RoundedCornersExtender ID="txt_frmdate_RoundedCornersExtender" runat="server"
                        Enabled="True" Radius="10" TargetControlID="txt_frmdate" ClientIDMode="Static">
                    </ajaxToolkit:RoundedCornersExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_frmDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td>To Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_toDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:RoundedCornersExtender ID="txt_todate_RoundedCornersExtender" runat="server"
                        Enabled="True" Radius="10" TargetControlID="txt_todate" ClientIDMode="Static">
                    </ajaxToolkit:RoundedCornersExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_toDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Select Role:
                </td>
                <td>
                    <asp:DropDownList ID="ddlVersion" runat="server" ClientIDMode="Static" Width="320px">
                        <asp:ListItem Value="3.9" Selected="True">Version less than or equal to 3.9</asp:ListItem>
                        <asp:ListItem Value="4.0">Version more than or equal to 4.0</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="padding-top: 0.5em">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                                    runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search"
                                    OnClick="btn_search_Click" ClientIDMode="Static" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div align="center">
            <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy"
                TargetControlID="txt_frmDate" />
            <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender1" runat="server" Format="MM/dd/yyyy"
                TargetControlID="txt_toDate" />
        </div>
        <div align="center">
            <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                <asp:Image ID="Image1" runat="server" ImageUrl="image/ajax-loader.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </div>
        <div align="center" runat="server" id="div1">
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="No records found pertaining to your search criteria."
                Visible="False"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Green" Visible="False"></asp:Label>
            <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" ImageUrl="~/Image/EXCEL.ICO"
                OnClick="ImageButton1_Click" Visible="false" Style="text-align: left" Width="20px" />
            <br />

            <div>
                <div style="width: 1000px; overflow: auto; font-size: medium">
                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Style="margin-top: 0px;width:max-content;"
                        BorderColor="#003366" BorderWidth="2px" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" Visible="false">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" BorderColor="WhiteSmoke" />
                        <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                    <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" Style="margin-top: 0px;width:max-content;"
                        BorderColor="#003366" BorderWidth="2px" OnPageIndexChanging="GridView2_PageIndexChanging" 
                        OnRowDataBound="GridView2_RowDataBound" OnRowCreated="GridView2_RowCreated" AutoGenerateColumns="false" Visible="false">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" BorderColor="WhiteSmoke" />
                        <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <Columns>       
                            <asp:BoundField HeaderText="VISIT ID" DataField="VISIT ID" />
                            <asp:BoundField HeaderText="USER ID" DataField="USER ID" />
                            <asp:BoundField HeaderText="ATM ID" DataField="ATM ID" />
                            <asp:BoundField HeaderText="SITE ID" DataField="Site ID" />
                            <asp:BoundField HeaderText="SITE TYPE" DataField="SITE TYPE" />
                            <asp:BoundField HeaderText="LOCATION" DataField="LOCATION" />
                            <asp:BoundField HeaderText="STATE" DataField="STATE" />
                            <asp:BoundField HeaderText="BANK NAME" DataField="BANK NAME" />
                            <asp:BoundField HeaderText="MSP" DataField="MSP" />
                            <asp:BoundField HeaderText="DATE OF VISIT" DataField="DATE OF VISIT" />
                            <asp:BoundField HeaderText="TIME OF VISIT" DataField="TIME OF VISIT" />
                            <asp:BoundField HeaderText="VISIT TYPE" DataField="VISIT TYPE" />
                            <asp:BoundField HeaderText="VISIT REMARK" DataField="VISIT REMARK" />
                            <asp:BoundField HeaderText="ATM MACHINE WORKING FINE?" DataField="ATM MACHINE WORKING FINE?" />
                            <asp:BoundField HeaderText="CARETAKER AVAILABLE?" DataField="CARETAKER AVAILABLE?" />
                            <asp:BoundField HeaderText="CARETAKER NAME" DataField="CARETAKER NAME" />
                            <asp:BoundField HeaderText="CARETAKER NUMBER" DataField="CARETAKER NUMBER" />
                            <asp:BoundField HeaderText="CLEANING DONE REGULARLY?" DataField="CLEANING DONE REGULARLY?" />
                            <asp:BoundField HeaderText="How Severe is the upkeep issue" DataField="How Severe is the upkeep issue" />
                            <asp:BoundField HeaderText="FLOORING PROPER?" DataField="FLOORING PROPER?" />
                            <asp:BoundField HeaderText="DUST BIN OK?" DataField="DUST BIN OK?" />
                            <asp:BoundField HeaderText="BACKROOM OK?" DataField="BACKROOM OK?" />
                            <asp:BoundField HeaderText="WRITING LEDGE AND VMS PROPER?" DataField="WRITING LEDGE AND VMS PROPER?" />
                            <asp:BoundField HeaderText="Signage & Lollipop Cleaned or not?" DataField="Signage & Lollipop Cleaned or not?" />
                            <asp:BoundField HeaderText="FIRE EXTINGUISHER AVAILABLE?" DataField="FIRE EXTINGUISHER AVAILABLE?" />
                            <asp:BoundField HeaderText="IS THE FIRE EXTINGUISHER EXPIRED" DataField="IS THE FIRE EXTINGUISHER EXPIRED" />
                            <asp:BoundField HeaderText="IS RNM OK?" DataField="IS RNM OK?" />
                            <asp:BoundField HeaderText="How Severe is the RnM issue" DataField="How Severe is the RnM issue" />
                            <asp:BoundField HeaderText="LIGHTS OK?" DataField="LIGHTS OK?" />
                            <asp:BoundField HeaderText="No. Of CFL Working" DataField="No. Of CFL Working" />
                            <asp:BoundField HeaderText="GLOW SIGN PROPER?" DataField="GLOW SIGN PROPER?" />
                            <asp:BoundField HeaderText="DOOR WORKING PROPERLY?" DataField="DOOR WORKING PROPERLY?" />
                            <asp:BoundField HeaderText="WALLS PROPER?" DataField="WALLS PROPER?" />
                            <asp:BoundField HeaderText="CEILING PROPER?" DataField="CEILING PROPER?" />
                            <asp:BoundField HeaderText="DOOR MAT AVAILABLE?" DataField="DOOR MAT AVAILABLE?" />
                            <asp:BoundField HeaderText="IS AC Installed at Site" DataField="IS AC Installed at Site" />
                            <asp:BoundField HeaderText="AC WORKING PROPERLY?" DataField="AC WORKING PROPERLY?" />
                            <asp:BoundField HeaderText="AC Connected with timer?" DataField="AC Connected with timer?" />
                            <asp:BoundField HeaderText="AC connected with meter?" DataField="AC connected with meter?" />
                            <asp:BoundField HeaderText="UPS AND BATTERIES WORKING?" DataField="UPS AND BATTERIES WORKING?" />
                            <asp:BoundField HeaderText="CAMERAS AVAILABLE AT SITE?" DataField="CAMERAS AVAILABLE AT SITE?" />
                            <asp:BoundField HeaderText="Signage & Lollipop is working?" DataField="Signage & Lollipop is working?" />
                            <asp:BoundField HeaderText="ANY ISSUE AFFECTNG THE TRANSACTIONS?" DataField="ANY ISSUE AFFECTNG THE TRANSACTIONS?" />
                            <asp:BoundField HeaderText="Feedback from Neighboring Shops/ LL" DataField="Feedback from Neighboring Shops/ LL" />
                            <asp:BoundField HeaderText="VSAT Ballasting" DataField="VSAT Ballasting" />
                            <asp:BoundField HeaderText="Mandatory Notices" DataField="Mandatory Notices" />
                            <asp:BoundField HeaderText="Electricity Bill Payment" DataField="Electricity Bill Payment" />
                            <asp:BoundField HeaderText="Any New Bills at Site" DataField="Any New Bills at Site" />
                            <asp:BoundField HeaderText="Submeter Reading" DataField="Submeter Reading" />
                            <asp:BoundField HeaderText="Any Power Theft Noticed" DataField="Any Power Theft Noticed" />
                            <asp:BoundField HeaderText="Multimeter Reading of Earthing" DataField="Multimeter Reading of Earthing" />
                            <asp:BoundField HeaderText="Is the Visit along with PM Engineer and CRA" DataField="Is the Visit along with PM Engineer and CRA" />
                            <asp:BoundField HeaderText="If Yes: PM Docket No:" DataField="If Yes: PM Docket No:" />
                            <asp:BoundField HeaderText="If Yes: Is PM Done properly" DataField="If Yes: Is PM Done properly" />
                            <asp:BoundField HeaderText="Cash Tallied with Admin Balance, Machine Counter and Physical Couting" DataField="Cash Tallied with Admin Balance, Machine Counter and Physical Couting" />
                            <asp:BoundField HeaderText="Housekeeping done on site?" DataField="Housekeeping done on site?" />
                            <asp:BoundField HeaderText="Deep cleaning done on site?" DataField="Deep cleaning done on site?" />
                            <asp:BoundField HeaderText="Raw Power Status" DataField="Raw Power Status" />
                            <asp:BoundField HeaderText="Multimeter P N Reading" DataField="Multimeter P N Reading" />
                            <asp:BoundField HeaderText="Multimeter P E Reading" DataField="Multimeter P E Reading" />
                            <asp:BoundField HeaderText="Multimeter N E Reading" DataField="Multimeter N E Reading" />
                            <asp:BoundField HeaderText="UPS P N Reading" DataField="UPS P N Reading" />
                            <asp:BoundField HeaderText="UPS P E Reading" DataField="UPS P E Reading" />
                            <asp:BoundField HeaderText="UPS N E Reading" DataField="UPS N E Reading" />
                            <asp:BoundField HeaderText="Power availability in a day (no of Hrs)" DataField="Power availability in a day (no of Hrs)" />
                            <asp:BoundField HeaderText="Frequency of power failure in a day" DataField="Frequency of power failure in a day" />
                            <asp:BoundField HeaderText="Is the ODU-IDU Connection done as per requirement" DataField="Is the ODU-IDU Connection done as per requirement" />
                            <asp:BoundField HeaderText="Other ATMs nearby (range within 500 meters)" DataField="Other ATMs nearby (range within 500 meters)" />
                            <asp:BoundField HeaderText="Stabilizer available" DataField="Stabilizer available" />
                            <asp:BoundField HeaderText="Isolation available" DataField="Isolation available" />
                            <asp:BoundField HeaderText="Monkey cage available" DataField="Monkey cage available" />
                            <asp:BoundField HeaderText="Is CAM1 Working?" DataField="Is CAM1 Working?" />
                            <asp:BoundField HeaderText="Is CAM2 Working?" DataField="Is CAM2 Working?" />
                            <asp:BoundField HeaderText="Is the Image getting stored?" DataField="Is the Image getting stored?" />
                            <asp:BoundField HeaderText="Is EJ getting pulled?" DataField="Is EJ getting pulled?" />
                            <asp:BoundField HeaderText="IS ATM POWER SWITCH AVAILABLE INSIDE BACK ROOM ?" DataField="IS ATM POWER SWITCH AVAILABLE INSIDE BACK ROOM ?" />
                            <asp:BoundField HeaderText="HEIGHT OF THE ATM ROOM ?" DataField="HEIGHT OF THE ATM ROOM ?" />
                            <asp:BoundField HeaderText="IS RAMP AVAILABLE ?" DataField="IS RAMP AVAILABLE ?" />
                            <asp:BoundField HeaderText="IS LAN ROUTING PROPER ? " DataField="IS LAN ROUTING PROPER ?" />
                            <asp:BoundField HeaderText="Space availability in ATM backroom for 2 units with dimensions 2X2 each ?" DataField="Space availability in ATM backroom for 2 units with dimensions 2X2 each ?" />
                            <asp:BoundField HeaderText="Space availability in ATM lobby for 2 units with size dimensions 2X2 each ?" DataField="Space availability in ATM lobby for 2 units with size dimensions 2X2 each ?" />

                            <asp:BoundField HeaderText="LENGTH" DataField="LENGTH1" />
                            <asp:BoundField HeaderText="WIDTH" DataField="WIDTH1" />
                            <asp:BoundField HeaderText="TOTAL AREA" DataField="I.Total ATM area allocated Sq. Feet" />

                            <asp:BoundField HeaderText="LENGTH" DataField="LENGTH2" />
                            <asp:BoundField HeaderText="WIDTH" DataField="WIDTH2" />
                            <asp:BoundField HeaderText="TOTAL AREA" DataField="II.ATM lobby size in Sq. feet" />

                            <asp:BoundField HeaderText="LENGTH" DataField="LENGTH3" />
                            <asp:BoundField HeaderText="WIDTH" DataField="WIDTH3" />
                            <asp:BoundField HeaderText="TOTAL AREA" DataField="III.ATM Back room size in Sq. Feet" />

                            <asp:BoundField HeaderText="Network feasibility Voice ? " DataField="Network feasibility Voice ?" />
                            <asp:BoundField HeaderText="Network feasibility Data ? " DataField="Network feasibility Data ?" />
                            <asp:BoundField HeaderText="Is shutter open close activity happening ? " DataField="Is shutter open close activity happening ?" />
                            <asp:BoundField HeaderText="Shutter open/Close activity happening daily ? " DataField="Shutter open/Close activity happening daily ?" />
                            <asp:BoundField HeaderText="OTHER REMARK" DataField="OTHER REMARK" />
                            <asp:BoundField HeaderText="AC SRNO" DataField="AC SRNO" />
                            <asp:BoundField HeaderText="AC MAKE" DataField="AC MAKE" />
                            <asp:BoundField HeaderText="ATM SRNO" DataField="ATM SRNO" />
                            <asp:BoundField HeaderText="ATM MAKE" DataField="ATM MAKE" />
                            <asp:BoundField HeaderText="BAT1 SRNO" DataField="BAT1 SRNO" />
                            <asp:BoundField HeaderText="BAT1 MAKE" DataField="BAT1 MAKE" />
                            <asp:BoundField HeaderText="BAT2 SRNO" DataField="BAT2 SRNO" />
                            <asp:BoundField HeaderText="BAT2 MAKE" DataField="BAT2 MAKE" />
                            <asp:BoundField HeaderText="BAT3 SRNO" DataField="BAT3 SRNO" />
                            <asp:BoundField HeaderText="BAT3 MAKE" DataField="BAT3 MAKE" />
                            <asp:BoundField HeaderText="MODEM SRNO" DataField="MODEM SRNO" />
                            <asp:BoundField HeaderText="MODEM MAKE" DataField="MODEM MAKE" />
                            <asp:BoundField HeaderText="UPS SRNO" DataField="UPS SRNO" />
                            <asp:BoundField HeaderText="UPS MAKE" DataField="UPS MAKE" />
                            <asp:BoundField HeaderText="LATITUDE" DataField="LATITUDE" />
                            <asp:BoundField HeaderText="LONGITUDE" DataField="LONGITUDE" />
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
        <hr />
    </div>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.2/themes/redmond/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
    <link href="DropDownCss/pqselect.dev.css" rel="stylesheet" />
    <script src="DropDownCss/pqselect.dev.js"></script>
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
            alert(response.d);
            var DropDownList1 = $("[id*=DropDownList1]");
            DropDownList1.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(r.d, function () {
                DropDownList1.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
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
