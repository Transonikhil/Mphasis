<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usersmac.aspx.cs" Inherits="Mphasis_webapp.Usersmac" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        	<script type="text/javascript">

		    function getid(lnk) {
		        var row = lnk.parentNode.parentNode;
		        var userid = row.cells[1].textContent.trim();
		        document.getElementById('<%=hfdeletevid.ClientID%>').value = userid;
          
      };
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="auto-style1" style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;">
         <strong>DEVICE ID RESET</strong>
        </div>
    <br />
    <div style="font-variant:small-caps; font-size:large;font-family:Cambria, Cochin, Georgia, Times; width:85%">

    <div align="center" style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:x-large; font-weight: bolder;">
        Search User:<asp:TextBox ID="TextBox1" runat="server" MaxLength="20" Height="22px" Width="320px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" BorderWidth="0px" BackColor="#003366" ForeColor="White" />
    </div>
    
        
        <div align="center" style="font-variant:small-caps">
        <br />
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"
                            Text="No records found pertaining to your search. Please select other search criteria." 
                           ></asp:Label>
                    </div>
        
        <br />
            <div align="center">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" AllowPaging="true"
                    ForeColor="#333333" GridLines="Both" OnRowCommand="GridView1_RowCommand" Width="1050px" 
                    style="margin-top: 0px" AutoGenerateColumns="true" DataSourceID="SqlDataSource1" BorderWidth="2px" BorderColor="#003366">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                         <asp:TemplateField HeaderText="RESET DEVICE ID">
                           
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkreverify" runat="server" ClientIDMode="Static" CommandName="RESET" OnClientClick="return getid(this);">RESET</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select userid as [USERID],USERNAME,role as [ROLE],CM as [REGIONAL MANAGER],RM as [REGIONAL HEAD],case when deviceid is null then 'NA' else deviceid end as [DEVICE ID] from users WHERE ROLE not IN ('admin') and ([userid] LIKE '%' + @userid + '%') order by userid asc">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TextBox1" DefaultValue="%" Name="userid" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="hfdeletevid" runat="server" ClientIDMode="Static"  />
            </div>
        <hr />
        </div> 
</asp:Content>
