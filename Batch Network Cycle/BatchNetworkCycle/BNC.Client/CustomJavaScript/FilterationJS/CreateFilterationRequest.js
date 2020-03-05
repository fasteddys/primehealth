var AllCategoryies;
var TotalNumberOfClaims = 0;
var TxtOldValue=0;
var Status = "";
$(document).ready(function () {
        GetCountOfClaimsOfFilterationRequest();
        CheckIsTransferedFilterationRequest();
        //-------------------------------------------------------------
       //1
        var _reqid = $("#FilterationRequestID")[0].textContent;//1
      //  Common.checkLockedRequest(Common.Entites.FilterationRequest, _reqid)//2
        //--------------------------------------------------------
    if (Common.EnabledForTeam([Common.Team.Filteration, Common.Team.Admin],
        Common.Entites.FilterationRequest, _reqid,
        [Common.Team.Provider, Common.Team.Admin])) {

        if (Status == "FinishedFiltration") {
            if (Common.EnabledForTeam([Common.Team.Filteration, Common.Team.Admin])) {

                $("#SendToBatching").show();
            }
            GetFilterationRequestBeforeBatching();
            ReceivingRequestsCount();

        }
        else if (Status == "UnderFiltrationProcess") {
            GetAllCategoryies();

            $("#SaveCountOfFilteration").show();

        }
        else if (Status == "PendingFiltration") {
            GetAllCategoryies();

            $("#SaveCountOfFilteration").show();

        }
        else {
            ReceivingRequestsCount();

            ShowFilterationRequestData();
        }
    }
});
function GetFilterationRequestBeforeBatching()
    {
    var URLGetFilterationRequestData = "/Filteration/GetFilterationRequestBeforeBatching?FilterationRequestID=" + $("#FilterationRequestID")[0].textContent;
    Common.Ajax('get', URLGetFilterationRequestData, null, 'json', AppendAllFilterationDetialsData, false);

}
function ShowFilterationRequestData() {
    var URLGetFilterationRequestData = "/Filteration/GetFilterationRequestDetails?FilterationRequestID=" + $("#FilterationRequestID")[0].textContent;
    Common.Ajax('get', URLGetFilterationRequestData, null, 'json', AppendAllFilterationDetialsData, false);
}
function AppendAllFilterationDetialsData(Result) {
    var AllFilterationDetials = Result.Data;
    AllFilterationDetials.forEach(function (item) {
        // do something with `item`
        $("#FilterationDetailsData").append(
            '<div class="form-group row" ><div class="col-md-5"><label for="InPatientPrimeHealth">'
            + item.FilterationCategoryName + ' <span class="red"> *</span> </label></div><div class="col-md-7"><input type="number" onchange="OnchangeOfClaimsCount(this)" onfocus="BeforchangeOfClaimsCount(this)" class="form-control" id="'
            + item.FilterationCategoryID + '" placeholder="'
            + item.FilterationCategoryName + '" value="' + item.ClaimsCount+'" /></div></div >'
        );
    }); 

    $("#FilterationCommentDiv").hide();
}
function CheckIsTransferedFilterationRequest() {
    var IsTransferedFilterationRequestURL = "/Filteration/GetFilterationRequestStatus?FilterationRequestID="+ $("#FilterationRequestID")[0].textContent;
    Common.Ajax('get', IsTransferedFilterationRequestURL, null, 'json', SuccessIsTransferedFilterationRequest, false);
}
function SuccessIsTransferedFilterationRequest(Result) {

    Status= Result.Data;
}
function GetAllCategoryies() {
    var GetAllCategoryies = "/Filteration/GetAllCategoryies";
    Common.Ajax('get', GetAllCategoryies, null, 'json', SucessGetAllCategoryies, false);
    ReceivingRequestsCount();

}
function SucessGetAllCategoryies(result) {
    AllCategoryies = result.Data;
    AllCategoryies.forEach(function (item) {
        // do something with `item`
      $("#FilterationDetailsData").append(
          '<div class="form-group row" ><div class="col-md-5"><label for="InPatientPrimeHealth">'
          + item.FilterationCategoryName + ' <span class="red"> *</span> </label></div><div class="col-md-7"><input type="number" onchange="OnchangeOfClaimsCount(this)" onfocus="BeforchangeOfClaimsCount(this)" class="form-control" id="'
          + item.FilterationCategoryID + '" placeholder="'
          + item.FilterationCategoryName + '" /></div></div >'
        );
    }); 
}
$("#btnSaveFilterationRequest").click(function () {
    SaveFilterationRequest();

    });
function BeforchangeOfClaimsCount(txtvalue) {
    if (isNumeric(txtvalue.value)) {
        TxtOldValue = parseInt(txtvalue.value, 10);

    } else {
        TxtOldValue = parseInt(0, 10);
    }
  
}
function OnchangeOfClaimsCount(txtvalue)
{
    if (isNumeric(txtvalue.value)) {
        TotalNumberOfClaims = TotalNumberOfClaims - TxtOldValue;
        TotalNumberOfClaims = TotalNumberOfClaims + parseInt(txtvalue.value, 10);
        $("#FilterationTotalClaimsCount").val(TotalNumberOfClaims);
    }
    else {
        txtvalue.value = 0;
        TotalNumberOfClaims = TotalNumberOfClaims - parseInt(TxtOldValue, 10);
        $("#FilterationTotalClaimsCount").val(TotalNumberOfClaims);

    }

    if (TotalNumberOfClaims > $("#RecievingTotalClaimsCount").val()) {
        $("#differentBetweenRecievingAndFilteration").val("<");

    }
    else if (TotalNumberOfClaims < $("#RecievingTotalClaimsCount").val()) {
        $("#differentBetweenRecievingAndFilteration").val(">");

    }
    else if (TotalNumberOfClaims == $("#RecievingTotalClaimsCount").val()) {
        $("#differentBetweenRecievingAndFilteration").val("=");
    }
}
function ReceivingRequestsCount() {
    var ReceivingRequestsCountURL = "/Receiving/ReceivingRequestsCount?FilterationRequestID="
        + $("#FilterationRequestID")[0].textContent;
    Common.Ajax('get', ReceivingRequestsCountURL, null, 'json', SucessReceivingRequestsCount, false);
}
function SucessReceivingRequestsCount(Count)
{
    $("#RecievingTotalClaimsCount").val(Count);
    if (TotalNumberOfClaims > Count) {
        $("#differentBetweenRecievingAndFilteration").val("<");

    }
    else if (TotalNumberOfClaims < Count) {
        $("#differentBetweenRecievingAndFilteration").val(">");

 }
    else if (TotalNumberOfClaims == Count) {
        $("#differentBetweenRecievingAndFilteration").val("=");
 }
}
function isNumeric(value) {
    return /^-{0,1}\d+$/.test(value);
}
function GetCountOfClaimsOfFilterationRequest() {
    var GetFilterationRequestClaimsCount = "/Filteration/GetCountOfClaimsOfFilterationRequest?FilterationRequestID="
        + /*Common.getParameterByName("FilterationRequestID")*/
         $("#FilterationRequestID")[0].textContent
        ;
    Common.Ajax('get', GetFilterationRequestClaimsCount, null, 'json', SucessGetFilterationRequestClaimsCount, false);
}
function SucessGetFilterationRequestClaimsCount(Result) {
    $("#FilterationTotalClaimsCount").val(Result.Data);
    TotalNumberOfClaims = Result.Data;
}
$("#SaveCountOfFilteration").click(function () {
    Common.ShowModal("IsLastBatchingRequestModal");

});
$("#NotTheLastBatchingRequest").click(function () {
    Common.HideModal("IsLastBatchingRequestModal");

    Common.ShowModal("ConfirmModal");

});
$("#YesIsTheLastBatchingRequest").click(function () {
    Common.HideModal("IsLastBatchingRequestModal");
    if (TotalNumberOfClaims == $("#RecievingTotalClaimsCount").val())
    {
        Common.ShowModal("SendToBatchingModal");
    }
    else
    {
        Common.ShowModal("SendToProvidersTeamModal");
    }

    
});
$("#SendToBatching").click(function ()
{
    var SendToBatchingUrl = "/Filteration/SendFiltrationRequestToBatching?FilterationRequestID=" + $("#FilterationRequestID")[0].textContent;
    Common.Ajax('get', SendToBatchingUrl, null, 'json', SucessSendToBatching, false);
});
$("#CloseFiltration").click(
    function () {
         var ArrOfDetails = [];
    AllCategoryies.forEach(function (item) {
        var value = $("#FilterationDetailsData").find('#' + item.FilterationCategoryID).val();
        var x = Common.GetUserData().UserID;
        var obj = {
            FilterationOfficerClaimsCount: value,
            CategoryID: item.FilterationCategoryID,
            OfficerID: Common.GetUserData().UserID,
            comment: $("#FilterationComment").val(),
            FilterationRequestID: $("#FilterationRequestID")[0].textContent /*Common.getParameterByName("FilterationRequestID")*/

        };
        ArrOfDetails.push(obj);
    });

        var CloseFiltrationUrl = "/Filteration/CloseFiltrationRequest";//?ReceivingRequestID=" + Common.getParameterByName("ReceivingRequestID");
        Common.Ajax('Post', CloseFiltrationUrl, JSON.stringify(ArrOfDetails), 'json', SucessSendToBatching, false);
});


function SucessSendToBatching(Result)
{
    
    if (Result.Success) {

        ShowALert(2, Result.Message);
    } else {
        ShowALert(2, Result.Message);
    }
}
function SaveFilterationRequest() {
        var ArrOfDetails = [];
        AllCategoryies.forEach(function (item) {
            var value = $("#FilterationDetailsData").find('#' + item.FilterationCategoryID).val();
            var x = Common.GetUserData().UserID;
            var obj = {
                FilterationOfficerClaimsCount: value,
                CategoryID: item.FilterationCategoryID,
                OfficerID: Common.GetUserData().UserID,
                comment: $("#FilterationComment").val(),
                FilterationRequestID: $("#FilterationRequestID")[0].textContent
            };
            ArrOfDetails.push(obj);
        });
        Common.HideModal("ConfirmModal");
    var CreateFilterationRequestDetial = "/Filteration/CreateFilterationRequestDetial?FilterationRequestID="
        + $("#FilterationRequestID")[0].textContent; /*Common.getParameterByName("FilterationRequestID");*/
        Common.Ajax('post', CreateFilterationRequestDetial, JSON.stringify(ArrOfDetails), 'json', SucessCreateFilterationRequestDetial, false);
    
}
function SucessCreateFilterationRequestDetial(Result) {
    if (Result.Success) {

        ShowALert(2, Result.Message);
    } else {
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

