<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainPage1.aspx.cs" Inherits="Mphasis_webapp.CH.MainPage1" EnableEventValidation="false" %>

<%@ Register TagPrefix="obout" Namespace="OboutInc.ImageZoom" Assembly="obout_ImageZoom_NET" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <style type="text/css">
        /*zoom image*/ .viewer {
            width: 29%;
            height: 200px;
            border: 1px solid black;
            position: relative;
        }

        .wrapper {
            overflow: hidden;
        }

        .button {
            font-size: 11px;
            font-weight: bold;
            font-family: Arial;
            color: #ffffff;
            min-width: 54px;
            height: 24px;
            white-space: nowrap;
            cursor: pointer;
            outline: 0 none;
            padding: 0 10px 2px;
            text-align: center;
            border-radius: 2px 2px 2px 2px;
            border: 1px solid #4980C1;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#5384BE', endColorstr='#4386D7'); /* for IE */
            -ms-filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#5384BE', endColorstr='#4386D7'); /* for IE 8 and above */
            background: -webkit-gradient(linear, left top, left bottom, from(#5384BE), to(#4386D7)); /* for webkit browsers */
            background: -moz-linear-gradient(top, #5384BE, #4386D7); /* for firefox 3.6+ */
            background: -o-linear-gradient(top, #5384BE, #4386D7); /* for Opera */
        }

        .modal {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }

        #divImage {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 550px;
            width: 600px;
            padding: 3px;
            border: solid 1px black;
        }

        #divImagechk {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 550px;
            width: 600px;
            padding: 3px;
            border: solid 1px black;
        }

        * html #divImagechk {
            position: absolute;
        }
        /*-------------------------------------*/ /*rotate image*/

        .north {
            transform: rotate(0deg);
            -ms-transform: rotate(0deg); /* IE 9 */
            -webkit-transform: rotate(0deg); /* Safari and Chrome */
        }

        .west {
            transform: rotate(90deg);
            -ms-transform: rotate(90deg); /* IE 9 */
            -webkit-transform: rotate(90deg); /* Safari and Chrome */
        }

        .south {
            transform: rotate(180deg);
            -ms-transform: rotate(180deg); /* IE 9 */
            -webkit-transform: rotate(180deg); /* Safari and Chrome */
        }

        .east {
            transform: rotate(270deg);
            -ms-transform: rotate(270deg); /* IE 9 */
            -webkit-transform: rotate(270deg); /* Safari and Chrome */
        }

        .auto-style2 {
            width: 38%;
        }

        .auto-style3 {
            width: 12%;
            height: 37px;
        }

        .auto-style4 {
            width: 30%;
            height: 37px;
        }

        .button {
        }
    </style>

    <script type="text/javascript">
        function LoadDiv1(url) {

            try {
                var img = new Image();
                var bcgDiv = document.getElementById("divBackground");
                var imgDiv = document.getElementById("divImage");
                var imgFull = document.getElementById("imgFull");
                var imgLoader = document.getElementById("imgLoader");

                imgLoader.style.display = "block";
                img.onload = function () {
                    imgFull.src = img.src;
                    imgFull.style.display = "block";
                    imgLoader.style.display = "none";
                };
                img.src = url;
                var width = document.body.clientWidth;
                if (document.body.clientHeight > document.body.scrollHeight) {
                    bcgDiv.style.height = document.body.clientHeight + "px";
                }
                else {
                    bcgDiv.style.height = document.body.scrollHeight + "px";
                }

                imgDiv.style.left = (width - 650) / 2 + "px";
                imgDiv.style.top = "20px";
                bcgDiv.style.width = "100%";

                bcgDiv.style.display = "block";
                imgDiv.style.display = "block";
                return false;
            }
            catch (Err) {
                alert(Err);
            }
        }

        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");

            imgFull.className = "north";

            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }

            return false;
        }

        function change(btn) {

            try {
                var cls = btn.className;

                if (cls == "north") {
                    btn.className = "west";
                } else if (cls == "west") {
                    btn.className = "south";
                } else if (cls == "south") {
                    btn.className = "east";
                } else if (cls == "east") {
                    btn.className = "north";
                }
            }
            catch (err) {
                alert(err.message);
            }
            return false;
        }

        function ShowModal() {

            $find('pop').show();
        }

        var CurrentPage = 1;
        function GetImageIndex(obj) {
            while (obj.parentNode.tagName != "TD")
                obj = obj.parentNode;
            var td = obj.parentNode;
            var tr = td.parentNode;
            if (td.rowIndex % 2 == 0) {
                return td.cellIndex + tr.rowIndex;
            }
            else {
                return td.cellIndex + (tr.rowIndex * 2);
            }
        }
        function LoadDiv(url, lnk) {
            try {
                debugger;
                var img = new Image();
                var bcgDiv = document.getElementById("divBackground");
                var imgDiv = document.getElementById("divImage");
                var imgFull = document.getElementById("imgFull");
                var imgLoader = document.getElementById("imgLoader");
                var dl = document.getElementById("<%=DataList1.ClientID%>");
                var imgs = dl.getElementsByTagName("img");


                CurrentPage = GetImageIndex(lnk.parentNode) + 1;

                imgLoader.style.display = "block";

                img.onload = function () {
                    imgFull.src = img.src;
                    imgFull.style.display = "block";
                    imgLoader.style.display = "none";
                };
                img.src = url;
                Prepare_Pager(imgs.length);
                var width = document.body.clientWidth;
                if (document.body.clientHeight > document.body.scrollHeight) {
                    bcgDiv.style.height = document.body.clientHeight + "px";
                }
                else {
                    bcgDiv.style.height = document.body.scrollHeight + "px";
                }

                imgDiv.style.left = (width - 650) / 2 + "px";
                imgDiv.style.top = "20px";
                bcgDiv.style.width = "100%";

                bcgDiv.style.display = "block";
                imgDiv.style.display = "block";
                return false;
            }
            catch (Error) { alert(Error); }
        }


        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");

            imgFull.className = "north";

            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }

            return false;
        }
        function change(btn) {

            try {

                var cls = btn.className;

                if (cls == "north") {
                    btn.className = "west";
                } else if (cls == "west") {
                    btn.className = "south";
                } else if (cls == "south") {
                    btn.className = "east";
                } else if (cls == "east") {
                    btn.className = "north";
                }
            }
            catch (err) {
                alert(err.message);
            }
            return false;
        }
        function doPaging(lnk) {
            var dl = document.getElementById("<%=DataList1.ClientID%>");
            var imgs = dl.getElementsByTagName("img");
            var imgLoader = document.getElementById("imgLoader");
            var imgFull = document.getElementById("imgFull");

            var img = new Image();
            if (lnk.id == "Next") {
                if (CurrentPage < imgs.length) {
                    CurrentPage++;
                    imgLoader.style.display = "block";
                    imgFull.style.display = "none";
                    imgFull.className = "north";
                    img.onload = function () {
                        imgFull.src = imgs[CurrentPage - 1].src;
                        imgFull.style.display = "block";
                        imgLoader.style.display = "none";
                    };
                }
            }
            else {
                if (CurrentPage > 1) {
                    CurrentPage--;
                    imgLoader.style.display = "block";
                    imgLoader.style.display = "none";
                    imgFull.className = "north";
                    img.onload = function () {
                        imgFull.src = imgs[CurrentPage - 1].src;
                        imgFull.style.display = "block";
                        imgLoader.style.display = "none";
                    };
                }
            }
            Prepare_Pager(imgs.length);
            img.src = imgs[CurrentPage - 1].src;
        }
        function Prepare_Pager(imgCount) {
            var Previous = document.getElementById("Previous");
            var Next = document.getElementById("Next");
            var lblPrevious = document.getElementById("lblPrevious");
            var lblNext = document.getElementById("lblNext");
            if (CurrentPage < imgCount) {
                lblNext.style.display = "none";
                Next.style.display = "block";
            }
            else {
                lblNext.style.display = "block";
                Next.style.display = "none";
            }
            if (CurrentPage > 1) {
                Previous.style.display = "block";
                lblPrevious.style.display = "none";
            }
            else {
                Previous.style.display = "none";
                lblPrevious.style.display = "block";
            }
        }

    </script>

    <script type="text/javascript">

        function LoadDivchk(url) {

            var img = new Image();
            var bcgDiv = document.getElementById("divBackgroundchk");
            var imgDiv = document.getElementById("divImagechk");
            var imgFull = document.getElementById("Image2");
            var imgLoader = document.getElementById("img1");

            imgLoader.style.display = "block";
            img.onload = function () {
                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgLoader.style.display = "none";
            };
            img.src = url;
            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }

            imgDiv.style.left = (width - 650) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "100%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }

        function HideDivchk() {
            var bcgDiv = document.getElementById("divBackgroundchk");
            var imgDiv = document.getElementById("divImagechk");
            var imgFull = document.getElementById("Image2");

            imgFull.className = "north";

            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }

            return false;
        }

        function changechk(btn) {
            debugger;
            try {
                var cls = btn.className;

                if (cls == "north") {
                    btn.className = "west";
                } else if (cls == "west") {
                    btn.className = "south";
                } else if (cls == "south") {
                    btn.className = "east";
                } else if (cls == "east") {
                    btn.className = "north";
                }
            }
            catch (err) {
                alert(err.message);
            }
            return false;
        }
    </script>

    <style>
        .north1 {
            transform: rotate(0deg);
            -ms-transform: rotate(0deg); /* IE 9 */
            -webkit-transform: rotate(0deg); /* Safari and Chrome */
        }

        .west1 {
            transform: rotate(90deg);
            -ms-transform: rotate(90deg); /* IE 9 */
            -webkit-transform: rotate(90deg); /* Safari and Chrome */
        }

        .south1 {
            transform: rotate(180deg);
            -ms-transform: rotate(180deg); /* IE 9 */
            -webkit-transform: rotate(180deg); /* Safari and Chrome */
        }

        .east1 {
            transform: rotate(270deg);
            -ms-transform: rotate(270deg); /* IE 9 */
            -webkit-transform: rotate(270deg); /* Safari and Chrome */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div style="color: #003366; font-size: x-large; font-weight: bolder;">
        <strong>Audit Report</strong>
    </div>
    <div style="font-size: large; width: 100%; padding-top: 1em;">
        <fieldset>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="auto-style3">ATMID
                    </td>
                    <td class="auto-style4">
                        <asp:Label ID="lbl_atmID" runat="server" Style="color: #000099; font-weight: 700"></asp:Label>
                    </td>
                    <td class="auto-style3">BANK
                    </td>
                    <td class="auto-style4">
                        <asp:Label ID="lbl_bankid" runat="server" Style="color: #000099; font-weight: 700"></asp:Label>
                    </td>
                    <td rowspan="6" align="left" valign="top">
                        <%-- <asp:Image ID="Image1" runat="server"
                    ImageUrl="http://transovative.com/uploader/TransientStorage/Sign_Sumit-Aug_13_2012_12_48_31_PM.jpg"
                    AlternateText="Image not Available" Height="100px" Width="100px" />--%>
                        <asp:ImageButton ID="ImageButton23" runat="server" Width="100px" Height="100px" Style="cursor: pointer"
                            OnClientClick="return LoadDiv1(this.src);" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">ADDRESS
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_add" runat="server" Style="color: #000099; font-weight: 700"></asp:Label>
                    </td>
                    <td class="auto-style3">CITY
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lbl_city" runat="server" Style="color: #000099; font-weight: 700"></asp:Label>
                    </td>
                    <asp:Label runat="server" ID="lblimg"></asp:Label>
    </div>
    </td> </tr>
    <tr>
        <td class="auto-style3">PINCODE
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_pin" runat="server" Style="color: #000099; font-weight: 700"></asp:Label>
        </td>
        <td class="auto-style3">STATE
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_state" runat="server" Style="color: #000099; font-weight: 700"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">REGION
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_reg" runat="server" Style="color: #000099; font-weight: 700"></asp:Label>
        </td>
        <td class="auto-style3" visible="false">
            <%--ZONE--%>
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_zone" runat="server" Style="color: #000099; font-weight: 700"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">VISIT ID
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_vid" runat="server" Style="color: #000099; font-weight: 700;"></asp:Label>
        </td>
        <td class="auto-style3">VISIT DATE
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_vdate" runat="server" Style="color: #000099; font-weight: 700;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">VISITED BY
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_visitedby" runat="server" Style="color: #000099; font-weight: 700;"></asp:Label>
        </td>
        <td class="auto-style3">VISIT TIME
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_vtime" runat="server" Style="color: #000099; font-weight: 700;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">VISIT TYPE
        </td>
        <td class="auto-style2">
            <asp:Label ID="lbl_typ" runat="server" Style="color: #000099; font-weight: 700;"></asp:Label>
        </td>
        <td class="auto-style3">VISIT REMARK
        </td>
        <td>
            <asp:Label ID="lbl_rmk" runat="server" Style="color: #000099; font-weight: 700;"></asp:Label>
        </td>
    </tr>
    </table>
    <%--            <div id="divBackground" class="modal">
        </div>--%>
    <%-- <div id="divImage1" class="info">
            <table style="height: 100%; width: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="imgFull1" class="north" runat="server" alt="" src="" Style="display: none; height: 500px; width: 500px"
                            ClientIDMode="Static" />
                        <asp:Image ID="imgLoader1" runat="server" alt="" src="images/loader.gif" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="bottom">
                        <asp:Button runat="server" ID="button1" Text="Rotate" OnClientClick="return change(imgFull1);"
                            ClientIDMode="Static" CssClass="button" />
                        <asp:Button runat="server" ID="btnClose1" Text="Close" OnClientClick="return HideDiv();"
                            ClientIDMode="Static" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>--%>
    <%--            <div>
                <br /><hr /><br />
            </div>--%>
    <%--   <div style="width: 1100px; overflow-x: scroll">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton2" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton3" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton4" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton5" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton6" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton7" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton8" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton9" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton10" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton11" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton12" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton13" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton14" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton15" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton16" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton17" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton18" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton19" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton20" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton21" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton22" runat="server" Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>--%>
    <div>
        <asp:DataList ID="DataList1" runat="server" RepeatColumns="10" RepeatLayout="Table"
            Width="500px">
            <ItemTemplate>
                <br />
                <table cellpadding="5px" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("FilePath")%>' Width="100px"
                                Height="100px" onclick="LoadDiv(this.src, this)" Style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
                <br />
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div id="divBackground" class="modal">
    </div>
    <div id="divImage">
        <table style="height: 100%; width: 100%">
            <tr>
                <td valign="middle" align="center" colspan="3" style="height: 300px;">
                    <img id="imgLoader" clientidmode="Static" runat="server" alt="" src="Image/loader.gif" />
                    <asp:Image ID="imgFull" class="north" runat="server" alt="" src="" Style="display: none; height: 500px; width: 500px"
                        ClientIDMode="Static" />
                </td>
            </tr>
            <tr>
                <td align="left" style="padding: 10px; width: 200px">
                    <a id="Previous" href="javascript:" onclick="doPaging(this)" style="display: none">Previous</a>
                    <span id="lblPrevious">Previous</span>
                </td>
                <td style="text-align: center">
                    <asp:Button runat="server" ID="button" Text="Rotate" OnClientClick="return change(imgFull);"
                        ClientIDMode="Static" CssClass="button" />
                    <asp:Button runat="server" ID="btnClose" Text="Close" OnClientClick="return HideDiv()"
                        ClientIDMode="Static" CssClass="button" />
                </td>
                <td align="right" style="padding: 10px; width: 200px">
                    <a id="Next" href="javascript:" onclick="doPaging(this)">Next</a> <span id="lblNext"
                        style="display: none">Next</span>
                </td>
            </tr>
        </table>
    </div>
    <div id="divBackgroundchk" class="modal">
    </div>
    <div id="divImagechk">
        <table style="height: 100%; width: 100%">
            <tr>
                <td valign="middle" align="center" colspan="3" style="height: 300px;">
                    <img id="img1" clientidmode="Static" runat="server" alt="" src="Image/loader.gif" />
                    <asp:Image ID="Image2" class="north" runat="server" alt="" src="" Style="display: none; height: 500px; width: 500px"
                        ClientIDMode="Static" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button runat="server" ID="button1" Text="Rotate" OnClientClick="return changechk(Image2);"
                        ClientIDMode="Static" CssClass="button" />
                    <asp:Button runat="server" ID="Button2" Text="Close" OnClientClick="return HideDivchk()"
                        ClientIDMode="Static" CssClass="button" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <br />
        <hr />
        <br />
    </div>
    <table width="100%" style="vertical-align: top" border="1">
        <tr>
            <td colspan="4" align="center" style="font-weight: bold">QUESTIONS
            </td>
            <%--  <td colspan="2">
                &nbsp;
            </td>--%>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ1" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q2" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none;">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ2" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q3" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none;">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ3" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q4" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none;">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ4" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q5" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ5" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr style="display: none">
            <td>
                <asp:Label ID="lbl_q6" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ6" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q7" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ7" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q8" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ8" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q9" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ9" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q10" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none;">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ10" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q42" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label42" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ42" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl11" runat="server">
            <td>
                <asp:Label ID="lbl_q11" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none;">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ11" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl59" runat="server">
            <td>
                <asp:Label ID="lbl_q59" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label59" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton1" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>

        <td>
            <asp:Label ID="lbl_q12" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label12" runat="server"></asp:Label>
        </td>
        <td style="text-align: center">
            <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ12" runat="server"
                Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
        </td>
        </tr>
        <tr style="display: none">
            <td>
                <asp:Label ID="lbl_q13" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label13" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ13" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q14" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label14" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ14" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q15" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label15" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none;">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ15" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q16" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label16" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ16" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q17" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label17" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ17" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q18" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label18" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ18" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q19" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label19" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ19" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q20" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ20" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q21" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label21" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ21" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q22" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label22" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ22" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q23" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label23" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none;">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ23" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q24" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label24" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ24" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q25" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label25" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ25" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q26" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label26" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none;">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ26" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q27" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label27" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ27" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q28" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label28" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ28" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q29" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label29" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ29" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q30" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label30" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ30" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q31" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label31" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ31" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q32" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label32" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ32" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q33" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label33" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ33" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q34" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label34" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ34" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q35" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label35" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ35" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl36" runat="server">
            <td>
                <asp:Label ID="lbl_q36" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label36" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ36" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q37" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label37" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ37" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q38" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label38" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ38" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q39" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label39" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ39" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_q40" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label40" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ40" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr style="display: none">
            <td>
                <asp:Label ID="lbl_q41" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label41" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ41" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>

        <tr id="lblHK" runat="server">
            <td>
                <asp:Label ID="lbl_q61" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label61" runat="server"></asp:Label>
            </td>
        </tr>

        <tr id="lblDClean" runat="server">
            <td>
                <asp:Label ID="lbl_q62" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label62" runat="server"></asp:Label>
            </td>

        </tr>
        <tr id="lbl43" runat="server">
            <td>
                <asp:Label ID="lbl_q43" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label43" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ43" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl44" runat="server">
            <td>
                <asp:Label ID="lbl_q44" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label44" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ44" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl45" runat="server">
            <td>
                <asp:Label ID="lbl_q45" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label45" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ45" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl46" runat="server">
            <td>
                <asp:Label ID="lbl_q46" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label46" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ46" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl47" runat="server">
            <td>
                <asp:Label ID="lbl_q47" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label47" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ47" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl48" runat="server">
            <td>
                <asp:Label ID="lbl_q48" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label48" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ48" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl49" runat="server">
            <td>
                <asp:Label ID="lbl_q49" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label49" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ49" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl50" runat="server">
            <td>
                <asp:Label ID="lbl_q50" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label50" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ50" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl51" runat="server">
            <td>
                <asp:Label ID="lbl_q51" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label51" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ51" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl52" runat="server">
            <td>
                <asp:Label ID="lbl_q52" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label52" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ52" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl53" runat="server">
            <td>
                <asp:Label ID="lbl_q53" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label53" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ53" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl54" runat="server">
            <td>
                <asp:Label ID="lbl_q54" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label54" runat="server"></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ54" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl55" runat="server">
            <td>
                <asp:Label ID="lbl_q55" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label55" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ55" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl56" runat="server">
            <td>
                <asp:Label ID="lbl_q56" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label56" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ56" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl57" runat="server">
            <td>
                <asp:Label ID="lbl_q57" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label57" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ57" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl58" runat="server">
            <td>
                <asp:Label ID="lbl_q58" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label58" runat="server"></asp:Label>
            </td>
            <td >
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ58" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="tr_qu59" runat="server">
            <td>
                <asp:Label ID="lbl_qu59" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_an59" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton3" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="tr_qu60" runat="server">
            <td>
                <asp:Label ID="lbl_qu60" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_an60" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton4" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="tr_qu61" runat="server">
            <td>
                <asp:Label ID="lbl_qu61" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_an61" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton5" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="tr_qu62" runat="server">
            <td>
                <asp:Label ID="lbl_qu62" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbl_an62" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton6" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>

          <tr id="lbl60" runat="server">
            <td>
                <asp:Label ID="lbl_q60" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label60" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton2" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>

        <tr id="lbl63" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q63" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label63" runat="server"></asp:Label>
            </td>
            <td >
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ63" runat="server" 
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl64" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q64" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label64" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton8" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl65" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q65" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label65" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton9" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
       <tr id="lbl66" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q66" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label66" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton7" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        
         <tr id="lbl67" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q67" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label67" runat="server"></asp:Label>
            </td>
            <td>
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ67" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl68" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q68" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label68" runat="server"></asp:Label>
            </td>
            <td>
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="imgQ68" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
        <tr id="lbl69" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q69" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label69" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton12" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
          <tr id="lbl70" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q70" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label70" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton13" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
          <tr id="lbl71" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q71" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label71" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton71" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
          <tr id="lbl72" runat="server" visible="false">
            <td>
                <asp:Label ID="lbl_q72" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label72" runat="server"></asp:Label>
            </td>
            <td style="text-align: center; display: none">
                <asp:ImageButton ImageAlign="AbsMiddle" AlternateText="No Image" ID="ImageButton72" runat="server"
                    Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDivchk(this.src);" />
            </td>
        </tr>
       
              
    </table>
    </fieldset> </div>
    <div style="color: #003366; font-size: x-large; font-weight: bolder;">
        <strong>Asset Details</strong>
    </div>
    <div style="font-size: large; width: 1050px; padding-top: 1em;">
        <fieldset>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="10" CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="909px" Style="margin-top: 0px" AutoGenerateColumns="true"
                            BorderWidth="2px" BorderColor="#003366">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
