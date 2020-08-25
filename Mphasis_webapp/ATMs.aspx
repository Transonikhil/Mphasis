<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ATMs.aspx.cs" Inherits="Mphasis_webapp.ATMs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <asp:Panel runat="server" Font-Names="Cambria, Cochin, Georgia, Times">
    <div  style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;">
         <strong>ATM</strong>
        </div>
<br />
    <div  style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:large; font-weight: bolder;">
        <table>
            <tr>
            <td>
        Search ATM:

            </td>
            <td>
             <asp:TextBox ID="txt_search" runat="server" MaxLength="20" Height="22px" Width="320px" ToolTip="Search ATM by ID"></asp:TextBox></td>
                <td>
        <asp:Button ID="btn_Search" runat="server" Text="Search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" BorderWidth="0px" BackColor="#003366" ForeColor="White" OnClick="btn_Search_Click" /></td>
        <td>
        Search Location:</td> 
                <td><asp:TextBox ID="txt_search1" runat="server" MaxLength="20" Height="22px" Width="320px" ToolTip="Search ATM by Location"></asp:TextBox></td>
                <td>
        <asp:Button ID="btn_Search1" runat="server" Text="Search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" BorderWidth="0px" BackColor="#003366" ForeColor="White" /></td>
                </tr>
        </table>
    </div>
    <p class="style3">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                            Text="No records found pertaining to your search. Please select other search criteria." 
                            Visible="False" Font-Size="Large"></asp:Label>
                    </p>
            <div align="center" style="font-size:large">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" AllowPaging="true" PageSize="10" EnableSortingAndPagingCallbacks="true"
                    ForeColor="#333333" GridLines="None" Width="909px" 
                    style="margin-top: 0px" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BorderWidth="2px" BorderColor="#003366">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="gridclass" BackColor="#003366" Font-Bold="false" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" 
                        VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        <Columns>
<asp:HyperLinkField DataNavigateUrlFields="atmid" DataNavigateUrlFormatString="viewATM.aspx?atmid={0}" 
                            DataTextField="atmid" Text="Visit ID" HeaderText="ATM">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:HyperLinkField>


                        <asp:BoundField DataField="Location" HeaderText="Location" 
                            SortExpression="Location" />
                        <asp:BoundField DataField="Bankid" HeaderText="Bank" SortExpression="Bankid" />
                        <asp:BoundField DataField="Client" HeaderText="Client" 
                            SortExpression="Client" />
                    </Columns>
                            </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="SELECT [atmid], [location], [bankid], [client] FROM atms WHERE (([atmid] LIKE '%' + @atmid + '%') AND ([location] LIKE '%' + @location + '%'))">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txt_search" DefaultValue="%" Name="atmid" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="txt_search1" DefaultValue="%" Name="location" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        <br />
        </asp:Panel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <style type="text/css">
        .style3
        {
            text-align: center;
        }
        .gridclass
        {
            font-variant:small-caps;
        }
    </style>

</asp:Content>
