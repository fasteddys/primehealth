

$(document).ready(
        getmytickets(),
     closedticket(),
     countNumberOfpendin(),
     PendingforReviewticket(),
     getTheBestEmplyee(),
     getThemostaskEmplyee(),
     PendingforReviewDataquest(),
     PendingforReviewITfusion()

        );

function getmytickets()
{
    debugger;

    var User_ID = getCookie("UserIDCookie");
    $.ajax({
        url: 'http://10.1.1.25:9292/Dashboard/countNumberOfNewTicket',
        type: 'Get',
        data: { 'UserID': User_ID },
        contentType: 'application/json',
        success: getmyticketsonsuccess
       
    });
}

function getmyticketsonsuccess(data) {
    var newpost = document.getElementById("newpost");
    newpost.innerHTML = data;
}

function getCookie(name) {

    match = document.cookie.match(new RegExp(name + '=([^;]+)'));
    if (match) return match[1];
}




function closedticket() {
    debugger;

    var User_ID = getCookie("UserIDCookie");
    $.ajax({
        url: 'http://10.1.1.25:9292/Dashboard/countNumberOfClosedJson',
        type: 'Get',
        data: { 'UserID': User_ID },
        contentType: 'application/json',
        success: getclosedtickt

    });
}

function getclosedtickt(data) {
    var newpost = document.getElementById("closedpost");
    newpost.innerHTML = data;
}



function countNumberOfpendin() {
    debugger;

    var User_ID = getCookie("UserIDCookie");
    $.ajax({
        url: 'http://10.1.1.25:9292/Dashboard/countNumberOfpendingForConfirmation',
        type: 'Get',
        data: { 'UserID': User_ID },
        contentType: 'application/json',
        success: pendingForConfirmation

    });
}

function pendingForConfirmation(data) {
    var newpost = document.getElementById("pendingForConfirmation");
    newpost.innerHTML = data;
}


function PendingforReviewticket() {
    debugger;

    var User_ID = getCookie("UserIDCookie");
    $.ajax({
        url: 'http://10.1.1.25:9292/Dashboard/countNumberOfPendingforReview',
        type: 'Get',
        data: { 'UserID': User_ID },
        contentType: 'application/json',
        success: PendingforReview

    });
}

function PendingforReview(data) {
    var newpost = document.getElementById("PendingforReview");
    newpost.innerHTML = data;
}








function getTheBestEmplyee() {
    debugger;

    var User_ID = getCookie("UserIDCookie");
    $.ajax({
        url: 'http://10.1.1.25:9292/Dashboard/TheBestEmplyee',
        type: 'Get',
        contentType: 'application/json',
        success: getTheBestEmplyeesuccess

    });
}

function getTheBestEmplyeesuccess(data) {

    var table = document.getElementById("Main");
    //$(table).empty();
    var count = 0;
    for (var item in data)
    { count++; }

    for (var i = 0; i < count ; i++) {

        //var row = $(" <div class='progress-content'><div class='row'><div class='col-lg-4'><div class='progress-text'>" +
        //    data[i].username +
        //    "</div></div><div class='col-lg-8'><div class='current-progressbar'><div class='progress'><div class='progress-bar progress-bar-primary w-"
        //    + data[i].prcent + " role='progressbar aria-valuenow='40' aria-valuemin='0' aria-valuemax='100'>"
        //    + data[i].prcent + "</div></div></div></div></div></div>");




        //var row = $("<div class='progress-content'><div class='row'><div class='col-lg-4'><div class='progress-text'>"
        //    + data[i].username + "</div></div><div class='col-lg-8'><div class='current-progressbar'><div class='progress'><div class='progress-bar progress-bar-primary w-"
        //    + data[i].prcent + " role='progressbar' aria-valuenow='" + data[i].prcent + "%' aria-valuemin='0' aria-valuemax='100'>"
        //    + data[i].prcent + "</div></div></div></div></div></div>");
        var row = $("<a name='poll_bar'>"+data[i].username  +"</a> <span name='poll_val'>"+data[i].prcent+"%</span><br/>");
        $(table).append(row);
        addstyle();
                             
    }



}


function addstyle() {
    // add button style 
    $("[name='poll_bar'").addClass("btn btn-default");
    // Add button style with alignment to left with margin.
    $("[name='poll_bar'").css({ "text-align": "left", "margin": "5px" });

    //loop 
    $("[name='poll_bar'").each(
            function (i) {
                //get poll value 	
                var bar_width = (parseFloat($("[name='poll_val'").eq(i).text()) / 2).toString();
                bar_width = bar_width + "%"; //add percentage sign.										
                //set bar button width as per poll value mention in span.
                $("[name='poll_bar'").eq(i).width(bar_width);

                //Define rules.
                var bar_width_rule = parseFloat($("[name='poll_val'").eq(i).text());
                if (bar_width_rule >= 50) { $("[name='poll_bar'").eq(i).addClass("btn btn-sm btn-success") }
                if (bar_width_rule < 50) { $("[name='poll_bar'").eq(i).addClass("btn btn-sm btn-warning") }
                if (bar_width_rule <= 10) { $("[name='poll_bar'").eq(i).addClass("btn btn-sm btn-danger") }

                //Hide dril down divs
                $("#" + $("[name='poll_bar'").eq(i).text()).hide();
            });

    //On click main menu bar show its particular detail div.
    $("[name='poll_bar'").click(function () {
        //Hide all 
        $(".panel-body").children().hide();
        //Display only selected bar texted div sub chart.
        $("#" + $(this).text()).show();
        //If not inner drill down sub detail found then move to main menu.
        if ($("#" + $(this).text()).length == 0) {
            $("#Main").show();
        }
    });
}








function getThemostaskEmplyee() {
    debugger;

    var User_ID = getCookie("UserIDCookie");
    $.ajax({
        url: 'http://10.1.1.25:9292/Dashboard/ThemostaskEmplyee',
        type: 'Get',
        contentType: 'application/json',
        success: ThemostaskEmplyee

    });
}

function ThemostaskEmplyee(data) {

    var table = document.getElementById("ThemostaskEmp");
    //$(table).empty();
    var count = 0;
    for (var item in data)
    { count++; }

    for (var i = 0; i < count ; i++) {


        var row = 

        $("<div><span><i class='fa fa-user fa-3x' style='color:red;'></i></span> <i>" + data[i] + "</i></div>");


        $(table).append(row);

    }



}



















function PendingforReviewDataquest() {
    debugger;

    var User_ID = getCookie("UserIDCookie");
    $.ajax({
        url: 'http://10.1.1.25:9292/Dashboard/countNumberOfPendingforReviewDataquest',
        type: 'Get',
        data: { 'UserID': User_ID },
        contentType: 'application/json',
        success: getNumberOfPendingforReviewDataquest

    });
}

function getNumberOfPendingforReviewDataquest(data) {
    var newpost = document.getElementById("PendingforReviewDataquest");
    newpost.innerHTML = data;
}





function PendingforReviewITfusion() {
    debugger;

    var User_ID = getCookie("UserIDCookie");
    $.ajax({
        url: 'http://10.1.1.25:9292/Dashboard/countNumberOfPendingforReviewITfusion',
        type: 'Get',
        data: { 'UserID': User_ID },
        contentType: 'application/json',
        success: countNumberOfPendingforReviewITfusion

    });
}

function countNumberOfPendingforReviewITfusion(data) {
    var newpost = document.getElementById("PendingforReviewITfusion");
    newpost.innerHTML = data;
}