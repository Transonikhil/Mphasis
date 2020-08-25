<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mphasis_webapp.Default" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .style3
        {
            width: 80%;
        }
        .style5
        {
            text-align: right;
        }
        .style6
        {
            color: #800000;
            font-size: medium;
        }
        .style7
        {
            font-size: x-small;
        }
        .style8
        {
            color: #800000;
            text-align: center;
        }
        .style9
        {
            text-align: center;
        }
        .style10
        {
            font-size: medium;
        }
        .styleall 
        {
            font-size:x-large;font-family:Cambria, Cochin, Georgia, Times;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Panel ID="white_panel" runat="server" BackColor="White" Width="90%" BorderWidth="5" BorderColor="#003366" HorizontalAlign="Center">
            <div style="font-size:x-large;font-family:Cambria, Cochin, Georgia, Times;width:100%" align="center">
                
                <table class="styleall" width="80%">
                    <tr>
                        <td class="style9">
                            <span class="style6"><strong>Number Of Sites Assigned to me:</strong></span>
                            <asp:Label ID="lbl_siteassigned" runat="server" CssClass="styleall"></asp:Label>
                        </td>
                        <td class="style9">
                                <span class="style6"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Monitor Field Force</strong></span> 
                                <asp:ImageButton ID="ImageButton2" runat="server" Height="48px" ImageUrl="~/Image/monitor.jpg" Width="43px" OnClick="ImageButton2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="style9">
                            <table class="style3">
                                <tr>
                                    <td>
                            <asp:Chart ID="Chart1" runat="server" 
                                Height="184px" Width="365px" Palette="Fire" style="text-align: center" DataSourceID="SqlDataSource1" >
                                <Series>
                                    <asp:Series ChartType="Pie" Name="Series1" XValueMember="Audited" 
                                        YValueMembers="Audited" Legend="Legend1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D=true>
<Area3DStyle Enable3D="True"></Area3DStyle>
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend Name="Legend1" Title="Sites Audited / Pending">
                                    </asp:Legend>
                                </Legends>
                            </asp:Chart>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select count(distinct atmid) as Audited,'Y' as [Y] from DR_CTP where  DATEPART(mm,vdate)=datepart(mm,CONVERT(VARCHAR(10), GETDATE(), 101)) Union all select case when (select distinct COUNT(atmid) - (select count(distinct atmid) from DR_CTP where DATEPART(mm,vdate)=datepart(mm,CONVERT(VARCHAR(10), GETDATE(), 101))) from ATMs where atmstatus&lt;&gt;'Inactive')&lt;=0 then 0 else (select distinct COUNT(atmid) - (select count(distinct atmid) from DR_CTP where DATEPART(mm,vdate)=datepart(m,CONVERT(VARCHAR(10), GETDATE(), 101))) from ATMs where atmstatus&lt;&gt;'Inactive') end ,'Y'"></asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8">
                            <strong>Audits Completed in Current Month: <asp:Label 
                                ID="lbl_auditinCurrentMonth" runat="server" 
                                style="color: #0000FF; font-weight: 700" CssClass="style10"></asp:Label>
                            </strong></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style8">
                            &nbsp;</td>
                        <td class="style8">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style8" colspan="2">
                            ______________________________________________________________________________________________________</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Chart ID="Chart3" runat="server" Width="853px" Palette="Bright" DataSourceID="SqlDataSource2">
                                <Series>
                                    <asp:Series Name="Series1" XValueMember="vdate" YValueMembers="visit">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" >
                                    <AxisY Title="Number of Audits Completed"></AxisY>
                                    <AxisX Title="" IntervalType="Auto" Interval=1></AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select convert(datetime,vdate) as [vdate], COUNT(*) as [visit] from DR_CTP where  convert(datetime,vdate) BETWEEN CONVERT(VARCHAR(10), dateadd(D, -7, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) group by vdate union all select CONVERT(VARCHAR(10), GETDATE(), 101) AS [vDATE],'0' union all  select dateadd(D,-1,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-2,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-3,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all   Select dateadd(D,-4,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-5,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-6,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' union all  select dateadd(D,-7,CONVERT(VARCHAR(10), GETDATE(), 101)) AS [vDATE],'0' order by convert(datetime,vdate)">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="style8">
                            <strong>Audit Summary of Last 7 Days</strong></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="style8">
                            ____________________________________________________________________________________
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td colspan="2" class="style8">
                            <asp:Chart ID="Chart4" runat="server" Width="853px" Palette="Bright" DataSourceID="SqlDataSource3">
                                <Series>
                                    <asp:Series Name="Series1" XValueMember="area officer" YValueMembers="visit" IsValueShownAsLabel="True" >
                                        <SmartLabelStyle AllowOutsidePlotArea="Yes" />
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea4">
                                         <AxisY Title="Number of Audits Completed"></AxisY>
                                         <AxisX Title="" IntervalType="Auto" Interval=1></AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select u.userid as [area officer],COUNT(d.vid) as [visit] from DR_CTP d, users u where  d.USERID=u.userid and convert(datetime,d.vdate ) between CONVERT(VARCHAR(10), dateadd(D, -30, GETDATE()), 101) AND CONVERT(VARCHAR(10), GETDATE(), 101) group by u.userid union all select distinct userid, '0' from users where userid&lt;&gt;'AREA OFFICER' and userid&lt;&gt;'admin' and userid not in (select distinct u.userid from dr_ctp d, users u where d.userid=u.userid)  order by visit desc"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td colspan="2" class="style8">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Maroon" 
                                Text="Area Officer Wise Summary For Last 30 Days"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="style5"><span class="style7">Powered By </span> <a href="http://transovative.com/" title="Transformation Through Innovation">
                            <span class="style7">Transovative.com</span></a>&nbsp;</td>
                    </tr>
                </table>
            </div>
    </asp:Panel>
</asp:Content>
