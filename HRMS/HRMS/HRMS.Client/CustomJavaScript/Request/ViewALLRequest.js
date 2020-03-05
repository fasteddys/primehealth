var Requests = [];
var Approvals = [];
var LeaveTypes = [];
var RequestStatus = [];



$(document).ready(function () {
    //GetAllRequests();
   // GetAllApprovals();
    GetAllLeaveType();
    GetAllRequestStatus();
    $("#RequestStatus").val(3);
    FindRequest();
});
function GetAllRequests() {
    var urlGetAllRequests = ConfigurationData.GlobalApiURL+"/Request/GetALLUserRequests?UserID=" + LoggedUserData.GlobalUserID;
    Common.Ajax('get', urlGetAllRequests, null, 'json', SucessGetRequests, false);
}
function SucessGetRequests(Requests) {
    //for (var i = 0; i < Requests.Data.length; i++)
    //{
    //    $("#RequestsTable").append('<tr><td>' + Requests.Data[i].RequestID + '</td><td>' + Requests.Data[i].LeaveType
    //       + '</td><td>' + Requests.Data[i].CreationDate + '</td><td>' + Requests.Data[i].DurationFrom + '</td><td>' + Requests.Data[i].DurationTo
    //       + '</td><td>' + Requests.Data[i].BackToWork + '</td><td>' + Requests.Data[i].RequesStatus + '</td><td>' + Requests.Data[i].Number +
    //       '</td><td>' + Requests.Data[i].Unit
    //       +'</td><td></tr>'
    //        );
    //}


}
function GetAllApprovals() {
    var urlGetAllApprovals = ConfigurationData.GlobalApiURL+"/Request/GetALLUserRequests?UserID=" + LoggedUserData.GlobalUserID;
    Common.Ajax('get', urlGetAllApprovals, null, 'json', SucessGetApprovals, false);
}
function SucessGetApprovals(Approvals) {
    for (var i = 0; i < Approvals.Data.length; i++) {
        var x = moment(Approvals.Data[i].RequestDuration).format('yyyy-MM-dd');

        $("#ApprovalsTable").append('<tr><td>' + Approvals.Data[i].RequestID + '</td><td>' + Approvals.Data[i].UserName + '</td><td>' + Approvals.Data[i].LeaveType
           + '</td><td>' + Approvals.Data[i].CreationDate + '</td><td>' + Approvals.Data[i].DurationFrom + '</td><td>' + Approvals.Data[i].DurationTo
            + '</td><td>' + Approvals.Data[i].BackToWork + '</td><td>' + Approvals.Data[i].RequesStatus + '</td><td>' + Approvals.Data[i].RequestDuration +
           '</td><td>' + Approvals.Data[i].Unit
           + '</td><td></tr>'
            );
    }


}
function GetAllLeaveType() {
    var urlAllLeaveTypes = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes?UserID=" + LoggedUserData.GlobalUserID;
    Common.Ajax('get', urlAllLeaveTypes, null, 'json', SucessGetLeaves, false);
}
function SucessGetLeaves(Leaves) {
    LeaveTypes = Leaves;
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $("#RequestLeaveType").append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
        $("#ApprovalsLeaveType").append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');

    }


}
function GetAllRequestStatus() {
    var urlAllRequestStatus = ConfigurationData.GlobalApiURL +"/Request/GetAllRequestStatusTypes";
    Common.Ajax('get', urlAllRequestStatus, null, 'json', SucessGetRequestStatus, false);
}
function SucessGetRequestStatus(RequestsStatus) {
    RequestStatus = RequestsStatus;
    for (var i = 0; i < RequestStatus.Data.length; i++) {
        $("#ApprovalsStatus").append('<option value="' + RequestStatus.Data[i].RequestStatusID + '">' + RequestStatus.Data[i].RequestStatusName + '</option>');
        $("#RequestStatus").append('<option value="' + RequestStatus.Data[i].RequestStatusID + '">' + RequestStatus.Data[i].RequestStatusName + '</option>');
    }
}
var FindRequest = function () {
    //Object Manager
    var RequestStatus = $("#RequestStatus")[0].value;
    var RequestLeaveType = $("#RequestLeaveType")[0].value;
    var RequestFrom = $("#RequestDateFrom")[0].value;
    var RequestTo = $("#RequestDateTo")[0].value;
    var NewSearch = {
        UserID: LoggedUserData.GlobalUserID,
        StatusID: RequestStatus,
        LeaveTypeID: RequestLeaveType,
        From: RequestFrom,
        To: RequestTo
    };
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Request/FiltterRequests";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(NewSearch), 'json', SucessSearchRequest, false);
};
function SucessSearchRequest(Result) {
    $('#LeavesListTable').DataTable().destroy();
    $('#LeavesListTable').DataTable({
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "RequestID" },//1
            { "data": "LeaveType" },//2
            {
                data: "CreationDate",//3
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            {
                data: "DurationFrom",//3
                "render": function (data, type, full, meta) {
                    if (LeaveDurationUnitEnum.Days == full.Unit)
                        return moment(data).format('DD/MM/YYYY ');
                    else {
                        return moment(data).format("DD/MM/YYYY hh:mm:ss A");
                    }
                }
            },
            {
                data: "DurationTo",//3
                "render": function (data, type, full, meta) {
                    if (LeaveDurationUnitEnum.Days == full.Unit)
                        return moment(data).format('DD/MM/YYYY ');
                    else {
                        return moment(data).format("DD/MM/YYYY hh:mm:ss A");
                    }
                }
            },
            {
                data: "BackToWork",//4
                "render": function (data, type, full, meta) {
                    if (LeaveDurationUnitEnum.Days == full.Unit)
                        return moment(data).format('DD/MM/YYYY ');
                    else {
                        return moment(data).format("DD/MM/YYYY hh:mm:ss A");
                    }
                }
            },
            { "data": "RequestStatus" },//2
            { "data": "RequestDuration" },//2
            { "data": "Unit" },//2
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<a href="/Request/ViewRequestsDetails?RequestID=' + data.RequestID + '">Open</a></td > <td>'
                }

            }

        ]
    })
   
}
var FindApproval = function () {
    //Object Manager
    var ApprovalsStatus = $("#ApprovalsStatus")[0].value;
    var ApprovalsLeaveType = $("#ApprovalsLeaveType")[0].value;
    var ApprovalFrom = $("#ApprovalDateFrom")[0].value;
    var ApprovalTo = $("#ApprovalDateTo")[0].value;
    var NewSearch = {
        UserID: LoggedUserData.GlobalUserID,
        StatusID: ApprovalsStatus,
        LeaveTypeID: ApprovalsLeaveType,
        From: ApprovalFrom,
        To: ApprovalTo
    };
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Request/FiltterApprovals";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(NewSearch), 'json', SucessSearchApproval, false);
};
function SucessSearchApproval(Result) {
    //alert(Result.Message);
    $("#LeavesRequestListTable").empty();
    for (var i = 0; i < Result.Data.length; i++) {
        $("#LeavesRequestListTable").append('<tr><td>' + Result.Data[i].RequestID + '</td><td>' + Result.Data[i].UserName + '</td><td>' + Result.Data[i].LeaveType
            + '</td><td>' + moment(Result.Data[i].CreationDate).format('DD/MM/YYYY hh:mm:ss A') + '</td><td>' + moment(Result.Data[i].DurationFrom).format('DD/MM/YYYY hh:mm:ss A') + '</td><td>'
            + moment(Result.Data[i].DurationTo).format('DD/MM/YYYY hh:mm:ss A')
            + '</td><td>' + moment(Result.Data[i].BackToWork).format('DD/MM/YYYY hh:mm:ss A') + '</td><td>' + Result.Data[i].RequestStatus + '</td><td>'
            + moment(Result.Data[i].RequestDuration).format('DD/MM/YYYY hh:mm:ss A')+
           '</td><td>' + Result.Data[i].Unit + '</td><td><a href="/Request/ViewRequestsDetails?RequestID=' + Result.Data[i].RequestID + '">Open</a></td><td>'
           + '</tr>'
            );
    }
}











