//Popup Add User
var addUserModal = document.getElementById("addUserModal");
var addUserBtn = document.getElementById("addUserBtn");
var addUserSpan = document.getElementsByClassName("close")[0];
addUserBtn.onclick = function () {
    addUserModal.style.display = "block";
}
addUserSpan.onclick = function () {
    addUserModal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == addUserModal) {
        addUserModal.style.display = "none";
    }
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
