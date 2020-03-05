<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>
<!--[if IE 8]> 				 <html class="no-js lt-ie9" lang="en" > <![endif]-->
<!--[if gt IE 8]><!--> <html class="no-js" lang="en" > <!--<![endif]-->

  
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" href="../assets/img/fav_ico.ico" />
    <title>Archiving - Login Page</title>
    <!-- Bootstrap core CSS -->
    <link href="LoginAssets/css/bootstrap.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="LoginAssets/css/login.css" rel="stylesheet">
    <link href="LoginAssets/css/animate-custom.css" rel="stylesheet">


    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="LoginAssets/js/html5shiv.js"></script>
      <script src="LoginAssets/js/respond.min.js"></script>
    <![endif]-->

    <script src="LoginAssets/js/custom.modernizr.html" type="text/javascript"></script>

</head>
    <body>
    <div class="container" id="login-block">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-sm-offset-3 col-md-offset-4">
                <h3 class="animated bounceInDown">Login</h3>
                <div class="login-box clearfix animated flipInY">
                    <div class="login-logo">
                        <a href="#">
                            <img src="LoginAssets/img/login-logo.png" alt="Company Logo" height="95px" width="220px" /></a>
                    </div>
                    <hr />
                    <div class="login-form">
                        <div class="login-links">
                            <form runat="server" id="LoginFrm">
                                <asp:TextBox runat="server" ID="UserTxt" placeholder="User Name" required="required" />
                                <asp:TextBox runat="server" ID="PassTxt" placeholder="Password" required="required" TextMode="Password" />
                                <asp:Button ID="LoginBtn" runat="server" Cssclass="btn btn-login" Text="Login" OnClick="LoginBtn_Click" />
                            </form>
                        </div>
                        <div class="alert alert-danger" runat="server" id="divError">
                            <i class="glyphicon icon-error-alt"></i>Error in Login Please Check User Name Or/And Password
                        </div>
                     <span style="position:relative; left:20px" class="sm-block"><a href="#" class="m-l-10 m-r-10">Developed By Ahmed Rateb</a> | <a href="#" class="m-l-10">IT Development Team</a></span>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="LoginAssets/jquery/1.9.1/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="LoginAssets/js/jquery-1.9.1.min.js"><\/script>')</script>
    <script src="LoginAssets/js/bootstrap.min.js"></script>
    <script src="LoginAssets/js/placeholder-shim.min.js"></script>
    <script src="LoginAssets/js/custom.js"></script>
</body>
</html>