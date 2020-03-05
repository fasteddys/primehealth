$(document).ready(function () {
    GetVisitData();
    GetCountriesCodes();
    $("#VisitNo").html(localStorage.getItem("VisitID"));
    $("#VisiterName").html(localStorage.getItem("ClientName"));
    $("#HotelName").html(localStorage.getItem("HotelName"));
    $("#CountryName").html(' ,' + localStorage.getItem("CountryName"));
    $("#LocationName").html(localStorage.getItem("LocationName"));
    $("#StartVisitDate").html(localStorage.getItem("StartVisitDate"));
    $("#EndVisitDate").html(localStorage.getItem("EndVisitDate"));
    var ListTrips = JSON.parse( localStorage.getItem("ListTrips"));
    for (var i = 0; i < ListTrips.length; i++)
        {

        $("#ListTrips").append(
            
            '<div class="checklist-item  one-half-responsive"><em style="padding-left: 5px !important;padding-left: 5px !important;"><span onclick="EditTrip(' + ListTrips[i].ClientTripRequestID +')"><i class="fa fa-edit" style="font-size:15px;color:blue"></i></span></em><em style="padding-left: 5px !important;padding-right: 5px !important;">'
            + moment(ListTrips[i].ClientDesiredTripDate).format('DD MMM')

            
            + '<br><span>' + moment(ListTrips[i].ClientDesiredTripDate).format('YYYY') + '</span></em > <strong>'
            + ListTrips[i].TripName + '</strong><em class="pull-right" style="padding-left: 5px !important;padding-right: 5px !important;">'
            + ListTrips[i].NumberOfAdults
            + '<br><span>Adults</span></em><em class="pull-right" style="padding-left: 5px !important;padding-right: 5px !important;">'
            + ListTrips[i].NumberOfChildren
            + '<br><span>Childs</span></em></div>'
            + ''
        );

        
        
    }
    //disableBackButton.Disable();

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
        var CurrentClientData = JSON.parse(localStorage.getItem("UserData"));
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

    $('#EditRequestData').click(function () {
        var trip = {
            ClientDesiredTripDate: $('#DesiredDate').val(),
            NumberOfAdults: $('#NumberOfAudlt').val(),
            NumberOfChildren: $('#NumberOfChildren').val(),
            ClientNotes: $('#ClientNotes').val(),
            ClientTripRequestID: $('#TripRequestID').val(),
            LoggedUser: CurrentClientData.ClientID
        };

        
            var urlEditTripRequest = GlobalResourses.BaseURL + "ClientTripRequests/EditTripRequest";
            Common.Ajax('Post', urlEditTripRequest, JSON.stringify(trip), 'json', SuccessEditTripRequest, false);
        
        
    });

    $('#EditVisit').click(function () {
        var CurrentVisit = JSON.parse(localStorage.getItem("CurrentVisit")); 
        if (GetCurrentDate() > moment(CurrentVisit.EndVisitDate).format('DD/MM/YYYY')) {
            Alert.Show("You Can't Edit Expired Visit");
        }
        else {
            ShowModal.show("EditVisitDatesModal");
            GetVisitData();
            GetHotels();
        }
        
    });

    $('#EditVisitFromModal').click(function () {
        var VisitData = {
            VisitID: JSON.parse(localStorage.getItem("CurrentVisit")).CurrentVisitID,
            StartVisitDate: $('#StartDate').val(),
            EndVisitDate: $('#EndDate').val(),
            HotelID: $('#HotelID').val(),
            LoggedUser: JSON.parse(localStorage.getItem("UserData")).ClientID
        };

        var urlEditVisit = GlobalResourses.BaseURL + "ClientVisits/EditVisit";
        Common.Ajax('Post', urlEditVisit, JSON.stringify(VisitData), 'json', SuccessEditVisit, false);
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
            NotificationMethodID: CurrentClientData.NotificationMethodID
        };
        localStorage.setItem("UserData", JSON.stringify(NewClientData));
        window.location.reload();
    }        
}

function SuccessEditVisit(Result) {
    if (Result.Success === true) {
        var CurrentVisit = JSON.parse(localStorage.getItem("CurrentVisit"));

        var NewVisitData = {
            CountryID: CurrentVisit.CountryID,
            LocationID: CurrentVisit.LocationID,
            CurrentVisitID: CurrentVisit.CurrentVisitID,
            HotelID: Result.Data.HotelID,
            HotelName: Result.Data.HotelName,
            StartVisitDate: moment(Result.Data.StartVisitDate).format('YYYY-MM-DD'),
            EndVisitDate: moment(Result.Data.EndVisitDate).format('YYYY-MM-DD')
        };
        localStorage.setItem("CurrentVisit", JSON.stringify(NewVisitData));
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

function EditTrip(Obj) {

    var ListTrips = JSON.parse(localStorage.getItem("ListTrips"));
    for (var i = 0; i < ListTrips.length; i++) {
        if (ListTrips[i].ClientTripRequestID === Obj) {
            var ClientDesiredTripDate = ListTrips[i].ClientDesiredTripDate;
            $('#DesiredDate').val(moment(ClientDesiredTripDate).format('YYYY-MM-DD'));
            $('#NumberOfChildren').val(ListTrips[i].NumberOfChildren);
            $('#NumberOfAudlt').val(ListTrips[i].NumberOfAdults);
            $('#ClientNotes').val(ListTrips[i].ClientNotes);
            $('#TripRequestID').val(ListTrips[i].ClientTripRequestID);
        }
    }

    if (GetCurrentDate() > moment(ClientDesiredTripDate).format('DD/MM/YYYY')) {
        Alert.Show("You Can't Edit Expired Trip");
    }
    else {
        //var d = $('#TripRequestID').val();
        ShowModal.show("ModalShowBookDetails");
    }
}

function SuccessEditTripRequest(Result) {
    var ListTrips = JSON.parse(localStorage.getItem("ListTrips"));
    for (var i = 0; i < ListTrips.length; i++) {
        if (ListTrips[i].ClientTripRequestID === Result.Data.ClientTripRequestID) {
            ListTrips[i].ClientDesiredTripDate = Result.Data.ClientDesiredTripDate;
            ListTrips[i].NumberOfAdults = Result.Data.NumberOfAdults;
            ListTrips[i].NumberOfChildren = Result.Data.NumberOfChildren;
            ListTrips[i].ClientNotes = Result.Data.ClientNotes;
            break;
        }
    }
    localStorage.setItem("ListTrips", JSON.stringify(ListTrips));
    window.location.reload();
}

function GetHotels() {
    var urlGetAllOperatingCountries = GlobalResourses.BaseURL + "hotel/SearchHotel?LanguageFK=" + localStorage.getItem("LanguageID")
        + "&OperatingLocation=" + localStorage.getItem("VisitLocationID") + "& OperatingCountry=" + localStorage.getItem("VisitCountryID");
    Common.Ajax('get', urlGetAllOperatingCountries, null, 'json', SucessGetAllHotels, false);
}
function SucessGetAllHotels(Result) {
    $('#HotelID').select2();
    for (var i = 0; i < Result.Data.length; i++) {
        $('#HotelID').append('<option value="' + Result.Data[i].HotelID + '">' + Result.Data[i].Name + '</option>');
    }
    $('#HotelID').append('<option value="-1">Other</option>');

    $('#HotelID').change(function () {
        if ($('#HotelID').val() == "-1") {
            $("#HotelNameDIv").show();
            $("#HotelName").addClass('IsRequirdField');
        }
        else {
            $("#HotelNameDIv").hide();
            $("#HotelName").removeClass('IsRequirdField');

        }
    });

}

function GetVisitData() {
    var CurrentVisit = JSON.parse(localStorage.getItem("CurrentVisit"));

    $('#StartDate').val(moment(CurrentVisit.StartVisitDate).format('YYYY-MM-DD'));
    $('#EndDate').val(moment(CurrentVisit.EndVisitDate).format('YYYY-MM-DD'));
    $('#HotelID').val(HotelID);
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

function GetCurrentDate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    return dd + '/' + mm + '/' + yyyy;
}