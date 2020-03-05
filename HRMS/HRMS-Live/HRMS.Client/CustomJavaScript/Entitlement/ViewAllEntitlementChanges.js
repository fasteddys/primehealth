$(document).ready(function () {
    GetAllUsers();
    GetAllLeaveType();
    GetAllDepartments();
    GetAllChangedBy();
});

function GetAllUsers() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetAllUsers, false);
}

function SucessGetAllUsers(Users) {
    for (var i = 0; i < Users.Data.length; i++) {
        $("#UsersEntitlements").append('<option value="' + Users.Data[i].UserID + '">' + Users.Data[i].UserName + '</option>');
    }
}

function GetAllLeaveType() {
    var urlAllLeaveTypes = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes";
    Common.Ajax('get', urlAllLeaveTypes, null, 'json', SucessGetLeaves, false);
}

function SucessGetLeaves(Leaves) {
    LeaveTypes = Leaves;
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $("#EntitlementsLeaveType").append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
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

function GetAllChangedBy() {
    var urlGetAllChangedBy = ConfigurationData.GlobalApiURL + "/UserEntitlement/GetAllEntitlementChangedBy";
    Common.Ajax('get', urlGetAllChangedBy, null, 'json', SucessurlGetAllChangedBy, false);
}

function SucessurlGetAllChangedBy(Result) {
    for (var i = 0; i < Result.Data.length; i++) {
        $("#ChangedBy").append('<option value="' + Result.Data[i].EntitlementChangedByID + '" value="' + Result.Data[i].EntitlementChangedByName + '">' + Result.Data[i].EntitlementChangedByName + '</option>');
    }
}

$("#EntitlementsLeaveType").change(function () {
    var ListLeaveTypeIDs = [];
    var SelectedValus = $('option:selected', this);

    for (var i = 0; i < SelectedValus.length; i++) {
        ListLeaveTypeIDs.push(SelectedValus[i].value);
    }
});

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
        $("#UsersEntitlements").empty();
        $("#SelectSubDepartment").empty();
        GetAllUsers();
    }

    else {
        urlGetUsers = ConfigurationData.GlobalApiURL + "/Users/GetUsersByDepartmrntIDs";
        Common.Ajax('post', urlGetUsers, JSON.stringify(ListDepartmentIDs), 'json', SucessurlGetUsers, false);
    }

});

function SucessurlGetSubDepartments(SubDepartment) {
    //$("#SelectSubDepartment").empty();
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
    $("#UsersEntitlements").empty();
    for (var i = 0; i < Users.Data.length; i++) {
        $("#UsersEntitlements").append('<option UsersID="' + Users.Data[i].UserID + '" value="' + Users.Data[i].UserID + '">' + Users.Data[i].UserName + '</option>');
    }
}


$('#FindEntitlements').click(function () {
    var DateFrom = $('#EntitlementsFrom').val();
    var DateTo = $('#EntitlementsTo').val();
    var LeaveTypeID = $('#EntitlementsLeaveType').val();
    var EntitlementChangedByID = $('#ChangedBy').val();
    var UserID = $('#UsersEntitlements').val();
    var RequestStatus = 1;//$("#RequestStatus")[0].value;
    var RequestLeaveType = $("#EntitlementsLeaveType")[0].value;

    var From = $("#EntitlementsFrom")[0].value;
    var To = $("#EntitlementsTo")[0].value;
    var ListSubDepartment = [];
    var ListDepartment = [];
    var ListLeaveType = [];

    //GetEntitlementsHistoryForUser(DateFrom, DateTo, LeaveTypeID, UserID);

    if (DateFrom === '' || DateTo === '' || LeaveTypeID.length ==0)
        ShowALert(1, "Please, insert required fields(*) before save");
    else {
        var ListUsers = [];
        var SelectedUsers = $('option:selected', $("#UsersEntitlements"));

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

        var SelectedLeaveType = $('option:selected', $("#EntitlementsLeaveType"));
        for (var c = 0; c < SelectedLeaveType.length; c++) {
            ListLeaveType.push(SelectedLeaveType[c].value);
        }

        var NewSearch = {
            StatusID: RequestStatus,
            ListLeaveType: ListLeaveType,
            EntitlementChangedByID: EntitlementChangedByID,
            From: From,
            To: To,
            ListUsers: ListUsers,
            ListSubDepartment: ListSubDepartment,
            ListDepartment: ListDepartment
        };
        var urlGetUserByID = ConfigurationData.GlobalApiURL + "/UserEntitlement/HRFilterUserEntitlementChanges";
        Common.Ajax('post', urlGetUserByID, JSON.stringify(NewSearch), 'json', SucessGetUserEntitlementChangesForUser);
    }
    

    
});

//function GetEntitlementsHistoryForUser(From, To, Type, UserID) {
//    var EntitlementChangeFilterObj = {
//        UserID: UserID,
//        LeaveTypeID: Type,
//        From: From,
//        To: To
//    };
//    urlGetUserTimeAttendance = ConfigurationData.GlobalApiURL + "/UserEntitlement/FilterUserEntitlementChanges";
//    Common.Ajax('POST', urlGetUserTimeAttendance, JSON.stringify(EntitlementChangeFilterObj), 'json', SucessGetUserEntitlementChangesForUser, false);
//}

function SucessGetUserEntitlementChangesForUser(Result) {
    $('#EntitlementsListTable').DataTable().destroy();
    $('#EntitlementsListTable').DataTable({
        retrieve: true,

        "data": Result.Data,
        "columns": [
            { "data": "UserName" },
            { "data": "AccessControlID" },//2
            { "data": "LeaveTypeName" },
            { "data": "EntitlementBefore" },
            { "data": "EntitlementTo" },
            { "data": "EntitlementChangedBy" },
            { "data": "Request" },
            { "data": "RequestDuration" },
            { "data": "UserChangeEntitlement" },
            {
                data: "ActionDate",
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }   
            },
            { "data": "Comment" }
        ]
    });

    //$("#EntitlementsListTable").empty();
    //for (var i = 0; i < Result.Data.length; i++) {
    //    var ActionDate = Result.Data[i].ActionDate !== null ? Result.Data[i].ActionDate : '-----';
    //    var TARowDate = '<tr>' +
    //        '<td>' + Result.Data[i].UserName + '</td>' +
    //        '<td>' + Result.Data[i].LeaveTypeName + '</td>' +
    //        //'<td>' + moment(Result.Data[i].DurationFrom).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        //'<td>' + moment(Result.Data[i].DurationTo).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        //'<td>' + moment(Result.Data[i].BackToWork).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        '<td>' + Result.Data[i].EntitlementBefore + '</td>' +
    //        '<td>' + Result.Data[i].EntitlementTo + '</td>' +
    //        '<td>' + Result.Data[i].EntitlementChangedBy + '</td>' +
    //        '<td>' + Result.Data[i].RequestDuration + '</td>' +
    //        '<td>' + Result.Data[i].UserChangeEntitlement + '</td>' +
    //        '<td>' + moment(ActionDate).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        '<td>' + Result.Data[i].Comment + '</td>' +
    //        '</tr >';
    //    $("#EntitlementsListTable").append(TARowDate);
    //}
}

$('#ExtractEntitlementsChanges').click(function () {
    var DateFrom = $('#EntitlementsFrom').val();
    var DateTo = $('#EntitlementsTo').val();
    var LeaveTypeID = $('#EntitlementsLeaveType').val();
    var UserID = $('#UsersEntitlements').val();
    var EntitlementChangedByID = $('#ChangedBy').val();

    //GetEntitlementsHistoryForUser(DateFrom, DateTo, LeaveTypeID, UserID);

    var RequestStatus = 1;//$("#RequestStatus")[0].value;
    var RequestLeaveType = $("#EntitlementsLeaveType")[0].value;
    var From = $("#EntitlementsFrom")[0].value;
    var To = $("#EntitlementsTo")[0].value;
    var ListSubDepartment = [];
    var ListDepartment = [];
    var ListLeaveType = [];

    if (DateFrom === '' || DateTo === '' || LeaveTypeID === null)
        ShowALert(1, "Please, insert required fields(*) before save");
    else {
        var ListUsers = [];
        var SelectedUsers = $('option:selected', $("#UsersEntitlements"));

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

        var SelectedLeaveType = $('option:selected', $("#EntitlementsLeaveType"));
        for (var c = 0; c < SelectedLeaveType.length; c++) {
            ListLeaveType.push(SelectedLeaveType[c].value);
        }

        var NewSearch = {
            StatusID: RequestStatus,
            ListLeaveType: ListLeaveType,
            EntitlementChangedByID: EntitlementChangedByID,
            From: From,
            To: To,
            ListUsers: ListUsers,
            ListSubDepartment: ListSubDepartment,
            ListDepartment: ListDepartment
        };
        var urlGetUserByID = ConfigurationData.GlobalApiURL + "/UserEntitlement/HRFilterUserEntitlementChangesExcel";
        //Common.Ajax('post', urlGetUserByID, JSON.stringify(NewSearch), 'json', SucessGetUserEntitlementChangesForUser, false);

        var today = new Date();
        var date = today.getFullYear() + '_' + (today.getMonth() + 1) + '_' + today.getDay() + "_" + today.getHours() + "_" + today.getMinutes() + "_" + today.getSeconds();

        xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            var a;
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                // Trick for making downloadable link
                a = document.createElement('a');
                a.href = window.URL.createObjectURL(xhttp.response);
                // Give filename you wish to download
                a.download = "CypressEntitlementChangeReport" + date + ".xls";
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


});



