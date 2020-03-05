<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" EnableEventValidation="false" Inherits="CallCenterNotesApp.Portal.Login" %>

<!DOCTYPE html>

<html class="no-js" lang="en">


<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" href="../LoginAssets/img/clipboard.ico" />
    <title>CallCenter APP - Login Page</title>

    <!-- Bootstrap core CSS -->
    <link href="../LoginAssets/css/bootstrap.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../LoginAssets/css/login.css" rel="stylesheet">
    <link href="../LoginAssets/css/animate-custom.css" rel="stylesheet">
    <script src="../LoginAssets/js/custom.modernizr.html" type="text/javascript"></script>

</head>
<body>
    <div class="container" id="login-block">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-sm-offset-3 col-md-offset-4">
                <h3 class="animated bounceInDown">CallCenter System Login</h3>
                <div class="login-box clearfix animated flipInY">
                    <div class="login-logo">
                        <a href="#">
                            <img src="../LoginAssets/img/login-flogo.png" alt="Company Logo" /></a>
                    </div>
                    <hr />
                    <div class="login-form">
                        <div class="login-links">
                            <form runat="server" id="LoginFrm" autocomplete="off">
                                
                                <asp:TextBox runat="server" ID="UserTxt" autocomplete="off" placeholder="User name" required="required" />
                                <asp:TextBox runat="server" ID="PassTxt" autocomplete="off" placeholder="Password" required="required" TextMode="Password" />
                                <asp:Button ID="LoginBtn" runat="server" CssClass="btn btn-login" Text="Login" OnClick="LoginBtn_Click" />
                            </form>
                        </div>
                        <div class="alert alert-danger" runat="server" id="divError">
                            <i class="glyphicon icon-error-alt"></i>
                            <asp:Label runat="server" ID="errormessage">Error in Login Please Check User Name and Password</asp:Label>
                        </div>
                        <footer class="container animated bounceInDown">
                            <p id="footer-text"><small>Copyright &copy; <%: DateTime.Now.Year %> <a href="#">PrimeHealth | Developed By
                                <br />
                            </a>S</small>oftware Development Team</p>
                        </footer>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script src="../LoginAssets/jquery/1.9.1/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="LoginAssets/js/jquery-1.9.1.min.js"><\/script>')</script>
    <script src="../LoginAssets/js/bootstrap.min.js"></script>
    <script src="../LoginAssets/js/placeholder-shim.min.js"></script>
    <script src="../LoginAssets/js/custom.js"></script>
    <script src="../Scripts/GetIPAddress.js"></script>
</body>
<script>
    GetIPAddressFun();
    var IPs;
    setTimeout(function ()
    {
        IPs = IP[0];
        setCookie("IPAddress", IPs, 1);

    }, 500);


    function setCookie(cname, cvalue, exdays)
    {
  var d = new Date();
  d.setTime(d.getTime() + (exdays*24*60*60*1000));
  var expires = "expires="+ d.toUTCString();
  document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
    
</script>
</html>
