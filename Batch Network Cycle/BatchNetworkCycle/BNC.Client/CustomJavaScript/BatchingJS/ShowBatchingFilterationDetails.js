$(document).ready(function () {
    GetBatchingFilterationDetailsByStatus();
});
/*-----------------------------------------------------------*/
function GetBatchingFilterationDetailsByStatus() {

    var SearchCriteria = {
        TableID: Common.Entites.BatchingFilterationDetails,
        FieldsNames: 'BatchingFilterationDetailID,FilterationRequestFK,FilterationCategoryFK,TotalClaimsCount,RemainingClaimsCount,CreationDate',
        UserID: null,
        StatusID: Common.getParameterByName("Status")
    };

    var urlgetAllFinishedBatchingRequests = "/Batching/GetBatchingFilterationDetailsByStatus";
    Common.Ajax("Post", urlgetAllFinishedBatchingRequests, JSON.stringify(SearchCriteria), 'json', sucessgetAllFinishedBatchingRequests);

}



function sucessgetAllFinishedBatchingRequests(Res) {
    $('#BatchingFilterationRequestsDataTable').DataTable().destroy();
    var $table = $('#BatchingFilterationRequestsDataTable');
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Res.Data,
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
                    return '<a href="/Batching/AddBatchingRequest?FilterationRequestDetailID=' + data.BatchingFilterationDetailID + '" class="btn btn-success">View</a>';

                }
            },
        ]
    });
    //show
}//3
