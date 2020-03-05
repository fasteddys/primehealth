$(document).ready(function () {
    GetCountriesCodes();

    var urlGetTripDetails = GlobalResourses.BaseURL + "Trips/GetTripData?TripID=" +
        localStorage.getItem("TripID") + "&languageID=" + localStorage.getItem("LanguageID");
    Common.Ajax('get', urlGetTripDetails, null, 'json', SucessGetTripDetails, false);


    $('#BooKTrip').click(function () {
        ShowModal.show("ModalShowBookDetails");


    });

    $('#OKButton').click(function () {
        window.location.href = ("MainTripScreen.html");
    });


    $('#SaveRequestData').click(function () {
        var RequestData = {
            ClientDesiredTripDate: $('#DesiredDate').val(),
            ClientNotes: $('#ClientNotes').val(),
            NumberOfAdults: $('#NumberOfAudlt').val(),
            NumberOfChildren: $('#NumberOfChildren').val(),
            ClientVisitID: JSON.parse(localStorage.getItem("CurrentVisit")).CurrentVisitID,
            ClientID: (JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientID),
            TripID: localStorage.getItem("TripID")
        };
          // document.getElementById('ClientNotes').validity.valid;
     if(ValidateRequirdData.IsValid())
{
        var urlSaveRequest = GlobalResourses.BaseURL + "ClientTrips/StartNewTrip";
        Common.Ajax('Post', urlSaveRequest, JSON.stringify(RequestData), 'json', SucessSaveRequest, false);

        } else { Alert.Show("Complete Trip Data"); }
     ShowModal.hide("ModalShowBookDetails");
    });

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

function SucessGetTripDetails(Result) {
    $("#LocationName").html(Result.Data.LocationName);
    $("#CountryName").html(Result.Data.CountryName);
    $("#TripName").html(Result.Data.TripName);
    //$("#TripShortDesc").html(Result.TripShortDesc);
    $("#TripDescription").html(Result.Data.TripDetailedDesc);
    
    $("#TripCountry").html(Result.Data.CountryName);
    $("#TripLocaton").html(Result.Data.LocationName);

    
    var PriceText = "";
    
    for (var j = 0; j < Result.Data.TripCurrency.length; j++) {
        if (i == Result.Data.TripCurrency.length - 1 && Result.Data.TripCurrency.length != 1) {
            PriceText = PriceText + Result.Data.TripCurrency[j].CurrencySign + Result.Data.TripCurrency[j].Price + ",";
        }
        else {

            PriceText = PriceText + Result.Data.TripCurrency[j].CurrencySign + Result.Data.TripCurrency[j].Price;

        }
    }



        $("#TripPrice").html(PriceText);
    for (var i = 0;i< Result.Data.Photos.length;i++) {
        $("#TripImages").append('<a href= "#" class="swiper-slide store-slider-item" data- swiper - slide - index="0" style= "width: 474px;">'
            +'<img class="responsive-image no-bottom" src= "' + Result.Data.Photos[i].PhotoPath+'" alt= "img" ></a > ');
    }


    //StartNewTrip
}
function SucessSaveRequest(Result) {
    if (localStorage.getItem("LanguageID") == '1')
        Alert.Show("شكرا لك لحجز رحله  (" + Result.Data.TripName + ")" +
            ", رقم حجزك هو #(" + Result.Data.ClientTripRequestID + "), سوف نتواصل معك في اقرب وقت");
        
    else if (localStorage.getItem("LanguageID") == '2')
        Alert.Show("Thank you for booking (" + Result.Data.TripName + ") trip" +
            ", your booking ref is #(" + Result.Data.ClientTripRequestID + "), Our Representative will Contact you soon");
    //window.location.href = ("MainTripScreen.html");

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


