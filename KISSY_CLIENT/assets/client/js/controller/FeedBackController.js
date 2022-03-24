$("#feedBackSend").click(function () {
    $("#feedBackMessage").html("");
    var Name = $("#feedBackName").val();
    var Phone = $("#feedBackSDT").val();
    var Email = $("#feedBackEmail").val();
    var Address = $("#feedBackDiaChi").val();
    var Content = $("#feedBackYeuCau").val();
    if (Name.length < 1) {
        $("#feedBackMessage").html("Bắt buộc nhập tên");
        return;
    }
     
    if (Phone.length < 1) {
        $("#feedBackMessage").html("Bắt buộc nhập số điện thoại");
        return
    }

    if (Address.length < 1) {
        $("#feedBackMessage").html("Bắt buộc nhập địa chỉ");
        return;
    }
    if (Name.length < 1) {
        $("#feedBackMessage").html("Bạn hãy nhập nội dung feedback nhé!");
        return;
    }
    var data = {
        Name: Name,
        Phone: Phone,
        Email: Email,
        Address: Address,
        Content: Content,

    }
    $.ajax({
        url: '/MainKissy/InsertFeedBack',
        type: 'POST',
        dataType: 'html',
        data: { "feedback": data },
        success: function (res) {
            $("#feedBackMessage").html(res);
        }
    });
})