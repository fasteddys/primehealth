var VacType;
var NumberOfDays;
var vacationDate;
var SelectVacation;
var LeaveTypeID;
var UserID = 0;
var isMonthly;
var Year;
$(document).ready(function () {
    GetUserName();
    GetAllAbsence();
    GetAllEntitlements();
    EmptyData();
});
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function GetUserName() {
    var urlGetUserName = ConfigurationData.GlobalApiURL + "/Users/GetUserByID?UserID=" + getParameterByName('UserID');
    Common.Ajax('get', urlGetUserName, null, 'json', SucessGetUserName, false);
}

function SucessGetUserName(Result) {
    if (Result.Success === true) {
        var userName = Result.Data.UserName;
        $('#Title').append(" For (" + userName + ")");
    }
}

//Selece Absence Type
function GetAllAbsence() {
    var urlGetAllAbsence = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllTypesForEntitlmentChange?UserID=" + getParameterByName('UserID');
    Common.Ajax('get', urlGetAllAbsence, null, 'json', SucessGetAllAbsence, false);
}


function SucessGetAllAbsence(Absence) {
    for (var i = 0; i < Absence.Data.length; i++) {
        $("#SelectAbsencetype").append('<option LeaveTypeID="' + Absence.Data[i].LeaveTypeID + '" value="' + Absence.Data[i].LeaveTypeID + '">' + Absence.Data[i].LeaveTypeName + '</option>');
    }
}

function GetEntiltementYears(LeaveTypeID) {
    var urlGetEntiltementYears = ConfigurationData.GlobalApiURL + "/UserEntitlement/GetEntiltementYears?LeaveTypeID=" + LeaveTypeID + "&UserID=" + getParameterByName('UserID');
    Common.Ajax('GET', urlGetEntiltementYears, null, 'json', SucessurlGetEntiltementYears, false);
}

function SucessurlGetEntiltementYears(Years) {

    for (var i = 0; i < Years.Data.length; i++) {
        $("#Year").append('<option value="' + Years.Data[i] + '">' + Years.Data[i] + '</option>');
    }
}

function GetAllEntiltementMonths(LeaveTypeID, Year) {
    var urlGetAllEntiltementMonths = ConfigurationData.GlobalApiURL + "/UserEntitlement/GetAllEntiltementMonths?LeaveTypeID=" + LeaveTypeID + "&UserID=" + getParameterByName('UserID') + "&Year=" + Year;
    Common.Ajax('GET', urlGetAllEntiltementMonths, null, 'json', SucessurlGetAllEntiltementMonths, false);
}

function SucessurlGetAllEntiltementMonths(Months) {
    for (var i = 0; i < Months.Data.length; i++) {
        $("#Month").append('<option value="' + Months.Data[i] + '">' + Months.Data[i] + '</option>');
    }
}

function GetEntitlementQtyForMonthly(LeaveTypeID, Year, Month) {
    var urlEntitlements = ConfigurationData.GlobalApiURL + "/UserEntitlement/GetMonthlyEntitlementQuantity?LeaveTypeID=" + LeaveTypeID + "&UserID=" + getParameterByName('UserID') + "&Year=" + Year +"&Month=" + Month;
    Common.Ajax('GET', urlEntitlements, null, 'json', SucessGetEntitlementQty, false);
}

function SucessGetEntitlementQty(Result) {
    $("#EntitlementNumber").val(Result.Data);
}

function GetAllEntitlements() {
    var urlGetAllEntitlements = ConfigurationData.GlobalApiURL + "/UserEntitlement/GetAllUserEntitlements?UserID=" + getParameterByName('UserID');
    Common.Ajax('GET', urlGetAllEntitlements, null, 'json', SucessGetAllEntitlements, false);
}

function SucessGetAllEntitlements(Entitlements) {
    //debugger;
    for (var i = 0; i < Entitlements.Data.length; i++) {
        if (Entitlements.Data[i].LeaveTypeFK == $('#SelectAbsencetype').find('option:selected').attr('LeaveTypeID')) {
            $('#adjustEntitlementUnit').html('');
            $('#adjustEntitlementUnitInText').html('');

            $("#EntitlementNumber").val(Entitlements.Data[i].EntitlementModifiedQty); 
            $('#adjustEntitlementUnit').html(Entitlements.Data[i].DurationUnit);
            $('#adjustEntitlementUnitInText').html('As of ' + Entitlements.Data[i].DurationUnit);
        }
    }
}

function IsMonthlyLeaveType(ID) {
    var urlIsMonthlyLeaveType = ConfigurationData.GlobalApiURL + "/LeaveType/IsMonthlyLeaveType?LeaveTypeID=" + ID;
    Common.Ajax('get', urlIsMonthlyLeaveType, null, 'json', SucessGetCheckLeaves, false);
}

function SucessGetCheckLeaves(Result) {
    if (Result.Success === true && Result.Data === true)
        isMonthly = true;
}

$("#SelectAbsencetype").change(function () {
    //var ListSubDepartmentIDs = [];
    

    //var urlGetUsers;

    //for (var i = 0; i < SelectedValus.length; i++) {
    //    ListSubDepartmentIDs.push(SelectedValus[i].value);
    //}
});


$("#SelectAbsencetype").change(function () {
    EmptyData();
    GetAllEntitlements(); 

    LeaveTypeID = $("#SelectAbsencetype").find(":selected").val();
    isMonthly = false;

    IsMonthlyLeaveType(LeaveTypeID);

    if (isMonthly) {
        GetEntiltementYears(LeaveTypeID);
        //GetAllEntiltementMonths(LeaveTypeID);
        $("#YearDiv").show();
        $("#MonthDiv").show();
        $("#YearLabel").show();
        $("#MonthLabel").show();
        $("#HideDiv1").hide();
        $("#HideDiv2").hide();
        //$("#MonthEmptyDiv").hide();
    }
    else {
        $("#YearDiv").hide();
        $("#MonthDiv").hide();
        $("#YearLabel").hide();
        $("#MonthLabel").hide();
        $("#HideDiv1").show();
        $("#HideDiv2").show();
        // $("#MonthEmptyDiv").show();
    }
});

$("#Year").change(function () {
    Year = $("#Year").find(":selected").val();

    $('#Month').empty();
    $('#Month').append("<option disabled selected></option>");
    GetAllEntiltementMonths(LeaveTypeID, Year);
});

$("#Month").change(function () {
    var SelectedValus = $("#Month").find(":selected").val();

    $('#EntitlementNumber').val('');
    GetEntitlementQtyForMonthly(LeaveTypeID, Year, SelectedValus);
});

var userEntitlementObj;

$("#EditEntitlement").click(function () {
    /*if ($('#EntitlementNumber').val() == '') {
        ShowALert(4, 'Absence  Type is required');
        return;
    }*/
    if ($('#SelectAbsencetype').find('option:selected').attr('LeaveTypeID') == undefined) {
        ShowALert(4, 'Absencetype is required');
        return;
    }
    if ($('#NumberOfDays').val() === '') {
        ShowALert(4, 'Number of days is required');
        return;
    }   
    if ($('#ModificationDate').val() === '') {
        ShowALert(4, 'Modification Date is required');
        return;
    } 
    if ($('#Comment').val() === '') {
        ShowALert(4, 'Comment is required');
        return;
    }
    var x = $('#Month').val();
    if (isMonthly && $('#Month').val() === null) {
        ShowALert(4, 'Month is required');
        return;
    }
    if (isMonthly && $('#Year').val() === null) {
        ShowALert(4, 'Year is required');
        return;
    }
  
    userEntitlementObj = {
        LeaveTypeFK: $('#SelectAbsencetype').find('option:selected').attr('LeaveTypeID'),
        UserFK: getParameterByName('UserID'),
        EntitlementModifiedQty: $('#NumberOfDays').val(),
        ModificationDate: $('#ModificationDate')[0].value,     
        Comment: $('#Comment').val(),
        IsAddition: $("input[name='Sign']:checked").val(),
        Years: $("#Year").find(":selected").val(),
        Month: $("#Month").find(":selected").val(),
        ModifiedBy: LoggedUserData.GlobalUserID = document.getElementById("GlobalUserID").value,
        ModifiedByUserId: LoggedUserData.GlobalUserID
    };
    var urlGetUserByID = ConfigurationData.GlobalApiURL + "/UserEntitlement/EditUserTotalEntitlement";
    Common.Ajax('post', urlGetUserByID, JSON.stringify(userEntitlementObj), 'json', SucessUpdateEntitlement, false);

});

function SucessUpdateEntitlement(result) {
    // alert("User Added Successfully");
    //location.reload();
    if (result.Success == true)
        ShowALert(2, result.Message);
    else if (result.Success == false)
        ShowALert(4, result.Message);
    //var AlertMessage = "Entitlement Updated Successfully"
    
    EmptyData();
}

function EmptyData() {
    //$("#SelectAbsencetype").empty();
    $('#NumberOfDays').val('');
    $('#ModificationDate').val('');
    $('#Comment').val('');
    $('#EntitlementNumber').val('');
    $('#Year').empty();
    $('#Year').append("<option disabled selected></option>");
    $('#Month').empty();
    $('#Month').append("<option disabled selected></option>");
    //$('#Year').empty();

    //$('#SelectAbsencetype').val("default");
    //$("input[name='Sign']:checked").empty();

}

