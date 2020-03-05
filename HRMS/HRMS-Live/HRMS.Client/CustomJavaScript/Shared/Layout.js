// Alert Div types : info=1,success=2,warning=3,danger=4

var LoggedUserData = {
    GlobalUserID: 0,
    GlobalUserName: "",
    GlobalUserProfilePicture: "",
    GlobalProfilePictureLocation:"",
    GlobalHasCompletedUserProfileData: "False",
    IsHR: ""
};

var ConfigurationData = {
    GlobalApiURL: "",
    PictureLocation:""
};
var LeaveDurationUnitEnum = {
    Days: 'Days',
    Hours: 'Hours'
};

$(document).ready(function () {
    //Activate();

    //var Wheight = $(window).height() / 2 - $(".Rotator").height() / 2 + "px";
    //console.log(Wheight);
    //$(".Rotator").css("marginTop", Wheight);
    //$(".Rotator").slideDown(1000);
    //$(".Rotator").slideDown(1000).animate({
    //    marginTop: Wheight,
    //}, 500, function () {
    //});
    $("#LoadingDiv").delay(750).hide(0);//750
    SetConfiguratedData();
    ShowApprovals();
    CheckUserDataStatus();
    ShowPendingRequests();
    ActiveNavigationBarCell();
    checkImageAvailabilityLayout(ConfigurationData.PictureLocation + LoggedUserData.GlobalUserProfilePicture);

    $("nav a").on("click", function () {
        $(this).parent().addClass("nav-active").siblings().removeClass("nav-active");
    });
    //notifications un active employees
    $("#showNotifactionEmployeeUnActive").on("click", function () {
        $(".employeeNotificationToActive").empty();
        GetAllUserThatWillBeActive();//notifications
        console.log("okey");
    });
    GetNumberOfAllUserThatWillBeActive();
    //----------------------------------------
    $("#showNotifactionEmployeeName").on("click", function () {
        $('#InsuranceEmployeeData').empty();
        GetEmployeesDeserveMedicalInsurance();//notifications
        console.log("okey");
    });
    GetEmployeesNumberDeserveMedicalInsurance();
});
//unActive employee
function GetNumberOfAllUserThatWillBeActive() {

    urlGetNumberOfAllUserThatWillBeActive = ConfigurationData.GlobalApiURL + "/Users/GetNumberOfAllUserThatWillBeActive";
    Common.Ajax("GET", urlGetNumberOfAllUserThatWillBeActive, null, 'json', SucessGetNumberOfAllUserThatWillBeActive, false);
}
function SucessGetNumberOfAllUserThatWillBeActive(Result) {
    if (Result.Success) {
        $(".numberOfNotifactions").html(Result.Data);
    }
}
function GetAllUserThatWillBeActive()
{
    global = false;
    urlGetAllEmployeesListThatWillBeActive = ConfigurationData.GlobalApiURL + "/Users/GetAllUserThatWillBeActive";
    Common.Ajax("GET", urlGetAllEmployeesListThatWillBeActive, null, 'json', SucessGetAllEmployeesListThatWillBeActive, false, null, null, null, global);
}
function SucessGetAllEmployeesListThatWillBeActive(Result)
{
    if (Result.Success)
    {
        $(".numberOfNotifactions").html(Result.Data.length);
        //$(".employeeNotificationToActive").empty();
        for (var i = 0; i < Result.Data.length; i++) {
          $(".employeeNotificationToActive").append("<li><a class='clearfix' href='/Configuration/EditEmployees?id=" + Result.Data[i].UserID + "'><figure class='image'><img src='../../img/!sample-user.jpg' alt='Joseph Doe Junior' class='rounded-circle' /></figure><span class='title'>" + Result.Data[i].UserName + "</span><span class='message'>Employee Is Not Active.</span><a></li>");
           
        }
    }
}
//--------------------------------
function GetEmployeesNumberDeserveMedicalInsurance() {
    var urlGetEmployeesNumber = ConfigurationData.GlobalApiURL + "/Users/GetEmployeesNumberDeserveMedicalInsurance";
    Common.Ajax('get', urlGetEmployeesNumber, null, 'json', SuccessGetEmployeesNumberDeserveMedicalInsurance, false);
}

function SuccessGetEmployeesNumberDeserveMedicalInsurance(Result) {
    if (Result.Success === true) {
        $('.InsuranceEmployeeNumbers').text(Result.Data);
        //GetEmployeesDeserveMedicalInsurance();
    }
}

function GetEmployeesDeserveMedicalInsurance() {
    global = false;
    var urlGetEmployeesData = ConfigurationData.GlobalApiURL + "/Users/GetEmployeesDeserveMedicalInsurance";
    Common.Ajax('get', urlGetEmployeesData, null, 'json', SuccessGetEmployeesDataDeserveMedicalInsurance, false, null, null, null, global);
}

function SuccessGetEmployeesDataDeserveMedicalInsurance(Result) {
    if (Result.Success === true) {
        for (var i = 0; i < Result.Data.length; i++) {
            $('#InsuranceEmployeeData').append("<li><a class='clearfix' href='/Configuration/EditEmployees?id=" + Result.Data[i].UserID + "'><figure class='image'><img src='../../img/!sample-user.jpg' alt='Joseph Doe Junior' class='rounded-circle' /></figure><span class='title'>" + Result.Data[i].UserName + "</span><span class='message'>Employee Deserves Medical ID.</span><a></li>");
        }
    }
}



function SetConfiguratedData() {
    LoggedUserData.GlobalUserID = document.getElementById("GlobalUserID").value;
    LoggedUserData.GlobalUserName = document.getElementById("GlobalUserName").value;
    LoggedUserData.GlobalUserProfilePicture = document.getElementById("GlobalUserProfilePicture").value;
    LoggedUserData.GlobalHasCompletedUserProfileData = document.getElementById("GlobalHasCompletedUserProfileData").value;
    ConfigurationData.GlobalApiURL = document.getElementById("GlobalApiURL").value;
    ConfigurationData.PictureLocation = document.getElementById("GlobalPictureLocation").value;
    LoggedUserData.IsHR = document.getElementById("GlobalIsHR").value;
}
function Logout()
{
    var UserID = LoggedUserData.GlobalUserID;
    var UserIP = "";
    //Common.Ajax('get', 'https://api.ipify.org/?format=json', null, 'json', SuccessGetClientIP, false);
    //function SuccessGetClientIP(e) {
    //    UserIP = e.ip;
    //}

    var loginDTO = {
        UserID: UserID,
        //IPAddress: UserIP,
        UserName: LoggedUserData.GlobalUserName
    };

    var ActionUrl = ConfigurationData.GlobalApiURL + "/Login/LogOut";
    Common.Ajax('post', ActionUrl, JSON.stringify(loginDTO), 'json', LogOutSuccess);
}
function ForceLogout() {
    var UserID = LoggedUserData.GlobalUserID;
    var UserIP = "";
    Common.Ajax('get', 'https://api.ipify.org/?format=json', null, 'json', SuccessGetClientIP, false);
    function SuccessGetClientIP(e) {
        UserIP = e.ip;
    }

    var loginDTO = {
        UserID: UserID,
        IPAddress: UserIP
    };

    var ActionUrl = ConfigurationData.GlobalApiURL + "/Login/ForceLogOut";
    Common.Ajax('post', ActionUrl, JSON.stringify(loginDTO), 'json', LogOutSuccess);
}
function LogOutSuccess() {
    window.location.href = '/Login/Logout/';
}
var angle = 0;
setInterval(function () {
    //console.log(angle);
    $("#image")
        .css('-webkit-transform', 'rotate(' + angle + 'deg)')
        .css('-moz-transform', 'rotate(' + angle + 'deg)')
        .css('-ms-transform', 'rotate(' + angle + 'deg)');
    angle++; angle++; angle++;
}, 20);
$(document).ajaxStart(function () {

    $("#LoadingDiv").show();
//    var Wheight = $(window).height() / 2 - $(".Rotator").height() / 2 + "px";
//    $(".Rotator").slideDown(1000).animate({
//        marginTop: Wheight,
//    }, 500, function () {
//    });
});
$(document).ajaxStop(function () {
    //var Wheight = $(window).height() / 2 - $(".Rotator").height() / 2 + "px";
    //$(".Rotator").slideDown(1000).animate({
    //    marginTop: Wheight,
    //}, 500, function () {
    //});
    $("#LoadingDiv").delay(50).hide(0);
});
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
function ShowApprovals() {
    var urlCheckURL = ConfigurationData.GlobalApiURL+"/Manager/CheckUserIsManager?ID=" + LoggedUserData.GlobalUserID;
    Common.Ajax('post', urlCheckURL, null, 'json', SucessShowTabs, false);
}
function SucessShowTabs(result) {
    if (result.Data.ShowApproval === false) {
        //document.getElementById("ApprovalTab").style.display = "none";
    }
    else {
        //  document.getElementById("ApprovalTab").style.display = "visible";
    }
    /*if (result.Data.SowhConfigrations === false) {
        document.getElementById("ConfigurationTab").style.display = "none";

    }
    else {
        document.getElementById("ConfigurationTab").style.display = "visible";

    }*/
    /*if (result.Data.SowhHRReport === false) {
        document.getElementById("HRReportTab").style.display = "none";

    }*/
    /*if (result.Data.SowhHRReport === true) {
        document.getElementById("HRReportTab").style.display = "visible";
        var icon1 = document.getElementById("hr1");
        var icon2 = document.getElementById("hr2");
        var icon3 = document.getElementById("hr3");
        //var icon4 = document.getElementById("hr4");
        icon1.className += " ConfgIcon-disabled";
        icon2.className += " ConfgIcon-disabled";
        icon3.className += " ConfgIcon-disabled";
        //icon4.className += " ConfgIcon-disabled";

    }*/

    

}
function CheckUserDataStatus()
{
    if (LoggedUserData.GlobalHasCompletedUserProfileData === "False")
    {
        ShowUserDataModal();
    }
}
function ShowUserDataModal()
{
    $('#CompleteUserDataModal').modal({ backdrop: 'static', keyboard: false });

   
}
function SubmitUserModalData()
{
    var urlSubmitUpdatedUserData = ConfigurationData.GlobalApiURL + "/Users/EditAdditionalUserData";
    if ($("#PersonalEmail").val() === "")
    {
        ShowALert(4, "Personal Email Is Required");
    }
    else if (IsEmail($("#PersonalEmail").val()) === false)
    {
       
        ShowALert(4, "Please Enter a Vaild Email");
    }
    else if ($("#HomeAddress").val() === "")
    {
        ShowALert(4, "Home Address Is Required");
    }
    //else if ($("#SeniorityYears").val() === "")
    //{
    //    ShowALert(4, "Seniority Years Is Required");
    //}
    //else if ($("#SeniorityMonths").val() === "")
    //{
    //    ShowALert(4, "Seniority Month Is Required");
    //}
    else {
        var EditUserData = GetUpdatedUserData();
        Common.Ajax('Post', urlSubmitUpdatedUserData, JSON.stringify(EditUserData), 'json', SucessSubmitUpdatedUserData, false);
    }
}
function GetUpdatedUserData() {

    var UserData =
    {
        UserID: LoggedUserData.GlobalUserID,
        PersonalEmail: $("#PersonalEmail").val(),
        HomeAddress: $("#HomeAddress").val(),
        CurrentAddress: $("#CurrentAddress").val(),
        //SeniorityBeforeHireYear: $("#SeniorityYears").val(),
        //SeniorityBeforeHireMonth: $("#SeniorityMonths").val(),
        ModifiedByUserId: LoggedUserData.GlobalUserID

    };

    return UserData;
}
function SucessSubmitUpdatedUserData(Result) {

    if (Result.Success === true)
    {
        ShowALert(2, Result.Message);
        Logout();
    }
    else {
        ShowALert(4, Result.Message);
    }
}
function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
function ShowPendingRequests() {
    var PendingRequestStatusId = 3;
    var RequestDetail = {
        UserID: LoggedUserData.GlobalUserID,
        StatusID: PendingRequestStatusId,
        From: "",
        To: ""
    };

    var urlPendingRequestData = ConfigurationData.GlobalApiURL + "/Request/RequestsPendingManagerApproval";
    Common.Ajax('Post', urlPendingRequestData, JSON.stringify(RequestDetail), 'json', SucessGetPendingRequestsQuantity, false);
}
function SucessGetPendingRequestsQuantity(Result) {
    if (Result.Success === true) {
        $("#PendingRequestQuantity").html(Result.Data.length);
    }
}
function GoBack() {
    window.history.back();
}
function ActiveNavigationBarCell() {
    var path = window.location.pathname
    $(".nav a").each(function () {
        var href = $(this).attr('href').split("?")[0];
        //var href = window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + x;
            //if (path.substring(0, href.length) === href) {
             if (path ==href) {
                $(this).closest('li').addClass('nav-active'); //#fafafa
            }
              
    });
}
//function check(src) {
//    return $("<img>").attr('src', src);
//}

//$("<img>").on("error", function (e) {
//    var anonymousImg = '/img/profile.png';
//    return $("<img>").attr('src', anonymousImg);
//    //console.log(e.type, this.src)
//})
function checkImageAvailabilityLayout(src) {
    $("<img>").attr('src', src)
        .on("error", function (e) {

            $('.layoutImage').attr('src', '../../img/user_profile.jpg');
        })
        .on("load", function (e) {
            $('.layoutImage').attr('src', src);
        })
}
//$('<img>').error(function () {
   
//});

/*$(".nav-parent").click(function () {
    var ClassNamee = $(".nav-parent").attr('class');
    ClassNamee += ' nav-active';
});*/

/*function ActivateLi() {
    var Element = $('.sub_links li').attr('class');
    //alert(Element);
    if (Element === 'nav-active') {
        var UpdatedClass = $('.sub_links li ul li').attr('class');
        UpdatedClass += ' nav-active';
    }       
}*/
