var addIdeaPopup = document.getElementById("createIdeaPopup");

function ShowPopupCreateIdea() {
    addIdeaPopup.style.display = "block";
    getCategoriesForCreateIdea();
}


function getCategoriesForCreateIdea() {
    $('#category_list option:not(:first)').remove();
    $.ajax({
        url: window.location.origin + '/idea/categories-selection',
        type: "GET",
        dataType: "JSON",
        data: "",
        success: function (categories) {
            $.each(categories, function (i, category) {
                $("#category_list").append(
                    $('<option></option>').val(category.id).html(category.name));
            });
        }
    })
}


function CloseAddIdeaPopup() {
    addIdeaPopup.style.display = "none";
}

//Soft Delete Idea
function ToggleSoftDeleteIdea(id) {
    var confirmResult = confirm("Are you sure?");
    if (!confirmResult)
        return;

    $.ajax({
        url: window.location.origin + '/idea/softdelete/' + id,
        type: 'PUT',
        success: function () {
            window.location.reload();
        },
    });
}