$(document).ready(function () {
    GetBatchData();
});
function GetBatchData() {

    var urlGetBatchData = "/Batching/getBatchData?BatchID=" + Common.getParameterByName("BatchID");
    Common.Ajax("get", urlGetBatchData, null, 'json', sucessGetBatchData);
}
function sucessGetBatchData(Result) {

    $("#BatchClaimsCount").val(Result.Data.NumberOfBatchClaims);
    $("#BatchIdOnSystem").val(Result.Data.BatchNumber);
    $("#BatchingComment").val(Result.Data.BatchingOfficerComment);
    $("#SystemNameFk").val(Result.Data.BatchSystemFK);
    if (Common.EnabledForTeam([Common.Team.Recieving, Common.Team.Admin],
        Common.Entites.BatchRequest,
        Result.Data.BatchingRequestID,
        [Common.Team.Provider, Common.Team.Admin])) {
        if (Result.Data.StatusFK == Common.Status.NewBatch) {

            

                $("#btnTransferBatchToAudit").show();

            
        }
    }
}
$("#btnTransferBatchToAudit").click(function () {
    var urlTransferBatchToAudit = "/Batching/TransferBatchRequestToAudit?BatchID=" + Common.getParameterByName("BatchID");
    Common.Ajax("get", urlTransferBatchToAudit, null, 'json', sucessTransferBatchToAudit);
});
function sucessTransferBatchToAudit(Result) {
    if (Result.Success) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);
    }

}
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