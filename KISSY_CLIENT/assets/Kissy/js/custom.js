
$(function () {
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode == 59 || charCode == 46)
            return true;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
    $("#btnAddGioHangDetail").click(function () {
        var color = $("#ChoseColor").val();
        var size = $("#ChoseSize").val();
        var soluong = $("#quantity").val();
        if (soluong.length < 1)
            soluong = 1;
        var sanphamid = $(this).data("id");
        var gia = $(this).data("gia");
        var promotionId = $(this).attr('data-pID');
        var promotionDetailId = $(this).attr('data-pdID');
        var promotionValue = $(this).attr('data-pValue');
        var IsMoney = $(this).attr('data-pdIDType');
        $.ajax({
            url: "/View_Cart/AddCart",
            data: { "id": sanphamid, "soluong": soluong, "gia": gia, "color": color, "size": size, "pID": promotionId, "pdID": promotionDetailId, "pValue": promotionValue, "pType": IsMoney },
            method: "POST",
            dataType: "JSON",
            success: function (data) {
                $(".soluong").html(data);
                window.location.href = "/gio-hang";
            }

        });
    })
    $(".qty-up").click(function () {
        var currQty = 1 * $("#quantity").val();
        $("#quantity").val(currQty + 1);
        

    })
    $(".qty-down").click(function () {
        var currQty = 1 * $("#quantity").val();
        if (currQty <= 1) {
            $("#quantity").val(1);

        }
        else {
            currQty = currQty - 1;
            $("#quantity").val(currQty);
        }

    })
    $('#ChoseSize').on('change', function () {
        LoadSanPhamTuongTu();
    });
    $('#ChoseColor').on('change', function () {
        LoadSanPhamTuongTu();
    });
    function LoadSanPhamTuongTu() {
        var color = $("#ChoseColor").val();
        var size = $("#ChoseSize").val();
        var spid = $("#hiddenID").val();
        $.ajax({
            url: "/PageDetails/FilterProduct",
            data: { "id": spid, "color": color, "size": size },
            method: "GET",
            dataType: "Html",
            success: function (data) {
                $("#FilterSanpHam").html(data);
        
            }

        });
    }
    $('.bxslider').bxSlider({
        mode: 'fade',
        auto: true,
        pager: false,
        speed: 1000
    });
    $("#thuonghieuLeft i.tog").click(function () {

        var status = $("#thuonghieuLeft .content").is(":hidden");
        if (status) {
            $(this).removeClass("fa-plus").addClass("fa-minus");
        }
        else {
            $(this).removeClass("fa-minus").addClass("fa-plus")
        }

        $("#thuonghieuLeft .content").slideToggle();
    });
    GetDevice();
    window.addEventListener('resize', GetDevice);
    function GetDevice() {
        var divWidth = window.innerWidth;
        if (divWidth <= 460) {
            $("#sliderWeb").hide();
            $("#sliderMobile").show();

        }
        else if (divWidth > 460 && divWidth <= 750) {
            $("#sliderWeb").hide();
            $("#sliderMobile").show();
        }
        else if (divWidth > 750) {
            $("#sliderWeb").show();
            $("#sliderMobile").hide();
        }
    }
    $("#btnScrollToComment").click(function () {
        $('html, body').animate({
            scrollTop: $("#elementbtnScrollToComment").offset().top
        }, 2000);
    });

$(document).ready(function () {


    //https://www.jqueryscript.net/demo/Image-Carousel-Slider-SkySlider/

    
    
});

   
    $(".btnAddGioHang").click(function () {

        var soluong = $(this).data("soluong");
        var sanphamid = $(this).data("id");
        var gia = $(this).data("gia");
        $.ajax({
            url: "/View_Cart/AddCart",
            data: { "id": sanphamid, "soluong": soluong, "gia": gia },
            method: "POST",
            dataType: "JSON",
            success: function (data) {
                $("#showsoluong").html("(" + data + ")");
                window.location.href = "/gio-hang";
            }

        });
    })

    $(".btnMuaNgay").click(function () {

        var soluong = $(this).data("soluong");
        var sanphamid = $(this).data("id");
        var gia = $(this).data("gia");
        $.ajax({
            url: "/View_Cart/AddCart",
            data: { "id": sanphamid, "soluong": soluong, "gia": gia },
            method: "POST",
            dataType: "JSON",
            success: function (data) {
                $("#showsoluong").html("(" + data + ")");
                if (data > 0) {
                    window.location.href = "/View_Playout/thongtinthanhtoan";
                }
            }

        });
    })
    // MrCường Gmail:mcuong.fit 
    // Sự kiện click chuột lên menu mobile
    $("#MenuTrenMobileID").click(function () {

        //xóa class hiddenHeader cho hiện menu
        $("#MenuTrenMobile_noidungID").removeClass("hiddenHeader");
        var clickon = document.getElementById('closeHeaderMobile')
        var clickmenu = document.getElementById('MenuTrenMobile_noidungID')
        //kiểm tra  id = closeHeaderMobile đã được click chưa, nếu phải add class hiddenHeader cho ẩn menu
        clickon.addEventListener('mousedown', function (e) {

            $("#MenuTrenMobile_noidungID").addClass("hiddenHeader");
        });
        //set sự kiện click chuột mới có  nhấp lên "id =closeHeaderMobile" hay không thì vẫn gọi class hiddenHeader "class=MenuTrenMobile_noidungID" ẩn menu
        document.documentElement.addEventListener('mouseup', function (e) {


            if (e.target == clickon) {
                $("#MenuTrenMobile_noidungID").addClass("hiddenHeader");
            }
            if (e.target != clickmenu) {
                $("#MenuTrenMobile_noidungID").addClass("hiddenHeader");
            }

        });
    })
    $(".MenuTrenMobile_noidung").click(function () {
        $("#MenuTrenMobile_noidungID").removeClass("hiddenHeader");
    })
    $("#cd-lateral-nav .item-has-children .showul").click(function () {

        if ($(this).parent().children("ul:first").css('display') == 'block') {

            $(this).parent().children("ul:first").attr("style", "display:none!important");
            $(this).find('i').removeClass('fa-minus').addClass('fa-plus');
        }
        else {

            $(this).parent().children("ul:first").attr("style", "display:block!important");
            $(this).find('i').removeClass('fa-plus').addClass('fa-minus');
        }
    })
    $(".footer2 .box").click(function () {
        var findclass = $(this).find('.box_khung');
        if (findclass.length > 0) {
            $(this).find('.box_title').find('i').removeClass('fa-plus').addClass('fa-minus');
            findclass.removeClass('box_khung').addClass('showbox_khung');
        }
        else {
            $(this).find('.box_title').find('i').removeClass('fa-minus').addClass('fa-plus');
            $(this).find('.showbox_khung').removeClass('showbox_khung').addClass('box_khung');
        }

    })

    $(".header_Mobile3_1 >span").click(function () {
        if ($(".search-bar").css('display') == 'block') {
            $(".search-bar").attr('style', 'display:none');
        }

        else {


            $(".search-bar").attr('style', 'display:block');
            $("#qSearch").focus();
        }

    })
});