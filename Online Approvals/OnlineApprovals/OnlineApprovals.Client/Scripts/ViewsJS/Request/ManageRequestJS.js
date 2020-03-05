var AddReplyUrl = $("#UrlAddReply").val();
var AddReplyAttachUrl = $("#UrlAddReplyAttach").val();
var ReopenURL = $("#UrlReopen").val();
var TerminateUrl = $("#UrlTerminate").val();


$('#SubmitComment').click(function () {
    debugger;
    var notes = $("#AppendedNotes").val();
    var RquestID = $("#RequestIDHidden").val();
    var DetailsType = $("#DetailsTypeHidden").val();
    var UserTypeID = $("#UserTypeHidden").val(); 
    var USerID = $("#USerID").val(); 
    
    var Details = {
        RequestID:RquestID,
        RequestDetailsType : DetailsType,
        Notes : notes,
        UserTypeID: UserTypeID,
        UserID: USerID
    }

    var RequestDetails = JSON.stringify(Details);
    var urlAddReply = AddReplyUrl

    Common.Ajax('post', urlAddReply, Details, 'json', SubmitCommentSuccess);
});

function SubmitCommentSuccess(data) {
    if (data != 0) {
        debugger;
        var RquestID = $("#RequestIDHidden").val();
        var urlAddReplyAttach = AddReplyAttachUrl+'?tt='+ RquestID +'&tx='+data
        var formData = new FormData();
        var totalFiles = document.getElementById('FileUploader').files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementsByClassName('MyUploader')[0].files[i];
            formData.append('FileUploader', file, file.name);
        }
        //Common.Ajax('POST', url, formData, 'json', Successup);
    }
    $.ajax({
        
        type: "POST", url: urlAddReplyAttach,
        data: formData, dataType: 'json', contentType: false, processData: false,
        success: function (result) {
            if (result.ValidationID == 1) {
                document.getElementById("Success-Div-Text").innerHTML = result.Message
                $('#Success-Div').show();
                $("html, body").animate({ scrollTop: 0 }, "slow");
                UpdatePartial();
                ClearInputFields();
            }
            else if (result.ValidationID == 0) {
                document.getElementById("Alert-Div-Text").innerHTML = result.Message
                $('#Alert-Div').show();
                $("html, body").animate({ scrollTop: 0 }, "slow");
                ClearInputFields();
            }
            else {
                document.getElementById("Alert-Div-Text").innerHTML = "Sorry..somthing went wrong Please try again";
                $('#Alert-Div').show();
                $("html, body").animate({ scrollTop: 0 }, "slow");
                ClearInputFields();
            }

        }
    });
}

function Reopen(reason) {
   
    var RquestID = $("#RequestIDHidden").val();
    urlReopen = ReopenURL+'?id=' + RquestID + "&tz=" + reason;
    Common.Ajax('POST', urlReopen, null, 'json', SuccessReopen);
}

function Terminate(reason) {
   
    var RquestID = $("#RequestIDHidden").val();
    urlTerminate = TerminateUrl+'?id=' + RquestID + "&tq=" + reason;
    Common.Ajax('POST', urlTerminate, null, 'json', TerminateSuccess);
}

function TerminateSuccess(tmessage) {
    if (tmessage.ValidationID==1) {
        document.getElementById("Success-Div-Text").innerHTML = tmessage.Message
        $('#Success-Div').show();
        $("html, body").animate({ scrollTop: 0 }, "slow");
        UpdatePartial();
        UpdateTerminateAccessControl();
    }
    else {
        document.getElementById("Alert-Div-Text").innerHTML = tmessage.Message
        $('#Alert-Div').show();
        $("html, body").animate({ scrollTop: 0 }, "slow");
    }

}

function SuccessReopen(smessage) {
    if (smessage.ValidationID == 1) {
        document.getElementById("Success-Div-Text").innerHTML = smessage.Message
        $('#Success-Div').show();
        $("html, body").animate({ scrollTop: 0 }, "slow");
        UpdatePartial();
        UpdateReopenAccessControl();
    }
    else {
        document.getElementById("Alert-Div-Text").innerHTML = smessage.Message
        $('#Alert-Div').show();
        $("html, body").animate({ scrollTop: 0 }, "slow");
    }
}


function AccessControl() {
    debugger;
    var statusID = $('#StatusIDHidden').val();
    var statusApproved = $('#StatusApprovedHidden').val();
    var statusRejected = $('#StatusRejectedHidden').val();
    var statusNew = $('#StatusNewHidden').val();
    var statusTerminated = $('#StatusTerminatedHidden').val();

    if (statusID != statusApproved && statusID != statusRejected && statusID != statusTerminated) {
        $('#SubmitComment').show();
        if (statusID == statusNew) {
            $('#Terminate').show();
            $('#ButtonsDiv').show();
        }
    }
    else if (statusID == statusApproved || statusID == statusRejected) {
        $('#Reopen').show();
        $('#ButtonsDiv').show();


    }

}

function UpdateReopenAccessControl() {
    $('#Reopen').hide();
    $('#ButtonsDiv').hide();
    $('#SubmitComment').show();
}

function UpdateTerminateAccessControl() {
    $('#Reopen').hide();
    $('#SubmitComment').hide();
    $('#Terminate').hide();
    $('#ButtonsDiv').hide();

}

$('#Reopen').on('click', function () {
    swal({
        title: "Type Reopen Reason",
        type: "input",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Reopen reason"
    },
        function (inputValue) {
            if (inputValue === false) return false;
            if (inputValue === "") {
                swal.showInputError("You need to write something!");
                return false;
            }
            swal({
                title: "Reason Submitted",
                text: "You wrote: " + inputValue,
                type: "success",
                confirmButtonColor: "#2196F3"
            });
            Reopen(inputValue);
        });
    
});


$('#Terminate').on('click', function () {
    swal({
        title: "Type Termination Reason",
        type: "input",
        showCancelButton: true,
        confirmButtonColor: "#2196F3",
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Terminate reason"
    },
        function (inputValue) {
            if (inputValue === false) return false;
            if (inputValue === "") {
                swal.showInputError("You need to write something!");
                return false
            }
            swal({
                title: "Reason Submitted",
                text: "You wrote: " + inputValue,
                type: "success",
                confirmButtonColor: "#2196F3"
            });
            Terminate(inputValue);
        });

});


function appandnotes() {
    debugger;
    var numItems = $('.appandnote').length;
    for (var i = 0; i < numItems; i++) {
        var item1 = document.getElementsByClassName("appandnote")[i];
        if (item1 != null) {
            let item = document.getElementsByClassName("appandnote")[i].textContent;

            var parent = item1.parentElement;
            // parent.removeChild(parent.childNodes[0]);
            // parent.textContent = "";
            $(parent).append(item);
            item1.textContent = '';
            item1.style.display = "none";
        }
    }
}


function ClearInputFields() {
    debugger;
    $(".ClearInputFields").val('');
    $("#AppendedNotes").value="";
    //document.getElementById('AppendedNotes').innerText = "";
}