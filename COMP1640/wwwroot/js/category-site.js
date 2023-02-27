//Popup Add Category
var addCategoryModal = document.getElementById("addCategoryModal");
var addCategoryBtn = document.getElementById("addCategoryBtn");
var addCategorySpan = document.getElementsByClassName("close")[0];
console.log(addCategoryModal)
addCategoryBtn.onclick = function () {
    addCategoryModal.style.display = "block";
}
addCategorySpan.onclick = function () {
    addCategoryModal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == addCategoryModal) {
        addCategoryModal.style.display = "none";
    }
}
//Delete User
function DeleteCategory(id) {
    var confirmResult = confirm("Are you sure you want to delete this category?");
    if (!confirmResult)
        return;

    $.ajax({
        url: window.location.origin + '/idea/category/' + id,
        type: 'PUT',
        success: function () {
            window.location.reload();
        },
    });
}