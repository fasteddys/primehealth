var RequestStatus;
var btnCheckedForDelete = false;
var EnableCancel = false;

$(document).ready(function () {
    $("#RequestHandlerTable").hide();
    $("#RequestHandlerHoursTable").hide();
    $("#ShowDeletionModel").attr("style","visibility:hidden");
    $("#Cancelbtn").attr("style", "visibility:hidden");

    GetRequestDetails();
    GETDetails();
    ViewRequstAttachment();
    if (LoggedUserData.IsHR == 'True' && RequestStatus == 'Approved') {
       // $("#ShowDeletionModel").show();
        $("#ShowDeletionModel").attr("style", "visibility:visible");

        GETRequestHandler();
    }
    CheckEnableCancel();
    if (EnableCancel) {
        $("#Cancelbtn").attr("style", "visibility:visible");

      //  $("#Cancelbtn").show();
        

    }
    $(".CancelRequestForDeleted").on("click", function () {
        btnCheckedForDelete = false;
        $("#ShowDeleteDaysAlert").attr("disabled", true);
        $("#ShowDeleteHoursAlert").attr("disabled", true);

        $("#RequestHandlerTable .deletedBut").removeClass("btn-secondary").addClass("btn-primery");
        $("#RequestHandlerHoursTable .deletedBut").removeClass("btn-secondary").addClass("btn-primery");
        $("#HoursDeletionReason,#DaysDeletionReason").val("");
        var InputCheckBoxDeleteList = $(".InputCheckBoxDelete");
        for (var i = 0; i < InputCheckBoxDeleteList.length; i++) {
            InputCheckBoxDeleteList[i].checked = true;
        }
        //$(".InputCheckBoxDelete").checked = true;
    });
});

function GetRequestDetails() {
    //var urlGetRequestDetails = ConfigurationData.GlobalApiURL+"/Request/ViewRequestsDetails";

    var urlGetRequestDetails = ConfigurationData.GlobalApiURL+"/Request/ViewRequestsDetails?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', urlGetRequestDetails, null, 'json', SucessGetRequestDetails, false);
}

function CheckEnableCancel()
{

    var urlCheckCancelEnable = ConfigurationData.GlobalApiURL + "/Request/CheckCancelEnable?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', urlCheckCancelEnable, null, 'json', SucessCheckCancelEnable, false);
}
function SucessCheckCancelEnable(Result) {
    if (Result.Data == true) {
        EnableCancel = true;
    }
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
    RequestStatus = Details.Data.RequestStatus;

    
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
function GETDetails() {

    var RequestsDetailsURL = ConfigurationData.GlobalApiURL + "/Request/GetAllRequestDetails?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', RequestsDetailsURL, null, 'json', SucessGetDetails, false);
}
function SucessGetDetails(result) {

    for (var i = 0; i < result.Data.length; i++) {
        if (result.Data[i].DetailsCreator == null)
        { result.Data[i].DetailsCreator = "System"; }
        var detail = "<tr><td>" + moment(result.Data[i].CreationDate).format('DD/MM/YYYY hh:mm:ss A') + "</td><td>" +
            result.Data[i].RequestDetailsComment + "</td><td>" + result.Data[i].DetailsCreator + "</td></tr>";
        $("#RequestDetails").append(detail);

    }

}
$("#PrintBtn").click(function () {

    var RequestPrintURL = ConfigurationData.GlobalApiURL + "/Request/Report?RequestID=" + getParameterByName('RequestID');
    //window.open(RequestPrintURL, 'window name', 'window settings');
    var win = window.open(RequestPrintURL, '_self');
    // win.focus();

});
function SuccessPrint() {

}
function ViewRequstAttachment() {
    var urlGetAllAttachment = ConfigurationData.GlobalApiURL + "/Request/GetALLRequestAttachment?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', urlGetAllAttachment, null, 'json', SucessGetAllAttachment, false);
}
function SucessGetAllAttachment(Result) {
    if (Result.Data.length < 1) {
        $("#RequestAttachment").hide();
    } else {
        for (var i = 0; i < Result.Data.length; i++) {
            //   var path = "'"+Result.Data[i].RequestAttachmentPath+"'";
            //path = path.concat("'", path, "'");

            var DownloadLink = ' &nbsp;<a href="/Request/DownloadFile?FilePath=' + Result.Data[i].RequestAttachmentPath + '" download>' + Result.Data[i].RequestAttachmentName + '</a>&nbsp;';
            $("#RequestAttachment").append(DownloadLink);
        }
    }
}


function GETRequestHandler() {
    var RequestsDetailsURL = ConfigurationData.GlobalApiURL + "/Request/GetRequestHandlerRecords?RequestID=" + getParameterByName('RequestID');
    Common.Ajax('get', RequestsDetailsURL, null, 'json', SucessGETRequestHandler, false);
}
function SucessGETRequestHandler(result) {
    for (var i = 0; i < result.Data.length; i++) {

        if (result.Data[i].DurationType === "Days") {
            $("#RequestHandlerTableBody").append(
                "<tr><td>" +
                moment(result.Data[i].Offday).format('DD/MM/YYYY')
                + "</td><td>" + result.Data[i].RequestHandlerDuration + " " + result.Data[i].DurationType +
                "</td><td><div class='form-check'><a  onclick='InputCheckBoxDeleteClick(this)' value='" + result.Data[i].RequestHandlerID +
                "' Duration='" + result.Data[i].RequestHandlerDuration + "' Offday='" + result.Data[i].Offday
                + "' class= 'InputCheckBoxDelete' href='#'><span class='fas fa-trash fa-2x btn btn-primery deletedBut'></span></a></div ></td></tr>");
            $("#RequestHandlerTable").show();

        }


        else {
            $("#RequestHandlerHoursTableBody").append(
                "<tr><td>" + 
                moment(result.Data[i].Offday).format('DD/MM/YYYY hh:mm:ss A')
                + "</td><td>" + result.Data[i].RequestHandlerDuration + " " + result.Data[i].DurationType +
                "</td><td><div class='form-check'><a  onclick='InputCheckBoxDeleteClick(this)'  value='" + result.Data[i].RequestHandlerID
                + "' Duration='" + result.Data[i].RequestHandlerDuration + "' Offday='" + result.Data[i].Offday +
                "' class= 'InputCheckBoxDelete' href='#'><span class='fas  fa-trash fa-2x btn btn-primery deletedBut'></span></a></div ></td></tr>");
            $("#RequestHandlerHoursTable").show();


        }

    }



}
$("#DeleteHandlerbtn").click(function () {

    var ListRequesthandelers = [];
    var ListOfDeletedTimes = $(".InputCheckBoxDelete");
    for (var i = 0; i < ListOfDeletedTimes.length; i++) {
        if (ListOfDeletedTimes[i].checked == false) {
            var RequesthandelerItem = {
                RequestHandlerID: ListOfDeletedTimes[i].attributes.value.value,
                RequestHandlerDuration: ListOfDeletedTimes[i].attributes.duration.value,
                Offday: ListOfDeletedTimes[i].attributes.offday.value
            };
            ListRequesthandelers.push(RequesthandelerItem);
        }

    }

    var RequesthandelerDTO = {
        RequestID: getParameterByName('RequestID'),
        DeletedDays: ListRequesthandelers,
        DeletedTimes: null,
        DaysDeletionReason: $("#DaysDeletionReason").val(),
        UserDeleterID: LoggedUserData.GlobalUserID

    };


    var DeletePartialPeriodFromRequestURL = ConfigurationData.GlobalApiURL + "/Request/DeletePartialPeriodFromRequest";
    getParameterByName('RequestID');
    Common.Ajax('Post', DeletePartialPeriodFromRequestURL, JSON.stringify(RequesthandelerDTO), 'json', SucessDeletePartialPeriodFromRequest, false);

});
$("#DeleteHandlerHoursbtn").click(function () {
    var ListRequesthandelers = [];
    var ListOfDeletedTimes = $(".InputCheckBoxDelete");
    for (var i = 0; i < ListOfDeletedTimes.length; i++) {
        if (ListOfDeletedTimes[i].checked == false) {
            var RequesthandelerItem = {
                RequestHandlerID: ListOfDeletedTimes[i].attributes.value.value,
                RequestHandlerDuration: ListOfDeletedTimes[i].attributes.duration.value,
                Offday: ListOfDeletedTimes[i].attributes.offday.value

            };
            ListRequesthandelers.push(RequesthandelerItem);
        }

    }

    var RequesthandelerDTO = {
        RequestID: getParameterByName('RequestID'),
        DeletedDays: null,
        DeletedTimes: ListRequesthandelers,
        HoursDeletionReason: $("#HoursDeletionReason").val(),
        UserDeleterID: LoggedUserData.GlobalUserID
    };


    var DeletePartialPeriodFromRequestURL = ConfigurationData.GlobalApiURL + "/Request/DeletePartialPeriodFromRequest";
    getParameterByName('RequestID');
    Common.Ajax('Post', DeletePartialPeriodFromRequestURL, JSON.stringify(RequesthandelerDTO), 'json', SucessDeletePartialPeriodFromRequest, false);


});
function InputCheckBoxDeleteClick(Btn) {
    if (Btn.checked == true || Btn.checked == null) {
        Btn.checked = false;
        Btn.children[0].className = "fas fa-trash fa-2x btn btn-secondary deletedBut";
        if ($("#HoursDeletionReason")[0].value != "") {
            $("#ShowDeleteHoursAlert").attr("disabled", false);

        }
        if ($("#DaysDeletionReason")[0].value != "" ) {
            $("#ShowDeleteDaysAlert").attr("disabled", false);

        }
    }
    else {
        Btn.checked = true;
        Btn.children[0].className = "fas fa-trash fa-2x btn btn-primery deletedBut";
        btnCheckedForDelete = checkIfButtonIsClickedForDeleted();
        if (btnCheckedForDelete == false)
        {
            $("#ShowDeleteHoursAlert").attr("disabled", true);
            $("#ShowDeleteDaysAlert").attr("disabled", true);
        }


    }

}
function SucessDeletePartialPeriodFromRequest(Result) {
    if (Result.Success)
    {
            location.reload();
    }
    else
    {

        ShowALert(4, Result.Message);

    }


}
$("#HoursDeletionReason").keyup(function ()
{
    btnCheckedForDelete = checkIfButtonIsClickedForDeleted();
    var x = $("#HoursDeletionReason");
    if ($("#HoursDeletionReason")[0].value != "" && btnCheckedForDelete) {
        $("#ShowDeleteHoursAlert").attr("disabled", false);
    }
    else {

            $("#ShowDeleteHoursAlert").attr("disabled", true);


    }


});
$("#DaysDeletionReason").keyup(function () {
    btnCheckedForDelete = checkIfButtonIsClickedForDeleted();
    if ($("#DaysDeletionReason")[0].value != "" && btnCheckedForDelete)
    {
        $("#ShowDeleteDaysAlert").attr("disabled", false);


    }
    else
    {
        $("#ShowDeleteDaysAlert").attr("disabled", true);

    }
    
});


//function if any button is clicked for deleted
function checkIfButtonIsClickedForDeleted() {
    var allButtonsForDeleted = $(".deletedBut");
    for (var i = 0; i < allButtonsForDeleted.length; i++) {
        var x = allButtonsForDeleted[i].className;
        if (x.includes("btn-secondary")) {
            return true;
        }
    }
    return false;
}
//------------------------------------------

$("#CancelRequestbtn").click(function () {
    var RequestCancelDTO = {
        RequestID: getParameterByName('RequestID'),
        CancelReason: $("#CancelReason").val(),
        UserDeleterID: LoggedUserData.GlobalUserID
    };
    var RequestCancelURL = ConfigurationData.GlobalApiURL + "/Request/CancelRequest";

    Common.Ajax('Post', RequestCancelURL,
        JSON.stringify(RequestCancelDTO), 'json', SucessCancelRequest, false);


});

function SucessCancelRequest(Result) {
    if (Result.Success) {
        location.reload();
    }
    else {

        ShowALert(4, Result.Message);

    }


}

$("#CancelReason").keyup(function () {
    if ($("#CancelReason")[0].value != "") {
        $("#CancelRequestbtn").attr("disabled", false);


    }
    else {
        $("#CancelRequestbtn").attr("disabled", true);

    }

});
