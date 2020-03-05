
$(document).ready(function () {
    GetAllSubordinate();
});
function GetAllSubordinate(){
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/ApprovalFlow/GetUserApprovedByManager?UserIDOfManager=" + LoggedUserData.GlobalUserID;
    Common.Ajax('get', urlGetUserByID, null, 'json', SucessGetAllSubordinate, false);
}
function SucessGetAllSubordinate(Result) {
    //alert(Result.Message);
    $("#ApprovalsTable").empty();
    $('#TableData').DataTable().destroy();
    $('#TableData').DataTable({
        data: Result.Data,
        columns: [
            { "data": "UserID" },

            { "data": "UserName" },

            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    return "<a href='/User/UserProfile?LID=" + LoggedUserData.GlobalUserID+"&UID=" + full.UserID + "'>Open</a>";
                }
            }
        ]


    });
}
