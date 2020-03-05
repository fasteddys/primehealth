$(document).ready(function () {
    GetAllUsersSubDepartment();
    GetAllSubDepartments();
    //GetAllDepartments();
    GetAllUsersDepartment();
    GetAllDepartmentsDropDown();
});

// #region Subdepartment  
function GetAllUsersSubDepartment() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL+"/Users/GetAllUsersSubDepartment";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetAllUsers, false);
}

function SucessGetAllUsers(UsersDetails) {

    var Table = $("#UserslistTable");
    for (var i = 0; i < UsersDetails.Data.length; i++) {

        if (UsersDetails.Data[i].UserSubDepartmentName === null) {

            UsersDetails.Data[i].UserSubDepartmentName = "---------------------";
        }

        Table.append('<tr class="clickable-row"><td><div class="checkboxContainer"><input type="checkbox" UserId="'
            + UsersDetails.Data[i].UserID + '" class="chkDelete"> <label for="null"><span></span></label></div></td><td>'
            + UsersDetails.Data[i].UserName + '</td><td>' + UsersDetails.Data[i].UserEmail + '</td></tr>');
    }
   
}

function GetAllSubDepartments() {
    var urlGetAllPosition = ConfigurationData.GlobalApiURL+"/SubDepartment/GetAllSubDepartments";
    Common.Ajax('get', urlGetAllPosition, null, 'json', SucessGetAllSubDepartments, false);
}

function SucessGetAllSubDepartments(SubDepartmentDropDownList) {
    for (var i = 0; i < SubDepartmentDropDownList.Data.length; i++) {
        $("#SelectSubDepartmentDL").append('<option  SubDepaermentID="' + SubDepartmentDropDownList.Data[i].SubDepartmentID + '" value="' + SubDepartmentDropDownList.Data[i].SubDepartmentName + '">' + SubDepartmentDropDownList.Data[i].SubDepartmentName + '</option>');
    }
}



// #region Save Link Users To Subdepartment 
var SubDepartment;
var SaveListSubDeptBTN = function () {
    var Idstemp = {};
    var UsersID = [];
    var all, checked, notChecked, Table;

    all = $("#UserslistTable input:checkbox");
    checked = all.filter(":checked");
    Table = $("#UserslistTable");
    var SubDepaermentID = $("#SelectSubDepartmentDL option:selected")[0].attributes.SubDepaermentID.value;
    //var DepaermentID = $("#SelectDepartmentDL option:selected")[0].attributes.DepaermentID.value;

    SubDepartment = $("#SelectSubDepartmentDL option:selected")[0].value;
    for (var i = 0; i < checked.length; i++) {
        Idstemp = {
            ID: checked[i].attributes.userid.value,
            SubDepartmentID: SubDepaermentID,
            ModifiedByUserId: LoggedUserData.GlobalUserID
            //DepartmentID: DepaermentID
        };
        UsersID.push(Idstemp);
    }
    var UsersIDs = UsersID;
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Users/SumbitUsersToSubDepartment";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(UsersIDs), 'json', SucessSumbitUsersToSubDepartment, false);
};

function SucessSumbitUsersToSubDepartment(Result) {

    if (Result.Success === true) {
        ShowALert(2, Result.Message );
        EmptyData();
        GetAllUsersSubDepartment();
        GetAllSubDepartments();
        //var alertext = "Users Added Successfully to SubDepartment  " + SubDepartment;
        //ShowALert(2, alertext);
        ////GetAllOfficialHolidays()
    }
    else {
        ShowALert(4, Result.Message);

    }
 

}

function EmptyData() {
    $("#UserslistTable").empty();
    $("#SelectSubDepartmentDL").empty();
}


// #endregion Save Link Users To Subdepartment

// #endregion SubDepartment

//____________________________________________________________________________________________________________
//____________________________________________________________________________________________________________

// #region Department  

function GetAllUsersDepartment() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL+"/Users/GetAllUsersDepartment";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetAllUser, false);
}


function SucessGetAllUser(UsersDetails) {

    var Table = $("#UsersDetailsTable");
    for (var i = 0; i < UsersDetails.Data.length; i++) {

        if (UsersDetails.Data[i].SubDepartmentName === null) {

            UsersDetails.Data[i].SubDepartmentName = "---------------------";
        }

        Table.append('<tr class="clickable-row"><td><div class="checkboxContainer"><input type="checkbox" UserId="'
            + UsersDetails.Data[i].UserID + '" class="chkDelete"> <label for="null"><span></span></label></div></td><td>'
            + UsersDetails.Data[i].UserName + '</td><td>' + UsersDetails.Data[i].UserEmail + '</td></tr>');
    }
}


function GetAllDepartmentsDropDown() {
    var urlGetAllDepartments = ConfigurationData.GlobalApiURL+"/Department/GetAllDepartments";
    Common.Ajax('get', urlGetAllDepartments, null, 'json', SucessGetAllDepartments, false);
}

function SucessGetAllDepartments(DepartmentDropDownList) {
    for (var i = 0; i < DepartmentDropDownList.Data.length; i++) {
        $("#SelectDepartmentDL").append('<option  DepaermentID="' + DepartmentDropDownList.Data[i].DepartmentID + '" value="' + DepartmentDropDownList.Data[i].DepartmentName + '">' + DepartmentDropDownList.Data[i].DepartmentName + '</option>');
    }
}



var Department;
var SaveListBTN = function () {
    var Idstemp = {};
    var UsersID = [];
    var all, checked, notChecked, Table;

    all = $("#UsersDetailsTable input:checkbox");
    checked = all.filter(":checked");
    Table = $("#UsersDetailsTable");
    var DepaermentID = $("#SelectDepartmentDL option:selected")[0].attributes.DepaermentID.value;

    Department = $("#SelectDepartmentDL option:selected")[0].value;
    for (var i = 0; i < checked.length; i++) {
        Idstemp = {
            ID: checked[i].attributes.userid.value,
            DepartmentID: DepaermentID,
            ModifiedByUserId: LoggedUserData.GlobalUserID

        };
        UsersID.push(Idstemp);
    }
    var UsersIDs = UsersID;
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Users/SumbitUsersToDepartment";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(UsersIDs), 'json', SucessSumbitUsersToDepartment, false);
};

function SucessSumbitUsersToDepartment(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        EmptyData2();
        GetAllUsersDepartment();
        GetAllDepartmentsDropDown();
        //var alertext = "Users Added Successfully to Department  " + Department;
        //ShowALert(2, alertext);
        //window.location.href = '/Configuration/Holidays';
        ////GetAllOfficialHolidays()
    }
    else {
        ShowALert(4, Result.Message);

    }


}

function EmptyData2() {
    $("#UsersDetailsTable").empty();
    $("#SelectDepartmentDL").empty();
}


// #endregion Department