$(document).ready(function () {
    var UserName = (JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientName);
    // var x = $("#UserName");
    //$("#ClientName").html(UserName);
    if (UserName !== null) {
        $(".EditWelcome").removeClass('fa fa-star-o');
        $("#WelcomeText").removeClass('HTML_SideView_Welcome');

        $(".EditWelcome").addClass('fa fa-edit');
        $("#WelcomeText").addClass('HTML_SideView_Edit');
    }

     


    if (JSON.parse(localStorage.getItem("CurrentVisit")) === null) {
        $("#HomePage").click(function () {

            window.location.href = ("Homepage.html");


        });


    } else {

        var today = new Date();
        var dd = String(today.getDate()).padStart(0, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        today = mm + '/' + dd + '/' + yyyy;
        //today.addda

        var CurrentVisit = JSON.parse(localStorage.getItem("CurrentVisit"));
        var EndDate = new Date(CurrentVisit.EndVisitDate);
        var x = new Date(today);


        if (new Date(today).getDate() <= EndDate.getDate()) {


            $("#HomePage").click(function () {

                window.location.href = ("MainTripScreen.html");


            });
        } else {
            $("#HomePage").click(function () {

                window.location.href = ("Homepage.html");


            });
        }


      

    }
    // var x =JSON.parse(localStorage.getItem("UserData"));
    if (JSON.parse(localStorage.getItem("UserData")) !== null) {
        $("#PreviousVisits").show();
        

     }
    $("#PreviousVisits").click(function () {

        window.location.href = ("PreviousVisits.html");


    });
    $("#AboutUs").click(function () {

        window.location.href = "Aboutus.html";


    });
    $("#ContactUs").click(function () {

        window.location.href = "ContactUs.html";


    });
   // disableBackButton.Disable();

});