$(document).ready(function () {
    var myParam = location.search.split('ID=')[1];

    GetRequestData(myParam);

    $('#btnAssignReAdministrationClosingRequest').click(function () {
        var RequestData = {
            BatchClosingReAdministrationRequestID: myParam,
            AssignedByAdminFK: Common.GetUserData().UserID,
            AssignedByAdminComment: $('#ReAdministrationClosingComment').val()
        };

        var urlAssignClosingRequest = '/ReAdministration/AssignBatchClosingReAdministrationRequest';
        Common.Ajax("POST", urlAssignClosingRequest, JSON.stringify(RequestData), 'json', SucessAssignClosingRequest);
    });

    $('#btnConfirmClosingRequest').click(function () {
        var RequestData = {
            BatchClosingReAdministrationRequestID: myParam,
            ConfirmedReceivingComment: $('#ReAdministrationClosingComment').val()
        };

        var urlConfirmRecievingByOfficer = '/ReAdministration/ConfirmBatchClosingReAdministrationRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessConfirmRecievingByOfficer);
    });

    $('#btnFinishClosingRequest').click(function () {
        var RequestData = {
            BatchClosingReAdministrationRequestID: myParam,
            FinalClosureComment: $('#ReAdministrationClosingComment').val()
            //ArchivingSystemTicketNo: $('#ArchivingSystemTicketNo').val()
        };

        var urlConfirmRecievingByOfficer = '/ReAdministration/FinishBatchClosingReAdministrationRequest';
        Common.Ajax("POST", urlConfirmRecievingByOfficer, JSON.stringify(RequestData), 'json', SucessFinishByOfficer);
    });

    $('#btnLock').click(function () {
        var RequestData = {
            TableID: Common.Entites.ClosedBatchReAdministrationRequest,
            RecordID: myParam,
            UserID: Common.GetUserData().UserID,
            IsLocked: true
        };
        var urlLockRequest = '/Provider/LockUnLockRequest';
        Common.Ajax("POST", urlLockRequest, JSON.stringify(RequestData), 'json', SucessLockRequest);
    });

    $('#btnUnLock').click(function () {
        var RequestData = {
            TableID: Common.Entites.ClosedBatchReAdministrationRequest,
            RecordID: myParam,
            UserID: Common.GetUserData().UserID,
            IsLocked: false
        };
        var urlUnLockRequest = '/Provider/LockUnLockRequest';
        Common.Ajax("POST", urlUnLockRequest, JSON.stringify(RequestData), 'json', SucessUnLockRequest);
    });
});

function GetRequestData(ID) {
    var urlGetRequestData = '/ReAdministration/GetRequestData?ID=' + ID;
    Common.Ajax("GET", urlGetRequestData, null, 'json', SucessGetRequestData);
}

function SucessGetRequestData(Result) {
    if (Result.Success === true) {
        if (Common.EnabledForTeam([Common.Team.Readministration,
            Common.Team.Admin], Common.Entites.ClosedBatchReAdministrationRequest,
            Result.Data.BatchClosingReAdministrationRequestID,
            [Common.Team.Provider, Common.Team.Admin])
        ) {
            if (Result.Data.ReAdministrationOfficerAssignedTime === null)
                $('#btnAssignReAdministrationClosingRequest').show();
            else if (Result.Data.ConfirmedReceivingOn === null)
                $('#btnConfirmClosingRequest').show();
            else if (Result.Data.FinalClosureTime === null)
                $('#btnFinishClosingRequest').show();
        }
        else {
            ShowALert(4, Result.Message);
        }
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


