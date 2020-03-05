
var RequestID = $("#RequestIDHidden").val();


$("#AssignRequest").click(function () {
    alert("hobaaaaa");
})

//$("#AssignRequest").click(function () {
//    alert("a7eeeeeeh");    //$.ajax({
//        type: "POST", url: 'request/AssignRequest?id='+RequestID,
//        dataType: 'json',
//        success: function (result) {
//            document.getElementById("Success-Div-Text").innerHTML = result
//            $('#Success-Div').show();
//            $("html, body").animate({ scrollTop: 0 }, "slow");
//            UpdatePartial();
//            alert(result);
//        }
//    });
//})
//$("#submitComment").click(function () {
//    debugger;
//    var Comment = $("#comment").val();
//    var iD = RequestID;
//    var data = {
//        RequestComment: Comment,
//        ID: iD
//    }
//    $.ajax({
//        type: "POST", url: 'request/SubmitComment',
//        data: data, dataType: 'json',
//        success: function (result) {
//            document.getElementById("Success-Div-Text").innerHTML = result
//            $('#Success-Div').show();
//            $("html, body").animate({ scrollTop: 0 }, "slow");
//            UpdatePartial();
//            alert(result);
//        }
//    });
//})