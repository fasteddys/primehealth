$(document).ready(LoadPage());
function LoadPage() {

    var url = ConfigurationData.GlobalApiURL+"/TimeAttendance/getAttendanceByUserID";
    Common.Ajax('get', url, null, 'json', success);
    function success(data) {

       // var table = document.getElementById("AttindanceTable");

        for(var i=0;i<data.length ;i++ )
        {
            var row = '<tr><td>' + data[i].Date + '</td>' + '<td>' + data[i].fingerprintIn + '</td>' + '<td>' + data[i].fingerprintOut + '</td></tr>'


        }
}