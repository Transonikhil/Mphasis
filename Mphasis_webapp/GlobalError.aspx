<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GlobalError.aspx.cs" Inherits="Mphasis_webapp.GlobalError" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">  
    body, html { height: 100%; }
    #right { height: 100%; }
    .accordion {  width: 400px;  height:200px;font-size:xx-large;background-color:#003366;color:white; }
    .accordionHeader {  background-color:darkgray; }  
    </style>

    <title></title>

    <link href="http://cdn.kendostatic.com/2013.2.918/styles/kendo.common.min.css" rel="stylesheet" />
    <link href="http://cdn.kendostatic.com/2013.2.918/styles/kendo.rtl.min.css" rel="stylesheet" />
    <link href="http://cdn.kendostatic.com/2013.2.918/styles/kendo.default.min.css" rel="stylesheet" />
    <link href="http://cdn.kendostatic.com/2013.2.918/styles/kendo.dataviz.min.css" rel="stylesheet" />
    <link href="http://cdn.kendostatic.com/2013.2.918/styles/kendo.dataviz.default.min.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="http://cdn.kendostatic.com/2013.2.918/js/kendo.all.min.js"></script>
    <script src="http://cdn.kendostatic.com/2013.2.918/js/kendo.timezones.min.js"></script>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    
    <form id="form1" runat="server" style="background-image:url(Image/squairy_light.png);min-height:100%">
        <asp:ScriptManager ID="s" runat="server"></asp:ScriptManager>
        <div align="center" style="min-height:100%;padding-top:3em">
            <div align="center" style="width:50%" >
                <div class="k-block" align="center" style="background-color:#003366">
                    <asp:Accordion  ID="Accordion2" SelectedIndex="-1" HeaderCssClass="accordion" ContentCssClass="accordionHeader" RequireOpenedPane="false" runat="server" FadeTransitions="true" Height="151px">
                        
<Panes>
 <asp:AccordionPane ID="AccordionPane1" runat="server">
                                <Header>
                                    <div style="padding-top:2.5em">
                                    
                                        <asp:Label ID="head" runat="server" Font-Names="Segoe UI"></asp:Label>

                                    </div>

                                </Header>
                                <Content>
                                    <asp:Label ID="lbl_err" Text="error display here" runat="server"></asp:Label>
                                </Content>
                                </asp:AccordionPane>

 </Panes>
 </asp:Accordion>
                </div>
            </div>
            
            <div class="k-header k-block k-shadow" align="center" style="width:50%;font-size:large">
                    Click <asp:LinkButton ID="lb" runat="server" OnClick="lb_Click" Text="here"></asp:LinkButton> to go back to login page
                </div>
        </div>
    </form>
</body>
</html>
