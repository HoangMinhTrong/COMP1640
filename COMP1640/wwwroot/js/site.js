//Popup Add User
var addUserModal = document.getElementById("addUserModal");
var addUserBtn = document.getElementById("addUserBtn");
var addUserSpan = document.getElementsByClassName("close")[0];
addUserBtn.onclick = function () {
    addUserModal.style.display = "block";
    fillDropDownListForCreateAccount();
}
addUserSpan.onclick = function () {
    addUserModal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == addUserModal) {
        addUserModal.style.display = "none";
    }
}
function fillDropDownListForCreateAccount() {
    $('#roles_list option:not(:first)').remove();
    $('#departments_list option:not(:first)').remove();
    $.ajax({
        url: window.location.origin + '/hrm/role',
        type: "GET",
        dataType: "JSON",
        data: "",
        success: function(data){
            var roles = data.roles; 
            var departments = data.departments;
            $.each(roles, function (i, role) {
                $("#roles_list").append(
                    $('<option></option>').val(role.id).html(role.name));
            });
            $.each(departments, function (i, department) {
                $("#departments_list").append(
                    $('<option></option>').val(department.id).html(department.name));
            });
        }
    })
}



//Popup Edit User
var editUserBtn = document.getElementsByClassName("editUserBtn");
var editUserModal = document.getElementById("editUserModal");
var editUserSpan = document.getElementsByClassName("close")[1];

for (var i = 0; i < editUserBtn.length; i++) {
    editUserBtn[i].onclick = function () {
        editUserModal.style.display = "block";
    }
}


editUserSpan.onclick = function () {
    editUserModal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == editUserModal) {
        editUserModal.style.display = "none";
    }
}
