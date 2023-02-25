
var viewPersonalModal = document.getElementById("personal-profile");


function ViewPersonalDetail() {
    $.ajax({
        url: window.location.origin + '/personal/profile',
        type: 'GET',
        success: function (user) {
            viewPersonalModal.style.display = "block";
            $(".profile-email").text(user.email);
            $(".profile-role").text(user.role);
            $(".profile-gender").text(user.gender);
            $(".profile-phonenumber").text(user.phonenumber);
            $(".profile-username").text(user.userName);
            $(".profile-department").text(user.deprtment);

        }
    });
}

window.onclick = function (event) {
   viewPersonalModal.style.display = "none";
}