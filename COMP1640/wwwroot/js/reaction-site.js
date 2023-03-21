
function React(event, ideaId, status)
{
    event.preventDefault();
    event.stopPropagation();
    var thumbsUpIcon = document.getElementById("thumbupBtn-" + ideaId);
    var thumbsDownIcon = document.getElementById("thumbdownBtn-" + ideaId);
    var totalThumbsUp = document.getElementById("total-thumbs-up-" + ideaId)
    var totalThumbsDown = document.getElementById("total-thumbs-down-" + ideaId)


    const requestBody = {
        ideaId: ideaId,
        reactionStatusEnum: status
    };
 

    // Send API request
    $.ajax({
        url: window.location.origin + "/reaction",
        type: 'POST',
        data: JSON.stringify(requestBody),
        contentType: 'application/json',
        success: function (data) {  
            responseStatus = data.status

            switch (responseStatus) {
                case 1:
                    thumbsUpIcon.className = "fas fa-thumbs-up me-2";
                    thumbsDownIcon.className = "far fa-thumbs-down me-2"
                    break;
                case 2:
                    thumbsUpIcon.className = "far fa-thumbs-up me-2";
                    thumbsDownIcon.className = "fas fa-thumbs-down me-2";
                    break;
                default:
                    thumbsUpIcon.className = "far fa-thumbs-up me-2";
                    thumbsDownIcon.className = "far fa-thumbs-down me-2";
                    break;
            }

            totalThumbsUp.innerHTML = data.totalLike;
            totalThumbsDown.innerHTML = data.totalDisLike;
        }
    });
}

document.getElementById('anonymousCheckbox').addEventListener('change', function () {
    var hiddenInput = document.querySelector('input[name="isAnonymous"]');
    hiddenInput.value = this.checked ? 'true' : 'false';
});
