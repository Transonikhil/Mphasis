<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Mphasis_webapp.DashBoard" %>
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
    <link href="~/Styles/animate.css" rel="Stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="js/jquery.min.js" type="text/javascript"></script>
<div style="font-size:x-large;font-family:Cambria, Cochin, Georgia, Times;width:100%" align="center">        
    <asp:LinkButton ID="hpl"  runat="server" OnClick="hpl_Click" ></asp:LinkButton>
        </div>
        <br>
    <center>        
    <asp:Button ID="Button4" PostBackUrl="~/DashBoard.aspx?request=ctp" Font-Names="Cambria, Cochin, Georgia, Times" runat="server" Text="ICICI Audits" BorderWidth="0px" BackColor="#003366" Height="40px" Width="180px" Font-Size="18px" ForeColor="White" CausesValidation="False" EnableViewState="False" UseSubmitBehavior="False" />
    <asp:Button ID="Button1" PostBackUrl="~/DashBoard.aspx?request=hdfc" Font-Names="Cambria, Cochin, Georgia, Times" runat="server" Text="HDFC Audits" BorderWidth="0px" BackColor="#003366" Height="40px" Width="180px" Font-Size="18px" ForeColor="White" CausesValidation="False" EnableViewState="False" UseSubmitBehavior="False" />
    <asp:Button ID="Button2" PostBackUrl="~/DashBoard.aspx?request=axis" Font-Names="Cambria, Cochin, Georgia, Times" runat="server" Text="BOM Audits" BorderWidth="0px" BackColor="#003366" Height="40px" Width="180px" Font-Size="18px" ForeColor="White" CausesValidation="False" EnableViewState="False" UseSubmitBehavior="False" />
    </center>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<%--            <asp:Timer ID="timer" runat="server" Interval="500" OnTick="timer_Tick"></asp:Timer>
--%>
            <div align="center" style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; width: 100%">
                <div align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl="image/ajax-loader.gif" Style="position: absolute; top: 369px; left: 50%;" CssClass="animated fadeInRight" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>

                <asp:Panel ID="white_panel" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="5" HorizontalAlign="Center" Width="90%">
                    <asp:Panel ID="no_branch" runat="server" CssClass="animated fadeInRight">
                        <table class="styleall" width="80%">
                            <tr>
                                <td class="style9"><span class="style6"><strong>Number Of Sites Assigned to me:</strong></span>
    
                                    <asp:Label ID="lbl_siteassigned" Text="50" runat="server" CssClass="styleall"></asp:Label>
                                </td>
                                <td class="style9"><span class="style6"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Monitor Field Force</strong></span>
                                    <asp:ImageButton ID="ImageButton2" runat="server" Height="48px" ImageUrl="~/Image/monitor.jpg" PostBackUrl="FieldTracker.aspx?Offline=True" Width="43px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style9" colspan="2">
                                    <table class="style3">
                                        <tr>
                                            <td>
                                       <%--         <asp:Chart ID="Chart1" runat="server"
                                                    Height="184px" Width="365px" Palette="Fire" Style="text-align: center">
                                                    <Series>
                                                        <asp:Series ChartType="Pie" Name="Series1" XValueMember="audited"
                                                            YValueMembers="audited" Legend="Legend1">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                                            <Area3DStyle Enable3D="True"></Area3DStyle>
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                    <Legends>
                                                        <asp:Legend Name="Legend1" Title="Sites Audited / Pending">
                                                        </asp:Legend>
                                                    </Legends>
                                                </asp:Chart>--%>
                                                <div id="container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"><%=piechart()%></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style8"><strong>Audits Completed in Current Month:
                                            <asp:Label ID="lbl_auditinCurrentMonth" runat="server" CssClass="style10" Style="color: #0000FF; font-weight: 700"></asp:Label>
                                            </strong></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8">&nbsp;</td>
                                <td class="style8">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8" colspan="2">______________________________________________________________________________________________________</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <%--<asp:Chart ID="Chart3" runat="server" Palette="Bright" Width="853px">
                                        <Series>
                                            <asp:Series IsValueShownAsLabel="True" Name="Series1" XValueMember="vdate" YValueMembers="visit">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisY Title="Number of Audits Completed">
                                                </AxisY>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>--%>
                                    <div id="Columndig" style="min-width: 310px; height: 400px; margin: 0 auto"><%=barchat()%></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style8" colspan="2"><strong>Audit Summary of Last 7 Days</strong></td>
                            </tr>
                            <tr>
                                <td class="style8" colspan="2">____________________________________________________________________________________
                                </td>
                            </tr>
                            <tr>
                                <td class="style8" colspan="2">
                                    <%--<asp:Chart ID="Chart4" runat="server" Palette="Bright" Width="853px">
                                        <Series>
                                            <asp:Series IsValueShownAsLabel="True" Name="Series1" XValueMember="area officer" YValueMembers="visit">
                                                <SmartLabelStyle AllowOutsidePlotArea="Yes" />
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea4">
                                                <AxisY Title="Number of Audits Completed">
                                                </AxisY>
                                                <AxisX Interval="1" IntervalType="Auto" Title="">
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>--%>
                                    <div id="ColumndigforOfficer" style="min-width: 310px; height: 400px; margin: 0 auto"><%=AreaOfficerWiseSummary()%></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style8" colspan="2">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Area Officer Wise Summary For Last 30 Days"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5" colspan="2"><span class="style7">Powered By </span><a href="http://transovative.com/" title="Transformation Through Innovation"><span class="style7">Transovative.com</span></a>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
