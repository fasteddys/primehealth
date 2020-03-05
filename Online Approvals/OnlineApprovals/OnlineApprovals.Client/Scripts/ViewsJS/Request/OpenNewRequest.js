$(document).ready(LoadPage());
function LoadPage() {

    //Display buttons untill enter CoInsurance Percentage
    $("#btnSubmit,.add-new").attr("disabled", "true");
    $("#CoInsurancePercentage").blur(function () {
        if ($(this).val() !== "") {
            $("#btnSubmit,.add-new").removeAttr("disabled");
            $("#SpanCoInsuarnceNote").hide();
        } else {
            $("#btnSubmit,.add-new").attr("disabled", "true");
            $("#SpanCoInsuarnceNote").show();
        }
    });

    $('#SubmitRequest').attr("disabled", "true");
    $("#DemandedQuantity").blur(function () {
        if ($(this).val() !== "") {
            $('#SubmitRequest').removeAttr("disabled");
        } else {
            $('#SubmitRequest').attr("disabled", "true");
        }
    });

    //Fill Listed Drugs Dropdown
    var url = '/Request/SearchDrugList';
    Common.Ajax('get', url, null, 'json', success);
    function success(data) {
        for (var i = 0; i < data.length; i++) {
            var opt = new Option(data[i].DrugName);

            var att = document.createAttribute("DrugID");
            att.value = data[i].DrugID;
            opt.setAttributeNode(att);

            var attx = document.createAttribute("Quaninty");
            attx.value = data[i].Quaninty;
            opt.setAttributeNode(attx);

            var UnitQuaninty = document.createAttribute("UnitQuaninty");
            UnitQuaninty.value = data[i].UnitQuaninty;
            opt.setAttributeNode(UnitQuaninty);

            $("#DrugListSelect").append(opt);
        }
    }
    AddUnListedDrugs();
    $("#DrugListSelect").select2();
    //Restrict Claim datepicker
    $("#ClaimDate").datepicker({     
        dateFormat: 'dd-mm-yy',
        minDate: -15,
        maxDate: 0,
    });

}

// Add Table Manual from click add new drug
function AddUnListedDrugs() {
    $('[data-toggle="tooltip"]').tooltip();
    var actions = $("table td:last-child").html();

    // Append table with add row form on add new button click
    $(".add-new").click(function () {
        $(this).attr("disabled", "disabled");
        var index = $("table tbody tr:last-child").index();
        var row = '<tr class="ClearRows">' +
            '<td><input type="text" class="form-control DrugName" value="-1" name="DrugName" id="DrugName" ></td>' +
            '<td><input type="text" class="form-control DrugID" name="DrugID" id="DrugID"></td>' +
            '<td><input type="text" class="form-control Quantity" name="Quaninty" id="Quaninty"></td>' +
            '<td><input type="text" class="form-control UnitQuaninty" name="UnitQuaninty" id="UnitQuaninty" onkeypress="return isNumberKey(event)"></td>' +
            '<td><input type="text" class="form-control DemandedQuantity" name="DemandedQuantity" id="DemandedQuantity" onkeypress="return isNumberKey(event)"></td>' +
            '<td><input type="text" class="form-control price" name="priceID" onkeypress="return"></td>' +
            '<td><input type="text" class="form-control TotalPrice" name="TotalPrice" id="TotalPrice" disabled></td>' +
            '<td><input type="text" class="form-control Coprice" name="Coprice" id="Coprice" disabled></td>' +
            '<td><input type="text" class="form-control Listed" value="UnListed" name="Listed" id="Listed" readonly></td>' +
            '<td>' + actions + '</td>' +
            '</tr>';
        $("table").append(row);
        $("table tbody tr").eq(index + 1).find(".add, .edit").toggle();
        $('[data-toggle="tooltip"]').tooltip();

    });

    // Add row on add button click
    $(document).on("click", ".add", function () {
        var empty = false;
        var input = $(this).parents("tr").find('input[type="text"]');
        input.each(function () {
            if (!$(this).val()) {
                $(this).addClass("error");
                empty = true;
            } else {
                $(this).removeClass("error");
            }
        });
        $(this).parents("tr").find(".error").first().focus();
        if (!empty) {
            input.each(function () {
                $(this).parent("td").html($(this).val());
            });

          //  $(this).parents("tr").find(".add, .edit").toggle();
            $(".add").hide();
            $(".add-new").removeAttr("disabled");
        }
        //display Submit button untill enter Demanded Quantity
        var x = $("#DemandedQuantity");
        $("#DemandedQuantity").blur(
            blurfunction()
        );
    });


            //// Edit row on edit button click
    //$(document).on("click", ".edit", function () {
    //    debugger;
    //    $(this).parents("tr").find("td:not(:last-child)").each(function () {
    //        $(this).html('<input type="text" class="form-control" value="' + $(this).text() + '">');
    //    });
    //    $(this).parents("tr").find(".add, .edit").toggle();
    //    $(".add-new").attr("disabled", "disabled");
    //});



    // Delete row on delete button click
    $(document).on("click", ".delete", function () {
        debugger;
        $(this).parents("tr").remove();
        $(".add-new").removeAttr("disabled");
        IntialaizeTotalCalculators();
        ClaculateAllTotalPrice();
    });

}

//display Submit button untill enter Demanded Quantity
function blurfunction() {
    if ($("#DemandedQuantity").val() !== "") {
        $('#SubmitRequest').removeAttr("disabled");
    } else {
        $('#SubmitRequest').attr("disabled", "true");
    }
}

//Append Data in Drug Details table from Auto Fill Search 
function AppendRow() {

    var SelectedItem = $("#select2-DrugListSelect-container");
    var ID = $('#DrugListSelect').find(":selected")[0].attributes[0].value;
    var Quantity = $('#DrugListSelect').find(":selected")[0].attributes[1].value;
    var UnitQuantity = $('#DrugListSelect').find(":selected")[0].attributes[2].value;
    var response = { DrugName: SelectedItem[0].innerText, DrugID: ID, Quaninty: Quantity, UnitQuaninty: UnitQuantity };
    var Table = $("#TableBody1");
    Table.append("<tr class='ClearRows'><td>" + response.DrugID + "</td><td>" + response.DrugName + "</td><td>" + response.Quaninty + "</td><td>" + response.UnitQuaninty +
        "</td><td><input type='text' class='form-control DemandedQuantity' name='DemandedQuantity' onkeypress='return isNumberKey(event)'></td><td><input type='text' class='form-control' name='priceID' onkeypress='return'></td><td><input type='text' class='form-control TotalPrice' id='TotalPrice' name='TotalPrice' disabled></td><td><input type='text' class='form-control Coprice' id='Coprice' name='Coprice' disabled></td><td>Listed</td><td><a class='add' id='aaaa' title='Add' data-toggle='tooltip'><i class='material-icons'>&#xE03B;</i></a><a class='edit' title='Edit' data-toggle='tooltip'><i class='material-icons'>&#xE254;</i></a><a class='delete' title='Delete' data-toggle='tooltip'><i class='material-icons'>&#xE872;</i></a></td></tr>");

    $(".add").show();
    $(" .edit").hide();


}

//Force only numbers
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

//update Co-Price after update Co-InsurancePercentage 
$("#CoInsurancePercentage").change(function () {
    UpdateCoprice();
    CalculateTotalPrice();
});

function UpdateCoprice() {
    var DrugTable = document.getElementById('DrugTable');
    //gets rows of table
    var rowLength = DrugTable.rows.length;
    var DemandedDrugs = [];

    //loops through rows
    for (i = 2; i < rowLength; i++) {

        //gets cells of current row
        var DrugTableCell = DrugTable.rows.item(i).cells;

        //gets amount of cells of current row
        var cellLength = DrugTableCell.length;
        var Totalprice = DrugTableCell[6].innerHTML;
        var Copercent = $("#CoInsurancePercentage").val();
        var coprice = Totalprice * (Copercent / 100);
        DrugTableCell[7].innerHTML = coprice;
    }
    ClaculateAllTotalPrice();
}

// Calculate Total Amount
var ClaculateTotalPrice = 0;
var ClaculateTotalCoPrice = 0;
var ClaculateTotalApprovalPrice = 0;

$(document).on("click", ".add", function () {
    ClaculateAllTotalPrice();
});

function ClaculateAllTotalPrice() {
    IntialaizeTotalCalculators();
    var DrugTable = document.getElementById('DrugTable');
    var CoPercentage = $("#CoInsurancePercentage").val();
    //gets rows of table
    var rowLength = DrugTable.rows.length;
    for (i = 2; i < rowLength; i++) {
        //gets cells of current row
        var DrugTableCell = DrugTable.rows.item(i).cells;
        var TotalPrice = DrugTableCell[6].firstChild.value;
        if (TotalPrice == undefined) {
            TotalPrice = DrugTableCell[6].firstChild.data;
        }


        var tempTotal = ClaculateTotalPrice;
        var temptotalprice = Number(TotalPrice);
        if (!isNaN(TotalPrice)) {
            ClaculateTotalPrice = tempTotal + temptotalprice;
        }
    }
    ClaculateTotalCoPrice = (ClaculateTotalPrice * (CoPercentage / 100));
    ClaculateTotalApprovalPrice = ClaculateTotalPrice - ClaculateTotalCoPrice;

    document.getElementById("TotalPayments").innerHTML = ClaculateTotalPrice
    document.getElementById("TotalApprovalPrice").innerHTML = ClaculateTotalApprovalPrice
    document.getElementById("TotalMemberPays").innerHTML = ClaculateTotalCoPrice
}

//Calculate price of Drug
$('#DrugTable').on('change', 'input', function CalculateTotalPrice() {
    debugger;
    var row = $(this).closest('tr');

    if (row["context"].name == "priceID") {
        var PriceValue = row["context"].value;
        var DemandedQuan = row[0].cells[4].firstChild.value;
        var UnitQuan;
        if (row[0].cells[3].firstChild.value == undefined) {
            UnitQuan = row[0].cells[3].innerHTML;
        }
        else {
            UnitQuan = row[0].cells[3].firstChild.value;
        }
        var Percent = $("#CoInsurancePercentage").val();
        var TotalPrice = (DemandedQuan / UnitQuan) * PriceValue;
        var Coprice = TotalPrice * (Percent / 100);
        $("#TotalPrice", row).val(TotalPrice);
        $("#Coprice", row).val(Coprice);
    }
});

//Send Request
$('#SubmitRequest').click(function () {

    var validator;
    var checked = $("input:checked").length > 0;
    var date = $("#ClaimDate").val();
       //validate for Empty input Text
    $('input[type="number"],input[type="tel"],input[type="date"],#ClaimDate,#Diagnose,#ClientName').each(
        function () {
            if ($.trim($(this).val()) === '') {
                var x= $(this);
                $(this).css({
                    "border": "1px solid red",
                    "background": "#FFCECE"
                });
                $("html, body").animate({ scrollTop: 0 }, "slow");
                validator = false;
            }
            else {
                validator = true;
                $(this).css({
                    "border": "",
                    "background": ""
                });
            }
        });
    if (checked != true) {
        alert("Please check at least one checkbox");
        $("html, body").animate({ scrollTop: 0 }, "slow");
        validator = false;
    }
    if (date == "") {
        alert("Please Enter Claim Date");
        $("html, body").animate({ scrollTop: 0 }, "slow");
        validator = false;
    }
    //Check for Medical OR Financial Approval

    if (validator === true) {

        var MedicalApproval;
        if ($("#MedicalApproval").prop("checked")) {
            MedicalApproval = true;
        } else {
            MedicalApproval = false;
        }

        var FinancialApproval;
        if ($("#FinancialApproval").prop("checked")) {
            FinancialApproval = true;
        } else {
            FinancialApproval = false;
        }
        var x = $("#TotalPayments");

        //Object of main data ..........................
        var RequestObj = {
            MedicalID: $("#MedicalID").val(),
            MemberName: $("#MemberName").val(),
            ClaimNumber: $("#ClaimNumber").val(),
            MobileNumber: $("#PhoneNumber").val(),
            ClientName: $("#ClientName").val(),
            CoInsurancePerc: $("#CoInsurancePercentage").val(),
            ClaimDate: $("#ClaimDate").val(),
            MedicalApproval: MedicalApproval,
            FinnacialApproval: FinancialApproval,
            Diagnose: $("#Diagnose").val(),
            Note: $("#Note").val(),
            TotalPayments: $("#TotalPayments")[0].innerHTML,
            TotalApprovalPrice: $("#TotalApprovalPrice")[0].innerHTML,
            TotalMemberPays: $("#TotalMemberPays")[0].innerHTML,
        };

        //Object of Drugs .......................
        var DemandedDrugsObj = [];

        //gets table by Id
        var DrugTable = document.getElementById('DrugTable');

        //gets rows of table
        var rowLength = DrugTable.rows.length;
        var DemandedDrugs = [];

        //loops through rows
        for (i = 2; i < rowLength; i++) {

            //gets cells of current row
            var DrugTableCell = DrugTable.rows.item(i).cells;

            //gets amount of cells of current row
            var cellLength = DrugTableCell.length;

            var CheckListed;
            if (DrugTableCell[8].innerHTML === "Listed") {
                CheckListed = true;
            }
            else {
                CheckListed = false;
            }

            var DrugIDUnlisted;
            if (DrugTableCell[0].innerHTML === -1) {
                DrugIDUnlisted = null;
            }
            else {
                DrugIDUnlisted = DrugTableCell[0].innerHTML;
            }


            $("#dare_price").change(function () {
                var total;
                $('#total_price').val(total);
            });

            var DrugsDetailDTO = {
                DrugID: DrugIDUnlisted,
                DrugName: DrugTableCell[1].innerHTML,
                Quaninty: DrugTableCell[2].innerHTML,
                UnitQuantity: DrugTableCell[3].innerHTML,
                DemandedQuantity: DrugTableCell[4].innerHTML,
                UnitPrice: DrugTableCell[5].innerHTML,
                TotalPrice: DrugTableCell[6].innerHTML,
                Coprice: DrugTableCell[7].innerHTML,
                Listed: CheckListed
            };
            DemandedDrugs.push(DrugsDetailDTO);
        }
        var RequestToSend = {
            Request: RequestObj,
            DemandedDrugs
        };
        var RequestData = JSON.stringify(RequestToSend);
        var url = "/Request/OpenNewRequest/";
        Common.Ajax('POST', url, RequestToSend, 'json', OnRequestSuccess);
    }
});

function OnRequestSuccess(data) {
    if (data > 0) {
        var url = '/Request/AddRequestAttach?tt=' + data;
        var formData = new FormData();
        var totalFiles = document.getElementById('FileUploader').files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementsByClassName('MyUploader')[0].files[i];
            formData.append('FileUploader', file, file.name);
        }
        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (result) {
                swal({
                    title: "Request Sent Successfully with ID " + data,
                    type: "success",
                    text: "Request Sent Successfully with ID " + data,
                    confirmButtonColor: "#2196F3"
                });
                ClearInputFields();            
                LoadPage();
            }
        });
    }
    else if (data === -1) {
        alert("This Claim already Exist");
    }

}

function ClearInputFields() {
    $(".ClearInputFields").val('');
    $('.ClearCheckBoxes').attr('checked', false);
    $(".ClearRows").remove();
    document.getElementById('TotalMemberPays').innerText = "";
    document.getElementById('TotalPayments').innerText = "";
    document.getElementById('TotalApprovalPrice').innerText = "";
}

//Co-Insurance Percentage More than 100
$('#CoInsurancePercentage').keyup(function () {
    if ($(this).val() > 100) {
        alert("No numbers above 100");
        $(this).val('100');
    }
});

//CoInsurance Percentage less than 0
$('#CoInsurancePercentage').keyup(function () {
    if ($(this).val() < 0) {
        alert("No numbers less 0");
        $(this).val('0');
    }
});

//Validation Medical ID digits 
$("#MedicalID").on("blur", function () {
    var mobNum = $(this).val();
    var filter = /^\d*(?:\.\d{1,2})?$/;

    if (filter.test(mobNum)) {
        if (mobNum.length > 10) {
            alert("Medical ID not allow more than 10");
            $("#mobile-valid").removeClass("hidden");
            $("#folio-invalid").addClass("hidden");
            $("#MedicalID").focusin();
            mobNum = $(this).val('');
        }


        else if (mobNum.length < 5) {
            alert('Please put more than 5 Numbers For Medical ID');
            $("#folio-invalid").removeClass("hidden");
            $("#mobile-valid").addClass("hidden");
            $("#MedicalID").focusin();
            mobNum = $(this).val('');
            //return false;
        }
    }
});

function IntialaizeTotalCalculators() {
    ClaculateTotalPrice = 0;
    ClaculateTotalCoPrice = 0;
    ClaculateTotalApprovalPrice = 0;
}

