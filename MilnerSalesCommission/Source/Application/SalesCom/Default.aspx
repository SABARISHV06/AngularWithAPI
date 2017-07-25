<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SalesCommission.SalesComIndex" %>

<!DOCTYPE html>

<html data-ng-app="mtisalescom">

<head data-ng-controller="HeaderController">
    <title>{{Header.GetTitle()}}</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
   <!-- <link rel="shortcut icon" href="shortcut.ico"> -->
   <link rel="apple-touch-icon" sizes="57x57" href="Content/images/favicon/apple-icon-57x57.png">
   <link rel="apple-touch-icon" sizes="60x60" href="Content/images/favicon/apple-icon-60x60.png">
   <link rel="apple-touch-icon" sizes="72x72" href="Content/images/favicon/apple-icon-72x72.png">
   <link rel="apple-touch-icon" sizes="76x76" href="Content/images/favicon/apple-icon-76x76.png">
   <link rel="apple-touch-icon" sizes="114x114" href="Content/images/favicon/apple-icon-114x114.png">
   <link rel="apple-touch-icon" sizes="120x120" href="Content/images/favicon/apple-icon-120x120.png">
   <link rel="apple-touch-icon" sizes="144x144" href="Content/images/favicon/apple-icon-144x144.png">
   <link rel="apple-touch-icon" sizes="152x152" href="Content/images/favicon/apple-icon-152x152.png">
   <link rel="apple-touch-icon" sizes="180x180" href="Content/images/favicon/apple-icon-180x180.png">
   <link rel="icon" type="image/png" sizes="192x192"  href="Content/images/favicon/android-icon-192x192.png">
   <link rel="icon" type="image/png" sizes="32x32" href="Content/images/favicon/favicon-32x32.png">
   <link rel="icon" type="image/png" sizes="96x96" href="Content/images/favicon/favicon-96x96.png">
   <link rel="icon" type="image/png" sizes="16x16" href="Content/images/favicon/favicon-16x16.png">
   <meta name="msapplication-TileColor" content="#ffffff">
   <meta name="msapplication-TileImage" content="Content/images/favicon/ms-icon-144x144.png">
   <meta name="theme-color" content="#ffffff">

    <%: Styles.Render("~/Css")%>
    <%: Scripts.Render("~/Js")%>
    <%: Scripts.Render("~/appviews")%>
<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
<!--[if lt IE 9]>
			  <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
			  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
			<![endif]-->
</head>

<body>

                 <div data-ng-view=""></div>
     
<footer class="footer">
      <div class="container">
        <p class="text-muted">© 2017, Milner Sales Commission</p>
      </div>
</footer>


    <script>

        function GetSession() {
            var sessionid = "<%=Session.SessionID%>";
        }

        function GetAppVersion() {
            return '<%= Session["ReleaseValue"] %>';
        }

        var url_prefix = "api/";
        var appname = "../";
        var uuid = "sales_" + "<%=Session.SessionID%>";
        var appendguid = "?s=" + uuid;

        var preurl = function (url) {
            return url_prefix + url + appendguid;
        }

        var posturl = function (url) {
        }

        var imgurl = function (url) {
            return appname + url;
        }

        var ap2 = location.href.toLowerCase();

        //Development server will have exactly one '/' after http://localhost
        var isDevelopmentServer = !(ap2.toLowerCase().indexOf('salescom') > 0);
        if (!isDevelopmentServer) {
            url_prefix = "../sct/";
            //url_prefix = "../";
        }

    </script>
</body>
</html>


