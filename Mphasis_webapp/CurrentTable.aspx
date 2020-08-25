<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrentTable.aspx.cs" Inherits="Mphasis_webapp.CurrentTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .hfield
        {
            text-decoration:none;
        }
        .auto-style1
        {
            width: 100%;
        }
        .bordersetting
        {
            border:dotted 1px black;
			text-align: center;
        }
		.auto-style4
        {
            width: 165px;
			text-align: center;
        }
        .auto-style5
        {
            width: 169px;
			text-align: center;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Panel ID="white_panel" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="5" HorizontalAlign="Center" Width="90%">
     <div style="color: #003366; font-family:Cambria, Cochin, Georgia, Times ;font-variant:small-caps; font-size:xx-large; font-weight: bolder;">
        <u>MSP-WISE PERFORMANCE REPORT</u>
         </div>
   <asp:Label ID="P8" Visible="false" runat="server"></asp:Label>
    <div style="font-size:large;font-family:Cambria, Cochin, Georgia, Times">
        <div align="center">
            <div align="center" style="font-family:Cambria, Cochin, Georgia, Times;font-variant:small-caps">
                <br />
                <table cellpadding="0" cellspacing="0" style="background-color:#D3DEEF">
                    <tr>
                        <td style="font-weight:bold;background-color:#5078B3;border:dotted 1px black;color:white" class="auto-style4">
                            
                            MSP</td>
                        <td style="font-weight:bold;background-color:#5078B3;border:dotted 1px black;color:white" class="auto-style5">

                            PERCENTAGE</td>
                    </tr>
                    <tr>
                        <td style="border:dotted 1px black" class="auto-style4">AGS</td>
                        <td style="border:dotted 1px black" class="auto-style5">
                            <asp:Label ID="P1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td style="border:dotted 1px black;display:none" class="auto-style4">
                            
                            DCB</td>
                        <td style="border:dotted 1px black" class="auto-style5">

                            <asp:Label ID="P2" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="border:dotted 1px black" class="auto-style4">
                            
                            EURONET</td>
                        <td style="border:dotted 1px black" class="auto-style5">

                            <asp:Label ID="P3" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="border:dotted 1px black" class="auto-style4">
                            
                            FSS</td>
                        <td style="border:dotted 1px black" class="auto-style5">

                            <asp:Label ID="P4" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>

                            <asp:Label ID="P5" runat="server" style="display:none"></asp:Label>

                        </td>
                    </tr>
                    <tr style="display:none">
                        <td style="border:dotted 1px black;display:none " class="auto-style4">
                            
                            ICICI</td>
                        <td style="border:dotted 1px black" class="auto-style5">

                            <asp:Label ID="P6" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="border:dotted 1px black" class="auto-style4">
                            
                            HITACHI</td>
                        <td style="border:dotted 1px black" class="auto-style5">

                            <asp:Label ID="P7" runat="server"></asp:Label>

                        </td>
                    </tr>
                    
                    <tr>
                        <td style="border:dotted 1px black" class="auto-style4">TCPSL</td>
                        <td style="border:dotted 1px black" class="auto-style5">
                            <asp:Label ID="P9" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4" style="border:dotted 1px black">
                            HDFC</td>
                        <td class="auto-style5" style="border:dotted 1px black">
                            <asp:Label ID="P10" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <!--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>" SelectCommand="select VID,ATMID,substring(vdate,4,3) + substring(vdate,1,2)+ substring(vdate,6,5) as [AUDIT DATE], vtime as [AUDIT TIME] from DR_CTP where vid not like '%XXX%' order by srno desc"></asp:SqlDataSource>-->
                <table class="auto-style1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">AGS STATUS = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%> </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW1" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW1" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT1" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW2" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW2" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT2" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW3" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW3" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT3" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW4" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW4" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT4" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP4" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW5" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW5" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT5" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP5" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Batteries</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW6" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW6" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT6" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP6" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">U.P.S</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW7" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW7" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT7" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP7" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Skimming Device Found</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW8" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW8" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT8" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP8" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available </td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW9" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW9" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT9" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP9" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW10" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW10" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT10" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP10" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW81" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW81" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT81" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP81" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Dustbin</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW82" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW82" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT82" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP82" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Door Mat</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW83" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW83" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT83" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP83" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Main Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW84" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW84" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT84" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP84" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Backroom Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW85" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW85" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT85" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP85" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lollypop</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW86" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW86" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT86" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP86" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Any Incident</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW111" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW111" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT111" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP111" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">
                            <asp:Label ID="LA" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                
                <table style="display:none" class="auto-style1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">DCB = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%></td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW51" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW51" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT51" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP51" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW52" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW52" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT52" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP52" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW53" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW53" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT53" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP53" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW54" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW54" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT54" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP54" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW55" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW55" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT55" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP55" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">notice board</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW56" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW56" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT56" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP56" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW57" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW57" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT57" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP57" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW58" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW58" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT58" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP58" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW59" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW59" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT59" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP59" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">electricity bill paid</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW60" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW60" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT60" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP60" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">
                            <asp:Label ID="LA4" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <br />
                <table class="auto-style1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">EURONET = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%></td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW21" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW21" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT21" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP21" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW22" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW22" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT22" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP22" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW23" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW23" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT23" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP23" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW24" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW24" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT24" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP24" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW25" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW25" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT25" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP25" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Batteries</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW26" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW26" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT26" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP26" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">U.P.S</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW27" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW27" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT27" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP27" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Skimming Device Found</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW28" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW28" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT28" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP28" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available </td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW29" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW29" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT29" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP29" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW30" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW30" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT30" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP30" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW87" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW87" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT87" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP87" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Dustbin</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW88" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW88" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT88" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP88" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Door Mat</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW89" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW89" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT89" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP89" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Main Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW90" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW90" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT90" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP90" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Backroom Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW91" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW91" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT91" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP91" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lollypop</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW92" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW92" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT92" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP92" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Any Incident</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW112" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW112" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT112" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP112" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">
                            <asp:Label ID="LA1" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <br />
                <table class="auto-style1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">FSS = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%></td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW61" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW61" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT61" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP61" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW62" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW62" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT62" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP62" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW63" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW63" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT63" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP63" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW64" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW64" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT64" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP64" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW65" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW65" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT65" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP65" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Batteries</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW66" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW66" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT66" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP66" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">U.P.S</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW67" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW67" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT67" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP67" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Skimming Device Found</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW68" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW68" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT68" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP68" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available </td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW69" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW69" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT69" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP69" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW70" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW70" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT70" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP70" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW93" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW93" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT93" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP93" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Dustbin</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW94" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW94" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT94" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP94" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Door Mat</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW95" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW95" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT95" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP95" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Main Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW96" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW96" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT96" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP96" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Backroom Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW97" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW97" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT97" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP97" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lollypop</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW98" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW98" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT98" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP98" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Any Incident</td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LNW113" runat="server" ForeColor="Red"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LW113" runat="server" ForeColor="Green"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LT113" runat="server"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                            
                            <asp:Label ID="LP113" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">
                            <asp:Label ID="LA5" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                
                <table class="auto-style1" cellpadding="0" cellspacing="0" style="display:none">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">HDFC = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%></td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW41" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW41" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT41" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP41" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW42" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW42" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT42" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP42" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW43" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW43" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT43" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP43" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW44" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW44" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT44" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP44" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW45" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW45" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT45" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP45" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">notice board</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW46" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW46" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT46" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP46" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW47" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW47" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT47" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP47" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW48" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW48" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT48" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP48" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW49" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW49" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT49" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP49" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">electricity bill paid</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW50" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW50" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT50" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP50" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">
                            <asp:Label ID="LA3" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                
               
                
                <table class="auto-style1" style="display:none" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">ICICI = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%></td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW71" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW71" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT71" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP71" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW72" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW72" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT72" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP72" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW73" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW73" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT73" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP73" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW74" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW74" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT74" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP74" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW75" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW75" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT75" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP75" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">notice board</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW76" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW76" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT76" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP76" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW77" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW77" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT77" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP77" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW78" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW78" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT78" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP78" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW79" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW79" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT79" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP79" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">electricity bill paid</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW80" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW80" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT80" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP80" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">
                            <asp:Label ID="LA6" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <br />
                
                
                <table class="auto-style1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">HITACHI = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%></td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW31" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW31" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT31" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP31" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW32" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW32" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT32" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP32" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW33" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW33" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT33" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP33" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW34" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW34" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT34" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP34" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW35" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW35" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT35" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP35" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Batteries</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW36" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW36" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT36" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP36" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">U.P.S</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW37" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW37" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT37" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP37" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Skimming Device Found</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW38" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW38" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT38" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP38" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available </td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW39" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW39" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT39" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP39" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW40" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW40" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT40" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP40" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW99" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW99" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT99" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP99" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Dustbin</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW100" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW100" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT100" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP100" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Door Mat</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW101" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW101" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT101" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP101" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Main Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW102" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW102" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT102" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP102" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Backroom Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW103" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW103" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT103" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP103" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lollypop</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW104" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW104" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT104" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP104" runat="server"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Any Incident</td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LNW114" runat="server" ForeColor="Red"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LW114" runat="server" ForeColor="Green"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LT114" runat="server"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                            
                            <asp:Label ID="LP114" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">
                            <asp:Label ID="LA2" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
    
                <hr />
                <br />
                <table class="auto-style1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">TCPSL = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%></td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW11" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW11" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT11" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP11" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW12" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW12" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT12" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP12" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW13" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW13" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT13" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP13" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW14" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW14" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT14" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP14" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW15" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW15" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT15" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP15" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Batteries</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW16" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW16" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT16" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP16" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">U.P.S</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW17" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW17" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT17" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP17" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Skimming Device Found</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW18" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW18" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT18" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP18" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available </td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW19" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW19" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT19" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP19" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW20" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW20" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT20" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP20" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW105" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW105" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT105" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP105" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Dustbin</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW106" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW106" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT106" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP106" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Door Mat</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW107" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW107" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT107" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP107" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Main Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW108" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW108" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT108" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP108" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Backroom Door</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW109" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW109" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT109" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP109" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lollypop</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW110" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW110" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT110" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP110" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Any Incident</td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LNW115" runat="server" ForeColor="Red"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LW115" runat="server" ForeColor="Green"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LT115" runat="server"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                            
                            <asp:Label ID="LP115" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">
                            <asp:Label ID="LA0" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                  <table class="auto-style1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="5" style="font-weight:bold;font-size:larger;border:dotted 1px black;background-color:#5078B3;text-align:center;color:white">HDFC = <%=DateTime.Now.Date.ToString("dd'.'MM'.'yyyy")%></td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td style="text-align:left;border:dotted 1px black;width:32%"><u>particular</u></td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">not working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">working</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">total</td>
                        <td style="border:dotted 1px black;width:17%;text-align: center">percentage</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Housekeeping</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW116" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW116" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT116" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP116" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">ac working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW117" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW117" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT117" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP117" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">atm working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW118" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW118" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT118" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP118" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">signage working</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW119" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW119" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT119" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP119" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">extinguisher available</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW120" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW120" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT120" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP120" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Batteries</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW121" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW121" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT121" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP121" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">U.P.S</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW122" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW122" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT122" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP122" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Skimming Device Found </td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW123" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW123" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT123" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP123" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Cbd Available </td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW124" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW124" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT124" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP124" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">Backroom Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW125" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW125" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT125" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP125" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lobby Light</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW126" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW126" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT126" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP126" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Dustbin</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW127" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW127" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT127" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LP127" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Door Mat</td>
                        <td class="bordersetting">
                            <asp:Label ID="LNW128" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LW128" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                        <td class="bordersetting">
                            <asp:Label ID="LT128" runat="server"></asp:Label>
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LP128" runat="server"></asp:Label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Main Door</td>
                        <td class="bordersetting">
                         
                            <asp:Label ID="LNW129" runat="server" ForeColor="Red"></asp:Label>
                         
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LW129" runat="server" ForeColor="Green"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LT129" runat="server"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LP129" runat="server"></asp:Label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Backroom Door</td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LNW130" runat="server" ForeColor="Red"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LW130" runat="server" ForeColor="Green"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LT130" runat="server"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LP130" runat="server"></asp:Label>
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Lollypop</td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LNW131" runat="server" ForeColor="Red"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LW131" runat="server" ForeColor="Green"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LT131" runat="server"></asp:Label>
                           
                        </td>
                        <td class="bordersetting">
                           
                            <asp:Label ID="LP131" runat="server"></asp:Label>
                           
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left; border: dotted 1px black">
                            Any Incident</td>
                        <td class="bordersetting">
                           
                          
                           
                            <asp:Label ID="LNW132" runat="server" ForeColor="Red"></asp:Label>
                           
                          
                           
                        </td>
                        <td class="bordersetting">
                           
                           
                           
                            <asp:Label ID="LW132" runat="server" ForeColor="Green"></asp:Label>
                           
                           
                           
                        </td>
                        <td class="bordersetting">
                           
                           
                           
                            <asp:Label ID="LT132" runat="server"></asp:Label>
                           
                           
                           
                        </td>
                        <td class="bordersetting">
                               
                            <asp:Label ID="LP132" runat="server"></asp:Label>
                               
                        </td>
                    </tr>
                    <tr style="background-color: #D3DEEF">
                        <td colspan="4" class="bordersetting" >Average</td>
                        <td class="bordersetting">

                            <asp:Label ID="LA7" runat="server"></asp:Label>

                        </td>
                    </tr>
                </table>
            </div>
            </div>
        </div>
        </asp:Panel>
</asp:Content>
