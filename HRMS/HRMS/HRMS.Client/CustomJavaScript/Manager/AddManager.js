var ManageID;
var SubordinatesCount;
var ManagerName;

$(document).ready(function () {
    GetAllManagers();
    GetAllSubDepartments();
    GetAllUsers();
});

//Selece Users To Be Manager
function GetAllManagers() {
    var urlGetAllManager = ConfigurationData.GlobalApiURL + "/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllManager, null, 'json', SucessGetAllManagers, false);
}


function SucessGetAllManagers(Managers) {
    for (var i = 0; i < Managers.Data.length; i++) {
        $("#SelectManagerDropDownList").append('<option ManagerID="' + Managers.Data[i].UserID + '" value="' + Managers.Data[i].UserName + '">' + Managers.Data[i].UserName + '</option>')
    }
}

//Append Manager In Table
$("#SelectManagerDropDownList").change(function () {

    ManagerName = $("#SelectManagerDropDownList").val();
    ManageID = $('option:selected', this).attr('ManagerID');
    $("#AppendManagersBody").empty();
    GetAllUsersUnderManager();
    $("#ManagerNameForUsers").text(ManagerName);
    $("#AppendManagersTable").append('<tr IDUser="' + ManageID + '" ><td style="width: 60%">' + ManagerName + '</td><td style="width:20%" SubOrdinateFor="' + ManagerName + '">' + SubordinatesCount + '</td>' +
        '<td style="width: 20%; text-align: right; color: #aaa;">' +
        ' <a href = "#" onclick="DeleteManagersTableRow(this)"><i class="fas fa-trash fa-2x" aria-hidden="true"></i></a>' +
        '</td ></tr> ');
});

function DeleteManagersTableRow(RawObject) {
    
    RawObject.closest('tr').remove();
}

//Get All Users Under Manager 
function GetAllUsersUnderManager() {
    var urlGetAllUsersUnderManager = ConfigurationData.GlobalApiURL + "/Users/GetALLUserDetailsByID?UserID=" + ManageID;
    Common.Ajax('get', urlGetAllUsersUnderManager, null, 'json', SucessGetAllUsersUnderManager, false);
}


function SucessGetAllUsersUnderManager(Users) {
   
    SubordinatesCount = "";
    if (Users.Data.Users.length > 0) {
        SubordinatesCount = Users.Data.Users.length;
        var Table = $("#UsersUnderManaerTable");
        Table[0].innerHTML = "";
        for (var i = 0; i < Users.Data.Users.length; i++) {
            Table.append('<tr deleteuserid="' + Users.Data.Users[i].NormalUsersId + '" > <td style="width: 80%">' + Users.Data.Users[i].NormalUsersName + '</td><td style="width: 20%; text-align: right; color: #aaa;"> <a href = "#" onclick="DeleteUserTableRow(this)"><i class="fas fa-trash fa-2x" aria-hidden="true"></i></a></td></tr >');
        }

        var TableSubDept = $("#AppendSubOrdinatesTable");
        TableSubDept[0].innerHTML = "";
        for (var j = 0; j < Users.Data.SubDepartments.length; j++) {
            TableSubDept.append('<tr SubDeptid="' + Users.Data.SubDepartments[j].SubDepartmentID + '"> <td style="width: 80%">' + Users.Data.SubDepartments[j].SubDepartmentName + '</td><td style="width: 20%; text-align: right; color: #aaa;"> <a href = "#" onclick="DeleteSubDeptTableRow(this)"><i class="fas fa-trash fa-2x" aria-hidden="true"></i></a></td></tr >');
        }
    }
    else {
        SubordinatesCount = "";
    }
}


//Get SubDepartments DropDownlist
function GetAllSubDepartments() {
    var urlGetAllSubDepartments = ConfigurationData.GlobalApiURL + "/SubDepartment/GetAllSubDepartments";
    Common.Ajax('get', urlGetAllSubDepartments, null, 'json', SucessurlGetAllSubDepartments, false);
}


function SucessurlGetAllSubDepartments(SubDepartments) {

    for (var i = 0; i < SubDepartments.Data.length; i++) {
        $("#AddsubordinateDropDpwnList").append('<option SubDeptID="' + SubDepartments.Data[i].SubDepartmentID + '" value="' + SubDepartments.Data[i].SubDepartmentName + '">' + SubDepartments.Data[i].SubDepartmentName + '</option>')
    }
}


$("#AddsubordinateDropDpwnList").change(function () {

    var SubOrdinatesName = $("#AddsubordinateDropDpwnList").val();
    var SubdepartmentID = $('option:selected', this).attr('subdeptid');
    $("#AppendSubOrdinatesTable").append('<tr  SubdepartmentId= "' + SubdepartmentID + '"><td style="width: 60%">' + SubOrdinatesName + '</td><td style="width: 20%; text-align: right; color: #aaa;">' +
        ' <a href = "#" onclick="DeleteUserRow(this)"><i class="fas fa-trash fa-2x" aria-hidden="true"></i></a>' +
        '</td ></tr> ');

});


function DeleteSubOrdinateTableRow(RawObject) {
    RawObject.closest('tr').remove();
}


function GetAllUsers() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetAllUsers, false);
}


function SucessGetAllUsers(Users) {
    for (var i = 0; i < Users.Data.length; i++) {
        $("#AppendUsersDropDownList").append('<option UserID="' + Users.Data[i].UserID + '" value="' + Users.Data[i].UserName + '">' + Users.Data[i].UserName + '</option>')
    }
}


$("#AppendUsersDropDownList").change(function () {

    var UserName = $("#AppendUsersDropDownList").val();
    var UserID = $('option:selected', this).attr('UserID');

    $("#UsersUnderManaerTable").append('<tr  UserId= "' + UserID + '"><td style="width: 60%">' + UserName + '</td><td style="width: 20%; text-align: right; color: #aaa;">' +
        ' <a href = "#" onclick="DeleteUserRow(this)"><i class="fas fa-trash fa-2x" aria-hidden="true"></i></a>' +
        '</td ></tr> ');
});

function DeleteUserRow(RawObject) {
    RawObject.closest('tr').remove();
}


function DeleteUserTableRow(RawObject) {

    RawObject.closest('tr').remove();
}


function SucessurlDeleteUserByID() {

}


function DeleteSubDeptTableRow(RawObject) {

    RawObject.closest('tr').remove();
}


var SaveListBTN = function () {
    //debugger;

    //Object Manager
    var rowCount = $("#AppendManagersTable >tbody >tr");
    var ManagerID = rowCount[0].attributes[0].value;


    //Object Users
    var ListUsersIDs = [];
    var Table = $("#UsersUnderManaerTable >tr");
    for (var i = 0; i < Table.length; i++) {
        ListUsersIDs.push(Table[i].attributes[0].value);
    }


    // Object SubDepartment
    var ListSubDepartmentIDs = [];
    var TableSubDepts = $("#AppendSubOrdinatesTable >tr");
    for (var j = 0; j < TableSubDepts.length; j++) {
        ListSubDepartmentIDs.push(TableSubDepts[j].attributes[0].value);
    }


    var ManagerToSubordinatesDTO = {
        UserID: ManagerID,
        ListUsersIDs: ListUsersIDs,
        ListSubDepartmentIDs: ListSubDepartmentIDs
    };
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Manager/AddPersonsAndSubDepartmentToManager";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(ManagerToSubordinatesDTO), 'json', SucessSubmitManager, false);
};


function SucessSubmitManager(Result) {
    // alert("User Added Successfully");
    //location.reload();
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        EmptyData();
        GetAllManagers();
        GetAllSubDepartments();
        GetAllUsers();
    }
    else {
        ShowALert(4, Result.Message);

    }


}

function EmptyData() {
    $("#SelectManagerDropDownList").empty();
    $("#AppendSubOrdinatesTable").empty();
    $("#AddsubordinateDropDpwnList").empty();
    $("#AppendSubOrdinatesTable").empty();
    $("#AppendManagersTable").empty();
    $("#UsersUnderManaerTable").empty();
}

$("#RestPage").click(function () {

    $("#UsersUnderManaerTable").empty();
    $("#AppendSubOrdinatesTable").empty();
    $("#AppendManagersBody").empty();
});

//function DecrimentSubordinatorNumber() {
//    debugger;

//    var ManagerNames = $("#ManagerNameForUsers").text(ManagerName);
//    var SubordnatesCell = $("#AppendManagersTable").find("[SubOrdinateFor='" + ManagerNames + "']");
//    var SubordnatesCount = SubordnatesCell.innerHTML();
//    SubordnatesCount--;
//    SubordnatesCell.innerHTML() = SubordnatesCount;
//}
