
$(document).ready(function () {
    GetAllManagers();
    GetAllSubDepartments();
});
//Get All Managers
function GetAllManagers() {
    var urlGetAllManager = ConfigurationData.GlobalApiURL+"/Manager/GetAllManagers";
    Common.Ajax('get', urlGetAllManager, null, 'json', SucessGetAllManagers, false);
}
function SucessGetAllManagers(Managers) {
    for (var i = 0; i < Managers.Data.length; i++) {
        $("#DirectManager").append('<option ManagerID="' + Managers.Data[i].ManagerID + '" value="' + Managers.Data[i].ManagerName + '">' + Managers.Data[i].ManagerName + '</option>')
    }
}
//Get All SubDepartments
function GetAllSubDepartments() {
    var urlGetAllTeams = ConfigurationData.GlobalApiURL+"/SubDepartment/GetAllSubDepartments";
    Common.Ajax('get', urlGetAllTeams, null, 'json', SucessurlGetAllTeams, false);
}
function SucessurlGetAllTeams(SubDepartment) {
    for (var i = 0; i < SubDepartment.Data.length; i++) {
        $("#Teams").append('<option DepartmentID="' + SubDepartment.Data[i].SubDepartmentID + '" value="' + SubDepartment.Data[i].SubDepartmentName + '">' + Department.Data[i].SubDepartmentName + '</option>')
    }
}
var SaveUserBTN = function () {
    debugger;
    var FirstName = $('#FirstName').val();
    var LastName = $('#LastName').val();
    var Email = $('#Email').val(); 
    var HireDate = $('#HireDate').val();
    var Teams = $('#Teams').val();
    var BirthDate = $('#BirthDate').val();
   // var HireDate = $('#HireDate').val();
    var ProbationEnd = $('#ProbationEnd').val();
    var TerminationDate = $('#TerminationDate').val();

}