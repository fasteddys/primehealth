$(document).ajaxStart(function () {
    $("#LoadingDiv").show();
});
$(document).ajaxStop(function () {
    $("#LoadingDiv").delay(50).hide(0);
});

$('#SumbitChangePassword').on('click', function () {
    $("#Alert-Div-Pass").hide();
    debugger;
    var OldPass = $("#OldPassword").val();
    var NewPass = $("#NewPassword").val();
    var ConfirmPass = $("#ConfirmPassword").val();
    if (OldPass == "" || NewPass == "" || ConfirmPass == "") {
        document.getElementById("Alert-Div-Pass-Text").innerHTML = "You Must Fill All Data"
        $("#Alert-Div-Pass").show();
    }
    else if (NewPass != ConfirmPass) {
        document.getElementById("Alert-Div-Pass-Text").innerHTML = "Confirm Password must be identical to new Password please Retype it again"
        $("#Alert-Div-Pass").show();

    }
    else if (OldPass == NewPass) {
        document.getElementById("Alert-Div-Pass-Text").innerHTML = "New Password Can't be as the old one .. Please Pick a new one"
        $("#Alert-Div-Pass").show();
    }
    else {
        var ChangeObj = {
            OldPassword: OldPass,
            NewPassword: NewPass,
            ConfirmPassword: ConfirmPass
        }
        var url = '/Login/ChangePassword';


        Common.Ajax('post', url, ChangeObj, 'json', SuccessChange);
    }


})
function SuccessChange(data) {
    if (data==1) {
        $('#modal_ChangePassword').modal('toggle');
        swal({
            title: "Success",
            text: "You Password Changed Successfully",
            confirmButtonColor: "#66BB6A",
            type: "success"
        });
    }
    else if (data==0) {
        swal({
            title: "Oops...",
            text: "You didn't Type the Correct Old Password ... Please Try again",
            confirmButtonColor: "#EF5350",
            type: "error"
        });
    }
    else if (data == 2) {
        swal({
            title: "Oops...",
            text: "your Password was not changed ... Please Try again",
            confirmButtonColor: "#EF5350",
            type: "error"
        });
    }
}


$(document).ready(function () {
        var url = '';
        var dataToPost;//=null;


        url = '/Home/GetCountofRequest'; // MVC Action Name


        // For Post Method
         Common.Ajax('post', url, dataToPost, 'json', successUserCreateHandler);

    


});

function successUserCreateHandler(data)
{
   // $("#NewRequests").html() = data;

    document.getElementById("AllTickets").innerHTML = data.TotalTickets;
    document.getElementById("ApprovedRequests").innerHTML = data.ApprovedTickets;

    document.getElementById("RejectedRequest").innerHTML = data.RejectedTickets;

    document.getElementById("TerminiatedRequests").innerHTML = data.TerminateTickets;
    document.getElementById("AssignedTickets").innerHTML = data.AssignedTickets;
    document.getElementById("PendingTickets").innerHTML = data.PendingApprovalTickets;
    document.getElementById("PendingProviderTickets").innerHTML = data.PendingProvidersTickets;
    document.getElementById("ReopendTickets").innerHTML = data.ReopendTickets;
    document.getElementById("NewTickets").innerHTML = data.NewTickets;
    document.getElementById("UserName").innerHTML = data.UserName;
    document.getElementById("GratingUserName").innerHTML = data.UserName;
    
    document.getElementById("SublocationName").innerHTML = data.Location;

    


    

}
$(document).ready(function () {

    var path = window.location.pathname;
    var value = window.location.search;

    if (path == "/home/index")
    {
        document.getElementById("Dashboard").setAttribute("class", "active");
        
    }
    if (path == "/Request/OpenNewRequest") {
        document.getElementById("OpenNewRequestTAB").setAttribute("class", "active");

    }
    if (path == "/Request/ShowRequestList") {
        document.getElementById("AllRequestTAB").setAttribute("class", "active");

    }
    if (path == "/Request/ShowRequestLists" && value == '?sid=6') {
        document.getElementById("AprrovedRequestTAB").setAttribute("class", "active");

    }
    if (path == "/Request/ShowRequestLists" && value == '?sid=7') {
        document.getElementById("RejectedRequestTAB").setAttribute("class", "active");

    }
    if (path == "/Request/ShowRequestLists" && value == '?sid=9') {
        document.getElementById("TerminatedRequestTAB").setAttribute("class", "active");

    }


    if (path == "/Request/ShowRequestLists" && value == '?sid=1') {
        document.getElementById("NewRequestTAB").setAttribute("class", "active");
        document.getElementsByClassName("hidden-ul")[0].setAttribute("style", "display: block;");

        

    }
    if (path == "/Request/ShowRequestLists" && value == '?sid=2') {
        document.getElementById("AssignedRequestTAB").setAttribute("class", "active");
        document.getElementsByClassName("hidden-ul")[0].setAttribute("style", "display: block;");

    }
    if (path == "/Request/ShowRequestLists" && value == '?sid=10') {
        document.getElementById("PendingRequestTAB").setAttribute("class", "active");
        document.getElementsByClassName("hidden-ul")[0].setAttribute("style", "display: block;");

    }
    if (path == "/Request/ShowRequestLists" && value == '?sid=8') {
        document.getElementById("PendingProviderRequestTAB").setAttribute("class", "active");
        document.getElementsByClassName("hidden-ul")[0].setAttribute("style", "display: block;");

    }

    if (path == "/Request/ShowRequestLists" && value == '?sid=4') {
        document.getElementById("ReopendRequestTAB").setAttribute("class", "active");
        document.getElementsByClassName("hidden-ul")[0].setAttribute("style", "display: block;");

    }

});




