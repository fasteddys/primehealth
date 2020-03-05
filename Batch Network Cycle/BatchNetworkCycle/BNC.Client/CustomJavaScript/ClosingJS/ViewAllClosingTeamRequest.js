var LoggedUserID;
$(document).ready(function () {
    GetStatusForEntity();

    $("#btnGetAllRequest").click(function () {
        GetAllClosingTeamRequest();
    });
});

function GetStatusForEntity() {
    var urlGetStatusForEntity = '/Entities/GetStatusOfEntity?EntityID=' + Common.Entites.DataEntryClosingRequest;
    Common.Ajax("GET", urlGetStatusForEntity, null, 'json', SucessGetStatusForEntity);
}

function SucessGetStatusForEntity(Result) {
    $('#RequestsStatus').empty();
    if (Result.Success) {
        for (var i = 0; i < Result.Data.length; i++) {
            $('#RequestsStatus').append(new Option(Result.Data[i].StatusName, Result.Data[i].StatusID));
        }
    }
}

function GetAllClosingTeamRequest() {
    var Search = {
        FieldsNames: 'BatchClosingRequestID,BatchClosingStatusName,TotalClaimsCount,ConfirmReceivingTime,FinishedReviewingTime',
        StartDate: $('#RequestsFrom').val(),
        EndDate: $('#RequestsTo').val(),
        TableID: Common.Entites.DataEntryClosingRequest,
        StatusID: $('#RequestsStatus').val()
    };
    if (Search.StartDate == '' || Search.EndDate == '' || Search.StatusID == '')
        ShowALert(1, 'Please Enter Required Data');
    else {
        var URLGetAllClosingTeamRequest = "/Closing/GetMyBatchClosingRequests";
        Common.Ajax("POST", URLGetAllClosingTeamRequest, JSON.stringify(Search), 'json', SucessGetAllClosingTeamRequest);
    }
}

function SucessGetAllClosingTeamRequest(Result) {
    $('#AllBatchClosingRequests').DataTable().destroy();
    var $table = $('#AllBatchClosingRequests');
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
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="RedirectToAction(' + data.BatchClosingRequestID
                        + ')">Open</button>';
                }
            }
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
    $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');
}

function RedirectToAction(ID) {
    var Request = {
        //DataEntryAdminComment: 
    };
    window.location.href = '/Closing/ClosingTeamRequestActions?ID=' + ID;//AddDataEntryRequest?ID=' + ID;
}