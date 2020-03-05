$(document).ready(function () {

    //disableBackButton.Disable();
    //alert("work??");
    var urlGetAllLanguage = GlobalResourses.BaseURL + "Language/GetAllLanguage";
   // alert(urlGetAllLanguage);

    Common.Ajax('get', urlGetAllLanguage, null, 'json', SucessGetAllLanguage, false);
   // alert("work??");
    
});
function SucessGetAllLanguage(Result) {
   // alert("work??");

    for (var i = 0; i < Result.Data.length; i++) {
        //alert(Result.Data[i].LanguageName);


        $('#languages').append('<a onclick="GetApplicationTexts(' + Result.Data[i].LanguageID + ')"><div class="activity-item one-half-responsive"><i class="fa fa-circle color-green-dark"></i><strong>' + Result.Data[i].LanguageName + '</strong></div></a>');


    }
}

function GetApplicationTexts(LanguageID) {


    localStorage.setItem("LanguageID", LanguageID);

    var urlGetApplicationTexts = GlobalResourses.BaseURL + "InternalApplicationText/GetAllInternalApplicationTextByLangugeID?LangugeID=" + LanguageID;
    Common.Ajax('get', urlGetApplicationTexts, null, 'json', SucessGetApplicationTexts, false);
    GetAllCountries();
    window.location.href = ("Homepage.html");
}
function GetAllCountries() {
    var urlGetAllCountries = GlobalResourses.BaseURL + "Country/GetAllCountries";
    Common.Ajax('get', urlGetAllCountries, null, 'json', SucessGetAllCountries, false);

}
function SucessGetAllCountries(Result) {
    var ListOfCountries = [];
    for (var i = 0; i < Result.Data.length; i++) {

        var item = {
            CountryCode: Result.Data[i].CountryCode,
            CountryName: Result.Data[i].CountryName,
            CountryID: Result.Data[i].CountryID
        };
        ListOfCountries.push(item);

    }
    localStorage.setItem("ListOfCountries", JSON.stringify(ListOfCountries));

}
function SucessGetApplicationTexts(Result) {
    var ListOfTexts = [];
    for (var i = 0; i < Result.Data.length; i++) {

        var item = {
            SelectorName: Result.Data[i].SelectorName,
            Text: Result.Data[i].Text,
            SelectorIsTageID: Result.Data[i].SelectorIsTageID
        };
        ListOfTexts.push(item);

    }
    localStorage.setItem("ListOfTexts", JSON.stringify(ListOfTexts));


}