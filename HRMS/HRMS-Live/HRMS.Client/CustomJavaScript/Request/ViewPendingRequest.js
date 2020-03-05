$(document).ready(function () {
    GetAllUsers();
    GetAllLeaveType();
    GetAllDepartments();
});
/*get all users*/
function GetAllUsers() {

    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessUrlGetUsers, false);
}
function SucessUrlGetUsers(Users) {
    $("#PendingRequestsUsers").empty();
    $("#PendingRequestsUsers").append("<option value=''  disabled selected >Select User</option>");
    $("#PendingRequestsUsers").append('<option value="0" style="color:red">All Users</option>');
    for (var i = 0; i < Users.Data.length; i++) {
        $("#PendingRequestsUsers").append('<option value="' + Users.Data[i].UserID + '">' + Users.Data[i].UserName + '</option>');
    }

}
/*-----------------------------------------------*/
/*get all leave type*/
function GetAllLeaveType() {
    var urlAllLeaveTypes = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes";
    Common.Ajax('get', urlAllLeaveTypes, null, 'json', SucessGetLeaves, false);
}
function SucessGetLeaves(Leaves) {
    LeaveTypes = Leaves;
    $("#PendingRequestsLeaveType").append("<option value=0 >All Leave Type</option>")
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $("#PendingRequestsLeaveType").append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
    }
}
/*-----------------------------------------------*/
/*get all departments*/
function GetAllDepartments() {
    var urlGetAllDepatments = ConfigurationData.GlobalApiURL + "/Department/GetAllDepartments";
    Common.Ajax('get', urlGetAllDepatments, null, 'json', SucessurlGetAllDepartments, false);
}
function SucessurlGetAllDepartments(Department) {
    for (var i = 0; i < Department.Data.length; i++) {
        $("#SelectDepartment").append('<option value="' + Department.Data[i].DepartmentID + '">' + Department.Data[i].DepartmentName + '</option>');
    }
}
$("#SelectDepartment").change(function () {
    var ListSubDepartmentIDs = [];
    var SelectedValus = $('option:selected', this);
    var urlGetUsers;
    for (var i = 0; i < SelectedValus.length; i++) {
        ListSubDepartmentIDs.push(SelectedValus[i].value);
    }
    var urlGetSubDepartments = ConfigurationData.GlobalApiURL + "/SubDepartment/GetSubDepartmentByDepartmrntIDs";
    Common.Ajax('post', urlGetSubDepartments, JSON.stringify(ListSubDepartmentIDs), 'json', SucessurlGetSubDepartments, false);

    //Get Users By Department

    var ListDepartmentIDs = [];
    for (var j = 0; j < SelectedValus.length; j++) {
        ListDepartmentIDs.push(SelectedValus[j].value);
    }

    if (SelectedValus.length === 0) {
        $("#SelectSubDepartment").empty();
        GetAllUsers();
    }

    else {
        urlGetUsers = ConfigurationData.GlobalApiURL + "/Users/GetUsersByDepartmrntIDs";
        Common.Ajax('post', urlGetUsers, JSON.stringify(ListDepartmentIDs), 'json', SucessUrlGetUsers, false);
    }

});
/*-----------------------------------------------*/
/*get SubDepartment*/
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
        Common.Ajax('post', urlGetUsers, JSON.stringify(ListDepartmentIDs), 'json', SucessUrlGetUsers, false);
    }
    else {
        urlGetUsers = ConfigurationData.GlobalApiURL + "/SubDepartment/GetPersonsBySubDepartmentIDs";
        Common.Ajax('post', urlGetUsers, JSON.stringify(ListSubDepartmentIDs), 'json', SucessUrlGetUsers, false);
    }


});
function SucessurlGetSubDepartments(SubDepartment) {
    $("#SelectSubDepartment").empty();
    for (var i = 0; i < SubDepartment.Data.length; i++) {
        $("#SelectSubDepartment").append('<option SubDepartmentID="' + SubDepartment.Data[i].SubDepartmentID + '" value="' + SubDepartment.Data[i].SubDepartmentID + '">' + SubDepartment.Data[i].SubDepartmentName + '</option>');
    }
}
/*-----------------------------------------------*/
/*Find Rejection Reasons*/
$("#FindPendingRequests").on("click", function () {
    var DateFrom = $('#PendingRequestsFrom').val();
    var DateTo = $('#PendingRequestsTo').val();
    var LeaveTypeID = $('#PendingRequestsLeaveType').val();
    var UserID = $('#PendingRequestsUsers').val();
    console.log(UserID)
    if (DateFrom == "" || DateTo == "")
        ShowALert(1, "You Must Choices Date From and To");
    else
    {
        var NewSearchForUserRejection =
            {
                RequestUserID: UserID,
                RequestForm: DateFrom,
                RequestTo: DateTo,
                RequestLeaveTypeID: LeaveTypeID,
                RequestStatus: 2
            }

        var urlGetRejectionReasonForUserByID = ConfigurationData.GlobalApiURL + "/Request/GetAllPendingRequest";//will be change
        Common.Ajax('post', urlGetRejectionReasonForUserByID, JSON.stringify(NewSearchForUserRejection), 'json', SucessurlGetRejectionReasonForUserByID);

    }
   })
function SucessurlGetRejectionReasonForUserByID(Result) {
    $('#PendingRequestsTable').DataTable().destroy();
    $('#PendingRequestsTable').DataTable({
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "RequestID" },//1
            { "data": "UserName" },//2
            { "data": "AccessControlID" },//2
            {
                data: "RequestCreateDate",//3
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            { "data": "LeaveTypeName" },//8
            {
                data: "RequestStartDate",//4
                //"render": function (data)
                //{
                //    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                //}

            },
            {
                data: "RequestEndDate",//5
                //"render": function (data) {
                //    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                //}

            },
            { "data": "RequestDuration" },//6
            { "data": "RequestDurationUnitName" },//7
            { "data": "ManagerName" },//9
            { "data": "PendingFrom" }//10
        ]
    })
}
/*-----------------------------------------------*/
/*Report*/
$('#ExtractPendingRequests').click(function () {
    /*get rejection reason for a user*/
    var DateFrom = $('#PendingRequestsFrom').val();
    var DateTo = $('#PendingRequestsTo').val();
    var LeaveTypeID = $('#PendingRequestsLeaveType').val();
    var userName = $('#PendingRequestsUsers :selected').text();
    var UserID = $('#PendingRequestsUsers').val();
    if (DateFrom == "" || DateTo == "")
        ShowALert(1, "You Must Choices Date From and To");
    else
    {

        var NewSearch = {
            RequestUserID: UserID,
            RequestForm: DateFrom,
            RequestTo: DateTo,
            RequestLeaveTypeID: LeaveTypeID,
            RequestStatus: 2
        };
        var urlGetRejectionReasonForUserByIDExcel = ConfigurationData.GlobalApiURL + "/Request/GetAllPendingRequestExcel";
        //var urlGetRejectionReasonForUserByID = ConfigurationData.GlobalApiURL + "/Request/GetAllRejectionReason";
        //Common.Ajax('post', urlGetRejectionReasonForUserByID, JSON.stringify(NewSearchForUserRejection), 'json', SucessurlGetRejectionReasonForUserByID, false);
        ///*----------------------------*/
        var today = new Date();
        var date = "_" + userName + "_" + today.getFullYear() + '_' + (today.getMonth() + 1) + '_' + today.getDay() + "_" + today.getHours() + "_" + today.getMinutes() + "_" + today.getSeconds();
        xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var a;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                // Trick for making downloadable link
                a = document.createElement('a');
                a.href = window.URL.createObjectURL(xhttp.response);
                // Give filename you wish to download
                a.download = "CypressPendingReport" + date + ".xls";
                a.style.display = 'none';
                document.body.appendChild(a);
                a.click();
            }
        };
        // Post data to URL which handles post request
        xhttp.open("POST", urlGetRejectionReasonForUserByIDExcel);
        xhttp.setRequestHeader("Content-Type", "application/json");
        // You should set responseType as blob for binary responses
        xhttp.responseType = 'blob';
        xhttp.send(JSON.stringify(NewSearch));
    }
});
/*-----------------------------------------------*/
