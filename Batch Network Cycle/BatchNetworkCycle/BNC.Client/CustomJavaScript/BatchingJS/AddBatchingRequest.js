var AllClaimsCount = 0;
var resGetParameterByName;
$(document).ready(function () {
    GetClaimsNumber();
});
function GetClaimsNumber() {
    resGetParameterByName = Common.getParameterByName("FilterationRequestDetailID");//change
    var urlgetNumberOfClaimsAtFilterationRequestDetail = "/Filteration/getNumberOfClaimsAtFilterationRequestDetail?FilterationRequestDetailID=" + resGetParameterByName;
    Common.Ajax("get", urlgetNumberOfClaimsAtFilterationRequestDetail, null, 'json', sucessgetNumberOfClaimsAtFilterationRequestDetail);
}
function sucessgetNumberOfClaimsAtFilterationRequestDetail(Result) {
    if (Result.Success) {
        AllClaimsCount = Result.Data;
        $("#AllClaimsCount").val(Result.Data);
        if (AllClaimsCount > 0) {
            if (Common.EnabledForTeam([Common.Team.Batching, Common.Team.Admin],
                Common.Entites.BatchingFilterationDetails,
                Common.getParameterByName("FilterationRequestDetailID"),
                [Common.Team.Provider, Common.Team.Admin])) {
                $("#btnCreateBatch").show();
            }
        }
    }
    else {
        //$("#AllClaimsCount").text("No RequestDetail");
        //$("#AllClaimsCount").val(0)

    }
}

$("#btnCreateBatch").on("click", function () {
    var _NumberOfBatchClaims = $("#BatchClaimsCount").val();
    var _BatchSystemFK = $("#SystemNameFk").val();
    var _BatchNumber = $("#BatchIdOnSystem").val();
    var _BatchingOfficerComment = $("#BatchingComment").val();
    if (_NumberOfBatchClaims == "" || _BatchSystemFK == "" || _BatchNumber == "") {
        ShowALert(1, "Please Enter All Data");
    }
    else if (_NumberOfBatchClaims > AllClaimsCount) {
        ShowALert(3, "Number Of Batch Claims > All Claims Count");
    }
    else {
        var urlAddBatchingRequest = "/Batching/AddBatchingRequest";
        var NewAddBatchingRequest =
        {
            NumberOfBatchClaims: _NumberOfBatchClaims,
            BatchSystemFK: _BatchSystemFK,
            BatchNumber: _BatchNumber,
            BatchingOfficerFK: Common.GetUserData().UserID,//user assign
            BatchingOfficerComment: _BatchingOfficerComment,
            BatchingFilterationDetailsFK: Common.getParameterByName("FilterationRequestDetailID")//change to any row of 5 record in out pation
        };
        Common.Ajax("post", urlAddBatchingRequest, JSON.stringify(NewAddBatchingRequest), 'json', sucessAddBatchingRequest);
    }
});
/*-----------------------------------------------------------*/
function sucessAddBatchingRequest(Res)
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