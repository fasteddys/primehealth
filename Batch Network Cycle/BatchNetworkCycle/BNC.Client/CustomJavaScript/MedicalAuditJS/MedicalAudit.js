var AllClaimsCount = 0;
var BatchAuditID = 0;

$(document).ready(function () {
    GetMedicalAuditDate();
    //$('#SendToSpecialApprovalModel').modal('show');

});
function getNumberOfClaims() {
    var urlgetNumberOfClaimsAtBatchingRequest = "/Batching/getNumberOfClaimsAtBatchAuditRequest?BatchingAuditRequestID="
        + Common.getParameterByName("BatchID");//change
    Common.Ajax("get", urlgetNumberOfClaimsAtBatchingRequest, null, 'json', sucessgetNumberOfClaimsAtBatchingRequest);
    
}
function GetMedicalAuditDate() {
    var urlGetMedicalAuditData = "/MedicalAudit/GetMedicalAuditRequestData?BatchID=" + Common.getParameterByName("BatchID");
    Common.Ajax("get", urlGetMedicalAuditData, null, 'json', sucessGetMedicalAuditDate);
}
function sucessGetMedicalAuditDate(Result) {
    $("#MedicalNumberOfApprovedClaims").val(Result.Data.NumberOfApprovedClaims);
    $("#MedicalAmountOfApprovedClaims").val(Result.Data.ApprovedClaimsAmount);
    $("#MedicalNumberOfPendingClaims").val(Result.Data.NumberOfPendingClaims);
    $("#MedicalAmountOfPendingClaims").val(Result.Data.PendingClaimsAmount);
    $("#MedicalNumberOfRejectClaims").val(Result.Data.NumberOfRejectedClaims);
    $("#MedicalAmountOfRejectClaims").val(Result.Data.RejectedClaimsAmount);
    $("#MedicalComment").val(Result.Data.MedicalAuditOfficerComment);
    $("#ClaimsCount").val(Result.Data.TotalNumberOfApprovedClaims);
    $("#BatchID").val(Common.getParameterByName("BatchID"));
    BatchAuditID = Result.Data.BatchAuditRequestFK;

    AllClaimsCount = Result.Data.TotalNumberOfApprovedClaims;
    CheckAuditRequestStatus();

}
$("#btnTransferRequest").on("click", function () {
    var BatchAudit = {
        BatchAuditingRequestID: BatchAuditID,
        BatchingRequestFK: Common.getParameterByName("BatchID")
    };
    var urlTransferRequest = "/FinancialAudit/TransferRequestToNextAuditStep";
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
function CheckAuditRequestStatus() {
    var urlMedicalAuditRequestAdded = "/Audit/CheckAuditRequestStatus?BatchID=" + Common.getParameterByName("BatchID");
    Common.Ajax("get", urlMedicalAuditRequestAdded, null, 'json', SucessMedicalAuditRequestAdded);
}
function SucessMedicalAuditRequestAdded(Result) {
    if (Common.EnabledForTeam([Common.Team.MedicalAudit, Common.Team.Admin], Common.Entites.AuditRequest, BatchAuditID, [Common.Team.Provider, Common.Team.Admin])) {
        if (Result.Data === "PendingMedical") {
            $("#btnAssignMedicalRequest").show();
            Common.DisableNumberInputType();

        }
        else if (Result.Data === "AssignMedical") {
            $("#btnSaveMedicalRequest").show();

        }
        else if (Result.Data === "PendingTransferFromMidecalAudit") {
            $("#btnTransferRequest").show();

        }
        else {
            ;
        }
    }
}
$("#btnAssignMedicalRequest").on("click", function () {

 
    var urlAddMedicalAuditRequest = "/MedicalAudit/AssignMedicalAuditRequest";
        var NewAddMedicalAuditRequest =
        {
            BatchRequestFK: Common.getParameterByName("BatchID"),
          
            MedicalAuditOfficerFK: Common.GetUserData().UserID,
            BatchAuditRequestFK: BatchAuditID

        };
        Common.Ajax("post", urlAddMedicalAuditRequest, JSON.stringify(NewAddMedicalAuditRequest), 'json', sucessAddMedicalAuditRequest);
    
});
$("#btnSaveMedicalRequest").on("click", function () {
    var _NumberOfApprovedClaims = $("#MedicalNumberOfApprovedClaims").val();
    var _ApprovedClaimsAmount = $("#MedicalAmountOfApprovedClaims").val();
    var _NumberOfPendingClaims = $("#MedicalNumberOfPendingClaims").val();
    var _PendingClaimsAmount = $("#MedicalAmountOfPendingClaims").val();
    var _NumberOfRejectedClaims = $("#MedicalNumberOfRejectClaims").val();
    var _RejectedClaimsAmount = $("#MedicalAmountOfRejectClaims").val();
    var _MedicalAuditOfficerComment = $("#MedicalComment").val();
    var sumOFAllClaims = parseInt(_NumberOfApprovedClaims) + parseInt(_NumberOfPendingClaims) + parseInt(_NumberOfRejectedClaims);
    if (_NumberOfApprovedClaims == "" || _ApprovedClaimsAmount == "" || _NumberOfPendingClaims == "" || _PendingClaimsAmount == "" || _NumberOfRejectedClaims == "" || _RejectedClaimsAmount == "") {
        ShowALert(1, "Please Enter All Data");
    }
    else if (sumOFAllClaims != AllClaimsCount) {
        ShowALert(3, "Number Of Enter Claims != All Claims Count");
    }
    else if (_PendingClaimsAmount > 0)
    {
        $('#SendToSpecialApprovalModel').modal('show');
        $("#spPendingClaimsCount").text(_NumberOfPendingClaims);

    }
    else
    {
        SaveMedicalRequest();
    }
});
$("#btnSendToSpecialApproval").on("click", function () {
    SaveMedicalRequest();
    
})
function SaveMedicalRequest()
{
    var _NumberOfApprovedClaims = $("#MedicalNumberOfApprovedClaims").val();
    var _ApprovedClaimsAmount = $("#MedicalAmountOfApprovedClaims").val();
    var _NumberOfPendingClaims = $("#MedicalNumberOfPendingClaims").val();
    var _PendingClaimsAmount = $("#MedicalAmountOfPendingClaims").val();
    var _NumberOfRejectedClaims = $("#MedicalNumberOfRejectClaims").val();
    var _RejectedClaimsAmount = $("#MedicalAmountOfRejectClaims").val();
    var _MedicalAuditOfficerComment = $("#MedicalComment").val();
    var _ReqByUserNote = $("#specialApprovalUserComment").val();


    var urlAddMedicalAuditRequest = "/MedicalAudit/AddMedicalAuditRequest";
    var NewAddMedicalAuditRequest =
        {
            BatchRequestFK: Common.getParameterByName("BatchID"),
            NumberOfApprovedClaims: _NumberOfApprovedClaims,
            ApprovedClaimsAmount: _ApprovedClaimsAmount,
            NumberOfPendingClaims: _NumberOfPendingClaims,
            PendingClaimsAmount: _PendingClaimsAmount,
            NumberOfRejectedClaims: _NumberOfRejectedClaims,
            RejectedClaimsAmount: _RejectedClaimsAmount,
            MedicalAuditOfficerComment: _MedicalAuditOfficerComment,
            MedicalAuditOfficerFK: Common.GetUserData().UserID,
            ReqByUserNote: _ReqByUserNote,
            BatchAuditRequestFK: BatchAuditID


        };
    Common.Ajax("post", urlAddMedicalAuditRequest, JSON.stringify(NewAddMedicalAuditRequest), 'json', sucessAddMedicalAuditRequest);

}
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
function sucessAddMedicalAuditRequest(Res)
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
/*-----------------------------------------------------------*/
function hideModle() {
    Common.HideModal('printInvoice');
}
function ShowModal() {
    Common.ShowModal('printInvoice');

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
/*-----------------------------------------------------------*/