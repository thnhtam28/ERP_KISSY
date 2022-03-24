using ERP_API.EnumData;
using ERP_API.Models;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{
    public class View_ChopDealController : Controller
    {
        ProductOperation productOperation;
        public View_ChopDealController()
        {
            productOperation = new ProductOperation();
        }
        // GET: View_ChopDeal
        [HttpPost]
        public ActionResult AddChopDealGiohang(ChopdealViewModel model)
        {
            Sale_Product P = productOperation.GetSale_Product(model.ProductID);
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
                    Price = model.Price,
                    ProductSumary = P.Sumary,
                    Quantity = 1,
                    Total = model.Price,
                    TypeProduct = (int)EnumCartProduct.ChopDeal,
                    SKU= P.Code
            };
                objCart.AddToCart(item);
                Session["Cart"] = objCart;
                Session["ThongTinChopDeal"] = model;
            }
            return RedirectToAction("Cart", "View_PLayout");
        }


        [HttpPost]
        public ActionResult AddDeal()
        {
            return View();
        }
    }
}