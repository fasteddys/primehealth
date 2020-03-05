$(document).ready(function () {
    console.log("welcome");
    var urlPendingTasks = "/UserDailsTasks/NumberOfPendingTasks";
    Common.Ajax("get", urlPendingTasks, false, 'json', sucessGetNumberOfPendingTasks)
    //--------------------------------------------------------------------
    var urlDoneTasks = "/UserDailsTasks/NumberOfDoneTasks";
    Common.Ajax("get", urlDoneTasks, false, 'json', sucessGetNumberOfDoneTasks)
   //--------------------------------------------------------------------
    var urlAssignTasks = "/UserDailsTasks/NumberOfAssignTasks";
    Common.Ajax("get", urlAssignTasks, false, 'json', sucessGetNumberOfAssignTasks)
   //--------------------------------------------------------------------
    var urlAllTasks = "/UserDailsTasks/NumberOfAllTasks";
    Common.Ajax("get", urlAllTasks, false, 'json', sucessGetNumberOfAllTasks)
   //--------------------------------------------------------------------
    var urlMyTasks = "/Users/NumberOfMyTasks";
    Common.Ajax("get", urlMyTasks, false, 'json', sucessGetNumberOfMyTasks)
   //--------------------------------------------------------------------
    new WOW().init();
    //-----------------------------------------------------------
    $("body").niceScroll({
        cursorcolor: "#1479cf",
        cursorwidth: "20px",
        horizrailenabled: false,
        cursorheight: "150px",
        //cursorborder: "1px solid red", // css definition for cursor border
        cursorborderradius: "10px",
    });
});
// number of pending and done and assign  & my tasks 
function sucessGetNumberOfPendingTasks(result)
{
    console.log(result);
    $("#numberOfPendingTask").text(result);
}
function sucessGetNumberOfDoneTasks(result) {
    console.log(result);
    $("#numberOfDoneTask").text(result);
} 
function sucessGetNumberOfAssignTasks(result) {
    console.log(result);
    $("#numberOfAssignTask").text(result);
}
function sucessGetNumberOfAllTasks(result) {
    console.log(result);
    $("#numberOfAllTask").text(result);
    $("#numberOfAllTask2").text(result);
    $("#numberOfAllTask3").text(result);

}
function sucessGetNumberOfMyTasks(result) {
    console.log(result);
    $("#numberOfMyTask").text(result);
}
//--------------------------------------------------------------------
