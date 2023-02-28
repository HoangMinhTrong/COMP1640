//Popup Add Category
var addDepartmentModal = document.getElementById("addDepartmentModal");
var addDepartmentBtn = document.getElementById("addDepartmentBtn");
var addDepartmentSpan = document.getElementsByClassName("close")[0];
console.log(addDepartmentModal)
addDepartmentBtn.onclick = function () {
    addDepartmentModal.style.display = "block";
}
addDepartmentSpan.onclick = function () {
    addDepartmentModal
}
window.onclick = function (event) {
    if (event.target == addDepartmentModal) {
        addDepartmentModal.style.display = "none";
    }
}

