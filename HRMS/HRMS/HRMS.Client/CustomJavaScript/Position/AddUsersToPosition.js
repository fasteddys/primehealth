$(document).ready(function () {
    GetAllUsers();
    GetAllPositions(); 
});
function GetAllUsers() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL+"/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetAllUsers, false);
}
function SucessGetAllUsers(UsersDetails) {
    var Table = $("#AddUsersToPositionTable");
    for (var i = 0; i < UsersDetails.Data.length; i++) {

        if (UsersDetails.Data[i].UserPositionName === null) {

            UsersDetails.Data[i].UserPositionName = "------------";
        }

        Table.append('<tr class="clickable-row"><td><div class="checkboxContainer"><input type="checkbox" UserId="'
            + UsersDetails.Data[i].UserID + '" class="chkDelete"> <label for="null"><span></span></label></div></td><td>'
            + UsersDetails.Data[i].UserName + '</td><td>' + UsersDetails.Data[i].UserEmail + '</td><td>' + UsersDetails.Data[i].PositionName + '</td></tr>');
    }
}
function GetAllPositions() {
    var urlGetAllPosition = ConfigurationData.GlobalApiURL+"/Position/GetAllPositions";
    Common.Ajax('get', urlGetAllPosition, null, 'json', SucessGetAllPositions, false);
}
function SucessGetAllPositions(PositionDropDownList) {
    for (var i = 0; i < PositionDropDownList.Data.length; i++) {
        $("#SelectPositionDL").append('<option  PositionID="' + PositionDropDownList.Data[i].PositionID + '" value="' + PositionDropDownList.Data[i].PositionName + '">' + PositionDropDownList.Data[i].PositionName + '</option>');
    }
}
var PositionName;
var SaveListBTN = function () {
    var Idstemp = {};
    var UsersID = [];
    var all, checked, notChecked, Table;

    all = $("#AddUsersToPositionTable input:checkbox");
    checked = all.filter(":checked");
    Table = $("#AddUsersToPositionTable");
    var PositionID = $("#SelectPositionDL option:selected")[0].attributes.PositionID.value;
    PositionName = $("#SelectPositionDL option:selected")[0].value;
    for (var i = 0; i < checked.length; i++) {
        Idstemp = {
            ID: checked[i].attributes.UserID.value,
            PositionFK: PositionID,
            ModifiedByUserId: LoggedUserData.GlobalUserID

        };
        UsersID.push(Idstemp);
    }

    var UsersIDs = UsersID;
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Users/GetUserListIDs";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(UsersIDs), 'json', SucessSubmitUsersToPosition, false);
};
function SucessSubmitUsersToPosition(Result) {
    var AlertText = "User Added Successfully to position  " + PositionName;
    if (Result.Success === true) {
        ShowALert(2, AlertText);
        EmptyData();
        GetAllUsers();
        GetAllPositions(); 
    }
    else {
        ShowALert(4, Result.Message);

    }
 
}

function EmptyData() {
    $("#SelectPositionDL").empty();
    $("#AddUsersToPositionTable").empty();
    
}