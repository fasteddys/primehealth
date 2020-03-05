var Common = {
    Ajax: function (httpMethod, url, data, type, successCallBack, async, cache, traditional, ContentType, global) {
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
            global: global,
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