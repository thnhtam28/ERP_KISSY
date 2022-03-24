//Khai báo biến toàn cục
var d = new Date();
var currentMonth = d.getMonth() + 1;
var currentQuarter = 1;
switch (currentMonth) {
    case 1:
    case 2:
    case 3:
        currentQuarter = 1;
        break;
    case 4:
    case 5:
    case 6:
        currentQuarter = 2;
        break;
    case 7:
    case 8:
    case 9:
        currentQuarter = 3;
        break;
    case 10:
    case 11:
    case 12:
        currentQuarter = 4;
        break;
}

//Cái gì ready cho zô đây nha
$(document).ready(function () {
    $('.numberVNI').each(function () {
        $(this).val($(this).val().replace(',', '.'))
    });

    $('footer, .main-content-popup').dblclick(function () {
        $('.profiler-results').remove();
    });

    //Xử lý cái nút thu nhỏ sidebar
    var sidebar_collapse_class = localStorage.getItem('sidebar-collapse');
    var $sidebar = $('#sidebar');
    var $logo_small = $('#navbar-container .logo-small');
    var $logo_big = $('#navbar-container .logo-big');

    if (sidebar_collapse_class == 'menu-min') {
        $sidebar.addClass('menu-min');
        $logo_small.toggle();
        $logo_big.toggle();
        $('#sidebar-collapse i').attr('class', $('#sidebar-collapse i').data('icon2'))
    }

    $('#sidebar-collapse').click(function () {
        if ($sidebar.hasClass('menu-min') == false) {
            localStorage.setItem('sidebar-collapse', 'menu-min');
            $logo_small.show();
            $logo_big.hide();
        } else {
            localStorage.removeItem('sidebar-collapse');
            sidebar_collapse = localStorage.getItem('sidebar-collapse');
            $logo_small.hide();
            $logo_big.show();
        }
    });




    setTimeout(function () {
        $('#sidebar > .nav > li.active').each(function (index, li) {
            var li_child = $(li).find('li');
            //console.log(li_child);
            if (li_child.length != 0) {
                $(li).addClass('open');
                var pathName = location.pathname;
                var a = li_child.find('a[href="' + pathName + '"]');
                if (a.length != 0)
                    a.parent('li').addClass('active');
            }
        });
    }, 1000);

    /*********************************************/
    //Khi load popup lên xong thì Hide Loading
    window.onload = function () {
        if (window.parent != null)
            window.parent.HideLoading();
    }

    $('#myModal').on('hidden', function () {
        enableScroll();
    });

    $('form').not('.no_show_loading').submit(function () {
        ShowLoading();
        if ($(this).valid()) {
        }
        else {
            HideLoading();
        }
    });

    //Cài đặt định dạng cho các input là kiểu số
    $('.input-price').priceFormat({
        centsSeparator: ',',
        thousandsSeparator: '.',
        prefix: '',
        suffix: '',
        clearPrefix: true,
        clearSuffix: true,
        //allowNegative: true,
        //limit: 2,
        centsLimit: 0
    });

    $('.input-price').focus(function () {
        $(this).select();
    });

    $(".input-float").numberFloatFormat();

    //Đặt lại độ rộng cho .edit-view label/control
    function arrangeLayout() {
        setTimeout(function () {
            $(".edit-view .control-group").each(function () {
                var label = $(this).find(".control-label");
                var value = $(this).find(".control-value");

                if ($(this).width() > 0) {
                    value.width($(this).width() - label.width() - 40);
                }
            })
        }, 10);
    }

    $(window).resize(function () {
        arrangeLayout();
    });

    $("ul.nav-tabs li a").click(function () {
        arrangeLayout();
    });
});

/******************************************************************************/

var previewFileImage = function (file, display) {

    var input = file.target;

    var reader = new FileReader();
    reader.onload = function () {
        var dataURL = reader.result;
        var output = document.querySelector(display);
        output.src = dataURL;
    };

    reader.readAsDataURL(input.files[0]);
};

var previewFileImage2 = function (file, display) {
    var input = file.target;

    if (input.files[0] != null) {
        document.getElementById("s1").style.display = 'block';
        $("#img1").val(0);
        var reader = new FileReader();
        reader.onload = function () {
            var dataURL = reader.result;
            var output = document.querySelector(display + ' ' + 'span#s1 img');
            output.src = dataURL;
        };
        reader.readAsDataURL(input.files[0]);
    } else {
        document.getElementById("s1").style.display = 'none';

    }

    if (input.files[1] != null) {
        document.getElementById("s2").style.display = 'block';
        $("#img2").val(0);
        var reader2 = new FileReader();
        reader2.onload = function () {
            var dataURL = reader2.result;
            var output = document.querySelector(display + ' ' + 'span#s2 img');

            output.src = dataURL;
        };


        reader2.readAsDataURL(input.files[1]);
    } else {
        document.getElementById("s2").style.display = 'none';

    }
    if (input.files[2] != null) {
        document.getElementById("s3").style.display = 'block';
        $("#img3").val(0);
        var reader3 = new FileReader();
        reader3.onload = function () {
            var dataURL = reader3.result;
            var output = document.querySelector(display + ' ' + 'span#s3 img');
            output.src = dataURL;
        };


        reader3.readAsDataURL(input.files[2]);
    } else {
        document.getElementById("s3").style.display = 'none';

    }



    if (input.files[3] != null) {
        document.getElementById("s4").style.display = 'block';
        $("#img4").val(0);
        var reader4 = new FileReader();
        reader4.onload = function () {
            var dataURL = reader4.result;
            var output = document.querySelector(display + ' ' + 'span#s4 img');
            output.src = dataURL;
        };


        reader4.readAsDataURL(input.files[3]);
    } else {
        document.getElementById("s4").style.display = 'none';

    }

    if (input.files[4] != null) {
        document.getElementById("s5").style.display = 'block';
        $("#img5").val(0);
        var reader5 = new FileReader();
        reader5.onload = function () {
            var dataURL = reader5.result;
            var output = document.querySelector(display + ' ' + 'span#s5 img');
            output.src = dataURL;
        };
        reader5.readAsDataURL(input.files[4]);
    } else {
        document.getElementById("s5").style.display = 'none';

    }

    if (input.files[5] != null) {
        document.getElementById("s6").style.display = 'block';
        $("#img6").val(0);
        var reader6 = new FileReader();
        reader6.onload = function () {
            var dataURL = reader6.result;
            var output = document.querySelector(display + ' ' + 'span#s6 img');
            output.src = dataURL;
        };


        reader6.readAsDataURL(input.files[5]);
    } else {
        document.getElementById("s6").style.display = 'none';

    }


    if (input.files[6] != null) {
        document.getElementById("s7").style.display = 'block';
        $("#img7").val(0);
        var reader7 = new FileReader();
        reader7.onload = function () {
            var dataURL = reader7.result;
            var output = document.querySelector(display + ' ' + 'span#s7 img');
            output.src = dataURL;
        };


        reader7.readAsDataURL(input.files[6]);
    } else {
        document.getElementById("s7").style.display = 'none';

    }

    if (input.files[7] != null) {
        document.getElementById("s8").style.display = 'block';
        $("#img8").val(0);
        var reader8 = new FileReader();
        reader8.onload = function () {
            var dataURL = reader8.result;
            var output = document.querySelector(display + ' ' + 'span#s8 img');
            output.src = dataURL;
        };


        reader8.readAsDataURL(input.files[7]);
    } else {
        document.getElementById("s8").style.display = 'none';

    }




};
// method khởi tạo các input theo type của phần đặc tính động (vì load lên theo ajax)
function initDataTypeInput($multi_field) {
    $($multi_field).each(function (index, elem) {
        switch ($(elem).attr('type')) {
            case 'date':
                $(elem).attr('type', 'text');
                //https://bootstrap-datepicker.readthedocs.io/en/latest/options.html#defaultviewdate
                moment();
                $(elem).datepicker({
                    //startDate: new Date(),
                    format: 'dd-mm-yyyy',
                });
                break;
            case 'datetime':
                $(elem).attr('type', 'text');
                //https://eonasdan.github.io/bootstrap-datetimepicker/
                moment();
                $(elem).dateptimeicker({
                    defaultDate: new Date(),
                    format: 'H:mm DD-MM-YYYY',
                    ignoreReadonly: true,
                });
                break;
        }
    });
};

// Xóa các thông báo yêu cầu chọn hoặc nhập
function resetLableRequired($form) {
    $form = $($form);
    $form.find('select[data-val="true"], input[data-val="true"]').each(function (index, elem) {
        $(elem).change(function () {
            $(elem).next('span').text('');
        });
    });
};

//kiểm tra đã chọn hoặc nhập hay chưa đối với các trường thêm động
function checkFieldLiveRequired($form) {
    $form = $($form);
    var flag = true;
    $form.find('select[data-val="true"], input[data-val="true"]').filter(':not(input[type="hidden"])').each(function (index, elem) {
        if ($(elem).next('span').length > 0) {
            $(elem).next('span').text('');
        }
        else {
            $(elem).after($('<span class="text-danger"></span>'));
        }

        if ($(elem).val() == '') {
            var message = $(elem).data('val-required') != undefined ? $(elem).data('val-required') : 'Hãy nhập/chọn cho ô này!';
            $(elem).next('span').text(message);
            $(elem).next('span').parent().find('input').first().focus();
            flag = false;
            console.log($(elem));
        } else {
            switch ('') {
                case 'range':
                    break;
            }
        }
    });
    return flag;
};


//ẩn thẻ div bao quanh tên và ô nhập giá trị của thuộc tính
function hideGroupRootFromInput($input) {
    $($input).closest('.form-group').hide();
};
//hiện thẻ div bao quanh tên và ô nhập giá trị của thuộc tính
function showGroupRootFromInput($input) {
    $($input).closest('.form-group').show();
};

// method for custom validate MVC
function onErrorValid(error, inputElement) {  // 'this' is the form element
    var container = $(this).find("[data-valmsg-for='" + escapeAttributeValue(inputElement[0].name) + "']"),
        replace = $.parseJSON(container.attr("data-valmsg-replace")) !== false;

    container.removeClass("field-validation-valid").addClass("field-validation-error");
    error.data("unobtrusiveContainer", container);

    if (replace) {
        container.empty();
        error.removeClass("input-validation-error").appendTo(container);
    }
    else {
        error.hide();
    }
};

function onSuccessValid(error) {  // 'this' is the form element
    var container = error.data("unobtrusiveContainer"),
        replace = $.parseJSON(container.attr("data-valmsg-replace"));

    if (container) {
        container.addClass("field-validation-valid").removeClass("field-validation-error");
        error.removeData("unobtrusiveContainer");

        if (replace) {
            container.empty();
        }
    }
};
// end method for custom validate MVC


// left: 37, up: 38, right: 39, down: 40,
// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };

function preventDefault(e) {
    e = e || window.event;
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}

function preventDefaultForScrollKeys(e) {
    if (keys[e.keyCode]) {
        preventDefault(e);
        return false;
    }
}

function disableScroll() {
    if (window.addEventListener) // older FF
        window.addEventListener('DOMMouseScroll', preventDefault, false);
    window.onwheel = preventDefault; // modern standard
    window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
    window.ontouchmove = preventDefault; // mobile
    document.onkeydown = preventDefaultForScrollKeys;
}

function enableScroll() {
    if (window.removeEventListener)
        window.removeEventListener('DOMMouseScroll', preventDefault, false);
    window.onmousewheel = document.onmousewheel = null;
    window.onwheel = null;
    window.ontouchmove = null;
    document.onkeydown = null;
}

function OpenPopup(url, title, w, h, size) {
    if (url != '') {
        $("#myModal .modal-dialog").removeClass("Return-Dialog");
        $("#myModal .modal-header").removeClass("Close-popup");
        $("#myModal .modal-content").removeClass("Return-Content");
        $("#myModal .modal-body").removeClass("Return-Body");
        $("#myModal .iframe-container").removeClass("Return-Iframe");
        $("#myModal").removeClass("modal-full2");
        $("#myModal .main-content-popup #page_toolbar").addClass("Return-Toolbar");
        if (w == "0") {
            $("#myModal").addClass("modal-full");
        }
        else {
            $("#myModal").removeClass("modal-full");
        }

        $("#myModal .modal-title").text(title);
        $("#myModal .modal-body .iframe-container").html("<iframe src=\"" + url + "\"></iframe>");
        if (h == "100%") {
            //$("#myModal .modal-body").height($(window).height() - $("#myModal .modal-header").height() - 100);
            //$("#myModal .modal-body iframe").height("100%");
            $("#myModal .modal-body").height("100%");
        }
        else
            $("#myModal .modal-body").height(h);

        $('#myModal').modal('show');
        $("#myModal .modal-body .img-loading-wrap").show();
    }
}
function PopupReturnProduct(url, title, w, h, size) {
    if (url != '') {
        if (w == "0") {
            $("#myModal").addClass("modal-full2");
        }
        else {
            $("#myModal").removeClass("modal-full2");
        }

        $("#myModal .modal-title").text(title);
        $("#myModal .modal-dialog").addClass("Return-Dialog");
        $("#myModal .modal-content").addClass("Return-Content");
        $("#myModal .modal-body").addClass("Return-Body");
        $("#myModal .iframe-container").addClass("Return-Iframe");
        $("#myModal .main-content-popup #page_toolbar").addClass("Return-Toolbar");
        $("#myModal .modal-body .iframe-container").html("<iframe src=\"" + url + "\"></iframe>");
        if (h == "100%") {
            //$("#myModal .modal-body").height($(window).height() - $("#myModal .modal-header").height() - 100);
            //$("#myModal .modal-body iframe").height("100%");
            $("#myModal .modal-body").height("100%");
        }
        else
            $("#myModal .modal-body").height(h);

        $('#myModal').modal('show');
        $("#myModal .modal-body .img-loading-wrap").show();
    }
}

function ClosePopup(bReload) {
    $("#myModal .modal-body .iframe-container").html("");
    $('#myModal').modal('hide');

    if (bReload)
        location.reload(true);
}

function ClosePopupAndReloadPage() {
    ShowLoading();
    ClosePopup(true);
}

function ShowLoading() {
    $(".img-loading-wrap").show();
}

function HideLoading() {
    $(".img-loading-wrap").hide();
}

//------------------------------------------------------------------------------------

function capitalizeFirstAllWords(str) {
    var pieces = str.split(" ");
    for (var i = 0; i < pieces.length; i++) {
        var j = pieces[i].charAt(0).toUpperCase();
        pieces[i] = j + pieces[i].substr(1);
    }
    return pieces.join(" ");
}

function handleNewItemClick(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        ShowLoading();
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (isAppend)
                    $(parent).append(data);
                else
                    $(parent).prepend(data);

                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}
function addClass(elem, cls) {
    var current = elem.className;
    cls = (current) ? ' ' + cls : cls;
    elem.className += cls;
}

function ClickEventHandler2(isAppend, url, parent, formData, ProductCode, callbackFunction) {
    debugger
    if (url != "") {
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var tab;
                if (isAppend) {
                    if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                      
                        parent = ".detailList1";
                        tab = 1;
                        var tmp = 0;
                        $("#tbllistOrderDetail1 TBODY TR").each(function () {
                            var row = $(this);

                            var ProductCode1 = $(this).closest('tr').find("td:eq(5) input").val();
                            if (String(ProductCode).trim().localeCompare(String(ProductCode1).trim()) == 0) {
                                tmp = 1;
                            }
                            

                        });
                        if (tmp == 1) {
                            return;
                        } else {
                            $(parent).prepend(data);
                            isone = 0;
                        }
                       

                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        parent = ".detailList2";
                        tab = 2;
                        var tmp = 0;
                        $("#tbllistOrderDetail2 TBODY TR").each(function () {
                            var row = $(this);

                            var ProductCode1 = $(this).closest('tr').find("td:eq(5) input").val();

                            if (String(ProductCode).trim().localeCompare(String(ProductCode1).trim()) == 0) {
                                tmp = 1;
                            }
                            //alert(ProductCode);

                        });
                        if (tmp == 1) {
                            return;
                        } else {
                            $(parent).prepend(data);
                            isone = 0;
                        }

                    } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        parent = ".detailList3";
                        tab = 3;
                        var tmp = 0;
                        $("#tbllistOrderDetail3 TBODY TR").each(function () {
                            var row = $(this);

                            var ProductCode1 = $(this).closest('tr').find("td:eq(5) input").val();

                            if (String(ProductCode).trim().localeCompare(String(ProductCode1).trim()) == 0) {
                                tmp = 1;
                            }
                            //alert(ProductCode);

                        });
                        if (tmp == 1) {
                            return;
                        } else {
                            $(parent).prepend(data);
                            isone = 0;
                        }
            
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
                        parent = ".detailList4";
                        tab = 4;
                        var tmp = 0;
                        $("#tbllistOrderDetail4 TBODY TR").each(function () {
                            var row = $(this);

                            var ProductCode1 = $(this).closest('tr').find("td:eq(5) input").val();

                            if (String(ProductCode).trim().localeCompare(String(ProductCode1).trim()) == 0) {
                                tmp = 1;
                            }
                            //alert(ProductCode);

                        });
                        if (tmp == 1) {
                            return;
                        } else {
                            $(parent).prepend(data);
                            isone = 0;
                        }
  
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
                        parent = ".detailList5";
                        tab = 5;
                        var tmp = 0;
                        $("#tbllistOrderDetail5 TBODY TR").each(function () {
                            var row = $(this);

                            var ProductCode1 = $(this).closest('tr').find("td:eq(5) input").val();

                            if (String(ProductCode).trim().localeCompare(String(ProductCode1).trim()) == 0) {
                                tmp = 1;
                            }
                            //alert(ProductCode);

                        });
                        if (tmp == 1) {
                            return;
                        } else {
                            $(parent).prepend(data);
                            isone = 0;
                        }
                    } else {
                        parent = ".detailList6";
                        tab = 6;
                        var tmp = 0;
                        $("#tbllistOrderDetail6 TBODY TR").each(function () {
                            var row = $(this);

                            var ProductCode1 = $(this).closest('tr').find("td:eq(5) input").val();

                            if (String(ProductCode).trim().localeCompare(String(ProductCode1).trim()) == 0) {
                                tmp = 1;
                            }
                           

                        });
                        if (tmp == 1) {
                            return;
                        } else {
                            $(parent).prepend(data);
                            isone = 0;
                        }
                    }

                   
                    $('#inpBarcode').val('');
                    if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        var rows = document.querySelectorAll('#tbllistOrderDetail1 TBODY TR');
                        var row = rows[rows.length - 1];
                        addClass(row, 'selected_grey');
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        var rows = document.querySelectorAll('#tbllistOrderDetail2 TBODY TR');
                        var row = rows[rows.length - 1];
                        addClass(row, 'selected_grey');
                    } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        var rows = document.querySelectorAll('#tbllistOrderDetail3 TBODY TR');
                        var row = rows[rows.length - 1];
                        addClass(row, 'selected_grey');
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
                        var rows = document.querySelectorAll('#tbllistOrderDetail4 TBODY TR');
                        var row = rows[rows.length - 1];
                        addClass(row, 'selected_grey');
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
                        var rows = document.querySelectorAll('#tbllistOrderDetail5 TBODY TR');
                        var row = rows[rows.length - 1];
                        addClass(row, 'selected_grey');
                    } else {
                        var rows = document.querySelectorAll('#tbllistOrderDetail6 TBODY TR');
                        var row = rows[rows.length - 1];
                        addClass(row, 'selected_grey');
                    }

                    //alert(rows.length);
                    //Gọi đến hàm change table
                    var tbl = "#tbllistOrderDetail" + tab;
                    $(tbl).trigger('change');

                }
                else {
                    if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        parent = ".detailList1";
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        parent = ".detailList2";
                    } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        parent = ".detailList3";
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
                        parent = ".detailList4";
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
                        parent = ".detailList5";
                    } else {
                        parent = ".detailList6";
                    }
                    $(parent).prepend(data);
                }



                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}
function GetDiscountToday(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        ShowLoading();
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (isAppend) {
                    if (tab3 == 0 && tab1 == 1 && tab2 == 0) {
                        parent = ".detailList1";
                    }
                    if (tab3 == 0 && tab1 == 0 && tab2 == 1) {
                        parent = ".detailList2";
                    }
                    if (tab3 == 1 && tab1 == 0 && tab2 == 0) {
                        parent = ".detailList3";
                    }
                    $(parent).append(data);
                }
                else {
                    if (tab3 == 0 && tab1 == 1 && tab2 == 0) {
                        parent = ".detailList1";
                    }
                    if (tab3 == 0 && tab1 == 0 && tab2 == 1) {
                        parent = ".detailList2";
                    }
                    parent = ".detailList3";
                    if (tab3 == 1 && tab1 == 0 && tab2 == 0) {
                    }
                    $(parent).prepend(data);
                }


                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}
function ClickEventHandler(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        ShowLoading();
        //alert("ok vao day 1");
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (isAppend)
                    $(parent).prepend(data);
                else
                    $(parent).prepend(data);

                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}


function ClickEventHandlerHOAPD(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (isAppend)
                    $(parent).append(data);
                else
                    $(parent).prepend(data);
                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
               
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}
function ClickEventHandlerSearchCus(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") { 
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (isAppend) {
                    if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        $("#idboxsearch2").val(result.FullName);
                        $("#CustomerName1").val(result.FullName);
                        $("#CustomerId1").val(result.Id);
                        tinhkhuyenmaiHHVip(1);
                        checkcustomertab1 = result.Id;
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        $("#idboxsearch2").val(result.FullName);
                        $("#CustomerName2").val(result.FullName);
                        $("#CustomerId2").val(result.Id);
                        tinhkhuyenmaiHHVip(2);
                        checkcustomertab2 = result.Id;
                    } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
                        $("#idboxsearch2").val(result.FullName);
                        $("#CustomerName3").val(result.FullName);
                        $("#CustomerId3").val(result.Id);
                        tinhkhuyenmaiHHVip(3);
                        checkcustomertab3 = result.Id;
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
                        $("#idboxsearch2").val(result.FullName);
                        $("#CustomerName4").val(result.FullName);
                        $("#CustomerId4").val(result.Id);
                        tinhkhuyenmaiHHVip(4);
                        checkcustomertab4 = result.Id;
                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
                        $("#idboxsearch2").val(result.FullName);
                        $("#CustomerName5").val(result.FullName);
                        $("#CustomerId5").val(result.Id);
                        tinhkhuyenmaiHHVip(5);
                        checkcustomertab5 = result.Id;
                    } else {
                        $("#idboxsearch2").val(result.FullName);
                        $("#CustomerName6").val(result.FullName);
                        $("#CustomerId6").val(result.Id);
                        tinhkhuyenmaiHHVip(6);
                        checkcustomertab6 = result.Id;
                    }
                }

                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (result) {
                console.log(result);
                HideLoading();
            }
        });
    }
    return false;
}
function ClickEventHandlerSearchCus2(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (isAppend) {
                    if (tab3 == 0 && tab1 == 1 && tab2 == 0) {

                        $("#CustomerName").val(result.FullName);
                        $("#CustomerName1").val(result.FullName);
                        $("#CustomerPhone1").val(result.Phone);
                        $("#CustomerId").val(result.Id);
                        $("#CustomerId_DisplayText").val(result.FullName);
                        $("#TaxCode1").val(result.TaxCode);
                        $("#BankAccount1").val(result.BankAccount);
                        $("#BankName1").val(result.BankName);
                        $("#Address1").val(result.Address);
                        checkcustomertab1 = result.Id;



                    } else if (tab3 == 0 && tab1 == 0 && tab2 == 1) {
                        $("#CustomerName2").val(result.FullName);

                        $("#CustomerPhone2").val(result.Phone);
                        $("#CustomerId2").val(result.Id);
                        $("#CustomerId2_DisplayText").val(result.FullName);
                        $("#TaxCode2").val(result.TaxCode);
                        $("#BankAccount2").val(result.BankAccount);
                        $("#BankName2").val(result.BankName);
                        $("#Address2").val(result.Address);
                        checkcustomertab2 = result.Id;

                    } else {
                        $("#CustomerName3").val(result.FullName);
                        $("#CustomerPhone3").val(result.Phone);
                        $("#CustomerId3").val(result.Id);
                        $("#CustomerId3_DisplayText").val(result.FullName);
                        $("#TaxCode3").val(result.TaxCode);
                        $("#BankAccount3").val(result.BankAccount);
                        $("#BankName3").val(result.BankName);
                        $("#Address3").val(result.Address);
                        checkcustomertab3 = result.Id;

                    }

                }

                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (result) {
                console.log(result);
                HideLoading();
            }
        });
    }
    return false;
}
function currencyFormat(value) {
    value = value.toString().replace(/\D+\-/g, ''); // number only
    return value.replace(/\d(?=(?:\d{3})+(?!\d))/g, '$&.');
}

function alertPopup(title, text, type) {
    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: title,
        // (string | mandatory) the text inside the notification
        text: text,
        class_name: 'gritter-' + type + ' gritter-light' //gritter-dark
    });
};

function scrollToTopPosition(number) {
    $('html, body').animate({ scrollTop: number }, 500);
};


function getListByProperty(arr, propertyName, propertyValue) {
    var objects = arr.filter(function (obj) {
        if (obj[propertyName] !== undefined && obj[propertyName] !== null) {
            return (obj[propertyName].toString() == propertyValue.toString());
        } else {
            return null;
        }
    });

    return objects;
}

function getSelectedText() {
    if (window.getSelection) {
        return window.getSelection().toString();
    } else if (document.selection) {
        return document.selection.createRange().text;
    }
    return '';
};

function convertCSharpDateToJavaScriptDate(stringDate) {
    var src = "/Date(1302589032000+0400)/";
    //Remove all non-numeric (except the plus)
    stringDate = stringDate.replace(/[^0-9 +]/g, '');
    //Create date
    var myDate = new Date(parseInt(stringDate));
    return myDate;
};

function convertVNtoEN(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
    str = str.replace(/-+-/g, "-");
    str = str.replace(/^\-+|\-+$/g, "");

    return str;
};

function ClickEventHandlerHTML(isAppend, url, parent, formData, callbackFunction) {
    if (url != "") {
        ShowLoading();
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (isAppend)
                    $(parent).html(data);
                else
                    $(parent).prepend(data);

                HideLoading();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call();
                }
            },
            error: function (data) {
                console.log(data);
                HideLoading();
            }
        });
    }
    return false;
}
