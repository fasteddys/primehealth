$(document).ready(function () {
    setTimeout(myFunction, 2000);
    //disableBackButton.Disable();

});

function myFunction() {
    var x = localStorage.getItem("LanguageID");
    if (localStorage.getItem("LanguageID") === null) {
        $("#Chooseyourlanguage").attr("href", "Chooseyourlanguage.html");
        $.mobile.changePage("Chooseyourlanguage.html", { transition: "slide" });
        setTimeout(function () { window.location = "Chooseyourlanguage.html"; }, 500);
    }
    else {
        //window.location.href = ("Homepage.html");
    
    if (JSON.parse(localStorage.getItem("CurrentVisit")) === null) {
  
        $("#HomePage").attr("href", "Homepage.html");
        $.mobile.changePage("Homepage.html", { transition: "slide" });
       window.location.href = "Homepage.html";
    } else {

        var today = new Date();
        var dd = String(today.getDate()).padStart(0, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        today = mm + '/' + dd + '/' + yyyy;
        //today.addda

        var CurrentVisit = JSON.parse(localStorage.getItem("CurrentVisit"));
        var EndDate = new Date(CurrentVisit.EndVisitDate);
       // var x = new Date(today);


        if (new Date(today).getDate() <= EndDate.getDate()) {


           

                $("#HomePage").attr("href", "MainTripScreen.html");
                $.mobile.changePage("MainTripScreen.html", { transition: "slide" });
                window.location.href = "MainTripScreen.html";
        } else {
                $("#HomePage").attr("href", "Homepage.html");
                $.mobile.changePage("Homepage.html", { transition: "slide" });
                window.location.href = "Homepage.html";

        }










    }
    }

}