var myParam;

$(document).ready(function () {
    myParam = location.search.split('ID=')[1];

    var urlGetBatchAuditingRequest = '/DataEntryAdminstration/GetDataEntryAdminstrationRequest?ID=' + myParam;
    Common.Ajax("GET", urlGetBatchAuditingRequest, null, 'json', SucessGetBatchAuditingRequest);


});
$('#btnSaveDataEntryAdminstrationRequest').click(function () {
    AssignDataEntryAdminstrationRequest();
});
function AssignDataEntryAdminstrationRequest() {
    var RequestData = {
        DataEntryAdminstrationRequestID: myParam,
        //BatchRequestFK: $('#BatchID').val(),
        //BatchAuditingRequestID: myParam,
        DataEntryAdminAssigneeFK: Common.GetUserData().UserID,
        DataEntryAdminComment: $('#DataEntryAdminstrationComment').val()
        //OriginalApprovedClaimsNumber: $('#TotalBatchClaims').val()
        //RemainingUnassignedNumberOfClaims: $('#RemainingBatchClaims').val() - $('#DataEntryAdminstrationClaimsCount').val()
    };

    var urlAddBatchAuditingRequest = '/DataEntryAdminstration/AssignDataEntryAdminstrationRequest';
    Common.Ajax("POST", urlAddBatchAuditingRequest, JSON.stringify(RequestData), 'json', SucessAddBatchAuditingRequest);
}

function SucessGetBatchAuditingRequest(Result) {
    if (Result.Success === true) {
        $('#BatchID').val(Result.Data.BatchRequestFK);
        $('#TotalBatchClaims').val(Result.Data.OriginalApprovedClaimsNumber);
    }
    else
        ShowALert(4, Result.Message);
}

function SucessAddBatchAuditingRequest(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        window.location.href = '/DataEntryAdminstration/ViewAllDataEntryAdminstrationRequests?status=' + Common.Status.DataEntryAdminAssign;
    }
    else {
        ShowALert(4, Result.Message);
    }
}
