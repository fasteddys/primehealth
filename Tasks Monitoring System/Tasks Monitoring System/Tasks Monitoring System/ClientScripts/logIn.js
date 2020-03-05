$(document).ready(function () {

    $("#signIn").on("click", function () {
        Login();
    });
    new WOW().init();
    //------------------------------------------------------
    $("body").niceScroll({
        cursorcolor: "#6600FF",
        cursorwidth: "20px",
        horizrailenabled: false,
        cursorheight: "150px",
        //cursorborder: "1px solid red", // css definition for cursor border
        cursorborderradius: "10px",
    });
   //------------------------------------------------------

});

$(document).on('keypress', function (e) {
    if (e.which === 13) {
        Login();
    }
});

function Login() {
    var userName = $("#userName").val();
    var password = $("#password").val();
    if (userName == "" || password == "") {
        //alert("Please Enter All Data")
        ShowALert(1, "Please Enter All Data");
    }
    else {
        var urlLoginUser = "/Users/UserLogIn";
        var NewLoginData =
            {
                UserName: userName,
                Password: password
            }
        Common.Ajax("post", urlLoginUser, JSON.stringify(NewLoginData), 'json', sucessLogIn)
    }


}
function sucessLogIn(Result) {
    if (!Result) {
        ShowALert(4, "User Not Founded");
    }
    else {

        window.location.href = "/UserDailsTasks/Index";
    }
}