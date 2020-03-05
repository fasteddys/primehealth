$(document).ready(function () {
    GetCountriesCodes();
    
    var urlGetAllClientVisits = GlobalResourses.BaseURL + "ClientVisits/GetAllClientVisits?ClientID=" + (JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientID);
    Common.Ajax('get', urlGetAllClientVisits, null, 'json', SucessGetAllClientVisits, false);

   SucessGetAllCountries();

   $('#Welcome').click(function () {
       CloseSideMenu();

        if (JSON.parse(localStorage.getItem("UserData")) !== null) {
            //window.location.href = ("TripsListing.html");
            ShowModal.show("ModelEdit");
            //$('.sidebar-left').hide();

            var profileData = JSON.parse(localStorage.getItem("UserData"));
            var CountryCodePhoneNum = profileData.ClientPhone.split('-');
            $('#EditUserName').val(profileData.ClientName);
            $('#EditPhoneNumberCode').val(CountryCodePhoneNum[0]).change();
            $('#EditPhone').val(CountryCodePhoneNum[1]);
            $('#EditEmail').val(profileData.ClientEmail);
            $('#EditPassport').val(profileData.ClientPassport);
            $('#EditCountry').val(profileData.CountryID).change();
        }
            
    });

    $('#Edit').click(function () {
        var ClientData = {
            ClientID: JSON.parse(localStorage.getItem("UserData")).ClientID,
            LoggedUser: JSON.parse(localStorage.getItem("UserData")).ClientID,
            ClientName: $('#EditUserName').val(),
            ClientEmail: $('#EditEmail').val(),
            ClientPhone: $('#EditPhoneNumberCode').val() + '-' + $('#EditPhone').val(),
            ClientPassport: $('#EditPassport').val(),
            CountryID: $('#EditCountry').val()
        };

        var urlEditClientData = GlobalResourses.BaseURL + "Client/EditClientData";
        Common.Ajax('Post', urlEditClientData, JSON.stringify(ClientData), 'json', SuccessEditClientData, false);
    });

});

function SuccessEditClientData(Result) {
    if (Result.Success === true) {
        var CurrentClientData = JSON.parse(localStorage.getItem("UserData"));
        var NewClientData = {
            ClientDeviceID: CurrentClientData.ClientDeviceID,
            ClientEmail: Result.Data.ClientEmail,
            ClientID: Result.Data.ClientID,
            ClientName: Result.Data.ClientName,
            ClientPassport: Result.Data.ClientPassport,
            ClientPhone: Result.Data.ClientPhone,
            CountryID: Result.Data.CountryID,
            HotelName: CurrentClientData.HotelName,
            NotificationMethodID: CurrentClientData.NotificationMethodID,
            LoggedUser: CurrentClientData.ClientID
        };
        localStorage.setItem("UserData", JSON.stringify(NewClientData));
        window.location.reload();
    }        
}

function SucessGetAllCountries() {
    var Result = Common.GetALLCountries();
    $('#EditCountry').select2();
    for (var i = 0; i < Result.length; i++) {
        $('#EditCountry').append('<option value="'+Result[i].CountryID +'">'+Result[i].CountryName +'</option>');
    }

}

function SucessGetAllClientVisits(Result) {
    for (var i = 0; i < Result.Data.length; i++)
        $('#ClientVisits').append('<a class="user-list-2" onclick="GetVisitsDetails(' +
            Result.Data[i].VisitID +
            ')" ><div><img data-original="images/pictures/1t.jpg" alt="img" class="preload-image"><strong>' +
            Result.Data[i].LocationName + ' ,' +
            Result.Data[i].CountryName + '</strong><em><i class="fa fa-clock-o"></i><span>From:'
            + moment(Result.Data[i].StartVisitDate).format('DD/MM/YYYY')
            + '</span><span> | To:' +
            moment(Result.Data[i].EndVisitDate).format('DD/MM/YYYY')
             +
        '</span></em><i class="fa fa-angle-right pull-right" style="margin-top:5%;color:white !important"></i></div></a>');
    
}
function GetVisitsDetails(VisitID) {

    var urlGetClientVisitDetails = GlobalResourses.BaseURL + "ClientVisits/GetClientVisitDetails?ClientVisitID=" + VisitID;
    Common.Ajax('get', urlGetClientVisitDetails, null, 'json', SucessGetClientVisitDetails, false);
}
function SucessGetClientVisitDetails(Result) {
    var x = moment(Result.Data.StartVisitDate).format('DD/MM/YYYY');

    localStorage.setItem("VisitID", Result.Data.VisitID);
    localStorage.setItem("CountryName", Result.Data.CountryName);
    localStorage.setItem("LocationName", Result.Data.LocationName);
    localStorage.setItem("HotelName", Result.Data.HotelName);
    localStorage.setItem("ClientName", Result.Data.ClientName);
    localStorage.setItem("EndVisitDate", moment(Result.Data.EndVisitDate).format('DD/MM/YYYY') );
    localStorage.setItem("StartVisitDate", moment(Result.Data.StartVisitDate).format('DD/MM/YYYY'));
    localStorage.setItem("ListTrips", JSON.stringify(Result.Data.ListTrips));
    //var dirPath = dirname(location.href);
    //fullPath = dirPath + "/PreviousVisitDetails.html";
    //window.location.href = fullPath;
    // window.open('PreviousVisitDetails.html', '_self', 'location=yes', true);
   // window.location.href = "http://www.w3schools.com", true;
    window.location.href = ("PreviousVisitDetails.html");

    //window.location = "PreviousVisitDetails.html";
}

function dirname(path) {
    return path.replace(/\\/g, '/').replace(/\/[^\/]*$/, '');
}

function GetCountriesCodes() {
   
  SucessGetAllCountriesCodes();
}

function SucessGetAllCountriesCodes() {
    var Result = Common.GetALLCountries();
    $('#EditPhoneNumberCode').select2();
    for (var i = 0; i < Result.length; i++) {
        $('#EditPhoneNumberCode').append('<option value="' + Result[i].CountryCode + '">' + Result[i].CountryCode + '</option>');
    }
}