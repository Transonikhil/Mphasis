<%@ Page Title="" Language="C#" MasterPageFile="~/Uploader.Master" AutoEventWireup="true" CodeBehind="UserSearch.aspx.cs" Inherits="Mphasis_webapp.UserSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <table style="width:1000px">
        <tr>
            <td style="width:400px" >

            </td>
            <td style="text-align:center">
                Search Users : <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox> &nbsp;&nbsp; <asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" Text="Search" style="cursor:pointer" Font-Names="Cambria, Cochin, Georgia, Times" BorderWidth="0px" BackColor="#6699ff" Height="20px" Width="100px" Font-Size="15px" ForeColor="White"  CausesValidation="False" EnableViewState="False" UseSubmitBehavior="False"  ClientIDMode="Static"/>
            </td>
        </tr>
        <tr style="height:10px">
            <td>

            </td>
        </tr>
        <tr style="text-align:center;">
            <td>

            </td>
            <td>
                
                <asp:GridView ID="GridView1" runat="server" CellPadding="4"
                    ForeColor="#333333" GridLines="None" AllowSorting="True" Width="909px" 
                    style="margin-top: 0px" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BorderWidth="2px" BorderColor="#003366">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="userid" ControlStyle-ForeColor="Black" DataNavigateUrlFormatString="AtmMap.aspx?userid={0}" 
                            DataTextField="userid" Text="User" HeaderText="User">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:HyperLinkField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#666666"  ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" 
                        VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="SELECT distinct [userid] FROM [users] WHERE ([userid] LIKE '%' + @userid + '%') and role='AO' and status<>'DEL'">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtsearch" DefaultValue="%" Name="userid" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
