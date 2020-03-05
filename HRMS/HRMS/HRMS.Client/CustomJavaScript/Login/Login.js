var IP = "";
var ConfigurationData = {
    GlobalApiURL: ""
};
$(function ()
{
     GetIPAddressFun();
    history.pushState(null, null, location.href);
    window.onpopstate = function () {
        history.go(1);
    };
    SetConfiguratedData();
   
});


function Login()
{
    var UserName = $("#UserNameTxt").val();
    var Password = $("#PasswordTxt").val();   
    var loginDTO = {
        UserName: UserName,
        UserPassword: Password,
        IPAddress: IP[0]
    };
    var ActionUrl = ConfigurationData.GlobalApiURL + "/Login/Login";
    Common.Ajax('post', ActionUrl, JSON.stringify(loginDTO), 'json', LoginSuccess);
   
} 


function LoginSuccess(Result)
{

    if (Result.Data.ApprovalFlowFK === null)
    {
        $('.overlayLoadingLogin').hide();
        swal("Oops", "Error,Please Refer to your system administrator or HR department !", "error");     
    }
    else if (Result.Success === true)
    {
    

        createCookie("CypressUserID", Result.Data.UserID, 1);
        createCookie("CypressUserName", Result.Data.UserName, 1);
        createCookie("ProfilePictureURL", Result.Data.UserImgURL, 1);
        createCookie("HasCompletedUserProfileData", Result.Data.HasCompletedUserProfileData, 1);
        createCookie("IsHr", Result.Data.IsHR, 1);
        createCookie("IsAdmin", Result.Data.IsAdmin, 1);
        createCookie("IsManager", Result.Data.IsManager, 1);
       
         window.location.href = "/Dashboard/Index";
    }
    else
    {
        window.location.href = ConfigurationData.GlobalApiURL+"/Login/UnAuthorized";

    }



}
function createCookie(name, value, days) {
    var expires;

    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + expires + "; path=/";
}
function SetConfiguratedData() {
    ConfigurationData.GlobalApiURL = document.getElementById("GlobalApiURL").value;
}


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

$(document).on('keypress', function (e)
{
    if (e.which === 13) {
        Login();
    }
});

$(document).ajaxStart(function () {
    $('.overlayLoadingLogin').fadeIn().delay(200000).fadeOut();
});

$(document).ajaxStop(function () {
    $('.overlayLoadingLogin').fadeOut();
});






