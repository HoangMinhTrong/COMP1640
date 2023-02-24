//Popup Add User
var addAcademicYearModel = document.getElementById("addAcademicYearModel");
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

    if (closure <= Date.now()) {
        alert('Closure date must be greater than today');
        return;
    }
    
    if (closure >= finalClosure) {
        alert('Closure date must be before Final Closure date');
        return;
    }
    if (finalClosure >= end) {
        alert('Final Closure date must be before End date');
        return;
    }

    createAcademicYearForm.submit();
});