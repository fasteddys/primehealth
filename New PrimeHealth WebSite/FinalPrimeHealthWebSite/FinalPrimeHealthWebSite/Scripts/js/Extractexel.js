function exractexel() {
    var input = document.getElementById('input')

    input.addEventListener('change', function () {
        readXlsxFile(input.files[0]).then(function (data) {
            // `data` is an array of rows
            // each row being an array of cells.
         //   document.getElementById('result').innerText = JSON.stringify(data, null, 2)
        //  var columns=  data[0][1];
          var table = document.getElementById("usersdata");
           // $(table).empty();

          var count = 0;
            var columnscount=3
          for (var item in data)
          { count++; }
          for (var i = 1; i < count ; i++)
          {
             
              var row = $("<tr><td>" + data[i][0] + "</td><td>" + data[i][1] + "</td><td>" + data[i][2] + "</td></tr>");


              $(table).append(row);
     




          }

         // document.getElementById("example23_filter").innerHTML = "";
         // $('.dt-buttons').remove();
         // $('.dataTables_filter').remove();

          
         // document.getElementById("example23_info").innerHTML = "";
         // document.getElementById("example23_paginate").innerHTML = "";

          
          
         // var tag = document.getElementsByTagName("body")[0].innerHTML;
         // document.getElementsByTagName("body")[0].innerHTML = "";
         // var body = document.getElementsByTagName("body");

         //$( body).append(tag);


         //reload_js('~/js/custom.min.js');
         //     reload_js('~/js/lib/datatables/datatables.min.js');
         //     reload_js('~/js/lib/datatables/cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js');
         //     reload_js('~/js/lib/datatables/cdn.datatables.net/buttons/1.2.2/js/buttons.flash.min.js');
         //     reload_js('~/js/lib/datatables/cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js');
         //     reload_js('~/js/lib/datatables/cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js');
         //     reload_js('~/js/lib/datatables/cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js');
         //     reload_js('~/js/lib/datatables/cdn.datatables.net/buttons/1.2.2/js/buttons.html5.min.js');
         //     reload_js('~/js/lib/datatables/cdn.datatables.net/buttons/1.2.2/js/buttons.print.min.js');
         //     reload_js('~/js/lib/datatables/datatables-init.js');

        }, (error) => {
            console.error(error)
            alert("Error while parsing Excel file. See console output for the error stack trace.")
        })
    })




    function reload_js(src) {
     //   $('script[src="' + src + '"]').remove();
       // $('<script>').attr('src', src).appendTo('scirepts');
    }







}


