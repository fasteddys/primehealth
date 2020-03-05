$("#UpdateTrip").click(

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
            TripID: $("#TripID").val(),
            LocationID: $("#LocationID").val(),
            Status_ID: $("#Status_ID").val(),
            CountryID: $("#CountryID").val(),
            LangugeID: $("#LangugeID").val(),
            TripName: $("#TripName").val(),
            TripShortDesc: $("#TripShortDesc").val(),
            TripDetailedDesc: $("#TripDetailedDesc").val(),
            Price: $("#Price").val(),
            Photos: Photos


        };

        $.ajax({
            url: "/Trip/EditTrip",
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
                    type: "POST", url: '/Trip/AddAttachJson?TripID=' + $("#TripID").val() + "&languageID="+$("#LangugeID").val(),
                    data: formData, dataType: 'json', contentType: false
                    , processData: false

                });
                //@* $("#UpdatedPartial").html( @Html.Partial("_ManageTicket", result));*@
                //  }


            }
        });
    });