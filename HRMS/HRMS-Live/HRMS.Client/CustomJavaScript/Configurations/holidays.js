$(document).ready(function () {
    GetAllOfficialHolidays();
    var officialHolidaysEditId = getParameterByName("id");
    if (officialHolidaysEditId!=null)
    {
        getOfficialHolidayById(officialHolidaysEditId);
    }  
});
/*------------------------------------------------*/
function GetAllOfficialHolidays()
{
    var urlGetOfficialHolidays = ConfigurationData.GlobalApiURL + "/OfficialHolidays/GetAllOfficialHolidays";
    Common.Ajax('get', urlGetOfficialHolidays, null, 'json', SucessGetAllOfficialHolidays, false);
}
function SucessGetAllOfficialHolidays(Result)
{
    $('#OfficialHolidaysTable').DataTable().destroy();
    $('#OfficialHolidaysTable').DataTable({
        retrieve: true,
        //"ordering": false,
        "data": Result.Data,
        "columns": [
            { "data": "officialHolidaysId" },//1
            { "data": "officialHolidaysName" },//2
            { "data": "officialHolidaysDate" },//3
            { "data": "IsOfficial" },//4
            { "data": "AddedBy" },//5
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    //return '<a id="editOfficialHolidays"  data-value=' + data.officialHolidaysId +' class="btn btn-info btn-sm" href=/Configuration/Edit?officialHolidaysId=' + data.officialHolidaysId + '>' + 'Edit' + '</a>' ;
                    return '<a id="editOfficialHolidays" data-value=' + data.officialHolidaysId + ' data-officialdata=' + data.officialHolidaysDate + ' class="btn btn-info btn-sm" href="/Configuration/EditOfficialHolidays?id='+data.officialHolidaysId+'">' + 'Edit' + '</a>';

                }
                
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<a id="deleteOfficialHolidays"  data-value=' + data.officialHolidaysId + ' data-officialdata=' + data.officialHolidaysDate + ' class="btn btn-danger btn-sm" href="#" >' + 'Delete' + '</a>';
                }

            }
              
        ],
    })
}
/*------------------------------------------------*/
/*delete Official Holidays By Id*/
$(document).on("click", '#deleteOfficialHolidays', function () {
    officialHolidaysIdForDeleted = $(this).data("value");
    var officialHolidaysDateForDeleted = $(this).data("officialdata");
    officialHolidaysDateForDeleted=convertStringToDate(officialHolidaysDateForDeleted)
    var DateNow = moment(Date(Date.now())).format('DD/MM/YYYY');
    DateNow = convertStringToDate(DateNow);
    if (DateNow >= officialHolidaysDateForDeleted)
    {
        ShowALert(1,"Can't Delete This official Holidays")
    }
    else
    {
        var urlDeleteOfficialHolidaysById = ConfigurationData.GlobalApiURL + "/OfficialHolidays/DeleteOfficialHolidaysById?id=" + officialHolidaysIdForDeleted;
        Common.Ajax('delete', urlDeleteOfficialHolidaysById, null, 'json', deleteIsSucess, false);
    }

});
function deleteIsSucess (Result)
{
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        GetAllOfficialHolidays();
    }
    else {
        ShowALert(4, Result.Message);

    }
}
/*---------------------------------------------------------------------------------------------*/
//edit
$(document).on("click", "#editOfficialHolidays", function () {
    var OfficialHolidaysId = $(this).data("value")
});
function getOfficialHolidayById(id)
{
    var urlGetOfficialHolidayById = ConfigurationData.GlobalApiURL + "/OfficialHolidays/GetOfficialHolidayById?id=" + id;
    Common.Ajax('get', urlGetOfficialHolidayById, null, 'json', sucessGetOfficialHolidaysById, false);
}
function sucessGetOfficialHolidaysById(result)
{
    var DateNow = moment(Date(Date.now())).format('DD/MM/YYYY');
    DateNow = convertStringToDate(DateNow);
    var officialHolidaysDateForDeleted = result.Data.officialHolidaysDate;
    officialHolidaysDateForDeleted = convertStringToDate(officialHolidaysDateForDeleted)
    if (officialHolidaysDateForDeleted < DateNow)
    {
        $("#OfficialHolidaysStart").attr('disabled', 'disabled');
        $("#OfficialHolidaysName").attr('disabled', 'disabled');
        $("#OfficialHolidaysAddedBy").attr('disabled', 'disabled');
        $("#btnForSaveOfficialHolidays").attr('disabled', 'disabled');
        $("#OfficialHolidaysTypeYes").attr("disabled", 'disabled');
        $("#OfficialHolidaysTypeNo").attr("disabled", 'disabled');

    }
    $("#OfficialHolidaysStart").val(result.Data.officialHolidaysDate);
    $("#OfficialHolidaysName").val(result.Data.officialHolidaysName)
    $("#OfficialHolidaysAddedBy").val(result.Data.AddedBy)
    $("#OfficialHolidaysId").val(result.Data.officialHolidaysId)

    if (result.Data.IsOfficial == "1")
    {
        $("#OfficialHolidaysTypeYes").attr("checked", true);
    }
    else
    {
        $("#OfficialHolidaysTypeNo").attr("checked", true);
    }
}
$("#btnForSaveOfficialHolidays").on("click", function () {
    var officialDate = $("#OfficialHolidaysStart").val();
    var officialName = $("#OfficialHolidaysName").val();
    var officialType = $("input[name = 'OfficialType']:checked").val();
    if (officialDate == "" || officialName == "" || officialType == "") {
        ShowALert(1, "You Must Enter All Data");

    }
    var urlEditOfficialHolidays = ConfigurationData.GlobalApiURL + "/OfficialHolidays/EditOfficialHolidays";//will be change
    var NewOfficialHolidays =
        {
            OfficialHolidaysStart: officialDate,
            OfficialHolidaysName: officialName,
            OfficialHolidaysType: officialType,
            UserId: LoggedUserData.GlobalUserID,
            OfficialHolidaysId:getParameterByName("id")
        }
    Common.Ajax('post', urlEditOfficialHolidays, JSON.stringify(NewOfficialHolidays), 'json', sucessEditOrAddOfficialHolidays, false);

})
function sucessEditOrAddOfficialHolidays(Result)
{
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        window.location.href = '/Configuration/Holidays';
        //GetAllOfficialHolidays()
    }
    else {
        ShowALert(4, Result.Message);

    }

}
/*---------------------------------------------------------------------------------------------*/
/*Add New Official*/
$("#AddNewOfficialHolidays").on("click", function () {
    //console.log("Okey");
});
$("#btnForAddNewOfficialHolidays").on("click", function () {
    var officialDate = $("#OfficialHolidaysStart").val();
    var officialName = $("#OfficialHolidaysName").val();
    var officialType = $("input[name = 'OfficialType']:checked").val();
    if (officialDate == "" || officialName == "" || officialType == "")
    {
        ShowALert(1, "You Must Enter All Data");

    }
    else
    {

    var urlCheckIfThisDayHaveOfficialHoldays = ConfigurationData.GlobalApiURL + "/OfficialHolidays/CheckIfThisDayHaveOfficialHoldays"
    var NewOfficialHolidays=
        {
            OfficialHolidaysStart: officialDate,
            OfficialHolidaysName: officialName,
            OfficialHolidaysType: officialType,
            UserId: LoggedUserData.GlobalUserID
        }
    Common.Ajax('post', urlCheckIfThisDayHaveOfficialHoldays, JSON.stringify(NewOfficialHolidays), 'json', sucessCheckIfThisDayHaveOfficialHoldays, false);

    }
})
function sucessCheckIfThisDayHaveOfficialHoldays(result)
{
    if (result == 0)
    {
        ShowALert(3, "This Day Is less than Today Date " + moment(Date.now()).format('DD/MM/YYYY'));
    }
    else if (result == 2)
    {
        ShowALert(3, "This Day Is Have Official Holidays!!");
    }
    else
    {
        var officialDate = $("#OfficialHolidaysStart").val();
        var officialName = $("#OfficialHolidaysName").val();
        var officialType = $("input[name = 'OfficialType']:checked").val();
        var urlAddNewOfficialHolidays = ConfigurationData.GlobalApiURL + "/OfficialHolidays/AddNewOfficialHolidays";//will be change
        var NewOfficialHolidays =
            {
                OfficialHolidaysStart: officialDate,
                OfficialHolidaysName: officialName,
                OfficialHolidaysType: officialType,
                UserId: LoggedUserData.GlobalUserID
            }
        Common.Ajax('post', urlAddNewOfficialHolidays, JSON.stringify(NewOfficialHolidays), 'json', sucessEditOrAddOfficialHolidays, false);
    }

}
/*---------------------------------------------------------------------------------------------*/
//globel function
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
function convertStringToDate(stringDate) {
    listStringDate = stringDate.split('/');
    return new Date(listStringDate[2], listStringDate[1] - 1, listStringDate[0])
}
