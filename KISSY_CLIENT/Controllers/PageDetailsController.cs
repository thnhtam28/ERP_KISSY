using ERP_API.Filters;
using ERP_API.Models;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using ERP_API.Operation.Nomal;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{
    public class PageDetailsController : Controller
    {
        TinTucOperation tinTucOperation;
        ApiPageSetupOperation apiPageSetupOperation;
        ProductOperation productOperation;
        DM_NHOMSANPHAMOperation menu;
        ApiCartOperation apiCartOperation;
        QuanHuyenOperation quanHuyenOperation;
        KhuyenMaiOperation khuyenMaiOperation;
        public PageDetailsController()
        {
            tinTucOperation = new TinTucOperation();
            apiPageSetupOperation = new ApiPageSetupOperation();
            productOperation = new ProductOperation();
            menu = new DM_NHOMSANPHAMOperation();
            apiCartOperation = new ApiCartOperation();
            quanHuyenOperation = new QuanHuyenOperation();
            khuyenMaiOperation = new KhuyenMaiOperation();
        }
        private void CEOStr(string urlWeb, string urlimage, string META_TITLE = "", string META_DESCRIPTION = "")
        {
            Page_Setup page_Setup = apiPageSetupOperation.GetPage_Setup();
            ViewBag.CompanyName = page_Setup.CompanyName;
            ViewBag.UrlWebsite = urlWeb;
            ViewBag.META_TITLE = META_TITLE;
            ViewBag.META_DESCRIPTION = META_DESCRIPTION;
            if (string.IsNullOrEmpty(urlimage))
                ViewBag.Image = page_Setup.Logo;
            else
                ViewBag.Image = urlimage;
            ViewBag.FacebookPixelID = Common.GetSetting("FacebookPixelID");
            ViewBag.IDGoogleconversion = Common.GetSetting("IDGoogleconversion");
            ViewBag.Google_GTM = Common.GetSetting("Google_GTM");
            ViewBag.Google_TrackingID = Common.GetSetting("Google_TrackingID");
            ViewBag.chatbox = Common.GetSetting_Script("chatbot");

        }
        // GET: PageDetails
        public ActionResult tintucchitietkissy(int id, string slug)
        {
            DM_TINTUC tintuc = tinTucOperation.GetTintuc(id);
            if (tintuc == null)
                return RedirectToAction("Index", "Home");
            DM_NHOMTIN nhomtin = tinTucOperation.GetNhomtin(tintuc.NHOMTIN_ID);
            if (nhomtin != null)
                ViewBag.Menu = nhomtin.TEN_LOAISANPHAM;
            else
                ViewBag.Menu = "";
            CEOStr(Url.Action("tintucchitietkissy", "PageDetails", new { id = id }), tintuc.ANH_DAIDIEN, tintuc.META_TITLE, tintuc.META_DESCRIPTION);
            ViewBag.DanhsachNhomTin = tinTucOperation.GetNhomTinTuc();
            ViewBag.SocialNetwork_News = Common.GetSetting("SocialNetwork_News");

            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "ViewContent";
            ViewBag.ecomm_pagetype_GG = "ViewContent";
            ViewBag.ecomm_totalvalue = "0";


            //begin tao breackscum
            Dictionary<string, string> keyValueMenu = new Dictionary<string, string>();
            string url = Url.Action("nhomtintuc", "PageDetails", new { id = nhomtin.NHOMTIN_ID });
            keyValueMenu.Add(nhomtin.TEN_LOAISANPHAM, url);
            ViewBag.Menu1 = keyValueMenu;
            //end tao breakcsum
            ViewBag.FacebookPixelID = Common.GetSetting("PLUGIN_FACEBOOK");
            var dataTinlienquan = tinTucOperation.GetDanhSachTinTucNoiBatLienQuan(tintuc.TINTUC_ID).Take(3).ToList();
            var datatinMoiNhat = tinTucOperation.GetDanhSachTinTucMoiNhat(3);
            ViewBag.TinTucLienQuan = dataTinlienquan;
            ViewBag.datatinMoiNhat = datatinMoiNhat;
            if (string.IsNullOrEmpty(slug))
            {
                DM_TINTUC TT = tinTucOperation.GetTintuc(id);
                slug = TT.URL_SLUG;
                return RedirectToRoute("tintucchitietkissy", new { id = id, slug = slug });
            }


            return View(tintuc);
        }

        public ActionResult baivietchitietkissy(string code)
        {
            DM_BAIVE_YHL baiviet = tinTucOperation.GetBaiViet(code);
            if (baiviet == null)
                return RedirectToAction("Index", "Home");

            CEOStr(Url.Action("baivietchitietkissy", "PageDetails", new { id = code }), baiviet.HINHANH, "", "");

            ViewBag.SocialNetwork_News = Common.GetSetting("SocialNetwork_News");

            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "ViewContent";
            ViewBag.ecomm_pagetype_GG = "ViewContent";
            ViewBag.ecomm_totalvalue = "0";



            return View(baiviet);
        }
        public ActionResult sanphamchitiet(int? id, string slug)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int productid = (int)id;
            Product_Promotion sale_Product = productOperation.GetKhuyenMai_Kissy(productid);
            //Sale_Product_SKU sale_Product_SKU = 
            List<Sale_Product_SKU> listsku = productOperation.GET_Sale_Product_SKU().Where(x => x.Product_idSKU == productid).ToList();
            if(listsku.Count == 0)
            {
                var a = productOperation.GET_Sale_Product_SKU().Where(x => x.Product_id == productid).FirstOrDefault();
                if (a != null)
                {
                    listsku = productOperation.GET_Sale_Product_SKU().Where(x => x.Product_idSKU == a.Product_idSKU).ToList();
                }
            }
            List<string> size = new List<string>();
            List<string> colour = new List<string>();
            // size.Add(sale_Product.Sale_Product.Size);
            // colour.Add(sale_Product.Sale_Product.color);
            foreach (var i in listsku)
            {
                if (sale_Product.Sale_Product.color != i.color)
                {
                    colour.Add("," + i.color);
                }
                if (sale_Product.Sale_Product.Size != i.Size)
                {

                    size.Add("," + i.Size);
                }


            }
            size = size.Distinct().ToList();
            colour = colour.Distinct().ToList();
            foreach (var i in size)
            {
                sale_Product.Sale_Product.Size = sale_Product.Sale_Product.Size + i;
            }
            foreach (var i in colour)
            {
                sale_Product.Sale_Product.color = sale_Product.Sale_Product.color + i;
            }

            List<string> imgList = new List<string>();
            if (!string.IsNullOrEmpty(sale_Product.Sale_Product.List_Image))
            {
                List<string> listImgThum = sale_Product.Sale_Product.List_Image.Split(',').ToList();
                foreach (string imageUrl in listImgThum)
                {
                    imgList.Add(productOperation.GetUrlImage(imageUrl));
                }
            }
            ViewBag.imgList = imgList;

            if (sale_Product.Sale_Product == null)
                return RedirectToAction("Index", "Home");

            Dictionary<string, string> keyValueMenu = new Dictionary<string, string>();
            //if (!string.IsNullOrEmpty(sale_Product.Sale_Product.NHOMSANPHAM_ID_LST))
            //{
            //    var arrID = sale_Product.Sale_Product.NHOMSANPHAM_ID_LST.Split(',').ToList();
            //    int menuID = int.Parse(arrID.FirstOrDefault());
            //    DM_NHOMSANPHAM nhomsp = menu.GetNHOMSANPHAM(menuID);
            //    string url = Url.Action("sanphamchitiet", "PageDetails", new { id = sale_Product.Sale_Product.Id,slug= sale_Product.Sale_Product .URL_SLUG});
            //    keyValueMenu.Add(nhomsp.TEN_NHOMSANPHAM, url);
            //}
            //else
            //{
            //    var loaispID = sale_Product.Sale_Product.LOAISANPHAM_ID;
            //    if (loaispID != null)
            //    {
            //        int idnotNull = (int)loaispID;
            //        DM_LOAISANPHAM loaisp = menu.Get_LOAISANPHAM(idnotNull);
            //        string url = Url.Action("lstproductloaisanpham", "View_Playout", new { id = loaisp.LOAISANPHAM_ID });
            //        keyValueMenu.Add(loaisp.TEN_LOAISANPHAM, url);
            //    }
            //}
            ViewBag.Menu = keyValueMenu;
            List<Page_ViewProductUser> listview = new List<Page_ViewProductUser>();
            List<Sale_Product> list = Session["ViewProduct"] as List<Sale_Product>;

            List<Product_Promotion> listEx = new List<Product_Promotion>();

            if (list != null)
            {
                foreach (var item in list)
                {
                    if (productOperation.isConHang(item.Id))
                    {
                        productOperation = new ProductOperation();
                        Product_Promotion product_Promotion = productOperation.GetSale_Product_Promotion(item.Id);
                        listEx.Add(product_Promotion);
                    }

                }
            }

            ViewBag.ecomm_prodid = sale_Product.Sale_Product.Code;
            ViewBag.ecomm_pagetype_FB = "ViewContent";
            ViewBag.ecomm_pagetype_GG = "offerdetail";
            ViewBag.ecomm_totalvalue = sale_Product.Sale_Product.PriceOutBound;
            string strCategoty = "";

            int pLOAISANPHAM_ID = 0;
            string pIDLOAISANPHAMLIST = sale_Product.Sale_Product.NHOMSANPHAM_ID_LST;
            if (!string.IsNullOrEmpty(pIDLOAISANPHAMLIST))
            {
                pLOAISANPHAM_ID = int.Parse(pIDLOAISANPHAMLIST.Split(',')[0]);
            }


            strCategoty = menu.Get_STRCATEGOTY(pLOAISANPHAM_ID);
            ViewBag.ValueFB = "{ content_type: 'product', content_ids: '[\"" + sale_Product.Sale_Product.Code + "\"]', value: '" + sale_Product.Sale_Product.PriceOutBound + "', content_name: '" + sale_Product.Sale_Product.Name + "' ,currency: 'VND', content_category: '" + strCategoty + "' }";


            if (list != null)
            {
                if (list.Count(a => a.Id == id) == 0)
                {
                    list.Add(sale_Product.Sale_Product);
                    listEx.Add(sale_Product);
                    Session["ViewProduct"] = list;
                    Session["ViewProductEx"] = listEx;
                }

            }
            else
            {
                list = new List<Sale_Product>();
                list.Add(sale_Product.Sale_Product);
                listEx.Add(sale_Product);
                Session["ViewProduct"] = list;
                Session["ViewProductEx"] = listEx;
            }
            if (!string.IsNullOrEmpty(User.Identity.GetUserId()))
            {
                Page_ViewProductUser model = new Page_ViewProductUser();
                model.UserID = User.Identity.GetUserId();
                model.ProductID = id;
                apiCartOperation.InsertViewProduct(model);
            }

            List<Sale_Product> listproduct = productOperation.GetSanPhamTuongTu(sale_Product.Sale_Product.Id).Take(8).ToList();

            //begin hoa sua lai
            List<Product_Promotion> listEx2 = new List<Product_Promotion>();

            if (listproduct != null)
            {
                foreach (var item in listproduct)
                {
                    if (productOperation.isConHang(item.Id))
                    {
                        productOperation = new ProductOperation();
                        Product_Promotion product_Promotion = productOperation.GetSale_Product_Promotion(item.Id);
                        listEx2.Add(product_Promotion);
                    }

                }
            }
            //end hoa sua lai

            ViewBag.listSPLQ = listproduct;
            ViewBag.listSPLQEx = listEx2;
            //List<Product_Size> listsize = new List<Product_Size>();
            //listsize.Add(new Product_Size { ID = sale_Product.Sale_Product.Id, Size = sale_Product.Sale_Product.Size });
            //listsize.AddRange(productOperation.GetProduct_Sizes(sale_Product.Sale_Product.Id));
            ViewBag.ProductSize = !string.IsNullOrEmpty(sale_Product.Sale_Product.Size) ? sale_Product.Sale_Product.Size.Split(',').ToList() : new List<string>();
            CEOStr(Url.Action("sanphamchitiet", "PageDetails", new { id = id }), sale_Product.Sale_Product.Image_Name, sale_Product.Sale_Product.META_TITLE, sale_Product.Sale_Product.META_DESCRIPTION);
            ViewBag.SocialNetwork_Product = Common.GetSetting("SocialNetwork_Product");


            ViewBag.IsTinTuc = true;
            return View(sale_Product);
        }
        public ActionResult hethongcuahang()
        {
            var hethong = productOperation.GetHeThongCuaHang();
            var datatinMoiNhat = tinTucOperation.GetDanhSachTinTucMoiNhat(3);
            ViewBag.datatinMoiNhat = datatinMoiNhat;
            return View(hethong);
        }
        public ActionResult danhmuctintuc(int? page)
        {
            if (page == null)
                page = 1;
            int skip = ((int)page - 1) * 6;
            var data = tinTucOperation.GetDanhSachTinTucMoiNhat(100);
            var dataCurr = data.Skip(skip).Take(6);
            ViewBag.Page = page;
            ViewBag.TotalPage = data.Count % 6 > 0 ? (data.Count / 6) + 1 : data.Count / 6;
            return View(dataCurr);
        }
        private bool ExistString(string lstColor, string color)
        {
            if (string.IsNullOrEmpty(lstColor))
                return false;
            List<string> lstS = lstColor.Split(',').ToList();
            var col = lstS.FirstOrDefault(a => a == color);
            return col != null ? true : false;
        }
        [HttpGet]
        public ActionResult FilterProduct(int id, string color, string size)
        {
            List<Product_Promotion> result = new List<Product_Promotion>();
            List<Sale_Product> listproduct = productOperation.GetSanPhamTuongTu(id);

            foreach (var product in listproduct)
            {
                var sizeGroup = productOperation.GetProduct_Sizes(product.Id);
                Product_Size product_Size = sizeGroup.FirstOrDefault(a => a.Size == size);
                if (ExistString(product.color, color) && ExistString(product.Size, size))
                {
                    Product_Promotion product_Promotion = productOperation.GetSale_Product_Promotion(product.Id);
                    if (product_Promotion != null)
                        result.Add(product_Promotion);
                }

            }
            return PartialView(result.Take(8).ToList());
        }
        public ActionResult cart()
        {
            ShoppingCartModels model = new ShoppingCartModels();
            if (Session["Cart"] != null)
                model.Cart = (ShoppingCart)Session["Cart"];
            else
                model.Cart = new ShoppingCart();
            CEOStr(Url.Action("cart", "PageDetails"), null);
            string StrSKU = "";
            decimal? pTotal = 0;
            string strCatetogory = "";
            for (int i = 0; i < model.Cart.ListItem.Count; i++)
            {
                StrSKU = StrSKU + "\",\"" + model.Cart.ListItem[i].SKU;
                pTotal = pTotal + model.Cart.ListItem[i].Total;
                strCatetogory = model.Cart.ListItem[i].ProductName;
            }
            if (StrSKU != "")
            {
                StrSKU = StrSKU.Substring(3);
            }

            ViewBag.ecomm_prodid = StrSKU;
            ViewBag.ecomm_pagetype_FB = "AddToCart";
            ViewBag.ecomm_pagetype_GG = "conversionintent";
            ViewBag.ecomm_totalvalue = pTotal;
            ViewBag.ValueFB = "{ content_type: 'product', content_ids: '[\"" + StrSKU + "\"]', value: '" + pTotal + "',currency: 'VND', content_category: '" + strCatetogory + "' }";
            ViewBag.userid = User.Identity.GetUserId();

            List<Product_Promotion> listEx = Session["ViewProductEx"] as List<Product_Promotion>;
            ViewBag.lstDaXem = listEx;
            return View(model);
        }
        public ActionResult GetQuanHuyen(string id)
        {
            List<System_Loc_District> quanhuyen = quanHuyenOperation.GetQuanHuyen(id);
            return Json(quanhuyen, JsonRequestBehavior.AllowGet);
        }
        public ActionResult checkout()
        {
            CheckOutViewModel result = new CheckOutViewModel();
            ShoppingCartModels model = new ShoppingCartModels();
            if (Session["Cart"] != null)
                model.Cart = (ShoppingCart)Session["Cart"];
            else
            {
                return RedirectToAction("cart");
            }
            ShoppingCartItem_hoadon km = model.Cart.GetPriceKhuyenMai();

            if (km != null)
            {
                model.Cart.PromotionId = km.PromotionId;
                model.Cart.PromotionDetailId = km.PromotionDetailId;
                model.Cart.PromotionValue = km.PromotionValue;
                model.Cart.IsMoney = km.IsMoney;
            }


            CEOStr(Url.Action("cart", "PageDetails"), null);
            string StrSKU = "";
            decimal? pTotal = 0;
            string strCatetogory = "";
            for (int i = 0; i < model.Cart.ListItem.Count; i++)
            {
                StrSKU = StrSKU + "\",\"" + model.Cart.ListItem[i].SKU;
                pTotal = pTotal + model.Cart.ListItem[i].Total;
                strCatetogory = model.Cart.ListItem[i].ProductName;
            }
            if (StrSKU != "")
            {
                StrSKU = StrSKU.Substring(3);
            }

            ViewBag.ecomm_prodid = StrSKU;
            ViewBag.ecomm_pagetype_FB = "AddToCart";
            ViewBag.ecomm_pagetype_GG = "conversionintent";
            ViewBag.ecomm_totalvalue = pTotal;
            ViewBag.ValueFB = "{ content_type: 'product', content_ids: '[\"" + StrSKU + "\"]', value: '" + pTotal + "',currency: 'VND', content_category: '" + strCatetogory + "' }";
            ViewBag.userid = User.Identity.GetUserId();
            result.ShoppingCartModels = model;

            result.System_Loc_Provinces = quanHuyenOperation.GetTinhThanhPho(); ;
            return View(result);
        }


        [HttpPost]
        public JsonResult ChangeProduct(int id, string color, string size)
        {
            int productid = (int)id;


            Sale_Product_SKU prosku = productOperation.GET_Sale_Product_SKU().Where(x => x.Product_idSKU == productid && x.color == color && x.Size == size).FirstOrDefault();
            if(prosku == null)
            {
                var a = productOperation.GET_Sale_Product_SKU().Where(x => x.Product_id == productid).FirstOrDefault();
                prosku = productOperation.GET_Sale_Product_SKU().Where(x => x.Product_idSKU == a.Product_idSKU && x.color == color && x.Size == size).FirstOrDefault();
            }
            if(prosku != null){
                Product_Promotion sale_Product = productOperation.GetKhuyenMai_Kissy(prosku.Product_id.Value);
                return Json(sale_Product);
            }
            return Json(0);
            //List<string> imgList = new List<string>();
            //if (!string.IsNullOrEmpty(sale_Product.Sale_Product.List_Image))
            //{
            //    List<string> listImgThum = sale_Product.Sale_Product.List_Image.Split(',').ToList();
            //    foreach (string imageUrl in listImgThum)
            //    {
            //        imgList.Add(productOperation.GetUrlImage(imageUrl));
            //    }
            //}
            //ViewBag.imgList = imgList;
            
            
        }
    }
}