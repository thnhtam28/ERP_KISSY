
function searchProductByBarCodeContain(barcode) {
    barcode = barcode.toLowerCase();
    var $optionList = $("#productSelectList2").find('option');

    var arrResulft = [];
    for (var i = 0; i < $optionList.length; i++) {
        var data_code = $($optionList[i]).data('code') != undefined ? $($optionList[i]).data('code').toString().toLowerCase() : undefined;
        if (barcode.indexOf(data_code) != -1)
            arrResulft.push($($optionList[i]).attr('value'));

        if (arrResulft.length == 1) {
            return arrResulft[0];
        }
    }


    return arrResulft[0];
}
//$("#PriceCustomer1").change(function () {
//    $("#TotalCustomer1").val($("#PriceCustomer1").val());
//});


function tinh_thanh_tien_moi_dong(id) {
    var input_price = $('#DetailList_' + id + '__Price');
    var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

    //Số lượng
    var input_qty = $('tr#product_item_' + id).find('.detail_item_qty');
    var qty = 1;
    if (input_qty.val() == '') {
        input_qty.val(1);
    } else {
        qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
    }
    //Thành tiền
    var total = parseFloat(price) * qty;
    var totalAmount = total;
    console.log(totalAmount);
    //Chiết khấu
    var input_fixed_discount = $('tr#product_item_' + id).find('.detail_item_fixed_discount');
    var input_discount = $('tr#product_item_' + id).find('.detail_item_discount');

    var discount = 0;
    // var fixed_discount = 0;

    if (input_discount.val() == '') {
        input_discount.val(0);
    } else {
        discount = parseInt(removeComma(input_discount.val()));
    }
    //if (input_fixed_discount.val() == '') {
    //    input_fixed_discount.val(0);
    //} else {
    //    fixed_discount = parseInt(removeComma(input_fixed_discount.val()));
    //}

    var discountAmount = discount * total / 100;
    //var fixeddiscountAmount = fixed_discount * total / 100;


    $('tr#product_item_' + id).find('.detail_item_discount_amount').val(numeral(discountAmount).format('0,0'));
    //$('tr#product_item_' + id).find('.detail_item_fixed_discount_amount').val(numeral(fixeddiscountAmount).format('0,0'));

    totalAmount = totalAmount - discountAmount;
    $('tr#product_item_' + id).find('.detail_item_total').text(numeral(totalAmount).format('0,0'));
}

function tinh_tong_tien() {
    var total = 0;
    var total1 = 0;
    var ckcd = 0;
    var ckdx = 0;
    var _total_vat = 0;
    var selector = '.detailList tr';
    $(selector).each(function (index, elem) {
        if ($(elem).find('.detail_item_total').text() != '') { // la số thì mới tính
            total += parseFloat(removeComma($(elem).find('.detail_item_total').text()));
            $("#TotalAmount").val(numeral(total).format('0,0'));


        }

        if ($(elem).find('.detail_item_qty').val() != '') { // la số thì mới tính
            total1 += parseInt($(elem).find('.detail_item_qty').val().replace(/\-./g, ''));
            $("#TongSoLuong").text(currencyFormat(total1));
        }


        //tong chiet khau dot xuat
        if ($(elem).find('.detail_item_discount_amount').val() != '') { // la số thì mới tính
            var ck2 = removeComma($(elem).find('.detail_item_discount_amount').val());
            ckdx += parseFloat(ck2);
            //  $('#mask-IrregularDiscount').val(numeral(ckdx).format('0,0'));
            $('#IrregularDiscount').val(numeral(ckdx).format('0,0'));
            //console.log(ckdx)1233;
        }


    });
}

//mcuong.fit
//*************************************************begin tính tổng tiền các tab***************************************************************
function tinh_tong_tien1() {
    //alert("vao day roi");
    var total = 0;
    var total1 = 0;
    var ckcd = 0;
    var ckdx = 0;
    var _total_vat = 0;
    var isTab = 1;
    var selector = '.detailList1 tr';
    var totalisdis;
    $(selector).each(function (index, elem) {
        if ($(elem).find('.detail_item_total_1').text() != '') { // la số thì mới tính
            total += parseFloat(removeComma($(elem).find('.detail_item_total_1').text()));
        }
        if ($(elem).find('.detail_item_qty_1').val() != '') { // la số thì mới tính
            total1 += parseInt($(elem).find('.detail_item_qty_1').val().replace(/\-./g, ''));
            $("#TongSoLuong1").text(currencyFormat(total1));
        }


        //tong chiet khau dot xuat
        if ($(elem).find('.detail_item_discount_1_amount').val() != '') { // la số thì mới tính
            var ck2 = removeComma($(elem).find('.detail_item_discount_1_amount').val());
            ckdx += parseFloat(ck2);
            //  $('#mask-IrregularDiscount').val(numeral(ckdx).format('0,0'));
            $('#IrregularDiscount_1').val(numeral(ckdx).format('0,0'));
            //console.log(ckdx);
        }
    });

    ///
    totalisdis = total;
    if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {
        var i;
        var first = 0;
        var Ismoneys = false;
        var k = 0;
        var k_id = 0;
        var k_id_cha = 0;
        var totalKm = 0;
        for (i = 0; i < ArrHdDiscountAll.length; i++) {
            if (ArrHdDiscountAll[i].type == "ALLCN") {
                if (ArrHdDiscountAll[i].Minvalue <= total) {
                    if (ArrHdDiscountAll[i].IsMoney == true) {
                        if (first == 0) {
                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                            k = ArrHdDiscountAll[i].CommissionValue;
                            //alert(ArrHdDiscountAll[i].id);
                            //alert(ArrHdDiscountAll[i].id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            first = 1;

                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                        Ismoneys = true;

                    } else {
                        if (first == 0) {

                            k = ArrHdDiscountAll[i].CommissionValue;

                            //alert(ArrHdDiscountAll[i].Id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            //alert(ArrHdDiscountAll[i].Id_cha);
                            //alert(k_id_cha);

                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            first = 1;
                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                    }

                }
                continue;
            }
            else if (ArrHdDiscountAll[i].type == "NV") {
                var customerIdNV1 = $("#CustomerId1").val();

                if (customerIdNV1 != null && customerIdNV1.length > 0) {
                    customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                    var j;
                    for (j = 0; j < customerNV.length; j++) {
                        if (customerIdNV1 == customerNV[j]) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }



            }
            else {
                if (ArrHdDiscountAll[i].type == "CUST") {
                    var customerIdNV1 = $("#CustomerId1").val();
                    if (customerIdNV1 != null && customerIdNV1.length > 0) {
                        var cust = ArrHdDiscountAll[i].listIDcustomer;
                        if (customerIdNV1 == cust) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }




            }



        }
        if (totalKm != 0) {

            if (k != 0) {
                totalisdis -= totalKm;
            }
            $("#TotalAmount1").val(numeral(totalisdis).format('0,0'));
            $("#Tab1Money").val(numeral(totalisdis).format('0,0'));
            $("#MoneyPay").val(numeral(totalisdis).format('0,0'));
            $("#TotalPay").val(numeral(totalisdis).format('0,0'));
            $("#Tab1Total").val(totalisdis);
            //alert(k_id);
            //alert(k_id_cha);
            if (Ismoneys == true) {
                $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                $("#Tab11Discount").val(numeral(k).format('0,0') + " đ");
                $("#Tab1Discount").val(k + "-đ-" + k_id + "-" + k_id_cha);
            } else {
                $("#DiscountTab1").val(k + " %");
                $("#Tab11Discount").val(k + " %");
                $("#Tab1Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
            }
        } else {
            $("#DiscountTab1").val('');
            $("#Tab11Discount").val('');
            $("#Tab1Discount").val('');
        }



        //ArrHdDiscountAll[i].TypeApply;
        //tmp.type = ArrHdDiscountAll[i].type;
        //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
        //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
        //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
        //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
    } else {

        HideLoading();
    }

   
   
    $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
    $("#TotalAmountPay").val(numeral(totalisdis).format('0,0')); //Tiền hàng phải
    /////
    var check = document.getElementById("TypeDiscountHD");
    var discount = $("#DiscountAll").val();
    if (isNull(discount) == 1) {
        discount = 0;
    }
    if (check.checked == true) {
        totalisdis -= parseFloat(removeComma(discount));
    }
    else {
        totalisdis -= parseFloat(removeComma(discount) * totalisdis / 100);
    }
    $("#TotalAmountTab1").val(numeral(total).format('0,0')); //Tổng 
    $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
    $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
    $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
    $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1

    //hoapd them vao de reset lai khi co su thay doi tien tong
    $("#ATMPay").val(""); //Tổng 
    $("#TransferPay").val(""); //Tổng 
    $("#ATM" + isTab).val("");
    $("#Transfer" + isTab).val("");
    $("#ChangeMoney").val("");
    
}
function tinh_tong_tien2() {
    var total = 0;
    var total1 = 0;
    var ckcd = 0;
    var ckdx = 0;
    var _total_vat = 0;
    var totalisdis;
    var isTab = 2;
    var selector = '.detailList2 tr';
    var pricus1 = $("#PriceCustomer2").val();
    console.log(pricus1)
    $(selector).each(function (index, elem) {
        console.log('after')
        if ($(elem).find('.detail_item_total_2').text() != '') { // la số thì mới tính
            total += parseFloat(removeComma($(elem).find('.detail_item_total_2').text()));

        }
        if ($(elem).find('.detail_item_qty_2').val() != '') { // la số thì mới tính
            total1 += parseInt($(elem).find('.detail_item_qty_2').val().replace(/\-./g, ''));
            $("#TongSoLuong2").text(currencyFormat(total1));
        }
        //tong chiet khau dot xuat
        if ($(elem).find('.detail_item_discount_2_amount').val() != '') { // la số thì mới tính
            var ck2 = removeComma($(elem).find('.detail_item_discount_2_amount').val());
            ckdx += parseFloat(ck2);
            //  $('#mask-IrregularDiscount').val(numeral(ckdx).format('0,0'));
            $('#IrregularDiscount_2').val(numeral(ckdx).format('0,0'));
            //console.log(ckdx);
        }

    });

    //--
    totalisdis = total;

    if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {
        var i;
        var first = 0;
        var Ismoneys = false;
        var k = 0;
        var k_id = 0;
        var k_id_cha = 0;
        var totalKm = 0;
        for (i = 0; i < ArrHdDiscountAll.length; i++) {
            if (ArrHdDiscountAll[i].type == "ALLCN") {
                if (ArrHdDiscountAll[i].Minvalue <= total) {
                    if (ArrHdDiscountAll[i].IsMoney == true) {
                        if (first == 0) {
                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                            k = ArrHdDiscountAll[i].CommissionValue;
                            //alert(ArrHdDiscountAll[i].id);
                            //alert(ArrHdDiscountAll[i].id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            first = 1;

                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                        Ismoneys = true;

                    } else {
                        if (first == 0) {

                            k = ArrHdDiscountAll[i].CommissionValue;

                            //alert(ArrHdDiscountAll[i].Id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            //alert(ArrHdDiscountAll[i].Id_cha);
                            //alert(k_id_cha);

                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            first = 1;
                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                    }

                }
                continue;
            }
            else if (ArrHdDiscountAll[i].type == "NV") {
                var customerIdNV1 = $("#CustomerId2").val();

                if (customerIdNV1 != null && customerIdNV1.length > 0) {
                    customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                    var j;
                    for (j = 0; j < customerNV.length; j++) {
                        if (customerIdNV1 == customerNV[j]) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }



            }
            else {
                if (ArrHdDiscountAll[i].type == "CUST") {
                    var customerIdNV1 = $("#CustomerId2").val();
                    if (customerIdNV1 != null && customerIdNV1.length > 0) {
                        var cust = ArrHdDiscountAll[i].listIDcustomer;
                        if (customerIdNV1 == cust) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }
            }

        }
        if (totalKm != 0) {
            //alert(k);
            if (k != 0) {
                totalisdis -= totalKm;
            }
            $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
            $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
            $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
            $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
            $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1
            if (Ismoneys == true) {
                $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                $("#Tab22Discount").val(numeral(k).format('0,0') + " đ");
                $("#Tab2Discount").val(k + "-đ-" + k_id + "-" + k_id_cha);
            } else {
                $("#DiscountTab1").val(k + " %");
                $("#Tab22Discount").val(k + " %");
                $("#Tab2Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
            }
        } else {
            $("#DiscountTab1").val('');
            $("#Tab22Discount").val('');
            $("#Tab2Discount").val('');
        }

        //ArrHdDiscountAll[i].TypeApply;
        //tmp.type = ArrHdDiscountAll[i].type;
        //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
        //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
        //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
        //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
    } else {

        HideLoading();
    }
    ///
    $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
    $("#TotalAmountPay").val(numeral(totalisdis).format('0,0')); //Tiền hàng phải

    var check = document.getElementById("TypeDiscountHD");
    var discount = $("#DiscountAll").val();
    if (isNull(discount) == 1) {
        discount = 0;
    }
    if (check.checked == true) {
        totalisdis -= parseFloat(removeComma(discount));
    }
    else {
        totalisdis -= parseFloat(removeComma(discount) * totalisdis / 100);
    }
    $("#TotalAmountTab1").val(numeral(total).format('0,0')); //Tổng 
    $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
    $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
    $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
    $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1


    //hoapd them vao de reset lai khi co su thay doi tien tong
    $("#ATMPay").val(""); //Tổng 
    $("#TransferPay").val(""); //Tổng 
    $("#ATM" + isTab).val("");
    $("#Transfer" + isTab).val("");
    $("#ChangeMoney").val("");
    /////
}
function tinh_tong_tien3() {
    var total = 0;
    var total1 = 0;
    var ckcd = 0;
    var ckdx = 0;
    var _total_vat = 0;
    var totalisdis;
    var isTab = 3;
    var selector = '.detailList3 tr';
    $(selector).each(function (index, elem) {
        if ($(elem).find('.detail_item_total_3').text() != '') { // la số thì mới tính
            total += parseFloat(removeComma($(elem).find('.detail_item_total_3').text()));

        }


        if ($(elem).find('.detail_item_qty_3').val() != '') { // la số thì mới tính
            total1 += parseInt($(elem).find('.detail_item_qty_3').val().replace(/\-./g, ''));
            $("#TongSoLuong3").text(currencyFormat(total1));
        }


        //tong chiet khau dot xuat
        if ($(elem).find('.detail_item_discount_3_amount').val() != '') { // la số thì mới tính
            var ck2 = removeComma($(elem).find('.detail_item_discount_3_amount').val());
            ckdx += parseFloat(ck2);
            //  $('#mask-IrregularDiscount').val(numeral(ckdx).format('0,0'));
            $('#IrregularDiscount_3').val(numeral(ckdx).format('0,0'));
            //console.log(ckdx);
        }


    });
    //////
    totalisdis = total;

    if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {
        var i;
        var first = 0;
        var Ismoneys = false;
        var k = 0;
        var k_id = 0;
        var k_id_cha = 0;
        var totalKm = 0;
        for (i = 0; i < ArrHdDiscountAll.length; i++) {
            if (ArrHdDiscountAll[i].type == "ALLCN") {
                if (ArrHdDiscountAll[i].Minvalue <= total) {
                    if (ArrHdDiscountAll[i].IsMoney == true) {
                        if (first == 0) {
                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                            k = ArrHdDiscountAll[i].CommissionValue;
                            //alert(ArrHdDiscountAll[i].id);
                            //alert(ArrHdDiscountAll[i].id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            first = 1;

                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                        Ismoneys = true;

                    } else {
                        if (first == 0) {

                            k = ArrHdDiscountAll[i].CommissionValue;

                            //alert(ArrHdDiscountAll[i].Id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            //alert(ArrHdDiscountAll[i].Id_cha);
                            //alert(k_id_cha);

                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            first = 1;
                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                    }

                }
                continue;
            }
            else if (ArrHdDiscountAll[i].type == "NV") {
                var customerIdNV1 = $("#CustomerId3").val();

                if (customerIdNV1 != null && customerIdNV1.length > 0) {
                    customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                    var j;
                    for (j = 0; j < customerNV.length; j++) {
                        if (customerIdNV1 == customerNV[j]) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }



            }
            else {
                if (ArrHdDiscountAll[i].type == "CUST") {
                    var customerIdNV1 = $("#CustomerId3").val();
                    if (customerIdNV1 != null && customerIdNV1.length > 0) {
                        var cust = ArrHdDiscountAll[i].listIDcustomer;
                        if (customerIdNV1 == cust) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }




            }



        }
        if (totalKm != 0) {
            if (k != 0) {
                totalisdis -= totalKm;
            }
            $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
            $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
            $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
            $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
            $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1
            if (Ismoneys == true) {
                $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                $("#Tab33Discount").val(numeral(k).format('0,0') + " đ");
                $("#Tab3Discount").val(k + "-đ-" + k_id + "-" + k_id_cha);
            } else {
                $("#DiscountTab1").val(k + " %");
                $("#Tab33Discount").val(k + " %");
                $("#Tab3Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
            }
        } else {
            $("#DiscountTab1").val('');
            $("#Tab33Discount").val('');
            $("#Tab3Discount").val('');
        }

        //ArrHdDiscountAll[i].TypeApply;
        //tmp.type = ArrHdDiscountAll[i].type;
        //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
        //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
        //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
        //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
    } else {

        HideLoading();
    }
    ////

    $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
    $("#TotalAmountPay").val(numeral(totalisdis).format('0,0')); //Tiền hàng phải
    var check = document.getElementById("TypeDiscountHD");
    var discount = $("#DiscountAll").val();
    if (isNull(discount) == 1) {
        discount = 0;
    }
    if (check.checked == true) {
        totalisdis -= parseFloat(removeComma(discount));
    }
    else {
        totalisdis -= parseFloat(removeComma(discount) * totalisdis / 100);
    }
    $("#TotalAmountTab1").val(numeral(total).format('0,0')); //Tổng
    $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
    $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
    $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
    $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1



    //hoapd them vao de reset lai khi co su thay doi tien tong
    $("#ATMPay").val(""); //Tổng 
    $("#TransferPay").val(""); //Tổng 
    $("#ATM" + isTab).val("");
    $("#Transfer" + isTab).val("");
    $("#ChangeMoney").val("");
    /////
}
function tinh_tong_tien4() {
    var total = 0;
    var total1 = 0;
    var ckcd = 0;
    var ckdx = 0;
    var _total_vat = 0;
    var totalisdis;
    var isTab = 4;
    var selector = '.detailList4 tr';
    var pricus1 = $("#PriceCustomer4").val();
    console.log(pricus1)
    $(selector).each(function (index, elem) {
        console.log('after')
        if ($(elem).find('.detail_item_total_4').text() != '') { // la số thì mới tính
            total += parseFloat(removeComma($(elem).find('.detail_item_total_4').text()));

        }

        if ($(elem).find('.detail_item_qty_4').val() != '') { // la số thì mới tính
            total1 += parseInt($(elem).find('.detail_item_qty_4').val().replace(/\-./g, ''));
            $("#TongSoLuong4").text(currencyFormat(total1));
        }
        //tong chiet khau dot xuat
        if ($(elem).find('.detail_item_discount_4_amount').val() != '') { // la số thì mới tính
            var ck2 = removeComma($(elem).find('.detail_item_discount_4_amount').val());
            ckdx += parseFloat(ck2);
            //  $('#mask-IrregularDiscount').val(numeral(ckdx).format('0,0'));
            $('#IrregularDiscount_4').val(numeral(ckdx).format('0,0'));
            //console.log(ckdx);
        }

    });
    //////
    totalisdis = total;

    if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {
        var i;
        var first = 0;
        var Ismoneys = false;
        var k = 0;
        var k_id = 0;
        var k_id_cha = 0;
        var totalKm = 0;
        for (i = 0; i < ArrHdDiscountAll.length; i++) {
            if (ArrHdDiscountAll[i].type == "ALLCN") {
                if (ArrHdDiscountAll[i].Minvalue <= total) {
                    if (ArrHdDiscountAll[i].IsMoney == true) {
                        if (first == 0) {
                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                            k = ArrHdDiscountAll[i].CommissionValue;
                            //alert(ArrHdDiscountAll[i].id);
                            //alert(ArrHdDiscountAll[i].id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            first = 1;

                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                        Ismoneys = true;

                    } else {
                        if (first == 0) {

                            k = ArrHdDiscountAll[i].CommissionValue;

                            //alert(ArrHdDiscountAll[i].Id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            //alert(ArrHdDiscountAll[i].Id_cha);
                            //alert(k_id_cha);

                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            first = 1;
                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                    }

                }
                continue;
            }
            else if (ArrHdDiscountAll[i].type == "NV") {
                var customerIdNV1 = $("#CustomerId4").val();

                if (customerIdNV1 != null && customerIdNV1.length > 0) {
                    customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                    var j;
                    for (j = 0; j < customerNV.length; j++) {
                        if (customerIdNV1 == customerNV[j]) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }



            }
            else {
                if (ArrHdDiscountAll[i].type == "CUST") {
                    var customerIdNV1 = $("#CustomerId4").val();
                    if (customerIdNV1 != null && customerIdNV1.length > 0) {
                        var cust = ArrHdDiscountAll[i].listIDcustomer;
                        if (customerIdNV1 == cust) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }




            }



        }
        if (totalKm != 0) {
            if (k != 0) {
                totalisdis -= totalKm;
            }
            $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
            $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
            $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
            $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
            $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1
            if (Ismoneys == true) {
                $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                $("#Tab44Discount").val(numeral(k).format('0,0') + " đ");
                $("#Tab4Discount").val(k + "-đ-" + k_id + "-" + k_id_cha);
            } else {
                $("#DiscountTab1").val(k + " %");
                $("#Tab44Discount").val(k + " %");
                $("#Tab4Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
            }
        } else {
            $("#DiscountTab1").val('');
            $("#Tab44Discount").val('');
            $("#Tab4Discount").val('');
        }

        //ArrHdDiscountAll[i].TypeApply;
        //tmp.type = ArrHdDiscountAll[i].type;
        //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
        //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
        //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
        //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
    } else {

        HideLoading();
    }
    ///

    $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
    $("#TotalAmountPay").val(numeral(totalisdis).format('0,0')); //Tiền hàng phải
    var check = document.getElementById("TypeDiscountHD");
    var discount = $("#DiscountAll").val();
    if (isNull(discount) == 1) {
        discount = 0;
    }
    if (check.checked == true) {
        totalisdis -= parseFloat(removeComma(discount));
    }
    else {
        totalisdis -= parseFloat(removeComma(discount) * totalisdis / 100);
    }
    $("#TotalAmountTab1").val(numeral(total).format('0,0')); //Tổng
    $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
    $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
    $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
    $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1

    //hoapd them vao de reset lai khi co su thay doi tien tong
    $("#ATMPay").val(""); //Tổng 
    $("#TransferPay").val(""); //Tổng 
    $("#ATM" + isTab).val("");
    $("#Transfer" + isTab).val("");
    $("#ChangeMoney").val("");
    /////
}
function tinh_tong_tien5() {
    var total = 0;
    var total1 = 0;
    var ckcd = 0;
    var ckdx = 0;
    var _total_vat = 0;
    var totalisdis;
    var isTab = 5;
    var selector = '.detailList5 tr';
    var pricus1 = $("#PriceCustomer5").val();
    console.log(pricus1)
    $(selector).each(function (index, elem) {
        console.log('after')
        if ($(elem).find('.detail_item_total_5').text() != '') { // la số thì mới tính
            total += parseFloat(removeComma($(elem).find('.detail_item_total_5').text()));

        }


        if ($(elem).find('.detail_item_qty_5').val() != '') { // la số thì mới tính
            total1 += parseInt($(elem).find('.detail_item_qty_5').val().replace(/\-./g, ''));
            $("#TongSoLuong5").text(currencyFormat(total1));
        }


        //tong chiet khau dot xuat
        if ($(elem).find('.detail_item_discount_5_amount').val() != '') { // la số thì mới tính
            var ck2 = removeComma($(elem).find('.detail_item_discount_5_amount').val());
            ckdx += parseFloat(ck2);
            //  $('#mask-IrregularDiscount').val(numeral(ckdx).format('0,0'));
            $('#IrregularDiscount_5').val(numeral(ckdx).format('0,0'));
            //console.log(ckdx);
        }


    });
    //////
    totalisdis = total;

    if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {
        var i;
        var first = 0;
        var Ismoneys = false;
        var k = 0;
        var k_id = 0;
        var k_id_cha = 0;
        var totalKm = 0;
        for (i = 0; i < ArrHdDiscountAll.length; i++) {
            if (ArrHdDiscountAll[i].type == "ALLCN") {
                if (ArrHdDiscountAll[i].Minvalue <= total) {
                    if (ArrHdDiscountAll[i].IsMoney == true) {
                        if (first == 0) {
                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                            k = ArrHdDiscountAll[i].CommissionValue;
                            //alert(ArrHdDiscountAll[i].id);
                            //alert(ArrHdDiscountAll[i].id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            first = 1;

                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                        Ismoneys = true;

                    } else {
                        if (first == 0) {

                            k = ArrHdDiscountAll[i].CommissionValue;

                            //alert(ArrHdDiscountAll[i].Id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            //alert(ArrHdDiscountAll[i].Id_cha);
                            //alert(k_id_cha);

                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            first = 1;
                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                    }

                }
                continue;
            }
            else if (ArrHdDiscountAll[i].type == "NV") {
                var customerIdNV1 = $("#CustomerId5").val();

                if (customerIdNV1 != null && customerIdNV1.length > 0) {
                    customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                    var j;
                    for (j = 0; j < customerNV.length; j++) {
                        if (customerIdNV1 == customerNV[j]) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }



            }
            else {
                if (ArrHdDiscountAll[i].type == "CUST") {
                    var customerIdNV1 = $("#CustomerId5").val();
                    if (customerIdNV1 != null && customerIdNV1.length > 0) {
                        var cust = ArrHdDiscountAll[i].listIDcustomer;
                        if (customerIdNV1 == cust) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }




            }



        }
        if (totalKm != 0) {
            if (k != 0) {
                totalisdis -= totalKm;
            }
            $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
            $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
            $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
            $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
            $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1
            if (Ismoneys == true) {
                $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                $("#Tab55Discount").val(numeral(k).format('0,0') + " đ");
                $("#Tab5Discount").val(k + "-đ-" + k_id + "-" + k_id_cha);
            } else {
                $("#DiscountTab1").val(k + " %");
                $("#Tab55Discount").val(k + " %");
                $("#Tab5Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
            }
        } else {
            $("#DiscountTab1").val('');
            $("#Tab55Discount").val('');
            $("#Tab5Discount").val('');
        }

        //ArrHdDiscountAll[i].TypeApply;
        //tmp.type = ArrHdDiscountAll[i].type;
        //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
        //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
        //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
        //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
    } else {

        HideLoading();
    }
    ///
    $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
    $("#TotalAmountPay").val(numeral(totalisdis).format('0,0')); //Tiền hàng phải
    var check = document.getElementById("TypeDiscountHD");
    var discount = $("#DiscountAll").val();
    if (isNull(discount) == 1) {
        discount = 0;
    }
    if (check.checked == true) {
        totalisdis -= parseFloat(removeComma(discount));
    }
    else {
        totalisdis -= parseFloat(removeComma(discount) * totalisdis / 100);
    }
    $("#TotalAmountTab1").val(numeral(total).format('0,0')); //Tổng
    $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
    $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
    $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
    $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1

    //hoapd them vao de reset lai khi co su thay doi tien tong
    $("#ATMPay").val(""); //Tổng 
    $("#TransferPay").val(""); //Tổng 
    $("#ATM" + isTab).val("");
    $("#Transfer" + isTab).val("");
    $("#ChangeMoney").val("");
    /////
}
function tinh_tong_tien6() {
    var total = 0;
    var total1 = 0;
    var ckcd = 0;
    var ckdx = 0;
    var _total_vat = 0;
    var totalisdis;
    var isTab = 6;
    var selector = '.detailList6 tr';
    var pricus1 = $("#PriceCustomer6").val();
    console.log(pricus1)
    $(selector).each(function (index, elem) {
        console.log('after')
        if ($(elem).find('.detail_item_total_6').text() != '') { // la số thì mới tính
            total += parseFloat(removeComma($(elem).find('.detail_item_total_6').text()));

        }
        //$("#PriceCustomer6").keyup(function () {
        //    //xử lý số tiền nhận từ customer
        //    $("#TotalAmount6").val(numeral(total).format('0,0'));
        //    var pricus = $("#PriceCustomer6").val();
        //    var totalcus1 = pricus - total;
        //    console.log('test:' + $("#TotalAmount6").text());
        //    //console.log(pricus - total);
        //    if (totalcus1 < 0) {

        //        $("#TotalCustomer6").val('Thiếu :' + ' ' + numeral(totalcus1).format('0,0') + ' ' + 'VNĐ');
        //    } else if (totalcus1 == 0) {
        //        $("#TotalCustomer6").val('Thiếu :' + ' ' + 0 + ' ' + 'VNĐ');
        //    }
        //    else {

        //        $("#TotalCustomer6").val('Tiền thừa :' + ' ' + numeral(totalcus1).format('0,0') + ' ' + 'VNĐ');
        //    }


        //});

        if ($(elem).find('.detail_item_qty_6').val() != '') { // la số thì mới tính
            total1 += parseInt($(elem).find('.detail_item_qty_6').val().replace(/\-./g, ''));
            $("#TongSoLuong6").text(currencyFormat(total1));
        }


        //tong chiet khau dot xuat
        if ($(elem).find('.detail_item_discount_6_amount').val() != '') { // la số thì mới tính
            var ck2 = removeComma($(elem).find('.detail_item_discount_6_amount').val());
            ckdx += parseFloat(ck2);
            //  $('#mask-IrregularDiscount').val(numeral(ckdx).format('0,0'));
            $('#IrregularDiscount_6').val(numeral(ckdx).format('0,0'));
            //console.log(ckdx);
        }


    });

    ///
    totalisdis = total;

    if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {
        var i;
        var first = 0;
        var Ismoneys = false;
        var k = 0;
        var k_id = 0;
        var k_id_cha = 0;
        var totalKm = 0;
        for (i = 0; i < ArrHdDiscountAll.length; i++) {
            if (ArrHdDiscountAll[i].type == "ALLCN") {
                if (ArrHdDiscountAll[i].Minvalue <= total) {
                    if (ArrHdDiscountAll[i].IsMoney == true) {
                        if (first == 0) {
                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                            k = ArrHdDiscountAll[i].CommissionValue;
                            //alert(ArrHdDiscountAll[i].id);
                            //alert(ArrHdDiscountAll[i].id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            first = 1;

                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                        Ismoneys = true;

                    } else {
                        if (first == 0) {

                            k = ArrHdDiscountAll[i].CommissionValue;

                            //alert(ArrHdDiscountAll[i].Id_cha);
                            k_id = ArrHdDiscountAll[i].id;
                            k_id_cha = ArrHdDiscountAll[i].id_cha;
                            //alert(ArrHdDiscountAll[i].Id_cha);
                            //alert(k_id_cha);

                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            first = 1;
                        } else {
                            k = ArrHdDiscountAll[i].CommissionValue;
                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                            if (tmp > totalKm) {
                                totalKm = tmp;
                            }
                        }
                    }

                }
                continue;
            }
            else if (ArrHdDiscountAll[i].type == "NV") {
                var customerIdNV1 = $("#CustomerId6").val();

                if (customerIdNV1 != null && customerIdNV1.length > 0) {
                    customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                    var j;
                    for (j = 0; j < customerNV.length; j++) {
                        if (customerIdNV1 == customerNV[j]) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }



            }
            else {
                if (ArrHdDiscountAll[i].type == "CUST") {
                    var customerIdNV1 = $("#CustomerId6").val();
                    if (customerIdNV1 != null && customerIdNV1.length > 0) {
                        var cust = ArrHdDiscountAll[i].listIDcustomer;
                        if (customerIdNV1 == cust) {
                            if (ArrHdDiscountAll[i].Minvalue <= total) {
                                if (ArrHdDiscountAll[i].IsMoney == true) {
                                    if (first == 0) {
                                        totalKm = ArrHdDiscountAll[i].CommissionValue;
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = ArrHdDiscountAll[i].CommissionValue;
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                    Ismoneys = true;

                                } else {
                                    if (first == 0) {
                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        k_id = ArrHdDiscountAll[i].id;
                                        k_id_cha = ArrHdDiscountAll[i].id_cha;

                                        totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        first = 1;
                                    } else {

                                        k = ArrHdDiscountAll[i].CommissionValue;
                                        tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                        if (tmp > totalKm) {
                                            totalKm = tmp;
                                        }
                                    }
                                }

                            }
                        }
                        continue;

                    }
                }




            }



        }
        if (totalKm != 0) {
            if (k != 0) {
                totalisdis -= totalKm;
            }
            $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
            $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
            $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
            $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
            $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1
            if (Ismoneys == true) {
                $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                $("#Tab66Discount").val(numeral(k).format('0,0') + " đ");
                $("#Tab6Discount").val(k + "-đ-" + k_id + "-" + k_id_cha);
            } else {
                $("#DiscountTab1").val(k + " %");
                $("#Tab66Discount").val(k + " %");
                $("#Tab6Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
            }
        } else {
            $("#DiscountTab1").val('');
            $("#Tab66Discount").val('');
            $("#Tab6Discount").val('');
        }

        //ArrHdDiscountAll[i].TypeApply;
        //tmp.type = ArrHdDiscountAll[i].type;
        //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
        //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
        //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
        //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
    } else {

        HideLoading();
    }
    ///
    $("#TotalAmountPay").val(numeral(totalisdis).format('0,0')); //Tiền hàng phải
    $("#TotalAmount1").val(numeral(totalisdis).format('0,0')); //thanh toán trái
    var check = document.getElementById("TypeDiscountHD");
    var discount = $("#DiscountAll").val();
    if (isNull(discount) == 1) {
        discount = 0;
    }
    if (check.checked == true) {
        totalisdis -= parseFloat(removeComma(discount));
    }
    else {
        totalisdis -= parseFloat(removeComma(discount) * totalisdis / 100);
    }
    $("#TotalAmountTab1").val(numeral(total).format('0,0')); //Tổng 
    $("#TotalPay").val(numeral(totalisdis).format('0,0')); //Thanh toán phải
    $("#MoneyPay").val(numeral(totalisdis).format('0,0')); //Tiền mặt phải
    $("#Tab" + isTab + "Money").val(numeral(totalisdis).format('0,0'));  // Tiền mặt 1
    $("#Tab" + isTab + "Total").val(totalisdis); //Thanh toán 1


    //hoapd them vao de reset lai khi co su thay doi tien tong
    $("#ATMPay").val(""); //Tổng 
    $("#TransferPay").val(""); //Tổng 
    $("#ATM" + isTab).val("");
    $("#Transfer" + isTab).val("");
    $("#ChangeMoney").val("");
    /////
}
//*************************************************End tính tổng tiền các tab***************************************************************
//mcuong.fit@gmail.com
//*************************************************begin Khuyễn mãi nhóm vip Theo hàng hóa các tab*********************************************



//*************************************************End tính Khuyễn mãi nhóm vip Theo hàng hóa các tab***************************************************************



//mcuong.fit@gmail.com
//*************************************************begin Khuyễn mãi các tab***************************************************************
function tinhkhuyenmaiHd() {
    if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
        var total = 0;
        var totalKm = 0;
        var tmp = 0;
        var customerNV = new Array();
        var selector = '.detailList1 tr';
        $(selector).each(function (index, elem) {
            if ($(elem).find('.detail_item_total_1').text() != '') { // la số thì mới tính
                total += parseFloat(removeComma($(elem).find('.detail_item_total_1').text()));
                //xử lý số tiền nhận từ customer
            }

        });

        if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {

            var i;
            var first = 0;
            var Ismoneys = false;
            var k = 0;
            for (i = 0; i < ArrHdDiscountAll.length; i++) {
                if (ArrHdDiscountAll[i].type == "ALLCN") {
                    if (ArrHdDiscountAll[i].Minvalue <= total) {
                        if (ArrHdDiscountAll[i].IsMoney == true) {
                            if (first == 0) {
                                totalKm = ArrHdDiscountAll[i].CommissionValue;
                                k = ArrHdDiscountAll[i].CommissionValue;
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = ArrHdDiscountAll[i].CommissionValue;
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                            Ismoneys = true;

                        } else {
                            if (first == 0) {

                                k = ArrHdDiscountAll[i].CommissionValue;
                                totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                        }

                    }
                    continue;
                }
                else if (ArrHdDiscountAll[i].type == "NV") {
                    var customerIdNV11 = $("#CustomerId").val();
                    if (customerIdNV11 != null && customerIdNV11.length > 0) {
                        customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                        var j;
                        for (j = 0; j < customerNV.length; j++) {
                            if (customerIdNV11 == customerNV[j]) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;

                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;

                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);

                                            first = 1;
                                        } else {
                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);

                                            if (tmp > totalKm) {
                                                k = ArrHdDiscountAll[i].CommissionValue;

                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }



                }
                else {
                    if (ArrHdDiscountAll[i].type == "CUST") {
                        var customerIdNV11 = $("#CustomerId").val();
                        if (customerIdNV11 != null && customerIdNV11.length > 0) {
                            var cust = ArrHdDiscountAll[i].listIDcustomer;
                            if (customerIdNV11 == cust) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;

                                            first = 1;
                                        } else {

                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);

                                            first = 1;
                                        } else {

                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);

                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }




                }



            }

            if (totalKm != 0) {
                if (k != 0) {
                    total -= totalKm;
                }
                $("#TotalAmount1").val(numeral(total).format('0,0'));
                $("#TotalPay").val(numeral(total).format('0,0'));
                $("#Tab1Total").val(numeral(total).format('0,0'));
                if (Ismoneys == true) {
                    $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                    $("#Tab11Discount").val(numeral(k).format('0,0') + " đ");
                    $("#Tab1Discount").val(k + " đ");
                } else {
                    $("#DiscountTab1").val(k + " %");
                    $("#Tab11Discount").val(k + " %");
                    $("#Tab1Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
                }
            } else {
                $("#DiscountTab1").val('');
                $("#Tab11Discount").val('');
                $("#Tab1Discount").val('');
            }

            //ArrHdDiscountAll[i].TypeApply;
            //tmp.type = ArrHdDiscountAll[i].type;
            //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
            //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
            //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
            //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
        } else {

            HideLoading();
        }
    } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
        var total = 0;

        var totalKm = 0;
        var tmp = 0;
        var customerNV = new Array();
        var selector = '.detailList2 tr';
        $(selector).each(function (index, elem) {
            if ($(elem).find('.detail_item_total_2').text() != '') { // la số thì mới tính
                total += parseFloat(removeComma($(elem).find('.detail_item_total_2').text()));
                //xử lý số tiền nhận từ customer
            }

        });

        if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {

            var i;
            var first = 0;
            var Ismoneys = false;
            var k = 0;
            for (i = 0; i < ArrHdDiscountAll.length; i++) {
                if (ArrHdDiscountAll[i].type == "ALLCN") {
                    if (ArrHdDiscountAll[i].Minvalue <= total) {
                        if (ArrHdDiscountAll[i].IsMoney == true) {
                            if (first == 0) {
                                totalKm = ArrHdDiscountAll[i].CommissionValue;
                                k = ArrHdDiscountAll[i].CommissionValue;
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = total - ArrHdDiscountAll[i].CommissionValue;
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                            Ismoneys = true;

                        } else {
                            if (first == 0) {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                        }

                    }
                    continue;
                }
                else if (ArrHdDiscountAll[i].type == "NV") {
                    var customerIdNV22 = $("#CustomerId2").val();

                    if (customerIdNV22 != null && customerIdNV22.length > 0) {
                        customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                        var j;
                        for (j = 0; j < customerNV.length; j++) {
                            if (customerIdNV1 == customerNV[j]) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {

                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                k = ArrHdDiscountAll[i].CommissionValue;
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }



                }
                else {
                    if (ArrHdDiscountAll[i].type == "CUST") {
                        var customerIdNV22 = $("#CustomerId2").val();
                        if (customerIdNV22 != null && customerIdNV22.length > 0) {
                            var cust = ArrHdDiscountAll[i].listIDcustomer;
                            if (customerIdNV22 == cust) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }




                }



            }


            if (totalKm != 0) {
                if (k != 0) {
                    total -= totalKm;
                }
                $("#TotalAmount1").val(numeral(total).format('0,0'));
                $("#TotalPay").val(numeral(total).format('0,0'));
                $("#Tab2Total").val(numeral(total).format('0,0'));
                if (Ismoneys == true) {
                    $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                    $("#Tab22Discount").val(numeral(k).format('0,0') + " đ");
                    $("#Tab2Discount").val(k + " đ");
                } else {
                    $("#DiscountTab1").val(k + " %");
                    $("#Tab22Discount").val(k + " %");
                    $("#Tab2Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
                }
            } else {
                $("#DiscountTab1").val('');
                $("#Tab22Discount").val('');
                $("#Tab2Discount").val('');
            }

            //ArrHdDiscountAll[i].TypeApply;
            //tmp.type = ArrHdDiscountAll[i].type;
            //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
            //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
            //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
            //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
        } else {

            HideLoading();
        }
    } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
        var total = 0;
        var totalKm = 0;
        var tmp = 0;
        var customerNV = new Array();
        var selector = '.detailList3 tr';
        $(selector).each(function (index, elem) {
            if ($(elem).find('.detail_item_total_3').text() != '') { // la số thì mới tính
                total += parseFloat(removeComma($(elem).find('.detail_item_total_3').text()));
                //xử lý số tiền nhận từ customer
            }

        });

        if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {

            var i;
            var first = 0;
            var Ismoneys = false;
            var k = 0;
            for (i = 0; i < ArrHdDiscountAll.length; i++) {
                if (ArrHdDiscountAll[i].type == "ALLCN") {
                    if (ArrHdDiscountAll[i].Minvalue <= total) {
                        if (ArrHdDiscountAll[i].IsMoney == true) {
                            if (first == 0) {
                                totalKm = ArrHdDiscountAll[i].CommissionValue;
                                k = ArrHdDiscountAll[i].CommissionValue;
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = ArrHdDiscountAll[i].CommissionValue;
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                            Ismoneys = true;

                        } else {
                            if (first == 0) {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                        }

                    }
                    continue;
                }
                else if (ArrHdDiscountAll[i].type == "NV") {
                    var customerIdNV33 = $("#CustomerId3").val();

                    if (customerIdNV33 != null && customerIdNV33.length > 0) {
                        customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                        var j;
                        for (j = 0; j < customerNV.length; j++) {
                            if (customerIdNV1 == customerNV[j]) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {

                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                k = ArrHdDiscountAll[i].CommissionValue;
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }



                }
                else {
                    if (ArrHdDiscountAll[i].type == "CUST") {
                        var customerIdNV33 = $("#CustomerId3").val();
                        if (customerIdNV33 != null && customerIdNV33.length > 0) {
                            var cust = ArrHdDiscountAll[i].listIDcustomer;
                            if (customerIdNV33 == cust) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }




                }



            }


            if (totalKm != 0) {
                if (k != 0) {
                    total -= totalKm;
                }
                $("#TotalAmount1").val(numeral(total).format('0,0'));
                $("#TotalPay").val(numeral(total).format('0,0'));
                $("#Tab3Total").val(numeral(total).format('0,0'));
                if (Ismoneys == true) {
                    $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                    $("#Tab33Discount").val(numeral(k).format('0,0') + " đ");
                    $("#Tab3Discount").val(k + " đ");
                } else {
                    $("#DiscountTab1").val(k + " %");
                    $("#Tab33Discount").val(k + " %");
                    $("#Tab3Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
                }
            }

            //ArrHdDiscountAll[i].TypeApply;
            //tmp.type = ArrHdDiscountAll[i].type;
            //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
            //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
            //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
            //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
        } else {

            HideLoading();
        }
    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
        var total = 0;
        var totalKm = 0;
        var tmp = 0;
        var customerNV = new Array();
        var selector = '.detailList4 tr';
        $(selector).each(function (index, elem) {
            if ($(elem).find('.detail_item_total_4').text() != '') { // la số thì mới tính
                total += parseFloat(removeComma($(elem).find('.detail_item_total_4').text()));
                //xử lý số tiền nhận từ customer
            }

        });

        if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {

            var i;
            var first = 0;
            var Ismoneys = false;
            var k = 0;
            for (i = 0; i < ArrHdDiscountAll.length; i++) {
                if (ArrHdDiscountAll[i].type == "ALLCN") {
                    if (ArrHdDiscountAll[i].Minvalue <= total) {
                        if (ArrHdDiscountAll[i].IsMoney == true) {
                            if (first == 0) {
                                totalKm = ArrHdDiscountAll[i].CommissionValue;
                                k = ArrHdDiscountAll[i].CommissionValue;
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = ArrHdDiscountAll[i].CommissionValue;
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                            Ismoneys = true;

                        } else {
                            if (first == 0) {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                        }

                    }
                    continue;
                }
                else if (ArrHdDiscountAll[i].type == "NV") {
                    var customerIdNV33 = $("#CustomerId4").val();

                    if (customerIdNV33 != null && customerIdNV33.length > 0) {
                        customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                        var j;
                        for (j = 0; j < customerNV.length; j++) {
                            if (customerIdNV1 == customerNV[j]) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {

                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                k = ArrHdDiscountAll[i].CommissionValue;
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }



                }
                else {
                    if (ArrHdDiscountAll[i].type == "CUST") {
                        var customerIdNV33 = $("#CustomerId4").val();
                        if (customerIdNV33 != null && customerIdNV33.length > 0) {
                            var cust = ArrHdDiscountAll[i].listIDcustomer;
                            if (customerIdNV33 == cust) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }




                }



            }


            if (totalKm != 0) {
                if (k != 0) {
                    total -= totalKm;
                }
                $("#TotalAmount1").val(numeral(total).format('0,0'));
                $("#TotalPay").val(numeral(total).format('0,0'));
                $("#Tab4Total").val(numeral(total).format('0,0'));
                if (Ismoneys == true) {
                    $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                    $("#Tab44Discount").val(numeral(k).format('0,0') + " đ");
                    $("#Tab4Discount").val(k + " đ");
                } else {
                    $("#DiscountTab1").val(k + " %");
                    $("#Tab44Discount").val(k + " %");
                    $("#Tab4Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
                }
            } else {
                $("#DiscountTab1").val('');
                $("#Tab44Discount").val('');
                $("#Tab4Discount").val('');
            }

            //ArrHdDiscountAll[i].TypeApply;
            //tmp.type = ArrHdDiscountAll[i].type;
            //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
            //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
            //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
            //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
        } else {

            HideLoading();
        }
    } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
        var total = 0;
        var totalKm = 0;
        var tmp = 0;
        var customerNV = new Array();
        var selector = '.detailList5 tr';
        $(selector).each(function (index, elem) {
            if ($(elem).find('.detail_item_total_5').text() != '') { // la số thì mới tính
                total += parseFloat(removeComma($(elem).find('.detail_item_total_5').text()));
                //xử lý số tiền nhận từ customer
            }

        });

        if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {

            var i;
            var first = 0;
            var Ismoneys = false;
            var k = 0;
            for (i = 0; i < ArrHdDiscountAll.length; i++) {
                if (ArrHdDiscountAll[i].type == "ALLCN") {
                    if (ArrHdDiscountAll[i].Minvalue <= total) {
                        if (ArrHdDiscountAll[i].IsMoney == true) {
                            if (first == 0) {
                                totalKm = ArrHdDiscountAll[i].CommissionValue;
                                k = ArrHdDiscountAll[i].CommissionValue;
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = ArrHdDiscountAll[i].CommissionValue;
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                            Ismoneys = true;

                        } else {
                            if (first == 0) {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                        }

                    }
                    continue;
                }
                else if (ArrHdDiscountAll[i].type == "NV") {
                    var customerIdNV33 = $("#CustomerId5").val();

                    if (customerIdNV33 != null && customerIdNV33.length > 0) {
                        customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                        var j;
                        for (j = 0; j < customerNV.length; j++) {
                            if (customerIdNV1 == customerNV[j]) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {

                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                k = ArrHdDiscountAll[i].CommissionValue;
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }



                }
                else {
                    if (ArrHdDiscountAll[i].type == "CUST") {
                        var customerIdNV33 = $("#CustomerId5").val();
                        if (customerIdNV33 != null && customerIdNV33.length > 0) {
                            var cust = ArrHdDiscountAll[i].listIDcustomer;
                            if (customerIdNV33 == cust) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }




                }



            }


            if (totalKm != 0) {
                if (k != 0) {
                    total -= totalKm;
                }
                $("#TotalAmount1").val(numeral(total).format('0,0'));
                $("#TotalPay").val(numeral(total).format('0,0'));
                $("#Tab5Total").val(numeral(total).format('0,0'));
                if (Ismoneys == true) {
                    $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                    $("#Tab55Discount").val(numeral(k).format('0,0') + " đ");
                    $("#Tab5Discount").val(k + " đ");
                } else {
                    $("#DiscountTab1").val(k + " %");
                    $("#Tab55Discount").val(k + " %");
                    $("#Tab5Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
                }
            } else {
                $("#DiscountTab1").val('');
                $("#Tab55Discount").val('');
                $("#Tab5Discount").val('');
            }

            //ArrHdDiscountAll[i].TypeApply;
            //tmp.type = ArrHdDiscountAll[i].type;
            //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
            //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
            //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
            //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
        } else {

            HideLoading();
        }
    }
    else {
        var total = 0;
        var totalKm = 0;
        var tmp = 0;
        var customerNV = new Array();
        var selector = '.detailList6 tr';
        $(selector).each(function (index, elem) {
            if ($(elem).find('.detail_item_total_6').text() != '') { // la số thì mới tính
                total += parseFloat(removeComma($(elem).find('.detail_item_total_6').text()));
                //xử lý số tiền nhận từ customer
            }

        });

        if (ArrHdDiscountAll != null && ArrHdDiscountAll.length != 0) {

            var i;
            var first = 0;
            var Ismoneys = false;
            var k = 0;
            for (i = 0; i < ArrHdDiscountAll.length; i++) {
                if (ArrHdDiscountAll[i].type == "ALLCN") {
                    if (ArrHdDiscountAll[i].Minvalue <= total) {
                        if (ArrHdDiscountAll[i].IsMoney == true) {
                            if (first == 0) {
                                totalKm = ArrHdDiscountAll[i].CommissionValue;
                                k = ArrHdDiscountAll[i].CommissionValue;
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = ArrHdDiscountAll[i].CommissionValue;
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                            Ismoneys = true;

                        } else {
                            if (first == 0) {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                first = 1;
                            } else {
                                k = ArrHdDiscountAll[i].CommissionValue;
                                tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                if (tmp > totalKm) {
                                    totalKm = tmp;
                                }
                            }
                        }

                    }
                    continue;
                }
                else if (ArrHdDiscountAll[i].type == "NV") {
                    var customerIdNV33 = $("#CustomerId6").val();

                    if (customerIdNV33 != null && customerIdNV33.length > 0) {
                        customerNV = ArrHdDiscountAll[i].listIDcustomer.split(',');
                        var j;
                        for (j = 0; j < customerNV.length; j++) {
                            if (customerIdNV1 == customerNV[j]) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {

                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                k = ArrHdDiscountAll[i].CommissionValue;
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }



                }
                else {
                    if (ArrHdDiscountAll[i].type == "CUST") {
                        var customerIdNV33 = $("#CustomerId6").val();
                        if (customerIdNV33 != null && customerIdNV33.length > 0) {
                            var cust = ArrHdDiscountAll[i].listIDcustomer;
                            if (customerIdNV33 == cust) {
                                if (ArrHdDiscountAll[i].Minvalue <= total) {
                                    if (ArrHdDiscountAll[i].IsMoney == true) {
                                        if (first == 0) {
                                            totalKm = ArrHdDiscountAll[i].CommissionValue;
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total - ArrHdDiscountAll[i].CommissionValue;
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                        Ismoneys = true;

                                    } else {
                                        if (first == 0) {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            totalKm = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            first = 1;
                                        } else {
                                            k = ArrHdDiscountAll[i].CommissionValue;
                                            tmp = total * (ArrHdDiscountAll[i].CommissionValue / 100);
                                            if (tmp > totalKm) {
                                                totalKm = tmp;
                                            }
                                        }
                                    }

                                }
                            }
                            continue;

                        }
                    }




                }



            }


            if (totalKm != 0) {
                if (k != 0) {
                    total -= totalKm;
                }
                $("#TotalAmount1").val(numeral(total).format('0,0'));
                $("#TotalPay").val(numeral(total).format('0,0'));
                $("#Tab6Total").val(numeral(total).format('0,0'));
                if (Ismoneys == true) {
                    $("#DiscountTab1").val(numeral(k).format('0,0') + " đ");
                    $("#Tab66Discount").val(numeral(k).format('0,0') + " đ");
                    $("#Tab6Discount").val(k + " đ");
                } else {
                    $("#DiscountTab1").val(k + " %");
                    $("#Tab66Discount").val(k + " %");
                    $("#Tab6Discount").val(k + "-%-" + k_id + "-" + k_id_cha);
                }
            } else {
                $("#DiscountTab1").val('');
                $("#Tab66Discount").val('');
                $("#Tab6Discount").val('');
            }

            //ArrHdDiscountAll[i].TypeApply;
            //tmp.type = ArrHdDiscountAll[i].type;
            //tmp.listIDcustomer = ArrHdDiscountAll[i].listIDcustomer;
            //tmp.Minvalue = ArrHdDiscountAll[i].Minvalue;
            //tmp.CommissionValue = ArrHdDiscountAll[i].CommissionValue;
            //tmp.IsMoney = ArrHdDiscountAll[i].IsMoney;
        } else {

            HideLoading();
        }
    }


    HideLoading();
};
//mcuong.fit@gmail.com
//*************************************************End tính KM các tab***************************************************************
//*************************************************begin tính khuyễn mãi toàn hóa đơn các tab***************************************************************
function tinhDiscountAll1() {

}
function tinhDiscountAll1() {

}
function tinhDiscountAll1() {

}
function tinhDiscountAll1() {

}
function tinhDiscountAll1() {

}
function tinhDiscountAll1() {

}

//*************************************************end tính khuyễn mãi toàn hóa đơn các tab***************************************************************
function findPromotion($detail_item_id) {
    var categoryCode = $detail_item_id.closest('tr').find('.detail_item_category_type').val();
    var productId = $detail_item_id.val();
    var quantity = $detail_item_id.closest('tr').find('.detail_item_qty').val();
    quantity = parseInt(quantity);

    //1: ưu tiên cho sản phẩm
    var promotion_product = promotion.productList.filter(function (obj) {
        return obj.ProductId == productId && obj.QuantityFor >= quantity;
    });

    //console.log('promotion_product', promotion_product);
    if (promotion_product.length > 0) {
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text(promotion_product[0].PercentValue + '%');
        $detail_item_id.closest('tr').find('.detail_item_promotion_id').val(promotion_product[0].PromotionId);
        $detail_item_id.closest('tr').find('.detail_item_promotion_detail_id').val(promotion_product[0].Id);
        $detail_item_id.closest('tr').find('.detail_item_promotion_value').val(promotion_product[0].PercentValue);

        var promotionItem = promotion.promotionList.find(function (obj) {
            return obj.Id == promotion_product_category.PromotionId;
        });
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').attr('title', promotionItem != undefined ? promotionItem.Name : "");

        return;
    }

    //2: xét đến danh mục: tất cả sản phẩm (hàm find chỉ trả về phần tử đầu tiên tìm đc)
    var promotion_product_category = promotion.productCategoryList.find(function (obj) {
        return obj.ProductCategoryCode == categoryCode;
    });
    //console.log('promotion_product_category', promotion_product_category);
    if (promotion_product_category != undefined) {
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text(promotion_product_category.PercentValue + '%');
        $detail_item_id.closest('tr').find('.detail_item_promotion_id').val(promotion_product_category.PromotionId);
        $detail_item_id.closest('tr').find('.detail_item_promotion_detail_id').val(promotion_product_category.Id);
        $detail_item_id.closest('tr').find('.detail_item_promotion_value').val(promotion_product_category.PercentValue);

        var promotionItem = promotion.promotionList.find(function (obj) {
            return obj.Id == promotion_product_category.PromotionId;
        });
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').attr('title', promotionItem != undefined ? promotionItem.Name : "");

        return;
    }

    //3: xét đến cho tất cả sản phẩm
    var promotion_all = promotion.promotionList.find(function (obj) {
        return obj.IsAllProduct == true;
    });

    //console.log('promotion_all', promotion_all);
    if (promotion_all != undefined) {
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text(promotion_all.PercentValue + '%');
        $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').attr('title', promotion_all.Name);

        $detail_item_id.closest('tr').find('.detail_item_promotion_id').val(promotion_all.Id);
        $detail_item_id.closest('tr').find('.detail_item_promotion_value').val(promotion_all.PercentValue);
        return;
    }

    //nếu không có thì mặc định là 0
    $detail_item_id.closest('tr').find('.detail_item_promotion .display-value').text('0%');
    return;
}

function checkChosenProductOnTable() {
    var flag = true;
    $('#detailList select.detail_item_id').each(function (index, elem) {
        if ($(elem).val() == '') {
            var message = $(elem).data('val-required') != undefined ? $(elem).data('val-required') : 'Chưa chọn sản phẩm!';
            $(elem).next('span').text(message);
            flag = false;
        }
    });
    return flag;
}


/*********** page load ***************/
$(function () {
    $("#categorySelectList").hide();
    LoadNumberInput();

    $('#PaymentViewModel_Amount').val('0').numberFormat();
    $("#ProductItemCount").attr("readonly", "true");


    tinh_tong_tien();
    tinh_tong_tien1();
    tinh_tong_tien2();
    tinh_tong_tien3();
    tinh_tong_tien4();
    tinh_tong_tien5();
    tinh_tong_tien6();

    $("#productCode").focus();

    //Event
    var timeout = null;
    $("#productName").keyup(function (e) {
        if (e.which == 13 || e.which == 37 || e.which == 38 || e.which == 39 || e.which == 40)
            return;

        var searchText = $(this).val().trim();

        //if you already have a timout, clear it
        if (timeout) { clearTimeout(timeout); }

        //start new time, to perform ajax stuff in 500ms
        timeout = setTimeout(function () {
            $("#product_search_control .result").html("");
            // Toán tử số học (Arithmetic operators), Toán tử quan hệ (Relational operator), Toán tử logic (Logical operator), Toán tử điều kiện (Condition operator), Các toán tử tăng, giảm (Increment and decrement operator), Toán tử gán (Assignment operator)
            var relational_operator = '>,>=,<,<=,=';
            var isSearchNumber = false;

            //lấy từ đầu tiên (tính bằng khoảng trắng) trong nội dung tìm kiếm, xem có các toán tử so sánh hay không để tìm kiếm cho giá trị số
            var first_word = searchText.split(' ')[0].trim().replace(/\d/g, '');
            if (relational_operator.indexOf(first_word) != -1) {
                isSearchNumber = true;

                // nếu tìm kiếm cho số thì bỏ đi các toán tử so sánh, chỉ giữ lại số
                searchText = searchText.replace(/\D/g, '');
            }

            searchText = convertVNtoEN(searchText);

            if (searchText != '') {
                var result = [];
                var numberOfItem = 0;
                for (var i in list) {
                    var obj = list[i];
                    if (numberOfItem <= 10) {
                        //Xét theo từng danh mục chọn
                        var checkCategory = false;
                        switch ($("#categorySelectList option:selected").val()) {
                            case "0": //1. Tất cả
                                checkCategory = true;
                                break;
                            case "1": //2. Sản phẩm
                                if (obj.Type == 'product')
                                    checkCategory = true;
                                break;
                            case "2": //3. Dịch vụ
                                if (obj.Type == 'service')
                                    checkCategory = true;
                                break;
                            default: //4. Danh mục cụ thể
                                if (obj.CategoryCode == $("#categorySelectList option:selected").val())
                                    checkCategory = true;
                                break;
                        }

                        if (checkCategory && convertVNtoEN(obj.Name).indexOf(searchText) != -1) {
                            result.push(obj);
                            numberOfItem++;
                        }
                    }
                }

                if (result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        var typeCss = result[i].Type == "product" ? "blue" : "orange";
                        var item = "<a class='item " + typeCss + "' data-id='" + result[i].Id + "' data-lo='" + result[i].LoCode + "' data-expiry-date='" + result[i].ExpiryDate + "'onclick='selectItem(" + result[i].Id + "," + result[i].LoCode + "," + result[i].ExpiryDate + ")'>" + result[i].Code + "-" + result[i].Name + " (Lô:" + result[i].LoCode + "-HSD:" + result[i].ExpiryDate + ")" + "</a>";
                        $("#product_search_control .result").append(item);
                    }

                    $("#product_search_control .result .item:first").addClass("selected");
                    $("#product_search_control .result").show();
                }
            }

        }, 300);
    });

    $("#productName").keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
        }
    });




    $("#productName").click(function () {
        $("#product_search_control .result").html("");

        var result = [];
        var numberOfItem = 0;
        for (var i in list) {
            var obj = list[i];
            if (numberOfItem <= 10) {
                //Xét theo từng danh mục chọn
                var checkCategory = false;
                switch ($("#categorySelectList option:selected").val()) {
                    case "0": //1. Tất cả
                        checkCategory = true;
                        break;
                    case "1": //2. Sản phẩm
                        if (obj.Type == 'product')
                            checkCategory = true;
                        break;
                    case "2": //3. Dịch vụ
                        if (obj.Type == 'service')
                            checkCategory = true;
                        break;
                    default: //4. Danh mục cụ thể
                        if (obj.CategoryCode == $("#categorySelectList option:selected").val())
                            checkCategory = true;
                        break;
                }

                if (checkCategory) {
                    result.push(obj);
                }
            }
        }

        if (result.length > 0) {
            for (i = 0; i < result.length; i++) {
                var typeCss = result[i].Type == "product" ? "blue" : "orange";
                var item = "<a class='item " + typeCss + "' data-id='" + result[i].Id + "' data-lo='" + result[i].LoCode + "' data-expiry-date='" + result[i].ExpiryDate + "' onclick='selectItem(" + result[i].Id + "," + result[i].LoCode + "," + result[i].ExpiryDate + ")'>" + result[i].Code + "-" + result[i].Name + " (Lô:" + result[i].LoCode + "-HSD:" + result[i].ExpiryDate + ")" + "</a>";
                $("#product_search_control .result").append(item);
            }

            $("#product_search_control .result .item:first").addClass("selected");
            $("#product_search_control .result").show();
        }
    });


    $("#productName").keydown(function (e) {
        if (e.which == 37 || e.which == 39) {
            e.preventDefault();
        }

        if (e.which == 40) {
            e.preventDefault();
            var selectedItem = $("#product_search_control .result .item.selected");
            if (!selectedItem.is(':last-child')) {
                selectedItem.removeClass("selected").next().addClass("selected");
            }
        }
        else if (e.which == 38) {
            e.preventDefault();
            var selectedItem = $("#product_search_control .result .item.selected");
            if (!selectedItem.is(':first-child')) {
                selectedItem.removeClass("selected").prev().addClass("selected");
            }
        }
        else if (e.which == 13) {
            var selectedItem = $("#product_search_control .result .item.selected");
            var id = selectedItem.data("id");
            selectItem(id);
        }
    });

    //Sự kiện khi bấm ra ngoài thì ẩn khung tìm kiếm
    $(window).click(function () {
        $("#product_search_control .result").hide();
        $("#productName").val("");
        $("#product_search_control .result").html("");

        $("#popup_archive").removeClass("collapse in");
    });

    $('#productName').bind('click', function (event) {
        event.stopPropagation();
        $("#product_search_control .result").show();
    });

    $('#popup_archive').bind('click', function (event) {
        event.stopPropagation();
    });

    //khi nhập barcode
    //$('#product_barcode3').change(function () {
    //    alert('code js');
    //    var $this = $(this);
    //    if ($this.val() != '') {

    //        var barcode = $this.val();

    //        //đặt lại giá trị rỗng
    //        $this.val('').focus();

    //        var valueSearch = searchProductByBarCodeContain(barcode);
    //        if (valueSearch == undefined) {
    //            alert('Không tìm thấy sản phẩm với mã code trên!');
    //            return;
    //        }

    //        $('#productSelectList2').val(valueSearch).trigger("load");
    //    }
    //});

    //mcuong.fit@gmail.com
    //*************************************************begin XÓA SP các tab***************************************************************
    $('#listOrderDetail').on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove();

        var countItem = $('.detailList tr').length;
        $('#ProductItemCount').val(countItem);

        if (countItem == 0) {
            $('#ProductItemCount').val('');
            $('#TongSoLuong').text('');
            $('#TongThanhTien').text('');

        }

        tinh_tong_tien();

        $('.detailList tr').each(function (index, tr) {
            $(tr).attr('role', index).attr("id", "product_item_" + index).data("id", index);
            $(tr).find('td:first-child').text(index + 1);
            $(tr).find('.pricetest').attr('name', 'DetailList[' + index + '].PriceTest').attr('id', 'DetailList_' + index + '__PriceTest');
            $(tr).find('.product_type').attr('name', 'DetailList[' + index + '].ProductType').attr('id', 'DetailList_' + index + '__ProductType');

            $(tr).find('.detail_locode').attr('name', 'DetailList[' + index + '].LoCode').attr('id', 'DetailList_' + index + '__LoCode');
            $(tr).find('.detail_expirydate').attr('name', 'DetailList[' + index + '].ExpiryDate').attr('id', 'DetailList_' + index + '__ExpiryDate');

            $(tr).find('.detail_item_id input').attr('name', 'DetailList[' + index + '].ProductId').attr('id', 'DetailList_' + index + '__ProductId');
            $(tr).find('.detail_item_qty').attr('name', 'DetailList[' + index + '].Quantity').attr('id', 'DetailList_' + index + '__Quantity');
            $(tr).find('.detail_item_price').last().attr('name', 'DetailList[' + index + '].Price').attr('id', 'DetailList_' + index + '__Price');
            $(tr).find('.detail_item_price').first().attr('id', 'mask-DetailList_' + index + '__Price');

            $(tr).find('.detail_item_fixed_discount').attr('name', 'DetailList[' + index + '].FixedDiscount').attr('id', 'DetailList_' + index + '__FixedDiscount');
            $(tr).find('.detail_item_fixed_discount_amount').attr('name', 'DetailList[' + index + '].FixedDiscountAmount1').attr('id', 'DetailList_' + index + '__FixedDiscountAmount');
            $(tr).find('.detail_item_discount').attr('name', 'DetailList[' + index + '].IrregularDiscount').attr('id', 'DetailList_' + index + '__IrregularDiscount');
            $(tr).find('.detail_item_discount_amount').attr('name', 'DetailList[' + index + '].IrregularDiscountAmount1').attr('id', 'DetailList_' + index + '__IrregularDiscountAmount');

        });
    });
    //TAB 1
    $('#listOrderDetail1').on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove();

        var countItem = $('.detailList1 tr').length;
        $('#ProductItemCount').val(countItem);

        if (countItem == 0) {
            $('#TotalAmountTab1').val("");
            $('#DiscountTab1').val("");
            $('#TotalAmount1').val("");
            $("#TotalPay").val("");
            $('#ProductItemCount').val('');
            $('#PriceCustomer1').text('');
            $('#TotalAmount1').text('');
            $('#TotalCustomer1').text('');
        }

        ChangeTable(1);
        tinh_tong_tien1();

        $('.detailList1 tr').each(function (index, tr) {
            $(tr).attr('role', index).attr("id", "product_item_" + index).data("id", index);
            $(tr).find('td:first-child').text(index + 1);
            $(tr).find('.pricetest').attr('name', 'DetailList1[' + index + '].PriceTest').attr('id', 'DetailList_' + index + '__PriceTest');
            $(tr).find('.product_type').attr('name', 'DetailList1[' + index + '].ProductType').attr('id', 'DetailList_' + index + '__ProductType');

            $(tr).find('.detail_locode').attr('name', 'DetailList1[' + index + '].LoCode').attr('id', 'DetailList_' + index + '__LoCode');
            $(tr).find('.detail_expirydate').attr('name', 'DetailList1[' + index + '].ExpiryDate').attr('id', 'DetailList_' + index + '__ExpiryDate');

            $(tr).find('.detail_item_id input').attr('name', 'DetailList1[' + index + '].ProductId').attr('id', 'DetailList_' + index + '__ProductId');
            $(tr).find('.detail_item_qty').attr('name', 'DetailList1[' + index + '].Quantity').attr('id', 'DetailList_' + index + '__Quantity');
            $(tr).find('.detail_item_price').last().attr('name', 'DetailList1[' + index + '].Price').attr('id', 'DetailList_' + index + '__Price');
            $(tr).find('.detail_item_price').first().attr('id', 'mask-DetailList_' + index + '__Price');

            $(tr).find('.detail_item_fixed_discount').attr('name', 'DetailList1[' + index + '].FixedDiscount').attr('id', 'DetailList_' + index + '__FixedDiscount');
            $(tr).find('.detail_item_fixed_discount_amount').attr('name', 'DetailList1[' + index + '].FixedDiscountAmount1').attr('id', 'DetailList_' + index + '__FixedDiscountAmount');
            $(tr).find('.detail_item_discount').attr('name', 'DetailList1[' + index + '].IrregularDiscount').attr('id', 'DetailList_' + index + '__IrregularDiscount');
            $(tr).find('.detail_item_discount_amount').attr('name', 'DetailList1[' + index + '].IrregularDiscountAmount1').attr('id', 'DetailList_' + index + '__IrregularDiscountAmount');

        });
    });
    //TAB 2
    $('#listOrderDetail2').on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove();

        var countItem = $('.detailList2 tr').length;
        $('#ProductItemCount').val(countItem);

        if (countItem == 0) {
            $('#TotalAmountTab1').text('');
            $('#DiscountTab1').text('');
            $('#TotalAmount1').text('');
            $("#TotalPay").val("");
            $('#ProductItemCount').val('');
            $('#PriceCustomer2').text('');
            $('#TotalAmount2').text('');
            $('#TotalCustomer2').text('');
        }

        ChangeTable(2);
        tinh_tong_tien2();

        $('.detailList2 tr').each(function (index, tr) {
            $(tr).attr('role', index).attr("id", "product_item_" + index).data("id", index);
            $(tr).find('td:first-child').text(index + 1);
            $(tr).find('.pricetest').attr('name', 'DetailList2[' + index + '].PriceTest').attr('id', 'DetailList_' + index + '__PriceTest');
            $(tr).find('.product_type').attr('name', 'DetailList2[' + index + '].ProductType').attr('id', 'DetailList_' + index + '__ProductType');

            $(tr).find('.detail_locode').attr('name', 'DetailList2[' + index + '].LoCode').attr('id', 'DetailList_' + index + '__LoCode');
            $(tr).find('.detail_expirydate').attr('name', 'DetailList2[' + index + '].ExpiryDate').attr('id', 'DetailList_' + index + '__ExpiryDate');

            $(tr).find('.detail_item_id input').attr('name', 'DetailList2[' + index + '].ProductId').attr('id', 'DetailList_' + index + '__ProductId');
            $(tr).find('.detail_item_qty').attr('name', 'DetailList2[' + index + '].Quantity').attr('id', 'DetailList_' + index + '__Quantity');
            $(tr).find('.detail_item_price').last().attr('name', 'DetailList2[' + index + '].Price').attr('id', 'DetailList_' + index + '__Price');
            $(tr).find('.detail_item_price').first().attr('id', 'mask-DetailList_' + index + '__Price');

            $(tr).find('.detail_item_fixed_discount').attr('name', 'DetailList2[' + index + '].FixedDiscount').attr('id', 'DetailList_' + index + '__FixedDiscount');
            $(tr).find('.detail_item_fixed_discount_amount').attr('name', 'DetailList2[' + index + '].FixedDiscountAmount1').attr('id', 'DetailList_' + index + '__FixedDiscountAmount');
            $(tr).find('.detail_item_discount').attr('name', 'DetailList2[' + index + '].IrregularDiscount').attr('id', 'DetailList_' + index + '__IrregularDiscount');
            $(tr).find('.detail_item_discount_amount').attr('name', 'DetailList2[' + index + '].IrregularDiscountAmount1').attr('id', 'DetailList_' + index + '__IrregularDiscountAmount');

        });
    });
    //TAB3
    $('#listOrderDetail3').on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove();

        var countItem = $('.detailList3 tr').length;
        $('#ProductItemCount').val(countItem);

        if (countItem == 0) {
            $('#TotalAmountTab1').text('');
            $('#DiscountTab1').text('');
            $('#TotalAmount1').text('');
            $("#TotalPay").val("");
            $('#ProductItemCount').val('');
            $('#PriceCustomer3').text('');
            $('#TotalAmount3').text('');
            $('#TotalCustomer3').text('');
        }

        ChangeTable(3);
        tinh_tong_tien3();

        $('.detailList3 tr').each(function (index, tr) {
            $(tr).attr('role', index).attr("id", "product_item_" + index).data("id", index);
            $(tr).find('td:first-child').text(index + 1);
            $(tr).find('.pricetest').attr('name', 'DetailList3[' + index + '].PriceTest').attr('id', 'DetailList_' + index + '__PriceTest');
            $(tr).find('.product_type').attr('name', 'DetailList3[' + index + '].ProductType').attr('id', 'DetailList_' + index + '__ProductType');

            $(tr).find('.detail_locode').attr('name', 'DetailList3[' + index + '].LoCode').attr('id', 'DetailList_' + index + '__LoCode');
            $(tr).find('.detail_expirydate').attr('name', 'DetailList3[' + index + '].ExpiryDate').attr('id', 'DetailList_' + index + '__ExpiryDate');

            $(tr).find('.detail_item_id input').attr('name', 'DetailList3[' + index + '].ProductId').attr('id', 'DetailList_' + index + '__ProductId');
            $(tr).find('.detail_item_qty').attr('name', 'DetailList3[' + index + '].Quantity').attr('id', 'DetailList_' + index + '__Quantity');
            $(tr).find('.detail_item_price').last().attr('name', 'DetailList3[' + index + '].Price').attr('id', 'DetailList_' + index + '__Price');
            $(tr).find('.detail_item_price').first().attr('id', 'mask-DetailList_' + index + '__Price');

            $(tr).find('.detail_item_fixed_discount').attr('name', 'DetailList3[' + index + '].FixedDiscount').attr('id', 'DetailList_' + index + '__FixedDiscount');
            $(tr).find('.detail_item_fixed_discount_amount').attr('name', 'DetailList3[' + index + '].FixedDiscountAmount1').attr('id', 'DetailList_' + index + '__FixedDiscountAmount');
            $(tr).find('.detail_item_discount').attr('name', 'DetailList3[' + index + '].IrregularDiscount').attr('id', 'DetailList_' + index + '__IrregularDiscount');
            $(tr).find('.detail_item_discount_amount').attr('name', 'DetailList3[' + index + '].IrregularDiscountAmount1').attr('id', 'DetailList_' + index + '__IrregularDiscountAmount');

        });
    });
    $('#listOrderDetail4').on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove();

        var countItem = $('.detailList4 tr').length;
        $('#ProductItemCount').val(countItem);

        if (countItem == 0) {
            $('#TotalAmountTab1').text('');
            $('#DiscountTab1').text('');
            $('#TotalAmount1').text('');
            $("#TotalPay").val("");
            $('#ProductItemCount').val('');
            $('#PriceCustomer4').text('');
            $('#TotalAmount4').text('');
            $('#TotalCustomer4').text('');
        }

        ChangeTable(4);
        tinh_tong_tien4();

        $('.detailList4 tr').each(function (index, tr) {
            $(tr).attr('role', index).attr("id", "product_item_" + index).data("id", index);
            $(tr).find('td:first-child').text(index + 1);
            $(tr).find('.pricetest').attr('name', 'DetailList4[' + index + '].PriceTest').attr('id', 'DetailList_' + index + '__PriceTest');
            $(tr).find('.product_type').attr('name', 'DetailList4[' + index + '].ProductType').attr('id', 'DetailList_' + index + '__ProductType');

            $(tr).find('.detail_locode').attr('name', 'DetailList4[' + index + '].LoCode').attr('id', 'DetailList_' + index + '__LoCode');
            $(tr).find('.detail_expirydate').attr('name', 'DetailList4[' + index + '].ExpiryDate').attr('id', 'DetailList_' + index + '__ExpiryDate');

            $(tr).find('.detail_item_id input').attr('name', 'DetailList4[' + index + '].ProductId').attr('id', 'DetailList_' + index + '__ProductId');
            $(tr).find('.detail_item_qty').attr('name', 'DetailList4[' + index + '].Quantity').attr('id', 'DetailList_' + index + '__Quantity');
            $(tr).find('.detail_item_price').last().attr('name', 'DetailList4[' + index + '].Price').attr('id', 'DetailList_' + index + '__Price');
            $(tr).find('.detail_item_price').first().attr('id', 'mask-DetailList_' + index + '__Price');

            $(tr).find('.detail_item_fixed_discount').attr('name', 'DetailList4[' + index + '].FixedDiscount').attr('id', 'DetailList_' + index + '__FixedDiscount');
            $(tr).find('.detail_item_fixed_discount_amount').attr('name', 'DetailList4[' + index + '].FixedDiscountAmount1').attr('id', 'DetailList_' + index + '__FixedDiscountAmount');
            $(tr).find('.detail_item_discount').attr('name', 'DetailList4[' + index + '].IrregularDiscount').attr('id', 'DetailList_' + index + '__IrregularDiscount');
            $(tr).find('.detail_item_discount_amount').attr('name', 'DetailList4[' + index + '].IrregularDiscountAmount1').attr('id', 'DetailList_' + index + '__IrregularDiscountAmount');

        });
    });
    $('#listOrderDetail5').on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove();

        var countItem = $('.detailList5 tr').length;
        $('#ProductItemCount').val(countItem);

        if (countItem == 0) {
            $('#TotalAmountTab1').text('');
            $('#DiscountTab1').text('');
            $('#TotalAmount1').text('');
            $("#TotalPay").val("");
            $('#ProductItemCount').val('');
            $('#PriceCustomer5').text('');
            $('#TotalAmount5').text('');
            $('#TotalCustomer5').text('');
        }

        ChangeTable(5);
        tinh_tong_tien5();

        $('.detailList5 tr').each(function (index, tr) {
            $(tr).attr('role', index).attr("id", "product_item_" + index).data("id", index);
            $(tr).find('td:first-child').text(index + 1);
            $(tr).find('.pricetest').attr('name', 'DetailList5[' + index + '].PriceTest').attr('id', 'DetailList_' + index + '__PriceTest');
            $(tr).find('.product_type').attr('name', 'DetailList5[' + index + '].ProductType').attr('id', 'DetailList_' + index + '__ProductType');

            $(tr).find('.detail_locode').attr('name', 'DetailList5[' + index + '].LoCode').attr('id', 'DetailList_' + index + '__LoCode');
            $(tr).find('.detail_expirydate').attr('name', 'DetailList5[' + index + '].ExpiryDate').attr('id', 'DetailList_' + index + '__ExpiryDate');

            $(tr).find('.detail_item_id input').attr('name', 'DetailList5[' + index + '].ProductId').attr('id', 'DetailList_' + index + '__ProductId');
            $(tr).find('.detail_item_qty').attr('name', 'DetailList5[' + index + '].Quantity').attr('id', 'DetailList_' + index + '__Quantity');
            $(tr).find('.detail_item_price').last().attr('name', 'DetailList5[' + index + '].Price').attr('id', 'DetailList_' + index + '__Price');
            $(tr).find('.detail_item_price').first().attr('id', 'mask-DetailList_' + index + '__Price');

            $(tr).find('.detail_item_fixed_discount').attr('name', 'DetailList5[' + index + '].FixedDiscount').attr('id', 'DetailList_' + index + '__FixedDiscount');
            $(tr).find('.detail_item_fixed_discount_amount').attr('name', 'DetailList5[' + index + '].FixedDiscountAmount1').attr('id', 'DetailList_' + index + '__FixedDiscountAmount');
            $(tr).find('.detail_item_discount').attr('name', 'DetailList5[' + index + '].IrregularDiscount').attr('id', 'DetailList_' + index + '__IrregularDiscount');
            $(tr).find('.detail_item_discount_amount').attr('name', 'DetailList5[' + index + '].IrregularDiscountAmount1').attr('id', 'DetailList_' + index + '__IrregularDiscountAmount');

        });
    });
    $('#listOrderDetail6').on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove();

        var countItem = $('.detailList6 tr').length;
        $('#ProductItemCount').val(countItem);
        if (countItem == 0) {
            $('#TotalAmountTab1').text('');
            $('#DiscountTab1').text('');
            $('#TotalAmount1').text('');
            $("#TotalPay").val("");
            $('#ProductItemCount').val('');
            $('#PriceCustomer6').text('');
            $('#TotalAmount6').text('');
            $('#TotalCustomer2').text('');
        }

        ChangeTable(6);
        tinh_tong_tien6();

        $('.detailLis6 tr').each(function (index, tr) {
            $(tr).attr('role', index).attr("id", "product_item_" + index).data("id", index);
            $(tr).find('td:first-child').text(index + 1);
            $(tr).find('.pricetest').attr('name', 'DetailList6[' + index + '].PriceTest').attr('id', 'DetailList_' + index + '__PriceTest');
            $(tr).find('.product_type').attr('name', 'DetailList6[' + index + '].ProductType').attr('id', 'DetailList_' + index + '__ProductType');

            $(tr).find('.detail_locode').attr('name', 'DetailList6[' + index + '].LoCode').attr('id', 'DetailList_' + index + '__LoCode');
            $(tr).find('.detail_expirydate').attr('name', 'DetailList6[' + index + '].ExpiryDate').attr('id', 'DetailList_' + index + '__ExpiryDate');

            $(tr).find('.detail_item_id input').attr('name', 'DetailList6[' + index + '].ProductId').attr('id', 'DetailList_' + index + '__ProductId');
            $(tr).find('.detail_item_qty').attr('name', 'DetailList6[' + index + '].Quantity').attr('id', 'DetailList_' + index + '__Quantity');
            $(tr).find('.detail_item_price').last().attr('name', 'DetailList6[' + index + '].Price').attr('id', 'DetailList_' + index + '__Price');
            $(tr).find('.detail_item_price').first().attr('id', 'mask-DetailList_' + index + '__Price');

            $(tr).find('.detail_item_fixed_discount').attr('name', 'DetailList6[' + index + '].FixedDiscount').attr('id', 'DetailList_' + index + '__FixedDiscount');
            $(tr).find('.detail_item_fixed_discount_amount').attr('name', 'DetailList6[' + index + '].FixedDiscountAmount1').attr('id', 'DetailList_' + index + '__FixedDiscountAmount');
            $(tr).find('.detail_item_discount').attr('name', 'DetailList6[' + index + '].IrregularDiscount').attr('id', 'DetailList_' + index + '__IrregularDiscount');
            $(tr).find('.detail_item_discount_amount').attr('name', 'DetailList6[' + index + '].IrregularDiscountAmount1').attr('id', 'DetailList_' + index + '__IrregularDiscountAmount');

        });
    });
    //mcuong.fit@gmail.com
    //*************************************************END XÓA SP các tab***************************************************************
    //Khi thay đổi số lượng
    $('#listOrderDetail').on('change', '.detail_item_qty', function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal <= 0) {
            $(this).val(1);
        }
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        var _product_id = $("#DetailList_" + id + "__ProductId").val();
        var _LoCode = $("#DetailList_" + id + "__LoCode").val();
        var _ExpiryDate = $("#DetailList_" + id + "__ExpiryDate").val();
        var _quantity_inventory = $(this).data("quantity-inventory");
        var selector = '.detailList tr';
        var quantity_used = 0;
        $(selector).each(function (index, elem) {
            if (index != id) {
                var product_id = $("#DetailList_" + index + "__ProductId").val();
                var LoCode = $("#DetailList_" + index + "__LoCode").val();
                var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                var Quantity = $("#DetailList_" + index + "__Quantity").val();
                if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                    quantity_used += parseInt(removeComma(Quantity));
                }
            }
        });
        var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
        var _quantity = parseInt(removeComma($(this).val()));
        //$("#status").text("");
        //if (_quantity > inventory_qty) {
        //    $("#DetailList_" + id + "__Quantity").val(inventory_qty);
        //    $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
        //}

        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien();
    });

    //Khi thay đổi giá
    $('#listOrderDetail').on('change', '.detail_item_price', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien();
    });

    //Khi thay đổi chiết khấu
    $('#listOrderDetail').on('change', '.detail_item_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        var input_price = $('#DetailList_' + id + '__Price');
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty');
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chiết khấu
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_discount');
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));
        }
        var discountAmount = discount * total / 100;
        $('tr#product_item_' + id).find('.detail_item_discount_amount').val(numeral(discountAmount).format('0,0'));
        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien();
    });
    //Khi thay đổi chiết khấu
    $('#listOrderDetail').on('change', '.detail_item_fixed_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        var input_price = $('#DetailList_' + id + '__Price');
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty');
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chiết khấu
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_fixed_discount');
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));
        }
        var discountAmount = discount * total / 100;
        $('tr#product_item_' + id).find('.detail_item_fixed_discount_amount').val(numeral(discountAmount).format('0,0'));

        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien();
    });
    //

    //mcuong.fit@gmail.com
    //*************************************************begin TAB1 *****************************************************************************

    //listOrderDetail1

    //Khi thay đổi số lượng
    $('#listOrderDetail1').on('change', function () {

        //tính tổng cộng

        tinh_tong_tien1();
    });
    $('#listOrderDetail1').on('change', '.detail_item_qty_1', function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal <= 0) {
            $(this).val(1);
        }
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        var _product_id = $("#DetailList_" + id + "__ProductId").val();
        var _LoCode = $("#DetailList_" + id + "__LoCode").val();
        var _ExpiryDate = $("#DetailList_" + id + "__ExpiryDate").val();
        var _quantity_inventory = $(this).data("quantity-inventory");
        var selector = '.detailList1 tr';
        var quantity_used = 0;
        $(selector).each(function (index, elem) {
            if (index != id) {
                var product_id = $("#DetailList_" + index + "__ProductId").val();
                var LoCode = $("#DetailList_" + index + "__LoCode").val();
                var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                var Quantity = $("#DetailList_" + index + "__Quantity").val();
                if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                    quantity_used += parseInt(removeComma(Quantity));
                }
            }
        });
        var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
        var _quantity = parseInt(removeComma($(this).val()));
        $("#status").text("");
        //if (_quantity > inventory_qty) {
        //    $("#DetailList_" + id + "__Quantity").val(inventory_qty);
        //    $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
        //}
        if (isplus === -10 && isminus === -10) {
            isplus = -3;
            isminus = -3;
        }
        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien1();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi giá
    $('#listOrderDetail1').on('change', '.detail_item_price_1', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien1();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi chiết khấu
    $('#listOrderDetail1').on('change', '.detail_item_discount_1', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        ChangeDiscount(1, id);

    });
    $('#listOrderDetail1').on('change', '.detail_item_discount_1_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        ChangeDiscountAmount(1, id);
    });

    //$('#listOrderDetail1').on('keypress', '.detail_item_price_1', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $(this).parent().next().find("input:first").focus().select();
    //    }
    //});

    //$('#listOrderDetail1').on('keypress', '.detail_item_qty_1', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $(this).parent().next().find("input:first").focus().select();
    //    }
    //});

    //$('#listOrderDetail1').on('keypress', '.detail_item_discount_1', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $("#productCode").focus();
    //    }
    //});
    $('#listOrderDetail1').on('focus', '.detail_item_discount_1', function () {
        $(this).select();
    });
    $('#listOrderDetail1').on('focus', '.detail_item_fixed_1_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail1').on('focus', '.detail_item_price_1', function () {
        $(this).select();
    });

    $('#listOrderDetail1').on('focus', '.detail_item_qty_1', function () {
        $(this).select();
    });
    //$('#listOrderDetail1').on('change', '.detail_item_discount_1_amount', function () {
    //    var $this = $(this);

    //    var id = $this.closest('tr').data('id');
    //    tinh_thanh_tien_moi_dong2(id);
    //    tinh_tong_tien1();
    //    tinhkhuyenmaiHd();
    //});
    $('#listOrderDetail1').on('change', '.detail_item_fixed_discount_1_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien1();
        tinhkhuyenmaiHd();
    });

    //*************************************************end tab1***************************************************************
    //mcuong.fit@gmail.com
    //*************************************************begin TAB2***************************************************************
    //listOrderDetail2

    //Khi thay đổi số lượng
    $('#listOrderDetail2').on('change', '.detail_item_qty_2', function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal <= 0) {
            $(this).val(1);
        }
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        var _product_id = $("#DetailList_" + id + "__ProductId").val();
        var _LoCode = $("#DetailList_" + id + "__LoCode").val();
        var _ExpiryDate = $("#DetailList_" + id + "__ExpiryDate").val();
        var _quantity_inventory = $(this).data("quantity-inventory");
        var selector = '.detailList2 tr';
        var quantity_used = 0;
        $(selector).each(function (index, elem) {
            if (index != id) {
                var product_id = $("#DetailList_" + index + "__ProductId").val();
                var LoCode = $("#DetailList_" + index + "__LoCode").val();
                var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                var Quantity = $("#DetailList_" + index + "__Quantity").val();
                if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                    quantity_used += parseInt(removeComma(Quantity));
                }
            }
        });
        var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
        var _quantity = parseInt(removeComma($(this).val()));
        //$("#status").text("");
        //if (_quantity > inventory_qty) {
        //    $("#DetailList_" + id + "__Quantity").val(inventory_qty);
        //    $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
        //}

        if (isplus === -10 && isminus === -10) {
            isplus = -3;
            isminus = -3;
        }
        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien2();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi giá
    $('#listOrderDetail2').on('change', '.detail_item_price_2', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien2();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi chiết khấu
    $('#listOrderDetail2').on('change', '.detail_item_discount_2', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscount(2, id);
    });
    $('#listOrderDetail2').on('change', '.detail_item_discount_2_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscountAmount(2, id);
    });
    //Khi thay đổi chiết khấu
    $('#listOrderDetail2').on('change', '.detail_item_fixed_2_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        var input_price = $('#DetailList_' + id + '__Price');
        //var input_price = $('tr#product_item_' + id).find('.detail_item_price_2');

        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_2');
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chiết khấu
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_fixed_2_discount');
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));
        }
        //var discountAmount = discount * total / 100;
        //$('tr#product_item_' + id).find('.detail_item_fixed_discount_2_amount').val(numeral(discountAmount).format('0,0'));

        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien2();
        tinhkhuyenmaiHd();
    });
    $('#listOrderDetail2').on('keypress', '.detail_item_price_2', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $(this).parent().next().find("input:first").focus().select();
        }
    });

    $('#listOrderDetail2').on('keypress', '.detail_item_qty_2', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $(this).parent().next().find("input:first").focus().select();
        }
    });

    $('#listOrderDetail2').on('keypress', '.detail_item_discount_2', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $("#productCode").focus();
        }
    });
    $('#listOrderDetail2').on('focus', '.detail_item_discount_2', function () {
        $(this).select();
    });
    $('#listOrderDetail2').on('focus', '.detail_item_fixed_2_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail2').on('focus', '.detail_item_price_2', function () {
        $(this).select();
    });

    $('#listOrderDetail2').on('focus', '.detail_item_qty_2', function () {
        $(this).select();
    });
    //$('#listOrderDetail2').on('change', '.detail_item_discount_2_amount', function () {
    //    var $this = $(this);
    //    var id = $this.closest('tr').data('id');
    //    tinh_thanh_tien_moi_dong2(id);
    //    tinh_tong_tien2();
    //    tinhkhuyenmaiHd();
    //});
    $('#listOrderDetail2').on('change', '.detail_item_fixed_discount_2_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien2();
        tinhkhuyenmaiHd();
    });
    //*************************************************end tab2***************************************************************
    //mcuong.fit@gmail.com
    //************************************************* begin tab3***************************************************************
    //listOrderDetail3
    //Khi thay đổi số lượng
    $('#listOrderDetail3').on('change', '.detail_item_qty_3', function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal <= 0) {
            $(this).val(1);
        }
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        var _product_id = $("#DetailList_" + id + "__ProductId").val();
        var _LoCode = $("#DetailList_" + id + "__LoCode").val();
        var _ExpiryDate = $("#DetailList_" + id + "__ExpiryDate").val();
        var _quantity_inventory = $(this).data("quantity-inventory");
        var selector = '.detailList3 tr';
        var quantity_used = 0;
        $(selector).each(function (index, elem) {
            if (index != id) {
                var product_id = $("#DetailList_" + index + "__ProductId").val();
                var LoCode = $("#DetailList_" + index + "__LoCode").val();
                var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                var Quantity = $("#DetailList_" + index + "__Quantity").val();
                if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                    quantity_used += parseInt(removeComma(Quantity));
                }
            }
        });
        var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
        var _quantity = parseInt(removeComma($(this).val()));
        //$("#status").text("");
        //if (_quantity > inventory_qty) {
        //    $("#DetailList_" + id + "__Quantity").val(inventory_qty);
        //    $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
        //}

        if (isplus === -10 && isminus === -10) {
            isplus = -3;
            isminus = -3;
        }
        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien3();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi giá
    $('#listOrderDetail3').on('change', '.detail_item_price_3', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien3();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi chiết khấu
    $('#listOrderDetail3').on('change', '.detail_item_discount_3', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscount(3, id);
    });
    $('#listOrderDetail3').on('change', '.detail_item_discount_3_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscountAmount(3, id);
    });
    //Khi thay đổi chiết khấu
    $('#listOrderDetail3').on('change', '.detail_item_fixed_3_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        var input_price = $('#DetailList_' + id + '__Price');
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_3');
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chiết khấu
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_fixed_3_discount');
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));
        }
        var discountAmount = discount * total / 100;
        $('tr#product_item_' + id).find('.detail_item_fixed_discount_3_amount').val(numeral(discountAmount).format('0,0'));

        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien3();
        tinhkhuyenmaiHd();
    });
    $('#listOrderDetail3').on('keypress', '.detail_item_price_3', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $(this).parent().next().find("input:first").focus().select();
        }
    });

    $('#listOrderDetail3').on('keypress', '.detail_item_qty_3', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $(this).parent().next().find("input:first").focus().select();
        }
    });

    $('#listOrderDetail3').on('keypress', '.detail_item_discount_3', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $("#productCode").focus();
        }
    });
    $('#listOrderDetail3').on('focus', '.detail_item_discount_3', function () {
        $(this).select();
    });
    $('#listOrderDetail3').on('focus', '.detail_item_fixed_3_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail3').on('focus', '.detail_item_price_3', function () {
        $(this).select();
    });

    $('#listOrderDetail3').on('focus', '.detail_item_qty_3', function () {
        $(this).select();
    });
    //$('#listOrderDetail3').on('change', '.detail_item_discount_3_amount', function () {
    //    var $this = $(this);
    //    var id = $this.closest('tr').data('id');
    //    tinh_thanh_tien_moi_dong2(id);
    //    tinh_tong_tien3();
    //    tinhkhuyenmaiHd();
    //});
    $('#listOrderDetail3').on('change', '.detail_item_fixed_discount3_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien3();
        tinhkhuyenmaiHd();
    });
    //*************************************************end tab3***************************************************************
    //mcuong.fit@gmail.com
    //************************************************* begin tab4***************************************************************
    //listOrderDetail4
    //Khi thay đổi số lượng
    $('#listOrderDetail4').on('change', '.detail_item_qty_4', function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal <= 0) {
            $(this).val(1);
        }
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        var _product_id = $("#DetailList_" + id + "__ProductId").val();
        var _LoCode = $("#DetailList_" + id + "__LoCode").val();
        var _ExpiryDate = $("#DetailList_" + id + "__ExpiryDate").val();
        var _quantity_inventory = $(this).data("quantity-inventory");
        var selector = '.detailList4 tr';
        var quantity_used = 0;
        $(selector).each(function (index, elem) {
            if (index != id) {
                var product_id = $("#DetailList_" + index + "__ProductId").val();
                var LoCode = $("#DetailList_" + index + "__LoCode").val();
                var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                var Quantity = $("#DetailList_" + index + "__Quantity").val();
                if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                    quantity_used += parseInt(removeComma(Quantity));
                }
            }
        });
        var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
        var _quantity = parseInt(removeComma($(this).val()));
        //$("#status").text("");
        //if (_quantity > inventory_qty) {
        //    $("#DetailList_" + id + "__Quantity").val(inventory_qty);
        //    $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
        //}

        if (isplus === -10 && isminus === -10) {
            isplus = -3;
            isminus = -3;
        }
        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien4();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi giá
    $('#listOrderDetail4').on('change', '.detail_item_price_4', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien4();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi chiết khấu
    $('#listOrderDetail4').on('change', '.detail_item_discount_4', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscount(4, id);
    });
    $('#listOrderDetail4').on('change', '.detail_item_discount_4_amount', function () {

        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscountAmount(4, id);
    });
    //Khi thay đổi chiết khấu
    $('#listOrderDetail4').on('change', '.detail_item_fixed_4_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        var input_price = $('#DetailList_' + id + '__Price');
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_4');
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chiết khấu
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_fixed_4_discount');
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));
        }
        var discountAmount = discount * total / 100;
        $('tr#product_item_' + id).find('.detail_item_fixed_discount_4_amount').val(numeral(discountAmount).format('0,0'));

        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien4();
        tinhkhuyenmaiHd();
    });
    //$('#listOrderDetail4').on('keypress', '.detail_item_price_4', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $(this).parent().next().find("input:first").focus().select();
    //    }
    //});

    //$('#listOrderDetail4').on('keypress', '.detail_item_qty_4', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $(this).parent().next().find("input:first").focus().select();
    //    }
    //});

    //$('#listOrderDetail4').on('keypress', '.detail_item_discount_4', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $("#productCode").focus();
    //    }
    //});
    $('#listOrderDetail4').on('focus', '.detail_item_discount_4', function () {
        $(this).select();
    });
    $('#listOrderDetail4').on('focus', '.detail_item_fixed_3_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail4').on('focus', '.detail_item_price_4', function () {
        $(this).select();
    });

    $('#listOrderDetail4').on('focus', '.detail_item_qty_4', function () {
        $(this).select();
    });

    $('#listOrderDetail4').on('change', '.detail_item_fixed_discount4_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien4();
        tinhkhuyenmaiHd();
    });
    //*************************************************end tab4***************************************************************
    //mcuong.fit@gmail.com
    //************************************************* begin tab5***************************************************************
    //listOrderDetail5
    //Khi thay đổi số lượng
    $('#listOrderDetail5').on('change', '.detail_item_qty_5', function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal <= 0) {
            $(this).val(1);
        }
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        var _product_id = $("#DetailList_" + id + "__ProductId").val();
        var _LoCode = $("#DetailList_" + id + "__LoCode").val();
        var _ExpiryDate = $("#DetailList_" + id + "__ExpiryDate").val();
        var _quantity_inventory = $(this).data("quantity-inventory");
        var selector = '.detailList5 tr';
        var quantity_used = 0;
        $(selector).each(function (index, elem) {
            if (index != id) {
                var product_id = $("#DetailList_" + index + "__ProductId").val();
                var LoCode = $("#DetailList_" + index + "__LoCode").val();
                var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                var Quantity = $("#DetailList_" + index + "__Quantity").val();
                if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                    quantity_used += parseInt(removeComma(Quantity));
                }
            }
        });
        var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
        var _quantity = parseInt(removeComma($(this).val()));
        //$("#status").text("");
        //if (_quantity > inventory_qty) {
        //    $("#DetailList_" + id + "__Quantity").val(inventory_qty);
        //    $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
        //}

        if (isplus === -10 && isminus === -10) {
            isplus = -3;
            isminus = -3;
        }
        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien5();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi giá
    $('#listOrderDetail5').on('change', '.detail_item_price_5', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien5();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi chiết khấu
    $('#listOrderDetail5').on('change', '.detail_item_discount_5', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscount(5, id);
    });
    $('#listOrderDetail5').on('change', '.detail_item_discount_5_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscountAmount(5, id);
    });
    //Khi thay đổi chiết khấu
    $('#listOrderDetail5').on('change', '.detail_item_fixed_5_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        var input_price = $('#DetailList_' + id + '__Price');
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_5');
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chiết khấu
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_fixed_5_discount');
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));
        }
        var discountAmount = discount * total / 100;
        $('tr#product_item_' + id).find('.detail_item_fixed_discount_5_amount').val(numeral(discountAmount).format('0,0'));

        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien5();
        tinhkhuyenmaiHd();
    });
    //$('#listOrderDetail5').on('keypress', '.detail_item_price_5', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $(this).parent().next().find("input:first").focus().select();
    //    }
    //});

    //$('#listOrderDetail5').on('keypress', '.detail_item_qty_5', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $(this).parent().next().find("input:first").focus().select();
    //    }
    //});

    //$('#listOrderDetail5').on('keypress', '.detail_item_discount_5', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $("#productCode").focus();
    //    }
    //});
    $('#listOrderDetail5').on('focus', '.detail_item_discount_5', function () {
        $(this).select();
    });
    $('#listOrderDetail5').on('focus', '.detail_item_fixed_5_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail5').on('focus', '.detail_item_price_5', function () {
        $(this).select();
    });

    $('#listOrderDetail5').on('focus', '.detail_item_qty_5', function () {
        $(this).select();
    });
    //$('#listOrderDetail5').on('change', '.detail_item_discount_5_amount', function () {
    //    var $this = $(this);
    //    var id = $this.closest('tr').data('id');
    //    tinh_thanh_tien_moi_dong2(id);
    //    tinh_tong_tien5();
    //    tinhkhuyenmaiHd();
    //});
    $('#listOrderDetail5').on('change', '.detail_item_fixed_discount6_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien5();
        tinhkhuyenmaiHd();
    });
    //*************************************************end tab5***************************************************************
    //mcuong.fit@gmail.com
    //************************************************* begin tab6***************************************************************
    //listOrderDetail6
    //Khi thay đổi số lượng
    $('#listOrderDetail6').on('change', '.detail_item_qty_6', function () {
        $(this).val($(this).val().replace(/\-/g, ''));
        $(this).val($(this).val().replace(/[^0-9.,]/g, ''));
        var ralVal = numeral($(this).val());
        if (ralVal <= 0) {
            $(this).val(1);
        }
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        var _product_id = $("#DetailList_" + id + "__ProductId").val();
        var _LoCode = $("#DetailList_" + id + "__LoCode").val();
        var _ExpiryDate = $("#DetailList_" + id + "__ExpiryDate").val();
        var _quantity_inventory = $(this).data("quantity-inventory");
        var selector = '.detailList6 tr';
        var quantity_used = 0;
        $(selector).each(function (index, elem) {
            if (index != id) {
                var product_id = $("#DetailList_" + index + "__ProductId").val();
                var LoCode = $("#DetailList_" + index + "__LoCode").val();
                var ExpiryDate = $("#DetailList_" + index + "__ExpiryDate").val();
                var Quantity = $("#DetailList_" + index + "__Quantity").val();
                if (product_id == _product_id && LoCode == _LoCode && ExpiryDate == _ExpiryDate) { // la số thì mới tính
                    quantity_used += parseInt(removeComma(Quantity));
                }
            }
        });
        var inventory_qty = parseInt(_quantity_inventory) - parseInt(quantity_used);
        var _quantity = parseInt(removeComma($(this).val()));
        //$("#status").text("");
        //if (_quantity > inventory_qty) {
        //    $("#DetailList_" + id + "__Quantity").val(inventory_qty);
        //    $("#status").text("Tổng số lượng xuất ra không được lớn hơn số lượng tồn kho hiện tại!!");
        //}

        if (isplus === -10 && isminus === -10) {
            isplus = -3;
            isminus = -3;
        }
        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien6();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi giá
    $('#listOrderDetail6').on('change', '.detail_item_price_6', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien6();
        tinhkhuyenmaiHd();
    });

    //Khi thay đổi chiết khấu
    $('#listOrderDetail6').on('change', '.detail_item_discount_6', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscount(6, id);
    });
    $('#listOrderDetail6').on('change', '.detail_item_discount_6_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        ChangeDiscountAmount(6, id);
    });
    //Khi thay đổi chiết khấu
    $('#listOrderDetail6').on('change', '.detail_item_fixed_6_discount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');

        var input_price = $('#DetailList_' + id + '__Price');
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_6');
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chiết khấu
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_fixed_6_discount');
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));
        }
        var discountAmount = discount * total / 100;
        $('tr#product_item_' + id).find('.detail_item_fixed_discount_6_amount').val(numeral(discountAmount).format('0,0'));

        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien6();
        tinhkhuyenmaiHd();
    });
    //$('#listOrderDetail6').on('keypress', '.detail_item_price_6', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $(this).parent().next().find("input:first").focus().select();
    //    }
    //});

    //$('#listOrderDetail6').on('keypress', '.detail_item_qty_6', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $(this).parent().next().find("input:first").focus().select();
    //    }
    //});

    //$('#listOrderDetail6').on('keypress', '.detail_item_discount_6', function (e) {
    //    if (e.which == 13) {
    //        e.preventDefault();
    //        $("#productCode").focus();
    //    }
    //});
    $('#listOrderDetail6').on('focus', '.detail_item_discount_6', function () {
        $(this).select();
    });
    $('#listOrderDetail6').on('focus', '.detail_item_fixed_6_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail6').on('focus', '.detail_item_price_6', function () {
        $(this).select();
    });

    $('#listOrderDetail6').on('focus', '.detail_item_qty_6', function () {
        $(this).select();
    });
    //$('#listOrderDetail6').on('change', '.detail_item_discount_6_amount', function () {
    //    var $this = $(this);
    //    var id = $this.closest('tr').data('id');
    //    tinh_thanh_tien_moi_dong2(id);
    //    tinh_tong_tien6();
    //    tinhkhuyenmaiHd();
    //});
    $('#listOrderDetail6').on('change', '.detail_item_fixed_discount6_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien6();
        tinhkhuyenmaiHd();
    });
    //*************************************************end tab6***************************************************************
    //Khi thay đổi mức chiết khấu chung
    $('#InputDiscount').change(function () {
        if ($(this).val() != '') {
            $(".detail_item_discount").val($(this).val()).trigger("change");
        }
    });
    $('#InputFixedDiscount').change(function () {
        if ($(this).val() != '') {
            $(".detail_item_fixed_discount").val($(this).val()).trigger("change");
        }
    });

    //keyress
    $('#InputDiscount').keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            if ($(this).val() != '') {
                $(".detail_item_discount").val($(this).val()).trigger("change");
            }
        }
    });
    $('#InputFixedDiscount').keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            if ($(this).val() != '') {
                $(".detail_item_fixed_discount").val($(this).val()).trigger("change");
            }
        }
    });
    $('#listOrderDetail').on('keypress', '.detail_item_price', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $(this).parent().next().find("input:first").focus().select();
        }
    });

    $('#listOrderDetail').on('keypress', '.detail_item_qty', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $(this).parent().next().find("input:first").focus().select();
        }
    });

    $('#listOrderDetail').on('keypress', '.detail_item_discount', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $("#productCode").focus();
        }
    });

    $('#productCode').keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            $('#productCode').trigger('change');
        }
    });

    $(window).keydown(function (e) {
        if (e.which == 120 || e.which == 13) {
            e.preventDefault();
            $("#btnShowPayment").click();
        } else
            if (e.which == 119) {
                e.preventDefault();
                OpenPopup('/Customer/Index?IsPopup=true&jsCallback=selectItem_CustomerId', 'Tìm kiếm dữ liệu', 800, 600);
            }
            else if (e.which == 113) {
                e.preventDefault();
                $("#categorySelectList").trigger('chosen:activate');
            }
            else
                if (e.which == 114) {
                    e.preventDefault();
                    $("#productCode").focus();
                }
                else if (e.which == 115) {
                    e.preventDefault();
                    $("#productName").focus();
                }
    });

    //focus
    $('#InputDiscount').focus(function () {
        $(this).select();
    });
    $('#InputFixedDiscount').focus(function () {
        $(this).select();
    });
    $('#listOrderDetail').on('focus', '.detail_item_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail').on('focus', '.detail_item_fixed_discount', function () {
        $(this).select();
    });
    $('#listOrderDetail').on('focus', '.detail_item_price', function () {
        $(this).select();
    });

    $('#listOrderDetail').on('focus', '.detail_item_qty', function () {
        $(this).select();
    });



    //Cho cái đếm tổng cộng nó readonly
    $("#ProductItemCount").attr("readonly", "true");
    //Khi thay đổi chiết khấu
    $('#listOrderDetail').on('change', '.detail_item_discount_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien();
    });
    $('#listOrderDetail').on('change', '.detail_item_fixed_discount_amount', function () {
        var $this = $(this);
        var id = $this.closest('tr').data('id');
        tinh_thanh_tien_moi_dong2(id);
        tinh_tong_tien();
    });
    //mcuong.fit@gmail.com
    //*************************************************begin tính tổng tiền theo dòng chung***************************************************************
    function tinh_thanh_tien_moi_dong2(id) {
        //tính tổng cộng
        if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var input_price = $('tr#product_item_' + id).find('.detail_item_price_1');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var input_price = $('tr#product_item_' + id).find('.detail_item_price_2');
        } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var input_price = $('tr#product_item_' + id).find('.detail_item_price_3');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
            var input_price = $('tr#product_item_' + id).find('.detail_item_price_4');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
            var input_price = $('tr#product_item_' + id).find('.detail_item_price_5');
        } else {
            var input_price = $('tr#product_item_' + id).find('.detail_item_price_6');
        }
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_1');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_2');
        } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_3');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
            var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_4');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
            var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_5');
        } else {
            var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_6');
        }

        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chiết khấu
        if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var discounts = $('tr#product_item_' + id).find('.detail_item_discount_1');
            var input_discount = $('tr#product_item_' + id).find('.detail_item_discount_1_amount');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var discounts = $('tr#product_item_' + id).find('.detail_item_discount_2');
            var input_discount = $('tr#product_item_' + id).find('.detail_item_discount_2_amount');
        } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            var discounts = $('tr#product_item_' + id).find('.detail_item_discount_3');
            var input_discount = $('tr#product_item_' + id).find('.detail_item_discount_3_amount');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
            var discounts = $('tr#product_item_' + id).find('.detail_item_discount_4');
            var input_discount = $('tr#product_item_' + id).find('.detail_item_discount_4_amount');
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
            var discounts = $('tr#product_item_' + id).find('.detail_item_discount_5');
            var input_discount = $('tr#product_item_' + id).find('.detail_item_discount_5_amount');
        } else {
            var discounts = $('tr#product_item_' + id).find('.detail_item_discount_6');
            var input_discount = $('tr#product_item_' + id).find('.detail_item_discount_6_amount');
        }
       // alert('SLD:' + SLDau +'SL:' + qty + '  plus:' + isplus + '  minus:' + isminus + '  one:' + isone);
        var kkk = removeComma(input_discount.val());
        if (input_discount.val() == 0 || input_discount.val() == "0" || isNull(input_discount) == 1) {
            kkk = removeComma(discounts.val() * price / 100);
        }
        else {
            // khi thay đổi số lượng
            if (isplus === -3 & isminus === -3) {
                kkk = kkk / SLDau;
            }

            // khi thay đổi giá tiền
            else if (isplus === -2 & isminus === -2) {
                kkk = kkk;
            }
            //Khi thay đổi %
            else if (isplus === -1 & isminus === -1) {
                kkk = kkk;
            }
            //Khi bấm cộng trừ
            else
            {
                if (isplus === 1 & isminus === 0) {
                    kkk = kkk / (qty - 1);
                }
                else if (isplus === 0 & isminus === 1) {
                    if (qty >= 1) {
                        if (isone === 1) {
                            kkk = kkk;
                        }
                        else {

                            kkk = kkk / (qty + 1);
                        }
                    }
                }
                //if (qty >= 2) {
                //    if (isplus === 1) {
                //        kkk = kkk / (qty - 1);

                //    } else if (isplus === 0 && isminus === 1) {
                //        kkk = kkk / (qty + 1);
                //    }
                //}
                //else if (qty === 2) {
                //    if (isplus === 1) {
                //        kkk = kkk / (qty - 1);

                //    } else if (isplus === 0 && isminus === 1) {
                //        kkk = kkk / (qty + 1);
                //    }
                //}
                //else if (qty === 1) {

                //    if (isminus === 1) {
                //        if (isone == 0) {
                //            kkk = kkk / (qty + 1);
                //            isone = 1;
                //        }
                //        if (isone == 1) {
                //            kkk = kkk / qty;
                //        }

                //    }
                //}
            }

        }

        var kkkk = kkk * qty;
        if (input_discount.val() != 0 & input_discount.val() != "0" & isNull(input_discount) == 0) {
            input_discount.val(numeral(kkkk).format('0,0'));
        }
        //alert("Giảm/1sp: " + kkk + "- Tổng giảm: " + kkkk);



        totalAmount = totalAmount - kkkk;
        if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            $('#ProductItemCount1').val($('#listOrderDetail1 .detailList1 tr').length);
            $('tr#product_item_' + id).find('.detail_item_total_1').text(numeral(totalAmount).format('0,0'));
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            $('#ProductItemCount2').val($('#listOrderDetail2.detailList2 tr').length);
            $('tr#product_item_' + id).find('.detail_item_total_2').text(numeral(totalAmount).format('0,0'));
        } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
            $('#ProductItemCount3').val($('#listOrderDetail3 .detailList3 tr').length);
            $('tr#product_item_' + id).find('.detail_item_total_3').text(numeral(totalAmount).format('0,0'));
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
            $('#ProductItemCount4').val($('#listOrderDetail4 .detailList4 tr').length);
            $('tr#product_item_' + id).find('.detail_item_total_4').text(numeral(totalAmount).format('0,0'));
        } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
            $('#ProductItemCount5').val($('#listOrderDetail5 .detailList5 tr').length);
            $('tr#product_item_' + id).find('.detail_item_total_5').text(numeral(totalAmount).format('0,0'));
        } else {
            $('#ProductItemCount6').val($('#listOrderDetail6 .detailList6 tr').length);
            $('tr#product_item_' + id).find('.detail_item_total_6').text(numeral(totalAmount).format('0,0'));
        }

        isplus = -10;
        isminus = -10;
        isone = 0;
        //SLDau = qty;
    }

    //*************************************************end tính tổng tiền chung***************************************************************
    ////////////////// <====================== Check =================>
    function Tinh_Tong_Tien_Chung(isTab) {
        if (isTab == 1) {

            tinh_tong_tien1();
        }
        else if (isTab == 2) {
            tinh_tong_tien2();
        }
        else if (isTab == 3) {

            tinh_tong_tien3();
        }
        else if (isTab == 4) {

            tinh_tong_tien4();
        }
        else if (isTab == 5) {

            tinh_tong_tien5();
        }
        else if (isTab == 6) {

            tinh_tong_tien6();
        }
    }
    function ChangeDiscountAmount(isTab, id) {
        isplus = -2;
        isminus = -2;
        var input_price = $('tr#product_item_' + id).find('.detail_item_price_' + isTab);
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;
        //S? lu?ng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_' + isTab);
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành ti?n
        var total = parseFloat(price) * qty;
        var totalAmount = total;
        //Chi?t kh?u
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_discount_' + isTab + '_amount');

        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));

            tinh_thanh_tien_moi_dong2(id);
            Tinh_Tong_Tien_Chung(isTab);
            tinhkhuyenmaiHd();
        }

    }
    function ChangeDiscount(isTab, id) {
        isplus = -1;
        isminus = -1;
        var input_price = $('tr#product_item_' + id).find('.detail_item_price_' + isTab);
        var price = input_price.val() != '' ? removeComma(input_price.val()) : 0;

        //Số lượng
        var input_qty = $('tr#product_item_' + id).find('.detail_item_qty_' + isTab);
        var qty = 1;
        if (input_qty.val() == '') {
            input_qty.val(1);
        } else {
            qty = parseInt(removeComma(input_qty.val())) < 0 ? parseInt(removeComma(input_qty.val())) * -1 : parseInt(removeComma(input_qty.val()));
        }
        //Thành tiền
        var total;
        if (qty > 2) {
            total = parseFloat(price) * qty;
        } else {
            total = parseFloat(price) * 1;
        }
        var totalAmount = total;
        //Chiết khấu
        var discount = 0;
        var input_discount = $('tr#product_item_' + id).find('.detail_item_discount_' + isTab);
        if (input_discount.val() == '') {
            input_discount.val(0);
        } else {
            discount = parseInt(removeComma(input_discount.val()));
        }
        var discountAmount = discount * total / 100;
        $('tr#product_item_' + id).find('.detail_item_discount_' + isTab + '_amount').val(0);
        // alert(discount+'-' + $('tr#product_item_' + id).find('.detail_item_discount_' + isTab + '_amount').val());

        //tính tổng cộng
        tinh_thanh_tien_moi_dong2(id);
        Tinh_Tong_Tien_Chung(isTab);
        tinhkhuyenmaiHd();
    }


    $('#listOrderDetail1').on('click', '.detail_item_qty_1', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_1').val();

    });
    $('#listOrderDetail2').on('click', '.detail_item_qty_2', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_2').val();

    });
    $('#listOrderDetail3').on('click', '.detail_item_qty_3', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_3').val();

    });
    $('#listOrderDetail4').on('click', '.detail_item_qty_4', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_4').val();

    });
    $('#listOrderDetail5').on('click', '.detail_item_qty_5', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_5').val();

    });
    $('#listOrderDetail6').on('click', '.detail_item_qty_6', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_6').val();

    });
    $('#listOrderDetail1').on('focus', '.detail_item_qty_1', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_1').val();

    });
    $('#listOrderDetail2').on('focus', '.detail_item_qty_2', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_2').val();

    });
    $('#listOrderDetail3').on('focus', '.detail_item_qty_3', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_3').val();

    });
    $('#listOrderDetail4').on('focus', '.detail_item_qty_4', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_4').val();

    });
    $('#listOrderDetail5').on('focus', '.detail_item_qty_5', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_5').val();

    });
    $('#listOrderDetail6').on('focus', '.detail_item_qty_6', function () {
        var $this = $(this);
        SLDau = $this.closest('tr').find('.detail_item_qty_6').val();
    });

    //$('#idboxsearch2').change(function () {
    //    if (isNull($('#idboxsearch2').val()) === 1) {
           
    //            if (tab3 == 0 && tab1 == 1 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
    //                removeDiscount(1);
    //                tinh_tong_tien1();
    //            } else if (tab3 == 0 && tab1 == 0 && tab2 == 1 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
    //                removeDiscount(2);
    //                tinh_tong_tien2();
    //            } else if (tab3 == 1 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 0 && tab6 == 0) {
    //                removeDiscount(3);
    //                tinh_tong_tien3();
    //            } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 1 && tab5 == 0 && tab6 == 0) {
    //                removeDiscount(4);
    //                tinh_tong_tien4();
    //            } else if (tab3 == 0 && tab1 == 0 && tab2 == 0 && tab4 == 0 && tab5 == 1 && tab6 == 0) {
    //                removeDiscount(5);
    //                tinh_tong_tien5();
    //            } else {
    //                removeDiscount(6);
    //                tinh_tong_tien6();
    //            }
    //    }
    //});
});

// latuyen.fit Comit lan 3