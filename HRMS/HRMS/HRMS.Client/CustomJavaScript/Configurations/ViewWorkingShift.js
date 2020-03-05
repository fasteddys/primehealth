var Shifts = [];

$(document).ready(function () {

    GetAllShifts();

});

function GetAllShifts() {
    var urlAllShifts = ConfigurationData.GlobalApiURL + "/WorkingShift/GetAllWorkingShift";
    Common.Ajax('GET', urlAllShifts, null, 'json', SucessGetShifts, false);
}

function SucessGetShifts(ShiftsResult) {
    $('#ShiftsListTable').DataTable().destroy();
    $('#ShiftsListTable').DataTable({
        retrieve: true,
        //"ordering": false,
        "data": ShiftsResult.Data,
        "columns": [
            { "data": "ShiftName" },//1
            { "data": "ShiftStart" },//1
            { "data": "ShiftEnd" },//2
            { "data": "GraceIn" },//3
            { "data": "GraceOut" },//4
            //{ "data": "AddedBy" },//5
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    //return '<a id="editOfficialHolidays"  data-value=' + data.officialHolidaysId +' class="btn btn-info btn-sm" href=/Configuration/Edit?officialHolidaysId=' + data.officialHolidaysId + '>' + 'Edit' + '</a>' ;
                    return '<a  class="btn btn-info btn-sm" href="/Configuration/EditWorkingShift?ID=' + data.ShiftID + '">' + 'Edit' + '</a>';

                }

            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<a id="" data-value=' + data.ShiftID + ' class="btn btn-danger btn-sm disable" href="#" >' + 'Delete' + '</a>';
                }
            }

        ]
    });

    //Shifts = ShiftsResult;
    //for (var i = 0; i < Shifts.Data.length; i++) {
    //    $("#Shift").append('<tr class="clickable-row"><td>' + Shifts.Data[i].ShiftName + '</td>' + 
    //        '<td>' + Shifts.Data[i].ShiftStart + '</td>' +
    //        '<td>' + Shifts.Data[i].ShiftEnd + '</td>' +
    //        '<td>' + Shifts.Data[i].GraceIn + '</td>' +
    //        '<td>' + Shifts.Data[i].GraceOut + '</td>' +
    //        '<td>' + Shifts.Data[i].IsActive + '</td>' +
    //        + '</tr > ');
    //}
}


$(document).on("click",".disable", function () {
    var RecordID = $(this).data("value");

    var urlDisableShift = ConfigurationData.GlobalApiURL + "/WorkingShift/DisapleWorkingShift?ID=" + RecordID;
    Common.Ajax('POST', urlDisableShift, null, 'json', SucessDeleteShift, false);
});

function SucessDeleteShift(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);
    }
}