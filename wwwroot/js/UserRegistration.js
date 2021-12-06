$(function () {
    $("#UserRegistrationModal input[name='Username']").blur(function () {
        var username = $("#UserRegistrationModal input[name='Username']").val();

        var url = "UserAuth/UserNameExists?username=" + username;

        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                if (data == true) {
                    PresentCloseableBootstrapAlert("#alert_placeholder_register", "warning", "Invalid username!", "This username is already taken.");
                } else {
                    CloseAlert("#alert_placeholder_register");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
                PresentCloseableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });
    }
    );

    var registrationButton = $("#UserRegistrationModal button[name='register']").click(onUserRegisterClick);

    function onUserRegisterClick() {
        var url = "UserAuth/Register";

        var antiForgeryToken = $("#UserRegistrationModal input[name='__RequestVerificationToken']").val();

        var username = $("#UserRegistrationModal input[name='Username']").val();
        var password = $("#UserRegistrationModal input[name='Password']").val();
        var confirmPassword = $("#UserRegistrationModal input[name='ConfirmPassword']").val();
        var email = $("#UserRegistrationModal input[name='Email']").val();
        var firstName = $("#UserRegistrationModal input[name='FirstName']").val();
        var lastName = $("#UserRegistrationModal input[name='LastName']").val();

        var userInput = {
            __RequestVerificationToken: antiForgeryToken,
            Username: username,
            Password: password,
            ConfirmPassword: confirmPassword,
            Email: email,
            FirstName: firstName,
            LastName: lastName
        };

        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: function (data) {
                var parsed = $.parseHTML(data);
                var hasError = $(parsed).find("input[name='RegistrationInValid']").val() == "true";

                if (hasError) {
                    $("#UserRegistrationModal").html(data);
                    var registrationButton = $("#UserRegistrationModal button[name='register']").click(onUserRegisterClick);

                    var form = $("#UserRegistrationForm");

                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);
                } else {
                    location.href = "Home/Index";
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
                PresentCloseableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });
    }
});