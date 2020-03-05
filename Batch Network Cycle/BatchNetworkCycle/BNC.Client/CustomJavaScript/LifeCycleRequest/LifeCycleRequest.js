var AllClaimsCount = 0;
var Res;
$(document).ready(function () {
    //1 get Number Of Claims At Filteration Request Detail
    $("#selectSearchBy").change(function () {
     var val=$(this).val();
     if (val == 1)
     {
         $("#searchByBilllingMonthAndYear").hide(500, function ()
         {
             $("#searchByBatchId").show(1000);
             removeDataAtTimeLine();

         });
        //--------------------------------------------
         HideData(); 

     }
     else if (val == 2)
     {
         $("#searchByBatchId").hide(500, function () {
             $("#searchByBilllingMonthAndYear").show(1000);
             removeDataAtTimeLine();

         });
         //--------------------------------------------
         HideData(); 
     }
    })
    //2 get life cycle Batch Request
    $("#btnSearchRequestByBatchId").on("click", function () {
        removeDataAtTimeLine();
        HideData(); 

        //--------------------------------------------------------------------------------------------------
        var _BatchNumber = $("#BatchIdOnSystem").val();
        var _BatchSystemFK = $("#SystemNameFk").val();
        if (_BatchNumber == "" || _BatchSystemFK == "")
        {
            ShowALert(1, "Please Enter All Data");
        }
        else
        {
                var urlSearchLifeCycleRequest = "/Receiving/getRequestLifeCycleDisplay";
            var LifeCycleRequestInput =
                {
                    BatchSystemFK: _BatchSystemFK,
                    BatchNumber: _BatchNumber,
                    serachByBitch:1,
                }
            Common.Ajax("post", urlSearchLifeCycleRequest, JSON.stringify(LifeCycleRequestInput), 'json', sucessSearchLifeCycleRequestForBatch);
        } 

    })
    //3 get life cycle Receiving Request
    $("#btnSearchRequestByBilllingMonthAndYear").on("click", function () {
        removeDataAtTimeLine();
        HideData(); 
        //--------------------------------------------------------------------------------------------------
        var BillingYearAndMonth = $("#BilllingMonthAndYear").val();
        var _BillingYear = $("#BilllingMonthAndYear").val().split("-")[0];
        var _BillingMonth = $("#BilllingMonthAndYear").val().split("-")[1];
        var _ProviderFK = $("#ProviderID").val();
        if (BillingYearAndMonth=="" || _ProviderFK == "")
        {
            ShowALert(1, "Please Enter All Data");

        }
        else
        {
           //--------------------------------------------------------------------------------------------------
            var urlSearchLifeCycleRequest = "/Receiving/getRequestLifeCycleDisplay";
            var LifeCycleRequestInput =
                {
                    BillingYear: _BillingYear,
                    BilllingMonth: _BillingMonth,
                    ProviderFK: _ProviderFK,
                    serachByBitch: 0,
                }
            Common.Ajax("post", urlSearchLifeCycleRequest, JSON.stringify(LifeCycleRequestInput), 'json', sucessSearchLifeCycleRequestForRecevingRequest);

        }
       
    })
    //-------------------------------------------------------------------------------------
    $(".ReceivingTableButtonShowAndHide").on("click", function () {
        $("#ReceivingData").show(1000);
        $(this).hide();
        removeDataAtTimeLine();
    })
    $(".FilitrationTableButtonShowAndHide").on("click", function () {
        $("#BatchingFilterationData").show(1000);
        $(this).hide();
        removeDataAtTimeLine();

    })
    $(".BatchTableButtonShowAndHide").on("click", function () {
        $("#BatchingData").show(1000);
        $(this).hide();
        removeDataAtTimeLine();

    })
});
/*-----------------------------------------------------------*/
function sucessSearchLifeCycleRequestForBatch(Res)
{
    if (Res.Success) {
        //show
        $(".timeline").show(200);
        /*-----------------------------------------------------------*/
        //Batching Team Data
        if (Res.Data.BatchingRequest != null)
        {
            displayBatchingTeam(Res);//1
        }
        /*-----------------------------------------------------------*/
        //AuditingTeam
        if(Res.Data.BatchAuditingRequest!=null)
        {
            displayAuditingTeam(Res);//2
        }
        /*-----------------------------------------------------------*/
        //sub AuditingTeam
        if (Res.Data.CategoryAuditList!=null)
        {
            displayAuditCategTeam(Res);//3
        }
        /*-----------------------------------------------------------*/
        //DataEntry Adminstration Team Data
        if (Res.Data.DataEntryAdminstrationRequest != null)
        {
            displayDataEntryAdminstrationTeam(Res);//4
        }
        /*-----------------------------------------------------------*/
        //DataEntry Batch Assigning
        if (Res.Data.DataEntryBatchAssigningRequestList != null)
        {
            displayDataEntryBatchAssigningTeam(Res)//5
        }
        /*-----------------------------------------------------------*/
        //Batch Closing Team
        if (Res.Data.BatchClosingRequest != null)
        {
            displayBatchClosingTeam(Res)//6
        }
        /*-----------------------------------------------------------*/
        //Batch Closing ReAdministration Team
        if (Res.Data.BatchClosingReAdministrationRequest != null)
        {
            BatchClosingReAdministrationTeam(Res);
        }
        /*-----------------------------------------------------------*/
    }
    else {
        ShowALert(4, Res.Message);
    }
}//1
function sucessSearchLifeCycleRequestForRecevingRequest(Res)
{
    if (Res.Success)
    {
        displayRecievingRequest(Res);

    }
    else
    {
        ShowALert(4, Res.Message);

    }
}//2
/*-----------------------------------------------------------*/
function displayRecievingRequest(Res)
{
    $('#RecievingRequestDatabale').DataTable().destroy();
    var $table = $('#RecievingRequestDatabale');
    var RecievingRequestList = [];
    for (var i = 0; i < Res.Data.length; i++) {
        RecievingRequestList.push(Res.Data[i].RecievingRequest);
    }
    this.Res = Res;
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": RecievingRequestList,
        "columns": [     
            { "data": "ReceivingRequestID" },
            { "data": "ProviderName" },
            { "data": "ReceivingOfficerCalimsCount" },
            { "data": "BillingYear" },
            { "data": "BilllingMonth" },
            {
                data: "CreationDate",//3
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button class="btn btn-info" onclick="displayBatchingfilitrationRequests('+data.ReceivingRequestID+')">Filitration Requests</button>';

                }
            }, 
        ]
    });
    //show
    $("#ReceivingData").show(1000);
} //1
/*-----------------------------------------------------------*/
function displayBatchingfilitrationRequests(ReceivingRequestID) {
    $('#BatchingFilterationRequestsDataTable').DataTable().destroy();
    var $table = $('#BatchingFilterationRequestsDataTable');
    var BatchingFilterationRequestsList = [];
    for (var i = 0; i < Res.Data.length; i++) {
        for (var j = 0; j < Res.Data[i].filterationRquestlistInfo.length; j++) {
            if (Res.Data[i].filterationRquestlistInfo[j].BatchingFilterationDetails.ReceivingRequestIFK == ReceivingRequestID)
            {
                BatchingFilterationRequestsList.push(Res.Data[i].filterationRquestlistInfo[j].BatchingFilterationDetails);
            }

        }
    }
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": BatchingFilterationRequestsList,
        "columns": [
            { "data": "BatchingFilterationDetailID" },
            { "data": "TotalClaimsCount" },
            { "data": "RemainingClaimsCount" },
            { "data": "CategoryName" },
            {
                data: "CreationDate",//3
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button class="btn btn-info" onclick="displayBatchingRequests(' +data.BatchingFilterationDetailID + ')">Show Batch</button>';

                }
            }, 
        ]
    });
    //show
    $("#ReceivingData").hide(500);
    $(".ReceivingTableButtonShowAndHide").show(500);
    $("#BatchingFilterationData").show(1000);
}//2
/*-----------------------------------------------------------*/
function displayBatchingRequests(BatchingFilterationDetailsFK) {
    $('#BatchingRequestsDataTable').DataTable().destroy();
    var $table = $('#BatchingRequestsDataTable');
    var BatchingRequestsList = [];
    for (var i = 0; i < Res.Data.length; i++) {
        for (var j = 0; j < Res.Data[i].filterationRquestlistInfo.length; j++) {
            for (var k = 0; k < Res.Data[i].filterationRquestlistInfo[j].BatchingRequestInfoList.length; k++) {
                if (Res.Data[i].filterationRquestlistInfo[j].BatchingRequestInfoList[k].BatchingRequest.BatchingFilterationDetailsFK == BatchingFilterationDetailsFK)
                    {
                    BatchingRequestsList.push(Res.Data[i].filterationRquestlistInfo[j].BatchingRequestInfoList[k].BatchingRequest);
                }
            }

        }
    }
    var table = $table.DataTable({
        sDom: '<"text-right mb-md"T><"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
        buttons: ['print', 'excel', 'pdf'],
        retrieve: true,
        "data": BatchingRequestsList,
        "columns": [
            { "data": "BatchingRequestID" },
            { "data": "NumberOfBatchClaims" },
            { "data": "BatchSystemName" },
            { "data": "BatchNumber" },
            //{ "data": "OfficerName" },
            //{ "data": "BatchingOfficerComment" },
            //{ "data": "FilterationCategoryName" },
            //{
            //    data: "CreationDate",//3
            //    "render": function (data) {
            //        return moment(data).format('DD/MM/YYYY hh:mm:ss A');
            //    }
            //},
            {
                data: "CreationDate",//3
                "render": function (data) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss A');
                }
            },
           {
               "mData": null,
               "bSortable": false,
               "mRender": function (data) {
                   return '<button class="btn btn-info" onclick="showTimeLine('+data.BatchingRequestID+')">Life Cycle</button>';

               }
           }, 
        ]
    });
    //show
    $("#BatchingFilterationData").hide(500);
    $(".FilitrationTableButtonShowAndHide").show(500);
    $("#BatchingData").show(1000);

}//3
/*-----------------------------------------------------------*/
function displayBatchingTeam(Res)
{
        var textHtml;
        var BatchingTeamDate = moment(Res.Data.BatchingRequest.CreationDate).format('DD/MM/YYYY');
        var BatchingTeamTime = moment(Res.Data.BatchingRequest.CreationDate).format('hh:mm A');
        var BatchNumber = Res.Data.BatchingRequest.BatchNumber;
        var BatchSystemName = Res.Data.BatchingRequest.BatchSystemName;
        var NumberOfBatchClaims = Res.Data.BatchingRequest.NumberOfBatchClaims;
        var FilterationCategoryName = Res.Data.BatchingRequest.FilterationCategoryName;
        var OfficerName = Res.Data.BatchingRequest.OfficerName;
        var BatchingOfficerComment = Res.Data.BatchingRequest.BatchingOfficerComment;
        $("#BatchingTeam").empty();
        textHtml = $('<div id="BatchingTeamDate" class="tm-title">'+
                        '<h5 class="Date m-0 pt-2 pb-2 text-capitalize text-danger">Batching Team</h5>'+
                        '<h5 class="CurrentDate Date m-0 pt-2 pb-2 text-uppercase text-info">'+BatchingTeamDate+'</h5>'+
                        '</div >'+
                        '<ol id="BatchingTeamDetials" class="tm-items">'+
                            '<li id="BatchingTeamInfo">'+
                                '<div class="tm-info">'+
                                    '<div class="tm-icon"><i class="fas fa-star"></i></div>'+
                                    '<time class="tm-datetime" datetime="2017-11-22 19:13">'+
                                        '<div class="BatchingTeamTime mt-3 tm-datetime-time">'+BatchingTeamTime+'</div>'+
                                    '</time>'+
                                '</div>'+
                                '<div class="tm-box appear-animation appear-animation-visible" data-appear-animation="fadeInRight" data-appear-animation-delay="100">'+
                                    '<p>'+
                                        'Batch Created with number id <span id="BatchNumber" class="text-primary">#'+BatchNumber+'</span> at <span id="BatchSystemName" class="text-primary">#'+BatchSystemName+'</span> with Number Of Claims <span id="NumberOfBatchClaims" class="text-primary">#'+NumberOfBatchClaims+'</span> at Categery <span id="FilterationCategoryName" class="text-primary">#'+FilterationCategoryName+'</span>'+
                                    '</p>'+
                                    '<div class="tm-meta">'+
                                        '<span>'+
                                            '<i class="fas fa-user"></i> By <a id="userName" href="#">'+OfficerName+'</a>'+
                                        '</span>'+
                                    '</div>'+
                                '</div>'+
                            '</li>'+
                            '<li id="BatchingTeamUserComment">'+
                                '<div class="tm-info">'+
                                    '<div class="tm-icon"><i class="fas fa-comments"></i></div>'+
                                    '<time class="tm-datetime" datetime="2017-11-19 18:13">'+
                                        '<div class="BatchingTeamTime mt-3 tm-datetime-time">'+BatchingTeamTime+'</div>'+
                                    '</time>'+
                                '</div>'+
                                '<div class="tm-box appear-animation appear-animation-visible" data-appear-animation="fadeInRight" data-appear-animation-delay="250">'+
                                    '<p class="Comment">'+BatchingOfficerComment+'</p>'+
                                '</div>'+
                            '</li>'+
                        '</ol>');

        $("#BatchingTeam").append(textHtml);
        if (BatchingOfficerComment != " ") {
            $("#BatchingTeamUserComment").hide();
        }
}//1
/*-----------------------------------------------------------*/
function displayAuditingTeam(Res)//2
{
        var textHtml;
        var AuditingTeamDate = moment(Res.Data.BatchAuditingRequest.CreationDate).format('DD/MM/YYYY');
        var AuditingTeamTime = moment(Res.Data.BatchAuditingRequest.CreationDate).format('hh:mm A');
        var AuditingTransferToDataEntryOn = moment(Res.Data.BatchAuditingRequest.CreationDate).format('DD/MM/YYYY');
        var NumberOfAuditingBatchClaims = Res.Data.BatchAuditingRequest.NumberOfAuditingBatchClaims;
        var TotalNumberOfApprovedClaims = Res.Data.BatchAuditingRequest.TotalNumberOfApprovedClaims;
        var TotalNumberOfRejectedClaims = Res.Data.BatchAuditingRequest.TotalNumberOfRejectedClaims;
        var numberOfAuditingInFlow = 0;
        if (Res.Data.CategoryAuditList!=null)
        {
            numberOfAuditingInFlow = Res.Data.CategoryAuditList.length; 

        }
        $("#AuditingTeam").empty();
        textHtml = $('<div id="AuditingTeamDate" class="tm-title">'+
                         '<h5 class="Date m-0 pt-2 pb-2 text-capitalize text-danger" >Auditing Team</h5>'+
                         '<h5 class=" CurrentDate m-0 pt-2 pb-2 text-uppercase text-info">'+AuditingTeamDate+'</h5>'+
                      '</div >'+
                    '<ol id="AuditingTeamDetials" class="tm-items">'+
                        '<li id="AuditingTeamInfo">'+
                            '<div class=" tm-info">'+
                                '<div class="tm-icon"><i class="fas fa-star"></i></div>'+
                                '<time class="tm-datetime" datetime="2017-11-22 19:13">'+
                                    '<div class="AuditingTeamTime tm-datetime-time">'+AuditingTeamTime+'</div>'+
                                '</time>'+
                            '</div>'+
                            '<div class="tm-box appear-animation appear-animation-visible" data-appear-animation="fadeInRight" data-appear-animation-delay="100">'+
                                '<p>'+
                                       'Bitch Arrived To Audit Team With NumberClaims <span id="NumberOfAuditingBatchClaims" class="text-primary">'+NumberOfAuditingBatchClaims+'</span> and'+
                                       'Total Number Of Approved Claims is  <span id="TotalNumberOfApprovedClaims" class="text-primary">'+TotalNumberOfApprovedClaims+'</span>'+
                                       'Total Number Of Rejected Claims is  <span id="TotalNumberOfRejectedClaims" class="text-primary">'+TotalNumberOfRejectedClaims+'</span>'+
                                '</p>'+
                                '<div class="tm-meta">'+
                                   '<span>'+
                                        '<i class="fas fa-tag"></i>'+
                                        '<a id="numberOfAuditingInFlow" href="#">'+numberOfAuditingInFlow+'</a> Auditing Flow Team'+
                                   '</span>'+
                                '</div>'+

                            '</div>'+
                        '</li>'+
                    '</ol>');
        $("#AuditingTeam").append(textHtml);
}//2
/*------------------------------------------------------------*/
function displayAuditCategTeam(Res)
{
    var textHtml;
    var AuditingCategTeamDate;
    var AuditingCategTime;
    var NumberOfApprovedClaims;
    var ApprovedClaimsAmount;
    var NumberOfPendingClaims;
    var PendingClaimsAmount;
    var NumberOfRejectedClaims;
    var RejectedClaimsAmount;
    var UserName;
    var numberOfAuditingInFlow = Res.Data.CategoryAuditList.length; 
    $("#AuditCategTeam").empty();
    for (var i = 0; i < numberOfAuditingInFlow; i++) {
        AuditingCategTeamDate = moment(Res.Data.CategoryAuditList[i].AssignedTime).format('DD/MM/YYYY');
        AuditingCategTime = moment(Res.Data.CategoryAuditList[i].AssignedTime).format('hh:mm A');
        NumberOfApprovedClaims = Res.Data.CategoryAuditList[i].NumberOfApprovedClaims;
        ApprovedClaimsAmount = Res.Data.CategoryAuditList[i].ApprovedClaimsAmount;
        NumberOfPendingClaims = Res.Data.CategoryAuditList[i].NumberOfPendingClaims;
        PendingClaimsAmount = Res.Data.CategoryAuditList[i].PendingClaimsAmount;
        NumberOfRejectedClaims = Res.Data.CategoryAuditList[i].NumberOfRejectedClaims;
        RejectedClaimsAmount = Res.Data.CategoryAuditList[i].RejectedClaimsAmount;
        UserName = Res.Data.CategoryAuditList[i].OfficerName;
        CategoryAuditName = Res.Data.CategoryAuditList[i].CategoryAuditName
        comment = Res.Data.CategoryAuditList[i].CategoryAuditOfficerComment
        textHtml = $('<div class="AuditCategTeamDetails col-md-10">' +
            '<div class="tm-title">' +
            '<h5 class="Date m-0 pt-2 pb-2 text-capitalize text-danger">' + CategoryAuditName + '</h5>' +
            '<h5 class="CurrentDate m-0 pt-2 pb-2 text-uppercase text-info">' + AuditingCategTeamDate + '</h5>' +//change 
            '</div>' +
            '  <ol class="tm-items">' +
            '      <li>' +
            '        <div class="tm-info">' +
            '            <div class="tm-icon"><i class="fas fa-heart"></i></div>' +
            '         <time class="tm-datetime" datetime="2017-09-08 16:13">' +
            '             <div class="tm-datetime-time">' + AuditingCategTime + '</div>' +//change
            '         </time>' +
            '       </div>' +
            '        <div class="tm-box appear-animation  appear-animation-visible" data-appear-animation="fadeInRight">' +
            '            <div class="row">' +
            '              <div class="col-md-12 mb-2">' +
            '                  <div class="card">' +
            '                         <div class="card-body">' +
            '                           <h5 class="card-title">' + CategoryAuditName + '</h5>' +
            //'                             <p class="card-text">Some quick example text to build on the card title and make up the bulk of the cards content</p >' +
            '                         </div>' +
            '                         <ul class="list-group list-group-flush">' +
            '                           <li class="list-group-item">Number Of Approved Claims <span class="text-primary">' + NumberOfApprovedClaims + '<\span></li>' +//change
            '                           <li class="list-group-item">Approved Claims Amount <span class="text-primary">' + ApprovedClaimsAmount + '<\span></li>' +//change
            '                           <li class="list-group-item">Number Of Pending Claims <span class="text-primary">' + NumberOfPendingClaims + '<\span></li>' +//change
            '                           <li class="list-group-item">Pending Claims Amount <span class="text-primary">' + PendingClaimsAmount + '<\span></li>' +//change
            '                           <li class="list-group-item">Number Of Rejected Claims <span class="text-primary">' + NumberOfRejectedClaims + '<\span></li>' +//change
            '                           <li class="list-group-item">Rejected Claims Amount <span class="text-primary">' + RejectedClaimsAmount + '<\span></li>' +//change
            '                           <li class="list-group-item">Comment: <span class="text-primary">' + comment + '<\span></li>' +//change
            '                        </ul>' +
            '                      <div class="card-body">' +
            '                           <span>' +
            '                               <i class="fas fa-user"></i> By <a id="userName" href="#">' + UserName + '</a>' +//change
            '                          </span>' +
            '                      </div>' +
            '                  </div>' +
            '              </div>' +
            '          </div>' +
            '      </div>' +
            '   </li>' +
            '  </ol>' +
            ' </div >');
        $("#AuditCategTeam").append(textHtml);
    }
}//3
/*------------------------------------------------------------*/
function displayDataEntryAdminstrationTeam(Res)
{
     var textHtml;
        var DataEntryAdminstrationTeamDate = moment(Res.Data.DataEntryAdminstrationRequest.AssignedTime).format('DD/MM/YYYY');
        var DataEntryAdminstrationTeamTime = moment(Res.Data.DataEntryAdminstrationRequest.AssignedTime).format('hh:mm A');
        OfficerName = Res.Data.DataEntryAdminstrationRequest.OfficerName;
        DataEntryAdminComment=Res.Data.DataEntryAdminstrationRequest.DataEntryAdminComment

        $("#DataEntryAdminstrationTeam").empty();
        textHtml = $('<div id="DataEntryAdminstrationTeamDate" class="tm-title">'+
                        '<h5 class="Date m-0 pt-2 pb-2 text-capitalize text-danger">DataEntry Adminstration Team</h5>'+
                        '<h5 class="CurrentDate m-0 pt-2 pb-2 text-uppercase text-info">'+DataEntryAdminstrationTeamDate+'</h5>'+
                      '</div>'+
                        '<ol class="tm-items DataEntryAdminstrationTeamDetials">'+
                            '<li id="DataEntryAdminstrationTeamInfo">'+
                                '<div class="tm-info">'+
                                    '<div class="tm-icon"><i class="fas fa-star"></i></div>'+
                                    '<time class="tm-datetime" datetime="2017-11-22 19:13">'+
                                        '<div class="DataEntryAdminstrationTeamTime tm-datetime-time">'+DataEntryAdminstrationTeamTime+'</div>'+
                                    '</time>'+
                                '</div>'+
                                '<div class="tm-box appear-animation appear-animation-visible"  data-appear-animation="fadeInRight" data-appear-animation-delay="100">'+
                                    '<p>Battch arrived to DataEntry Adminstration team</p>'+
                                    '<div class="tm-meta">'+
                                        '<span>'+
                                            '<i class="fas fa-user"></i> By <a id="userName" href="#">'+OfficerName+'</a>'+
                                        '</span>'+
                                    '</div>'+
                                '</div>'+
                            '</li>'+
                            '<li id="DataEntryAdminstrationTeamUserComment">'+
                                '<div class="tm-info">'+
                                    '<div class="tm-icon"><i class="fas fa-comments"></i></div>'+
                                    '<time class="tm-datetime" datetime="2017-11-19 18:13">'+
                                        '<div class="DataEntryAdminstrationTeamTime mt-3 tm-datetime-time">'+DataEntryAdminstrationTeamTime+'</div>'+
                                    '</time>'+
                                '</div>'+
                                '<div class="tm-box appear-animation appear-animation-visible" data-appear-animation="fadeInRight" data-appear-animation-delay="250">'+
                                    '<p class="Comment">'+DataEntryAdminComment+'</div>'+
                            '</li>'+
                        '</ol>');


        if(DataEntryAdminComment != "") {
            $("#DataEntryAdminstrationTeamUserComment").show();
        }
        $("#DataEntryAdminstrationTeam").append(textHtml);
      
}//4
/*------------------------------------------------------------*/
function displayDataEntryBatchAssigningTeam(Res)
{
    var textHtml;
    var DataEntryBatchAssigningTeamDate;
    var DataEntryBatchAssigningTime;
    var numberOfAssigningInFlow = Res.Data.DataEntryBatchAssigningRequestList.length;
    var DataEntryOfficerReceivingComment;
    var DataEntryOfficerFinishedComment;
    var DataEntryAdminComment;
    var DataEntryOfficerName;
    var AssignedClaimsNumber;

    $("#DataEntryBatchAssigning").empty();
    for (var i = 0; i < numberOfAssigningInFlow; i++) {
        DataEntryBatchAssigningTeamDate = moment(Res.Data.DataEntryBatchAssigningRequestList[i].AssignedTime).format('DD/MM/YYYY');
        DataEntryBatchAssigningTime = moment(Res.Data.DataEntryBatchAssigningRequestList[i].AssignedTime).format('hh:mm A');
        DataEntryOfficerReceivingComment = Res.Data.DataEntryBatchAssigningRequestList[i].DataEntryOfficerReceivingComment;
        DataEntryOfficerFinishedComment = Res.Data.DataEntryBatchAssigningRequestList[i].DataEntryOfficerFinishedComment;
        DataEntryAdminComment = Res.Data.DataEntryBatchAssigningRequestList[i].DataEntryAdminComment;
        DataEntryOfficerName = Res.Data.DataEntryBatchAssigningRequestList[i].DataEntryOfficerName;
        AssignedClaimsNumber = Res.Data.DataEntryBatchAssigningRequestList[i].AssignedClaimsNumber;

        textHtml = $('<div class="AuditCategTeamDetails col-md-10">' +
            '<div class="tm-title">' +
            '<h5 class="Date m-0 pt-2 pb-2 text-capitalize text-danger">DataEntry Batch Assigning</h5>' +
            '<h5 class="CurrentDate m-0 pt-2 pb-2 text-uppercase text-info">' + DataEntryBatchAssigningTeamDate + '</h5>' +//change 
            '</div>' +
            '  <ol class="tm-items">' +
            '      <li>' +
            '        <div class="tm-info">' +
            '            <div class="tm-icon"><i class="fas fa-heart"></i></div>' +
            '         <time class="tm-datetime" datetime="2017-09-08 16:13">' +
            '             <div class="tm-datetime-time">' + DataEntryBatchAssigningTime + '</div>' +//change
            '         </time>' +
            '       </div>' +
            '        <div class="tm-box appear-animation  appear-animation-visible" data-appear-animation="fadeInRight">' +
            '            <div class="row">' +
            '              <div class="col-md-12 mb-2">' +
            '                  <div class="card">' +
            '                         <div class="card-body">' +
            '                           <h5 class="card-title">' + DataEntryOfficerName + '</h5>' +
            //'                             <p class="card-text">Some quick example text to build on the card title and make up the bulk of the cards content</p >' +
            '                         </div>' +
            '                         <ul class="list-group list-group-flush">' +
            '                           <li class="list-group-item">Data Entry Admin Comment: <span class="text-primary">' + DataEntryAdminComment + '<\span></li>' +//change
            '                           <li class="list-group-item">DataEntry Officer Receiving Comment: <span class="text-primary">' + DataEntryOfficerReceivingComment + '<\span></li>' +//change
            '                           <li class="list-group-item">DataEntry Officer Finished Comment: <span class="text-primary">' + DataEntryOfficerFinishedComment + '<\span></li>' +//change
            '                        </ul>' +
            '                      <div class="card-body">' +
            '                           <span>' +
            '                               <i class="fas fa-user"></i> By <a id="userName" href="#">' + DataEntryOfficerName + '</a>' +//change
            '                          </span>' +
            '                      </div>' +
            '                  </div>' +
            '              </div>' +
            '          </div>' +
            '      </div>' +
            '   </li>' +
            '  </ol>' +
            ' </div >');
        $("#DataEntryBatchAssigning").append(textHtml);
    }
}//5
/*------------------------------------------------------------*/
function displayBatchClosingTeam(Res)
{
    var textHtml;
    var BatchClosingTeamTeamDate;
    var BatchClosingTeamTime;
    var ClosingOfficerAssignedComment;
    var ConfirmReceivingComment;
    var FinishedReviewingComment;
    var BatchClosingTeamOfficerName;

    $("#BatchClosingTeam").empty();
    BatchClosingTeamDate = moment(Res.Data.BatchClosingRequest.ClosingOfficerAssignedTime).format('DD/MM/YYYY');
    BatchClosingTeamTime = moment(Res.Data.BatchClosingRequest.ClosingOfficerAssignedTime).format('hh:mm A');
    ClosingOfficerAssignedComment = Res.Data.BatchClosingRequest.ClosingOfficerAssignedComment;
    ConfirmReceivingComment = Res.Data.BatchClosingRequest.ConfirmReceivingComment;
    FinishedReviewingComment = Res.Data.BatchClosingRequest.FinishedReviewingComment;
    BatchClosingTeamOfficerName = Res.Data.BatchClosingRequest.OfficerName;

    textHtml = $('<div class="AuditCategTeamDetails col-md-10">' +
        '<div class="tm-title">' +
        '<h5 class="Date m-0 pt-2 pb-2 text-capitalize text-danger">Batch Closing Team</h5>' +
        '<h5 class="CurrentDate m-0 pt-2 pb-2 text-uppercase text-info">' + BatchClosingTeamDate + '</h5>' +//change 
        '</div>' +
        '  <ol class="tm-items">' +
        '      <li>' +
        '        <div class="tm-info">' +
        '            <div class="tm-icon"><i class="fas fa-heart"></i></div>' +
        '         <time class="tm-datetime" datetime="2017-09-08 16:13">' +
        '             <div class="tm-datetime-time">' + BatchClosingTeamTime + '</div>' +//change
        '         </time>' +
        '       </div>' +
        '        <div class="tm-box appear-animation  appear-animation-visible" data-appear-animation="fadeInRight">' +
        '            <div class="row">' +
        '              <div class="col-md-12 mb-2">' +
        '                  <div class="card">' +
        '                         <div class="card-body">' +
        '                           <h5 class="card-title">' + BatchClosingTeamOfficerName + '</h5>' +
        //'                             <p class="card-text">Some quick example text to build on the card title and make up the bulk of the cards content</p >' +
        '                         </div>' +
        '                         <ul class="list-group list-group-flush">' +
        '                           <li class="list-group-item">Assigned Comment: <span class="text-primary">' + ClosingOfficerAssignedComment + '<\span></li>' +//change
        '                           <li class="list-group-item">Receiving Comment: <span class="text-primary">' + ConfirmReceivingComment + '<\span></li>' +//change
        '                           <li class="list-group-item">Finished Comment: <span class="text-primary">' + FinishedReviewingComment + '<\span></li>' +//change
        '                        </ul>' +
        '                      <div class="card-body">' +
        '                           <span>' +
        '                               <i class="fas fa-user"></i> By <a id="userName" href="#">' + BatchClosingTeamOfficerName + '</a>' +//change
        '                          </span>' +
        '                      </div>' +
        '                  </div>' +
        '              </div>' +
        '          </div>' +
        '      </div>' +
        '   </li>' +
        '  </ol>' +
        ' </div >');
    $("#BatchClosingTeam").append(textHtml);
}//6
/*------------------------------------------------------------*/
function BatchClosingReAdministrationTeam(Res)
{
    var textHtml;
    var BatchClosingReAdministrationTeamDate;
    var BatchClosingReAdministrationTeamTime;
    var ConfirmedReceivingComment;
    var AssignedByAdminComment;
    var FinalClosureComment;
    var ReAdministrationOfficerName;
    var ArchivingSystemTicketNo;

    $("#BatchClosingReAdministrationTeam").empty();
    BatchClosingReAdministrationTeamDate = moment(Res.Data.BatchClosingReAdministrationRequest.ReAdministrationOfficerAssignedTime).format('DD/MM/YYYY');
    BatchClosingReAdministrationTeamTime = moment(Res.Data.BatchClosingReAdministrationRequest.ReAdministrationOfficerAssignedTime).format('hh:mm A');
    AssignedByAdminComment = Res.Data.BatchClosingReAdministrationRequest.AssignedByAdminComment;
    ConfirmedReceivingComment = Res.Data.BatchClosingReAdministrationRequest.ConfirmedReceivingComment;
    FinalClosureComment = Res.Data.BatchClosingReAdministrationRequest.FinalClosureComment;
    ReAdministrationOfficerName = Res.Data.BatchClosingReAdministrationRequest.ReAdministrationOfficerName;
    ArchivingSystemTicketNo = Res.Data.BatchClosingReAdministrationRequest.ArchivingSystemTicketNo;

    textHtml = $('<div class="AuditCategTeamDetails col-md-10">' +
        '<div class="tm-title">' +
        '<h5 class="Date m-0 pt-2 pb-2 text-capitalize text-danger">Closing ReAdministration Team</h5>' +
        '<h5 class="CurrentDate m-0 pt-2 pb-2 text-uppercase text-info">' + BatchClosingReAdministrationTeamDate + '</h5>' +//change 
        '</div>' +
        '  <ol class="tm-items">' +
        '      <li>' +
        '        <div class="tm-info">' +
        '            <div class="tm-icon"><i class="fas fa-heart"></i></div>' +
        '         <time class="tm-datetime" datetime="2017-09-08 16:13">' +
        '             <div class="tm-datetime-time">' + BatchClosingReAdministrationTeamTime + '</div>' +//change
        '         </time>' +
        '       </div>' +
        '        <div class="tm-box appear-animation  appear-animation-visible" data-appear-animation="fadeInRight">' +
        '            <div class="row">' +
        '              <div class="col-md-12 mb-2">' +
        '                  <div class="card">' +
        '                         <div class="card-body">' +
        '                           <h5 class="card-title">Archiving TicketNo:' + ArchivingSystemTicketNo + '</h5>' +
        //'                             <p class="card-text">Some quick example text to build on the card title and make up the bulk of the cards content</p >' +
        '                         </div>' +
        '                         <ul class="list-group list-group-flush">' +
        '                           <li class="list-group-item">Assigned Comment: <span class="text-primary">' + AssignedByAdminComment + '<\span></li>'+//change
        '                           <li class="list-group-item">Receiving Comment: <span class="text-primary">' + ConfirmedReceivingComment + '<\span></li>' +//change
        '                           <li class="list-group-item">Finished Comment: <span class="text-primary">' + FinalClosureComment + '<\span></li>' +//change
        '                        </ul>' +
        '                      <div class="card-body">' +
        '                           <span>' +
        '                               <i class="fas fa-user"></i> By <a id="userName" href="#">' + ReAdministrationOfficerName + '</a>' +//change
        '                          </span>' +
        '                      </div>' +
        '                  </div>' +
        '              </div>' +
        '          </div>' +
        '      </div>' +
        '   </li>' +
        '  </ol>' +
        ' </div >');
    $("#BatchClosingReAdministrationTeam").append(textHtml);
}//7
/*------------------------------------------------------------*/
// this function used when click at Life Cycle for Batching 
function showTimeLine(BatchingRequestID)
{
    $(".BatchTableButtonShowAndHide").show(500);
    $("#BatchingData").hide(500);
    removeDataAtTimeLine();
    var urlSearchLifeCycleRequestByBatchingId = '/Receiving/getRequestLifeCycleDisplayByBatchingID?BatchingRequestID='+BatchingRequestID;
    Common.Ajax("get", urlSearchLifeCycleRequestByBatchingId, null, 'json', sucessSearchLifeCycleRequestForBatch);

}//8
/*------------------------------------------------------------*/
function removeDataAtTimeLine()
{
    $(".timeline").hide(100, function () {
        $("#BatchingTeam").empty();//1
        $("#AuditingTeam").empty();//2
        $("#AuditCategTeam").empty();//3
        $("#DataEntryAdminstrationTeam").empty();//4
        $("#DataEntryBatchAssigning").empty();//5
        $("#BatchClosingTeam").empty();//6
        $("#BatchClosingReAdministrationTeam").empty();//7
    });

}//9
/*------------------------------------------------------------*/
function HideData()
{
    $("#ReceivingData,#BatchingFilterationData,#BatchingData").hide();
    $(".ReceivingTableButtonShowAndHide,.FilitrationTableButtonShowAndHide,.BatchTableButtonShowAndHide").hide();
    $("#BatchingTeamUserComment,#DataEntryAdminstrationTeamUserComment").hide();
}
/*------------------------------------------------------------*/