$(document).ready(function () {
    GetBatchingFilteration();
});

function GetBatchingFilteration() {
    var URLBatchingFilteration = "/Filteration/GetBatchingFilterationDetails";
    Common.Ajax("get", URLBatchingFilteration, null, 'json', sucessGetBatchingFilteration);
}

function sucessGetBatchingFilteration(Result) {
    $('#BatchingFilterationDetailssDataTable').DataTable().destroy();
    var $table = $('#BatchingFilterationDetailssDataTable');
    //var table = $table.dataTable({
    //});
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [

            //{
            //    "mData": null,
            //    "bSortable": false,
            //    "mRender": function (data) {
            //        return '<a href="/Batching/AddBatchingRequest?FilterationRequestDetailID=' + data.BatchingFilterationDetailID
            //            + '">' + data.BatchingFilterationDetailID + '</a>';
            //    }
            //},
            { "data": "FilterationCategoryID" },
            { "data": "FilterationRequestID" },
            { "data": "RemainingClaimsCount" },
            { "data": "TotalClaimsCount" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="Redirect(' + data.BatchingFilterationDetailID
                        + ')">Open</button>';
                }
            }
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingFilterationDetailssDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingFilterationDetailssDataTable_wrapper .dt-buttons');
    $('#BatchingFilterationDetailssDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');

}
function Redirect(ID) {
    window.location.href = '/Batching/AddBatchingRequest?FilterationRequestDetailID=' + ID;
}

