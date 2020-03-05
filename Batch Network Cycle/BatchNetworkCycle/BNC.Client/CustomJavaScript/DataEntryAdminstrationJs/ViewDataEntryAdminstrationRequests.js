var Status;
$(document).ready(function () {
    Status = location.search.split('Status=')[1];
    GetDataEntryAdminstrationRequests();
});

function GetDataEntryAdminstrationRequests() {
    var SearchCriteria = {
        TableID: Common.Entites.DataEntryAdminstrationRequest,
        FieldsNames: 'DataEntryAdminstrationRequestID,BatchRequestFK,DataEntryAdministrationStatusFK,OriginalApprovedClaimsNumber,RemainingUnassignedNumberOfClaims',
        UserID: null/*Common.GetUserData().UserID*/,
        StatusID: Status
    };
    var URLGetDataEntryRequests = "/DataEntryAdminstration/GetMyDataEntryAdminstrationRequests";
    Common.Ajax("POST", URLGetDataEntryRequests, JSON.stringify(SearchCriteria), 'json', SucessGetDataEntryRequests);
}

function SucessGetDataEntryRequests(Result) {
    $('#DataEntryAdminstrationRequests').DataTable().destroy();
    var $table = $('#DataEntryAdminstrationRequests');
    //var table = $table.dataTable({
    //});
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "DataEntryAdminstrationRequestID" },
            { "data": "BatchRequestFK" },
            //{ "data": "DataEntryAdministrationStatusName" },
            { "data": "RemainingUnassignedNumberOfClaims" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="RedirectToAssign(' + data.DataEntryAdminstrationRequestID
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
    //if (Status == Common.Status.PendingTransferToClosingTeam)
    //    window.location.href = '/DataEntryAdminstration/DataEntryAdminstrationRequestTransfer?ID=' + ID;
    //else if (Status == Common.Status.DataEntryAdminAssign)
        window.location.href = '/DataEntry/AddDataEntryRequest?ID=' + ID;
}