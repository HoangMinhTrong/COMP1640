//Popup Add Category
var addDepartmentModal = document.getElementById("addDepartmentModal");
var addDepartmentBtn = document.getElementById("addDepartmentBtn");
var addDepartmentSpan = document.getElementsByClassName("close")[0];

addDepartmentBtn.onclick = function () {
    addDepartmentModal.style.display = "block";
    fillDropDownListForCreateDepartment();
}
addDepartmentSpan.onclick = function () {
    addDepartmentModal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == addDepartmentModal) {
        addDepartmentModal.style.display = "none";
    }
}

function fillDropDownListForCreateDepartment() {
    $('#qacoordinators_list option:not(:first)').remove();
    $.ajax({
        url: window.location.origin + '/department/coodinator-selection',
        type: "GET",
        dataType: "JSON",
        data: "",
        success: function (data) {
            var qacoordinators = data;
            $.each(qacoordinators, function (i, qacoordinator) {
                $("#qacoordinators_list").append(
                    $('<option></option>').val(qacoordinator.id).html(qacoordinator.name));
            });

        }
    })
}


//Delete Department
function DeleteDepartment(id) {
    var confirmResult = confirm("Are you sure you want to delete this Department?");
    if (!confirmResult)
        return;

    $.ajax({
        url: window.location.origin + '/department/' + id,
        type: 'PUT',
        success: function () {
            window.location.reload();
        },
    });
}


//Popup Edit Department
var editDepartmentModal = document.getElementById("editDepartmentModal");
var editDepartmentSpan = document.getElementsByClassName("close")[1];

editDepartmentSpan.onclick = function () {
    editDepartmentModal.style.display = "none";
}
function EditDepartmentInfo() {
    var departmentId = $(".info-departmentId").val();
    var myObject = {
        Name: $(".info-name").val(),
        qacoordinatorId: $(".info-coordinator").val(),
    };
    $.ajax({
        url: window.location.origin + '/department/edit/' + departmentId,
        type: 'PUT',
        data: JSON.stringify(myObject),
        contentType: 'application/json',
        success: function () {
            alert('Edit successfully.');
            window.location.reload()
        }
    });
}

function ViewDepartmentDetail(id) {
    $.ajax({
        url: window.location.origin + '/department/' + id,
        type: 'GET',
        success: function (department) {
            editDepartmentModal.style.display = "block";
            fillDropDownListForCreateDepartment();
            $(".info-departmentId").val(department.id);
            $(".info-name").val(department.name);
            fillDropDownListForEditDepartment(department.qacoordinatorId);
        }
    });
}

function fillDropDownListForEditDepartment(coordinatorId) {
    $('#qacoordinators_list_edit option:not(:first)').remove();
    $.ajax({
        url: window.location.origin + '/department/coodinator-selection',
        type: "GET",
        dataType: "JSON",
        data: "",
        success: function (data) {
            var coodinators = data;
            $.each(coodinators, function (i, coodinator) {
                $("#qacoordinators_list_edit").append(
                    $('<option></option>').val(coodinator.id).html(coodinator.name).prop("selected", coodinator.id == coordinatorId)
                );
            });
        }
    })
}