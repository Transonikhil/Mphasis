<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrentAuditSuspected.aspx.cs" Inherits="Mphasis_webapp.CurrentAuditSuspected" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript">

        function windowOnLoad() {
            document.getElementById('txtfromdate').readOnly = true;
            document.getElementById('txttodate').readOnly = true;
        }

        window.onload = windowOnLoad;
    </script>

<link href="Styles/animate.css" rel="Stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server
        <div style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;" class="animated fadeInLeft">
        Latest Audits
         </div>

    <div style="font-size:x-large;font-family:Cambria, Cochin, Georgia, Times; font-variant:small-caps; width:100%" align="center" >
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
	<br/>
	
    <asp:Image ID="Image1" runat="server" ImageUrl="image/ajax-loader.gif" /><br>
	<asp:Label runat="server" ForeColor="#003366" Text="This may take a while, please wait..."></asp:Label>
    </ProgressTemplate>
    </asp:UpdateProgress>
    </div>
<asp:UpdatePanel runat="server" id="up" ChildrenAsTriggers="true" UpdateMode="Always">
<ContentTemplate>
<asp:Timer  ID="timer" runat="server"  ontick="timer_Tick"></asp:Timer>
<br />
    <div>
                <table cellpadding="0" cellspacing="0" width="60%">
                    <tr>
                        <td>FROM DATE :&nbsp;
                            <asp:TextBox ID="txtfromdate" runat="server" ClientIDMode="Static" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True" Format="MM/dd/yyyy" TargetControlID="txtfromdate">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>TO DATE :&nbsp;
                            <asp:TextBox ID="txttodate" runat="server" Width="150px" ClientIDMode="Static"></asp:TextBox>
                            <asp:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="MM/dd/yyyy" Enabled="True" TargetControlID="txttodate">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txttodate" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align:left">
                            <asp:Button ID="btn_Submit" BorderWidth="0px" BackColor="#003366" Height="30px" Width="100px" Font-Names="Arial"
                                Font-Size="14px" ForeColor="White" runat="server" Text="SUBMIT" CausesValidation="true" OnClick="btn_Submit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
<br />
<div align="right" class="animated fadeInLeft">
<asp:Label ID="Label3" runat="server" ForeColor="Green" Visible="False" style="font-size:x-large"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>
    <div align="center" class="animated fadeInLeft">
	<br/>
    <asp:GridView ID="GridView1" runat="server" 
                AllowPaging ="True" PageSize = "10" 
                    CellPadding="4"
                    ForeColor="#333333" GridLines="None" Width="909px" 
                    style="margin-top: 0px" AutoGenerateColumns="false" BorderWidth="2px" 
                    BorderColor="#003366" onrowdatabound="GridView1_RowDataBound"
					onpageindexchanging="GridView1_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" 
                        VerticalAlign="Middle" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        <Columns>
        <asp:BoundField HeaderText="USER" DataField="USERID" />
        <asp:BoundField HeaderText="VISIT DATE" DataField="VISIT DATE" />
        <asp:BoundField HeaderText="COUNT" DataField="COUNT" />
        <asp:HyperLinkField HeaderText="REPORTS" />
        </Columns>
    </asp:GridView>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
