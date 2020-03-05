$(document).ready(function () {
    //GetTemplateData();
});

function GetTemplateData() {

    var urlCreateExel = "/Provider/ExtractProvidersTemplateExcel";

    var date = GetTodayDate();

    var xhttp = new XMLHttpRequest();

    // Post data to URL which handles post request
    xhttp.open("POST", urlCreateExel);  
    
    xhttp.setRequestHeader("Content-Type", "application/json; charset=utf-8");
    // You should set responseType as blob for binary responses
    xhttp.responseType = 'blob';
    xhttp.send(" ... ");
    //xhttp.send(JSON.stringify(shiftObj));

    xhttp.onreadystatechange = function () {
        var a;
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            // Trick for making downloadable link
            a = document.createElement('a');
            a.href = window.URL.createObjectURL(xhttp.response);
            // Give filename you wish to download
            a.download = "ProvidersDataTemplate " + date + ".xlsx";
            a.style.display = 'none';
            document.body.appendChild(a);
            a.click();
        }
    };
}

function GetTodayDate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = dd + '-' + mm + '-' + yyyy;
    return today;
}