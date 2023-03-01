//function CheckStatus(ideaId) {
//     $.ajax({
//        url: window.location.origin + '/reaction/checkstatus/' + ideaId,
//        type: 'GET',
//        data: "",
//        contentType: 'application/json',
//        success: function (data) {
//            alert(data.status);
//            //return data.status
//        }
//    });
//}

function GiveThumbUp(ideaId) {
    $.ajax({
        url: window.location.origin + '/reaction/checkstatus/' + ideaId,
        type: 'GET',
        data: "",
        contentType: 'application/json',
        success: function (data) {
        var status = data.status;

        if (status == 0) {
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
            if (status == 1) {
                $.ajax({
                    url: window.location.origin + '/reaction/cancelthumbup/' + ideaId,
                    type: 'DELETE',
                    data: "",
                    contentType: 'application/json',
                    success: function () {
                        alert('Cancel Like successfully.');
                        window.location.reload();
                    }
                });
            }
        if (status == 2) alert("You disliked this post. Please undisliked first");
        }
    });
}

function GiveThumbDown(ideaId) {
    $.ajax({
        url: window.location.origin + '/reaction/checkstatus/' + ideaId,
        type: 'GET',
        data: "",
        contentType: 'application/json',
        success: function (data) {
            var status = data.status;
            if (status == 0) {
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
            if (status == 1) alert("You liked this post. Please unliked first");
            if (status == 2) {
                $.ajax({
                    url: window.location.origin + '/reaction/cancelthumbdown/' + ideaId,
                    type: 'DELETE',
                    data: "",
                    contentType: 'application/json',
                    success: function () {
                        alert('Cancel Dislike successfully.');
                        window.location.reload();
                    }
                });
            }
        }
    });
}