$(document).ready(function () {
    var urlgetMyBatching = "/Batching/getMyBatchingData";
    Common.Ajax( "get", urlgetMyBatching, null, 'json', sucessgetgetMyBatching);

});
/*-----------------------------------------------------------*/
function sucessgetgetMyBatching(Result) {
    if (Result.Success) {
        displayBatchingRequests(Result.Data);
    }
    else {
        ;
    }
}
function displayBatchingRequests(Res) {
    $('#BatchingRequestsDataTable').DataTable().destroy();
    var $table = $('#BatchingRequestsDataTable');
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Res,
        "columns": [
            { "data": "BatchingRequestID" },
            { "data": "NumberOfBatchClaims" },
            { "data": "BatchSystemName" },
            { "data": "BatchNumber" },
            {
                data: "CreationDate",//3
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<a href="#" class="btn btn-info">View</a>';

                }
            },
        ]
    });
    //show
}//3