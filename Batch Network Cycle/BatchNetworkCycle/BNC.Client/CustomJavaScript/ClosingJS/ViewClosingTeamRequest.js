var LoggedUserID;
var myParam;
$(document).ready(function () {
    myParam = location.search.split('Status=')[1];
    GetBatchClosingRequests();
});

function GetBatchClosingRequests() {
    var SearchCriteria = {
        TableID: Common.Entites.BatchClosingRequest,
        FieldsNames: 'BatchClosingRequestID,DataEntryAdminstrationRequestFK,BatchClosingStatusFK,ConfirmReceivingTime,FinishedReviewingTime',
        UserID: null,
        StatusID: myParam
    };
    var URLGetMyBatchClosingRequests = "/Closing/GetMyBatchClosingRequests";
    Common.Ajax("POST", URLGetMyBatchClosingRequests, JSON.stringify(SearchCriteria), 'json', SucessGetMyBatchClosingRequests);
}

function SucessGetMyBatchClosingRequests(Result) {
    $('#MyBatchClosingRequests').DataTable().destroy();
    var $table = $('#MyBatchClosingRequests');
    //var table = $table.dataTable({
    //});
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "BatchClosingRequestID" },
            { "data": "BatchClosingStatusName" },
            { "data": "TotalClaimsCount" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    //if (data.ConfirmReceivingTime === null)
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="RedirectToAction(' + data.BatchClosingRequestID
                            + ')">Open</button>';
                    //else if (data.FinishedReviewingTime === null)
                    //    return '<button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="RedirectToFinish(' + data.BatchClosingRequestID
                    //        + ')">Open</button>';
                }
            }
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
    $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');
}

function RedirectToAction(ID) {
    window.location.href = '/Closing/ClosingTeamRequestActions?ID=' + ID;
}

//function RedirectToFinish(ID) {
//    window.location.href = '/Closing/FinishClosingTeamRequest?ID=' + ID;
//}