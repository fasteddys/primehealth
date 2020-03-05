var LeaveTypes = [];

$(document).ready(function () {

    GetAllLeaveType();


});

function GetAllLeaveType() {
    var urlAllLeaveTypes = ConfigurationData.GlobalApiURL + "/LeaveType/GetAllLeaveTypes";
    Common.Ajax('get', urlAllLeaveTypes, null, 'json', SucessGetLeaves, false);
}

function SucessGetLeaves(Leaves) {
    LeaveTypes = Leaves;
    for (var i = 0; i < LeaveTypes.Data.length; i++) {
        $("#LeavesType").append('<tr class="clickable-row"><td>' + LeaveTypes.Data[i].LeaveTypeName +'</td></tr>');

    }


}