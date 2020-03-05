$(document).ready(function () {
});
var WorkingWeek;
var Save = function () {
    var Days = [];
    for (var i = 1; i < 8; i++)
    {
        var name = "#Day"+i;
        var Row = $(name);
        //
        var FromTime =ConvertFromShotTimeToongTime( Row.find(".From")[0].value);
        var ToTime =ConvertFromShotTimeToongTime( Row.find(".To")[0].value);
        var Totalhours = Row.find(".Totalhours")[0].value;
        var GraceIn = Row.find(".GraceIn")[0].value;
        var GraceOut = Row.find(".GraceOut")[0].value;

        var IsActive = Row.find(".IsActive")[0].checked;
        if (Totalhours === "") {
            Totalhours = 0;
        }


        var day = {
            DayID: i,
            StartTime: FromTime,
            EndTime: ToTime,
            WorkingDuration: Totalhours,
            GraceIn: GraceIn,
            GraceOut: GraceOut,
            IsActive: IsActive
        };
        
        Days.push(day);
    }

    WorkingWeek = {
        WorkingWeekName: $("#WorkingWeekName")[0].value,
        ListWorkingWeekDetails: Days
    };


    var urlWorkingWeek = ConfigurationData.GlobalApiURL + "/WorkingWeek/AddNewWorkingWeek";
    Common.Ajax('post', urlWorkingWeek, JSON.stringify(WorkingWeek), 'json', SucessSave, false);



}

function SucessSave(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(1, Result.Message);


    }
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
    return sHours + ":" + sMinutes+ ":" +"00";
}