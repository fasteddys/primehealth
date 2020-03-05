var AllClaimsCount=0;
$(document).ready(function () {
    //1 get Number Of Claims At Filteration Request Detail
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
        else {
            var urlAddFinancialAuditRequest = "/FinancialAudit/AddFinancialAuditRequest";
            var NewAddFinancialAuditRequest =
            {
                BatchRequestFK: 1,//change  
                NumberOfApprovedClaims: _NumberOfApprovedClaims,
                ApprovedClaimsAmount: _ApprovedClaimsAmount,
                NumberOfPendingClaims: _NumberOfPendingClaims,
                PendingClaimsAmount: _PendingClaimsAmount,
                NumberOfRejectedClaims: _NumberOfRejectedClaims,
                RejectedClaimsAmount: _RejectedClaimsAmount,
                FinancialAuditOfficerComment: _FinancialAuditOfficerComment,
                FinancialAuditOfficerFK: Common.GetUserData().UserID

            };
            Common.Ajax("post", urlAddFinancialAuditRequest, JSON.stringify(NewAddFinancialAuditRequest), 'json', sucessAddFinancialAuditRequest);
        }
    });
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
function sucessAddFinancialAuditRequest(Res)
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