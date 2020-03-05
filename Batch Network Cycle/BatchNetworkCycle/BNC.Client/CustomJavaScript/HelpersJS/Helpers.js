var EnaleLockOrUnlock;
var EntitesID;
var RequestID;
var Locked;
var Common = {

    Ajax: function (httpMethod, url, data, type, successCallBack, async, cache, traditional, ContentType) {
        if (typeof async === "undefined") {
            async = true;
        }
        if (typeof cache === "undefined") {
            cache = false;
        }
        if (typeof traditional === "undefined") {
            traditional = true;
        }
        //if (httpMethod === "post") {
        //    ContentType = 'application/json'
        //}


        var ajaxObj = $.ajax({
            type: httpMethod.toUpperCase(),
            url: url,
            data: data,
            dataType: type,
            async: async,
            cache: cache,
            crossDomain: true,
            contentType: 'application/json',
            processData: false,
            success: successCallBack,
            error: function (err, type, httpStatus) {
                Common.AjaxFailureCallback(err, type, httpStatus);
            }
        });

        return ajaxObj;
    },

    DisplaySuccess: function (message) {
        Common.ShowSuccessSavedMessage(message);
    },

    DisplayError: function (error) {
        Common.ShowFailSavedMessage(message);
    },

    AjaxFailureCallback: function (err, type, httpStatus) {
        var failureMessage = 'Error occurred in ajax call' + err.status + " - " + err.responseText + " - " + httpStatus;
        console.log(failureMessage);
    },

    ShowSuccessSavedMessage: function (messageText) {

        //use jquery BlockUI library to display message

        //   $.blockUI({ message: messageText });
        // setTimeout($.unblockUI, 1500);
    },

    ShowFailSavedMessage: function (messageText) {

        //use jquery BlockUI library to display message

        // $.blockUI({ message: messageText });
        // setTimeout($.unblockUI, 1500);
    },
    ShowModal: function (modelName) {
        $("#" + modelName).modal('show');
    },

    HideModal: function (modelName) {
        $("#" + modelName).modal('hide');
    },
    GetUserData: function () {
        var UserData;

        var name = "UserData" + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                UserData = c.substring(name.length, c.length);
            }
        }

        return JSON.parse(UserData);

    },
    getParameterByName: function (name) {

        var url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    },
    Status: Object.freeze(
        {
            NewReceived: 1,
            Printed: 2,
            PendingFiltration: 3,
            FinishedFiltration: 4,
            PendingBatching: 5,
            UnderBatchingProcess: 6,
            FinishedBatching: 7,
            PendingAudit: 8,
            PendingDataEntryFinish: 9,
            PendingMedical: 10,
            PendingFindincial: 11,
            PendingMi: 12,
            PendingReconilition: 13,
            PendingProvider: 14,
            FinishedAudit: 15,
            NewAdministrationRequest: 16,
            DataEntryAdminAssign: 17,
            PendingDataEntryAssign: 18,
            PendingTransferToClosingTeam: 19,
            PendingDataEntryConfirm: 20,
            ConfirmDataEntryAssign: 21,
            DataEntryAssignFinished: 22,
            NewClosingRequest: 23,
            AssignToClosing: 24,
            ClosingConfirmReceiving: 25,
            ClosingFinished: 26,
            AssignMedical: 30,
            AssignFindincial: 31,
            AssignMi: 32,
            AssignReconilition: 33,
            AssignProvider: 34,
            ReAdministrationAssign: 35,
            ReAdministrationNewRequest: 36,
            ReAdministrationConfirmReceving: 37,
            ReAdministrationFinished: 38,
            FiltrationTransferredToBatching: 39,
            UnderFiltrationProcess: 40,
            PendingTransferFromMidecalAudit: 41,
            PendingTransferFromMIAudit: 42,
            PendingTransferFromFindincialAudit: 43,
            PendingTransferFromReconilitionAudit: 44,
            TransferedFromRecieving: 45,
            DataEntryAdminFinished: 46,
            ClosingPendingTransfer: 47,
            NewBatch: 48

        }),
    Entites: Object.freeze({
        ReceivingRequest: 1,
        FilterationRequest: 2,
        BatchRequest: 3,
        AuditRequest: 6,
        DataEntryRequest: 7,
        DataEntryAdminstrationRequest: 8,
        BatchingFilterationDetails: 9,
        DataEntryClosingRequest: 10,
        ClosedBatchReAdministrationRequest: 11,
        DataEntryBatchAssigningRequest: 12,
        BatchClosingRequest: 13,
        ReAdministrationRequest: 14,
        MedicalAuditRequest: 15,
        FinancialAuditRequest: 16,
        MIAuditRequest: 17,
        ReconciliationAuditRequest: 18,
        BatchAuditingRequest: 21
    }),
    Team: Object.freeze({
        DataEntry: 1,
        DataEntryAdmin: 2,
        Provider: 4,
        Recieving: 5,
        Filteration: 6,
        Audit: 7,
        Admin: 8,
        Closing: 9,
        MedicalAudit: 11,
        FinincialAudit: 12,
        ReconciliationAudit: 15,
        MisrInsuranceAudit: 21,
        Batching: 22,
        Readministration:23
    }),
    SPStatus: Object.freeze({
        Pending: 1,
        Approved: 2,
        Rejected:4,
        Voided: 5,
        DataManipulated:7,
        AutoApproved: 12,

    }),
    SPAction: Object.freeze({
        Approved: 2,
        Rejected: 3,
        DataManipulated: 5,
    }),
    System: Object.freeze({
       
        MCA: 1,
        IbnSina: 4,


    }),
    DisableNumberInputType: function () {
        var inp = document.getElementsByTagName('input');
        for (var i in inp) {
            if (inp[i].type == "number") {
                inp[i].setAttribute("disabled", "true");

            }
        }
    },
    EnabledForTeam: function (ArrayOfTeamsAllowed, EntityID, ReqID, ArrayOfTeamsAllowedTolockAndUpnLock) {
        if (EntityID != null && ReqID != null) {
            EntitesID = EntityID;
            RequestID = ReqID;
            this.checkLockedRequest(EntityID, ReqID, ArrayOfTeamsAllowedTolockAndUpnLock);
            this.enableLockOrUnLockbtn(ArrayOfTeamsAllowedTolockAndUpnLock);
           
        }
        var TeamID = Common.GetUserData().TeamID;
        if (ArrayOfTeamsAllowed.includes(TeamID)) {
            return true;
        }
        else {
            return false;
        }

    },
    EnabledForUser: function (UserID, EntityID, ReqID) {
        if (EntityID != null && ReqID != null) {
            var x = this.checkLockedRequest(EntityID, ReqID);
        }
        if (this.GetUserData().UserID === UserID) {
            return true;
        }
        else {
            return false;
        }

    },
    checkLockedRequest: function (_EntityID, _ReqID) {
        this.disabledFormInput();
        //-------------------------------------------------------------    
        var urlCheckLockedRequest = "/Provider/CheckLockedRequest";
        var NewCheckLockedRequest =
        {
            EntityID: _EntityID,
            ReqID: _ReqID
        };
        Common.Ajax("post", urlCheckLockedRequest, JSON.stringify(NewCheckLockedRequest), 'json', successCheckLockedRequest, false);
    },
    disabledFormInput: function () {
        $("form button,form input,form .form-control").attr("disabled", true);
        $("form button,form input,form .form-control").css("pointer-events", "none");
        $(".formOverlay").slideDown(1000);

    },
    enabledFormInput: function () {
        $("form button,form input,form .form-control").attr("disabled", false);
        $("form button,form input,form .form-control").css("pointer-events", "auto");
        $("form .formOverlay").hide();

    },
    enableLockOrUnLockbtn(ArrayOfTeamsAllowedTolockAndUpnLock) {
        if (EnaleLockOrUnlock === true) {
            var TeamID = Common.GetUserData().TeamID;
            if (ArrayOfTeamsAllowedTolockAndUpnLock.includes(TeamID)) {

                if (Locked === true)
                {
                    $("#btnUnLock").show();
                }
                else if (Locked === false)
                {
                    $("#btnLock").show();
                }
            }

        }
        
    }
};
//----------------------------------------------------------------------
//checked if request is locked or not
function successCheckLockedRequest(Result) {
    if (Result.IsLockedRequest == true) {
        Locked = true;
        Common.disabledFormInput();
        EnaleLockOrUnlock = Result.RequestCanLocked;
    }
    else {
        Locked = false;
        Common.enabledFormInput();
        EnaleLockOrUnlock = Result.RequestCanLocked;
    }
}
$(document).ready(function () {
        $('#btnLock').click(function () {
            var RequestData = {
                TableID: EntitesID,
                RecordID: RequestID,
                UserID: Common.GetUserData().UserID,
                IsLocked: true
            };
            var urlLockRequest = '/Provider/LockUnLockRequest';
            Common.Ajax("POST", urlLockRequest, JSON.stringify(RequestData), 'json', SucessLockRequest);
        });
        $('#btnUnLock').click(function () {
            var RequestData = {
                TableID: EntitesID,
                RecordID: RequestID,
                UserID: Common.GetUserData().UserID,
                IsLocked: false
            };
            var urlUnLockRequest = '/Provider/LockUnLockRequest';
            Common.Ajax("POST", urlUnLockRequest, JSON.stringify(RequestData), 'json', SucessUnLockRequest);
        });
        function SucessLockRequest(Result) {
            if (Result.Success) {
                ShowALert(2, Result.Message);
            }
            else {
                ShowALert(4, Result.Message);
            }
        }
        function SucessUnLockRequest(Result) {
            if (Result.Success) {
                ShowALert(2, Result.Message);
            }
            else {
                ShowALert(4, Result.Message);
            }
        }
    });