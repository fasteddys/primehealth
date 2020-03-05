var Requests = [];
var Approvals = [];
var LeaveTypes = [];
var RequestStatus = [];
var RequestId;
$(document).ready(function () {
    //GetAllRequests();
    // GetAllApprovals();
    GetAllLeaveType();
    GetAllRequestStatus();
    GetAllDepartments();
    GetAllUsers();
    $("#btnSearchForRequest").on("click", function () {
         RequestId = $("#RequestNumber").val();
        console.log(RequestId)
        var urlRequestApproved= ConfigurationData.GlobalApiURL + "/Request/CheckIfRequestIsApproved?id=" + RequestId;
        Common.Ajax('get', urlRequestApproved, null, 'json', SucessGetRequestApproved, false);

    }) 

});
function SucessGetRequestApproved(result)
{
    if (result.Data == true)
    {
        //window.location.href = "/Request/ViewRequestsDetails?RequestID=" + RequestId;
        //window.location.assign == "/Request/ViewRequestsDetails?RequestID=" + RequestId;
        console.log(window.location.href)
        //var MianServer = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2]
        //console.log(window.location.href.split('/')[0])
        window.open(
            "/Request/ViewRequestsDetails?RequestID=" + RequestId,
            '_blank' // <- This is what makes it open in a new window.
        );

    }
    else
    {
        ShowALert(4, "This Request Not Approved or Not Founded");
    }
}
function GetAllRequests() {
    var urlGetAllRequests = ConfigurationData.GlobalApiURL + "/Request/GetALLUserRequests?UserID=" + 596;
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
    var urlGetAllApprovals = ConfigurationData.GlobalApiURL + "/Request/GetALLUserRequests?UserID=" + 596;
    Common.Ajax('get', urlGetAllApprovals, null, 'json', SucessGetApprovals, false);
}
function SucessGetApprovals(Approvals) {
    for (var i = 0; i < Approvals.Data.length; i++) {
        $("#ApprovalsTable").append('<tr><td>' + Approvals.Data[i].RequestID +
            '</td><td>' + Approvals.Data[i].UserName + '</td><td>' + Approvals.Data[i].LeaveType
            + '</td><td>' + moment(Approvals.Data[i].CreationDate).format('DD/MM/YYYY hh:mm:ss A') + '</td><td>' +
           
            moment(Approvals.Data[i].DurationFrom).format('DD/MM/YYYY hh:mm:ss A')
            + '</td><td>' + 
            moment(Approvals.Data[i].DurationTo).format('DD/MM/YYYY hh:mm:ss A')
            + '</td><td>' + 
            moment(Approvals.Data[i].BackToWork).format('DD/MM/YYYY hh:mm:ss A')
            + '</td><td>' + Approvals.Data[i].RequesStatus +
            '</td><td>' + Approvals.Data[i].RequestDuration +
            '</td><td>' + Approvals.Data[i].Unit
            + '</td><td></tr>'
        );
    }


}
function GetAllLeaveType() {
    var urlAllLeaveTypes = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes";
    Common.Ajax('get', urlAllLeaveTypes, null, 'json', SucessGetLeaves, false);
}
function SucessGetLeaves(Leaves) {
    LeaveTypes = Leaves;
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $("#RequestLeaveType").append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
        $("#ApprovalsLeaveType").append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
    }
}

function GetAllUsers() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetAllUsers, false);
}

function SucessGetAllUsers(Users) {
    for (var i = 0; i < Users.Data.length; i++) {
        $("#SelectUsers").append('<option value="' + Users.Data[i].UserID + '">' + Users.Data[i].UserName + '</option>');
    }
}

function GetAllRequestStatus() {
    SucessGetRequestStatus();
}
function SucessGetRequestStatus() {
    var Approved = {
        RequestStatusID: 1,
        RequestStatusName: "Approved"
    };
    //var Rejected = {
    //    RequestStatusID: 2,
    //    RequestStatusName: "Rejected"
    //};


    RequestStatus.push(Approved);
   // RequestStatus.push(Rejected);

    for (var i = 0; i < RequestStatus.length; i++) {
        $("#RequestStatus").append('<option value="' + RequestStatus[i].RequestStatusID + '">' + RequestStatus[i].RequestStatusName + '</option>');

    }


}
var FindRequest = function () {
    //Object Manager
    //var RequestStatus = 1//$("#RequestStatus")[0].value 2=reject 1=approved;
    var RequestLeaveType = $("#RequestLeaveType")[0].value;
    var From = $("#RequestDateFrom")[0].value;
    var To = $("#RequestDateTo")[0].value;
    var ListSubDepartment = [];
    var ListDepartment = [];


    var ListUsers = [];
    var SelectedUsers = $('option:selected', $("#SelectUsers"));

    for (var i = 0; i < SelectedUsers.length; i++) {
        ListUsers.push(SelectedUsers[i].value);
    }

    var SelectedDepartment = $('option:selected', $("#SelectDepartment"));

    for (var j = 0; j < SelectedDepartment.length; j++) {
        ListDepartment.push(SelectedDepartment[j].value);
    }

    var SelectedSubDepartment = $('option:selected', $("#SelectSubDepartment"));

    for (var k = 0; k < SelectedSubDepartment.length; k++) {
        ListSubDepartment.push(SelectedSubDepartment[k].value);
    }

    if (From == "" || To == "")
        ShowALert(1, "You Must Choices Date From and To");
    else
    {
        var NewSearch = {
            //StatusID: RequestStatus,
            LeaveTypeID: RequestLeaveType,
            From: From,
            To: To,
            ListUsers: ListUsers,
            ListSubDepartment: ListSubDepartment,
            ListDepartment: ListDepartment
        };
        var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Request/HRFiltterRequests";
        Common.Ajax('post', urlGetUserByID, JSON.stringify(NewSearch), 'json', SucessSearchRequest);
    }
  
};
function SucessSearchRequest(Result) {
    //alert(Result.Message);

    $("#ApprovalsTable").empty();



    //for (var i = 0; i < Result.Data.length; i++) {


    //    $("#ApprovalsTable").append('<tr><td>' + Result.Data[i].RequestID + '</td><td>' + Result.Data[i].UserName + '</td><td>' + Result.Data[i].LeaveType
    //        + '</td><td>' + moment(Result.Data[i].CreationDate).format('DD/MM/YYYY hh:mm:ss A')
    //        + '</td><td>' + moment(Result.Data[i].DurationFrom).format('DD/MM/YYYY hh:mm:ss A')
    //        + '</td><td>' + moment(Result.Data[i].DurationTo).format('DD/MM/YYYY hh:mm:ss A')
    //        + '</td><td>' + moment(Result.Data[i].BackToWork).format('DD/MM/YYYY hh:mm:ss A')
    //        + '</td><td>' + Result.Data[i].RequestStatus
    //        + '</td><td>' + Result.Data[i].RequestDuration +
    //        '</td><td>' + Result.Data[i].Unit + '</td><td><a href="/Request/ViewRequestsDetails?RequestID=' + Result.Data[i].RequestID + '">Open</a></td><td>'
    //        + '</tr>'
    //    );


   // }

    $('#TableData').DataTable().destroy();

$('#TableData').DataTable({
    data: Result.Data,
    columns: [
        {
            "data": "RequestID",
            "render": function (data, type, full, meta) {
                return "<a href='/Request/ViewRequestsDetails?RequestID=" + data + "'>" + data + "</a>";
            }
        },
        { "data": "UserName" },
        { "data": "AccessControlID" },
        { "data": "LeaveType" },
        {
            "render": function (data, type, full, meta) {
                //if (full.Unit === "Days") {
                //    return moment(full.CreationDate).format('DD/MM/YYYY');
                //}
                //else {
                    return moment(full.CreationDate).format('DD/MM/YYYY hh:mm:ss A');


                //}

            }
        },
        {
            "render": function (data, type, full, meta) {
                if (full.Unit === "Days") {
                    return moment(full.DurationFrom).format('DD/MM/YYYY');
                }
                else {
                    return moment(full.DurationFrom).format('DD/MM/YYYY hh:mm:ss A');


                }

            }
        },
        {
            "render": function (data, type, full, meta) {
                if (full.Unit === "Days") {
                    return moment(full.DurationTo).format('DD/MM/YYYY');
                }
                else {
                    return moment(full.DurationTo).format('DD/MM/YYYY hh:mm:ss A');


                }

            }
        },
        {
            "render": function (data, type, full, meta) {
                if (full.Unit ==="Days") {
                    return moment(full.BackToWork).format('DD/MM/YYYY');
                }
                else {
                    return moment(full.BackToWork).format('DD/MM/YYYY hh:mm:ss A');


                }

            }
        },
        { "data": "RequestDuration" },
        { "data": "Unit" },
        { "data": "PartialDurationUnit" },
        { "data": "PunchIn" },
        { "data": "PunchOut" }
        //{
        //    sortable: false,
        //    "render": function (data, type, full, meta) {
        //        return "<a href='/Request/ViewRequestsDetails?RequestID=" + full.RequestID + "'>Open</a>";
        //    }
        //}

    ]
    
   
    });





}
function SucessSearchApproval(Result) {
    //alert(Result.Message);

    $("#ApprovalsTable").empty();



    for (var i = 0; i < Result.Data.length; i++) {
        $("#ApprovalsTable").append('<tr><td>' + Result.Data[i].RequestID + '</td><td>' + Result.Data[i].UserName + '</td><td>' + Result.Data[i].LeaveType
            + '</td><td>' + Result.Data[i].CreationDate + '</td><td>' + Result.Data[i].DurationFrom + '</td><td>' + Result.Data[i].DurationTo
            + '</td><td>' + Result.Data[i].BackToWork + '</td><td>' + Result.Data[i].RequestStatus + '</td><td>' + Result.Data[i].RequestDuration +
            '</td><td>' + Result.Data[i].Unit + '</td><td><a href="/Request/ManageRequest?RequestID=' + Result.Data[i].RequestID + '">Open</a></td><td>'
            + '</tr>'
        );
    }
}
function GetAllDepartments() {
    var urlGetAllDepatments = ConfigurationData.GlobalApiURL + "/Department/GetAllDepartments";
    Common.Ajax('get', urlGetAllDepatments, null, 'json', SucessurlGetAllDepartments, false);
}
function SucessurlGetAllDepartments(Department) {
    for (var i = 0; i < Department.Data.length; i++) {
        $("#SelectDepartment").append('<option DepartmentID="' + Department.Data[i].DepartmentID + '" value="' + Department.Data[i].DepartmentID + '">' + Department.Data[i].DepartmentName + '</option>');
    }
}
$("#SelectDepartment").change(function () {

    var ListSubDepartmentIDs = [];
    var SelectedValus = $('option:selected', this);

    for (var i = 0; i < SelectedValus.length; i++) {
        ListSubDepartmentIDs.push(SelectedValus[i].value);
    }
    var urlGetSubDepartments = ConfigurationData.GlobalApiURL + "/SubDepartment/GetSubDepartmentByDepartmrntIDs";
    Common.Ajax('post', urlGetSubDepartments, JSON.stringify(ListSubDepartmentIDs), 'json', SucessurlGetSubDepartments, false);

    //Get Users By Department

    var ListDepartmentIDs = [];
    for (var i = 0; i < SelectedValus.length; i++) {
        ListDepartmentIDs.push(SelectedValus[i].value);
    }

    if (SelectedValus.length === 0) {
        GetAllUsers();
    }
    else {
        var urlGetUsers = ConfigurationData.GlobalApiURL + "/Users/GetUsersByDepartmrntIDs";
        Common.Ajax('post', urlGetUsers, JSON.stringify(ListDepartmentIDs), 'json', SucessurlGetUsers, false);
    }


});
function SucessurlGetSubDepartments(SubDepartment) {
    $("#SelectSubDepartment").empty();
    for (var i = 0; i < SubDepartment.Data.length; i++) {
        $("#SelectSubDepartment").append('<option SubDepartmentID="' + SubDepartment.Data[i].SubDepartmentID + '" value="' + SubDepartment.Data[i].SubDepartmentID + '">' + SubDepartment.Data[i].SubDepartmentName + '</option>');
    }
}
$("#SelectSubDepartment").change(function () {

    var ListSubDepartmentIDs = [];
    var SelectedValus = $('option:selected', this);
    var urlGetUsers;

    for (var i = 0; i < SelectedValus.length; i++) {
        ListSubDepartmentIDs.push(SelectedValus[i].value);
    }

    if (SelectedValus.length === 0) {
        var ListDepartmentIDs = [];
        SelectedValus = $('#SelectDepartment option:selected');
        for (var j = 0; j < SelectedValus.length; j++) {
            ListDepartmentIDs.push(SelectedValus[j].value);
        }

        urlGetUsers = ConfigurationData.GlobalApiURL + "/Users/GetUsersByDepartmrntIDs";
        Common.Ajax('post', urlGetUsers, JSON.stringify(ListDepartmentIDs), 'json', SucessurlGetUsers, false);
    }
    else {
        urlGetUsers = ConfigurationData.GlobalApiURL + "/SubDepartment/GetPersonsBySubDepartmentIDs";
        Common.Ajax('post', urlGetUsers, JSON.stringify(ListSubDepartmentIDs), 'json', SucessurlGetUsers, false);
    }

});
function SucessurlGetUsers(Users) {
    $("#SelectUsers").empty();
    for (var i = 0; i < Users.Data.length; i++) {
        $("#SelectUsers").append('<option UsersID="' + Users.Data[i].UserID + '" value="' + Users.Data[i].UserID + '">' + Users.Data[i].UserName + '</option>');
    }
}
//GetAllUserBySubDepartment
var ExtractExcel = function () {
    //Object Manager
    var RequestStatus = 1;//$("#RequestStatus")[0].value;
    var RequestLeaveType = $("#RequestLeaveType")[0].value;
    var From = $("#RequestDateFrom")[0].value;
    var To = $("#RequestDateTo")[0].value;
    var ListSubDepartment = [];
    var ListDepartment = [];


    var ListUsers = [];
    var SelectedUsers = $('option:selected', $("#SelectUsers"));

    for (var i = 0; i < SelectedUsers.length; i++) {
        ListUsers.push(SelectedUsers[i].value);
    }

    var SelectedDepartment = $('option:selected', $("#SelectDepartment"));

    for (var j = 0; j < SelectedDepartment.length; j++) {
        ListDepartment.push(SelectedDepartment[j].value);
    }

    var SelectedSubDepartment = $('option:selected', $("#SelectSubDepartment"));

    for (var k = 0; k < SelectedSubDepartment.length; k++) {
        ListSubDepartment.push(SelectedSubDepartment[k].value);
    }


    if (From == "" || To == "")
        ShowALert(1, "You Must Choices Date From and To");
    else
    {
        var NewSearch = {
            StatusID: RequestStatus,
            LeaveTypeID: RequestLeaveType,
            From: From,
            To: To,
            ListUsers: ListUsers,
            ListSubDepartment: ListSubDepartment,
            ListDepartment: ListDepartment
        };

    
        var today = new Date();
        var date = today.getFullYear() + '_' + (today.getMonth() + 1) + '_' + today.getDay() + "_" + today.getHours() + "_" + today.getMinutes() + "_" + today.getSeconds();

        var urlGetUserByID = ConfigurationData.GlobalApiURL+ "/Request/ExtractHRReportExecl";


        // Use XMLHttpRequest instead of Jquery $ajax
        xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var a;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                // Trick for making downloadable link
                a = document.createElement('a');
                a.href = window.URL.createObjectURL(xhttp.response);
                // Give filename you wish to download
                a.download = "CypressLeaveRequestReport" + date+".xls";
                a.style.display = 'none';
                document.body.appendChild(a);
                a.click();
            }
        };
        // Post data to URL which handles post request
        xhttp.open("POST", urlGetUserByID);
        xhttp.setRequestHeader("Content-Type", "application/json");
        // You should set responseType as blob for binary responses
        xhttp.responseType = 'blob';
        xhttp.send(JSON.stringify(NewSearch));
    }
};
var ExtractPDF = function () {

    //Object Manager
    var RequestStatus = 1;//$("#RequestStatus")[0].value;
    var RequestLeaveType = $("#RequestLeaveType")[0].value;
    var From = $("#RequestDateFrom")[0].value;
    var To = $("#RequestDateTo")[0].value;
    var ListSubDepartment = [];
    var ListDepartment = [];


    var ListUsers = [];
    var SelectedUsers = $('option:selected', $("#SelectUsers"));

    for (var i = 0; i < SelectedUsers.length; i++) {
        ListUsers.push(SelectedUsers[i].value);
    }

    var SelectedDepartment = $('option:selected', $("#SelectDepartment"));

    for (var j = 0; j < SelectedDepartment.length; j++) {
        ListDepartment.push(SelectedDepartment[j].value);
    }

    var SelectedSubDepartment = $('option:selected', $("#SelectSubDepartment"));

    for (var k = 0; k < SelectedSubDepartment.length; k++) {
        ListSubDepartment.push(SelectedSubDepartment[k].value);
    }

    if (From == "" || To == "")
        ShowALert(1, "You Must Choices Date From and To");

    else
    {
        var NewSearch = {
            StatusID: RequestStatus,
            LeaveTypeID: RequestLeaveType,
            From: From,
            To: To,
            ListUsers: ListUsers,
            ListSubDepartment: ListSubDepartment,
            ListDepartment: ListDepartment
        };


        var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Request/ExtractHRReportPDF";
        var today = new Date();
        var date = today.getFullYear() + '_' + (today.getMonth() + 1) + '_' + today.getDay() + "_" + today.getHours() + "_" + today.getMinutes() + "_" + today.getSeconds();


        // Use XMLHttpRequest instead of Jquery $ajax
        xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var a;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                // Trick for making downloadable link
                a = document.createElement('a');
                a.href = window.URL.createObjectURL(xhttp.response);
                // Give filename you wish to download
                a.download = "CypressLeaveRequestReport" + date + ".pdf";
                a.style.display = 'none';
                document.body.appendChild(a);
                a.click();
            }
        };
        // Post data to URL which handles post request
        xhttp.open("POST", urlGetUserByID);
        xhttp.setRequestHeader("Content-Type", "application/json");
        // You should set responseType as blob for binary responses
        xhttp.responseType = 'blob';
        xhttp.send(JSON.stringify(NewSearch));
    }
   
};
//validation input only
function validate(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}







