$(function () {
    var buttonPayClick = $("#buy").click(onPayButtonClick);
    function onPayButtonClick() {
        var url = "/Home/Payout";
        var cart = JSON.parse(localStorage.getItem("cart") || "[]");
        if (cart.length == 0) {
            PresentCloseableBootstrapAlert("#alert_placeholder_delete", "danger", "Error!", "Cart is Empty!");
            $("#alert_placeholder_delete").show().first().delay(2000).slideUp(200, function () {
                $(this).hide();
            });
            return;
        }
        var shopModel = {
            Cart: cart
        }
        $.ajax({
            type: "POST",
            url: url,
            data: shopModel,
            success: function (data) {
                var parsed = $.parseHTML(data);
                var notInStock = $(parsed).find("input[name='NotInStock']").val();
                var stockParsed = JSON.parse(notInStock);
                var cart = JSON.parse(localStorage.getItem("cart"));
                var total = 0;
                cart.forEach(el => {
                    if (stockParsed.some((element) => element.Id == el.Id)) {
                        $("#" + el.Id + ' .stock_placeholder_alert').html("<strong class='text-danger'>Not in stock!</strong>");
                    } else {
                        total += parseFloat(el.Cost);
                        $("#" + el.Id + ' .stock_placeholder_alert').html("<strong class='text-success'>Bought!</strong>");
                    }
                });
                $("#total").html(total);
                $("#buy").hide();
                $("#dismis").html("Finish");
                $("#buy").click(onPayButtonClick);
                localStorage.clear();
            }, error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
                PresentCloseableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);
                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });
    };
});