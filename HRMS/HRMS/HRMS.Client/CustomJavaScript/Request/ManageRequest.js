$(document).ready(function () {
    ShowApprovalAndRejectionBtn();
    GETRequestsDetails();
    GETDetails();
    ViewRequstAttachment();
  
});



function ShowApprovalAndRejectionBtn() {
    var CheckRequestPendingManager =
    {
        UserID: LoggedUserData.GlobalUserID,
        RequestID: getParameterByName('RequestID')
    };
    var urlCheckURL = ConfigurationData.GlobalApiURL+"/Request/CheckIfRequestPendingManagerApproval";
    Common.Ajax('post', urlCheckURL, JSON.stringify(CheckRequestPendingManager), 'json', SucessShow, false);
}
function SucessShow(result)
{
    if (result.Data === false)
        {
             //   $("#ApprovalRejectionBtns").hide();
             //   document.getElementById("ApprovalRejectionBtns").style.visibility = "hidden";

        }
    else
    {
       // $("#show")
        //$("#ApprovalRejectionBtns").show();
      //  document.getElementById("ApprovalRejectionBtns").style.display = 'none';
        document.getElementById("ApprovalRejectionBtns").style.visibility = "visible";
    }

}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

var RequestIDs = getParameterByName('RequestID');

var ApproveBTN = function () {
    var ManagerActionDTO = {
        UserID: LoggedUserData.GlobalUserID,
        RequestID: RequestIDs,
        ModifiedByUserId: LoggedUserData.GlobalUserID,


    };

    var urlApprove = ConfigurationData.GlobalApiURL + "/Request/Approve";
    Common.Ajax('post', urlApprove, JSON.stringify(ManagerActionDTO), 'json', SucessApprove, false);
};

function SucessApprove(result) {
    if (result.Success) {
        if (!result.Data) {
            
            ShowALert(2, result.Message);
            //location.reload();

                //location.reload();
            window.location.replace("/Request/ViewAllApproval");

            }
            else {
            location.reload();

            }               

    }
    else {

        ShowALert(4, result.Message);

    }


}

var RejectBTN = function () {
    var ManagerActionDTO = {
        UserID: LoggedUserData.GlobalUserID,
        RequestID: RequestIDs,
        RejectionReason: $("#RejectionReason").val(),
        ModifiedByUserId: LoggedUserData.GlobalUserID
    };
    var urlReject = ConfigurationData.GlobalApiURL + "/Request/Reject";
    Common.Ajax('post', urlReject, JSON.stringify(ManagerActionDTO), 'json', SucessReject, false);
};

function SucessReject(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        location.reload();
    }
    else {
        ShowALert(4, Result.Message);

    }

}
function GETRequestsDetails() {
    var RequestsDetailsURL = ConfigurationData.GlobalApiURL+"/Request/ViewRequestsDetails?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', RequestsDetailsURL, null, 'json', SucessRequestsDetails, false);
}
function SucessRequestsDetails(result) {
}





function GETDetails() {
    
    var RequestsDetailsURL = ConfigurationData.GlobalApiURL + "/Request/GetAllRequestDetails?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', RequestsDetailsURL, null, 'json', SucessGetDetails, false);
}
function SucessGetDetails(result) {

    for (var i = 0; i < result.Data.length; i++)
    {
        if (result.Data[i].DetailsCreator == null)
        { result.Data[i].DetailsCreator = "System"; }
        var detail = '<li><a target="_blank" href="#"' + result.Data[i].DetailsCreator + '</a>' + result.Data[i].DetailsCreator + '<a href="#" class="float-right">' + moment(result.Data[i].CreationDate).format('DD/MM/YYYY hh:mm:ss A')  + '</a><p>' + result.Data[i].RequestDetailsComment + '</p></li>'
        $("#RequestDetails").append(detail);

    }
}



$(document).ready(function () {
    GetRequestDetails();
});


function GetRequestDetails() {
    //var urlGetRequestDetails = ConfigurationData.GlobalApiURL+"/Request/ViewRequestsDetails";

    var urlGetRequestDetails = ConfigurationData.GlobalApiURL+"/Request/ViewRequestsDetails?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', urlGetRequestDetails, null, 'json', SucessGetRequestDetails, false);
}
function SucessGetRequestDetails(Details) {
    var CreationDate, DurationFrom, DurationTo, BackToWork;

    if (LeaveDurationUnitEnum.Days == Details.Data.DurationUnit) {
        DurationFrom = moment(Details.Data.DurationFrom).format('DD/MM/YYYY');
        DurationTo = moment(Details.Data.DurationTo).format('DD/MM/YYYY');
        BackToWork = moment(Details.Data.BackToWork).format('DD/MM/YYYY');
    }
    else if (LeaveDurationUnitEnum.Hours == Details.Data.DurationUnit) {
        DurationFrom = moment(Details.Data.DurationFrom).format('DD/MM/YYYY hh:mm:ss A');
        DurationTo = moment(Details.Data.DurationTo).format('DD/MM/YYYY hh:mm:ss A');
        BackToWork = moment(Details.Data.BackToWork).format('DD/MM/YYYY hh:mm:ss A');
    }

    $("#RequestID").text(getParameterByName('RequestID'));
    $("#UserName").text(Details.Data.UserName);
    $("#DepartmentName").text(Details.Data.DepartmentName);
    $("#SubDepartmentName").text(Details.Data.SubDepartmentName);
    $("#PositionName").text(Details.Data.PositionName);
    $("#LeaveTypeName").text(Details.Data.LeaveTypeName);
    $("#DurationFrom").text(DurationFrom);
    $("#DurationTo").text(DurationTo);
    $("#BackToWork").text(BackToWork);
    $("#CreationDate").text(moment(Details.Data.CreationDate).format('DD/MM/YYYY hh:mm:ss A'));
    $("#Status").text(Details.Data.RequestStatus);
    $("#RequestDuration").text(Details.Data.RequestDuration);
    $("#Reason").text(Details.Data.Reason != '' ? Details.Data.Reason : '-');
    $("#Comment").text(Details.Data.Comment != '' ? Details.Data.Comment : '-');
    $("#Substitute").text(Details.Data.SubstituteName != '' ? Details.Data.SubstituteName : '-');
    if (Details.Data.EntitlementTotal != -1)
    {
        $("#EntitlementTotal").text(Details.Data.EntitlementTotal + " " + Details.Data.DurationUnit);

    }
    else
    {
        $("#EntitlementTotal").text("Unlimited");

    }

}

var PrintBTN = function () {


    var UserName = $("#UserName").text();
    var DepartmentName = $("#DepartmentName").text();
    var SubDepartmentName = $("#SubDepartmentName").text();
    var PositionName = $("#PositionName").text();
    var LeaveTypeName = $("#LeaveTypeName").text();
    var DurationFrom = $("#DurationFrom").text();
    var DurationTo = $("#DurationTo").text();
    var BackToWork = $("#BackToWork").text();
    var RequestDuration = $("#RequestDuration").text();


    var RequestDetailsView = {
        RequestID: RequestIDs,
        UserName: UserName,
        SubDepartmentName: SubDepartmentName,
        PositionName: PositionName,
        LeaveTypeName: LeaveTypeName,
        DurationFrom: DurationFrom,
        DurationTo: DurationTo,
        BackToWork: BackToWork,
        RequestDuration: RequestDuration

    };
    var urlPrint = ConfigurationData.GlobalApiURL + "/Request/Report";
    CommonPDF.Ajax('post', urlPrint, JSON.stringify(RequestDetailsView), 'application / pdf', SucessPrint, false);
};

function SucessPrint(result) {
        alert(result);

}

var CommonPDF = {

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
    },

    ShowSuccessSavedMessage: function (messageText) {

        //use jquery BlockUI library to display message

        $.blockUI({ message: messageText });
        setTimeout($.unblockUI, 1500);
    },

    ShowFailSavedMessage: function (messageText) {

        //use jquery BlockUI library to display message

        $.blockUI({ message: messageText });
        setTimeout($.unblockUI, 1500);
    }
};

function ViewRequstAttachment() {
    


    var urlGetAllAttachment = ConfigurationData.GlobalApiURL + "/Request/GetALLRequestAttachment?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', urlGetAllAttachment, null, 'json', SucessGetAllAttachment, false);
}

function SucessGetAllAttachment(Result) {
    if (Result.Data.length < 1 )
    {
        $("#RequestAttachment").hide();
    }else {
        for (var i = 0; i < Result.Data.length; i++) {
            //   var path = "'"+Result.Data[i].RequestAttachmentPath+"'";
            //path = path.concat("'", path, "'");

            var DownloadLink = ' &nbsp;<a href="/Request/DownloadFile?FilePath=' + Result.Data[i].RequestAttachmentPath + '" download>' + Result.Data[i].RequestAttachmentName + '</a>&nbsp;';
            $("#RequestAttachment").append(DownloadLink);
        }
    }
}
