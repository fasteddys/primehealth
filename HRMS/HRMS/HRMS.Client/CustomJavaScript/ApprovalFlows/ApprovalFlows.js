//Global Variables
var AllUsersList = [];
var LeaveTypes = [];
var ApprovalRoleCounter = 1;
var ApprovalStepCounter = 1;


$(document).ready(function () {
    GetUsersList();
    GetLeaveTypes();
    AddAprovalRole();

});
//add alternative approval role
$('#AddAltFlowButton').click(function () {
    AddAprovalRole();
});
//get users list
function GetUsersList() {
    urlGetAllUsers = ConfigurationData.GlobalApiURL+"/Users/GetAllUsers";
    Common.Ajax('get', urlGetAllUsers, null, 'json', SucessGetUsers, false);
}
//get leaves list
function GetLeaveTypes() {
    urlGetAllLeaveTypes = ConfigurationData.GlobalApiURL+"/LeaveType/GetAllLeaveTypes";
    Common.Ajax('get', urlGetAllLeaveTypes, null, 'json', SucessGetLeaves, true);
}

function SubmitApprovalFlow() {
    var ApprovalFlow = GetSubmitObjectData();
    var leaveName = $("#Approvalflowname").val();
    var ApprovalFlowContanerDTO = {
        ApprovalFlowName: leaveName,
        ApprovalFlow: ApprovalFlow
    };
    urlGetAllLeaveTypes = ConfigurationData.GlobalApiURL+"/ApprovalFlow/AddNewApprovalFlow";
    Common.Ajax('POST', urlGetAllLeaveTypes, JSON.stringify(ApprovalFlowContanerDTO), 'json', SucessAddLeave, true);
}
//success get users list
function SucessGetUsers(users) {
    AllUsersList = users;
}
//success get leaves list
function SucessGetLeaves(Result) {
    LeaveTypes = Result;

}
function SucessAddLeave(Result) {
      if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);

    }

}


// append approval role or allternative approval role
function AddAprovalRole() {

    var RoleTitle = "Default approval steps";
    if (ApprovalRoleCounter > 1) {
        RoleTitle = '<a class="nav - link pull-right" href="#" id="D' + ApprovalRoleCounter + '" onclick="DeleteApprovalRole(this)"><i class="fas fa-trash" aria-hidden="true"></i></a> Alltarnative Flow for Type';
    }

    var RoleHtml = '<div class="onerow" id="Role' + ApprovalRoleCounter + '">' +
        '<div class="col6 last">' +
        '<fieldset id="alternativeFlowsContainer" class="formGroup">' +
        '<fieldset class="ruleFieldset">' +
        '<legend class="alternativeLegend CheckAlter">' +
        '<span class="title">' + RoleTitle + '</span>' +
        '<span class="caseConfigContainer"></span>' +
        '</legend>' +
        '<div class="onerow flowDetails_autoAcceptation" style="display: block;">' +
        '<div class="col12">' +
        '<span class=" fa fa-check-circle fa-2x"></span> Automatic immediate approval.' +
        '<br><br>' +
        'Add approvers to steps to make flow non-automatic.' +
        '</div>' +
        '</div>' +
        '<fieldset class="formGroup">' +
        '<div class="onerow" id="' + ApprovalRoleCounter + '">' +
        '<div class="col12">' +
        '<div class="flowPresentationContainer">' +
        '<div class="FlowPresentation hiddenStatusColumn">' +
        '<div class="FlowPresentation_Step">' +
        '<div class="FlowPresentation_State"></div>' +
        '<div class="FlowPresentation_Indicator"></div>' +
        '<div class="FlowPresentation_Desc">' +
        '<div class="row">' +
        '<div class="col-md-10">' +
        '<select multiple data-plugin-selectTwo class="form-control populate" id="' + ApprovalRoleCounter + '_' + ApprovalStepCounter + '" onchange="AddApprovalStep(this);">' +
        '<option value="-1">Direct Manager</option>' +
        '<option value="-2">Team Leader</option>' +
        '</select>' +
        '</div>' +
        '<div class="col-md-2 pull-right">' +
        '<a class="nav-link" >' +
        '<i class="fas fa-trash" aria-hidden="true"></i>' +
        '</a>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '<div class="FlowPresentation_NextArrow"></div>' +
        '<div class="FlowPresentation_Number">1</div>' +
        '<div class="FlowPresentation_Clear">&nbsp;</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</fieldset>' +
        '</fieldset>' +
        '</fieldset>' +
        '</div>' +
        '</div>';
    $("#ApprovalRoles").append(RoleHtml);
    if (ApprovalRoleCounter > 1) {
        AppendLeaveTypeSelect(ApprovalRoleCounter);
        AppendLeaveTypesOptions(ApprovalRoleCounter);
    }
    var SID = '#' + ApprovalRoleCounter + '_' + ApprovalStepCounter;
    $(SID).select2({
        multiple: true
    });
    AppendUsersOptions(ApprovalRoleCounter + '_' + ApprovalStepCounter);
    ApprovalStepCounter++;
    ApprovalRoleCounter++;
}
// append approval step to approval role
function AddApprovalStep(obj) {
    var SlectedStepID = obj.id;
    var RoleID = SlectedStepID.substring(0, 1);
    var StID = '#' + RoleID + ' select:last';
    var testobj = $(StID).find();
    var LastRoleStepID = $(StID).attr("id");
    //if (typeof LastRoleStepID === 'undefined') {
    //    LastRoleStepID = SlectedStepID;
    //}

    if (LastRoleStepID === SlectedStepID) {
        var StepHtml =
            '<div class="col12" id="Step' + ApprovalStepCounter + '">' +
            '<div class="flowPresentationContainer">' +
            '<div class="FlowPresentation hiddenStatusColumn">' +
            '<div class="FlowPresentation_Step">' +
            '<div class="FlowPresentation_State"></div>' +
            '<div class="FlowPresentation_Indicator"></div>' +
            '<div class="FlowPresentation_Desc">' +
            '<div class="row">' +
            '<div class="col-md-10">' +
            '<select multiple data-plugin-selectTwo class="form-control populate" id="' + RoleID + '_' + ApprovalStepCounter + '" onchange="AddApprovalStep(this)">' +
            '<option value="-1">Direct Manager</option>' +
            '<option value="-2">Team Leader</option>' +
            '</select>' +
            '</div>' +
            '<div class="col-md-2 pull-right">' +
            '<a class="nav-link"  id="' + ApprovalStepCounter + '" onclick="DeleteApprovalStep(this)">' +
            '<i class="fas fa-trash" aria-hidden="true"></i>' +
            '</a>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="FlowPresentation_NextArrow"></div>' +
            '<div class="FlowPresentation_Number">1</div>' +
            '<div class="FlowPresentation_Clear">&nbsp;</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';
        var RID = '#' + RoleID;
        var SID = '#' + RoleID + '_' + ApprovalStepCounter;
        $(RID).append(StepHtml);
        $(SID).select2({
            multiple: true
        });
        AppendUsersOptions(RoleID + '_' + ApprovalStepCounter);
        ApprovalStepCounter++;
    }
}
// append users to dropdown
function AppendUsersOptions(SelectID) {
    var SID = '#' + SelectID;
    for (var i = 0; i < AllUsersList.Data.length; i++) {
        $(SID).append('<option value="' + AllUsersList.Data[i].UserID + '">' + AllUsersList.Data[i].UserName + '</option>');
    }
}
// append leave type to dropdown
function AppendLeaveTypeSelect(RoleID) {
    var StID = '#Role' + RoleID + ' .CheckAlter';
    var hTml = '<select  data-plugin-selectTwo class="form-control populate" id="SelectLeaveType' + RoleID + '""><option selected disabled></option ></select >';
    $(StID).append(hTml);
}
// append dropdown for allternative approval role
function AppendLeaveTypesOptions(RoleID) {
    var StID = '#Role' + RoleID + ' select:first';
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $(StID).append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
    }
}
// delete approval step
function DeleteApprovalStep(DeleteObj) {
    var StepID = "#Step" + DeleteObj.id;
    $(StepID).remove();
}
//delete approval role (alternative only)
function DeleteApprovalRole(RoleDeleteObj) {
    RoleDeleteObjID = RoleDeleteObj.id.substring(1, RoleDeleteObj.length);
    var RoleID = "#Role" + RoleDeleteObjID;
    $(RoleID).remove();
}

function GetSubmitObjectData() {
    var ApprovalFlowsList = [];
    var OrderIncrementer = 1;
    for (var i = 1; i < ApprovalRoleCounter; i++) {
        var ApprovalFlowDTO = {};
        var RoleApprovalsSteps = [];
        var RoleDiv = '#Role' + i;
        var ForLeaveID = -1;
        OrderIncrementer = 1;
        if (i > 1) {
            var LeaveTypeSelect = '#SelectLeaveType' + i;
            ForLeaveID = $(LeaveTypeSelect).val();
        }
        for (var z = 1; z < ApprovalStepCounter; z++) {
            var StepID = '#' + i + '_' + z;
            if ($(StepID).length) {
                var UserIDs = $(StepID).val();
                if (UserIDs !== null) {
                    for (var x = 0; x < UserIDs.length; x++) {
                            var TempApprovalStep = {
                                UserID: UserIDs[x],
                                Order: OrderIncrementer
                            };
                            RoleApprovalsSteps.push(TempApprovalStep);
                    }
                    OrderIncrementer++;
                }
            }

        }

        ApprovalFlowDTO = {
            ApprovalRoleID: i,
            ApprovalRoleLeaveID: ForLeaveID,
            Steps: RoleApprovalsSteps
        };
        ApprovalFlowsList.push(ApprovalFlowDTO);
    }
    return ApprovalFlowsList;
}

