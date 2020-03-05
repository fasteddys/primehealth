$(document).ready(function () {
    GetReceivingRequest();
});

function GetReceivingRequest() {

    var SearchCriteria = {
        TableID: Common.Entites.ReceivingRequest,
        FieldsNames: 'ReceivingRequestID,ReceivingOfficerCalimsCount,ProviderFK,BillingYear,BilllingMonth,ReceivingDate',
        UserID: null,
        StatusID: Common.getParameterByName("Status")
    };

    var URLReceivingRequest = "/Receiving/GetRecievingRequestsByStatus";
    Common.Ajax("post", URLReceivingRequest, JSON.stringify(SearchCriteria), 'json', sucessGetReceivingRequest);
}
function sucessGetReceivingRequest(Result) {
    $('#RecievingRequestDatabale').DataTable().destroy();
    var $table = $('#RecievingRequestDatabale');
    //var table = $table.dataTable({
    //});
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [

            { "data": "ReceivingRequestID" },
            { "data": "ProviderName" },
            { "data": "ClaimsCount" },
            { "data": "BillingYear" },
            { "data": "BilllingMonth" },
            {
                data: "ReceivingDate",
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="Redirect(' + data.ReceivingRequestID
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
    window.location.href = '/Receiving/AddRecievingRequest?ReceivingRequestID=' + ID;
}