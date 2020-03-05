<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Complaints.aspx.cs" Inherits="WS.Complaints" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--[if lt IE 7]>      <html lang="en" class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
    <!--[if IE 7]>         <html lang="en" class="no-js lt-ie9 lt-ie8"> <![endif]-->
    <!--[if IE 8]>         <html lang="en" class="no-js lt-ie9"> <![endif]-->
    <!--[if gt IE 8]><!-->
    <html lang="en" class="no-js">
    <!--<![endif]-->
    <!-- meta character set -->
    <meta charset="utf-8">
    <!-- Always force latest IE rendering engine or request Chrome Frame -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>Prime Health For Medical Insurance S.A.E</title>
    <!-- Meta Description -->
    <meta name="description">
    <meta name="keywords">
   
    <!-- Mobile Specific Meta -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- CSS
		================================================== -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,700' rel='stylesheet'
        type='text/css' />
    <!-- Fontawesome Icon font -->
    <link rel="stylesheet" href="css/font-awesome.min.css" />
     <link href="http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
    <!-- bootstrap.min -->
    <link rel="stylesheet" href="css/jquery.fancybox.css" />
    <!-- bootstrap.min -->
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <!-- bootstrap.min -->
    <link rel="stylesheet" href="css/owl.carousel.css" />
    <!-- bootstrap.min -->
    <link rel="stylesheet" href="css/slit-slider.css" />
    <!-- bootstrap.min -->
    <link rel="stylesheet" href="css/animate.css" />
    <!-- Main Stylesheet -->
    <link rel="stylesheet" href="css/main.css" />
    <!-- Modernizer Script for old Browsers -->
    <script src="js/modernizr-2.6.2.min.js"></script>
</head>
<body id="body">
    <form id="form1" runat="server">
    <!--
        Fixed Navigation
        ==================================== -->
    <header id="navigation" class="navbar-inverse navbar-fixed-top animated-header">
            <div class="container">
                <div class="navbar-header">
                    <!-- responsive nav button -->
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
						<span class="sr-only">Toggle navigation</span>
						<span class="icon-bar"></span>
						<span class="icon-bar"></span>
						<span class="icon-bar"></span>
                    </button>
					<!-- /responsive nav button -->
					
					<!-- logo -->
         	<a href="Index.aspx#body"><img src="img/prime.png" alt="companylogo" width="156px" height="46px"/></a>

					<!-- /logo -->
                </div>

				<!-- main nav -->
                <nav class="collapse navbar-collapse navbar-right" role="navigation">
                    <ul id="nav" class="nav navbar-nav">
                        <li><a href="Index.aspx#body">Home</a></li>
                        <li><a href="Index.aspx#service">Service</a></li>
                        <li><a href="Index.aspx#portfolio">Profile</a></li>
                        <li><a href="Index.aspx#products">Products</a></li>
                        <li><a href="Index.aspx#Request">Offer Request</a></li>
                         <li><a href="Index.aspx#careers">Careers</a></li>
                        <li><a href="Index.aspx#clients">Our Corporate Clients</a></li>
                        <li><a href="Index.aspx#contact">Contact Us</a></li>
                        <li><a href="Complaints.aspx">Complaints</a></li>
                    </ul>
                </nav>
				<!-- /main nav -->
				
            </div>
        </header>
    <!--
        End Fixed Navigation
        ==================================== -->
    <main class="site-content" role="main">
        <section id="complaints" class="parallax">
            <br />
             <div class="sec-title text-center wow animated fadeInDown">
            <h2>Complaint Form</h2>
							</div>
				<div class="overlay">
					<div class="container">
                        <div id="welcomediv" runat="server">
                       <h3>Welcome Guest, Please Login First To Submit Complaint</h3>
                        </div>
                         <div id="clientdiv" runat="server">
                       <h3  id="clientname" runat="server"></h3>
                        </div>
                        <br />
					<div class="col-md-7 wow animated fadeInLeft" id="complaindiv" runat="server">
                        
            <form action="#" method="post">
	              <div class="container">
    <div class="row">
        <div class="col-xs-6">
           <div class="input-field">
           <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Your Name..." runat="server"></asp:TextBox>
		  </div>
            </div>
        <div class="col-xs-6">
         <asp:RequiredFieldValidator  runat="server" ControlToValidate="txtName" CssClass="form-control" ErrorMessage="Please Enter Your Name" BorderStyle="None" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        </div>
</div>
                  <div class="container">
    <div class="row">
        <div class="col-xs-4">
           	<div class="input-field">
									 <asp:TextBox ID="txtemail" CssClass="form-control" placeholder="Your Email..." runat="server"></asp:TextBox>
								</div>
            </div>
        <div class="col-xs-4">
            						                         <asp:RequiredFieldValidator  runat="server" ControlToValidate="txtemail" CssClass="form-control" ErrorMessage="Please Enter your E-Mail" BorderStyle="None" ForeColor="Red"></asp:RequiredFieldValidator>


        </div>
      <div class="col-xs-4">
            			                         <asp:RegularExpressionValidator  runat="server" ControlToValidate="txtemail" CssClass="form-control" ErrorMessage="Please Enter valid a E-Mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" BorderStyle="None" ForeColor="Red"></asp:RegularExpressionValidator>


        </div>
            </div>
    </div>
                  <div class="container">
    <div class="row">
        <div class="col-xs-6">
           	<div class="input-field">
									 <asp:TextBox ID="txtsubject" CssClass="form-control" placeholder="Your Subject..." runat="server"></asp:TextBox>
								</div>
           </div>
        <div class="col-xs-6">
            <asp:RequiredFieldValidator  runat="server" ControlToValidate="txtsubject" CssClass="form-control" ErrorMessage="Please Enter your Subject" BorderStyle="None" ForeColor="Red"></asp:RequiredFieldValidator>

        </div>
</div>
</div>                                     
                  <div class="container">
    <div class="row">
        <div class="col-xs-6">
           	 <div class="input-field">
                              <asp:DropDownList ID="drp1" runat="server" AppendDataBoundItems="true">
                                  <asp:ListItem Value="0">Please Select Department...</asp:ListItem>
                              </asp:DropDownList>
								</div>
            </div>
        <div class="col-xs-6">
                                <asp:RequiredFieldValidator  runat="server" ControlToValidate="drp1" ErrorMessage="Please select a valid Department" InitialValue="0" CssClas="form-control" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>

        </div>

    </div>
</div>
                  <div class="container">
    <div class="row">
        <div class="col-xs-6">
           	 <div class="input-field">
                                 <asp:TextBox ID="txtmessage" CssClass="form-control" placeholder="your Complaint..." runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
								</div>
            </div>
        <div class="col-xs-6">
                                                    <asp:RequiredFieldValidator  runat="server" ControlToValidate="txtmessage" CssClass="form-control" ErrorMessage="Please Enter your complaint" BorderStyle="None" ForeColor="Red"></asp:RequiredFieldValidator>


        </div>
</div>
</div>
                  <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-blue btn-effect" Text="Submit" OnClick="Submit_ServerClick"></asp:Button>

							</form>      
                        
                              
                        
                    </div>

                            <div id="successaddition" runat="server">
                            Dear <strong> <span id="usernamespan" runat="server"></span> </strong> , Thanks For Contacting us. <strong> your ticket number is <span id="ticketidspan" runat="server"></span></strong>. We will contact you shortly.
                                <br />
                                <br />
                            <asp:Button ID="backtocomplaints" runat="server" CssClass="btn btn-blue btn-effect" Text="Back" OnClick="Back_ServerClick"></asp:Button>
                        </div>
                            <div class="animated fadeInLeft" id="Myticketsdiv" runat="server" style="position:absolute; left:72%">
            
                                    <a href="Portal/Dashboard.aspx" target="_blank">Click Here To View your Tickets</a>	       
						
</div>

          <div class="col-md-5 wow animated fadeInRight" id="loginform" runat="server">

              
                          <div class="form-horizontal">
                    <fieldset>
                        <legend>Login Form <i class="fa fa-sign-in pull-right"></i></legend>
                        <br />
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="ID" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="TextMail" runat="server" placeholder="User Name" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Password" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="TextPassword" runat="server" placeholder="Password" CssClass="form-control"
                                    TextMode="Password"></asp:TextBox>
                                <br />
                                <div>
                             <asp:Button ID="LoginBtn" runat="server" Cssclass="btn btn-login" Text="Login" OnClick="Login_ServerClick"  />

                                </div>
                            </div>
                        </div>
                        </fieldset>

</div>

					</div>
				</div>
			</section>
        

			
		</main>
    <footer id="footer">
			<div class="container">
				<div class="row text-center">
					<div class="footer-content">						
						<strong>Copyright|IT Development Team|Prime Health</strong>
					</div>
				</div>
			</div>
		</footer>
    <!-- Essential jQuery Plugins
		================================================== -->
    <!-- Main jQuery -->
    <script src="js/jquery-1.11.1.min.js"></script>
    <!-- Twitter Bootstrap -->
    <script src="js/bootstrap.min.js"></script>
    <!-- Single Page Nav -->
    <!-- jquery.fancybox.pack -->
    <script src="js/jquery.fancybox.pack.js"></script>
    <!-- Google Map API -->
    <script src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <!-- Owl Carousel -->
    <script src="js/owl.carousel.min.js"></script>
    <!-- jquery easing -->
    <script src="js/jquery.easing.min.js"></script>
    <!-- Fullscreen slider -->
    <script src="js/jquery.slitslider.js"></script>
    <script src="js/jquery.ba-cond.min.js"></script>
    <!-- onscroll animation -->
    <script src="js/wow.min.js"></script>
    <!-- Custom Functions -->
    <script src="js/main.js"></script>
    </form>
</body>
</html>