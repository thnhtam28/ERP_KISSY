function formatTienVietNam(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode == 59 || charCode == 46)
        return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
}
$(".btnViewSamesize").click(function () {
    $(".btnViewSamesize").removeClass("active");
    $(this).addClass("active");
    var sanphamid = $(this).data("id");
    $.ajax({
        url: "/View_PLayout/GetSanPham",
        data: { "id": sanphamid},
        method: "POST",
        dataType: "JSON",
        success: function (data) {
            var Giasanpham = 0;
            $("#imgSanPham").attr("src", data.Sale_Product.Image_Name);
            $("#nameProduct").html(data.Sale_Product.Name);
            $("#masp").html("Mã SP: " + data.Sale_Product.Code);
            
            var outputgia = "";
            if (data.Sale_CommissionCus != null && data.Sale_Commision_Customer != null) {
                if (data.Sale_Commision_Customer.IsMoney == true) {
                    Giasanpham = data.Sale_Product.PriceOutBound - data.Sale_Commision_Customer.CommissionValue;
                }
                else {
                    Giasanpham = data.Sale_Product.PriceOutBound * (1 - (data.Sale_Commision_Customer.CommissionValue / 100));

                }

                outputgia = '<span class="GiaSPDealCu"><del>' + formatTienVietNam(data.Sale_Product.PriceOutBound) + '<sup>đ</sup></del></span>';
                outputgia += '<span>' + formatTienVietNam(Giasanpham) + '<sup>đ</sup></span>';
                $("#giasp").html(outputgia);
            }
            else {
                Giasanpham = data.Sale_Product.PriceOutBound;
                $("#giasp").html(' <span>' + formatTienVietNam(Giasanpham) + '<sup>đ</sup></span>');
               
            }
            $("#contentProduct").html(data.Sale_Product.Description);
            $("#sumaryProduct").html(data.Sale_Product.Description_brief);
            $("#hdsdContent").html(data.Sale_Product.HDSD);
            $(".btnAddGioHang").attr("data-id", data.Sale_Product.Id);
            $(".btnAddGioHang").attr("data-gia", Giasanpham);
        }

    });
})

$(".qty-up").click(function () {
    var value = $("#quantity").val();

        $("#quantity").val(1 *value + 1);

})
$(".qty-down").click(function () {
    var value = $("#quantity").val();
    if (value <= 0) { }
    else {
        $("#quantity").val(1*value - 1);
    }
})

$("#quantity").keydown(function (event) {

    // Allow only backspace and delete
    if (event.keyCode == 46 || event.keyCode == 8) {
        // let it happen, don't do anything
    }
    else {
        // Ensure that it is a number and stop the keypress
        if (event.keyCode < 48 || event.keyCode > 57) {
            event.preventDefault();
        }
    }
})
$(".aHreftImageSub").click(function () {
    var dataimg = $(this).data("image");
    $("#imgSanPham").attr("src", dataimg);
})