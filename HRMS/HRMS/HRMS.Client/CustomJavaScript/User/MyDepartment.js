$(document).ready(function () {
    GetDepartmentUsers();
});


function checkImageAvailability(src, UserId)
{
    $('<img>').attr('src', src).on("error", function (e) {
        $('#' + UserId).attr('src', '/img/user_profile.jpg');
    })
    .on("load", function (e) {
        $('#' + UserId).attr('src', src);
    })
      
}
function GetDepartmentUsers() {
    var urlGetDepartmentUsers = ConfigurationData.GlobalApiURL+"/users/GetUserDepartmentUsers?UserID=" + LoggedUserData.GlobalUserID;
    Common.Ajax('get', urlGetDepartmentUsers, null, 'json', SucessGetDepartmentUsers, false);
}
function SucessGetDepartmentUsers(UsersData) {
    RenderDepartmentUsers(UsersData);
}
function RenderDepartmentUsers(UsersData) {
    for (var i = 0; i < UsersData.Data.length; i++) {
        var SubDepartment, DirectlyReportsTo, TeamLeader;
        var UserPictureUrlMyDep = '';
      
        if (UsersData.Data[i].ProfilePictureURL === null) {
            UserPictureUrlMyDep = '/img/user_profile.png';
            checkImageAvailability(UserPictureUrlMyDep, UserId);
        }
        else {
            UserPictureUrlMyDep = ConfigurationData.PictureLocation + UsersData.Data[i].ProfilePictureURL;
            var UserId = UsersData.Data[i].UserID;
            checkImageAvailability(UserPictureUrlMyDep, UserId);       
        }

        if (UsersData.Data[i].SubDepartment === null)
            SubDepartment = 'None';
        else
            SubDepartment = UsersData.Data[i].SubDepartment;

        if (UsersData.Data[i].DirectlyReportsTo === null)
            DirectlyReportsTo = 'None';
        else
            DirectlyReportsTo = UsersData.Data[i].DirectlyReportsTo;

        if (UsersData.Data[i].TeamLeader === null)
            TeamLeader = 'None';
        else
            TeamLeader = UsersData.Data[i].TeamLeader;

    var Usercard = '<div class="col-md-2 col-md-3 mb-1 mb-md-0 UserCard">'+
        '<section class="card">'+
        '<div class="card-body">'+
        '<div class="thumb-info mb-3 thumbnail ThumbnailImgContainer">' +
        '<img id="' + UsersData.Data[i].UserID +'" class="rounded img-fluid UserImageThumbnail deptImg" alt="User Picture">' +
        '<input type="file" id="imgupload" class="Cimgupload" style="display:none" />' +
        '<div class="thumb-info-title">' +
        '<span class="thumb-info-inner">' + UsersData.Data[i].UserName +'</span>' +
        '<span class="thumb-info-type">' + UsersData.Data[i].Position +'</span>' +
        '</div>' +
        '</div>' +
        '<ul class="simple-todo-list mt-3">' +
        '<li style="padding:0 0 4px 4px">Company : <span class="text-color-primary" style="font-size: 89%">' + UsersData.Data[i].CompanyName + '</span></li>' +
        '<li style="padding:0 0 4px 4px">Manager : <span class="text-color-primary" style="font-size: 89%">' + DirectlyReportsTo + '</span></li>' +
        '<li style="padding:0 0 4px 4px">Team : <span class="text-color-primary" style="font-size: 89%">' + SubDepartment + '</span></li>' +
        '<li style="padding:0 0 4px 4px">T-Leader : <span class="text-color-primary" style="font-size: 89%">' + TeamLeader + '</span></li>' +
        '</ul>' +
        '</div>' +
        '</section>' +
        '</div >';

        $("#UsersCards").append(Usercard);
    }
}