var myParam;
$(document).ready(function () {
    myParam = location.search.split('ID=')[1];
    GetRequestData(myParam);
    GetDataEntryBatchAssigningRequest();

    $('#btnConfirmDataEntryRequest').click(function () {
        var RequestData = {
            DataEntryBatchAssigningRequestID: myParam,
            DataEntryOfficerReceivingComment: $('#DataEntryComment').val(),
            DataEntryOfficerFK: Common.GetUserData().UserID
        };

        var urlConfirmRecievingByOfficer = '/DataEntry/ConfirmRecievingByOfficer';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessConfirmRecievingByOfficer);
    });

    $('#btnFinishDataEntryRequest').click(function () {
        var RequestData = {
            DataEntryBatchAssigningRequestID: myParam,
            DataEntryOfficerFinishedComment: $('#DataEntryComment').val(),
            DataEntryOfficerFK: Common.GetUserData().UserID
        };

        var urlConfirmRecievingByOfficer = '/DataEntry/DataEntryOfficerFinished';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessFinishByOfficer);
    });
    $('#btnLock').click(function () {
        var RequestData = {
            TableID: Common.Entites.DataEntryBatchAssigningRequest,
            RecordID: myParam,
            UserID: Common.GetUserData().UserID,
            IsLocked: true
        };
        var urlLockRequest = '/Provider/LockUnLockRequest';
        Common.Ajax("POST", urlLockRequest, JSON.stringify(RequestData), 'json', SucessLockRequest);
    });
    $('#btnUnLock').click(function () {
        var RequestData = {
            TableID: Common.Entites.DataEntryBatchAssigningRequest,
            RecordID: myParam,
            UserID: Common.GetUserData().UserID,
            IsLocked: false
        };
        var urlUnLockRequest = '/Provider/LockUnLockRequest';
        Common.Ajax("POST", urlUnLockRequest, JSON.stringify(RequestData), 'json', SucessUnLockRequest);
    });
});

function GetRequestData(ID) {
    var urlGetRequestData = '/DataEntry/GetRequestData?ID=' + ID;
    Common.Ajax("GET", urlGetRequestData, null, 'json', SucessGetRequestData);
}

function SucessGetRequestData(Result) {
    if (Result.Success === true) {
        if (Common.EnabledForTeam([Common.Team.DataEntry,
        Common.Team.Admin], Common.Entites.DataEntryRequest,
            Result.Data.DataEntryBatchAssigningRequestID,
            [Common.Team.Provider, Common.Team.Admin])
        )
        {
            if (Result.Data.ConfirmRecievingByOfficerTime === null)
            {
                $('#btnConfirmDataEntryRequest').show();
            }
            else if (Result.Data.DataEntryOfficerFinishedOnSystemTime === null)
            {
                $('#btnFinishDataEntryRequest').show();
            }
            $('#DataEntryComment').val(Result.Data.DataEntryOfficerReceivingComment);
        }
    }
    else {
        ShowALert(4, Result.Message);
    }
}

function GetDataEntryBatchAssigningRequest() {
    var urlGetDataEntryBatchRequest = '/DataEntry/GetDataEntryBatchAssigningRequestByID?RequestID=' + myParam;
    Common.Ajax("GET", urlGetDataEntryBatchRequest, null, 'json', SucessGetDataEntryBatchRequest);
}

function SucessGetDataEntryBatchRequest(Result) {
    if (Result.Success === true) 
        $('#AssignedClaims').val(Result.Data.AssignedClaimsNumber);
    else
        ShowALert(4, Result.Message);
}

function SucessConfirmRecievingByOfficer(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        location.reload();
    }        

    else 
        ShowALert(4, Result.Message);
}

function SucessFinishByOfficer(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        location.reload();
    }
    else
        ShowALert(4, Result.Message);
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

