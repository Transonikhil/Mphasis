<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRights.aspx.cs" Inherits="Mphasis_webapp.RM.UserRights" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div  style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;">
         <strong>User Rights</strong>
        </div>
    <br />
    <div style="font-size:x-large;font-family:Cambria, Cochin, Georgia, Times">
    
        <div  style=" text-align:center; color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:x-large; font-weight: bolder;">
        Search User:<asp:TextBox ID="TextBox1" runat="server" MaxLength="20" Height="22px" Width="320px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" BorderWidth="0px" BackColor="#003366" ForeColor="White" />
    </div>
        <br />
       <div align="center"style="font-variant:small-caps">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                            Text="No records found pertaining to your search. Please select other search criteria." 
                            Visible="False"></asp:Label>
            </div>
        <br />
             <div align="center" style="font-variant:small-caps;font-size:large">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" AllowPaging="true" 
                 Font-Size="Large" BorderWidth="2px" BorderColor="#003366"
                    ForeColor="#333333" GridLines="None" AllowSorting="True" Width="800px" 
                    style="margin-top: 0px" AutoGenerateColumns="False" 
                     DataSourceID="SqlDataSource1">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
<asp:HyperLinkField DataNavigateUrlFields="User" DataNavigateUrlFormatString="MAPATMS.aspx?userid={0}" 
                            DataTextField="User" Text="User" HeaderText="User">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:HyperLinkField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" 
                        VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" 
                     SelectCommand="select a.userid as [User], count(atmid) as [ATM] from users a left outer join usermap  as b on a.userid=b.userid where a.rm like @rm and role in ('AO','DE') and a.userid like '%' + @user +'%' group by a.userid order by a.userid">
                     <SelectParameters>
                         <asp:ControlParameter ControlID="TextBox1" DefaultValue="%" Name="user" 
                             PropertyName="Text" />
                         <asp:SessionParameter Name="rm" DefaultValue="%" SessionField="Sess_username" />
                     </SelectParameters>
                 </asp:SqlDataSource>
                 <br />
            </div>
        </div>
</asp:Content>
