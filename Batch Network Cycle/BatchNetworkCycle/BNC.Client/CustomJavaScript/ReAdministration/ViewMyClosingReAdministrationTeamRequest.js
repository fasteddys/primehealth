var LoggedUserID;
var myParam;
$(document).ready(function () {
    myParam = location.search.split('Status=')[1];
    GetBatchClosingReAdministrationRequests();
});

function GetBatchClosingReAdministrationRequests() {
    var SearchCriteria = {
        TableID: Common.Entites.ClosedBatchReAdministrationRequest,
        FieldsNames: 'ClosedBatchReAdmissionRequestID,ReAdministrationStatusFK,BatchClosingRequestFK,ConfirmedReceivingOn,FinalClosureTime',
        UserID: Common.GetUserData().UserID,
        StatusID: myParam
    };
    var URLGetMyBatchClosingReAdministrationRequests = "/ReAdministration/GetMyBatchClosingReAdministrationRequests";
    Common.Ajax("POST", URLGetMyBatchClosingReAdministrationRequests, JSON.stringify(SearchCriteria), 'json', SucessGetMyBatchClosingReAdministrationRequests);
}

function SucessGetMyBatchClosingReAdministrationRequests(Result) {
    $('#MyBatchClosingReAdministrationRequests').DataTable().destroy();
    var $table = $('#MyBatchClosingReAdministrationRequests');
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "BatchClosingReAdministrationRequestID" },
            { "data": "ReAdministrationStatusName" },
            { "data": "TotalClaimsCount" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    //if (data.ReceivedFromClosingOn === null)
                    return '<button type="button" class="btn btn-primary" data-dismiss="modal" onclick="RedirectToActions(' + data.BatchClosingReAdministrationRequestID
                            + ')">Open</button>';
                    //else if (data.FinalClosureTime === null)
                    //    return '<button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="RedirectToFinish(' + data.BatchClosingReAdministrationRequestID
                    //        + ')">Open</button>';
                }
            }
        ]
    });
    $('<div />').addClass('dt-buttons mb-2 pb-1 text-right').prependTo('#BatchingRequestsDataTable_wrapper');
    $table.DataTable().buttons().container().prependTo('#BatchingRequestsDataTable_wrapper .dt-buttons');
    $('#BatchingRequestsDataTable_wrapper').find('.btn-secondary').removeClass('btn-secondary').addClass('btn-default');
}

function RedirectToActions(ID) {
    window.location.href = '/ReAdministration/ClosingReAdministrationTeamRequestActions?ID=' + ID;
}

//function RedirectToFinish(ID) {
//    window.location.href = '/ReAdministration/AssignClosingReAdministrationTeamRequest?ID=' + ID;
//}