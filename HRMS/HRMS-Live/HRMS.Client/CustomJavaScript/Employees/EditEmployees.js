var UserID =0;
var TerminationDate;
var IsNew = false;

$(document).ready(function () {
    $("#LoadingDiv").delay(50).hide(0);
    UserID = getParameterByName("id")
    console.log(UserID)
     //UserID = $("#UserID").val();
    GetDropdownsData();
    GetUserData();
    GetGenderDropdownsData();
    $("#LoadingDiv").delay(50).hide(0);
    $("#PProfilePic").on("click", function () {
        $("#imgupload").trigger("click");
    })
    //$("#PProfilePic").on("click", function () {
    //    $("#imgupload").trigger("click");
    //})
});

//globel function
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
//-----------------------------------------------------
function GetGenderDropdownsData() {
    urlGetDropdownsData = ConfigurationData.GlobalApiURL + "/LeaveType/GetLeaveTypeDropDowns";
    Common.Ajax('get', urlGetDropdownsData, null, 'json', SucessfulGetGenderDropdownsData, false);
}

function SucessfulGetGenderDropdownsData(DropdownsData) {
    
}

function GetDropdownsData() {
    urlGetDropdownsData = ConfigurationData.GlobalApiURL+"/Users/GetUserDropDownData";
    Common.Ajax('get', urlGetDropdownsData, null, 'json', SucessurlGetDropdownsData, false);
}

function SucessurlGetDropdownsData(DropdownsData) {
    RenderDropdownData(DropdownsData);
}

function RenderDropdownData(DropdownsData) {
    for (var i = 0; i < DropdownsData.Data.ApprovalFlow.length; i++) {
        var ApprovalFlowOption = '<option value="' + DropdownsData.Data.ApprovalFlow[i].ApprovalFlowID + '">' + DropdownsData.Data.ApprovalFlow[i].ApprovalFlowName+'</option>';
        $("#ApprovalFlow").append(ApprovalFlowOption);
    }

    var Manageroption = '<option value="null">None</option>';
    $("#DirectManager").append(Manageroption);
    for (var M = 0; M < DropdownsData.Data.UserDTO.length; M++) {
        Manageroption = '<option value="' + DropdownsData.Data.UserDTO[M].UserID + '">' + DropdownsData.Data.UserDTO[M].UserName + '</option>';
        $("#DirectManager").append(Manageroption);
    }

    var TeamManageroption = '<option value="null">None</option>';
    $("#TeamManager").append(TeamManageroption);
    for (var n = 0; n < DropdownsData.Data.ManagerDTO.length; n++) {
        TeamManageroption = '<option value="' + DropdownsData.Data.ManagerDTO[n].ManagerID + '">' + DropdownsData.Data.ManagerDTO[n].ManagerName + '</option>';
        $("#TeamManager ").append(TeamManageroption);
    }

    for (var x = 0; x < DropdownsData.Data.Departments.length; x++) {
        var DepartmentOption = '<option value="' + DropdownsData.Data.Departments[x].DepartmentID + '">' + DropdownsData.Data.Departments[x].DepartmentName + '</option>';
        $("#Department").append(DepartmentOption);
    }

    var SubDepartmentOption = '<option value="null">None</option>';
    $("#SubDepartment").append(SubDepartmentOption);
    for (var z = 0; z < DropdownsData.Data.SubDepartments.length; z++) {
        SubDepartmentOption = '<option value="' + DropdownsData.Data.SubDepartments[z].SubDepartmentID + '">' + DropdownsData.Data.SubDepartments[z].SubDepartmentName + '</option>';
        $("#SubDepartment").append(SubDepartmentOption);
    }
    if (DropdownsData.Data.Positios.length !== null) {
        for (var y = 0; y < DropdownsData.Data.Positios.length; y++) {
            var PositionOption = ' <option value="' + DropdownsData.Data.Positios[y].PositionID + '">' + DropdownsData.Data.Positios[y].PositionName + '</option>';
        $("#Position").append(PositionOption);
    }
    }

    for (var i = 0; i < DropdownsData.Data.GenderTypes.length; i++) {
        var GenderOption = '<option value="' + DropdownsData.Data.GenderTypes[i].GenderID + '">' + DropdownsData.Data.GenderTypes[i].GenderName + '</option>';
        $("#Gender").append(GenderOption);
    }

    for (var i = 0; i < DropdownsData.Data.ContractTypes.length; i++) {
        var ContractTypeOption = '<option value="' + DropdownsData.Data.ContractTypes[i].ContractTypeID + '">' + DropdownsData.Data.ContractTypes[i].ContractTypeName + '</option>';
        $("#ContractType").append(ContractTypeOption);
    }

    for (var i = 0; i < DropdownsData.Data.Companies.length; i++) {
        var CompanyOption = '<option value="' + DropdownsData.Data.Companies[i].CompanyID + '">' + DropdownsData.Data.Companies[i].CompanyName + '</option>';
        $("#Company").append(CompanyOption);
    }
    //var GenderMale = ' <option value="' + 1 + '">' + "male" + '</option>';
    //var GenderFemale = ' <option value="' + 2 + '">' + "female" + '</option>';

    //$("#Gender").append(GenderMale);
    //$("#Gender").append(GenderFemale);

   // var FullTime = ' <option value="' + 1 + '">' + "Full Time" + '</option>';
   // var PartTime = ' <option value="' + 2 + '">' + "Part Time" + '</option>';

   // $("#ContractType").append(FullTime);
   // $("#ContractType").append(PartTime);







    //for (var x = 0; x < DropdownsData.Data.UserDTO.length; x++) {
    //    var DepartmentOption = '<option value="' + DropdownsData.Data.UserDTO[x].ContractTypeID + '">' + DropdownsData.Data.UserDTO[x].ContractTypeName + '</option>';
    //    $("#Contracttype").append(DepartmentOption);
    //}

}

function GetUserData() {
    urlGetUserData = ConfigurationData.GlobalApiURL + "/Users/GetAllUserProfileData?LoggedUserID=" + LoggedUserData.GlobalUserID +"&UserID=" + UserID;
    Common.Ajax('get', urlGetUserData, null, 'json', SucessGetUserData, false);
}

function SucessGetUserData(UserData) {
    RenderUserData(UserData);
}

function RenderUserData(EmployeeData) {
    debugger;
    var dateFormate;
    EmployeeData.Data.UserID !== null ? $("#CypressID").val(EmployeeData.Data.UserID) : null;
    EmployeeData.Data.UserName !== null ? $("#EmployeeName").val(EmployeeData.Data.UserName) : null;
    EmployeeData.Data.UserEmail !== null ? $("#Email").val(EmployeeData.Data.UserEmail) : null;
    EmployeeData.Data.UserArabicName !== null ? $("#EmployeeArabicName").val(EmployeeData.Data.UserArabicName) : null;

    var HireDate = EmployeeData.Data.HireDate !== null ? EmployeeData.Data.HireDate.split(' ') : null;
    EmployeeData.Data.HireDate !== null ? $("#HireDate").val(HireDate[0]).change() : null;

    EmployeeData.Data.DepartmentID !== null ? $("#Department").val('' + EmployeeData.Data.DepartmentID + '').change() : null;
    
    $("#SubDepartment").val('' + EmployeeData.Data.SubDepartmentID + '').change();
    //EmployeeData.Data.SubDepartmentID !== null ? $("#SubDepartment").val(EmployeeData.Data.SubDepartmentID) : null;
    EmployeeData.Data.positionID !== null ? $("#Position").val('' + EmployeeData.Data.positionID + '').change() : null;   
    // $("#DirectManager").val(EmployeeData.Data.DirectManagerID);
    $("#DirectManager").val('' + EmployeeData.Data.DirectManagerID + '').change();

    EmployeeData.Data.CompanyID !== null ? $("#Company").val('' + EmployeeData.Data.CompanyID + '').change() : null;
    EmployeeData.Data.ContractTypeID !== null ? $("#ContractType").val('' + EmployeeData.Data.ContractTypeID + '').change() : null;

    $("#TeamManager").val('' + EmployeeData.Data.TeamManagerID + '').change();
    //EmployeeData.Data.TeamManagerID !== null ? $("#TeamManager").val(EmployeeData.Data.TeamManagerID) : null;

    var BirthDate = EmployeeData.Data.BirthDate !== null ? EmployeeData.Data.BirthDate.split(' ') : null;
    EmployeeData.Data.BirthDate !== null ? $("#BirthDate").val(BirthDate[0]).change() : null;

    var ProbationDate = EmployeeData.Data.ProbationDate !== null ? EmployeeData.Data.ProbationDate.split(' ') : null;
    EmployeeData.Data.ProbationDate !== null ? $("#ProbationEnd").val(ProbationDate[0]).change() : null;

    var TerminationDate = EmployeeData.Data.TerminationDate !== null ? EmployeeData.Data.TerminationDate.split(' ') : null;
    EmployeeData.Data.TerminationDate !== null ? $("#TerminationDate").val(TerminationDate[0]).change() : null;

    EmployeeData.Data.SeniorityBeforeHireYear !== null ? $("#SenioritybeforeYears").val(EmployeeData.Data.SeniorityBeforeHireYear) : null;
    EmployeeData.Data.SeniorityBeforeHireMonth !== null ? $("#SenioritybeforeMonths").val(EmployeeData.Data.SeniorityBeforeHireMonth) : null;
    EmployeeData.Data.NationalID !== null ? $("#NationalID").val(EmployeeData.Data.NationalID) : null;
    EmployeeData.Data.MedicalID !== null ? $("#MedicalID").val(EmployeeData.Data.MedicalID) : null;
    EmployeeData.Data.BusinessPhone !== null ? $("#BusinessPhone").val(EmployeeData.Data.BusinessPhone) : null;
    EmployeeData.Data.EmployeeNumber !== null ? $("#EmployeeNumber").val(EmployeeData.Data.EmployeeNumber) : null;
    EmployeeData.Data.CurrentAddress !== null ? $("#CurrentAddress").val(EmployeeData.Data.CurrentAddress) : null;
    EmployeeData.Data.HomeAddress !== null ? $("#HomeAddress").val(EmployeeData.Data.HomeAddress) : null;
    EmployeeData.Data.CustomeNote !== null ? $("#CustomeNote").val(EmployeeData.Data.CustomeNote) : null;
    //var x = $("#Gender").attr("title", "Male");

    EmployeeData.Data.GenderID !== null ? $("#Gender").val('' + EmployeeData.Data.GenderID + '').change() : null; 
    EmployeeData.Data.ApprovalFlowID !== null ? $("#ApprovalFlow").val('' + EmployeeData.Data.ApprovalFlowID + '') : null;

    if (EmployeeData.Data.ProfilePictureURL !== null)//image
    { 
        checkImageAvailability(ConfigurationData.PictureLocation + EmployeeData.Data.ProfilePictureURL);
        //checkImageAvailability("D:\\TFS\\HRMS\\HRMS.Client\\imageProfilePic\\" + UserData.Data.ProfilePictureURL);
        //checkImageAvailability("../imageProfilePic/"+ UserData.Data.ProfilePictureURL);
    }
    else
        $("#PProfilePic").attr('src', '/img/user_profile.jpg');

    
    if (EmployeeData.Data.IsActive !== true && EmployeeData.Data.IsDeleted !== true) {
        if (EmployeeData.Data.IsActive === null) {
            IsNew = true;
            $("#DeActivateMessage").html('This user is recently added');
            $("#BContent").text('Activate');
            EnableInputFields();
        }
        else {
            $("#DeActivateMessage").html('This user is recently added');
            $("#BContent").text('Activate');
            EnableInputFields();
            //$('#Save').attr('onclick', 'return false;');
            //$('#Deactivate').attr('onclick', 'return false;');
        } 
        
    }                                                                                                                

    else if (EmployeeData.Data.IsActive !== true && EmployeeData.Data.IsActive === false) {
        $("#DeActivateMessage").html('This user was Deactivated at ' + TerminationDate);
        DisableInputFields();
    }

    else if (EmployeeData.Data.IsDeleted === true)
        $("#DeActivateMessage").html('This user was Deleted at ' + TerminationDate);

    else if (EmployeeData.Data.IsActive === true){
        $("#Reactivate").hide();
        $("#Deactivate").show();
    }
    if (!EmployeeData.Data.IsActive)
    {
        $(".UploadProfilePicture").attr("hidden", true);//image
    }
    else
    {
        $(".UploadProfilePicture").attr("hidden", false);//image

    }
}

//images
function checkImageAvailability(src) {
    $("<img>").attr('src', src)
        .on("error", function (e) {
            $('.profileImge').attr('src', '/img/user_profile.jpg');
        })
        .on("load", function (e) {

            $('.profileImge').attr('src', src);

        });
}
function PreviewImage(event) {
    var input = event.target;
    var reader = new FileReader();
    reader.onload = function () {
        var dataURL = reader.result;
        document.getElementById("PProfilePic").src = dataURL;
        $("#chagePhotoBtn").attr("disabled", false);

    };   
    reader.readAsDataURL(input.files[0]);
} 
$("#chagePhotoBtn").click(function () {
    var UrlAddProfileImg = '/user/AddProfilePicture?id=' + UserID;
    var DisplayImg = GetAttachments("imgupload", "Cimgupload");//fileName id,class name Cimgupload
    $.ajax({
        type: "POST", url: UrlAddProfileImg,
        data: DisplayImg, dataType: 'json', contentType: false, processData: false,
        success: function (result) {
            if (result.Success === true) {
                 ShowALert(2, result.Message);
                //change path in dataBase
            //    var profilePictureDTO =
            //        {
            //            UserID: UserID,
            //            Path: result.Data,
            //            ModifiedByUserId:UserID

            //        };
            //    var setPictureUrl = ConfigurationData.GlobalApiURL + "/Users/AddProfilePictureURL";
            //    Common.Ajax('POST', setPictureUrl, JSON.stringify(profilePictureDTO), 'json', SucesssetPictureUrl, true);
            }
            else {
                ShowALert(4, result.Message);
            }
        }
    });
});
function SucesssetPictureUrl(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        //$("#ProfilePicture").addClass("active").siblings().removeClass("active");
        //location.reload();
    }
    else {
        ShowALert(4, Result.Message);

    }
}
$(".ShowAlertDismiss").click(function () {
    location.reload();
});
function GetAttachments(InputID, InputClass) {
    var formData = new FormData();
    var totalFiles = document.getElementById(InputID).files.length;
    for (var i = 0; i < totalFiles; i++) {
        var file = document.getElementsByClassName(InputClass)[0].files[i];
        formData.append(InputID, file, file.name);
    }
    return formData;
}
//-----------------------
function DisableInputFields() {
    $("input").prop('disabled', true);
    $("#DeleteTerminationDate").prop('disabled', false);
    $("select").prop('disabled', true);
    $("#Reactivate").show();
    $("#Deactivate").hide();
    $("#Save").hide();
}

function EnableInputFields() {
    //$("input").prop('disabled', false);
    $("#DeleteTerminationDate").prop('disabled', false);
    //$("select").prop('disabled', false);
    $("#Reactivate").show();
    $("#Deactivate").hide();
    $("#Save").hide();
}

function SubmitUpdatedUserData() {
    var urlSubmitUpdatedUserData = ConfigurationData.GlobalApiURL+"/Users/EditUserData";
    var EditUserData = GetUpdatedUserData();

    if (EditUserData.UserArabicName === '' || EditUserData.ApprovalFlowID === '' || EditUserData.CompanyID === ''
        || EditUserData.ContractTypeID === '' || EditUserData.DepartmentID === '' || EditUserData.GenderID === ''
        || EditUserData.positionID === '' || EditUserData.BirthDate === '' || EditUserData.HireDate === ''
        || EditUserData.NationalID === '' || EditUserData.SeniorityBeforeHireYear === '' || EditUserData.SeniorityBeforeHireMonth === '') {
        ShowALert(4, 'Please, insert required fields(*) before save');
    }
    
    else 
        Common.Ajax('Post', urlSubmitUpdatedUserData, JSON.stringify(EditUserData), 'json', SucessSubmitUpdatedUserData, false);
}
function SucessSubmitUpdatedUserData(Result) {

    if (Result.Success === true) {
        ShowALert(2, Result.Message);
    }
    else {
        ShowALert(4, Result.Message);
    }


}

function GetUpdatedUserData() {
    var UserData = {
        //UserID : $("#UserID").val(),
        UserID: getParameterByName("id"),
        UserName: $("#EmployeeName").val(),
        HireDate: $("#HireDate").val(),
        DepartmentID: $("#Department").val(),
        SubDepartmentID: $("#SubDepartment").val(),
        UserEmail: $("#Email").val(),
        GenderID: $("#Gender").val(),
        ProbationDate: $("#ProbationEnd").val(),
        TerminationDate: $("#TerminationDate").val(),
        BirthDate: $("#BirthDate").val(),
        SeniorityBeforeHireYear: $("#SenioritybeforeYears").val(),
        SeniorityBeforeHireMonth: $("#SenioritybeforeMonths").val(),
        DirectManagerID: $("#DirectManager").val(),
        TeamManagerID: $("#TeamManager").val(),
        positionID: $("#Position").val(),
        BusinessPhone: $("#BusinessPhone").val(),
        EmployeeNumber: $("#EmployeeNumber").val(),
        CustomeNote: $("#CustomeNote").val(),
        ApprovalFlowID: $("#ApprovalFlow").val(),
        ContractTypeID: $("#ContractType").val(),
        NationalID: $("#NationalID").val(),
        MedicalID: $("#MedicalID").val(),
        CurrentAddress: $("#CurrentAddress").val(),
        HomeAddress: $("#HomeAddress").val(),
        UserArabicName: $("#EmployeeArabicName").val(),
        CompanyID: $("#Company").val(),
        ModifiedByUserId: LoggedUserData.GlobalUserID
    };
    return UserData;
}

$('#ApprovalFlow').change(function () {
    var ApprovalFlowID = $('#ApprovalFlow :selected').val();
    CheckApprovalFlow(ApprovalFlowID);
});

function CheckApprovalFlow(ApprovalFlowID)
{
    var CheckObj = {
        UserID: UserID,
        ApprovalFlowID: ApprovalFlowID
    };

    var urlGetUserData = ConfigurationData.GlobalApiURL + "/ApprovalFlow/CheckIfApprovalFlowVaildForUser";
    
    Common.Ajax('post', urlGetUserData, JSON.stringify(CheckObj), 'json', SucessValidateApprovalFlow, false);
}

function SucessValidateApprovalFlow(Result) {
    if (Result.Success === false) {
        ShowALert(4, Result.Message);
    }
}

function ShowDeactivateTerminationDateModal() {
    $('#UserDeactivateTerminationDate').modal({ backdrop: 'static', keyboard: false });
}

function ShowDeactivateConfirmationModel() {
    TerminationDate = $('#DeactivateTerminationDate').val();
    $('#Confirmation').modal({ backdrop: 'static', keyboard: false });
    $('#ConfirmationText').html("Are you sure you want to Deactivate " + $("#EmployeeName").val() + " From " + TerminationDate + ", You can Reactivate this user later");
    $("#Cofirmed").on("click", function () {
        DeActivateUser();
    });
}

function DeActivateUser() {
    var UserData = {
        UserID: UserID,
        TerminationDate: TerminationDate,
        ModifiedByUserId: LoggedUserData.GlobalUserID
    };

    var urlGetUserData = ConfigurationData.GlobalApiURL + "/Users/DeactivateUser";
    Common.Ajax('POST', urlGetUserData, JSON.stringify(UserData), 'json', SucessDeActivateUser, false);
}

function SucessDeActivateUser(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        $("#DeActivateMessage").html('This user was Deactivated at ' + TerminationDate).change();
        location.reload();
    }   
    else if (Result.Success === false) {
        ShowALert(4, Result.Message);
    }
}

function ShowDeleteTerminationDateModal() {
    $('#UserDeleteTerminationDate').modal({ backdrop: 'static', keyboard: false });
}

function ShowDeleteConfirmationModel() {
    TerminationDate = $('#DeleteTerminationDate').val();
    $('#Confirmation').modal({ backdrop: 'static', keyboard: false });
    $('#ConfirmationText').html("Are you sure you want to Delete " + $("#EmployeeName").val() + " From " + TerminationDate + ", You can't restore this user later");
    $("#Cofirmed").on("click", function () {
        DeleteUser();
    });
}

function DeleteUser() {
    var UserData = {
        UserID: UserID,
        TerminationDate: TerminationDate,
        ModifiedByUserId: LoggedUserData.GlobalUserID
    };

    var urlGetUserData = ConfigurationData.GlobalApiURL + "/Users/DeleteUser";
    Common.Ajax('POST', urlGetUserData, JSON.stringify(UserData), 'json', SucessDeleteUser, false);
}

function SucessDeleteUser(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        $("#DeActivateMessage").html('This user was Deleted at ' + TerminationDate).change();
        window.location.href = '/Configuration/Employees';
    }
    else if (Result.Success === false) {
        ShowALert(4, Result.Message);
    }
}

function ShowReactivateConfirmationModel() {
    $('#Confirmation').modal({ backdrop: 'static', keyboard: false });
    $('#ConfirmationText').html("Are you sure you want to Reactivate " + $("#EmployeeName").val() + ", You can Deactivate this user at any time");
    $("#Cofirmed").on("click", function () {
        ReactivateUser();
    });
}

function ReactivateUser() {
    var EditUserData;
    //Update new user Date 
    
        EditUserData = GetUpdatedUserData();

    if (EditUserData.UserArabicName === '' || EditUserData.ApprovalFlowID === '' || EditUserData.CompanyID === ''
        || EditUserData.ContractTypeID === '' || EditUserData.DepartmentID === '' || EditUserData.GenderID === ''
        || EditUserData.positionID === '' || EditUserData.BirthDate === '' || EditUserData.HireDate === ''
        || EditUserData.NationalID === '') {
        ShowALert(4, 'Please, insert required fields(*) before save');
    }
    else {
        var urlGetUserData = ConfigurationData.GlobalApiURL + "/Users/ReactivateUser";
        Common.Ajax('POST', urlGetUserData, JSON.stringify(EditUserData), 'json', SucessReactiveUser, false);
    }
    

    //EditUserData = {
    //    UserID: UserID,
    //    ModifiedByUserId: LoggedUserData.GlobalUserID,
    //    IsNew: IsNew
    //};

    
}

function SucessReactiveUser(Result) {
    if (Result.Success === true) {
        ShowALert(2, Result.Message);
        $("#DeActivateMessage").html('').change();
        location.reload();
    }
    else if (Result.Success === false) {
        ShowALert(4, Result.Message);
    }
}
