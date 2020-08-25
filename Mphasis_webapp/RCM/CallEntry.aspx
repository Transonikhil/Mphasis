<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CallEntry.aspx.cs" Inherits="Mphasis_webapp.RCM.CallEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

     function windowOnLoad() {
         document.getElementById('txtfollowup').readOnly = true;
         
     }

     window.onload = windowOnLoad;
    </script>
    <style type="text/css">
        .Button {
            background-color: #6666FF;
            font-weight: bold;
            color: white;
            -webkit-border-radius: 20px;
            border-radius: 5px;
            cursor: pointer;
        }

        .border {
            border-bottom: #000000 solid 1px;
            border-right: #000000 solid 1px;
            font-size: 12px;
            font-family: Arial;
            height: 20px;
        }

        .confirm-dialog {
            margin: 0px auto;
            width: 291px; /*PADDING-TOP: 14px;*/
            position: relative;
            border: orange solid 1px;
            background-color: white;
            top: 2px;
            left: 0px;
            height: 142px;
        }

            .confirm-dialog .inner {
                /*PADDING-RIGHT: 20px;PADDING-LEFT: 20px;*/ /*PADDING-BOTTOM: 11px;
            FLOAT: left;MARGIN: 0px 0px -20px 0px;*/
                width: 290px; /*PADDING-TOP: 0px;*/
                height: 129px;
            }

            .confirm-dialog H2 {
                font-weight: bold;
                font-size: 1.25em;
                color: #000000;
                text-align: center;
            }

            .confirm-dialog input {
            }

            .confirm-dialog .base {
                /*PADDING-BOTTOM: 4px;MARGIN-LEFT: -11px;MARGIN-RIGHT: -11px; 
            PADDING-TOP: 4px;*/
                text-align: center;
            }

        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .auto-style1 {
            height: 16px;
            color: black;
        }

        .auto-style2 {
            height: 20px;
            color: black;
        }

        .auto-style3 {
            width: 27%;
            height: 23px;
            color: black;
        }

        .auto-style4 {
            width: 25%;
            height: 23px;
            color: black;
        }

        .auto-style5 {
            width: 21%;
            height: 23px;
            color: black;
        }
    </style>

    <script type="text/javascript">
        function chkfile() {
            try {
                var x = document.getElementById("ddsubcall").value;
                var rem = document.getElementById("txtrem").value;
                var fol = document.getElementById("txtfollowup").value;
                var ufile = document.getElementById("FileUpload1").value;

                if (x == "CLOSED" || x == "RESOLVED" || x == "CLOSE") {
                    if (rem.trim() == "" || fol.trim() == "") {
                        alert('Please enter followup remark.');
                        return false;
                    }
                    else {
                        return true;
                    }
                }

                if (x == "DISPATCHED") {
                    if (rem.trim() == "" || fol.trim() == "") {
                        alert('Please enter followup remark and time.');
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            }
            catch (err) {
                alert('Please check attachment');
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <caption style="font-family: Arial; font-size: x-large; background-color: #003366; color: white; margin-bottom: 8px; border: #000000 1px solid">
                CALL UPDATE</caption>
            <tr>
                <td style="width: 49%">
                    <table style="width: 100%; border: #000000 1px solid; margin-bottom: 16px; font-size: 12px; font-family: Arial"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="4" style="text-align: center; background-color: #003366; color: white; border: #000000 solid 1px">CALL INFO
                            </td>
                        </tr>
                        <tr>
                            <td style="color: black; border-bottom: #000000 solid 1px; border-right: #000000 solid 1px" class="auto-style3">Ticket No
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px"
                                class="auto-style3">
                                <asp:Label ID="lbldocket" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; color: black;" class="auto-style4">Ticket Created Date
                            </td>
                            <td style="border-bottom: #000000 solid 1px" class="auto-style5">
                                <asp:Label ID="lblopendate" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 27%; height: 23px; color: black; border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">Client Ticket No</td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; width: 27%; height: 23px">
                                <asp:Label ID="lblbankdocket" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; color: black;">Ticket Created Time
                            </td>
                            <td style="border-bottom: #000000 solid 1px; height: 23px">
                                <asp:Label ID="lblopentime" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; width: 27%; height: 23px; color: black;">Client
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; width: 27%; height: 23px;">
                                <asp:Label ID="lblclient" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; color: black;">Ticket Created By
                            </td>
                            <td style="border-bottom: #000000 solid 1px; height: 23px">
                                <asp:Label ID="lbldocketcreatedby" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; width: 27%; height: 23px; color: black;">ATM ID
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; width: 27%; height: 23px;">
                                <asp:Label ID="lblatmid" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; color: black;">Downtime
                            </td>
                            <td style="border-bottom: #000000 solid 1px; height: 23px">
                                <asp:Label ID="lbldowntime" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td style="width: 27%; height: 23px; color: black;">&nbsp;</td>
                            <td style="border-right: #000000 solid 1px; width: 27%; height: 23px;">
                                <asp:Label ID="lbldt" runat="server" ForeColor="Black" Font-Bold="true" Visible="false"></asp:Label>
                            </td>
                            <td></td>
                            <td>
                                <asp:Label ID="Label1" runat="server" ForeColor="Black" Font-Bold="True" Visible="False"></asp:Label>
                               <%--<asp:Label ID="Label2" runat="server" ForeColor="Black" Font-Bold="True" Visible="False"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; background-color: #003366; color: white; border-bottom: #000000 solid 1px; border-top: #000000 solid 1px">SITE INFORMATION
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; height: 23px; color: black;">Bank
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px;">
                                <asp:Label ID="lblbank" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="vertical-align: top; color: black;">Address
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                        <td></td>
                        <td></td>
                            <%--<td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; height: 23px; color: black;">Onsite/Offsite
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px;">
                                <asp:Label ID="lblonoff" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>--%>
                            <td rowspan="4" colspan="2" style="vertical-align: top; border-bottom: #000000 solid 1px;">
                                <asp:Label ID="lbladd" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; height: 23px; color: black;">State
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px;">
                                <asp:Label ID="lblbranch" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; color: black;">Location Name
                            </td>
                            <td class="auto-style2" style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px;">
                                <asp:Label ID="lblloc" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: white; height: 23px; color: black;">&nbsp;
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px;">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; border-top: #000000 solid 1px; height: 23px; color: black;">TAT
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px;">
                                <asp:Label ID="lbltat" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; height: 23px; color: black;">City
                            </td>
                            <td style="border-bottom: #000000 solid 1px;">
                                <asp:Label ID="lbldist" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; height: 23px; color: black;">RCM Name
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px;">
                                <asp:Label ID="lblname1" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; height: 23px; color: black;">Project
                            </td>
                            <td style="border-bottom: #000000 solid 1px;">
                                <asp:Label ID="lblnum1" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <%--<td style="border-right: #000000 solid 1px; height: 23px; color: black;">Custodian 2:
                            </td>
                            <td style="border-right: #000000 solid 1px;">
                                <asp:Label ID="lblname2" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>--%>
                            <td style="border-right: #000000 solid 1px; height: 23px; color: black;">Region
                            </td>
                            <td>
                                <asp:Label ID="lblnum2" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 2%"></td>
                <td style="width: 49%">
                    <table style="width: 100%; border: #000000 1px solid; margin-bottom: 16px; margin-top: 0px; height: 362px; font-size: 12px; font-family: Arial"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="4" style="text-align: center; background-color: #003366; color: white; height: 14px; border-bottom: #000000 solid 1px">
                            PROBLEM INFORMATION
                        </tr>
                        <tr>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; width: 27%; height: 23px; color: black;">Problem Type
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px; width: 27%">
                                <asp:Label ID="lblprblmtype" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; width: 25%; height: 23px; color: black;">Priority
                            </td>
                            <td style="border-bottom: #000000 solid 1px; width: 21%">
                                <asp:Label ID="lblservicetype" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; height: 23px; color: black;">Problem Code
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">
                                <asp:Label ID="lblprblmcode" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 72px; vertical-align: top; color: black">
                                <span>Call Open Remark</span><br />
                                <asp:Label ID="lblremark" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; background-color: #003366; color: white; height: 14px; border-bottom: #000000 solid 1px; border-top: #000000 solid 1px">CALL DISPATCH INFORMATION
                            </td>
                        </tr>
                        <tr>

                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black; height: 20px">Call Opened From</td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">
                                <asp:Label ID="txtdisploc" runat="server" Height="14px" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="auto-style1" style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black;">Resolved Date</td>
                            <td style="border-bottom: #000000 solid 1px">
                                <asp:DropDownList ID="ddrescode" runat="server" Font-Size="10px" Width="128px" Visible="false">
                                    <asp:ListItem>Resolution Code 1</asp:ListItem>
                                    <asp:ListItem>Resolution Code 2</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblresdate" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black; height: 20px">Dispatch Date
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">
                                <asp:Label ID="lbldispatchdate" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black;">Resolved Time</td>
                            <td style="border-bottom: #000000 solid 1px;">
                                <asp:Label ID="txtrestype" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>

                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black; height: 20px">Dispatch Time</td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">
                                <asp:Label ID="txtcustreach" runat="server" Height="14px" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black;">Resolved By</td>
                            <td style="border-bottom: #000000 solid 1px;">
                                <asp:Label ID="txtresolvedby" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black;">Call Close Date
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">
                                <asp:Label ID="lblclosedate" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black;">Call Status
                            </td>
                            <td style="border-bottom: #000000 solid 1px; height: 20px">
                                <asp:Label ID="lblcallstatus" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right: #000000 solid 1px; color: black;">Call Close Time
                            </td>
                            <td style="border-right: #000000 solid 1px">
                                <asp:Label ID="lblclosetime" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; color: black;">Sub Call Type
                            </td>
                            <td style="height: 20px">
                                <asp:Label ID="lblsubcalltype" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td colspan="4" style="text-align: left; background-color: #003366; color: white; height: 15px; border-bottom: #000000 solid 1px; border-top: #000000 solid 1px">Consumables Used
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">JP Ribbon :
                                <asp:DropDownList ID="ddjpribbon" runat="server" Width="50px" Height="18px">
                                    <asp:ListItem>0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">JP Roll :
                                <span>
                                    <asp:DropDownList ID="ddjproll" runat="server" Width="50px" Height="18px">
                                        <asp:ListItem>0</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </td>
                            <td style="border-bottom: #000000 solid 1px; border-right: #000000 solid 1px">RP Ribbon :
                                <asp:DropDownList ID="ddrpribbon" runat="server" Width="50px" Height="18px">
                                    <asp:ListItem>0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="border-bottom: #000000 solid 1px;">RP Roll :
                                <asp:DropDownList ID="ddrproll" runat="server" Width="50px" Height="18px">
                                    <asp:ListItem>0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td style="height: 23px; background-color: #003366; color: white;">Travel Expense
                            </td>
                            <td>
                                <asp:TextBox ID="txttravelexpense" runat="server" Height="14px"></asp:TextBox>
                            </td>
                            <td style="color: black;">Travel Remark
                            </td>
                            <td>
                                <asp:TextBox ID="txttravelremark" runat="server" Height="14px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="height: 23px; background-color: #003366; color: white;">Attach File (on call close):
                            </td>
                            <td colspan="3">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:Button Text="Upload" ID="btn_upload" Visible="false" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server">
                        <table id="tblremark" style="width: 100%; border: #000000 solid 1px" cellpadding="0"
                            cellspacing="0">
                            <tr align="center">
                                <td colspan="6" style="font-family: Arial; background-color: #003366; color: white; border-bottom: #000000 solid 1px">Follow Up Details
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 7%; border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; font-size: 13px; font-family: Arial;">Date
                                </td>
                                <td style="width: 7%; border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; font-size: 13px; font-family: Arial;">Time
                                </td>
                                <td style="width: 10%; border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; font-size: 13px; font-family: Arial;">Updated By
                                </td>
                                <td style="width: 35%; border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; font-size: 13px; font-family: Arial;">Comments
                                </td>
                                <td style="width: 15%; border-right: #000000 solid 1px; border-bottom: #000000 solid 1px; font-size: 13px; font-family: Arial;">Sub Call Type
                                </td>
                                <td style="width: 33%; border-bottom: #000000 solid 1px; font-size: 13px; font-family: Arial;"><asp:Label runat="server" ID="lblfollowuptext">Date and time of Dispatch/Resolved/Closure<br />(yyyy-MM-dd HH:mm:ss)</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px"></td>
                                <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px"></td>
                                <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px"></td>
                                <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px">
                                    <asp:TextBox ID="txtrem" runat="server" TextMode="MultiLine" Width="95%" Style="margin-top: 5px; margin-left: 3px"></asp:TextBox>
                                </td>
                                <td style="border-right: #000000 solid 1px; border-bottom: #000000 solid 1px">
                                    <asp:DropDownList ID="ddsubcall" runat="server" Font-Size="10px" Width="130px" onchange="getactivity()">
                                        <%--<asp:ListItem>DISPATCHED</asp:ListItem>
                                        <asp:ListItem>RESOLVED</asp:ListItem>
                                        <asp:ListItem>CLOSED</asp:ListItem>
                                        <asp:ListItem>RE-OPEN</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <td style="border-bottom: #000000 solid 1px;align:center">
                                    <asp:TextBox ID="txtfollowup" runat="server" Width="150px" ClientIDMode="Static" >
                                    </asp:TextBox>&nbsp;<cc1:TimeSelector ID="txtvtime" Width="130px" runat="server" AllowSecondEditing="true" SelectedTimeFormat="TwentyFour"></cc1:TimeSelector>
                                    <asp:CalendarExtender ID="txtfollowup_CalendarExtender" runat="server" 
                                        TargetControlID="txtfollowup" Format="yyyy'-'MM'-'dd">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <%=getRemarks()%>
                            <tr>
                                <td colspan="6" style="text-align: center">
                                    <asp:Button BackColor="#003366" Font-Size="18px" ForeColor="White" ID="btnupdate"
                                        runat="server" Text="Update" CssClass="Button" Width="133px" OnClick="btnupdate_Click"
                                        OnClientClick="return chkfile();" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btn_Submit0" runat="server" OnClick="btn_Submit0_Click" Visible="false" />
                                    <asp:Button ID="btn_review"  CssClass="Button" runat="server"
                                        BackColor="#003366" Font-Size="18px" ForeColor="White" Visible="false"  Text="Review Call" />

                                    <script>
                                        function review() {
                                            document.getElementById('div_review').style.display = 'block';
                                            return false;
                                        }
                                    </script>

                                    <div id="div_review" style="height: 200px; width: 500px; background-color: White; border: solid 5px #003366; display: none; top: 50%; left: 30%; position: absolute">
                                        <div align="right" onclick="document.getElementById('div_review').style.display='none'"
                                            style="display: inline block; cursor: pointer; padding-right: 0.5em; font-size: large; color: #003366; font-weight: bold">
                                            X
                                        </div>
                                        <div style="font-size: large; color: #003366; display: inline block;">
                                            -Review Details-<hr />
                                        </div>
                                        <table width="100%" cellpadding="5" cellspacing="5" align="left">
                                            <tr>
                                                <td>UserName:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_user" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Comments:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_rcomment" TextMode="MultiLine" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td align="left">
                                                    <asp:Button ID="btn_reopen" CssClass="Button" runat="server" BackColor="#003366" Font-Size="18px" ForeColor="White" Text="Re-Open Call" />
                                                    <asp:Button ID="btn_reviewd" CssClass="Button" runat="server" BackColor="#003366" Font-Size="18px" ForeColor="White" Text="Review Call" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>

            <tr>
                <td colspan="4">
                    <fieldset>
                        <legend title="">Uploaded Attachment</legend>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" EnableViewState="False"
                            OnRowDataBound="GridView1_RowDataBound" DataSourceID="SqlDataSource1">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:BoundField DataField="ATTACHED FILE" SortExpression="ATTACHED FILE" HeaderText="ATTACHED FILE" HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                                <asp:BoundField DataField="SIZE" SortExpression="SIZE" HeaderText="SIZE" HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" ReadOnly="True" />
                                <asp:BoundField DataField="ATTACHED DATE" SortExpression="ATTACHED DATE" HeaderText="ATTACHED DATE" HeaderStyle-ForeColor="Blue" HeaderStyle-Font-Size="Larger" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                            </Columns>
                            <EmptyDataTemplate>
                                NO RECORDS FOUND
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                            <PagerStyle HorizontalAlign="Center" BackColor="#8DB4E2" />
                            <RowStyle HorizontalAlign="Center" ForeColor="#333333" />
                            <AlternatingRowStyle HorizontalAlign="Center" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                            SelectCommand="Select distinct Attachedfile as [ATTACHED FILE],remarks as [COMMENTS],Size as [SIZE], CONVERT(VARCHAR(10), AttachedDate, 103) + ' '+ 
                                                CONVERT(VARCHAR(5), AttachedDate, 108) as [ATTACHED DATE],srno from Activity_Log where vid like @doc order by srno desc">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="doc" QueryStringField="doc" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </fieldset>
                </td>
            </tr>
            <%--<tr>
                <td colspan="3" style="text-align: center">
                    <asp:Button ID="btnjust" CssClass="Button" runat="server" BackColor="#003366" Font-Size="18px" Width="200" ForeColor="White" Text="ADD JUSTIFICATION" OnClick="btnjust_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="font-weight: bold">Justification Details
                </td>
            </tr>
            <tr>
                <td colspan="3" style="font-weight: bold">

                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" EnableViewState="False" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:BoundField DataField="JUSTIFICATION NO" HeaderText="JUSTIFICATION NO" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-Font-Size="Larger" HeaderStyle-ForeColor="Blue" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                            <asp:BoundField DataField="CREATED DATE" HeaderText="CREATED DATE" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-Font-Size="Larger" HeaderStyle-ForeColor="Blue" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" ReadOnly="True" />
                            <asp:BoundField DataField="CREATED BY" HeaderText="CREATED BY" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-Font-Size="Larger" HeaderStyle-ForeColor="Blue" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                            <asp:BoundField DataField="QUOTED AMOUNT" HeaderText="QUOTED AMOUNT" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-Font-Size="Larger" HeaderStyle-ForeColor="Blue" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" ReadOnly="True" />
                            <asp:BoundField DataField="NEGOTIATED AMOUNT" HeaderText="NEGOTIATED AMOUNT" HeaderStyle-BackColor="#8DB4E2" HeaderStyle-BorderColor="Black" HeaderStyle-BorderWidth="1px" HeaderStyle-Font-Size="Larger" HeaderStyle-ForeColor="Blue" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px" />
                        </Columns>
                        <EmptyDataTemplate>
                            NO RECORDS FOUND
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#8DB4E2" HorizontalAlign="Center" />
                        <RowStyle ForeColor="#333333" HorizontalAlign="Center" />
                        <AlternatingRowStyle HorizontalAlign="Center" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnstr %>"
                        SelectCommand="Select distinct jid as [JUSTIFICATION NO],QAmt as [QUOTED AMOUNT],NAmt as [NEGOTIATED AMOUNT],j.OpenBy as [CREATED BY],
                                                 CONVERT(VARCHAR(10), j.OpenDate, 103) + ' '+ CONVERT(VARCHAR(5), j.OpenDate, 108) as [CREATED DATE],j.srno from 
                                                Justification j inner join IncidentsNew1 i on j.vid=i.vid where j.vid like @doc order by srno desc">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="doc" QueryStringField="doc" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>
                </td>
            </tr>

            <tr>
                <td colspan="3" style="display: none">
                    <asp:ModalPopupExtender ID="m" OkControlID="btnOK" TargetControlID="btn_Submit0"
                        PopupControlID="Panel2" runat="server" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="Panel2" runat="server" CssClass="confirm-dialog">
                        <div class="inner">
                            <table>
                                <tr>
                                    <td>
                                        <img src="Images/alert.jpg" style="width: 35px; height: 40px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldate" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblval" runat="server">Please confirm you have entered consumables and conveyance details!</asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div class="base">
                                <asp:Button ID="btnOK" runat="server" CssClass="Button" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btncontinue" runat="server" CssClass="Button" OnClick="btncontinue1_Click"
                                    Text="Proceed" Width="67px" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="m1" TargetControlID="btn_Submit0" PopupControlID="Panel3"
                        runat="server" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="Panel3" runat="server" CssClass="confirm-dialog">
                        <div class="inner">
                            <br />
                            <h2>FLM Visit Done :
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem>YES</asp:ListItem>
                                    <asp:ListItem>NO</asp:ListItem>
                                </asp:DropDownList>
                            </h2>
                            <div class="base">
                                <asp:Button ID="Button1" runat="server" Text="OK" CssClass="Button" OnClick="btncontinue_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
