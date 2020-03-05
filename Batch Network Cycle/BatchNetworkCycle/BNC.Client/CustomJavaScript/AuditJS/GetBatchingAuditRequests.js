$(document).ready(function () {
    GetBatchingRequest();
});

function GetBatchingRequest() {
    var URLBatchingRequest = "/Audit/GetBatchingAuditRequests";
    Common.Ajax("get", URLBatchingRequest, null, 'json', sucessGetBatchingRequest);
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
                {
                    "mData": null,
                    "bSortable": false,
                    "mRender": function (data) {
                        return '<a href="#" onclick="RediirectToAudit(' + data.BatchingRequestID
                            + ')">' + data.BatchingRequestID + '</a>';
                    }
                },
                { "data": "NumberOfBatchClaims" },
                { "data": "BatchSystemFK" },
                { "data": "BatchingOfficerFK" },
                { "data": "BatchingOfficerComment" }
                //{ "data": "BatchNumber" }

                

                //{
                //    data: "RequestCreateDate",//3
                //    "render": function (data) {
                //        return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                //    }
                //},
                //{ "data": "LeaveTypeName" },//8
                //{
                //  data: "RequestStartDate",//4
                //"render": function (data)
                //{
                //    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                //}

                //},
                // {
                //  data: "RequestEndDate",//5
                //"render": function (data) {
                //    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                //}

                // },
                //{ "data": "RequestDuration" },//6
                // { "data": "RequestDurationUnitName" },//7
                // { "data": "ManagerName" },//9
                //{ "data": "DeletedReasonComment" },//10
                //{
                //    "mData": null,
                //    "bSortable": false,
                //    "mRender": function (data) {
                //        //return '<a id="editOfficialHolidays"  data-value=' + data.officialHolidaysId +' class="btn btn-info btn-sm" href=/Configuration/Edit?officialHolidaysId=' + data.officialHolidaysId + '>' + 'Edit' + '</a>' ;
                //        return '<button id="showRequestReasonsForDeleted" data-value=' + data.RequestID + '  class="btn btn-info btn-sm " data-toggle="modal" data-target="#RequestsDeletedReasonsDataModal">Display</button>';

                //    }

                //}
            ]
        });
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
