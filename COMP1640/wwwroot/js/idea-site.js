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
