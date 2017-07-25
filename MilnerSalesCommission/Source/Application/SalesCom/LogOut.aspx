<%@ Page Language="C#" AutoEventWireup="true" Inherits="LogOut" Codebehind="LogOut.aspx.cs" %>
<!DOCTYPE html>

<html >
<head runat="server">
    <title> Log out</title>
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
</head>
<body>
    <form id="form1" runat="server">

<div>
    <header>  
        <nav class="navbar">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar"> <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </button>
                    <a class="navbar-brand hidden-xs" href="#"><img src="Content/images/logo.jpg" class="img-responsive" alt="Milner Sales Commission Logo"></a> <a class="navbar-brand hidden-lg hidden-md hidden-sm" href="#"><img src="Content/images/mob-logo.jpg" class="img-responsive" alt="Milner Sales Commission Logo"></a>
                </div>
                
                <div class="top-menu">
                    <!-- Welcome message based on user id-->
                    <ul class="nav pull-right top-menu">
                         <li class="pull-left"><a class="btn logout" onclick="logout()" href="/SalesWeb">Login</a></li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    
       <div id="content-wrapper">
           <div class="container">
                <div class="text-center">
                    <asp:Label 
                        ID="LabelLogout" 
                        runat="server" 
                        Text="You have signed out. Click login to sign back into sales commission."
                        SkinID="label_general" />
                 </div>
           </div>

       </div>
       
    <footer class="footer">
      <div class="container">
        <p class="text-muted">© 2017, Milner Sales Commission</p>
      </div>
</footer>

</div>
    </form>
</body>
</html>
<script>
    function logout() {
        try {
            document.execCommand("ClearAuthenticationCache");
        }
        catch (e) {

        }
    }
   
</script>
