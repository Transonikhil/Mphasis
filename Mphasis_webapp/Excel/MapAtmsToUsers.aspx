<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapAtmsToUsers.aspx.cs" Inherits="Mphasis_webapp.Excel.MapAtmsToUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" lang="javascript">
        function ShowModal() {

            $find('pop').show();
        }
    </script>
    <style type="text/css">
        .lbl {
            text-align: right;
            font-size: 12px;
            font-family: Arial;
            background-color: #D9D9D9;
        }

        .Button {
            background-color: #6666FF;
            font-weight: bold;
            color: white;
            -webkit-border-radius: 20px;
            border-radius: 5px;
            cursor: pointer;
        }

        .confirm-dialog {
            MARGIN: 0px auto;
            WIDTH: 329px;
            PADDING-TOP: 14px;
            POSITION: relative;
            border: orange solid 1px;
            background-color: white;
            top: 9px;
            left: 0px;
            height: auto;
        }

            .confirm-dialog .inner {
                PADDING-RIGHT: 20px;
                PADDING-LEFT: 20px;
                PADDING-BOTTOM: 11px;
                FLOAT: left;
                MARGIN: 0px 0px -20px 0px;
                WIDTH: 290px;
                PADDING-TOP: 0px;
            }

            .confirm-dialog H2 {
                FONT-WEIGHT: bold;
                FONT-SIZE: 1.25em;
                COLOR: #000000;
                TEXT-ALIGN: center;
            }

            .confirm-dialog input {
            }

            .confirm-dialog .base {
                PADDING-BOTTOM: 4px;
                MARGIN-LEFT: -11px;
                MARGIN-RIGHT: -11px;
                PADDING-TOP: 4px;
                TEXT-ALIGN: center;
            }

        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .autocomplete {
            margin: 0px;
            background-color: White;
            cursor: default;
            overflow-y: auto;
            overflow-x: hidden;
            max-height: 180px;
            max-width: 163px;
            border: goldenrod solid 1px;
            text-align: left;
            z-index: 10000;
        }

        #imgclose {
            height: 31px;
        }
    </style>
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
        
        function ShowModal() {

            $find('modalPopupBehavior').show();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="text-align: center">
            <table style="width: 100%">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="ExcelUploader.aspx">HOME</asp:HyperLink>
                    </td>
                </tr>
                </table>
                </div>
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
    </form>
</body>
</html>
