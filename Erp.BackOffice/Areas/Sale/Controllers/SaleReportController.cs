using System.Globalization;
using Erp.BackOffice.Filters;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Staff.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Helpers;
using Erp.Domain.Helper;
using Newtonsoft.Json;
using Erp.Domain.Account.Interfaces;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Areas.Administration.Models;
using System.Data;
using System.Web;
using Erp.Domain.Entities;
using PagedList;
using ClosedXML.Excel;
using System.IO;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class SaleReportController : Controller
    {
        private readonly IBranchRepository BranchRepository;
        private readonly IUserRepository userRepository;
        private readonly IBranchDepartmentRepository branchDepartmentRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly ISaleReportRepository saleReportRepository;
        private readonly IProductInvoiceRepository invoiceRepository;
        private readonly IPurchaseOrderRepository purchaseOrderRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IProductInboundRepository inboundRepository;
        private readonly IProductOutboundRepository outboundRepository;
        private readonly ISalesReturnsRepository salesReturnsRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IQueryHelper QueryHelper;
        private readonly IRequestInboundRepository requestInboundRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly ICommisionStaffRepository commisionStaffRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly ITotalDiscountMoneyNTRepository TotalDiscountMoneyNTRepository;
        private readonly ISettingRepository settingRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IYHL_KIENHANG_TRA_CTIETRepositories kienhangtrachitietRepositories;
        private readonly IYHL_KIENHANG_GUI_CTIETRepositories kienhangguichitietRepositories;
        public SaleReportController(
            IBranchRepository _Branch
            , IUserRepository _user
            , IBranchDepartmentRepository branchDepartment
            , IStaffsRepository staff
            , ISaleReportRepository saleReport
            , IProductInvoiceRepository invoice
            , IPurchaseOrderRepository purchaseOrder
            , IWarehouseRepository warehouse
            , IProductInboundRepository inbound
            , IProductOutboundRepository outbound
            , ISalesReturnsRepository salesReturns
            , IQueryHelper _QueryHelper
             , ICustomerRepository _Customer
            , IRequestInboundRepository requestInbound
            , IProductOrServiceRepository product
            , IInventoryRepository inventory
            , ICommisionStaffRepository commisionStaff
            , ITemplatePrintRepository templatePrint
            , ITotalDiscountMoneyNTRepository totalDiscountMoneyNT
            , ISettingRepository setting
            , ILocationRepository location
            , IYHL_KIENHANG_GUI_CTIETRepositories kienhangguichitiet
            , IYHL_KIENHANG_TRA_CTIETRepositories kienhangtrachitiet
            )
        {
            BranchRepository = _Branch;
            userRepository = _user;
            branchDepartmentRepository = branchDepartment;
            staffRepository = staff;
            saleReportRepository = saleReport;
            invoiceRepository = invoice;
            purchaseOrderRepository = purchaseOrder;
            warehouseRepository = warehouse;
            inboundRepository = inbound;
            outboundRepository = outbound;
            salesReturnsRepository = salesReturns;
            customerRepository = _Customer;
            QueryHelper = _QueryHelper;
            requestInboundRepository = requestInbound;
            ProductRepository = product;
            inventoryRepository = inventory;
            commisionStaffRepository = commisionStaff;
            templatePrintRepository = templatePrint;
            TotalDiscountMoneyNTRepository = totalDiscountMoneyNT;
            settingRepository = setting;
            locationRepository = location;
            kienhangguichitietRepositories = kienhangguichitiet;
            kienhangtrachitietRepositories = kienhangtrachitiet;
        }

        #region Báo cáo kho
        public ActionResult Inventory()
        {
            var warehouseList = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            ViewBag.warehouseList = warehouseList.AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });
            return View();
        }
        public ActionResult BaoCaoNhapXuatTon()
        {
            var warehouseList = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            ViewBag.warehouseList = warehouseList.AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });
            return View();

        }
        public ActionResult BaoCaoXuatKho()
        {
            var warehouseList = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            ViewBag.warehouseList = warehouseList.AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });
            return View();
        }
        public ActionResult ChartInboundAndOutboundInMonth(string single, int? year, int? month, int? quarter, int? week, string group, string branchId)
        {
            var model = new ChartInboundAndOutboundInMonthViewModel();
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            group = string.IsNullOrEmpty(group) ? "" : group;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            branchId = branchId == null ? "" : branchId.ToString();
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");

            var dataInbound = SqlHelper.QuerySP<ChartItem>("spSale_ReportProductInbound", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                ProductGroup = group,
                branchId = branchId
            });

            var dataOutbound = SqlHelper.QuerySP<ChartItem>("spSale_ReportProductOutbound", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                ProductGroup = group,
                branchId = branchId
            });

            //Xử lý dữ liệu
            if (dataInbound.Count() > 0)
            {
                foreach (var item in dataInbound)
                {
                    if (!string.IsNullOrEmpty(item.label))
                    {
                        item.label = item.label.Trim().Replace("\t", "");
                    }
                }
            }

            if (dataOutbound.Count() > 0)
            {
                foreach (var item in dataOutbound)
                {
                    if (!string.IsNullOrEmpty(item.label))
                    {
                        item.label = item.label.Trim().Replace("\t", "");
                    }
                }
            }

            var category = dataInbound.Select(item => item.label).Union(dataOutbound.Select(item => item.label));
            var qGroupTemp = dataInbound.Select(item => new { GroupName = item.group, NumberOfInbound = Convert.ToInt32(item.data), NumberOfOutbound = 0 })
                .Union(dataOutbound.Select(item => new { GroupName = item.group, NumberOfInbound = 0, NumberOfOutbound = Convert.ToInt32(item.data) }));

            //Thống kế theo nhóm sản phẩm
            var qGroup = qGroupTemp.GroupBy(
                item => item.GroupName,
                (key, g) => new
                {
                    GroupName = key,
                    NumberOfInbound = g.Sum(i => i.NumberOfInbound),
                    NumberOfOutbound = g.Sum(i => i.NumberOfOutbound)
                }
            );

            model.jsonInbound = JsonConvert.SerializeObject(dataInbound);

            model.jsonOutbound = JsonConvert.SerializeObject(dataOutbound);

            model.jsonCategory = JsonConvert.SerializeObject(category);

            model.GroupList = qGroup.ToDataTable();

            if (dataInbound.Count() > 0)
            {
                model.TongNhap = dataInbound.Sum(item => Convert.ToInt32(item.data));
            }
            else
            {
                model.TongNhap = 0;
            }

            if (dataOutbound.Count() > 0)
            {
                model.TongXuat = dataOutbound.Sum(item => Convert.ToInt32(item.data));
            }
            else
            {
                model.TongXuat = 0;
            }

            model.Year = year.Value;
            model.Month = month.Value;
            model.Single = single;
            model.Week = week.Value;
            return View(model);
        }
        public ActionResult RequestInbound(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId)
        {
            SaleReportRequestInboundViewModel model = new SaleReportRequestInboundViewModel();
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }
            var jsonData = new List<ChartItem>();
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
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

            //int? intBrandID = int.Parse(strBrandID);
            var qThongKeYeuCau = requestInboundRepository.GetAllvwRequestInbound()
                   .Where(x => x.CreatedDate > StartDate
                       && x.CreatedDate < EndDate).ToList();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (!string.IsNullOrEmpty(branchId))
            {
                qThongKeYeuCau = qThongKeYeuCau.Where(item => ("," + strBrandID + ",").Contains("," + item.BranchId + ",") == true).ToList();
            }

            //Thống kê đơn hàng khởi tạo/đang xử lý
            model.NumberOfRequestInbound = qThongKeYeuCau.Count();
            model.NumberOfRequestInbound_Error_success = qThongKeYeuCau.Where(item => item.Error == true && (item.ErrorQuantity.Value <= 0)).Count();
            model.NumberOfRequestInbound_Error_no_success = qThongKeYeuCau.Where(item => item.Error == true && item.ErrorQuantity.Value > 0).Count();
            model.NumberOfRequestInbound_Error = qThongKeYeuCau.Where(item => item.Error == true).Count();
            model.NumberOfRequestInbound_inbound_complete = qThongKeYeuCau.Where(x => x.Status == "inbound_complete").Count();
            model.NumberOfRequestInbound_InProgress = qThongKeYeuCau.Where(x => x.Status == "ApprovedASM").Count();
            model.NumberOfRequestInbound_new = qThongKeYeuCau.Where(x => x.Status == "new").Count();
            model.NumberOfRequestInbound_refure = qThongKeYeuCau.Where(x => x.Status == "refure").Count();
            model.NumberOfRequestInbound_shipping = qThongKeYeuCau.Where(x => x.Status == "shipping").Count();
            model.NumberOfRequestInbound_wait_shipping = qThongKeYeuCau.Where(x => x.Status == "ApprovedKT").Count();
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.branchId = strBrandID;
            return View(model);
        }

        public ActionResult Tinhtrangchuyenkho(int? size, int? page, string txtCode, string txtMinAmount, string txtMaxAmount, string txtWarehouseDestination, int? warehouseSourceId, int? warehouseDestinationId, string startDate, string endDate, string txtProductCode, string Status)
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
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            IEnumerable<ProductOutboundViewModel> q = outboundRepository.GetAllvwProductOutboundFull().Where(x => (x.BranchId == intBrandID || intBrandID == 0 || x.BranchId_nhan == intBrandID) && (x.WarehouseDestinationId != null))
                .Select(item => new ProductOutboundViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    TotalAmount = item.TotalAmount,
                    InvoiceCode = item.InvoiceCode,
                    PurchaseOrderCode = item.PurchaseOrderCode,
                    WarehouseDestinationName = item.WarehouseDestinationName,
                    WarehouseSourceName = item.WarehouseSourceName,
                    CustomerName = item.CustomerName,
                    Type = "internal",
                    IsArchive = item.IsArchive,
                    WarehouseDestinationId = item.WarehouseDestinationId,
                    WarehouseSourceId = item.WarehouseSourceId,
                    InvoiceIsDeleted = item.InvoiceIsDeleted,
                    IsDeleted = item.IsDeleted,
                    TT = item.TT,
                    BranchId = item.BranchId,
                    QuantityProduct = item.QuantityProduct
                }).OrderByDescending(m => m.Id);


            //Tìm những phiếu xuất có chứa mã sp
            if (!string.IsNullOrEmpty(txtProductCode))
            {
                txtProductCode = txtProductCode.Trim();
                var productListId = ProductRepository.GetAllvwProduct()
                    .Where(item => item.Code == txtProductCode).Select(item => item.Id).ToList();

                if (productListId.Count > 0)
                {
                    List<int> listProductOutboundId = new List<int>();
                    foreach (var id in productListId)
                    {
                        var list = outboundRepository.GetAllvwProductOutboundDetailByProductId(id)
                            .Select(item => item.ProductOutboundId.Value).Distinct().ToList();

                        listProductOutboundId.AddRange(list);
                    }

                    q = q.Where(item => listProductOutboundId.Contains(item.Id));
                }
            }

            if (string.IsNullOrEmpty(txtCode) == false || string.IsNullOrEmpty(txtWarehouseDestination) == false || warehouseSourceId != null)
            {
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                txtWarehouseDestination = txtWarehouseDestination == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtWarehouseDestination);
                q = q.Where(x => x.Code.ToLowerOrEmpty().Contains(txtCode)
                    || Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.WarehouseDestinationName).Contains(txtWarehouseDestination)
                    || x.PurchaseOrderCode.ToLowerOrEmpty().Contains(txtWarehouseDestination)
                    || x.InvoiceCode.ToLowerOrEmpty().Contains(txtWarehouseDestination)
                    //|| x.WarehouseSourceId == warehouseSourceId
                    );
            }
            if (warehouseSourceId != null && warehouseSourceId.Value > 0)
            {
                q = q.Where(x => x.WarehouseSourceId == warehouseSourceId);
            }

            if (warehouseDestinationId != null && warehouseDestinationId.Value > 0)
            {
                q = q.Where(x => x.WarehouseDestinationId == warehouseDestinationId);
            }
            //q = q.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            // lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    q = q.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate);
                }
            }

            decimal minAmount;
            if (decimal.TryParse(txtMinAmount, out minAmount))
            {
                q = q.Where(x => x.TotalAmount >= minAmount);
            }

            decimal maxAmount;
            if (decimal.TryParse(txtMaxAmount, out maxAmount))
            {
                if (maxAmount > 0)
                {
                    q = q.Where(x => x.TotalAmount <= maxAmount);
                }
            }

            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "KT")
                {
                    q = q.Where(x => x.TT == "KT");
                }
                else if (Status == "DC")
                {
                    q = q.Where(x => x.TT == "DC");
                }
                else if (Status == "DN")
                {
                    q = q.Where(x => x.TT == "DN");
                }


            }


            //phân trang trong foreach
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });


            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }


            ViewBag.size = items;
            ViewBag.currentSize = size;

            page = page ?? 1;

            int pageSize = (size ?? 10);


            int pageNumber = (page ?? 1);
            //end phân trang trong foreach

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q.ToPagedList(pageNumber, pageSize));

        }
        #endregion

        #region Báo cáo bán hàng/doanh thu
        public ActionResult Summary(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, int? branchId)
        {
            SaleReportSumaryViewModel model = new SaleReportSumaryViewModel();

            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            string BranchId = branchId == null ? "" : branchId.Value.ToString();

            //kiểm tra chi nhánh - cong
            HttpRequestBase request = this.Request;
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

            //int? intBrandID = int.Parse(strBrandID);

            BranchId = strBrandID;

            //end kiểm tra chi nhánh
            //BranchId = Erp.BackOffice.Helpers.Common.GetSetting("BranchId");
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            //var qThongKeBanHang = invoiceRepository.GetAllvwInvoiceDetails().AsEnumerable()
            //        .Where(x => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + x.BranchId + ",") == true) && x.IsArchive &&x.ProductInvoiceDate > StartDate && x.ProductInvoiceDate < EndDate);

            var invoice = invoiceRepository.GetAllvwProductInvoice().Where(x => x.IsArchive == true).AsEnumerable();

            //Thống kê đơn hàng khởi tạo/đang xử lý
            model.NumberOfProductInvoice_Pendding = invoice.AsEnumerable()
             .Where(x => x.Status == App_GlobalResources.Wording.OrderStatus_pending).Count();
            model.NumberOfProductInvoice_InProgress = invoice.AsEnumerable()
             .Where(x => x.Status == App_GlobalResources.Wording.OrderStatus_inprogress).Count();


            var qProductInvoice = invoiceRepository.GetAllvwProductInvoice().AsEnumerable()
                .Where(x => x.IsArchive && x.CreatedDate > StartDate && x.CreatedDate < EndDate);
            if (!string.IsNullOrEmpty(CityId))
            {
                qProductInvoice = qProductInvoice.Where(item => item.CityId == CityId);
                //invoice = invoice.Where(item => item.CityId == CityId);
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                qProductInvoice = qProductInvoice.Where(item => item.DistrictId == DistrictId);
                //invoice = invoice.Where(item => item.DistrictId == DistrictId);
            }
            if ((!string.IsNullOrEmpty(BranchId)) && (BranchId != "0"))
            {
                qProductInvoice = qProductInvoice.Where(item => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + item.BranchId + ",") == true));
                //invoice = invoice.Where(item => (string.IsNullOrEmpty(BranchId) || ("," + BranchId + ",").Contains("," + item.BranchId + ",") == true));
            }

            var Revenue = qProductInvoice.Sum(item => item.TotalAmount);
            var NumberOfProductInvoice = qProductInvoice.Count();

            model.Revenue = Revenue;
            model.NumberOfProductInvoice = NumberOfProductInvoice;

            model.ProductInvoiceList = new List<ProductInvoiceViewModel>();
            model.ProductInvoiceList = SqlHelper.QuerySP<ProductInvoiceViewModel>("spSale_LiabilitiesDrugStore_haopd", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                branchId = BranchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();

            var qThongKeBanHang_TheoNhanVien = new List<ChartItem>();
            var qThongKeBanHang_TheoProvinceName = new List<ChartItem>();
            var title1 = "";
            var title2 = "";


            //if (branchId == null)
            //{
            //    if (string.IsNullOrEmpty(DistrictId))
            //    {
            //        if (string.IsNullOrEmpty(CityId))
            //        {
            //            //Thống kê bán hàng theo khách hàng
            //            qThongKeBanHang_TheoKhachHang = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.CityId, item.ProvinceName, item.TotalAmount })
            //               .ToList()
            //               .GroupBy(l => new { l.CityId, l.ProvinceName })
            //               .Select(cl => new ChartItem
            //               {
            //                   label = cl.Key.ProvinceName,
            //                   data = cl.Sum(i => i.TotalAmount),
            //                   id = cl.Key.CityId,
            //               }).ToList();
            //            title = "Thống kê doanh số theo Tỉnh/Thành phố";
            //        }
            //        else
            //        {
            //            //Thống kê bán hàng theo khách hàng
            //            qThongKeBanHang_TheoKhachHang = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.DistrictId, item.DistrictName, item.TotalAmount })
            //               .ToList()
            //               .GroupBy(l => new { l.DistrictId, l.DistrictName })
            //               .Select(cl => new ChartItem
            //               {
            //                   label = cl.Key.DistrictName,
            //                   data = cl.Sum(i => i.TotalAmount),
            //                   id = cl.Key.DistrictId,
            //               }).ToList();
            //            title = "Thống kê doanh số theo Quận/Huyện";
            //        }

            //    }
            //    else
            //    {
            //        //Thống kê bán hàng theo khách hàng
            //        qThongKeBanHang_TheoKhachHang = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.CustomerId, item.CustomerName, item.TotalAmount })
            //           .ToList()
            //           .GroupBy(l => new { l.CustomerId, l.CustomerName })
            //           .Select(cl => new ChartItem
            //           {
            //               label = cl.Key.CustomerName,
            //               data = cl.Sum(i => i.TotalAmount),
            //               id = cl.Key.CustomerId.ToString(),
            //           }).ToList();
            //        title = "Thống kê doanh số theo KH";
            //    }

            //}
            //else
            //{
            //}
            //Thống kê bán hàng theo khách hàng
            qThongKeBanHang_TheoProvinceName = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.CityId, item.ProvinceName, item.TotalAmount })
               .ToList()
               .GroupBy(l => new { l.CityId, l.ProvinceName })
               .Select(cl => new ChartItem
               {
                   label = cl.Key.ProvinceName,
                   data = cl.Sum(i => i.TotalAmount),
                   id = cl.Key.CityId,
               }).ToList();
            title1 = "Thống kê doanh số theo Tỉnh/Thành phố";
            //Thống kê bán hàng theo khách hàng
            qThongKeBanHang_TheoNhanVien = qProductInvoice.Where(item => item.TotalAmount > 0).Select(item => new { item.CreatedUserId, item.SalerFullName, item.TotalAmount })
                   .ToList()
                   .GroupBy(l => new { l.CreatedUserId, l.SalerFullName })
                   .Select(cl => new ChartItem
                   {
                       label = cl.Key.SalerFullName,
                       data = cl.Sum(i => i.TotalAmount),
                       id = cl.Key.CreatedUserId.Value.ToString(),
                   }).ToList();
            title2 = "Thống kê doanh số theo nhân viên";

            ViewBag.jsonThongKeBanHang_TheoProvinceName = JsonConvert.SerializeObject(qThongKeBanHang_TheoProvinceName);
            ViewBag.Title_qThongKeBanHang_TheoProvinceName = title1;

            ViewBag.jsonThongKeBanHang_TheoNhanVien = JsonConvert.SerializeObject(qThongKeBanHang_TheoNhanVien);
            ViewBag.Title_qThongKeBanHang_TheoNhanVien = title2;

            ViewBag.single = single;
            ViewBag.Year = year;
            ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");
            return View(model);
        }

        public ActionResult ChartInvoiceDayInMonth(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, int? branchId)
        {
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            string BranchId = branchId == null ? "" : branchId.Value.ToString();
            //Kiểm tra chi nhánh
            HttpRequestBase request = this.Request;
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

            //int? intBrandID = int.Parse(strBrandID);

            BranchId = strBrandID;

            //end kiểm tra chi nhánh
            //BranchId = Erp.BackOffice.Helpers.Common.GetSetting("BranchId");
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");

            if (single == "month" || single == "week")
            {
                var data = SqlHelper.QuerySP<ChartItem>("spSale_ChartInvoiceDayInMonth", new
                {
                    StartDate = d_startDate,
                    EndDate = d_endDate,
                    branchId = BranchId,
                    CityId = CityId,
                    DistrictId = DistrictId
                });

                var jsonData = new List<ChartItem>();
                for (DateTime dt = StartDate; dt <= EndDate; dt = dt.AddDays(1))
                {
                    string label = dt.Day + "/" + dt.Month;
                    var obj = data.Where(item => item.label == label).FirstOrDefault();
                    if (obj == null)
                    {
                        obj = new ChartItem();
                        obj.label = label;
                        obj.data = 0;
                    }

                    jsonData.Add(obj);
                }

                string json = JsonConvert.SerializeObject(jsonData);
                ViewBag.json = json;
            }
            else
            {
                var data = SqlHelper.QuerySP<ChartItem>("spSale_ChartInvoiceDayInMonth2", new
                {
                    StartDate = d_startDate,
                    EndDate = d_endDate,
                    branchId = BranchId,
                    CityId = CityId,
                    DistrictId = DistrictId
                });

                var jsonData = new List<ChartItem>();
                for (int i = StartDate.Month; i <= EndDate.Month; i++)
                {
                    string label = i + "/" + year.Value;
                    var obj = data.Where(item => item.label == label).FirstOrDefault();
                    if (obj == null)
                    {
                        obj = new ChartItem();
                        obj.label = label;
                        obj.data = 0;
                    }

                    jsonData.Add(obj);
                }

                string json = JsonConvert.SerializeObject(jsonData);
                ViewBag.json = json;
            }

            return View();
        }

        public ActionResult ChartProductSaleInMonth(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, int? branchId)
        {
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            string BranchId = branchId == null ? "" : branchId.Value.ToString();
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            //if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(BranchId))
            //{
            //    BranchId = Helpers.Common.CurrentUser.DrugStore;
            //}
            //kiểm tra chi nhánh - cong
            HttpRequestBase request = this.Request;
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

            //int? intBrandID = int.Parse(strBrandID);

            BranchId = strBrandID;


            //
            //BranchId = Erp.BackOffice.Helpers.Common.GetSetting("BranchId");
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");

            var data = SqlHelper.QuerySP<ChartItem>("spSale_ChartProductSaleInMonth", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                branchId = BranchId,
                CityId = CityId,
                DistrictId = DistrictId
            });

            string json = JsonConvert.SerializeObject(data);
            ViewBag.json = json;

            return View();
        }

        public ActionResult ChartServiceSaleInMonth(string single, int? year, int? month, int? quarter, int? week, string branchId)
        {
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            branchId = branchId == null ? "" : branchId;

            //if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeName != "CSKH")
            //{
            //    branchId = Helpers.Common.CurrentUser.DrugStore;
            //}
            branchId = Erp.BackOffice.Helpers.Common.GetSetting("BranchId");
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            var data = SqlHelper.QuerySP<ChartItem>("spSale_ChartServiceSaleInMonth", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                branchId = branchId
            });
            //            var data = SqlHelper.QuerySQL<ChartItem>(string.Format(@"SELECT TOP 10 *
            //                                                                    FROM 
            //                                                                    (
            //	                                                                    SELECT vwSale_ProductAndService.Code as label, vwSale_ProductAndService.Name as label2, sum(Quantity * Price) As data
            //	                                                                    FROM [vwSale_ProductInvoiceDetail] as PInD
            //                                                                        left outer join vwSale_ProductAndService on vwSale_ProductAndService.Id = PInD.ProductId
            //	                                                                    WHERE ('{2}' is null or ',' + '{2}' + ','  like '%,'+PInD.BranchId+',%') and PInD.IsArchive = 1 and vwSale_ProductAndService.Type = 'service' and PInD.CreatedDate > '{0}' and PInD.CreatedDate < '{1}'
            //	                                                                    GROUP BY ProductId, vwSale_ProductAndService.Code, vwSale_ProductAndService.Name
            //                                                                    ) as Tbx
            //                                                                    order by data desc", StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), branchId));

            string json = JsonConvert.SerializeObject(data);
            ViewBag.json = json;
            return View();
        }

        public ActionResult Commision()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
                SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }

        public ActionResult BaoCaoBanHang()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }
        public ActionResult InvoiceByBranch()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
                SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }

        public ActionResult InvoiceByCustomer()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
                SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }

        public ActionResult BaoCaoDonHang()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }
        public ActionResult BaoCaoSoLuongBan()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }
        public ActionResult BaoCaoHangBanTraLai()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }

        #endregion

        #region Báo cáo thu/chi/công nợ
        public ActionResult ReceiptList()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }
        public ActionResult ReceiptListCustomer()
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

            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            //Danh sách nhân viên sale
            var SaleList = userRepository.GetAllvwUsers().AsEnumerable().Select(item => new { item.FullName, item.Id, item.BranchId });

            if (intBrandID > 0)
            {
                SaleList = SaleList.Where(x => x.BranchId == intBrandID);
            }

            var SaleList2 = SaleList.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id + ""
            });
            ViewBag.SaleList = SaleList2;
            //Danh sách khách hàng
            ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);//customerRepository.GetAllCustomer().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId.Value)
            //.Select(item => new SelectListItem
            //{
            //    Value = item.Id + "",
            //    Text = item.CompanyName
            //}).ToList();

            //ViewBag.customerList = customerList;
            return View();
        }
        public ActionResult PaymentList()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }

        public ActionResult BaoCaoCongNoTongHop()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            //Danh sách nhân viên sale
            var SaleList = userRepository.GetUserbyUserType("Sales Excutive").Select(item => new { item.FullName, item.Id }).ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id + ""
                });

            ViewBag.SaleList = SaleList;
            //Danh sách khách hàng
            ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }

        public ActionResult BaoCaoCongNoChiTiet()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }

        public ActionResult BaoCaoDoanhThu()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            //Danh sách nhân viên sale
            var SaleList = userRepository.GetUserbyUserType("Sales Excutive").Select(item => new { item.FullName, item.Id, item.BranchId }).ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id + ""
                });

            ViewBag.SaleList = SaleList;
            //Danh sách khách hàng
            ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }

        public ActionResult BaoCaoCongNoPhaiThu()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            //Danh sách khách hàng
            ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }
        public ActionResult BaoCaoTongChi()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            return View();
        }

        public ActionResult BaoCaoCongNoTongHopNCC()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            ////Danh sách nhân viên sale
            //var SaleList = userRepository.GetUserbyUserType("Sales Excutive").Select(item => new { item.FullName, item.Id }).ToList()
            //    .Select(x => new SelectListItem
            //    {
            //        Text = x.FullName,
            //        Value = x.Id + ""
            //    });

            //ViewBag.SaleList = SaleList;
            //Danh sách khách hàng
            //ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }

        public ActionResult BaoCaoCongNoPhaiTraNCC()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
               SelectListItem
            { Text = x.Name, Value = x.Id + "" });
            ////Danh sách khách hàng
            //ViewBag.customerList = SelectListHelper.GetSelectList_Customer(null, null);
            return View();
        }
        #endregion

        #region - Json -
        public JsonResult GetListJsonInvoiceDetailById(int? Id)
        {
            if (Id == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = invoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id.Value);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult BaoCaoKhoTheoNgay(string group, string category, string manufacturer, string branchId, int? page, string StartDate, string EndDate, string WarehouseId)
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
            group = group == null ? "" : group;
            category = category == null ? "" : category;
            branchId = intBrandID.ToString();
            manufacturer = manufacturer == null ? "" : manufacturer;
            WarehouseId = WarehouseId == null ? "" : WarehouseId;
            EndDate = string.IsNullOrEmpty(EndDate) ? "" : EndDate;
            StartDate = string.IsNullOrEmpty(StartDate) ? "" : StartDate;

            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(WarehouseId))
            {
                WarehouseId = Helpers.Common.CurrentUser.WarehouseId;
            }


            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59);

            var d_startDate = (!string.IsNullOrEmpty(StartDate) == true ? DateTime.ParseExact(StartDate, "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : aDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            var d_endDate = (!string.IsNullOrEmpty(EndDate) == true ? DateTime.ParseExact(EndDate, "dd/MM/yyyy", null).AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd HH:mm:ss") : retDateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            var data = SqlHelper.QuerySP<spBaoCaoNhapXuatTon_TuanViewModel>("spSale_BaoCaoNhapXuatTon_Tuan", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                WarehouseId = WarehouseId,
                CategoryCode = category,
                ProductGroup = group,
                Manufacturer = manufacturer
            }).ToList();
            if (intBrandID > 0)
            {
                data = data.Where(x => x.BranchId == intBrandID).ToList();
            }
            var product_outbound = SqlHelper.QuerySP<spBaoCaoXuatViewModel>("spSale_BaoCaoXuat", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                WarehouseId = WarehouseId,

                CategoryCode = category,
                ProductGroup = group,
                Manufacturer = manufacturer
            }).ToList();
            //var pager = new Pager(data.Count(), page, 20);

            var Items = data.OrderBy(m => m.ProductCode)
              //.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
              .Select(item => new spBaoCaoNhapXuatTon_TuanViewModel
              {
                  CategoryCode = item.CategoryCode,
                  First_InboundQuantity = item.First_InboundQuantity,
                  First_OutboundQuantity = item.First_OutboundQuantity,
                  First_Remain = item.First_Remain,
                  Last_InboundQuantity = item.Last_InboundQuantity,
                  Last_OutboundQuantity = item.Last_OutboundQuantity,
                  ProductCode = item.ProductCode,
                  ProductId = item.ProductId,
                  ProductName = item.ProductName,
                  ProductUnit = item.ProductUnit,
                  Remain = item.Remain,
                  ProductMinInventory = item.ProductMinInventory,
                  LoCode = item.LoCode,
                  ExpiryDate = item.ExpiryDate,
                  WarehouseName = item.WarehouseName,
                  WarehouseId = item.WarehouseId
              }).ToList();

            ViewBag.productInvoiceDetailList = product_outbound;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];


            return View(Items);
        }
        public ViewResult InventoryWarning(int? WarehouseId)
        {
            var listProduct = ProductRepository.GetAllvwProductByType("product");
            var viewModel = new IndexViewModel<ProductViewModel>
            {
                Items = listProduct.Where(m => m.QuantityTotalInventory < m.MinInventory)
                .Select(item => new ProductViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    PriceInbound = item.PriceInbound,
                    Type = item.Type,
                    Unit = item.Unit,
                    ModifiedDate = item.ModifiedDate,
                    MinInventory = item.MinInventory,
                    QuantityTotalInventory = item.QuantityTotalInventory,
                    CategoryCode = item.CategoryCode,

                }).ToList(),
                //Pager = pager
            };

            List<InventoryViewModel> inventoryList = new List<InventoryViewModel>();
            foreach (var item in viewModel.Items)
            {
                List<InventoryViewModel> inventoryP = inventoryRepository.GetAllvwInventoryByProductId(item.Id)
                .Select(itemV => new InventoryViewModel
                {

                    ProductId = itemV.ProductId,
                    Quantity = itemV.Quantity,
                    WarehouseId = itemV.WarehouseId,
                    ProductCode = itemV.ProductCode,
                    ProductName = itemV.ProductName,
                    WarehouseName = itemV.WarehouseName,
                    CBTK = itemV.CBTK,

                }).Where(u => u.CBTK > 0).ToList();
                if (WarehouseId != null)
                {
                    inventoryP = inventoryP.Where(u => u.WarehouseId == WarehouseId).ToList();
                    if (inventoryP.Count() > 0)
                    {
                        inventoryList.AddRange(inventoryP);
                    }
                }
                else
                {
                    inventoryP = inventoryP.Where(u => u.WarehouseId == 8).ToList();
                    if (inventoryP.Count() > 0)
                    {
                        inventoryList.AddRange(inventoryP);
                    }
                }
                //if (inventoryP.Count > 0)
                //inventoryList.AddRange(inventoryP);
            }
            inventoryList = inventoryList.OrderBy(u => u.Quantity).ToList();
            ViewBag.inventoryList = inventoryList;

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(viewModel);
        }
        public ActionResult ProductInvoiceList(string ProductGroup, string manufacturer, string CategoryCode, string BranchId, string startDate, string endDate, int? ProductId)
        {
            var q = invoiceRepository.GetAllvwInvoiceDetails().Where(x => x.ProductType == "product" && x.IsArchive == true);

            if (startDate == null && endDate == null)
            {
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // Cộng thêm 1 tháng và trừ đi một ngày.
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                startDate = aDateTime.ToString();
                endDate = retDateTime.ToString();
            }

            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    q = q.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate);
                }
            }
            BranchId = Erp.BackOffice.Helpers.Common.GetSetting("BranchId");
            if (string.IsNullOrEmpty(BranchId))
            {
                q = q.Where(x => ("," + BranchId + ",").Contains("," + x.BranchId + ",") == true);
            }
            if (!string.IsNullOrEmpty(ProductGroup))
            {
                q = q.Where(x => x.ProductGroup == ProductGroup);
            }
            if (!string.IsNullOrEmpty(manufacturer))
            {
                q = q.Where(x => x.Manufacturer == manufacturer);
            }
            if (!string.IsNullOrEmpty(CategoryCode))
            {
                q = q.Where(x => x.CategoryCode == CategoryCode);
            }
            if (ProductId != null && ProductId.Value > 0)
            {
                q = q.Where(x => x.ProductId == ProductId);
            }
            var model = q.Select(item => new ProductInvoiceDetailViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Amount = item.Amount,
                CategoryCode = item.CategoryCode,
                IsReturn = item.IsReturn,
                FixedDiscount = item.FixedDiscount,
                FixedDiscountAmount = item.FixedDiscountAmount,
                IrregularDiscountAmount = item.IrregularDiscountAmount,
                IrregularDiscount = item.IrregularDiscount,
                CheckPromotion = item.CheckPromotion,
                ProductType = item.ProductType,
                ProductCode = item.ProductCode,
                ProductGroup = item.ProductGroup,
                ProductInvoiceCode = item.ProductInvoiceCode,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductInvoiceDate = item.ProductInvoiceDate,
                ProductInvoiceId = item.ProductInvoiceId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                CustomerId = item.CustomerId,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                BranchId = item.BranchId,
                BranchName = item.BranchName
            }).OrderByDescending(m => m.ModifiedDate).ToList();
            return View(model);
        }

        public ActionResult XuatExcel(string html)
        {
            Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoTonKho" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Write(html);
            Response.End();
            return Content("success");
        }
        #region Báo cáo mua hàng
        public ActionResult PurchaseOrderBySupplier()
        {
            ViewBag.branchList = BranchRepository.GetAllBranch().AsEnumerable().Select(x => new
                SelectListItem
            { Text = x.Name, Value = x.Id + "" });

            return View();
        }
        #endregion

        public ActionResult InventoryQueryExpiryDate()
        {
            var warehouseList = warehouseRepository.GetAllWarehouse();
            ViewBag.warehouseList = warehouseList.AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });
            return View();
        }


        public ActionResult CommisionStaff(string BranchId, int? year, int? month, int? StaffId)
        {
            var q = commisionStaffRepository.GetAllvwCommisionStaff();
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.year == year);
            }
            else
            {
                q = q.Where(x => x.year == DateTime.Now.Year);
            }
            BranchId = Erp.BackOffice.Helpers.Common.GetSetting("BranchId");
            if (!string.IsNullOrEmpty(BranchId))
            {
                q = q.Where(x => ("," + BranchId + ",").Contains("," + x.BranchId + ",") == true);
            }
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.year == year);
            }
            if (month != null && month.Value > 0)
            {
                q = q.Where(x => x.month == month);
            }
            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(x => x.StaffId == StaffId);
            }
            var model = q.Select(item => new CommisionStaffViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AmountOfCommision = item.AmountOfCommision,
                BranchId = item.BranchId,
                BranchName = item.BranchName,
                InvoiceDetailId = item.InvoiceDetailId,
                InvoiceId = item.InvoiceId,
                InvoiceType = item.InvoiceType,
                IsResolved = item.IsResolved,
                month = item.month,
                Note = item.Note,
                PercentOfCommision = item.PercentOfCommision,
                ProductCode = item.ProductCode,
                ProductImage = item.ProductImage,
                ProductInvoiceCode = item.ProductInvoiceCode,
                ProductName = item.ProductName,
                StaffCode = item.StaffCode,
                StaffId = item.StaffId,
                StaffName = item.StaffName,
                StaffProfileImage = item.StaffProfileImage,
                year = item.year
            }).OrderByDescending(m => m.CreatedDate).ToList();
            return View(model);
        }
        public ActionResult ListCommisionStaff(int? StaffId)
        {
            var q = commisionStaffRepository.GetAllvwCommisionStaff();
            q = q.Where(x => x.year == DateTime.Now.Year);
            if (StaffId != null && StaffId.Value > 0)
            {
                q = q.Where(x => x.StaffId == StaffId);
            }
            var model = q.Select(item => new CommisionStaffViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AmountOfCommision = item.AmountOfCommision,
                BranchId = item.BranchId,
                BranchName = item.BranchName,
                InvoiceDetailId = item.InvoiceDetailId,
                InvoiceId = item.InvoiceId,
                InvoiceType = item.InvoiceType,
                IsResolved = item.IsResolved,
                month = item.month,
                Note = item.Note,
                PercentOfCommision = item.PercentOfCommision,
                ProductCode = item.ProductCode,
                ProductImage = item.ProductImage,
                ProductInvoiceCode = item.ProductInvoiceCode,
                ProductName = item.ProductName,
                StaffCode = item.StaffCode,
                StaffId = item.StaffId,
                StaffName = item.StaffName,
                StaffProfileImage = item.StaffProfileImage,
                year = item.year

            }).OrderByDescending(m => m.CreatedDate).ToList();
            return View(model);
        }

        public ActionResult BaoCaoDoanhThuTheoNgay(string startDate, string endDate, string CityId, string DistrictId, int? branchId)
        {
            var PaymentMethod = Request["Paymethod"];
            var q = invoiceRepository.GetAllvwProductInvoice().Where(x => x.IsArchive == true);
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            if (startDate == null && endDate == null)
            {
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // Cộng thêm 1 tháng và trừ đi một ngày.
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                startDate = aDateTime.ToString("dd/MM/yyyy");
                endDate = retDateTime.ToString("dd/MM/yyyy");
            }
            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    ViewBag.retDateTime = d_endDate;
                    ViewBag.aDateTime = d_startDate;
                    q = q.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate);
                }
            }
            //if (!string.IsNullOrEmpty(PaymentMethod))
            //{

            //        q = q.Where(x => x.PaymentMethod == PaymentMethod);

            //}

            //if (branchId != null && branchId.Value > 0)
            //{
            //    q = q.Where(x => x.BranchId == branchId);
            //    branch_list = branch_list.Where(x => x.Id == branchId).ToList();
            //}
            //if (!string.IsNullOrEmpty(CityId))
            //{
            //    q = q.Where(x => x.CityId == CityId);
            //    branch_list = branch_list.Where(x => x.CityId == CityId).ToList();
            //}
            //if (!string.IsNullOrEmpty(DistrictId))
            //{
            //    q = q.Where(x => x.DistrictId == DistrictId);
            //    branch_list = branch_list.Where(x => x.DistrictId == DistrictId).ToList();
            //}
            var model = q.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedDate = item.CreatedDate,
                BranchId = item.BranchId,
                TotalAmount = item.TotalAmount,
                RemainingAmount = item.RemainingAmount,
                PaidAmount = item.PaidAmount,
                Month = item.Month,
                Day = item.Day,
                Year = item.Year,

            }).OrderByDescending(m => m.CreatedDate).ToList();

            //if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan())
            //{
            // //   branch_list = branch_list.Where(x => ("," + user.DrugStore + ",").Contains("," + x.Id + ",") == true).ToList();
            //    model = model.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true).ToList();
            //}

            if (!string.IsNullOrEmpty(PaymentMethod))
            {
                if (PaymentMethod == "Tiền mặt")
                {

                    model = q.Select(item => new ProductInvoiceViewModel
                    {
                        Id = item.Id,
                        IsDeleted = item.IsDeleted,
                        CreatedDate = item.CreatedDate,
                        BranchId = item.BranchId,
                        TotalAmount = item.MoneyPay.Value,
                        RemainingAmount = item.RemainingAmount,
                        PaidAmount = item.PaidAmount,
                        Month = item.Month,
                        Day = item.Day,
                        Year = item.Year,

                    }).OrderByDescending(m => m.CreatedDate).ToList();
                }
                if (PaymentMethod == "Chuyển khoản")
                {
                    model = q.Select(item => new ProductInvoiceViewModel
                    {
                        Id = item.Id,
                        IsDeleted = item.IsDeleted,
                        CreatedDate = item.CreatedDate,
                        BranchId = item.BranchId,
                        TotalAmount = item.TransferPay.Value,
                        RemainingAmount = item.RemainingAmount,
                        PaidAmount = item.PaidAmount,
                        Month = item.Month,
                        Day = item.Day,
                        Year = item.Year,

                    }).OrderByDescending(m => m.CreatedDate).ToList();
                }
                if (PaymentMethod == "Quẹt thẻ")
                {
                    model = q.Select(item => new ProductInvoiceViewModel
                    {
                        Id = item.Id,
                        IsDeleted = item.IsDeleted,
                        CreatedDate = item.CreatedDate,
                        BranchId = item.BranchId,
                        TotalAmount = item.ATMPay.Value,
                        RemainingAmount = item.RemainingAmount,
                        PaidAmount = item.PaidAmount,
                        Month = item.Month,
                        Day = item.Day,
                        Year = item.Year,

                    }).OrderByDescending(m => m.CreatedDate).ToList();
                }
            }

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
            var branch_list = BranchRepository.GetAllBranch().ToList();

            if (intBrandID != null && intBrandID > 0)
            {
                model = model.Where(x => x.BranchId == intBrandID).ToList();
                branch_list = branch_list.Where(x => x.Id == intBrandID).ToList();
            }
            ViewBag.branchList = branch_list;

            return View(model);
        }
        #region DoanhsoThang

        public ActionResult DoanhThuNgay(int? page, int? size, string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? CreatedUserId, int? productId, string startDate, string endDate, bool? HTTT)
        {
            var PaymentMethod = Request["Paymethod"];
            var Sizeproduct = Request["Sizeproduct"];
            var Colorproduct = Request["Colorproduct"];
            var q = invoiceRepository.GetAllvwInvoiceDetails().AsEnumerable().Where(x => x.IsArchive == true);
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? Erp.BackOffice.Helpers.Common.GetSetting("BranchId") : branchId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;


            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(startDate))
            {
                StartDate = Convert.ToDateTime(DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                EndDate = Convert.ToDateTime(DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)).AddHours(23).AddMinutes(59);
            }

            //Lay don hang
            var productinvoice = invoiceRepository.GetAllvwProductInvoice().AsEnumerable().Where(x => x.IsArchive == true);

            q = q.Where(x => x.ProductInvoiceDate >= StartDate && x.ProductInvoiceDate <= EndDate);
            productinvoice = productinvoice.Where(x => x.CreatedDate >= StartDate && x.CreatedDate <= EndDate);
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
            if (intBrandID != null && intBrandID > 0)
            {
                q = q.Where(x => x.BranchId == intBrandID);
                productinvoice = productinvoice.Where(x => x.BranchId == intBrandID);
            }
            //
            //if (!string.IsNullOrEmpty(branchId))
            //{
            //    q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true));
            //}
            if (CreatedUserId != null && CreatedUserId.Value > 0)
            {
                q = q.Where(x => x.CreatedUserId == CreatedUserId);
                productinvoice = productinvoice.Where(x => x.CreatedUserId == CreatedUserId);
            }

            if (productId != null && productId.Value > 0)
            {
                q = q.Where(x => x.ProductId == productId);
            }

            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(x => x.CityId == CityId);
            }

            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(x => x.DistrictId == DistrictId);
            }

            if (HTTT == null || HTTT == false)
            {

                if (!string.IsNullOrEmpty(Sizeproduct))
                {
                    q = q.Where(x => x.Size == Sizeproduct);
                }

                if (!string.IsNullOrEmpty(Colorproduct))
                {
                    q = q.Where(x => x.color == Colorproduct);
                }
            }

            var model = q.Select(item => new ProductInvoiceDetailViewModel
            {

                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Amount = item.Amount2,
                CategoryCode = item.CategoryCode,
                IsReturn = item.IsReturn,
                FixedDiscount = item.FixedDiscount,
                FixedDiscountAmount = item.FixedDiscountAmount,
                IrregularDiscountAmount = item.IrregularDiscountAmount,
                IrregularDiscount = item.IrregularDiscount,
                CheckPromotion = item.CheckPromotion,
                ProductType = item.ProductType,
                ProductCode = item.ProductCode,
                ProductGroup = item.ProductGroup,
                ProductInvoiceCode = item.ProductInvoiceCode,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductInvoiceDate = item.ProductInvoiceDate,
                ProductInvoiceId = item.ProductInvoiceId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                CustomerId = item.CustomerId,
                CustomerCode = item.CustomerCode,
                color = item.color,
                Size = item.Size,
                CustomerName = item.CustomerName,
                BranchId = item.BranchId,
                BranchName = item.BranchName,
                StaffName = item.StaffName,
                CustomerPhone = item.CustomerPhone,
                QuantitySaleReturn = item.QuantitySaleReturn,


                //    StaffId = item.StaffId
            }).OrderByDescending(m => m.CreatedDate).ToList();
            foreach (var item in model)
            {

                var a = DateTime.Now;
                DateTime? temp = item.CreatedDate;


                if (temp.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) == a.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    var b = temp.Value.ToShortTimeString();
                    item.CreatedDateTemp01 = b;

                }
                else
                {
                    var c = temp.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    item.CreatedDateTemp01 = c;
                }
            }

            var InvoiceList = productinvoice.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                UserName = item.UserName,
                CreatedDate = item.CreatedDate,
                //CreatedDateTemp = item.CreatedDateTemp,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Code = item.Code,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                TotalAmount = item.TotalAmount,
                //FixedDiscount = item.FixedDiscount,
                TaxFee = item.TaxFee,
                CodeInvoiceRed = item.CodeInvoiceRed,
                Status = item.Status,
                IsArchive = item.IsArchive,
                ProductOutboundId = item.ProductOutboundId,
                ProductOutboundCode = item.ProductOutboundCode,
                Note = item.Note,
                CancelReason = item.CancelReason,
                BranchId = item.BranchId,
                CustomerId = item.CustomerId,
                BranchName = item.BranchName,
                CompanyName = item.CompanyName,
                Email = item.Email,
                CustomerPhone = item.CustomerPhone,
                Address = item.Address,
                DiscountTabBillAmount = item.DiscountTabBillAmount,
                DiscountTabBill = item.DiscountTabBill,
                isDisCountAllTab = item.isDisCountAllTab,
                DisCountAllTab = item.DisCountAllTab,
                Birthday = item.Birthday,
                Gender = item.Gender,
                Mobile = item.Mobile,
                TienTra = item.TienTra,
                SalerFullName = item.SalerFullName,
                MoneyPay = item.MoneyPay,
                TransferPay = item.TransferPay,
                ATMPay = item.ATMPay

            }).OrderByDescending(m => m.Id).ToList();

            //lọc theo hình thức thanh toán
            if (!string.IsNullOrEmpty(PaymentMethod))
            {
                if (PaymentMethod == "Tiền mặt")
                {
                    foreach (var item in InvoiceList)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.MoneyPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
                if (PaymentMethod == "Chuyển khoản")
                {
                    foreach (var item in InvoiceList)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.TransferPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
                if (PaymentMethod == "Quẹt thẻ")
                {
                    foreach (var item in InvoiceList)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.ATMPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
                //q = q.Where(x => x.PaymentMethod == PaymentMethod);

            }
            //phân trang trong foreach
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });


            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }


            ViewBag.size = items;
            ViewBag.currentSize = size;

            page = page ?? 1;

            int pageSize = (size ?? 10);


            int pageNumber = (page ?? 1);
            //end phân trang trong foreach
            ViewBag.page = page;
            ViewBag.model = model;
            ViewBag.InvoiceList = InvoiceList.ToPagedList(pageNumber, pageSize);
            ViewBag.Invoice2 = InvoiceList;
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ExportMonthRevenue()
        {
            return View();
        }
        public DataTable getDataMonth(string single, int? year, int? month, int? quarter, int? week, bool? HTTT, string PaymentMethod, string Sizeproduct, string Colorproduct)
        {
            //

            var dt = new DataTable();


            return dt;
        }
        #endregion
        public ActionResult DoanhSoThang(string single, int? year, int? month, int? quarter, int? week)
        {
            try
            {


                //var q = kienhangguichitietRepositories.GetAllYHL_KIENHANG_GUI_CTIET().AsEnumerable().Where(x => x.SO_HIEU != "");
                // var tra = kienhangtrachitietRepositories.GetAllYHL_KIENHANG_TRA_CTIET().AsEnumerable().Where(x => x.SO_HIEU != "");
                //List<YHL_KIENHANG_GUI_CTIET> arrkq = new List<YHL_KIENHANG_GUI_CTIET>();

                var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
                year = year == null ? DateTime.Now.Year : year;
                month = month == null ? DateTime.Now.Month : month;
                single = single == null ? "month" : single;
                quarter = quarter == null ? 1 : quarter;

                Calendar calendar = CultureInfo.InvariantCulture.Calendar;
                var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                week = week == null ? weekdefault : week;


                DateTime StartDate = DateTime.Now;
                DateTime EndDate = DateTime.Now;

                ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

                var data = SqlHelper.QuerySP<YHL_KIENHANG_GUI_CTIETViewModel>("DOANHSOKIENHANGs", new
                {
                    startdate = StartDate,
                    EndDate = EndDate

                }).ToList();
                int tmp = 0;
                //q = q.Where(x => x.CreatedDate >= StartDate && x.CreatedDate <= EndDate);
                // tra = tra.Where(x => x.CreatedDate >= StartDate && x.CreatedDate <= EndDate);
                //foreach (var item in q)
                //{
                //    int i = 0;
                //    foreach (var iss in tra)
                //    {
                //        if (item.SO_HIEU == iss.SO_HIEU)
                //        {
                //            i = 1;
                //        }
                //    }
                //    if (i == 0)
                //    {
                //        arrkq.Add(item);
                //    }
                //}
                //if (!string.IsNullOrEmpty(branchId))
                //{
                //  q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true));
                //}
                //if (!string.IsNullOrEmpty(CityId))
                //{
                //    q = q.Where(x => x. == CityId);
                //}
                //if (!string.IsNullOrEmpty(DistrictId))
                //{
                //    q = q.Where(x => x.DistrictId == DistrictId);
                //}
                var model = data.Select(item => new YHL_KIENHANG_GUI_CTIETViewModel
                {

                    KIENHANG_GUI_CTIET_ID = item.KIENHANG_GUI_CTIET_ID,
                    KIENHANG_GUI_ID = item.KIENHANG_GUI_ID,
                    TRANG_THAIDONHANG_GUI = item.TRANG_THAIDONHANG_GUI,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    GHI_CHU = item.GHI_CHU,
                    IsDeleted = item.IsDeleted,
                    CreatedUserId = item.CreatedUserId,

                    AssignedUserId = item.AssignedUserId,
                    STT_DEN = item.STT_DEN,

                    SO_HIEU = item.SO_HIEU,
                    MA_DON_HANG = item.MA_DON_HANG,
                    NGAY_KG = item.NGAY_KG,
                    NGUOI_GUI = item.NGUOI_GUI,
                    NGUOI_NHAN = item.NGUOI_NHAN,
                    DC_NHAN = item.DC_NHAN,
                    TEN_BC_NHAN = item.TEN_BC_NHAN,
                    KHOI_LUONG = item.KHOI_LUONG,
                    KHOI_LUONG_QD = item.KHOI_LUONG_QD,
                    NOI_DUNG = item.NOI_DUNG,
                    DV_DB = item.DV_DB,
                    TRI_GIA = item.TRI_GIA,
                    vung_xa = item.vung_xa,
                    CUOC_DV = item.CUOC_DV,
                    Cuoc_COD = item.Cuoc_COD,
                    CUOC_DVCT = item.CUOC_DVCT,
                    TIEN_VAT = item.TIEN_VAT,
                    TONG_CUOC = item.TONG_CUOC,
                    DT_NHAN = item.DT_NHAN,
                    TRI_GIA_MUA = item.TRI_GIA_MUA,
                    TRI_GIA_TRA = item.TRI_GIA_TRA

                    //    StaffId = item.StaffId
                }).OrderByDescending(m => m.CreatedDate).ToList();
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult Sale_BaoCaoXuatKho()
        {

            return View();
        }
        public PartialViewResult _GetListSale_BCXK(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId, string manufacturer)
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
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = intBrandID.ToString();
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = new List<Sale_BaoCaoXuatKhoViewModel>();
            data = SqlHelper.QuerySP<Erp.BackOffice.Sale.Models.Sale_BaoCaoXuatKhoViewModel>("spSale_BaoCaoXuatKho", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            if (!string.IsNullOrEmpty(manufacturer))
            {
                data = data.Where(x => x.Manufacturer == manufacturer).ToList();
            }
            ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");
            return PartialView(data);
        }

        public ActionResult Sale_BaoCaoNhapXuatTon()
        {
            return View();
        }

        public PartialViewResult _GetListSale_BCNXT(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId, string manufacturer)
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
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = intBrandID.ToString();
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            manufacturer = manufacturer == null ? "" : manufacturer;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            if (!string.IsNullOrEmpty(manufacturer))
            {
                data = data.Where(x => x.Manufacturer == manufacturer).ToList();
            }
            ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");
            return PartialView(data);
        }

        public ActionResult Sale_BaoCaoTonKho()
        {
            return View();
        }

        public PartialViewResult _GetListSale_BCTK(string CityId, string DistrictId, string BranchId, int? WarehouseId, string ProductGroup, string CategoryCode, string Manufacturer, int? page, int? size, string NameCode)
        {
            //get cookie brachID 
            //page = 7;
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
            //BranchId = string.IsNullOrEmpty(BranchId) ? Helpers.Common.CurrentUser.BranchId.ToString() : BranchId;
            BranchId = intBrandID.ToString();
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;


            var data = SqlHelper.QuerySP<Sale_BaoCaoTonKhoViewModel>("spSale_BaoCaoTonKho", new
            {
                BranchId = BranchId,
                WarehouseId = WarehouseId,
                ProductGroup = ProductGroup,
                CategoryCode = CategoryCode,
                Manufacturer = Manufacturer,
                CityId = CityId,
                DistrictId = DistrictId
            }).Where(n => n.Price != null && n.WarehouseName != null).ToList();
            //var branch_list = BranchRepository.GetAllBranch().ToList();
            var Warehouse = warehouseRepository.GetAllWarehouse().ToList();
            var listproduct = ProductRepository.GetAllProduct().Select(x => new ProductViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            }).ToList();
            if (!string.IsNullOrEmpty(NameCode))
            {
                NameCode = NameCode == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(NameCode).Trim();

                listproduct = listproduct.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(NameCode) || x.Code.ToLower().Contains(NameCode)).ToList();

            }
            if (intBrandID != null && intBrandID > 0)
            {

                Warehouse = Warehouse.Where(x => x.BranchId == intBrandID).ToList();
            }
            if (WarehouseId != 0)
            {
                Warehouse = Warehouse.Where(x => x.Id == WarehouseId).ToList();
            }


            //phân trang trong foreach
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });


            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }


            ViewBag.size = items;
            ViewBag.currentSize = size;

            page = page ?? 1;

            int pageSize = (size ?? 10);


            int pageNumber = (page ?? 1);
            //end phân trang trong foreach

            ViewBag.data = data;
            ViewBag.Warehouse = Warehouse;
            return PartialView(listproduct.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Sale_BaoCaoCongNoTongHop()
        {
            return View();
        }

        public PartialViewResult _GetListSale_BCCNTH(string StartDate, string EndDate, string BranchId, int? SalerId)
        {
            StartDate = StartDate == null ? "" : StartDate;
            EndDate = EndDate == null ? "" : EndDate;
            BranchId = string.IsNullOrEmpty(BranchId) ? Erp.BackOffice.Helpers.Common.GetSetting("BranchId") : BranchId;
            SalerId = SalerId == null ? 0 : SalerId;

            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            if (StartDate == null && EndDate == null && string.IsNullOrEmpty(BranchId))
            {
                StartDate = aDateTime.ToString("dd/MM/yyyy");
                EndDate = retDateTime.ToString("dd/MM/yyyy");
            }
            var d_startDate = (!string.IsNullOrEmpty(StartDate) ? DateTime.ParseExact(StartDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");
            var d_endDate = (!string.IsNullOrEmpty(EndDate) ? DateTime.ParseExact(EndDate.ToString(), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss") : "");

            var data = SqlHelper.QuerySP<Sale_BaoCaoCongNoTongHopViewModel>("spSale_BaoCaoCongNoTongHop", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                BranchId = BranchId,
                SalerId = SalerId
            }).ToList();
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            return PartialView(data);
        }

        public ActionResult Sale_LiabilitiesDrugStore(string single, int? year, int? month, int? quarter, int? week, int? branchId, string CityId, string DistrictId)
        {
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            string BranchId = branchId == null ? Erp.BackOffice.Helpers.Common.GetSetting("BranchId") : branchId.Value.ToString();
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");

            var data = SqlHelper.QuerySP<ProductInvoiceViewModel>("spSale_LiabilitiesDrugStore_haopd", new
            {
                StartDate = d_startDate,
                EndDate = d_endDate,
                branchId = BranchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            return View(data);
        }

        public ActionResult Sale_BCDoanhSoTheoSP(string start, string end)
        {
            var PaymentMethod = Request["Paymethod"];
            var q = invoiceRepository.GetAllvwInvoiceDetails().AsEnumerable().Where(x => x.IsArchive == true).ToList();

            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(start, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(end, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    q = q.Where(x => x.ProductInvoiceDate >= d_startDate && x.ProductInvoiceDate <= d_endDate).ToList();
                }
            }
            if (!string.IsNullOrEmpty(PaymentMethod))
            {
                q = q.Where(x => x.PaymentMethod == PaymentMethod).ToList();
            }


            var model = q.Select(item => new ProductInvoiceDetailViewModel
            {
                Id = item.Id,
                CustomerCode = item.CustomerCode,
                ProductId = item.ProductId,
                Amount = item.Amount2,
                CompanyName = item.CompanyName,
                ProductName = item.ProductName,
                ProductCode = item.ProductCode,
                ProvinceName = item.ProvinceName,
                DistrictName = item.DistrictName,
                Address = item.Address,
                Quantity = item.Quantity,
                BranchId = item.BranchId

            }).ToList();

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
            var branch_list = BranchRepository.GetAllBranch().ToList();

            if (intBrandID != null && intBrandID > 0)
            {
                model = model.Where(x => x.BranchId == intBrandID).ToList();
                branch_list = branch_list.Where(x => x.Id == intBrandID).ToList();
            }

            var list_Customer = model.GroupBy(x => new { x.CustomerCode, x.CompanyName })
                .Select(x => new CustomerViewModel
                {
                    Code = x.Key.CustomerCode,
                    CompanyName = x.Key.CompanyName,
                    ProvinceName = x.FirstOrDefault().ProvinceName,
                    DistrictName = x.FirstOrDefault().DistrictName,
                    Address = x.FirstOrDefault().Address

                }).OrderBy(x => x.ProvinceName).ThenBy(x => x.DistrictName).ToList();
            ViewBag.list_Customer = list_Customer;

            var product_list = model.GroupBy(x => new { x.ProductId, x.ProductCode, x.ProductName })
                .Select(x => new ProductViewModel
                {
                    Id = x.Key.ProductId.Value,
                    Name = x.Key.ProductName,
                    Code = x.Key.ProductCode
                }).ToList();
            ViewBag.productList = product_list;
            ViewBag.branchlist = branch_list;
            return View(model);
        }

        public PartialViewResult ChartInboundAndOutbound(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? Erp.BackOffice.Helpers.Common.GetSetting("BranchId") : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;

            var jsonData = new List<ChartItem>();
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            //kiểm tra chi nhánh - cong
            HttpRequestBase request = this.Request;
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

            //int? intBrandID = int.Parse(strBrandID);

            branchId = strBrandID;

            //
            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();

            foreach (var item in data.GroupBy(x => x.ProductCode))
            {
                var obj = new Erp.BackOffice.Sale.Models.ChartItem();
                obj.label = item.Key;
                obj.data = item.Sum(x => x.Last_InboundQuantity);
                obj.data2 = item.Sum(x => x.Last_OutboundQuantity);
                jsonData.Add(obj);
            }
            //ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            //ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");
            string json = JsonConvert.SerializeObject(jsonData);
            ViewBag.json = json;
            return PartialView(data);
        }

        //public ActionResult PrintDTNgay(string startDate, string endDate, string CityId, string DistrictId, int? branchId, bool ExportExcel = false)
        //{
        //    var model = new TemplatePrintViewModel();
        //    //lấy logo công ty
        //    var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
        //    var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
        //    var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
        //    var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
        //    var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
        //    var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
        //    var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
        //    var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
        //    //lấy hóa đơn.
        //    var q = invoiceRepository.GetAllvwProductInvoice().Where(x => x.IsArchive == true);
        //    var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
        //    if (startDate == null && endDate == null)
        //    {
        //        DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //        // Cộng thêm 1 tháng và trừ đi một ngày.
        //        DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
        //        startDate = aDateTime.ToString("dd/MM/yyyy");
        //        endDate = retDateTime.ToString("dd/MM/yyyy");
        //    }
        //    //Lọc theo ngày
        //    DateTime d_startDate, d_endDate;
        //    if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
        //    {
        //        if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
        //        {
        //            d_endDate = d_endDate.AddHours(23).AddMinutes(59);
        //            ViewBag.retDateTime = d_endDate;
        //            ViewBag.aDateTime = d_startDate;
        //            q = q.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate);
        //        }
        //    }
        //    var branch_list = BranchRepository.GetAllBranch().Where(x => x.ParentId != null).Select(item => new BranchViewModel
        //    {
        //        Id = item.Id,
        //        Name = item.Name,
        //        Code = item.Code,
        //        DistrictId = item.DistrictId,
        //        CityId = item.CityId
        //    }).ToList();
        //    if (branchId != null && branchId.Value > 0)
        //    {
        //        q = q.Where(x => x.BranchId == branchId);
        //        branch_list = branch_list.Where(x => x.Id == branchId).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(CityId))
        //    {
        //        q = q.Where(x => x.CityId == CityId);
        //        branch_list = branch_list.Where(x => x.CityId == CityId).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(DistrictId))
        //    {
        //        q = q.Where(x => x.DistrictId == DistrictId);
        //        branch_list = branch_list.Where(x => x.DistrictId == DistrictId).ToList();
        //    }
        //    var detail = q.Select(item => new ProductInvoiceViewModel
        //    {
        //        Id = item.Id,
        //        IsDeleted = item.IsDeleted,
        //        CreatedDate = item.CreatedDate,
        //        BranchId = item.BranchId,
        //        TotalAmount = item.TotalAmount,
        //        RemainingAmount = item.RemainingAmount,
        //        PaidAmount = item.PaidAmount,
        //        Month = item.Month,
        //        Day = item.Day,
        //        Year = item.Year
        //    }).OrderByDescending(m => m.CreatedDate).ToList();
        //    if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan())
        //    {
        //        branch_list = branch_list.Where(x => ("," + user.DrugStore + ",").Contains("," + x.Id + ",") == true).ToList();
        //    }

        //    //lấy template phiếu xuất.
        //    var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInvoice")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        //    //truyền dữ liệu vào template.
        //    model.Content = template.Content;

        //    model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintDTNgay(detail, d_startDate, d_endDate));
        //    model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
        //    model.Content = model.Content.Replace("{System.CompanyName}", company);
        //    model.Content = model.Content.Replace("{System.AddressCompany}", address);
        //    model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
        //    model.Content = model.Content.Replace("{System.Fax}", fax);
        //    model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
        //    model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
        //    model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

        //    if (ExportExcel)
        //    {
        //        Response.AppendHeader("content-disposition", "attachment;filename=" + "DoanhThuTheoNgay" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
        //        Response.Charset = "";
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.Write(model.Content);
        //        Response.End();
        //    }
        //    return View(model);
        //}

        //string buildHtmlDetailList_PrintDTNgay(List<ProductInvoiceViewModel> detailList, DateTime d_startDate, DateTime d_endDate)
        //{
        //    decimal? tong_tien = 0;

        //    //Tạo table html chi tiết phiếu xuất
        //    string detailLists = "<table class=\"invoice-detail\">\r\n";
        //    detailLists += "<thead>\r\n";
        //    detailLists += "	<tr>\r\n";

        //    detailLists += "		<th>STT</th>\r\n";
        //    detailLists += "		<th>Nhà thuốc</th>\r\n";
        //    for (DateTime dt = d_startDate; dt <= d_endDate; dt = dt.AddDays(1))
        //    {
        //        detailLists += "		<th>" + dt.ToString("dd/MM/yyyy") + "</th>\r\n";
        //    }
        //    detailLists += "		<th>Số ngày</th>\r\n";
        //    detailLists += "		<th>Tổng tiền</th>\r\n";
        //    detailLists += "	</tr>\r\n";
        //    detailLists += "</thead>\r\n";
        //    detailLists += "<tbody>\r\n";
        //    var index = 1;

        //    foreach (var item in detailList)
        //    {
        //        decimal? subTotal = item.Quantity * item.Price.Value;

        //        tong_tien += subTotal;
        //        detailLists += "<tr>\r\n"
        //        + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
        //        + "<td class=\"text-left code_product\">" + item.BranchName + "</td>\r\n";
        //        for (DateTime dt = d_startDate; dt <= d_endDate; dt = dt.AddDays(1))
        //        {
        //            detailLists += "<td class=\"text-left \">" + item.ProductName + "</td>\r\n";
        //        }
        //        detailLists += "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
        //          + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
        //          + "<td class=\"text-center\">" + item.Unit + "</td>\r\n"
        //          + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Quantity) + "</td>\r\n"
        //          + "<td class=\"text-right code_product\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</td>\r\n"
        //          + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(subTotal, null) + "</td>\r\n"
        //          + "</tr>\r\n";
        //    }
        //    detailLists += "</tbody>\r\n";
        //    detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
        //    detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
        //                 + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong_tien, null)
        //                 + "</td></tr>\r\n";
        //    if (model.TaxFee > 0)
        //    {
        //        var vat = tong_tien * Convert.ToDecimal(model.TaxFee);
        //        var tong = tong_tien + vat;
        //        detailLists += "<tr><td colspan=\"8\" class=\"text-right\">VAT (" + model.TaxFee + "%)</td><td class=\"text-right\">"
        //                + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(vat, null)
        //                + "</td></tr>\r\n";
        //        detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng tiền cần thanh toán</td><td class=\"text-right\">"
        //            + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong, null)
        //            + "</td></tr>\r\n";
        //    }


        //    detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Đã thanh toán</td><td class=\"text-right\">"
        //                + (model.PaidAmount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model.PaidAmount, null) : "0")
        //                + "</td></tr>\r\n";
        //    detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Còn lại phải thu</td><td class=\"text-right\">"
        //                + (model.RemainingAmount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model.RemainingAmount, null) : "0")
        //                + "</td></tr>\r\n";
        //    detailLists += "</tfoot>\r\n</table>\r\n";

        //    return detailLists;
        //}

        public ActionResult PrintBCXK(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId, string manufacturer, bool ExportExcel = false)
        {
            ////get cookie brachID 
            //HttpRequestBase request = this.HttpContext.Request;
            //string strBrandID = "0";
            //if (request.Cookies["BRANCH_ID_CookieName"] != null)
            //{
            //    strBrandID = request.Cookies["BRANCH_ID_CookieName"].Value;
            //    if (strBrandID == "")
            //    {
            //        strBrandID = "0";
            //    }
            //}

            ////get  CurrentUser.branchId

            //if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
            //{
            //    strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
            //}

            //int? intBrandID = int.Parse(strBrandID);
            var model = new TemplatePrintViewModel();
            //lấy logo công ty
            var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? Erp.BackOffice.Helpers.Common.GetSetting("BranchId") : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = new List<Sale_BaoCaoXuatKhoViewModel>();
            data = SqlHelper.QuerySP<Erp.BackOffice.Sale.Models.Sale_BaoCaoXuatKhoViewModel>("spSale_BaoCaoXuatKho", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            if (!string.IsNullOrEmpty(manufacturer))
            {
                data = data.Where(x => x.Manufacturer == manufacturer).ToList();
            }
            ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
            ViewBag.EndDate = EndDate.ToString("dd/MM/yyyy");

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintBCXK")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCXK(data));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoXuatKho" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }
        string buildHtmlDetailList_PrintBCXK(List<Sale_BaoCaoXuatKhoViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Danh mục</th>\r\n";
            detailLists += "		<th>Nhà sản xuất</th>\r\n";
            detailLists += "		<th>Kho</th>\r\n";
            detailLists += "		<th>Số Lô</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>Đơn giá</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Xuất bán</th>\r\n";
            detailLists += "		<th>Xuất vận chuyển</th>\r\n";
            detailLists += "		<th>Xuất sử dụng</th>\r\n";
            detailLists += "		<th>Tổng xuất</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {
                var subTotal = item.invoice + item._internal + item.service;
                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                + "<td class=\"text-left \">" + item.CategoryCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.Manufacturer + "</td>\r\n"
                + "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</td>\r\n"
                + "<td class=\"text-center\">" + item.Unit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.invoice + "</td>\r\n"
            + "<td class=\"text-right orderNo\">" + item._internal + "</td>\r\n"
              + "<td class=\"text-right orderNo\">" + item.service + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + subTotal + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"10\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.invoice), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x._internal), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.service), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.service + x._internal + x.invoice), null)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        public ActionResult PrintBCTK(string NameCode, string CityId, string DistrictId, string BranchId, int? WarehouseId, string ProductGroup, string CategoryCode, string Manufacturer, bool ExportExcel = false)
        {
            var model = new TemplatePrintViewModel();
            //lấy logo công ty
            var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            BranchId = string.IsNullOrEmpty(BranchId) ? Erp.BackOffice.Helpers.Common.GetSetting("BranchId") : BranchId;
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;

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
            //BranchId = string.IsNullOrEmpty(BranchId) ? Helpers.Common.CurrentUser.BranchId.ToString() : BranchId;
            BranchId = intBrandID.ToString();

            var data = SqlHelper.QuerySP<Sale_BaoCaoTonKhoViewModel>("spSale_BaoCaoTonKho", new
            {
                BranchId = BranchId,
                WarehouseId = WarehouseId,
                ProductGroup = "",
                CategoryCode = "",
                Manufacturer = "",
                CityId = "",
                DistrictId = ""
            }).ToList();
            var Warehouse = warehouseRepository.GetAllWarehouse().ToList();
            var listproduct = ProductRepository.GetAllProduct().Select(x => new ProductViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            }).ToList();
            if (!string.IsNullOrEmpty(NameCode))
            {
                NameCode = NameCode == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(NameCode).Trim();

                listproduct = listproduct.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(NameCode) || x.Code.ToLower().Contains(NameCode)).ToList();

            }
            if (intBrandID != null && intBrandID > 0)
            {

                Warehouse = Warehouse.Where(x => x.BranchId == intBrandID).ToList();
            }
            if (WarehouseId != 0)
            {
                Warehouse = Warehouse.Where(x => x.Id == WarehouseId).ToList();
            }

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintBCTK")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCTK(data, listproduct, Warehouse));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoTonKho" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }
        string buildHtmlDetailList_PrintBCTK(List<Sale_BaoCaoTonKhoViewModel> detailList, List<ProductViewModel> listproduct, List<Warehouse> Warehouse)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            //detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Tổng</th>\r\n";
            foreach (var item in Warehouse)
            {
                detailLists += "<th>" + item.Name + "</th>\r\n";
            }
            //detailLists += "		<th>Nhà sản xuất</th>\r\n";
            //detailLists += "		<th>Kho</th>\r\n";
            //detailLists += "		<th>Số Lô</th>\r\n";
            //detailLists += "		<th>Hạn dùng</th>\r\n";
            //detailLists += "		<th>ĐVT</th>\r\n";
            //detailLists += "		<th>Số lượng</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in listproduct)
            {
                var tong = detailList.Where(x => x.ProductCode == item.Code).Sum(x => x.Quantity);
                detailLists += "<tr>\r\n"
                //+ "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.Code + "</td>\r\n"
                + "<td class=\"text-left \">" + item.Name + "</td>\r\n"
                + "<td class=\"text-left \">" + tong + "</td>\r\n";

                foreach (var i in Warehouse)
                {
                    var sl = detailList.Where(x => x.ProductCode == item.Code && x.WarehouseName == i.Name).Sum(x => x.Quantity);

                    detailLists += "<td class=\"text-left \">" + sl + "</td>\r\n";
                }

                detailLists += "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"9\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.Quantity)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        public ActionResult PrintBCNXT(string single, int? year, int? month, int? quarter, int? week, string CityId, string DistrictId, string branchId, int? WarehouseId, string manufacturer, bool ExportExcel = false)
        {
            var model = new TemplatePrintViewModel();
            //lấy logo công ty
            var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            single = single == null ? "month" : single;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? Erp.BackOffice.Helpers.Common.GetSetting("BranchId") : branchId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            var data = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
            {
                StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                WarehouseId = WarehouseId,
                branchId = branchId,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            if (!string.IsNullOrEmpty(manufacturer))
            {
                data = data.Where(x => x.Manufacturer == manufacturer).ToList();
            }
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintBCNXT")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCNXT(data));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoXuatNhapTon" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }
        string buildHtmlDetailList_PrintBCNXT(List<Sale_BaoCaoNhapXuatTonViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Kho</th>\r\n";
            detailLists += "		<th>Số Lô</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>Đơn giá nhập</th>\r\n";
            detailLists += "		<th>Đơn giá xuất</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Tồn đầu kỳ</th>\r\n";

            detailLists += "		<th>Nhập trong kỳ</th>\r\n";
            detailLists += "		<th>Xuất trong kỳ</th>\r\n";
            detailLists += "		<th>Tồn cuối kỳ</th>\r\n";
            detailLists += "		<th>Tổng tiền tồn</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {

                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                + "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right code_product\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-right \">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound, null) + "</td>\r\n"
                + "<td class=\"text-right \">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceOutbound, null) + "</td>\r\n"
                + "<td class=\"text-center\">" + item.ProductUnit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.First_Remain + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.Last_InboundQuantity + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.Last_OutboundQuantity + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.Remain + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound * item.Remain, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"9\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.First_Remain)
                         + "</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.Last_InboundQuantity)
                         + "</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.Last_OutboundQuantity)
                         + "</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.Remain)
                          + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Remain * x.PriceInbound), null)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }


        public ActionResult Sale_BCTong(int? year, int? month, string CityId, string DistrictId, string branchId)
        {
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            int? week = 1;
            int quarter = 1;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, "month", year.Value, month.Value, quarter, ref week);
            bool hasSearch = false;

            var q = TotalDiscountMoneyNTRepository.GetvwAllTotalDiscountMoneyNT().ToList();
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Year == year).ToList();
                //   hasSearch = true;
            }
            if (!string.IsNullOrEmpty(branchId))
            {
                hasSearch = true;
                q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.DrugStoreId + ",") == true)).ToList();
            }
            if (month != null && month.Value > 0)
            {
                //   hasSearch = true;
                q = q.Where(x => x.Month == month).ToList();
            }
            if (hasSearch)
            {
                if (q.Count() > 0)
                {
                    decimal pDoanhsoTong = 0;
                    var TotalDiscountMoneyNT = q.FirstOrDefault();
                    if (TotalDiscountMoneyNT != null && TotalDiscountMoneyNT.IsDeleted != true)
                    {
                        var model = new TotalDiscountMoneyNTViewModel();
                        AutoMapper.Mapper.Map(TotalDiscountMoneyNT, model);
                        model.ListNXT = new List<Sale_BaoCaoNhapXuatTonViewModel>();
                        var invoice = invoiceRepository.GetAllvwProductInvoice().ToList().Where(x => ("," + branchId + ",").Contains("," + x.BranchId + ",") == true && x.IsArchive == true);
                        pDoanhsoTong = invoice.Sum(x => x.TotalAmount);
                        if (year != null && year.Value > 0)
                        {
                            invoice = invoice.Where(x => x.Year == year).ToList();
                            //   hasSearch = true;
                        }
                        if (month != null && month.Value > 0)
                        {
                            //   hasSearch = true;
                            invoice = invoice.Where(x => x.Month == month).ToList();
                        }


                        model.DoanhSo = invoice.Sum(x => x.TotalAmount);
                        model.ChietKhauCoDinh = invoice.Sum(x => x.FixedDiscount);
                        model.ChietKhauDotXuat = invoice.Sum(x => x.IrregularDiscount);


                        #region Hoapd tinh lai VIP

                        var qProductInvoiceVIP = invoiceRepository.GetAllvwProductInvoice().AsEnumerable()
                            .Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true && item.IsArchive == true));

                        if (year != null && year.Value > 0)
                        {
                            qProductInvoiceVIP = qProductInvoiceVIP.Where(x => x.Year <= year).ToList();
                            //   hasSearch = true;
                        }
                        if (month != null && month.Value > 0)
                        {
                            //   hasSearch = true;
                            qProductInvoiceVIP = qProductInvoiceVIP.Where(x => ((x.Month <= month && x.Year == year) || (x.Year < year))).ToList();
                        }

                        decimal pDoansoVip = qProductInvoiceVIP.Sum(x => x.TotalAmount);
                        var setting = settingRepository.GetSettingByKey("setting_point").Value;
                        setting = string.IsNullOrEmpty(setting) ? "0" : setting;
                        var rf = pDoansoVip / Convert.ToDecimal(setting);
                        string[] arrVal = rf.ToString().Split(',');
                        var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                        model.PointVIP = value;

                        #endregion








                        model.ListNXT = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
                        {
                            StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            WarehouseId = "",
                            branchId = branchId,
                            CityId = CityId,
                            DistrictId = DistrictId
                        }).ToList();
                        return View(model);
                    }
                }
                else
                {
                    var model = new TotalDiscountMoneyNTViewModel();
                    model.ListNXT = new List<Sale_BaoCaoNhapXuatTonViewModel>();
                    ViewBag.FailedMessage = "Không tìm thấy dữ liệu";
                    return View(model);
                }
            }
            else
            {
                var model = new TotalDiscountMoneyNTViewModel();
                model.ListNXT = new List<Sale_BaoCaoNhapXuatTonViewModel>();
                ViewBag.FailedMessage = "Chưa chọn nhà thuốc";
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Sale_BCTong");
        }

        #region PrintBCTong
        public ActionResult PrintBCTong(int? year, int? month, string CityId, string DistrictId, string branchId, bool ExportExcel = false)
        {
            var model = new TemplatePrintViewModel();
            //lấy logo công ty
            var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            branchId = string.IsNullOrEmpty(branchId) ? "" : branchId;
            int? week = 1;
            int quarter = 1;
            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            {
                branchId = Helpers.Common.CurrentUser.DrugStore;
            }

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, "month", year.Value, month.Value, quarter, ref week);
            bool hasSearch = false;

            var q = TotalDiscountMoneyNTRepository.GetvwAllTotalDiscountMoneyNT().ToList();
            if (year != null && year.Value > 0)
            {
                q = q.Where(x => x.Year == year).ToList();
            }
            if (!string.IsNullOrEmpty(branchId))
            {
                hasSearch = true;
                q = q.Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.DrugStoreId + ",") == true)).ToList();
            }
            if (month != null && month.Value > 0)
            {
                q = q.Where(x => x.Month == month).ToList();
            }
            if (hasSearch)
            {
                if (q.Count() > 0)
                {
                    var TotalDiscountMoneyNT = q.FirstOrDefault();
                    if (TotalDiscountMoneyNT != null && TotalDiscountMoneyNT.IsDeleted != true)
                    {
                        var model2 = new TotalDiscountMoneyNTViewModel();
                        AutoMapper.Mapper.Map(TotalDiscountMoneyNT, model2);
                        model2.ListNXT = new List<Sale_BaoCaoNhapXuatTonViewModel>();
                        var invoice = invoiceRepository.GetAllvwProductInvoice().ToList().Where(x => ("," + branchId + ",").Contains("," + x.BranchId + ",") == true && x.IsArchive == true);
                        decimal pDoansotong = invoice.Sum(x => x.TotalAmount);

                        if (year != null && year.Value > 0)
                        {
                            invoice = invoice.Where(x => x.Year == year).ToList();
                        }
                        if (month != null && month.Value > 0)
                        {
                            invoice = invoice.Where(x => x.Month == month).ToList();
                        }

                        model2.DoanhSo = invoice.Sum(x => x.TotalAmount);
                        model2.ChietKhauCoDinh = invoice.Sum(x => x.FixedDiscount);
                        model2.ChietKhauDotXuat = invoice.Sum(x => x.IrregularDiscount);




                        #region Hoapd tinh lai VIP

                        var qProductInvoiceVIP = invoiceRepository.GetAllvwProductInvoice().AsEnumerable()
                            .Where(item => (string.IsNullOrEmpty(branchId) || ("," + branchId + ",").Contains("," + item.BranchId + ",") == true && item.IsArchive == true));

                        if (year != null && year.Value > 0)
                        {
                            qProductInvoiceVIP = qProductInvoiceVIP.Where(x => x.Year <= year).ToList();
                            //   hasSearch = true;
                        }
                        if (month != null && month.Value > 0)
                        {
                            //   hasSearch = true;
                            qProductInvoiceVIP = qProductInvoiceVIP.Where(x => ((x.Month <= month && x.Year == year) || (x.Year < year))).ToList();
                        }

                        decimal pDoansoVip = qProductInvoiceVIP.Sum(x => x.TotalAmount);
                        var setting = settingRepository.GetSettingByKey("setting_point").Value;
                        setting = string.IsNullOrEmpty(setting) ? "0" : setting;
                        var rf = pDoansoVip / Convert.ToDecimal(setting);
                        string[] arrVal = rf.ToString().Split(',');
                        var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                        model2.PointVIP = value;

                        #endregion















                        model2.ListNXT = SqlHelper.QuerySP<Sale_BaoCaoNhapXuatTonViewModel>("spSale_BaoCaoNhapXuatTon", new
                        {
                            StartDate = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            EndDate = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            WarehouseId = "",
                            branchId = branchId,
                            CityId = CityId,
                            DistrictId = DistrictId
                        }).ToList();
                        //lấy template phiếu xuất.
                        var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintBCTong")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                        //truyền dữ liệu vào template.
                        model.Content = template.Content;
                        model.Content = model.Content.Replace("{DrugStoreName}", model2.DrugStoreName);
                        model.Content = model.Content.Replace("{Address}", model2.Address + ", " + model2.WardName + ", " + model2.DistrictName + ", " + model2.ProvinceName);
                        model.Content = model.Content.Replace("{DoanhSo}", model2.DoanhSo.ToCurrencyStr(null));
                        model.Content = model.Content.Replace("{CKCD}", model2.ChietKhauCoDinh.ToCurrencyStr(null));
                        model.Content = model.Content.Replace("{CKDX}", model2.ChietKhauDotXuat.ToCurrencyStr(null));
                        model.Content = model.Content.Replace("{MonthYear}", model2.Month + " NĂM " + model2.Year);
                        model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        model.Content = model.Content.Replace("{QuantityDay}", Erp.BackOffice.Helpers.Common.PhanCachHangNgan2(model2.QuantityDay));
                        model.Content = model.Content.Replace("{PercentDecrease}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model2.PercentDecrease, null));
                        model.Content = model.Content.Replace("{DecreaseAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model2.DecreaseAmount, null));
                        model.Content = model.Content.Replace("{DiscountAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model2.DiscountAmount, null));
                        model.Content = model.Content.Replace("{RemainingAmount}", Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model2.RemainingAmount, null));
                        model.Content = model.Content.Replace("{PointVIP}", Erp.BackOffice.Helpers.Common.PhanCachHangNgan(model2.PointVIP));
                        model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintBCTong(model2.ListNXT));
                        model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
                        model.Content = model.Content.Replace("{System.CompanyName}", company);
                        model.Content = model.Content.Replace("{System.AddressCompany}", address);
                        model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
                        model.Content = model.Content.Replace("{System.Fax}", fax);
                        model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
                        model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
                        model.Content = model.Content.Replace("{MonthYearDS}", model2.Month + "/" + model2.Year);

                        if (ExportExcel)
                        {
                            Response.AppendHeader("content-disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMdd") + "BCTong" + ".xls");
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.Write(model.Content);
                            Response.End();
                        }
                    }
                }
                else
                {
                    ViewBag.FailedMessage = "Không tìm thấy dữ liệu";
                    return View(model);
                }
            }
            else
            {
                ViewBag.FailedMessage = "Chưa chọn nhà thuốc";
                return View(model);
            }

            return View(model);
        }




        string buildHtmlDetailList_PrintBCTong(List<Sale_BaoCaoNhapXuatTonViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            //detailLists += "		<th>Kho</th>\r\n";
            detailLists += "		<th>Số Lô</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>Đơn giá nhập</th>\r\n";
            detailLists += "		<th>Đơn giá xuất</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Tồn đầu kỳ</th>\r\n";

            detailLists += "		<th>Nhập trong kỳ</th>\r\n";
            detailLists += "		<th>Xuất trong kỳ</th>\r\n";
            detailLists += "		<th>Tồn cuối kỳ</th>\r\n";
            detailLists += "		<th>Tổng tiền tồn</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {

                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                //+ "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right expiry_date\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-right \">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound, null) + "</td>\r\n"
                + "<td class=\"text-right \">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceOutbound, null) + "</td>\r\n"
                + "<td class=\"text-center\">" + item.ProductUnit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.First_Remain + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.Last_InboundQuantity + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.Last_OutboundQuantity + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.Remain + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.PriceInbound * item.Remain, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"8\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.First_Remain)
                         + "</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.Last_InboundQuantity)
                         + "</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.Last_OutboundQuantity)
                         + "</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.Remain)
                          + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.Remain * x.PriceInbound), null)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        #endregion

        public ActionResult Sale_InventoryExpiryDate()
        {
            return View();
        }
        public PartialViewResult _GetSale_InventoryExpiryDate(string CityId, string DistrictId, string BranchId, int? WarehouseId, string ProductGroup, string CategoryCode, string Manufacturer, string StartDate, string EndDate)
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
            BranchId = intBrandID.ToString();
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;

            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            //.AddHours(23).AddMinutes(59);
            EndDate = string.IsNullOrEmpty(EndDate) ? retDateTime.ToString("dd/MM/yyyy") : EndDate;
            StartDate = string.IsNullOrEmpty(StartDate) ? aDateTime.ToString("dd/MM/yyyy") : StartDate;

            var data = SqlHelper.QuerySP<Sale_BaoCaoTonKhoViewModel>("spSale_BaoCaoTonKho", new
            {
                BranchId = BranchId,
                WarehouseId = WarehouseId,
                ProductGroup = ProductGroup,
                CategoryCode = CategoryCode,
                Manufacturer = Manufacturer,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();

            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(StartDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(EndDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    data = data.Where(x => x.ExpiryDate >= d_startDate && x.ExpiryDate <= d_endDate).ToList();
                }
            }
            return PartialView(data);
        }
        public ActionResult PrintInventoryExpiryDate(string CityId, string DistrictId, string BranchId, int? WarehouseId, string ProductGroup, string CategoryCode, string Manufacturer, string StartDate, string EndDate, bool ExportExcel = false)
        {
            var model = new TemplatePrintViewModel();
            //lấy logo công ty
            var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
            var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
            var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
            var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
            var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
            var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
            var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            BranchId = string.IsNullOrEmpty(BranchId) ? Erp.BackOffice.Helpers.Common.GetSetting("BranchId") : BranchId;
            CityId = CityId == null ? "" : CityId;
            DistrictId = DistrictId == null ? "" : DistrictId;
            WarehouseId = WarehouseId == null ? 0 : WarehouseId;
            ProductGroup = ProductGroup == null ? "" : ProductGroup;
            CategoryCode = CategoryCode == null ? "" : CategoryCode;
            Manufacturer = Manufacturer == null ? "" : Manufacturer;
            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            // Cộng thêm 1 tháng và trừ đi một ngày.
            DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
            //.AddHours(23).AddMinutes(59);
            EndDate = string.IsNullOrEmpty(EndDate) ? retDateTime.ToString("dd/MM/yyyy") : EndDate;
            StartDate = string.IsNullOrEmpty(StartDate) ? aDateTime.ToString("dd/MM/yyyy") : StartDate;

            var data = SqlHelper.QuerySP<Sale_BaoCaoTonKhoViewModel>("spSale_BaoCaoTonKho", new
            {
                BranchId = BranchId,
                WarehouseId = WarehouseId,
                ProductGroup = ProductGroup,
                CategoryCode = CategoryCode,
                Manufacturer = Manufacturer,
                CityId = CityId,
                DistrictId = DistrictId
            }).ToList();
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(StartDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(EndDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    data = data.Where(x => x.ExpiryDate >= d_startDate && x.ExpiryDate <= d_endDate).ToList();
                }
            }
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("PrintInventoryExpiryDate")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList_PrintInventoryExpiryDate(data));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + "BaoCaoSPHetHanSuDung" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }

        string buildHtmlDetailList_PrintInventoryExpiryDate(List<Sale_BaoCaoTonKhoViewModel> detailList)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Danh mục</th>\r\n";
            detailLists += "		<th>Nhà sản xuất</th>\r\n";
            detailLists += "		<th>Kho</th>\r\n";
            detailLists += "		<th>Số Lô</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>Số lượng</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {
                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                + "<td class=\"text-left \">" + item.CategoryCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.Manufacturer + "</td>\r\n"
                + "<td class=\"text-left \">" + item.WarehouseName + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-center\">" + item.ProductUnit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + item.Quantity + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"9\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + detailList.Sum(x => x.Quantity)
                         + "</tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        public ActionResult ListLocation()
        {
            var data = locationRepository.GetAllList().ToList();
            return View(data);
        }
        #region CreateLocation
        public ActionResult CreateLocation(string Id, string ParentId, string Group)
        {
            var model = new LocationViewModel();
            if (!string.IsNullOrEmpty(Id))
            {
                model = SqlHelper.QuerySP<Erp.BackOffice.Sale.Models.LocationViewModel>("spGetAll_Province", new
                {
                    Group = Group,
                    Id = Id
                }).FirstOrDefault();
                model.Setting = "update";
                model.Group = Group;
            }
            else
            {
                model.Setting = "insert";
                model.Group = Group;
                if (model.Group == "District")
                {
                    model.ProvinceId = ParentId;
                }
                if (model.Group == "Ward")
                {
                    model.DistrictId = ParentId;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateLocation(LocationViewModel model, bool IsPopup)
        {
            if (ModelState.IsValid)
            {
                if (model.Group == "Province")
                {
                    var aa = SqlHelper.ExecuteSP("spInputProvince", new
                    {
                        Group = model.Setting,
                        Id = model.ProvinceId,
                        Name = model.Name,
                        Type = model.Type
                    });
                }
                if (model.Group == "District")
                {
                    var aa = SqlHelper.ExecuteSP("spInputDistrict", new
                    {
                        Group = model.Setting,
                        Id = model.DistrictId,
                        Name = model.Name,
                        Type = model.Type,
                        ProvinceId = model.ProvinceId,
                        Location = model.Location
                    });
                }
                if (model.Group == "Ward")
                {
                    var aa = SqlHelper.ExecuteSP("spInputWard", new
                    {
                        Group = model.Setting,
                        Id = model.WardId,
                        Name = model.Name,
                        Type = model.Type,
                        DistrictId = model.DistrictId,
                        Location = model.Location
                    });
                }


                if (IsPopup)
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                else
                {
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("ListLocation");
                }
            }
            return RedirectToAction("Create");
        }

        public ActionResult CheckIDLocation(string id, string setting)
        {
            setting = setting.Trim();
            var _location = locationRepository.GetAllList()
                .Where(item => item.Id == id).FirstOrDefault();
            if (_location != null)
            {
                if (!string.IsNullOrEmpty(id) && setting == "insert")
                    return Content("Mã này đã có!");
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

        #region Delete Location

        [HttpPost]
        public ActionResult DeleteLocation(string id, string group)
        {
            var aa = SqlHelper.ExecuteSP("spDeleteLocation", new
            {
                Group = group,
                Id = id
            });
            if (aa > 0)
                return Content("Success");
            else
                return Content("Error");
        }
        #endregion
        #endregion




        #region Doanh So Tong
        public ViewResult Revenue(string start, string end, int? UserId, int? size, int? page)
        {
            if (start == null && end == null)
            {
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // Cộng thêm 1 tháng và trừ đi một ngày.
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                start = aDateTime.ToString("dd/MM/yyyy");
                end = retDateTime.ToString("dd/MM/yyyy");
            }
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



            var productinvoice = invoiceRepository.GetAllvwProductInvoice().AsEnumerable().Where(x => (x.BranchId == intBrandID || intBrandID == 0) && (x.Status == "complete"));
            var invoicedetail = invoiceRepository.GetAllvwInvoiceDetails().AsEnumerable().Where(x => x.BranchId == intBrandID || intBrandID == 0); ;
            var branch_list = BranchRepository.GetAllBranch().ToList();
            var salereturn = salesReturnsRepository.GetAllSalesReturns().AsEnumerable().Where(x => x.BranchId == intBrandID || intBrandID == 0);
            //begin hoapd them vao de tinh tien ban hang, tien mat, chuyen khoan, quet the
            ViewBag.tienbanhang = 0;
            ViewBag.sodonhang = 0;
            ViewBag.tienhangtra = 0;
            ViewBag.sodonhangtra = 0;
            ViewBag.tienbanhang_TM = 0;
            ViewBag.tienbanhang_CK = 0;
            ViewBag.tienbanhang_THE = 0;





            //end hoapd them vao de tinh tien ban hang, tien mat, chuyen khoan, quet the


            //Lấy ds don hang 
            var modeldetail = invoicedetail.Select(x => new ProductInvoiceDetailViewModel
            {
                Id = x.Id,
                Price = x.Price,
                ProductId = x.ProductId,
                PromotionDetailId = x.PromotionDetailId,
                PromotionId = x.PromotionId,
                PromotionValue = x.PromotionValue,
                Quantity = x.Quantity,
                Unit = x.Unit,
                ProductType = x.ProductType,
                FixedDiscount = x.FixedDiscount,
                FixedDiscountAmount = x.FixedDiscountAmount,
                IrregularDiscountAmount = x.IrregularDiscountAmount,
                IrregularDiscount = x.IrregularDiscount,
                CategoryCode = x.CategoryCode,
                ProductInvoiceCode = x.ProductInvoiceCode,
                ProductName = x.ProductName,
                ProductCode = x.ProductCode,
                ProductInvoiceId = x.ProductInvoiceId,
                ProductGroup = x.ProductGroup,
                CheckPromotion = x.CheckPromotion,
                IsReturn = x.IsReturn,
                //Status = x.Status,
                Amount = x.Amount,
                LoCode = x.LoCode,
                ExpiryDate = x.ExpiryDate,
                Origin = x.Origin,
                TaxFeeAmount = x.TaxFeeAmount,
                TaxFee = x.TaxFee,
                Amount2 = x.Amount2,
                BranchId = x.BranchId,
                QuantitySaleReturn = x.QuantitySaleReturn,
                CreatedDate = x.CreatedDate,
                CreatedUserId = x.CreatedUserId

            }).ToList();


            var model = productinvoice.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                UserName = item.UserName,
                CreatedDate = item.CreatedDate,
                //CreatedDateTemp = item.CreatedDateTemp,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Code = item.Code,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                TotalAmount = item.TotalAmount,
                //FixedDiscount = item.FixedDiscount,
                TaxFee = item.TaxFee,
                CodeInvoiceRed = item.CodeInvoiceRed,
                Status = item.Status,
                IsArchive = item.IsArchive,
                ProductOutboundId = item.ProductOutboundId,
                ProductOutboundCode = item.ProductOutboundCode,
                Note = item.Note,
                CancelReason = item.CancelReason,
                BranchId = item.BranchId,
                CustomerId = item.CustomerId,
                BranchName = item.BranchName,
                CompanyName = item.CompanyName,
                Email = item.Email,
                CustomerPhone = item.CustomerPhone,
                Address = item.Address,
                DiscountTabBillAmount = item.DiscountTabBillAmount,
                DiscountTabBill = item.DiscountTabBill,
                isDisCountAllTab = item.isDisCountAllTab,
                DisCountAllTab = item.DisCountAllTab,
                Birthday = item.Birthday,
                Gender = item.Gender,
                Mobile = item.Mobile,
                TienTra = item.TienTra,
                SalerFullName = item.SalerFullName,
                MoneyPay = item.MoneyPay,
                TransferPay = item.TransferPay,
                ATMPay = item.ATMPay

            }).OrderByDescending(m => m.Id).ToList();

            //Lay nguoi ban
            var user = userRepository.GetAllvwUsers();
            var usermodel = user.Select(item => new UserViewModel
            {
                Id = item.Id,
                FullName = item.LastName + " " + item.FirstName,
                BranchId = item.BranchId
            }).ToList();


            //Lay cua hang
            var branch = branch_list.Select(item => new BranchViewModel
            {
                Id = item.Id,
                Name = item.Name

            }).ToList();
            //Lấy hàng trả 
            var salereturnmodel = salereturn.Select(x => new SalesReturnsViewModel
            {
                Id = x.Id,
                BranchId = x.BranchId,
                CreatedUserId = x.CreatedUserId,
                CreatedDate = x.CreatedDate,
                TotalAmount = x.TotalAmount

            }).ToList();

            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(start, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(end, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    model = model.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                    modeldetail = modeldetail.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                    salereturnmodel = salereturnmodel.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                }
            }

            if (intBrandID != null && intBrandID > 0)
            {
                model = model.Where(x => x.BranchId == intBrandID).ToList();
                branch = branch.Where(x => x.Id == intBrandID).ToList();
                modeldetail = modeldetail.Where(x => x.BranchId == intBrandID).ToList();
                //usermodel = usermodel.Where(x => x.BranchId == intBrandID).ToList();
                salereturnmodel = salereturnmodel.Where(x => x.BranchId == intBrandID).ToList();
            }
            if (UserId != null)
            {
                model = model.Where(x => x.CreatedUserId == UserId).ToList();
                modeldetail = modeldetail.Where(x => x.CreatedUserId == UserId).ToList();
                salereturnmodel = salereturnmodel.Where(x => x.CreatedUserId == UserId).ToList();
            }

            //phân trang trong foreach
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });


            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }


            ViewBag.size = items;
            ViewBag.currentSize = size;

            page = page ?? 1;

            int pageSize = (size ?? 10);


            int pageNumber = (page ?? 1);
            //end phân trang trong foreach

            ViewBag.page = page;
            ViewBag.Listdetails = modeldetail;
            ViewBag.BranchList = branch;
            ViewBag.user = usermodel;
            ViewBag.salereturn = salereturnmodel;
            ViewBag.model = model;



            if (model.Count() > 0)
            {
                ViewBag.tienbanhang = model.Sum(x => x.TotalAmount);
                ViewBag.sodonhang = model.Count();
                ViewBag.tienhangtra = salereturnmodel.Sum(x => x.TotalAmount);
                ViewBag.sodonhangtra = salereturnmodel.Count();
                ViewBag.tienbanhang_TM = model.Sum(x => x.MoneyPay);
                ViewBag.tienbanhang_CK = model.Sum(x => x.TransferPay);
                ViewBag.tienbanhang_THE = model.Sum(x => x.ATMPay);
            }



            return View(model.ToPagedList(pageNumber, pageSize));
            //return View(model);
        }

        public PartialViewResult _RevenueProduct(string start, string end, int? page, int? size, string searchproduct)
        {
            if (start == null && end == null)
            {
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // Cộng thêm 1 tháng và trừ đi một ngày.
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                start = aDateTime.ToString("dd/MM/yyyy");
                end = retDateTime.ToString("dd/MM/yyyy");
            }
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

            var details = invoiceRepository.GetAllvwInvoiceDetails().ToList();
            var productlist = ProductRepository.GetAllProduct().AsEnumerable();


            var salereturn = salesReturnsRepository.GetAllvwReturnsDetails().AsEnumerable().Where(x => x.BranchId == intBrandID || intBrandID == 0);



            var salereturnmodel = salereturn.Select(x => new SalesReturnsDetailViewModel
            {
                BranchId = x.BranchId,
                CreatedUserId = x.CreatedUserId,
                CreatedDate = x.CreatedDate,
                ProductId = x.ProductId,
                Price = x.Price,
                Quantity = x.Quantity,
                ProductName = x.ProductName,
                ProductCode = x.ProductCode,
            }).ToList();








            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(start, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(end, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);

                    details = details.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                    salereturnmodel = salereturnmodel.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                }
            }

            if (intBrandID != null && intBrandID > 0)
            {

                details = details.Where(x => x.BranchId == intBrandID).ToList();

                salereturnmodel = salereturnmodel.Where(x => x.BranchId == intBrandID).ToList();
            }

            if (!string.IsNullOrEmpty(searchproduct))
            {
                searchproduct = searchproduct.Trim();

                searchproduct = Helpers.Common.ChuyenThanhKhongDau(searchproduct);

                details = details.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.ProductCode).ToLower().Contains(searchproduct) || Helpers.Common.ChuyenThanhKhongDau(x.ProductName).ToLower().Contains(searchproduct)).ToList();


            }
            List<ProductViewModel> model = new List<ProductViewModel>();
            foreach (var group in details.GroupBy(x => x.ProductId))
            {
                var product = ProductRepository.GetvwProductById(group.Key.Value);
                if (product != null)
                {
                    model.Add(new ProductViewModel
                    {
                        Id = group.Key.Value,
                        QuantityTotalInventory = salereturnmodel.Where(y => y.ProductId == product.Id).Sum(x => x.Quantity),
                        Unit = group.FirstOrDefault().Unit,
                        PriceOutbound = product.PriceOutbound,
                        Quantity = group.Sum(x => x.Quantity),
                        Code = product.Code,
                        Name = product.Name,
                        CategoryCode = product.CategoryCode,
                        Amount = group.Sum(x => x.Amount)
                    });
                }
            }

            //begin su ly cho truong hop tra hang khong co san pham ban trong thoi gian thong ke
            if (salereturnmodel.Count > 0)
            {
                foreach (var group in salereturnmodel.GroupBy(x => x.ProductId))
                {
                    var product = model.Where(x=>x.Id== group.FirstOrDefault().ProductId);
                    if (product.Count() == 0)
                    {
                        model.Add(new ProductViewModel
                        {
                            Id = group.Key.Value,
                            QuantityTotalInventory = salereturnmodel.Where(y => y.ProductId == group.FirstOrDefault().ProductId).Sum(x => x.Quantity),
                            Unit = group.FirstOrDefault().UnitProduct,
                            PriceOutbound = group.FirstOrDefault().Price,
                            Quantity = 0,
                            Code = group.FirstOrDefault().ProductCode,
                            Name = group.FirstOrDefault().ProductName,
                            CategoryCode = group.FirstOrDefault().CategoryCode,
                            Amount = 0
                        });
                    }
                }
            }
            //end su ly cho truong hop tra hang khong co san pham ban trong thoi gian thong ke



            //phân trang trong foreach
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });


            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }


            ViewBag.size = items;
            ViewBag.currentSize = size;




            page = page ?? 1;

            int pageSize = (size ?? 10);


            int pageNumber = (page ?? 1);
            //end phân trang trong foreach

            ViewBag.tienhangtra = 0;
            ViewBag.sodonhangtra = 0;



            if (salereturnmodel.Count() > 0)
            {
                ViewBag.tienhangtra = salereturnmodel.Sum(x => (x.Price * x.Quantity));
                ViewBag.sodonhangtra = salereturnmodel.Sum(x => x.Quantity);
            }

            ViewBag.model = model;
            return PartialView(model.ToPagedList(pageNumber, pageSize));
        }



        //Xuat excel
        public ActionResult ExportRevenue(string start, string end, int? UserId, int? tab, string searchproduct)
        {


            DataTable dt = getData(start, end, UserId, tab, searchproduct);
            //Name of File  
            string fileName = "DoanhSo" + DateTime.Now + ".xlsx";
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

        }

        public DataTable getData(string start, string end, int? UserId, int? tab, string searchproduct)
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


            var productinvoice = invoiceRepository.GetAllvwProductInvoice().AsEnumerable();
            var invoicedetail = invoiceRepository.GetAllvwInvoiceDetails().AsEnumerable();
            var branch_list = BranchRepository.GetAllBranch().ToList();
            var salereturn = salesReturnsRepository.GetAllSalesReturns().AsEnumerable();
            //
            //Lấy ds don hang 
            var modeldetail = invoicedetail.Select(x => new ProductInvoiceDetailViewModel
            {
                Id = x.Id,
                Price = x.Price,
                ProductId = x.ProductId,
                PromotionDetailId = x.PromotionDetailId,
                PromotionId = x.PromotionId,
                PromotionValue = x.PromotionValue,
                Quantity = x.Quantity,
                Unit = x.Unit,
                ProductType = x.ProductType,
                FixedDiscount = x.FixedDiscount,
                FixedDiscountAmount = x.FixedDiscountAmount,
                IrregularDiscountAmount = x.IrregularDiscountAmount,
                IrregularDiscount = x.IrregularDiscount,
                CategoryCode = x.CategoryCode,
                ProductInvoiceCode = x.ProductInvoiceCode,
                ProductName = x.ProductName,
                ProductCode = x.ProductCode,
                ProductInvoiceId = x.ProductInvoiceId,
                ProductGroup = x.ProductGroup,
                CheckPromotion = x.CheckPromotion,
                IsReturn = x.IsReturn,
                //Status = x.Status,
                Amount = x.Amount,
                LoCode = x.LoCode,
                ExpiryDate = x.ExpiryDate,
                Origin = x.Origin,
                TaxFeeAmount = x.TaxFeeAmount,
                TaxFee = x.TaxFee,
                Amount2 = x.Amount2,
                BranchId = x.BranchId,
                QuantitySaleReturn = x.QuantitySaleReturn,
                CreatedDate = x.CreatedDate,
                CreatedUserId = x.CreatedUserId

            }).ToList();


            var model = productinvoice.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                UserName = item.UserName,
                CreatedDate = item.CreatedDate,
                //CreatedDateTemp = item.CreatedDateTemp,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Code = item.Code,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                TotalAmount = item.TotalAmount,
                //FixedDiscount = item.FixedDiscount,
                TaxFee = item.TaxFee,
                CodeInvoiceRed = item.CodeInvoiceRed,
                Status = item.Status,
                IsArchive = item.IsArchive,
                ProductOutboundId = item.ProductOutboundId,
                ProductOutboundCode = item.ProductOutboundCode,
                Note = item.Note,
                CancelReason = item.CancelReason,
                BranchId = item.BranchId,
                CustomerId = item.CustomerId,
                BranchName = item.BranchName,
                CompanyName = item.CompanyName,
                Email = item.Email,
                CustomerPhone = item.CustomerPhone,
                Address = item.Address,
                DiscountTabBillAmount = item.DiscountTabBillAmount,
                DiscountTabBill = item.DiscountTabBill,
                isDisCountAllTab = item.isDisCountAllTab,
                DisCountAllTab = item.DisCountAllTab,
                Birthday = item.Birthday,
                Gender = item.Gender,
                Mobile = item.Mobile,
                TienTra = item.TienTra,
                SalerFullName = item.SalerFullName,
                MoneyPay = item.MoneyPay,
                TransferPay = item.TransferPay,
                ATMPay = item.ATMPay

            }).OrderByDescending(m => m.Id).ToList();

            //Lay nguoi ban
            var user = userRepository.GetAllvwUsers();
            var usermodel = user.Select(item => new UserViewModel
            {
                Id = item.Id,
                FullName = item.LastName + " " + item.FirstName,
                BranchId = item.BranchId
            }).ToList();


            //Lay cua hang
            var branch = branch_list.Select(item => new BranchViewModel
            {
                Id = item.Id,
                Name = item.Name

            }).ToList();
            //Lấy hàng trả 
            var salereturnmodel = salereturn.Select(x => new SalesReturnsViewModel
            {
                Id = x.Id,
                BranchId = x.BranchId,
                CreatedUserId = x.CreatedUserId,
                CreatedDate = x.CreatedDate

            }).ToList();

            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(start, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(end, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    model = model.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                    modeldetail = modeldetail.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                    salereturnmodel = salereturnmodel.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                }
            }

            if (intBrandID != null && intBrandID > 0)
            {
                model = model.Where(x => x.BranchId == intBrandID).ToList();
                branch = branch.Where(x => x.Id == intBrandID).ToList();
                modeldetail = modeldetail.Where(x => x.BranchId == intBrandID).ToList();
                usermodel = usermodel.Where(x => x.BranchId == intBrandID).ToList();
                salereturnmodel = salereturnmodel.Where(x => x.BranchId == intBrandID).ToList();
            }
            if (UserId != null)
            {
                model = model.Where(x => x.CreatedUserId == UserId).ToList();
                modeldetail = modeldetail.Where(x => x.CreatedUserId == UserId).ToList();
                salereturnmodel = salereturnmodel.Where(x => x.CreatedUserId == UserId).ToList();
            }

            //Creating DataTable  
            DataTable dt = new DataTable();
            //Setiing Table Name  

            if (tab == 1) //theo thoi gian
            {
                dt.TableName = "Doanh số theo đơn hàng";
                //Add Columns  
                dt.Columns.Add("Đơn hàng", typeof(string));
                dt.Columns.Add("Ngày bán", typeof(string));
                dt.Columns.Add("Thu ngân", typeof(string));
                dt.Columns.Add("Khách hàng", typeof(string));
                dt.Columns.Add("Số lượng", typeof(string));
                dt.Columns.Add("Thành tiền", typeof(string));
                dt.Columns.Add("Giảm giá", typeof(string));
                dt.Columns.Add("Tổng tiền", typeof(string));
                dt.Columns.Add("Tiền mặt", typeof(string));
                dt.Columns.Add("Chuyển khoản", typeof(string));
                dt.Columns.Add("Thẻ", typeof(string));
                //dt.Columns.Add("Trạng thái", typeof(string));
                dt.Columns.Add("Ghi chú", typeof(string));
                dt.Columns.Add("Tên hàng", typeof(string));
                dt.Columns.Add("Mã hàng", typeof(string));
                dt.Columns.Add("SL", typeof(string));
                dt.Columns.Add("Giá bán", typeof(string));
                dt.Columns.Add("Giam giá", typeof(string));
                dt.Columns.Add("Thành tiên", typeof(string));
                //Add Rows in DataTable  
                //dt.Rows.Add(1, "A12-000011266", 15, 300000);
                //dt.Rows.Add(2, "A13-000014533", 20, 200000);
                //dt.Rows.Add(3, "A13-000014535", 20, 400000);
                foreach (var item in model)
                {
                    var sol = modeldetail.Where(x => x.ProductInvoiceId == item.Id).Sum(x => x.Quantity);
                    var thanhtien = modeldetail.Where(x => x.ProductInvoiceId == item.Id).Sum(x => x.Quantity * x.Price);
                    dt.Rows.Add(item.Code, item.CreatedDate.ToString(), item.SalerFullName, item.CustomerName, CommonSatic.ToCurrencyStr(sol, null), CommonSatic.ToCurrencyStr(thanhtien, null),
                        CommonSatic.ToCurrencyStr(thanhtien - item.TotalAmount, null), CommonSatic.ToCurrencyStr(item.TotalAmount, null),
                        CommonSatic.ToCurrencyStr(item.MoneyPay, null), CommonSatic.ToCurrencyStr(item.ATMPay, null), CommonSatic.ToCurrencyStr(item.TransferPay, null), item.Note);
                    var detail = modeldetail.Where(x => x.ProductInvoiceId == item.Id).ToList();
                    foreach (var i in detail)
                    {
                        if (i.IrregularDiscountAmount != null)
                        {
                            dt.Rows.Add("", "", "", "", "", "", "", "",
                                    "", "", "", "", i.ProductName, i.ProductCode, CommonSatic.ToCurrencyStr(i.Quantity, null), CommonSatic.ToCurrencyStr(i.Price, null),
                                         CommonSatic.ToCurrencyStr(i.IrregularDiscountAmount, null)
                                        , CommonSatic.ToCurrencyStr(i.Amount, null));
                        }
                        else
                        {
                            dt.Rows.Add("", "", "", "", "", "", "", "",
                                "", "", "", "", i.ProductName, i.ProductCode, CommonSatic.ToCurrencyStr(i.Quantity, null), CommonSatic.ToCurrencyStr(i.Price, null),
                                     "0"
                                    , CommonSatic.ToCurrencyStr(i.Amount, null));
                        }
                    }


                }
                dt.AcceptChanges();
            }

            if (tab == 2) //theo nguoi ban
            {
                dt.TableName = "Doanh số theo nhân viên";
                //Add Columns  
                dt.Columns.Add("Người bán", typeof(string));
                dt.Columns.Add("Tiền bán hàng", typeof(string));
                dt.Columns.Add("Số đơn hàng", typeof(string));
                dt.Columns.Add("Hàng hóa bán", typeof(string));
                dt.Columns.Add("Tiền trả hàng", typeof(string));
                dt.Columns.Add("Đơn hàng trả", typeof(string));
                dt.Columns.Add("Hàng hóa trả", typeof(string));
                dt.Columns.Add("Mã đơn hàng", typeof(string));
                dt.Columns.Add("Ngày", typeof(string));
                dt.Columns.Add("Khách hàng", typeof(string));
                dt.Columns.Add("SL hàng", typeof(string));
                dt.Columns.Add("Tổng tiền", typeof(string));
                //Add Rows in DataTable  

                foreach (var item in usermodel)
                {
                    var salert = salereturn.Where(x => x.CreatedUserId == item.Id).ToList();
                    var list = model.Where(x => x.CreatedUserId == item.Id).ToList();
                    var detail = modeldetail.Where(x => x.CreatedUserId == item.Id).ToList();
                    dt.Rows.Add(item.FullName, CommonSatic.ToCurrencyStr(list.Sum(x => x.TotalAmount), null), list.Count().ToString(), detail.Sum(x => x.Quantity).ToString(),
                        CommonSatic.ToCurrencyStr(list.Sum(x => x.TienTra), null)
                        , salert.Count().ToString(), detail.Sum(x => x.Quantity - x.QuantitySaleReturn).ToString());
                    foreach (var i in list)
                    {
                        var sl = modeldetail.Where(x => x.ProductInvoiceId == i.Id).Sum(x => x.Quantity);
                        dt.Rows.Add("", "", "", "", ""
                       , "", "", i.Code, i.CreatedDate, i.CustomerName, sl.ToString(), CommonSatic.ToCurrencyStr(i.TotalAmount, null));
                    }
                }
                dt.AcceptChanges();
            }

            if (tab == 3) //theo cua hangs
            {
                dt.TableName = "Doanh số theo cửa hàng";
                //Add Columns  
                dt.Columns.Add("Cửa hàng", typeof(string));
                dt.Columns.Add("Tiền bán hàng", typeof(string));
                dt.Columns.Add("Số đơn hàng", typeof(string));
                dt.Columns.Add("Hàng hóa bán", typeof(string));
                dt.Columns.Add("Tiền trả hàng", typeof(string));
                dt.Columns.Add("Đơn hàng trả", typeof(string));
                dt.Columns.Add("Hàng hóa trả", typeof(string));
                dt.Columns.Add("Mã đơn hàng", typeof(string));
                dt.Columns.Add("Ngày", typeof(string));
                dt.Columns.Add("Khách hàng", typeof(string));
                dt.Columns.Add("SL hàng", typeof(string));
                dt.Columns.Add("Tổng tiền", typeof(string));
                //Add Rows in DataTable  
                foreach (var item in branch)
                {
                    var salert = salereturn.Where(x => x.BranchId == item.Id).ToList();
                    var list = model.Where(x => x.BranchId == item.Id).ToList();
                    var detail = modeldetail.Where(x => x.BranchId == item.Id).ToList();
                    dt.Rows.Add(item.Name, CommonSatic.ToCurrencyStr(list.Sum(x => x.TotalAmount), null), list.Count().ToString(), detail.Sum(x => x.Quantity).ToString(),
                        CommonSatic.ToCurrencyStr(list.Sum(x => x.TienTra), null)
                        , salert.Count().ToString(), detail.Sum(x => x.Quantity - x.QuantitySaleReturn).ToString());
                    foreach (var i in list)
                    {
                        var sl = modeldetail.Where(x => x.ProductInvoiceId == i.Id).Sum(x => x.Quantity);
                        dt.Rows.Add("", "", "", "", ""
                       , "", "", i.Code, i.CreatedDate, i.CustomerName, sl.ToString(), CommonSatic.ToCurrencyStr(i.TotalAmount, null));
                    }
                }
                dt.AcceptChanges();
            }
            if (tab == 4)
            {
                List<ProductViewModel> productlist = new List<ProductViewModel>();
                if (!string.IsNullOrEmpty(searchproduct))
                {
                    searchproduct = searchproduct.Trim();

                    searchproduct = Helpers.Common.ChuyenThanhKhongDau(searchproduct);

                    modeldetail = modeldetail.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.ProductCode).ToLower().Contains(searchproduct)
                    || Helpers.Common.ChuyenThanhKhongDau(x.ProductName).ToLower().Contains(searchproduct)).ToList();


                }
                foreach (var group in modeldetail.GroupBy(x => x.ProductId))
                {
                    var product = ProductRepository.GetvwProductById(group.Key.Value);
                    if (product != null)
                    {
                        productlist.Add(new ProductViewModel
                        {
                            Id = group.Key.Value,
                            QuantityTotalInventory = group.Sum(x => x.QuantitySaleReturn),
                            Unit = group.FirstOrDefault().Unit,
                            PriceOutbound = product.PriceOutbound,
                            Quantity = group.Sum(x => x.Quantity),
                            Code = product.Code,
                            Name = product.Name,
                            CategoryCode = product.CategoryCode,
                            Amount = group.Sum(x => x.Amount)
                        });
                    }
                }
                dt.TableName = "Doanh số theo hàng hóa";
                //Add Columns  
                dt.Columns.Add("Mã sản phẩm", typeof(string));
                dt.Columns.Add("Tên sản phẩm", typeof(string));
                dt.Columns.Add("SL bán", typeof(int));
                dt.Columns.Add("Tiền bán hàng", typeof(string));
                dt.Columns.Add("SL trả", typeof(int));
                dt.Columns.Add("Tiền trả hàng", typeof(string));
                foreach (var item in productlist)
                {
                    dt.Rows.Add(item.Code, item.Name, item.Quantity, CommonSatic.ToCurrencyStr(item.Amount, null), item.Quantity - item.QuantityTotalInventory, CommonSatic.ToCurrencyStr((item.Quantity - item.QuantityTotalInventory) * item.PriceOutbound, null));
                }

            }
            return dt;
        }


        #endregion


        #region Báo cáo theo khách hàng 
        public ViewResult CustomerReport(string start, string end, string Paymethod, string TenMaDT, int? size, int? page)
        {

            if (start == null && end == null)
            {
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // Cộng thêm 1 tháng và trừ đi một ngày.
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                start = aDateTime.ToString("dd/MM/yyyy");
                end = retDateTime.ToString("dd/MM/yyyy");
            }

            //Cửa hàng
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
            if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
            {
                strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
            }

            int? intBrandID = int.Parse(strBrandID);


            var productinvoice = invoiceRepository.GetAllvwProductInvoice().Select(item => new ProductInvoiceViewModel
            {

                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                UserName = item.UserName,
                CreatedDate = item.CreatedDate,
                Code = item.Code,
                TotalAmount = item.TotalAmount,
                TaxFee = item.TaxFee,
                Status = item.Status,
                IsArchive = item.IsArchive,
                BranchId = item.BranchId,
                CustomerId = item.CustomerId,
                Email = item.Email,
                CustomerPhone = item.CustomerPhone,
                DiscountTabBillAmount = item.DiscountTabBillAmount,
                DiscountTabBill = item.DiscountTabBill,
                isDisCountAllTab = item.isDisCountAllTab,
                DisCountAllTab = item.DisCountAllTab,
                TienTra = item.TienTra,
                MoneyPay = item.MoneyPay,
                TransferPay = item.TransferPay,
                ATMPay = item.ATMPay
            }).OrderByDescending(m => m.CreatedDate).ToList();

            //Loc theo hinh thuc thanh toán
            if (!string.IsNullOrEmpty(Paymethod))
            {
                if (Paymethod == "Tiền mặt")
                {
                    foreach (var item in productinvoice)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.MoneyPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
                if (Paymethod == "Chuyển khoản")
                {
                    foreach (var item in productinvoice)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.TransferPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
                if (Paymethod == "Quẹt thẻ")
                {
                    foreach (var item in productinvoice)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.ATMPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
            }


            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(start, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(end, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    productinvoice = productinvoice.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();

                }
            }
            //loc theo cua hang
            if (intBrandID != null && intBrandID > 0)
            {
                productinvoice = productinvoice.Where(x => x.BranchId == intBrandID).ToList();
            }

            //Lay khach hang
            var customer = customerRepository.GetAllvwCustomer().Select(item => new CustomerViewModel
            {
                Id = item.Id,
                Code = item.Code,
                Address = item.Address,
                SearchFullName = item.SearchFullName,
                Birthday = item.Birthday,
                Gender = item.Gender,
                FirstName = item.FirstName,
                LastName = item.LastName,
                FullName = item.FullName,
                ProvinceName = item.ProvinceName,
                DistrictName = item.DistrictName,
                WardName = item.WardName,
                DistrictId = item.DistrictId,
                CityId = item.CityId,
                WardId = item.WardId,
                Mobile = item.Mobile,
                Email = item.Email,
            }).AsEnumerable().OrderByDescending(m => m.Id).ToList();

            //Lay khách hàng có giao dịch trong khoản thời gian tìm kiếm
            var customerlist = new List<CustomerViewModel>();
            foreach (var item in customer)
            {
                var pro = productinvoice.Where(x => x.CustomerId == item.Id).Count();
                if (pro > 0)
                {
                    customerlist.Add(item);
                }
            }
            customer = customerlist;

            //loc theo mã, tên, sdt
            if (!string.IsNullOrEmpty(TenMaDT))
            {
                TenMaDT = Helpers.Common.ChuyenThanhKhongDau(TenMaDT).ToLower();
                customer = customer.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.FullName).ToLower().Contains(TenMaDT)
                || Helpers.Common.ChuyenThanhKhongDau(x.Code).ToLower().Contains(TenMaDT) ||
                Helpers.Common.ChuyenThanhKhongDau(x.Mobile).Contains(TenMaDT)

                ).ToList();
            }

            //phân trang trong foreach
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });


            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }


            ViewBag.size = items;
            ViewBag.currentSize = size;

            page = page ?? 1;
            //if(grid_page != null)
            //{
            //    page = grid_page;
            //}
            int pageSize = (size ?? 10);


            int pageNumber = (page ?? 1);
            //end phân trang trong foreach
            ViewBag.page = page;
            ViewBag.ProductInvoice = productinvoice;
            return View(customer.ToPagedList(pageNumber, pageSize));
        }


        //Xuat excel
        public ActionResult ExportCusRevenue(string start, string end, string Paymethod, string TenMaDT)
        {
            DataTable dt = getDataFromCus(start, end, Paymethod, TenMaDT);
            //Name of File  
            string fileName = "DoanhSoKH" + DateTime.Now + ".xlsx";
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
            // return View();
        }

        public DataTable getDataFromCus(string start, string end, string Paymethod, string TenMaDT)
        {
            if (start == null && end == null)
            {
                DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                // Cộng thêm 1 tháng và trừ đi một ngày.
                DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1);
                start = aDateTime.ToString("dd/MM/yyyy");
                end = retDateTime.ToString("dd/MM/yyyy");
            }

            //Cửa hàng
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
            if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
            {
                strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
            }

            int? intBrandID = int.Parse(strBrandID);


            var productinvoice = invoiceRepository.GetAllvwProductInvoice().Select(item => new ProductInvoiceViewModel
            {

                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                UserName = item.UserName,
                CreatedDate = item.CreatedDate,
                Code = item.Code,
                TotalAmount = item.TotalAmount,
                TaxFee = item.TaxFee,
                Status = item.Status,
                IsArchive = item.IsArchive,
                BranchId = item.BranchId,
                CustomerId = item.CustomerId,
                Email = item.Email,
                CustomerPhone = item.CustomerPhone,
                DiscountTabBillAmount = item.DiscountTabBillAmount,
                DiscountTabBill = item.DiscountTabBill,
                isDisCountAllTab = item.isDisCountAllTab,
                DisCountAllTab = item.DisCountAllTab,
                TienTra = item.TienTra,
                MoneyPay = item.MoneyPay,
                TransferPay = item.TransferPay,
                ATMPay = item.ATMPay
            }).OrderByDescending(m => m.CreatedDate).ToList();

            //Loc theo hinh thuc thanh toán
            if (!string.IsNullOrEmpty(Paymethod))
            {
                if (Paymethod == "Tiền mặt")
                {
                    foreach (var item in productinvoice)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.MoneyPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
                if (Paymethod == "Chuyển khoản")
                {
                    foreach (var item in productinvoice)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.TransferPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
                if (Paymethod == "Quẹt thẻ")
                {
                    foreach (var item in productinvoice)
                    {
                        if (item.MoneyPay != null)
                        {
                            item.TotalAmount = item.ATMPay.Value;
                        }
                        else
                        {
                            item.TotalAmount = 0;
                        }
                    }
                }
            }


            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(start, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(end, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    productinvoice = productinvoice.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();

                }
            }
            //loc theo cua hang
            if (intBrandID != null && intBrandID > 0)
            {
                productinvoice = productinvoice.Where(x => x.BranchId == intBrandID).ToList();
            }

            //Lay khach hang
            var customer = customerRepository.GetAllvwCustomer().Select(item => new CustomerViewModel
            {
                Id = item.Id,
                Code = item.Code,
                Address = item.Address,
                SearchFullName = item.SearchFullName,
                Birthday = item.Birthday,
                Gender = item.Gender,
                FirstName = item.FirstName,
                LastName = item.LastName,
                FullName = item.FullName,
                ProvinceName = item.ProvinceName,
                DistrictName = item.DistrictName,
                WardName = item.WardName,
                DistrictId = item.DistrictId,
                CityId = item.CityId,
                WardId = item.WardId,
                Mobile = item.Mobile,
                Email = item.Email,
            }).AsEnumerable().OrderByDescending(m => m.Id).ToList();

            //Lay khách hàng có giao dịch trong khoản thời gian tìm kiếm
            var customerlist = new List<CustomerViewModel>();
            foreach (var item in customer)
            {
                var pro = productinvoice.Where(x => x.CustomerId == item.Id).Count();
                if (pro > 0)
                {
                    customerlist.Add(item);
                }
            }
            customer = customerlist;

            //loc theo mã, tên, sdt
            if (!string.IsNullOrEmpty(TenMaDT))
            {
                TenMaDT = Helpers.Common.ChuyenThanhKhongDau(TenMaDT).ToLower();
                customer = customer.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.FullName).ToLower().Contains(TenMaDT)
                || Helpers.Common.ChuyenThanhKhongDau(x.Code).ToLower().Contains(TenMaDT) ||
                Helpers.Common.ChuyenThanhKhongDau(x.Mobile).Contains(TenMaDT)

                ).ToList();
            }

            //Creating DataTable  
            DataTable dt = new DataTable();
            dt.TableName = "Doanh số theo khách hàng";
            //Add Columns  
            dt.Columns.Add("Mã khách hàng", typeof(string));
            dt.Columns.Add("Tên khách hàng", typeof(string));
            dt.Columns.Add("SĐT", typeof(string));
            dt.Columns.Add("Ngày mua gần nhất", typeof(string));
            dt.Columns.Add("Số đơn hàng", typeof(int));
            dt.Columns.Add("Tổng tiền", typeof(string));
            dt.Columns.Add("Hóa đơn", typeof(string));
            dt.Columns.Add("Ngày", typeof(string));
            dt.Columns.Add("Tổng tiền đơn hàng", typeof(string));
            //Add Rows in DataTable  
            foreach (var item in customer)
            {
                var listinvoice = productinvoice.Where(x => x.CustomerId == item.Id).ToList();
                var tien = listinvoice.Sum(x => x.TotalAmount);
                dt.Rows.Add(item.Code, item.FullName, item.Mobile, listinvoice[0].CreatedDate.Value.ToString("dd/MM/yyyy"), listinvoice.Count(), Helpers.CommonSatic.ToCurrencyStr(tien, null).Replace(".", ","));
                foreach (var i in listinvoice)
                {
                    dt.Rows.Add("", "", "", "", Convert.DBNull, Convert.DBNull, i.Code, i.CreatedDate.Value.ToString("dd/MM/yyyy"), Helpers.CommonSatic.ToCurrencyStr(i.TotalAmount, null).Replace(".", ","));
                }

            }
            return dt;
        }
        #endregion
        //<append_content_action_here>
    }
}
