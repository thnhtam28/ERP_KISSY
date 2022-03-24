using ERP_API.EnumData;
using ERP_API.Models;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using ERP_API.Operation.Nomal;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{
    public class View_CartController : Controller
    {
        ApiProductOperation apiProductOperation;
        //ApiCartOperation apiCartOperation;
        CartOperation cartOperation;
        ProductOperation productOperation;
        public View_CartController()
        {
            apiProductOperation = new ApiProductOperation();
            //apiCartOperation = new ApiCartOperation();
            cartOperation = new CartOperation();
            //
            productOperation = new ProductOperation();
        }
        // thêm vào giỏ hàng 1 sản phẩm có id = id của sản phẩm
        [HttpPost]
        public ActionResult AddCart(int id, int soluong, decimal gia, string color, string size, int pID, int pdID, decimal pValue, bool pType)
        {
            string mess = "0";
            Sale_Product P = productOperation.GetSale_Product(id);
            if (P != null)
            {
                ShoppingCart objCart = (ShoppingCart)Session["Cart"];
                if (objCart == null)
                {
                    objCart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem()
                {
                    Photo = P.Image_Name,
                    ProductName = P.Name,
                    ProductID = P.Id,
                    Price = gia,
                    ProductSumary = P.Sumary,
                    Quantity = soluong,
                    Total = gia * soluong,
                    TypeProduct = (int)EnumCartProduct.BinhThuong,
                    SKU = P.Code,
                    Color = color,
                    Size = size,
                    PromotionDetailId = pdID,
                    PromotionId = pID,
                    PromotionValue = pValue,
                    IsMoney = pType
                };
                objCart.AddToCart(item);
                Session["Cart"] = objCart;
                Session["soluong"] = objCart.CountItemCart();
                mess = objCart.CountItemCart().ToString();

            }

            return Json(mess);
        }

        public ActionResult DeleteItemFromCart(int id)
        {
            ShoppingCart objCart = (ShoppingCart)Session["Cart"];
            if (objCart != null)
            {
                objCart.RemoveFromCart(id);
                Session["Cart"] = objCart;
                Session["soluong"] = "(" + objCart.CountItemCart() + ")";
            }
            return RedirectToAction("cart", "PageDetails");
        }
        public ActionResult UpdateQuantity(int proID, int quantity)
        {
            if (quantity < 0)
            {
                quantity = 0;
            }
            ShoppingCart objCart = (ShoppingCart)Session["Cart"];
            if (objCart != null)
            {

                objCart.UpdateQuantity(proID, quantity);
                Session["Cart"] = objCart;
            }


            return Json(objCart.GetTotal(), JsonRequestBehavior.AllowGet);

        }
        public ActionResult cartTablePartial(ShoppingCart objCart)
        {
            ShoppingCartModels model = new ShoppingCartModels();
            if (Session["Cart"] != null)
                model.Cart = (ShoppingCart)Session["Cart"];
            else
                model.Cart = new ShoppingCart();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult checkout(CartCheckOutInfo model)
        {
            string mess = "";
            ShoppingCart objCart = (ShoppingCart)Session["Cart"];
            CartMessage cartMessage = new CartMessage();

            if (objCart != null)
            {
                model.UserID = User.Identity.GetUserId();
                model.Total = objCart.GetTotal();
                CartCheckOut cartCheckOut = new CartCheckOut();
                cartCheckOut.cartCheckOutInfo = model;
                cartCheckOut.cartCheckOutInfo.PHISHIP = objCart.PhiShip;
                cartCheckOut.ListItem = objCart.ListItem;

                cartCheckOut.cartCheckOutInfo.PromotionId = objCart.PromotionId;
                cartCheckOut.cartCheckOutInfo.PromotionDetailId = objCart.PromotionDetailId;
                cartCheckOut.cartCheckOutInfo.PromotionValue = objCart.PromotionValue;
                cartCheckOut.cartCheckOutInfo.IsMoney = objCart.IsMoney;




                string madh = "";
                string thongbao = "";
                mess = cartOperation.InsertInvoice(cartCheckOut, ref madh, ref thongbao);
                if (mess == "OK")
                {
                    cartMessage.Message = "Đặt hàng thành công";
                    cartMessage.MessageProduct = thongbao;
                    cartMessage.Total = string.Format("{0:0,0 VNĐ}", model.Total);
                    cartMessage.Total_value = model.Total;
                    cartMessage.StrSKU = "";
                    for (int i = 0; i < cartCheckOut.ListItem.Count; i++)
                    {
                        cartMessage.StrSKU = cartMessage.StrSKU + "\",\"" + cartCheckOut.ListItem[i].SKU;
                    }
                    if (cartMessage.StrSKU != "")
                    {
                        cartMessage.StrSKU = cartMessage.StrSKU.Substring(3);
                    }
                    cartMessage.CountItem = cartCheckOut.ListItem.Count;
                    cartMessage.Icon = "../img/success-icon.png";
                    cartMessage.HTTT = "Thanh toán khi nhận hàng (COD)";
                    cartMessage.MaDH = madh;
                    Session["Cart"] = null;
                    Session["soluong"] = "(0)";
                    // Session["ThongTinChopDeal"] = null;
                }

            }
            else
            {
                cartMessage.Message = "Đơn hàng của bạn đã hết hạn, do chờ đợi quá lâu. Xin vui lòng chọn lại sản phẩm.";
                cartMessage.Total = "0";
                cartMessage.Total_value = 0;
                cartMessage.Icon = "../img/information-icon.png";
                cartMessage.HTTT = "";
                cartMessage.MaDH = "";

            }
            Session["thongbaoCart"] = cartMessage;
            string url = Url.Action("confirmCheckout", "View_Cart");
            return Json(url);
        }
        public ActionResult confirmCheckout()
        {
            CartMessage cartMessage = new CartMessage();
            cartMessage = Session["thongbaoCart"] as CartMessage;
            return View(cartMessage);
        }
        public ActionResult UpdatePriceShip(int value, string thanhpho, string quanhuyen)
        {
            ShoppingCart objCart = (ShoppingCart)Session["Cart"];
            if (objCart != null)
            {
                objCart.PhiShip = value;
                objCart.ThanhPho = thanhpho;
                objCart.QuanHuyen = quanhuyen;
                Session["Cart"] = objCart;
            }
            return Json(objCart.GetTotal(), JsonRequestBehavior.AllowGet);
        }
    }
}