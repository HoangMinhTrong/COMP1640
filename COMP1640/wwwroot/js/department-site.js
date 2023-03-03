//Popup Add Category
var addDepartmentModal = document.getElementById("addDepartmentModal");
var addDepartmentBtn = document.getElementById("addDepartmentBtn");
var addDepartmentSpan = document.getElementsByClassName("close")[0];
console.log(addDepartmentModal)
addDepartmentBtn.onclick = function () {
    addDepartmentModal.style.display = "block";
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
        url: window.location.origin + '/department/',
        type: "GET",
        dataType: "JSON",
        data: "",
        success: function (data) {
            var qacoordinators = data.qacoordinators;
            $.each(qacoordinators, function (i, qacoordinator) {
                $("#qacoordinators_list").append(
                    $('<option></option>').val(qacoordinator.id).html(qacoordinator.name));
            });
        }
    })
}
