$(document).ready(function () {
    GetAllApprovalFlow();
});

//get ApprovalFlow Data
function GetAllApprovalFlow() {
    var urlGetAllApprovalFlow = ConfigurationData.GlobalApiURL+"/ApprovalFlow/GetAllApprovalFlow";
    Common.Ajax('get', urlGetAllApprovalFlow, null, 'json', SucessGetAllApprovalFlow, false);
}
function SucessGetAllApprovalFlow(Approval) {
    var Table = $("#ApprovalFlowTable");
    for (var i = 0; i < Approval.Data.length; i++) {
        Table.append('<tr class="clickable-row" onclick="GetLApprovalFlowByID(' + Approval.Data[i].ApprovalFlowID + ')"><td>' + Approval.Data[i].ApprovalFlowName + '</td><td>' + Approval.Data[i].UsersCount + '</td></tr>');
    }
}
function GetLApprovalFlowByID(ID) {
    var urlGetLApprovalFlowByID = ConfigurationData.GlobalApiURL+"/ApprovalFlow/GetApprovalFlowByID?FlowID=" + ID;
    Common.Ajax('get', urlGetLApprovalFlowByID, null, 'json', SucessGetLApprovalFlowByID, false);
}


//function SucessGetLApprovalFlowByID(Details) {
//    $("#FlowName").text(Details.Data.ApprovalFlowName);
//    $("#flowDetails_flowsContainer").empty();
//    ApprovalFlow = "<fieldset class='formGroup'>";
//    for (var i = 0; i < Details.Data.ApprovalFlow.length; i++) {
//        $("#flowDetails_flowsContainer").append("<h5 class='alternativeLegend'>Default approval steps</h5><div  class='flowAlternative_content' ><div><div class='FlowPresentation hiddenStatusColumn' id='Layout'></div></div></div>");

//     var Count = Details.Data.ApprovalFlow[i].Steps.length-1;
                     
//     for (var j = 0; j < Details.Data.ApprovalFlow[i].Steps[Count].Order; j++)
//     {
//         ApprovalFlow =ApprovalFlow+ "<div class='FlowPresentation_Step'><div class='FlowPresentation_State'></div><div class='FlowPresentation_Indicator'></div><div class='FlowPresentation_Number'>" + (j + 1) + "</div>"

//         var SameOrderUsers=[];
//         for (var o = 0; o < Details.Data.ApprovalFlow[i].Steps.length; o++)
//         {
//             if (j == Details.Data.ApprovalFlow[i].Steps[o].Order-1) {
//                 SameOrderUsers.push(Details.Data.ApprovalFlow[i].Steps[o]);
//             }

//         }

//         for (var s = 0; s < SameOrderUsers.length; s++)
//         {
//             var Step = "";
//             if (-1 === SameOrderUsers[s].UserID) {
//                 Step = "<div class='FlowPresentation_Desc'>" + "Direct Manager" + "</div>";
//                 ApprovalFlow = ApprovalFlow + Step;
//             }
//             else if (-2 === SameOrderUsers[s].UserID) {
//                 Step = "<div class='FlowPresentation_Desc'>" + "Team Manager" + "</div>";
//                 ApprovalFlow = ApprovalFlow + Step;
//             }
//             else {

//                 Step = "<div class='FlowPresentation_Desc'>" + SameOrderUsers[s].UserName + "</div>";
//                 ApprovalFlow = ApprovalFlow + Step;
//             }

//         }

//         ApprovalFlow = ApprovalFlow + "<div class='FlowPresentation_NextArrow'></div></div></div><div class='FlowPresentation_Clear'>&nbsp;</div>";

//        }

//     ApprovalFlow = ApprovalFlow + "</fieldset>"

//       $("#Layout").append(ApprovalFlow);

//    }

//}
