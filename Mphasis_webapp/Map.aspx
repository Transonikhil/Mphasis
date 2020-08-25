<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="Mphasis_webapp.Map" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" 
        TargetControlID="DropDownList_User" PromptCssClass="PromptCSS" 
        PromptText="Click again &amp; type to Search">  
        </ajaxToolkit:ListSearchExtender> 

    <% Response.Write(Session("mymap"))%>
  <div id="map" style="width:907px;height:400px;"></div>
        <br />
        <asp:Label ID="lbl_err" runat="server" ForeColor="Red"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="location" DataValueField="atmid">
            <asp:ListItem Value="select">--Select--</asp:ListItem>
    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" 
                                        SelectCommand="select distinct f.atmid,a.atmid + ' - ' + a.location as location from fence f, atms a where f.atmid=a.atmid order by f.atmid">
                                    </asp:SqlDataSource>

    <asp:Button ID="Button1" runat="server" Text="Remove Fence" />
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Please Select Officer:<asp:DropDownList ID="DropDownList_User" runat="server" Height="22px" Width="183px" DataSourceID="SqlDataSource2" DataTextField="userid" DataValueField="userid">
                </asp:DropDownList>
    <asp:Button ID="btn_SearchUser" runat="server" Text="Search" />
   <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" 
                    SelectCommand="SELECT distinct [userid] FROM [location] where userid<>'admin' and l_date is not null order by userid" ></asp:SqlDataSource>
</asp:Content>
