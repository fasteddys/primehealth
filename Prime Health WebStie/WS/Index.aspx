<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WS.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <link rel="shortcut icon" href="img/icon.ico" />
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
<div>
				<a href="Index.aspx#body"><img src="img/prime.png" alt="companylogo" width="156px" height="46px"/></a>

</div>
					
					
					<!-- /logo -->
                </div>

				<!-- main nav -->
                <nav class="collapse navbar-collapse navbar-right" role="navigation">
                    <ul id="nav" class="nav navbar-nav">
                        <li><a href="#body">Home</a></li>
                        <li><a href="#service">Service</a></li>
                        <li><a href="#portfolio">Profile</a></li>
                        <li><a href="#products">Products</a></li>
                        <li><a href="#Request">Offer Request</a></li>
                         <li><a href="#careers">Careers</a></li>
                        <li><a href="#clients">Our Corporate Clients</a></li>
                        <li><a href="#contact">Contact Us</a></li>
                        <li><a href="#">Complaints</a></li>
                    </ul>
                </nav>
				<!-- /main nav -->
				
            </div>
        </header>
    <!--
        End Fixed Navigation
        ==================================== -->
    <main class="site-content" role="main">
		
        <!--
        Home Slider
        ==================================== -->
		
        
        <section id="home-slider">
            <div id="slider" class="sl-slider-wrapper" >

				<div class="sl-slider">
				
					<div class="sl-slide">

						<div class="bg-img bg-img-1"></div>

						<div class="slide-caption">
                            <div class="caption-content" >
                               <h2 class="animated bounceInUp">Our <label style="color:green">Prime</label>  Concern is your <label style="color:blueviolet">Health</label> </h2>
                                <span class="animated fadeInDown">Egypt's Medical Insurance Leaders</span>
                         <a href="#about" class="btn btn-success btn-effect">Read More</a>
                            </div>
                        </div>
					</div>
                    </div>
                </div>
            </section>
       

		<%--<section id="home-slider">
            <div id="slider" class="sl-slider-wrapper">

				<div class="sl-slider">
				
					<div class="sl-slide">

						<div class="bg-img bg-img-1"></div>

						<div class="slide-caption">
                            <div class="caption-content" >
<%--                                <h2 class="animated bounceInUp">Our <label style="color:green">Prime</label>  Concern is your <label style="color:blueviolet">Health</label> </h2>--%>
<%--                                <span class="animated fadeInDown">Clean and Professional one page Template</span>
     <a href="#" class="btn btn-success btn-effect">Join US</a>
                            </div>
                        </div>
					</div>
					
					<div class="sl-slide" data-orientation="vertical" data-slice1-rotation="10" data-slice2-rotation="-15" data-slice1-scale="1.5" data-slice2-scale="1.5">
					
						<div class="bg-img bg-img-2"></div>
						<div class="slide-caption">
                            <div class="caption-content">
                                <h2 class="animated fadeInDown">Bootstrap Onepage aspx Template</h2>
                                <span>Clean and Professional one page Template</span>
                                <a href="#" class="btn btn-success btn-effect">Join US</a>
                            </div>
                        </div>
						
					</div>
					
					<div class="sl-slide" data-orientation="horizontal" data-slice1-rotation="3" data-slice2-rotation="3" data-slice1-scale="2" data-slice2-scale="1">
						
						<div class="bg-img bg-img-3"></div>
						<div class="slide-caption">
                            <div class="caption-content">
                                 <h2 class="animated fadeInDown">Bootstrap Onepage aspx Template</h2>
                                <span>Clean and Professional one page Template</span>
                                <a href="#" class="btn btn-success btn-effect">Join US</a>
                            </div>
                        </div>

					</div>

				</div><!-- /sl-slider -->

                <!-- 
                <nav id="nav-arrows" class="nav-arrows">
                    <span class="nav-arrow-prev">Previous</span>
                    <span class="nav-arrow-next">Next</span>
                </nav>
                -->
                
                <nav id="nav-arrows" class="nav-arrows hidden-xs hidden-sm visible-md visible-lg">
                    <a href="javascript:;" class="sl-prev">
                        <i class="fa fa-angle-left fa-3x"></i>
                    </a>
                    <a href="javascript:;" class="sl-next">
                        <i class="fa fa-angle-right fa-3x"></i>
                    </a>
                </nav>
                

				<nav id="nav-dots" class="nav-dots visible-xs visible-sm hidden-md hidden-lg">
					<span class="nav-dot-current"></span>
					<span></span>
					<span></span>
				</nav>

			</div><!-- /slider-wrapper -->
		</section>--%>
		
        <!--
        End Home SliderEnd
        ==================================== -->
			
			<!-- about section -->
			<section id="about" >
				<div class="container">
					<div class="row; animated fadeInLeft">
						<div class="col-md-4 wow animated fadeInLeft">
							<div class="recent-works">
                                                        <br />
								<h3>News & Events</h3>
								<div id="works">
									<div class="work-item">
										<p>Check here frequently for our latest news and events.</p>
									</div>
									<div class="work-item">
<%--	                                  <p></p>--%>
									</div>
									<div class="work-item">
<%--	                                <p></p>--%>
									</div>
								</div>
							</div>
						</div>
						<div class="col-md-7 col-md-offset-1 wow animated fadeInRight">
							<div class="welcome-block">
								<h2>Prime Health Egypt</h2>								
						     	 <div class="message-body">
									<img src="img/member-2.jpg" class="img-responsive" alt="member">
						       		<h5 style="text-align:justify">Prime Health was established in 2001, as the first third party administrator to serve the medical and insurance field in the Egyptian market.
                                            specialize in medical claims management. Following its acquisition Prime Health became member of Medgulf group in 2009. 
                                           The Mediterranean and Gulf insurance and reinsurance (Medgulf) is a leading regional insurance Company providing the retail & institutional markets with comprehensive insurance coverage through its operations in various countries.
                                           <br />
                                                Medgulf has been proudly serving its clients for more than 30 years. 
                                              Prime Heath vision is to fix the damage done in the markets to insurance industry image and set up the right procedures and modules to develop a healthy market in order to offer the community the solid security they needed, allied with professional products and quality services.
                                         
                                         
                                               </h5>
						     	 </div>

                               
<%--						       	<a href="#" class="btn btn-border btn-effect pull-right">Read More</a>--%>
						    </div>
						</div>
					</div>
				</div>
			</section>
			<!-- end about section -->
				
			
			<!-- Service section -->
			<section id="service">
				<div class="container">
					<div class="row">
						<div class="sec-title text-center">
                                                        <br />
                            
							<h2 class="wow animated bounceInLeft">Services</h2>
							<strong class="wow animated bounceInRight">Our Key Features</strong>
						</div>
						
						<div class="col-md-3 col-sm-6 col-xs-12 text-center wow animated zoomIn">
							<div class="service-item">
								<div>
									<img src="img/icon1.png" />
								</div>
								<h3>Member Account</h3>
								<a href="http://phe-dashboard.it-fusion.org/PrimeHealth/saw.dll?Dashboard&NQUser=member&NQPassword=member" target="_blank">This link provides subscribers with their personalised medical histoy since their policy inception including all prescribed medicines .</a>
							</div>
						</div>
					
						<div class="col-md-3 col-sm-6 col-xs-12 text-center wow animated zoomIn" data-wow-delay="0.3s">
							<div class="service-item">
								<div>
									<img src="img/icon2.png" />
								</div>
								<h3>Corporate Account</h3>
								<a href="http://phe-dashboard.it-fusion.org/PrimeHealth/saw.dll?Dashboard&NQUser=invoices&NQPassword=invoices" target="_blank">This link provides your company with full information on the medical utilization and financial updates of your group.</a>
							</div>
						</div>
					
						<div class="col-md-3 col-sm-6 col-xs-12 text-center wow animated zoomIn" data-wow-delay="0.6s">
							<div class="service-item">
								<div>
									<img src="img/icon3.png" />
								</div>
								<h3>Provider Account</h3>
								<a href="http://phe-dashboard.it-fusion.org/PrimeHealth/saw.dll?Dashboard&NQUser=provider&NQPassword=provider" target="_blank">This link provides access to the service providers accounts. Access is only granted to service providers.</a>
							</div>
						</div>
					
						<div class="col-md-3 col-sm-6 col-xs-12 text-center wow animated zoomIn" data-wow-delay="0.9s">
							<div class="service-item">
								<div>
									<img src="img/icon4.png" />
								</div>
								
								<h3>Network</h3>
								<a href="#" id="btndownload">This link provides any subscirber with all contacts of the medical providers contatracted by Prime Health</a>
                                	
                                <br />
 

                      
							</div>
						</div>
						
					</div>
				</div>
			</section>
			<!-- end Service section -->
			
			<!-- portfolio section -->
			<section id="portfolio">
				<div class="container">
					<div class="row">
						<div class="sec-title text-center wow animated fadeInDown">
                                                        <br />
							<h2>Profile</h2>
							<%--<p></p>--%>
						</div>
						<ul class="project-wrapper wow animated fadeInUp">
							<li class="portfolio-item">
								<img src="img/portfolio/item.jpg" class="img-responsive">
								<figcaption >
									<p>A unique approach for a better Health Management</p>
								</figcaption>
								<ul>
									<%--<li><a class="fancybox" href="img/portfolio/item.jpg"></a></li>--%>
									
								</ul>
							</li>
							
							<li class="portfolio-item">
								<img src="img/portfolio/item2.jpg" class="img-responsive">
								<figcaption >
									<p>We are among the top to expand beyond Egyptian territory.</p>
								</figcaption>
								<ul>
<%--									<li><a class="fancybox" href="img/slider/banner.jpg"></a></li>--%>
								</ul>
							</li>
							
							<li class="portfolio-item">
								<img src="img/portfolio/item3.jpg" class="img-responsive">
								<figcaption>
									<p> Leaders In delivery beyond Our promises.</p>
								</figcaption>
								<ul>
<%--									<li><a class="fancybox"  href="img/portfolio/item3.jpg"></a></li>--%>
								</ul>
							</li>
							
							<li class="portfolio-item">
                               
                               
								<img src="img/portfolio/item4.jpg" class="img-responsive">
                                
								<figcaption>
									<p>We are among the top to take an electronic approach with our clients, soon with our providers.</p>
								</figcaption>
								<ul>
<%--									<li><a class="fancybox" href="img/portfolio/item4.jpg"></a></li>--%>
								</ul>
							</li>
							
							<li class="portfolio-item">
								<img src="img/portfolio/item5.jpg" class="img-responsive">
								<figcaption>
									<p>Prime Health is your absolute paperless desk</p>
								</figcaption>
								<ul>
<%--									<li><a class="fancybox" href="img/portfolio/item5.jpg"></a></li>--%>
								</ul>
							</li>
						</ul>
						
					</div>
				</div>
			</section>
			<!-- end portfolio section -->
			
	
			
			<!-- Product section -->
			<section id="products">
                <br />
				<div class="container">
					<div class="row">
					
						<div class="sec-title text-center wow animated fadeInDown">
							<h2>Products</h2>
							<strong>We are pleased to inform you that the following products are available from Prime Health:</strong>
						</div>
					<div class="row">
                        					<ul class="project-wrapper wow animated fadeInUp">
							<li class="portfolio-item">
								<img src="img/icon5.png" class="img-responsive" >
								<figcaption >
                      <p>Local Healthcare Programs to cover you and/or members of your family with direct billing and outside Egypt on reimbursement .</p>
								</figcaption>
								<ul>
<%--									<li><a class="fancybox" href="img/portfolio/item.jpg"></a></li>--%>
									
								</ul>
							</li>
							
							
							
							<li class="portfolio-item">
								<img src="img/icon8.jpg" class="img-responsive">
								<figcaption>
									<p>Inpatient and OutPatient inside & outside network with direct billing reimbursement according to Egypt rate,World Wide excluding U.S.A & Canada.</p>
								</figcaption>
								<ul>
<%--									<li><a class="fancybox"  href="img/portfolio/item3.jpg"></a></li>--%>
								</ul>
							</li>
							
							<li class="portfolio-item">
								<img src="img/icon7.jpg" class="img-responsive">
								<figcaption>
									<p>Medical insurance to cover groups over 25 persons as corporate medical benefits to be given to all employees and/or their families of your company.</p>
								</figcaption>
								<ul>
<%--									<li><a class="fancybox" href="img/portfolio/item4.jpg"></a></li>--%>
								</ul>
							</li>
						</ul>

					</div>	
		
					</div>
				</div>
			</section>
			<!-- end Price section -->
			
        <section id="Request">
				<div class="container">
					<div class="row">
						<div class="sec-title text-center wow animated fadeInDown">
                                                        <br />
							<h2>Offer Request</h2>
							<h3>What type of Plan you need ?</h3>
						</div>
					<div class="sec-title text-center wow animated fadeInDown">
                     <h4>If you want to inquiry about information or request an offer from Prime Health ,Please contact Us on  <strong>Sales@prime-health.org</strong></h4>

					</div>	
		
					</div>
				</div>
			</section>

          <section id="careers">
				<div class="container">
					<div class="row">
						<div class="sec-title text-center wow animated fadeInDown">
                                                        <br />
							<h2>Careers</h2>
							<h4 style="text-align:justify">Prime Health is a medical Third Party Administrator dealing with Multinational and Egyptian large corporations looking for Medical Insurance coverage for their employees. 

Prime Health has one of the most industry success stories in this field. 

A wide career opportunity is opened for ambitious University graduates, so why not considering a long term and fruitful career, in one of the most successful businesses today</h4>
						
					
                     <h4 style="text-align:justify">For you to reach unlimited opportunities, Send Us your recent C.V on <strong>Info@prime-health.org</strong></h4>
                            </div>
					</div>
				</div>
			</section>

          <section id="clients">
				<div class="container">
					<div class="row">
						<div class="sec-title text-center wow animated fadeInDown">
                                                        <br />
							<h2>Some of Our Corporate Clients</h2>
                            </div>
					</div>
				</div>
              <div class="container">
        <div class="row">
                       <div class="col-xs-3">
                                       <img src="img/Clients/1.jpg" alt="image" style="height:90px;width:230px"/>
                        </div>
        <div class="col-xs-3">
                                       <img src="img/Clients/2.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
      <div class="col-xs-3">
                                       <img src="img/Clients/3.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
      <div class="col-xs-3">
                                       <img src="img/Clients/4.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
            </div>
    </div>
              <br />
              <div class="container">
        <div class="row">
                       <div class="col-xs-3">
                                       <img src="img/Clients/5.jpg" alt="image" style="height:90px;width:230px"/>
                        </div>
        <div class="col-xs-3">
                                       <img src="img/Clients/6.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
      <div class="col-xs-3">
                                       <img src="img/Clients/7.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
      <div class="col-xs-3">
                                       <img src="img/Clients/8.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
            </div>
    </div>
              <br />
              <div class="container">
        <div class="row">
                       <div class="col-xs-3">
                                       <img src="img/Clients/9.jpg" alt="image" style="height:90px;width:230px"/>
                        </div>
        <div class="col-xs-3">
                                       <img src="img/Clients/10.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
      <div class="col-xs-3">
                                       <img src="img/Clients/11.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
      <div class="col-xs-3">
                                       <img src="img/Clients/12.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
            </div>
    </div>
              <br />
              <div class="container">
        <div class="row">
                       <div class="col-xs-3">
                                       <img src="img/Clients/13.jpg" alt="image" style="height:90px;width:230px"/>
                        </div>
        <div class="col-xs-3">
                                       <img src="img/Clients/14.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
             <div class="col-xs-3">
                                       <img src="img/Clients/15.jpg" alt="image" style="height:90px;width:230px"/>
        </div>

             <div class="col-xs-3">
                                       <img src="img/Clients/16.gif" alt="image" style="height:90px;width:230px"/>
        </div>
            </div>
    </div>
              <br />

                  <div class="container">
        <div class="row">
                       <div class="col-xs-3">
                                       <img src="img/Clients/17.jpg" alt="image" style="height:90px;width:230px"/>
                        </div>
        <div class="col-xs-3">
                                       <img src="img/Clients/18.png" alt="image" style="height:90px;width:230px"/>
        </div>
             <div class="col-xs-3">
                                       <img src="img/Clients/19.jpg" alt="image" style="height:90px;width:230px"/>
        </div>

             <div class="col-xs-3">
                                       <img src="img/Clients/20.png" alt="image" style="height:90px;width:230px"/>
        </div>
            </div>
    </div>
<%--              <div class="container">
        <div class="row">
                       <div class="col-xs-3">
                                       <img src="img/Clients/9.jpg" alt="image" style="height:90px;width:230px"/>
                        </div>
        <div class="col-xs-3">
                                       <img src="img/Clients/10.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
      <div class="col-xs-3">
                                       <img src="img/Clients/11.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
      <div class="col-xs-3">
                                       <img src="img/Clients/12.jpg" alt="image" style="height:90px;width:230px"/>
        </div>
            </div>
    </div>--%>
			</section>

			<!-- Social section -->
			<%--<section id="social" class="parallax">
				<div class="overlay">
					<div class="container">
						<div class="row">
						
							<div class="sec-title text-center white wow animated fadeInDown">
								<h2>Contact Us</h2>
								<h3 style="color:red">Call Center (16950)</h3>
							</div>
							
							<ul class="social-button">
								<li class="wow animated zoomIn"><a href="#"><i class="fa fa-thumbs-up fa-2x"></i></a></li>
								<li class="wow animated zoomIn" data-wow-delay="0.3s"><a href="#"><i class="fa fa-twitter fa-2x"></i></a></li>
								<li class="wow animated zoomIn" data-wow-delay="0.6s"><a href="#"><i class="fa fa-dribbble fa-2x"></i></a></li>							
							</ul>
							
						</div>
					</div>
				</div>
			</section>--%>
			<!-- end Social section -->
       
						
						
							<%--<form action="#" method="post">
								<div class="input-field">
                                    <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Your Name..." runat="server"></asp:TextBox>
									
								</div>
								<div class="input-field">
									 <asp:TextBox ID="txtemail" CssClass="form-control" placeholder="Your Email..." runat="server"></asp:TextBox>
								</div>
								<div class="input-field">
									 <asp:TextBox ID="txtsubject" CssClass="form-control" placeholder="Your Subject..." runat="server"></asp:TextBox>
								</div>
								<div class="input-field">
                                 <asp:TextBox ID="txtmessage" CssClass="form-control" placeholder="Message..." runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
									
								</div>
                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-blue btn-effect" Text="Submit"></asp:Button>
						       
							</form>--%>
               
       

			<!-- Contact section -->
			<section id="contact" >
				<div class="container">
					<div class="row">
						<div class="sec-title text-center wow animated fadeInDown">
                                                        <br />
							<h2>Contacts</h2>
								<h3 style="color:red">Call Center (16950)&nbsp;&nbsp;&nbsp;&nbsp; </h3>
						</div>
						
						
						<div class="col-md-7 contact-form wow animated fadeInLeft">
							<%--<form action="#" method="post">
								<div class="input-field">
                                    <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Your Name..." runat="server"></asp:TextBox>
									
								</div>
								<div class="input-field">
									 <asp:TextBox ID="txtemail" CssClass="form-control" placeholder="Your Email..." runat="server"></asp:TextBox>
								</div>
								<div class="input-field">
									 <asp:TextBox ID="txtsubject" CssClass="form-control" placeholder="Your Subject..." runat="server"></asp:TextBox>
								</div>
								<div class="input-field">
                                 <asp:TextBox ID="txtmessage" CssClass="form-control" placeholder="Message..." runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
									
								</div>
                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-blue btn-effect" Text="Submit"></asp:Button>
						       
							</form>--%>
                            <div class="col-md-5 wow animated fadeInRight">
							<address class="contact-details">
								<h3>Prime Health Contacts</h3>	
                                <p>Head Office</p>					
								<p><i class="fa fa-calendar"></i>Address: 6D/5 El laslky St, New Maadi - Cairo</p><br>
								<p><i class="fa fa-phone"></i>Tel :- +(020) 25173546/79/78</p>
								<p><i class="fa fa-calendar"></i>Work Times: all week days from 8:30 to 5:00 except Friday & Saturday</p>
							</address>
                                <br />
                                <address class="contact-details">
								<h3>Alexandria Office</h3>					
								<p><i class="fa fa-calculator"></i>Address : 14 Street Ptolemy El-Falaky from the Corniche - Saba Basha</p><br>
								<p><i class="fa fa-phone"></i>Tel :- 03-5841794/5</p>
								<p><i class="fa fa-fax"></i>Fax :03-5841790</p>
								<p><i class="fa fa-mobile"></i>Mobile : 010-00035042</p>
							</address>
						</div>
						</div>
						
						<div class="col-md-5 wow animated fadeInRight">
							<address class="contact-details">
								<h3>Call Center</h3>						
								<p><i class="fa fa-calendar"></i>Work Times: 24 hours, seven days a week including holidays</p><br>
								<p><i class="fa fa-phone"></i>Tel :- +(020)-25173577</p>
								<p><i class="fa fa-mobile"></i>Mobile : 012-3979724</p>
								<p><i class="fa fa-fax"></i>Fax : 02-25173540</p>
								<p><i class="fa fa-envelope"></i>callcenter@prime-health.org</p>
                                <p><i class=""></i>For any inquiries / Feedback:</p>
                                <p><i class="fa fa-envelope"></i>info@prime-health.org</p>
							</address>
                             <br />
                            </div>
                        <div class="col-md-5 wow animated fadeInRight">
                            <address class="contact-details">
                                <h3>Download Mobile Application</h3>	
                                 <a href="https://play.google.com/store/apps/details?id=io.PrimeHealth.PrimeHealth" target="_blank">
   <img src="img/googlePlayBadgeBig.png" alt="HTML tutorial"  style="height:90px;width:230px">
                                   

</a>
                                </address>
               <br />           
                            <address class="contact-details">
                                 <a href="https://itunes.apple.com/us/app/primehealth/id1074826596?ls=1&mt=8" target="_blank">
   <img src="img/downloadIOS.jpg" alt="HTML tutorial" style="height:90px;width:230px">
</a>
                            </address>
						</div>
			
					</div>
                    </div>
				
			</section>
			<!-- end Contact section -->

       
        <%--<section id="complaints" class="parallax">
             <div class="sec-title text-center wow animated fadeInDown">
            <h2>Complaint Form</h2>
							</div>
				<div class="overlay">
					<div class="container">
					<form action="#" method="post">
								<div class="input-field">
                                    <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Your Name..." runat="server"></asp:TextBox>
									
								</div>
                               <div class="input-field">
                                    <asp:TextBox ID="txtphone" CssClass="form-control" placeholder="Your Phone..." runat="server"></asp:TextBox>
									
								</div>
								<div class="input-field">
									 <asp:TextBox ID="txtemail" CssClass="form-control" placeholder="Your Email..." runat="server"></asp:TextBox>
								</div>
								<div class="input-field">
                                    <asp:DropDownList ID="drp1" runat="server"></asp:DropDownList>
								</div>
								<div class="input-field">
                                 <asp:TextBox ID="txtmessage" CssClass="form-control" placeholder="your Complaint..." runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
									
								</div>
                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-blue btn-effect" Text="Submit"></asp:Button>
						       
							</form>
					</div>
				</div>
			</section>--%>
        

			
		</main>
    <footer id="footer">
			<div class="container">
				<div class="row text-center">
					<div class="footer-content">
						<%--<div class="wow animated fadeInDown">
							<p>newsletter signup</p>
							<p>Get Cool Stuff! We hate spam!</p>
						</div>
						<form action="#" method="post" class="subscribe-form wow animated fadeInUp">
							<div class="input-field">
								<input type="email" class="subscribe form-control" placeholder="Enter Your Email...">
								<button type="submit" class="submit-icon">
									<i class="fa fa-paper-plane fa-lg"></i>
								</button>
							</div>
						</form>
						<div class="footer-social">
							<ul>
								<li class="wow animated zoomIn"><a href="#"><i class="fa fa-thumbs-up fa-3x"></i></a></li>
								<li class="wow animated zoomIn" data-wow-delay="0.3s"><a href="#"><i class="fa fa-twitter fa-3x"></i></a></li>
								<li class="wow animated zoomIn" data-wow-delay="0.6s"><a href="#"><i class="fa fa-skype fa-3x"></i></a></li>
								<li class="wow animated zoomIn" data-wow-delay="0.9s"><a href="#"><i class="fa fa-dribbble fa-3x"></i></a></li>
								<li class="wow animated zoomIn" data-wow-delay="1.2s"><a href="#"><i class="fa fa-youtube fa-3x"></i></a></li>
							</ul>
						</div>--%>
						
						<strong>Copyright|IT Development Team|Prime Health</strong>
					</div>
				</div>
			</div>
		</footer>
    <!-- Essential jQuery Plugins
		================================================== -->
    <!-- Main jQuery -->
    <script src="js/jquery-1.11.1.min.js"></script>
        <script type="text/javascript">
            $('#btndownload').click(function (e) {
                e.preventDefault();
                window.location = "http://41.128.183.109:3636/api/Values/Generate?format=xlsx";
            });
</script>
        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

            ga('create', 'UA-80000446-1', 'auto');
            ga('send', 'pageview');

</script>
    <!-- Twitter Bootstrap -->
    <script src="js/bootstrap.min.js"></script>
    <!-- Single Page Nav -->
<%--        <script src="js/jquery.smint.js"></script>--%>
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


