<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Mphasis_webapp.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="auto-style1" style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;">
         <strong>Users</strong>
        </div>
    <br />
    <div style="font-variant:small-caps; font-size:large;font-family:Cambria, Cochin, Georgia, Times; width:85%">

    <div align="center" style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:x-large; font-weight: bolder;">
        Search User:<asp:TextBox ID="TextBox1" runat="server" MaxLength="20" Height="22px" Width="320px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" BorderWidth="0px" BackColor="#003366" ForeColor="White" />
    </div>
    
        
        <div align="center" style="font-variant:small-caps">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"
                            Text="No records found pertaining to your search. Please select other search criteria." 
                            Visible="False"></asp:Label>
                    </div>
        
        <br />
            <div align="center">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" AllowSorting="True" Width="909px" 
                    style="margin-top: 0px" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BorderWidth="2px" BorderColor="#003366">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="userid" DataNavigateUrlFormatString="ViewUser.aspx?userid={0}" 
                            DataTextField="userid" Text="User" HeaderText="User">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:HyperLinkField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#003366"  ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" 
                        VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="SELECT [userid] FROM [users] WHERE ([userid] LIKE '%' + @userid + '%')">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TextBox1" DefaultValue="%" Name="userid" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        <hr />
        </div>  
</asp:Content>
