
//Modal Versioning
var versioningModal = document.getElementById("versioningModal");
var versioningBtn = document.getElementsByClassName("versioningBtn");
var versioningSpan = document.getElementsByClassName("close")[1];

function ViewVersioningIdea(event, id) {
    event.stopPropagation();
    $.ajax({
        url: window.location.origin + '/idea/history?ideaId=' + id,
        method: "GET",
        success: function (response) {
            versioningModal.style.display = "block";
            let ideaVersioning = "";
            // Iterate over the response data using forEach
            response.forEach(function (history) {
                ideaVersioning += `
                <div class="d-flex flex-column">
                    <div class="col mb-4">
                        <div class="card shadow-sm">
                            <div class="card-body px-3 py-3 px-md-3">
                                <h5 class="fw-bold text-success card-title">Title: ${history.title}</h5>
                                <div class="d-flex">
                                    <div>
                                        <p class="me-2">Category: ${history.category}</p>
                                        <p class="me-2">Content: ${history.content}</p>
                                        <p class="me-2">Created On: ${history.createdOn}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
            });
            $("#Idea-versioning").html(ideaVersioning);
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

versioningSpan.onclick = function (event) {
    event.stopPropagation();
    versioningModal.style.display = "none";
}

window.onclick = function (event) {
    event.stopPropagation();
    if (event.target == versioningModal) {
        versioningModal.style.display = "none";
    }
}