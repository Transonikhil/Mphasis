<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GPSOffReport.aspx.cs" Inherits="Mphasis_webapp.GPSOffReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <style type="text/css">
        .style1 {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function windowOnLoad() {
            document.getElementById('txt_frmDate').readOnly = true;
            document.getElementById('txt_toDate').readOnly = true;
        }
        window.onload = windowOnLoad;
    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

    <script type="text/javascript">

        <%--$(document).ready(function () {
            PopulateLocation();
        });

        function PopulateLocation() {
            <%--// var shift = $("#<%=dd_client.ClientID %>").val();
            var category = $("#<%=dd_client.ClientID %>").val();
            // shift = JSON.stringify(shift);
            //category = JSON.stringify(category);
            try {
                // $("#<%=dd_officer.ClientID %>").empty().append('<option selected="selected" value="Select">Loading...</option>');

                $.ajax({
                    type: "POST",
                    url: 'UserwiseReport.aspx/PopulateLocation',
                    //data: '{shift: ' + shift + ',category: ' + category + '}',
                    data: '{category: ' + category + '}',

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

        }--%>

        function OnPopulated(response) {
            $("#<%=dd_officer.ClientID %>").attr('disabled', false);
        PopulateControl(response.d, $("#<%=dd_officer.ClientID %>"));
    }

    </script>

    <script type="text/javascript">
        function PopulateControl(list, control) {
            debugger;
            control.empty();
            if (list.length > 0) {
                control.removeAttr("disabled");
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
            var users = $('#<%=hdnlocation.ClientID%>').val();
            if (users != "") {
                $('#<%=dd_officer.ClientID %>').val(users);
                $('#<%=hdfUsers.ClientID%>').val('');
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div style="color: #003366; font-family: Cambria, Cochin, Georgia, Times; font-variant: small-caps; font-size: xx-large; font-weight: bolder;">
       MANUAL INTERNET AND GPS OFF REPORT
    </div>
    <br />
    <asp:HiddenField ID="hdnlocation" runat="server" />
    <asp:HiddenField ID="hdfUsers" runat="server" />

    <table width="90%">
        <tr>
            <%--<td>Role :
            </td>
            <td>
                <asp:DropDownList ID="dd_role" Width="200px" runat="server" 
                     AutoPostBack="true"  OnSelectedIndexChanged="dd_client_SelectedIndexChanged">
                <asp:ListItem Text="ALL" Value="%"></asp:ListItem>
                </asp:DropDownList>
            </td>--%>
            <td>Officer :
            </td>
            <td>
                <asp:DropDownList ID="dd_officer" Width="200px" CssClass="textb" runat="server">
                    <%--DataSourceID="SqlDataSource4"
                                    DataTextField="txt" DataValueField="val">--%>
                </asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="sql_client" />
            </td>

        </tr>
        <tr>
            <td>From Date:
            </td>
            <td>
                <asp:TextBox ID="txt_frmDate" Height="20px" Width="200px" runat="server" ClientIDMode="static"></asp:TextBox>
                <ajaxToolkit:RoundedCornersExtender ID="txt_frmdate_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_frmdate">
                </ajaxToolkit:RoundedCornersExtender>
            </td>
            <td>To Date:
            </td>
            <td>
                <asp:TextBox ID="txt_toDate" Height="20px" Width="200px" runat="server" ClientIDMode="static"></asp:TextBox>
                <ajaxToolkit:RoundedCornersExtender ID="txt_todate_RoundedCornersExtender" runat="server" Enabled="True" Radius="10" TargetControlID="txt_todate">
                </ajaxToolkit:RoundedCornersExtender>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td style="padding-top: 0.5em">
                <asp:Button ID="btn_search" Height="30px" Width="100px" Font-Size="18px" Font-Names="Cambria, Cochin, Georgia, Times;" runat="server" BorderWidth="0px" BackColor="#003366" ForeColor="White" Text="Search" OnClick="btn_search_Click" />
                <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" 
                            ImageUrl="~/Image/EXCEL.ICO" Width="20px" OnClick="ImageButton1_Click" />
                	 </td>
        </tr>

    </table>


    <div align="center">
        <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_frmDate" />
        <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_toDate" />



    </div>
      <div align="center">

                <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                    Text="No records found pertaining to your search. Please select other search criteria."
                    Visible="False" Font-Size="Large"></asp:Label>
                <br />
                <div id="imgexcel" runat="server" style="display: none;">
                    <asp:Label ID="Label3" runat="server" ForeColor="Green" Visible="False" Font-Size="Large"></asp:Label>
                    &nbsp;&nbsp;
                <asp:ImageButton runat="server" Text="Export To Excel" Width="45" Height="45" Visible="false" ImageUrl="Image/EXCEL.ICO" OnClick="exel_Click"></asp:ImageButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </div>
            <br />
    <asp:UpdatePanel runat="server" ID="up" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
          

            </div>
   <div align="center">
       <asp:GridView ID="GridView1" AutoGenerateColumns="true" AllowPaging="true" runat="server" PageSize="12"
           CellPadding="4" Font-Size="10pt"
           ForeColor="#333333" GridLines="Vertical" AllowSorting="false" Width="909px"
           Style="margin-top: 0px" OnPageIndexChanging="GridView1_PageIndexChanging">
           <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
           <EditRowStyle BackColor="#999999" />
           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
           <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
           <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center"
               VerticalAlign="Middle" />
           <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
           <SortedAscendingCellStyle BackColor="#E9E7E2" />
           <SortedAscendingHeaderStyle BackColor="#506C8C" />
           <SortedDescendingCellStyle BackColor="#FFFDF8" />
           <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
           <Columns>
               <%--<asp:HyperLinkField DataTextField="" DataNavigateUrlFields="VID" 
                            DataNavigateUrlFormatString="ReportOperation.aspx?auditid={0}&dnld_excel=Y" Text="Excel" 
                            HeaderText="Report" />--%>

               <%-- <asp:HyperLinkField DataTextField="VID" DataNavigateUrlFields="VID" 
                            DataNavigateUrlFormatString="MainPage.aspx?auditid={0}" Text="Visit ID" 
                            HeaderText="Visit ID" />--%>

               <%--<asp:HyperLinkField DataTextField="" DataNavigateUrlFields="VID" 
                            DataNavigateUrlFormatString="photos.aspx?branch=true&&auditid={0}" Text="Photos" target="_blank"
                            HeaderText="View Photos" />--%>
           </Columns>
       </asp:GridView>
   </div>
            <hr />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
