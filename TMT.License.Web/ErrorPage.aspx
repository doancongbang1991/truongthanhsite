<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="TMT.License.Web.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Page Not Found</title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            background: #efefef;
            font-family: Georgia, Times, Verdana, Geneva, Arial, Helvetica, sans-serif;
        }
        div#mother
        {
            margin: 0 auto;
            width: 943px;
            height: 572px;
            position: relative;
        }
        div#errorBox
        {
            background: url('../TMT.License.Web/images/bg-404.png') no-repeat top left;
            width: 943px;
            height: 572px;
            margin: auto;
        }
        div#errorText
        {
            color: #39351e;
            padding: 120px 0 0 360px;
        }
        div#errorText p
        {
            width: 526px;
            font-size: 11px;
            font-weight:500;
            line-height: 24px;
            height: 30px;
        }
        
        div.link
        {
            /*background:#f90;*/
            height: 50px;
            width: 145px;
            float: left;
        }
        div#home
        {
            margin: 20px 0 0 444px;
        }
        div#contact
        {
            margin: 20px 0 0 25px;
        }
        h1
        {
            font-size: 40px;
            margin-bottom: 20px;
        }
    </style>
    <link href="styles/style.css" rel="stylesheet" type="text/css" />
    <script src="../TMT.License.Web/scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var bodyHeight = $(window).height();
            var contentHeight = $("#mother").height();
            var newHeight;

            if (bodyHeight < contentHeight) {
                var newHeight = contentHeight - bodyHeight;

            } else {
                var newHeight = bodyHeight - contentHeight;

            }
            newHeight = newHeight / 2;

            $("body").css({ marginTop: newHeight });

        });
    </script>
</head>
<body>
    <form ID="form1" runat="server">
    <div id="mother">
        <div id="errorBox">
            <div id="errorText">
                <h1>
                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h1>
                <p style="height: 200px;">
                    <asp:Label ID="lblMessenger" runat="server" Text=""></asp:Label>
                </p>
                <asp:Button ID="btLogin" CssClass="button" runat="server" Text="Login with other account"
                    OnClick="btLogin_Click" Width="165px" />&nbsp;&nbsp;
                <asp:Button ID="btHome" CssClass="button" runat="server" Text="Home Page" OnClick="btHome_Click"
                    Width="120px" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>