var LoggedUserID;
$(document).ready(function () {
    GetStatusForEntity();

    $("#btnGetAllRequest").click(function () {
        GetAllClosingTeamRequest();
    });
});

function GetStatusForEntity() {
    var urlGetStatusForEntity = '/Entities/GetStatusOfEntity?EntityID=' + Common.Entites.ClosedBatchReAdministrationRequest;
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
        FieldsNames: 'ClosedBatchReAdmissionRequestID,ReAdministrationStatusFK,BatchClosingRequestFK,ConfirmedReceivingOn,FinalClosureTime',
        StartDate: $('#RequestsFrom').val(),
        EndDate: $('#RequestsTo').val(),
        TableID: Common.Entites.ClosedBatchReAdministrationRequest,
        StatusID: $('#RequestsStatus').val()
    };
    if (Search.StartDate == '' || Search.EndDate == '' || Search.StatusID == '')
        ShowALert(1, 'Please Enter Required Data');
    else {
        var URLGetAllClosingReAdministrationRequest = "/ReAdministration/GetMyBatchClosingReAdministrationRequests";
        Common.Ajax("POST", URLGetAllClosingReAdministrationRequest, JSON.stringify(Search), 'json', SucessGetAllClosingReAdministrationRequest);
    }
}

function SucessGetAllClosingReAdministrationRequest(Result) {
    $('#AllBatchReAdministrationClosingRequests').DataTable().destroy();
    var $table = $('#AllBatchReAdministrationClosingRequests');
    //var table = $table.dataTable({
    //});
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "BatchClosingReAdministrationRequestID" },
            { "data": "ReAdministrationStatusName" },
            { "data": "TotalClaimsCount" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="RedirectToAssign(' + data.BatchClosingReAdministrationRequestID
                        + ')">Open</button>';
                }
            }
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
    $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');
}

function RedirectToAssign(ID) {
    var Request = {
        //DataEntryAdminComment: 
    };
    window.location.href = '/ReAdministration/ClosingReAdministrationTeamRequestActions?ID=' + ID;//AddDataEntryRequest?ID=' + ID;
}