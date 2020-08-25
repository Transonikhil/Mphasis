<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrentAudit.aspx.cs" Inherits="Mphasis_webapp.CurrentAudit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/animate.css" rel="Stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
<asp:Timer  ID="timer" runat="server" Interval="500" ontick="timer_Tick"></asp:Timer>
<div align="right" class="animated fadeInLeft">
<asp:Label ID="Label3" runat="server" ForeColor="Green" Visible="False" style="font-size:x-large"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>
    <div align="center" class="animated fadeInLeft">
	<br/>
    <asp:GridView ID="GridView1" runat="server" 
                AllowPaging ="True" PageSize = "10" 
                    CellPadding="4"
                    ForeColor="#333333" GridLines="None" Width="909px" 
                    style="margin-top: 0px" AutoGenerateColumns="true" BorderWidth="2px" 
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
        <asp:HyperLinkField HeaderText="Report" />
        <asp:HyperLinkField HeaderText="Download" />
        </Columns>
    </asp:GridView>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
