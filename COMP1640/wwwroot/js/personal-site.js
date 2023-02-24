


function ViewPersonalDetail() {
    $.ajax({
        url: window.location.origin + '/Personal/ViewProfile/',
        type: 'GET',
        success: function (user) {
            viewPersonalModal.style.display = "block";
            fillDropDownListForEditAccount(user.roleId, user.departmentId)
            $(".info-userId").val(user.id);
            $(".info-email").val(user.email);
            $(".gender-" + user.gender).prop("selected", true);
            $(".info-birthday").val(user.birthday);
        }
    });
}