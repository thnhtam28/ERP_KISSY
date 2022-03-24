using ERP_API.EnumData;
using ERP_API.Filters;
using ERP_API.Models;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using ERP_API.Operation.Nomal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{
    public class GroupDetailsController : Controller
    {
        // GET: GroupDetails

        TinTucOperation tinTucOperation;
        ProductOperation productOperation;
        DM_NHOMSANPHAMOperation menu;
        ApiPageSetupOperation apiPageSetupOperation;
        DM_NHOMSANPHAMOperation nhomsanphamOperation;
        KhuyenMaiOperation khuyenMaiOperation;
        public GroupDetailsController()
        {

            tinTucOperation = new TinTucOperation();
            productOperation = new ProductOperation();
            menu = new DM_NHOMSANPHAMOperation();
            apiPageSetupOperation = new ApiPageSetupOperation();
            nhomsanphamOperation = new DM_NHOMSANPHAMOperation();
            khuyenMaiOperation = new KhuyenMaiOperation();
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
        Dictionary<int, string> GetGia()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();
            lst.Add(1, "Dưới 100");
            lst.Add(2, "Từ 100k-200k");
            lst.Add(3, "Từ 200k-300k");
            lst.Add(4, "Từ 300k-400k");
            lst.Add(5, "Trên 400k");
            return lst;
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
                decimal? gia = item.GiaKM;
                switch (priceType)
                {
                    case 0:
                        products.Add(item);
                        break;
                    case 1:
                        if (gia < 100000)
                            products.Add(item);
                        break;
                    case 2:
                        if (gia >= 100000 && gia <= 200000)
                            products.Add(item);
                        break;
                    case 3:
                        if (gia >= 200000 && gia <= 300000)
                            products.Add(item);
                        break;
                    case 4:
                        if (gia >= 300000 && gia <= 400000)
                            products.Add(item);
                        break;
                    case 5:
                        if (gia >= 400000)
                            products.Add(item);
                        break;
                }


            }
            return products;
        }
        private List<Product_Promotion> CheckSanPhamNhom(List<Product_Promotion> lstProduct, int nhomid)
        {
            List<Product_Promotion> products = new List<Product_Promotion>();
            foreach (var item in lstProduct)
            {
               List<int> nhomcha= productOperation.GetNhomCha(item.Sale_Product);
                if (nhomcha.Contains(nhomid))
                {
                    products.Add(item);
                }
            }
           
            return products;
        }
        public ActionResult Search(string search = "", int checkprice = 0, List<int> checkMenus = null, int page = 1, string nextprev = "")
        {
            if (checkMenus == null)
            {
                checkMenus = new List<int>();
            }
            ViewBag.searchstr = search;
            ViewBag.checkprice = checkprice;
            ViewBag.checkMenus = checkMenus;
            ViewBag.gia = GetGia();
            CEOStr(Url.Action("lstBestSeller", "GroupDetails"), null);
            List<Product_Promotion> lstProduct_tam = new List<Product_Promotion>();
            if (string.IsNullOrEmpty(search))
            {
                for (int i = 0; i < checkMenus.Count; i++)
                {
                    lstProduct_tam.AddRange(productOperation.GetSale_ProductsPromotion(checkMenus[i]));
                }

            }
            else
            {
                lstProduct_tam = productOperation.GetSearchProduct(search);
            }

            //begin hoapd su ly trung san pham
            bool mbChecktrung = false;
            List<Product_Promotion> lstProduct = new List<Product_Promotion>();
            foreach (var item in lstProduct_tam)
            {
                if (lstProduct.Count() > 0)
                {
                    mbChecktrung = false;
                    foreach (var item1 in lstProduct)
                    {
                        if (item1.Sale_Product.Id== item.Sale_Product.Id)
                        {
                            mbChecktrung = true;
                        }
                    }
                    
                    if (mbChecktrung == false)
                    {
                        lstProduct.Add(item);
                    }

                }
                else
                {
                    lstProduct.Add(item);
                }

            }
            //end hoapd su ly trung san pham


            lstProduct = CheckGia(lstProduct, checkprice);
            //if(checkMenus!=null)
            //    lstProduct = CheckSanPhamNhom(lstProduct, checkMenus);
            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "Search";
            ViewBag.ecomm_pagetype_GG = "searchresults";


            ViewBag.ecomm_totalvalue = "0";
            var danhsachmenu = menu.GetTatCaDanhMuc();
            if (checkMenus != null)
            {
                foreach (var mn in danhsachmenu)
                {
                    if (checkMenus.Contains(mn.MenuCap1.NHOMSANPHAM_ID))
                        mn.Checked = true;
                    else
                        mn.Checked = false;
                }
            }
            ViewBag.DanhSachMenu = danhsachmenu;


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
        public ActionResult lstproduct(int? id, string slug, int checkprice = 0, List<int> checkMenus = null, int page = 1, string nextprev = "")
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            DM_NHOMSANPHAM SP;
            SP = nhomsanphamOperation.GET_NHOMSAMPHAMID(id);
            if (string.IsNullOrEmpty(slug))
            {
                slug = SP.URL_SLUG;
                return RedirectToAction("lstproduct", "GroupDetails", new { id = id, slug = slug, checkprice = checkprice });
            }
            int idNotnull = (int)id;
            ViewBag.checkprice = checkprice;
            ViewBag.checkMenus = checkMenus;
            ViewBag.menuid = id;
            ViewBag.slug = slug;
            ViewBag.gia = GetGia();
            var menuStr = menu.GetNHOMSANPHAM(idNotnull);
            if (menuStr != null)
            {
                ViewBag.TenMenu = menuStr.TEN_NHOMSANPHAM;
            }
            else
                ViewBag.TenMenu = "";
            Dictionary<string, string> keyValueMenu = new Dictionary<string, string>();

            if (menuStr != null && (menuStr.CAP_NHOMSANPHAM == 2))
            {
                var menuStr1 = menu.GetNHOMSANPHAM(menuStr.NHOM_CHA.Value);

                string url = Url.Action("lstproduct", "GroupDetails", new { id = menuStr1.NHOMSANPHAM_ID });
                keyValueMenu.Add(menuStr1.TEN_NHOMSANPHAM, url);
            }
            ViewBag.Menu = keyValueMenu;

            DM_NHOMSANPHAM NSP = nhomsanphamOperation.GetNHOMSANPHAMCEO(id);
            CEOStr(Url.Action("lstproduct", "GroupDetails", new { id = id }), null, NSP.META_TITLE, NSP.META_DESCRIPTION);
            List<Product_Promotion> lstProduct = productOperation.GetSale_ProductsPromotion(idNotnull).Where(x => x.Sale_Product.is_display == true).ToList();

            string strSKU = "";
            string strCategoty = nhomsanphamOperation.Get_STRCATEGOTY(id);


            if (lstProduct != null)
            {
                foreach (var item in lstProduct)
                {
                    strSKU = strSKU + "\",\"" + item.Sale_Product.Code;
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

            var danhsachmenu = menu.GetTatCaDanhMuc();
            if (checkMenus != null)
            {
                foreach (var mn in danhsachmenu)
                {
                    if (checkMenus.Contains(mn.MenuCap1.NHOMSANPHAM_ID))
                        mn.Checked = true;
                    else
                        mn.Checked = false;
                }
            }
            ViewBag.DanhSachMenu = danhsachmenu;

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


            //GetSale_Product_Promotion


            return View(lstProduct);
        }
        public ActionResult khuyenmai(int? menuid, int checkprice = 0, List<int> checkMenus = null, int page = 1, string nextprev = "")
        {
            ViewBag.checkprice = checkprice;
            ViewBag.checkMenus = checkMenus;
            ViewBag.gia = GetGia();
            CEOStr(Url.Action("khuyenmai", "GroupDetails"), null);
            List<Product_Promotion> lstProduct = khuyenMaiOperation.GetKhuyenMaiKissy();

     
            if (checkMenus != null)
            {
                List<Product_Promotion> lstProductMU = new List<Product_Promotion>();
                foreach (int mid in checkMenus)
                {
                    lstProductMU.AddRange( CheckSanPhamNhom(lstProduct, mid));
                }
                lstProduct = lstProductMU;
            }
            lstProduct = CheckGia(lstProduct, checkprice);
            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "Search";
            ViewBag.ecomm_pagetype_GG = "searchresults";


            ViewBag.ecomm_totalvalue = "0";
            var danhsachmenu = menu.GetTatCaDanhMuc();
            if (checkMenus != null)
            {
                foreach (var mn in danhsachmenu)
                {
                    if (checkMenus.Contains(mn.MenuCap1.NHOMSANPHAM_ID))
                        mn.Checked = true;
                    else
                        mn.Checked = false;
                }
            }
            ViewBag.DanhSachMenu = danhsachmenu;


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
    }
}