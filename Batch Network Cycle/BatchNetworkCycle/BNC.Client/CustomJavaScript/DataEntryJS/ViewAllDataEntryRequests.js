var LoggedUserID;
//$(function () {
//    $.connection.hub.logging = true; 
//    // Create a proxy to signalr hub on web server. It reference the hub.
//    var notificationFromHub = $.connection.appHub;
//    alert('Hi');

//    // Connect to signalr hub
//    $.connection.hub.start().done(function () {
//        //notificationFromHub.server.show();
//        alert('Hi1');
//        GetDataEntryAdminstrationRequests();
//    });

//    // Notify to client with the recent updates
//    notificationFromHub.client.displayRows = function () {
//        alert('Hi2');
//        GetDataEntryAdminstrationRequests();
//    };
//});

$(document).ready(function () {
    GlobalUserID = 5;//$('#GlobalUserID').val();
    GetStatusForEntity();
    $("#btnGetAllRequest").click(function () {
        GetDataEntryAdminstrationRequests();
    });

    //$(function () {
    //    // Proxy created on the fly
    //    var cus = $.connection.appHub;
    //    // Declare a function on the job hub so the server can invoke it
    //    cus.client.displayRows = function () {
    //        GetDataEntryAdminstrationRequests();
    //    };
    //    // Start the connection
    //    $.connection.hub.start();
    //    GetDataEntryAdminstrationRequests();
    //});
});

function GetStatusForEntity() {
    var urlGetStatusForEntity = '/Entities/GetStatusOfEntity?EntityID=' + Common.Entites.DataEntryBatchAssigningRequest;
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

function GetDataEntryAdminstrationRequests() {
    var Search = {
        FieldsNames: 'DataEntryBatchAssigningRequestID,DataEntryBatchAssigningStatusFK,DataEntryAdministrationRequestFK,AssignedClaimsNumber,ConfirmRecievingByOfficerTime,DataEntryOfficerFinishedOnSystemTime',
        StartDate: $('#RequestsFrom').val(),
        EndDate: $('#RequestsTo').val(),
        TableID: Common.Entites.DataEntryBatchAssigningRequest,
        StatusID: $('#RequestsStatus').val()
    };
    if (Search.StartDate == '' || Search.EndDate == '' || Search.StatusID == '')
        ShowALert(1, 'Please Enter Required Data');
    else {
        var URLGetDataEntryRequests = "/DataEntry/GetDataEntryBatchAssigningRequest";
        Common.Ajax("post", URLGetDataEntryRequests, JSON.stringify(Search), 'json', SucessGetDataEntryRequests);
    }
}

function SucessGetDataEntryRequests(Result) {
    $('#DataEntryRequests').DataTable().destroy();
    var $table = $('#DataEntryRequests');

    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "DataEntryBatchAssigningRequestID" },
            { "data": "DataEntryAdministrationRequestFK" },
            { "data": "DataEntryBatchAssigningStatusName" },
            { "data": "AssignedClaimsNumber" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="RedirectToAssign(' + data.DataEntryBatchAssigningRequestID
                        + ')">Open</button>';
                }
            }
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
    $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');
}

function RedirectToAssign(ID) {
    window.location.href = '/DataEntry/DataEntryActions?ID=' + ID;
}