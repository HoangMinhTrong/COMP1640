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

document.getElementById('anonymousCheckbox').addEventListener('change', function() {
    var hiddenInput = document.querySelector('input[name="isAnonymous"]');
    hiddenInput.value = this.checked ? 'true' : 'false';
});