using ERP_API.Models;
using ERP_API.Models.Account;
using ERP_API.Operation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_API.Operation.Nomal;
using ERP_API.Models.ViewModel;
using Microsoft.AspNet.Identity;
using ERP_API.Operation.API;
using ERP_API.EnumData;
using ERP_API.Filters;
using Erp.Domain.Helper;

namespace ERP_API.Controllers
{
    public class OptionValue
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class View_PLayoutController : Controller
    {
        ApiCategoryOperation apiCategoryOperation;
        ApiProductOperation apiProductOperation;
        apiOperation userAPI;
        ApiPageSetupOperation apiPageSetupOperation;
        ApiPageNewOperation apiPageNewOperation;
        ApiPageSileOperation apiPageSileOperation;
        ApiCartOperation apiCartOperation;
        //
        DM_NHOMSANPHAMOperation menu;
        DM_LoaiSanPhamOperation loaisanpham;
        ProductOperation productOperation;
        DealHotOperation dealHotOperation;
        DM_NGHESY_TINDUNGOperation nghesyOpertion;
        SlideOperation slideOperation;
        CategoriesOperation categoriesOperation;
        DM_NHOMSANPHAMOperation nhomsanphamOperation;
        QuanHuyenOperation quanHuyenOperation;
        BaiVietOperation baiVietOperation;
        TinTucOperation tinTucOperation;
        InvoiceOperation invoiceOperation;
        FeedBackOperation feedBackOperation;
        public View_PLayoutController()
        {
            apiCategoryOperation = new ApiCategoryOperation();
            apiProductOperation = new ApiProductOperation();
            userAPI = new apiOperation();
            apiPageSetupOperation = new ApiPageSetupOperation();
            apiPageNewOperation = new ApiPageNewOperation();
            apiPageSileOperation = new ApiPageSileOperation();
            apiCartOperation = new ApiCartOperation();
            //
            menu = new DM_NHOMSANPHAMOperation();
            loaisanpham = new DM_LoaiSanPhamOperation();
            productOperation = new ProductOperation();
            dealHotOperation = new DealHotOperation();
            nghesyOpertion = new DM_NGHESY_TINDUNGOperation();
            slideOperation = new SlideOperation();
            categoriesOperation = new CategoriesOperation();
            nhomsanphamOperation = new DM_NHOMSANPHAMOperation();
            quanHuyenOperation = new QuanHuyenOperation();
            baiVietOperation = new BaiVietOperation();
            tinTucOperation = new TinTucOperation();
            invoiceOperation = new InvoiceOperation();
            feedBackOperation = new FeedBackOperation();
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
        // GET: PLayout
        #region Header
        public ActionResult HeaderPartial()
        {
            Page_Setup page_Setup = apiPageSetupOperation.GetPage_Setup();
            ViewBag.GMT_HEAD = Common.GetSetting_Script("GMT_HEAD");
            ViewBag.GMT_BODY = Common.GetSetting_Script("GMT_BODY");

            return PartialView(page_Setup);
        }
        public ActionResult loginPartial()
        {
            CustomerInfo user = null;
            if (!string.IsNullOrEmpty(User.Identity.GetUserId()))
                user = userAPI.GetCustomerInfo(User.Identity.GetUserId());
            return PartialView(user);
        }
        public ActionResult searchHeaderPartial()
        {
            //var list = nhomsanphamOperation.GetCapDM_NHOMSANPHAMs(1);
            return PartialView();
        }
        public ActionResult Search(string search = "", int sort = 0, List<string> checkthuonghieu = null, int checkprice = 0, int page = 1, string nextprev = "")
        {

            ViewBag.listThuongHieu = checkthuonghieu;
            ViewBag.sort = new SelectList(GetSortProduct(), "Value", "Text", sort);
            ViewBag.sortValue = sort;
            ViewBag.searchstr = search;
            ViewBag.checkprice = checkprice;
            ViewBag.gia = GetGia();
            List<ThuongHieuViewModel> thuonghieu = productOperation.GetThuongHieu();
            ViewBag.thuonghieu = thuonghieu;
            CEOStr(Url.Action("lstBestSeller", "View_Playout"), null);
            List<Product_Promotion> lstProduct = productOperation.GetSearchProduct(search);
            ViewBag.sanphamnoibat = lstProduct.Where(a => a.Sale_Product.IS_NOIBAT == true).OrderByDescending(a => Guid.NewGuid()).Take(5).ToList();
            if (checkthuonghieu != null)
            {
                lstProduct = lstProduct.Where(a => checkthuonghieu.Contains(a.Sale_Product.Origin)).ToList();
            }
            lstProduct = CheckGia(lstProduct, checkprice);

            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "Search";
            ViewBag.ecomm_pagetype_GG = "searchresults";


            ViewBag.ecomm_totalvalue = "0";

            switch (sort)
            {
                case (int)EnumSortProduct.MD:
                    break;
                case (int)EnumSortProduct.GIACT:
                    lstProduct = SortGia(lstProduct, (int)EnumSortProduct.GIACT);
                    break;
                case (int)EnumSortProduct.GIATC:
                    lstProduct = SortGia(lstProduct, (int)EnumSortProduct.GIATC);
                    break;
                case (int)EnumSortProduct.TENAZ:
                    lstProduct = lstProduct.OrderBy(a => a.Sale_Product.Name).ToList();
                    break;
                case (int)EnumSortProduct.TENZA:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.Name).ToList();
                    break;
                case (int)EnumSortProduct.CU:
                    lstProduct = lstProduct.OrderBy(a => a.Sale_Product.Id).ToList();
                    break;
                case (int)EnumSortProduct.MOI:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.Id).ToList();
                    break;
                case (int)EnumSortProduct.BANCHAY:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.IS_NOIBAT).ToList();
                    break;
            }
            int soluong1trang = 30;
            int soluongsp = lstProduct.Count;
            int sotrang = soluongsp / soluong1trang;
            if ((soluongsp % soluong1trang) > 0)
                sotrang = sotrang + 1;
            ViewBag.sotrang = sotrang;

            if (nextprev == ">")
                page = page + 1;
            if (nextprev == "<")
                page = page - 1;
            if (page < 0) page = 0;
            if (page > sotrang) page = sotrang;

            ViewBag.page = page;
            if (sotrang > 1)
                lstProduct = lstProduct.Skip((page - 1) * soluong1trang).Take(soluong1trang).ToList();
            return View(lstProduct);
        }
        public ActionResult cartPartial()
        {
            return PartialView();
        }
        public ActionResult HeaderMenuPartial()
        {
            var value = menu.GetTatCaDanhMuc();
            return PartialView(value);
        }
        public ActionResult HeaderMenuMobilePartial()
        {
            string userID = User.Identity.GetUserId();
            CustomerInfo customerInfo = userAPI.GetCustomerInfo(userID);
            ViewBag.userid = userID;
            ViewBag.nameuser = customerInfo.LastName;
            var value = menu.GetTatCaDanhMuc();
            return PartialView(value);
        }
        #endregion

        #region Main
        #region MenuLeft
        public ActionResult MenuLeftPartial()
        {
            var list = apiCategoryOperation.GetCategories();
            return PartialView(list);
        }
        #endregion
        #region Partial_Danh sách sản phẩm theo Menu
        public ActionResult Main_ListProductPartial()
        {
            List<System_Category_View> system_Category = apiCategoryOperation.GetCategories();
            foreach (var item in system_Category)
                item.Sale_Products = apiProductOperation.Get_Products(item.Value);

            return PartialView(system_Category);
        }
        public ActionResult Main_LoaiSanPhamKhongCha()
        {
            return PartialView(loaisanpham.GetLoaiSanPhamNullViews());
        }
        public ActionResult Main_LoaiSanPham()
        {
            return PartialView(loaisanpham.GetLoaiSanPhamViews());
        }
        public ActionResult Main_SanPham_bestsaller()
        {
            return PartialView(productOperation.GetProduct_Best_seller());
        }

        public ActionResult Main_chopdealPartial()
        {
            return PartialView(dealHotOperation.GetKM_DotXuat());
        }
        public ActionResult Main_chopdealInformationPartial(int id, decimal price)
        {
            int? soluongcon = dealHotOperation.GetKM_DotXuat().Product_Commussions.FirstOrDefault(a => a.Sale_Product.Id == id).Sale_Inventory.Quantity;
            ChopdealViewModel chopdealViewModel = new ChopdealViewModel();
            chopdealViewModel.Status = true ? soluongcon > 0 : chopdealViewModel.Status = false;
            chopdealViewModel.ProductID = id;
            chopdealViewModel.Price = price;
            return PartialView(chopdealViewModel);
        }
        public ActionResult Main_NgheSyTinDungPartial()
        {
            return PartialView(nghesyOpertion.GetBVNgheSyTinDung());
        }
        public ActionResult Main_GioiThieuPartial()
        {
            return PartialView(nghesyOpertion.Get_BAIVE_YHL());
        }
        public ActionResult Main_BaoChiPartial()
        {
            return PartialView(nghesyOpertion.GetBAOCHIs());
        }
        #endregion
        #region Main tin tức
        public ActionResult Main_NewPartial()
        {
            List<Page_New> list = apiPageNewOperation.GetPage_News().Take(10).ToList();
            return PartialView(list);
        }
        #endregion
        #region Main Slide
        public ActionResult Main_SlidePartial()
        {
            List<DM_BANNER_SLIDER> List = slideOperation.GetSlides();
            return PartialView(List);
        }
        #endregion
        #endregion

        #region footer
        public ActionResult FooterPartial()
        {

            Page_Setup page_Setup = apiPageSetupOperation.GetPage_Setup();
            return PartialView(page_Setup);
        }
        #endregion

        #region Other
        public ActionResult OtherPartial()
        {
            return PartialView();
        }
        #region xem nhanh
        [HttpGet]
        public JsonResult ViewProduct(int id)
        {
            Sale_Product product = apiProductOperation.Get_Product(id);

            return Json(product, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region view
        public ActionResult tintucnoibatkhuyenmai()
        {
            ViewBag.listNoiBat = tinTucOperation.GetDanhSachTinTucNoiBatMoiNhat(4);
            ViewBag.listKhuyenMai = tinTucOperation.GetTinKhuyenMais().Take(4).ToList();
            CEOStr(Url.Action("tintucnoibatkhuyenmai", "View_Playout"), null);
            return View();
        }
        public ActionResult tinnoibat(int page = 1, string nextprev = "")
        {
            var data = tinTucOperation.GetDanhSachTinTucNoiBatMoiNhat();
            int soluong1trang = 30;
            int soluongsp = data.Count;
            int sotrang = soluongsp / soluong1trang;
            if ((soluongsp % soluong1trang) > 0)
                sotrang = sotrang + 1;
            ViewBag.sotrang = sotrang;
            if (nextprev == ">")
                page = page + 1;
            if (nextprev == "<")
                page = page - 1;
            if (page < 0) page = 0;
            if (page > sotrang) page = sotrang;

            ViewBag.page = page;
            if (sotrang > 1)
                data = data.Skip((page - 1) * soluong1trang).Take(soluong1trang).ToList();
            CEOStr(Url.Action("tinnoibat", "View_Playout", new { page = page }), null);
            return View(data);

        }
        public ActionResult tinkhuyenmai(int page = 1, string nextprev = "")
        {
            var data = tinTucOperation.GetTinKhuyenMais();
            int soluong1trang = 30;
            int soluongsp = data.Count;
            int sotrang = soluongsp / soluong1trang;
            if ((soluongsp % soluong1trang) > 0)
                sotrang = sotrang + 1;
            ViewBag.sotrang = sotrang;
            if (nextprev == ">")
                page = page + 1;
            if (nextprev == "<")
                page = page - 1;
            if (page < 0) page = 0;
            if (page > sotrang) page = sotrang;

            ViewBag.page = page;
            if (sotrang > 1)
                data = data.Skip((page - 1) * soluong1trang).Take(soluong1trang).ToList();
            CEOStr(Url.Action("tinkhuyenmai", "View_Playout", new { page = page }), null);
            return View(data);

        }
        public ActionResult tintuckhuyenmaichitiet(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            int idt = (int)id;
            DM_TIN_KHUYENMAI model = tinTucOperation.GetTinKhuyenMai(idt);
            if (model == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("tintuckhuyenmaichitiet", "View_Playout", new { id = id }), model.ANH_DAIDIEN);
            return View(model);
        }
        public ActionResult camnhankhachhang(int page = 1, string nextprev = "")
        {
            var data = nghesyOpertion.GetCamNhanKhachHang();
            int soluong1trang = 20;
            int soluongsp = data.Count;
            int sotrang = soluongsp / soluong1trang;
            if ((soluongsp % soluong1trang) > 0)
                sotrang = sotrang + 1;
            ViewBag.sotrang = sotrang;
            if (nextprev == ">")
                page = page + 1;
            if (nextprev == "<")
                page = page - 1;
            if (page < 0) page = 0;
            if (page > sotrang) page = sotrang;

            ViewBag.page = page;
            if (sotrang > 1)
                data = data.Skip((page - 1) * soluong1trang).Take(soluong1trang).ToList();
            CEOStr(Url.Action("camnhankhachhang", "View_Playout", new { page = page }), null);
            return View(data);
        }
        public ActionResult lstBestSeller(int sort = 0, List<string> checkthuonghieu = null, int checkprice = 0, int page = 1, string nextprev = "")
        {

            ViewBag.listThuongHieu = checkthuonghieu;
            ViewBag.sort = new SelectList(GetSortProduct(), "Value", "Text", sort);
            ViewBag.sortValue = sort;
            ViewBag.checkprice = checkprice;
            ViewBag.gia = GetGia();

            List<ThuongHieuViewModel> thuonghieu = productOperation.GetThuongHieu();
            ViewBag.thuonghieu = thuonghieu;


            CEOStr(Url.Action("lstBestSeller", "View_Playout"), null);
            List<Product_Promotion> lstProduct = productOperation.GetProduct_Best_seller();
            ViewBag.sanphamnoibat = lstProduct.Where(a => a.Sale_Product.IS_NOIBAT == true).OrderByDescending(a => Guid.NewGuid()).Take(5).ToList();
            if (checkthuonghieu != null)
            {
                lstProduct = lstProduct.Where(a => checkthuonghieu.Contains(a.Sale_Product.Origin)).ToList();
            }
            lstProduct = CheckGia(lstProduct, checkprice);
            switch (sort)
            {
                case (int)EnumSortProduct.MD:
                    break;
                case (int)EnumSortProduct.GIACT:
                    lstProduct = SortGia(lstProduct, (int)EnumSortProduct.GIACT);
                    break;
                case (int)EnumSortProduct.GIATC:
                    lstProduct = SortGia(lstProduct, (int)EnumSortProduct.GIATC);
                    break;
                case (int)EnumSortProduct.TENAZ:
                    lstProduct = lstProduct.OrderBy(a => a.Sale_Product.Name).ToList();
                    break;
                case (int)EnumSortProduct.TENZA:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.Name).ToList();
                    break;
                case (int)EnumSortProduct.CU:
                    lstProduct = lstProduct.OrderBy(a => a.Sale_Product.Id).ToList();
                    break;
                case (int)EnumSortProduct.MOI:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.Id).ToList();
                    break;
                case (int)EnumSortProduct.BANCHAY:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.IS_NOIBAT).ToList();
                    break;
            }
            int soluong1trang = 30;
            int soluongsp = lstProduct.Count;
            int sotrang = soluongsp / soluong1trang;
            if ((soluongsp % soluong1trang) > 0)
                sotrang = sotrang + 1;
            ViewBag.sotrang = sotrang;

            if (nextprev == ">")
                page = page + 1;
            if (nextprev == "<")
                page = page - 1;
            if (page < 0) page = 0;
            if (page > sotrang) page = sotrang;

            ViewBag.page = page;
            if (sotrang > 1)
                lstProduct = lstProduct.Skip((page - 1) * soluong1trang).Take(soluong1trang).ToList();
            return View(lstProduct);
        }
        public List<OptionValue> GetSortProduct()
        {
            List<OptionValue> list = new List<OptionValue>();
            list = new List<OptionValue> {
                new OptionValue { Value = "0", Text = "Mặc định" },
                 new OptionValue { Value = "1", Text = "Giá: từ cao đến thấp" },
                  new OptionValue { Value = "2", Text = "Giá: từ thấp đến cao" },
                   new OptionValue { Value = "3", Text = "Tên: A-Z" },
                    new OptionValue { Value = "4", Text = "Tên: Z-A" },
                     new OptionValue { Value = "5", Text = "Cũ nhất" },
                      new OptionValue { Value = "6", Text = "Mới nhất" },
                       new OptionValue { Value = "7", Text = "Bán chạy nhất" }
            };
            return list;
        }
        private List<Product_Promotion> SortGia(List<Product_Promotion> lstProduct, int type)
        {
            List<Product_Promotion> products = new List<Product_Promotion>();

            foreach (var item in lstProduct)
            {
                decimal? gia = item.Sale_Product.PriceOutBound;
                if (item.Sale_CommissionCus != null)
                {
                    if (item.Sale_Commision_Customer.IsMoney == true)
                        gia = item.Sale_Product.PriceOutBound - item.Sale_Commision_Customer.CommissionValue;
                    else
                        gia = item.Sale_Product.PriceOutBound * (1 - (item.Sale_Commision_Customer.CommissionValue / 100));
                }
                item.GiaKM = gia;
                products.Add(item);
            }
            if (type == (int)EnumSortProduct.GIATC)
                products = products.OrderBy(a => a.GiaKM).ToList();
            if (type == (int)EnumSortProduct.GIACT)
                products = products.OrderByDescending(a => a.GiaKM).ToList();

            return products;
        }
        private List<Product_Promotion> CheckGia(List<Product_Promotion> lstProduct, int priceType)
        {
            List<Product_Promotion> products = new List<Product_Promotion>();

            foreach (var item in lstProduct)
            {
                decimal? gia = item.Sale_Product.PriceOutBound;
                if (item.Sale_CommissionCus != null)
                {
                    if (item.Sale_Commision_Customer.IsMoney == true)
                        gia = item.Sale_Product.PriceOutBound - item.Sale_Commision_Customer.CommissionValue;
                    else
                        gia = item.Sale_Product.PriceOutBound * (1 - (item.Sale_Commision_Customer.CommissionValue / 100));
                }
                switch (priceType)
                {
                    case 0:
                        products.Add(item);
                        break;
                    case 1:
                        if (gia < 500000)
                            products.Add(item);
                        break;
                    case 2:
                        if (gia >= 500000 && gia <= 1000000)
                            products.Add(item);
                        break;
                    case 3:
                        if (gia > 1000000)
                            products.Add(item);
                        break;
                }


            }
            return products;
        }
        Dictionary<int, string> GetGia()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();
            lst.Add(0, "Tất cả");
            lst.Add(1, "< 500,000đ");
            lst.Add(2, "500,000đ - 1,000,000đ");
            lst.Add(3, "> 1,000,000đ");
            return lst;
        }

        public ActionResult lstproduct(int? id, string slug, int sort = 0, List<string> checkthuonghieu = null, int checkprice = 0, int page = 1, string nextprev = "")
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            int idNotnull = (int)id;
            //var checkBox = Request.Form["checkthuonghieu"];
            ViewBag.menuid = id;
            ViewBag.listThuongHieu = checkthuonghieu;
            ViewBag.sort = new SelectList(GetSortProduct(), "Value", "Text", sort);
            ViewBag.gia = GetGia();
            ViewBag.sortValue = sort;
            ViewBag.checkprice = checkprice;
            var menuStr = menu.GetNHOMSANPHAM(idNotnull);
            if (menuStr != null)
            {
                ViewBag.TenMenu = menuStr.TEN_NHOMSANPHAM;
            }
            else
                ViewBag.TenMenu = "";



            //begin tao breadcums
            Dictionary<string, string> keyValueMenu = new Dictionary<string, string>();

            if (menuStr != null && (menuStr.CAP_NHOMSANPHAM == 2))
            {
                var menuStr1 = menu.GetNHOMSANPHAM(menuStr.NHOM_CHA.Value);

                string url = Url.Action("lstproduct", "View_Playout", new { id = menuStr1.NHOMSANPHAM_ID });
                keyValueMenu.Add(menuStr1.TEN_NHOMSANPHAM, url);
            }
            ViewBag.Menu = keyValueMenu;

            //if (!string.IsNullOrEmpty(sale_Product.Sale_Product.NHOMSANPHAM_ID_LST))
            //{
            //    var arrID = sale_Product.Sale_Product.NHOMSANPHAM_ID_LST.Split(',').ToList();
            //    int menuID = int.Parse(arrID.FirstOrDefault());
            //    DM_NHOMSANPHAM nhomsp = menu.GetNHOMSANPHAM(menuID);
            //    string url = Url.Action("lstproduct", "View_Playout", new { id = nhomsp.NHOMSANPHAM_ID });
            //    keyValueMenu.Add(nhomsp.TEN_NHOMSANPHAM, url);
            //}

            //end tao breadcums


            List<Danh_MucMenu_SanPham> danhmuc = nhomsanphamOperation.GetDanhMucTheoCapAll(idNotnull);
            List<ThuongHieuViewModel> thuonghieu = productOperation.GetThuongHieu();
            ViewBag.thuonghieu = thuonghieu;
            ViewBag.DanhsachMenu = danhmuc;
            DM_NHOMSANPHAM NSP = nhomsanphamOperation.GetNHOMSANPHAMCEO(id);
            CEOStr(Url.Action("lstproduct", "View_Playout", new { id = id }), null, NSP.META_TITLE, NSP.META_DESCRIPTION);
            List<Product_Promotion> lstProduct = productOperation.GetSale_ProductsPromotion(idNotnull);
            if (id == 284)
            {
                var data = nhomsanphamOperation.GET_vwProduct_Promotion();
                foreach (var item in data)
                {
                    Product_Promotion tmp = new Product_Promotion();

                    tmp = productOperation.GetSale_Product_Promotion2(item.ProductId);
                    lstProduct.Add(tmp);


                }
            }
            ViewBag.sanphamnoibat = lstProduct.Where(a => a.Sale_Product.IS_NOIBAT == true).OrderByDescending(a => Guid.NewGuid()).Take(5).ToList();
            if (checkthuonghieu != null)
            {
                lstProduct = lstProduct.Where(a => checkthuonghieu.Contains(a.Sale_Product.Origin)).ToList();
            }

            string strSKU = "";
            string strCategoty = nhomsanphamOperation.Get_STRCATEGOTY(id);


            if (lstProduct != null)
            {
                foreach (var item in lstProduct)
                {
                    strSKU = strSKU + "\",\"" + item.Sale_Product.Code;
                    //strCategoty = item.Sale_Product.ProductGroup;
                }
                if (strSKU != "")
                {
                    strSKU = strSKU.Substring(3);
                }
            }

            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "ViewCategory";
            ViewBag.ecomm_pagetype_GG = "ViewCategory";
            ViewBag.ecomm_totalvalue = "0";
            ViewBag.ValueFB = "{ content_type: 'product', content_ids: '[\"" + strSKU + "\"]' ,currency: 'VND', content_category: '" + strCategoty + "' }";


            lstProduct = CheckGia(lstProduct, checkprice);
            switch (sort)
            {
                case (int)EnumSortProduct.MD:
                    break;
                case (int)EnumSortProduct.GIACT:
                    lstProduct = SortGia(lstProduct, (int)EnumSortProduct.GIACT);
                    break;
                case (int)EnumSortProduct.GIATC:
                    lstProduct = SortGia(lstProduct, (int)EnumSortProduct.GIATC);
                    break;
                case (int)EnumSortProduct.TENAZ:
                    lstProduct = lstProduct.OrderBy(a => a.Sale_Product.Name).ToList();
                    break;
                case (int)EnumSortProduct.TENZA:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.Name).ToList();
                    break;
                case (int)EnumSortProduct.CU:
                    lstProduct = lstProduct.OrderBy(a => a.Sale_Product.Id).ToList();
                    break;
                case (int)EnumSortProduct.MOI:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.Id).ToList();
                    break;
                case (int)EnumSortProduct.BANCHAY:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.IS_NOIBAT).ToList();
                    break;
            }
            int soluong1trang = 30;
            int soluongsp = lstProduct.Count;
            int sotrang = soluongsp / soluong1trang;
            if ((soluongsp % soluong1trang) > 0)
                sotrang = sotrang + 1;
            ViewBag.sotrang = sotrang;

            if (nextprev == ">")
                page = page + 1;
            if (nextprev == "<")
                page = page - 1;
            if (page < 0) page = 0;
            if (page > sotrang) page = sotrang;

            ViewBag.page = page;
            if (sotrang > 1)
                lstProduct = lstProduct.Skip((page - 1) * soluong1trang).Take(soluong1trang).ToList();

            DM_NHOMSANPHAM SP;
            SP = nhomsanphamOperation.GET_NHOMSAMPHAMID(id);
            if (string.IsNullOrEmpty(slug))
            {
                slug = SP.URL_SLUG;
                return RedirectToRoute("ListSanPhamTheoMenu", new { id = id, slug = slug });
            }
            //GetSale_Product_Promotion
           
           
            return View(lstProduct);
        }

        public ActionResult lstproductloaisanpham(int? id, string slug = "", int sort = 0, List<string> checkthuonghieu = null, int checkprice = 0, int page = 1, string nextprev = "")
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            int idNotNull = (int)id;
            //var checkBox = Request.Form["checkthuonghieu"];
            ViewBag.menuid = id;
            ViewBag.listThuongHieu = checkthuonghieu;
            ViewBag.sort = new SelectList(GetSortProduct(), "Value", "Text", sort);
            ViewBag.sortValue = sort;
            ViewBag.gia = GetGia();
            ViewBag.checkprice = checkprice;

            List<ThuongHieuViewModel> thuonghieu = productOperation.GetThuongHieu();
            ViewBag.thuonghieu = thuonghieu;
            DM_LOAISANPHAM LSP;
            LSP = nhomsanphamOperation.Get_LOAISANPHAM(id);
            CEOStr(Url.Action("lstproductloaisanpham", "View_Playout", new { id = id }), null, LSP.META_TITLE, LSP.META_DESCRIPTION);
            List<Product_Promotion> lstProduct = productOperation.GetSale_ProductsPromotionLoaiSanPham(idNotNull);
            ViewBag.sanphamnoibat = lstProduct.Where(a => a.Sale_Product.IS_NOIBAT == true).OrderByDescending(a => Guid.NewGuid()).Take(5).ToList();
            if (checkthuonghieu != null)
            {
                lstProduct = lstProduct.Where(a => checkthuonghieu.Contains(a.Sale_Product.Origin)).ToList();
            }
            lstProduct = CheckGia(lstProduct, checkprice);




            string strSKU = "";
            string strCatogoty = "Danh muc san pham";
            if (lstProduct != null)
            {
                foreach (var item in lstProduct)
                {
                    strSKU = strSKU + "\",\"" + item.Sale_Product.Code;
                    strCatogoty = item.Sale_Product.ProductGroup;
                }
                if (strSKU != "")
                {
                    strSKU = strSKU.Substring(3);
                }
            }

            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "ViewCategory";
            ViewBag.ecomm_pagetype_GG = "ViewCategory";
            ViewBag.ecomm_totalvalue = "0";

            int pLOAISANPHAM_ID = 0;
            string pIDLOAISANPHAMLIST = "";
            if (lstProduct.Count > 0)
            {
                pIDLOAISANPHAMLIST = lstProduct[0].Sale_Product.LOAISANPHAM_ID_LST;
            }

            if (pIDLOAISANPHAMLIST != "")
            {
                pLOAISANPHAM_ID = int.Parse(pIDLOAISANPHAMLIST.Split(',')[0]);
            }
            strCatogoty = nhomsanphamOperation.Get_STRCATEGOTY_LOAISP(pLOAISANPHAM_ID);
            ViewBag.TenMenu = strCatogoty;

            ViewBag.ValueFB = "{ content_type: 'product', content_ids: '[\"" + strSKU + "\"]' ,currency: 'VND', content_category: '" + strCatogoty + "' }";



            switch (sort)
            {
                case (int)EnumSortProduct.MD:
                    break;
                case (int)EnumSortProduct.GIACT:
                    lstProduct = SortGia(lstProduct, (int)EnumSortProduct.GIACT);
                    break;
                case (int)EnumSortProduct.GIATC:
                    lstProduct = SortGia(lstProduct, (int)EnumSortProduct.GIATC);
                    break;
                case (int)EnumSortProduct.TENAZ:
                    lstProduct = lstProduct.OrderBy(a => a.Sale_Product.Name).ToList();
                    break;
                case (int)EnumSortProduct.TENZA:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.Name).ToList();
                    break;
                case (int)EnumSortProduct.CU:
                    lstProduct = lstProduct.OrderBy(a => a.Sale_Product.Id).ToList();
                    break;
                case (int)EnumSortProduct.MOI:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.Id).ToList();
                    break;
                case (int)EnumSortProduct.BANCHAY:
                    lstProduct = lstProduct.OrderByDescending(a => a.Sale_Product.IS_NOIBAT).ToList();
                    break;
            }
            int soluong1trang = 30;
            int soluongsp = lstProduct.Count;
            int sotrang = soluongsp / soluong1trang;
            if ((soluongsp % soluong1trang) > 0)
                sotrang = sotrang + 1;
            ViewBag.sotrang = sotrang;

            if (nextprev == ">")
                page = page + 1;
            if (nextprev == "<")
                page = page - 1;
            if (page < 0) page = 0;
            if (page > sotrang) page = sotrang;

            ViewBag.page = page;
            if (sotrang > 1)
                lstProduct = lstProduct.Skip((page - 1) * soluong1trang).Take(soluong1trang).ToList();

            DM_LOAISANPHAM SP;
            SP = nhomsanphamOperation.Get_LOAISANPHAM(id);

            if (string.IsNullOrEmpty(slug))
            {
                slug = SP.URL_SLUG;
                return RedirectToRoute("Loaisanpham", new { id = id, slug = slug });
            }
            return View(lstProduct);
        }
        public ActionResult lstAllProduct()
        {
            var list = productOperation.GetSale_Products();
            CEOStr(Url.Action("lstAllProduct", "View_Playout"), null);
            return View(list);
        }

        private Sale_Product ChangeImageName(Sale_Product sale_Product)
        {
            if (sale_Product != null)
            {
                sale_Product.Image_Name = Common.GetUrlImage(sale_Product.Image_Name, "product-image-folder-client", "product");

            }
            return sale_Product;
        }


        public ActionResult productdetail(int? id, string slug)
        {
           
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int productid = (int)id;
            Product_Promotion sale_Product = productOperation.GetSale_Product_Promotion(productid);


            if (sale_Product.Sale_Product == null)
                return RedirectToAction("Index", "Home");
            Dictionary<string, string> keyValueMenu = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(sale_Product.Sale_Product.NHOMSANPHAM_ID_LST))
            {
                var arrID = sale_Product.Sale_Product.NHOMSANPHAM_ID_LST.Split(',').ToList();
                int menuID = int.Parse(arrID.FirstOrDefault());
                DM_NHOMSANPHAM nhomsp = menu.GetNHOMSANPHAM(menuID);
                string url = Url.Action("lstproduct", "View_Playout", new { id = nhomsp.NHOMSANPHAM_ID });
                keyValueMenu.Add(nhomsp.TEN_NHOMSANPHAM, url);
            }
            else
            {
                var loaispID = sale_Product.Sale_Product.LOAISANPHAM_ID;
                if (loaispID != null)
                {
                    int idnotNull = (int)loaispID;
                    DM_LOAISANPHAM loaisp = menu.Get_LOAISANPHAM(idnotNull);
                    string url = Url.Action("lstproductloaisanpham", "View_Playout", new { id = loaisp.LOAISANPHAM_ID });
                    keyValueMenu.Add(loaisp.TEN_LOAISANPHAM, url);
                }
            }
            ViewBag.Menu = keyValueMenu;
            List<Page_ViewProductUser> listview = new List<Page_ViewProductUser>();
            List<Sale_Product> list = Session["ViewProduct"] as List<Sale_Product>;

            List<Product_Promotion> listEx=new List<Product_Promotion>();

            if (list != null)
            {
                foreach (var item in list)
                {
                    if (productOperation.isConHang(item.Id))
                    {
                        productOperation= new ProductOperation();
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
            if (pIDLOAISANPHAMLIST != "")
            {
                pLOAISANPHAM_ID = int.Parse(pIDLOAISANPHAMLIST.Split(',')[0]);
            }


            strCategoty = nhomsanphamOperation.Get_STRCATEGOTY(pLOAISANPHAM_ID);
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

            List<Sale_Product> listproduct = productOperation.GetSanPhamTuongTu(sale_Product.Sale_Product.Id).Take(12).ToList();

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
            List<Product_Size> listsize = new List<Product_Size>();
            listsize.Add(new Product_Size { ID = sale_Product.Sale_Product.Id, Size = sale_Product.Sale_Product.Size });
            listsize.AddRange(productOperation.GetProduct_Sizes(sale_Product.Sale_Product.Id));
            ViewBag.ProductSize = listsize.OrderBy(a => a.Size).ToList();
            CEOStr(Url.Action("productdetail", "View_Playout", new { id = id }), sale_Product.Sale_Product.Image_Name, sale_Product.Sale_Product.META_TITLE, sale_Product.Sale_Product.META_DESCRIPTION);
            ViewBag.SocialNetwork_Product = Common.GetSetting("SocialNetwork_Product");

            // redirect on slug based url here only
            // or you can create action filter so that it works everywhere in your application
            if (string.IsNullOrEmpty(slug))
            {
                slug = sale_Product.Sale_Product.URL_SLUG;
                return RedirectToRoute("Chitietsanpham", new { id = id, slug = slug });
            }

            return View(sale_Product);
        }
        public ActionResult GetSanPham(int id)
        {
            Product_Promotion sale_Product = productOperation.GetSale_Product_Promotion(id);
            return Json(sale_Product, JsonRequestBehavior.AllowGet);
        }
        public ActionResult cart()
        {
            ShoppingCartModels model = new ShoppingCartModels();
            string userID = User.Identity.GetUserId();
            List<Sale_ProductInvoice_View> sale_ProductInvoice_Views = invoiceOperation.GetInvoices(userID);
            thongtinnguoimuaViewModel tmp = new thongtinnguoimuaViewModel();
            tmp.Address = "";
            tmp.CustomerName = "";
            tmp.CustomerPhone = "";
            tmp.District_Id = "";
            tmp.Email = "";
            tmp.Province_Id = "";
            var data = SqlHelper.QuerySP<thongtinnguoimuaViewModel>("THONGTINNGUOIMUAss", new
            {
                USERID = userID
            }).ToList();

            if (data.Count() > 0 && sale_ProductInvoice_Views.Count()>0)
            {
                data[data.Count() - 1].District_Id = sale_ProductInvoice_Views[sale_ProductInvoice_Views.Count() - 1].District_Id;
                data[data.Count() - 1].Province_Id = sale_ProductInvoice_Views[sale_ProductInvoice_Views.Count() - 1].Province_Id;
            }
           
       
            if (Session["Cart"] != null)
                model.Cart = (ShoppingCart)Session["Cart"];
            else
                model.Cart = new ShoppingCart();


            var aa = Session["ShoppingCartItem"];

            ViewBag.QuanHuyen = quanHuyenOperation.GetTinhThanhPho();
            CEOStr(Url.Action("cart", "View_Playout"), null);
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
            ViewBag.userid = userID;


            if (data.Count() > 0 && sale_ProductInvoice_Views.Count()>0)
            {
                model.thongtinngmua = data[data.Count() - 1];
            }
            else
            {
                model.thongtinngmua = tmp;
            }
            
            return View(model);
        }
        public ActionResult cartTablePartial()
        {
            ShoppingCartModels model = new ShoppingCartModels();
            if (Session["Cart"] != null)
                model.Cart = (ShoppingCart)Session["Cart"];
            else
                model.Cart = new ShoppingCart();
            return PartialView(model);
        }
        public ActionResult GetQuanHuyen(string id)
        {
            List<System_Loc_District> quanhuyen = quanHuyenOperation.GetQuanHuyen(id);
            return Json(quanhuyen, JsonRequestBehavior.AllowGet);
        }
        public ActionResult cartInfo()
        {
            if (Session["thongbaoCart"] == null)
                return RedirectToAction("Index", "Home");
            CartMessage cartMessage = (CartMessage)Session["thongbaoCart"];
            CEOStr(Url.Action("cartInfo", "View_Playout"), null);
            ViewBag.FacebookPixelID = Common.GetSetting("FacebookPixelID");
            ViewBag.IDGoogleconversion = Common.GetSetting("IDGoogleconversion");
            ViewBag.chatbox = Common.GetSetting_Script("chatbot");
            ViewBag.Google_GTM = Common.GetSetting("Google_GTM");
            ViewBag.Google_TrackingID = Common.GetSetting("Google_TrackingID");
            ViewBag.ecomm_prodid = cartMessage.StrSKU;
            ViewBag.ecomm_pagetype_FB = "Purchase";
            ViewBag.ecomm_pagetype_GG = "conversion";
            ViewBag.ecomm_totalvalue = cartMessage.Total_value;
            ViewBag.ValueFB = "{ content_type: 'Purchase', content_ids: '[\"" + cartMessage.StrSKU + "\"]' , value: '" + cartMessage.Total_value + "', num_items: '" + cartMessage.CountItem + "',  currency: 'VND' }";
            return View(cartMessage);
        }
        public ActionResult thongtinthanhtoan()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("Index", "Home");
            ViewBag.QuanHuyen = quanHuyenOperation.GetTinhThanhPho();
            return View();
        }
        public ActionResult lstNew()
        {
            var listmenu = apiCategoryOperation.GetCategories();
            ViewBag.Menu = listmenu;
            List<Page_New> list = apiPageNewOperation.GetPage_News();

            CEOStr(Url.Action("lstNew", "View_Playout"), null);
            return View(list);
        }
        public ActionResult lstNewwithCategory(string id)
        {
            var listmenu = apiCategoryOperation.GetCategories();
            ViewBag.Menu = listmenu;
            List<Page_New> list = apiPageNewOperation.GetNewWithCategory(id);

            CEOStr(Url.Action("lstNewwithCategory", "View_Playout"), null);
            return View(list);
        }
        public ActionResult newdetail(int id)
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiViet(id);

            CEOStr(Url.Action("newdetail", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }

        public ActionResult login()
        {
            CEOStr(Url.Action("login", "View_Playout"), null);
            return View();
        }
        public ActionResult register()
        {
            CEOStr(Url.Action("register", "View_Playout"), null);
            return View();
        }
        public ActionResult tintuc(string slug)
        {
            ViewBag.DanhsachNhomTin = tinTucOperation.GetNhomTinTuc();
            ViewBag.DanhSachTinTucMoiNhat = tinTucOperation.GetDanhSachTinTucNoiBatMoiNhat(4);
            CEOStr(Url.Action("tintuc", "View_Playout"), null);


            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "Newslist";
            ViewBag.ecomm_pagetype_GG = "Newslist";
            ViewBag.ecomm_totalvalue = "0";

            return View();
        }
        public ActionResult tintucchitiet(int id, string slug)
        {
            DM_TINTUC tintuc = tinTucOperation.GetTintuc(id);
            if (tintuc == null)
                return RedirectToAction("Index", "Home");
            DM_NHOMTIN nhomtin = tinTucOperation.GetNhomtin(tintuc.NHOMTIN_ID);
            if (nhomtin != null)
                ViewBag.Menu = nhomtin.TEN_LOAISANPHAM;
            else
                ViewBag.Menu = "";
            CEOStr(Url.Action("tintucchitiet", "View_Playout", new { id = id }), tintuc.ANH_DAIDIEN, tintuc.META_TITLE, tintuc.META_DESCRIPTION);
            ViewBag.DanhsachNhomTin = tinTucOperation.GetNhomTinTuc();
            ViewBag.SocialNetwork_News = Common.GetSetting("SocialNetwork_News");

            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "ViewContent";
            ViewBag.ecomm_pagetype_GG = "ViewContent";
            ViewBag.ecomm_totalvalue = "0";


            //begin tao breackscum
            Dictionary<string, string> keyValueMenu = new Dictionary<string, string>();
            string url = Url.Action("nhomtintuc", "View_Playout", new { id = nhomtin.NHOMTIN_ID });
            keyValueMenu.Add(nhomtin.TEN_LOAISANPHAM, url);
            ViewBag.Menu1 = keyValueMenu;
            //end tao breakcsum



            if (string.IsNullOrEmpty(slug))
            {
                DM_TINTUC TT = tinTucOperation.GetTintuc(id);
                slug = TT.URL_SLUG;
                return RedirectToRoute("tintucchitiet", new { id = id, slug = slug });
            }
            return View(tintuc);
        }
        public ActionResult nhomtintuc(int? id, string slug, int page = 1)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            int idtt = (int)id;
            DM_NHOMTIN tintuc = tinTucOperation.GetNhomtin(idtt);
            if (tintuc == null)
                return RedirectToAction("Index", "Home");
            ViewBag.Menu = tintuc.TEN_LOAISANPHAM;
            List<DM_TINTUC> results = tinTucOperation.GetDanhSachTinTuc(idtt);
            CEOStr(Url.Action("nhomtintuc", "View_Playout", new { id = id }), tintuc.ANH_DAIDIEN, tintuc.META_TITLE, tintuc.META_DESCRIPTION);
            int soluong = 20;
            int soluongpage = results.Count / soluong;
            if (results.Count % soluong > 0)
                soluongpage = soluongpage + 1;
            results = results.Skip((page - 1) * soluong).Take(soluong).ToList();

            ViewBag.SoluongPage = soluongpage;
            ViewBag.Nhomtin = tintuc.NHOMTIN_ID;
            ViewBag.page = page;
            ViewBag.DanhsachNhomTin = tinTucOperation.GetNhomTinTuc();
            //begin tao breackscum
            Dictionary<string, string> keyValueMenu = new Dictionary<string, string>();
            string url = Url.Action("tintuc", "View_Playout", "");
            keyValueMenu.Add("Tin tức", url);
            ViewBag.Menu = keyValueMenu;
            //end tao breakcsum

            ViewBag.TenMenu = tintuc.TEN_LOAISANPHAM;

            DM_NHOMTIN NT;
            NT = tinTucOperation.GetNhomtin(id);
            if (string.IsNullOrEmpty(slug))
            {
                slug = NT.URL_SLUG;
                return RedirectToRoute("nhomtintuc", new { slug = slug });
            }
            return View(results);
        }

        public ActionResult gioithieuyhl()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("VEYHL");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("gioithieuyhl", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }
        public ActionResult chinhsachbaomat()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("CHINHSACH_BAOMAT");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("chinhsachbaomat", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }


        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.CreatedDate = DateTime.Now;
            feedback.Phone = mobile;
            feedback.Content = content;
            feedback.Address = address;

            var id = feedBackOperation.InsertFeedBack(feedback);
            if (id > 0)
            {
                bool bsend = Common.SendEmail(email, name + "-Phone: " + mobile + "-Address: " + address, "Nội dung yêu cầu: " + content);
                return Json(new
                {
                    status = true
                });

            }

            else
                return Json(new
                {
                    status = false
                });

        }

        public ActionResult lienhe()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("LIENHE");
            baiviet.lat = Common.GetSetting("LAT");
            baiviet.lng = Common.GetSetting("LNG");
            ViewBag.GoogleMapAPIkey = Common.GetSetting("GoogleMapAPIkey");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("lienhe", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }
        public ActionResult vanhoa()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("VANHOA_YHL");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("vanhoa", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }

        public ActionResult phuongthucdathang()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("PHUONG_THUCDATHAN");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("phuongthucdathang", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }
        public ActionResult chinhsachvanchuyen()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("CHINHSACH_VANCHUYEN");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("chinhsachvanchuyen", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }
        public ActionResult chinhsachdoitra()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("CHINHSACH_DOITRA");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("chinhsachdoitra", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }
        public ActionResult dieukhoanmientru()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("DIEUKHOAN_MIENTRU");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("dieukhoanmientru", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }
        public ActionResult tuyendung()
        {
            DM_BAIVE_YHL baiviet = baiVietOperation.GetBaiVietCode("TUYENDUNG_YHL");
            if (baiviet == null)
                return RedirectToAction("Index", "Home");
            CEOStr(Url.Action("tuyendung", "View_Playout"), baiviet.HINHANH);
            return View(baiviet);
        }

        public ActionResult buyinfo()
        {
            string userID = User.Identity.GetUserId();
            var data = SqlHelper.QuerySP<thongtinnguoimuaViewModel>("THONGTINNGUOIMUAss", new
            {
                USERID = userID
            }).ToList();

            CEOStr(Url.Action("buyinfo", "View_Playout"), null);

            if (!string.IsNullOrEmpty(userID))
            {

                InfomationUser infomationUser = new InfomationUser();
                CustomerInfo infouser = new CustomerInfo();
                List<Sale_ProductInvoice_View> sale_ProductInvoice_Views = invoiceOperation.GetInvoices(userID);
                List<Sale_Product> sale_Products = apiProductOperation.GetPage_ViewProductUsers(User.Identity.GetUserId());
                CustomerInfo customerInfo = userAPI.GetCustomerInfo(userID);
                infomationUser.CustomerInfo = customerInfo;

                thongtinnguoimuaViewModel tmp = new thongtinnguoimuaViewModel();


                if (customerInfo.Address != null)
                {
                    tmp.Address = customerInfo.Address;
                }
                else
                {
                    tmp.Address = "";
                }
                if (customerInfo.LastName != null && customerInfo.Firstname != null)
                {
                    tmp.CustomerName = customerInfo.LastName + "" + customerInfo.Firstname;
                }
                else if (customerInfo.LastName != null && customerInfo.Firstname == null)
                {
                    tmp.CustomerName = customerInfo.LastName;
                }
                else if (customerInfo.LastName == null && customerInfo.Firstname != null)
                {
                    tmp.CustomerName = customerInfo.Firstname;
                }
                else
                {
                    tmp.CustomerName = " ";
                }
                if (customerInfo.PhoneNumber != null)
                {
                    tmp.CustomerPhone = customerInfo.PhoneNumber;
                }
                else
                {
                    tmp.CustomerPhone = " ";
                }
                if (customerInfo.Email != null)
                {
                    tmp.Email = customerInfo.Email;
                }
                else
                {
                    tmp.Email = " ";
                }
                tmp.District_Id = "";

                tmp.Province_Id = "";

                if (data.Count() > 0)
                {
                    infomationUser.thongtinmuavm = data[data.Count() - 1];
                }
                else
                {
                    infomationUser.thongtinmuavm = tmp;

                }
                infomationUser.Sale_ProductInvoice_Views = sale_ProductInvoice_Views;
                infomationUser.Page_ViewProductUsers = sale_Products;
                return View(infomationUser);
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public ActionResult buyinfoDetailPartial(int invoiceID)
        {
            string userID = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userID))
            {
                var data = invoiceOperation.GetSale_ProductInvoice(userID, invoiceID);
                return PartialView(data);
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public ActionResult buyinfoDetailPartialcanhan(int invoiceID)
        {
            string userID = User.Identity.GetUserId();
            var data = SqlHelper.QuerySP<thongtinnguoimuaViewModel>("THONGTINNGUOIMUAs", new
            {
                USERID = userID
            }).ToList();
            if (!string.IsNullOrEmpty(userID))
            {
                var datas = invoiceOperation.GetSale_ProductInvoice(userID, invoiceID);
                return PartialView(data);
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        #endregion

        #region Comment in Product Detail

        public ActionResult commentProductPartial(string id)
        {
            ViewBag.ProductID = id;
            return PartialView();
        }
        public ActionResult showCommentPartial(int id)
        {
            List<Page_Comment> listConment = new List<Page_Comment>();
            listConment = apiProductOperation.GetPage_Comments(id).ToList();

            return PartialView(listConment);
        }
        [HttpPost]
        public ActionResult InsertComment(string text, int? productID)
        {

            if (!string.IsNullOrEmpty(User.Identity.GetUserId()))
            {
                Page_Comment page_Comment = new Page_Comment();
                page_Comment.UserID = User.Identity.GetUserId();
                page_Comment.UserName = User.Identity.GetUserName();
                page_Comment.ProductID = productID;
                page_Comment.Content = text;
                bool result = apiProductOperation.InsertComment(page_Comment);
                return Json(result);
            }
            else
            {
                return Json("Xin vui lòng đăng nhập trước khi bình luận sản phẩm");
            }


        }
        #endregion
    }
}