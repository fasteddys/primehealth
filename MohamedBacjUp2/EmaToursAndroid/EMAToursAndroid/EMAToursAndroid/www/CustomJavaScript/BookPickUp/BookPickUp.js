$(document).ready(function () {
    GetCountriesCodes();
    $("#BookPickUp").click(
        function () {
            var PickUp = {
       
                BackFlightTime: $("#BackFlightTime").val(),
                //DesiredPickupTime: $("#DesiredPickupTime").val(),
                NumberOfMembers: $("#NumberOfMembers").val(),
                ClientNote: $("#Note").val(),
                ClientVisitFK: JSON.parse(localStorage.getItem("CurrentVisit")).CurrentVisitID == 0 ? null : JSON.parse(localStorage.getItem("CurrentVisit")).CurrentVisitID,
                ClientID: (JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientID),
                PickUpRequestID: 0
            };
            if (localStorage.getItem("CurrentPickUpID") === null) {
                if (ValidateRequirdData.IsValid()) {
                    var urlStartNewPickUp = GlobalResourses.BaseURL + "PickUp/AddPickUpRequest";
                    Common.Ajax('Post', urlStartNewPickUp, JSON.stringify(PickUp), 'json', SucessurlStartNewPickUp, false);
                } else {
                    Alert.Show("Please Complete PickUp Request Data ");
                }
            }
            else {
                ShowModal.show("ModelConfirmation");
            }
            
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

    $('#ConfirmEdit').click(function () {
        var CurrentClientData = JSON.parse(localStorage.getItem("UserData"));
        var PickUp = {

            BackFlightTime: $("#BackFlightTime").val(),
            //DesiredPickupTime: $("#DesiredPickupTime").val(),
            NumberOfMembers: $("#NumberOfMembers").val(),
            ClientNote: $("#Note").val(),
            ClientVisitFK: JSON.parse(localStorage.getItem("CurrentVisit")).CurrentVisitID,
            ClientID: (JSON.parse(localStorage.getItem("UserData")) == null ? null : JSON.parse(localStorage.getItem("UserData")).ClientID),
            PickUpRequestID: localStorage.getItem("CurrentPickUpID"),
            LoggedUser: CurrentClientData.ClientID
        };

        var urlEditPickUp = GlobalResourses.BaseURL + "PickUp/EditPickUpRequest";
        Common.Ajax('Post', urlEditPickUp, JSON.stringify(PickUp), 'json', SucessEditPickUp, false);
    });

    $('#CancelEdit').click(function () {
        ShowModal.hide("ModelConfirmation");
    });

    $('#OKButton').click(function () {
        window.location.href = ("MainTripScreen.html");
    });
    
    if (localStorage.getItem("CurrentPickUpID") !== null) {
        var urlGetPickUp = GlobalResourses.BaseURL + "PickUp/GetPickUpRequest?ClientPickUpID=" + localStorage.getItem("CurrentPickUpID");
        Common.Ajax('Get', urlGetPickUp, null, 'json', SucessGetPickUp, false);        
    }
});


function SucessGetAllCountries() {
    var Result = Common.GetALLCountries();

    $('#EditCountry').select2();
    for (var i = 0; i < Result.length; i++) {
        $('#EditCountry').append('<option value="' + Result[i].CountryID + '">' + Result[i].CountryName + '</option>');
    }
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

//function SucessGetAllCountries(Result) {
//    $('#EditCountry').select2();
//    for (var i = 0; i < Result.Data.length; i++) {
//        $('#EditCountry').append('<option value="'+Result.Data[i].CountryID +'">'+Result.Data[i].CountryName +'</option>');
//    }

//}

function SucessurlStartNewPickUp(Result) {
    localStorage.setItem("CurrentPickUpID", Result.Data.PickUpRequestID);
    Alert.Show("Thank you for completing pickup required data With Pickup ref#" + Result.Data.PickUpRequestID +

        " , we will set the pickup time and inform you via our application");
 
}

function SucessGetPickUp(Result) {
    $("#BackFlightTime").val(moment(Result.Data.BackFlightTime).format('YYYY-MM-DD'));
    $("#NumberOfMembers").val(Result.Data.NumberOfMembers);
    $("#Note").val(Result.Data.ClientNote);

    //$("#BookPickUp").removeClass('BT_BookPickup_Button');
    $("#BookPickUp").addClass('HTML_BookPickup_EditButton');
    $("#Header1").removeClass("HTML_BookPickup_AddPickUpRequest");
    $("#Header1").addClass("HTML_BookPickup_EditPickUpRequest");
    $("#Header2").removeClass("HTML_BookPickup_FillPickUpData");
    $("#Header2").addClass("HTML_BookPickup_EditPickUpData");
}

function SucessEditPickUp(Result) {
    window.location.reload();
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
