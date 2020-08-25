<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChronicleIssues.aspx.cs" Inherits="Mphasis_webapp.RCM.ChronicleIssues" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style id="Style1" type="text/css" runat="server">
    .autocomplete
    {
        margin:0px;
        background-color:White;
        cursor:default;
        overflow-y:auto;
        overflow-x:hidden;
        max-height:180px;
        max-width:300px;
        border:goldenrod solid 1px;
        text-align:left;
        z-index:10000;
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
        Chronic Issue Report
        <br />
    </div>
    <div align="left">
        <br />
    </div>
    <br />
    <br />
    <div style="font-size: x-large; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps;
        width: 100%">
      <%--  <asp:UpdatePanel ID="u" runat="server">
            <ContentTemplate>--%>
        <table width="90%">
            <tr>
                <td>
                    Enter Atmid/Siteid:
                </td>
                <%--<td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="320px" DataSourceID="SqlDataSource1"
                        DataTextField="atmid" DataValueField="atmid">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="select distinct atmid from atms order by atmid">
                    </asp:SqlDataSource>
                </td>--%>
               <td>
                    <asp:TextBox ID="txtAtm" runat="server" Width="320px" Height="20px"></asp:TextBox>
                    <ajaxtoolkit:AutoCompleteExtender ServiceMethod="SearchCustomers" 
                        CompletionListCssClass="autocomplete" MinimumPrefixLength="1" 
                        CompletionInterval="10" EnableCaching="false" CompletionSetCount="10" 
    TargetControlID="txtAtm"
    ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
    </ajaxtoolkit:AutoCompleteExtender>
                     <ajaxtoolkit:roundedcornersextender id="Roundedcornersextender1" runat="server"
                        enabled="True" radius="10" targetcontrolid="txtAtm">
                                        </ajaxtoolkit:roundedcornersextender>

                    <br />
                      <%--  SelectCommand="SELECT [ATMID] FROM [ATMs]"--%>
                </td>

                <td>
                    Select Fault:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="320px" DataSourceID="SqlDataSource2"
                        DataTextField="fault" DataValueField="faultid">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="select faultdescription as [fault],faultid from faultcode">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_frmDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxtoolkit:roundedcornersextender id="txt_frmdate_RoundedCornersExtender" runat="server"
                        enabled="True" radius="10" targetcontrolid="txt_frmdate" clientidmode="Static">
                                            </ajaxtoolkit:roundedcornersextender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_frmDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <td>
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txt_toDate" Height="20px" Width="320px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <ajaxtoolkit:roundedcornersextender id="txt_todate_RoundedCornersExtender" runat="server"
                        enabled="True" radius="10" targetcontrolid="txt_todate" clientidmode="Static">
                                            </ajaxtoolkit:roundedcornersextender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_toDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding-top: 0.5em">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                                    runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search"
                                    OnClick="btn_search_Click" ClientIDMode="Static" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div align="center">
            <ajaxtoolkit:calendarextender id="defaultCalendarExtender" runat="server" format="MM/dd/yyyy"
                targetcontrolid="txt_frmDate" />
            <ajaxtoolkit:calendarextender id="defaultCalendarExtender1" runat="server" format="MM/dd/yyyy"
                targetcontrolid="txt_toDate" />
        </div>
        <div align="center">
            <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                <asp:Image ID="Image1" runat="server" ImageUrl="image/ajax-loader.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </div>
      <div  align="center" runat="server" id="div1">
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="No records found pertaining to your search criteria."
                Visible="False"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Green" Visible="False"></asp:Label>
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" ImageUrl="~/Image/EXCEL.ICO"
                                    OnClick="ImageButton1_Click" Visible="false" Style="text-align: left" Width="20px" />
            <br />
        
        <div align="center">
            <div style="width: 800px; overflow: auto; font-size: medium">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Style="margin-top: 0px"
                    Width="909px" BorderColor="#003366" BorderWidth="2px" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowDataBound="GridView1_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <sortedascendingcellstyle backcolor="#E9E7E2" />
                    <sortedascendingheaderstyle backcolor="#506C8C" />
                    <sorteddescendingcellstyle backcolor="#FFFDF8" />
                    <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div></div>
       
        <%--</ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="ImageButton1" />
             </Triggers>
    </asp:UpdatePanel>--%>
        <hr />
    </div>
</asp:Content>
