$(document).ready(function () {
    GetBatchingRequest();
});

function GetBatchingRequest() {

    var SearchCriteria = {
        TableID: Common.Entites.BatchRequest,
        FieldsNames: 'BatchingRequestID,NumberOfBatchClaims,BatchNumber,BatchSystemFK,CreationDate',
        UserID: null,
        StatusID: Common.getParameterByName("Status")
    };

    var URLBatchingRequest = "/Batching/GetBatchingRequestsByStatus";
    Common.Ajax("post", URLBatchingRequest, JSON.stringify(SearchCriteria), 'json', displayBatchingRequests);
}

function displayBatchingRequests(Res) {
    $('#BatchingRequestsDataTable').DataTable().destroy();
    var $table = $('#BatchingRequestsDataTable');
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Res.Data,
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


        ]
    });
}
function Redirect(ID) {
    window.location.href = '/Batching/ViewBatchingRequest?BatchID=' + ID;
}
//ViewBatchingRequest