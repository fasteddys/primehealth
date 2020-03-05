var ListLeaveTypeFields = [];
var IsSuspendForAll = false;
$(function () {
    var urlGetLeaveTypeDB = ConfigurationData.GlobalApiURL + "/LeaveType/GetLeaveTypeDropDowns";

    Common.Ajax('get', urlGetLeaveTypeDB, null, 'json', SucessGetLeaveTypes, true);

    GetEmployeesList();
    GetAllSubDepartments();
    GetAllDepartments();
   

  

});
function GetMinOneDayDurations() {
    var urlGetLeaveTypeMinOneDayDuration = ConfigurationData.GlobalApiURL + "/LeaveType/GetLeaveTypeMinOneDayDuration?DurationUnitID=" + $("#LeaveDurationUnitDD").val();

    Common.Ajax('get', urlGetLeaveTypeMinOneDayDuration, null, 'json', SucessGetMinOneDayDurations, true);
}
function SucessGetMinOneDayDurations(Result) {
    $("#MinLeaveDurationWithinOneDayDD").empty();

    for (var i = 0; i < Result.Data.length; i++) {
        $("#MinLeaveDurationWithinOneDayDD").append("<option value='" + Result.Data[i].MinOneDayDurationID + "'>" + Result.Data[i].MinOneDayDurationName + "</option>");
    }
}
function SucessGetLeaveTypes(Result){
    
    for (var i = 0; i < Result.Data.LeaveDurationUnit.length; i++)
    {
        if (i == 0) {
            $("#LeaveDurationUnitDD").append("<option  value='" + Result.Data.LeaveDurationUnit[i].LeaveTypeDurationUnitID + "'>" + Result.Data.LeaveDurationUnit[i].LeaveTypeDurationUnitName + "</option>");
            $("#LeaveDurationUnitDD").val(Result.Data.LeaveDurationUnit[i].LeaveTypeDurationUnitID);
        }
        else {
            $("#LeaveDurationUnitDD").append("<option  value='" + Result.Data.LeaveDurationUnit[i].LeaveTypeDurationUnitID + "'>" + Result.Data.LeaveDurationUnit[i].LeaveTypeDurationUnitName + "</option>");

        }
        GetMinOneDayDurations();


    }
    for (var i = 0; i < Result.Data.LeaveTypeFields.length; i++) {
        var LeaveTypeField = {
            LeaveTypeFieldID: Result.Data.LeaveTypeFields[i].LeaveTypeFieldID,
            LeaveTypeFieldName: Result.Data.LeaveTypeFields[i].LeaveTypeFieldName

        };
        ListLeaveTypeFields.push(LeaveTypeField);
        $("#FieldsAndvisibility").append("<tr class='clickable-row'><td>" + Result.Data.LeaveTypeFields[i].LeaveTypeFieldName
            + "<td><select data-plugin-selectTwo class='form-control populate' value='" + Result.Data.LeaveTypeFields[i].LeaveTypeFieldID + "' id='LeaveTypeVisibility" + Result.Data.LeaveTypeFields[i].LeaveTypeFieldName + "DD'></select></td></td></tr>");
        for (var j = 0; j < Result.Data.LeaveTypeFieldsVisibility.length; j++) {

            $("#LeaveTypeVisibility" + Result.Data.LeaveTypeFields[i].LeaveTypeFieldName + "DD").append("<option value='"
                + Result.Data.LeaveTypeFieldsVisibility[j].LeaveTypeFieldsVisibilityID + "'>"+Result.Data.LeaveTypeFieldsVisibility[j].LeaveTypeFieldsVisibilityName + "</option>");
        }

    }
    

    for (var i = 0; i < Result.Data.LeaveTypeAccuralPeriod.length; i++)
    {
        $("#AccuralPeriodsDD").append("<option value='" + Result.Data.LeaveTypeAccuralPeriod[i].AccuralPeriodID + "'>" + Result.Data.LeaveTypeAccuralPeriod[i].AccuralPeriodName + "</option>");

    }
    for (var i = 0; i < Result.Data.LeaveTypeConsiderationType.length; i++)
    {
        $("#LeaveTypeConsiderationsDD").append("<option value='" + Result.Data.LeaveTypeConsiderationType[i].LeaveTypeConsiderationAsID + "'>" + Result.Data.LeaveTypeConsiderationType[i].LeaveTypeConsiderationAsName + "</option>");

    }
    for (var i = 0; i < Result.Data.LeaveTypeEntitlementSource.length; i++)
    { 
        
        $("#EntitlementTypeDD").append("<option value='" + Result.Data.LeaveTypeEntitlementSource[i].EntitlementSourceID + "'>" + Result.Data.LeaveTypeEntitlementSource[i].EntitlementSourceName + "</option>");
    
    }
    for (var i = 0; i < Result.Data.LeaveTypeEntitlementType.length; i++)
    {
        $("#EntitlementAccuralDD").append("<option value='" + Result.Data.LeaveTypeEntitlementType[i].LeaveTypeEntitlementTypeID + "'>" + Result.Data.LeaveTypeEntitlementType[i].LeaveTypeEntitlementTypeName + "</option>");
    }
   
    for (var i = 0; i < Result.Data.LeaveTypeFirstAccuralMethod.length; i++)
    {
        $("#FirstAccuralForNewEmployeesDD").append("<option value='" + Result.Data.LeaveTypeFirstAccuralMethod[i].FirstAccuralMethodID + "'>" + Result.Data.LeaveTypeFirstAccuralMethod[i].FirstAccuralMethodName + "</option>");
    }

    for (var i = 0; i < Result.Data.LeaveTypeGainingEligibilityMethod.length; i++) {
        $("#EmployeeGainsEligibility").append("<option value='" + Result.Data.LeaveTypeGainingEligibilityMethod[i].GainingEligibilityMethodID + "'>" + Result.Data.LeaveTypeGainingEligibilityMethod[i].GainingEligibilityMethodName + "</option>");
    }
 
      
    for (var i = 0; i < Result.Data.GenderType.length; i++) {
        $("#GenderTypeDD").append("<option value='" + Result.Data.GenderType[i].GenderID + "'>" + Result.Data.GenderType[i].GenderName + "</option>");
    }

    for (var i = 0; i < Result.Data.ContractType.length; i++) {
        $("#ContractTypeDD").append("<option value='" + Result.Data.ContractType[i].ContractTypeID + "'>" + Result.Data.ContractType[i].ContractTypeName + "</option>");
    }

    for (var i = 0; i < Result.Data.LeaveTypeProrateCalculationMode.length; i++) {
        
        $("#LeaveTypeMinOneDayDurationDD").append("<option value='" + Result.Data.LeaveTypeProrateCalculationMode[i].ProrateCalculationModeID + "'>" + Result.Data.LeaveTypeProrateCalculationMode[i].ProrateCalculationModeName + "</option>");
    }


    for (var i = 0; i < Result.Data.LeaveTypeProrateMethod.length; i++) {
        $("#ProrateMethodDD").append("<option value='" + Result.Data.LeaveTypeProrateMethod[i].ProrateMethodID + "'>" + Result.Data.LeaveTypeProrateMethod[i].ProrateMethodName + "</option>");
        
    }
    for (var i = 0; i < Result.Data.LeaveTypes.length; i++)
    {
        $("#TakeEntitlementFromDD").append('<option EmployeeEarnsNumberOfUnits="' + Result.Data.LeaveTypes[i].EmployeeEarnsNumberOfUnits +'" value=' + Result.Data.LeaveTypes[i].LeaveTypeID + '>' + Result.Data.LeaveTypes[i].LeaveTypeName + '</option>');

    }
    for (var i = 0; i < Result.Data.LeaveTypeMonthlyAaccuralDays.length; i++) {
        $("#MonthlyAaccuralDaysDD").append('<option  value=' + Result.Data.LeaveTypeMonthlyAaccuralDays[i].LeaveTypeMonthlyAaccuralDaysValue
            + '>' + Result.Data.LeaveTypeMonthlyAaccuralDays[i].LeaveTypeMonthlyAaccuralDaysDescription + '</option>');

    }
    


    for (var i = 0; i < Result.Data.LeaveTypeRestriction.length; i++) {

        if (Result.Data.LeaveTypeRestriction[i].RestrictionDescription.includes('##') || Result.Data.LeaveTypeRestriction[i].RestrictionDescription.includes('&')) {
            var inputText = '<div class="col-lg-1"><input type="number" class="form-control Number' + Result.Data.LeaveTypeRestriction[i].RestrictionName + '"/></div>';
            var FinalString = Result.Data.LeaveTypeRestriction[i].RestrictionDescription.replace(/##/g, inputText);
            FinalString = FinalString.replace(/&/g, '<span class="UnitText"> ' + $("#LeaveDurationUnitDD option:selected").text() +'</span> ');
            
            $("#Restrictions").append(
                '<div class= "form-group row"><div class="col-lg-1"><div class="checkbox-custom checkbox-default"><input type="checkbox" id="'
                + Result.Data.LeaveTypeRestriction[i].RestrictionName + 'checkbox' + '"/>' +
                '<label for="checkboxExample1">' +'</label></div ></div >' + FinalString
                

                );
        }
        
        
        else {

            $("#Restrictions").append('<div class= "form-group row"><div class="col-lg-5"><div class="checkbox-custom checkbox-default"><input type="checkbox" id="' + Result.Data.LeaveTypeRestriction[i].RestrictionName + "checkbox" + '"><label for="checkboxExample1">'
                + Result.Data.LeaveTypeRestriction[i].RestrictionDescription
                + '</label ></div ></div > <div class="col-lg-1"><input type="number" class="form-control" id="'
                + "Number" + Result.Data.LeaveTypeRestriction[i].RestrictionName + '"></div><span class="UnitText">' + $("#LeaveDurationUnitDD option:selected").text() + '</span></div>');

        }



    }
    

    for (var i = 1; i < 13; i++) {
        $("#Mounth").append('<option value=' + i + '>' + i + '</option>');
        $("#NumberOfMonthsToExpireCarriedOverLeaveDD").append('<option value=' + i + '>' + i + '</option>');
        $("#MonthsToGainsEligibilityDD").append('<option value=' + i + '>' + i + '</option>');

        
    }
    $("#LeavePartailUnit").hide();
    $("#MonthNumber").hide();
    $("#TakeEntitlementFromTxt,#TakeEntitlementMax").hide();
    $("#EveryMonthAccuralDayDD").hide();
    $("#BasedonDD").hide();
    var x = $("#NumberAbsenceNotShorterThan")[0];
    $("#NumberOfMonthsToExpireCarriedOverLeaveDD").prop('disabled', true);
    $("#MaxNumberOfDaysToCarriedOver").prop('disabled', true);
    $("#MaxmimAllowNegativeEntitlement").prop('disabled', true);
    $(".NumberIfAbsenceLongerThan").prop('disabled', true);
    $(".NumberAbsenceRequestedUpTo").prop('disabled', true);
    $("#NumberOfDayIfAbsenceLongerThan").prop('disabled', true);
    $("#NumberAbsenceNotLongerThan").prop('disabled', true);
    $("#NumberAbsenceNotShorterThan").prop('disabled', true);
    $(".NumberIfAbsenceLongerThan").prop('disabled', true);
    $("#NumberOfMonthsToExpireCarriedOverLeaveDD").val(null).change();
    $("#TakeEntitlementFromDD").val(null).change();

    
    DropDownListChange();
    
}
function DropDownListChange() {
    $("#EntitlementTypeDD").change(function () {
        if ($("#EntitlementTypeDD").val() === "1") {
            $("#TakeEntitlementFromTxt,#TakeEntitlementMax").hide();
            $("#TakeEntitlementFromDD,#LeaveTypeMaxAmount").value = null;

        }
        else {
            $("#TakeEntitlementFromTxt,#TakeEntitlementMax").show();

        }
    });
    $("#EntitlementAccuralDD").change(function () {
        if ($("#EntitlementAccuralDD").val() === "1") {
            $("#IsAccured").show();
            var IsAccuredDiv = $("#IsAccured select");

            for (var i = 0; i < IsAccuredDiv.length; i++) {
                if (i == 1  ||i == 5 || i == 6) {
                    IsAccuredDiv[i].value = null;
                    IsAccuredDiv[i].parentElement.parentElement.setAttribute("style", "display: none;");

                } else {
                    IsAccuredDiv.eq(i).val(1).change();
                }

            }

            var IsAccuredInput = $("#IsAccured input");

            for (var i = 0; i < IsAccuredInput.length; i++) {

                IsAccuredInput[i].value = null;

            }





        }
        else {
            $("#IsAccured").hide();
            var IsAccuredDiv = $("#IsAccured select");
            for (var i = 0; i < IsAccuredDiv.length; i++) {
                IsAccuredDiv[i].value = null;

            }


        }
    });
    $("#EmployeeGainsEligibility").change(function () {

        if ("1" === $("#EmployeeGainsEligibility").val()) {
            $("#MonthNumber").hide();        
            $("#MonthsToGainsEligibilityDD").val(null).change();
        }
        else {
            $("#MonthNumber").show();
            $("#MonthsToGainsEligibilityDD").val(1).change();
            var x = $("#MonthsToGainsEligibilityDD").val();
        }

    });
    $("#FirstAccuralForNewEmployeesDD").change(function () {

        if ("1" === $("#FirstAccuralForNewEmployeesDD").val()) {
            $("#BasedonDD").hide();
            $("#LeaveTypeMinOneDayDurationDD").val(null);
            $("#ProrateMethodDD").val(null);

            

        }
        else {
            $("#BasedonDD").show();
            $("#LeaveTypeMinOneDayDurationDD").eq(0).val(1).change();
            $("#ProrateMethodDD").eq(0).val(1).change();
        }

    });
    $("#AccuralPeriodsDD").change(function () {

        if ("1" === $("#AccuralPeriodsDD").val()) {
            $("#CarryOverTab").show();
            $("#EveryMonthAccuralDayDD").hide();
        }
        else {
            $("#CarryOverTab").hide();
            $("#EveryMonthAccuralDayDD").show();
            var CarryoverSelect = $("#Carry-over select");
            for (var i = 0; i < CarryoverSelect.length; i++) {
                CarryoverSelect[i].value = null;

            }
            var Carryoverinput = $("#Carry-over input");
            for (var i = 0; i < Carryoverinput.length; i++) {
                Carryoverinput[i].checked = null;
                Carryoverinput[i].value = null;

            }







        }

    });
    $("#EntitlementTypeDD").change(function () {

        if ($("#EntitlementTypeDD").val() === "1")
        {
            $("#CarryOverTab").show();
            $("#AccuralTab").show();
            $("#IsAccured").show();
            $("#EntitlementAccuralDD").val(1).change();
            
            var IsAccuredDiv = $("#IsAccured select");

            for (var i = 0; i < IsAccuredDiv.length; i++) {
                if (i == 1 || i == 3 || i == 5 || i == 6) {
                    IsAccuredDiv[i].value = null;
                    IsAccuredDiv[i].parentElement.parentElement.setAttribute("style", "display: none;");

                } else {
                    IsAccuredDiv[i].value = 1;
                }

            }
            $("#TakeEntitlementFromDD").val(null);


            var IsAccuredInput = $("#IsAccured input");

            for (var i = 0; i < IsAccuredInput.length; i++) {

                IsAccuredInput[i].value = null;

            }







        }
        else
        {
            debugger;
            $("#CarryOverTab").hide();
            var CarryoverSelect = $("#Carry-over select");
            for (var i = 0; i < CarryoverSelect.length; i++) {
                CarryoverSelect[i].value = null;

            }
            var Carryoverinput = $("#Carry-over input");
            for (var i = 0; i < Carryoverinput.length; i++) {
                Carryoverinput[i].checked = null;
                Carryoverinput[i].value = null;

            }

            $("#AccuralTab").hide();
   
            var AccuralTabinput = $("#Accural input");
            for (var i = 0; i < AccuralTabinput.length; i++) {
                AccuralTabinput[i].checked = null;
                AccuralTabinput[i].value = null;

            }
            $("#EffectiveDate")[0].value = null;
            
            var AccuralTabselect = $("#Accural select");

            for (var i = 0; i < AccuralTabselect.length; i++) {
                if (i == 1 || i == 3 || i == 5 || i == 6) {
                    AccuralTabselect[i].value = null;
                    AccuralTabselect[i].parentElement.parentElement.setAttribute("style", "display: none;");

                } else {
                    AccuralTabselect[i].value = 1;
                }

            }

            $("#TakeEntitlementFromDD").val(1).change();








        }

    });
    $("#EnableCarryOverUnusedEntitlementcheckbox").change(function () {
        if ($("#EnableCarryOverUnusedEntitlementcheckbox")[0].checked) {
            $("#MaxNumberOfDaysToCarriedOver").prop('disabled', false);
        }
        else {


            $("#MaxNumberOfDaysToCarriedOver").prop('disabled', true);

            $("#MaxNumberOfDaysToCarriedOver")[0].value = 0;
        }
    });
    $("#ExpireCarriedOverAfterTimecheckbox").change(function () {
        if ($("#ExpireCarriedOverAfterTimecheckbox")[0].checked) {
            $("#NumberOfMonthsToExpireCarriedOverLeaveDD").prop('disabled', false);
            $("#NumberOfMonthsToExpireCarriedOverLeaveDD").val(1).change();
        } else {


            $("#NumberOfMonthsToExpireCarriedOverLeaveDD").prop('disabled', true);

            $("#NumberOfMonthsToExpireCarriedOverLeaveDD").val(null).change();


        }
    });
    $("#AbsenceNotLongerThancheckbox").change(function () {
        if ($("#AbsenceNotLongerThancheckbox")[0].checked) {
            $("#NumberAbsenceNotLongerThan").prop('disabled', false);
        } else {


            $("#NumberAbsenceNotLongerThan").prop('disabled', true);

            $("#NumberAbsenceNotLongerThan")[0].value = null;
        }
    });
    $("#AbsenceNotShorterThancheckbox").change(function () {
        if ($("#AbsenceNotShorterThancheckbox")[0].checked) {
            $("#NumberAbsenceNotShorterThan").prop('disabled', false);

        } else {

            $("#NumberAbsenceNotShorterThan").prop('disabled', true);

            $("#NumberAbsenceNotShorterThan")[0].value = null;


        }
    });
    $("#AbsenceRequestedUpTocheckbox").change(function () {
        if ($("#AbsenceRequestedUpTocheckbox")[0].checked) {
            $(".NumberAbsenceRequestedUpTo").prop('disabled', false);
        } else {

            $(".NumberAbsenceRequestedUpTo").prop('disabled', true);

            $(".NumberAbsenceRequestedUpTo")[0].value = null;

        }
    });
    $("#IfAbsenceLongerThancheckbox").change(function () {
        if ($("#IfAbsenceLongerThancheckbox")[0].checked) {
            $(".NumberIfAbsenceLongerThan").prop('disabled', false);
            $("#NumberofdaysAllowedToMakeRequestInThePast").prop('disabled', false);

        } else {


            $(".NumberIfAbsenceLongerThan").prop('disabled', true);
            $(".NumberIfAbsenceLongerThan")[0].value = null;
            $(".NumberIfAbsenceLongerThan").prop('disabled', true);
            $(".NumberIfAbsenceLongerThan")[1].value = null;
        }
    });
    $("#AllowNegativeEntitlementcheckbox").change(function () {
        if ($("#AllowNegativeEntitlementcheckbox")[0].checked) {

            $("#MaxmimAllowNegativeEntitlement").prop('disabled', false);

        } else {

            $("#MaxmimAllowNegativeEntitlement").prop('disabled', true);

            $("#MaxmimAllowNegativeEntitlement")[0].value = null;

        }
    });
    $("#EnablePartailUnitCheckbox").change(function () {
        if (this.checked) {
            $("#LeavePartailUnit").show();
            LeaveDurationUnitChange();
        }
        else {
            $("#LeavePartailUnitDD").empty();
            $("#LeavePartailUnit").hide();
        }
    });
    $("#LeaveDurationUnitDD").change(function () {
        LeaveDurationUnitChange();
        GetMinOneDayDurations();
        var x = $("#LeaveDurationUnitDD option:selected").text();
        $(".UnitText").text(x );

    });
    $("#TakeEntitlementFromDD").change(function () {
        var x = $("#TakeEntitlementFromDD option:selected").attr("EmployeeEarnsNumberOfUnits");
        $("#ParentLeaveTypeEntitlement").text(x);

    });
    $("#LeaveTypeMaxAmount").change(function () {

        if (parseInt($("#LeaveTypeMaxAmount").val()) > parseInt($("#ParentLeaveTypeEntitlement").text())) 
       {
            ShowALert(4, "You Can Not Take More Than " + $("#ParentLeaveTypeEntitlement").text() +" "+ $("#LeaveDurationUnitDD option:selected").text());
            $("#LeaveTypeMaxAmount").val(null);
        }


    });
    


}
function GetEmployeesList() {
    var urlGetEmployeesList = ConfigurationData.GlobalApiURL + "/Users/GetAllUsers";
    Common.Ajax('GET', urlGetEmployeesList, null, 'json', SucessGetEmployees, false);
}
function SucessGetEmployees(EmployeesList) {
    for (var i = 0; i < EmployeesList.Data.length; i++) {
     
        $("#RestrictedEmployeeDD").append('<option value="' + EmployeesList.Data[i].UserID + '">' + EmployeesList.Data[i].UserName + '</option>');
    }
}
function SubmitLeaveType() {
    var LeaveName = $("#LeaveName").val();
    var urlGetEmployeesList = ConfigurationData.GlobalApiURL + "/LeaveType/AddLeaveType?LeaveTypeName=" + LeaveName;
    Common.Ajax('GET', urlGetEmployeesList, null, 'json', SucessAddLeaveType, false);
}
function GetAllSubDepartments() {
    var urlGetAllSubDepartment = ConfigurationData.GlobalApiURL + "/SubDepartment/GetAllSubDepartments";
    Common.Ajax('get', urlGetAllSubDepartment, null, 'json', SucessurlGetAllTeams, false);
}
function SucessurlGetAllTeams(SubDepartment) {
    for (var i = 0; i < SubDepartment.Data.length; i++) {

        $("#SubDepartmentDD").append('<option  value="' + SubDepartment.Data[i].SubDepartmentID + '">' + SubDepartment.Data[i].SubDepartmentName + '</option>');

    }
}
function GetAllDepartments() {
    var urlGetAllDepartment = ConfigurationData.GlobalApiURL + "/Department/GetAllDepartments";
    Common.Ajax('get', urlGetAllDepartment, null, 'json', SucessurlGetAllDepartment, false);
}
function SucessurlGetAllDepartment(Department) {
    for (var i = 0; i < Department.Data.length; i++) {

        $("#DepartmentDD").append('<option  value="' + Department.Data[i].DepartmentID + '">' + Department.Data[i].DepartmentName + '</option>');

    }
}



$("#MinLeaveDurationWithinOneDayDD").change(function () {
   
    LeaveDurationUnitChange();
});

$("#AttachmentsRequiredcheckbox").change(function () {

    if ($("#AttachmentsRequiredcheckbox")[0].checked) {
        $("#EnableAttachmentscheckbox").prop("checked", true);
    }
    //else {
    //    $("#EnableAttachmentscheckbox").prop("checked", false);

    //}
});
$("#EnableAttachmentscheckbox").change(function () {

    if ($("#AttachmentsRequiredcheckbox")[0].checked) {
        $("#EnableAttachmentscheckbox").prop("checked", true);
    }
    //else {
    //    $("#EnableAttachmentscheckbox").prop("checked", false);

    //}
});




function LeaveDurationUnitChange() {
    if ($("#EnablePartailUnitCheckbox")[0].checked) {
        var urlGetPartailUnit = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypePartialDurationUnits?DurationUnitID="
            + $("#LeaveDurationUnitDD")[0].value + "&MinOneDayDurationID=" + $('option:selected', $("#MinLeaveDurationWithinOneDayDD")).val();
        Common.Ajax('GET', urlGetPartailUnit, null, 'json', SucessLeavePartailUnit, false);
    }
}
function SucessLeavePartailUnit(Result) {
    $("#LeavePartailUnitDD").empty();
    if (Result.Data.length == 0) {
        $("#EnablePartailUnitCheckbox").prop("checked", false);
        $("#EnablePartailUnitCheckbox").change();

    }
    
    for (var i = 0; i < Result.Data.length; i++) {
        $("#LeavePartailUnitDD").append('<option  value="' + Result.Data[i].PartialDurationUnitID + '">' + Result.Data[i].PartialDurationUnitName + '</option>');
    }
}
var Save = function () {
    var IsValid = false;
    var x = $("#LeaveName");
    if ($("#LeaveName").val() != "") { IsValid = true; }


    if (IsValid == true) {
        var LeaveTypeFields = [];
        for (var i = 0; i < ListLeaveTypeFields.length; i++) {


            var LeaveTypeField = {
                LeaveTypeFieldID: ListLeaveTypeFields[i].LeaveTypeFieldID,
                LeaveTypeFieldVisibilityID: $("#LeaveTypeVisibility" + ListLeaveTypeFields[i].LeaveTypeFieldName + "DD").val()
            };
            LeaveTypeFields.push(LeaveTypeField);


        }







        var ListLeavePartailUnit = [];
        var LeavePartailUnit = $('option:selected', $("#LeavePartailUnitDD"));
        for (var i = 0; i < LeavePartailUnit.length; i++) {
            ListLeavePartailUnit.push(LeavePartailUnit[i].value);
        }

        var ListDepartmentIDS = [];
        var DepartmentIDS = $('option:selected', $("#DepartmentDD"));
        for (var i = 0; i < DepartmentIDS.length; i++) {
            ListDepartmentIDS.push(DepartmentIDS[i].value);
        }

        var ListSubDepartmentIDS = [];
        var SubDepartmentIDS = $('option:selected', $("#SubDepartmentDD"));
        for (var i = 0; i < SubDepartmentIDS.length; i++) {
            ListSubDepartmentIDS.push(SubDepartmentIDS[i].value);
        }

        var ListUserIDS = [];
        var UserIDS = $('option:selected', $("#RestrictedEmployeeDD"));
        for (var i = 0; i < UserIDS.length; i++) {
            ListUserIDS.push(UserIDS[i].value);
        }

        var ListContractTypeIDS = [];
        var ContractTypeIDS = $('option:selected', $("#ContractTypeDD"));
        for (var i = 0; i < ContractTypeIDS.length; i++) {
            ListContractTypeIDS.push(ContractTypeIDS[i].value);
        }

        var ListGenderIDS = [];
        var GenderIDS = $('option:selected', $("#GenderTypeDD"));
        for (var i = 0; i < GenderIDS.length; i++) {
            ListGenderIDS.push(GenderIDS[i].value);
        }


        var RestrictedEntity = {

            DepartmentIDS: ListDepartmentIDS,
            SubDepartmentIDS: ListSubDepartmentIDS,
            UserIDS: ListUserIDS,
            ContractTypeIDS: ListContractTypeIDS,
            GenderIDS: ListGenderIDS


        };


        if ($("#EnableCarryOverUnusedEntitlementcheckbox")[0].checked && $("#MaxNumberOfDaysToCarriedOver")[0].value == "")
        {
            $("#MaxNumberOfDaysToCarriedOver")[0].value = null;
            
        }

        var TakeMaxFromParent = $("#LeaveTypeMaxAmount")[0].value;

        if (TakeMaxFromParent === null || TakeMaxFromParent === "")
        {
            TakeMaxFromParent = null;
        }

        var LeaveTypeContainer = {
            LeaveName: $("#LeaveName")[0].value,
            EntitlementTypeID: $("#EntitlementTypeDD").val(),
            LeaveTypeConsiderationsID: $("#LeaveTypeConsiderationsDD").val(),
            DurationUnitID: $("#LeaveDurationUnitDD").val(),
            TakeEntitlementFromID: $("#TakeEntitlementFromDD").val(),
            LeavePartailUnit: ListLeavePartailUnit,
            TakemaxamountFromParentLeaveType: TakeMaxFromParent,
            AccuralID: $("#EntitlementAccuralDD").val(),
            EmployeeGainsEligibilityAfterID: $("#EmployeeGainsEligibility")[0].value,
            EmployeeEarnsNumberOfUnits: $("#EmployeeEarnsNumberOfUnits")[0].value,
            OverSeniorityYears: $("#seniorityYears")[0].value,
            OverSeniorityEmployeeEarns: $("#AmountOverSeniorityEmployeeEarns")[0].value,
            NumberOfMonthsTogainseligibility: $("#MonthsToGainsEligibilityDD").val(),
            ProrateCalculationModeID: $("#LeaveTypeMinOneDayDurationDD")[0].value,
            ProrateMethodID: $("#ProrateMethodDD")[0].value,
            EnableCarryOverUnusedEntitlement: $("#EnableCarryOverUnusedEntitlementcheckbox")[0].checked,
            EnableCarryoverNegativeEntitlement: $("#EnableCarryoverNegativeEntitlementcheckbox")[0].checked,
            ExpireCarriedOverAfterTime: $("#ExpireCarriedOverAfterTimecheckbox")[0].checked,
            NumberOfMonthsToExpireCarriedOverLeave: $("#NumberOfMonthsToExpireCarriedOverLeaveDD")[0].value,
            MaxNumberOfDaysToCarriedOver: $("#MaxNumberOfDaysToCarriedOver")[0].value,
            EnableAttachments: $("#EnableAttachmentscheckbox")[0].checked,
            AbsenceNotLongerThan: $("#AbsenceNotLongerThancheckbox")[0].checked,
            AbsenceNotShorterThan: $("#AbsenceNotShorterThancheckbox")[0].checked,
            AbsenceRequestedUpTo: $("#AbsenceRequestedUpTocheckbox")[0].checked,
            IfAbsenceLongerThan: $("#IfAbsenceLongerThancheckbox")[0].checked,
            NumberAbsenceNotShorterThan: $("#NumberAbsenceNotShorterThan")[0].value,
            NumberAbsenceNotLongerThan: $("#NumberAbsenceNotLongerThan")[0].value,
            NumberDaysRequestedUpTo: $(".NumberAbsenceRequestedUpTo")[0].value,
            NumberOfDayIfAbsenceLongerThan: $(".NumberIfAbsenceLongerThan")[0].value,
            NumberOfDaysAllowedToMakeRequestInThePast: $(".NumberIfAbsenceLongerThan")[1].value,
            LeaveTypeFields: LeaveTypeFields,
            EffectiveDate: $("#EffectiveDate")[0].value,
            RestrictedEntitys: RestrictedEntity,
            Allownegativeentitlement: $("#AllowNegativeEntitlementcheckbox")[0].checked,
            MaxmimAllownegativeentitlement: $("#MaxmimAllowNegativeEntitlement")[0].value,
            MinLeaveDurationWithinOneDay: $("#MinLeaveDurationWithinOneDayDD").val(),
            AccuralPeriodID: $("#AccuralPeriodsDD").val(),
            FirstAccuralMethodForNewEmployeesID: $("#FirstAccuralForNewEmployeesDD").val(),
            AttachmentsRequired: $("#AttachmentsRequiredcheckbox")[0].checked,
            EnablePartailUnit: $("#EnablePartailUnitCheckbox")[0].checked,
            MonthlyAaccuralDays: $("#MonthlyAaccuralDaysDD").val(),
            IsSuspend: IsSuspendForAll
            
        };
        if ($("#EffectiveDate")[0].value==="") {

            LeaveTypeContainer.EffectiveDate = null;
        }

        debugger;
        
        var urlAddLeaveType = ConfigurationData.GlobalApiURL + "/LeaveType/AddLeaveType";
        Common.Ajax('post', urlAddLeaveType, JSON.stringify(LeaveTypeContainer), 'json', SucessSave, false);
    }
};
function SucessSave(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);


    }

}
$("#SuspendForAll").click(
    function () {
        if (IsSuspendForAll == false) {
            $("#ShowSuspendAlert").show();
        }
        else {

            $("#ShowCancelSuspendAlert").show();

        }
    }

);
$("#Suspend").click(
    function () {
        $("#ShowSuspendAlert").hide();
        IsSuspendForAll = true;
        $("#RestrictedEmployeeDD").prop('disabled', true);
        $("#SubDepartmentDD").prop('disabled', true);
        $("#ContractTypeDD").prop('disabled', true);
        $("#GenderTypeDD").prop('disabled', true);
        $("#DepartmentDD").prop('disabled', true);
        $("#SuspendForAll").text("Cancel Suspend");

    }
);
$("#CancelSuspend").click(
    function () {
        $("#ShowCancelSuspendAlert").hide();
        IsSuspendForAll = false;
        $("#RestrictedEmployeeDD").prop('disabled', false);
        $("#SubDepartmentDD").prop('disabled', false);
        $("#ContractTypeDD").prop('disabled', false);
        $("#GenderTypeDD").prop('disabled', false);
        $("#DepartmentDD").prop('disabled', false);
        $("#SuspendForAll").text("Suspend For All");

    }
);
$(".CancelAction").click(
    function () {

        $("#ShowCancelSuspendAlert").hide();

        $("#ShowSuspendAlert").hide();

    }
);




