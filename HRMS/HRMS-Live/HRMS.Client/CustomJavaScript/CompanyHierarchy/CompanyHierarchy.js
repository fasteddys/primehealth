var ManagerID;
var DepartmentID;
var NewSubDepartmentName;

$(document).ready(function () {
    GetAllSubDepartments();
    GetAllDepartments();
    GetAllManagers();
    GetAllDepartment(); 
});

//SubDepartment Company Hierarcky TAB ------------------------------------------------------------

// #region SubDepartment 

function GetAllSubDepartments() {
    var urlGetAllTeams = ConfigurationData.GlobalApiURL+"/SubDepartment/GetAllSubDepartments";
    Common.Ajax('get', urlGetAllTeams, null, 'json', SucessurlGetAllTeams, false);
}

function SucessurlGetAllTeams(Teams) {
    $('#datatableSubDepartment').DataTable().destroy();
    $('#datatableSubDepartment').DataTable({
        retrieve: true,
        "data": Teams.Data,
        "columns": [
            { "data": "SubDepartmentName" },//1
            { "data": "UsersCount" },//2
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button id="showUser" class="btn btn-info btn-sm" onclick="GetSubDepartmentByID(' + data.SubDepartmentID + ')">' + 'Display Users' + '</button>';
                }

            }
        ],
    })

    //var TableTeam = $("#SubDepartmentTable");
    
    //TableTeam.empty();
    //for (var i = 0; i < Teams.Data.length; i++) {
    //    TableTeam.append('<tr class="clickable-row" onclick="GetSubDepartmentByID(' + Teams.Data[i].SubDepartmentID + ')"><td>' + Teams.Data[i].SubDepartmentName + '</td><td>' + Teams.Data[i].UsersCount + '</td></tr>');
    //}

}

function GetSubDepartmentByID(SubDepartmentID) {
    var urlGetSubDepartmentByID = ConfigurationData.GlobalApiURL+"/SubDepartment/GetSubDepartmentByID?SubDepartmentID=" + SubDepartmentID;
    Common.Ajax('get', urlGetSubDepartmentByID, null, 'json', SucessGetSubDepartmentByID, false);
}

function SucessGetSubDepartmentByID(PersonsOfSubDepartment) {
    
    var urlGetUsersByID = ConfigurationData.GlobalApiURL+"/SubDepartment/GetPersonsBySubDepartmentID?SubDepartmentID=" + PersonsOfSubDepartment.Data.SubDepartmentID;
    Common.Ajax('get', urlGetUsersByID, null, 'json', SucessGetUsersBySubDepartmentID, false);
}

function SucessGetUsersBySubDepartmentID(UsersDetails) {
    $('#dataTableUsers').DataTable().destroy();
    $('#dataTableUsers').DataTable({
        retrieve: true,
        "data": UsersDetails.Data,
        "columns": [
            { "data": "UserName" },//2
        ],
    })

    //var TableUsersListTable = $("#UsersTable");
    //TableUsersListTable.empty();
    //for (var i = 0; i < UsersDetails.Data.length+100; i++) {

    //    TableUsersListTable.append('<tr class="clickable-row"><td>' + UsersDetails.Data[0].UserName + '</td></tr>');
    //}
}

function GetAllDepartments() {
    var urlGetAllDepatments = ConfigurationData.GlobalApiURL+"/Department/GetAllDepartments";
    Common.Ajax('get', urlGetAllDepatments, null, 'json', SucessurlGetAllDepartments, false);
}

function SucessurlGetAllDepartments(Department) {
    for (var i = 0; i < Department.Data.length; i++) {
        $("#SelectDepartment").append('<option DepartmentID="' + Department.Data[i].DepartmentID + '" value="' + Department.Data[i].DepartmentName + '">' + Department.Data[i].DepartmentName + '</option>')
    }
}

function GetAllManagers() {
    var urlGetAllManager = ConfigurationData.GlobalApiURL+"/Manager/GetAllManagers";
    Common.Ajax('get', urlGetAllManager, null, 'json', SucessGetAllManagers, false);
}

function SucessGetAllManagers(Managers) {
    for (var i = 0; i < Managers.Data.length; i++) {
        $("#SelectTeamManager").append('<option ManagerID="' + Managers.Data[i].ManagerID + '" value="' + Managers.Data[i].ManagerName + '">' + Managers.Data[i].ManagerName + '</option>')
    }
}

var NewSubDept = {};
var SaveSubDepartmentBTN = function () {

    NewSubDepartmentName = $("#AddSubDepartment").val();
    if (NewSubDepartmentName !=="") {
        DepartmentID = $("#SelectDepartment option:selected")[0].attributes.DepartmentId.value;
        ManagerID = $("#SelectTeamManager option:selected")[0].attributes.ManagerId.value;

        NewSubDept = {
            TeamManagerFK: ManagerID,
            DepartmentFK: DepartmentID,
            SubDepartmentName: NewSubDepartmentName,
        }
        var urlGetNewSubDept = ConfigurationData.GlobalApiURL+"/SubDepartment/AddNewSubDepartment";
        Common.Ajax('Post', urlGetNewSubDept, JSON.stringify(NewSubDept), 'json', SucessAddNewSubDept, false);

    }
    //else {
            
    //}
  
}

function SucessAddNewSubDept(Result) {
    if (Result.Success === true) {
        $("#AddSubDepartmentModal").modal("hide");
        ShowALert(2, Result.Message);
        GetAllSubDepartments();
    }
    else {
        ShowALert(4, Result.Message);

    }
}


// #endregion SubDepartment

// Department Company Hierarcky TAB --------------------------------------------------------

// #region Department 

function GetAllDepartment() {
    var urlGetAllDepartment = ConfigurationData.GlobalApiURL+"/Department/GetAllDepartments";
    Common.Ajax('get', urlGetAllDepartment, null, 'json', SucesssurlGetAllDepartments, false);
}

function SucesssurlGetAllDepartments(Details) {

    $('#dataTableDepartment').DataTable().destroy();
    $('#dataTableDepartment').DataTable({
        retrieve: true,
        "data": Details.Data,
        "columns": [
            { "data": "DepartmentName" },//1
            { "data": "SubDepartmentCount" },//1
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button id="showUser" class="btn btn-info btn-sm" onclick="GetAllSubDepartmentByDepartmentID(' + data.DepartmentID + ')">' + 'Display SubDepartments' + '</button>';
                }

            }
        ],
    })
    //var Table = $("#DepartmentTable");
    //for (var i = 0; i < Details.Data.length; i++) {
    //    Table.append('<tr class="clickable-row" onclick="GetAllSubDepartmentByDepartmentID(' + Details.Data[i].DepartmentID + ')"><td>' + Details.Data[i].DepartmentName + '</td><td>' + Details.Data[i].SubDepartmentCount + '</td></tr>');
    //}
}


function GetAllSubDepartmentByDepartmentID(ID) {
    var urlGetDepartmentByID = ConfigurationData.GlobalApiURL+"/Department/GetDepartmentByID?DepartmentID=" + ID;
    Common.Ajax('get', urlGetDepartmentByID, null, 'json', SucesssGetSubDepartmentByID, false);
}

function SucesssGetSubDepartmentByID(SubDepartmentsOfDepartment) {

    var urlGetDepartmentByID = ConfigurationData.GlobalApiURL+"/SubDepartment/GetSubDepartmentByDepartmentID?DepartmentID=" + SubDepartmentsOfDepartment.Data.DepartmentID;
    Common.Ajax('get', urlGetDepartmentByID, null, 'json', SucesssGetDepartmentByID, false);
}


function SucesssGetDepartmentByID(SubDeptDetails) {

    $('#SubDataTableSubDepartments').DataTable().destroy();
    $('#SubDataTableSubDepartments').DataTable({
        retrieve: true,
        "data": SubDeptDetails.Data,
        "columns": [
            { "data": "SubDepartmentName" },//1
        ],
    })
    //var TableUsersListTable = $("#DeptTable");
    //TableUsersListTable.empty();
    //for (var i = 0; i < SubDeptDetails.Data.length; i++) {

    //    TableUsersListTable.append('<tr class="clickable-row"><td>' + SubDeptDetails.Data[i].SubDepartmentName + '</td></tr>');
    //}
}


var SaveDepartmentBTN = function () {

    var NewDepartmentName = $("#AddDepartment").val();
    if (NewDepartmentName !== "") {
        var NewDepartment = {
            DepartmentName: NewDepartmentName,
        }
        var newDept = NewDepartment;
        var urlGetNewDepartment = ConfigurationData.GlobalApiURL+"/Department/AddNewDepartment";
        Common.Ajax('Post', urlGetNewDepartment, JSON.stringify(newDept), 'json', SucessAddNewDepartment, false);

    }
    else {

    }
    
}

function SucessAddNewDepartment(Result) {
    if (Result.Success === true) {
     $("#AddDepartmentModal").modal("hide");
        ShowALert(2, Result.Message);
        GetAllDepartment();
    }
    else {
        ShowALert(4, Result.Message);

    }
}
// #endregion SubDepartment
