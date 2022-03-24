
function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode == 59 || charCode == 46)
        return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
}
$('.btnUpdateQty').bind('click keyup', function () {

    var value = $(this).val();
    if (1 * value < 0) {
        $(this).val(0);
    }
    if (value.length < 0) {
        value = 0;
    }

    var sanphamid = $(this).data("id");
    $.ajax({
        url: "/View_Cart/UpdateQuantity",
        data: { "proID": sanphamid, "quantity": value },
        method: "GET",
        dataType: "JSON",
        success: function (data) {
            //$("#showTable").html(data);
            debugger
            var gia = $("#txtGia" + sanphamid).val();
            if (value <= 0) {
                value = 0;
                $("#GuiDonHang").addClass('donhang');
               // $("#guidon").attr("href","#");
            }
            else
            {
                $("#GuiDonHang").removeClass('donhang');
            }
            $('.btnUpdateQty').each(function () {
                console.log($(this).val());
                if ($(this).val() <= 0) {
                    $("#GuiDonHang").addClass('donhang');
                }
            });
            $("#txtThanhTien" + sanphamid).find(".tien").html(formatNumber(gia * value) +" VNĐ");
            $("#txtThanhTien" + sanphamid).find(".thanhtien").val(gia * value);
            var tongtien = 0;
            $(".thanhtien").each(function () {
                tongtien += 1 * $(this).val();
            });
           
            AddTien(tongtien);

        }

    });
});


$("#quantity").change(function () {
    var value = $("#quantity").val();
    if (value < 0) {
        value = 0;
    }
    $('#gioHangdetail').attr('data-soluong', value);
    $('#gioHangdetailMuaNgay').attr('data-soluong', value);
});
$("#btnCheckout").click(function () {
    var thanhpho = $('#billing_address_province option:selected').text();

    var quanhuyen = $('#billing_address_District option:selected').text();
    var hoten = $("#txtHoten").val();
    var dienthoai = $("#txtDienThoai").val();
    var diachi = $("#txtDiaChi").val();


    if (hoten.length < 1) {
        alert("Bạn vui lòng cho cửa hàng biết tên của bạn hoặc người nhận hàng nhé!");
        return;
    }
    if (dienthoai.length < 10) {
        alert("Bạn vui lòng cho cửa hàng biết số điện thoại của bạn hoặc người nhận hàng nhé!");
        return;
    }
    if (diachi.length < 1) {
        alert("Bạn vui lòng cho cửa hàng biết địa chỉ người nhận hàng nhé!");
        return;
    }
    var data = {
        Name: hoten,
        Sex: true,
        Email: "",
        Phone: dienthoai,
        Note: "",
        UserID: "",
        Call: true,
        Total: 0,
        Address: diachi,
        Province_id: thanhpho + "",
        District_id: quanhuyen + ""
    }
    $.ajax({
        url: "/View_Cart/checkout",
        data: { "model": data },
        method: "POST",
        dataType: "JSON",
        success: function (data) {
            window.location.href = data;
        }

    });
})

$("#billing_address_province").change(function () {
    loadQuanHuyen();
})

loadQuanHuyen();
function loadQuanHuyen() {
    var oldData = $("#hiddenQuanhuyen").val();
    var id = $("#billing_address_province").val();
    var phiship = $("#billing_address_province option:selected").data('phiship');
    if (phiship == null)
        phiship = 0;
    if (phiship.length > 0) {
        $("#txtPhiShip").html(formatNumber(phiship) + " VNĐ");
    }
    else {
        $("#txtPhiShip").html(0 + " VNĐ");
    }

    var output = "";
    $.ajax({
        url: "/PageDetails/GetQuanHuyen",
        data: { "id": id },
        method: "GET",
        dataType: "JSON",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (oldData == data[i].DistrictId) {
                    output += ' <option value="' + data[i].DistrictId + '" data-phiship="' + data[i].PHISHIP + '" data-huyen="' + data[i].Type + '" selected>' + data[i].Type + " " + data[i].Name + '</option>';
                }
                else {
                    output += ' <option value="' + data[i].DistrictId + '" data-phiship="' + data[i].PHISHIP + '" data-huyen="' + data[i].Type + '">' + data[i].Type + " " + data[i].Name + '</option>';
                }
               
            }
            $("#billing_address_District").html(output);

            loadShip();
        }

    });
}
function loadShip() {
    var phishipTinh = $("#billing_address_province option:selected").data('phiship');
    var phiship = $("#billing_address_District option:selected").data('phiship');
    var idThanhPho = $("#billing_address_province").val();
    var idQuanHuyen = $("#billing_address_District").val();
    if (phiship == null || phiship.length < 1)
        phiship = phishipTinh;
    $.ajax({
        url: "/View_Cart/UpdatePriceShip",
        data: { "value": phiship, "thanhpho": idThanhPho, "quanhuyen": idQuanHuyen },
        method: "GET",
        dataType: "JSON",
        success: function (data) {
            $("#hiddenQuanhuyen").val(idQuanHuyen);
            $("#txtPhiShip").html(formatNumber(phiship) + " VNĐ");
            $("#txtTongHoadon").html(formatNumber(data) + " VNĐ");
        }

    });
}
$("#billing_address_District").change(function () {

    loadShip();

})
function AddTien(tongtien) {
    $("#hiddenTongTien").val(tongtien);
    $("#hiddenThanhTien").val(tongtien);
    Tinhtoantien();
}
function Tinhtoantien() {
    $("#txtTongTien").html(formatNumber($("#hiddenTongTien").val()) + " VNĐ");
    $("#txtTongHoadon").html(formatNumber($("#hiddenThanhTien").val()) + " VNĐ");
    $("#txtPriceKM").html(formatNumber($("#hiddenPriceKM").val()) + " VNĐ");
}
if (document.readyState === "complete") { alert("ok") }