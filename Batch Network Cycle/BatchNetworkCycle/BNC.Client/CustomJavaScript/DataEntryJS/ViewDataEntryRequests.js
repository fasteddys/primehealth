var Status;
$(document).ready(function () {
    Status = location.search.split('Status=')[1];
    GetDataEntryBatchRequest();
});

function GetDataEntryBatchRequest() {
    //var URLGetDataEntryRequests = "/DataEntry/GetDataEntryBatchAssigningRequest?UserID=" + LoggedUserID;
    //Common.Ajax("GET", URLGetDataEntryRequests, null, 'json', SucessGetDataEntryRequests);

    var SearchCriteria = {
        TableID: Common.Entites.DataEntryBatchAssigningRequest,
        FieldsNames: 'DataEntryBatchAssigningRequestID,DataEntryAdministrationRequestFK,DataEntryBatchAssigningStatusFK,AssignedClaimsNumber,ConfirmRecievingByOfficerTime,DataEntryOfficerFinishedOnSystemTime',
        UserID: null,
        StatusID: Status
    };
    var URLGetDataEntryRequests = "/DataEntry/GetMyDataEntryBatchAssigningRequest";
    Common.Ajax("POST", URLGetDataEntryRequests, JSON.stringify(SearchCriteria), 'json', SucessGetDataEntryRequests);
}

function SucessGetDataEntryRequests(Result) {
    $('#DataEntryRequests').DataTable().destroy();
    var $table = $('#DataEntryRequests');
    //var table = $table.dataTable({
    //});
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [            
            { "data": "DataEntryBatchAssigningRequestID" },
            { "data": "DataEntryAdministrationRequestFK" },
            //{ "data": "DataEntryBatchAssigningStatusName" },
            { "data": "AssignedClaimsNumber" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    //if (data.ConfirmRecievingByOfficerTime === null)
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="RedirectToActions(' + data.DataEntryBatchAssigningRequestID
                            + ')">Open</button>';
                    //else if (data.DataEntryOfficerFinishedOnSystemTime === null)
                    //    return '<button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="RedirectToFinish(' + data.DataEntryBatchAssigningRequestID
                    //        + ')">Finish</button>';
                }
            }
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
    $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');
}

function RedirectToActions(ID) {
    window.location.href = '/DataEntry/DataEntryActions?ID=' + ID;
}

//function RedirectToFinish(ID) {
//    window.location.href = '/DataEntry/DataEntryFinish?ID=' + ID;
//}