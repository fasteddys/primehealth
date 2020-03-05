var Depatmentnumber = window.getCookie("DepIDCookie");
function getCookie(name) {

    match = document.cookie.match(new RegExp(name + '=([^;]+)'));
    if (match) return match[1];
}

var User_ID = window.getCookie("UserIDCookie");
function getCookie(name) {

    match = document.cookie.match(new RegExp(name + '=([^;]+)'));
    if (match) return match[1];
}

if (Depatmentnumber != "1") {

    var USERSSidemenu = document.getElementById("USERSSidemenu");
    USERSSidemenu.innerHTML = "";

    var DepartmentsSidemenu = document.getElementById("DepartmentsSidemenu");
    DepartmentsSidemenu.setAttribute("style", "display:none;");
    DepartmentsSidemenu.innerHTML = "";

    var SubDepartmentsSidemenu = document.getElementById("SubDepartmentsSidemenu");
    SubDepartmentsSidemenu.innerHTML = "";

    var mailbox = document.getElementsByClassName("mailbox")[0].parentElement;
    mailbox.innerHTML = "";
    var MY_ASSIGN_TICKET = document.getElementsByClassName("MY_ASSIGN_TICKET")[0];
    MY_ASSIGN_TICKET.innerHTML = "";
    var MY_ASSIGN_TICKET = document.getElementsByClassName("MY_ASSIGN_TICKET")[1];
    MY_ASSIGN_TICKET.innerHTML = "";



    var ITStatstics1 = document.getElementsByClassName("ITStatstics")[0];
    var ITStatstics2 = document.getElementsByClassName("ITStatstics")[1];
    if (ITStatstics1 != null && ITStatstics2 != null) {
        ITStatstics1.innerHTML = "";
        ITStatstics2.innerHTML = "";
    }



}
else {
    var OpenNewTicket = document.getElementsByClassName("OpenNewTicket")[0].parentElement;
    OpenNewTicket.innerHTML = "";

    $(document).ready(

               getmytickets(),


        setInterval(function () { getmytickets(); }, 70000)

   )
}
setInterval(function () { getmymessage(); }, 70000);

function getmytickets() {



    //var cookie= function getCookie(name) {
    //     var re = new RegExp(name + "=([^;]+)");
    //     var value = re.exec(document.cookie);
    //     return (value != null) ? unescape(value[1]) : null;
    // }



    //var value = $.cookie("cookie");
    var cookie = document.cookie;
    var getCookieValues;
    function getcookie() {
        var cookieArray = cookie.split('=');
        getCookieValues = cookieArray[1].trim();
    };
    $.ajax({
        url: 'http://10.1.1.25:9292//Notifications/Push/',
        data: { 'ID': User_ID },
        type: 'Get',
        contentType: 'application/json',
        success: getmyticketsonsuccess,
        error: onerror
    });
}
function getmyticketsonsuccess(data) {


    var table = document.getElementsByClassName("message-center")[0];
    $(table).empty();
    var count = 0;
    for (var item in data)
    { count++; }
    document.getElementById("numberofnotifications").innerHTML = count;
    for (var i = 0; i < count ; i++) {

        var row = $("<a href='/Ticket/ManageTicket?id=" +
            data[i].Ticketnumber +
            "'> <div class='btn btn-info btn-circle m-r-10'><i class='ti-user'></i></div><div class='mail-contnet'><h5>new Ticket opend by "
            + data[i].username +
            "</h5> <span class='time'>"
            + data[i].TicketDate + "</span></div></a>");
        $(table).append(row);


    }


    // $("dropdown-menu dropdown-menu-right mailbox animated zoomIn").append(table);
    //         $("#dropdown-menu dropdown-menu-right mailbox animated zoomIn").scrollTop($("#conversation")[0].scrollHeight);


}





$(document).ready(
  function () {

      getmymessage()
  });

function getmymessage() {



    //var cookie= function getCookie(name) {
    //     var re = new RegExp(name + "=([^;]+)");
    //     var value = re.exec(document.cookie);
    //     return (value != null) ? unescape(value[1]) : null;
    // }



    //var value = $.cookie("cookie");
    var cookie = document.cookie;
    var getCookieValues;
    function getcookie() {
        var cookieArray = cookie.split('=');
        getCookieValues = cookieArray[1].trim();
    };
    $.ajax({
        url: 'http://10.1.1.25:9292//Notifications/Getmessages/',
        data: { 'ID': User_ID },
        type: 'Get',
        contentType: 'application/json',
        success: getmymessagesonsuccess,
        error: onerror
    });
}
function getmymessagesonsuccess(data) {

    if (Depatmentnumber != "1") {
        var table = document.getElementsByClassName("message-center")[0];

    } else {
        var table = document.getElementsByClassName("message-center")[1];


    }
    $(table).empty();
    var count = 0;
    for (var item in data)
    { count++; }
    document.getElementById("numberofmessages").innerHTML = count;
    //document.getElementById("numberofnotifications").innerHTML = count;
    for (var i = 0; i < count ; i++) {

        var row = $("<a href='/Ticket/ManageTicket?id=" +
            data[i].Ticketnumber + "'><div class='fa fa-envelope' style='font-size: 25px;'><span class='profile-status online pull-right'></span> </div>  <div class='mail-contnet'><h5>"
            + data[i].username + "</h5><span class='time'>"
            + data[i].TicketDate + "</span></div></a>");
        $(table).append(row);


    }


    // $("dropdown-menu dropdown-menu-right mailbox animated zoomIn").append(table);
    //         $("#dropdown-menu dropdown-menu-right mailbox animated zoomIn").scrollTop($("#conversation")[0].scrollHeight);


}














