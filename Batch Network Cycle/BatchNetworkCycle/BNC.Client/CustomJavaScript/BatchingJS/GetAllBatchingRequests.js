$(document).ready(function () {
    GetStatusForEntity();
    $("#btnGetAllRequest").click(function () {
        GetBatchingRequest();
    });
});

function GetStatusForEntity() {
    var urlGetStatusForEntity = '/Entities/GetStatusOfEntity?EntityID=' + Common.Entites.BatchRequest;
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

function GetBatchingRequest() {
    var Search = {
        FieldsNames: 'BatchingRequestID,NumberOfBatchClaims,BatchNumber,BatchSystemFK,CreationDate',
        StartDate: $('#RequestsFrom').val(),
        EndDate: $('#RequestsTo').val(),
        TableID: Common.Entites.BatchRequest,
        StatusID: $('#RequestsStatus').val()
    };

    if (Search.StartDate == '' || Search.EndDate == '' || Search.StatusID == '')
        ShowALert(1, 'Please Enter Required Data');
    else {
        var URLBatchingRequest = "/Batching/GetBatchingRequestsByStatus";
        Common.Ajax("POST", URLBatchingRequest, JSON.stringify(Search), 'json', SucessGetBatchingRequest);
    }
}
function SucessGetBatchingRequest(Result) {
    $('#BatchingRequestDatatable').DataTable().destroy();
    var $table = $('#BatchingRequestDatatable');
    //var table = $table.dataTable({
    //});
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "BatchingRequestID" },
            { "data": "BatchSystemName" },
            { "data": "BatchNumber" },
            { "data": "NumberOfBatchClaims" },
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
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#RecievingRequestDatabale_wrapper');
    $table.DataTable().buttons().container().prependTo('#RecievingRequestDatabale_wrapper .dt-buttons');
    $('#RecievingRequestDatabale_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');

}
function Redirect(ID) {
    window.location.href = '/Batching/ViewBatchingRequest?BatchID=' + ID;
}