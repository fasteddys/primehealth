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
    var urlGetStatusForEntity = '/Entities/GetStatusOfEntity?EntityID=' + Common.Entites.DataEntryAdminstrationRequest;
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
        FieldsNames: 'DataEntryAdminstrationRequestID,BatchRequestFK,OriginalApprovedClaimsNumber',
        StartDate: $('#RequestsFrom').val(),
        EndDate: $('#RequestsTo').val(),
        TableID: Common.Entites.DataEntryAdminstrationRequest,
        StatusID: $('#RequestsStatus').val()
    };
    if (Search.StartDate == '' || Search.EndDate == '' || Search.StatusID == '')
        ShowALert(1, 'Please Enter Required Data');
    else {
        var URLGetDataEntryRequests = "/DataEntryAdminstration/GetAllDataEntryAdminstrationRequests";
        Common.Ajax("post", URLGetDataEntryRequests, JSON.stringify(Search), 'json', SucessGetDataEntryRequests);
    }   
}

function SucessGetDataEntryRequests(Result) {
    $('#DataEntryAdminstrationRequests').DataTable().destroy();
    var $table = $('#DataEntryAdminstrationRequests');

    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "DataEntryAdminstrationRequestID" },
            { "data": "BatchRequestFK" },
            { "data": "OriginalApprovedClaimsNumber" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="RedirectToAssign(' + data.DataEntryAdminstrationRequestID
                        + ')">Open</button>'; //+
                        //'<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="LockRequest(' + data.DataEntryAdminstrationRequestID
                        //+ ')">Lock</button></div>';
                }
            }
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
    $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');
}

function RedirectToAssign(ID) {
    var Request = {
        //DataEntryAdminComment: 
    };
    window.location.href = '/DataEntry/AddDataEntryRequest?ID=' + ID;//AddDataEntryRequest?ID=' + ID;
}

function LockRequest(ID) {
    var RequestData = {
        RecordID: ID,
        TableID: 8,
        UserID: Common.GetUserData().UserID,
        IsLocked: true
    };

    var URLLockRequest = "/DataEntryAdminstration/LockRequest";
    Common.Ajax("POST", URLLockRequest, JSON.stringify(RequestData), 'json', SucessLockRequest);
}

function SucessLockRequest(Result) {
    if (Result.Success) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);
    }
}