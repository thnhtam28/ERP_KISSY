using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using System.Web.Script.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using Erp.Domain.Entities;
using Excels = Microsoft.Office.Interop.Excel;
using ImportExeclModel = Erp.BackOffice.Sale.Models.ImportExeclModel;
using Erp.Domain.Sale.Repositories;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using Erp.Domain.Sale;
using Excel;
using System.Data;
using Erp.BackOffice.Areas.Sale.Models;
using Erp.Domain.Repositories;
using Erp.BackOffice.Account.Controllers;
using Erp.BackOffice.Account.Models;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]


    public class ProductController : Controller
    {
        private readonly IProductOrServiceRepository productRepository;
        private readonly IObjectAttributeRepository ObjectAttributeRepository;
        private readonly ISupplierRepository SupplierRepository;
        private readonly IUserRepository userRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IDM_NHOMSANPHAMRepositories dm_nhomsanphamRepository;
        private readonly IDM_BEST_SELLERRepositories dm_bestsellerRepository;
        private readonly IDM_LOAISANPHAMRepositories dm_loaisanphamRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly IProductInboundRepository ProductInboundRepository;
        private readonly IWarehouseRepository WarehouseRepository;
        private readonly IProductOutboundRepository productOutboundRepository;

        public ProductController(
            IProductOrServiceRepository _Product
            , IObjectAttributeRepository _ObjectAttribute
            , ISupplierRepository _Supplier
            , IUserRepository _user
            , ICategoryRepository category
            , IDM_NHOMSANPHAMRepositories dm_nhomsanpham
            , IDM_BEST_SELLERRepositories dm_bestseller
            , IDM_LOAISANPHAMRepositories dm_loaisanpham
             , ITemplatePrintRepository templatePrint
            , IProductInboundRepository _ProductInbound
             , IWarehouseRepository _Warehouse
            , IProductOutboundRepository _ProductOutbound
            )
        {
            productRepository = _Product;
            ObjectAttributeRepository = _ObjectAttribute;
            SupplierRepository = _Supplier;
            userRepository = _user;
            categoryRepository = category;
            dm_nhomsanphamRepository = dm_nhomsanpham;
            dm_bestsellerRepository = dm_bestseller;
            dm_loaisanphamRepository = dm_loaisanpham;
            templatePrintRepository = templatePrint;
            ProductInboundRepository = _ProductInbound;
            WarehouseRepository = _Warehouse;
            productOutboundRepository = _ProductOutbound;
        }

        #region Index



        public ViewResult Index(string txtSearch, string txtCode, string CategoryCode, int? NHOMSANPHAM_ID, int? BranchId, string Status, int? AmountPage, string ProductGroup, SearchObjectAttributeViewModel SearchOjectAttr)
        {
            //get cookie brachID 
            HttpRequestBase request = this.HttpContext.Request;
            string strBrandID = "0";
            string pAmountPage = "45";
            if (request.Cookies["BRANCH_ID_CookieName"] != null)
            {
                strBrandID = request.Cookies["BRANCH_ID_CookieName"].Value;
                if (strBrandID == "")
                {
                    strBrandID = "0";
                }
            }


            //begin get cookie numberow
            if (request.Cookies["NUMBERROW_INVOICE_CookieName"] != null)
            {
                pAmountPage = request.Cookies["NUMBERROW_INVOICE_CookieName"].Value;
                if (pAmountPage == "")
                {
                    pAmountPage = "45";
                }
            }

            if (pAmountPage != null)
            {
                ViewBag.AmountPage = int.Parse(pAmountPage);

            }
            else

            {
                ViewBag.pAmountPage = 45;
            }

            //end get cookie numberow
            var nhomSP = new DM_NHOMSANPHAM();

            IEnumerable<ProductViewModel> q = productRepository.GetAllvwProduct().AsEnumerable()
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    IsDeleted = item.IsDeleted,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    PriceOutbound = item.PriceOutbound,
                    Barcode = item.Barcode,
                    Type = item.Type,
                    Unit = item.Unit,
                    CategoryCode = item.CategoryCode,
                    DiscountStaff = item.DiscountStaff,
                    IsMoneyDiscount = item.IsMoneyDiscount,
                    NHOMSANPHAM_ID = item.NHOMSANPHAM_ID,
                    //NHOMSANPHAM_ID_LST =  item.NHOMSANPHAM_ID_LST,
                    ProductGroupName = item.ProductGroupName,
                    Color_product = item.color,
                    Size = item.Size,
                    NhomCha = item.NhomCha,
                    Image_Pos = item.Image_Pos

                }).OrderByDescending(m => m.Id).ToList();

            //var group = q.GroupBy(x => x.CategoryCode).ToList();
            //int dem = 1;
            //foreach (var item in group)
            //{
            //    Category category = new Category();
            //    category.IsDeleted = false;
            //    category.CreatedUserId = WebSecurity.CurrentUserId;
            //    category.ModifiedUserId = WebSecurity.CurrentUserId;
            //    category.CreatedDate = DateTime.Now;
            //    category.ModifiedDate = DateTime.Now;
            //    category.Value = item.Key;
            //    category.Name = item.Key;
            //    category.Code = "product";
            //    category.OrderNo = dem;
            //    categoryRepository.InsertCategory(category);
            //    dem++;
            //}
            //nếu có tìm kiếm nâng cao thì lọc trước

            //if (string.IsNullOrEmpty(txtSearch) == false || string.IsNullOrEmpty(txtCode) == false)
            //{
            //    txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);
            //    txtCode = txtCode == "" ? "~" : txtCode.ToLower();
            //    q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLower().Contains(txtCode));
            //}

            if (string.IsNullOrEmpty(txtSearch) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch).Trim();
                //txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLower().Contains(txtSearch));
            }
            if (NHOMSANPHAM_ID > 0)
            {
                var AllNhom = productRepository.GetAllvwProduct().Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                if (AllNhom.Any())
                {
                    q = q.Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                }
                else
                {
                    q = q.Where(x => x.NHOMSANPHAM_ID == NHOMSANPHAM_ID).ToList();
                }






            }

            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }
            //if (LOAISANPHAM_ID != null && LOAISANPHAM_ID > 0)
            //{
            //    q = q.Where(x => x.LOAISANPHAM_ID == LOAISANPHAM_ID).ToList();
            //}
            //decimal minPriceInbound;
            //if (decimal.TryParse(txtMinPrice, out minPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound >= minPriceInbound);
            //}

            //decimal maxPriceInbound;
            //if (decimal.TryParse(txtMaxPrice, out maxPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound <= maxPriceInbound);
            //}


            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            ViewBag.Tongdong = q.Count();
            return View(q);
        }




        public ViewResult IndexSearch(string txtSearch, string txtCode, string CategoryCode, string ProductGroup, int? pruductid, int? NHOMSANPHAM_ID, SearchObjectAttributeViewModel SearchOjectAttr)
        {
            var nhomSP = new DM_NHOMSANPHAM();



            IEnumerable<vwProductsamegroup> qsamegroup = productRepository.GetAllvwProductsamegroup().AsEnumerable().Where(item => item.Product_id == pruductid)
               .Select(item => new vwProductsamegroup
               {
                   Product_id = item.Product_idsame,
               }).ToList();

            IEnumerable<ProductViewModel> q = productRepository.GetAllvwProduct1().AsEnumerable().
                Where(x => !qsamegroup.Any(item => item.Product_id == x.Id)).Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    PriceOutbound = item.PriceOutbound,
                    Barcode = item.Barcode,
                    Type = item.Type,
                    Unit = item.Unit,
                    CategoryCode = item.CategoryCode,
                    DiscountStaff = item.DiscountStaff,
                    IsMoneyDiscount = item.IsMoneyDiscount,
                    NHOMSANPHAM_ID = item.NHOMSANPHAM_ID,
                    Image_Pos = item.Image_Pos,
                    TEN_NHOMSANPHAM = item.ProductGroupName,
                    NhomCha = item.NhomCha
                }).OrderByDescending(m => m.Id).ToList();

            //var group = q.GroupBy(x => x.CategoryCode).ToList();
            //int dem = 1;
            //foreach (var item in group)
            //{
            //    Category category = new Category();
            //    category.IsDeleted = false;
            //    category.CreatedUserId = WebSecurity.CurrentUserId;
            //    category.ModifiedUserId = WebSecurity.CurrentUserId;
            //    category.CreatedDate = DateTime.Now;
            //    category.ModifiedDate = DateTime.Now;
            //    category.Value = item.Key;
            //    category.Name = item.Key;
            //    category.Code = "product";
            //    category.OrderNo = dem;
            //    categoryRepository.InsertCategory(category);
            //    dem++;
            //}
            //nếu có tìm kiếm nâng cao thì lọc trước
            if (SearchOjectAttr.ListField != null)
            {
                if (SearchOjectAttr.ListField.Count > 0)
                {
                    //lấy các đối tượng ObjectAttributeValue nào thỏa đk có AttributeId trong ListField và có giá trị tương ứng trong ListField
                    var listObjectAttrValue = ObjectAttributeRepository.GetAllObjectAttributeValue().AsEnumerable().Where(attr => SearchOjectAttr.ListField.Any(item => item.Id == attr.AttributeId && Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(attr.Value).Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Value)))).ToList();

                    //tiếp theo tìm các sản phẩm có id bằng với ObjectId trong listObjectAttrValue vừa tìm được
                    q = q.Where(product => listObjectAttrValue.Any(item => item.ObjectId == product.Id));

                    ViewBag.ListOjectAttrSearch = new JavaScriptSerializer().Serialize(SearchOjectAttr.ListField.Select(x => new { Id = x.Id, Value = x.Value }));
                }
            }

            if (NHOMSANPHAM_ID > 0)
            {

                var AllNhom = productRepository.GetAllvwProduct().Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                if (AllNhom.Any())
                {
                    q = q.Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                }
                else
                {
                    q = q.Where(x => x.NHOMSANPHAM_ID == NHOMSANPHAM_ID).ToList();
                }


            }


            if (string.IsNullOrEmpty(txtSearch) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch).Trim();
                //txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLower().Contains(txtSearch));
            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }

            //decimal minPriceInbound;
            //if (decimal.TryParse(txtMinPrice, out minPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound >= minPriceInbound);
            //}

            //decimal maxPriceInbound;
            //if (decimal.TryParse(txtMaxPrice, out maxPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound <= maxPriceInbound);
            //}

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }


        public ViewResult IndexSearch2(string txtSearch, string txtCode, string CategoryCode, string ProductGroup, int? pruductid, SearchObjectAttributeViewModel SearchOjectAttr)
        {


            IEnumerable<vwProductsamesize> qsamesize = productRepository.GetAllvwProductsamesize().AsEnumerable().Where(item => item.Product_id == pruductid)
             .Select(item => new vwProductsamesize
             {
                 Product_id = item.Product_idsame,
             }).ToList();
            var nhomSP = new DM_NHOMSANPHAM();
            IEnumerable<ProductViewModel> q = productRepository.GetAllvwProduct2().AsEnumerable().
                 Where(x => !qsamesize.Any(item => item.Product_id == x.Id))
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    PriceOutbound = item.PriceOutbound,
                    Barcode = item.Barcode,
                    Type = item.Type,
                    Unit = item.Unit,
                    CategoryCode = item.CategoryCode,
                    DiscountStaff = item.DiscountStaff,
                    IsMoneyDiscount = item.IsMoneyDiscount
                }).OrderByDescending(m => m.Id);

            //var group = q.GroupBy(x => x.CategoryCode).ToList();
            //int dem = 1;
            //foreach (var item in group)
            //{
            //    Category category = new Category();
            //    category.IsDeleted = false;
            //    category.CreatedUserId = WebSecurity.CurrentUserId;
            //    category.ModifiedUserId = WebSecurity.CurrentUserId;
            //    category.CreatedDate = DateTime.Now;
            //    category.ModifiedDate = DateTime.Now;
            //    category.Value = item.Key;
            //    category.Name = item.Key;
            //    category.Code = "product";
            //    category.OrderNo = dem;
            //    categoryRepository.InsertCategory(category);
            //    dem++;
            //}
            //nếu có tìm kiếm nâng cao thì lọc trước
            if (SearchOjectAttr.ListField != null)
            {
                if (SearchOjectAttr.ListField.Count > 0)
                {
                    //lấy các đối tượng ObjectAttributeValue nào thỏa đk có AttributeId trong ListField và có giá trị tương ứng trong ListField
                    var listObjectAttrValue = ObjectAttributeRepository.GetAllObjectAttributeValue().AsEnumerable().Where(attr => SearchOjectAttr.ListField.Any(item => item.Id == attr.AttributeId && Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(attr.Value).Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Value)))).ToList();

                    //tiếp theo tìm các sản phẩm có id bằng với ObjectId trong listObjectAttrValue vừa tìm được
                    q = q.Where(product => listObjectAttrValue.Any(item => item.ObjectId == product.Id));

                    ViewBag.ListOjectAttrSearch = new JavaScriptSerializer().Serialize(SearchOjectAttr.ListField.Select(x => new { Id = x.Id, Value = x.Value }));
                }
            }

            if (string.IsNullOrEmpty(txtSearch) == false || string.IsNullOrEmpty(txtCode) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLower().Contains(txtCode));
            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }
            //decimal minPriceInbound;
            //if (decimal.TryParse(txtMinPrice, out minPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound >= minPriceInbound);
            //}

            //decimal maxPriceInbound;
            //if (decimal.TryParse(txtMaxPrice, out maxPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound <= maxPriceInbound);
            //}


            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }

        public ViewResult IndexSearch3(string txtSearch, string txtCode, string CategoryCode, string ProductGroup, int? NHOMSANPHAM_ID, SearchObjectAttributeViewModel SearchOjectAttr)
        {
            //IEnumerable<ProductViewModel> qsamesize = productRepository.GetAllProduct().AsEnumerable().Where(item => item.Id == pruductid)
            // .Select(item => new ProductViewModel
            // {
            //     Id = item.Id,
            // }).ToList();
            var nhomSP = new DM_NHOMSANPHAM();
            IEnumerable<ProductViewModel> q = productRepository.GetAllvwProduct().Where(m => m.IS_NEW != true).AsEnumerable()
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    PriceOutbound = item.PriceOutbound,
                    Barcode = item.Barcode,
                    Type = item.Type,
                    Unit = item.Unit,
                    CategoryCode = item.CategoryCode,
                    DiscountStaff = item.DiscountStaff,
                    IsMoneyDiscount = item.IsMoneyDiscount,
                    NHOMSANPHAM_ID = item.NHOMSANPHAM_ID,
                    ProductGroupName = item.ProductGroupName,
                    NhomCha = item.NhomCha
                }).OrderByDescending(m => m.Id);

            if (SearchOjectAttr.ListField != null)
            {
                if (SearchOjectAttr.ListField.Count > 0)
                {
                    //lấy các đối tượng ObjectAttributeValue nào thỏa đk có AttributeId trong ListField và có giá trị tương ứng trong ListField
                    var listObjectAttrValue = ObjectAttributeRepository.GetAllObjectAttributeValue().AsEnumerable().Where(attr => SearchOjectAttr.ListField.Any(item => item.Id == attr.AttributeId && Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(attr.Value).Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Value)))).ToList();

                    //tiếp theo tìm các sản phẩm có id bằng với ObjectId trong listObjectAttrValue vừa tìm được
                    q = q.Where(product => listObjectAttrValue.Any(item => item.ObjectId == product.Id));

                    ViewBag.ListOjectAttrSearch = new JavaScriptSerializer().Serialize(SearchOjectAttr.ListField.Select(x => new { Id = x.Id, Value = x.Value }));
                }
            }

            if (NHOMSANPHAM_ID > 0)
            {

                var AllNhom = productRepository.GetAllvwProduct().Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                if (AllNhom.Any())
                {
                    q = q.Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                }
                else
                {
                    q = q.Where(x => x.NHOMSANPHAM_ID == NHOMSANPHAM_ID).ToList();
                }


            }


            if (string.IsNullOrEmpty(txtSearch) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch).Trim();
                //txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLower().Contains(txtSearch));
            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }
            //var group = q.GroupBy(x => x.CategoryCode).ToList();
            //int dem = 1;
            //foreach (var item in group)
            //{
            //    Category category = new Category();
            //    category.IsDeleted = false;
            //    category.CreatedUserId = WebSecurity.CurrentUserId;
            //    category.ModifiedUserId = WebSecurity.CurrentUserId;
            //    category.CreatedDate = DateTime.Now;
            //    category.ModifiedDate = DateTime.Now;
            //    category.Value = item.Key;
            //    category.Name = item.Key;
            //    category.Code = "product";
            //    category.OrderNo = dem;
            //    categoryRepository.InsertCategory(category);
            //    dem++;
            //}
            //nếu có tìm kiếm nâng cao thì lọc trước



            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }


        #endregion

        #region Xu ly file excel
        //Get action method
        [System.Web.Mvc.HttpGet]
        public ActionResult ImportFile()
        {
            var resultData = new List<ImportExeclModel>();
            return View(resultData);
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult ImportFile(HttpPostedFileBase file)
        {
            var resultData = new List<ImportExeclModel>();
            var path = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = DateTime.Now.Ticks + file.FileName;
                path = Path.Combine(Server.MapPath("~/fileuploads/"),
                Path.GetFileName(fileName));


                file.SaveAs(path);
                ViewBag.FileName = fileName;
            }

            resultData = ReadDataFileExcel2(path);

            return View(resultData);
        }
        private List<ImportExeclModel> ReadDataFileExcel(string filePath)
        {
            var resultData = new List<ImportExeclModel>();
            //read file
            //resultData = ReadDataFromFileExcel(path);
            Excels.Application app = new Excels.Application();
            Excels.Workbook wb = app.Workbooks.Open(filePath);
            Excels.Worksheet ws = wb.ActiveSheet;
            Excels.Range range = ws.UsedRange;
            for (int row = 1; row <= range.Rows.Count; row++)
            {
                var product = new ImportExeclModel()
                {
                    LoaiHang = ((Excels.Range)range.Cells[row, 1]).Text,
                    NhomHang = ((Excels.Range)range.Cells[row, 2]).Text,
                    MaHang = ((Excels.Range)range.Cells[row, 3]).Text,
                    TenHangHoa = ((Excels.Range)range.Cells[row, 4]).Text,
                    GiaBan = ((Excels.Range)range.Cells[row, 5]).Text,
                    GiaVon = ((Excels.Range)range.Cells[row, 6]).Text,
                    TonKho = ((Excels.Range)range.Cells[row, 7]).Text,
                    KHDat = ((Excels.Range)range.Cells[row, 8]).Text,
                    TonNhoNhat = ((Excels.Range)range.Cells[row, 9]).Text,
                    TonLonNhat = ((Excels.Range)range.Cells[row, 10]).Text,
                    DVT = ((Excels.Range)range.Cells[row, 11]).Text,
                    ThuocTinh = ((Excels.Range)range.Cells[row, 14]).Text,
                    MaHHLienQuan = ((Excels.Range)range.Cells[row, 15]).Text,
                    TrongLuong = ((Excels.Range)range.Cells[row, 17]).Text,
                    DangKinhDoanh = ((Excels.Range)range.Cells[row, 18]).Text,
                    BanTrucTiep = ((Excels.Range)range.Cells[row, 19]).Text,

                };
                resultData.Add(product);
            }
            return resultData;
        }

        #region Xuất excel

        public DataTable getData()
        {
            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  
            dt.TableName = "ProductInBoundData";
            //Add Columns  
            dt.Columns.Add("Tên Sản Phẩm", typeof(string));
            dt.Columns.Add("Mã Sản Phẩm", typeof(string));
            dt.Columns.Add("Giá Mua", typeof(decimal));
            dt.Columns.Add("Giá Bán", typeof(decimal));
            dt.Columns.Add("Số Lượng", typeof(int));
            dt.Columns.Add("Cảnh báo số lượng nhỏ hơn ?", typeof(int));
            dt.Columns.Add("VAT", typeof(int));
            dt.Columns.Add("Quản lí tồn kho ?", typeof(string));
            dt.Columns.Add("Cho phép bán hàng khi không đủ ?", typeof(string));
            dt.Columns.Add("Size", typeof(string));
            dt.Columns.Add("Màu", typeof(string));
            dt.Columns.Add("Đơn vị", typeof(string));
            dt.Columns.Add("Nhóm hàng", typeof(string));
            dt.Columns.Add("Nhà sản xuất", typeof(string));
            dt.Columns.Add("Hiển thị ra POS?", typeof(string));
            //Add Rows in DataTable  
            dt.Rows.Add("Hang hoa 3", "SP00000303", 10000, 100000, 20, 10, 0, "Có", "Có", "size S", "đỏ", "Cái", "Quần Jean", "Apple", "Có");
            dt.Rows.Add("Hang hoa 5", "SP00000309", 10000, 100000, 20, 10, 0, "Có", "Có", "size M", "xanh", "Cái", "Quần Tây", "Apple", "Có");
            dt.AcceptChanges();
            return dt;
        }
        public ActionResult DownLoadEXcel()
        {
            DataTable dt = getData();
            string fileName = "ExcelProduct.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            //var model = new ImportHangKM();
            //Encoding encoding = Encoding.UTF8;
            //var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ImportProduct")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            ////Response.ContentEncoding = Encoding.Unicode;
            //model.Content = template.Content;

            //Response.ContentType = "application/vnd.ms-excel";
            ////Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AppendHeader("content-disposition", "attachment;filename=" + "MauImportSP_Excel" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            //Response.Charset = encoding.EncodingName;
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);


            //Response.Write(model.Content);
            //Response.End();
            //return View(model);

        }
        public ActionResult ExportExcel()
        {
            List<ProductViewModel> q = productRepository.GetAllvwProduct().AsEnumerable()
               .Select(item => new ProductViewModel
               {
                   Id = item.Id,
                   CreatedUserId = item.CreatedUserId,
                   CreatedDate = item.CreatedDate,
                   ModifiedUserId = item.ModifiedUserId,
                   ModifiedDate = item.ModifiedDate,
                   Name = item.Name,
                   Code = item.Code,
                   PriceInbound = item.PriceInbound,
                   PriceOutbound = item.PriceOutbound,
                   Barcode = item.Barcode,
                   Type = item.Type,
                   Unit = item.Unit,
                   CategoryCode = item.CategoryCode,
                   DiscountStaff = item.DiscountStaff,
                   IsMoneyDiscount = item.IsMoneyDiscount,
                   NHOMSANPHAM_ID = item.NHOMSANPHAM_ID,
                   //NHOMSANPHAM_ID_LST =  item.NHOMSANPHAM_ID_LST,
                   ProductGroupName = item.ProductGroupName,
                   Color_product = item.color,
                   Size = item.Size

               }).OrderByDescending(m => m.Id).ToList();
            var model = new ImportHangKM();
            model.Content = buildHtmlDetailList_Product(q);


            Encoding encoding = Encoding.UTF8;

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("content-disposition", "attachment;filename=" + "Hanghoa" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            Response.Charset = encoding.EncodingName;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            Response.Write(model.Content);
            Response.End();


            return View();

        }

        string buildHtmlDetailList_Product(List<ProductViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã hàng</th>\r\n";
            detailLists += "		<th>Tên hàng</th>\r\n";
            //detailLists += "		<th>Tồn kho</th>\r\n";
            detailLists += "		<th>Tồn kho tối thiểu</th>\r\n";
            //detailLists += "		<th>Kho</th>\r\n";
            //detailLists += "		<th>Số Lô</th>\r\n";
            // detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>Giá vốn</th>\r\n";
            detailLists += "		<th>Giá bán</th>\r\n";
            //detailLists += "		<th>Số lượng</th>\r\n";
            detailLists += "		<th>Đơn vị</th>\r\n";
            //detailLists += "		<th>Tồn đầu kỳ</th>\r\n";

            //detailLists += "		<th>Nhập trong kỳ</th>\r\n";
            //detailLists += "		<th>Xuất trong kỳ</th>\r\n";
            //detailLists += "		<th>Tồn cuối kỳ</th>\r\n";
            //detailLists += "		<th>Tổng tiền tồn</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {

                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.Code + "</td>\r\n"
                + "<td class=\"text-left \">" + item.Name + "</td>\r\n"
                //+ "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                // + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                // + "<td class=\"text-right expiry_date\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-center\">" + item.MinInventory + "</td>\r\n"
                // + "<td class=\"text-center\">" + item.MinInventory + "</td>\r\n"
                + "<td class=\"text-right \">" + CommonSatic.ToCurrencyStr(item.PriceInbound, "VND").Replace(".", ",") + "</td>\r\n"
                + "<td class=\"text-right \">" + CommonSatic.ToCurrencyStr(item.PriceOutbound, "VND").Replace(".", ",") + "</td>\r\n"
                //+ "<td class=\"text-center\">" + item.MinInventory + "</td>\r\n"
                + "<td class=\"text-center\">" + item.Unit + "</td>\r\n"
                //+ "<td class=\"text-right orderNo\">" + item.First_Remain + "</td>\r\n"
                //+ "<td class=\"text-right orderNo\">" + item.Last_InboundQuantity + "</td>\r\n"
                //+ "<td class=\"text-right orderNo\">" + item.Last_OutboundQuantity + "</td>\r\n"
                //+ "<td class=\"text-right orderNo\">" + item.Remain + "</td>\r\n"
                //+ "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound * item.Remain, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            //detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            //detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
            //             + detailList.Sum(x => x.First_Remain)
            //             + "</td><td class=\"text-right\">"
            //             + detailList.Sum(x => x.Last_InboundQuantity)
            //             + "</td><td class=\"text-right\">"
            //             + detailList.Sum(x => x.Last_OutboundQuantity)
            //             + "</td><td class=\"text-right\">"
            //             + detailList.Sum(x => x.Remain)
            //              + "</td><td class=\"text-right\">"
            //             + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Remain * x.PriceInbound), null)
            //             + "</tr>\r\n";
            //detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }
        #endregion
        private List<ImportExeclModel> ReadDataFileExcel2(string filePath)
        {
            var resultData = new List<ImportExeclModel>();



            //readfile 
            if (filePath == "" || filePath == null)
            {
                TempData["Message"] = "Vui lòng chọn file import !!!";
                return resultData;

            }
            //FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {


                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                if (filePath.IndexOf(".xlsx") > 0)
                {
                    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    using (var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        //...

                        //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                        //DataSet result = excelReader.AsDataSet();
                        //...
                        //4. DataSet - Create column names from first row
                        excelReader.IsFirstRowAsColumnNames = true;
                        DataSet result = excelReader.AsDataSet();

                        //5. Data Reader methods
                        while (excelReader.Read())
                        {
                            var donhang = new ImportExeclModel()
                            {
                                //LoaiHang = excelReader.GetString(0),
                                //NhomHang = excelReader.GetString(1),
                                //MaHang = excelReader.GetString(2),
                                //TenHangHoa = excelReader.GetString(3),
                                //GiaBan = excelReader.GetString(4),
                                //GiaVon = excelReader.GetString(5),
                                //TonKho = excelReader.GetString(6),
                                //KHDat = excelReader.GetString(7),
                                //TonNhoNhat = excelReader.GetString(8),
                                //TonLonNhat = excelReader.GetString(9),
                                //DVT = excelReader.GetString(10),
                                //ThuocTinh = excelReader.GetString(13),
                                //MaHHLienQuan = excelReader.GetString(14),
                                //TrongLuong = excelReader.GetString(16),
                                //DangKinhDoanh = excelReader.GetString(17),
                                //BanTrucTiep = excelReader.GetString(18),

                                TenHangHoa = excelReader.GetString(0).ToString().Trim(),
                                MaHang = excelReader.GetString(1).ToString().Trim(),
                                GiaVon = excelReader.GetString(2),
                                GiaBan = excelReader.GetString(3),
                                TonKho = excelReader.GetString(4),
                                TonNhoNhat = excelReader.GetString(5),
                                VAT = excelReader.GetString(6),
                                KHDat = excelReader.GetString(7),
                                BanAm = excelReader.GetString(8),
                                Size = excelReader.GetString(9),
                                Mau = excelReader.GetString(10),
                                DVT = excelReader.GetString(11),
                                MaHHLienQuan = excelReader.GetString(12),
                                DangKinhDoanh = excelReader.GetString(13),
                                Display = excelReader.GetString(14),
                                //BanTrucTiep = excelReader.GetString(15),
                            };
                            resultData.Add(donhang);
                            //excelReader.GetInt32(0);
                        }

                        //6. Free resources (IExcelDataReader is IDisposable)
                        excelReader.Close();
                    }
                }
                else
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    excelReader.IsFirstRowAsColumnNames = true;
                    DataSet result = excelReader.AsDataSet();

                    //5. Data Reader methods
                    while (excelReader.Read())
                    {
                        var donhang = new ImportExeclModel()
                        {

                            TenHangHoa = excelReader.GetString(0).ToString().Trim(),
                            MaHang = excelReader.GetString(1).ToString().Trim(),
                            GiaVon = excelReader.GetString(2),
                            GiaBan = excelReader.GetString(3),
                            TonKho = excelReader.GetString(4),
                            TonNhoNhat = excelReader.GetString(5),
                            VAT = excelReader.GetString(6),
                            KHDat = excelReader.GetString(7),
                            BanAm = excelReader.GetString(8),
                            Size = excelReader.GetString(9),
                            Mau = excelReader.GetString(10),
                            DVT = excelReader.GetString(11),
                            MaHHLienQuan = excelReader.GetString(12),
                            DangKinhDoanh = excelReader.GetString(13),
                            Display = excelReader.GetString(14),
                        };
                        resultData.Add(donhang);
                        //excelReader.GetInt32(0);
                    }
                    excelReader.Close();

                }


                //...

            }

            return resultData;

        }

        /// <summary>
        /// Doc du lieu tu file excel vua upload insert vao db
        /// </summary>
        /// <param name="currentFile"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        //public ActionResult SaveFileExcel(string currentFile)
        //{
        //    var path = Path.Combine(Server.MapPath("~/fileuploads/"),
        //    Path.GetFileName(currentFile));
        //    var dataExcels = ReadDataFileExcel2(path);
        //    var listProductCodeDaco = new List<string>();
        //    // goi code insert vao db
        //    int i = 0;
        //    foreach (var importExeclModel in dataExcels)
        //    {
        //        //goi insert tung product vao bang Sale_Product
        //        if (i > 0)
        //        {
        //            //Kiem tra trung ma sp truoc khi insert
        //            var productExist = productRepository.GetProductByCode(importExeclModel.MaHang);

        //            //Neu no null tuc la no chua co trong bang product
        //            if (productExist == null)
        //            {
        //                var product = new Product();
        //                product.IsDeleted = false;
        //                product.Code = importExeclModel.MaHang;
        //                product.Name = importExeclModel.TenHangHoa;
        //                product.Origin = importExeclModel.LoaiHang;
        //                product.Manufacturer = importExeclModel.LoaiHang;
        //                product.CategoryCode = importExeclModel.LoaiHang;
        //                product.ProductGroup = importExeclModel.NhomHang;
        //                product.Unit = importExeclModel.DVT;
        //                //product.ProductLinkId = 0;
        //                product.Size = importExeclModel.ThuocTinh;
        //                product.PriceInbound = Convert.ToDecimal(importExeclModel.GiaVon.Replace(",", string.Empty));
        //                product.PriceOutbound = Convert.ToDecimal(importExeclModel.GiaBan.Replace(",", string.Empty));
        //                product.MinInventory = Convert.ToInt32(importExeclModel.TonKho.Replace(".0", string.Empty));
        //                product.CreatedDate = DateTime.Now;
        //                product.Type = "product";
        //                productRepository.InsertProduct(product);

        //            }
        //            else
        //            {
        //                listProductCodeDaco.Add(importExeclModel.MaHang);
        //            }
        //        }
        //        i++;
        //    }
        //    if (listProductCodeDaco.Count > 0)
        //    {
        //        //ViewBag.ErrorMesseage = $"Ma du lieu them vao da ton tai:{string.Join(",", listProductCodeDaco)}";
        //        ViewBag.ErrorMesseage = ("Dữ Liệu Đã Tồn Tại: " + string.Join(",", listProductCodeDaco));
        //        return View("ImportFile", dataExcels);
        //    }
        //    return RedirectToAction("/Index");
        //}
        public ActionResult SaveFileExcel(string currentFile, string command)
        {

            if (command.Equals("Luu3"))
            {
                return RedirectToAction("/ImportFile");
            }



            var path = Path.Combine(Server.MapPath("~/fileuploads/"),
            Path.GetFileName(currentFile));
            var dataExcels = ReadDataFileExcel2(path);
            var listProductCodeDaco = new List<string>();
            // goi code insert vao db
            int i = 0;
            //
            //get cookie brachID 
            HttpRequestBase request = this.HttpContext.Request;
            string strBrandID = "0";
            if (request.Cookies["BRANCH_ID_CookieName"] != null)
            {
                strBrandID = request.Cookies["BRANCH_ID_CookieName"].Value;
                if (strBrandID == "")
                {
                    strBrandID = "0";
                }
            }

            //get  CurrentUser.branchId

            if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
            {
                strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
            }

            int? intBrandID = int.Parse(strBrandID);
            //
            var warehouse = WarehouseRepository.GetAllWarehouse().Where(x => x.BranchId == intBrandID).FirstOrDefault();
            //
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var ProductInbound = new ProductInbound();
                ProductInbound.IsDeleted = false;
                ProductInbound.CreatedUserId = WebSecurity.CurrentUserId;
                ProductInbound.ModifiedUserId = WebSecurity.CurrentUserId;
                ProductInbound.CreatedDate = DateTime.Now;
                ProductInbound.ModifiedDate = DateTime.Now;
                ProductInbound.BranchId = intBrandID;
                ProductInbound.WarehouseDestinationId = warehouse.Id;
                List<ProductInboundDetail> listNewCheckSameId = new List<ProductInboundDetail>();
                foreach (var importExeclModel in dataExcels)
                {
                    //goi insert tung product vao bang Sale_Product
                    if (i > 0)
                    {
                        //Kiem tra trung ma sp truoc khi insert
                        var productExist = productRepository.GetProductByCode(importExeclModel.MaHang);

                        //Neu no null tuc la no chua co trong bang product
                        if (productExist == null)
                        {

                            var product = new Product();
                            product.IsDeleted = false;
                            product.Code = importExeclModel.MaHang;
                            product.Name = importExeclModel.TenHangHoa;
                            //product.Origin = importExeclModel.LoaiHang;
                            product.Manufacturer = importExeclModel.DangKinhDoanh;
                            //product.CategoryCode = importExeclModel.LoaiHang;
                            //product.ProductGroup = importExeclModel.NhomHang;
                            product.Unit = importExeclModel.DVT;
                            //product.ProductLinkId = 0;
                            product.Size = importExeclModel.Size;
                            product.color = importExeclModel.Mau;

                            product.PriceInbound = Convert.ToDecimal(importExeclModel.GiaVon.Replace(",", string.Empty));
                            product.PriceOutbound = Convert.ToDecimal(importExeclModel.GiaBan.Replace(",", string.Empty));
                            product.MinInventory = Convert.ToInt32(importExeclModel.TonNhoNhat.Replace(".0", string.Empty));
                            product.CreatedDate = DateTime.Now;

                            product.Type = "product";
                            if (Helpers.Common.ChuyenThanhKhongDau(importExeclModel.BanAm).Contains("co"))
                            {
                                product.IS_ALOW_BAN_AM = true;
                            }
                            if (Helpers.Common.ChuyenThanhKhongDau(importExeclModel.Display).Contains("co"))
                            {
                                product.is_display = true;
                            }
                            //var a = Helpers.Common.ChuyenThanhKhongDau(importExeclModel.MaHHLienQuan);
                            var nhomsp = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByTEN_NHOMSANPHAM(importExeclModel.MaHHLienQuan);

                            if (nhomsp != null)
                            {
                                product.NHOMSANPHAM_ID_LST = nhomsp.NHOMSANPHAM_ID.ToString();
                                product.NHOMSANPHAM_ID = nhomsp.NHOMSANPHAM_ID;
                            }
                            product.List_Image = "";

                            productRepository.InsertProduct(product);

                            var Idcha = product.Id;
                            var SKUId = productRepository.GetAllvwProductSKU().Where(n => n.Product_id == Idcha).FirstOrDefault();
                            var ProductSKU5 = new Domain.Sale.Entities.Sale_Product_SKU();
                            if (SKUId == null)
                            {

                                ProductSKU5.CreatedUserId = WebSecurity.CurrentUserId;
                                ProductSKU5.ModifiedUserId = WebSecurity.CurrentUserId;
                                ProductSKU5.CreatedDate = DateTime.Now;
                                ProductSKU5.ModifiedDate = DateTime.Now;
                                ProductSKU5.AssignedUserId = WebSecurity.CurrentUserId;
                                ProductSKU5.IsDeleted = true;
                                ProductSKU5.color = product.color;
                                ProductSKU5.Size = product.Size;
                                ProductSKU5.Product_id = product.Id;
                                ProductSKU5.Product_idSKU = product.Id;
                                productRepository.InsertProduct_SKU(ProductSKU5);

                            }
                            //Tạo phiếu nhập kho 
                            listNewCheckSameId.Add(new Domain.Sale.Entities.ProductInboundDetail
                            {
                                ProductId = product.Id,
                                Quantity = Convert.ToInt32(importExeclModel.TonKho.Replace(".0", string.Empty)),
                                Unit = product.Unit,
                                Price = product.PriceInbound,
                                IsDeleted = false,
                                CreatedUserId = WebSecurity.CurrentUserId,
                                ModifiedUserId = WebSecurity.CurrentUserId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                //ExpiryDate = group.ExpiryDate,
                                // LoCode = group.LoCode
                            });



                        }
                        else
                        {
                            listProductCodeDaco.Add(importExeclModel.MaHang);
                        }
                    }
                    i++;

                }
                ProductInbound.TotalAmount = listNewCheckSameId.Sum(x => x.Price * x.Quantity);
                // ProductInbound.Type = (order.Id == 0 ? "manual" : (order.SupplierId != null ? "external" : "internal"));
                if (listNewCheckSameId.Any())
                {
                    if (command == "Luu")
                    {
                        ProductInboundRepository.InsertProductInbound(ProductInbound);
                        string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Inbound");
                        ProductInbound.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, ProductInbound.Id);

                        ProductInboundRepository.UpdateProductInbound(ProductInbound);

                        //Thêm chi tiết phiếu nhập

                        foreach (var item in listNewCheckSameId)
                        {
                            item.ProductInboundId = ProductInbound.Id;
                            ProductInboundRepository.InsertProductInboundDetail(item);
                        }

                        //Thêm vào quản lý chứng từ
                        TransactionController.Create(new TransactionViewModel
                        {
                            TransactionModule = "ProductInbound",
                            TransactionCode = ProductInbound.Code,
                            TransactionName = "Nhập kho"
                        });
                    }
                }
                scope.Complete();
            }


            if (listProductCodeDaco.Count > 0)
            {
                //ViewBag.ErrorMesseage = $"Ma du lieu them vao da ton tai:{string.Join(",", listProductCodeDaco)}";
                ViewBag.ErrorMesseage = ("Dữ Liệu Đã Tồn Tại: " + string.Join(",", listProductCodeDaco));
                return View("ImportFile", dataExcels);
            }
            return RedirectToAction("/Index");
        }
        #endregion

        #region Create
        public ViewResult Create()
        {

            var model = new ProductViewModel();
            model.PriceInbound = 0;
            model.PriceOutbound = 0;
            model.MinInventory = 0;
            model.is_display = true;
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]

        //[ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model, string img1, string img2, string img3, string img4, string img5, string img6, string img7, string img8, string command)
        {

            string[] listimgdelete = { img1, img2, img3, img4, img5, img6, img7, img8 };
            CategoryRepository p = new CategoryRepository(new Domain.ErpDbContext());
            var categories = p.GetListCategoryByCode("Color_product").OrderBy(m => m.OrderNo).Where(m => m.Value == model.Color_product).FirstOrDefault();
            var categories1 = p.GetListCategoryByCode("Size_product").OrderBy(m => m.OrderNo).Where(m => m.Value == model.Size_product).FirstOrDefault();
            var NameColorSP = categories.Name;
            var NameSizeSP = categories1.Name;
            //var errors = ModelState.Values.SelectMany(v => v.Errors);


            if (ModelState.IsValid)
            {


                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {




                    var GroupPrice = Request["GroupPrice"];
                    var Product = new Domain.Sale.Entities.Product();
                    AutoMapper.Mapper.Map(model, Product);
                    Product.IsDeleted = false;
                    Product.CreatedUserId = WebSecurity.CurrentUserId;
                    Product.ModifiedUserId = WebSecurity.CurrentUserId;
                    Product.CreatedDate = DateTime.Now;
                    Product.ModifiedDate = DateTime.Now;
                    Product.GroupPrice = GroupPrice;
                    Product.HDSD = model.HDSD;
                    Product.THANH_PHAN = model.THANH_PHAN;
                    Product.THUONG_HIEU = model.THUONG_HIEU;
                    Product.Description_brief = model.Description_brief;
                    Product.Description = model.Description;
                    Product.NHOMSANPHAM_ID = model.NHOMSANPHAM_ID;
                    Product.color = NameColorSP;
                    Product.Size = NameSizeSP;
                    Product.IS_NEW = model.IS_NEW;
                    Product.IS_HOT = model.IS_HOT;
                    Product.is_display = model.is_display;
                    //Product.PriceInbound = model.PriceInbound.Value;
                    //Product.PriceOutbound = model.PriceOutbound.Value;
                    if (model.NHOMSANPHAM_ID_LST == null)
                    {
                        Product.NHOMSANPHAM_ID_LST = string.Join(",", model.NHOMSANPHAM_ID);
                    }
                    if (model.LOAISANPHAM_ID_LST == null)
                    {
                        Product.LOAISANPHAM_ID_LST = string.Join(",", model.LOAISANPHAM_ID);
                    }
                    //Product.NHOMSANPHAM_ID = model.NHOMSANPHAM_ID;
                    //Product.LOAISANPHAM_ID = model.LOAISANPHAM_ID;
                    if (model.PriceInbound == null)
                    {
                        Product.PriceInbound = 0;
                    }
                    var GetNhomID = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOMSANPHAM_ID);
                    var KitudauNhomSP = Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(GetNhomID.TEN_NHOMSANPHAM.Substring(0, 1)).ToUpper();

                    var IdSizeSP = model.Size_product;
                    if (Product.Code == "" || Product.Code == null)
                    {
                        Product.Code = (KitudauNhomSP + model.Color_product + model.Size_product) + '-' + (Erp.BackOffice.Helpers.Common.GetOrderNo("Product", model.Code));
                    }
                    else
                    {
                        Product.Code = model.Code;
                    }


                    //begin kiem tra trung ma
                    var product1 = productRepository.GetAllProduct()
                    .Where(item => item.Code == Product.Code && (item.color == Product.color && item.Size == Product.Size)).FirstOrDefault();
                    if (product1 != null)
                    {
                        ViewBag.errors = "Trùng mã sản phẩm";
                        TempData[Globals.SuccessMessageKey] = "Trùng mã sản phẩm";
                        ViewBag.ErrorMesseage = "Trùng mã sản phẩm";
                        //return RedirectToAction("Edit", new { Id = model.Id, IsPopup = Request["IsPopup"] });
                        return RedirectToAction("Edit", new { Id = model.Id });
                    }

                    //end kiem tra trung ma


                    //var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("product-image-folder"));
                    var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("product-image-folder"));
                    if (Request.Files["file-imagepos"] != null)
                    {
                        var file = Request.Files["file-imagepos"];
                        if (file.ContentLength > 0)
                        {
                            string image_namepos = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_pos_" + file.FileName;
                            bool isExists = System.IO.Directory.Exists(path);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(path);
                            file.SaveAs(path + image_namepos);
                            Product.Image_Pos = image_namepos;

                        }
                    }
                    if (Request.Files["file-image"] != null)
                    {
                        int count = Request.Files.Count;

                        string nameimgs = "";
                        for (int i = 0; i < count - 1; i++)
                        {

                            if (listimgdelete[i] != "1")
                            {

                                if (i == count - 1)

                                {
                                    var b = Request.Files[i];


                                    if (b.ContentLength > 0)

                                    {
                                        string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + b.FileName;
                                        bool isExists = System.IO.Directory.Exists(path);
                                        if (!isExists)
                                            System.IO.Directory.CreateDirectory(path);
                                        nameimgs += image_name;
                                        b.SaveAs(path + image_name);
                                    }
                                }
                                else
                                {

                                    var a = Request.Files[i];


                                    if (a.ContentLength > 0)
                                    {

                                        string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + a.FileName;
                                        if (i == 0)
                                        {
                                            Product.Image_Name = image_name;
                                        }
                                        bool isExists = System.IO.Directory.Exists(path);
                                        if (!isExists)
                                            System.IO.Directory.CreateDirectory(path);
                                        nameimgs += image_name + ",";
                                        a.SaveAs(path + image_name);


                                    }
                                }


                            }
                        }
                        if (nameimgs.Length > 1)
                        {
                            nameimgs = nameimgs.Substring(0, nameimgs.Length - 1);
                        }
                        Product.List_Image = nameimgs;
                        productRepository.InsertProduct(Product);
                        Erp.BackOffice.Helpers.Common.SetOrderNo("Product");

                        var Idcha = Product.Id;
                        var SKUId = productRepository.GetAllvwProductSKU().Where(n => n.Product_id == Idcha).FirstOrDefault();
                        var ProductSKU5 = new Domain.Sale.Entities.Sale_Product_SKU();
                        if (SKUId == null)
                        {

                            ProductSKU5.CreatedUserId = WebSecurity.CurrentUserId;
                            ProductSKU5.ModifiedUserId = WebSecurity.CurrentUserId;
                            ProductSKU5.CreatedDate = DateTime.Now;
                            ProductSKU5.ModifiedDate = DateTime.Now;
                            ProductSKU5.AssignedUserId = WebSecurity.CurrentUserId;
                            ProductSKU5.IsDeleted = true;
                            ProductSKU5.color = Product.color;
                            ProductSKU5.Size = Product.Size;
                            ProductSKU5.Product_id = Product.Id;
                            ProductSKU5.Product_idSKU = Product.Id;
                            productRepository.InsertProduct_SKU(ProductSKU5);

                        }
                        //begin up hinh anh cho client
                        path = Helpers.Common.GetSetting("product-image-folder-client");
                        string prjClient = Helpers.Common.GetSetting("prj-client");
                        //string prjClient = "KISSY_CLIENT";
                        var filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                        DirectoryInfo di = new DirectoryInfo(filepath);
                        if (prjClient.Trim() == "")
                        {
                            filepath = di.Parent.FullName + path.Replace("/", @"\");
                        }
                        else
                        {
                            filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                        }

                        if (Request.Files["file-image"] != null)
                        {

                            for (int i = 0; i < count; i++)
                            {

                                if (listimgdelete[i] != "1")
                                {
                                    var a = Request.Files[i];
                                    if (a.ContentLength > 0)
                                    {
                                        if (i == count - 1)
                                        {
                                            var b = Request.Files[i];


                                            string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + a.FileName;
                                            bool isExists = System.IO.Directory.Exists(path);
                                            if (!isExists)
                                                System.IO.Directory.CreateDirectory(path);
                                            nameimgs += image_name + ",";
                                            a.SaveAs(path + image_name);
                                        }
                                        else
                                        {
                                            if (a.ContentLength > 0)
                                            {

                                                string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + a.FileName;
                                                bool isExists = System.IO.Directory.Exists(filepath);
                                                if (!isExists)
                                                    System.IO.Directory.CreateDirectory(filepath);

                                                a.SaveAs(filepath + image_name);


                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                    //end up hinh anh cho client



                    //tạo đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                    ObjectAttributeController.CreateOrUpdateForObject(Product.Id, model.AttributeValueList);

                    if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                    {
                        return RedirectToAction("Edit", new { Id = model.Id, IsPopup = Request["IsPopup"] });
                    }
                    scope.Complete();
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    if (command.Equals("Luu2"))
                    {
                        return RedirectToAction("/Create");
                    }
                    else if (command.Equals("Luu"))
                    {
                        return RedirectToAction("/Index");
                    }

                }

            }
            string errors = string.Empty;
            foreach (var modalState in ModelState.Values)
            {
                errors += modalState.Value + ": ";
                foreach (var error in modalState.Errors)
                {
                    errors += error.ErrorMessage;
                }
            }

            ViewBag.errors = errors;

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult Create1(ProductViewModel model, string img1, string img2, string img3, string img4, string img5, string img6, string img7, string img8)
        {


            string[] listimgdelete = { img1, img2, img3, img4, img5, img6, img7, img8 };
            CategoryRepository p = new CategoryRepository(new Domain.ErpDbContext());
            var categories = p.GetListCategoryByCode("Color_product").OrderBy(m => m.OrderNo).Where(m => m.Value == model.Color_product).FirstOrDefault();
            var categories1 = p.GetListCategoryByCode("Size_product").OrderBy(m => m.OrderNo).Where(m => m.Value == model.Size_product).FirstOrDefault();
            var NameColorSP = categories.Name;
            var NameSizeSP = categories1.Name;
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {


                var GroupPrice = Request["GroupPrice"];
                var Product = new Domain.Sale.Entities.Product();
                AutoMapper.Mapper.Map(model, Product);
                Product.IsDeleted = false;
                Product.CreatedUserId = WebSecurity.CurrentUserId;
                Product.ModifiedUserId = WebSecurity.CurrentUserId;
                Product.CreatedDate = DateTime.Now;
                Product.ModifiedDate = DateTime.Now;
                Product.GroupPrice = GroupPrice;
                Product.HDSD = model.HDSD;
                Product.THANH_PHAN = model.THANH_PHAN;
                Product.THUONG_HIEU = model.THUONG_HIEU;
                Product.Description_brief = model.Description_brief;
                Product.Description = model.Description;
                Product.NHOMSANPHAM_ID = model.NHOMSANPHAM_ID;
                Product.color = NameColorSP;
                Product.Size = NameSizeSP;
                Product.IS_NEW = model.IS_NEW;
                Product.IS_HOT = model.IS_HOT;
                Product.is_display = model.is_display;
                //Product.PriceInbound = model.PriceInbound.Value;
                //Product.PriceOutbound = model.PriceOutbound.Value;
                if (model.NHOMSANPHAM_ID_LST == null)
                {
                    Product.NHOMSANPHAM_ID_LST = string.Join(",", model.NHOMSANPHAM_ID);
                }
                if (model.LOAISANPHAM_ID_LST == null)
                {
                    Product.LOAISANPHAM_ID_LST = string.Join(",", model.LOAISANPHAM_ID);
                }
                //Product.NHOMSANPHAM_ID = model.NHOMSANPHAM_ID;
                //Product.LOAISANPHAM_ID = model.LOAISANPHAM_ID;
                if (model.PriceInbound == null)
                {
                    Product.PriceInbound = 0;
                }
                var GetNhomID = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOMSANPHAM_ID);
                var KitudauNhomSP = Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(GetNhomID.TEN_NHOMSANPHAM.Substring(0, 1)).ToUpper();

                var IdSizeSP = model.Size_product;
                if (Product.Code == "" || Product.Code == null)
                {
                    Product.Code = (KitudauNhomSP + model.Color_product + model.Size_product) + '-' + (Erp.BackOffice.Helpers.Common.GetOrderNo("Product", model.Code));
                }
                else
                {
                    Product.Code = model.Code;
                }


                //begin kiem tra trung ma
                var product1 = productRepository.GetAllProduct()
                .Where(item => item.Code == Product.Code).FirstOrDefault();
                if (product1 != null)
                {
                    ViewBag.errors = "Trùng mã sản phẩm";
                    TempData[Globals.SuccessMessageKey] = "Trùng mã sản phẩm";
                    ViewBag.ErrorMesseage = "Trùng mã sản phẩm";
                    //return RedirectToAction("Edit", new { Id = model.Id, IsPopup = Request["IsPopup"] });
                    return RedirectToAction("Edit", new { Id = model.Id });
                }

                //end kiem tra trung ma


                //var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("product-image-folder"));
                var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("product-image-folder"));
                if (Request.Files["file-imagepos"] != null)
                {
                    var file = Request.Files["file-imagepos"];
                    if (file.ContentLength > 0)
                    {
                        string image_namepos = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_pos_" + file.FileName;
                        bool isExists = System.IO.Directory.Exists(path);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(path);
                        file.SaveAs(path + image_namepos);
                        Product.Image_Pos = image_namepos;

                    }
                }
                if (Request.Files["file-image"] != null)
                {
                    int count = Request.Files.Count;

                    string nameimgs = "";
                    for (int i = 0; i < count - 1; i++)
                    {

                        if (listimgdelete[i] != "1")
                        {

                            if (i == count - 1)

                            {
                                var b = Request.Files[i];


                                if (b.ContentLength > 0)

                                {
                                    string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + b.FileName;
                                    bool isExists = System.IO.Directory.Exists(path);
                                    if (!isExists)
                                        System.IO.Directory.CreateDirectory(path);
                                    nameimgs += image_name;
                                    b.SaveAs(path + image_name);
                                }
                            }
                            else
                            {

                                var a = Request.Files[i];


                                if (a.ContentLength > 0)
                                {

                                    string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + a.FileName;
                                    if (i == 0)
                                    {
                                        Product.Image_Name = image_name;
                                    }
                                    bool isExists = System.IO.Directory.Exists(path);
                                    if (!isExists)
                                        System.IO.Directory.CreateDirectory(path);
                                    nameimgs += image_name + ",";
                                    a.SaveAs(path + image_name);


                                }
                            }


                        }
                    }
                    if (nameimgs.Length > 1)
                    {
                        nameimgs = nameimgs.Substring(0, nameimgs.Length - 1);
                    }
                    Product.List_Image = nameimgs;
                    productRepository.InsertProduct(Product);
                    Erp.BackOffice.Helpers.Common.SetOrderNo("Product");
                    //begin up hinh anh cho client
                    path = Helpers.Common.GetSetting("product-image-folder-client");
                    string prjClient = Helpers.Common.GetSetting("prj-client");
                    //string prjClient = "KISSY_CLIENT";
                    var filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                    DirectoryInfo di = new DirectoryInfo(filepath);
                    if (prjClient.Trim() == "")
                    {
                        filepath = di.Parent.FullName + path.Replace("/", @"\");
                    }
                    else
                    {
                        filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                    }

                    if (Request.Files["file-image"] != null)
                    {

                        for (int i = 0; i < count; i++)
                        {

                            if (listimgdelete[i] != "1")
                            {
                                var a = Request.Files[i];
                                if (a.ContentLength > 0)
                                {
                                    if (i == count - 1)
                                    {
                                        var b = Request.Files[i];


                                        string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + a.FileName;
                                        bool isExists = System.IO.Directory.Exists(path);
                                        if (!isExists)
                                            System.IO.Directory.CreateDirectory(path);
                                        nameimgs += image_name + ",";
                                        a.SaveAs(path + image_name);
                                    }
                                    else
                                    {
                                        if (a.ContentLength > 0)
                                        {

                                            string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + a.FileName;
                                            bool isExists = System.IO.Directory.Exists(filepath);
                                            if (!isExists)
                                                System.IO.Directory.CreateDirectory(filepath);

                                            a.SaveAs(filepath + image_name);


                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                //end up hinh anh cho client



                //tạo đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                ObjectAttributeController.CreateOrUpdateForObject(Product.Id, model.AttributeValueList);

                if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                {
                    return RedirectToAction("Edit", new { Id = model.Id, IsPopup = Request["IsPopup"] });
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("/Create");
            }

            string errors = string.Empty;
            foreach (var modalState in ModelState.Values)
            {
                errors += modalState.Value + ": ";
                foreach (var error in modalState.Errors)
                {
                    errors += error.ErrorMessage;
                }
            }

            ViewBag.errors = errors;

            return View(model);
        }
        public ActionResult CheckCodeExsist(int? id, string code)
        {
            code = code.Trim();
            var product = productRepository.GetAllProduct()
                .Where(item => item.Code == code).FirstOrDefault();
            if (product != null)
            {
                if (id == null || (id != null && product.Id != id))
                    return Content("Trùng mã sản phẩm!");
                else
                {
                    return Content("");
                }
            }
            else
            {
                return Content("");
            }
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {


            var Product = productRepository.GetProductById(Id.Value);
            //int NHOMSANPHAM_ID = Product.NHOMSANPHAM_ID;
            CategoryRepository p = new CategoryRepository(new Domain.ErpDbContext());
            var categories = p.GetListCategoryByCode("Color_product").OrderBy(m => m.OrderNo).Where(m => m.Name == Product.color).FirstOrDefault();
            var categories1 = p.GetListCategoryByCode("Size_product").OrderBy(m => m.OrderNo).Where(m => m.Name == Product.Size).FirstOrDefault();


            var ValueColorSP = "";
            var valueSizeSP = "";
            var NameColor = "";
            var NameSize = "";
            if (categories != null)
            {
                ValueColorSP = categories.Value;
                NameColor = categories.Name;
            }

            if (categories1 != null)
            {
                valueSizeSP = categories1.Value;
                NameSize = categories1.Name;
            }




            string listcolor = Product.color;
            string listsize = Product.Size;
            string[] listimg = Product.List_Image.Split(',');
            string[] LOAISANPHAM_ID_LST = null;
            if (Product.LOAISANPHAM_ID_LST != null)
            {
                LOAISANPHAM_ID_LST = Product.LOAISANPHAM_ID_LST.Split(',');
            }

            if (Product != null && Product.IsDeleted != true)
            {
                var model = new ProductViewModel();
                AutoMapper.Mapper.Map(Product, model);

                //model.Image_Name = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(Product.Image_Name, "product-image-folder", "product");
                string productId = "," + model.Id + ",";
                var supplierList = SupplierRepository.GetAllSupplier().AsEnumerable().Where(item => ("," + item.ProductIdOfSupplier + ",").Contains(productId) == true).ToList();
                ViewBag.supplierList = supplierList;
                Product.CreatedUserId = WebSecurity.CurrentUserId;
                Product.ModifiedUserId = WebSecurity.CurrentUserId;
                Product.CreatedDate = DateTime.Now;
                Product.ModifiedDate = DateTime.Now;
                Product.GroupPrice = model.GroupPrice;
                Product.HDSD = model.HDSD;
                Product.THANH_PHAN = model.THANH_PHAN;
                Product.THUONG_HIEU = model.THUONG_HIEU;
                Product.Description_brief = model.Description_brief;
                Product.Description = model.Description;
                Product.NHOMSANPHAM_ID = model.NHOMSANPHAM_ID;
                Product.color = model.Color_product;
                Product.Size = model.Size_product;
                Product.IS_ALOW_BAN_AM = model.IS_ALOW_BAN_AM;
                Product.IS_NGUNG_KD = model.IS_NGUNG_KD;
                Product.IS_COMBO = model.IS_COMBO;
                Product.IS_NOIBAT = model.IS_NOIBAT;
                Product.IS_NEW = model.IS_NEW;
                Product.STT_ISNEW = model.STT_ISNEW;
                Product.IS_HOT = model.IS_HOT;
                Product.LIST_TAGS = model.LIST_TAGS;
                Product.is_price_unknown = model.is_price_unknown;
                Product.is_display = model.is_display;
                Product.PriceInbound = model.PriceInbound;
                Product.PriceOutbound = model.PriceOutbound;

                if (model.NHOMSANPHAM_ID_LST == null)
                {
                    Product.NHOMSANPHAM_ID_LST = string.Join(",", model.NHOMSANPHAM_ID);
                }
                if (model.LOAISANPHAM_ID_LST == null)
                {
                    Product.LOAISANPHAM_ID_LST = string.Join(",", model.LOAISANPHAM_ID);
                }
                //model.NHOMSANPHAM_ID_LST = NHOMSANPHAM_ID;
                model.LOAISANPHAM_ID = model.LOAISANPHAM_ID;
                model.Color_product = ValueColorSP;
                model.Size_product = valueSizeSP;
                model.NameColor = NameColor;
                model.NameSize = NameSize;
                model.listimg_product = listimg;
                model.is_display = Product.is_display;
                Product.URL_SLUG = model.URL_SLUG;
                Product.Description_brief = model.Description_brief;
                Product.is_price_unknown = model.is_price_unknown;
                IEnumerable<vwProductsamegroup> q = productRepository.GetAllvwProductsamegroup().AsEnumerable().Where(item => item.Product_id == Id)
                .Select(item => new vwProductsamegroup
                {
                    Id = item.Id,
                    Product_id = item.Product_id,
                    Product_idsame = item.Product_idsame,
                    Name = item.Name,
                    Code = item.Code,
                    PriceOutBound = item.PriceOutBound,
                    TEN_LOAISANPHAM = item.TEN_LOAISANPHAM,
                    TEN_NHOMSANPHAM = item.TEN_NHOMSANPHAM,
                    stt = item.stt
                }).OrderBy(x => x.stt);


                IEnumerable<vwProductsamesize> q2 = productRepository.GetAllvwProductsamesize().AsEnumerable().Where(item => item.Product_id == Id)
               .Select(item => new vwProductsamesize
               {
                   Id = item.Id,
                   Product_id = item.Product_id,
                   Product_idsame = item.Product_idsame,
                   Name = item.Name,
                   Code = item.Code,
                   PriceOutBound = item.PriceOutBound,
                   TEN_LOAISANPHAM = item.TEN_LOAISANPHAM,
                   TEN_NHOMSANPHAM = item.TEN_NHOMSANPHAM,
                   stt = item.stt
               }).OrderBy(x => x.stt);

                IEnumerable<vwProductSKU> q3 = productRepository.GetAllvwProductSKU().Where(item => item.Product_idSKU == Id).AsEnumerable()
              .Select(item => new vwProductSKU
              {
                  Id = item.Id,
                  Product_id = item.Product_id,
                  Product_idSKU = item.Product_idSKU,
                  color = item.color,
                  Size = item.Size,
                  CreatedDate = item.CreatedDate,
                  AssignedUserId = item.AssignedUserId,
                  IsDeleted = item.IsDeleted,
                  CreatedUserId = item.CreatedUserId,
                  ModifiedDate = item.ModifiedDate,
                  ModifiedUserId = item.ModifiedUserId,
                  CodeSKU = item.CodeSKU,
                  CodeSP = item.CodeSP,
                  Is_display = item.Is_display

              }).OrderBy(n => n.CodeSP).ToList();

                if (q3.Count() == 0)
                {

                    var sku = productRepository.GetAllvwProductSKU().Where(item => item.Product_id == Id).AsEnumerable().SingleOrDefault();
                    if (sku != null)
                    {
                        q3 = productRepository.GetAllvwProductSKU().Where(item => item.Product_idSKU == sku.Product_idSKU).AsEnumerable()
                       .Select(item => new vwProductSKU
                       {
                           Id = item.Id,
                           Product_id = item.Product_id,
                           Product_idSKU = item.Product_idSKU,
                           color = item.color,
                           Size = item.Size,
                           CreatedDate = item.CreatedDate,
                           AssignedUserId = item.AssignedUserId,
                           IsDeleted = item.IsDeleted,
                           CreatedUserId = item.CreatedUserId,
                           ModifiedDate = item.ModifiedDate,
                           ModifiedUserId = item.ModifiedUserId,
                           CodeSKU = item.CodeSKU,
                           CodeSP = item.CodeSP,
                           Is_display = item.Is_display

                       }).OrderBy(n => n.CodeSP).ToList();
                    }
                }
                ViewBag.listProduct = q;
                ViewBag.listProduct2 = q2;
                ViewBag.listProduct3 = q3;

                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("/Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductViewModel model, string img1, string img2, string img3, string img4, string img5, string img6, string img7, string img8)
        {

            string[] listimgdelete = { img1, img2, img3, img4, img5, img6, img7, img8 };

            //string messages = string.Join("; ", ModelState.Values
            //                            .SelectMany(x => x.Errors)
            //                            .Select(x => x.ErrorMessage));

            CategoryRepository p = new CategoryRepository(new Domain.ErpDbContext());
            var categories = p.GetListCategoryByCode("Color_product").OrderBy(m => m.OrderNo).Where(m => m.Value == model.Color_product).FirstOrDefault();
            var categories1 = p.GetListCategoryByCode("Size_product").OrderBy(m => m.OrderNo).Where(m => m.Value == model.Size_product).FirstOrDefault();
            var NameColorSP = categories.Name;
            var NameSizeSP = categories1.Name;
            if (ModelState.IsValid)
            {
                var Product = productRepository.GetProductById(model.Id);
                string[] listimg = Product.List_Image.Split(',');
                string imgpos = Product.Image_Pos;
                AutoMapper.Mapper.Map(model, Product);
                Product.ModifiedUserId = WebSecurity.CurrentUserId;
                Product.ModifiedDate = DateTime.Now;
                Product.NHOMSANPHAM_ID = model.NHOMSANPHAM_ID;
                Product.LOAISANPHAM_ID = model.LOAISANPHAM_ID;
                Product.Manufacturer = model.Manufacturer;
                Product.Unit = model.Unit;
                Product.MinInventory = model.MinInventory;
                Product.Description = model.Description;
                Product.HDSD = model.HDSD;
                Product.THANH_PHAN = model.THANH_PHAN;
                Product.THUONG_HIEU = model.THUONG_HIEU;
                Product.PriceInbound = model.PriceInbound;
                Product.PriceOutbound = model.PriceOutbound;
                Product.TaxFee = model.TaxFee;
                Product.IS_ALOW_BAN_AM = model.IS_ALOW_BAN_AM;
                Product.IS_NGUNG_KD = model.IS_NGUNG_KD;
                Product.IS_COMBO = model.IS_COMBO;
                Product.IS_NOIBAT = model.IS_NOIBAT;
                Product.IS_NEW = model.IS_NEW;
                Product.STT_ISNEW = model.STT_ISNEW;
                Product.IS_HOT = model.IS_HOT;
                Product.LIST_TAGS = model.LIST_TAGS;
                Product.META_TITLE = model.META_TITLE;
                Product.META_DESCRIPTION = model.META_DESCRIPTION;
                Product.is_price_unknown = model.is_price_unknown;
                Product.is_display = model.is_display;
                Product.Description_brief = model.Description_brief;
                Product.color = NameColorSP;
                Product.Size = NameSizeSP;
                //Product.NHOMSANPHAM_ID_LST = string.Join(",", model.NHOMSANPHAM_ID_LST.ToArray());
                var GetNhomID = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOMSANPHAM_ID);
                var KitudauNhomSP = Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(GetNhomID.TEN_NHOMSANPHAM.Substring(0, 1)).ToUpper();
                var IdColorSP = model.Color_product;
                var IdSizeSP = model.Size_product;
                if (Product.Code == "" || Product.Code == null)
                {
                    Product.Code = (KitudauNhomSP + IdColorSP + IdSizeSP) + '-' + (Erp.BackOffice.Helpers.Common.GetOrderNo("Product", model.Code));
                }
                else
                {
                    Product.Code = model.Code;
                }
                if (model.NHOMSANPHAM_ID_LST != null || model.NHOMSANPHAM_ID_LST == null)
                {
                    Product.NHOMSANPHAM_ID_LST = string.Join(",", model.NHOMSANPHAM_ID);
                }
                if (model.LOAISANPHAM_ID_LST != null || model.NHOMSANPHAM_ID_LST == null)
                {
                    Product.LOAISANPHAM_ID_LST = string.Join(",", model.LOAISANPHAM_ID);
                }
                //begin kiem tra trung ma

                var product1 = productRepository.GetAllProduct()
              .Where(item => item.Id != Product.Id && item.Code == model.Code && (item.color == Product.color && item.Size == Product.Size)).FirstOrDefault();
                if (product1 != null)
                {
                    ViewBag.errors = "Trùng mã sản phẩm";
                    TempData[Globals.SuccessMessageKey] = "Trùng mã sản phẩm";
                    ViewBag.ErrorMesseage = "Trùng mã sản phẩm";
                    return RedirectToAction("Edit", new { Id = model.Id, IsPopup = Request["IsPopup"] });

                }
                //end kiem tra trung ma
                Product.URL_SLUG = model.URL_SLUG;
                //begin up hinh anh cho backend
                var path = Helpers.Common.GetSetting("product-image-folder");
                var path2 = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("product-image-folder"));
                var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                if (Request.Files["file-imagepos"] != null)
                {
                    var file = Request.Files["file-imagepos"];
                    if (file.ContentLength > 0)
                    {
                        //FileInfo fi = new FileInfo(Server.MapPath("~" + path) + Product.Image_Pos);
                        //if (fi.Exists)
                        //{
                        //    fi.Delete();
                        //}

                        string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_pos_" + file.FileName;

                        bool isExists = System.IO.Directory.Exists(path2);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(path2);
                        file.SaveAs(path2 + image_name);
                        Product.Image_Pos = image_name;
                    }
                    else
                    {
                        Product.Image_Pos = imgpos;
                    }
                }
                if (Request.Files["file-image"] != null)
                {
                    int count = Request.Files.Count;
                    string nameimgs = "";
                    for (int i = 0; i < count; i++)
                    {
                        if (listimgdelete[i] != "1")
                        {
                            if (i == count - 1)
                            {
                                var b = Request.Files[i];


                                if (b.ContentLength > 0)
                                {
                                    string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + b.FileName;
                                    bool isExists = System.IO.Directory.Exists(path2);
                                    if (!isExists)
                                        System.IO.Directory.CreateDirectory(path2);
                                    nameimgs += image_name;
                                    b.SaveAs(path2 + image_name);



                                }
                            }
                            else
                            {
                                var a = Request.Files[i];


                                if (a.ContentLength > 0)
                                {
                                    //tồn tại hình mới thì xóa toàn bộ hình cũ
                                    //foreach (var item in listimg)
                                    //{
                                    //    FileInfo fi = new FileInfo(Server.MapPath("~" + path) + item);
                                    //    if (fi.Exists)
                                    //    {
                                    //        fi.Delete();
                                    //    }
                                    //}

                                    string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + a.FileName;
                                    if (i == 0)
                                    {
                                        Product.Image_Name = image_name;
                                    }
                                    bool isExists = System.IO.Directory.Exists(path2);
                                    if (!isExists)
                                        System.IO.Directory.CreateDirectory(path2);
                                    nameimgs += image_name + ",";
                                    a.SaveAs(path2 + image_name);


                                }
                            }
                        }
                    }
                    var aa = Request.Files[0];


                    if (nameimgs.Length > 1)
                    {
                        nameimgs = nameimgs.Substring(0, nameimgs.Length - 1);
                    }

                    if (aa.ContentLength > 0)
                    {
                        Product.List_Image = nameimgs;
                    }
                    else
                    {
                        string imgnamenew = "";
                        for (int j = 0; j < listimg.Count(); j++)
                        {
                            if (listimgdelete[j] != "1")
                            {
                                imgnamenew += listimg[j] + ",";

                            }
                            else
                            {
                                //FileInfo fi = new FileInfo(Server.MapPath("~" + path) + listimg[j]);
                                //if (fi.Exists)
                                //{
                                //    fi.Delete();
                                //}
                            }

                        }

                        if (imgnamenew.Length > 1)
                        {
                            imgnamenew = imgnamenew.Substring(0, imgnamenew.Length - 1);
                        }
                        Product.List_Image = imgnamenew;
                    }

                    //end up hinh anh cho backend


                    //begin up hinh anh cho client
                    path = Helpers.Common.GetSetting("product-image-folder-client");
                    string prjClient = Helpers.Common.GetSetting("prj-client");
                    //string prjClient = "KISSY_CLIENT";
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                    DirectoryInfo di = new DirectoryInfo(filepath);
                    if (prjClient.Trim() == "")
                    {
                        filepath = di.Parent.FullName + path.Replace("/", @"\");
                    }
                    else
                    {
                        filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                    }

                    if (Request.Files["file-image"] != null)
                    {

                        for (int i = 0; i < count; i++)
                        {
                            if (listimgdelete[i] != "1")
                            {
                                if (i == count - 1)
                                {
                                    var b = Request.Files[i];


                                    if (b.ContentLength > 0)
                                    {
                                        string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + b.FileName;
                                        bool isExists = System.IO.Directory.Exists(filepath);
                                        if (!isExists)
                                            System.IO.Directory.CreateDirectory(filepath);
                                        nameimgs += image_name;
                                        b.SaveAs(filepath + image_name);


                                    }
                                }
                                else
                                {
                                    var a = Request.Files[i];


                                    if (a.ContentLength > 0)
                                    {
                                        //tồn tại hình mới thì xóa toàn bộ hình cũ
                                        //foreach (var item in listimg)
                                        //{
                                        //    FileInfo fi = new FileInfo(filepath + item);
                                        //    if (fi.Exists)
                                        //    {
                                        //        fi.Delete();
                                        //    }
                                        //}

                                        string image_name = "product_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(Product.Code, @"\s+", "_")) + "_" + a.FileName;
                                        bool isExists = System.IO.Directory.Exists(filepath);
                                        if (!isExists)
                                            System.IO.Directory.CreateDirectory(filepath);
                                        nameimgs += image_name + ",";
                                        a.SaveAs(filepath + image_name);


                                    }
                                }
                            }
                        }
                    }
                    //for (int j = 0; j < listimg.Count(); j++)
                    //{
                    //    if (listimgdelete[j] == "1")
                    //    {
                    //        FileInfo fi = new FileInfo(filepath + listimg[j]);
                    //        if (fi.Exists)
                    //        {
                    //            fi.Delete();
                    //        }
                    //    }


                    //}
                    //end up hinh anh cho client

                    productRepository.UpdateProduct(Product);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("/Index");
                }
            }
            return View(model);
        }

        #endregion

        #region NewProduct
        public ViewResult NewProduct(string txtSearch, string txtCode, string CategoryCode, string ProductGroup, int? NHOMSANPHAM_ID, SearchObjectAttributeViewModel SearchOjectAttr)
        {
            var nhomSP = new DM_NHOMSANPHAM();
            //List<Product> q = productRepository.GetlistAllProduct().Where(m=>m.IS_NEW == true).ToList();
            //List<ProductViewModel> models = new List<ProductViewModel>();
            //foreach (var item in q) {
            //    ProductViewModel product = new ProductViewModel();
            //    product.Id = item.Id;
            //    product.CreatedUserId = item.CreatedUserId;
            //    product.CreatedDate = item.CreatedDate;
            //    product.ModifiedUserId = item.ModifiedUserId;
            //    product.ModifiedDate = item.ModifiedDate;
            //    product.Name = item.Name;
            //    product.Code = item.Code;
            //    product.PriceInbound = item.PriceInbound;
            //    product.PriceOutbound = item.PriceOutbound;
            //    product.Barcode = item.Barcode;
            //    product.Type = item.Type;
            //    product.Unit = item.Unit;
            //    CategoryCode = item.CategoryCode;
            //    product.DiscountStaff = item.DiscountStaff;
            //    product.IsMoneyDiscount = item.IsMoneyDiscount;
            //    product.IS_NEW = item.IS_NEW;
            //    product.STT_ISNEW = item.STT_ISNEW;
            //    models.Add(product);
            //};
            IEnumerable<ProductViewModel> q = productRepository.GetAllvwProduct().AsEnumerable()
            .Select(item => new ProductViewModel
            {

                Id = item.Id,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Name = item.Name,
                Code = item.Code,
                PriceInbound = item.PriceInbound,
                PriceOutbound = item.PriceOutbound,
                Barcode = item.Barcode,
                Type = item.Type,
                Unit = item.Unit,
                NHOMSANPHAM_ID = item.NHOMSANPHAM_ID,
                ProductGroupName = item.ProductGroupName,
                CategoryCode = item.CategoryCode,
                DiscountStaff = item.DiscountStaff,
                IsMoneyDiscount = item.IsMoneyDiscount,
                IS_NEW = item.IS_NEW,
                STT_ISNEW = item.STT_ISNEW,
                NhomCha = item.NhomCha

            }).Where(m => m.IS_NEW == true).OrderByDescending(m => m.STT_ISNEW);

            if (NHOMSANPHAM_ID > 0)
            {

                var AllNhom = productRepository.GetAllvwProduct().Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                if (AllNhom.Any())
                {
                    q = q.Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                }
                else
                {
                    q = q.Where(x => x.NHOMSANPHAM_ID == NHOMSANPHAM_ID).ToList();
                }


            }


            if (string.IsNullOrEmpty(txtSearch) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch).Trim();
                //txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLower().Contains(txtSearch));
            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }

            //decimal minPriceInbound;
            //if (decimal.TryParse(txtMinPrice, out minPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound >= minPriceInbound);
            //}

            //decimal maxPriceInbound;
            //if (decimal.TryParse(txtMaxPrice, out maxPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound <= maxPriceInbound);
            //}


            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }


        #endregion


        #region HotProduct
        public ViewResult HotProduct(string txtSearch, string txtCode, string CategoryCode, string ProductGroup, int? NHOMSANPHAM_ID, SearchObjectAttributeViewModel SearchOjectAttr)
        {
            var nhomSP = new DM_NHOMSANPHAM();
            //List<Product> q = productRepository.GetlistAllProduct().Where(m=>m.IS_NEW == true).ToList();
            //List<ProductViewModel> models = new List<ProductViewModel>();
            //foreach (var item in q) {
            //    ProductViewModel product = new ProductViewModel();
            //    product.Id = item.Id;
            //    product.CreatedUserId = item.CreatedUserId;
            //    product.CreatedDate = item.CreatedDate;
            //    product.ModifiedUserId = item.ModifiedUserId;
            //    product.ModifiedDate = item.ModifiedDate;
            //    product.Name = item.Name;
            //    product.Code = item.Code;
            //    product.PriceInbound = item.PriceInbound;
            //    product.PriceOutbound = item.PriceOutbound;
            //    product.Barcode = item.Barcode;
            //    product.Type = item.Type;
            //    product.Unit = item.Unit;
            //    CategoryCode = item.CategoryCode;
            //    product.DiscountStaff = item.DiscountStaff;
            //    product.IsMoneyDiscount = item.IsMoneyDiscount;
            //    product.IS_NEW = item.IS_NEW;
            //    product.STT_ISNEW = item.STT_ISNEW;
            //    models.Add(product);
            //};
            IEnumerable<ProductViewModel> q = productRepository.GetAllvwProduct().AsEnumerable()
            .Select(item => new ProductViewModel
            {
                Id = item.Id,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Name = item.Name,
                Code = item.Code,
                PriceInbound = item.PriceInbound,
                PriceOutbound = item.PriceOutbound,
                Barcode = item.Barcode,
                Type = item.Type,
                Unit = item.Unit,
                CategoryCode = item.CategoryCode,
                DiscountStaff = item.DiscountStaff,
                IsMoneyDiscount = item.IsMoneyDiscount,
                NHOMSANPHAM_ID = item.NHOMSANPHAM_ID,
                ProductGroupName = item.ProductGroupName,
                IS_HOT = item.IS_HOT,
                STT_ISHOT = item.STT_ISHOT,
                NhomCha = item.NhomCha

            }).Where(m => m.IS_HOT == true).OrderByDescending(m => m.STT_ISHOT);


            if (NHOMSANPHAM_ID > 0)
            {

                var AllNhom = productRepository.GetAllvwProduct().Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                if (AllNhom.Any())
                {
                    q = q.Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                }
                else
                {
                    q = q.Where(x => x.NHOMSANPHAM_ID == NHOMSANPHAM_ID).ToList();
                }


            }


            if (string.IsNullOrEmpty(txtSearch) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch).Trim();
                //txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLower().Contains(txtSearch));
            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }
            //decimal minPriceInbound;
            //if (decimal.TryParse(txtMinPrice, out minPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound >= minPriceInbound);
            //}

            //decimal maxPriceInbound;
            //if (decimal.TryParse(txtMaxPrice, out maxPriceInbound))
            //{
            //    q = q.Where(x => x.PriceInbound <= maxPriceInbound);
            //}


            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }

        //Add hot products
        public ViewResult IndexSearch4(string txtSearch, string txtCode, string CategoryCode, string ProductGroup, int? NHOMSANPHAM_ID, SearchObjectAttributeViewModel SearchOjectAttr)
        {
            //IEnumerable<ProductViewModel> qsamesize = productRepository.GetAllProduct().AsEnumerable().Where(item => item.Id == pruductid)
            // .Select(item => new ProductViewModel
            // {
            //     Id = item.Id,
            // }).ToList();
            var nhomSP = new DM_NHOMSANPHAM();
            IEnumerable<ProductViewModel> q = productRepository.GetAllvwProduct().Where(m => m.IS_HOT != true).AsEnumerable()
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    PriceOutbound = item.PriceOutbound,
                    Barcode = item.Barcode,
                    Type = item.Type,
                    Unit = item.Unit,
                    CategoryCode = item.CategoryCode,
                    DiscountStaff = item.DiscountStaff,
                    IsMoneyDiscount = item.IsMoneyDiscount,
                    NHOMSANPHAM_ID = item.NHOMSANPHAM_ID,
                    ProductGroupName = item.ProductGroupName,
                    NhomCha = item.NhomCha

                }).OrderByDescending(m => m.Id);

            if (SearchOjectAttr.ListField != null)
            {
                if (SearchOjectAttr.ListField.Count > 0)
                {
                    //lấy các đối tượng ObjectAttributeValue nào thỏa đk có AttributeId trong ListField và có giá trị tương ứng trong ListField
                    var listObjectAttrValue = ObjectAttributeRepository.GetAllObjectAttributeValue().AsEnumerable().Where(attr => SearchOjectAttr.ListField.Any(item => item.Id == attr.AttributeId && Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(attr.Value).Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Value)))).ToList();

                    //tiếp theo tìm các sản phẩm có id bằng với ObjectId trong listObjectAttrValue vừa tìm được
                    q = q.Where(product => listObjectAttrValue.Any(item => item.ObjectId == product.Id));

                    ViewBag.ListOjectAttrSearch = new JavaScriptSerializer().Serialize(SearchOjectAttr.ListField.Select(x => new { Id = x.Id, Value = x.Value }));
                }
            }

            if (NHOMSANPHAM_ID > 0)
            {

                var AllNhom = productRepository.GetAllvwProduct().Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                if (AllNhom.Any())
                {
                    q = q.Where(x => x.NhomCha == NHOMSANPHAM_ID).ToList();
                }
                else
                {
                    q = q.Where(x => x.NHOMSANPHAM_ID == NHOMSANPHAM_ID).ToList();
                }


            }


            if (string.IsNullOrEmpty(txtSearch) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch).Trim();
                //txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLower().Contains(txtSearch));
            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }
            //var group = q.GroupBy(x => x.CategoryCode).ToList();
            //int dem = 1;
            //foreach (var item in group)
            //{
            //    Category category = new Category();
            //    category.IsDeleted = false;
            //    category.CreatedUserId = WebSecurity.CurrentUserId;
            //    category.ModifiedUserId = WebSecurity.CurrentUserId;
            //    category.CreatedDate = DateTime.Now;
            //    category.ModifiedDate = DateTime.Now;
            //    category.Value = item.Key;
            //    category.Name = item.Key;
            //    category.Code = "product";
            //    category.OrderNo = dem;
            //    categoryRepository.InsertCategory(category);
            //    dem++;
            //}
            //nếu có tìm kiếm nâng cao thì lọc trước



            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }

        [HttpPost]
        public ActionResult chonsanpham4()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = productRepository.GetProductById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("/Index");
                        //}

                        item.IS_HOT = true;
                        productRepository.UpdateProduct(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;

                return RedirectToAction("/IndexSearch4", new { IsPopup = false, Test = "abc" });

            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("/Index");
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateSTTProductHot(List<HotProductViewModel> listsp)
        {

            try
            {

                if (listsp != null && listsp.Count > 0)
                {

                    foreach (var item in listsp)
                    {
                        if (item.STT_Moi > 0)
                        {
                            var Product = productRepository.GetProductById(item.Id);
                            Product.STT_ISHOT = item.STT_Moi;
                            productRepository.UpdateProduct(Product);
                        }
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    }
                }

                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }
        public ViewResult UpdateSTTProductHot()
        {

            //List<CustomerTest> list;
            //var model = new ProductViewModel();
            IEnumerable<ProductViewModel> q = productRepository.GetAllvwProduct().AsEnumerable()
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    PriceOutbound = item.PriceOutbound,
                    Barcode = item.Barcode,
                    Type = item.Type,
                    Unit = item.Unit,
                    CategoryCode = item.CategoryCode,
                    DiscountStaff = item.DiscountStaff,
                    IsMoneyDiscount = item.IsMoneyDiscount,
                    IS_HOT = item.IS_HOT,
                    STT_ISHOT = item.STT_ISHOT
                }).Where(m => m.IS_HOT == true).OrderByDescending(m => m.STT_ISHOT).ToList();
            ViewBag.listProduct = q;

            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];

            return View();
        }


        //DeleteHotProduct
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult DeleteHotProduct(string id)
        {

            try
            {
                var Product = productRepository.GetProductById(int.Parse(id));
                Product.IS_HOT = false;
                if (Product.IS_HOT == false)
                {
                    Product.STT_ISHOT = null;
                }
                productRepository.UpdateProduct(Product);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;


                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete()
        {
            try
            {

                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {

                    var item = productRepository.GetProductById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    var numberCheck = Domain.Helper.SqlHelper.ExecuteScalarSPSotre("spCheckDeleteProduct", new { StoreProductId = int.Parse(arrDeleteId[i]) });
                    //var item = Domain.Helper.SqlHelper.QuerySP<ProductViewModel>("spCheckDeleteProduct", new
                    //{
                    //    ProductId = int.Parse(arrDeleteId[i])

                    //}).ToList();

                    int xx = int.Parse(numberCheck);
                    if (item != null && xx == 1)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                    }
                    else if (item != null && xx == 0)
                    {

                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("/Index");
                        //}

                        item.IsDeleted = true;
                        productRepository.UpdateProduct(item);
                    }
                }


                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("/Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("/Index");
            }
        }



        [HttpPost]
        public ActionResult chonsanpham(int pruductid)
        {
            try
            {
                //ViewBag.JavaScriptFunction =  ;//string.Format("ShowServerDateTime('{0}');", DateTime.Now.ToString());
                //model.JavascriptToRun = "ShowErrorPopup()";
                //string pruductid = Request["pruductid"];

                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {

                    var Product_samegroup = new Domain.Sale.Entities.Sale_Product_samegroup();
                    Product_samegroup.IsDeleted = false;
                    Product_samegroup.CreatedUserId = WebSecurity.CurrentUserId;
                    Product_samegroup.ModifiedUserId = WebSecurity.CurrentUserId;
                    Product_samegroup.CreatedDate = DateTime.Now;
                    Product_samegroup.ModifiedDate = DateTime.Now;
                    Product_samegroup.Product_id = pruductid;
                    Product_samegroup.Product_idsame = int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture);
                    //productRepository.
                    productRepository.InsertProduct_samegroupt(Product_samegroup);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                //return RedirectToAction("Edit", new { Id = pruductid, IsPopup = false }); 
                return RedirectToAction("IndexSearch", new { Id = pruductid, IsPopup = false, Test = "abc" });
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("/Index");
            }
        }


        [HttpPost]
        public ActionResult chonsanpham2(int pruductid)
        {
            try
            {

                //string pruductid = Request["pruductid"];

                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {

                    var Product_samesize = new Domain.Sale.Entities.Sale_Product_samesize();
                    Product_samesize.IsDeleted = false;
                    Product_samesize.CreatedUserId = WebSecurity.CurrentUserId;
                    Product_samesize.ModifiedUserId = WebSecurity.CurrentUserId;
                    Product_samesize.CreatedDate = DateTime.Now;
                    Product_samesize.ModifiedDate = DateTime.Now;
                    Product_samesize.Product_id = pruductid;
                    Product_samesize.Product_idsame = int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture);
                    //productRepository.
                    productRepository.InsertProduct_samesize(Product_samesize);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("IndexSearch2", new { Id = pruductid, IsPopup = Request["IsPopup"], Test = "1" });
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("/Index");
            }
        }

        [HttpPost]
        public ActionResult chonsanpham3()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = productRepository.GetProductById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("/Index");
                        //}

                        item.IS_NEW = true;
                        productRepository.UpdateProduct(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;

                return RedirectToAction("/IndexSearch3", new { IsPopup = false, Test = "abc" });

            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("/Index");
            }

        }
        #endregion

        #region  - Json -
        public JsonResult GetListJson()
        {
            var list = productRepository.GetAllProduct().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetList1Json()
        {
            var list = productRepository.GetAllvwProductsamegroup().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CheckSKU(string ColorSKU, string SizeSKU, string IdParent)
        {

            try
            {
                if (ColorSKU != null && SizeSKU != null && IdParent != null)
                {


                    var Idcha = int.Parse(IdParent);
                    var SKUId = productRepository.GetAllvwProductSKU().Where(n => n.Product_id == Idcha).FirstOrDefault();
                    var ProductSKU3 = new Domain.Sale.Entities.Sale_Product_SKU();
                    if (SKUId == null)
                    {
                        ProductSKU3.CreatedUserId = WebSecurity.CurrentUserId;
                        ProductSKU3.ModifiedUserId = WebSecurity.CurrentUserId;
                        ProductSKU3.CreatedDate = DateTime.Now;
                        ProductSKU3.ModifiedDate = DateTime.Now;
                        ProductSKU3.AssignedUserId = WebSecurity.CurrentUserId;
                        ProductSKU3.IsDeleted = false;
                        ProductSKU3.color = ColorSKU;
                        ProductSKU3.Size = SizeSKU;
                        ProductSKU3.Product_id = Idcha;
                        ProductSKU3.Product_idSKU = Idcha;
                        productRepository.InsertProduct_SKU(ProductSKU3);
                        return Json(3);
                    }
                    else
                    {
                        var IdProduct = productRepository.GetAllvwProductSKU().Where(n => n.Product_idSKU == SKUId.Product_idSKU && (n.color == ColorSKU && n.Size == SizeSKU)).FirstOrDefault();
                        if (IdProduct != null)
                        {
                            ViewBag.errors = "Trùng màu và size";
                            TempData[Globals.SuccessMessageKey] = "Trùng màu và size";
                            ViewBag.ErrorMesseage = "Trùng màu và size";
                            return Json(0);

                        }
                    }


                }

                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }
        #endregion

        #region DM_NhomSanPham
        public ViewResult DM_NhomSanPham()
        {
            var model = new DM_NHOMSANPHAMViewModel();

            //IEnumerable<DM_NHOMSANPHAMViewModel> list = new List<DM_NHOMSANPHAMViewModel>();
            var list = dm_nhomsanphamRepository.GetAllDM_NHOMSANPHAM().AsEnumerable()
            .Select(item => new DM_NHOMSANPHAMViewModel
            {
                NHOMSANPHAM_ID = item.NHOMSANPHAM_ID,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AssignedUserId = item.AssignedUserId,
                TEN_NHOMSANPHAM = item.TEN_NHOMSANPHAM,
                CAP_NHOMSANPHAM = item.CAP_NHOMSANPHAM,
                STT = item.STT,
                NHOM_CHA = item.NHOM_CHA,
                BANNER = item.BANNER,
                IS_SHOW = item.IS_SHOW,
                META_TITLE = item.META_TITLE,
                META_DESCRIPTION = item.META_DESCRIPTION,
                URL_SLUG = item.URL_SLUG
            }).OrderBy(x => x.STT).ToList();
            //list = AutoMapper.Mapper.Map(model, list);


            ViewBag.DM_NHOMSANPHAM = list;
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateNhomSanPham(DM_NHOMSANPHAMViewModel model)
        {

            if (model.STT != null && model.TEN_NHOMSANPHAM != null)
            {

                //**create**//
                if (model.NHOMSANPHAM_ID == 0)
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        #region Create
                        var dm_nhomsanpham = new Domain.Sale.Entities.DM_NHOMSANPHAM();
                        AutoMapper.Mapper.Map(model, dm_nhomsanpham);
                        dm_nhomsanpham.IsDeleted = false;
                        dm_nhomsanpham.CreatedUserId = WebSecurity.CurrentUserId;
                        dm_nhomsanpham.ModifiedUserId = WebSecurity.CurrentUserId;
                        dm_nhomsanpham.CreatedDate = DateTime.Now;
                        dm_nhomsanpham.ModifiedDate = DateTime.Now;
                        dm_nhomsanpham.AssignedUserId = WebSecurity.CurrentUserId;

                        dm_nhomsanpham.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);//???

                        var nhomsanpham = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByTEN_NHOMSANPHAM(model.TEN_NHOMSANPHAM);

                        if (nhomsanpham != null)
                        {
                            TempData[Globals.FailedMessageKey] = "Tên nhóm hàng đã tồn tại";
                            return RedirectToAction("DM_NhomSanPham");
                        }

                        if (model.NHOM_CHA == null)
                        {
                            dm_nhomsanpham.CAP_NHOMSANPHAM = 1;
                        }
                        else
                        {
                            if (model.CAP_NHOMSANPHAM < 3)
                            {
                                var nhomcha = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOM_CHA.Value);
                                dm_nhomsanpham.CAP_NHOMSANPHAM = nhomcha.CAP_NHOMSANPHAM + 1;
                            }
                            else
                            {
                                TempData[Globals.FailedMessageKey] = "Nhóm sản phẩm bạn chọn thuộc cấp 3";
                                return RedirectToAction("DM_NhomSanPham");
                            }

                        }

                        dm_nhomsanpham.BANNER = "";
                        dm_nhomsanphamRepository.InsertDM_NHOMSANPHAM(dm_nhomsanpham);
                        dm_nhomsanpham = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(dm_nhomsanpham.NHOMSANPHAM_ID);

                        //begin up hinh anh cho backend
                        var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("productgroups-image-folder"));
                        if (Request.Files["file-image"] != null)
                        {
                            var file = Request.Files["file-image"];
                            if (file.ContentLength > 0)
                            {
                                string image_name = "productgroups_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_nhomsanpham.NHOMSANPHAM_ID.ToString(), @"\s+", "_")) + file.FileName;
                                bool isExists = System.IO.Directory.Exists(path);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(path);
                                file.SaveAs(path + image_name);
                                dm_nhomsanpham.BANNER = image_name;
                            }
                        }
                        //end up hinh anh cho backend

                        //begin up hinh anh cho client
                        path = Helpers.Common.GetSetting("productgroups-image-folder-client");
                        string prjClient = Helpers.Common.GetSetting("prj-client");
                        string filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                        DirectoryInfo di = new DirectoryInfo(filepath);
                        filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                        if (Request.Files["file-image"] != null)
                        {
                            var file = Request.Files["file-image"];
                            if (file.ContentLength > 0)
                            {
                                //FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_nhomsanpham.BANNER);
                                //if (fi.Exists)
                                //{
                                //    fi.Delete();
                                //}

                                string image_name = "productgroups_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_nhomsanpham.NHOMSANPHAM_ID.ToString(), @"\s+", "_")) + file.FileName;

                                bool isExists = System.IO.Directory.Exists(filepath);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(filepath);
                                file.SaveAs(filepath + image_name);
                                dm_nhomsanpham.BANNER = image_name;
                            }
                        }
                        //end up hinh anh cho client

                        dm_nhomsanphamRepository.UpdateDM_NHOMSANPHAM(dm_nhomsanpham);

                        scope.Complete();

                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        return RedirectToAction("DM_NhomSanPham");
                        #endregion
                    }
                }
                else//**edit**//
                {
                    #region edit
                    var dm_nhomsanpham = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOMSANPHAM_ID);
                    dm_nhomsanpham.ModifiedUserId = WebSecurity.CurrentUserId;
                    dm_nhomsanpham.ModifiedDate = DateTime.Now;
                    dm_nhomsanpham.AssignedUserId = WebSecurity.CurrentUserId;
                    dm_nhomsanpham.TEN_NHOMSANPHAM = model.TEN_NHOMSANPHAM;
                    dm_nhomsanpham.NHOM_CHA = model.NHOM_CHA;
                    dm_nhomsanpham.STT = model.STT;
                    dm_nhomsanpham.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);
                    dm_nhomsanpham.META_TITLE = model.META_TITLE;
                    dm_nhomsanpham.META_DESCRIPTION = model.META_DESCRIPTION;
                    dm_nhomsanpham.URL_SLUG = model.URL_SLUG;

                    var oldItem = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOMSANPHAM_ID);
                    if (oldItem.TEN_NHOMSANPHAM != model.TEN_NHOMSANPHAM)
                    {
                        var checkTen = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByTEN_NHOMSANPHAM(model.TEN_NHOMSANPHAM);
                        if (checkTen != null)
                        {
                            TempData[Globals.FailedMessageKey] = "Tên nhóm hàng đã tồn tại";
                            return RedirectToAction("DM_NhomSanPham");
                        }
                    }

                    if (model.NHOM_CHA == null)
                    {
                        dm_nhomsanpham.CAP_NHOMSANPHAM = 1;
                    }
                    else
                    {
                        if (model.CAP_NHOMSANPHAM < 3)
                        {
                            if (model.NHOMSANPHAM_ID != model.NHOM_CHA)
                            {
                                var nhomcha = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOM_CHA.Value);
                                dm_nhomsanpham.CAP_NHOMSANPHAM = nhomcha.CAP_NHOMSANPHAM + 1;
                            }
                            else
                            {
                                TempData[Globals.FailedMessageKey] = "Nhóm sản phẩm không thể nằm trong chính nó";
                                return RedirectToAction("DM_NhomSanPham");
                            }
                        }
                        else
                        {
                            TempData[Globals.FailedMessageKey] = "Nhóm sản phẩm chỉ có 3 cấp";
                            return RedirectToAction("DM_NhomSanPham");
                        }

                    }

                    var path = Helpers.Common.GetSetting("productgroups-image-folder");
                    var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            //FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_nhomsanpham.BANNER);
                            //if (fi.Exists)
                            //{
                            //    fi.Delete();
                            //}

                            string image_name = "productgroups_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_nhomsanpham.NHOMSANPHAM_ID.ToString(), @"\s+", "_")) + file.FileName;

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_nhomsanpham.BANNER = image_name;
                        }
                    }







                    //begin up hinh anh cho client
                    path = Helpers.Common.GetSetting("productgroups-image-folder-client");
                    string prjClient = Helpers.Common.GetSetting("prj-client");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                    DirectoryInfo di = new DirectoryInfo(filepath);
                    filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            //FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_nhomsanpham.BANNER);
                            //if (fi.Exists)
                            //{
                            //    fi.Delete();
                            //}

                            string image_name = "productgroups_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_nhomsanpham.NHOMSANPHAM_ID.ToString(), @"\s+", "_")) + file.FileName;

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_nhomsanpham.BANNER = image_name;
                        }
                    }
                    //end up hinh anh cho client


                    dm_nhomsanphamRepository.UpdateDM_NHOMSANPHAM(dm_nhomsanpham);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("DM_NhomSanPham");
                    #endregion
                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteNhomSanPham()
        {
            int id = int.Parse(Request["Id"], CultureInfo.InvariantCulture);
            dm_nhomsanphamRepository.DeleteDM_NHOMSANPHAM(id);

            var subList = dm_nhomsanphamRepository.GetAllDM_NHOMSANPHAMByNHOM_CHA(id).ToList();

            foreach (var item in subList)
            {
                var subList2 = dm_nhomsanphamRepository.GetAllDM_NHOMSANPHAMByNHOM_CHA(item.NHOMSANPHAM_ID).ToList();
                foreach (var item2 in subList2)
                {
                    dm_nhomsanphamRepository.DeleteDM_NHOMSANPHAM(item2.NHOMSANPHAM_ID);
                }
                dm_nhomsanphamRepository.DeleteDM_NHOMSANPHAM(item.NHOMSANPHAM_ID);
            }


            TempData["AlertMessage"] = App_GlobalResources.Wording.DeleteSuccess;
            return RedirectToAction("DM_NhomSanPham");
        }
        #endregion

        #region DM_BESTSELLER
        public ViewResult DM_BestSeller()
        {
            var model = new DM_BEST_SELLERViewModel();

            var list = dm_bestsellerRepository.GetAllDM_BEST_SELLER().AsEnumerable()
            .Select(item => new DM_BEST_SELLERViewModel
            {
                BEST_SELLER_ID = item.BEST_SELLER_ID,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AssignedUserId = item.AssignedUserId,
                Product_Id = item.Product_Id,
                STT = item.STT,
                IS_SHOW = item.IS_SHOW
            }).OrderBy(x => x.STT).ToList();
            //model.bestsellerList = list;

            var productList = productRepository.GetAllvwProduct().AsEnumerable()
            .Select(item => new ProductViewModel
            {
                Id = item.Id,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Name = item.Name,
                Code = item.Code,
                PriceInbound = item.PriceInbound,
                PriceOutbound = item.PriceOutbound,
                Barcode = item.Barcode,
                Type = item.Type,
                Unit = item.Unit,
                CategoryCode = item.CategoryCode,
                DiscountStaff = item.DiscountStaff,
                IsMoneyDiscount = item.IsMoneyDiscount
            }).ToList();

            foreach (var item in list.ToList())
            {
                var product = productList.Find(x => x.Id == item.Product_Id);
                if (product != null)
                {
                    item.ProductName = product.Name;

                }
            }
            //model.bestsellerList = list;
            model.bestsellerList = list.ToList();
            ViewBag.ProductList = productList;
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateBestSeller(DM_BEST_SELLERViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.BEST_SELLER_ID == 0)
                {
                    var dm_bestseller = new Domain.Sale.Entities.DM_BEST_SELLER();
                    AutoMapper.Mapper.Map(model, dm_bestseller);
                    dm_bestseller.IsDeleted = false;
                    dm_bestseller.CreatedUserId = WebSecurity.CurrentUserId;
                    dm_bestseller.ModifiedUserId = WebSecurity.CurrentUserId;
                    dm_bestseller.CreatedDate = DateTime.Now;
                    dm_bestseller.ModifiedDate = DateTime.Now;
                    dm_bestseller.AssignedUserId = WebSecurity.CurrentUserId;

                    dm_bestsellerRepository.InsertDM_BEST_SELLER(dm_bestseller);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("DM_BestSeller");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBestSeller(DM_BEST_SELLERViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.bestsellerList.Any(x => x.BEST_SELLER_ID > 0))
                {
                    var q = model.bestsellerList.Where(x => x.BEST_SELLER_ID > 0).ToList();
                    foreach (var item in q)
                    {
                        var _update = dm_bestsellerRepository.GetDM_BEST_SELLERByBEST_SELLER_ID(item.BEST_SELLER_ID);
                        _update.ModifiedUserId = WebSecurity.CurrentUserId;
                        _update.ModifiedDate = DateTime.Now;
                        _update.AssignedUserId = WebSecurity.CurrentUserId;
                        _update.STT = item.STT;
                        _update.IS_SHOW = item.IS_SHOW;
                        dm_bestsellerRepository.UpdateDM_BEST_SELLER(_update);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                return RedirectToAction("DM_BestSeller");
            }
            return View();
        }

        public ActionResult DeleteBestSeller(int id)
        {
            try
            {
                var item = dm_bestsellerRepository.GetDM_BEST_SELLERByBEST_SELLER_ID(id);
                if (item != null)
                {
                    dm_bestsellerRepository.DeleteDM_BEST_SELLER(item.BEST_SELLER_ID);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("DM_BestSeller");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("DM_BestSeller");
            }
        }
        #endregion

        #region DM_LoaiSanPham
        public ViewResult DM_LoaiSanPham()
        {
            var model = new DM_LOAISANPHAMViewModel();

            //IEnumerable<DM_NHOMSANPHAMViewModel> list = new List<DM_NHOMSANPHAMViewModel>();
            var list = dm_loaisanphamRepository.GetAllDM_LOAISANPHAM().AsEnumerable()
            .Select(item => new DM_LOAISANPHAMViewModel
            {
                LOAISANPHAM_ID = item.LOAISANPHAM_ID,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AssignedUserId = item.AssignedUserId,
                TEN_LOAISANPHAM = item.TEN_LOAISANPHAM,
                //CAP_NHOMSANPHAM = item.CAP_NHOMSANPHAM,
                STT = item.STT,
                LOAISANPHAM_IDCHA = item.LOAISANPHAM_IDCHA,
                ANH_DAIDIEN = item.ANH_DAIDIEN,
                IS_SHOWMAIN = item.IS_SHOWMAIN,
                IsDeleted = item.IsDeleted,
                META_TITLE = item.META_TITLE,
                META_DESCRIPTION = item.META_DESCRIPTION,
                URL_SLUG = item.URL_SLUG
            }).OrderBy(x => x.STT).ToList();
            //list = AutoMapper.Mapper.Map(model, list);


            ViewBag.DM_LOAISANPHAM = list;
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateLoaiSanPham(DM_LOAISANPHAMViewModel model)
        {
            //if (model.LOAISANPHAM_IDCHA == null && model.STT != null && model.TEN_LOAISANPHAM != null)
            //{
            //    ModelState.IsValid = true;
            //}
            if (ModelState.IsValid)
            {

                //**create**//
                if (model.LOAISANPHAM_ID == 0)
                {

                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {

                        var dm_loaisanpham = new Domain.Sale.Entities.DM_LOAISANPHAM();
                        AutoMapper.Mapper.Map(model, dm_loaisanpham);
                        dm_loaisanpham.IsDeleted = false;
                        dm_loaisanpham.CreatedUserId = WebSecurity.CurrentUserId;
                        dm_loaisanpham.ModifiedUserId = WebSecurity.CurrentUserId;
                        dm_loaisanpham.CreatedDate = DateTime.Now;
                        dm_loaisanpham.ModifiedDate = DateTime.Now;
                        dm_loaisanpham.AssignedUserId = WebSecurity.CurrentUserId;


                        //thong tin SEO
                        dm_loaisanpham.META_TITLE = model.META_TITLE;
                        dm_loaisanpham.META_DESCRIPTION = model.META_DESCRIPTION;
                        dm_loaisanpham.URL_SLUG = model.URL_SLUG;

                        dm_loaisanpham.IS_SHOWMAIN = Convert.ToInt32(Request["is-show-checkbox"]);//???
                        dm_loaisanpham.ANH_DAIDIEN = "";
                        var loaisanpham = dm_loaisanphamRepository.GetDM_LOAISANPHAMByTEN_LOAISANPHAM(model.TEN_LOAISANPHAM);

                        if (loaisanpham != null)
                        {
                            TempData[Globals.FailedMessageKey] = "Tên loại hàng đã tồn tại";
                            return RedirectToAction("DM_LoaiSanPham");
                        }


                        dm_loaisanphamRepository.InsertDM_LOAISANPHAM(dm_loaisanpham);
                        dm_loaisanpham = dm_loaisanphamRepository.GetDM_LOAISANPHAMByLOAISANPHAM_ID(dm_loaisanpham.LOAISANPHAM_ID);


                        if (model.LOAISANPHAM_IDCHA == null)
                        {
                            //dm_nhomsanpham.CAP_NHOMSANPHAM = 1;
                        }
                        /* else
                         {
                            if (model.CAP_NHOMSANPHAM < 3)
                             {
                                 var nhomcha = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOM_CHA.Value);
                                 dm_nhomsanpham.CAP_NHOMSANPHAM = nhomcha.CAP_NHOMSANPHAM + 1;
                             }
                             else
                             {
                                 TempData[Globals.FailedMessageKey] = "Nhóm sản phẩm bạn chọn thuộc cấp 3";
                                 return RedirectToAction("DM_NhomSanPham");
                             }


                         }
                          */
                        //begin up hinh anh cho backend
                        var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("producttypes-image-folder"));
                        if (Request.Files["file-image"] != null)
                        {
                            var file = Request.Files["file-image"];
                            if (file.ContentLength > 0)
                            {
                                string image_name = "producttypes_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_loaisanpham.LOAISANPHAM_ID.ToString(), @"\s+", "_")) + file.FileName;
                                bool isExists = System.IO.Directory.Exists(path);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(path);
                                file.SaveAs(path + image_name);
                                dm_loaisanpham.ANH_DAIDIEN = image_name;
                            }
                        }
                        //begin up hinh anh cho backend



                        //begin up hinh anh cho client
                        path = Helpers.Common.GetSetting("producttypes-image-folder-client");
                        string prjClient = Helpers.Common.GetSetting("prj-client");
                        string filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                        DirectoryInfo di = new DirectoryInfo(filepath);
                        filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                        if (Request.Files["file-image"] != null)
                        {
                            var file = Request.Files["file-image"];
                            if (file.ContentLength > 0)
                            {
                                //FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_loaisanpham.ANH_DAIDIEN);
                                //if (fi.Exists)
                                //{
                                //    fi.Delete();
                                //}

                                string image_name = "producttypes_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_loaisanpham.LOAISANPHAM_ID.ToString(), @"\s+", "_")) + file.FileName;

                                bool isExists = System.IO.Directory.Exists(filepath);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(filepath);
                                file.SaveAs(filepath + image_name);
                                dm_loaisanpham.ANH_DAIDIEN = image_name;
                            }
                        }
                        //end up hinh anh cho client



                        dm_loaisanphamRepository.UpdateDM_LOAISANPHAM(dm_loaisanpham);
                        scope.Complete();

                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        return RedirectToAction("DM_LoaiSanPham");
                    }
                }
                else//**edit**//
                {
                    var dm_loaisanpham = dm_loaisanphamRepository.GetDM_LOAISANPHAMByLOAISANPHAM_ID(model.LOAISANPHAM_ID);
                    dm_loaisanpham.ModifiedUserId = WebSecurity.CurrentUserId;
                    dm_loaisanpham.ModifiedDate = DateTime.Now;
                    dm_loaisanpham.AssignedUserId = WebSecurity.CurrentUserId;
                    dm_loaisanpham.TEN_LOAISANPHAM = model.TEN_LOAISANPHAM;
                    dm_loaisanpham.LOAISANPHAM_IDCHA = model.LOAISANPHAM_IDCHA;
                    dm_loaisanpham.STT = model.STT;
                    dm_loaisanpham.IS_SHOWMAIN = Convert.ToInt32(Request["is-show-checkbox"]);
                    //thong tin SEO
                    dm_loaisanpham.META_TITLE = model.META_TITLE;
                    dm_loaisanpham.META_DESCRIPTION = model.META_DESCRIPTION;
                    dm_loaisanpham.URL_SLUG = model.URL_SLUG;

                    var oldItem = dm_loaisanphamRepository.GetDM_LOAISANPHAMByLOAISANPHAM_ID(model.LOAISANPHAM_ID);
                    if (oldItem.TEN_LOAISANPHAM != model.TEN_LOAISANPHAM)
                    {
                        var checkTen = dm_loaisanphamRepository.GetDM_LOAISANPHAMByTEN_LOAISANPHAM(model.TEN_LOAISANPHAM);
                        if (checkTen != null)
                        {
                            TempData[Globals.FailedMessageKey] = "Tên Loại hàng đã tồn tại";
                            return RedirectToAction("DM_LoaiSanPham");
                        }
                    }

                    /* if (model.LOAISANPHAM_IDCHA == null)
                     {
                         dm_nhomsanpham.CAP_NHOMSANPHAM = 1;
                     }
                     else
                     {
                         if (model.CAP_NHOMSANPHAM < 3)
                         {
                             if (model.NHOMSANPHAM_ID != model.NHOM_CHA)
                             {
                                 var nhomcha = dm_nhomsanphamRepository.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(model.NHOM_CHA.Value);
                                 dm_nhomsanpham.CAP_NHOMSANPHAM = nhomcha.CAP_NHOMSANPHAM + 1;
                             }
                             else
                             {
                                 TempData[Globals.FailedMessageKey] = "Nhóm sản phẩm không thể nằm trong chính nó";
                                 return RedirectToAction("DM_NhomSanPham");
                             }
                         }
                         else
                         {
                             TempData[Globals.FailedMessageKey] = "Nhóm sản phẩm chỉ có 3 cấp";
                             return RedirectToAction("DM_NhomSanPham");
                         }

                     */

                    //begin up hinh anh cho backend

                    var path = Helpers.Common.GetSetting("producttypes-image-folder");
                    var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            //FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_loaisanpham.ANH_DAIDIEN);
                            //if (fi.Exists)
                            //{
                            //    fi.Delete();
                            //}

                            string image_name = "producttypes_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_loaisanpham.LOAISANPHAM_ID.ToString(), @"\s+", "_")) + file.FileName;

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_loaisanpham.ANH_DAIDIEN = image_name;
                        }
                    }
                    //end up hinh anh cho backend

                    //begin up hinh anh cho client
                    path = Helpers.Common.GetSetting("producttypes-image-folder-client");
                    string prjClient = Helpers.Common.GetSetting("prj-client");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                    DirectoryInfo di = new DirectoryInfo(filepath);
                    filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            //FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_loaisanpham.ANH_DAIDIEN);
                            //if (fi.Exists)
                            //{
                            //    fi.Delete();
                            //}

                            string image_name = "producttypes_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_loaisanpham.LOAISANPHAM_ID.ToString(), @"\s+", "_")) + file.FileName;

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_loaisanpham.ANH_DAIDIEN = image_name;
                        }
                    }
                    //end up hinh anh cho client






                    dm_loaisanphamRepository.UpdateDM_LOAISANPHAM(dm_loaisanpham);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("DM_LoaiSanPham");

                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteLoaiSanPham()
        {
            int id = int.Parse(Request["Id"], CultureInfo.InvariantCulture);
            try
            {
                var item = dm_loaisanphamRepository.GetDM_LOAISANPHAMByLOAISANPHAM_ID(id);
                if (item != null)
                {
                    dm_loaisanphamRepository.DeleteDM_LOAISANPHAM(item.LOAISANPHAM_ID);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("DM_LoaiSanPham");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("DM_LoaiSanPham");
            }
        }
        #endregion



        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CapNhatDoUuTien(List<NewProductViewModel> listsp)
        {

            try
            {

                if (listsp != null && listsp.Count > 0)
                {

                    foreach (var item in listsp)
                    {
                        if (item.STT_Moi > 0)
                        {
                            var Product = productRepository.GetProductById(item.Id);
                            Product.STT_ISNEW = item.STT_Moi;
                            productRepository.UpdateProduct(Product);
                        }
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    }
                }

                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }


        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreateSKU(List<ProductSKUModel> listsp)
        {

            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (listsp != null && listsp.Count > 0)
                    {

                        foreach (var item in listsp)
                        {
                            if (item.ColorSKU != null && item.SizeSKU != null)
                            {
                                //var errors = ModelState.Values.SelectMany(v => v.Errors);
                                if (ModelState.IsValid)
                                {
                                    //var Product = new Domain.Sale.Entities.Product();
                                    var ProductSKU1 = new Domain.Sale.Entities.Product();
                                    var model = productRepository.GetProductById(int.Parse(listsp[0].Id.ToString()));
                                    AutoMapper.Mapper.Map(model, ProductSKU1);
                                    if (item.ColorSKU == model.color && item.SizeSKU == model.Size)
                                    {

                                        var Idcha = int.Parse(listsp[0].Id.ToString());
                                        var SKUId = productRepository.GetAllvwProductSKU().Where(n => n.Product_id == Idcha).FirstOrDefault();
                                        var ProductSKU3 = new Domain.Sale.Entities.Sale_Product_SKU();
                                        if (SKUId == null)
                                        {

                                            ProductSKU3.CreatedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.ModifiedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.CreatedDate = DateTime.Now;
                                            ProductSKU3.ModifiedDate = DateTime.Now;
                                            ProductSKU3.AssignedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.IsDeleted = false;
                                            ProductSKU3.color = model.color;
                                            ProductSKU3.Size = model.Size;
                                            ProductSKU3.Product_id = int.Parse(listsp[0].Id.ToString());
                                            ProductSKU3.Product_idSKU = int.Parse(listsp[0].Id.ToString());

                                        }
                                        else
                                        {
                                            var IdProduct = productRepository.GetAllvwProductSKU().Where(n => n.Product_idSKU == SKUId.Product_idSKU && (n.color == item.ColorSKU && n.Size == item.SizeSKU)).FirstOrDefault();
                                            //if (IdProduct != null)
                                            //{
                                            //    ViewBag.errors = "Trùng màu và size";
                                            //    TempData[Globals.SuccessMessageKey] = "Trùng màu và size";
                                            //    ViewBag.ErrorMesseage = "Trùng màu và size";
                                            //    return Json(0);

                                            //}
                                            //else
                                            //{
                                            ProductSKU3.CreatedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.ModifiedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.CreatedDate = DateTime.Now;
                                            ProductSKU3.ModifiedDate = DateTime.Now;
                                            ProductSKU3.AssignedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.IsDeleted = false;
                                            ProductSKU3.color = item.ColorSKU;
                                            ProductSKU3.Size = item.SizeSKU;
                                            ProductSKU3.Product_id = ProductSKU1.Id;
                                            ProductSKU3.Product_idSKU = IdProduct.Product_idSKU.Value;

                                            //}
                                            //productRepository.InsertProduct_SKU(ProductSKU3);
                                        }
                                        productRepository.InsertProduct_SKU(ProductSKU3);
                                        continue;
                                    }
                                    else
                                    {
                                        ProductSKU1.CreatedUserId = WebSecurity.CurrentUserId;
                                        ProductSKU1.ModifiedUserId = WebSecurity.CurrentUserId;
                                        ProductSKU1.CreatedDate = DateTime.Now;
                                        ProductSKU1.ModifiedDate = DateTime.Now;
                                        ProductSKU1.Name = model.Name;
                                        ProductSKU1.GroupPrice = model.GroupPrice;
                                        ProductSKU1.HDSD = model.HDSD;
                                        ProductSKU1.THANH_PHAN = model.THANH_PHAN;
                                        ProductSKU1.THUONG_HIEU = model.THUONG_HIEU;
                                        ProductSKU1.Description_brief = model.Description_brief;
                                        ProductSKU1.Description = model.Description;
                                        ProductSKU1.NHOMSANPHAM_ID = model.NHOMSANPHAM_ID;
                                        ProductSKU1.PriceInbound = model.PriceInbound;
                                        ProductSKU1.PriceOutbound = model.PriceOutbound;
                                        ProductSKU1.color = item.ColorSKU;
                                        ProductSKU1.Size = item.SizeSKU;
                                        ProductSKU1.Type = model.Type;
                                        if (model.IS_ALOW_BAN_AM == null)
                                        {
                                            ProductSKU1.IS_ALOW_BAN_AM = false;
                                        }
                                        else
                                        {
                                            ProductSKU1.IS_ALOW_BAN_AM = model.IS_ALOW_BAN_AM;
                                        }
                                        ProductSKU1.IsDeleted = model.IsDeleted;
                                        ProductSKU1.IS_NGUNG_KD = model.IS_NGUNG_KD;
                                        ProductSKU1.IS_COMBO = model.IS_COMBO;
                                        ProductSKU1.IS_NOIBAT = model.IS_NOIBAT;
                                        ProductSKU1.IS_NEW = model.IS_NEW;
                                        ProductSKU1.STT_ISNEW = model.STT_ISNEW;
                                        ProductSKU1.IS_HOT = model.IS_HOT;
                                        ProductSKU1.IsDeleted = model.IsDeleted;
                                        ProductSKU1.LIST_TAGS = model.LIST_TAGS;
                                        ProductSKU1.is_price_unknown = model.is_price_unknown;
                                        ProductSKU1.is_display = false;
                                        ProductSKU1.PriceInbound = model.PriceInbound;
                                        ProductSKU1.PriceOutbound = model.PriceOutbound;
                                        ProductSKU1.NHOMSANPHAM_ID = model.NHOMSANPHAM_ID;
                                        ProductSKU1.LOAISANPHAM_ID = model.LOAISANPHAM_ID;
                                        ProductSKU1.Image_Name = model.Image_Name;
                                        ProductSKU1.Image_Pos = model.Image_Pos;
                                        ProductSKU1.List_Image = model.List_Image;
                                        ProductSKU1.Unit = model.Unit;
                                        ProductSKU1.Origin = model.Origin;
                                        if (model.NHOMSANPHAM_ID_LST == null || model.NHOMSANPHAM_ID_LST != null)
                                        {
                                            ProductSKU1.NHOMSANPHAM_ID_LST = string.Join(",", model.NHOMSANPHAM_ID);
                                        }
                                        if (model.LOAISANPHAM_ID_LST == null || model.NHOMSANPHAM_ID_LST != null)
                                        {
                                            ProductSKU1.LOAISANPHAM_ID_LST = string.Join(",", model.LOAISANPHAM_ID);
                                        }
                                        var KitudauNhomSP = Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(model.Name.Substring(0, 1)).ToUpper();
                                        CategoryRepository p = new CategoryRepository(new Domain.ErpDbContext());
                                        var categories = p.GetListCategoryByCode("Color_product").OrderBy(m => m.OrderNo).Where(m => m.Name == item.ColorSKU).FirstOrDefault();
                                        var categories1 = p.GetListCategoryByCode("Size_product").OrderBy(m => m.OrderNo).Where(m => m.Name == item.SizeSKU).FirstOrDefault();
                                        var ValueColorSP = categories.Value;
                                        var ValueSizeSP = categories1.Value;
                                        var IdColorSP = ValueColorSP;
                                        var IdSizeSP = ValueSizeSP;
                                        //begin sinh ma

                                        string pSKUCode = (KitudauNhomSP + IdColorSP + IdSizeSP) + '-' + (Erp.BackOffice.Helpers.Common.GetOrderNo("Product", model.Code));
                                        var product1 = productRepository.GetAllProduct().Where(n => n.Code == pSKUCode && (n.color == item.ColorSKU && n.Size == item.SizeSKU)).FirstOrDefault();
                                        if (product1 != null)
                                        {
                                            ViewBag.errors = "Trùng mã sản phẩm";
                                            TempData[Globals.SuccessMessageKey] = "Trùng mã sản phẩm";
                                            ViewBag.ErrorMesseage = "Trùng mã sản phẩm";
                                            return Json(0);

                                        }
                                        //end sinh ma
                                        ProductSKU1.Code = pSKUCode;
                                        //model.NHOMSANPHAM_ID_LST = NHOMSANPHAM_ID;

                                        ProductSKU1.URL_SLUG = model.URL_SLUG;
                                        ProductSKU1.Description_brief = model.Description_brief;
                                        ProductSKU1.is_price_unknown = model.is_price_unknown;
                                        productRepository.InsertProduct(ProductSKU1);
                                        Erp.BackOffice.Helpers.Common.SetOrderNo("Product");

                                        //create product SKU
                                        var Idcha = int.Parse(listsp[0].Id.ToString());
                                        var SKUId = productRepository.GetAllvwProductSKU().Where(n => n.Product_id == Idcha).FirstOrDefault();
                                        var ProductSKU3 = new Domain.Sale.Entities.Sale_Product_SKU();
                                        if (SKUId == null)
                                        {

                                            ProductSKU3.CreatedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.ModifiedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.CreatedDate = DateTime.Now;
                                            ProductSKU3.ModifiedDate = DateTime.Now;
                                            ProductSKU3.AssignedUserId = WebSecurity.CurrentUserId;
                                            ProductSKU3.IsDeleted = false;
                                            ProductSKU3.color = model.color;
                                            ProductSKU3.Size = model.Size;
                                            ProductSKU3.Product_id = int.Parse(listsp[0].Id.ToString());
                                            ProductSKU3.Product_idSKU = int.Parse(listsp[0].Id.ToString());
                                            productRepository.InsertProduct_SKU(ProductSKU3);

                                        }
                                        else
                                        {
                                            var IdProduct = productRepository.GetAllvwProductSKU().Where(n => n.Product_idSKU == SKUId.Product_idSKU).FirstOrDefault();
                                            if (IdProduct != null)
                                            {
                                                ProductSKU3.CreatedUserId = WebSecurity.CurrentUserId;
                                                ProductSKU3.ModifiedUserId = WebSecurity.CurrentUserId;
                                                ProductSKU3.CreatedDate = DateTime.Now;
                                                ProductSKU3.ModifiedDate = DateTime.Now;
                                                ProductSKU3.AssignedUserId = WebSecurity.CurrentUserId;
                                                ProductSKU3.IsDeleted = false;
                                                ProductSKU3.color = item.ColorSKU;
                                                ProductSKU3.Size = item.SizeSKU;
                                                ProductSKU3.Product_id = ProductSKU1.Id;
                                                ProductSKU3.Product_idSKU = IdProduct.Product_idSKU.Value;

                                            }
                                            else
                                            {
                                                ProductSKU3.CreatedUserId = WebSecurity.CurrentUserId;
                                                ProductSKU3.ModifiedUserId = WebSecurity.CurrentUserId;
                                                ProductSKU3.CreatedDate = DateTime.Now;
                                                ProductSKU3.ModifiedDate = DateTime.Now;
                                                ProductSKU3.AssignedUserId = WebSecurity.CurrentUserId;
                                                ProductSKU3.IsDeleted = false;
                                                ProductSKU3.color = item.ColorSKU;
                                                ProductSKU3.Size = item.SizeSKU;
                                                ProductSKU3.Product_id = ProductSKU1.Id;
                                                ProductSKU3.Product_idSKU = SKUId.Product_idSKU.Value;
                                            }



                                            //productRepository.InsertProduct_SKU(ProductSKU3);
                                        }
                                        productRepository.InsertProduct_SKU(ProductSKU3);
                                    }




                                }
                                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                            }
                        }
                        scope.Complete();
                    }


                    return Json(1);
                }
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }
        public ViewResult CapNhatDoUuTien()
        {

            //List<CustomerTest> list;
            //var model = new ProductViewModel();
            IEnumerable<ProductViewModel> q = productRepository.GetAllProduct().AsEnumerable()
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    PriceOutbound = item.PriceOutbound,
                    Barcode = item.Barcode,
                    Type = item.Type,
                    Unit = item.Unit,
                    CategoryCode = item.CategoryCode,
                    DiscountStaff = item.DiscountStaff,
                    IsMoneyDiscount = item.IsMoneyDiscount,
                    IS_NEW = item.IS_NEW,
                    STT_ISNEW = item.STT_ISNEW
                }).Where(m => m.IS_NEW == true).OrderByDescending(m => m.STT_ISNEW).ToList();
            ViewBag.listProduct = q;

            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SuaDonGia(List<CustomerTest> listsp)
        {

            try
            {

                if (listsp != null && listsp.Count > 0)
                {

                    foreach (var item in listsp)
                    {
                        if (item.Giamoi > 0)
                        {
                            var Product = productRepository.GetProductById(item.Id);
                            Product.PriceOutbound = item.Giamoi;
                            productRepository.UpdateProduct(Product);
                        }
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    }
                }
                FeedGoogle("FeedGoogle");
                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }




        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Inmavachpdf(List<sanphamexcel> listsp)
        {

            try
            {
                string fileName = @"BarcodePdf" + DateTime.Now.Ticks + ".pdf";
                string strReportName = Path.Combine(Server.MapPath("~/fileuploads"), fileName);
                if (listsp != null && listsp.Count > 0)
                {

                    //in ma vach
                    // begin phan tham so
                    float myWidth = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("myWidthBARCODE"));
                    float myHeight = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("myHeightBARCODE"));
                    float newwithImage = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("newwithImageBARCODE"));
                    float intFontdongia = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("Fontdongia"));
                    float intFontTensanpham = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("FontTensanpham"));
                    float BarHeight = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("BarHeight"));

                    float marginleftPage = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("marginleftPage"));
                    float marginrightPage = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("marginrightPage"));
                    float margintopPage = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("margintopPage"));
                    float marginbottonPage = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("marginbottonPage"));


                    float SpacingBeforeDONGIA = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("SpacingBeforeDONGIA"));
                    float SpacingBeforeTEN = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("SpacingBeforeTEN"));

                    float SpacingAfterDONGIA = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("SpacingAfterDONGIA"));
                    float SpacingAfterTEN = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("SpacingAfterTEN"));



                    float IndentationLeftDONGIA = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("IndentationLeftDONGIA"));
                    float IndentationrightDONGIA = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("IndentationrightDONGIA"));

                    float IndentationLeftTEN = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("IndentationLeftTEN"));
                    float IndentationrightTEN = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("IndentationrightTEN"));




                    iTextSharp.text.Font fonttensanpham = new iTextSharp.text.Font(BaseFont.CreateFont(Server.MapPath("~/fileuploads") + "\\times.ttf", "Identity-H", embedded: false));
                    fonttensanpham.SetColor(0, 0, 0);
                    fonttensanpham.Size = intFontTensanpham;
                    Font boldFontdongia = new Font(Font.FontFamily.TIMES_ROMAN, intFontdongia, Font.BOLD);
                    float FixedHeightcell = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("FixedHeightcellBARCODE"));
                    float ox = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("oxBARCODE"));
                    float oy = float.Parse(Erp.BackOffice.Helpers.Common.GetSetting("oyBARCODE"));

                    // end phan tham so

                    var pgSize = new iTextSharp.text.Rectangle(myWidth, myHeight);
                    var pdfToCreate = new Document(pgSize, marginleftPage, marginrightPage, margintopPage, marginbottonPage);


                    // Create a new PdfWrite object, writing the output to a MemoryStream
                    var outputStream = new MemoryStream();

                    var pdfWriter = PdfWriter.GetInstance(pdfToCreate, new FileStream(strReportName, FileMode.Create));
                    PdfContentByte cb = new PdfContentByte(pdfWriter);
                    // Open the Document for writing
                    pdfToCreate.Open();
                    Barcode128 code128 = new Barcode128();
                    PdfPTable BarCodeTable = null;
                    int i = 0;
                    int i3 = 0;
                    int iitem = 0;
                    foreach (var item in listsp)
                    {
                        int SoLuong = int.Parse(item.SoLuong.Replace(".", ""));
                        for (int isoluon = 0; isoluon < SoLuong; isoluon++)
                        {


                            if (i % 3 == 0)
                            {
                                BarCodeTable = new PdfPTable(3);
                                BarCodeTable.ExtendLastRow = true;
                                // Create barcode
                                i3 = i;
                            }
                            code128.CodeType = Barcode.CODE128_UCC;
                            code128.Code = item.MaSanPham;
                            //code128.Extended = true;
                            code128.TextAlignment = Element.ALIGN_CENTER;
                            //code128.StartStopText = true;
                            //code128.ChecksumText = true;
                            //code128.GenerateChecksum = true;
                            code128.BarHeight = BarHeight;


                            // Generate barcode image
                            iTextSharp.text.Image image128 = code128.CreateImageWithBarcode(cb, null, null);

                            image128.ScaleAbsoluteWidth(newwithImage);

                            PdfPCell cell = new PdfPCell();
                            cell.FixedHeight = FixedHeightcell;
                            cell.MinimumHeight = FixedHeightcell;

                            //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            //cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            Paragraph p = new Paragraph();
                            //cell.Border = 0;
                            //cell.BorderColor = BaseColor.WHITE;

                            p.Add(new Chunk(image128, ox, oy));
                            //p.Add(new Phrase(100,item.Ten));
                            //cell.Column= ct;

                            cell.AddElement(p);

                            //Paragraph p1 = new Paragraph();
                            ////cell.Border = 0;
                            ////cell.BorderColor = BaseColor.WHITE;
                            //p1.Add(new Phrase(" ", boldFontdongia));

                            ////cell.Column= ct;
                            //cell.AddElement(p1);


                            Paragraph p11 = new Paragraph();
                            //cell.Border = 0;
                            //cell.BorderColor = BaseColor.WHITE;
                            p11.SpacingBefore = SpacingBeforeDONGIA;
                            p11.SpacingAfter = SpacingAfterDONGIA;
                            p11.Add(new Phrase(item.DonGia, boldFontdongia));
                            p11.Alignment = Element.ALIGN_CENTER;
                            p11.IndentationLeft = IndentationLeftDONGIA;
                            p11.IndentationRight = IndentationrightDONGIA;
                            cell.AddElement(p11);




                            Paragraph p2 = new Paragraph();
                            //cell.Border = 0;
                            //cell.BorderColor = BaseColor.WHITE;
                            p2.SpacingBefore = SpacingBeforeTEN;
                            p2.SpacingAfter = SpacingAfterTEN;
                            p2.Add(new Phrase(item.Ten, fonttensanpham));
                            p2.Alignment = Element.ALIGN_CENTER;
                            p2.IndentationLeft = IndentationLeftTEN;
                            p2.IndentationRight = IndentationrightTEN;

                            //cell.Column= ct;
                            cell.AddElement(p2);
                            cell.Border = Rectangle.NO_BORDER;
                            BarCodeTable.AddCell(cell);


                            // Add image to table cell

                            // Add table to document
                            if (((i - i3 + 1) == 3) || ((isoluon == (SoLuong - 1) && iitem == listsp.Count - 1)))
                            {
                                if (((i - i3 + 1) != 3) && ((isoluon == (SoLuong - 1) && iitem == listsp.Count - 1)))
                                {
                                    for (int i1 = 0; i1 < (3 - ((i + 1) % 3)); i1++)
                                    {
                                        PdfPCell cell1 = new PdfPCell();
                                        cell1.Border = Rectangle.NO_BORDER;
                                        BarCodeTable.AddCell(cell1);
                                    }
                                }

                                pdfToCreate.Add(BarCodeTable);
                                BarCodeTable.CompleteRow();
                                pdfToCreate.NewPage();

                            }
                            i++;
                        }
                        iitem++;
                    }
                    pdfToCreate.Close();
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                }
                //FeedGoogle("FeedGoogle");

                return Json(new { fileName = fileName, errorMessage = "" });
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }


        [HttpGet]
        public ActionResult Download(string file)
        {
            //get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/fileuploads"), file);

            //return the file for download, this is an Excel 
            //so I set the file content type to "application/vnd.ms-excel"
            return File(fullPath, "application/pdf", file);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Xoasanphammoi(string id)
        {

            try
            {
                var Product = productRepository.GetProductById(int.Parse(id));
                Product.IS_NEW = false;
                if (Product.IS_NEW == false)
                {
                    Product.STT_ISNEW = null;
                }
                productRepository.UpdateProduct(Product);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;


                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }




        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Xoasanphambancung(string id)
        {

            try
            {
                productRepository.DeleteProductsamesize(int.Parse(id));


                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult XoasanphamSKU(string id)
        {

            try
            {
                var item = productRepository.GetProduct_SKUById(int.Parse(id));
                productRepository.DeleteProduct(item.Product_id);
                productRepository.DeleteProductSKU(item.Id);


                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }



        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Xoasanphamtuongtu(string id)
        {

            try
            {
                productRepository.DeleteProductsamegroupt(int.Parse(id));


                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }


        #region SuaDonGia

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult FeedFacebook(List<CustomerTest> listsp)
        {

            try
            {

                if (listsp != null && listsp.Count > 0)
                {

                    foreach (var item in listsp)
                    {
                        if (item.Giamoi > 0)
                        {
                            var Product = productRepository.GetProductById(item.Id);
                            Product.PriceOutbound = item.Giamoi;
                            productRepository.UpdateProduct(Product);
                        }
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    }
                }
                return Json(1);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }


        public ActionResult FeedFacebook()
        {

            try
            {
                // ### CLEAR THE BROWSER OUTPUT AND WRITE TEXT/XML HEADER
                Response.Clear();
                Response.ContentType = "text/xml";
                Response.AddHeader("content-disposition", "attachment;filename=FeedXmlFile.xml");
                Response.ContentEncoding = Encoding.UTF8;


                XmlTextWriter objX = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
                objX.WriteStartDocument();

                objX.WriteStartElement("rss");
                objX.WriteAttributeString("version", "2.0");
                objX.WriteAttributeString("xmlns:g", "http://base.google.com/ns/1.0");

                objX.WriteStartElement("channel");
                objX.WriteElementString("title", Erp.BackOffice.Helpers.Common.GetSetting("website_title"));
                objX.WriteElementString("description", Erp.BackOffice.Helpers.Common.GetSetting("website_description"));
                objX.WriteElementString("link", Erp.BackOffice.Helpers.Common.GetSetting("website_link"));


                // ### GET ALL PRODUCTS; DATABASE CONNECTION SPECIFIED IN WEB.CONFIG FILE




                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = productRepository.GetvwProductById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        // ### OPEN AND WRITE THE ITEM ELEMENT
                        objX.WriteStartElement("item");

                        objX.WriteElementString("id", item.Code);
                        objX.WriteElementString("title", item.Name);
                        objX.WriteElementString("description", item.Description_brief);
                        objX.WriteElementString("g:price", item.PriceInbound.ToString());
                        objX.WriteElementString("link", Erp.BackOffice.Helpers.Common.GetSetting("api_url") + @"/chi-tiet-san-pham/sp" + item.Id);
                        objX.WriteElementString("g:image_link", Erp.BackOffice.Helpers.Common.GetSetting("api_url") + Helpers.Common.GetSetting("product-image-folder-client") + item.Image_Name);
                        objX.WriteElementString("g:brand", item.Origin);
                        objX.WriteElementString("g:product_type", item.ProductGroup);
                        objX.WriteElementString("g:condition", "new");
                        objX.WriteElementString("g:availability", "in stock");

                        // ### CLOSE ITEM
                        objX.WriteEndElement();
                    }
                }



                // ### CLOSE CHANNEL
                objX.WriteEndElement();

                // ### CLOSE RSS
                objX.WriteEndElement();

                objX.WriteEndDocument();

                objX.Flush();
                objX.Close();

                Response.End();
                return RedirectToAction("/Index");
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        //public bool isConHang(int id)
        //{
        //    var sale_Product = productRepository.GetProductById(id);

        //    if (sale_Product != null)
        //    {
        //        if (sale_Product.IS_ALOW_BAN_AM == true)
        //        {
        //            return true;
        //        }

        //        var value = (from c in Inventory
        //                     join d in Warehouse on c.WarehouseId equals d.Id
        //                     where
        //                                      (c.IsDeleted == null || c.IsDeleted == false)
        //                                     && (d.IsDeleted == null || d.IsDeleted == false)
        //                     && c.ProductId == id
        //                     select new Product_Commussion
        //                     {
        //                         Sale_Inventory = c,
        //                         Sale_Warehouse = d
        //                     }).FirstOrDefault();
        //        if (value != null)
        //        {
        //            if (value.Sale_Inventory == null)
        //                return false;
        //            if (value.Sale_Inventory.Quantity > 0)
        //                return true;
        //            else
        //                return false;
        //        }
        //        else
        //            return false;
        //    }
        //    else
        //        return false;

        //}

        public Product_Promotion GetSale_Product_Promotion(int id)
        {
            Product_Promotion product = new Product_Promotion();

            var sale_Products = productRepository.GetProductById(id);

            DateTime dt = DateTime.UtcNow.AddHours(7);
            product = (from a in new ErpSaleDbContext().CommissionCus
                       join b in new ErpSaleDbContext().CommisionCustomer on a.Id equals b.CommissionCusId
                       where b.ProductId == sale_Products.Id && (a.StartDate <= dt && a.EndDate >= dt)
                                         && (a.IsDeleted == null || a.IsDeleted == false)
                                         && (b.IsDeleted == null || b.IsDeleted == false)
                       select new Product_Promotion
                       {
                           Sale_CommissionCus = a,
                           Sale_Commision_Customer = b
                       }).FirstOrDefault();
            if (product == null)
                product = new Product_Promotion();


            return product;
        }











        public ActionResult FeedGoogle(string Feed)
        {

            try
            {

                ////int tmp = 0;
                // ### CLEAR THE BROWSER OUTPUT AND WRITE TEXT/XML HEADER
                Response.Clear();
                Response.ContentType = "text/xml";
                Response.AddHeader("content-disposition", "attachment;filename=FeedXmlFile.xml");
                Response.ContentEncoding = Encoding.UTF8;


                XmlTextWriter objX = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
                objX.WriteStartDocument();

                objX.WriteStartElement("rss");
                objX.WriteAttributeString("version", "2.0");
                objX.WriteAttributeString("xmlns:g", "http://base.google.com/ns/1.0");

                objX.WriteStartElement("channel");
                objX.WriteElementString("title", Erp.BackOffice.Helpers.Common.GetSetting("website_title"));
                objX.WriteElementString("description", Erp.BackOffice.Helpers.Common.GetSetting("website_description"));
                objX.WriteElementString("link", Erp.BackOffice.Helpers.Common.GetSetting("website_link"));


                // ### GET ALL PRODUCTS; DATABASE CONNECTION SPECIFIED IN WEB.CONFIG FILE



                string domainyhl = Erp.BackOffice.Helpers.Common.GetSetting("api_url");
                domainyhl = domainyhl.Remove(8, 1);
                string idDeleteAll = Request["DeleteId-checkbox"];
                string idDeleteAll1 = Request["isALL"].ToString();
                string[] arrDeleteId = null;
                if (idDeleteAll != null)
                {
                    arrDeleteId = idDeleteAll.Split(',');
                }


                //begin tạo List gia ban 
                List<vwProduct> Listsps = productRepository.GetAllvwProductList();
                List<decimal?> arrgias = new List<decimal?>();
                vwProduct_PromotionNew productExist = new vwProduct_PromotionNew();
                for (int i = 0; i < Listsps.Count(); i++)
                {

                    productExist = productRepository.GetAllvwProduct_Promotion(Listsps[i].Id);
                    if (productExist != null)
                    {

                        if (productExist.IsMoney == true)
                        {
                            var lamtron = Listsps[i].PriceOutbound - productExist.CommissionValue;
                            double ltr = Convert.ToDouble(lamtron);
                            ltr = Math.Round(ltr, 1);
                            decimal ltrx = Convert.ToDecimal(ltr);
                            arrgias.Add(ltrx);



                        }
                        else

                        {
                            var lamtron = Listsps[i].PriceInbound * (1 - (productExist.CommissionValue / 100));
                            double ltr = Convert.ToDouble(lamtron);
                            ltr = Math.Round(ltr, 1);
                            decimal ltrx = Convert.ToDecimal(ltr);
                            arrgias.Add(ltrx);


                        }


                    }
                    else
                    {
                        arrgias.Add(Listsps[i].PriceInbound);
                    }


                }
                //end tạo List gia ban 
                //idDeleteAll1.IndexOf("true") >= 0
                if (arrDeleteId != null)
                {


                    for (int i = 0; i < arrDeleteId.Count(); i++)
                    {
                        decimal? sale_P;
                        var item = productRepository.GetvwProductById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                        vwProduct_PromotionNew productExists = new vwProduct_PromotionNew();
                        productExists = productRepository.GetAllvwProduct_Promotion(item.Id);
                        if (productExist != null)
                        {

                            if (productExist.IsMoney == true)
                            {
                                var lamtron = item.PriceOutbound - productExist.CommissionValue;
                                double ltr = Convert.ToDouble(lamtron);
                                ltr = Math.Round(ltr, 1);
                                decimal ltrx = Convert.ToDecimal(ltr);
                                sale_P = ltrx;



                            }
                            else

                            {
                                var lamtron = item.PriceInbound * (1 - (productExist.CommissionValue / 100));
                                double ltr = Convert.ToDouble(lamtron);
                                ltr = Math.Round(ltr, 1);
                                decimal ltrx = Convert.ToDecimal(ltr);
                                sale_P = ltrx;


                            }


                        }
                        else
                        {
                            sale_P = item.PriceInbound;
                        }
                        if (item != null)
                        {
                            // ### OPEN AND WRITE THE ITEM ELEMENT

                            objX.WriteStartElement("item");
                            objX.WriteElementString("id", item.Code);
                            objX.WriteElementString("title", Helpers.Common.TitleCaseString(item.Name));
                            objX.WriteElementString("description", item.Description_brief);
                            if (Feed != null && Feed == "FeedGoogle")
                            {
                                objX.WriteElementString("g:price", item.PriceOutbound.ToString());

                                objX.WriteElementString("g:sale_price", sale_P.ToString());

                            }
                            else if (Feed != null && Feed == "FeedFacebook")
                            {



                                objX.WriteElementString("g:price", sale_P.ToString());

                            }
                            else
                            {



                                objX.WriteElementString("g:price", sale_P.ToString());

                            }

                            objX.WriteElementString("link", domainyhl + @"/chi-tiet-san-pham-sp-" + item.Id);
                            objX.WriteElementString("g:image_link", domainyhl + Helpers.Common.GetSetting("product-image-folder-client") + item.Image_Name);
                            objX.WriteElementString("g:brand", item.Origin);
                            objX.WriteElementString("g:product_type", item.ProductGroup);
                            objX.WriteElementString("g:condition", "new");
                            objX.WriteElementString("g:gtin", "");
                            objX.WriteElementString("g:mpn", "");
                            objX.WriteElementString("g:identifier_exists", "no");
                            objX.WriteElementString("g:availability", "in stock");
                            objX.WriteElementString("g:google_product_category", item.id_google_product_category);

                            // ### CLOSE ITEM
                            objX.WriteEndElement();
                        }
                    }

                }
                else
                {

                    if (arrDeleteId != null)
                    {

                    }




                    int tmp = 0;
                    var listSP = productRepository.GetAllvwProduct();

                    int sosp = listSP.Count();

                    var sale_Products = productRepository.GetProductById(1030);
                    foreach (var item in listSP)
                    {
                        // ### OPEN AND WRITE THE ITEM ELEMENT


                        objX.WriteStartElement("item");
                        objX.WriteElementString("id", item.Code);
                        objX.WriteElementString("title", Helpers.Common.TitleCaseString(item.Name));
                        objX.WriteElementString("description", item.Description_brief);

                        if (Feed != null && Feed == "FeedGoogle")
                        {
                            objX.WriteElementString("g:price", item.PriceOutbound.ToString());
                            string Sale_price = arrgias[tmp].ToString();
                            objX.WriteElementString("g:sale_price", Sale_price);
                            tmp++;
                        }
                        else if (Feed != null && Feed == "FeedFacebook")
                        {
                            string Sale_price = arrgias[tmp].ToString();


                            objX.WriteElementString("g:price", Sale_price);
                            tmp++;
                        }
                        else
                        {
                            string Sale_price = arrgias[tmp].ToString();


                            objX.WriteElementString("g:price", Sale_price);
                            tmp++;
                        }
                        objX.WriteElementString("link", domainyhl + @"/chi-tiet-san-pham-sp-" + item.Id);
                        objX.WriteElementString("g:image_link", domainyhl + Helpers.Common.GetSetting("product-image-folder-client") + item.Image_Name);
                        objX.WriteElementString("g:brand", item.Origin);
                        objX.WriteElementString("g:product_type", item.ProductGroup);
                        objX.WriteElementString("g:condition", "new");
                        objX.WriteElementString("g:gtin", "");
                        objX.WriteElementString("g:mpn", "");
                        objX.WriteElementString("g:identifier_exists", "no");
                        objX.WriteElementString("g:availability", "in stock");
                        objX.WriteElementString("g:google_product_category", item.id_google_product_category);

                        // ### CLOSE ITEM
                        objX.WriteEndElement();
                    }
                }


                // ### CLOSE CHANNEL
                objX.WriteEndElement();

                // ### CLOSE RSS
                objX.WriteEndElement();

                objX.WriteEndDocument();

                objX.Flush();
                objX.Close();

                Response.End();
                return RedirectToAction("/Index");
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        public ViewResult SuaDonGia()
        {

            //List<CustomerTest> list;
            //var model = new ProductViewModel();
            IEnumerable<ProductViewModel> q = productRepository.GetAllProduct().AsEnumerable()
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    PriceOutbound = item.PriceOutbound,
                    Barcode = item.Barcode,
                    Type = item.Type,
                    Unit = item.Unit,
                    CategoryCode = item.CategoryCode,
                    DiscountStaff = item.DiscountStaff,
                    IsMoneyDiscount = item.IsMoneyDiscount,

                }).OrderByDescending(m => m.Id).ToList();
            ViewBag.listProduct = q;

            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];

            return View();
        }
        #endregion

        #region In Mã Vạch
        public ActionResult PrintBarCode(int? Id, string source)
        {




            var model = new ProductInboundViewModel();
            var ProductInbound = new vwProductInbound();
            var ProductOutbound = new vwProductOutbound();
            model.DetailList = new List<ProductInboundDetailViewModel>();
            var i = 0;
            if (Id != null && Id > 0 && source == "in")
            {
                ProductInbound = ProductInboundRepository.GetvwProductInboundFullById(Id.Value);
                var Details = ProductInboundRepository.GetAllvwProductInboundDetailByInboundId(ProductInbound.Id)
                       .Select(x => new ProductInboundDetailViewModel
                       {
                           Id = x.Id,
                           Price = x.Price,
                           ProductId = x.ProductId,
                           ProductInboundId = x.ProductInboundId,
                           Quantity = x.Quantity,
                           ProductCode = "",
                           ExpiryDate = x.ExpiryDate,
                           LoCode = x.LoCode,
                           ProductDamagedId = x.ProductDamagedId,
                           Reason = x.Reason,
                           NumberAmount = x.NumberAmount,
                           Status = x.Status
                       }).OrderBy(x => x.Id).ToList();
                foreach (var item in Details)
                {
                    i++;
                    var pro = productRepository.GetProductById(item.ProductId.Value);
                    item.ProductCode = pro.Code;
                    item.ProductName = pro.Name;
                    item.Price = pro.PriceOutbound;
                    item.OrderNo = i;
                }
                model.DetailList = Details;
            }
            if (Id != null && Id > 0 && source == "out")
            {
                ProductOutbound = productOutboundRepository.GetvwProductOutboundFullById(Id.Value);
                var Details = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(ProductOutbound.Id)
                       .Select(x => new ProductInboundDetailViewModel
                       {
                           Id = x.Id,
                           Price = x.Price,
                           ProductId = x.ProductId,
                           ProductInboundId = x.ProductOutboundId,
                           Quantity = x.Quantity,
                           ProductCode = x.ProductCode,
                           ExpiryDate = x.ExpiryDate,
                           LoCode = x.LoCode

                       }).OrderBy(x => x.Id).ToList();
                foreach (var item in Details)
                {
                    i++;
                    var pro = productRepository.GetProductById(item.ProductId.Value);
                    item.ProductCode = pro.Code;
                    item.ProductName = pro.Name;
                    item.Price = pro.PriceOutbound;
                    item.OrderNo = i;
                }
                model.DetailList = Details;
            }

            var productList = productRepository.GetAllvwProduct()
           .Select(item => new ProductViewModel
           {
               Code = item.Code,
               Barcode = item.Barcode,
               Name = item.Name,
               Id = item.Id,
               CategoryCode = item.CategoryCode,
               PriceInbound = item.PriceOutbound,
               Unit = item.Unit,
               Image_Name = item.Image_Name,
               Origin = item.Origin
           });
            ViewBag.productList = productList;
            var product = productList.ToList();
            return View(model);
        }


        #region LoadProductItem
        public PartialViewResult LoadProductItem(int OrderNo, int ProductId, string ProductName, string Unit, int Quantity, decimal Price, string ProductCode, string ProductType, string LoCode, string ExpiryDate)
        {
            var model = new ProductInboundDetailViewModel();
            model.ProductName = ProductName;
            model.OrderNo = OrderNo;
            model.ProductId = ProductId;
            model.Unit = Unit;
            model.Quantity = Quantity;
            model.Price = Price;
            model.ProductCode = ProductCode;
            model.ProductType = ProductType;
            model.LoCode = LoCode;
            var pro2 = productRepository.GetAllProduct().ToList();
            var pro = productRepository.GetAllProduct().FirstOrDefault(x => x.Code == ProductCode);
            foreach (var item in pro2)
            {
                if (pro.Id == item.Id)
                {
                    if (model.ProductName == null)
                    {
                        model.OrderNo = OrderNo - 1;

                    }

                    //if (Price != pro.PriceInbound)
                    //{
                    //    model.Price = pro.PriceInbound;
                    //}
                    model.ProductId = pro.Id;
                    model.ProductName = pro.Name;
                    model.ProductType = pro.Type;
                    model.Unit = pro.Unit;
                    if (!string.IsNullOrEmpty(ExpiryDate))
                        model.ExpiryDate = Convert.ToDateTime(ExpiryDate);
                }
                else
                {
                    continue;
                }
            }





            return PartialView(model);
        }
        #endregion


        public ActionResult PrintBarCode2(int? Id)
        {
            var ProductInbound = new vwProductInbound();
            if (Id != null)
                ProductInbound = ProductInboundRepository.GetvwProductInboundFullById(Id.Value);

            var model = new ProductInboundViewModel();
            model.DetailList = new List<ProductInboundDetailViewModel>();
            var Details = ProductInboundRepository.GetAllvwProductInboundDetailByInboundId(ProductInbound.Id)
                        .Select(x => new ProductInboundDetailViewModel
                        {
                            Id = x.Id,
                            Price = x.Price,
                            ProductId = x.ProductId,
                            ProductInboundId = x.ProductInboundId,
                            Quantity = x.Quantity,
                            ProductCode = "",
                            ExpiryDate = x.ExpiryDate,
                            LoCode = x.LoCode,
                            ProductDamagedId = x.ProductDamagedId,
                            Reason = x.Reason,
                            NumberAmount = x.NumberAmount,
                            Status = x.Status
                        }).OrderBy(x => x.Id).ToList();
            model.DetailList = Details;
            var productList = productRepository.GetAllvwProduct()
               .Select(item => new ProductViewModel
               {
                   Code = item.Code,
                   Barcode = item.Barcode,
                   Name = item.Name,
                   Id = item.Id,
                   CategoryCode = item.CategoryCode,
                   PriceInbound = item.PriceOutbound,
                   Unit = item.Unit,
                   Image_Name = item.Image_Name,
                   Origin = item.Origin
               });
            ViewBag.productList = productList;
            return View(model);
        }
        #endregion
    }
}
