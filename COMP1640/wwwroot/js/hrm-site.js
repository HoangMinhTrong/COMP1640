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
        success: function (data) {
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
var editUserModal = document.getElementById("editUserModal");
var editUserBtn = document.getElementsByClassName("editUserBtn");
var editUserSpan = document.getElementsByClassName("close")[1];

function EditUserInfo() {
    var userId = $(".info-userId").val();
    var myObject = {
        Name: $(".info-username").val(),
        Email: $(".info-email").val(),
        RoleId: $(".info-role").val(),
        DepartmentId: $(".info-department").val(),
        Gender: $(".info-gender").val(),
        Birthday: $(".info-birthday").val(),
    };

    $.ajax({
        url: window.location.origin + '/hrm/user/' + userId,
        type: 'PUT',
        data: JSON.stringify(myObject),
        contentType: 'application/json',
        success: function () {
            alert('Saved successfully.');
            window.location.reload();
        }
    });
}

function ViewUserDetail(id) {
    $.ajax({
        url: window.location.origin + '/hrm/user/' + id,
        type: 'GET',
        success: function (user) {
            editUserModal.style.display = "block";
            fillDropDownListForEditAccount(user.roleId, user.departmentId)
            $(".info-userId").val(user.id);
            $(".info-username").val(user.userName);
            $(".info-email").val(user.email);
            $(".gender-" + user.gender).prop("selected", true);
            $(".info-birthday").val(user.birthday);
        }
    });
}

function fillDropDownListForEditAccount(roleId, departmentId) {
    $('#roles_list_edit option:not(:first)').remove();
    $('#departments_list_edit option:not(:first)').remove();
    $.ajax({
        url: window.location.origin + '/hrm/role',
        type: "GET",
        dataType: "JSON",
        data: "",
        success: function (data) {
            var roles = data.roles;
            var departments = data.departments;
            $.each(roles, function (i, role) {
                $("#roles_list_edit").append(
                    $('<option></option>').val(role.id).html(role.name).prop("selected", roleId == role.id)
                );
            });
            $.each(departments, function (i, department) {
                $("#departments_list_edit").append(
                    $('<option></option>').val(department.id).html(department.name).prop("selected", departmentId == department.id)
                );
            });
        }
    })
}

editUserSpan.onclick = function () {
    editUserModal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == editUserModal) {
        editUserModal.style.display = "none";
    }
}

//Delete User
function DeleteUser(id) {
    var confirmResult = confirm("Are you sure you want to delete this item?");
    if (!confirmResult)
        return;

    $.ajax({
        url: window.location.origin + '/hrm/' + id,
        type: 'DELETE',
        success: function () {
            window.location.reload();
        },
    });
}
