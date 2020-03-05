var CheckGetAllUsers = false;
var listUserstIDs = [];
listOFAttribute = [];
$(document).ready(function () {
    $(".divUsersName").hide();
    getAllPropertsForUsersInformations();
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
            //$("#UsersListName option:selected").removeAttr("selected");
            //$("#UsersListName").val('');

            //$('#UsersListName').prop('selectedIndex', -1)
            //$("#UsersListName option:selected").each(function () {
            //    $(this).removeAttr('selected');
            //});
            //$("#UsersListName option:selected").prop("selected", false);
            //$("#UsersListName").multiselect('refresh');
            //$("#UsersListName").multiselect('refresh');
        }
        console.log($(this).val());
    })
    $(".checkboxColumn").css("border", "1px solid red");
    $("#displayUserInformationData").attr("disabled", true);
    $("#PropertyUsersToDisplay ").on("change", ".checkboxColumn", function () {
        var count = $(".numberOfColumn").val();
        var limit = false;
        if (!($(this).is(':checked')))
        {
            $(".numberOfColumn").val(++count);
            listOFAttribute.splice(listOFAttribute.indexOf($(this).val()), 1);
            console.log(listOFAttribute.length);

        }
        if (count <= 0) {
            $(this).prop("checked", false);

        }
        else if ($(this).is(':checked')) {
            $(".numberOfColumn").val(--count);
            console.log($(this).val())
            listOFAttribute.push($(this).val())
            console.log(listOFAttribute.length);
       

        }
        if (count >=7)
            $("#displayUserInformationData").attr("disabled", true);
        else
            $("#displayUserInformationData").attr("disabled", false);
        //if (count <= 0) {
        //    $("#PropertyUsersToDisplay .checkboxColumn").attr("disabled", true);
        //}
    });
 
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
});
function removeselected()
{    var SelectedValus = $('#UsersListName option:selected');
    for (var i = 0; i < SelectedValus.length; i++) {
        SelectedValus[i].selected = false;
    }
}
/*-----------------------------------------------*/
/*Full checkbox with properts*/
function getAllPropertsForUsersInformations()
{
    var urlGetAllPropertsForUsersInformations = ConfigurationData.GlobalApiURL + "/Users/getAllUserInformationProperty";;
    Common.Ajax('get', urlGetAllPropertsForUsersInformations, false, 'json', sucessGetAllPropertsForUsersInformations)
}
function sucessGetAllPropertsForUsersInformations(result)
{
    for (var i = 0; i < result.Data.length; i++) {
        //$("#PropertyUsersToDisplay").append('<option value="' + Users.Data[i].UserID + '">' + Users.Data[i].UserName + '</option>');
        $("#PropertyUsersToDisplay").append('<input type="checkbox" class="checkboxColumn"  id="' + result.Data[i] + '" value="' + result.Data[i]+'"/>');
        $("#PropertyUsersToDisplay").append( '<label  id="'+ result.Data[i]+'">'+result.Data[i]+'</label>');
        $("#PropertyUsersToDisplay").append('<br>')

    }
}
/*-----------------------------------------------*/

/*Find Rejection Reasons*/
$("#FindUsersInformation").on("click", function () {

})

$("#displayUserInformationData").on("click", function () {
    $("#UserInformationPropertsDataModal").modal('hide');
    var selectedUsersBy = $("#InformationUsersSelectBy").val();
    var NewSearchForUserInformation =
        {
            FiliterUsersBy: selectedUsersBy,
            ListUserstIDs: listUserstIDs
        }

    var urlUsersInformations = ConfigurationData.GlobalApiURL + "/Users/UsersInformations";//will be change
    Common.Ajax('post', urlUsersInformations, JSON.stringify(NewSearchForUserInformation), 'json', SucessurlurlUsersInformations);

});
function SucessurlurlUsersInformations(Result) {
    $("#headUserInformation th").remove();
    for (var i = 0; i < listOFAttribute.length; i++) {
        $("#headUserInformation").append("<th>" + listOFAttribute[i] + "</th>")
    }

    //for (var i = 0; i < length; i++)
    //{
    //    { "data": listOFAttribute[counter] }

    //}
    arr = [];
    //arr = [
    //    { "data": listOFAttribute[counter++] },//1
    //    { "data": listOFAttribute[counter++] },//1
    //    { "data": listOFAttribute[counter++] },//1
    //    { "data": listOFAttribute[counter++] },//1

    //];
    for (j = 0; j < listOFAttribute.length; j++) {
        var item = { "data": listOFAttribute[j] }
        arr.push(item);
    }

    var counter = 0;
    $('#usersInformationTable').DataTable().destroy();
    $('#usersInformationTable').DataTable({
        retrieve: true,
        "data": Result.Data,
        "columns": arr
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
