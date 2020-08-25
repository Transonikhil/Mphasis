<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapAtmsToUsers.aspx.cs" Inherits="Mphasis_webapp.MapAtmsToUsers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <style type="text/css">
        .textEntry
        {
        }

        .style3
        {
            text-align: center;
        }

        .style4
        {
            font-size: xx-small;
        }
    </style>

    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right
        function IsAlphaNumeric(e) {
            //alert('hi');
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }

        function IsAlphaNumericx() {
            //var x= document.getElementById('<%= txt_Add.ClientID %>');
            document.getElementById('<%= txt_Add.ClientID %>') = document.getElementById('<%= txt_Add.ClientID %>').replace("\"", "");
        }

        function ShowModal() {

            $find('modalPopupBehavior').show();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <h2>MAP ATMS TO MULTPLE USERS</h2>
    <div >
        <fieldset>
            <legend>Enter Details</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <strong>Allot ATMs:</strong>
                        <br />
                        <span class="style4">Example: ATM1,ATM2,ATM3</span></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txt_Add" runat="server" Height="150px" TextMode="MultiLine" Width="750px" ForeColor="Blue" ClientIDMode="Static"  ondrop="return false;"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Add" ErrorMessage="Please enter atmid" ForeColor="Red" ValidationGroup="x"></asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="fte6" runat="server" TargetControlID="txt_Add" FilterType="Custom" FilterMode="InvalidChars"></asp:FilteredTextBoxExtender>
                        <%--InvalidChars="`~!@#$%^&*()_+{}:<>?[]-=\;'./"--%>
                    </td>

                </tr>
                <tr>
                    <td>
                        <strong>Allot Users:<br />
                        </strong><span class="style4">Example: User1,User2,User3</span></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txt_Delete" runat="server" Height="150px" TextMode="MultiLine" Width="752px" ForeColor="Blue" ClientIDMode="Static"  ondrop="return false;"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Delete" ErrorMessage="Please enter User" ForeColor="Red" ValidationGroup="x"></asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="fte7" runat="server" TargetControlID="txt_Delete" FilterType="Custom" FilterMode="InvalidChars"></asp:FilteredTextBoxExtender>
                    </td>
                </tr>
				<tr>
				<td>
				
            <table class="style1">
                <tr>
					<td>
					<asp:Button ID="btn_Update" runat="server" CommandName="Update" Text="Update" align="center" 
					OnClick="btn_Update_Click" height="35" width="100" ValidationGroup="x" />
					</td>
                </tr>
            </table>
				
				</td>
				</tr>
                <tr>
				<td>
				
                        <asp:Label ID="lbl_atmnotFound" runat="server" Style="color: #FF3300"></asp:Label>
				
				</td>
				</tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_atmnotFound1" runat="server" Style="color: #FF3300"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>

				<asp:Label ID="Label1" runat="server" ForeColor="Green" Visible="False"></asp:Label>
				
                    </td>
                </tr>

                <tr style="display:none">
                    <td style="text-align:center">
                        <asp:ModalPopupExtender ID="m1" TargetControlID="btn_Submit0" PopupControlID="Panel1" runat="server" BackgroundCssClass="modalBackground" BehaviorID="modalPopupBehavior">
                        </asp:ModalPopupExtender>
                      <asp:Panel ID="Panel1" runat="server">
                            <div class="inner">
                  <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                        <div>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/ajax-loader.gif" />
											
                                            <br />
                                            <h2 style="color: #09396A; font-weight: bold">LOADING DATA.. PLEASE WAIT..</h2>
                                        </div>
                                  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
        <p class="submitButton">
            
            <asp:Button ID="btn_Submit0" runat="server" OnClick="btn_Submit0_Click" style="display:none"/>
        </p>
    </div>
</asp:Content>
