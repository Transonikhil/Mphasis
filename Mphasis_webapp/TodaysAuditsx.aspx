<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodaysAuditsx.aspx.cs" Inherits="Mphasis_webapp.TodaysAuditsx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <style type="text/css">
        .style1
        {
            text-align: center;
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
       <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps;
        font-size: xx-large; font-weight: bolder;">
        ATM Wise - Audit Report For A Period
    </div>
    <br />
    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps">
        <table width="100%">
            <tr>
                <td>
                    Select ATM:
                </td>
                <td>
                    <asp:DropDownList ID="dd_atm" runat="server" Width="320px" DataSourceID="SqlDataSource1"
                        DataTextField="atmid">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="SELECT ltrim(rtrim([ATMID])) as [ATMID] FROM [ATMs] where atmstatus<>'DEL'"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_frmDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxtoolkit:roundedcornersextender id="txt_frmdate_RoundedCornersExtender" runat="server"
                        enabled="True" radius="10" targetcontrolid="txt_frmdate">
                                        </ajaxtoolkit:roundedcornersextender>
                </td>
                <td>
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_toDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxtoolkit:roundedcornersextender id="txt_todate_RoundedCornersExtender" runat="server"
                        enabled="True" radius="10" targetcontrolid="txt_todate">
                                        </ajaxtoolkit:roundedcornersextender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding-top: 0.5em">
                    <asp:Button ID="Button1" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                        runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search"
                        OnClick="Button1_Click" />
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" Style="text-align: left"
                        ImageUrl="~/Image/EXCEL.ICO" Width="20px"   OnClick="ImageButton1_Click" />
                </td>
            </tr>
        </table>
        <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" format="MM/dd/yyyy"
            targetcontrolid="txt_frmdate" />
        <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" format="MM/dd/yyyy"
            targetcontrolid="txt_todate" />
        &nbsp;&nbsp;&nbsp;
             <br />
        <div align="center">
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="No records found pertaining to your search. Please select other search criteria."
                Visible="False"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Green" Visible="False"></asp:Label>
        </div>
        <div align="center" style="height: 300px">
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                SelectCommand="Select c.vid, a.Location, a. Bankid as [Bank], a.Client as [Client], replace(substring(vid,len(userid)+2,len(vid)),'_',' ') as [Audit Date TIme] 
                from DR_CTP c, ATMs a where c.atmid=a.atmid AND c.ATMID like @atmid and Convert(date,vdate) between @from and @to"
                >
                <SelectParameters>
                <asp:ControlParameter ControlID="dd_atm" Name="atmid" DefaultValue="%" Type="String" />
                   <asp:ControlParameter ControlID="txt_frmDate" Name="from" DefaultValue="%" Type="String" />
                   <asp:ControlParameter ControlID="txt_toDate" Name="to" DefaultValue="%" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" Font-Size="10pt" EnableSortingAndPagingCallbacks="true"
                ForeColor="#333333" GridLines="None" AllowSorting="True"
                Width="909px" Style="margin-top: 0px" OnRowDataBound="GridView1_RowDataBound"
                AllowPaging="false">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <sortedascendingcellstyle backcolor="#E9E7E2" />
                <sortedascendingheaderstyle backcolor="#506C8C" />
                <sorteddescendingcellstyle backcolor="#FFFDF8" />
                <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                <Columns>
                    <asp:HyperLinkField DataTextField="" DataNavigateUrlFields="VID" DataNavigateUrlFormatString="Report.aspx?auditid={0}&dnld_excel=Y"
                        Text="Excel" HeaderText="Report" />
                    <asp:HyperLinkField DataTextField="" DataNavigateUrlFields="VID" DataNavigateUrlFormatString="photos.aspx?auditid={0}&dnld_pdf=Y"
                        Text="Photos" HeaderText="Download" />
                    <asp:HyperLinkField DataTextField="VID" DataNavigateUrlFields="VID" DataNavigateUrlFormatString="MainPage1.aspx?auditid={0}"
                        Text="Visit ID" HeaderText="Visit ID" />
                </Columns>
            </asp:GridView>
        </div>
        <hr />
    </div>
</asp:Content>
