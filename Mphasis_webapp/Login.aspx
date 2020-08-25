<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Mphasis_webapp.Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <style type="text/css">
        .overlay 
        {
         position:absolute;
         background-color: white;
         top: 0px;
         left: 10em;
         width: 50%;
         height: 50px;
         opacity: 0.8;
         -moz-opacity: 0.8;
         filter: alpha(opacity=80);
         -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=80)";
         z-index: 10000;
		line-height: 100%;
        }
        .watermark
        {
            color: lightgray;
        }
        .accordion {  
            width: 400px;  
        }  
          
        .accordionHeader {  
            border: 1px solid #2F4F4F;  
            color: white;  
            background-color: #2E4d7B;  
            font-family: Arial, Sans-Serif;  
            font-size: 12px;  
            font-weight: bold;  
            padding: 5px;  
            margin-top: 5px;  
            cursor: pointer;  
        }  
          
        .accordionHeaderSelected {  
            border: 1px solid #2F4F4F;  
            color: white;  
            background-color: #5078B3;  
            font-family: Arial, Sans-Serif;  
            font-size: 12px;  
            font-weight: bold;  
            padding: 5px;  
            margin-top: 5px;  
            cursor: pointer;  
        }  
          
        .accordionContent {  
            background-color: #D3DEEF;  
            border: 1px dashed #2F4F4F;  
            border-top: none;  
            padding: 5px;  
            padding-top: 10px;  
        }  
    </style>  
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <%--<div align="center">
    <div class="overlay" style="font-size:xx-large;color:Red">
        *Console Is Under Maintenance*
        </div></div>--%>
    <div class="page" style="background-image: url('/mAuditV6-modern/Image/squairy_light.png'); text-align:center; background-repeat: repeat;">
        <div style="background-image: url('Image/b3.png'); background-repeat: repeat;">
        
        <table style="width:100%">
            <tr>
                <td>
                    <div class="title">
                            <div style="font-weight: 700;margin: 0px;padding: 0px 0px 0px 20px;color: #f9f9f9;border: none;line-height: 2em;font-size: 2em;color: #000000; font-size: 36px; font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;">
                            mAudit Console
                             </div>
                    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                </td>
                <td align="right">
                    <table style="font-family:Cambria, Cochin, Georgia, Times">
                            <tr valign="baseline">
                                <td valign="bottom" style="font-size:large; text-align: left;">
                                    <table>
                                        <tr>
                                            <td>
                                                Login Id:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox runat="server" ID="txt_login" style="text-align: left"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="txt_login_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txt_login" ViewStateMode="Enabled" WatermarkText="User Name" WatermarkCssClass="watermark">
                                                </asp:TextBoxWatermarkExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                
                                </td>
                                <td style="font-size:large; text-align: left;" valign="bottom">

                                    <table>
                                        <tr>
                                            <td>
                                                Password:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox runat="server" ID="txt_pwd" style="text-align: left" TextMode="Password"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="txt_pwd_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txt_pwd" WatermarkText="Password" WatermarkCssClass="watermark">
                                                </asp:TextBoxWatermarkExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="bottom" style="padding-bottom:0.5em;font-variant:small-caps;">
                                    <asp:Button Font-Names="Cambria, Cochin, Georgia, Times" ID="btn_go" Text="Login" runat="server" Height="40px" Width="70px" BackColor="#003366" Font-Size="Large" ForeColor="White" OnClick="btn_go_Click"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        <br />
        <div style="text-align:center">
            <img alt="" src="Image/bigLogo.bmp" />
        </div>
        <br />
        <br />
    </form>
</body>
</html>
