$(document).ready(function () {
});

$("#GoAudit").click(function () {
  

    //var DirectBatchToAuditBageURL = ;
   // Common.Ajax('get', DirectBatchToAuditBageURL, 'json', SucessDirectBatchToAuditBage, false);


    var DirectBatchToAuditBageURL = "/Audit/DirectBatchToAuditBage?BatchID=12";
    Common.Ajax('get', DirectBatchToAuditBageURL, null, 'json', SucessDirectBatchToAuditBage, false);
});



function SucessDirectBatchToAuditBage(Result) {
    if (Result.Success === true) {
        window.location.href = "/" + Result.Data.AuditCategoriesControllerName + "/" +

            Result.Data.AuditCategoriesActionName;
            

    }
}