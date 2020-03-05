$(document).ready(function () {
    //GetShiftRange();
    //CheckEmptyDates();
    GetUsers();
    GetShifts();
    $("#Export").on("click", false);
    //document.getElementById("Export").disabled = false;
});

function GetUsers() {
    var urlGetUsersOfManager = ConfigurationData.GlobalApiURL + "/ApprovalFlow/GetUserApprovedByManager?UserIDOfManager=" + LoggedUserData.GlobalUserID;
    Common.Ajax('GET', urlGetUsersOfManager, null, 'json', GetUsersOfManager, false);
}

function GetUsersOfManager(Result) {
    for (var x = 0; x < Result.Data.length; x++) {
        var UserOption = '<option value="' + Result.Data[x].UserID + '">' + Result.Data[x].UserName + '</option>';
        $("#Users").append(UserOption);
    }
}

function GetShifts() {
    var urlGetShifts = ConfigurationData.GlobalApiURL + "/WorkingShift/GetAllActiveWorkingShift";
    Common.Ajax('GET', urlGetShifts, null, 'json', SucessGetShifts, false);
}

function SucessGetShifts(Result) {
    for (var x = 0; x < Result.Data.length; x++) {
        var ShiftsOption = '<option value="' + Result.Data[x].ShiftID + '">' + Result.Data[x].ShiftName + '</option>';
        $("#Shifts").append(ShiftsOption);
    }
}

function CheckEmptyDates() {
    if ($('#ShiftFrom').val() === '' || $('#ShiftTo').val() === '')
        ShowALert(4, 'You must enter From & To');
    else
        GetShiftRange();
}

function GetShiftRange() {
    //$("#Export").on("click", false);
    //$("#Export").attr("disabled", "disabled");
    //document.getElementById("Export").disabled = true;
    var shiftObj = {
        ManagerID: LoggedUserData.GlobalUserID,
        ShiftFrom: $('#ShiftFrom').val(),
        ShiftTo: $('#ShiftTo').val()
    };

    var urlCreateExel = ConfigurationData.GlobalApiURL + "/WorkingShift/ExtractShiftsTemplateExcel";
    //Common.Ajax('post', urlCreateExel, JSON.stringify(shiftObj), 'json', SucessGetShiftsTemplate, false);

    var today = new Date();
    var date = GetTodayDate();

    xhttp = new XMLHttpRequest();

    // Post data to URL which handles post request
    xhttp.open("POST", urlCreateExel);
    xhttp.setRequestHeader("Content-Type", "application/json");
    // You should set responseType as blob for binary responses
    xhttp.responseType = 'blob';
    xhttp.send(JSON.stringify(shiftObj));

    xhttp.onreadystatechange = function () {
        var a;
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            // Trick for making downloadable link
            a = document.createElement('a');
            a.href = window.URL.createObjectURL(xhttp.response);
            // Give filename you wish to download
            a.download = "UsersShiftsTemplate " + date + ".xlsx";
            a.style.display = 'none';
            document.body.appendChild(a);
            a.click();
            //document.getElementById("Export").disabled = false;
            //$("#Export").prop("disabled", false);
            //$("#Export").off("click");
        }
    };   
}

function SucessGetShiftsTemplate() {

}

function UploadFile() {
    
    var xhr = new XMLHttpRequest();
    var file = document.getElementById('myfile').files[0];
    var loggedUser = LoggedUserData.GlobalUserID;

    var FileUploadParameters = {
        FileName: file.name,
        LoggedUserID: LoggedUserData.GlobalUserID
    };
    var urlUploadFile = ConfigurationData.GlobalApiURL + "/WorkingShift/MyFileUpload";
    //Common.Ajax('POST', urlUploadFile, JSON.stringify(FileUploadParameters), 'json', SucessUploadFile, false);

    xhr.open("POST", urlUploadFile);
    xhr.setRequestHeader("filename", file.name);
    xhr.setRequestHeader("loggedUserID", loggedUser);
    xhr.send(file);

    xhr.onloadend = function () {
        if (this.readyState === 4 && this.status === 200) {
            ShowALert(2, "File Uploaded & Shifts Added Successfully");
        }
        else if (this.status === 400) {
            ShowALert(1, this.responseText);
        }
        else {
            ShowALert(4, this.responseText);
        }
    };
}

function SucessUploadFile(Result) {
    if (Result.Success === true) {
        ShowALert(2, "File Uploaded Successfully");
    }
    else {
        ShowALert(4, "File Upload Failed");
    }
}

$('#Users').change(function () {
    var currentUserID = $('#Users :selected').val();
    var shiftDate = $('#ShiftDate').val();

    var urlGetUserShiftName = ConfigurationData.GlobalApiURL + "/WorkingShift/GetUserShiftName?UserID=" + currentUserID + "&ShiftDate=" + shiftDate;
    Common.Ajax('GET', urlGetUserShiftName, null, 'json', SucessGetUserShiftName, false);
});

function SucessGetUserShiftName(Result) {
    if (Result.Data !== '')
        $('#ShiftInfo').val(Result.Data);
    else 
        $('#ShiftInfo').val('Off');
}

function SwapShiftValidation() {
    if ($('#ShiftDate').val() === '' || $('#Users').val() === null || $("#Shifts").val() === null)
        ShowALert(4, "You must enter required fields");
    else
        SwapShift();
}

function SwapShift() {
    var swapShiftDTO = {
        UserID: $('#Users').val(),
        ShiftDate: $('#ShiftDate').val(),
        OldShiftName: $('#ShiftInfo').val(),
        NewShiftID: $("#Shifts").val(),
        LoggedUserID: LoggedUserData.GlobalUserID
    };

    var urlSwapShift = ConfigurationData.GlobalApiURL + "/WorkingShift/SwapShift";
    Common.Ajax('POST', urlSwapShift, JSON.stringify(swapShiftDTO), 'json', SucessSwapShift, false);
}

function SucessSwapShift(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);
    }
}

function GetTodayDate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = dd + '-' + mm + '-' + yyyy;
    return today;
}