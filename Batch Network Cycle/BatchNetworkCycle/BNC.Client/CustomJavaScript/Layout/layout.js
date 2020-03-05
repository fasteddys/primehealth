
$(document).ready(function () {
    $("#LoadingDiv").delay(750).hide(0);//750
    getAllDataInfoAboutBatchingTeam();
    ActiveNavigationBarCell();
    // active menu that it open 
    $("nav a").on("click", function () {
        $(this).parent().addClass("nav-active").siblings().removeClass("nav-active");
    });
    //-------------------------------------------------------------
    $(':input[type="number"]').on("keyup", function () {
        if ($(this).val() < 0) {
            $(this).val('');

        }
    });
});
//---------------------------------------------------------------------
var angle = 0;
setInterval(function () {
    //console.log(angle);
    $("#image")
        .css('-webkit-transform', 'rotate(' + angle + 'deg)')
        .css('-moz-transform', 'rotate(' + angle + 'deg)')
        .css('-ms-transform', 'rotate(' + angle + 'deg)');
    angle++; angle++; angle++;
}, 20);
$(document).ajaxStart(function () {
    $("#LoadingDiv").show();
    //    var Wheight = $(window).height() / 2 - $(".Rotator").height() / 2 + "px";
    //    $(".Rotator").slideDown(1000).animate({
    //        marginTop: Wheight,
    //    }, 500, function () {
    //    });
});
$(document).ajaxStop(function () {
    //var Wheight = $(window).height() / 2 - $(".Rotator").height() / 2 + "px";
    //$(".Rotator").slideDown(1000).animate({
    //    marginTop: Wheight,
    //}, 500, function () {
    //});
    $("#LoadingDiv").delay(50).hide(0);
});
//---------------------------------------------------------------------
function getAllDataInfoAboutBatchingTeam()
{
    //var urlgetMyBatchingCount = "/Batching/getMyBatchingCount";
    //Common.Ajax("get", urlgetMyBatchingCount, null, 'json', sucessgetMyBatchingCount);
    //var urlgetBatchingCreatedCount = "/Batching/getBatchingCreatedCount";
    //Common.Ajax("get", urlgetBatchingCreatedCount, null, 'json', sucessgetBatchingCreatedCount);
    //var urlgetAllPendingBatchingRequestsCount = "/Batching/getAllPendingBatchingRequestsAtFilitrationBatchingCount";
    //Common.Ajax("get", urlgetAllPendingBatchingRequestsCount, null, 'json', sucessgetAllPendingBatchingRequestsCount);
    //var urlgetAllUnderPatchingProcessRequestsCount = "/Batching/getAllUnderPatchingProcessRequestsAtFilitrationBatchingCount";
    //Common.Ajax("get", urlgetAllUnderPatchingProcessRequestsCount, null, 'json', sucessgetAllUnderPatchingProcessRequestsCount);
    //var urlgetAllFinishedBatchingRequestsCount = "/Batching/getAllFinishedBatchingRequestsAtFilitrationBatchingCount";
    //Common.Ajax("get", urlgetAllFinishedBatchingRequestsCount, null, 'json', sucessgetAllFinishedBatchingRequestsCount);
    //var urlgetAllFilitrationBatchingRequestsCount = "/Batching/getAllFilitrationBatchingRequestsCount";
    //Common.Ajax("get", urlgetAllFilitrationBatchingRequestsCount, null, 'json', sucessgetAllFilitrationBatchingRequestsCount);

}
function sucessgetMyBatchingCount(Res)
{
    if (Res.Success)
    {
        $("#myBatchingCount").text(Res.Data);
    }
}
function sucessgetBatchingCreatedCount(Res) {
    if (Res.Success) {
        $("#BatchingCreatedCount").text(Res.Data);

    }
}
function sucessgetAllPendingBatchingRequestsCount(Res) {
    if (Res.Success) {
        $("#BatchingPendingCount").text(Res.Data);

    }
}
function sucessgetAllUnderPatchingProcessRequestsCount(Res) {
    if (Res.Success) {
        $("#BatchingUnderPatchingProcessCount").text(Res.Data);

    }
}
function sucessgetAllFinishedBatchingRequestsCount(Res) {
    if (Res.Success) {
        $("#BatchingFinishedCount").text(Res.Data);

    }
}
function sucessgetAllFilitrationBatchingRequestsCount(Res) {
    if (Res.Success) {
        $("#BatchingAllCount").text(Res.Data);

    }
}
//---------------------------------------------------------------------
// Alert Div types : info=1,success=2,warning=3,danger=4
function HideALer(Type) {
    if (Type === 1) {
        $("#ShowALertInfo").hide();
    }
    else if (Type === 2) {
        $("#ShowALertSuccess").hide();
    }
    else if (Type === 3) {
        $("#ShowALertWarning").hide();
    }
    else if (Type === 4) {
        $("#ShowALertDanger").hide();
    }
}
// Alert Div types : info=1,success=2,warning=3,danger=4
function ShowALert(Type, AlertText) {
    if (Type === 1) {
        $("#AlertInfoText").html(AlertText);
        $("#ShowALertInfo").show();
    }
    else if (Type === 2) {
        $("#AlertSuccessText").html(AlertText);
        $("#ShowALertSuccess").show();
    }
    else if (Type === 3) {
        $("#AlertWarningText").html(AlertText);
        $("#ShowALertWarning").show();
    }
    else if (Type === 4) {
        $("#AlertDangerText").html(AlertText);
        $("#ShowALertDanger").show();
    }
}
//----------------------------------------------------------------------
// active and expended link at sider
function ActiveNavigationBarCell() {
    var path;
    if (window.location.href.includes('?'))
    {
        path = window.location.pathname + '?' + window.location.href.split("?")[1];
    }
    else
    {
        path = window.location.pathname;
    }
    $(".nav a").each(function () {
       var href = $(this).attr('href');
       if (path == href) {
            $(this).closest('.nav-parent').addClass('nav-active nav-expanded'); //#fafafa
            $(this).closest('li').addClass('nav-active'); //#fafafa
        }

    });
}
//----------------------------------------------------------------------


