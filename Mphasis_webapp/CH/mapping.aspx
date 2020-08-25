<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mapping.aspx.cs" Inherits="Mphasis_webapp.CH.mapping" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div id="main">
        <asp:UpdatePanel runat="server" ID="updatepanelDropDownTaskType">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Submit0" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 40%">FROM USERID : 
                    <asp:DropDownList ID="ddlfrom" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource3" DataTextField="userid" DataValueField="userid" OnSelectedIndexChanged="ddlfrom_SelectedIndexChanged"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                                SelectCommand="SELECT [userid] FROM [users] WHERE CH like @rm and (status<>'DEL' or datastatus<>'DEL')">
                                <SelectParameters>
                                   <%-- <asp:Parameter DefaultValue="AO" Name="role" Type="String" />--%>
                                    <asp:SessionParameter Name="rm" SessionField="Sess_username" DefaultValue="%" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </td>
                        <td></td>
                        <td>TO USERID :
                    <asp:DropDownList ID="ddlto" runat="server" DataSourceID="SqlDataSource3" DataTextField="userid" DataValueField="userid"></asp:DropDownList>&nbsp;&nbsp; 
                            <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="javascript:$find('modalPopupBehavior2').show();">ALREADY EXISTING ATMS</asp:LinkButton>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: start">
                            <br />

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USERID" Width="366px" CellPadding="4"
                                ForeColor="Black" GridLines="Vertical" CssClass="alignment" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                <%--OnDataBound="GridView1_DataBound"--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No" HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2"
                                        HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="userid" HeaderText="USER ID" InsertVisible="False" ReadOnly="True" SortExpression="userid"
                                        HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                                    <asp:BoundField DataField="atmid" HeaderText="ATM ID" SortExpression="atmid"
                                        HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                                    <asp:TemplateField ItemStyle-Width="40px"
                                        HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEmp" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <EmptyDataTemplate>
                                    <div>
                                        <asp:Label ID="Label2" runat="server" Text="NO ATMS AVAILABLE"></asp:Label>
                                    </div>
                                </EmptyDataTemplate>

                                <PagerStyle HorizontalAlign="Center" BackColor="#8DB4E2" />
                                <RowStyle HorizontalAlign="Center" ForeColor="#333333" />
                                <AlternatingRowStyle HorizontalAlign="Center" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>

                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                            <script type="text/javascript" src="quicksearch.js"></script>
                            <script type="text/javascript">
                                $(function () {
                                    $('.search_textbox').each(function (i) {
                                        $(this).quicksearch("[id*=GridView1] tr:not(:has(th))", {
                                            'testQuery': function (query, txt, row) {
                                                return $(row).children(":eq(" + 2 + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                                            }
                                        });
                                    });
                                });
                            </script>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                                SelectCommand="SELECT [userid], [atmid] FROM [usermap] WHERE userid like @userid and (status<>'DEL' or serverstatus<>'DEL')">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlfrom" Name="userid" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>

                            </asp:SqlDataSource>

                        </td>
                        <td style="width: 20%" valign="top">
                            <center><asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="Validate" ForeColor="Red" ValidationGroup="x"></asp:CustomValidator></center>
                            <br />
                            <asp:Panel runat="server" ID="Panel1" Style="text-align: center">
                                <asp:Button ID="btnmove" runat="server" Text="MOVE" OnClick="btnmove_Click" ValidationGroup="x" Font-Names="Cambria, Cochin, Georgia, Times"
                                    BorderWidth="0px" BackColor="#006699" Height="30px" Font-Size="18px" ForeColor="White" Width="90px" /><br />
                                <br />
                                <br />
                                <asp:Button ID="btncopy" runat="server" Text="COPY" OnClick="btncopy_Click" ValidationGroup="x" Font-Names="Cambria, Cochin, Georgia, Times"
                                    BorderWidth="0px" BackColor="#006699" Height="30px" Font-Size="18px" ForeColor="White" Width="90px" /><br />
                                <br />
                                <br />
                                <asp:Button ID="btn" runat="server" Text="DELETE" OnClick="btn_Click" Font-Names="Cambria, Cochin, Georgia, Times"
                                    BorderWidth="0px" BackColor="#006699" Height="30px" Font-Size="18px" ForeColor="White" Width="90px" OnClientClick="return del();" /><br />
                                <asp:Button ID="btn_Submit0" runat="server" OnClick="btn_Submit0_Click" Style="display: none" />
                            </asp:Panel>
                        </td>
                        <td style="width: 40%" valign="top">
                            <br />
                            <asp:Panel runat="server" ID="Panel2" Style="text-align: center">

                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="USERID" Width="366px" CellPadding="4"
                                    ForeColor="Black" GridLines="Vertical" CssClass="alignment" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No" HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2"
                                            HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="userid" HeaderText="USER ID" InsertVisible="False" ReadOnly="True" SortExpression="userid"
                                            HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                                        <asp:BoundField DataField="atmid" HeaderText="ATM ID" SortExpression="atmid"
                                            HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                                    </Columns>

                                    <EmptyDataTemplate>
                                        <div>
                                            <asp:Label ID="Label2" runat="server" Text="NO ATMS AVAILABLE"></asp:Label>
                                        </div>
                                    </EmptyDataTemplate>

                                    <PagerStyle HorizontalAlign="Center" BackColor="#8DB4E2" />
                                    <RowStyle HorizontalAlign="Center" ForeColor="#333333" />
                                    <AlternatingRowStyle HorizontalAlign="Center" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>

                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                                    SelectCommand="SELECT [userid], [atmid] FROM [usermap] WHERE userid like @userid and (status='CRE' and serverstatus='CRE')">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlto" Name="userid" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center" valign="middle">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <div class="overlay" style="font-size: xx-large; padding-top: 8em; color: #003366">
                                        <center><bold><asp:Image ImageUrl="~/Image/ajax-loader.gif" runat="server" /><%--<br/>
                        &nbsp;&nbsp;&nbsp;&nbsp;Page Is Loading Please Wait...--%></bold>
							
                                </center>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>

                    <%--<tr>
                        <td colspan="3" style="text-align: center">
                            <asp:ModalPopupExtender ID="m1" TargetControlID="LinkButton1" PopupControlID="Panel3" runat="server" BackgroundCssClass="modalBackground" BehaviorID="modalPopupBehavior2">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="Panel3" runat="server">
                                <div class="inner">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <asp:Label ID="Label3" runat="server" Text="ATMID's : " Font-Bold="true"></asp:Label><br />
                                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>--%>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
