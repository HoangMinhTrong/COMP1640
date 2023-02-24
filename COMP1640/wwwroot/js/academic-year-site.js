//Popup Add User
var addAcademicYearModel = document.getElementById("addAcademicYearModal");
var addAcademicYearBtn = document.getElementById("addAcademicYearBtn");
var addAcademicYearSpan = document.getElementsByClassName("close")[0];
var submitAcademicYearBtn = document.getElementById("submit-btn");
const createAcademicYearForm = document.getElementById('create_academic_year_form');

addAcademicYearBtn.onclick = function () {
    addAcademicYearModel.style.display = "block";
}
addAcademicYearSpan.onclick = function () {
    addAcademicYearModel.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == addAcademicYearModel) {
        addAcademicYearModel.style.display = "none";
    }
}

submitAcademicYearBtn.addEventListener('click', function(event) {
    const closureDate = document.getElementById('closure_date');
    const finalClosureDate = document.getElementById('final_closure_date');
    const endDate = document.getElementById('end_date');

    const submitButton = document.getElementById('submitButton');
    // Prevent the form from submitting by default
    event.preventDefault();

    const closure = new Date(closureDate.value);
    const finalClosure = new Date(finalClosureDate.value);
    const end = new Date(endDate.value);

    validateClosureDates(closure, finalClosure, endDate);

    createAcademicYearForm.submit();
});

// Edit
// Popup Edit
var editAcademicYearModal = document.getElementById("editAcademicYearModal");
var editAcademicYearBtn = document.getElementsByClassName("editAcademicYearBtn");
var editAcademicYearSpan = document.getElementsByClassName("close")[0];

var editAcademicYearId;

// Input fields
const editName = document.getElementById('edit_name');
const editClosureDate = document.getElementById('edit_closure_date');
const editFinalClosureDate = document.getElementById('edit_final_closure_date');
const editEndDate = document.getElementById('edit_end_date');

// Form, btn
var editAcademicYearSubmitBtn = document.getElementById("submit-edit-btn");
var editAcademicYearForm = document.getElementById("edit_academic_year_form");


// handle
editAcademicYearSpan.onclick = function () {
    editAcademicYearModal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == editAcademicYearModal) {
        editAcademicYearModal.style.display = "none";
    }
}

editAcademicYearSubmitBtn.addEventListener('click', function(event) {
    // Prevent the form from submitting by default
    event.preventDefault();
    
    const requestBody = {
        name: editName.value,
        closureDate: editClosureDate.value,
        finalClosureDate: editFinalClosureDate.value,
        endDate: editEndDate.value,
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
    editClosureDate.setAttribute("value", new Date(academicYear.closureDate).toISOString().split('T')[0]);
    editFinalClosureDate.setAttribute("value", new Date(academicYear.finalClosureDate).toISOString().split('T')[0]);
    editEndDate.setAttribute("value", new Date(academicYear.endDate).toISOString().split('T')[0]);

    editAcademicYearModal.style.display = "block";
}


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

function validateClosureDates(closure, finalClosure, end) {
    if (closure <= Date.now()) {
        alert('Closure date must be greater than today');
        return false;
    }

    if (closure >= finalClosure) {
        alert('Closure date must be before Final Closure date');
        return false;
    }

    if (finalClosure >= end) {
        alert('Final Closure date must be before End date');
        return false;
    }

    return true;
}
