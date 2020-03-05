$(document).ready(function () {
    $('#Assignbtn').click(function () {
        const urlParams = new URLSearchParams(window.location.search);
        var myParam = urlParams.get('TripRequestID');
        var TripRequest = {
            ClientTripID : myParam,
            AssignedUserActionTime : $('#datetimepicker1').val()
        };
        $.ajax({
            url: '/Trip/AssignRequestAsync',//?TripRequestID=' + myParam,
            data: JSON.stringify(TripRequest),
            type: 'POST',
            contentType: 'application/json',
            success: getmyticketsonsuccess,
            error: onerror
        });



    })
});
    function getmyticketsonsuccess() {


    }

    function error() {


    }
    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }


$("#AddTrip").click(

    function () {
        var Photos = [];
        var m = $(".Photo");
        for (i = 0; i < $(".Photo").length; i++) {
            var x = $(".Photo")[i];
            var z = x.childNodes[3].checked;
            //var m = z;


            var Photo = {
                PhotoID: $(".Photo")[i].id,
                IsActive: z


            };
            Photos.push(Photo);
        }

        var data = {
            LocationID: $("#LocationID").val(),
            Status_ID: $("#Status_ID").val(),
            CountryID: $("#CountryID").val(),
            LangugeID: $("#LangugeID").val(),
            TripName: $("#TripName").val(),
            TripShortDesc: $("#TripShortDesc").val(),
            TripDetailedDesc: $("#TripDetailedDesc").val(),
            Price: $("#Price").val(),
            Photos: Photos,
            CurrencyID: $("#CurrencyID").val()


        };

        $.ajax({
            url: "/Trip/AddNewTrip",
            type: "Post",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                var formData = new FormData();
                var totalFiles = document.getElementById('File1').files.length;
                for (var i = 0; i < totalFiles; i++) {
                    var file = document.getElementsByClassName('test')[0].files[i];
                    formData.append('File1', file, file.name);
                }
                $.ajax({
                    type: "POST", url: '/Trip/AddAttachJson?TripID=' + result + "&languageID=" + $("#LangugeID").val(),
                    data: formData, dataType: 'json', contentType: false
                    , processData: false

                });
                //@* $("#UpdatedPartial").html( @Html.Partial("_ManageTicket", result));*@
                //  }


            }
        });


        $("#TripName").val("");
        $("#TripShortDesc").val("");
        $("#TripDetailedDesc").val("");
        $("#Price").val("");

    });