var Status = {};


$(document).ready(function () {
    GetServiceStatus();
});
function StartService(Service) {
    var ServiceName = Service.attributes.servicedname.value ;
    var urlGetStartService = ConfigurationData.GlobalApiURL + "/ControllWindowsService/StartService?ServiceName=" + ServiceName;
    Common.Ajax('get', urlGetStartService, null, 'json', SucessGetStartService, false);
};
function SucessGetStartService() {
    location.reload();
}
function StopService(Service) {
    var ServiceName = Service.attributes.servicedname.value;
    var urlGetStopService = ConfigurationData.GlobalApiURL + "/ControllWindowsService/StopService?ServiceName=" + ServiceName;
    Common.Ajax('get', urlGetStopService, ServiceName, 'json', SucessGetStopService, false);
};
function SucessGetStopService() {
    location.reload();
}
function GetServiceStatus() {
    var urlGetGETSTATUS = ConfigurationData.GlobalApiURL + "/ControllWindowsService/GEtAllServices";
    Common.Ajax('post', urlGetGETSTATUS, null, 'json', SucessGetGETSTATUS, false);
}
function SucessGetGETSTATUS(StatusData) {

    var TableService = $("#Table");

    for (var i = 0; i < StatusData.Data.length; i++) {
        var ButtonsHTML = "";
        if (StatusData.Data[i].ServiceStatusID === 1) {
            ButtonsHTML = '<input class="btn btn-success" id="StartButton" ServiceDName="' + StatusData.Data[i].ServiceName + '" type="submit" value="Start" onclick="StartService(this)" style="font-size:17px;padding:7px 65px;" />';
        }

        if (StatusData.Data[i].ServiceStatusID === 4 || StatusData.Data[i].ServiceStatusID === 2) {
            ButtonsHTML = '<input class="btn btn-danger" id="StopButton" ServiceDName="' + StatusData.Data[i].ServiceName +'" type="submit" value="Stop" onclick="StopService(this)" style="font-size:17px;padding:7px 65px;" />';
        }
       
        TableService.append('<tr class="clickable-row"><td>' + StatusData.Data[i].ServiceName + '</td>' +
            '<td>' + StatusData.Data[i].ServiceStatus + '</td>' +
            '<td>' +
        ButtonsHTML+
            '</td >'+
            '</tr >'
);
    }
}
 




