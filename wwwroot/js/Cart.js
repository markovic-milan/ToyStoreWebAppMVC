$(function () {
    var cartButton = $("#cart").click(onCartButtonClick);

    $(document).on('click', '.my_link', function () {

        var toyID = $(this).val();
        var cost = $("#total").html();
        var cart = JSON.parse(localStorage.getItem("cart") || "[]");
        var element = cart.find(element => element.Id == toyID);
        var filtered = cart.filter(checkId);
        var total = parseFloat(cost) - parseFloat(element.Cost);
        function checkId(toy) {
            return toy.Id != toyID;
        };
        localStorage.setItem("cart", JSON.stringify(filtered));

        var alertHtml = '<div class="alert alert-info alert-dismissible fade show col-md-12" role="alert">' +
            'Removed</div>';
        $("#" + toyID).html(alertHtml);
        $("#total").html(total);
        $(".alert").first().delay(1000).slideUp(200, function () {
            $(this).remove();
        });
    });

    function onCartButtonClick() {
        $("#buy").show();
        $("#dismis").html("Dismis");
        var dataHtml = "";
        var total = 0.0;
        var cart = JSON.parse(localStorage.getItem("cart") || "[]");
        if (cart.length > 0) {
            cart.forEach(item => {
                total += parseFloat(item.Cost);
                dataHtml += "<span class='row removable_row' id='" + item.Id + "'>" +
                    "<div class='col-md-2 d-flex align-items-center'>" +
                    "<button type='button' class='btn btn-danger my_link' name='my_link' value='" + item.Id +
                    "'><i class='fas fa-times'></i></button></div>" +
                    "<div class='col-md-3 d-flex align-items-center'>" + item.Name + "</div>" +
                    "<div class='col-md-4 d-flex align-items-center stock_placeholder_alert'></div>" +
                    "<div class='col-md-3 d-flex align-items-center'>" + item.Cost + "</div>" +
                    "<hr class='col-md-10 row-divider'/></span>"
            });

            dataHtml += "<div class='row'><div class='col-md-8 pl-2'><p class='pl-4'><strong>Total</strong></p>" +
                "</div><div class='col-md-4'><p><strong id='total'>" + total + "</strong><strong> KM</strong></p></div></div>" +
                "<div class='input-group'><div class='input-group-prepend'><span class='input-group-text' id=''>Credit Card Number</span>" +
                "</div><input type='text' class='form-control'></div>"
        } else {
            dataHtml += "<div class='col'><p>Oops Cart is empty!</p></div >";
        }

        $("#div_data").html(dataHtml);

        $("#UserPayoutModal").modal();
    }
});