var ShiftID;
$(document).ready(function () {
    ShiftID = getParameterByName("ID");
    GetDepartmentsData();
    GetShiftData(ShiftID);
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

function GetShiftData(ID) {
    var urlGetShiftData = ConfigurationData.GlobalApiURL + "/WorkingShift/GetWorkingShift?ID=" + ID;
    Common.Ajax('get', urlGetShiftData, null, 'json', SucessGetWorkingShift, false); 
}

function SucessGetWorkingShift(Result) {
    if (Result.Success) {
        DisplayData(Result.Data);
    }
    else {
        ShowALert(4, Result.Message);
    }
}

function DisplayData(Data) {
    $('#Name').attr("value", Data.ShiftName);
    $('#Start').attr("value", Data.ShiftStart);
    $('#End').attr("value", Data.ShiftEnd);
    $('#GraceIn').attr("value", Data.GraceIn);
    $('#GraceOut').attr("value", Data.GraceOut);
    $("#Department").val(Data.DepartmentID);
}

function EditShift() {
    var shiftObj = {
        ShiftID: ShiftID,
        ShiftName: $('#Name').val(),
        ShiftStart: ConvertFromShotTimeToongTime($('#Start').val()),
        ShiftEnd: ConvertFromShotTimeToongTime($('#End').val()),
        GraceIn: $('#GraceIn').val(),
        GraceOut: $('#GraceOut').val(),
        DepartmentID: $('#Department').val()
    };

    var urlAddShift = ConfigurationData.GlobalApiURL + "/WorkingShift/EditWorkingShift";
    Common.Ajax('post', urlAddShift, JSON.stringify(shiftObj), 'json', SucessAddShift, false);
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
    if (AMPM === "PM" && hours < 12) hours = hours + 12;
    if (AMPM === "AM" && hours === 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;
    return sHours + ":" + sMinutes + ":" + "00";
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}