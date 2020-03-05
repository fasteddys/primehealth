
$(document).ready(function () {
    Loader.Show();
    GetCountriesCodes();
    $('.page-preloader').addClass('show-preloader');
    $('#page-content, .landing-page').removeClass('fadeIn');

    $("#ShowNumber").click(function () {
        ShowModal.hide("ModelSignUp");
        ShowModal.show("ModalEnterPhone");

    });
    $("#ConfirmPhoneNumber")
        .click(
        function () {
            var Client = {
                ClientPhone: $("#PhoneNumberCode").val() + '-' + $("#PhoneNumber").val()
            };
            var urlCheckIfClientSignUp = GlobalResourses.BaseURL + "Client/CheckIfClientSignUp";
            Common.Ajax('Post', urlCheckIfClientSignUp, JSON.stringify(Client), 'json', SucessSignUp, false);

        });
     SucessGetAllCountries();
    var VisitData = JSON.parse(localStorage.getItem("CurrentVisit"));
    var urlGetAllTrips = GlobalResourses.BaseURL + "Trips/GetAllTripsByLocationData?LanagugeID=" + localStorage.getItem("LanguageID") + "&LocationID=" + VisitData.LocationID;
    Common.Ajax('get', urlGetAllTrips, null, 'json', SucessGetAllTrips, false);

    $('#SignUp').click(function () {
          if(ValidateRequirdData.IsValid())
               {
            var UserData = {
                ClientName: $('#UserName').val(),
                ClientPhone: $('#SPhoneNumberCodes').val() + '-' + $('#Phone').val(),
                ClientEmail: $('#Email').val(),
                ClientPassport: $('#Passport').val(),
                CountryID: $('#Country').val(),
            };

            var urlSignUp = GlobalResourses.BaseURL + "Client/SignUp";
            Common.Ajax('Post', urlSignUp, JSON.stringify( UserData), 'json', SucessSignUp, false);

       }else{
              Alert.Show("Please Complete Your Data ");
              ShowModal.hide("ModelSignUp");
}
    });


    var urlOperatingLocations = GlobalResourses.BaseURL + "OperatingLocations/GetLocationNameByID?LocationID=" + VisitData.LocationID;
    Common.Ajax('get', urlOperatingLocations, null, 'json', SucessOperatingLocations, false);

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

function SucessCheckIfClientSignUp(Result) {
    window.location.reload();
}

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

function SucessOperatingLocations(Result) {
    var x = $('.LocationName');
    $('.LocationName').html(Result.Data);

}
function SucessurlStartNewVisit(Result) {

    var VisitData = JSON.parse(localStorage.getItem("CurrentVisit"));
    var VisitNewData={
        CountryID:VisitData.CountryID,
        EndVisitDate: VisitData.EndVisitDate,
        HotelName:VisitData.HotelName,
        LocationID: VisitData.LocationID,
        StartVisitDate: VisitData.StartVisitDate,
        HotelID: VisitData.HotelID,
        CurrentVisitID:Result.Data
     };
    localStorage.setItem("CurrentVisit",JSON.stringify( VisitNewData));
    window.location.href = ("TripsListing.html");

}

function SucessSignUp(Result) {

    if (Result.Data.ClientID !== 0) {
        localStorage.setItem("UserData", JSON.stringify(Result.Data));
        var SavedVisitData = localStorage.getItem("CurrentVisit");
        var NewVisit = JSON.parse(SavedVisitData);


        var Visit = {
            ClientID: Result.Data.ClientID,
            CountryID: NewVisit.CountryID,
            LocationID: NewVisit.LocationID,
            EndVisitDate: NewVisit.EndVisitDate,
            StartVisitDate: NewVisit.StartVisitDate,
            HotelName: NewVisit.HotelName,
            HotelID: NewVisit.HotelID

        };


        var urlStartNewVisit = GlobalResourses.BaseURL + "ClientVisits/StartNewVisits";
        Common.Ajax('Post', urlStartNewVisit, JSON.stringify(Visit), 'json', SucessurlStartNewVisit, false);
        window.location.href = ("TripDetails.html");
    }
    else {
        window.location.reload();
    }
}
function SucessGetAllCountries() {
    var Result = Common.GetALLCountries();
    $('#Country, #EditCountry').select2();
    for (var i = 0; i < Result.length; i++) {
        $('#Country, #EditCountry').append('<option value="'+Result[i].CountryID +'">'+Result[i].CountryName +'</option>');
    }

}
function SucessGetAllTrips(Result) {

    for (var i = 0; i < Result.Data.length; i++) {
        var PriceText = "";
        for (var j = 0; j < Result.Data[j].TripCurrency.length; j++) {

            if (i == Result.Data[i].TripCurrency.length - 1 && Result.Data[i].TripCurrency.length!=1) {
                PriceText = PriceText + Result.Data[i].TripCurrency[j].CurrencySign + Result.Data[i].TripCurrency[j].Price+",";
            }
            else {

                PriceText = PriceText + Result.Data[i].TripCurrency[j].CurrencySign + Result.Data[i].TripCurrency[j].Price;

            }


        }
        //for (var n = 0; n < Result.Data[i].Photos.length; n++) {
        //   // checkImageAvailability(Result.Data[i].Photos[n].PhotoPath);

        //    //Result.Data[i].Photos[n].PhotoPath = $('#profileImge').src;
               
        //}
        //if (Result.Data[i].Photos[0] === "")
        //{
        //}
        if (Result.Data[i].Photos.length > 0) {
            $('#Trips').append(
                '<a href="#" class="store-card-cart" onclick="PickTrip('+ Result.Data[i].TripID+ ')"> <div href="#" class="store-card-item"><em class="bg-red-dark">' +
                PriceText +
                '</em><img onerror="Error(this)" onload="load(this)" class="responsive-image preload-image TripImages" data-original="' + Result.Data[i].Photos[0].PhotoPath
                + '" alt="img" src="'
                + Result.Data[i].Photos[0].PhotoPath +
                '" style="display: block;"><h3>'
                + Result.Data[i].TripName +
                '</h3></div></a>'
            );

        }
       

       // images / pictures / 1.jpg


    }
    if (Result.Data.length == 0) {
        alert("There is no Trips avalable right now.");
    }
    Loader.hide();
}
function PickTrip(TripID) {

    if ((JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientID) === null) {
        localStorage.setItem("TripID", TripID);
        //GetCountriesCodesForSigne();
        ShowModal.show("ModelSignUp");

    }
    //else if (localStorage.getItem("CurrentVisit") === null) {
    //    alert("No Visit Till Now");

    //}
    else {
        //Add  View Trip
        localStorage.setItem("TripID", TripID);
        window.location.href = ("TripDetails.html");

    }


}


   
//function checkImageAvailability(src) {
//    $("<img>").attr('src', src)
//        .on("error", function (e) {
//            $('.TripImages').attr('src', "images/pictures/NoImageAvailable.jpg");
//        })
//        .on("load", function (e) {

//            $('.TripImages').attr('src', src);
//        });
//}

 function  Error(e) {
   //  e.attr('src', "images/pictures/NoImageAvailable.jpg");
     e.src = "images/pictures/NoImageAvailable.jpg";
}
 function load(e) {

    // e.attr('src', src);
    }


 function GetCountriesCodes() {
      SucessGetAllCountriesCodes();
 }

 function SucessGetAllCountriesCodes() {
     var Result = Common.GetALLCountries();

     $('#PhoneNumberCode, #SPhoneNumberCodes, #EditPhoneNumberCode').select2();
     for (var i = 0; i < Result.length; i++) {
         $('#PhoneNumberCode, #SPhoneNumberCodes, #EditPhoneNumberCode').append('<option value="' + Result[i].CountryCode + '">' + Result[i].CountryCode + '</option>');
     }
 }
