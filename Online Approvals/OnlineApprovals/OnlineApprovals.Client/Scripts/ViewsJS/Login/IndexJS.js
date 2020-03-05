$(document).ajaxStart(function () {
    $("#LoadingDiv").show();
});
$(document).ajaxStop(function () {
    $("#LoadingDiv").delay(50).hide(0);
});
$(function () {
    history.pushState(null, null, location.href);
       window.onpopstate = function () {
           history.go(1);
       };
});

$("#LoginBtn").click(function ()
{
    var UserName = $("#UserNameTxt").val();
    var Password = $("#PasswordTxt").val();
    var RememberMe = $("#RememberMe").prop('checked');

    var loginDTO = {
        UserName: UserName,
        Password: Password,
        RememberMe: RememberMe
    };
    var ActionUrl = '/Login/LoginAuthentication';
    Common.Ajax('post', ActionUrl, loginDTO, 'json', LoginSuccess);
 
});
function LoginSuccess(Data)
{
    if (Data === "Success")
    {
        window.location.href = '/Home/Index';
    }
    else {
        swal("Creditionals Error !", "Please Check UserName or Password", "error");
    }

}