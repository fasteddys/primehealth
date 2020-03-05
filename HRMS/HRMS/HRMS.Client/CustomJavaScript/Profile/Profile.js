
var LeaveTypes = [];
var RequestStatus = [];
var LoggedID, UserID, RealLoggedUserID;
$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    LoggedID = urlParams.get('LID');
    UserID = urlParams.get('UID');
    RealLoggedUserID = document.getElementById("GlobalUserID").value;

    GetUserProfileData();
    $("#LoadingDiv").delay(50).hide(0);
    GetAllLeaveType();
    GetAllRequestStatus();
});

function checkImageAvailability(src) {
    $("<img>").attr('src', src)
        .on("error", function (e) {
            $('.profileImge').attr('src', '/img/user_profile.jpg');
        })
        .on("load", function (e) {
          
            $('.profileImge').attr('src', src);
        });
}

function GetUserProfileData() {
    var urlGetUserProfileData = ConfigurationData.GlobalApiURL + "/Users/GetAllUserProfileData?LoggedUserID=" + LoggedID + "&UserID=" + UserID;
    Common.Ajax('get', urlGetUserProfileData, null, 'json', SucessGetUserProfileData, false);
}
function SucessGetUserProfileData(UserData) {
    RenderProfileData(UserData);
}
function RenderProfileData(UserData) {
    var time = '';

    //Empty
    if (UserData.Data.UserID === 0) {
        window.location.href = "/User/UnAuthorizedPage";
    }

    //if (RealLoggedUserID !== LoggedID) {
    //    window.location.href = "/User/UnAuthorizedPage";
    //}
    
    if (UserData.Data.BirthDate !== null) {
        time = UserData.Data.BirthDate.split(' ');
        $("#DBirthDate").html(time[0]);
    }
    if (UserData.Data.HireDate !== null) {
        time = UserData.Data.HireDate.split(' ');
        $("#DHireDate").html(time[0]);
    }
    if (UserData.Data.ProbationDate !== null) {
        time = UserData.Data.ProbationDate.split(' ');
        $("#DProbationDate").html(time[0]);
    } else {
        $("#DProbationDate").hide();
        $("#DProbationDateCellLabel").hide();
    }
    if (UserData.Data.TerminationDate !== null) {
        time = UserData.Data.TerminationDate.split(' ');
        $("#DTerminationDate").html(time[0]);
    } else {
        $("#DTerminationDate").hide();
        $("#DTerminationDateCellLabel").hide();
    }
    
    //UserData.Data.ProfilePictureURL !== null ? $("#PProfilePic").attr('src', ConfigurationData.PictureLocation + UserData.Data.ProfilePictureURL).change() : $("#PProfilePic").attr('src', '/img/profile.png');
    if (UserData.Data.ProfilePictureURL !== null)
    {
        checkImageAvailability(ConfigurationData.PictureLocation + UserData.Data.ProfilePictureURL);
        //checkImageAvailability("D:\\TFS\\HRMS\\HRMS.Client\\imageProfilePic\\" + UserData.Data.ProfilePictureURL);
        //checkImageAvailability("../imageProfilePic/"+ UserData.Data.ProfilePictureURL);


    }
    else 
        $("#PProfilePic").attr('src', '/img/user_profile.jpg');

    UserData.Data.UserName !== null ? $("#PUserName").html(UserData.Data.UserName) : null;
    UserData.Data.positionName !== null ? $("#PPosition").html(UserData.Data.positionName) : null;
    UserData.Data.UserID !== null ? $("#CypressID").html(UserData.Data.UserID) : null;
    UserData.Data.DepartmentName !== null ? $("#PDepartment").html(UserData.Data.DepartmentName) : null;
    UserData.Data.SubDepartmentName !== null ? $("#PSubDepartment").html(UserData.Data.SubDepartmentName) : null;
    UserData.Data.UserEmail !== null ? $("#PEmail").html(UserData.Data.UserEmail) : null;
    UserData.Data.Gender !== null ? $("#DGender").html(UserData.Data.Gender) : null;
    UserData.Data.positionName !== null ? $("#DPosition").html(UserData.Data.positionName) : null;
    UserData.Data.DirectManagerName !== null ? $("#DDirectManager").html(UserData.Data.DirectManagerName) : null;
    UserData.Data.TeamManagerName !== null && UserData.Data.TeamManagerName !== UserData.Data.LDAPUserName ? $("#DTeamManager").html(UserData.Data.TeamManagerName) : $("#DTeamManager").html('None');
    //UserData.Data.BirthDate !== null ? $("#DBirthDate").html(UserData.Data.BirthDate) : null;
    //UserData.Data.HireDate !== null ? $("#DHireDate").html(UserData.Data.HireDate) : null;
    UserData.Data.SeniorityBeforeHireMonth !== null ? $("#DSeniority").html(UserData.Data.SeniorityBeforeHireYear + " Years " + UserData.Data.SeniorityBeforeHireMonth + " months" ) : null;
    //UserData.Data.ProbationDate !== null ? $("#DProbationDate").html(UserData.Data.ProbationDate) : null;
    //UserData.Data.TerminationDate !== null ? $("#DTerminationDate").html(UserData.Data.TerminationDate) : null;
    UserData.Data.EmployeeNumber !== null ? $("#DEmployeeNumber").html(UserData.Data.EmployeeNumber) : null;

    UserData.Data.WorkingModeName !== null ? $("#DWorkingMode").html(UserData.Data.WorkingModeName) : $("#DWorkingMode").html('None');

    UserData.Data.CompanyName !== null ? $("#DCompanyName").html(UserData.Data.CompanyName) : $("#DCompanyName").html('None');
    UserData.Data.ContractTypeName !== null ? $("#DContractType").html(UserData.Data.ContractTypeName) : $("#DContractType").html('None');
    UserData.Data.BusinessPhone !== null ? $("#DBusinessPhone").html(UserData.Data.BusinessPhone) : $("#DBusinessPhone").html('None');
    UserData.Data.NationalID !== '' ? $("#DNationalID").html(UserData.Data.NationalID) : $("#DNationalID").html('None');
    UserData.Data.MedicalID !== '' ? $("#DMedicalID").html(UserData.Data.MedicalID) : $("#DMedicalID").html('None');
    UserData.Data.CurrentAddress !== '' ? $("#DCurrentAddress").html(UserData.Data.CurrentAddress) : $("#DCurrentAddress").html('None');
    UserData.Data.HomeAddress !== '' ? $("#DHomeAddress").html(UserData.Data.HomeAddress) : $("#DHomeAddress").html('None');

    if (UserData.Data.LeaveTypesEntitlements.length > 0) {
        for (var i = 0; i < UserData.Data.LeaveTypesEntitlements.length; i++) {
            var tableRow = '<tr>' +
                '<td>' + UserData.Data.LeaveTypesEntitlements[i].LeaveTypeName + '</td>' +
                '<td style="width:80% !important">' + UserData.Data.LeaveTypesEntitlements[i].UserEntitlementAmount + '  ' + UserData.Data.LeaveTypesEntitlements[i].DurationUnit + ' &nbsp&nbsp<span style="color:red">' + UserData.Data.LeaveTypesEntitlements[i].Message +'</span></td>' +
                //'<td style="width:50% !important">' + UserData.Data.LeaveTypesEntitlements[i].DurationUnit + '</td>' +
                '</tr >';
            $("#LeavesOverView").append(tableRow);
        }
    }


}

$('.thumbnail').hover(
    function () {
        $(this).find('.caption').fadeIn(250); //.fadeIn(250)
    },
    function () {
        $(this).find('.caption').fadeOut(250); //.fadeOut(205)
    }
); 

$('#UploadPic').click(function () {
    var x = $('#UploadPic').attr('href');
    $('#imgupload').trigger('click');
});

//$('#imgupload').fileupload({
//    url: '//jquery-file-upload.appspot.com/',
//    dataType: 'json',
//    // Enable image resizing, except for Android and Opera,
//    // which actually support image resizing, but fail to
//    // send Blob objects via XHR requests:
//    disableImageResize: /Android(?!.*Chrome)|Opera/
//        .test(window.navigator && navigator.userAgent),
//    imageMaxWidth: 768,
//    imageMaxHeight: 960,
//    imageCrop: true // Force cropped images
//});

$("#imgupload").change(function () {
    var UrlAddProfileImg = '/user/AddProfilePicture?id=' + UserID;
    var DisplayImg = GetAttachments("imgupload", "Cimgupload");
    $.ajax({

        type: "POST", url: UrlAddProfileImg,
        data: DisplayImg, dataType: 'json', contentType: false, processData: false,
        success: function (result) {
           if (result.Success === true) {
            var profilePictureDTO =
                {
                UserID: UserID,
                Path: result.Data,
                ModifiedByUserId: UserID

                };
                var setPictureUrl = ConfigurationData.GlobalApiURL+"/Users/AddProfilePictureURL";
                Common.Ajax('POST', setPictureUrl, JSON.stringify(profilePictureDTO), 'json', SucesssetPictureUrl, true);
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
        $("#ProfilePicture").addClass("active");

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
$("#GetTimeAttendance").click(function () {
    var TAFrom = $("#TimeAttendanceFrom").val();
    var TATo = $("#TimeAttendanceTo").val();

    if (TAFrom === '')
        ShowALert(1, 'Date From is required');
    else
        GetTimeAttendanceForUser(TAFrom, TATo);
});
function GetTimeAttendanceForUser(From, To) {
    var TimeAttendanceParametersDTO = {
        UserID: UserID,
        StartDate: From,
        EndDate: To
    };
    var urlGetUserTimeAttendance = ConfigurationData.GlobalApiURL+"/Users/GetAttendanceByUserID";
    Common.Ajax('POST', urlGetUserTimeAttendance, JSON.stringify(TimeAttendanceParametersDTO), 'json', SucessGetTimeAttendance, false);
}
function SucessGetTimeAttendance(TimeAttendane) {
    $('#dataTableTimeAttendance').DataTable().destroy();
    $('#dataTableTimeAttendance').DataTable({
        retrieve: true,
        "ordering": false,
        "data": TimeAttendane.Data,
        "columns": [
            {
                data: "Date",//3
                "render": function (data, type, full, meta) {
                    return moment(convertStringToDate(data)).format('DD/MM/YYYY'); ;

                }
            },//1
            {
                data: "FingerPrintIn",//2
                "render": function (data) {
                    if (data != null) {
                        return data;

                    }
                    return '--';
                }
            },
            {
                data: "FingerPrintOut",//3
                "render": function (data) {
                    if (data != null) {
                        return data;

                    }
                    return '--';
                }
            },
           

        ],
    })

    //$("#TimeAttendanceTable").empty();
    //var FingerPrintIn = '--';
    //var FingerPrintOut = '--';
    //for (var i = 0; i < TimeAttendane.Data.length; i++) {
    //    if (TimeAttendane.Data[i].FingerPrintIn != null)
    //        FingerPrintIn = TimeAttendane.Data[i].FingerPrintIn;
    //    else
    //        FingerPrintIn = '----';

    //    if (TimeAttendane.Data[i].FingerPrintOut != null)
    //        FingerPrintOut = TimeAttendane.Data[i].FingerPrintOut;
    //    else
    //        FingerPrintOut = '----';

    //    var TARowDate = '<tr>' +
    //        '<td>' + TimeAttendane.Data[i].Date+'</td>' +
    //        '<td>' + FingerPrintIn + '</td>' +
    //        '<td>' + FingerPrintOut  + '</td>' +
    //        '</tr >';
    //    $("#TimeAttendanceTable").append(TARowDate);
    //}
}
$("#FindLeaves").click(function () {
    var LeavesFrom = $("#LeavesFrom").val();
    var LeavesTo = $("#LeavesTO").val();
    var LeaveType = $("#LeaveType").val();
    var LeaveStatus = $("#LeaveStatus").val();

    if ((LeavesFrom !== '' && LeavesTo === '') || (LeavesFrom === '' && LeavesTo !== '')) {
        ShowALert(4, 'Date From & To is required');
    }
    else
    {
       // GetLeavesHistoryForUser(LeavesFrom, LeavesTo, LeaveType, LeaveStatus);


        var LeaveFilterObj = {
            UserID: UserID,
            LeaveTypeID: LeaveType,
            StatusID: LeaveStatus,
            From: LeavesFrom,
            To: LeavesTo
        };
        var urlGetUserTimeAttendance = ConfigurationData.GlobalApiURL + "/Request/FiltterRequests";
        Common.Ajax('POST', urlGetUserTimeAttendance, JSON.stringify(LeaveFilterObj), 'json', SucessGetLeavesHistoryForUser, false);

    }
});
function SucessGetLeavesHistoryForUser(Leaves) {
    $('#dataTableLeavesRequestList').DataTable().destroy();
    $('#dataTableLeavesRequestList').DataTable({
        retrieve: true,
        "data": Leaves.Data,
        "columns": [
            { "data": "RequestID" },//1
            { "data": "LeaveType" },//1
            { "data": "RequestStatus" },//1
            {
                data: "DurationFrom",//3
                "render": function (data,type,full,meta) {
                    if (LeaveDurationUnitEnum.Days === full.Unit)
                    {
                        return moment(data).format('DD/MM/YYYY');

                    }
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            {
                data: "DurationTo",//3
                "render": function (data, type, full, meta) {
                    if (LeaveDurationUnitEnum.Days === full.Unit) {
                        return moment(data).format('DD/MM/YYYY');

                    }
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            {
                data: "BackToWork",//3
                "render": function (data, type, full, meta) {
                    if (LeaveDurationUnitEnum.Days === full.Unit) {
                        return moment(data).format('DD/MM/YYYY');

                    }
                     return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                 }
            },
            {
                data: "RequestDuration",
                "render": function (data, type, full, meta)
                {
                    return data + ' ' + full.Unit;
                }
            },//1

            {
                data: "CreationDate",//3
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            }

        ],
    })

    //$("#LeavesRequestListTable").empty();
    //for (var i = 0; i < Leaves.Data.length; i++) {
    //    if (LeaveDurationUnitEnum.Days === Leaves.Data[i].Unit) {
    //        //CreationDate = moment(Result.Data[i].CreationDate).format('DD/MM/YYYY');
    //        DurationFrom = moment(Leaves.Data[i].DurationFrom).format('DD/MM/YYYY');
    //        DurationTo = moment(Leaves.Data[i].DurationTo).format('DD/MM/YYYY');
    //        BackToWork = moment(Leaves.Data[i].BackToWork).format('DD/MM/YYYY');
    //    }
    //    else if (LeaveDurationUnitEnum.Hours === Leaves.Data[i].Unit) {
    //        //CreationDate = moment(Result.Data[i].CreationDate).format('DD/MM/YYYY hh:mm:ss A');
    //        DurationFrom = moment(Leaves.Data[i].DurationFrom).format('DD/MM/YYYY hh:mm:ss A');
    //        DurationTo = moment(Leaves.Data[i].DurationTo).format('DD/MM/YYYY hh:mm:ss A');
    //        BackToWork = moment(Leaves.Data[i].BackToWork).format('DD/MM/YYYY hh:mm:ss A');
    //    }

    //    var TARowDate = '<tr>' +
    //        '<td>' + Leaves.Data[i].RequestID + '</td>' +
    //        '<td>' + Leaves.Data[i].LeaveType + '</td>' +
    //        '<td>' + Leaves.Data[i].RequestStatus + '</td>' +
    //        '<td>' + DurationFrom + '</td>' +
    //        '<td>' + DurationTo + '</td>' +
    //        '<td>' + BackToWork + '</td>' +
    //        '<td>' + Leaves.Data[i].RequestDuration + ' ' + Leaves.Data[i].Unit + '</td>' +
    //        '<td>' + moment(Leaves.Data[i].CreationDate).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        '</tr >';
    //    $("#LeavesRequestListTable").append(TARowDate);
    //}
}
function GetAllLeaveType() {
    var urlAllLeaveTypes = ConfigurationData.GlobalApiURL+"/LeaveType/GetAllLeaveTypes";
    Common.Ajax('get', urlAllLeaveTypes, null, 'json', SucessGetLeaves, false);
}
function SucessGetLeaves(Leaves) {
    LeaveTypes = Leaves;
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $("#LeaveType, #EntitlementsLeaveType").append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');
        $("#ApprovalsLeaveType").append('<option value="' + LeaveTypes.Data[i].LeaveTypeID + '">' + LeaveTypes.Data[i].LeaveTypeName + '</option>');

    }


}
function GetAllRequestStatus() {
    var urlAllRequestStatus = ConfigurationData.GlobalApiURL + "/Request/GetAllRequestStatusTypes";//?LoggedUserID=" + LoggedUserData.IsHR;
    Common.Ajax('get', urlAllRequestStatus, null, 'json', SucessGetRequestStatus, false);
}
function SucessGetRequestStatus(RequestsStatus) {
    RequestStatus = RequestsStatus;

    for (var i = 0; i < RequestStatus.Data.length; i++) {
        if (LoggedUserData.IsHR === 'False' || LoggedUserData.GlobalUserID === UserID) {
            $("#LeaveStatus").append('<option value="' + RequestStatus.Data[i].RequestStatusID + '">' + RequestStatus.Data[i].RequestStatusName + '</option>');
            $("#RequestStatus").append('<option value="' + RequestStatus.Data[i].RequestStatusID + '">' + RequestStatus.Data[i].RequestStatusName + '</option>');
        }
        else if (LoggedUserData.IsHR === 'True' && RequestStatus.Data[i].RequestStatusName === 'Approved') {
            $("#LeaveStatus").empty();
            $("#LeaveStatus").append('<option value="' + RequestStatus.Data[i].RequestStatusID + '">' + RequestStatus.Data[i].RequestStatusName + '</option>');
            $("#RequestStatus").append('<option value="' + RequestStatus.Data[i].RequestStatusID + '">' + RequestStatus.Data[i].RequestStatusName + '</option>');
            break;
        }

    }

}
$("#FindEntitlements").click(function () {
    var EntitlementsFrom = $("#EntitlementsFrom").val();
    var EntitlementsTo = $("#EntitlementsTo").val();
    var EntitlementsLeaveType = $("#EntitlementsLeaveType").val();

    GetEntitlementsHistoryForUser(EntitlementsFrom, EntitlementsTo, EntitlementsLeaveType);
});
function GetEntitlementsHistoryForUser(From, To, Type) {
    var EntitlementChangeFilterObj = {
        UserID: UserID,
        LeaveTypeID: Type,
        From: From,
        To: To
    };
    urlGetUserTimeAttendance = ConfigurationData.GlobalApiURL + "/UserEntitlement/FilterUserEntitlementChanges";
    Common.Ajax('POST', urlGetUserTimeAttendance, JSON.stringify(EntitlementChangeFilterObj), 'json', SucessGetUserEntitlementChangesForUser, false);
}
function SucessGetUserEntitlementChangesForUser(Result) {
    
    $('#dataTableEntitlementsList').DataTable().destroy();
    $('#dataTableEntitlementsList').DataTable({
        retrieve: true,
        "ordering": false,
        "data": Result.Data,
        "columns": [
            { "data": "LeaveTypeName" },//1
            { "data": "EntitlementBefore" },//2
            { "data": "EntitlementTo" },//3
            { "data": "EntitlementChangedBy" },//4
            { "data": "Request" },//5
            { "data": "RequestDuration" },//6
            { "data": "UserChangeEntitlement" },//7
            {
                data: "ActionDate",//8
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            { "data": "Comment" }//9


        ]
    });
    //$("#EntitlementsListTable").empty();
    //for (var i = 0; i < Result.Data.length; i++) {
    //    var ActionDate = Result.Data[i].ActionDate !== null ? Result.Data[i].ActionDate : '---';
    //    var TARowDate = '<tr>' + 
    //        '<td>' + Result.Data[i].LeaveTypeName + '</td>' +
    //        //'<td>' + moment(Result.Data[i].DurationFrom).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        //'<td>' + moment(Result.Data[i].DurationTo).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        //'<td>' + moment(Result.Data[i].BackToWork).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        '<td>' + Result.Data[i].EntitlementBefore + '</td>' +
    //        '<td>' + Result.Data[i].EntitlementTo + '</td>' +
    //        '<td>' + Result.Data[i].EntitlementChangedBy + '</td>' +
    //        '<td>' + Result.Data[i].RequestDuration + '</td>' +
    //        '<td>' + Result.Data[i].UserChangeEntitlement + '</td>' +
    //        '<td>' + moment(ActionDate).format('DD/MM/YYYY hh:mm:ss A') + '</td>' +
    //        '<td>' + Result.Data[i].Comment + '</td>' +
    //        '</tr >';
    //    $("#EntitlementsListTable").append(TARowDate);
    //}
}
//-------------------------------------------------------------
function convertStringToDate(stringDate) {
    listStringDate = stringDate.split('/');
    return new Date(listStringDate[2], listStringDate[1] - 1, listStringDate[0])
}