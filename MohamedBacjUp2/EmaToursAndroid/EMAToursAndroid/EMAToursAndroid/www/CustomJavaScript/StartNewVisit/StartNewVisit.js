var CountryName = "";
var LocationName = "";

//BackButtonHandler.Attach(3);
  //  disableBackButton.Disable();

$(document).ready(function () {

    GetCountriesCodes();
    //BackButtonHandler.Attach(BackButtonHandler.Types.Exit);
    var urlGetAllOperatingCountries = GlobalResourses.BaseURL + "OperatingCountries/GetAllCountries?LanguageFK=" + localStorage.getItem("LanguageID");
    Common.Ajax('get', urlGetAllOperatingCountries, null, 'json', SucessGetAllOperatingCountries, false);
   // ShowModal.show("VisitDatesModal");

    //$('#OKButton').click(function () {
    //    ShowModal.hide("AlertPopup");
    //});

    $('#StartVisit').click(function () {
        if (ValidateRequirdData.IsValid()) {
            ShowModal.show("ModelConfirmation");
            ShowModal.hide("VisitDatesModal");
        } else {
            Alert.Show("Please Complete Visit Data ");
        } });

    $("#ConfirmVisit").click(
        function () {

   if(ValidateRequirdData.IsValid())
{
            var Visit = {
                CountryID: localStorage.getItem("VisitCountryID"),
                LocationID: localStorage.getItem("VisitLocationID"),
                EndVisitDate: $("#EndDate").val(),
                StartVisitDate: $("#StartDate").val(),
                HotelName: $("#HotelName").val()
       };

            if ($('#HotelID').val() == "-1") {
                Visit.HotelID = -1;
            }
            else {
                Visit['HotelID'] = $('#HotelID').select2('data')[0].id;
                Visit.HotelName = $('#HotelID').select2('data')[0].text;
            }
            
            localStorage.setItem("CurrentVisit", JSON.stringify(Visit) );
            window.location.href = ("MainTripScreen.html");
            // var urlStartNewVisit = GlobalResourses.BaseURL + "ClientVisits/StartNewVisits";
            //   Common.Ajax('Post', urlStartNewVisit, JSON.stringify(Visit), 'json', SucessurlStartNewVisit, false);

   }
   else {
       Alert.Show("Please Complete Visit Data ");

}
 });
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

function SucessGetAllOperatingCountries(Result) {  
    for (var i = 0; i < Result.Data.length; i++) {

        $('#Country').append(
            '<a href="#" onclick="SelectLocation(' + Result.Data[i].OperatingCountryID + ',this)"><div class="activity-item one-half-responsive"><i class="fa fa-circle color-green-dark"></i><strong>' + Result.Data[i].OperatingCountryName + '</strong></div></a>'
        );
 
    }

}
function SelectLocation(CountryID,obj) {

    var Country = {
        LanguageFK: localStorage.getItem("LanguageID"),
        OperatingCountryID: CountryID

    };

    var urlGetAllOperatingLocation = GlobalResourses.BaseURL + "OperatingLocations/GetAllOperatingLocations";
    Common.Ajax('Post', urlGetAllOperatingLocation, JSON.stringify( Country), 'json', SucessGetAllOperatingLocation, false);
    localStorage.setItem("VisitCountryID", CountryID);
    $('#CountryName').html(obj.text);



}
function SucessGetAllOperatingLocation(Result) {
    $('#Country').empty();
    for (var i = 0; i < Result.Data.length; i++) {
        $('#Country').append(
            '<a href="#" onclick="SetLocationValue(' + Result.Data[i].OperatingLocationID + ',this)"><div class="activity-item one-half-responsive"><i class="fa fa-circle color-green-dark"></i><strong>' + Result.Data[i].OperatingLocationName + '</strong></div></a>'
        );

    }
    $('#ChoseCountryLocations').replaceWith(
        function () {
            // Execute a callback to generate contents of the replacement
            // The $('<div>') part creates a div
            return $('<h3 id="ChoseCountryLocations"><a href="#"><i class="fa fa-arrow-circle-left" onclick="BackToCountries()"></i></a> Please choose <span>Location</span></h3>', {
                html: this.innerHTML // This takes the html of the 'a' tag and copies it to the new div
            });
        });
}



function SucessurlStartNewVisit(Result) {


    localStorage.setItem("CurrentVisitID", Result.Data);
    window.location.href = ("TripsListing.html");

}

function BackToCountries() {
    //$('#ChoseCountryLocations').hide();
    //$('#ChoseCountryTowns').show();
    $('#ChoseCountryLocations').replaceWith(
        function () {
            // Execute a callback to generate contents of the replacement
            // The $('<div>') part creates a div
            return $('<h3 id= "ChoseCountryLocations" class = "HTML_StartNewVisit_chooseTown"></h3>'
                , {
                html: this.innerHTML // This takes the html of the 'a' tag and copies it to the new div
            });
        });
    $('#Country').empty();

    var urlGetAllOperatingCountries = GlobalResourses.BaseURL + "OperatingCountries/GetAllCountries?LanguageFK=" + localStorage.getItem("LanguageID");
    Common.Ajax('get', urlGetAllOperatingCountries, null, 'json', SucessGetAllOperatingCountries, false);



}

function SetLocationValue(LocationID,Obj)
{
    localStorage.setItem("VisitLocationID", LocationID);
    //$('#ShowSignUp').click();
    ShowModal.show("VisitDatesModal");
    $('#LocationName').html(Obj.text);
    $('#HotelID').empty();
    $('#HotelID').select2();
    //$('#HotelID').append('<option value="-1">Other</option>');
    //$('#HotelID').append('<option value="-2">Other</option>');
    $('#HotelID').removeClass('select2');
    GetHotels();
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
        if ($('#HotelID').val()=="-1") {
            $("#HotelNameDIv").show();
            $("#HotelName").addClass('IsRequirdField');
        }
        else {
            $("#HotelNameDIv").hide();
            $("#HotelName").removeClass('IsRequirdField');

        }
    });

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