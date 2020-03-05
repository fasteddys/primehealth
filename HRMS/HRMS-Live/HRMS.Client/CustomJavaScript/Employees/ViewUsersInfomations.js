var CheckGetAllUsers = false;
var listUserstIDs = [];
$(document).ready(function () {
    $(".divUsersName").hide();
    $("#InformationUsersSelectBy").change(function () {
        if ($(this).val() == 3)
        {
            $(".divUsersName")
            $(".divUsersName").show();
            if (CheckGetAllUsers == false)
            {
                CheckGetAllUsers = true;
                GetAllUsers();
            }
        }
        else
        {
            $(".divUsersName").hide();
            $("#UsersListName option:selected").remove();

        }
        console.log($(this).val());
    })

});
/*get all users*/
function GetAllUsers() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessUrlGetUsers, false);
}
function SucessUrlGetUsers(Users) {
    $("#UsersListName").empty();
    for (var i = 0; i < Users.Data.length; i++) {
        $("#UsersListName").append('<option value="' + Users.Data[i].UserID + '">' + Users.Data[i].UserName + '</option>');
    }
}
/*-----------------------------------------------*/
/*get all departments*/

$("#UsersListName").change(function () {
    listUserstIDs = [];
    var SelectedValus = $('option:selected', this);
    for (var i = 0; i < SelectedValus.length; i++) {
        listUserstIDs.push(SelectedValus[i].value);
    }
    console.log(listUserstIDs);
});
/*-----------------------------------------------*/

/*Find Rejection Reasons*/
$("#FindUsersInformation").on("click", function () {
    var selectedUsersBy = $("#InformationUsersSelectBy").val();
    var NewSearchForUserInformation=
        {
            FiliterUsersBy: selectedUsersBy,
            ListUserstIDs: listUserstIDs
        }

    var urlUsersInformations = ConfigurationData.GlobalApiURL + "/Users/UsersInformations";//will be change
    Common.Ajax('post', urlUsersInformations, JSON.stringify(NewSearchForUserInformation), 'json', SucessurlurlUsersInformations);
})
function SucessurlurlUsersInformations(Result) {
    $('#usersInformationTable').DataTable().destroy();
    $('#usersInformationTable').DataTable({
        retrieve: true,
        "data": Result.Data,
        "columns": [
            { "data": "UserID" },//1
            { "data": "UserName" },//2
            { "data": "PhoneNumber" },//4

        ]
    })
}
/*-----------------------------------------------*/
/*Report*/
$('#ExtractUsersInformation').click(function () {
    var selectedUsersBy = $("#InformationUsersSelectBy").val();
    var NewSearchForUserInformation =
        {
            FiliterUsersBy: selectedUsersBy,
            ListUserstIDs: listUserstIDs
        }
    var urlGetUsersInformationsExcel = ConfigurationData.GlobalApiURL + "/Users/UsersInformationsExcel";
       var today = new Date();
    var date ="_"+today.getFullYear() + '_' + (today.getMonth() + 1) + '_' + today.getDay() + "_" + today.getHours() + "_" + today.getMinutes() + "_" + today.getSeconds();
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        var a;
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            // Trick for making downloadable link
            a = document.createElement('a');
            a.href = window.URL.createObjectURL(xhttp.response);
            // Give filename you wish to download
            a.download = "CypressUsersInformationsReport" + date + ".xls";
            a.style.display = 'none';
            document.body.appendChild(a);
            a.click();
        }
    };
    // Post data to URL which handles post request
    xhttp.open("POST", urlGetUsersInformationsExcel);
    xhttp.setRequestHeader("Content-Type", "application/json");
    // You should set responseType as blob for binary responses
    xhttp.responseType = 'blob';
    xhttp.send(JSON.stringify(NewSearchForUserInformation));
});
/*-----------------------------------------------*/
