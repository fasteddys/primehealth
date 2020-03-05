 function AssignTask(TaskID) {
    swal({
        title: "Enter an input !!",
        text: "Write something interesting !!",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "Write something",
        //attributes: {
        //    multiple: true,
        //},
    },
    function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("You need to write something!");
            return false;
        }

        var urlAssignTasks = "/UserDailsTasks/taskFinished?id=" + TaskID + "&Comment="+inputValue;
        Common.Ajax("get", urlAssignTasks, false, 'json', sucessAssignTasks);

    });
}
function sucessAssignTasks(Success) {
    if (Success == true) {
        swal("Done !!", "Success ", "success"); 
        //var x = JSON.parse(getCookie("UserData"));
        //Request.Cookies["CID"].Value
        window.location.href = "/Users/myTasks?userId=" + JSON.parse(getCookie("UserData")).UserID + "&CID=" + JSON.parse(getCookie("CID"));

    } else {
        swal("Error", "" + inputValue, "error");

    }
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}