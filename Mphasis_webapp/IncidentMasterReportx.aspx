<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidentMasterReportx.aspx.cs" Inherits="Mphasis_webapp.IncidentMasterReportx" %>

<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="System.IO" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js">
    </script>

    <style type="text/css">
        .item
        {
            position: relative;
            width: 100%;
        }
        .label
        {
            position: absolute;
            top: 0px;
            left: 25px;
        }
    </style>

    <script type="text/javascript">
        function ComboBox1_ItemClick(sender, index) {
            var item = ComboBox1.getItemByIndex(index);
            var checkbox = item.getElementsByTagName('INPUT')[0];
            checkbox.checked = !checkbox.checked;

            updateComboBoxSelection();
        }

        function updateComboBoxSelection() {
            var selectedItems = new Array();
            var checkboxes = ComboBox1.ItemsContainer.getElementsByTagName('INPUT');
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    selectedItems.push(ComboBox1.options[i].text);
                }
            }

            ComboBox1.setText(selectedItems.join(', '));
        }

        function handleCheckBoxClick(e) {
            if (!e) { e = window.event; }
            if (!e) { return false; }
            e.cancelBubble = true;
            if (e.stopPropagation) { e.stopPropagation(); }

            updateComboBoxSelection();
        }

        window.onload = function() {
            window.setTimeout(attachCheckBoxesOnClickHandlers, 250);
        }

        function attachCheckBoxesOnClickHandlers() {
            if (typeof (ComboBox1) == 'undefined' || typeof (ComboBox1.ItemsContainer) == 'undefined') {
                window.setTimeout(attachCheckBoxesOnClickHandlers, 250);
                return;
            }

            var checkboxes = ComboBox1.ItemsContainer.getElementsByTagName('INPUT');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].onclick = handleCheckBoxClick;
            }
        }

        function performSelection(selectionType) {
            var itemsIndexesToSelect = new Array();
            for (var i = 0; i < ComboBox1.options.length; i++) {
                var item = ComboBox1.getItemByIndex(i);
                var checkbox = item.getElementsByTagName('INPUT')[0];
                if (checkbox.checked != selectionType) {
                    ComboBox1_ItemClick(ComboBox1, i);
                }
            }
        }

        function selectAllItems() {
            performSelection(true);
        }

        function deselectAllItems() {
            performSelection(false);
        }

        function chkvalue() {
            var flag = false;
            var checkboxes = ComboBox1.ItemsContainer.getElementsByTagName('INPUT');
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked == true) {
                    flag = true;
                }
            }

            if (flag == false) {
                alert('Please select one faultcode.');
                return false;
            }
            else {
                return true;
            }
        }



      

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div style="height: auto">
        <table style="width: 100%; font-size: 12px; font-family: Arial" cellpadding="0" cellspacing="0">
            <tr style="color: black">
                <td style="width: 10%">
                    Criteria 1
                </td>
                <td style="width: 15%">
                    <asp:TextBox ID="txtCriteria1" runat="server" Width="175px"></asp:TextBox>
                </td>
                <td style="width: 10%">
                    Call Type
                </td>
                <td style="width: 15%">
                    <asp:DropDownList ID="ddcalltype" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%">
                    State
                </td>
                <td style="width: 15%">
                    <asp:DropDownList ID="ddzone" runat="server" Width="150px" DataSourceID="ZoneDataSource"
                        DataTextField="state" DataValueField="state">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="ZoneDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct [state] FROM [ATMs] where RCM like @reg">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddcity" DefaultValue="%" Name="reg" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td style="width: 25%">
                    <%--<asp:Label ID="Label1" runat="server" Font-Size="Larger" ForeColor="red">No Records Found</asp:Label>--%>
                    Project :
                    <asp:DropDownList ID="ddproject" runat="server" Width="150px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddbank_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="color: black">
                <td>
                    Start Date
                </td>
                <td>
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
                <td>
                    RM
                </td>
                <td>
                    <asp:DropDownList ID="ddcity" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddzone_SelectedIndexChanged">
                    </asp:DropDownList>
                    <%--<asp:SqlDataSource ID="CityDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct [username] as [RCM] FROM [Users]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddzone" DefaultValue="%" Name="state" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>
                </td>
                <td>
                    Bank
                </td>
                <td>
                    <asp:DropDownList ID="ddbank" runat="server" Width="150px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="BankDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="SELECT [BankName] FROM [BankMap] where userid=@bnk">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="%" Name="bnk" SessionField="sess_userid" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td style="text-align: center">
                    <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr style="color: black">
                <td>
                    End Date
                </td>
                <td>
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
                <td>
                    Fault Code
                </td>
                <td>
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="faulthf" />
                    <obout:combobox runat="server" id="ComboBox1" width="200px" datasourceid="SqlDataSource2"
                        datatextfield="faultcode" datavaluefield="faultcode" menuwidth="200px">
                        <ItemTemplate>
                            <div class="item">
                                <asp:CheckBox runat="server" ID="CheckBox1" />
                                <div class="label">
                                    <%# Eval("faultcode") %>
                                </div>
                            </div>
                        </ItemTemplate>
                        <ClientSideEvents OnItemClick="ComboBox1_ItemClick" />
                    </obout:combobox>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="SELECT  distinct LTRIM(rtrim( faultcode)) as faultcode  FROM [faultcode] ">
                    </asp:SqlDataSource>
                    <%--<asp:DropDownList ID="dd_faultC" runat="server" Width="150px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sql_fault" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct [faultcode] FROM [faultcode]"></asp:SqlDataSource>--%>
                </td>
                <td>
                    Ageing
                </td>
                <td>
                    <asp:DropDownList ID="dddowntime" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: center">
                    <asp:Button ID="btnview" Text="VIEW" OnClientClick="return chkvalue()" Font-Names="Arial"
                        BorderWidth="0px" BackColor="#006699" Height="30px" Font-Size="14px" ForeColor="White"
                        Width="90px" runat="server" OnClick="btnview_Click" />
                    &nbsp;
                    <asp:Button ID="btnreport" runat="server" Text="REPORT" OnClick="btnreport_Click"
                        Font-Names="Arial" BorderWidth="0px" BackColor="#006699" Height="30px" Font-Size="14px"
                        ForeColor="White" Width="90px" />
                    <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
            
                <td colspan="6" style="text-align:center;padding-left:80px">
                    <obout:oboutbutton id="OboutButton1" runat="server" text="Select all items" onclientclick="selectAllItems(); return false;" />
                    <obout:oboutbutton id="OboutButton3" runat="server" text="Deselect all items" onclientclick="deselectAllItems(); return false;" />
                </td>
            </tr>
            <tr style="color: black">
                <td colspan="2">
                    <asp:Label ID="Label4" runat="server" Visible="False"></asp:Label><asp:HiddenField
                        ID="Hid1" runat="server" />
                    <asp:HiddenField ID="Hid2" runat="server" />
                </td>
                <td colspan="2">
                    <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
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
                        <asp:GridView ID="GridView1" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
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
                                <asp:BoundField DataField="RESOLVED BY" SortExpression="RESOLVED BY" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="RESOLVED BY"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" ReadOnly="True" />
                                <asp:BoundField DataField="RESOLVED TIME" SortExpression="RESOLVED TIME" HeaderStyle-ForeColor="White"
                                    HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#003366" HeaderText="RESOLVED TIME"
                                    HeaderStyle-BorderColor="White" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black"
                                    ItemStyle-BorderWidth="1px" ReadOnly="True" />
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
                                <asp:BoundField DataField="X" SortExpression="X" HeaderStyle-ForeColor="White" HeaderStyle-Font-Size="Larger"
                                    HeaderStyle-BackColor="#003366" HeaderText="ATTACHMENT" HeaderStyle-BorderColor="White"
                                    HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px"
                                    ReadOnly="True" />
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
                        as [CALL OPEN TIME], a.BankID as [BANK],a.RCM as [CLIENT],a.state as [STATE],a.FLM as [SERVICE],f.faultcode as [COMPONENT],
                        I.downtime as [AGEING],i.ITAT as [TAT],CONVERT(VARCHAR(10), I.followupEnteredtime, 105) + ' '+ CONVERT(VARCHAR(5), 
                        I.followupEnteredTime, 108) as [LAST FOLLOWUP],I.UpdateRemark as [LAST FOLLOWUP REMARK],f.fPriority as [Critical],f.faultdescription as [SUB CALL TYPE],
                        I.[IncidentOpenBy] as [OPENED BY],'' as [X],i.callstatus as [CALL STATUS],i.resolvedBy as [RESOLVED BY],CONVERT(VARCHAR(10),I.ResolvedDate, 105) + ' '+ CONVERT(VARCHAR(5), 
                        I.Resolveddate, 108) as [RESOLVED TIME]
                        from IncidentsNew1 as I join atms a on i.ATMID=a.atmid join faultcode f on f.faultid=I.faultid 
                        where (I.ddocketnumber like @docket or I.ATMID like @atm or I.IncidentOpenBy like @atm) and I.callstatus like @callstatus and
                        a.branchid like @proj  and
                        a.State like @zone and faultcode in (@fault) and a.bankname like @bankid and a.RCM like @city and
                        Convert(int,replace(I.Downtime,':','')) &gt;=Convert(int,@i) and (CONVERT(date, OpenDate, 103) between Convert(date,@startdate,103) and 
                        Convert(date,@enddate,103)) and docketnumber like @hid1 order by CONVERT(date, OpenDate, 103) asc ">
 <%--       and a.bankname in (Select bankname from bankmap where userid = @user)--%>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtCriteria1" DefaultValue="%" Name="docket" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="txtCriteria1" DefaultValue="%" Name="atm" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="faulthf" DefaultValue="%" Name="fault" PropertyName="Value"
                Type="String" />
            <asp:ControlParameter ControlID="ddcalltype" DefaultValue="%" Name="callstatus" PropertyName="SelectedValue"
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
            <asp:ControlParameter ControlID="dddowntime" DefaultValue="120000" Name="downtime"
                PropertyName="SelectedValue" Type="String" />
            <asp:SessionParameter DefaultValue="%" Name="user" SessionField="sess_userid" />
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
