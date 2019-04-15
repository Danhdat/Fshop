var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/Home/san-pham/";
        });

        $('#btnUpdate').off('click').on('click', function () {
            var list = $('.txtQuantity');
            var listSize = $('.sizeProduct');
            var listCart = [];
            $.each(list, function (i, item) {
                listCart.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/UpdateCart/',
                data: {
                    cartModel: JSON.stringify(listCart),
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang/';
                    }
                }
            });
        });

        $('.sizeProduct').on('change', function () {
            var sizeID = $(this).val();
            var productID = $(this).data('id');

            $.ajax({
                url: '/Cart/updateSize/',
                data: {
                    sizes: sizeID,
                    productID: productID
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang/';
                    }
                }
            });
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            var productID = $(this).data('id');
            $.ajax({
                url: '/Cart/DeteleItem/',
                data: { id: productID },
                dataType: 'json',
                type: 'POST',
            })
                .done(function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang/';
                    }
                });
        });
    }
}
cart.init();