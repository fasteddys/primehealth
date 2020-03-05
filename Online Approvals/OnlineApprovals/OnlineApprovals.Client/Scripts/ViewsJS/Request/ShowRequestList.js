//window.onload = function () {

//    var url = '';
//    var dataToPost = { ProviderID: 898 }


//    url = '/Request/GetListOfRequests'; // MVC Action Name


//    // For Post Method
//    var list = Common.Ajax('Post', url, dataToPost, 'json', successUserCreateHandler);
//}


////$(document).ready(function () {

////    var url = '';
////    var dataToPost = { ProviderID: 898 }


////    url = '/Request/GetListOfRequests'; // MVC Action Name


////    // For Post Method
////    var list = Common.Ajax('Post', url, dataToPost, 'json', successUserCreateHandler);
////}
////);


//function successUserCreateHandler(response) {


//        var Table = $("#TableBody");
//        for (var i = 0; i < response.length; i++) {

//            Table.append(
//                "<tr role='row' ><td class='sorting_1' >" + response[i].ID + "</td > <td><a href='#'>" + response[i].ClaimNumber +
//                "</a ></td > <td>" + response[i].CreationTime +
          
//                "</td> <td>" + response[i].CallCenterAssignee + "</td> <td><span class='label label-" + response[i].Color +
//                "'>"+response[i].Status+"</span></td> <td class='text-center'><ul class='icons-list'><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'><i class='icon-menu9'></i></a><ul class='dropdown-menu dropdown-menu-right'><li><a href='#'><i class='icon-file-pdf'></i> Export to .pdf</a></li><li><a href='#'><i class='icon-file-excel'></i> Export to .csv</a></li><li><a href='#'><i class='icon-file-word'></i> Export to .doc</a></li></ul></li></ul></td></tr > ");


//        }
      




  
//}

function ShowDetails(ID) {
    $("#PartialView1").empty();
    var url = '/Request/_ViewInvoice?ID=' + ID;
    Common.Ajax('Post', url, ID, 'html', success);



}
function success(data) {
    $('#PartialView1').append(data);



}