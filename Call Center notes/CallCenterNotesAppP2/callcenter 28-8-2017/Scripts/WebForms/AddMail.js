function AddMail() {
    var EmailsArea = document.getElementById("ALLmails").innerHTML;
    var emailtext = "<input type='text' /><input type='button' value='Delete' />";
    EmailsArea.appendChild(emailtext);

}