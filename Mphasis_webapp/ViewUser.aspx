<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUser.aspx.cs" Inherits="Mphasis_webapp.ViewUser" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <div class="auto-style1" style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;">
         <strong>Add User</strong>
        </div>
              <div style="font-size:x-large;font-family:Cambria, Cochin, Georgia, Times; font-variant:small-caps; width:70%;padding-top:1em;">
                    <table style="width:100%;font-size:x-large;font-family:Cambria, Cochin, Georgia, Times;">
                        <tr>
                            <td class="auto-style2">
                            <asp:Label ID="Label1" runat="server" >User:</asp:Label></td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_user" runat="server" Height="20px" Width="320px"></asp:TextBox>
                                        </td>
                                        <td style="padding-top:0.3em">
                                            <asp:Panel ID="user_err" runat="server" Visible="false">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp;&nbsp;Please provide User ID &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <asp:RoundedCornersExtender ID="txt_user_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_user">
                                </asp:RoundedCornersExtender>
                            </td>
                            </tr>
                    <tr><td class="auto-style2">
                        <asp:Label ID="PasswordLabel" runat="server" >Password:</asp:Label></td>
                        <td>

                        <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_password" runat="server" Height="20px" Width="320px"></asp:TextBox>
                                        </td>
                                        <td style="padding-top:0.3em">
                                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp; Please provide Password &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            <asp:RoundedCornersExtender ID="txt_password_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_password">
                            </asp:RoundedCornersExtender>
                        </td>

                    </tr>
                    <tr><td class="auto-style2">
                        <asp:Label ID="Label2" runat="server" >Role:</asp:Label></td><td>
                        <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_Role" runat="server" Height="20px" Width="320px"></asp:TextBox>
                                        </td>
                                        <td style="padding-top:0.3em">
                                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp; Please provide Role &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            <asp:RoundedCornersExtender ID="txt_Role_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_Role">
                            </asp:RoundedCornersExtender>
                    </td></tr>
                    <tr><td class="auto-style2">
                        <asp:Label ID="Label5" runat="server" >AO:</asp:Label></td><td>
                        <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_AO" runat="server" Height="20px" Width="320px"></asp:TextBox>
                                        </td>
                                        <td style="padding-top:0.3em">
                                            <asp:Panel ID="Panel3" runat="server" Visible="false">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp; Please provide AO &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            <asp:RoundedCornersExtender ID="txt_AO_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_AO">
                            </asp:RoundedCornersExtender>
                    </td></tr>
                    <tr><td class="auto-style2">
                        <asp:Label ID="Label3" runat="server" >OC:</asp:Label></td><td>
                        <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_oc" runat="server" Height="20px" Width="320px"></asp:TextBox>
                                        </td>
                                        <td style="padding-top:0.3em">
                                            <asp:Panel ID="Panel4" runat="server" Visible="false">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp; Please provide OC &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            <asp:RoundedCornersExtender ID="txt_oc_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_oc">
                            </asp:RoundedCornersExtender>
                    </td></tr>
                    <tr><td class="auto-style2">
                        <asp:Label ID="Label6" runat="server" >FC:</asp:Label></td><td>
                        <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_fc" runat="server" Height="20px" Width="320px"></asp:TextBox>
                                        </td>
                                        <td style="padding-top:0.3em">
                                            <asp:Panel ID="Panel5" runat="server" Visible="false">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp; Please provide FC &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            <asp:RoundedCornersExtender ID="txt_fc_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_fc">
                            </asp:RoundedCornersExtender>
                    </td></tr>
                    <tr><td class="auto-style2">
                        <asp:Label ID="Label7" runat="server" >OM:</asp:Label></td><td>
                        <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_OM" runat="server" Height="20px" Width="320px"></asp:TextBox>
                                        </td>
                                        <td style="padding-top:0.3em">
                                            <asp:Panel ID="Panel6" runat="server" Visible="false">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp; Please provide OM &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            <asp:RoundedCornersExtender ID="txt_OM_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_OM">
                            </asp:RoundedCornersExtender>
                    </td></tr>
                    <tr><td width="115px">
                        <asp:Label ID="Label4" runat="server" >Status:</asp:Label></td>
                        <td>
                            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                            <asp:RadioButton ID="rdb_active" Text="Active" GroupName="status" runat="server" AutoPostBack="True" OnCheckedChanged="rdb_active_CheckedChanged" />
                            <asp:RadioButton ID="rdb_inactive" Text="Inactive" GroupName="status" runat="server" AutoPostBack="True" OnCheckedChanged="rdb_inactive_CheckedChanged" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                    </td>
                        </tr>

                    </table>
               <hr />
                  <table>
                      
                      <tr>
                          <td style="width:115px"></td>
                          <td>
                              <asp:Button ID="btn_Update" Height="30px" Width="190px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" CommandName="Update" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Update" OnClick="btn_Update_Click" />

                          </td>
                      </tr>
                  </table>
           </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <style type="text/css">
        .fcb {
        color: #333;
        }
        .fwb {
        font-weight: bold;
        }
        .fsl {
        font-size: large;
        }
        .auto-style1 {
            font-size: xx-large;
        }
        .auto-style2 {
            width: 115px;
            padding-top:0.3em;
        }
        .uiBoxRed {
        background-color: #ffebe8;
        border: 1px solid #dd3c10;
        }
    </style>

</asp:Content>
