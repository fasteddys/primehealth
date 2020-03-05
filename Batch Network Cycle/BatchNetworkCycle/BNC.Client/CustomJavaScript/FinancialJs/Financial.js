var AllClaimsCount = 0;
var BatchAuditID = 0;
$(document).ready(function () {
      GetFinancialAuditDate();
    //$('#SendToSpecialApprovalModel').modal('show');

});
function getNumberOfClaims() {
    var urlgetNumberOfClaimsAtBatchingRequest = "/Batching/getNumberOfClaimsAtBatchAuditRequest?BatchingAuditRequestID="
        + Common.getParameterByName("BatchID");//change
    Common.Ajax("get", urlgetNumberOfClaimsAtBatchingRequest, null, 'json', sucessgetNumberOfClaimsAtBatchingRequest);


}
function GetFinancialAuditDate() {
    var urlGetfinancialAuditData = "/FinancialAudit/GetfinancialAuditRequestData?BatchID="+ Common.getParameterByName("BatchID");
    Common.Ajax("get", urlGetfinancialAuditData, null, 'json', sucessGetFinancialAuditDate);
}
function sucessGetFinancialAuditDate(Result) {
    $("#FinancialNumberOfApprovedClaims").val(Result.Data.NumberOfApprovedClaims);
    $("#FinancialAmountOfApprovedClaims").val(Result.Data.ApprovedClaimsAmount);
    $("#FinancialNumberOfPendingClaims").val(Result.Data.NumberOfPendingClaims);
    $("#FinancialAmountOfPendingClaims").val(Result.Data.PendingClaimsAmount);
    $("#FinancialNumberOfRejectClaims").val(Result.Data.NumberOfRejectedClaims);
    $("#FinancialAmountOfRejectClaims").val(Result.Data.RejectedClaimsAmount);
    $("#FinancialComment").val(Result.Data.FinancialAuditOfficerComment);
    $("#ClaimsCount").val(Result.Data.TotalNumberOfApprovedClaims);
    $("#BatchID").val(Common.getParameterByName("BatchID"));
    BatchAuditID = Result.Data.BatchAuditRequestFK;
    AllClaimsCount = Result.Data.TotalNumberOfApprovedClaims;
    CheckAuditRequestStatus();

}
function CheckAuditRequestStatus() {
    var urlMedicalAuditRequestAdded = "/Audit/CheckAuditRequestStatus?BatchID=" + Common.getParameterByName("BatchID");
    Common.Ajax("get", urlMedicalAuditRequestAdded, null, 'json', SucessMedicalAuditRequestAdded);
}
$("#btnAssignFinancialRequest").on("click", function () {


    var urlAddFinancialAuditRequest = "/FinancialAudit/AssignFinancialAuditRequest";
    var NewAddFinancialAuditRequest =
    {
        BatchRequestFK: Common.getParameterByName("BatchID"),
        FinancialAuditOfficerFK: Common.GetUserData().UserID,
        BatchAuditRequestFK: BatchAuditID
    };
    Common.Ajax("post", urlAddFinancialAuditRequest, JSON.stringify(NewAddFinancialAuditRequest), 'json', sucessAddFinancialAuditRequest);

});
function SucessMedicalAuditRequestAdded(Result) {
    if (Common.EnabledForTeam([Common.Team.FinincialAudit, Common.Team.Admin], Common.Entites.AuditRequest, BatchAuditID, [Common.Team.Provider, Common.Team.Admin])) {

        if (Result.Data === "PendingFindincial") {
            $("#btnAssignFinancialRequest").show();
            Common.DisableNumberInputType();

        }
        else if (Result.Data === "AssignFindincial") {
            $("#btnSaveFinancialRequest").show();
        }
        else if (Result.Data === "PendingTransferFromFindincialAudit") {
            $("#btnTransferRequest").show();

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
    Common.Ajax("post", urlTransferRequest, JSON.stringify( BatchAudit), 'json', SucessTransferRequest);

});
function SucessTransferRequest(result) {
    if (result.Success) {
        ShowALert(2, result.Message);
    }
    else {
        ShowALert(4, result.Message);
    }}
/*-----------------------------------------------------------*/
//2 create Batch
$("#btnSaveFinancialRequest").on("click", function () {
    var _NumberOfApprovedClaims = $("#FinancialNumberOfApprovedClaims").val();
    var _ApprovedClaimsAmount = $("#FinancialAmountOfApprovedClaims").val();
    var _NumberOfPendingClaims = $("#FinancialNumberOfPendingClaims").val();
    var _PendingClaimsAmount = $("#FinancialAmountOfPendingClaims").val();
    var _NumberOfRejectedClaims = $("#FinancialNumberOfRejectClaims").val();
    var _RejectedClaimsAmount = $("#FinancialAmountOfRejectClaims").val();
    var _FinancialAuditOfficerComment = $("#FinancialComment").val();
    var sumOFAllClaims = parseInt(_NumberOfApprovedClaims) + parseInt(_NumberOfPendingClaims) + parseInt(_NumberOfRejectedClaims);
    if (_NumberOfApprovedClaims == "" || _ApprovedClaimsAmount == "" || _NumberOfPendingClaims == "" || _PendingClaimsAmount == "" || _NumberOfRejectedClaims == "" || _RejectedClaimsAmount == "") {
        ShowALert(1, "Please Enter All Data");
    }
    else if (sumOFAllClaims != AllClaimsCount) {
        ShowALert(3, "Number Of Enter Claims != All Claims Count");
    }
    else if (_PendingClaimsAmount > 0) {
        $('#SendToSpecialApprovalModel').modal('show');
        $("#spPendingClaimsCount").text(_NumberOfPendingClaims);

    }
    else {
        SaveAddFinancialAuditRequest();
        }
});
$("#btnSendToSpecialApproval").on("click", function () {
    SaveAddFinancialAuditRequest();
})
/*-----------------------------------------------------------*/
function sucessgetNumberOfClaimsAtBatchingRequest(Result)
{
    if (Result.Success) {
        AllClaimsCount = Result.Data;
        $("#ClaimsCount").val(Result.Data);
        $("#BatchID").val(Common.getParameterByName("BatchID"));

    }
    else
    {
        //$("#AllClaimsCount").text("No RequestDetail");
        //$("#AllClaimsCount").val(0)

    }
}
function sucessAddFinancialAuditRequest(Res)
{
    if (Res.Success)
    {
      $('#SendToSpecialApprovalModel').modal('hide');

        ShowALert(2, Res.Message);
    }
    else
    {
        ShowALert(4, Res.Message);
    }
}
function SaveAddFinancialAuditRequest()
{
    var _NumberOfApprovedClaims = $("#FinancialNumberOfApprovedClaims").val();
    var _ApprovedClaimsAmount = $("#FinancialAmountOfApprovedClaims").val();
    var _NumberOfPendingClaims = $("#FinancialNumberOfPendingClaims").val();
    var _PendingClaimsAmount = $("#FinancialAmountOfPendingClaims").val();
    var _NumberOfRejectedClaims = $("#FinancialNumberOfRejectClaims").val();
    var _RejectedClaimsAmount = $("#FinancialAmountOfRejectClaims").val();
    var _FinancialAuditOfficerComment = $("#FinancialComment").val();
    var _ReqByUserNote = $("#specialApprovalUserComment").val();

    var urlAddFinancialAuditRequest = "/FinancialAudit/AddFinancialAuditRequest";
    var NewAddFinancialAuditRequest =
        {
            BatchRequestFK: Common.getParameterByName("BatchID"),
            NumberOfApprovedClaims: _NumberOfApprovedClaims,
            ApprovedClaimsAmount: _ApprovedClaimsAmount,
            NumberOfPendingClaims: _NumberOfPendingClaims,
            PendingClaimsAmount: _PendingClaimsAmount,
            NumberOfRejectedClaims: _NumberOfRejectedClaims,
            RejectedClaimsAmount: _RejectedClaimsAmount,
            FinancialAuditOfficerComment: _FinancialAuditOfficerComment,
            FinancialAuditOfficerFK: Common.GetUserData().UserID,
            ReqByUserNote: _ReqByUserNote,
            BatchAuditRequestFK: BatchAuditID

        };
    Common.Ajax("post", urlAddFinancialAuditRequest, JSON.stringify(NewAddFinancialAuditRequest), 'json', sucessAddFinancialAuditRequest);
    
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