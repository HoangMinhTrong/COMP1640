async function GiveThumbUp(ideaId) {
    var currentStatus = await CheckCurrentStatusBeforeAction(ideaId);

    // Not give thumb up yet
    if (currentStatus.status == 0) {
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

    // Give thumb up already
    if (currentStatus.status == 1) {
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
    // Give thumb down already
    if (status == 2) alert("You disliked this post. Please undisliked first");

}

async function GiveThumbDown(ideaId) {
    var currentStatus = await CheckCurrentStatusBeforeAction(ideaId);

    // Not give thumb down yet
    if (currentStatus.status == 0) {
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

    // Give thumb up already
    if (currentStatus.status == 1) alert("You liked this post. Please unliked first");

    // Give thumb down already
    if (currentStatus.status == 2) {
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


function React(event ,ideaId, status)
{
    const requestBody = {
        ideaId: ideaId,
        reactionStatusEnum: status
    };
    event.preventDefault();
    $.ajax({
        // Ajax =>  HandleReact(ideaId, status)
        url: window.location.origin, // TODO: Correct url
        type: 'POST',
        data: JSON.stringify(requestBody),
        contentType: 'application/json',
        success: function (data) {
            window.location.reload();
        }
    });
   
    
}
// Check status function
function CheckCurrentStatusBeforeAction(ideaId) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: window.location.origin + '/reaction/checkstatus/' + ideaId,
            type: "GET",
            dataType: "JSON",
            data: "",
            success: function (data) {
                resolve(data)
            },
            error: function (error) {
                reject(error)
            }
        })
    })
}