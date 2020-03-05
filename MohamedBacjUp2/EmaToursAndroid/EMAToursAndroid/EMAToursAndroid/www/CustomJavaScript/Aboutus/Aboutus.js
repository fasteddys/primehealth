$(document).ready(function () {

    


    var urlAboutus = GlobalResourses.BaseURL + "Configuration/GetConfigurationValue?ConfigurationKey=" + "AboutUsData" + "&LanguageId=" + localStorage.getItem("LanguageID");
    Common.Ajax('get', urlAboutus, null, 'json', SucessurlGetConfigurationValue, false);

    SucessGetAllCountries();

    $('#Welcome').click(function () {
        CloseSideMenu();

        if (JSON.parse(localStorage.getItem("UserData")) !== null) {
            //window.location.href = ("TripsListing.html");
            ShowModal.show("ModelEdit");
            //$('.sidebar-left').hide();

            var profileData = JSON.parse(localStorage.getItem("UserData"));
            $('#EditUserName').val(profileData.ClientName);
            $('#EditPhone').val(profileData.ClientPhone);
            $('#EditEmail').val(profileData.ClientEmail);
            $('#EditPassport').val(profileData.ClientPassport);
            $('#EditCountry').val(profileData.CountryID);
        }
            
    });

    $('#Edit').click(function () {
        var ClientData = {
            ClientName: $('#EditUserName').val(),
            ClientEmail: $('#EditEmail').val(),
            ClientPhone: $('#EditPhone').val(),
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
            NotificationMethodID: CurrentClientData.NotificationMethodID
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
function SucessurlGetConfigurationValue(Result)
{
    $("#EmaTourDefinition").append(Result.Data);
}
