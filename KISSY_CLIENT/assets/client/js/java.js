function checkDeal(productid, price) {
    $("#showInformationchopdeal").html("");
    $.ajax({
        url: "/View_PLayout/Main_chopdealInformationPartial",
        data: { "id": productid, "price": price },
        method: "POST",
        dataType: "HTML",
        success: function (data) {
            $("#showInformationchopdeal").html(data);
            var element = document.getElementById("popupD");
            element.classList.add("dachop");
        }

    })
}
function updateDeal() {

}
function chopdeal(id,price) {

    checkDeal(id, price);

}
function xoadeal() {
	var element = document.getElementById("popupD");
	element.classList.remove("dachop");
}
function dmsp1() {
	var element = document.getElementById("DanhSachSanPham");
	element.classList.remove("doikieu2");
}
function dmsp2() {
	var element = document.getElementById("DanhSachSanPham");
	element.classList.add("doikieu2");
}