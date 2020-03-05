//<reference path="../chooselanguage/loadlanguage.js" />

// app ready function

$(document).load(function () {
    // reload the page if the type of navigation is backbutton 
    
    if (localStorage.getItem('LanguageID') == 1) {
        $('head').append('<link href="styles/ArStyle.css" rel="stylesheet" />');
    }
});

$(document).ready(function () {
    // reload the page if the type of navigation is backbutton 
    //if (performance.navigation.type == 2) {
    //    location.reload(true);
    //}


    Layout.AppendLayout();
    NotificationPopup.GetNotifications();
    if (localStorage.getItem('LanguageID') == 1) {
        $('head').append('<link href="styles/ArStyle.css" rel="stylesheet" />');
    }
});


var Common = {

    Ajax: function (httpMethod, url, data, type, successCallBack, async, cache, traditional, ContentType) {
        if (typeof async === "undefined") {
            async = true;
        }
        if (typeof cache === "undefined") {
            cache = false;
        }
        if (typeof traditional === "undefined") {
            traditional = true;
        }
        //if (httpMethod === "post") {
        //    ContentType = 'application/json'
        //}


        var ajaxObj = $.ajax({
            type: httpMethod.toUpperCase(),
            url: url,
            data: data,
            dataType: type,
            async: async,
            cache: cache,
            crossDomain: true,
            contentType: 'application/json',
            processData: false,
            success: successCallBack,
            error: function (err, type, httpStatus) {
                Common.AjaxFailureCallback(err, type, httpStatus);
            }
        });

        return ajaxObj;
    },

    DisplaySuccess: function (message) {
        Common.ShowSuccessSavedMessage(message);
    },

    DisplayError: function (error) {
        Common.ShowFailSavedMessage(message);
    },

    AjaxFailureCallback: function (err, type, httpStatus) {
        var failureMessage = 'Error occurred in ajax call' + err.status + " - " + err.responseText + " - " + httpStatus;
        console.log(failureMessage);
        alert(failureMessage);
    },

    ShowSuccessSavedMessage: function (messageText) {

        //use jquery BlockUI library to display message

        //   $.blockUI({ message: messageText });
        // setTimeout($.unblockUI, 1500);
    },

    ShowFailSavedMessage: function (messageText) {

        //use jquery BlockUI library to display message

        // $.blockUI({ message: messageText });
        // setTimeout($.unblockUI, 1500);
    },
    GetALLCountries: function () {

        return JSON.parse(localStorage.getItem("ListOfCountries"))
    }
};

var ShowModal = {
    show: function (ModalId) {
        var Selector = "#" + ModalId;
        $(Selector).addClass('active-modal-menu');
        $('.modal-menu-background').addClass('active-modal-menu-background');
        //window.onpopstate = function () {
        //    ShowModal.show(ModalId);
        //};
        //$('.open-modal-menu, .close-modal-menu, .modal-menu-background').click(function () {
        //    ShowModal.hide(ModalId);
        //});
    },
    hide: function (ModalId) {
        var Selector = "#" + ModalId;
        $(Selector).removeClass('active-modal-menu');
        $('.modal-menu-background').removeClass('active-modal-menu-background');
    },
    AttachModelBackEvent: function (ModalId) {
        ShowModal.hide(ModalId);
    }
};

var ValidateRequirdData = {
    IsValid: function () {

        var ClassName = "IsRequirdField";
        var Selector = "." + ClassName;
        var Inputs = $(Selector);
        for (var i = 0; i < Inputs.length; i++) {
            if (Inputs[i].value == "") {
                return false;
            }
        }

        return true;

    }
};

var GlobalResourses = {
    BaseURL: "http://196.218.187.228:8090/api/"
    //BaseURL: "http://localhost/EmaTours.API/Api/"
    //BaseURL: "http://localhost:25389/API/"
};

var NotificationPopup = {
    PopupId: "NotificationPopup",
    NotificationPadge: ".NotificationPadge",
    NotificationIcon: "#NotificationIcon  ",
    Show: function () {
        ShowModal.show(NotificationPopup.PopupId);
    },
    GetNotifications: function () {
        if ((JSON.parse(localStorage.getItem("UserData")) != null
            // JSON.parse(localStorage.getItem("CurrentVisit")).VisitID) != null
        )) {
            var UserNotification = {
                UserID: JSON.parse(localStorage.getItem("UserData")).ClientID
                // VisitID: JSON.parse(localStorage.getItem("CurrentVisit")).VisitID

            };


            var urlGetAllCountries = GlobalResourses.BaseURL + "Notification/GetClientNotifications";
            Common.Ajax('Post', urlGetAllCountries, JSON.stringify(UserNotification), 'json', NotificationPopup.CheckNotifications, false);
        }

        //NotificationPopup.CheckNotifications(Data);
    },
    GetMore: function () {
        // Redirect to all notification page (optional)     
    },
    CheckNotifications: function (Result) {
        var selector = NotificationPopup.NotificationIcon + " span";
        if (Result.Data.length == 0) {
            $(selector).text("0");
            $(NotificationPopup.NotificationIcon).prop("onclick", null).off("click");
            $(selector).removeClass(NotificationPopup.NotificationPadge);
        }
        else {
            $(selector).text(Result.Data.length);
            $(selector).addClass(NotificationPopup.NotificationPadge);
            NotificationPopup.AppendNotification(Result.Data);
        }
    },
    AppendNotification: function (Data) {
        var Selector = "#" + NotificationPopup.PopupId;
        NotifString = "";
        for (var i = 0; i < Data.length; i++) {
            // html fo notifcation icon
            NotifString += '<a class="menu-item no-pointer-events all-pointer-events" href="menu-sidebar-dual.html">' +
                '<i class="fa fa-home"></i><span style="padding-left:10%">' + Data[i].NotificationBody + '</span>' +
                '</a>';
        }
        $(Selector).append(NotifString);
    }

};

var ReloadPage = {
    Reload: function () {
        location.reload();
    }
};

var Layout = {
    NotificationHTML: '<div id="NotificationPopup" class="modal-menu modal-menu-dark">' +
    '<div style="margin:5%">' +
    '<div class="ContentDiv" id="Notifications">' +
    '</div>' +
    '</div >' +
    '</div >',

    //ExitHTML: '<div id="ExitPopup" class="modal-menu modal-menu-dark">' +
    //'<div style="margin:10%">' +
    //'<div class="HTML_ExitAppText" >' +
    //'</div>' +
    //'<a class="button button-ghost button-green button-center HTML_btn_Confirm"  onclick="ExitApp()" style="color:white !important ; height:5% !important">Ok</a>' +
    //'<a class="button button-ghost button-green button-center HTML_btn_Cancel"  onclick="ShowModal.hide("ExitPopup")" style="color:white !important ; height:5% !important">Cancel</a>' +
    //'</div >' +
    //'</div >',

    AlertHTML: '<div id="AlertPopup" class="modal-menu modal-menu-dark">' +
    '<div style="margin:10%">' +
    '<div class="" id="AlertText">' +
    '</div>' +
    '<button class="button button-ghost button-green button-center HTML_btn_Submit" id="OKButton"  onclick=' + 'ShowModal.hide("AlertPopup")' +'  style="color:white !important ; height:5% !important">OK</button>' +
    '</div >' +
    '</div >',
    LayoutDiv: "#LayoutDiv",
    AppendLayout: function () {
        $(Layout.LayoutDiv).append(Layout.NotificationHTML + Layout.AlertHTML /*+ Layout.ExitHTML*/);
    },
    RemoveLayout: function () {
        $(Layout.LayoutDiv).empty();
    }
}
var Alert = {
    DivID: "AlertPopup",
    TxtDivID: "#AlertText",
    Show: function (AlertText) {
        ShowModal.show(Alert.DivID);
        $(Alert.TxtDivID).empty();
        $(Alert.TxtDivID).append('<p>' + AlertText + '</p>');
    }

}
var Loader = {
    Show: function () {
        $('.page-preloader').addClass('show-preloader');
        $('#page-content, .landing-page').removeClass('show-containers');
    },
    hide: function () {
        $('.page-preloader').removeClass('show-preloader');
        $('#page-content, .landing-page').addClass('fadeIn show-containers');
    }
}

function CloseSideMenu() { //
    $('.material-menu').removeClass('opacity-out');
    $('.sidebar-left, .sidebar-right').removeClass('active-sidebar-box');
    $('.sidebar-tap-close').removeClass('active-tap-close');
    $("#page-content, .header, .footer-menu").css({
        "transform": "translateX(0px)",
        "-webkit-transform": "translateX(0px)",
        "-moz-transform": "translateX(0px)",
        "-o-transform": "translateX(0px)",
        "-ms-transform": "translateX(0px)"
    });
    return false;
}

// not used
//var disableBackButton = {
//    Disable: function () {
//        window.history.pushState(null, "", window.location.href);
//        //window.onpopstate = function () {
//        //    if (confirm("Are you sure you want to exit")) {
//        //        //close();
//        //        //navigator.app.exitApp();
//        //    }
//        //    else {

//        //        window.history.pushState(null, "", window.location.href);
//        //    }
//        //};
//    }
//};

//var BackButtonHandler = {
//    Types: {
//        Default: 1,
//        CloseModal: 2,
//        Exit: 3,
//        Reload: 4
//    },
//    Attach: function (Type) {      
//            if (Type == 3) {
//            window.history.pushState(null, "", window.location.href);
//            //window.onpopstate = function () {
//                if (confirm("Are you sure you want to exit")) {
//                    close();
//                    navigator.app.exitApp();
//                }
//            //    else {

//            //        window.history.pushState(null, "", window.location.href);
//            //    }
//            }

//        //case 4: {
//        //    window.history.pushState(null, "", window.location.href);
//        //    window.onpopstate = function () {
//        //        location.reload();
//        //    }
//        //}
//    }
//}


//function ExitApp() {
//        //throw new Error('Exit');
//        alert("Exit");
//navigator.app.exitApp();
//navigator.app.exitApp();

//    //point

//}

