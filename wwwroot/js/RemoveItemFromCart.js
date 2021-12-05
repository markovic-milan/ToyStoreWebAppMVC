$(function () {
    var removeButtonClick = $(".my_link").click(removeItem);
    function removeItem() {
        var toyID = $(this).val();
        alert(toyID);
        var url = "Home/Remove?id=" + toyID;
        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            success: function (data) {
                $("#UserPayoutModal").html(data);
                var alertHtml = '<div class="alert alert-info alert-dismissible fade show col-md-12" role="alert">' +
                    'Removed</div>';
                var removeButtonClick = $(".my_link").click(removeItem);
                $("#alert_placeholder_delete").html(alertHtml);
                $(".alert").first().delay(2000).slideUp(200, function () {
                    $(this).remove();
                });

            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
                // PresentCloseableBootstrapAlert(".my_link", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });
    }
});