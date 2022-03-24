var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            var name = $('#txtName').val();
            var mobile = $('#txtMobile').val();
            var address = $('#txtAddress').val();
            var email = $('#txtEmail').val();
            var content = $('#txtContent').val();
            if (name== "")
            {
                alert("Vui lòng nhập họ và tên");
                return;
            }

            if (address == "") {
                alert("Vui lòng nhập địa chỉ để chúng tôi liên hệ");
                return;
            }

            if (mobile == "") {
                alert("Vui lòng nhập số điện thoại để chúng tôi tiện liên hệ");
                return;
            }

            if (email == "") {
                alert("Vui lòng nhập Email để chúng tôi tiện liên hệ");
                return;
            }
            if (content == "") {
                alert("Vui lòng nhập nội dung yêu cầu để chúng tôi tiện liên hệ");
                return;
            }


            $.ajax({
                url: '/View_PLayout/Send',
                type: 'POST',
                dataType: 'json',
                data: {
                    name: name,
                    mobile: mobile,
                    address: address,
                    email: email,
                    content: content
                },
                success: function (res) {
                    if (res.status == true) {
                        alert('Gửi thành công');
                        contact.resetForm();
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtMobile').val('');
        $('#txtAddress').val('');
        $('#txtEmail').val('');
        $('#txtContent').val('');
    }
}
contact.init();