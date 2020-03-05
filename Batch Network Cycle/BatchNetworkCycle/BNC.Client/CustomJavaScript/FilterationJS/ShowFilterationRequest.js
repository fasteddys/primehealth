$(document).ready(function () {
    GetFilterationRequest();
});

function GetFilterationRequest() {


    var SearchCriteria = {
        TableID: Common.Entites.FilterationRequest,
        FieldsNames: 'ReceivingRequestFK',
        UserID: null,
        StatusID: Common.getParameterByName("Status")
    };


    var URLFilterationRequest = "/Filteration/GetFilterationRequestByStatus";
    Common.Ajax("post", URLFilterationRequest, JSON.stringify(SearchCriteria), 'json', sucessGetFilterationRequest);
}
function sucessGetFilterationRequest(Result) {
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
            //{
            //    "mData": null,
            //    "bSortable": false,
            //    "mRender": function (data) {
            //        return '<a href="/Filteration/CreateFilterationRequest?ReceivingRequestID=' + data.ReceivingRequestID
            //            + '">' + data.ReceivingRequestID + '</a>';
            //    }
            //},
            { "data": "ReceivingRequestID" },
            { "data": "ClaimsCount" },
            { "data": "ProviderID" },
            { "data": "BillingYear" },
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
    window.location.href = '/Filteration/CreateFilterationRequest?ReceivingRequestID=' + ID;
}