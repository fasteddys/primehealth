$(document).ready(function () {
    GetBatchingRequest();
});

function GetBatchingRequest() {
    var SearchCriteria = {
        TableID: Common.Entites.AuditRequest,
        FieldsNames: 'BatchingRequestFK,TotalNumberOfApprovedClaims',
        UserID: null,
        StatusID: Common.getParameterByName("Status")
    };

    var URLBatchingRequest = "/Audit/GetBatchingAuditRequestsByStatus";
    Common.Ajax("post", URLBatchingRequest, JSON.stringify(SearchCriteria), 'json', sucessGetBatchingRequest);
}

function sucessGetBatchingRequest(Result) {
    $('#BatchingRequestsDataTable').DataTable().destroy();
        var $table = $('#BatchingRequestsDataTable');
        //var table = $table.dataTable({
        //});
        var table =  $table.DataTable({
            sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
            buttons: ['print', 'excel', 'pdf'],
            retrieve: true,
            "data": Result.Data,
            "columns": [
                { "data": "BatchingRequestID" },
                { "data": "NumberOfBatchClaims" },
                { "data": "BatchSystemName" },
                { "data": "BatchNumber" },
                {
                    data: "CreationDate",
                    "render": function (data) {
                        return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                    }
                },
                {
                    "mData": null,
                    "bSortable": false,
                    "mRender": function (data) {
                        return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="Redirect(' + data.BatchingRequestID
                            + ')">Open</button>';
                    }
                }
            ]        });
        $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
        $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
        $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');

}

var RequestBatchID = 0;
function RediirectToAudit(BatchID) {
    var DirectBatchToAuditBageURL = "/Audit/DirectBatchToAuditBage?BatchID=" + BatchID;
    RequestBatchID = BatchID;
    Common.Ajax('get', DirectBatchToAuditBageURL, null, 'json', SucessDirectBatchToAuditBage, false);
}


function SucessDirectBatchToAuditBage(Result) {
    if (Result.Success === true) {
        window.location.href = "/" + Result.Data.AuditCategoriesControllerName + "/" +

            Result.Data.AuditCategoriesActionName + "?BatchID=" + RequestBatchID;


    }
}
function Redirect(ID) {
    window.location.href = '/ReconciliationAudit/AddReconciliationAuditRequest?BatchID=' + ID;
}
