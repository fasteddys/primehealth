var myParam;
$(document).ready(function () {
    myParam = location.search.split('ID=')[1];

    $('#btnConfirmClosingRequest').click(function () {
        var RequestData = {
            BatchClosingRequestID: myParam,
            ConfirmReceivingComment: $('#ClosingConfirmComment').val()
        };

        var urlConfirmRecievingByOfficer = '/Closing/ConfirmBatchClosingRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessConfirmRecievingByOfficer);
    });
});

function SucessConfirmRecievingByOfficer(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        window.location.href = '/Closing/ViewClosingTeamRequest?status=' + Common.Status.AssignToClosing;
    }      
    else
        ShowALert(4, Result.Message);
}