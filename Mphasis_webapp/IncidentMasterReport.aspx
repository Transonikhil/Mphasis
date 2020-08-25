<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidentMasterReport.aspx.cs" Inherits="Mphasis_webapp.IncidentMasterReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="System.IO" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        $(function checkUncheck() {
       
            $("[id*=chkAll]").bind("click", function () {
                if ($(this).is(":checked")) {
                    $("[id*=ddcalltype] input").attr("checked", "checked");
                } else {
                    $("[id*=ddcalltype] input").removeAttr("checked");
                }
                document.getElementById("<%= lblTotalSelectedEmailCount.ClientID %>").innerHTML = $("[id*=ddcalltype] input:checked").length + " item(s) selected";
            });
            $("[id*=ddcalltype] input").bind("click", function () {
                if ($("[id*=ddcalltype] input:checked").length == $("[id*=ddcalltype] input").length) {
                    $("[id*=chkAll]").attr("checked", "checked");
                } else {
                    $("[id*=chkAll]").removeAttr("checked");
                }
                document.getElementById("<%= lblTotalSelectedEmailCount.ClientID %>").innerHTML = $("[id*=ddcalltype] input:checked").length + " item(s) selected";
            });
                    

        $(document).on('click', function (e) {
            if (!$(e.target).closest('#selectall')[0]) {
                if (!$(e.target).closest('#ddcalltype')[0]) {
                    $('#dropdown').hide();
                }
            }
            else {
                $('#dropdown').show();
            }
        });
         });

        function windowOnLoad() {
        
            $('#dropdown').hide();
        }

       window.onload = windowOnLoad;
       
       
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
            font-size: 10px;
            font-weight: bold;
        }

        .alert {
            font-family: verdana;
            font-size: 10px;
            font-weight: normal;
            color: Red;
        }
    </style>

    <style type="text/css">
        .Button
        {
            background-color: #6666FF;
            font-weight: bold;
            color: white;
            -webkit-border-radius: 20px;
            border-radius: 5px;
            cursor: pointer;
        }
        .style1
        {
            width: 10%;
            height: 46px;
        }
        .style2
        {
            width: 15%;
            height: 46px;
        }
        .style3
        {
            width: 25%;
            height: 46px;
        }
        .style4
        {
            height: 47px;
        }
        .style5
        {
            height: 46px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div style="height: auto">
        <table style="width: 100%; font-size: 12px; font-family: Arial" cellpadding="0" cellspacing="0">
            <tr style="color: black">
                <td class="style1">
                    Criteria 1
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtCriteria1" runat="server" Width="175px"></asp:TextBox><asp:Label ID="ddcalltype1" runat="server" Visible="false" Width="175px"></asp:Label>
                </td>
                <td class="style1">
                    Call Type
                </td>
                <td class="style2">
                   <%-- <asp:DropDownList ID="ddcalltype" runat="server" Width="150px">
                    </asp:DropDownList>--%>
                    <div id="selectall" style="background-color: white; border: 1px #ADACAC solid; width: 220px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkAll" Text="Select All" runat="server" Font-Bold="True" ForeColor="#009933" />&nbsp;
                                                        <asp:Label ID="lblTotalSelectedEmailCount" runat="server" ForeColor="Red" Style="font-weight: 700"
                                                            Text=""></asp:Label>
                                            </td>
                                            <td>&nbsp;&nbsp;<asp:Image runat="server" ID="img" ImageUrl="image/drop.gif" ImageAlign="Right" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="scrollableDiv" id="dropdown">
                                    <asp:CheckBoxList ID="ddcalltype" runat="server" CssClass="inputCheckboxList">
                                        <asp:ListItem>CLOSE</asp:ListItem>
                                        <asp:ListItem>DISPATCHED</asp:ListItem>
                                        <asp:ListItem>OPEN</asp:ListItem>
                                        <asp:ListItem>RE-OPEN</asp:ListItem>
                                         <asp:ListItem>RESOLVED</asp:ListItem>
                                      </asp:CheckBoxList>
                                </div>

                </td>
                <td class="style1">
                    State
                </td>
                <td class="style2">
                    <asp:DropDownList ID="ddzone" runat="server" Width="150px" DataSourceID="ZoneDataSource"
                        DataTextField="state" DataValueField="state">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    <%--<asp:Label ID="Label1" runat="server" Font-Size="Larger" ForeColor="red">No Records Found</asp:Label>--%>
                    Project :
                    <asp:DropDownList ID="ddproject" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddbank_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="color: black">
                <td class="style4">
                    Start Date
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtStartDate" runat="server" Width="175px"></asp:TextBox>
                    <asp:CalendarExtender ID="StartDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtStartDate" Format="dd'/'MM'/'yyyy">
                    </asp:CalendarExtender>
                </td>
                <%--<td>Service Type</td>
                <td>
                    <asp:DropDownList ID="ddservicetype" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>--%>
                <td class="style4">
                    RCM
                </td>
                <td class="style4">
                    <asp:DropDownList ID="ddcity" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddzone_SelectedIndexChanged">
                    </asp:DropDownList>
                    <%--<asp:SqlDataSource ID="CityDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct [username] as [RCM] FROM [Users]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddzone" DefaultValue="%" Name="state" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>
                </td>
                <td class="style4">
                    Bank
                </td>
                <td class="style4">
                    <asp:DropDownList ID="ddbank" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                 <td class="style4" >
                   Client&nbsp;&nbsp;&nbsp;
                
                   
                    <asp:DropDownList ID="ddagency" runat="server" Width="150px" ></asp:DropDownList>
                   <%-- <asp:SqlDataSource ID="agencyDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct [userid] FROM [users] where role='vendor'"></asp:SqlDataSource>--%>
                </td>
               
            </tr>
            <tr style="color: black">
                <td class="style5">
                    End Date
                </td>
                <td class="style5">
                    <asp:TextBox ID="txtEndDate" runat="server" Width="175px"></asp:TextBox>
                    <asp:CalendarExtender ID="EndDate_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="txtEndDate" Format="dd'/'MM'/'yyyy">
                    </asp:CalendarExtender>
                </td>
                <%--<td>Client</td>
                <td>
                    <asp:DropDownList ID="ddclient" runat="server" Width="150px" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="ClientDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="Select distinct [CustomerName] FROM [Customer]"></asp:SqlDataSource>
                </td>--%>
                <td class="style5">
                    Fault Code
                </td>
                <td class="style5">
                    <asp:DropDownList ID="dd_faultC" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td class="style5">
                    Ageing
                </td>
                <td class="style5">
                    <asp:DropDownList ID="dddowntime" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: center" class="style5">
                    <asp:Button ID="btnview" Text="VIEW" Font-Names="Arial" BorderWidth="0px" BackColor="#006699"
                        Height="30px" Font-Size="14px" ForeColor="White" Width="90px" runat="server"
                        OnClick="btnview_Click" />
                    &nbsp;
                    <asp:Button ID="btnreport" runat="server" Text="REPORT" OnClick="btnreport_Click"
                        Font-Names="Arial" BorderWidth="0px" BackColor="#006699" Height="30px" Font-Size="14px"
                        ForeColor="White" Width="90px" />
                    <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr style="color: black">
                <td colspan="2">
                    <asp:Label ID="Label4" runat="server" Visible="False"></asp:Label><asp:HiddenField ID="Hid1" runat="server" />
                         <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                    <asp:HiddenField ID="Hid2" runat="server" />
                </td>
                <td colspan="2">
                    <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                    <asp:SqlDataSource ID="ZoneDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct [state] FROM [ATMs] where RCM like @reg">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddcity" DefaultValue="%" Name="reg" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label>
                    <asp:SqlDataSource ID="BankDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct [BankName] FROM [Bank]"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:SqlDataSource ID="sql_fault" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct [faultcode] FROM [faultcode]"></asp:SqlDataSource>
                </td>
            </tr>
            <%--<tr>
            <td colspan="6">
            </td>
            <td>
            
            </td></tr>--%>
            <tr style="">
                <td colspan="7">
                </td>
            </tr>
            <tr>
                <td colspan="9">
                    <br />
                    <div style="width: 1100px; overflow-x: scroll">
                        <asp:GridView ID="GridView1" runat="server" Width="100%"  PageSize="15" AutoGenerateColumns="False"
                            EnableViewState="False" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
                            OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:BoundField DataField="TICKET NO" SortExpression="TICKET NO" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="TICKET NO"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="ATM ID" SortExpression="ATM ID" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="ATM ID"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="BANK" SortExpression="BANK" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="BANK"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="CLIENT" SortExpression="CLIENT" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="RM"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="STATE" SortExpression="STATE" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="STATE"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="CALL STATUS" SortExpression="CALL STATUS" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="CALL STATUS"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="COMPONENT" SortExpression="COMPONENT" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="COMPONENT"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="SUB CALL TYPE" SortExpression="SUB CALL TYPE" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="SUB CALL TYPE"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="AGEING" SortExpression="AGEING" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="AGEING"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="TAT" SortExpression="TAT" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="TAT"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="Critical" SortExpression="Critical" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="PRIORITY"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="Critical" SortExpression="Critical" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="REPORT GENERATED THROUGH"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="OPENED BY" SortExpression="OPENED BY" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="OPENED BY"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" ReadOnly="True" />
                                <asp:BoundField DataField="CALL OPEN TIME" SortExpression=" CALL OPEN TIME" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="CALL OPEN TIME"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" ReadOnly="True" />
                                <asp:BoundField DataField="X" SortExpression="X" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger"
                                    HeaderStyle-BackColor="#003366" HeaderText="RESOLVED BY" HeaderStyle-BorderColor="White"
                                    HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px"
                                    ReadOnly="True" />
                                <asp:BoundField DataField="X" SortExpression="X" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger"
                                    HeaderStyle-BackColor="#003366" HeaderText="RESOLVED TIME" HeaderStyle-BorderColor="White"
                                    HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px"
                                    ReadOnly="True" />
                                <asp:BoundField DataField="X" SortExpression="X" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger"
                                    HeaderStyle-BackColor="#003366" HeaderText="CLOSED BY" HeaderStyle-BorderColor="White"
                                    HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px"
                                    ReadOnly="True" />
                                <asp:BoundField DataField="X" SortExpression="X" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger"
                                    HeaderStyle-BackColor="#003366" HeaderText="CLOSED TIME" HeaderStyle-BorderColor="White"
                                    HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px"
                                    ReadOnly="True" />
                                <asp:BoundField DataField="LAST FOLLOWUP" SortExpression="LAST FOLLOWUP" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="LAST FOLLOWUP"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" ReadOnly="True" />
                                <asp:BoundField DataField="LAST FOLLOWUP REMARK" SortExpression="LAST FOLLOWUP REMARK"
                                    HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366"
                                    HeaderText="LAST FOLLOWUP REMARK" HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px"
                                    ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="AGENCY NAME" SortExpression="client"
                                    HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366"
                                    HeaderText="AGENCY NAME" HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px"
                                    ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                                    <asp:BoundField DataField="Remark" SortExpression="Remark"
                                    HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366"
                                    HeaderText="Remark" HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px"
                                    ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                               
                            </Columns>
                            <%--<EmptyDataTemplate>NO RECORDS FOUND</EmptyDataTemplate>
                        <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" Font-Bold="true" />--%>
                            <PagerStyle HorizontalAlign="Center" BackColor="#8DB4E2" />
                            <RowStyle HorizontalAlign="Center" ForeColor="#333333" />
                            <AlternatingRowStyle HorizontalAlign="Center" />
                            <sortedascendingcellstyle backcolor="#E9E7E2" />
                            <sortedascendingheaderstyle backcolor="#506C8C" />
                            <sorteddescendingcellstyle backcolor="#FFFDF8" />
                            <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
        SelectCommand="Select a.ATMID as [ATM ID],I.ddocketnumber as [TICKET NO],CONVERT(VARCHAR(10), I.OpenDate, 105) + ' '+ CONVERT(VARCHAR(5), I.OpenDate, 108) 
                                    as [CALL OPEN TIME], a.BankID as [BANK],a.RCM as [CLIENT],a.state as [STATE],f.faultcode as [COMPONENT],
                                    I.downtime as [AGEING],f.TAT as [TAT],CONVERT(VARCHAR(10), I.followupEnteredtime, 105) + ' '+ CONVERT(VARCHAR(5), 
                                    I.followupEnteredTime, 108) as [LAST FOLLOWUP],I.UpdateRemark as [LAST FOLLOWUP REMARK],f.fPriority as [Critical],f.faultdescription+'/'+'' as [SUB CALL TYPE],
                                    u.[userid] as [OPENED BY],'' as [X],i.callstatus as [CALL STATUS],'NA' AS[AGENCY NAME],I.remark
                                    from IncidentsNew1 as I join atms a on i.ATMID=a.atmid join faultcode f on f.faultid=I.faultid join users u on u.userid=ltrim(rtrim(I.IncidentOpenBy))
                                    where ((I.ddocketnumber like @docket or I.ATMID like @atm or ltrim(rtrim(I.IncidentOpenBy)) like @atm) and I.callstatus in (@callstatus) and
                                    a.project like @proj and a.State like @zone and faultcode like @fault and a.bankid like @bankid and a.RCM like @city and
                                    Convert(int,replace(I.Downtime,':','')) &gt;=Convert(int,@i) and (CONVERT(date, OpenDate, 103) between Convert(date,@startdate,103) and 
                                    Convert(date,@enddate,103)) and docketnumber like @hid1) order by CONVERT(date, OpenDate, 103) asc ">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtCriteria1" DefaultValue="%" Name="docket" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="txtCriteria1" DefaultValue="%" Name="atm" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="dd_faultC" DefaultValue="%" Name="fault" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="ddcalltype1" DefaultValue="%" Name="callstatus" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="ddproject" DefaultValue="%" Name="proj" PropertyName="SelectedValue"
                Type="String" />
            <%--<asp:ControlParameter ControlID="ddclient" DefaultValue="%" Name="customername" PropertyName="SelectedValue" Type="String" />--%>
            <asp:ControlParameter ControlID="ddbank" DefaultValue="%" Name="bankid" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="ddzone" DefaultValue="%" Name="zone" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="ddcity" DefaultValue="%" Name="city" PropertyName="SelectedValue"
                Type="String" />
                <asp:ControlParameter ControlID="ddagency" DefaultValue="%" Name="ddagency" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="dddowntime" DefaultValue="120000" Name="downtime"
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="Label2" Name="i" PropertyName="Text" />
            <asp:ControlParameter ControlID="Label3" Name="startdate" PropertyName="Text" />
            <asp:ControlParameter ControlID="Label4" Name="enddate" PropertyName="Text" />
            <asp:ControlParameter ControlID="Label5" Name="svdate" PropertyName="Text" />
            <asp:ControlParameter ControlID="Label6" Name="evdate" PropertyName="Text" />
            <asp:ControlParameter ControlID="Hid1" Name="hid1" PropertyName="Value" />
            <asp:ControlParameter ControlID="Hid2" Name="hid2" PropertyName="Value" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
