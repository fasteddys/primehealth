$(document).ready(function () {
    if (Common.getParameterByName("ReceivingRequestID") === null)
    {
        if (Common.EnabledForTeam([Common.Team.Recieving, Common.Team.Admin])) {
            $("#btnAddRecievingRequest").show();
            Common.enabledFormInput();
        }
        // document.getElementById("btnAddRecievingRequest").style.visibility = "visible";


    } else
    {

        var urlGetRecievingRequestData = "/Receiving/GetRecievingRequestData?ReceivingRequestID=" + Common.getParameterByName("ReceivingRequestID");
        Common.Ajax("get", urlGetRecievingRequestData, null, 'json', sucessGetRecievingRequestData);

       // document.getElementById("btnTransferRequest").style.visibility = "visible";
    }
});



function sucessGetRecievingRequestData(Result) {

    $("#ReceivedClaimsCount").val(Result.Data.ReceivedClaimsCount);
    $("#ReceivedOfficerClaimsCount").val(Result.Data.ClaimsCount);
    $("#ProviderID").val(Result.Data.ProviderID);
    $("#ExpectedAmount").val(Result.Data.ExpectedAmount);
    $("#AgentName").val(Result.Data.AgentName);
    $('#CompanyID').val(Result.Data.CompanyFK);
    $('#GovernmentID').val(Result.Data.GovernmentFK);

    
    var x = document.getElementById("BilllingMonthAndYear");
    //document.getElementById("BilllingMonthAndYear").value = "2017-09";
    var month = "";
    if (Result.Data.BilllingMonth < 10) {
        month = "0" + Result.Data.BilllingMonth;
    }
    else {
        month = Result.Data.BilllingMonth;
    }
    document.getElementById("BilllingMonthAndYear").value = Result.Data.BillingYear + '-' + month;

    $("#ReceivedComment").val(Result.Data.ReceivingOfficerComment);
    if (Common.EnabledForTeam([Common.Team.Recieving,
        Common.Team.Admin], Common.Entites.ReceivingRequest,
        Result.Data.ReceivingRequestID,
        [Common.Team.Provider, Common.Team.Admin])
    )
    {
        if (Result.Data.StatusID == Common.Status.Printed && Result.Data.IsTransferdToFiltrationTeam == false)
        {
            $("#btnTransferRequest").show();

        }
        else if (Result.Data.StatusID == Common.Status.NewReceived)
        {
            $("#btnPrint").show();
        }
        else
        {
            $("#btnRePrint").show();
        }
    }
}
$("#btnAddRecievingRequest").on("click", function () {
    var _ReceivedClaimsCount = $("#ReceivedClaimsCount").val();
    var _ReceivedOfficerClaimsCount = $("#ReceivedOfficerClaimsCount").val();
    var _AgentName = $("#AgentName").val();

    
    var _ProviderFK = $("#ProviderID").val();
    var _ExpectedAmount = $("#ExpectedAmount").val();
    var _BilllingMonth = $("#BilllingMonthAndYear").val().split("-")[1];
    var _BillingYear = $("#BilllingMonthAndYear").val().split("-")[0];
    var _ReceivingOfficerComment = $("#ReceivedComment").val();

    var _CompanyFK = $('#CompanyID').val();
    var _GovernmentFK = $('#GovernmentID').val();

    if (_ReceivedClaimsCount == ""  || _ExpectedAmount == "" || _BilllingMonth == "" || _BillingYear == "" || _CompanyFK == "" || _GovernmentFK == "") {
        ShowALert(1, "Please Enter All Data");
        hideModle();
    }
    else {
        var urlAddReceivingRequest = "/Receiving/AddRecievingRequest";
        var NewAddReceivingRequest =
        {
            ReceivedClaimsCount: _ReceivedClaimsCount,
            ReceivingOfficerCalimsCount: _ReceivedOfficerClaimsCount,
            ProviderFK: _ProviderFK,
            ExpectedAmount: _ExpectedAmount,
            BilllingMonth: _BilllingMonth,
            BillingYear: _BillingYear,
            ReceivingOfficerComment: _ReceivingOfficerComment,
            ReceivingOfficerFK: Common.GetUserData().UserID,
            AgentName: _AgentName,
            CompanyFK: _CompanyFK,
            GovernmentFK: _GovernmentFK

        };
        Common.Ajax("post", urlAddReceivingRequest, JSON.stringify(NewAddReceivingRequest), 'json', sucessAddReceivingRequest);
    }
});
$("#btnTransferRequest").on("click", function () {
    var urlTransferRequest = "/Receiving/TransferRecievingRequestToFiltrationRequest?RecievingRequestID=" + Common.getParameterByName("ReceivingRequestID");

    Common.Ajax("get", urlTransferRequest, null, 'json', sucessTransferRequest);
    
});
$("#btnPrint").on("click", function () {
        window.open("/Receiving/PrintRecievingReportPDF?RecievingRequestID=" + Common.getParameterByName("ReceivingRequestID"), '_blank');
    location.reload();
});
$("#btnRePrint").on("click", function () {
    window.open("/Receiving/RePrintRecievingReportPDF?RecievingRequestID=" + Common.getParameterByName("ReceivingRequestID"), '_blank');
    location.reload();
});
function sucessPrint() { }
function sucessTransferRequest(Result) {
    if (Result.Success) {
        ShowALert(1, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);
    }
}
function sucessAddReceivingRequest(Result)
{
    if (Result.Success)
    {
        ShowModal();//to print invoice;
        ShowALert(2, Result.Message);
    }
    else
    {
        ShowALert(4, Result.Message);
    }
}
function hideModle()
{
    Common.HideModal('printInvoice');
}
function ShowModal() {
    Common.ShowModal('printInvoice');

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
