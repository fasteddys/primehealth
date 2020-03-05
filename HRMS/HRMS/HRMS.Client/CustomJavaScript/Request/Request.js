var LeaveTypes = [];
var UploadAttachment = false;
var Start ;
var End;
var ListWorkingDayDetails;
$(document).ready(function () {
    $("#UserDB").hide();
    $("#TimeDiv").hide();
    $("#PartialUnitTypeDiv").hide();
    $("#PartialUnitDiv").hide();

    
    
    GetAllLeaveType();
    GetEmployeesList();
    GetLeavesTypeFields();
    GetMinOneDayDurationByDurationUnit();
});

var LeavesTypeField;


function GetAllLeaveType() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes?UserID=" + LoggedUserData.GlobalUserID;
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetLeaves, false);
}

function SucessGetLeaves(Leaves) {
    LeaveTypes = Leaves;
    for (var i = 0; i < LeaveTypes.Data.length; i++)
    {
        $("#LeaveType").append('<option Unit="' + LeaveTypes.Data[i].Unit+'" value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
    }


}

$("#TimeFrom, #PartialUnitType").change(
   function () {

        var z = $("#TimeFrom").val();
        var str1 = z.split(":");
        var number = parseInt(str1[0]) + parseInt($("#PartialUnitType").val());

        if (number >= 12) {
            if (number == 12) {
                number = 12;
            } else {
                number = number - 12;
            }
            if (str1[0] < 12 && number <= 12) {
                if (str1[1].includes("PM")) {
                    str1[1] = str1[1].replace("PM", "AM");

                }
                else if (str1[1].includes("AM")) {
                    str1[1] = str1[1].replace("AM", "PM");
                }
            }
            
        }
        
        var string = number+":" +str1[1];
        $("#TimeTo").val(string);
        //var x = $("#PartialUnitType").val();
        //var m = $("#TimeFrom");

    });
var AddNewRequest = function () {
    //Object Manager
    var StartDate;
    var Message = "";
    var IsValid = true;
    var x = $('option:selected', $("#LeaveType")).attr('Unit');
    var EndDate;
    if ($('option:selected', $("#LeaveType")).attr('Unit') === "Days") {

        StartDate = $("#StartDate")[0].value;
        EndDate = $("#EndDate")[0].value;
    }
    else if ($('option:selected', $("#LeaveType")).attr('Unit') === "Hours") {
        StartDate = $("#StartDate")[0].value + " " + ConvertFromShotTimeToongTime($("#TimeFrom")[0].value);
        EndDate = $("#StartDate")[0].value + " " + ConvertFromShotTimeToongTime($("#TimeTo")[0].value);

        var TimeFrom = timeToSeconds(ConvertFromShotTimeToongTime($("#TimeFrom")[0].value));
        var TimeTo = timeToSeconds(ConvertFromShotTimeToongTime($("#TimeTo")[0].value));

        GetStartAndEndWorkingTime();
       // var m = timeToSeconds(Start);
        // var t = timeToSeconds(End);
        if (ListWorkingDayDetails.WorkingMode === "Regular") {


            if ((TimeFrom < timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[0].DayStart) || TimeFrom > timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[0].DayEnd)) || (TimeTo < timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[0].DayStart) || TimeTo > timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[0].DayEnd))) {

                IsValid = false;
                Message = Message + "Time Must Be Between " + ListWorkingDayDetails.WorkingDayDetailsDTO[0].DayStart + " and " + ListWorkingDayDetails.WorkingDayDetailsDTO[0].DayEnd;
            }
        }
        else if (ListWorkingDayDetails.WorkingMode === "Shift")
        {
            if (ListWorkingDayDetails.IsOffDay === true) {
                IsValid = false;
                Message = Message + "There Is No Shift In This Time";

            }
            else {
                for (var i = 0; i < ListWorkingDayDetails.WorkingDayDetailsDTO.length; i++)
                {
               
                    if (ListWorkingDayDetails.WorkingDayDetailsDTO[i].IsBetweenTwoDaysRequest)
                    {
                        var nextDayDate = $('#StartDate').datepicker('getDate');
                        //var nextDayDate = new Date();
                        //nextDayDate.setDate(date2.getDate() + 1);

                        var dd = nextDayDate.getDate()+1;
                        var mm = nextDayDate.getMonth()+1; //January is 0!

                        var yyyy = nextDayDate.getFullYear();
                        if (dd < 10)
                        {
                            dd = '0' + dd;
                        }
                        if (mm < 10)
                        {
                            mm = '0' + mm;
                        }
                        var nextDayDatetxt = dd + '/' + mm + '/' + yyyy;
                        var m = (ConvertFromShotTimeToongTime($("#TimeFrom")[0].value));
                        var z = (ConvertFromShotTimeToongTime($("#TimeTo")[0].value));
                        var v1 = TimeFrom < timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayStart);
                        var v2 = TimeTo < timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayEnd);
                        if ((TimeFrom < timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayStart)) ||
                            (TimeTo < timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayEnd)))
                        {

                            IsValid = false;
                            if (Message == "")
                            {
                                Message = Message + "Time Must Be Between " + ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayStart + " and " + ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayEnd;
                            }
                            else
                            {
                                Message = Message + ", Between " + ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayStart + " and " + ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayEnd;
                                
                            }
                        }
                        else
                        {
                            EndDate = nextDayDatetxt + " " + ConvertFromShotTimeToongTime($("#TimeTo")[0].value);
                            IsValid = true;
                            break;
                        }
                    }
                    else
                    {

                        if ((TimeFrom < timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayStart) ||
                            TimeFrom > timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayEnd)) ||
                            (TimeTo < timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayStart) ||
                                TimeTo > timeToSeconds(ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayEnd)))
                        {

                            IsValid = false;
                            if (Message == "") {
                                Message = Message + "Time Must Be Between " + ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayStart + " and " + ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayEnd;

                            }
                            else {

                                Message = Message + ", Or Between " + ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayStart + " and " + ListWorkingDayDetails.WorkingDayDetailsDTO[i].DayEnd;
                                
                            }

                            }
                        else
                        {
                            IsValid = true;
                            break;


                        }
                    }
                }
            }

        }

    }

    var LeveType = $("#LeaveType")[0].value;
    var UserID;
    var OnBehalfOfRequesterID;
    if ($("#AddRequestOnBehalfOf").attr("isInOnBehalfOf") === "false") {
        UserID = LoggedUserData.GlobalUserID;

    }
    else {
        UserID = $("#User")[0].value;
        OnBehalfOfRequesterID = LoggedUserData.GlobalUserID;


    }

    var RequestPartialUnitID;
    var x = $("#PartialUnit").val();
    RequestPartialUnitID = $("#PartialUnit").val();

    var NewRequest = {
        UserID: UserID,
        LeaveTypeID: LeveType,
        RequestStart: StartDate,
        RequestEnd: EndDate,
        RequestPartialUnitID: RequestPartialUnitID,
        Reason: null,
        Substitute: null,
        Comment: null,
        OnBehalfOfRequesterID: OnBehalfOfRequesterID
    };

    if ($("#Reason").attr("fieldstatus") === "Required" && $("#Reason").val() === "") {
        IsValid = false;
        Message = Message + " Reason is Requird ";

    }
    else {
        if ($("#Reason").val() === "") {
            NewRequest.Reason = null;
        }
        else { NewRequest.Reason = $("#Reason").val(); }
    }

    if ($("#Comment").attr("fieldstatus") === "Required" && $("#Comment").val() === "") {

        IsValid = false;
        Message = Message + "Comment Is Requird ";

    }
    else {
        if ($("#Comment").val() === "") {
            NewRequest.Comment = null;
        }
        else { NewRequest.Comment = $("#Comment").val(); }
    }




    if ($("#Substitute").attr("fieldstatus") === "Required" && $("#Substitute").val() === "") {

        IsValid = false;
        Message = Message + "Substitute Required ";


    }
    else {
        if ($("#Substitute").val() === "") {
            NewRequest.Substitute = null;
        }
        else { NewRequest.Substitute = $("#Substitute").val(); }
    }

    if ($("#Attachment").attr("fieldstatus") === "Required" && $("#Attachment").val() === "") {
        IsValid = false;
        Message = Message + " Attachment is Requird ";

    }
    else {
        if ($("#Attachment").val() === "" || $("#Attachment").val() == null) {
            $("#Attachment").val(null);
        }

        else {
            UploadAttachment = true;
        }
    }



    if (IsValid) {
        var urlGetUserByID = ConfigurationData.GlobalApiURL + "/Request/AddRequest";
        Common.Ajax('post', urlGetUserByID, JSON.stringify(NewRequest), 'json', SucessSubmitRequest, false);
    }
    else {

        ShowALert(1, Message);
        Message = "";
    }
};
function SucessSubmitRequest(Result) {
    var IsValid = true;
    if (Result.Success === true && UploadAttachment === true) {
        //var regex = new RegExp('##$$', 'g');
        //Result.Message = Result.Message.replace(regex, "<br/>");
        FileUpload(Result.Data);
        ShowALert(2, Result.Message);
    }
    else if (Result.Success === false) {
        //var regex = new RegExp('##$$', 'g');
        //Result.Message = Result.Message.replace(regex, "<br/>");
        ShowALert(4, Result.Message);

    }
    else if (Result.Success === true) {
        //var regex = new RegExp('##$$', 'g');
        //Result.Message = Result.Message.replace(regex, "<br/>");

        ShowALert(2, Result.Message);

    }
  

}
$("#LeaveType").change(function () {
    //var AccessContoSelect = $("#AccessContolListSelect").val();

  
        GetMinOneDayDurationByDurationUnit();
        $("#PartialUnitType").change();
    
    var SelectRow = $('option:selected', this).attr('Unit');

    if (SelectRow === "Days")
    {
      //  document.getElementById("StartDate").style.display = "visible";
      //  document.getElementById("EndDateDiv").style.display = "initial";
        $("#EndDateDiv").show();
        $("#TimeDiv").hide();

    }
    else {

       // document.getElementById("EndDateDiv").style.display = "none";
        $("#EndDateDiv").hide();
        $("#TimeDiv").show();

    }
    


    $("#ReasonDiv").remove();
    $("#SubstituteDiv").remove();
    $("#CommentDiv").remove();
    $("#AttachmentDiv").remove();
    
    GetLeavesTypeFields();


   // $("#EndDate")[0].

  //  $("#ACUserName").text(AccessContoSelect);
  //  $("#ACUserID").text(SelectRow);

});
//CheckIfDay
function GetEmployeesList() {
    urlGetEmployeesList = ConfigurationData.GlobalApiURL + "/Users/GetOnBehalfOfUsers?UserID=" + LoggedUserData.GlobalUserID;
    Common.Ajax('GET', urlGetEmployeesList, null, 'json', SucessGetEmployees, false);
}
function SucessGetEmployees(EmployeesList) {
    AppendEmployeesList(EmployeesList);

    if (EmployeesList.Data.length === 0) {
        $("#AddRequestOnBehalfOf").hide(); 
    }
}

function AppendEmployeesList(EmployeesList) {
    for (var i = 0; i < EmployeesList.Data.length; i++) {

        $("#User").append('<option ID="' + EmployeesList.Data[i].UserID +
            '" value="' + EmployeesList.Data[i].UserID + '">' +
            EmployeesList.Data[i].UserName + '</option>');

    }
}

$("#AddRequestOnBehalfOf").click(function () {
    var x= $("#AddRequestOnBehalfOf").attr("isInOnBehalfOf");

    if ($("#AddRequestOnBehalfOf").attr("isInOnBehalfOf") === "true") {
        $("#AddRequestOnBehalfOf").css('color', 'gray');
        $("#UserDB").hide();
        $("#AddRequestOnBehalfOf").attr("isInOnBehalfOf", "false");
        var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes?UserID=" + LoggedUserData.GlobalUserID;
        Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetOnBehalfLeaves, false);
    } else {
        $("#UserDB").show();
        $("#AddRequestOnBehalfOf").css('color', '#0088cc');
        $("#AddRequestOnBehalfOf").attr("isInOnBehalfOf", "true");
        var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes?UserID=" + $('option:selected', $("#User")).attr('id');
        Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetOnBehalfLeaves, false);

    }

});
function GetLeavesTypeFields() {
    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/LeaveType/GetLeavesTypeFields?LeaveTypeID=" + $("#LeaveType").val();
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetLeavesTypeFields, false);
    //LeaveType

}
function SucessGetLeavesTypeFields(Result) {
    if (Result.Data !== null)
        {
        var LeavesTypeField = {
            LeaveTypeField: Result.Data.LeaveTypeField,
            HasAttachemnt: Result.Data.HasAttachemnt,
            AttachemntIsRequired: Result.Data.AttachemntIsRequired

        };
    

    for (var i = 0; i < LeavesTypeField.LeaveTypeField.length; i++) {

        if (LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldVisibilityName !== "Disabled") {

            if (LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldName !== "Substitute") {
                $("#RequestData").append('<div class="form-group row" id = "' + LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldName
                    + 'Div" > <label class="col-lg-3 control-label">' + LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldName
                    + '</label> <div class="col-lg-6"><div class="input-group"><input type="text" class="form-control" id="'
                    + LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldName + '" FieldStatus="' + LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldVisibilityName + '"></div></div></div>');
            } else {

                $("#RequestData").append('<div class="form-group row" id = "' + LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldName
                    + 'Div" > <label class="col-lg-3 control-label">' + LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldName
                    + '</label> <div class="col-lg-6"><div class="input-group"><select data-plugin-selectTwo class="form-control populate" id="'
                    + LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldName + '" FieldStatus="' + LeavesTypeField.LeaveTypeField[i].LeaveTypeFieldVisibilityName + '"></select></div></div></div>');
                GetAllUsersInSameDepartment();
            }
        }
    }
    if (LeavesTypeField.HasAttachemnt === true) {
        if (LeavesTypeField.AttachemntIsRequired === true) {

            $("#RequestData").append(
                '<div class="form-group row" id="AttachmentDiv"><label class="col-lg-3 control-label">Attachment</label><div class="col-lg-6"><div class="input-group"><input type="file" multiple FieldStatus="'
                + "Required" +
                '" class="form-control Cimgupload" id="Attachment" /></div></div></div>');
        }
        else {

            $("#RequestData").append(
                '<div class="form-group row" id="AttachmentDiv"><label class="col-lg-3 control-label">Attachment</label><div class="col-lg-6"><div class="input-group"><input type="file" multiple FieldStatus="'
                + "Optional" +
                '" class="form-control Cimgupload" id="Attachment" /></div></div></div>');
        }

    }
}
   
}
function GetAllUsersInSameDepartment() {

    var urlGetAllUsersInSameDepartment = ConfigurationData.GlobalApiURL + "/Department/GetAllUserInSameDeparment?UserID=" + LoggedUserData.GlobalUserID;
    Common.Ajax('get', urlGetAllUsersInSameDepartment, null, 'json', SucessGetAllUsersInSameDepartment, false);



}
function SucessGetAllUsersInSameDepartment(Result) {
    $("#Substitute").append('<option value=""></option>');

    for (var i = 0; i < Result.Data.length; i++) {

        $("#Substitute").append('<option value="' + Result.Data[i].UserID + '">' + Result.Data[i].UserName + '</option>');

    }
}
//function FileUpload (RequestID) {
//    var UrlAddProfileImg = '/Request/AddRequestAttachment?RequestID=' + RequestID;

//    var DisplayImg = GetAttachments("Attachment", "Cimgupload");
//    $.ajax({

//        type: "POST",
//        url: UrlAddProfileImg,
//        data: DisplayImg,
//        dataType: 'json',
//        contentType: false,
//        processData: false,
//        Success: SuccessFileUpload()
//        });
//}
//function SuccessFileUpload(Result) {
    
//    var urlAddFileUpload = ConfigurationData.GlobalApiURL + "/Request/SaveFilesPath/";
//    Common.Ajax('Post', urlAddFileUpload, JSON.stringify(Result.Data), 'json', SuccessFileIsUpload, false);
//}
function FileUpload(RequestID) {
    var UrlAddProfileImg = '/Request/AddRequestAttachment?RequestID=' + RequestID;

    var DisplayImg = GetAttachments("Attachment", "Cimgupload");
    $.ajax({

        type: "POST", url: UrlAddProfileImg,
        data: DisplayImg, dataType: 'json', contentType: false, processData: false,
        success: function (result) {
           
            var urlAddFileUpload = ConfigurationData.GlobalApiURL + "/Request/SaveFilesPath/";
            var RequestAttachment = {
                RequestID: RequestID,
                AttachmentPath: result.Data
            };

            Common.Ajax('Post', urlAddFileUpload, JSON.stringify(RequestAttachment), 'json', SuccessFileIsUpload, false);
          
        }
    });
}
function SuccessFileIsUpload(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);

    }

}
function GetAttachments(InputID, InputClass) {
    var formData = new FormData();
    if (document.getElementById(InputID) !== null) {
        var totalFiles = document.getElementById(InputID).files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementsByClassName(InputClass)[0].files[i];
            formData.append(InputID, file, file.name);
        }
        return formData;
    }
}
function ConvertFromShotTimeToongTime(Time) {
    var time = Time;
    var hours = Number(time.match(/^(\d+)/)[1]);
    var minutes = Number(time.match(/:(\d+)/)[1]);
    var AMPM = time.match(/\s(.*)$/)[1];
    if (AMPM === "PM" && hours < 12) hours = hours + 12;
    if (AMPM === "AM" && hours == 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;
    return sHours + ":" + sMinutes + ":" + "00";
}
function GetMinOneDayDurationByDurationUnit() {
 

        if ($('option:selected', $("#LeaveType")).attr('Unit') === "Days" && $("#EndDate").val() === $("#StartDate").val() && $("#StartDate").val() !== "")
        {
            var urlPartialUnitByLeaveType = ConfigurationData.GlobalApiURL + "/LeaveType/GetOneDayMinDurationByLeaveTypeUnitID?DurationUnitID=" + 1;
            Common.Ajax('GET', urlPartialUnitByLeaveType, null, 'json', SucessGetPartialUnitByLeaveType, false);
        }

        else if ($('option:selected', $("#LeaveType")).attr('Unit') === "Hours")
        {

            var urlPartialUnitByLeaveType = ConfigurationData.GlobalApiURL + "/LeaveType/GetOneDayMinDurationByLeaveTypeUnitID?DurationUnitID=" + 2;
            Common.Ajax('GET', urlPartialUnitByLeaveType, null, 'json', SucessGetPartialUnitByLeaveType, false);

        }
    
    else
    {
        $("#PartialUnitTypeDiv").hide();
        $("#PartialUnitType").empty();
    }
    
}
function SucessGetPartialUnitByLeaveType(Result)
{
    $("#PartialUnitTypeDiv").hide();
    $("#PartialUnitType").empty();

    for (var i = 0; i < Result.Data.length; i++) {
        $("#PartialUnitTypeDiv").show();
        $("#PartialUnitType").append('<option PartialUnitID="' + Result.Data[i].MinOneDayDurationID+'" value="' + Result.Data[i].MinOneDayDurationValue  + '">' +
            Result.Data[i].MinOneDayDurationName + '</option>');

    }
}
function GetPartialDurationUnit() {
  
    var urlPartialUnitByLeaveType = ConfigurationData.GlobalApiURL + "/LeaveType/GetPartialDurationUnit?MinOneDayDuratioID=" +
        $('option:selected', $("#PartialUnitType")).attr('partialunitid');
        Common.Ajax('GET', urlPartialUnitByLeaveType, null, 'json', SucessGetPartialDurationUnit, false);
    



}
function SucessGetPartialDurationUnit(Result) {
    $("#PartialUnitDiv").hide();

    for (var i = 0; i < Result.Data.length; i++) {
        $("#PartialUnitDiv").show();

        $("#PartialUnit").append('<option value="' + Result.Data[i].PartialDurationUnitID + '">' +
            Result.Data[i].PartialDurationUnitName + '</option>');

    }


}
$("#PartialUnitType").change(function () {

    $("#PartialUnit").empty();
    if ($("#EndDate").val() === $("#StartDate").val() && $("#StartDate").val() !== "" &&

        $('option:selected', $("#LeaveType")).attr('Unit') === "Days") {
        GetPartialDurationUnit();
    }
    else if ($('option:selected', $("#LeaveType")).attr('Unit') === "Hours") {
        GetPartialDurationUnit();
    }
    else {
        $("#PartialUnitTypeDiv").hide();
        $("#PartialUnitType").empty();
    }
});
$("#EndDate").change(function () {
    $("#LeaveType").change();

});
$("#StartDate").change(function () {
    $("#LeaveType").change();


});

//$("#StartDate").datepicker({
//    onSelect: function () {
//        $(this).change();




//    }
//});

$(".cancelButton").click(function () {
    //goBack();
    document.getElementsByClassName("cancelButton")[0].setAttribute("href", "javascript:history.go(-1)");
});

function timeToSeconds(time) {
    time = time.split(/:/);
    return time[0] * 3600 + time[1] * 60 + time[2];
}

$("#User").change(function () {

    var urlGetAllUsers = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes?UserID=" + $('option:selected', $("#User")).attr('id');
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetOnBehalfLeaves, false);

});

function SucessGetOnBehalfLeaves(Leaves) {
    LeaveTypes = Leaves;
    $("#LeaveType").empty();
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $("#LeaveType").append('<option Unit="' + LeaveTypes.Data[i].Unit + '" value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
    }
}


function GetStartAndEndWorkingTime() {

    var urlGetStartAndEndWorkingTime = ConfigurationData.GlobalApiURL + "/Request/GetStartAndEndWorkingTime?UserID=" + LoggedUserData.GlobalUserID + "&RequestDate=" + $("#StartDate")[0].value;
    Common.Ajax('GET', urlGetStartAndEndWorkingTime, null, 'json', SucessGetStartAndEndWorkingTime, false);
}

function SucessGetStartAndEndWorkingTime(Result)
{

    ListWorkingDayDetails = Result.Data;


}
