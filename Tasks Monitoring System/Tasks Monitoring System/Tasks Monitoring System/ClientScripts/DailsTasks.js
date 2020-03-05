$(document).ready(function () {
    $("#searchTasks").on("click", function () {
        var from = $("#DailyTaskFrom").val();
        var to = $("#DailyTaskTo").val();
    if (from == '' || to=='')
    {
        ShowALert(1, "Enter All Data!!");
    }
    else
    {
        var urlGetDailyTasks = "/UserDailsTasks/findTasksSearch";
        var NewSearchForDailyTasks =
            {
                From: from,
                To: to
            }
        // Common.Ajax('post', urlUsersInformations, JSON.stringify(NewSearchForUserInformation), 'json', SucessurlurlUsersInformations);
        Common.Ajax("post", urlGetDailyTasks, JSON.stringify(NewSearchForDailyTasks), 'json', sucessGetDate)
    }
       
    });

});
function sucessGetDate(Result) {
    $('#DailyTaskTable').DataTable().destroy();
    $('#DailyTaskTable').DataTable({
        retrieve: true,
        "data": Result,
        "columns": [
            { "data": "UserDailyTasksID" },//1
            { "data": "CompanyName" },//2
            { "data": "TaskName" },//2
            { "data": "TaskPriority" },//2
            { "data": "TaskStatusName" },//2
            { "data": "UserName" },//2

            {
                data: "DateOfTask",//3
                "render": function (data) {
                    if (data != null)
                    {
                        return moment(data).format('DD/MM/YYYY');

                    }
                    else
                    {
                        return ""
                    }
                }
            },
            {
                data: "AssignTime",//3
                "render": function (data) {
                    if (data != null) {
                        return moment(data).format('DD/MM/YYYY  hh:mm:ss A');

                    }
                    else {
                        return ""
                    }
                }
            },
            {
                data: "CompletedOn",//3
                "render": function (data) {
                    if (data != null) {
                        return moment(data).format('DD/MM/YYYY  hh:mm:ss A');

                    }
                    else {
                        return ""
                    }
                }
            },
            { "data": "ClosingComment" },//2

        ]
    })
}
