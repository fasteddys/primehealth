var LeaveTypes = [];
$(document).ready(function () {
    GetAllLeaveType();
    GetEmployeesList();


});
function GetAllLeaveType() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetLeaves, false);
}
function SucessGetLeaves(Leaves) {
    LeaveTypes = Leaves;
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $("#LeaveType").append('<option IsDAy="' + LeaveTypes.Data[i].IsDay + '" value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
    }


}
var AddNewRequest = function () {
    //Object Manager
    var StartDate = $("#StartDate")[0].value;
    var EndDate = $("#EndDate")[0].value;
    var LeveType = $("#LeaveType")[0].value;
    var NewRequest = {
        UserID: LoggedUserData.GlobalUserID,
        LeaveTypeID: LeveType,
        RequestStart: StartDate,
        RequestEnd: EndDate
    };
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Request/AddRequest";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(NewRequest), 'json', SucessSubmitRequest, false);
};
function SucessSubmitRequest(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);


    }

}
$("#LeaveType").change(function () {
    //var AccessContoSelect = $("#AccessContolListSelect").val();
    var SelectRow = $('option:selected', this).attr('isday');
    if (SelectRow === "true") {
        //  document.getElementById("StartDate").style.display = "visible";
        //  document.getElementById("EndDateDiv").style.display = "initial";
        $("#EndDateDiv").show();

    }
    else {

        // document.getElementById("EndDateDiv").style.display = "none";
        $("#EndDateDiv").hide();

    }


    // $("#EndDate")[0].

    //  $("#ACUserName").text(AccessContoSelect);
    //  $("#ACUserID").text(SelectRow);

});
//CheckIfDay
function GetEmployeesList() {
    urlGetEmployeesList = ConfigurationData.GlobalApiURL + "/Users/GetAllUsers";
    Common.Ajax('GET', urlGetEmployeesList, null, 'json', SucessGetEmployees, false);
}
function SucessGetEmployees(EmployeesList) {
    AppendEmployeesList(EmployeesList);
}
function AppendEmployeesList(EmployeesList) {
    for (var i = 0; i < EmployeesList.Data.length; i++) {
        
        $("#User").append('<option ID="' + EmployeesList.Data[i].UserID +
            '" value="' + EmployeesList.Data[i].UserName + '">' +
            EmployeesList.Data[i].UserName + '</option>');
        
    }
}