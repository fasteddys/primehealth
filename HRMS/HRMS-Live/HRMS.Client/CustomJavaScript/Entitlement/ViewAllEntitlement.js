var isMonthly;

$(document).ready(function () {
    GetAllUsers();
    GetAllLeaveType();
    GetAllDepartments();
    GetAllEntiltementYears();
    //GetAllEntiltementMonths();
    $("#MonthEmptyDiv").show();
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

function IsMonthlyLeaveType(ID) {
    var urlIsMonthlyLeaveType = ConfigurationData.GlobalApiURL + "/LeaveType/IsMonthlyLeaveType?LeaveTypeID=" + ID;
    Common.Ajax('get', urlIsMonthlyLeaveType, null, 'json', SucessGetCheckLeaves, false);
}

function SucessGetCheckLeaves(Result) {
    if (Result.Success === true && Result.Data === true)
        isMonthly = true;
}

$("#EntitlementsLeaveType").change(function () {
    var ListLeaveTypeIDs = [];
    var SelectedValus = $('option:selected', this);//$("#EntitlementsLeaveType").find(":selected").val();
    isMonthly = false;

    for (var i = 0; i < SelectedValus.length; i++) {
        //ListLeaveTypeIDs.push(SelectedValus[i].value);

        IsMonthlyLeaveType(SelectedValus[i].value);

        if (isMonthly) {
            GetAllEntiltementMonths(SelectedValus[i].value);
            $("#MonthDiv").show();
            $("#MonthEmptyDiv").hide();
            break;
        }
        else {
            $("#MonthDiv").hide();
            $("#MonthEmptyDiv").show();
        }
    }

    
        
    //var urlGetUsers;

    //for (var i = 0; i < SelectedValus.length; i++) {
    //    ListSubDepartmentIDs.push(SelectedValus[i].value);
    //}
});

function GetAllDepartments() {
    var urlGetAllDepatments = ConfigurationData.GlobalApiURL + "/Department/GetAllDepartments";
    Common.Ajax('get', urlGetAllDepatments, null, 'json', SucessurlGetAllDepartments, false);
}

function SucessurlGetAllDepartments(Department) {
    for (var i = 0; i < Department.Data.length; i++) {
        $("#SelectDepartment").append('<option DepartmentID="' + Department.Data[i].DepartmentID + '" value="' + Department.Data[i].DepartmentID + '">' + Department.Data[i].DepartmentName + '</option>');
    }
}

function GetAllEntiltementYears() {
    var urlGetAllEntiltementYears = ConfigurationData.GlobalApiURL + "/UserEntitlement/GetAllEntiltementYears";
    Common.Ajax('get', urlGetAllEntiltementYears, null, 'json', SucessurlGetAllEntiltementYears, false);
}

function SucessurlGetAllEntiltementYears(Years) {
    for (var i = 0; i < Years.Data.length; i++) {
        $("#Year").append('<option value="' + Years.Data[i] + '">' + Years.Data[i] + '</option>');
    }
}

function GetAllEntiltementMonths(LeaveTypeID) {
    var urlGetAllEntiltementMonths = ConfigurationData.GlobalApiURL + "/UserEntitlement/GetAllEntiltementMonths?LeaveTypeID=" + LeaveTypeID;
    Common.Ajax('get', urlGetAllEntiltementMonths, null, 'json', SucessurlGetAllEntiltementMonths, false);
}

function SucessurlGetAllEntiltementMonths(Months) {
    $('#Month').empty();
    $('#Month').append("<option disabled selected></option>");

    for (var i = 0; i < Months.Data.length; i++) {
        $("#Month").append('<option value="' + Months.Data[i] + '">' + Months.Data[i] + '</option>');
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
        $("#UsersEntitlements").empty();
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
    //var DateFrom = $('#EntitlementsFrom').val();
    var Year = $('#Year').val();
    var Month = $('#Month').val();
    var LeaveTypeID = $('#EntitlementsLeaveType').val();
    var UserID = $('#UsersEntitlements').val();

    //GetEntitlementsHistoryForUser(DateFrom, DateTo, LeaveTypeID, UserID);

    if (Year === null || LeaveTypeID === null || (isMonthly && (Month === null)))
        ShowALert(4, "Please, insert required fields(*) before save");
    else {
        var RequestStatus = 1;//$("#RequestStatus")[0].value;
        var RequestLeaveType = $("#EntitlementsLeaveType")[0].value;
        //var From = $("#EntitlementsFrom")[0].value;
        //var To = $("#EntitlementsTo")[0].value;
        var ListSubDepartment = [];
        var ListDepartment = [];
        var ListLeaveType = [];


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
            Year: Year,
            Month: Month,
            //To: To,
            ListUsers: ListUsers,
            ListSubDepartment: ListSubDepartment,
            ListDepartment: ListDepartment
        };
        var urlGetUserByID = ConfigurationData.GlobalApiURL + "/UserEntitlement/HRFilterUserEntitlement";
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
    $('#EntitlementsTable').DataTable().destroy();
    $('#EntitlementsTable').DataTable({
        retrieve: true,

        "data": Result.Data,
        "columns": [
            { "data": "UserName" },
            { "data": "AccessControlID" },//2
            { "data": "LeaveTypeName" },
            { "data": "Year" },
            { "data": "Month" },
            { "data": "EntitlementTotal" }
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
    //var DateFrom = $('#EntitlementsFrom').val();
    var Year = $('#Year').val();
    var Month = $('#Month').val();
    var LeaveTypeID = $('#EntitlementsLeaveType').val();
    var UserID = $('#UsersEntitlements').val();

    //GetEntitlementsHistoryForUser(DateFrom, DateTo, LeaveTypeID, UserID);

    var RequestStatus = 1;//$("#RequestStatus")[0].value;
    var RequestLeaveType = $("#EntitlementsLeaveType")[0].value;
    //var From = $("#EntitlementsFrom")[0].value;
    //var To = $("#EntitlementsTo")[0].value;
    var ListSubDepartment = [];
    var ListDepartment = [];
    var ListUsers = [];
    var ListLeaveType = [];

    if (Year === null || LeaveTypeID === null || (isMonthly && (Month === null)))
        ShowALert(4, "Please, insert required fields(*) before save");
    else {
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
            Year: Year,
            Month: Month,
            //To: To,
            ListUsers: ListUsers,
            ListSubDepartment: ListSubDepartment,
            ListDepartment: ListDepartment
        };

        var urlGetUserByID = ConfigurationData.GlobalApiURL + "/UserEntitlement/HRFilterUserEntitlementExcel";
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
                a.download = "CypressEntitlementReport" + date + ".xls";
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



