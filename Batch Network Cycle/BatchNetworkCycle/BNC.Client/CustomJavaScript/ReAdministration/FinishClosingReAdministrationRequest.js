var myParam;
$(document).ready(function () {
    myParam = location.search.split('ID=')[1];
    //GetDataEntryBatchAssigningRequest();

    $('#btnFinishClosingRequest').click(function () {
        var RequestData = {
            BatchClosingReAdministrationRequestID: myParam,
            FinalClosureComment: $('#ClosingFinishComment').val()
            //ArchivingSystemTicketNo: $('#ArchivingSystemTicketNo').val()
        };

        var urlConfirmRecievingByOfficer = '/ReAdministration/FinishBatchClosingReAdministrationRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessConfirmRecievingByOfficer);
    });

    $('#AlertSuccessButton').click(function () {
        window.location.href = '/ReAdministration/ViewClosingReAdministrationTeamRequest?status=' + Common.Status.ReAdministrationConfirmReceving;
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
        //window.location.href = '/ReAdministration/ViewClosingReAdministrationTeamRequest?status=' + Common.Status.ReAdministrationConfirmReceving;
    }
    else
        ShowALert(4, Result.Message);
}