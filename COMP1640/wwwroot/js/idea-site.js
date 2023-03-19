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


function ApproveIdea(id) {
    $.ajax({
        url: window.location.origin + '/idea/' + id + '/approve',
        type: 'PUT',
        success: function () {
            alert('Approve successfully.');
            window.location.reload();
        }
    });
}


function RejectIdea(id) {
    $.ajax({
        url: window.location.origin + '/idea/' + id + '/reject',
        type: 'PUT',
        success: function () {
            alert('Approve successfully.');
            window.location.reload();
        }
    });
}
