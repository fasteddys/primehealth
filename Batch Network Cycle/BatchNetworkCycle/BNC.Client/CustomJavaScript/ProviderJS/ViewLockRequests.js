$(document).ready(function() {
    GetLockRequests();
});

function GetLockRequests() {
    var URLGetLockRequest = "/Provider/GetLockRequests";
    Common.Ajax("GET", URLGetLockRequest, null, 'json', SucessGetLockRequests);
}

function SucessGetLockRequests(Result) {
    $('#LockRequests').DataTable().destroy();
    var $table = $('#LockRequests');
    //var table = $table.dataTable({
    //});
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "RequestID" },
            { "data": "UserName" },
            { "data": "EntityName" },
            { "data": "Action"}
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
    $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');
}