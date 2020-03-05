
$(document).ready(function () {

    $("#Login").on("click", function () {
        logIn();
    });
    $(document).on("keypress", function (e) {

        if (e.which == 13)
        {
            logIn();
        }
    })

})

function logIn()
{
        var userName = $('#UserName').val();
        var password = $('#Password').val();
        if (userName == '' || password == '') {
            ShowALert(1, "Please Enter All Data");
        }
        else {
            var User = {
                UserName:userName,
                Password: password
            };

            var LoginURL = "/User/UserLogIn";
            Common.Ajax('post', LoginURL, JSON.stringify(User), 'json', SucessLogin, false);

        }
}
function SucessLogin(Result) {
    if (Result.Success==true) {
        //window.location.href = "/Receiving/ShowRecievingRequests";
        window.location.href = "/Receiving/getRequestLifeCycle";

    }
    else
    {
        ShowALert(4, Result.Message);
    }
}
// Alert Div types : info=1,success=2,warning=3,danger=4
function HideALer(Type) {
    if (Type === 1) {
        $("#ShowALertInfo").hide();
    }
    else if (Type === 2) {
        $("#ShowALertSuccess").hide();
    }
    else if (Type === 3) {
        $("#ShowALertWarning").hide();
    }
    else if (Type === 4) {
        $("#ShowALertDanger").hide();
    }
}
// Alert Div types : info=1,success=2,warning=3,danger=4
function ShowALert(Type, AlertText) {
    if (Type === 1) {
        $("#AlertInfoText").html(AlertText);
        $("#ShowALertInfo").show();
    }
    else if (Type === 2) {
        $("#AlertSuccessText").html(AlertText);
        $("#ShowALertSuccess").show();
    }
    else if (Type === 3) {
        $("#AlertWarningText").html(AlertText);
        $("#ShowALertWarning").show();
    }
    else if (Type === 4) {
        $("#AlertDangerText").html(AlertText);
        $("#ShowALertDanger").show();
    }
}
//-------------------------------------