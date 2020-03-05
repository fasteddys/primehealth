<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stop.aspx.cs" Inherits="CardCycle.Stop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Stop Page</title>
		<link href="css/style.css" rel="stylesheet" type="text/css"  media="all" />
		<script src="http://code.jquery.com/jquery-1.9.1.min.js" type="text/javascript"></script>
	    <script src="js/countdown.js" type="text/javascript"></script>	
	    <script src="js/init.js" type="text/javascript"></script>
	</head>
	<body>
        <form runat="server">
		<!---start-wrap---->
		<div class="wrap">
			<!---start-content---->
			<div class="content">
				<h1>System <spn style="color:#ff0000">under</spn> maintenance</h1>
				<p>We'll be here soon . Estimated Time Remaining <span style="color:#ff0000"> 8 Min </span> </p>
			</div>
 
 
  
			<!---End-content---->
			<!---start-countdown-timer----->
				<ul id="countdown">
                    <asp:scriptmanager ID="Scriptmanager1" runat="server"></asp:scriptmanager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
					<li class="second">
						<span class="hours2"><asp:Label ID="Label1" runat="server" Text="00" Font-Size="XX-Large"></asp:Label></span>
						<h3>hours</h3>
					</li>

					<li class="three">
						<span class="minutes2"><asp:Label ID="Label3" runat="server" Font-Size="XX-Large"></asp:Label></span>
						<h3>minutes</h3>
					</li>
					<li class="four">
						<span class="secondss"><asp:Label ID="Label2" runat="server" Font-Size="XX-Large"></asp:Label></span>
						<h3>seconds</h3>
					</li>
      <asp:Timer ID="tm1" Interval="1000" runat="server" ontick="tm1_Tick" />	
      </ContentTemplate>
  <Triggers>
    <asp:AsyncPostBackTrigger ControlID="tm1" EventName="Tick" />
  </Triggers>
</asp:UpdatePanel>
				</ul>

				<div class="clear"> </div>
				<!---End-countdown-timer----->
				<!---start-notified----->
			<br />
            <br />
            <br />
				<!---End-notified----->
				<!---start-copy-right----->
				<div class="copy-right">
					<p>Design by <a href="#">IT DESPARTMENT</a></p>
				</div>
				<!---End-copy-right----->
		</div>
		<!---End-wrap---->
            </form>
	</body>
</html>
