﻿$(document).ready(function () {
    var urlgetAllUnderPatchingProcessRequests = "/Batching/getAllUnderPatchingProcessRequestsAtFilitrationBatchingData";
    Common.Ajax("get", urlgetAllUnderPatchingProcessRequests, null, 'json', sucessgetAllUnderPatchingProcessRequests);
});
/*-----------------------------------------------------------*/
function sucessgetAllUnderPatchingProcessRequests(Result) {
    if (Result.Success) {
        displayFilitrationBatchingRequests(Result.Data);
    }
    else {

    }
}
function displayFilitrationBatchingRequests(Res) {
    $('#BatchingFilterationRequestsDataTable').DataTable().destroy();
    var $table = $('#BatchingFilterationRequestsDataTable');
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Res,
        "columns": [
            { "data": "BatchingFilterationDetailID" },
            { "data": "TotalClaimsCount" },
            { "data": "RemainingClaimsCount" },
            { "data": "CategoryName" },
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
                    return '<a href="/Batching/AddBatchingRequest?FilterationRequestDetailID=' + data.BatchingFilterationDetailID + '" class="btn btn-danger">View</a>';

                }
            },
        ]
    });
    //show
}//3