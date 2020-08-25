<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Memo.aspx.cs" Inherits="Mphasis_webapp.Memo" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js">
    </script>--%>

    

    <style type="text/css">
         .item {
            position: relative;
            width: 100%;
        }

        .label {
            position: absolute;
            top: 0px;
            left: 25px;
        }
        .style1
        {
            text-align: center;
        }
        .style2
        {
            width: 99%;
        }
        .style3
        {
            width: 243px;
            height: 67px;
        }
        .style6
        {
            width: 243px;
            height: 87px;
            text-align: right;
        }
        .style7
        {
            height: 87px;
            text-align: left;
            width: 504px;
        }
        .style8
        {
            width: 243px;
            height: 181px;
            text-align: right;
        }
        .style9
        {
            height: 181px;
            text-align: left;
            width: 504px;
        }
        .style10
        {
            color: #FF3300;
        }
        .style11
        {
            text-align: left;
            height: 53px;
            width: 504px;
        }
    
        .style4
        {
            font-size: xx-small;
        }
        .style12
        {
            width: 243px;
            height: 53px;
            text-align: right;
        }
        .style13
        {
            text-align: right;
        }
         .grid
        {
            width: 100%;
            text-transform: uppercase;
        }
        .style14
        {
            text-align: left;
            height: 67px;
            width: 504px;
        }
        .style15
        {
            width: 732px;
            height: 305px;
            overflow: auto;
        }
    </style>
     <script type="text/javascript">
         function error() {
             if (document.getElementById('txtuserid').value == '') 
             {
                 alert('Enter Userid');
             }
             if ((document.getElementById('txtremark').value == '') && (document.getElementById('txtuserid').value != '')) 
             {
                 alert('Enter Remark');
             }            
         }
    </script>
     <script src="js/jquery.min.js"></script>
     <script type="text/javascript">

         $(document).ready(function () {
             PopulateUsers();
            
         });
         var pageUrl = '<%=HttpContext.Current.Request.Url.AbsolutePath%>'

        function PopulateUsers() {
            $("#<%=ddUser.ClientID %>").attr('disabled', true);
            var category = $("#<%=ddlState.ClientID%>").val();
            // shift = JSON.stringify(shift);
            category = JSON.stringify(category);
            var role = $("#<%=ddrole.ClientID%>").val();
            role = JSON.stringify(role);

            try {

                $.ajax({
                    type: "POST",
                    url: pageUrl + '/PopulateUsers',
                    //data: '{shift: ' + shift + ',category: ' + category + '}',
                    data: '{category: ' + category + ', roles: ' + role + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnPopulated,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            } catch (er) {
                alert(er.Message);
            }

        }

        function OnPopulated(response) {
            PopulateControl(response.d, $("#<%=ddUser.ClientID %>"));
        }

    </script>

    <script type="text/javascript">
        function PopulateControl(list, control) { 
            control.empty();
            control.removeAttr("disabled");
            if (list.length > 0) {
                $.each(list, function () {
                    control.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
                $(function () {
                    $('select option').filter(function () {
                        return ($(this).val().trim() == "" && $(this).text().trim() == "");
                    }).remove();
                });
            }
            else {
                $(function () {
                    $('select option').filter(function () {
                        return ($(this).val().trim() == "" && $(this).text().trim() == "");
                    }).remove();
                });
            }

            var users = $('#<%=hdnUsers.ClientID%>').val();
            debugger;
            if (users != "") {
                $('#<%=ddUser.ClientID %>').val(users);
                $('#<%=hdnUsers.ClientID%>').val('');
            }
        }

    </script>
    <script>
        function forpostback() {
            document.getElementById("<%=btnpost.ClientID%>").click();
        }
    </script>
    <script  type="text/javascript">
        function windowOnLoad()
        {
            document.getElementById("<%=txtdate.ClientID%>").readOnly = true;
        }
        window.onload = windowOnLoad;
    </script>
     <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

                <script type="text/javascript" src="quicksearch.js"></script>

                <script type="text/javascript">
                    $(function () {
                        $('.search_textbox').each(function (i) {
                            $(this).quicksearch("[id*=GridView1] tr:not(:has(th))", {
                                'testQuery': function (query, txt, row) {
                                    return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                                }
                            });
                        });
                    });
                </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Button ID="btnpost" runat="server" OnClick="btnpost_Click" Style="display:none;" />
   <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps;
        font-size: xx-large; font-weight: bolder;">
       Memo
        <br />
    </div>
    
       <div>
        <fieldset style="width:100px;">
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnUsers" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                   <strong><asp:Label ID="lblRole" runat="server" Text="Role" Width="100px"></asp:Label> </strong>
                </td>
                <td>
                      <asp:DropDownList ID="ddrole" runat="server" AppendDataBoundItems="true" onchange="PopulateUsers();" ClientIDMode="Static" >
                        <asp:ListItem Value="%" Selected="True">ALL</asp:ListItem>
                        <asp:ListItem Value="AO">CE</asp:ListItem>
                        <asp:ListItem>DE</asp:ListItem>
                        <asp:ListItem>CM</asp:ListItem>
                          <asp:ListItem>CH</asp:ListItem>
                          <asp:ListItem>RM</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><asp:Label ID="lblState" runat="server" Text="State" Width="100px"></asp:Label></strong>
                </td>
                <td>
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataSourceID="SqlDataSource3" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" 
                                                        DataTextField="state" DataValueField="statename" onchange="PopulateUsers();">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                                                        SelectCommand="select 'ALL' as [state],'%' as [statename] union all select distinct state as [statename],state from users WHERE ROLE IN ('AO','DE','CM')  and state is not null"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><asp:Label ID="lblOfficer" runat="server" Text="Select Officer" Width="100px"></asp:Label></strong>
                </td>
                <td>
                     <asp:DropDownList ID="ddUser" runat="server" Width="320px" 
                        DataTextField="username" DataValueField="userid" onchange="forpostback();">
                    </asp:DropDownList>
                   <%-- <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="select '%' as [userid],'ALL' as [username] union all select distinct userid,username as [username]  from users where status<>'DEL' AND role in ('CM','AO') order by userid">
                     <SelectParameters>
                            <asp:ControlParameter ControlID="ddrole" DefaultValue="%" Name="role" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><asp:Label ID="lblUserId" runat="server" Text="User ID" Width="100px"></asp:Label></strong></td>
                <td class="style9">
                    &nbsp;&nbsp;&nbsp;<span class="style4">Example: userid1,userid2,userid3</span><br />
                    &nbsp;
                    <asp:TextBox ID="txtuserid" runat="server" Height="147px" TextMode="MultiLine" ClientIDMode="Static"
                        Width="440px"></asp:TextBox>
                  <asp:FilteredTextBoxExtender ID="fte6" runat="server" TargetControlID="txtuserid" FilterType="Custom" FilterMode="InvalidChars"></asp:FilteredTextBoxExtender>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>
                    <br />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" 
                        SelectCommand="select UPPER(userid) from users where role='RO' and userid not in ('kkkkkk','kol')">
                    </asp:SqlDataSource>
                    </strong>
                    <br />
                    <span class="style10">&nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Label ID="lbl_usernotFound" runat="server" Style="color: #FF3300"></asp:Label>
                        <asp:Label ID="lbl_usernotFound1" runat="server" Style="color: #FF3300"></asp:Label>
                    </span><br />
                    </td>
                <%--<td>
                    <strong>
                    Select User List:-
                    <asp:DropDownList ID="ddlist" runat="server" AutoPostBack="True" 
                        CssClass="bold" DataSourceID="SqlDataSource1" DataTextField="Column1" 
                        DataValueField="Column1" Height="20px" 
                        onselectedindexchanged="ddlist_SelectedIndexChanged" Width="221px">
                    </asp:DropDownList>
                    </strong>
                </td>--%>
            </tr>
            <tr>
                <td>
                    <strong><asp:Label ID="lblMRemark" runat="server" Text="Memo Remark" Width="100px"></asp:Label></strong></td>
                <td>
                    &nbsp;&nbsp;<asp:TextBox ID="txtremark" runat="server" Height="147px" TextMode="MultiLine" ClientIDMode="Static"
                        Width="440px"></asp:TextBox>
                  <asp:FilteredTextBoxExtender ID="fte7" 
                        runat="server" TargetControlID="txtremark" FilterType="Custom" 
                        FilterMode="InvalidChars"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                    </td>
                <td style="text-align:center;margin-right:20px">
                    <strong>
                    <asp:Button ID="btnsend" runat="server" CssClass="bold" onclick="btnsend_Click" OnClientClick="error()" ClientIDMode="Static"
                        Text="Send"  Height="34px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;"
                                   BorderWidth="0px" BackColor="#003366" ForeColor="White"/>
                    </strong></td>
            </tr>
           </table>
            <hr />
            <table>
               <%-- <tr>
                    <td colspan="2" style="text-align:center;height:20px;color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps;
        font-size: xx-large; font-weight: bolder;"><asp:Label ID="lblTitle" runat="server" Text="Memo Report" Width="100%"></asp:Label>  </td>
                </tr>--%>
            <tr>
                <td>
                    <strong>Date&nbsp; </strong></td>
                <td class="style11">
                    <asp:TextBox ID="txtdate" Height="18px" Width="164px" runat="server" 
                        ClientIDMode="Static"></asp:TextBox> 
                        <asp:calendarextender id="calextDate" runat="server" format="MM/dd/yyyy"
                targetcontrolid="txtdate" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <strong>Status &nbsp; </strong>
                    <asp:DropDownList ID="ddstatus" runat="server" CssClass="bold" Height="22px" 
                        Width="125px">
                        <asp:ListItem>Unread</asp:ListItem>
                        <asp:ListItem>Read</asp:ListItem>
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="search" runat="server" CssClass="bold" onclick="search_Click" 
                        Text="Search" Font-Size="15px" Font-Names="Cambria, Cochin, Georgia, Times;"
                                   BorderWidth="0px" BackColor="#003366" ForeColor="White"/>
                        <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" Height="20px" Style="text-align: left"
                ImageUrl="~/Image/EXCEL.ICO" Width="20px" OnClick="ImageButton1_Click" />
                        
                   </td>
            </tr>
           
            <tr>
            
                <td>
                </td>
                <td colspan="2" class="style13">
                <div class="style15">
               <table width="100px">
                   
                   <tr>
                       <td colspan="2">
                            <asp:Label ID="lblNoRecord" runat="server" ForeColor="Red" Text="No records found."
                Visible="False" Width="320px" Font-Size="20px" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Visible="False"  Font-Size="20px" Font-Bold="true"></asp:Label>
                       </td>

                   </tr>
                   <tr>
                       <td>
                             <asp:GridView ID="GridView1" runat="server" Width="702px" 
                            AutoGenerateColumns="False" 
                        CssClass="grid" ClientIDMode="Static"
                        style="text-align:center;" onrowdatabound="GridView1_RowDataBound" OnDataBound="OnDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <sortedascendingcellstyle backcolor="#E9E7E2" />
                    <sortedascendingheaderstyle backcolor="#506C8C" />
                    <sorteddescendingcellstyle backcolor="#FFFDF8" />
                    <sorteddescendingheaderstyle backcolor="#6F8DAE" />
                        <Columns>
                           <asp:BoundField DataField="UserID" HeaderText="UserID" 
                                SortExpression="UserID" />
                            <asp:BoundField DataField="MemoRemark" HeaderText="MemoRemark" 
                                SortExpression="MemoRemark" />
                            <asp:BoundField DataField="Sent Date" HeaderText="Send Date" 
                                SortExpression="Sent Time" />
                            <asp:BoundField DataField="Sent Time" HeaderText="Send Time" 
                                SortExpression="Sent Time" />
                            <asp:BoundField DataField="Seen Date" HeaderText="SeenDate" 
                                SortExpression="SeenDate" />
                            <asp:BoundField DataField="Status" HeaderText="Status" 
                                SortExpression="Status" />
                        </Columns>
                    </asp:GridView>

                       </td>

                   </tr>
                  
                       <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

                <script type="text/javascript" src="quicksearch.js"></script>

                <script type="text/javascript">
                    $(function () {
                        $('.search_textbox').each(function (i) {
                            $(this).quicksearch("[id*=GridView1] tr:not(:has(th))", {
                                'testQuery': function (query, txt, row) {
                                    return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                                }
                            });
                        });
                    });
                </script>
                    <div>
                      <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" >
                        </asp:SqlDataSource>
                    </div>
                    </td>
                    </table>
    </div>
            </tr>
        </table>
    </fieldset>
    </div>
    <%-- <div>
        <asp:ModalPopupExtender ID="m2" BehaviorID="pop" TargetControlID="btn_Submit0" PopupControlID="pnl"
            runat="server" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnl" runat="server" CssClass="confirm-dialog">
            <div>
                <h2>
                    <asp:Label ID="lblSuccess" runat="server"> </asp:Label>
                    
                </h2>
            </div>
        </asp:Panel>
    </div>--%>
</asp:Content>
