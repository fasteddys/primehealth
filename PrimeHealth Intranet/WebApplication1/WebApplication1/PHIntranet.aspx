<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PHIntranet.aspx.cs" Inherits="WebApplication1.PHIntranet" %>

<!DOCTYPE HTML>
<!--
	Parallelism by HTML5 UP
	html5up.net | @n33co
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
-->
<html>
<head>
    <title>Prime Health Intranet</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--[if lte IE 8]><script src="assets/js/ie/html5shiv.js"></script><![endif]-->
    <link rel="stylesheet" href="assets/css/main.css" />
    <link rel="shortcut icon" href="/assets/images/1455824597_internet_intranet.ico" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300,400,700' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="assets/css/whhg.css" />
    <link rel="stylesheet" href="assets/css/grid.css">
    <link rel="stylesheet" href="assets/css/styles.css">

    <!-- TODO: uncomment skin styles. 
	     Note, you can use another skin found in the "css" folder, or create your own one -->
    <!-- <link rel="stylesheet" href="css/skin-dark.css"> -->

    <!--[if lt IE 9]>
		<link rel="stylesheet" href="css/ie.css">
	<![endif]-->

    <link rel="apple-touch-icon" href="assets/images/apple-touch-icon.png">
    <link rel="apple-touch-icon" sizes="72x72" href="assets/images/apple-touch-icon-72x72.png">
    <link rel="apple-touch-icon" sizes="114x114" href="assets/images/apple-touch-icon-114x114.png">
</head>
<body>
    <!--  LOGOTYPE LINE  -->
    <!--  TODO: Change domain name and call to action message below -->
    <div id="Head" class="container">
        <div class="row">
            <div class="col span_19">
                <h1 id="Domain">Prime Health For Medical Insurance</h1>
                <br />
                <h1 style="color: white; font-weight: bold">IT Department</h1>
            </div>
            <div id="Action" class="col span_5">
                <a href="#" class="btn btn-icon btn-block"><i class="icon icon-network"></i>My Intranet</a>
            </div>
        </div>
    </div>
    <div id="Content" class="container">
        <hr class="divider">
        <div class="row padding">
            <div class="col span_4">
                <a href="/PHInternet2.aspx" target="_blank">
                    <img src="images/thumbs/Oracle.png" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Medical System</h3>
            </div>
            <div class="col span_4">
                <a href="https://www.google.com.eg/" target="_blank">
                    <img src="images/thumbs/google-logo.png" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Google</h3>
            </div>
            <div class="col span_4">
                <a href="http://www.prime-health.org" target="_blank">
                    <img src="images/thumbs/PrimeHealth.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">PrimeHealth Website</h3>
            </div>
            <div class="col span_4">
                <a href="http://it-system/portal" target="_blank">
                    <img src="images/thumbs/sp.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">IT System</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.25/" target="_blank">
                    <img src="images/thumbs/Card Managment.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Card Cycle Manager</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.28/" target="_blank">
                    <img src="images/thumbs/Archiving.png" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Archiving System</h3>
            </div>
        </div>
        <div class="row padding">
            <div class="col span_4">
                <a href="http://10.1.1.28:5656/" target="_blank">
                    <img src="images/thumbs/TPA.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">TPA System</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.29:5050/" target="_blank">
                    <img src="images/thumbs/Medical Network.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Medical Network Panel</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.29:3636/api/Values/Generate?format=xlsx">
                    <img src="images/thumbs/HR.JPG" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">PrimeHealth Updated Medical Network Excel</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.25:5555/" target="_blank">
                    <img src="images/thumbs/Separating.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">TPA Filtration System</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.29:5050/" target="_blank">
                    <img src="images/thumbs/AdminPanel.png" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">CallCenter System</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.29/CallCenterSystem/Portal/Login.aspx" target="_blank">
                    <img src="images/thumbs/Courier Managment System.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Claims Dispatching System</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.25:9292/" target="_blank">
                    <img src="images/thumbs/PATRON-TICKETING.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Ticketing System</h3>
            </div>
            <div class="col span_4">
                <a href="https://ph.primehealth.local:8445/desktop/container/landing.jsp?locale=en_US" target="_blank">
                    <img src="images/thumbs/CallCenterAgent.jpg" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">CallCenter Agent Application</h3>
            </div>
            <div class="col span_4">
                <a href="https://www.nicedeer.net/Account/Login" target="_blank">
                    <img src="images/login-logo-1.png" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Nice Deer</h3>
            </div>
            <div class="col span_4">
                <a href="http://10.1.1.39:1010/" target="_blank">
                    <img src="images/thumbs/cypress-logo.png" style="width: 125px; height: 125px; line-height: 125px" /></a>
                <h3 style="font-weight: bold">Cypress</h3>
            </div>
        </div>
        <hr class="divider">
        <div id="Footer" class="container">
            <div class="row top">
                <div class="col span_16">Developed By Ahmed Rateb | Copyright &copy; 2017.</div>
                <div class="col span_8 align-right">IT Development Team | <a href="#">Prime Health</a></div>
            </div>
        </div>
    </div>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="assets/js/main.js"></script>
</body>
</html>
