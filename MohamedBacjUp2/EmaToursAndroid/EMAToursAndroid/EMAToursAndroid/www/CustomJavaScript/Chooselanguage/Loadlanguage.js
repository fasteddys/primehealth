
$(document).ready(function () {
    var ListOfTextsJson = JSON.parse(localStorage.getItem("ListOfTexts"));
    if (ListOfTextsJson !== null) {
        for (var i = 0; i < ListOfTextsJson.length; i++) {
            var x = $('#' + ListOfTextsJson[i].SelectorName);
            var y = $('#' + ListOfTextsJson[i].SelectorName).html();
            var TypeOfText = ListOfTextsJson[i].SelectorName.split("_")[0];

            if (ListOfTextsJson[i].SelectorIsTageID === true) {
                if (TypeOfText == 'HTML') {
                    $('#' + ListOfTextsJson[i].SelectorName).html(ListOfTextsJson[i].Text);
                }
                else if (TypeOfText == 'PH')
                {
                    $('#' + ListOfTextsJson[i].SelectorName).attr("placeholder",ListOfTextsJson[i].Text);
                }
            }
            else {
                if (TypeOfText == 'HTML') {
                    $('.' + ListOfTextsJson[i].SelectorName).html(ListOfTextsJson[i].Text);
                }
                else if (TypeOfText == 'PH') {
                    $('.' + ListOfTextsJson[i].SelectorName).attr("placeholder",ListOfTextsJson[i].Text);
                }
            }
        }
    }

});