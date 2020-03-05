$(document).ready(function () {
    GetDepartmentsData();
});

function GetDepartmentsData() {
    var urlGetDepartmentsData = ConfigurationData.GlobalApiURL + "/Department/GetAllDepartments";
    Common.Ajax('get', urlGetDepartmentsData, null, 'json', SucessGetDepartmentsData, false);
}

function SucessGetDepartmentsData(Result) {
    for (var x = 0; x < Result.Data.length; x++) {
        var DepartmentOption = '<option value="' + Result.Data[x].DepartmentID + '">' + Result.Data[x].DepartmentName + '</option>';
        $("#Department").append(DepartmentOption);
    }
}

function AddShift() {
    if ($('#Name').val() === '' || $('#Start').val() === '' || $('#End').val() === '' || $('#GraceIn').val() === '' || $('#GraceOut').val() === '' || $('#Department').val() === '')
        ShowALert(4, 'You must enter Required Fields');
    else {
        var shiftObj = {
            ShiftName: $('#Name').val(),
            ShiftStart: ConvertFromShotTimeToongTime($('#Start').val()),
            ShiftEnd: ConvertFromShotTimeToongTime($('#End').val()),
            GraceIn: $('#GraceIn').val(),
            GraceOut: $('#GraceOut').val(),
            DepartmentID: $("#Department").val(),
            AddedByUser: LoggedUserData.GlobalUserID
        };

        var urlAddShift = ConfigurationData.GlobalApiURL + "/WorkingShift/AddNewWorkingShift";
        Common.Ajax('post', urlAddShift, JSON.stringify(shiftObj), 'json', SucessAddShift, false);
    }
}

function SucessAddShift(Result) {
    if (Result.Success === true)
        ShowALert(2, Result.Message);
    else if (Result.Success === false)
        ShowALert(4, Result.Message);
}

function ConvertFromShotTimeToongTime(Time) {
    var time = Time;
    var hours = Number(time.match(/^(\d+)/)[1]);
    var minutes = Number(time.match(/:(\d+)/)[1]);
    var AMPM = time.match(/\s(.*)$/)[1];
    if (AMPM == "PM" && hours < 12) hours = hours + 12;
    if (AMPM == "AM" && hours == 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;
    return sHours + ":" + sMinutes + ":" + "00";
}