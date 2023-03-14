//Popup Add User
var addAcademicYearModal = document.getElementById("addAcademicYearModal");
var addAcademicYearBtn = document.getElementById("addAcademicYearBtn");
var addAcademicYearSpan = document.getElementById("close_addAcademicYearModal");
var submitAcademicYearBtn = document.getElementById("submit-btn");
const createAcademicYearForm = document.getElementById('create_academic_year_form');

addAcademicYearBtn.onclick = function () {
    addAcademicYearModal.style.display = "block";
}
addAcademicYearSpan.onclick = function () {
    addAcademicYearModal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == addAcademicYearModal) {
        addAcademicYearModal.style.display = "none";
    }
}

submitAcademicYearBtn.addEventListener('click', function(event) {
    const closureDate = document.getElementById('closure_date');
    const finalClosureDate = document.getElementById('final_closure_date');
    const openDate = document.getElementById('open_date');

    const submitButton = document.getElementById('submitButton');
    // Prevent the form from submitting by default
    event.preventDefault();

    const closure = new Date(closureDate.value);
    const finalClosure = new Date(finalClosureDate.value);
    const open = new Date(openDate.value);

    if (!validateClosureDates(open, closure, finalClosure)) return;

    createAcademicYearForm.submit();
});



// Edit Section --
var editAcademicYearModal = document.getElementById("editAcademicYearModal");
var editAcademicYearBtn = document.getElementsByClassName("editAcademicYearBtn");
var editAcademicYearSpan = document.getElementById("close_editAcademicYearModal");

window.onclick = function (event) {
    if (event.target == editAcademicYearModal) {
        editAcademicYearModal.style.display = "none";
    }
}

editAcademicYearSpan.onclick = function () {
    editAcademicYearModal.style.display = "none";
}

var editAcademicYearId;

// Input fields
const editName = document.getElementById('edit_name');
const editClosureDate = document.getElementById('edit_closure_date');
const editFinalClosureDate = document.getElementById('edit_final_closure_date');
const editOpenDate = document.getElementById('edit_open_date');

// Form, btn
var editAcademicYearSubmitBtn = document.getElementById("submit-edit-btn");

// Edit Section End --
editAcademicYearSubmitBtn.addEventListener('click', function(event) {
    // Prevent the form from submitting by default
    event.preventDefault();
    if (!validateClosureDates(editOpenDate.value, editClosureDate.value, editFinalClosureDate.value)) return;
    const requestBody = {
        name: editName.value,
        openDate: editOpenDate.value,
        closureDate: editClosureDate.value,
        finalClosureDate: editFinalClosureDate.value,
    };
    
    const requestUrl = `${window.location.origin}/academic-year/${editAcademicYearId}`;
    $.ajax({
        url: requestUrl,
        method: 'PUT',
        data: JSON.stringify(requestBody),
        contentType: 'application/json',
        success: function(data) {
            console.log('Academic year updated successfully');
            window.location.reload();
        },
        error: function(jqXHR, textStatus, errorThrown) {
            alert('Error updating academic year');
        }
    });
 
});

async function openEditModal(id)
{
    var academicYear = await getAcademicYearById(id)
    editAcademicYearId = id;
    editName.setAttribute("value", academicYear.name);
    editClosureDate.setAttribute("value", academicYear.closureDate);
    editFinalClosureDate.setAttribute("value", academicYear.finalClosureDate);
    editOpenDate.setAttribute("value", academicYear.openDate);

    editAcademicYearModal.style.display = "block";
}




// Delete Section --
function deleteAcademicYear(id) {
    var confirmResult = confirm("Are you sure?");
    if (!confirmResult)
        return;

    $.ajax({
        url: window.location.origin + '/academic-year/' + id,
        type: 'DELETE',
        success: function () {
            window.location.reload();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}
// Delete Section End --

// Extension methods --
function getAcademicYearById(id)
{
    return new Promise((resolve, reject) => {
        $.ajax({
            url: window.location.origin + '/academic-year/' + id,
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

function validateClosureDates(open, closure, finalClosure) {
    if (open < Date.now()) {
        alert('Open date must be greater than today');
        return false;
    }

    if (closure <= open) {
        alert('Closure date must be greater than Open date');
        return false;
    }

    if (finalClosure <= closure) {
        alert('Final date must be greater than Closure date');
        return false;
    }

    if (finalClosure <= open) {
        alert('Final date must be smaller than Open date');
        return false;
    }

    return true;
}
// Extension methods End --