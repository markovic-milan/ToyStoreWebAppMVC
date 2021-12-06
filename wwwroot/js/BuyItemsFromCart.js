$(function () {
    var buttonPayClick = $("#buy").click(onPayButtonClick);

    function onPayButtonClick() {
        var url = "/Home/Payout";
        var cart = JSON.parse(localStorage.getItem("cart") || "[]");
        var shopModel = {
            Cart: cart
        }
        $.ajax({
            type: "POST",
            url: url,
            data: shopModel,
            success: function (data) {
                console.error(data);

                localStorage.clear();
                location.href = "Home/Index";
            }, error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
                PresentCloseableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });
    };
});