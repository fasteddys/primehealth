var radioValueType ="3";
var AllEmployeesList=0;
$(document).ready(function () {
    GetEmployeesList();
    ResetUserData();
    $("#LoadingDiv").delay(50).hide(0);
    $(".emoloyeeType").click(function () {
        radioValueType = $(this).val();
        console.log(radioValueType);
        AppendEmployeesList(AllEmployeesList);
        console.log(AllEmployeesList)

    })
});


//$(".clickable-row").on('click', '.new_participant_form', function () {
//$('body').delegate('.clickable-row', 'click', function () {
//    $(".EmployeeInfo").html(" - ");
//    if ($(this).hasClass("AciveRow"))
//        $(this).removeClass('AciveRow');
//    else
//        $(this).addClass('AciveRow').siblings().removeClass('AciveRow');
//    var SelecteUserID = $(this).children(":first")[0].id;
//    GetEmployeeData(SelecteUserID);
//    });



//function Click() {
//    $(".EmployeeInfo").html(" - ");
//    if ($(this).hasClass("AciveRow"))
//        $(this).removeClass('AciveRow');
//    else
//        $(this).addClass('AciveRow').siblings().removeClass('AciveRow');
//    var SelecteUserID = $(this).children(":first")[0].id;
//    GetEmployeeData(SelecteUserID);
//};

// function DoubleClick () {
//    $(".EmployeeInfo").html(" - ");
//    if ($(this).hasClass("AciveRow"))
//        $(this).removeClass('AciveRow');
//    else
//        $(this).addClass('AciveRow').siblings().removeClass('AciveRow');
//    var SelecteUserID = $(this).children(":first")[0].id;
//    GetEmployeeData(SelecteUserID);
//};





//var DELAY = 2000,
//    clicks = 0,
//    timer = null;
//var ClickSelecteUserID;

//var DELAY = 1, clicks = 0, timer = null;

//$(function () {

//    $(".clickable-row").on("click", function (e) {

//        clicks++;  //count clicks

//        if (clicks === 1) {

//            timer = setTimeout(function () {

//               // alert("Single Click");  //perform single-click action    
      
//               // $(".clickable-row").on('click', '.new_participant_form', function () {
//                $('body').delegate('.clickable-row', 'click', function () {
//                    $(".EmployeeInfo").html(" - ");
//                    if ($(this).hasClass("AciveRow"))
//                        $(this).removeClass('AciveRow');
//                    else
//                        $(this).addClass('AciveRow').siblings().removeClass('AciveRow');
//                    ClickSelecteUserID = $(this).children(":first")[0].id;
//                    GetEmployeeData(ClickSelecteUserID);
//                });
//               // });
    

//                clicks = 0;             //after action performed, reset counter

//            }, DELAY);

//        } else {

//            clearTimeout(timer);    //prevent single-click action
//           // alert("Double Click");  //perform double-click action
//            if ($(this).hasClass("AciveRow"))
//                $(this).removeClass('AciveRow');
//            else
//                $(this).addClass('AciveRow').siblings().removeClass('AciveRow');
//            var dbSelecteUserID = $(this).children(":first")[0].id;
//          //  if (dbSelecteUserID !==	 ClickSelecteUserID)
//                EditUserRedirect(dbSelecteUserID);
            
//            clicks = 0;             //after action performed, reset counter
//        }

//    })
//        .on("dblclick", function (e) {
//            e.preventDefault();  //cancel system double-click event
//        });

//});


function GetEmployeesList() {
    urlGetEmployeesList = ConfigurationData.GlobalApiURL+"/Users/GetAllUsers";
    Common.Ajax('GET', urlGetEmployeesList, null, 'json', SucessGetEmployees, false);
}

function SucessGetEmployees(EmployeesList) {
    AppendEmployeesList(EmployeesList);
}

function GetEmplyeeData(id) {
    GetEmployeeData(id);
    GetEntitlementsHistoryForUser(id);
}
function AppendEmployeesList(EmployeesList) {
  var UserID1 = LoggedUserData.GlobalUserID.toString() + "&UID=";

    if (AllEmployeesList != 0)
    {
        EmployeesList = AllEmployeesList;

    }
    else
    {
        AllEmployeesList = EmployeesList;
    }
    $('#datatableEmployeesList').DataTable().destroy();
    var Employees = filiterListOfEmployee(EmployeesList)
    $('#datatableEmployeesList').DataTable({
        retrieve: true,
        "data": Employees,
        "columns": [
            { "data": "UserID" },//1
            { "data": "UserName" },//2
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<td><a href="#" style="color:#007bff;" onclick="GetEmplyeeData(' + data.UserID +
                ')"><i title="View Employee Data" class="far fa-file-alt"></i></a> &nbsp <a style="color:#007bff;" href="/Configuration/EditEmployees?id=' +
                        data.UserID +
                '"><i title="Edit Employee" class="fas fa-user-edit"></i></a> &nbsp <a style="color:#007bff;" href="/Configuration/Entitlement?UserID='
                        + data.UserID +
                '"><i title="Adjust Entitlement" class="fas fa-battery-half"></i></a> &nbsp<a style="color:#007bff;" href ="/User/UserProfile?LID='
                        + UserID1 + data.UserID +
                '"><i title="View Profile" class="fas fa-address-card"></i></a></td ></tr';
                }
            }
        ]
    })
        //$("#EmployeesTable > tr").remove();
    //for (var i = 0; i < EmployeesList.Data.length; i++) {
    //    var IsAcctive = EmployeesList.Data[i].isActive; 
    //    if (radioValueType == "0") {
    //        var UserID = LoggedUserData.GlobalUserID.toString() + "&UID=";
    //        var EmployeeRawDate = '<tr class="clickable-row">'
    //            + '<td>' + EmployeesList.Data[i].UserID + '</td>'
    //            + '<td id=' + EmployeesList.Data[i].UserID + '>' + EmployeesList.Data[i].UserName + '</td>'
    //            + '<td><a href="#" style="color:#007bff;" onclick="GetEmplyeeData(' + EmployeesList.Data[i].UserID +
    //            ')"><i title="View Employee Data" class="far fa-file-alt"></i></a> &nbsp <a style="color:#007bff;" href="/Configuration/EditEmployees?id=' +
    //            EmployeesList.Data[i].UserID +
    //            '"><i title="Edit Employee" class="fas fa-user-edit"></i></a> &nbsp <a style="color:#007bff;" href="/Configuration/Entitlement?UserID='
    //            + EmployeesList.Data[i].UserID +
    //            '"><i title="Adjust Entitlement" class="fas fa-battery-half"></i></a> &nbsp<a style="color:#007bff;" href ="/User/UserProfile?LID='
    //            + UserID + EmployeesList.Data[i].UserID +
    //            '"><i title="View Profile" class="fas fa-address-card"></i></a></td ></tr>';
    //        $("#EmployeesTable").append(EmployeeRawDate);
    //    }
    //    else if (radioValueType == "1" && IsAcctive == 0)
    //    {
    //        var UserID = LoggedUserData.GlobalUserID.toString() + "&UID=";
    //        var EmployeeRawDate = '<tr class="clickable-row">'
    //            + '<td>' + EmployeesList.Data[i].UserID + '</td>'
    //            + '<td id=' + EmployeesList.Data[i].UserID + '>' + EmployeesList.Data[i].UserName + '</td>'
    //            + '<td><a href="#" style="color:#007bff;" onclick="GetEmplyeeData(' + EmployeesList.Data[i].UserID +
    //            ')"><i title="View Employee Data" class="far fa-file-alt"></i></a> &nbsp <a style="color:#007bff;" href="/Configuration/EditEmployees?id=' +
    //            EmployeesList.Data[i].UserID +
    //            '"><i title="Edit Employee" class="fas fa-user-edit"></i></a> &nbsp <a style="color:#007bff;" href="/Configuration/Entitlement?UserID='
    //            + EmployeesList.Data[i].UserID +
    //            '"><i title="Adjust Entitlement" class="fas fa-battery-half"></i></a> &nbsp<a style="color:#007bff;" href ="/User/UserProfile?LID='
    //            + UserID + EmployeesList.Data[i].UserID +
    //            '"><i title="View Profile" class="fas fa-address-card"></i></a></td ></tr>';
    //        $("#EmployeesTable").append(EmployeeRawDate);
    //    }
    //    else if (radioValueType == "2" && IsAcctive == 1) {
    //        ShowALert("Okey");
    //        var UserID = LoggedUserData.GlobalUserID.toString() + "&UID=";
    //        var EmployeeRawDate = '<tr class="clickable-row">'
    //            + '<td>' + EmployeesList.Data[i].UserID + '</td>'
    //            + '<td id=' + EmployeesList.Data[i].UserID + '>' + EmployeesList.Data[i].UserName + '</td>'
    //            + '<td><a href="#" style="color:#007bff;" onclick="GetEmplyeeData(' + EmployeesList.Data[i].UserID +
    //            ')"><i title="View Employee Data" class="far fa-file-alt"></i></a> &nbsp <a style="color:#007bff;" href="/Configuration/EditEmployees?id=' +
    //            EmployeesList.Data[i].UserID +
    //            '"><i title="Edit Employee" class="fas fa-user-edit"></i></a> &nbsp <a style="color:#007bff;" href="/Configuration/Entitlement?UserID='
    //            + EmployeesList.Data[i].UserID +
    //            '"><i title="Adjust Entitlement" class="fas fa-battery-half"></i></a> &nbsp<a style="color:#007bff;" href ="/User/UserProfile?LID='
    //            + UserID + EmployeesList.Data[i].UserID +
    //            '"><i title="View Profile" class="fas fa-address-card"></i></a></td ></tr>';
    //        $("#EmployeesTable").append(EmployeeRawDate);
    //    }


        
    //}
}
function filiterListOfEmployee(EmployeesList)
{
    var EmployeeFilitrationList=[];
  for (var i = 0; i < EmployeesList.Data.length; i++) {
        var IsAcctive = EmployeesList.Data[i].isActive;
        if (radioValueType == "0" && IsAcctive==-1) {//null employee
            EmployeeFilitrationList.push(EmployeesList.Data[i])
        }//null active
        else if (radioValueType == "1" && IsAcctive == 0)
        {
            EmployeeFilitrationList.push(EmployeesList.Data[i])
        }//all nonActive
        else if (radioValueType == "2" && IsAcctive == 1) {//All actve and true
          EmployeeFilitrationList.push(EmployeesList.Data[i])
        }
        else if (radioValueType == "3")//all Users
        {
            EmployeeFilitrationList.push(EmployeesList.Data[i])

        }
    }
  return EmployeeFilitrationList;
}
function GetEmployeeData(ID) {
    var urlGetEmployeeData = ConfigurationData.GlobalApiURL + "/Users/GetAllUserProfileData?LoggedUserID=" + LoggedUserData.GlobalUserID + "&UserID=" + ID;
    Common.Ajax('GET', urlGetEmployeeData, null, 'json', SucessGetEmployeeData, false);
}
function SucessGetEmployeeData(EmployeeData) {
    if (EmployeeData.Data !== null) {
        ResetUserData();
        AppendEmployeeData(EmployeeData);
    } else {
        ShowALert(4, "Employee data not available");
        //ShowALert(4, EmployeeData.Message);

    } 
        
}
function AppendEmployeeData(EmployeeData) {
    var time = '';
    if (EmployeeData.Data.BirthDate !== null) {
        time = EmployeeData.Data.BirthDate.split(' ');
        $("#BirthDate").html(time[0]);
        time = '';
    }
    if (EmployeeData.Data.HireDate !== null) {
        time = EmployeeData.Data.HireDate.split(' ');
        $("#HirDate").html(time[0]);
        time = '';
    }
    if (EmployeeData.Data.ProbationDate !== null) {
        time = EmployeeData.Data.ProbationDate.split(' ');
        $("#ProbationEnd").html(time[0]);
        time = '';
    } else {
        $("#ProbationEnd").hide();
        $("#ProbationEndCellLabel").hide();
    }
    if (EmployeeData.Data.TerminationDate !== null) {
        time = EmployeeData.Data.TerminationDate.split(' ');
        $("#TerminationDate").html(time[0]);
        time = '';
    } else {
        $("#TerminationDate").hide();
        $("#TerminationDateCellLabel").hide();
    }
    EmployeeData.Data.UserID !== null ? $("#CypressID").html(EmployeeData.Data.UserID).change() : $("#CypressID").html('None');
    EmployeeData.Data.UserName !== null ? $("#UserName").html(EmployeeData.Data.UserName).change() : $("#UserName").html('None');
    EmployeeData.Data.UserEmail !== null ? $("#Email").html(EmployeeData.Data.UserEmail).change() : $("#Email").html('None');

    EmployeeData.Data.ApprovalFlowName !== null ? $("#ApprovalFlow").html(EmployeeData.Data.ApprovalFlowName).change() : $("#ApprovalFlow").html('None');
    EmployeeData.Data.CompanyName !== null ? $("#Company").html(EmployeeData.Data.CompanyName).change() : $("#Company").html('None');
    EmployeeData.Data.DepartmentName !== null ? $("#Department").html(EmployeeData.Data.DepartmentName).change() : $("#Department").html('None');
    EmployeeData.Data.SubDepartmentName !== null ? $("#SubDepartment").html(EmployeeData.Data.SubDepartmentName).change() : $("#SubDepartment").html('None');
    EmployeeData.Data.UserName !== null ? $("#Position").html(EmployeeData.Data.positionName).change() : $("#Position").html('None');
    EmployeeData.Data.ContractTypeName !== null ? $("#ContractType").html(EmployeeData.Data.ContractTypeName).change() : $("#ContractType").html('None');
    
    EmployeeData.Data.Gender !== null ? $("#Gender").html(EmployeeData.Data.Gender).change() : $("#Gender").html('None');
    EmployeeData.Data.DirectManagerName !== null ? $("#DirectManger").html(EmployeeData.Data.DirectManagerName).change() : $("#DirectManger").html('None');
    EmployeeData.Data.TeamManagerName !== null && EmployeeData.Data.TeamManagerName !== EmployeeData.Data.LDAPUserName ? $("#TeamManager").html(EmployeeData.Data.TeamManagerName).change() : $("#TeamManager").html('None');
    //EmployeeData.Data.BirthDate != null ? $("#BirthDate").html(EmployeeData.Data.BirthDate).change() : null;
    //EmployeeData.Data.HireDate != null ? $("#HirDate").html(EmployeeData.Data.HireDate).change() : null;
    //EmployeeData.Data.ProbationDate != null ? $("#ProbationEnd").html(EmployeeData.Data.ProbationDate).change() : null;
    //EmployeeData.Data.TerminationDate != null ? $("#TerminationDate").html(EmployeeData.Data.TerminationDate).change() : null;
    EmployeeData.Data.SeniorityBeforeHireYear !== null ? $("#SeniorityBeforeHireYears").html(EmployeeData.Data.SeniorityBeforeHireYear).change() : $("#SeniorityBeforeHireYears").html('None');
    EmployeeData.Data.SeniorityBeforeHireMonth !== null ? $("#SeniorityBeforeHireMonths").html(EmployeeData.Data.SeniorityBeforeHireMonth).change() : $("#SeniorityBeforeHireMonths").html('None');
    EmployeeData.Data.BusinessPhone !== null ? $("#BusinessPhone").html(EmployeeData.Data.BusinessPhone).change() : $("#BusinessPhone").html('None');
    EmployeeData.Data.EmployeeNumber !== null ? $("#EmployeeNumber").html(EmployeeData.Data.EmployeeNumber).change() : $("#EmployeeNumber").html('None');
    EmployeeData.Data.NationalID !== null ? $("#NationalID").html(EmployeeData.Data.NationalID).change() : $("#NationalID").html('None');
    EmployeeData.Data.MedicalID !== null ? $("#MedicalID").html(EmployeeData.Data.MedicalID).change() : $("#MedicalID").html('None');
    EmployeeData.Data.CurrentAddress !== "" ? $("#CurrentAddress").html(EmployeeData.Data.CurrentAddress).change() : $("#CurrentAddress").html('None');
    EmployeeData.Data.HomeAddress !== null ? $("#HomeAddress").html(EmployeeData.Data.HomeAddress).change() : $("#HomeAddress").html('None');
    EmployeeData.Data.CustomeNote !== "" ? $("#CustomNote").html(EmployeeData.Data.CustomeNote).change() : $("#CustomNote").html('None');
}
function EditUser(UserId) {
    EditUserRedirect(UserId);
}
function EditUserRedirect(ID) {
    //urlGetEmployeeData = ConfigurationData.GlobalApiURL+"/Users/GetAllUserProfileData?UserID=" + ID;
    //Common.Ajax('GET', urlGetEmployeeData, null, 'json', SucessGetEmployeeData, false);
    var win = window.open('/Configuration/EditEmployees/' + ID, '_blank');
    win.focus();

    //urlGetEmployeeData = "/Configuration/EditEmployees?ID=" + ID;
    //Common.Ajax('GET', urlGetEmployeeData, null, 'json', null, false);
}

function SucessRedirect() {

}

$(document).ready(function () {
    $('#example').DataTable();
});

function ResetUserData() {
    $("#CypressID").html('None');
    $("#UserID").html('None');
    $("#EmployeeName").html('None');
    $("#HireDate").html('None');
    $("#Department").html('None');
    $("#SubDepartment").html('None');
    $("#Email").html('None');
    $("#Gender").html('None');
    $("#ProbationEnd").html('None');
    $("#TerminationDate").html('None');
    $("#BirthDate").html('None');
    $("#SenioritybeforeYears").html('None');
    $("#SenioritybeforeMonths").html('None');
    $("#DirectManager").html('None');
    $("#TeamManager").html('None');
    $("#Position").html('None');
    $("#BusinessPhone").html('None');
    $("#EmployeeNumber").html('None');
    $("#CustomeNote").html('None');
    $("#ApprovalFlow").html('None');
    $("#ContractType").html('None');
    $("#Company").html('None');
    $("#NationalID").html('None');
    $("#MedicalID").html('None');
    $("#CurrentAddress").html('None');
    $("#HomeAddress").html('None');
}

function GetEntitlementsHistoryForUser(ID) {
    var EntitlementChangeFilterObj = {
        UserID: ID,
        LeaveTypeID: '',
        From: '',
        To: ''
    };
    urlGetUserTimeAttendance = ConfigurationData.GlobalApiURL + "/UserEntitlement/FilterUserEntitlementChanges";
    Common.Ajax('POST', urlGetUserTimeAttendance, JSON.stringify(EntitlementChangeFilterObj), 'json', SucessGetUserEntitlementChangesForUser, false);
}
//Add DataTable
function SucessGetUserEntitlementChangesForUser(Result) {
    $('#EntitlementChangeTable').DataTable().destroy();
    $('#EntitlementChangeTable').DataTable({
        retrieve: true,
        "ordering": false,
        "data": Result.Data,
        "columns": [
            { "data": "LeaveTypeName" },//1
            { "data": "EntitlementBeforeTo" },//2
            { "data": "EntitlementChangedBy" },//3
            { "data": "Request" },//4
            { "data": "Comment" },//5
            {
                data: "ActionDate",//6
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            }
        ]
    });
}