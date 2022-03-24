
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
    if (value.length < 1 || value*1<0) {
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
            var gia = $("#txtGia" + sanphamid).val();
            $("#txtThanhTien" + sanphamid).find(".tien").html(formatNumber(gia * value));
            $("#txtThanhTien" + sanphamid).find(".thanhtien").val(gia * value);
            var tongtien=0;
            $(".thanhtien").each(function () {
                tongtien += 1 *$(this).val();
            });
            
            AddTien(tongtien);
        }

    });
});
$(".btnUpdateQty").keyup(function () {
    var value = $(this).val();
    if (value.length < 1) {
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
            var gia = $("#txtGia" + sanphamid).val();
            $("#txtThanhTien" + sanphamid).find(".tien").html(formatNumber(gia * value));
            $("#txtThanhTien" + sanphamid).find(".thanhtien").val(gia * value);
            var tongtien = 0;
            $(".thanhtien").each(function () {
                    tongtien += 1*$(this).val();
            });
            AddTien(tongtien);
            
        }

    });
})

$("#quantity").change(function () {
    var value = $("#quantity").val();
    $('#gioHangdetail').attr('data-soluong', value);
    $('#gioHangdetailMuaNgay').attr('data-soluong', value);
});
$("#btnCheckout").click(function () {
    var thanhpho =   $('#billing_address_province option:selected').text();
  
    var quanhuyen = $('#billing_address_District option:selected').text();
    var diachi = $("#billing_address").val();
    var gioitinh = $("input:radio[name=gender]:checked").val()
    var name = $("#billing_address_full_name").val();
    var phone = $("#billing_address_phone").val();
    var email = $("#order_email").val();
    var goi = false;
    if ($('input[name=callbefor]').is(":checked")) {
        goi = true;
    }
    var note = $("#billing_note").val();
    
    if (diachi.length < 1) {
        alert("Bạn vui lòng cho cửa hàng biết địa chỉ giao hàng nhé!");
        return;
    }
    if (name.length < 2) {
        alert("Bạn vui lòng cho cửa hàng biết tên của bạn nhé!");
        return;
    }
    if (phone.length < 10) {
        alert("Bạn vui lòng cho cửa hàng biết số điện thoại của bạn nhé!");
        return;
    }
    var data = {
        Name: name,
        Sex: gioitinh,
        Email:email,
        Phone:phone,
        Note :note,
        UserID: "",
        Call:goi,
        Total: 0,
        Address: diachi,
        Province_id: thanhpho+"",
        District_id: quanhuyen+""
    }
    $.ajax({
        url: "/View_Cart/checkout",
        data: { "model": data },
        method: "POST",
        dataType: "JSON",
        success: function (data) {
            window.location.href=data;
        }

    });
})

$("#billing_address_province").change(function () {

    loadQuanHuyen();
})

loadQuanHuyen();
function loadQuanHuyen() {
    var id = $("#billing_address_province").val();
    var phiship = $("#billing_address_province option:selected").data('phiship');
    if (phiship == null)
        phiship = 0;
    if (phiship.length > 0) {
        $("#txtPhiShip").html(formatNumber(phiship) +"<sup>đ</sup>");
    }
    else {
        $("#txtPhiShip").html(0 + "<sup>đ</sup>");
    }

    var output = "";
    $.ajax({
        url: "/View_Playout/GetQuanHuyen",
        data: { "id": id },
        method: "GET",
        dataType: "JSON",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {

                output += ' <option value="' + data[i].DistrictId + '" data-phiship="' + data[i].PHISHIP + '" data-huyen="' + data[i].Type+'">' + data[i].Type + " " + data[i].Name + '</option>';
            }
            $("#billing_address_District").html(output);

            loadShip();
        }

    });
}
function loadShip() {
    var phishipTinh = $("#billing_address_province option:selected").data('phiship');
    var phiship = $("#billing_address_District option:selected").data('phiship');
    var huyen = $("#billing_address_District option:selected").data('huyen');
    if (phiship == null || phiship.length < 1)
        phiship = phishipTinh;
    $.ajax({
        url: "/View_Cart/UpdatePriceShip",
        data: { "value": phiship },
        method: "GET",
        dataType: "JSON",
        success: function (data) {
            $("#txtPhiShip").html(formatNumber(phiship) + "<sup>đ</sup>");
            $("#txtTongHoadon").html(formatNumber(data) + "<sup>đ</sup>");
        }

    });
}
$("#billing_address_District").change(function () {

    loadShip();
    
})
function AddTien(tongtien) {
    var phishipTinh = $("#billing_address_province option:selected").data('phiship');
    var phiship = $("#billing_address_District option:selected").data('phiship');

    if (phiship == null|| phiship == 0)
        phiship = phishipTinh;

 
        $("#hiddenTongTien").val(tongtien);

        $("#hiddenPhiShip").val(phiship);

    var thanhtien = tongtien + phiship;
    $("#hiddenThanhTien").val(thanhtien);
    Tinhtoantien();
}
function Tinhtoantien() {
    $("#txtTongTien").html(formatNumber($("#hiddenTongTien").val()) + "<sup>đ</sup>");
    $("#txtPhiShip").html(formatNumber($("#hiddenPhiShip").val()) + "<sup>đ</sup>");
    $("#txtTongHoadon").html(formatNumber($("#hiddenThanhTien").val()) + "<sup>đ</sup>");
}
if (document.readyState === "complete") { alert("ok") }