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
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using System.Web.Script.Serialization;
using Erp.Domain.Sale.Repositories;
using System.Data.Entity;
using System.Transactions;
using System.Web;
using Erp.Domain.Helper;
//vgbnjhgfghjh
namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class InventoryController : Controller
    {
        private readonly IWarehouseRepository WarehouseRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly IInventoryRepository InventoryRepository;
        private readonly IProductInboundRepository ProductInboundRepository;
        private readonly IProductOutboundRepository ProductOutboundRepository;
        private readonly IPhysicalInventoryRepository PhysicalInventoryRepository;
        private readonly IUserRepository userRepository;
        private readonly IDM_NHOMSANPHAMRepositories nhomSPRepository;
        public InventoryController(
            IInventoryRepository _Inventory
            , IProductOrServiceRepository _Product
            , IWarehouseRepository _Warehouse
            , IProductInboundRepository _ProductInbound
            , IProductOutboundRepository _ProductOutbound
            , IPhysicalInventoryRepository _PhysicalInventory
            , IUserRepository _user
            , IDM_NHOMSANPHAMRepositories _nhomsp
            )
        {
            WarehouseRepository = _Warehouse;
            ProductRepository = _Product;
            InventoryRepository = _Inventory;
            ProductInboundRepository = _ProductInbound;
            ProductOutboundRepository = _ProductOutbound;
            userRepository = _user;
            PhysicalInventoryRepository = _PhysicalInventory;
            nhomSPRepository = _nhomsp;

        }

        #region Index
        public ViewResult Index(string WarehouseId, string txtCode, string conHang , string category, string txtSearch, string manufacturer, int? page)
        {


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











            WarehouseId = WarehouseId == null ? "" : WarehouseId;
            category = category == null ? "" : category;
            txtSearch = txtSearch == null ? "" : txtSearch;
            txtCode = txtCode == null ? "" : txtCode;
            manufacturer = manufacturer == null ? "" : manufacturer;
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            var nhomsp = new List<DM_NHOMSANPHAM>();
            var a = new DM_NHOMSANPHAM();
            a.NHOMSANPHAM_ID = 0;
            a.TEN_NHOMSANPHAM = "Tất cả";
            nhomsp.Add(a);

            var nhomsp1 = nhomSPRepository.GetAllDM_NHOMSANPHAM().Where(u => u.NHOM_CHA != null).ToList();
            foreach(var b in nhomsp1)
            {
                nhomsp.Add(b);
            }
            
            
            ViewBag.listNhom = nhomsp;
            if (string.IsNullOrEmpty(conHang))
            {
                conHang = "1";
            }
            var listInventory = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = WarehouseId, HasQuantity = conHang, ProductCode = txtCode, ProductName = txtSearch, CategoryCode = "", ProductGroup = "", BranchId = 0, LoCode = "", ProductId = "", ExpiryDate = "" });

            //if (!string.IsNullOrEmpty(txtSearch))
            //{
            //    listInventory = listInventory.Where(n => n.ProductName.Contains(txtSearch)).ToList();
            //}
            var listProduct = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_ListProductFromInventory", new { WarehouseId = WarehouseId, HasQuantity = conHang, ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = category, BranchId = 0, CityId = "", DistrictId = "" });
            if (!string.IsNullOrEmpty(txtCode))
            {
                listProduct = listProduct.Where(n => n.ProductCode == txtCode || n.ProductName.Contains(txtCode)).ToList();
                //if (listInventory == null) {
                //    listInventory = listInventory.Where(n => n.ProductName.Contains(txtCode)).ToList();
                //}
            }
            if (!string.IsNullOrEmpty(txtSearch))
            {
                txtSearch = Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch).ToLower();
                listProduct = listProduct.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.ProductName).ToLower().Contains(txtSearch) || Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.ProductCode).ToLower().Contains(txtSearch)).ToList();
                //listProduct = listProduct.Where(n => n.ProductCode == txtSearch).ToList();
                //if (listProduct.Count() == 0)
                //{
                //    listProduct = listProduct.Where(n => n.ProductName.Contains(txtSearch)).ToList();
                //}
            }
        

            var warehouseList = new List<WarehouseViewModel>();

            warehouseList = WarehouseRepository.GetAllWarehouse()
                .Select(item => new WarehouseViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Address = item.Address,
                    Note = item.Note,
                    BranchId = item.BranchId
                }).OrderBy(x => x.BranchId).ThenBy(x => x.Name).ToList();




            List<string> listKeeperID = new List<string>();
            if (!string.IsNullOrEmpty(Erp.BackOffice.Helpers.Common.CurrentUser.WarehouseId))
            {
                listKeeperID = Erp.BackOffice.Helpers.Common.CurrentUser.WarehouseId.Split(',').ToList();
            }


            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin() && !Erp.BackOffice.Filters.SecurityFilter.IsKeToan())
            {
                warehouseList = warehouseList.Where(id1 => listKeeperID.Any(id2 => id2 == id1.Id.ToString())).ToList();
            }




            if (!string.IsNullOrEmpty(WarehouseId))
            {
                warehouseList = warehouseList.Where(x => ("," + WarehouseId + ",").Contains("," + x.Id + ",") == true).ToList();
            }
            if (!string.IsNullOrEmpty(manufacturer))
            {
                listInventory = listInventory.Where(x => x.Manufacturer == manufacturer).ToList();
                listProduct = listProduct.Where(x => x.Manufacturer == manufacturer).ToList();
            }
            ViewBag.inventoryList = listInventory.ToList();
            ViewBag.listProduct = listProduct.ToList();
            //  listProduct = listProduct.Where(id1 => listInventory.Any(id2 => id2.ProductId == id1.ProductId && id2.LoCode == id1.LoCode && id2.ExpiryDate == id1.ExpiryDate)).ToList();
            //var pager = new Pager(listProduct.Count(), page, 20);

            //var pageIndexViewModel = new IndexViewModel<InventoryViewModel>
            //{
            //    Items = listProduct.OrderBy(m => m.ProductCode).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
            //    .Select(item => new InventoryViewModel
            //    {
            //        ProductName = item.ProductName,
            //        ProductCode = item.ProductCode,
            //        LoCode = item.LoCode,
            //        ExpiryDate = item.ExpiryDate,
            //        day=item.day,
            //        month=item.month,
            //        year=item.year,
            //        WarehouseId=item.WarehouseId,
            //        ProductId=item.ProductId
            //    }).ToList(),
            //    Pager = pager
            //};

            ViewBag.warehouseList = warehouseList.ToList();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(listProduct);
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? Id, int? WarehouseId, string LoCode, int? day, int? month, int? year)
        {
            var Product = ProductRepository.GetvwProductById(Id.Value);
            if (Product != null && Product.IsDeleted != true)
            {
                var model = new ProductViewModel();
                AutoMapper.Mapper.Map(Product, model);

                var inboundDetails = ProductInboundRepository.GetAllvwProductInboundDetailByProductId(Id.Value).AsEnumerable()
                    .Where(item => item.IsArchive && item.LoCode == LoCode);
                var outboundDetails = ProductOutboundRepository.GetAllvwProductOutboundDetailByProductId(Id.Value).AsEnumerable()
                     .Where(item => item.IsArchive && item.LoCode == LoCode);

                if (WarehouseId != null && WarehouseId > 0)
                {
                    inboundDetails = inboundDetails.Where(x => x.WarehouseDestinationId == WarehouseId);
                    outboundDetails = outboundDetails.Where(x => x.WarehouseSourceId == WarehouseId);
                }
                if (day != null && month != null && year != null)
                {
                    inboundDetails = inboundDetails.Where(x => x.ExpiryDate != null && x.ExpiryDate.Value.Day == day && x.ExpiryDate.Value.Month == month && x.ExpiryDate.Value.Year == year);
                    outboundDetails = outboundDetails.Where(x => x.ExpiryDate != null && x.ExpiryDate.Value.Day == day && x.ExpiryDate.Value.Month == month && x.ExpiryDate.Value.Year == year);
                }
                else
                {
                    inboundDetails = inboundDetails.Where(x => x.ExpiryDate == null);
                    outboundDetails = outboundDetails.Where(x => x.ExpiryDate == null);
                }
                inboundDetails = inboundDetails.OrderBy(x => x.ProductInboundId);
                outboundDetails = outboundDetails.OrderBy(x => x.ProductOutboundId);
                ViewBag.inboundDetails = inboundDetails;
                ViewBag.outboundDetails = outboundDetails;

                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        public ActionResult DetailReportInventory(int? Id, int? WarehouseId, string LoCode, int? day, int? month, int? year)
        {



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









            var Product = ProductRepository.GetvwProductById(Id.Value);
            if (Product != null && Product.IsDeleted != true)
            {
                var model = new ProductViewModel();
                AutoMapper.Mapper.Map(Product, model);
                var listInventory = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = 0, LoCode = "", ProductId = Id, ExpiryDate = "" });


                var nxtDetails = SqlHelper.QuerySP<Sale_BaoCaoXuatKhoViewModelhoapd>("spSale_BaoCaoNhapXuatTon_hoapd", new
                {
                    @pProductId = Id
                }).ToList();



                //var inboundDetails = ProductInboundRepository.GetAllvwProductInboundDetailByProductId(Id.Value).AsEnumerable()
                //    .Where(item => item.IsArchive && item.LoCode == LoCode);
                //var outboundDetails = ProductOutboundRepository.GetAllvwProductOutboundDetailByProductId(Id.Value).AsEnumerable()
                //     .Where(item => item.IsArchive == true);
                var warehouseList = new List<WarehouseViewModel>();

                warehouseList = WarehouseRepository.GetAllWarehouse()
                    .Select(item => new WarehouseViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Code = item.Code,
                        Address = item.Address,
                        Note = item.Note,
                        BranchId = item.BranchId
                    }).OrderBy(x => x.Name).ToList();
                if (!string.IsNullOrEmpty(WarehouseId.ToString()))
                {
                    warehouseList = warehouseList.Where(x => ("," + WarehouseId + ",").Contains("," + x.Id + ",") == true).ToList();
                }


                var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
                List<string> listKeeperID = new List<string>();
                if (!string.IsNullOrEmpty(Erp.BackOffice.Helpers.Common.CurrentUser.WarehouseId))
                {
                    listKeeperID = Erp.BackOffice.Helpers.Common.CurrentUser.WarehouseId.Split(',').ToList();
                }
               

                if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin() && !Erp.BackOffice.Filters.SecurityFilter.IsKeToan())
                {
                    warehouseList = warehouseList.Where(id1 => listKeeperID.Any(id2 => id2 == id1.Id.ToString())).ToList();
                }




                //if (WarehouseId != null && WarehouseId > 0)
                //{
                //    inboundDetails = inboundDetails.Where(x => x.WarehouseDestinationId == WarehouseId);
                //    outboundDetails = outboundDetails.Where(x => x.WarehouseSourceId == WarehouseId);
                //}

                //if (day != null && month != null && year != null)
                //{
                //    inboundDetails = inboundDetails.Where(x => x.ExpiryDate != null && x.ExpiryDate.Value.Day == day && x.ExpiryDate.Value.Month == month && x.ExpiryDate.Value.Year == year);
                //    outboundDetails = outboundDetails.Where(x => x.ExpiryDate != null && x.ExpiryDate.Value.Day == day && x.ExpiryDate.Value.Month == month && x.ExpiryDate.Value.Year == year);
                //}
                //else
                //{
                //    inboundDetails = inboundDetails.Where(x => x.ExpiryDate == null);
                //    outboundDetails = outboundDetails.Where(x => x.ExpiryDate == null);
                //}
                //inboundDetails = inboundDetails.OrderBy(x => x.ProductInboundId);
                //outboundDetails = outboundDetails.OrderBy(x => x.ProductOutboundId);
                //ViewBag.inboundDetails = inboundDetails;
                //ViewBag.outboundDetails = outboundDetails;
                ViewBag.listInventory = listInventory;
                ViewBag.nxtDetails = nxtDetails;
                int pTongton = 0;
                foreach (var item in listInventory)
                {
                    pTongton = pTongton +   Helpers.Common.NVL_NUM_NT_NEW(item.Quantity);
                }   

                model.MinInventory = pTongton;


                var nxtDetails_dangchuyen = SqlHelper.QuerySP<Sale_BaoCaoXuatKhoViewModelhoapd>("spSale_BaoCaoNhapXuatTon_dangchuyen", new
                {
                    @pProductId = Id
                }).ToList();

                if (nxtDetails_dangchuyen.Count()>0)
                {
                    model.QuantityTotalInventory = nxtDetails_dangchuyen.ElementAt(0).soluongdangchuyen; 
                }


                ViewBag.warehouseList = warehouseList.ToList();
                return View(model);
            }
            
            if (Request.UrlReferrer != null)
            
               
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            

               
            
            return RedirectToAction("Index");
        }
        #endregion

        #region Json
        public JsonResult GetListProductJsonByWarehouseId(int? warehouseId)
        {
            if (warehouseId == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = Erp.Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = warehouseId, HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = "", LoCode = "", ProductId = "", ExpiryDate = "" }).ToList();
            foreach (var item in list)
            {
                item.strExpiryDate = item.ExpiryDate == null ? "" : item.ExpiryDate.Value.ToString("dd/MM/yyyy");
            }
            //var list = InventoryRepository.GetAllvwInventoryByWarehouseId(warehouseId.Value).AsEnumerable()
            //    .Select(item => new InventoryViewModel
            //    {
            //        Id=item.id
            //        CategoryCode = item.CategoryCode,
            //        ProductId = item.ProductId,
            //        ProductCode = item.ProductCode,
            //        ProductName = item.ProductName,
            //        Quantity = item.Quantity,
            //        LoCode = item.LoCode,
            //        strExpiryDate = item.ExpiryDate==null?"":item.ExpiryDate.Value.ToString("dd/MM/yyyy")
            //    }).OrderBy(item => item.CategoryCode).ThenBy(item => item.ProductCode).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Check or Update
        public static string Check(string productName, int productId, string LoCode, DateTime? ExpiryDate, int warehouseId, int quantityIn, int quantityOut)
        {
            //hoapd tam thoi bo check
            return "";

            return Update(productName, productId, LoCode, ExpiryDate, warehouseId, quantityIn, quantityOut, false);

        }


        public static string Check_mobile(string productName, int productId, string LoCode, DateTime? ExpiryDate, int warehouseId, int quantityIn, int quantityOut, int pCurrentUserId)
        {
            return Update_mobile(productName, productId, LoCode, ExpiryDate, warehouseId, quantityIn, quantityOut, pCurrentUserId, false);
        }

        public static string Update(string productName, int productId, string LoCode, DateTime? ExpiryDate, int warehouseId, int quantityIn, int quantityOut, bool isArchive = true)
        {
            string error = "";
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    ProductInboundRepository productInboundRepository = new ProductInboundRepository(new Domain.Sale.ErpSaleDbContext());
                    ProductOutboundRepository productOutboundRepository = new Domain.Sale.Repositories.ProductOutboundRepository(new Domain.Sale.ErpSaleDbContext());
                    InventoryRepository inventoryRepository = new Domain.Sale.Repositories.InventoryRepository(new Domain.Sale.ErpSaleDbContext());
                    LoCode = LoCode == null ? "" : LoCode;
                    var d_ExpiryDate = (ExpiryDate != null ? DateTime.ParseExact(ExpiryDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");
                    var inbound = Domain.Helper.SqlHelper.QuerySP<vwProductInboundDetail>("spSale_GetInboundDetail", new
                    {
                        ProductId = productId,
                        LoCode = LoCode,
                        ExpiryDate = d_ExpiryDate,
                        WarehouseDestinationId = warehouseId
                    }).ToList();

                    var outbound = Domain.Helper.SqlHelper.QuerySP<vwProductOutboundDetail>("spSale_GetOutboundDetail", new
                    {
                        ProductId = productId,
                        LoCode = LoCode,
                        ExpiryDate = d_ExpiryDate,
                        WarehouseSourceId = warehouseId
                    });
                    var inventoryCurrent_List = Erp.Domain.Helper.SqlHelper.QuerySP<Inventory>("spSale_Get_Inventory",
                             new
                             {
                                 WarehouseId = warehouseId,
                                 HasQuantity = "0",
                                 ProductCode = "",
                                 ProductName = "",
                                 CategoryCode = "",
                                 ProductGroup = "",
                                 BranchId = "",
                                 LoCode = LoCode,
                                 ProductId = productId,
                                 ExpiryDate = ExpiryDate
                             }).ToList();
                    if (string.IsNullOrEmpty(LoCode) || LoCode == "")
                    {
                        inbound = inbound.Where(x => string.IsNullOrEmpty(x.LoCode) || x.LoCode == "" || x.LoCode == null).ToList();
                        outbound = outbound.Where(x => string.IsNullOrEmpty(x.LoCode) || x.LoCode == "" || x.LoCode == null).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => string.IsNullOrEmpty(x.LoCode) || x.LoCode == "" || x.LoCode == null).ToList();
                    }
                    else
                    {
                        inbound = inbound.Where(x => x.LoCode == LoCode).ToList();
                        outbound = outbound.Where(x => x.LoCode == LoCode).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.LoCode == LoCode).ToList();
                    }
                    if (ExpiryDate == null)
                    {
                        inbound = inbound.Where(x => x.ExpiryDate == null).ToList();
                        outbound = outbound.Where(x => x.ExpiryDate == null).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.ExpiryDate == null).ToList();
                    }
                    else
                    {
                        inbound = inbound.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                        outbound = outbound.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                    }
                    var qty_inbound = inbound.Sum(x => x.Quantity);
                    var qty_outbound = outbound.Sum(x => x.Quantity);
                    var inventory = (inbound.Count() > 0 ? qty_inbound : 0) - (outbound.Count() > 0 ? qty_outbound : 0) + quantityIn - quantityOut;


                    for (int i = 0; i < inventoryCurrent_List.Count(); i++)
                    {
                        if (i > 0)
                        {
                            var id = inventoryCurrent_List[i].Id;
                            inventoryRepository.DeleteInventory(id);
                        }
                    }
                    //Sau khi thay đổi dữ liệu phải đảm bảo tồn kho >= 0

                    if (isArchive)
                    {
                        if (inventoryCurrent_List.Count() > 0)
                        {
                            if (inventoryCurrent_List[0].Quantity != inventory)
                            {
                                inventoryCurrent_List[0].Quantity = inventory;
                                inventoryRepository.UpdateInventory(inventoryCurrent_List[0]);
                            }
                        }
                        else
                        {
                            var insert = new Inventory();
                            insert.IsDeleted = false;
                            insert.CreatedUserId = WebSecurity.CurrentUserId;
                            insert.ModifiedUserId = WebSecurity.CurrentUserId;
                            insert.CreatedDate = DateTime.Now;
                            insert.ModifiedDate = DateTime.Now;
                            insert.WarehouseId = warehouseId;
                            insert.ProductId = productId;
                            insert.Quantity = inventory;
                            insert.LoCode = LoCode;
                            insert.ExpiryDate = ExpiryDate;
                            inventoryRepository.InsertInventory(insert);
                        }
                    }

                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                }
            }
            return error;
        }



        public static string Update_mobile(string productName, int productId, string LoCode, DateTime? ExpiryDate, int warehouseId, int quantityIn, int quantityOut, int pCurrentUserId, bool isArchive = true)
        {
            string error = "";
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {

                    ProductInboundRepository productInboundRepository = new ProductInboundRepository(new Domain.Sale.ErpSaleDbContext());
                    ProductOutboundRepository productOutboundRepository = new Domain.Sale.Repositories.ProductOutboundRepository(new Domain.Sale.ErpSaleDbContext());
                    InventoryRepository inventoryRepository = new Domain.Sale.Repositories.InventoryRepository(new Domain.Sale.ErpSaleDbContext());
                    LoCode = LoCode == null ? "" : LoCode;
                    var d_ExpiryDate = (ExpiryDate != null ? DateTime.ParseExact(ExpiryDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");

                    //lấy tất cả các phiếu nhập
                    var inbound = Domain.Helper.SqlHelper.QuerySP<vwProductInboundDetail>("spSale_GetInboundDetail", new
                    {
                        ProductId = productId,
                        LoCode = LoCode,
                        ExpiryDate = d_ExpiryDate,
                        WarehouseDestinationId = warehouseId
                    }).ToList();

                    //lất tất cả các phiếu xuất
                    var outbound = Domain.Helper.SqlHelper.QuerySP<vwProductOutboundDetail>("spSale_GetOutboundDetail", new
                    {
                        ProductId = productId,
                        LoCode = LoCode,
                        ExpiryDate = d_ExpiryDate,
                        WarehouseSourceId = warehouseId
                    });

                    //lấy tồn kho hiện 
                    var inventoryCurrent_List = Erp.Domain.Helper.SqlHelper.QuerySP<Inventory>("spSale_Get_Inventory",
                        new
                        {
                            WarehouseId = warehouseId,
                            HasQuantity = "1",
                            ProductCode = "",
                            ProductName = "",
                            CategoryCode = "",
                            ProductGroup = "",
                            BranchId = "",
                            LoCode = LoCode,
                            ProductId = productId,
                            ExpiryDate = ExpiryDate
                        }).ToList();


                    //lấy các sản phẩm nhập, xuất, tồn kho theo lô
                    if (string.IsNullOrEmpty(LoCode) || LoCode == "")
                    {
                        inbound = inbound.Where(x => string.IsNullOrEmpty(x.LoCode) || x.LoCode == "").ToList();
                        outbound = outbound.Where(x => string.IsNullOrEmpty(x.LoCode) || x.LoCode == "").ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => string.IsNullOrEmpty(x.LoCode)).ToList();
                    }
                    else
                    {
                        inbound = inbound.Where(x => x.LoCode == LoCode).ToList();
                        outbound = outbound.Where(x => x.LoCode == LoCode).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.LoCode == LoCode).ToList();
                    }

                    //lấy các sản phẩm nhập, xuất, tồn kho theo lô và ngày hết hạn
                    if (ExpiryDate == null)
                    {
                        inbound = inbound.Where(x => x.ExpiryDate == null).ToList();
                        outbound = outbound.Where(x => x.ExpiryDate == null).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.ExpiryDate == null).ToList();
                    }
                    else
                    {
                        inbound = inbound.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                        outbound = outbound.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                        inventoryCurrent_List = inventoryCurrent_List.Where(x => x.ExpiryDate == ExpiryDate).ToList();
                    }


                    var qty_inbound = inbound.Sum(x => x.Quantity);
                    var qty_outbound = outbound.Sum(x => x.Quantity);
                    //tính lại tồn kho dựa vào nhập và xuất
                    var inventory = (inbound.Count() > 0 ? qty_inbound : 0) - (outbound.Count() > 0 ? qty_outbound : 0) + quantityIn - quantityOut;


                    //begin xóa các dòng double của sản phẩm và lô của sản phẩm đó, chỉ giữ lại 1 dòng đầu tiên
                    for (int i = 0; i < inventoryCurrent_List.Count(); i++)
                    {
                        if (i > 0)
                        {
                            var id = inventoryCurrent_List[i].Id;
                            inventoryRepository.DeleteInventory(id);
                        }
                    }
                    //end xóa các dòng double của sản phẩm và lô của sản phẩm đó, chỉ giữ lại 1 dòng đầu tiên

                    //Sau khi thay đổi dữ liệu phải đảm bảo tồn kho >= 0
                    if (true)//inventory >= 0)
                    {
                        if (isArchive)
                        {
                            if (inventoryCurrent_List.Count() > 0)
                            {
                                if (inventoryCurrent_List[0].Quantity != inventory)
                                {
                                    inventoryCurrent_List[0].Quantity = inventory;
                                    inventoryRepository.UpdateInventory(inventoryCurrent_List[0]);
                                }
                            }
                            else
                            {
                                var insert = new Inventory();
                                insert.IsDeleted = false;
                                insert.CreatedUserId = pCurrentUserId;
                                insert.ModifiedUserId = pCurrentUserId;
                                insert.CreatedDate = DateTime.Now;
                                insert.ModifiedDate = DateTime.Now;
                                insert.WarehouseId = warehouseId;
                                insert.ProductId = productId;
                                insert.Quantity = inventory;
                                insert.LoCode = LoCode;
                                insert.ExpiryDate = ExpiryDate;
                                inventoryRepository.InsertInventory(insert);
                            }
                        }
                    }
                    else
                    {
                        error += string.Format("<br/>Dữ liệu tồn kho của sản phẩm \"{0}\" là {1}: không hợp lệ!", productName, inventory);
                    }
                    scope.Complete();
                    return error;

                }
                catch (Exception ex)
                {

                    return ex.Message;
                }
            }

        }
        #endregion

        #region UpdateAll
        public ActionResult UpdateAll(string url)
        {
            string rs = "";
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var inventoryList = InventoryRepository.GetAllInventory().ToList();
                    var inbound1 = ProductInboundRepository.GetAllvwProductInboundDetail().ToList();
                    var outbound1 = ProductOutboundRepository.GetAllvwProductOutboundDetail().ToList();
                    foreach (var item in inventoryList)
                    {
                        var warehouseId = item.WarehouseId.Value;
                        var productId = item.ProductId.Value;
                        var inbound = inbound1.ToList();
                        var outbound = outbound1.ToList();
                        if (string.IsNullOrEmpty(item.LoCode) || item.LoCode == "")
                        {
                            inbound = inbound.Where(x => string.IsNullOrEmpty(x.LoCode) || x.LoCode == "").ToList();
                            outbound = outbound.Where(x => string.IsNullOrEmpty(x.LoCode) || x.LoCode == "").ToList();
                        }
                        else
                        {
                            inbound = inbound.Where(x => x.LoCode == item.LoCode).ToList();
                            outbound = outbound.Where(x => x.LoCode == item.LoCode).ToList();
                        }
                        if (item.ExpiryDate == null)
                        {
                            inbound = inbound.Where(x => x.ExpiryDate == null).ToList();
                            outbound = outbound.Where(x => x.ExpiryDate == null).ToList();
                        }
                        else
                        {
                            inbound = inbound.Where(x => x.ExpiryDate.Value.ToShortDateString() == item.ExpiryDate.Value.ToShortDateString()).ToList();
                            outbound = outbound.Where(x => x.ExpiryDate.Value.ToShortDateString() == item.ExpiryDate.Value.ToShortDateString()).ToList();
                        }
                        var _inbound = inbound.Where(x => x.IsArchive
                              && x.ProductId == productId
                              && x.WarehouseDestinationId == warehouseId
                              ).ToList().Sum(x => x.Quantity);

                        var _outbound = outbound.Where(x => x.IsArchive
                            && x.ProductId == productId
                            && x.WarehouseSourceId == warehouseId
                            ).ToList().Sum(x => x.Quantity);
                        var inventory = (_inbound == null ? 0 : _inbound) - (_outbound == null ? 0 : _outbound);

                        if (item.Quantity != inventory)
                        {
                            rs += "<br/>" + item.ProductId + " | " + item.Quantity + " => " + inventory;
                            item.Quantity = inventory;
                            InventoryRepository.UpdateInventory(item);
                        }

                    }
                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                    return Redirect(url);
                }
            }
            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess + rs;
            return Redirect(url);
        }
        #endregion
    }
}
