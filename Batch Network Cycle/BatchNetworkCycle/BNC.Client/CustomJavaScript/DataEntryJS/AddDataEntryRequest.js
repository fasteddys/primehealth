var unAssignedClaims;
var myParam;
$(document).ready(function () {
    myParam = location.search.split('ID=')[1];
    urlGetBatchAuditingRequest();
    
});
function urlGetBatchAuditingRequest() {
    var urlGetBatchAuditingRequest = '/DataEntryAdminstration/GetDataEntryAdminstrationRequest?ID=' + myParam;
    Common.Ajax("GET", urlGetBatchAuditingRequest, null, 'json', SucessGetBatchAuditingRequest);
    
}
function SucessGetBatchAuditingRequest(Result) {
    if (Result.Success === true) {
        unAssignedClaims = Result.Data.RemainingUnassignedNumberOfClaims;

        $('#BatchID').val(Result.Data.BatchRequestFK);
        $('#TotalBatchClaims').val(Result.Data.OriginalApprovedClaimsNumber);
        if (Result.Data.DataEntryAdministrationStatusFK === Common.Status.DataEntryAdminAssign) {
            $('#RemainingUnAssignedClaims').val(Result.Data.RemainingUnassignedNumberOfClaims);
            $('.AssignFields').show();
            $('#btnSaveDataEntryRequest').show();
        }
        else if (Result.Data.DataEntryAdministrationStatusFK === Common.Status.NewAdministrationRequest) {
            $('#btnAssignDataEntryAdminstrationRequest').show();
        }

        else if (Result.Data.DataEntryAdministrationStatusFK === Common.Status.PendingTransferToClosingTeam) {
            $('#btnTransferDataEntryRequest').show();
            
        }
        //DataEntryAdministrationStatusFK
    }
    else
        ShowALert(4, Result.Message);
}

function SucessAddDataEntryBatchAssigningRequest(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        window.location.href = '/DataEntryAdminstration/ViewDataEntryAdminstrationRequests?Status=' + Common.Status.DataEntryAdminAssign;        
    }
    else {
        ShowALert(4, Result.Message);
    }
}
$('#DataEntryAdminstrationClaimsCount').change(function () {
    $('#RemainingUnAssignedClaims').val(unAssignedClaims - $('#DataEntryAdminstrationClaimsCount').val());
});

$('#btnSaveDataEntryRequest').click(function () {
    var RequestData = {
        AssignedClaimsNumber: $('#DataEntryAdminstrationClaimsCount').val(),
        DataEntryAdministrationRequestFK: myParam,
        DataEntryOfficerFK: $('#DataEntryUsers').val(),
        DataEntryOfficerName: $("#DataEntryUsers option:selected").text(),
        DataEntryAdminComment: $('#DataEntryComment').val()
    };

    if (RequestData.AssignedClaimsNumber == '' || RequestData.DataEntryOfficerName == 'Select Data Entry User')
        ShowALert(1, "Please Enter Required Data");
    else {
        var urlAddDataEntryBatchAssigningRequest = '/DataEntry/AddDataEntryBatchAssigningRequest';
        Common.Ajax("POST", urlAddDataEntryBatchAssigningRequest, JSON.stringify(RequestData), 'json', SucessAddDataEntryBatchAssigningRequest);
    }
});
$('#btnLock').click(function () {
    var RequestData = {
        TableID: Common.Entites.DataEntryAdminstrationRequest,
        RecordID: myParam,
        UserID: Common.GetUserData().UserID,
        IsLocked: true
    };
    var urlLockRequest = '/Provider/LockUnLockRequest';
    Common.Ajax("POST", urlLockRequest, JSON.stringify(RequestData), 'json', SucessLockRequest);
});
$('#btnUnLock').click(function () {
    var RequestData = {
        TableID: Common.Entites.DataEntryAdminstrationRequest,
        RecordID: myParam,
        UserID: Common.GetUserData().UserID,
        IsLocked: false
    };
    var urlUnLockRequest = '/Provider/LockUnLockRequest';
    Common.Ajax("POST", urlUnLockRequest, JSON.stringify(RequestData), 'json', SucessUnLockRequest);
});
function SucessLockRequest(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);    }
    else {
        ShowALert(4, Result.Message);
    }
}

function SucessUnLockRequest(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);
    }
}



$('#btnTransferDataEntryRequest').click(function () {
    var RequestData = {
        DataEntryBatchAssigningRequestID: myParam,
        TransferedToComment: $('#DataEntryComment').val()
        //DataEntryOfficerFK:Common.GetUserData().UserID
    };

    var urlTransferRequest = '/DataEntryAdminstration/TransferDataEntryAdminstrationRequestToClosingTeam';
    Common.Ajax("POST", urlTransferRequest, JSON.stringify(RequestData), 'json', SucessTransferRequest);
});

function SucessTransferRequest(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        window.location.href = '/DataEntryAdminstration/ViewDataEntryAdminstrationRequests?Status=' + Common.Status.PendingTransferToClosingTeam;
    }
    else
        ShowALert(4, Result.Message);
}

$('#btnAssignDataEntryAdminstrationRequest').click(function () {
    var RequestData = {
        DataEntryAdminstrationRequestID: myParam,
        //BatchRequestFK: $('#BatchID').val(),
        //BatchAuditingRequestID: myParam,
        DataEntryAdminAssigneeFK: Common.GetUserData().UserID,
        DataEntryAdminComment: $('#DataEntryComment').val()
        //OriginalApprovedClaimsNumber: $('#TotalBatchClaims').val()
        //RemainingUnassignedNumberOfClaims: $('#RemainingBatchClaims').val() - $('#DataEntryAdminstrationClaimsCount').val()
    };

    var urlAddBatchAuditingRequest = '/DataEntryAdminstration/AssignDataEntryAdminstrationRequest';
    Common.Ajax("POST", urlAddBatchAuditingRequest, JSON.stringify(RequestData), 'json', SucessAddBatchAuditingRequest);
});

function SucessAddBatchAuditingRequest(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);
    }
}

function HideALer(Type) {
    if (Type === 1) {
        $("#ShowALertInfo").hide();

    }
    else if (Type === 2) {
        $("#ShowALertSuccess").hide();
        location.reload();

    }
    else if (Type === 3) {
        $("#ShowALertWarning").hide();

    }
    else if (Type === 4) {
        $("#ShowALertDanger").hide();

    }
}