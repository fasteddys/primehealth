var myParam;
$(document).ready(function () {
    myParam = location.search.split('ID=')[1];

    $('#btnConfirmClosingRequest').click(function () {
        var RequestData = {
            BatchClosingReAdministrationRequestID: myParam,
            ConfirmedReceivingComment: $('#ClosingConfirmComment').val()
        };

        var urlConfirmRecievingByOfficer = '/ReAdministration/ConfirmBatchClosingReAdministrationRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessConfirmRecievingByOfficer);
    });
});

function SucessConfirmRecievingByOfficer(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        window.location.href = '/ReAdministration/ViewClosingReAdministrationTeamRequest?status=' + Common.Status.ReAdministrationAssign;
    }        
    else
        ShowALert(4, Result.Message);
}