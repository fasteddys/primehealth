var FirstSearch = true;
$(document).ready(function () {
    GetAllEntities();

    $("#RequestsUnLockedEntites").change(function () {
        GetStatusForEntity(this.value);
    });

    $("#btngetAllUnlockedRequest").click(function () {
        DoSearch();
    });

});

function GetAllEntities() {
    var urlGetAllEntities = '/Entities/GetEntitesForLock';
    Common.Ajax("GET", urlGetAllEntities, null, 'json', SucessGetAllEntities);
}

function SucessGetAllEntities(Result) {
    if (Result.Success) {
        for (var i = 0; i < Result.Data.length; i++) {
            $('#RequestsUnLockedEntites').append(new Option(Result.Data[i].SystemEntityName, Result.Data[i].SystemEntityID));
            //$('#RequestsUnLockedEntites').append("<option value='" + Result.Data[i].SystemEntityID + "'>" + Result.Data[i].SystemEntityName + "</option>");
        }
    }
}

function GetStatusForEntity(ID) {
    var urlGetStatusForEntity = '/Entities/GetStatusOfEntity?EntityID=' + ID;
    Common.Ajax("GET", urlGetStatusForEntity, null, 'json', SucessGetStatusForEntity);
}

function SucessGetStatusForEntity(Result) {
    $('#RequestsUnLockedStatus').empty();
    if (Result.Success) {
        for (var i = 0; i < Result.Data.length; i++) {
            $('#RequestsUnLockedStatus').append(new Option(Result.Data[i].StatusName, Result.Data[i].StatusID));
        }
    }
}

function DoSearch() {
    var urlSearch = "";
    var Search = {
        FieldsNames: '',
        StartDate: $('#RequestsUnLockedFrom').val(),
        EndDate: $('#RequestsUnLockedTo').val(),
        TableID: $('#RequestsUnLockedEntites').val(),
        StatusID: $('#RequestsUnLockedStatus').val()
    };

    if (Search.TableID == Common.Entites.ReceivingRequest) {
        Search.FieldsNames = 'ReceivingRequestID,BilllingMonth,BillingYear,ReceivedClaimsCount';
        urlSearch = '/Provider/GetRecievingRequestsByStatus';
    }
    else if (Search.TableID == Common.Entites.BatchRequest) {
        Search.FieldsNames = 'BatchingRequestID,NumberOfBatchClaims,BatchSystemFK,BatchNumber,CreationDate';
        urlSearch = '/Provider/GetBatchRequestsByStatus';
    }
    else if (Search.TableID == Common.Entites.BatchingFilterationDetails) {
        Search.FieldsNames = 'BatchingFilterationDetailID,TotalClaimsCount,FilterationCategoryFK';
        urlSearch = '/Provider/GetFilterationRequestsByStatus';
    }
    else if (Search.TableID == Common.Entites.AuditRequest) {
        Search.FieldsNames = 'BatchAuditingRequestID,BatchingRequestFK,BatchAuditingStatusFK,TotalNumberOfApprovedClaims,TotalNumberOfRejectedClaims';
        urlSearch = '/Provider/GetBatchAuditRequestByStatus';
    }
    else if (Search.TableID == Common.Entites.MedicalAuditRequest) {
        Search.FieldsNames = 'MedicalAuditRequestID,BatchAuditRequestFK,MedicalOfficerAssigneeFK,NumberOfApprovedClaims,NumberOfPendingClaims,NumberOfRejectedClaims';
        urlSearch = '/Provider/GetMedicalAuditRequestByStatus';
    }
    else if (Search.TableID == Common.Entites.FinancialAuditRequest) {
        Search.FieldsNames = 'FinancialAuditRequestID,BatchAuditRequestFK,FinancialAuditAssigneeFK,NumberOfApprovedClaims,NumberOfPendingClaims,NumberOfRejectedClaims';
        urlSearch = '/Provider/GetFinancialAuditRequestByStatus';
    }
    else if (Search.TableID == Common.Entites.MIAuditRequest) {
        Search.FieldsNames = 'MIAuditRequestID,BatchAuditRequestFK,MIOfficerAssigneeFK,NumberOfApprovedClaims,NumberOfPendingClaims,NumberOfRejectedClaims';
        urlSearch = '/Provider/GetMIAuditRequestByStatus';
    }
    else if (Search.TableID == Common.Entites.ReconciliationAuditRequest) {
        Search.FieldsNames = 'ReconciliationAuditRequestID,BatchAuditRequestFK,ReconciliationOfficerFK,NumberOfApprovedClaims,NumberOfPendingClaims,NumberOfRejectedClaims';
        urlSearch = '/Provider/GetReconciliationAuditRequestByStatus';
    }
    else if (Search.TableID == Common.Entites.DataEntryAdminstrationRequest) {
        Search.FieldsNames = 'DataEntryAdminstrationRequestID,BatchRequestFK,DataEntryAdminAssigneeFK,OriginalApprovedClaimsNumber';
        urlSearch = '/Provider/GetDataEntryAdminstrationRequestByStatus';
    }
    else if (Search.TableID == Common.Entites.DataEntryBatchAssigningRequest) {
        Search.FieldsNames = 'DataEntryBatchAssigningRequestID,DataEntryAdministrationRequestFK,DataEntryOfficerFK,AssignedClaimsNumber';
        urlSearch = '/Provider/GetDataEntryBatchAssigningRequestByStatus';
    }
    else if (Search.TableID == Common.Entites.DataEntryClosingRequest) {
        Search.FieldsNames = 'BatchClosingRequestID,DataEntryAdminstrationRequestFK,ClosingOfficerAssigneeFK';
        urlSearch = '/Provider/GetBatchClosingRequestByStatus';
    }
    //else if (Search.TableID == Common.Entites.ClosedBatchReAdministrationRequest) {
    //    Search.FieldsNames = 'BatchClosingRequestID,DataEntryAdminstrationRequestFK,ClosingOfficerAssigneeFK';
    //    urlSearch = '/Provider/GetBatchClosingRequestByStatus';
    //}
    else if (Search.TableID == Common.Entites.ClosedBatchReAdministrationRequest) {
        Search.FieldsNames = 'ClosedBatchReAdmissionRequestID,BatchClosingRequestFK,AssignedByAdminFK';
        urlSearch = '/Provider/GetClosedBatchReAdministrationRequestByStatus';
    }

    if (Search.StartDate !== '' && Search.EndDate !== '' && Search.TableID !== '')
        Common.Ajax('POST', urlSearch, JSON.stringify(Search), 'json', SucessGetSearchResult);
    else
        ShowALert(1, 'Please Enter Required Fields');
}

function SucessGetSearchResult(Result) {
    //$('#SearchResultTable').DataTable().destroy();
    //$('#SearchResultTable').empty();
    //$('#SearchResultTable').empty();
    if (Result.Success) {
        //if ($('#RequestsUnLockedEntites').val() == Common.Entites.ReceivingRequest) {
        if (!FirstSearch) {

            $('#SearchResultTable').DataTable().destroy();
        }
        else { FirstSearch = false; }
        //if ($.fn.DataTable.isDataTable('#SearchResultTable')) {
        //    $('#SearchResultTable').DataTable().destroy();
        //    //$('#SearchResultTable').remove();
        //}
        //var Columns = [];
        var Columns = [];

        var TableHeader = "<thead></thead>";
        $.each(Result.Data[0], function (key, value) {
            Columns.push({
                "data": key,
                "title": key

            });
           // TableHeader += "<th>" + key + "</th>";
        });
        //$("#SearchResultTable").append(TableHeader);
        $('#SearchResultTable').DataTable({
            "data": Result.Data,
            "columns": Columns,
            "columnDefs": [{
                "defaultContent": "-",
                "targets": ""
            }],
            "scrollX": true,
            "scrollCollapse": true
        });

    


            //CreateTable(Columns, Result.Data)
            //$('#SearchResultTable').DataTable({
            //    data: Result.Data,
            //    columns: Columns,
            //    //bSort: false,
            //    //retrieve: true,
            //    //aoColumns: [{ sWidth: "20%" }, { sWidth: "20%" }, { sWidth: "20%" }, { sWidth: "20%" }, { sWidth: "20%" }],
            //    //"scrollY": "200px",
            //    //"scrollCollapse": true,
            //    //"info": true,
            //    //"paging": true,
            //    //"columnDefs": [{
            //    //    "defaultContent": "-",
            //    //    "targets": "_all"
            //    //}]
            //});
        //}
    } else {
        ShowALert(4, Result.Message);        
    }
}

function CreateTable(Col, Data) {
    var table = document.createElement("table");
    //$(table).dataTable();
    $(table).attr("class", "table table-bordered table-striped mb-0");
    $(table).DataTable({
        bSort: false, order: [1, 'asc'], aoColumns: [{ sWidth: "45%" }, { sWidth: "45%" }, { sWidth: "10%" }],
        "scrollY": "200px",
        "scrollCollapse": true,
        "info": true,
        "paging": true});

    var thread = document.createElement("thead");
    var tbody = document.createElement("tbody");

    var tr = table.insertRow(-1);                   // TABLE ROW.

    for (var c = 0; c < Col.length; c++) {
        var th = document.createElement("th");      // TABLE HEADER.
        th.innerHTML = Col[c];
        tr.appendChild(th);
        thread.appendChild(tr);
    }

    // ADD JSON DATA TO THE TABLE AS ROWS.
    for (var i = 0; i < Data.length; i++) {

        tr = table.insertRow(-1);

        for (var j = 0; j < Col.length; j++) {
            var tabCell = tr.insertCell(-1);
            tabCell.innerHTML = Data[i][Col[j]];
        }
    }

    // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
    var divContainer = document.getElementById("showData");
    divContainer.innerHTML = "";
    divContainer.appendChild(table);
}