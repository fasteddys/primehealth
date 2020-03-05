
var SaveDepartmentBTN = function () {

    var NewDepartmentName = $("#AddDepartment").val();
    var NewDepartment = {
        DepartmentName: NewDepartmentName,
    }
    var newDept = NewDepartment;
    var urlGetNewDepartment = ConfigurationData.GlobalApiURL+"/Department/AddNewDepartment";
    Common.Ajax('Post', urlGetNewDepartment, JSON.stringify(newDept), 'json', SucessAddNewDepartment, false);
}

function SucessAddNewDepartment(Result) {
    if (Result.Success === true) {
        $("#AddDepartmentModal").modal("hide");
        ShowALert(2, Result.Message);
        GetAllDepartment();
    }
    else {
        ShowALert(4, Result.Message);

    }
}