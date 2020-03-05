var myParam;
$(document).ready(function () {
    myParam = location.search.split('ID=')[1];
    //GetDataEntryBatchAssigningRequest();

    $('#btnFinishClosingRequest').click(function () {
        var RequestData = {
            BatchClosingRequestID: myParam,
            FinishedReviewingComment: $('#ClosingFinishComment').val()
        };

        var urlConfirmRecievingByOfficer = '/Closing/FinishBatchClosingRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessConfirmRecievingByOfficer);
    });
});

//function GetDataEntryBatchAssigningRequest() {
//    var urlGetDataEntryBatchRequest = '/DataEntry/GetDataEntryBatchAssigningRequestByID?RequestID=' + myParam;
//    Common.Ajax("GET", urlGetDataEntryBatchRequest, null, 'json', SucessGetDataEntryBatchRequest);
//}

//function SucessGetDataEntryBatchRequest(Result) {
//    if (Result.Success === true)
//        $('#AssignedClaims').val(Result.Data.AssignedClaimsNumber);
//    else
//        ShowALert(4, Result.Message);
//}

function SucessConfirmRecievingByOfficer(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        window.location.href = '/Closing/ViewClosingTeamRequest?status=' + Common.Status.ClosingConfirmReceiving;
    }
    else
        ShowALert(4, Result.Message);
}