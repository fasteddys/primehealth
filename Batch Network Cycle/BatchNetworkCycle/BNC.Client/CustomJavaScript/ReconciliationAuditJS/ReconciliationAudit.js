var AllClaimsCount = 0;
var BatchAuditID=0;
$(document).ready(function () {
    GetReconciliationComment();
});
function getNumberOfClaims() {
    var urlgetNumberOfClaimsAtBatchingRequest = "/Batching/getNumberOfClaimsAtBatchAuditRequest?BatchingAuditRequestID=" +
        Common.getParameterByName("BatchID");//change
    Common.Ajax("get", urlgetNumberOfClaimsAtBatchingRequest, null, 'json', sucessgetNumberOfClaimsAtBatchingRequest);

}
function GetReconciliationComment() {

    var urlGetReconciliationComment = "/ReconciliationAudit/GetReconciliationComment?BatchID=" +
        Common.getParameterByName("BatchID");//change
    Common.Ajax("get", urlGetReconciliationComment, null, 'json', sucessGetReconciliationComment);
}

function sucessGetReconciliationComment(Result) {
    $("#ReconciliationComment").val(Result.Data.ReconciliationOfficerComment);
    $("#ClaimsCount").val(Result.Data.TotalClaimsNumber);
    BatchAuditID = Result.Data.BatchAuditRequestFK;
    CheckAuditRequestStatus();

}
function CheckAuditRequestStatus() {
    var urlMedicalAuditRequestAdded = "/Audit/CheckAuditRequestStatus?BatchID=" + Common.getParameterByName("BatchID");
    Common.Ajax("get", urlMedicalAuditRequestAdded, null, 'json', SucessMedicalAuditRequestAdded);
}
function SucessMedicalAuditRequestAdded(Result) {
    if (Common.EnabledForTeam([Common.Team.ReconciliationAudit, Common.Team.Admin], Common.Entites.AuditRequest, BatchAuditID, [Common.Team.Provider, Common.Team.Admin])) {
        if (Result.Data === "PendingReconilition") {
            $("#btnAssignRequest").show();

        }
        else if (Result.Data === "PendingTransferFromReconilitionAudit") {
            $("#btnTransferRequest").show();
        }
        else if (Result.Data === "AssignReconilition") {
            $("#btnSaveReconciliationRequest").show();

        }

        else {
            ;
        }
    }
}


$("#btnTransferRequest").on("click", function () {
    var urlTransferRequest = "/FinancialAudit/TransferRequestToNextAuditStep";
    var BatchAudit = {
        BatchAuditingRequestID: BatchAuditID,
        BatchingRequestFK: Common.getParameterByName("BatchID")
    };
    Common.Ajax("post", urlTransferRequest, JSON.stringify(BatchAudit), 'json', SucessTransferRequest);

});
$("#btnAssignRequest").on("click", function () {
    var urlAddMIAuditRequest = "/ReconciliationAudit/AssignReconciliationAuditRequest";
    var NewAddMIAuditRequest =
    {
        BatchRequestFK: Common.getParameterByName("BatchID"),
        ReconciliationOfficerFK: Common.GetUserData().UserID,
        BatchAuditRequestFK: BatchAuditID
    };
    Common.Ajax("post", urlAddMIAuditRequest, JSON.stringify(NewAddMIAuditRequest), 'json', sucessAddReconciliationAuditRequest);
});


function SucessTransferRequest(result) {
    if (result.Success) {
        ShowALert(2, result.Message);
    }
    else {
        ShowALert(4, result.Message);
    }
}
//2 create Batch
$("#btnSaveReconciliationRequest").on("click", function () {

    var _ReconciliationOfficerComment = $("#ReconciliationComment").val();
    if (_ReconciliationOfficerComment == "") {
        ShowALert(1, "Please Enter All Data");
    }
    else {
        var urlAddReconciliationAuditRequest = "/ReconciliationAudit/AddReconciliationAuditRequest";
        var NewAddReconciliationAuditRequest =
        {
            BatchRequestFK: Common.getParameterByName("BatchID"),
            ReconciliationOfficerComment: _ReconciliationOfficerComment,
            ReconciliationOfficerFK: Common.GetUserData().UserID,
            BatchAuditRequestFK: BatchAuditID
        };
        Common.Ajax("post", urlAddReconciliationAuditRequest, JSON.stringify(NewAddReconciliationAuditRequest), 'json', sucessAddReconciliationAuditRequest);
    }
});

/*-----------------------------------------------------------*/
function sucessgetNumberOfClaimsAtBatchingRequest(Result)
{
    if (Result.Success) {
        AllClaimsCount = Result.Data;
        $("#ClaimsCount").val(Result.Data);

    }
    else
    {
        //$("#AllClaimsCount").text("No RequestDetail");
        //$("#AllClaimsCount").val(0)

    }
}
function sucessAddReconciliationAuditRequest(Res)
{
    if (Res.Success)
    {
        ShowALert(2, Res.Message);
    }
    else
    {
        ShowALert(4, Res.Message);
    }
}
/*-----------------------------------------------------------*/
function hideModle() {
    Common.HideModal('printInvoice');
}
function ShowModal() {
    Common.ShowModal('printInvoice');

}
/*-----------------------------------------------------------*/

function HideALer(Type) {
    if (Type === 1) {
        $("#ShowALertInfo").hide();
        location.reload();
    }
    else if (Type === 2) {
        $("#ShowALertSuccess").hide();
        location.reload();
    }
    else if (Type === 3) {
        $("#ShowALertWarning").hide();
        location.reload();
    }
    else if (Type === 4) {
        $("#ShowALertDanger").hide();
        location.reload();
    }
}