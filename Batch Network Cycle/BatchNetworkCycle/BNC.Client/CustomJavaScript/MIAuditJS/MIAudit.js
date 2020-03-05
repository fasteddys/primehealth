var AllClaimsCount = 0;
var BatchAuditID = 0;
$(document).ready(function () {
    //1 get Number Of Claims At Filteration Request Detail
    GetReconciliationComment();
});

function GetNumberOfClaims() {

    var urlgetNumberOfClaimsAtBatchingRequest = "/Batching/getNumberOfClaimsAtBatchAuditRequest?BatchingAuditRequestID=" +
        + Common.getParameterByName("BatchID");//change
    Common.Ajax("get", urlgetNumberOfClaimsAtBatchingRequest, null, 'json', sucessgetNumberOfClaimsAtBatchingRequest);
}
function CheckAuditRequestStatus() {
    var urlMedicalAuditRequestAdded = "/Audit/CheckAuditRequestStatus?BatchID=" + Common.getParameterByName("BatchID");
    Common.Ajax("get", urlMedicalAuditRequestAdded, null, 'json', SucessMedicalAuditRequestAdded);
}
function GetReconciliationComment() {

    var urlGetMIComment = "/MIAudit/GetMIComment?BatchID=" +
        Common.getParameterByName("BatchID");//change
    Common.Ajax("get", urlGetMIComment, null, 'json', sucessGetMIComment);
}
function sucessGetMIComment(Result) {
    $("#MIComment").val(Result.Data.MIOfficerComment);
    $("#ClaimsCount").val(Result.Data.TotalClaimsNumber);
    BatchAuditID = Result.Data.BatchAuditRequestFK;

    CheckAuditRequestStatus();

}

function SucessMedicalAuditRequestAdded(Result) {
    if (Common.EnabledForTeam([Common.Team.MisrInsuranceAudit, Common.Team.Admin], Common.Entites.AuditRequest, BatchAuditID, [Common.Team.Provider, Common.Team.Admin])) {

        if (Result.Data === "PendingMi") {
            $("#btnAssignRequest").show();

        }
        else if (Result.Data === "PendingTransferFromMIAudit") {
            $("#btnTransferRequest").show();
        } else if (Result.Data === "AssignMi") {
            $("#btnSaveMIRequest").show();


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
function SucessTransferRequest(result) {
    if (result.Success) {
        ShowALert(2, result.Message);
    }
    else {
        ShowALert(4, result.Message);
    }
}
//2 create Batch
$("#btnSaveMIRequest").on("click", function () {

    var _MIAuditOfficerComment = $("#MIComment").val();
    if (_MIAuditOfficerComment == "") {
        ShowALert(1, "Please Enter All Data");
    }
    else {
        var urlAddMIAuditRequest = "/MIAudit/AddMIAuditRequest";
        var NewAddMIAuditRequest =
        {
            BatchRequestFK: Common.getParameterByName("BatchID"),
            MIOfficerComment: _MIAuditOfficerComment,
            MIOfficerFK: Common.GetUserData().UserID,
            BatchAuditRequestFK: BatchAuditID


        };
        Common.Ajax("post", urlAddMIAuditRequest, JSON.stringify(NewAddMIAuditRequest), 'json', sucessAddMedicalAuditRequest);
    }
});
$("#btnAssignRequest").on("click", function () {
        var urlAddMIAuditRequest = "/MIAudit/AssignMIAuditRequest";
        var NewAddMIAuditRequest =
        {
            BatchRequestFK: Common.getParameterByName("BatchID"),
            MIOfficerFK: Common.GetUserData().UserID,
            BatchAuditRequestFK: BatchAuditID
        };
    Common.Ajax("post", urlAddMIAuditRequest, JSON.stringify(NewAddMIAuditRequest), 'json', sucessAddMedicalAuditRequest)
});


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
function sucessAddMedicalAuditRequest(Res)
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