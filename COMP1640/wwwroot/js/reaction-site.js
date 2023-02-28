function GiveThumbUp(ideaId) {
    $.ajax({
        url: window.location.origin + '/reaction/thumbup/' + ideaId,
        type: 'POST',
        data: "",
        contentType: 'application/json',
        success: function () {
            alert('Liked successfully.');
            window.location.reload();
        }
    });
}

function GiveThumbDown(ideaId) {
    $.ajax({
        url: window.location.origin + '/reaction/thumbdown/' + ideaId,
        type: 'POST',
        data: "",
        contentType: 'application/json',
        success: function () {
            alert('Disliked successfully.');
            window.location.reload();
        }
    });
}