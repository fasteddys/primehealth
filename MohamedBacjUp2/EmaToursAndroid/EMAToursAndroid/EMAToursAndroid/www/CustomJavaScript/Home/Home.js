//disableBackButton.Disable();
$(document).ready(function () {
    

    //BackButtonHandler.Attach(3);


    ShowRating();

  SucessGetAllCountries();
 GetCountriesCodes();

 $("#RatePastVisit").click(function () {
     Rate.OpenRatePopUp();
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

    $("#ViewPreviousVisits").click(function () {

        if ((JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientID)=== null)
            {
                // $("#ShowEnterPhone").click();
            ShowModal.show("ModelSignUp");
            }
            else
            {

                window.location.href = ("PreviousVisits.html"); 


            }
    });
    $("#ShowNumber").click(function () {
        ShowModal.hide("ModelSignUp");
        ShowModal.show("ModalEnterPhone");
    });
    


    $("#StartVisit").click(function () {
        if ((JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientID) === null
            || (JSON.parse(localStorage.getItem("CurrentVisit")) == null ? null : JSON.parse(localStorage.getItem("CurrentVisit")).ClientID) === null
        ) {
            // $('#ShowSignUp').click();
            //ShowModal.show("ModelSignUp");
            window.location.href = ("StartNewVisit.html");

        }
        else {
            window.location.href = ("MainTripScreen.html");

        }
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

  if(     ValidateRequirdData.IsValid())
{
        var urlSignUp = GlobalResourses.BaseURL + "Client/SignUp";
        Common.Ajax('Post', urlSignUp, JSON.stringify(UserData), 'json', SucessSignUp, false);

}else{
      Alert.Show("Please Complete Your Data ");



}
    });

    SucessGetAllEditCountries();

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

function SucessGetAllEditCountries() {
    var Result = Common.GetALLCountries();
    $('#EditCountry').select2();
    for (var i = 0; i < Result.length; i++) {
        $('#EditCountry').append('<option value="'+Result[i].CountryID +'">'+Result[i].CountryName +'</option>');
    }

}

function SucessCheckIfClientSignUp(Result) {
    localStorage.setItem("UserData", JSON.stringify(Result.Data));
            window.location.href = ("PreviousVisits.html");

}
function SucessSignUp(Result) {
    if (Result.Data.ClientID !== 0) {
        localStorage.setItem("UserData", JSON.stringify(Result.Data));
        window.location.reload();
    }
    else {
        window.location.reload();
    }   
}

function SucessGetAllCountries() {
    var Result = Common.GetALLCountries();
    $('#PhoneNumberCode').select2();
    $('#Country').select2();

    for (var i = 0; i < Result.length; i++) {
        $('#PhoneNumberCode').append('<option value="'+Result[i].CountryCode +'">'+Result[i].CountryName +'</option>');

        $('#Country').append('<option value="' + Result[i].CountryID + '">' + Result[i].CountryName + '</option>');

    }

    

}

function ShowRating() {
    if (localStorage.getItem("CurrentVisit") !== null) {
        var today = new Date();
        var dd = String(today.getDate()).padStart(0, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();

        today = mm + '/' + dd + '/' + yyyy;
        //today.addda

        var CurrentVisit = JSON.parse(localStorage.getItem("CurrentVisit"));
        var EndDate = new Date(CurrentVisit.EndVisitDate);
        var x = new Date(today);


        if (new Date(today).getDate() > EndDate.getDate()) {


            $("#RatePastVisit").attr("Style","visibility:visible");
        }
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



//function SucessGetAllCountriesCodesForSigne(Result) {
//    $('#SPhoneNumberCodes').select2();
//    for (var i = 0; i < Result.Data.length; i++) {
//        $('#SPhoneNumberCodes').append('<option value="' + Result.Data[i].CountryCode + '">' + Result.Data[i].CountryCode + '</option>');
//    }
//}


//function onLoad() {
//    alert('test load ');
//    //document.addEventListener("deviceready", onDeviceReady, false);
//    document.addEventListener("backbutton", onBackKeyDown, false);

//}

//// device APIs are available
////
//function onDeviceReady() {
//    alert('test device ready ');
//    // Register the event listener
//}

//// Handle the back button
////
//function onBackKeyDown() {
//    alert('test onBackKeyDown ');
//    navigator.notification.confirm(
//        'Are you sure you want to exit?!', // message
//        onConfirm,            // callback to invoke with index of button pressed
//        'Confirm exit',           // title
//        ['Yes', 'No']     // buttonLabels
//    );
//    function onConfirm(buttonIndex) {
//        alert('test onConfirm ');
//        if (buttonIndex == 1) {
//            navigator.app.exitApp();
//        }
//    }
//}