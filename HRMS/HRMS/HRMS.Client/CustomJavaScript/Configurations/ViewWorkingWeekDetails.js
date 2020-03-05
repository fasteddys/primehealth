
$(document).ready(function () {
    
    var urlWorkingWeek = ConfigurationData.GlobalApiURL + "/WorkingWeek/SelectWorkingWeek?WorkingWeekID=" + getParameterByName("WorkingWeekID");
    Common.Ajax('get', urlWorkingWeek, null, 'json', SucessGetDetails, false);
});

function SucessGetDetails(Result) {
    var x = $("#WorkingWeekName");

    $("#WorkingWeekName")[0].value = Result.Data.WorkingWeekName;

    for (var i = 0; i < Result.Data.WorkingWeekDetailsDTO.length; i++)
    {
        var Checked="";

        if (Result.Data.WorkingWeekDetailsDTO[i].IsActive === true) {
            Checked = "Checked";
        }
        $("#Days").append("<tr id='Day"+i+"'><td width='10%'>" + Result.Data.WorkingWeekDetailsDTO[i].DayName +
            "</td><td width='20%'><input type='text' value=" + Result.Data.WorkingWeekDetailsDTO[i].StartTime +
            " data-plugin-timepicker='' class='form-control From'></td><td width='20%'><input type='text' value=" + Result.Data.WorkingWeekDetailsDTO[i].EndTime
            + " data-plugin-timepicker='' class='form-control To'></td><td width='10%'><input type='text' value=" + Result.Data.WorkingWeekDetailsDTO[i].WorkingDuration + " class='form-control Totalhours' /></td><td width='10%'><input type='text' value=" + Result.Data.WorkingWeekDetailsDTO[i].GraceIn + " class='form-control GraceIn' /></td><td width='10%'><input type='text' value=" + Result.Data.WorkingWeekDetailsDTO[i].GraceOut + " class='form-control GraceOut' /></td><td width='10%'><input type='checkbox' " + Checked + " class='IsActive'></td></tr>");
    }

}

function Edit() {
    var Days = [];
    for (var i = 0; i < 7; i++) {
        var name = "#Day" + i;
        var Row = $(name);
        var DayID = i + 1;
        //
        var x = Row.find(".From")[0];
        var FromTime = ConvertFromShotTimeToongTime(Row.find(".From")[0].value);
        var ToTime = ConvertFromShotTimeToongTime(Row.find(".To")[0].value);
        var Totalhours = Row.find(".Totalhours")[0].value;
        var GraceIn = Row.find(".GraceIn")[0].value;
        var GraceOut = Row.find(".GraceOut")[0].value;

        var IsActive = Row.find(".IsActive")[0].checked;
        if (Totalhours === "") {
            Totalhours = 0;
        }


        var day = {
            DayID: DayID,
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
        WorkingWeekID: getParameterByName("WorkingWeekID"),
        WorkingWeekName: $("#WorkingWeekName")[0].value,
        ListWorkingWeekDetails: Days
    };


    var urlWorkingWeek = ConfigurationData.GlobalApiURL + "/WorkingWeek/EditWorkingWeekWorkingWeekDetail";
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

function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
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












