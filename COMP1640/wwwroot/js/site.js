document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.nav-link').forEach(link => {
        if (link.getAttribute('href').toLowerCase() === location.pathname.toLowerCase()) {
            link.classList.add('active');
        } else {
            link.classList.remove('active');
        }
    });
})


var activateAccountPopup = document.getElementById("activate-account");
function ChangePassword() {
    var myObject = {
        NewPassword: $(".new-password").val(),
        ConfirmPassword: $(".confirm-password").val(),
    };

    if (myObject.NewPassword != myObject.ConfirmPassword) {
        alert('Your confirm password is incorrect !!!');
        return;
    }

    $.ajax({
        url: window.location.origin + '/personal/change-password',
        type: 'PUT',
        data: JSON.stringify(myObject),
        contentType: 'application/json',
        success: function () {
            alert('Change Password successfully.');
            activateAccountPopup.style.display = "none";
            window.location.reload();
        }
    });
}

function clearFilters() {
    document.getElementsByName("CategoryFilterOption")[0].value = null;
    document.getElementsByName("sortOption")[0].value = null;
    document.querySelector("form").submit();
}