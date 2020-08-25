<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAtms.aspx.cs" Inherits="Mphasis_webapp.AddAtms" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .auto-style1 {
            text-align: left;
            width:320px;
        }
        .auto-style2 {
            width: 25%;
        }
        .auto-style3
        {
            width: 25%;
        }
        .uiBoxRed {
        background-color: #ffebe8;
        border: 1px solid #dd3c10;
        }
        .fsl {
        font-size: large;
        }
        .fwb {
        font-weight: bold;
        }
        .fcb {
        color: #333;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="auto-style1" style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;">
         <strong>Add ATM Site</strong>
        </div>

           <div style="font-size:x-large;font-family:Cambria, Cochin, Georgia, Times; font-variant:small-caps; width:90%;padding-top:1em;">
               
                    <table style="width:100%">
                        <tr>
                            <td class="auto-style3"><asp:Label ID="Label1" runat="server" >ATM:</asp:Label></td>
                            <td class="auto-style1"><asp:TextBox ID="txt_atm" runat="server" Height="20px" Width="320px"></asp:TextBox>
                                <asp:RoundedCornersExtender ID="txt_atm_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_atm">
                                </asp:RoundedCornersExtender>
                            </td>
                            <td style="width: 30%" valign="bottom">
                                            <asp:Panel ID="user_err" runat="server" Visible="true" style="text-align: center">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp;&nbsp;Please provide User ID &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td>
                            <td>
                                            &nbsp;</td></tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="PasswordLabel" runat="server" >Location:</asp:Label></td><td class="auto-style1">
                        <asp:TextBox ID="txt_location" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_location_RoundedCornersExtender" runat="server" Enabled="True" TargetControlID="txt_location" Radius="10">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                                            <asp:Panel ID="user_err0" runat="server" Visible="true" style="text-align: center">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp;&nbsp;Please provide Location &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td><td>
                            &nbsp;</td></tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="Label6" runat="server" >Address Line 1:</asp:Label></td><td class="auto-style1">
                            <asp:TextBox ID="txt_addressline1" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_addressline1_RoundedCornersExtender" runat="server" Enabled="True" TargetControlID="txt_addressline1" Radius="10">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                            &nbsp;</td><td>
                            &nbsp;</td></tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="Label7" runat="server" >Address Line 2:</asp:Label></td><td class="auto-style1">
                            <asp:TextBox ID="txt_addressline2" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_addressline2_RoundedCornersExtender" runat="server" Enabled="True" TargetControlID="txt_addressline2" Radius="10">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                            &nbsp;</td><td>
                            &nbsp;</td></tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="Label8" runat="server" >City:</asp:Label></td><td class="auto-style1">
                            <asp:TextBox ID="txt_City" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_City_RoundedCornersExtender" runat="server" Enabled="True" TargetControlID="txt_City" Radius="10">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                            &nbsp;</td><td>
                            &nbsp;</td></tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="Label9" runat="server" >Pin Code:</asp:Label></td><td class="auto-style1">
                            <asp:TextBox ID="txt_Pin" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_Pin_RoundedCornersExtender" runat="server" Enabled="True" TargetControlID="txt_Pin" Radius="10">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                            &nbsp;</td><td>
                            &nbsp;</td></tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="Label10" runat="server" >State:</asp:Label></td><td class="auto-style1">
                            <asp:TextBox ID="txt_state" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_state_RoundedCornersExtender" runat="server" Enabled="True" TargetControlID="txt_state" Radius="10">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                            &nbsp;</td><td>
                            &nbsp;</td></tr>

                    <tr><td class="auto-style3">
                        <asp:Label ID="Label2" runat="server" >Bank:</asp:Label></td><td class="auto-style1">
                        <asp:TextBox ID="txt_bank" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_bank_RoundedCornersExtender" runat="server" Enabled="True" TargetControlID="txt_bank" Radius="10">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                                            <asp:Panel ID="user_err1" runat="server" Visible="true" style="text-align: center">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp;&nbsp;Please provide Bank &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td><td>
                            &nbsp;</td></tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="Label3" runat="server" >Customer:</asp:Label></td><td class="auto-style1">
                        <asp:TextBox ID="txt_customer" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_customer_RoundedCornersExtender" runat="server" Enabled="True" TargetControlID="txt_customer" Radius="10">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                                            <asp:Panel ID="user_err2" runat="server" Visible="true" style="text-align: center">
                                            <div class="pam login_error_box uiBoxRed">
                                                <div class="fsl fwb fcb">&nbsp;&nbsp;Please provide Customer &nbsp;</div>
                                            </div>
                                            </asp:Panel>
                                        </td><td>
                            &nbsp;</td></tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="Label4" runat="server" >Status:</asp:Label></td>
                        
                        <td class="auto-style1">
                            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                            <asp:RadioButton ID="rdb_active" Text="Active" GroupName="status" runat="server" AutoPostBack="True" OnCheckedChanged="rdb_active_CheckedChanged" />
                            <asp:RadioButton ID="rdb_inactive" Text="Inactive" GroupName="status" runat="server" AutoPostBack="True" OnCheckedChanged="rdb_inactive_CheckedChanged" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>

                        <td style="width: 30%" valign="bottom">
                            &nbsp;</td>

                        <td>
                            &nbsp;</td>

                    </tr>
                    <tr><td class="auto-style3">
                        <asp:Label ID="Label5" runat="server" >Site Number:</asp:Label></td><td class="auto-style1">
                        <asp:TextBox ID="txt_sitenumber" runat="server" Height="20px" Width="320px"></asp:TextBox>
                            <asp:RoundedCornersExtender ID="txt_sitenumber_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_sitenumber">
                            </asp:RoundedCornersExtender>
                    </td><td style="width: 30%" valign="bottom">
                            &nbsp;</td><td>
                            &nbsp;</td></tr>
                    </table>
                   <hr />
                   <table style="width:100%">
                       <tr>
                           <td class="auto-style2">
                        </td>
                        <td>
                            <asp:Button ID="btn_Update" Height="30px" Width="190px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Submit" OnClick="btn_Update_Click" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   </table>
            </div>

</asp:Content>
