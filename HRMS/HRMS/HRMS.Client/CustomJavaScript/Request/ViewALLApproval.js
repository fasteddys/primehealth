var Requests = [];
var Approvals = [];
var LeaveTypes = [];
var RequestStatus = [];

$(document).ready(function () {
    GetAllLeaveType();
    GetAllRequestStatus();
    $("#ApprovalsStatus").val(3);
    FindApproval();

});

function SucessGetApprovals(Approvals) {
    for (var i = 0; i < Approvals.Data.length; i++) {
        $("#ApprovalsTable").append('<tr><td>' + Approvals.Data[i].RequestID + '</td><td>' + Approvals.Data[i].UserName + '</td><td>' + Approvals.Data[i].LeaveType
            + '</td><td>' + moment(Approvals.Data[i].CreationDate).format('DD/MM/YYYY hh:mm:ss A') + '</td><td>'
            + moment(Approvals.Data[i].DurationFrom).format('DD/MM/YYYY hh:mm:ss A') + '</td><td>'
            + moment(Approvals.Data[i].DurationTo).format('DD/MM/YYYY hh:mm:ss A')
            + '</td><td>' + 
            moment(Approvals.Data[i].BackToWork).format('DD/MM/YYYY hh:mm:ss A')
            + '</td><td>' + Approvals.Data[i].RequesStatus + '</td><td>' +
            moment(Approvals.Data[i].RequestDuration).format('DD/MM/YYYY hh:mm:ss A')   +
           '</td><td>' + Approvals.Data[i].Unit
           + '</td><td></tr>'
            );
    }


}
function GetAllLeaveType() {
    var urlAllLeaveTypes = ConfigurationData.GlobalApiURL+"/LeaveType/GetAllLeaveTypes";
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
var FindApproval = function () {
    debugger;
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
    }
    var urlGetUserByID = ConfigurationData.GlobalApiURL+"/Request/FiltterApprovals";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(NewSearch), 'json', SucessSearchApproval, false);
}
function SucessSearchApproval(Result) {
    $('#ApprovalsDataTable').DataTable().destroy();
    $('#ApprovalsDataTable').DataTable({
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "RequestID" },//1
            { "data": "UserName" },//2
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
                    if (LeaveDurationUnitEnum.Days==full.Unit)
                        return moment(data).format('DD/MM/YYYY ');
                    else
                    {
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
                    return '<a href="/Request/ManageRequest?RequestID=' + data.RequestID + '">Open</a></td > <td>'

                }

            }

        ]
    })
    
}











