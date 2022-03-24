
$(function () {
    $('.bxslider').bxSlider({
        mode: 'fade',
        auto: true,
        pager: false,
		touchEnabled:false,
        speed:1000
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
    

});
$(document).ready(function () {
   

    //https://www.jqueryscript.net/demo/Image-Carousel-Slider-SkySlider/

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
    //$("#btnAddGioHangDetail").click(function () {

    //    var soluong = $("#quantity").val();
    //    var sanphamid = $(this).data("id");
    //    var gia = $(this).data("gia");
    //    $.ajax({
    //        url: "/View_Cart/AddCart",
    //        data: { "id": sanphamid, "soluong": soluong, "gia": gia },
    //        method: "POST",
    //        dataType: "JSON",
    //        success: function (data) {
    //            $("#showsoluong").html("(" + data + ")");
    //            window.location.href = "/gio-hang";
    //        }

    //    });
    //})
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
    //#cd - lateral - nav.item - has - children: hover: after{ content: "\f068"; }
    //#cd - lateral - nav.item - has - children: hover > ul{ display: block!important; }
   
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