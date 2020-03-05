var NewPositionName;



$(document).ready(function () {
    GetAllPositions();
});


function GetAllPositions() {
    var urlGetAllPosition = ConfigurationData.GlobalApiURL+"/Position/GetAllPositions";
    Common.Ajax('get', urlGetAllPosition, null, 'json', SucessGetAllPositions, false);
}
function SucessGetAllPositions(Positions) {
    $('#dataTablePosition').DataTable().destroy();
    $('#dataTablePosition').DataTable({
        retrieve: true,
        "data": Positions.Data,
        "columns": [
            { "data": "PositionName" },//1
            { "data": "UsersCount" },//1
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data) {
                    return '<button id="showUser" class="btn btn-info btn-sm" onclick="GetAllPersonsByPositionID(' + data.PositionID + ')">' + 'Display Users' + '</button>';
                }

            }
        ],
    })
    //var TablePosition = $("#PositionListTable");
    //for (var i = 0; i < Positions.Data.length; i++) {
    //    TablePosition.append('<tr class="clickable-row" onclick="GetAllPersonsByPositionID(' + Positions.Data[i].PositionID + ')"><td>' + Positions.Data[i].PositionName + '</td><td>' + Positions.Data[i].UsersCount + '</td></tr>');
    //}
}
function GetAllPersonsByPositionID(ID) {
    var urlGetPositionByID = ConfigurationData.GlobalApiURL+"/Position/GetPositionByID?PositionID=" + ID;
    Common.Ajax('get', urlGetPositionByID, null, 'json', SucessGetPersonsByPositionID, false);
}
function SucessGetPersonsByPositionID(Persons) {
    debugger;
    var urlGetPositionByID = ConfigurationData.GlobalApiURL+"/Position/GetPersonsByPositionID?PositionID=" + Persons.Data.PositionID;
    Common.Ajax('get', urlGetPositionByID, null, 'json', SucessGetPersonsByPosition, false);
}
function SucessGetPersonsByPosition(UsersDetails) {
    $('#dataTablePositionUsers').DataTable().destroy();
    $('#dataTablePositionUsers').DataTable({
        retrieve: true,
        "data": UsersDetails.Data,
        "columns": [
            { "data": "UserName" },//1

        ],
    })
    //var TableUsersListTable = $("#UsersListTable");
    //TableUsersListTable.empty();
    //for (var i = 0; i < UsersDetails.Data.length; i++) {

    //    TableUsersListTable.append('<tr class="clickable-row" onclick="GetAllPersonsByPositionID(' + UsersDetails.Data[i].PositionID + ')"><td>' + UsersDetails.Data[i].UserName + '</td></tr>');
    //}
}
var SavePositionBTN = function () {

    NewPositionName = $("#AddPosition").val();

    if (NewPositionName !== "") {
        var NewPosition = {
            PositionName: NewPositionName,
        }
        var newPO = NewPosition;
        var urlGetNewPosition = ConfigurationData.GlobalApiURL+"/Position/AddNewPosition";
        Common.Ajax('Post', urlGetNewPosition, JSON.stringify(newPO), 'json', SucessAddNewPosition, false); 
      
    }

}

function SucessAddNewPosition(Result) {
    if (Result.Success === true) {
        $("#AddPositionModal").modal("hide");
        ShowALert(2, Result.Message);
        GetAllPositions();
    }
    else {
        ShowALert(4, Result.Message);

    }
   
}
