$(document).ready(function () {
    GetAllWorkingWeek();
    
});
function GetAllWorkingWeek() {
    var urlGetAllWorkingWeek = ConfigurationData.GlobalApiURL + "/WorkingWeek/GetAllWorkingWeek";
    Common.Ajax('get', urlGetAllWorkingWeek, null, 'json', SucessurlGeturlGetAllWorkingWeek, false);
}
function SucessurlGeturlGetAllWorkingWeek(WorkingWeek) {
    var TBLWorkingWeek = $("#WorkingWeekTBL");
    for (var i = 0; i < WorkingWeek.Data.length; i++) {
        TBLWorkingWeek.append('<tr class="clickable-row" onclick="GetSubWorkingWeekByID(' + WorkingWeek.Data[i].WorkingWeekID + ')"><td>' +
            WorkingWeek.Data[i].WorkingWeekName + '</td><td>' + WorkingWeek.Data[i].UsersCount +
            '</td><td><button  onclick="WorkingWeekDetailsBTN('+WorkingWeek.Data[i].WorkingWeekID+')" type="button" class="btn btn-link">Details</button></td></tr>');
    }
}
function WorkingWeekDetailsBTN(WorkingWeekID) {

    window.location.href = '/Configuration/ViewWorkingWeek?WorkingWeekID=' + WorkingWeekID;
}
function AddNewWorkingWeek() {
    window.location.href = '/Configuration/WorkingWeek';
}
function GetSubWorkingWeekByID(WorkingWeekID) {
    var urlGetWorkingWeekByID = ConfigurationData.GlobalApiURL + "/WorkingWeek/GetWorkingWeekByID?WorkingWeekID=" + WorkingWeekID;
    Common.Ajax('get', urlGetWorkingWeekByID, null, 'json', SucessGetWorkingWeekByID, false);
}
function SucessGetWorkingWeekByID(PersonsOfWorkingWeek) {

    var urlGetUsersByID = ConfigurationData.GlobalApiURL + "/WorkingWeek/GetUsersByWorkingWeek?WorkingWeekID=" + PersonsOfWorkingWeek.Data.WorkingWeekID;
    Common.Ajax('get', urlGetUsersByID, null, 'json', SucessGetUsersByWorkingWeekID, false);
}
function SucessGetUsersByWorkingWeekID(UsersDetails) {

    var TableUsersListTable = $("#UsersList");
    TableUsersListTable.empty();
    for (var i = 0; i < UsersDetails.Data.length; i++) {

        TableUsersListTable.append('<tr class="clickable-row"><td>' + UsersDetails.Data[i].UserName + '</td></tr>');
    }
}