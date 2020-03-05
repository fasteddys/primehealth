$(document).ready(function () {
    var myParam = location.search.split('ID=')[1];

    GetRequestData(myParam);

    $('#btnAssignClosingRequest').click(function () {
        var RequestData = {
            BatchClosingRequestID: myParam,
            ClosingOfficerAssigneeFK: Common.GetUserData().UserID,
            ClosingOfficerAssignedComment: $('#ClosingAssignComment').val()
        };

        var urlAssignClosingRequest = '/Closing/AssignBatchClosingRequest';
        Common.Ajax("POST", urlAssignClosingRequest, JSON.stringify(RequestData), 'json', SucessAssignClosingRequest);
    });

    $('#btnConfirmClosingRequest').click(function () {
        var RequestData = {
            BatchClosingRequestID: myParam,
            ConfirmReceivingComment: $('#ClosingAssignComment').val()
        };

        var urlConfirmRecievingByOfficer = '/Closing/ConfirmBatchClosingRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessConfirmRecievingByOfficer);
    });

    $('#btnFinishClosingRequest').click(function () {
        var RequestData = {
            BatchClosingRequestID: myParam,
            FinishedReviewingComment: $('#ClosingAssignComment').val()
        };

        var urlConfirmRecievingByOfficer = '/Closing/FinishBatchClosingRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessFinishByOfficer);
    });

    $('#btnTransferClosingRequest').click(function () {
        var RequestData = {
            BatchClosingRequestID: myParam,
            TransferredBackToAdminComment: $('#ClosingAssignComment').val()
        };

        var urlConfirmRecievingByOfficer = '/Closing/TransferBatchClosingRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessTransferByOfficer);
    });

    $('#btnLock').click(function () {
        var RequestData = {
            TableID: Common.Entites.BatchClosingRequest,
            RecordID: myParam,
            UserID: Common.GetUserData().UserID,
            IsLocked: true
        };
        var urlLockRequest = '/Provider/LockUnLockRequest';
        Common.Ajax("POST", urlLockRequest, JSON.stringify(RequestData), 'json', SucessLockRequest);
    });

    $('#btnUnLock').click(function () {
        var RequestData = {
            TableID: Common.Entites.BatchClosingRequest,
            RecordID: myParam,
            UserID: Common.GetUserData().UserID,
            IsLocked: false
        };
        var urlUnLockRequest = '/Provider/LockUnLockRequest';
        Common.Ajax("POST", urlUnLockRequest, JSON.stringify(RequestData), 'json', SucessUnLockRequest);
    });
});

function GetRequestData(ID) {
    var urlGetRequestData = '/Closing/GetRequestData?ID=' + ID;
    Common.Ajax("GET", urlGetRequestData, null, 'json', SucessGetRequestData);
}

function SucessGetRequestData(Result) {
    if (Result.Success === true) {

        if (Common.EnabledForTeam([Common.Team.Closing,
        Common.Team.Admin], Common.Entites.DataEntryClosingRequest,
            Result.Data.BatchClosingRequestID,
            [Common.Team.Provider, Common.Team.Admin])
        ) {

            if (Result.Data.ClosingOfficerAssignedTime === null)
                $('#btnAssignClosingRequest').show();
            else if (Result.Data.ConfirmReceivingTime === null)
                $('#btnConfirmClosingRequest').show();
            else if (Result.Data.FinishedReviewingTime === null)
                $('#btnFinishClosingRequest').show();
            else if (Result.Data.TransferredBackToAdminTime === null)
                $('#btnTransferClosingRequest').show();

            $('#ClosingAssignComment').val(Result.Data.ClosingOfficerAssignedComment)  
        }
    }
    else {
        ShowALert(4, Result.Message);
    }
}

function SucessAssignClosingRequest(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        location.reload();
    }
    else {
        ShowALert(4, Result.Message);
    }
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

function SucessTransferByOfficer(Result) {
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
