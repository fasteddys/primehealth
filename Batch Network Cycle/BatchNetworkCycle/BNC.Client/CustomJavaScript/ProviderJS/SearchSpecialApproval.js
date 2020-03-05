var tempSPReqID = -1;
$(document).ready(function () {
    $("#btngetAllSPReq").on("click", function () {
        $('#SpApprovalReqDataTable').DataTable().destroy();
        var _SPStatusID = $("#SpStatutsFK").val();
        var _UserID = $("#UsersFK").val();
        var _From = $("#SPReqFrom").val();
        var _To = $("#SPReqTo").val();
        var _BatchNumber =  $("#SPReqBatchNumber").val();
        var _ProviderFK = $("#ProvidersFK").val();
        var _SPReasonFK = $("#SpReasonsFK").val();
        if (_SPStatusID =='')
        {
            ShowALert(1, "Please Enter Special Approval Status"); 
        }
        else
        {
            var urlSearchSpecialApprovalReqs = "/Provider/searchSpecialApprovalReq";
            var newSearchSpecialApprovalInput =
                {
                    SPStatusID: _SPStatusID,
                    UserID: _UserID,
                    From: _From,
                    To: _To,
                    BatchNumber: _BatchNumber,
                    ProviderFK: _ProviderFK,
                    SPReasonFK: _SPReasonFK,

                }
            Common.Ajax("post", urlSearchSpecialApprovalReqs, JSON.stringify(newSearchSpecialApprovalInput), 'json', SuccessSearchSpecialApproval);

        }
    }) 

    $("#btnSaveSpecialApprovalData").on("click", function () {
        var ApprovedClaims = 0;
        var RejectClaims = 0;
        var PendingClaimsCount = 0;
        if ($("#ApprovedClaims").val() != '')
        {
             ApprovedClaims = parseInt($("#ApprovedClaims").val());

        }
        if ($("#RejectClaims").val() != '') {
             RejectClaims = parseInt($("#RejectClaims").val());

        }
        if ($("#PendingClaimsCount").val() != '') {
             PendingClaimsCount = parseInt($("#PendingClaimsCount").val());

        }

        if (ApprovedClaims == '' && RejectClaims == '') {
            ShowALert(1, "Please Enter Data");
            //code add request 
        }


       else if (ApprovedClaims > PendingClaimsCount || RejectClaims > PendingClaimsCount || (ApprovedClaims + RejectClaims) > PendingClaimsCount )
        {
            ShowALert(3, "number of Claims > Pending Claims Count");
        }
        else if ((ApprovedClaims + RejectClaims) < PendingClaimsCount)
        {
            ShowALert(3, "number of Claims < Pending Claims Count");

        }
       else  if ((ApprovedClaims + RejectClaims) == PendingClaimsCount && tempSPReqID!=-1) {
            ChangeSpRequestLifeCycle(ApprovedClaims, RejectClaims);
        }
       //else if (ApprovedClaims != 0 && RejectClaims == 0 && (ApprovedClaims + RejectClaims) == PendingClaimsCount && tempSPReqID != -1) {
       //     ChangeSpRequestLifeCycle(ApprovedClaims, RejectClaims);
       // }
       //else if (ApprovedClaims == 0 && RejectClaims != 0 && (ApprovedClaims + RejectClaims) == PendingClaimsCount && tempSPReqID != -1) {
       //     ChangeSpRequestLifeCycle(ApprovedClaims, RejectClaims);
       // }

    })

    $("#ApprovedClaims,#RejectClaims").on("keyup", function () {
        $("#RemainingPendingClaims").val(($("#PendingClaimsCount").val() - $("#ApprovedClaims").val() - $("#RejectClaims").val()) +' remaining');
    })
})
/*------------------------------------------------------------------------*/
function ChangeSpRequestLifeCycle(_ApprovedClaims,_RejectClaims)
{
    var SPProviderComment = $("#SPProviderComment").val();
    var urlAddSpUserAndReq ="/Provider/ChangeSpRequestLifeCycle"
    var newSpAuditUserRequest=
      {
          PendingClaimCount:_ApprovedClaims + _RejectClaims,
          ApprovedClaimCount: _ApprovedClaims,
          RejectClaimCount: _RejectClaims,
          SpUserFk: 1,//change from c#
          SpActionFK: Common.SPAction.DataManipulated,
          SpReqFk: tempSPReqID,
          spUserComment:SPProviderComment

        }
    Common.Ajax("post", urlAddSpUserAndReq, JSON.stringify(newSpAuditUserRequest), 'json', SuccessAddSpUserAndAuditRequest)

}
function SuccessAddSpUserAndAuditRequest(Res)
{

    if (Res.Success)
    {
        ShowALert(2, Res.Message)
        $("#spDataDetailsReq").modal('hide');
        location.reload();

    }
    else
    {
        ShowALert(4, Res.Message)

    }
}
/*------------------------------------------------------------------------*/
function SuccessSearchSpecialApproval(Res)
{
    if (Res.Success)
    {
        displaySpecialApprovalReqs(Res);
    }
    else
    {
        ShowALert(1, Res.Message)
    }
}
function displaySpecialApprovalReqs(Res)
{
    $('#SpApprovalReqDataTable').DataTable().destroy();
    var $table = $('#SpApprovalReqDataTable');
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": Res.Data,
        "columns": [
            { "data": "SPReqID" },
            //{ "data": "ReqByUserFk" },
            { "data": "ReqByUserName" },
            //{ "data": "SPStatusFK" },
            { "data": "SPStatusName" },
            { "data": "ReqFK" },
            //{ "data": "EntrityFK" },
            { "data": "EntrityName" },
            //{ "data": "ReqByUserNote" },
            //{ "data": "IsLoadedByUser" },
            {
                data: "CreationDate",//3
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    if (data.SPStatusFK == Common.SPStatus.Pending)
                    {
                        return '<button class="btn btn-danger" onclick="showSpDataDetails(' + data.SPReqID + "," + data.EntrityFK + ')">View</button>';
                    }
                    else
                    {
                        return '<button class="btn btn-success" onclick="showSpDataDetails(' + data.SPReqID + "," + data.EntrityFK + ')">View</button>';

                    }

                }
            },
        ]
    });


}//3
/*------------------------------------------------------------------------*/
function showSpDataDetails(_SPReqID, _EntrityFK)
{
    tempSPReqID = _SPReqID;
    var urlSearchSpecialApprovalReqInfo = "/Provider/GetSpecialApprovalInfoByID";
    var newSearchSpecialApprovalInfo =
        {
            SPReqID: _SPReqID,
            EntrityFK: _EntrityFK
        };
    Common.Ajax("post", urlSearchSpecialApprovalReqInfo, JSON.stringify(newSearchSpecialApprovalInfo), 'json', SuccessSearchSpecialApprovalInfo);
}//change tempSPReqID
function SuccessSearchSpecialApprovalInfo(Res)
{
    if (Res.Success)
    {
        $("#spDataDetailsReq").modal('show');
        disabledInput();
        PutData(Res);
        if (Res.Data.SPStatusFK == Common.SPStatus.Pending)//change
        {
            EnapleSPTextBox();
            $(".spUserInfo").hide();
            $(".spFormEnterData").show();

        }
        else
        {
            $("#ActionOn").val(moment(Res.Data.ActionOn).format('DD/MM/YYYY hh:mm:ss A'));
            $("#userSystemName").val(Res.Data.SpUserName);
            $("#FullName").val(Res.Data.SpUserName);
            $("#ApprovalUserNotes").val(Res.Data.spUserActionComment);
            $(".spUserInfo").show();
            $(".spFormEnterData").hide();

        }
    }
    else
    {
        ShowALert(4, Res.Message);

    }
}
function PutData(Res)
{
    if (Res.Data.BatchSystemFK = Common.System.MCA)//change
    {
        //$("#hcpPin").val(Res.Data.MCAProviderPin + " Mca/" + Res.Data.IbnSinaProviderPin + "IbnSina");
        $("#hcpPin").val(Res.Data.MCAProviderPin);
    }
    else if (Res.Data.BatchSystemFK = Common.System.IbnSina) {
        $("#hcpPin").val(Res.Data.IbnSinaProviderPin);
    }
    $("#System").val(Res.Data.SystemName);
    $("#ProviderName").val(Res.Data.ProviderEnglishName);
    $("#Year").val(Res.Data.BillingYear);
    $("#Month").val(Res.Data.BilllingMonth);
    $("#RequestedBy").val(Res.Data.UserName);
    $("#Team").val(Res.Data.TeamName);
    $("#CreatedOn").val(moment(Res.Data.CreationDate).format('DD/MM/YYYY hh:mm:ss A'));
    $("#PendingClaims").val(Res.Data.NumberOfPendingClaims);
    $("#PendingClaimsCount").val(Res.Data.NumberOfPendingClaims);
    $("#BatchNumber").val(Res.Data.BatchNumber);
    $("#SpecialApprovalStatus").val("Status: " + Res.Data.SPStatusName);
    $("#SpecialApprovalStatusLabel").text(Res.Data.SPStatusName);

    $("#SpecialApprovalNotes").val(Res.Data.ReqByUserNote);
    $("#SpecialApprovalReasons> li").remove();
    for (var i = 0; i < Res.Data.Reasons.length; i++) {
        $("#SpecialApprovalReasons").append("<li>" + Res.Data.Reasons[i] + "</li>");
    }
    //if sp request is done or approve or reject or data minapulation
    //$("#ActionOn").val(Res.Data.ActionOn);
 
    /*------------------------------------------------------------------------*/
}
/*------------------------------------------------------------------------*/
function disabledInput() {
    $(" .modal .form-control,#btnSaveSpecialApprovalData").attr("disabled", true);
    $(".modal .form-control,#btnSaveSpecialApprovalData").css("pointer-events", "none");
}
function EnapleSPTextBox() {
    $('#ApprovedClaims,#RejectClaims,#btnSaveSpecialApprovalData,#SPProviderComment').attr("disabled", false);
    $("#ApprovedClaims,#RejectClaims,#btnSaveSpecialApprovalData,#SPProviderComment").css("pointer-events", "auto");


}
/*------------------------------------------------------------------------*/