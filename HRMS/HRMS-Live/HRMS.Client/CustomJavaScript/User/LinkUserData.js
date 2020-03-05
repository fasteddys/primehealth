$(document).ready(function () { 
    GetAllUsers();
    GetAccessControlUsers();
});

function GetAllUsers() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL+"/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetLDapUsers, false);
}
function SucessGetLDapUsers(Users) {
    var Table = $("#AppendUsersToTable");
    Table[0].innerHTML = "";
    for (var i = 0; i < Users.Data.length; i++) {
        Table.append('<tr class="clickablerow" onclick="GetLDapUserData(' + Users.Data[i].UserID + ')"><td><a href="#">' + Users.Data[i].UserName + '</a></td><td><a href="#">' + Users.Data[i].UserID + '</a></td></tr>');
    }
}
function GetAccessControlUsers() {
    var UrlGetAccessControlUsers = ConfigurationData.GlobalApiURL+"/Users/GetAllACUsers";
    Common.Ajax('get', UrlGetAccessControlUsers, null, 'json', SucessGetAccessControlCUsers, false);
}
function SucessGetAccessControlCUsers(AccessControlUser) {

    for (var i = 0; i < AccessControlUser.Data.length; i++) {
        $("#AccessContolListSelect").append('<option PID="' + AccessControlUser.Data[i].AccessControlUserID + '" value="' + AccessControlUser.Data[i].AccessControlUserName + '">' + AccessControlUser.Data[i].AccessControlUserName + '</option>')
    }
}
function GetLDapUserData(ID) {
    var urlGetUserByID = ConfigurationData.GlobalApiURL+"/Users/GetUserByID?UserID=" + ID;
    Common.Ajax('get', urlGetUserByID, null, 'json', SucessGetUser, false);
}
function SucessGetUser(Details) {
    $("#RenderdUsername").text(Details.Data.UserName);
    $("#RenderdEmail").text(Details.Data.UserEmail);
    $("#RenderdID").text(Details.Data.UserID);
}
$("#AccessContolListSelect").change(function () {
    var AccessContoSelect = $("#AccessContolListSelect").val();
    var SelectRow = $('option:selected', this).attr('PID');
    $("#ACUserName").text(AccessContoSelect);
    $("#ACUserID").text(SelectRow);

});
$("#SubmitLinkUser").click(function () {
    var username = $("#RenderdUsername")[0].innerText;
    var Email = $("#RenderdEmail")[0].innerText;
    var ID = $("#RenderdID")[0].innerText;
    var AccessControlUserFK = $("#ACUserID")[0].innerText;
    var AccessControlUsername = $("#ACUserName")[0].innerText;

    var user = {
        UserID: ID,
        LDAPUserName: username,
        UserEmail: Email,
        AccessControlUserID: AccessControlUserFK,
        UserName: AccessControlUsername,
        ModifiedByUserId: LoggedUserData.GlobalUserID
    };

    var urlUpdateUser = ConfigurationData.GlobalApiURL + "/Users/UpdateUserAccessControlID";
    Common.Ajax('post', urlUpdateUser, JSON.stringify(user), 'json', SucessLinkUsers, false);
});
function SucessLinkUsers(Result) {    
    $("#RenderdUsername").text("");
    $("#RenderdEmail").text("");
    $("#RenderdID").text("");
    $("#ACUserName").text("");
    $("#ACUserID").text("");

    if (Result.Success === true)
        ShowALert(2, "User Liked Successfully");
    else
        ShowALert(4, "User Not Liked");
}