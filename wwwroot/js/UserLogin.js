$(function () {
    var loginUserButton = $("#UserLoginModal button[name='login']").click(onUserLoginClick);
    function onUserLoginClick() {
        var url = "UserAuth/Login";

        var antiForgeryToken = $("#UserLoginModal input[name='__RequestVerificationToken']").val();

        var username = $("#UserLoginModal input[name='Username']").val();
        var password = $("#UserLoginModal input[name='Password']").val();

        var userInput = {
            __RequestVerificationToken: antiForgeryToken,
            Username: username,
            Password: password
        };

        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: function (data) {
                var parsed = $.parseHTML(data);
                var hasErrors = $(parsed).find("input[name='LoginInValid']").val() == "true";

                if (hasErrors == true) {
                    $("#UserLoginModal").html(data);

                    loginUserButton = $("#UserLoginModal button[name='login']").click(onUserLoginClick);

                    var form = $("#UserLoginForm");

                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);
                } else {
                    location.href = "Home/Index";
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
                PresentCloseableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);
                console.error(thownError + "\r\n" + xhr.status + "\r\n" + xhr.responseText);
            }
        });
    }
});