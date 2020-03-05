

$(document).ready(function () {
    ShowApprovals();
});
function ShowApprovals() {

    var urlCheckURL = "http://localhost:54412/api/Manager/CheckUserIsManager?ID=" + 1;
    Common.Ajax('post', urlCheckURL, null, 'json', SucessShow, false);
}
function SucessShow(result) {
    if (result.Data.ShowApproval === false) {
        document.getElementById("ApprovalTab").style.display = "none";
    }
    else {
    
      //  document.getElementById("ApprovalTab").style.display = "visible";

    }
    if (result.Data.SowhConfigrations === false) {
        document.getElementById("ConfigurationTab").style.display = "none";

    }
    else {

        document.getElementById("ConfigurationTab").style.display = "visible";

    }




}



function GetGlobalUserID()
{
    return document.getElementById("GlobalUserID").value;
}