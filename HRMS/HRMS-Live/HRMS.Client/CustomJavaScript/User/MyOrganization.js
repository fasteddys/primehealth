$(document).ready(function () {
    GetCompanyUsers();
});
function checkImageAvailability(src, UserId) {
    $('<img>').attr('src', src)
        .on("error", function (e) {
            $('#' + UserId).attr('src', '/img/user_profile.jpg');
        })
        .on("load", function (e) {
            $('#' + UserId).attr('src', src);
        });

}
function GetCompanyUsers() {
    var urlGetCompanyUsers = ConfigurationData.GlobalApiURL + "/users/GetOrganizationUsers";
    Common.Ajax('get', urlGetCompanyUsers, null, 'json', SucessGetCompanyUsers, false);
}
function SucessGetCompanyUsers(UsersData) {
    RenderCompanyUsers(UsersData);
}
function RenderCompanyUsers(UsersData) {
    for (var j = 0; j < UsersData.Data.length; j++) {
        var Usercard = '';
        var DepartmentSection = '';
        DepartmentSection = '<div class="row">' +
            '<div>' +
            '<h1>' + UsersData.Data[j].UserDepartmentName + ' Department' + '</h1>' +
            '</div></div>' +
            '<div class="row">';
        for (var i = 0; i < UsersData.Data[j].UsersCards.length; i++) {
            var SubDepartment, DirectlyReportsTo, TeamLeader, Email = '', UserNameFromEmail = '', ExtNumber;
            var UserPictureUrlMyDep = '';

            if (UsersData.Data[j].UsersCards[i].ProfilePictureURL === null) {
                UserPictureUrlMyDep = '/img/user_profile.png';
                checkImageAvailability(UserPictureUrlMyDep, UserId);
            }
            else {
                UserPictureUrlMyDep = ConfigurationData.PictureLocation + UsersData.Data[j].UsersCards[i].ProfilePictureURL;
                var UserId = UsersData.Data[j].UsersCards[i].UserID;
                checkImageAvailability(UserPictureUrlMyDep, UserId);
            }

            Email = UsersData.Data[j].UsersCards[i].UserEmail !== null ? UsersData.Data[j].UsersCards[i].UserEmail : 'None';
            UserNameFromEmail = Email !== null ? Email.split("@") : Email;

            SubDepartment = UsersData.Data[j].UsersCards[i].SubDepartment !== null ? UsersData.Data[j].UsersCards[i].SubDepartment : 'None';
            DirectlyReportsTo = UsersData.Data[j].UsersCards[i].DirectlyReportsTo !== null ? UsersData.Data[j].UsersCards[i].DirectlyReportsTo : 'None';
            TeamLeader = UsersData.Data[j].UsersCards[i].TeamLeader !== null ? UsersData.Data[j].UsersCards[i].TeamLeader : 'None';
            ExtNumber = UsersData.Data[j].UsersCards[i].ExtNumber !== null ? UsersData.Data[j].UsersCards[i].ExtNumber : 'None';

            DepartmentSection += '<div class="col-md-2 col-md-3 mb-1 mb-md-0 UserCard">' +
                '<section class="card">' +
                '<div class="card-body">' +
                '<div class="thumb-info mb-3 thumbnail ThumbnailImgContainer">' +
                '<img id="' + UsersData.Data[j].UsersCards[i].UserID + '" class="rounded img-fluid UserImageThumbnail deptImg" alt="User Picture">' +
                '<input type="file" id="imgupload" class="Cimgupload" style="display:none" />' +
                '<div class="thumb-info-title">' +
                '<span class="thumb-info-inner">' + UsersData.Data[j].UsersCards[i].UserName + '</span>' +
                '<span class="thumb-info-type">' + UsersData.Data[j].UsersCards[i].Position + '</span>' +
                '</div>' +
                '</div>' +
                '<ul class="simple-todo-list mt-3">' +
                '<li style="padding:0 0 4px 4px">Company : <span class="text-color-primary">' + UsersData.Data[j].UsersCards[i].CompanyName + '</span></li>' +
                '<li style="padding:0 0 4px 4px">Manager : <span class="text-color-primary">' + DirectlyReportsTo + '</span></li>' +
                '<li style="padding:0 0 4px 4px">Team : <span class="text-color-primary">' + SubDepartment + '</span></li>' +
                '<li style="padding:0 0 4px 4px">T-Leader : <span class="text-color-primary">' + TeamLeader + '</span></li>' +
                '<li style="padding:0 0 4px 4px">Email : <span class="text-color-primary"><a href="mailto:' + Email + '">' + UserNameFromEmail[0] + '</a></span></li>' +
                '<li style="padding:0 0 4px 4px">ExtNumber : <span class="text-color-primary">' + ExtNumber + '</span></li>' +
                '</ul>' +
                '</div>' +
                '</section>' +
                '</div >';

            //$("#UsersCards").append(Usercard);
        }
        DepartmentSection += '</div>';
        $("#DepartmentUsersCards").append(DepartmentSection);
    }

}