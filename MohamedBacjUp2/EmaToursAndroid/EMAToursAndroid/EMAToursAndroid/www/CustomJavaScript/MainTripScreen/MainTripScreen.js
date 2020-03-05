$(document).ready(function () {
    LoadNotifications();
    GetCountriesCodes();
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
 $('#SignUp').click(function () {
        var UserData = {
            ClientName: $('#UserName').val(),
            ClientPhone: $('#SPhoneNumberCodes').val() + '-' + $('#Phone').val(),
            ClientEmail: $('#Email').val(),
            ClientPassport: $('#Passport').val(),
            CountryID: $('#Country').val(),
            HotelName: $('#Hotel').val()
        };


        if(ValidateRequirdData.IsValid())
         {
        var urlSignUp = GlobalResourses.BaseURL + "Client/SignUp";
        Common.Ajax('Post', urlSignUp, JSON.stringify(UserData), 'json', SucessSignUp, false);
      }else
{
            Alert.Show("Please Complete Your Data");



        }    
    });

 SucessGetAllCountries();

    $('#StartTrip').click(function () {
        //if (localStorage.getItem("CurrentVisitID") === null) {
        //    window.location.href = ("StartNewVisit.html");

        //   // $('#ShowAddNewVisit').click();
        //}
        //else
        //{
        window.location.href = "TripsListing.html";
        //}
        
    });
    $('#BookPickUp').click(function () {
        var today = new Date();
        var dd = String(today.getDate()+1).padStart(0,'0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        today = mm + '/' + dd + '/' + yyyy;
        //today.addda

        var CurrentVisit = JSON.parse(localStorage.getItem("CurrentVisit"));
        
        var x = new Date(today);

        if (CurrentVisit == null) {
            Alert.Show("You should create visit first");
            return;
        }
        else 
            var EndDate = new Date(CurrentVisit.EndVisitDate);
            
        if (JSON.parse(localStorage.getItem("UserData")) === null) {
            ShowModal.show("ModelSignUp");
        }

      else  if (new Date(today).getDate() >= EndDate.getDate()) {


            window.location.href = ("BookPickUp.html");
        } else {
            Alert.Show("Pickup request will be only available 24 hours before your visit ends");
        }

    });




   var  VisitData=JSON.parse( localStorage.getItem("CurrentVisit"));
if(VisitData!==null){
  $('#VisitID').html(VisitData.CurrentVisitID);


   
   var urlOperatingLocations = GlobalResourses.BaseURL + "OperatingLocations/GetLocationNameByID?LocationID=" + VisitData.LocationID;
   Common.Ajax('get', urlOperatingLocations, null, 'json', SucessOperatingLocations, false);

   var urlOperatingCountry = GlobalResourses.BaseURL + "OperatingCountries/GetCountryNameByID?CountryID=" + VisitData.CountryID;
   Common.Ajax('get', urlOperatingCountry , null, 'json', SucessurlOperatingContry, false);
}
   //$('#StartNewVisit').click(function () {
   //    if (localStorage.getItem("VisitID") === null) {
   //        window.location.href = ("StartNewVisit.html");

   //       // $('#ShowAddNewVisit').click();
   //    }
   //    else {
   //        window.location.href = ("tripslisting.html");
   //    }

   //});
   //disableBackButton.Disable();
     

$('#Welcome').click(function () {
    SucessGetAllEditCountries();
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
        //$('.sidebar-left').show();    
    });

    $('#Edit').click(function () {
        var ClientData = {
            ClientID: JSON.parse(localStorage.getItem("UserData")).ClientID,
            LoggedUser: JSON.parse(localStorage.getItem("UserData")).ClientID,
            ClientName: $('#EditUserName').val(),
            ClientEmail: $('#EditEmail').val(),
            //CountryCode: $('EditPhoneNumberCode').val(),
            ClientPhone: $('#EditPhoneNumberCode').val() + '-' + $('#EditPhone').val(),
            ClientPassport: $('#EditPassport').val(),
            CountryID: $('#EditCountry').val()
        };

        var urlEditClientData = GlobalResourses.BaseURL + "Client/EditClientData";
        Common.Ajax('Post', urlEditClientData, JSON.stringify(ClientData), 'json', SuccessEditClientData, false);
    });

    if (localStorage.getItem("CurrentPickUpID") !== null) {
        $('#SchedulePickup').removeClass('HTML_MainTrip_SchedulePickup');
        $('#SchedulePickup').addClass('HTML_MainTrip_EditSchedulePickup');
    }
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

function SucessGetAllEditCountries() {
    var Result = Common.GetALLCountries();
    $('#EditCountry').select2();
    for (var i = 0; i < Result.length; i++) {
        $('#EditCountry').append('<option value="'+Result[i].CountryID +'">'+Result[i].CountryName +'</option>');
    }

}


function SucessOperatingLocations(Result) {
    $('.LocationName').html(Result.Data);

}
function SucessurlOperatingContry(Result) {
    $('#CountryName').html(Result.Data);

}

  
function SucessSignUp(Result) {
    if (Result.Data.ClientID !== 0){
        localStorage.setItem("UserData", JSON.stringify(Result.Data));
        $('#BookPickUp').click(); //   window.location.href = ("BookPickUp.html");
    }
    else {
        window.location.reload();
    }

}
function SucessGetAllCountries() {
    var Result = Common.GetALLCountries();
    $('#Country').select2();
    for (var i = 0; i < Result.length; i++) {
        $('#Country').append('<option value="'+Result[i].CountryID +'">'+Result[i].CountryName +'</option>');
    }

}

function LoadNotifications() {

    if ((JSON.parse(localStorage.getItem("UserData")) != null
        // JSON.parse(localStorage.getItem("CurrentVisit")).VisitID) != null
    )) {
        var UserNotification = {
            UserID: JSON.parse(localStorage.getItem("UserData")).ClientID
            // VisitID: JSON.parse(localStorage.getItem("CurrentVisit")).VisitID

        };


        var urlGetAllCountries = GlobalResourses.BaseURL + "Notification/GetClientNotifications";
        Common.Ajax('Post', urlGetAllCountries, JSON.stringify(UserNotification), 'json', SucessLoadNotifications, false);
    }

}
function SucessLoadNotifications(Result) {
    $(".NotificationPadge")[0].innerHTML = Result.Data.length;
    for (var i = 0; i < Result.Data.length; i++) {
        $("#Notifications").append(
            '<a href="#"><h4><span class=" fa fa-star-o"></span><span id="HTML_Home_BegainVisitHeader">'
            + Result.Data[i].NotificationHeader + '</span></h4><p><span id="HTML_Home_BegainVisitParagraph">'
            + Result.Data[i].NotificationBody + '</span></p></a><div class="decoration"></div>'
        );

    }
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

//function GetCountriesCodesForSigne() {
//    var urlGetAllCountriesCodes = GlobalResourses.BaseURL + "Country/GetAllCountries";
//    Common.Ajax('get', urlGetAllCountriesCodes, null, 'json', SucessGetAllCountriesCodesForSigne, false);
//}

//function SucessGetAllCountriesCodesForSigne(Result) {
//    $('#SPhoneNumberCodes').select2();
//    for (var i = 0; i < Result.Data.length; i++) {
//        $('#SPhoneNumberCodes').append('<option value="' + Result.Data[i].CountryCode + '">' + Result.Data[i].CountryCode + '</option>');
//    }
//}




