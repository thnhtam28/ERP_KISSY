using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Repositories;
using Erp.Domain.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Interfaces;
using Erp.BackOffice.Account.Controllers;
using Erp.Domain.Account.Entities;
using System.Xml.Linq;
using Erp.BackOffice.Areas.Cms.Models;
using Erp.Domain.Entities;
using Newtonsoft.Json;
using GenCode128;
using System.Drawing;
using System.IO;
using Erp.Domain.Crm.Interfaces;
using Erp.Domain.Staff.Entities;
using System.Web;
using Erp.Domain.Repositories;
using Erp.Domain;
using Erp.Domain.Sale;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.Transactions;
using Excels = Microsoft.Office.Interop.Excel;
using ImportDonHangModel = Erp.BackOffice.Sale.Models.ImportDonHangModel;
using Excel;
using System.Data;
using System.Windows.Forms;
using OfficeOpenXml;
using PagedList;
using Erp.BackOffice.Helpers;
using Erp.BackOffice.Areas.Sale.Models;
//13133
namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ProductInvoiceController : Controller
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IReceiptRepository ReceiptRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly ICommisionRepository commisionRepository;
        private readonly IProductOrServiceRepository ProductRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly IProductOutboundRepository productOutboundRepository;
        private readonly IStaffsRepository staffRepository;
        private readonly IUserRepository userRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IWarehouseLocationItemRepository warehouseLocationItemRepository;
        private readonly ICommisionStaffRepository commisionStaffRepository;
        private readonly IServiceComboRepository servicecomboRepository;
        private readonly IUsingServiceLogRepository usingServiceLogRepository;
        private readonly ILogServiceRemminderRepository logServiceReminderRepository;
        private readonly IServiceReminderGroupRepository ServiceReminderGroupRepository;
        private readonly ITransactionLiabilitiesRepository transactionLiabilitiesRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IUsingServiceLogDetailRepository usingServiceLogDetailRepository;
        private readonly ICommisionCustomerRepository commisionCustomerRepository;
        private readonly IServiceScheduleRepository serviceScheduleRepository;
        private readonly ITaskRepository taskRepository;
        private readonly IWorkSchedulesRepository WorkSchedulesRepository;
        private readonly IRegisterForOvertimeRepository RegisterForOvertimeRepository;
        private readonly IReceiptDetailRepository receiptDetailRepository;
        private readonly IYHL_KIENHANG_GUI_CTIETRepositories KIENHANG_GUI_CTIETRepository;
        private readonly IYHL_KIENHANG_GUIRepositories KIENHANG_GUIRepository;
        private readonly IYHL_KIENHANG_TRA_CTIETRepositories KIENHANG_TRA_CTIETRepository;
        private readonly IYHL_KIENHANG_TRARepositories KIENHANG_TRARepository;
        private readonly ICommissionCusRepository commissionCusRepository;
        private readonly ILogVipRepository logVipRepository;
        private readonly ICommisionApplyRepository commisionApplyRepository;
        private readonly IDM_NHOMSANPHAMRepositories dM_NHOMSANPHAMRepositories;
        private readonly INoteProductInvoiceRepository NoteProductInvoiceRepository;
        private readonly ISettingRepository _settingRepository;
         private readonly ISalesReturnsRepository salesReturnsRepository;
        public ProductInvoiceController(
            ITransactionRepository _transaction
            , IReceiptRepository _Receipt
            , IProductInvoiceRepository _ProductInvoice
            , ICommisionRepository _Commision
            , IProductOrServiceRepository _Product
            , ICustomerRepository _Customer
            , IInventoryRepository _Inventory
            , IProductOutboundRepository _ProductOutbound
            , IStaffsRepository _staff
            , IUserRepository _user
            , ITemplatePrintRepository _templatePrint
            , ICategoryRepository category
            , IWarehouseLocationItemRepository _WarehouseLocationItem
            , ICommisionStaffRepository _commisionStaff
            , IServiceComboRepository servicecombo
            , IUsingServiceLogRepository usingServiceLog
            , ILogServiceRemminderRepository logServiceReminder
            , IServiceReminderGroupRepository serviceReminderGroup
            , ITransactionLiabilitiesRepository _transactionLiabilities
            , IWarehouseRepository _Warehouse
            , IUsingServiceLogDetailRepository usingServiceLogDetail
            , ICommisionCustomerRepository _Commision_Customer
            , IServiceScheduleRepository schedule
            , ITaskRepository task
            , IWorkSchedulesRepository _WorkSchedules
            , IRegisterForOvertimeRepository _RegisterForOvertime
            , IReceiptDetailRepository receiptDetail
            , IDM_NHOMSANPHAMRepositories _DM_NHOMSANPHAMRepositories
            , IYHL_KIENHANG_GUI_CTIETRepositories kienhang_gui_ctiet
            , IYHL_KIENHANG_GUIRepositories kienhang_gui
            , IYHL_KIENHANG_TRA_CTIETRepositories kienhang_tra_ctiet
            , IYHL_KIENHANG_TRARepositories kienhang_tra
             , ICommissionCusRepository _Commision_Cus
              , ILogVipRepository _LogVip
             , ICommisionApplyRepository _CommisionApply
            , INoteProductInvoiceRepository NoteProductInvoice
            , ISettingRepository settingRepository
            ,ISalesReturnsRepository salesReturns
            )
        {
            transactionRepository = _transaction;
            ReceiptRepository = _Receipt;
            productInvoiceRepository = _ProductInvoice;
            commisionRepository = _Commision;
            ProductRepository = _Product;
            inventoryRepository = _Inventory;
            productOutboundRepository = _ProductOutbound;
            customerRepository = _Customer;
            staffRepository = _staff;
            userRepository = _user;
            templatePrintRepository = _templatePrint;
            categoryRepository = category;
            warehouseLocationItemRepository = _WarehouseLocationItem;
            commisionStaffRepository = _commisionStaff;
            usingServiceLogRepository = usingServiceLog;
            servicecomboRepository = servicecombo;
            logServiceReminderRepository = logServiceReminder;
            ServiceReminderGroupRepository = serviceReminderGroup;
            transactionLiabilitiesRepository = _transactionLiabilities;
            warehouseRepository = _Warehouse;
            usingServiceLogDetailRepository = usingServiceLogDetail;
            commisionCustomerRepository = _Commision_Customer;
            serviceScheduleRepository = schedule;
            taskRepository = task;
            WorkSchedulesRepository = _WorkSchedules;
            RegisterForOvertimeRepository = _RegisterForOvertime;
            receiptDetailRepository = receiptDetail;
            KIENHANG_GUI_CTIETRepository = kienhang_gui_ctiet;
            KIENHANG_GUIRepository = kienhang_gui;
            KIENHANG_TRA_CTIETRepository = kienhang_tra_ctiet;
            KIENHANG_TRARepository = kienhang_tra;
            commissionCusRepository = _Commision_Cus;
            logVipRepository = _LogVip;
            commisionApplyRepository = _CommisionApply;
            dM_NHOMSANPHAMRepositories = _DM_NHOMSANPHAMRepositories;
            NoteProductInvoiceRepository = NoteProductInvoice;
            _settingRepository = settingRepository;
            salesReturnsRepository = salesReturns;
        }



        #region Index

        public ViewResult Index(string txtCode, string txtMinAmount, string txtMaxAmount, string txtCusName, string startDate, string endDate, int? BranchId, string Status, int? AmountPage, int? UserId)
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




            //get  CurrentUser.branchId

            if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
            {
                strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
            }

            int? intBrandID = int.Parse(strBrandID);
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            var q = productInvoiceRepository.GetAllvwProductInvoiceFulls().AsEnumerable().Where(x => x.BranchId == intBrandID || intBrandID == 0);
            //var q = productInvoiceRepository.GetAllvwProductInvoiceFulls().ToList();
            //.Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId);
            var Alluser = userRepository.GetAllvwUsers().Where(n => n.BranchId == intBrandID).ToList();
            var salereturn = salesReturnsRepository.GetAllSalesReturns().AsEnumerable().Where(x => x.BranchId == intBrandID || intBrandID == 0);
            ViewBag.Listuser = Alluser;
            var model = q.Select(item => new ProductInvoiceViewModel
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
                TienTra = item.TienTra

            }).OrderByDescending(m => m.Id).ToList();

            ViewBag.TongSL = 0;
            foreach (var item in model)
            {
                var a = DateTime.Now;
                DateTime? temp = item.CreatedDate;


                if (temp.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) == a.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    var b = temp.Value.ToShortTimeString();
                    item.CreatedDateTemp = b;

                }
                else
                {
                    var c = temp.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    item.CreatedDateTemp = c;
                }



                var DetailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(item.Id).Select(x =>
             new ProductInvoiceDetailViewModel
             {
                 Id = x.Id,
                 Amount2 = x.Amount2
             }).OrderBy(x => x.Id).ToList();






            }
            if (!string.IsNullOrEmpty(txtCode))
            {
                model = model.Where(x => x.Code == txtCode).ToList();
            }

            //if (!string.IsNullOrEmpty(txtCusName))
            //{
            //    txtCusName = Helpers.Common.ChuyenThanhKhongDau(txtCusName);
            //    model = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.CustomerName).Contains(txtCusName)).ToList();
            //}
            if (!string.IsNullOrEmpty(txtCusName))
            {
                txtCusName = txtCusName.Trim();

                txtCusName = Helpers.Common.ChuyenThanhKhongDau(txtCusName);

                model = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.CustomerName).ToLower().Contains(txtCusName)).ToList();


            }
            //if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin())
            //{
            //    model = model.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true).ToList();
            //}
            if (UserId > 0)
            {
                model = model.Where(x => x.CreatedUserId == UserId).ToList();
            }
            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "delete")
                {
                    model = model.Where(x => x.IsDeleted == true).ToList();
                }
                if (Status == "complete")
                {
                    model = model.Where(x => x.Status == "complete").ToList();
                }

                if (Status == "inprogress")
                {
                    model = model.Where(x => x.Status == "inprogress").ToList();
                }
                if (Status == "pending")
                {
                    model = model.Where(x => x.Status == "pending").ToList();
                }

                if (Status == "inprogress")
                {
                    model = model.Where(x => x.Status == "inprogress").ToList();
                }
                if (Status == "dc")
                {
                    var productListId = ProductRepository.GetAllvwProductAndService()
                        .Where(item => item.Code == "ĐC").Select(item => item.Id).ToList();

                    if (productListId.Count > 0)
                    {
                        List<int> listProductInboundId = new List<int>();
                        foreach (var id in productListId)
                        {
                            var list = productInvoiceRepository.GetAllvwInvoiceDetails().Where(x => x.ProductCode == "ĐC")
                                .Select(item => item.ProductInvoiceId.Value).Distinct().ToList();

                            listProductInboundId.AddRange(list);
                        }

                        model = model.Where(item => listProductInboundId.Contains(item.Id)).ToList();
                    }
                }
            }


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
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    model = model.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                    salereturnmodel = salereturnmodel.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                }
            }

            decimal minAmount;
            if (decimal.TryParse(txtMinAmount, out minAmount))
            {
                model = model.Where(x => x.TotalAmount >= minAmount).ToList();
            }

            decimal maxAmount;
            if (decimal.TryParse(txtMaxAmount, out maxAmount))
            {
                if (maxAmount > 0)
                {
                    model = model.Where(x => x.TotalAmount <= maxAmount).ToList();
                }
            }
            if (BranchId != null && BranchId.Value > 0)
            {
                model = model.Where(x => x.BranchId == BranchId).ToList();
                salereturnmodel = salereturnmodel.Where(x => x.BranchId == intBrandID).ToList();
            }
            foreach (var item in model)
            {
                ViewBag.TongSL++;
            }

            ViewBag.Tongdong = 0;
            ViewBag.tongtien = 0;
            ViewBag.tientra = 0;
            ViewBag.tongthu = 0;
            if (model.Count > 0)
            {
                ViewBag.Tongdong = model.Count;
                ViewBag.tongtien = model.Sum(x => x.TotalAmount);
                ViewBag.tongthu = model.Sum(x => x.TotalAmount);
            }

            if (salereturnmodel.Count > 0)
            {

                ViewBag.tientra = salereturnmodel.Sum(x => x.TotalAmount);
                ViewBag.tongthu = model.Sum(x => x.TotalAmount) - salereturnmodel.Sum(x => x.TotalAmount);
            }


            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(model);
        }
        #endregion

        [HttpPost]
        public JsonResult ExportExcelInvoice(string txtCode, string txtMinAmount, string txtMaxAmount, string txtCusName, string startDate, string endDate, int? BranchId, string Status, int? UserId, int TongSL)
        {

            //get cookie brachID 
            HttpRequestBase request = this.HttpContext.Request;
            string strBrandID = "0";
            //string pAmountPage = "45";
            if (request.Cookies["BRANCH_ID_CookieName"] != null)
            {
                strBrandID = request.Cookies["BRANCH_ID_CookieName"].Value;
                if (strBrandID == "")
                {
                    strBrandID = "0";
                }
            }


            //begin get cookie numberow
            //if (request.Cookies["NUMBERROW_INVOICE_CookieName"] != null)
            //{
            //    pAmountPage = request.Cookies["NUMBERROW_INVOICE_CookieName"].Value;
            //    if (pAmountPage == "")
            //    {
            //        pAmountPage = "45";
            //    }
            //}



            if (TongSL != null || TongSL != 0)
            {
                ViewBag.AmountPage = TongSL;

            }
            else

            {
                ViewBag.pAmountPage = 45;
            }

            //end get cookie numberow




            //get  CurrentUser.branchId

            if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
            {
                strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
            }

            int? intBrandID = int.Parse(strBrandID);
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            var q = productInvoiceRepository.GetAllvwProductInvoiceFulls().Where(x => x.BranchId == intBrandID || intBrandID == 0);
            //var q = productInvoiceRepository.GetAllvwProductInvoiceFulls().ToList();
            //.Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId);
            var Alluser = userRepository.GetAllvwUsers().Where(n => n.BranchId == intBrandID).ToList();
            ViewBag.Listuser = Alluser;
            var model = q.Select(item => new ProductInvoiceViewModel
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
                TienTra = item.TienTra

            }).OrderByDescending(m => m.Id).ToList();

            ViewBag.TongSL = 0;
            foreach (var item in model)
            {
                var a = DateTime.Now;
                DateTime? temp = item.CreatedDate;


                if (temp.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) == a.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    var b = temp.Value.ToShortTimeString();
                    item.CreatedDateTemp = b;

                }
                else
                {
                    var c = temp.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    item.CreatedDateTemp = c;
                }

                item.TotalAmount = 0;

                var DetailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(item.Id).Select(x =>
             new ProductInvoiceDetailViewModel
             {
                 Id = x.Id,
                 Amount2 = x.Amount2
             }).OrderBy(x => x.Id).ToList();




                foreach (var items in DetailList)
                {
                    item.TotalAmount += items.Amount2;
                }
                if (item.DiscountTabBillAmount.HasValue)
                {
                    item.TotalAmount -= item.DiscountTabBillAmount.Value;
                }
                if (item.DisCountAllTab.HasValue)
                {
                    if (item.isDisCountAllTab == true)
                    {
                        item.TotalAmount -= (item.TotalAmount * item.DisCountAllTab.Value) / 100;
                    }
                    else
                    {
                        item.TotalAmount -= item.DisCountAllTab.Value;
                    }

                }

            }
            if (!string.IsNullOrEmpty(txtCode))
            {
                model = model.Where(x => x.Code == txtCode).ToList();
            }

            if (!string.IsNullOrEmpty(txtCusName))
            {
                txtCusName = Helpers.Common.ChuyenThanhKhongDau(txtCusName);
                model = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.CustomerName).Contains(txtCusName)).ToList();
            }

            //if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin())
            //{
            //    model = model.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true).ToList();
            //}
            if (UserId > 0)
            {
                model = model.Where(x => x.CreatedUserId == UserId).ToList();
            }
            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "delete")
                {
                    model = model.Where(x => x.IsDeleted == true).ToList();
                }
                if (Status == "complete")
                {
                    model = model.Where(x => x.Status == "complete").ToList();
                }

                if (Status == "inprogress")
                {
                    model = model.Where(x => x.Status == "inprogress").ToList();
                }
                if (Status == "pending")
                {
                    model = model.Where(x => x.Status == "pending").ToList();
                }

                if (Status == "inprogress")
                {
                    model = model.Where(x => x.Status == "inprogress").ToList();
                }
                if (Status == "dc")
                {
                    var productListId = ProductRepository.GetAllvwProductAndService()
                        .Where(item => item.Code == "ĐC").Select(item => item.Id).ToList();

                    if (productListId.Count > 0)
                    {
                        List<int> listProductInboundId = new List<int>();
                        foreach (var id in productListId)
                        {
                            var list = productInvoiceRepository.GetAllvwInvoiceDetails().Where(x => x.ProductCode == "ĐC")
                                .Select(item => item.ProductInvoiceId.Value).Distinct().ToList();

                            listProductInboundId.AddRange(list);
                        }

                        model = model.Where(item => listProductInboundId.Contains(item.Id)).ToList();
                    }
                }
            }

            //Lọc theo ngày
            DateTime d_startDate, d_endDate;
            if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_startDate))
            {
                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out d_endDate))
                {
                    d_endDate = d_endDate.AddHours(23).AddMinutes(59);
                    model = model.Where(x => x.CreatedDate >= d_startDate && x.CreatedDate <= d_endDate).ToList();
                }
            }

            decimal minAmount;
            if (decimal.TryParse(txtMinAmount, out minAmount))
            {
                model = model.Where(x => x.TotalAmount >= minAmount).ToList();
            }

            decimal maxAmount;
            if (decimal.TryParse(txtMaxAmount, out maxAmount))
            {
                if (maxAmount > 0)
                {
                    model = model.Where(x => x.TotalAmount <= maxAmount).ToList();
                }
            }
            if (BranchId != null && BranchId.Value > 0)
            {
                model = model.Where(x => x.BranchId == BranchId).ToList();
            }
            JsonResult jr = Json(model, JsonRequestBehavior.AllowGet);
            jr.MaxJsonLength = int.MaxValue;
            return jr;
        }
        #region Create

        public ActionResult Create(int? Id, int? CustomerId)
        {

            ProductInvoiceViewModel model = new ProductInvoiceViewModel();
            model.DetailList = new List<ProductInvoiceDetailViewModel>();
            if (!Filters.SecurityFilter.IsDrugStore())
            {
                return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
            }
            if (Id.HasValue && Id > 0)
            {
                var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id.Value);
                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || productInvoice.BranchId == Helpers.Common.CurrentUser.BranchId))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                AutoMapper.Mapper.Map(productInvoice, model);

                var detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();
                AutoMapper.Mapper.Map(detailList, model.DetailList);
            }

            model.ReceiptViewModel = new ReceiptViewModel();
            model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
            model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
            model.ReceiptViewModel.Amount = 0;

            var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");
            string image_folder_product = Helpers.Common.GetSetting("product-image-folder");
            //  string image_folder_service = Helpers.Common.GetSetting("service-image-folder");

            int branchId = Helpers.Common.CurrentUser.BranchId.Value;
            var productList = inventoryRepository.GetAllvwInventoryByBranchId(branchId).Where(x => x.Quantity > 0 && x.IsSale == true)
               .Select(item => new ProductViewModel
               {
                   Type = "product",
                   Code = item.ProductCode,
                   Name = item.ProductName,
                   Id = item.ProductId,
                   CategoryCode = string.IsNullOrEmpty(item.CategoryCode) ? "Sản phẩm khác" : item.CategoryCode,
                   PriceOutbound = item.ProductPriceOutbound,
                   Unit = item.ProductUnit,
                   QuantityTotalInventory = item.Quantity
               }).OrderBy(item => item.CategoryCode).ToList();
            var jsonProductInvoiceItem = productList.Select(item => new ProductInvoiceItemViewModel
            {
                Id = item.Id,
                Type = item.Type,
                Code = item.Code,
                Name = item.Name.Replace("\"", "\\\""),
                CategoryCode = item.CategoryCode,
                PriceOutbound = item.PriceOutbound,
                Unit = item.Unit,
                InventoryQuantity = item.QuantityTotalInventory.Value,
                Image_Name = item.Image_Name,
                IsCombo = item.IsCombo
            }).ToList();

            ViewBag.json = JsonConvert.SerializeObject(jsonProductInvoiceItem);

            List<SelectListItem> categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem { Text = "TẤT CẢ", Value = "0" });
            categoryList.Add(new SelectListItem { Text = "I. SẢN PHẨM", Value = "1" });
            foreach (var item in productList.GroupBy(i => i.CategoryCode).Select(i => new { Name = i.Key, NumberOfItem = i.Count() }))
            {
                categoryList.Add(new SelectListItem { Text = "---- " + item.Name + " (" + item.NumberOfItem + ")", Value = item.Name });
            }
            ViewBag.CategoryList = categoryList;

            //Thêm số lượng tồn kho cho chi tiết đơn hàng đã được thêm
            if (model.DetailList != null && model.DetailList.Count > 0)
            {
                foreach (var item in model.DetailList)
                {
                    var product = productList.Where(i => i.Id == item.ProductId).FirstOrDefault();
                    if (product != null)
                    {
                        item.QuantityInInventory = product.QuantityTotalInventory;
                        item.PriceTest = product.PriceOutbound;
                    }
                    else
                    {
                        item.Id = 0;
                    }
                }

                model.DetailList.RemoveAll(x => x.Id == 0);

                int n = 0;
                foreach (var item in model.DetailList)
                {
                    item.OrderNo = n;
                    n++;
                }
            }
            model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;

            int taxfee = 0;
            int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
            model.TaxFee = taxfee;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductInvoiceViewModel model)
        {
            if (ModelState.IsValid && model.DetailList.Count != 0)
            {
                ProductInvoice productInvoice = null;
                if (model.Id > 0)
                {
                    productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                    //Kiểm tra phân quyền Chi nhánh
                    if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.BranchId + ",").Contains("," + productInvoice.BranchId + ",") == true))
                    {
                        return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                    }
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        if (productInvoice != null)
                        {
                            //Nếu đã ghi sổ rồi thì không được sửa
                            if (productInvoice.IsArchive)
                            {
                                return RedirectToAction("Detail", new { Id = productInvoice.Id });
                            }

                            AutoMapper.Mapper.Map(model, productInvoice);
                        }
                        else
                        {
                            productInvoice = new ProductInvoice();
                            AutoMapper.Mapper.Map(model, productInvoice);
                            productInvoice.IsDeleted = false;
                            productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
                            productInvoice.CreatedDate = DateTime.Now;
                            productInvoice.Status = Wording.OrderStatus_pending;
                            productInvoice.BranchId = model.BranchId.Value;
                            productInvoice.IsArchive = false;
                            productInvoice.IsReturn = false;
                        }

                        //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                        List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
                        foreach (var group in model.DetailList)
                        {
                            var product = ProductRepository.GetProductById(group.ProductId.Value);
                            listNewCheckSameId.Add(new ProductInvoiceDetail
                            {
                                ProductId = product.Id,
                                ProductType = product.Type,
                                Quantity = group.Quantity,
                                Unit = product.Unit,
                                Price = group.Price,
                                PromotionDetailId = group.PromotionDetailId,
                                PromotionId = group.PromotionId,
                                PromotionValue = group.PromotionValue,
                                IsDeleted = false,
                                CreatedUserId = WebSecurity.CurrentUserId,
                                ModifiedUserId = WebSecurity.CurrentUserId,
                                CreatedDate = DateTime.Now,

                                ModifiedDate = DateTime.Now,
                                FixedDiscount = group.FixedDiscount,
                                FixedDiscountAmount = group.FixedDiscountAmount,
                                IrregularDiscount = group.IrregularDiscount,
                                IrregularDiscountAmount = group.IrregularDiscountAmount,
                                QuantitySaleReturn = group.Quantity,
                                CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false),
                                IsReturn = false,
                                Origin = product.Origin
                                //StaffId = group.StaffId,
                                //Status = group.Status

                            });
                        }

                        //Tính lại chiết khấu
                        foreach (var item in listNewCheckSameId)
                        {
                            var thanh_tien = item.Quantity * item.Price;
                            item.FixedDiscountAmount = Convert.ToInt32(item.FixedDiscount * thanh_tien / 100);
                            item.IrregularDiscountAmount = Convert.ToInt32(item.IrregularDiscount * thanh_tien / 100);
                            //tổng Point

                        }
                        var total = listNewCheckSameId.Sum(item => item.Quantity.Value * item.Price.Value - item.FixedDiscountAmount.Value - item.IrregularDiscountAmount);
                        productInvoice.TotalAmount = total.Value + (total.Value * Convert.ToInt32(Erp.BackOffice.Helpers.Common.GetSetting("VAT")));

                        productInvoice.IsReturn = false;
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.PaidAmount = 0;
                        productInvoice.RemainingAmount = productInvoice.TotalAmount;
                        //hàm edit 
                        if (model.Id > 0)
                        {
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            var listDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

                            //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail, UsingServiceLog,UsingServiceLogDetail,ServiceReminder
                            productInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

                            //thêm mới toàn bộ database
                            foreach (var item in listNewCheckSameId)
                            {
                                item.ProductInvoiceId = productInvoice.Id;
                                productInvoiceRepository.InsertProductInvoiceDetail(item);

                                //Kiểm tra xem KH có mua DV nào hay không, nếu có thì tạo dữ liệu cho bảng UsingService, ServiceReminder
                                //if (item.ProductType == "service")
                                //{
                                //    CreateUsingLogAndReminder(item, model.DetailList, productInvoice.CreatedDate);
                                //}
                            }

                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }
                        else
                        {
                            //hàm thêm mới
                            productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

                            //cập nhật lại mã hóa đơn
                            string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
                            productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);


                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }

                        //Tạo phiếu nhập, nếu là tự động
                        string sale_tu_dong_tao_chung_tu = Erp.BackOffice.Helpers.Common.GetSetting("sale_tu_dong_tao_chung_tu");
                        if (listNewCheckSameId.Where(x => x.ProductType == "product").Count() <= 0)
                        {
                            if (sale_tu_dong_tao_chung_tu == "true")
                            {
                                ProductOutboundViewModel productOutboundViewModel = new ProductOutboundViewModel();
                                var warehouseDefault = warehouseRepository.GetAllWarehouse().Where(x => ("," + Helpers.Common.CurrentUser.BranchId + ",").Contains("," + x.BranchId + ",") == true && x.IsSale == true).FirstOrDefault();

                                //Nếu trong đơn hàng có sản phẩm thì xuất kho
                                if (model.DetailList.Any(item => item.ProductType == "product") && warehouseDefault != null)
                                {
                                    productOutboundViewModel.InvoiceId = productInvoice.Id;
                                    productOutboundViewModel.InvoiceCode = productInvoice.Code;
                                    productOutboundViewModel.WarehouseSourceId = warehouseDefault.Id;
                                    productOutboundViewModel.Note = "Xuất kho cho đơn hàng " + productInvoice.Code;
                                    var DetailList = model.DetailList.Where(x => x.ProductType == "product").ToList();
                                    //Lấy dữ liệu cho chi tiết
                                    productOutboundViewModel.DetailList = new List<ProductOutboundDetailViewModel>();
                                    AutoMapper.Mapper.Map(DetailList, productOutboundViewModel.DetailList);

                                    var productOutbound = ProductOutboundController.AutoCreateProductOutboundFromProductInvoice(productOutboundRepository, productOutboundViewModel, productInvoice.Code);

                                    //Ghi sổ chứng từ phiếu xuất
                                    ProductOutboundController.Archive(productOutbound, TempData);
                                }

                                //Ghi sổ chứng từ bán hàng
                                model.Id = productInvoice.Id;
                                Archive(model);
                            }
                        }
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }
                return RedirectToAction("Detail", new { Id = productInvoice.Id });
            }

            return View();
        }
        #endregion

        //public void CreateUsingLogAndReminder(ProductInvoiceDetail item, List<ProductInvoiceDetailViewModel> model, DateTime? CreatedDate)
        //{
        //    //Kiểm tra xem nếu là dịch vụ, gói combo thì thêm số lần sử dụng .
        //    var product = ProductRepository.GetProductById(item.ProductId);
        //    var aa = model.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
        //    if (product.IsCombo == true)
        //    {
        //        var combo = servicecomboRepository.GetAllServiceCombo().Where(x => x.ComboId == item.ProductId).ToList();

        //        foreach (var x in combo)
        //        {
        //            var qty = x.Quantity * item.Quantity;
        //            //Tao số lần sử dụng dịch vụ
        //            CreateUsingServiceLog(item.Id, x.ServiceId, qty, aa.StaffId, aa.Status);
        //            //Tạo lịch sử lời nhắc nhở của từng dịch vụ
        //            CreateLogServiceReminder(item.Id, x.ServiceId, CreatedDate);
        //        }
        //    }
        //    else
        //    {
        //        CreateUsingServiceLog(item.Id, item.ProductId, item.Quantity, aa.StaffId, aa.Status);
        //        CreateLogServiceReminder(item.Id, item.ProductId, CreatedDate);
        //    }

        //}

        //#region SỐ lần sử dụng dịch vụ
        //public static void CreateUsingServiceLog(int? ServiceInvoiceDetailId, int? ServiceId, int? Quantity, int? StaffId, string Status)
        //{
        //    Erp.Domain.Sale.Repositories.UsingServiceLogRepository usingServiceLogRepository = new Erp.Domain.Sale.Repositories.UsingServiceLogRepository(new Domain.Sale.ErpSaleDbContext());
        //    Erp.Domain.Sale.Repositories.UsingServiceLogDetailRepository usingServiceLogDetailRepository = new Erp.Domain.Sale.Repositories.UsingServiceLogDetailRepository(new Domain.Sale.ErpSaleDbContext());
        //    var usingServiceLog = new UsingServiceLog();
        //    usingServiceLog.IsDeleted = false;
        //    usingServiceLog.CreatedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLog.ModifiedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLog.AssignedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLog.CreatedDate = DateTime.Now;
        //    usingServiceLog.ModifiedDate = DateTime.Now;
        //    usingServiceLog.ServiceInvoiceDetailId = ServiceInvoiceDetailId;
        //    usingServiceLog.ServiceId = ServiceId;
        //    usingServiceLog.Quantity = Quantity;
        //    usingServiceLog.QuantityUsed = 1;
        //    usingServiceLogRepository.InsertUsingServiceLog(usingServiceLog);

        //    var usingServiceLogDetail = new UsingServiceLogDetail();
        //    usingServiceLogDetail.IsDeleted = false;
        //    usingServiceLogDetail.CreatedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLogDetail.ModifiedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLogDetail.AssignedUserId = WebSecurity.CurrentUserId;
        //    usingServiceLogDetail.CreatedDate = DateTime.Now;
        //    usingServiceLogDetail.ModifiedDate = DateTime.Now;
        //    usingServiceLogDetail.UsingServiceId = usingServiceLog.Id;
        //    usingServiceLogDetail.Type = "usedservice";
        //    usingServiceLogDetail.StaffId = StaffId;
        //    usingServiceLogDetail.Status = Status;
        //    usingServiceLogDetailRepository.InsertUsingServiceLogDetail(usingServiceLogDetail);

        //}
        //#endregion

        //#region Lịch sử lời nhắc nhở của từng dịch vụ
        //public static void CreateLogServiceReminder(int? ServiceInvoiceDetailId, int? ServiceId, DateTime? InvoiceDate)
        //{
        //    Erp.Domain.Crm.Repositories.TaskRepository TaskRepository = new Erp.Domain.Crm.Repositories.TaskRepository(new Domain.Crm.ErpCrmDbContext());
        //    Erp.Domain.Sale.Repositories.ServiceReminderGroupRepository ServiceReminderGroupRepository = new Erp.Domain.Sale.Repositories.ServiceReminderGroupRepository(new Domain.Sale.ErpSaleDbContext());
        //    Erp.Domain.Sale.Repositories.ProductInvoiceRepository productInvoiceRepository = new Erp.Domain.Sale.Repositories.ProductInvoiceRepository(new Domain.Sale.ErpSaleDbContext());
        //    Erp.Domain.Sale.Repositories.LogServiceRemminderRepository logServiceReminderRepository = new Erp.Domain.Sale.Repositories.LogServiceRemminderRepository(new Domain.Sale.ErpSaleDbContext());
        //    var reminder = ServiceReminderGroupRepository.GetAllvwServiceReminderGroup().Where(q => q.ServiceId.Value == ServiceId).ToList();
        //    var invoicedetail = productInvoiceRepository.GetvwProductInvoiceDetailById(ServiceInvoiceDetailId.Value);
        //    foreach (var ii in reminder)
        //    {
        //        var logReminder = new LogServiceRemminder();
        //        logReminder.IsDeleted = false;
        //        logReminder.CreatedUserId = WebSecurity.CurrentUserId;
        //        logReminder.ModifiedUserId = WebSecurity.CurrentUserId;
        //        logReminder.AssignedUserId = WebSecurity.CurrentUserId;
        //        logReminder.CreatedDate = DateTime.Now;
        //        logReminder.ModifiedDate = DateTime.Now;
        //        logReminder.ProductInvoiceDetailId = ServiceInvoiceDetailId;
        //        logReminder.ReminderId = ii.ServiceReminderId;
        //        logReminder.ReminderName = ii.Name;
        //        logReminder.ServiceId = ServiceId;

        //        if (ii.Reminder == true)
        //        {
        //            var date = InvoiceDate.Value.AddDays(ii.QuantityDate.Value);
        //            logReminder.ReminderDate = date;
        //        }
        //        logServiceReminderRepository.InsertLogServiceRemminder(logReminder);

        //        if (ii.Reminder == true)
        //        {
        //            var date = InvoiceDate.Value.AddDays(ii.QuantityDate.Value);
        //            var Task = new Erp.Domain.Crm.Entities.Task();
        //            Task.IsDeleted = false;
        //            Task.CreatedUserId = WebSecurity.CurrentUserId;
        //            Task.ModifiedUserId = WebSecurity.CurrentUserId;
        //            Task.CreatedDate = DateTime.Now;
        //            Task.ModifiedDate = DateTime.Now;
        //            Task.Type = "task";
        //            Task.ParentType = "LogServiceRemminder";
        //            Task.ParentId = logReminder.Id;
        //            Task.Status = "pending";
        //            Task.StartDate = InvoiceDate;
        //            Task.DueDate = Convert.ToDateTime(date);
        //            Task.Subject = "Khách hàng " + invoicedetail.CustomerCode + " - " + invoicedetail.CustomerName + " hẹn tái khám ngày: " + Task.DueDate.Value.ToString("dd/MM/yyyy");
        //            Task.Description = Task.Subject;
        //            TaskRepository.InsertTask(Task);
        //        }
        //    }

        //    //ghi lịch sử lời nhắc vào task.
        //    // Crm.Controllers.ProcessController.Run("ProductInvoice", "Create", null, null, null, logReminder);
        //}
        //#endregion

        #region LoadProductItem

        public PartialViewResult LoadProductItem(int? OrderNo, int? ProductId, string ProductName, string Unit, int Quantity, decimal Price, string ProductCode, string ProductType, int QuantityInventory, bool? CheckPromotion, int? CustomerId)
        {
            var model = new ProductInvoiceDetailViewModel();
            model.OrderNo = OrderNo.Value;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
            model.Unit = Unit;
            model.Quantity = Quantity;
            model.Price = Price;
            model.ProductCode = ProductCode;
            model.ProductType = ProductType;
            model.QuantityInInventory = QuantityInventory;

            model.FixedDiscountAmount = 0;
            model.IrregularDiscountAmount = 0;
            model.FixedDiscount = 0;
            model.IrregularDiscount = 0;
            model.CheckPromotion = CheckPromotion;
            model.PriceTest = Price;
            //var commision = commisionCustomerRepository.GetAllCommisionCustomer()
            //    .Where(item => item.ProductId == ProductId && item.CustomerId == CustomerId && item.IsMoney != true).FirstOrDefault();
            //if (commision == null)
            //{
            //    model.DisCount = 0;
            //}
            //else
            //{
            //    model.DisCount = Convert.ToInt32(commision.CommissionValue);

            //}

            return PartialView(model);
        }
        #endregion

        #region Print


        private void PrintPhieu(int Id, bool ExportExcel = false)
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
            var taxcode = Erp.BackOffice.Helpers.Common.GetSetting("taxcode");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            //lấy hóa đơn.
            var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id);
            //lấy thông tin khách hàng
            var customer = customerRepository.GetvwCustomerByCode(productInvoice.CustomerCode);

            List<ProductInvoiceDetailViewModel> detailList = new List<ProductInvoiceDetailViewModel>();
            if (productInvoice != null && productInvoice.IsDeleted != true)
            {
                //lấy danh sách sản phẩm xuất kho
                detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id)
                        .Select(x => new ProductInvoiceDetailViewModel
                        {
                            Id = x.Id,
                            Price = x.Price,
                            ProductId = x.ProductId,
                            ProductName = x.ProductName,
                            ProductCode = x.ProductCode,
                            Quantity = x.Quantity,
                            Unit = x.Unit,
                            FixedDiscount = x.FixedDiscount.HasValue ? x.FixedDiscount.Value : 0,
                            FixedDiscountAmount = x.FixedDiscountAmount.HasValue ? x.FixedDiscountAmount : 0,
                            IrregularDiscount = x.IrregularDiscount.HasValue ? x.IrregularDiscount.Value : 0,
                            IrregularDiscountAmount = x.IrregularDiscountAmount.HasValue ? x.IrregularDiscountAmount : 0,
                            ProductGroup = x.ProductGroup,
                            CheckPromotion = x.CheckPromotion,
                            ProductType = x.ProductType,
                            CategoryCode = x.CategoryCode,
                            StaffName = x.StaffName,
                            LoCode = x.LoCode,
                            ExpiryDate = x.ExpiryDate,
                            Manufacturer = x.Manufacturer,
                            Amount = x.Amount,
                            Origin = x.Origin,
                            TaxFee = x.TaxFee,
                            TaxFeeAmount = x.TaxFeeAmount
                        }).ToList();
            }

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInvoice")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Code}", productInvoice.Code);
            model.Content = model.Content.Replace("{CreateDate}", productInvoice.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{Hours}", productInvoice.CreatedDate.Value.ToString("HH:mm"));
            model.Content = model.Content.Replace("{CustomerName}", productInvoice.CustomerName);
            model.Content = model.Content.Replace("{CustomerPhone}", productInvoice.CustomerPhone);
            model.Content = model.Content.Replace("{CompanyName}", productInvoice.CompanyName);
            model.Content = model.Content.Replace("{StaffCreateName}", productInvoice.SalerFullName);
            model.Content = model.Content.Replace("{BankAccount}", productInvoice.BankAccount);
            model.Content = model.Content.Replace("{BankName}", productInvoice.BankName);
            model.Content = model.Content.Replace("{TaxCode}", productInvoice.TaxCode);
            if (!string.IsNullOrEmpty(customer.Address))
            {
                model.Content = model.Content.Replace("{Address}", customer.Address + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Address}", "");
            }
            if (!string.IsNullOrEmpty(customer.DistrictName))
            {
                model.Content = model.Content.Replace("{District}", customer.DistrictName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{District}", "");
            }
            if (!string.IsNullOrEmpty(customer.WardName))
            {
                model.Content = model.Content.Replace("{Ward}", customer.WardName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Ward}", "");
            }
            if (!string.IsNullOrEmpty(customer.ProvinceName))
            {
                model.Content = model.Content.Replace("{Province}", customer.ProvinceName);
            }
            else
            {
                model.Content = model.Content.Replace("{Province}", "");
            }

            model.Content = model.Content.Replace("{Note}", productInvoice.Note);
            if (!string.IsNullOrEmpty(productInvoice.CodeInvoiceRed))
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code + " - " + productInvoice.CodeInvoiceRed);
            }
            else
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code);
            }
            model.Content = model.Content.Replace("{PaymentMethod}", productInvoice.PaymentMethod);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(productInvoice.TotalAmount.ToString()));

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList(detailList, productInvoice));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{System.TaxCode}", taxcode);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            //Tạo barcode
            Image barcode = Code128Rendering.MakeBarcodeImage(productInvoice.Code, 1, true);
            model.Content = model.Content.Replace("{BarcodeImgSource}", ImageToBase64(barcode, System.Drawing.Imaging.ImageFormat.Png));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + productInvoice.CreatedDate.Value.ToString("yyyyMMdd") + productInvoice.Code + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
        }


        public ViewResult Print(int Id, bool ExportExcel = false)
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
            var taxcode = Erp.BackOffice.Helpers.Common.GetSetting("taxcode");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            var receipt = Erp.BackOffice.Helpers.Common.GetSetting("");
            //lấy hóa đơn.
            var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id);
            //lấy thông tin khách hàng

            vwCustomer customer = new vwCustomer();
            if (productInvoice.CustomerId.HasValue)
            {
                customer = customerRepository.GetvwCustomerById(productInvoice.CustomerId.Value);
            }
            else
            {
                customer = customerRepository.GetvwCustomerByCode("KHACHVANGLAI");
            }

            List<ProductInvoiceDetailViewModel> detailList = new List<ProductInvoiceDetailViewModel>();
            if (productInvoice != null && productInvoice.IsDeleted != true)
            {
                //lấy danh sách sản phẩm xuất kho
                detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id)
                        .Select(x => new ProductInvoiceDetailViewModel
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
                            BranchName = x.BranchName
                        }).ToList();
                productInvoice.TotalAmount = 0;
                foreach (var item in detailList)
                {
                    productInvoice.TotalAmount += item.Amount2;
                }
            }

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInvoice")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Code}", productInvoice.Code);
            model.Content = model.Content.Replace("{CreateDate}", productInvoice.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{Hours}", productInvoice.CreatedDate.Value.ToString("HH:mm"));
            model.Content = model.Content.Replace("{CustomerName}", productInvoice.CustomerName);
            model.Content = model.Content.Replace("{CustomerPhone}", productInvoice.CustomerPhone);
            model.Content = model.Content.Replace("{CompanyName}", productInvoice.CompanyName);
            model.Content = model.Content.Replace("{StaffCreateName}", productInvoice.SalerFullName);
            model.Content = model.Content.Replace("{BankAccount}", productInvoice.BankAccount);
            model.Content = model.Content.Replace("{BankName}", productInvoice.BankName);
            model.Content = model.Content.Replace("{TaxCode}", productInvoice.TaxCode);
            /*if (!string.IsNullOrEmpty(customer.Address))
            {
                model.Content = model.Content.Replace("{Address}", customer.Address + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Address}", "");
            }
            if (!string.IsNullOrEmpty(customer.DistrictName))
            {
                model.Content = model.Content.Replace("{District}", customer.DistrictName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{District}", "");
            }
            if (!string.IsNullOrEmpty(customer.WardName))
            {
                model.Content = model.Content.Replace("{Ward}", customer.WardName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Ward}", "");
            }
            if (!string.IsNullOrEmpty(customer.ProvinceName))
            {
                model.Content = model.Content.Replace("{Province}", customer.ProvinceName);
            }
            else
            {
                model.Content = model.Content.Replace("{Province}", "");
            }*/

            model.Content = model.Content.Replace("{Note}", productInvoice.Note);
            if (!string.IsNullOrEmpty(productInvoice.CodeInvoiceRed))
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code + " - " + productInvoice.CodeInvoiceRed);
            }
            else
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code);
            }
            if (!string.IsNullOrEmpty(productInvoice.BranchName))
            {
                model.Content = model.Content.Replace("{BranchName}", productInvoice.BranchName);
            }
            else
            {
                model.Content = model.Content.Replace("{BranchName}", "");
            }
            model.Content = model.Content.Replace("{PaymentMethod}", productInvoice.PaymentMethod);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(productInvoice.TotalAmount.ToString()));

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList(detailList, productInvoice));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{System.TaxCode}", taxcode);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            //Tạo barcode
            Image barcode = Code128Rendering.MakeBarcodeImage(productInvoice.Code, 1, true);
            model.Content = model.Content.Replace("{BarcodeImgSource}", ImageToBase64(barcode, System.Drawing.Imaging.ImageFormat.Png));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + productInvoice.CreatedDate.Value.ToString("yyyyMMdd") + productInvoice.Code + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }
        public ViewResult PrintNT(int Id, bool ExportExcel = false)
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
            var taxcode = Erp.BackOffice.Helpers.Common.GetSetting("taxcode");
            var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";
            //lấy hóa đơn.
            var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id);
            //lấy thông tin khách hàng
            vwCustomer customer = new vwCustomer();
            if (productInvoice.CustomerId.HasValue)
            {
                customer = customerRepository.GetvwCustomerById(productInvoice.CustomerId.Value);
            }
            else
            {
                customer = customerRepository.GetvwCustomerByCode("KHACHVANGLAI");
            }


            List<ProductInvoiceDetailViewModel> detailList = new List<ProductInvoiceDetailViewModel>();
            if (productInvoice != null && productInvoice.IsDeleted != true)
            {
                //lấy danh sách sản phẩm xuất kho
                detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id)
                        .Select(x => new ProductInvoiceDetailViewModel
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
                            BranchName = x.BranchName
                        }).ToList();


            }

            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("pi2")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Code}", productInvoice.Code);
            model.Content = model.Content.Replace("{CreateDate}", productInvoice.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{Hours}", productInvoice.CreatedDate.Value.ToString("HH:mm"));
            model.Content = model.Content.Replace("{CustomerName}", productInvoice.CustomerName);
            model.Content = model.Content.Replace("{CustomerPhone}", productInvoice.CustomerPhone);
            model.Content = model.Content.Replace("{CompanyName}", productInvoice.CompanyName);
            model.Content = model.Content.Replace("{StaffCreateName}", productInvoice.SalerFullName);
            model.Content = model.Content.Replace("{BankAccount}", productInvoice.BankAccount);
            model.Content = model.Content.Replace("{BankName}", productInvoice.BankName);
            model.Content = model.Content.Replace("{TaxCode}", productInvoice.TaxCode);
            if (!string.IsNullOrEmpty(customer.Address))
            {
                model.Content = model.Content.Replace("{Address}", customer.Address + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Address}", "");
            }
            if (!string.IsNullOrEmpty(customer.DistrictName))
            {
                model.Content = model.Content.Replace("{District}", customer.DistrictName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{District}", "");
            }
            if (!string.IsNullOrEmpty(customer.WardName))
            {
                model.Content = model.Content.Replace("{Ward}", customer.WardName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Ward}", "");
            }
            if (!string.IsNullOrEmpty(customer.ProvinceName))
            {
                model.Content = model.Content.Replace("{Province}", customer.ProvinceName);
            }
            else
            {
                model.Content = model.Content.Replace("{Province}", "");
            }
            if (!string.IsNullOrEmpty(productInvoice.BranchName))
            {
                model.Content = model.Content.Replace("{BranchName}", productInvoice.BranchName);
            }
            else
            {
                model.Content = model.Content.Replace("{BranchName}", "");
            }

            model.Content = model.Content.Replace("{Note}", productInvoice.Note);
            if (!string.IsNullOrEmpty(productInvoice.CodeInvoiceRed))
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code + " - " + productInvoice.CodeInvoiceRed);
            }
            else
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code);
            }
            model.Content = model.Content.Replace("{PaymentMethod}", productInvoice.PaymentMethod);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(productInvoice.TotalAmount.ToString()));

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList(detailList, productInvoice));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            model.Content = model.Content.Replace("{System.TaxCode}", taxcode);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            //Tạo barcode
            Image barcode = Code128Rendering.MakeBarcodeImage(productInvoice.Code, 1, true);
            model.Content = model.Content.Replace("{BarcodeImgSource}", ImageToBase64(barcode, System.Drawing.Imaging.ImageFormat.Png));

            if (ExportExcel)
            {
                Response.AppendHeader("content-disposition", "attachment;filename=" + productInvoice.CreatedDate.Value.ToString("yyyyMMdd") + productInvoice.Code + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(model.Content);
                Response.End();
            }
            return View(model);
        }

        string buildHtmlDetailList(List<ProductInvoiceDetailViewModel> detailList, vwProductInvoice model)
        {
            decimal? tong_tien = 0;
            decimal? alldiscount = 0;
            decimal? tong_tien_thanh_toan = 0;
            decimal? discountalltab = 0;

            //int da_thanh_toan = 0;
            //int con_lai = 0;
            //var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id.Value);

            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            //detailLists += "		<th>STT</th>\r\n";
            //detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th style=\" text-align: left\">Đơn giá</th>\r\n";
            //detailLists += "		<th>Xuất xứ</th>\r\n";
            //detailLists += "		<th>Lô sản xuất</th>\r\n";
            //detailLists += "		<th>Hạn dùng</th>\r\n";
            //detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th style=\" text-align: right\">SL</th>\r\n";
            //detailLists += "		<th>Đơn giá</th>\r\n";
            //detailLists += "		<th>CKCĐ</th>\r\n";
            //detailLists += "		<th>CKĐX</th>\r\n";
            detailLists += "		<th style=\" text-align: right\">CK</th>\r\n";
            detailLists += "		<th style=\" text-align: right\">TT</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {
                decimal? subTotal = item.Quantity * item.Price.Value;

                alldiscount += item.IrregularDiscountAmount;
                tong_tien += subTotal;
                detailLists += "<tr>\r\n"

                + "<td class=\"text-left \">" + item.ProductName + "<p class=\"pp\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</p>" + "</td>\r\n"
                //+ "<td class=\"text-left \">" + item.Origin + "</td>\r\n"
                //+ "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                //+ "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                //+ "<td class=\"text-center\">" + item.Unit + "</td>\r\n"

                + "<td class=\"text-right code_product\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Quantity, null) + "</td>\r\n"
                //+ "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.FixedDiscountAmount, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.IrregularDiscountAmount, null) + "</td>\r\n"
                //+ "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.TaxFeeAmount, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(subTotal, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            alldiscount = model.IrregularDiscount;
            tong_tien_thanh_toan = model.TotalAmount;
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"3\" class=\"text-right\">Tổng tiền hàng:</td><td class=\"text-right\">"
                         //+ Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.FixedDiscountAmount), null)
                         //+ "</td><td class=\"text-right\">"
                         //+ Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.IrregularDiscountAmount), null)
                         //+ "</td><td class=\"text-right\">"
                         //+ Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.TaxFeeAmount), null)
                         //+ "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong_tien, null)
                         + "</td></tr>\r\n";
            //if (model.TaxFee > 0)
            //{
            //    var vat = tong_tien * Convert.ToDecimal(model.TaxFee)/100;
            //    var tong = tong_tien + vat;
            //    detailLists += "<tr><td colspan=\"11\" class=\"text-right\">VAT (" + model.TaxFee + "%)</td><td class=\"text-right\">"
            //            + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(vat, null)
            //            + "</td></tr>\r\n";
            //    detailLists += "<tr><td colspan=\"11\" class=\"text-right\">Tổng tiền cần thanh toán</td><td class=\"text-right\">"
            //        + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong, null)
            //        + "</td></tr>\r\n";
            //}


            detailLists += "<tr><td colspan=\"3\" class=\"text-right\">Giảm Giá:</td><td class=\"text-right\">"
                        + (alldiscount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr((alldiscount), null) : "0")
                        + "</td></tr>\r\n";
            detailLists += "<tr><td colspan=\"3\" class=\"text-right\">Tổng Thanh Toán:</td><td class=\"text-right\">"
                        + (tong_tien_thanh_toan > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr((tong_tien_thanh_toan), null) : "0")
                        + "</td></tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";



            return detailLists;
        }
        string buildHtmlDetailList2(List<ProductInvoiceDetailViewModel> detailList, ProductInvoice model)
        {
            decimal? tong_tien = 0;
            //int da_thanh_toan = 0;
            //int con_lai = 0;
            //var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id.Value);

            //Tạo table html chi tiết phiếu xuất
            string detailLists = "<table class=\"invoice-detail\">\r\n";
            detailLists += "<thead>\r\n";
            detailLists += "	<tr>\r\n";
            detailLists += "		<th>STT</th>\r\n";
            detailLists += "		<th>Mã SP</th>\r\n";
            detailLists += "		<th>Tên SP</th>\r\n";
            detailLists += "		<th>Xuất xứ</th>\r\n";
            detailLists += "		<th>Lô sản xuất</th>\r\n";
            detailLists += "		<th>Hạn dùng</th>\r\n";
            detailLists += "		<th>ĐVT</th>\r\n";
            detailLists += "		<th>SL</th>\r\n";
            detailLists += "		<th>Đơn giá</th>\r\n";
            detailLists += "		<th>CKCĐ</th>\r\n";
            detailLists += "		<th>CKĐX</th>\r\n";
            detailLists += "		<th>VAT</th>\r\n";
            detailLists += "		<th>Thành tiền</th>\r\n";
            detailLists += "	</tr>\r\n";
            detailLists += "</thead>\r\n";
            detailLists += "<tbody>\r\n";
            var index = 1;

            foreach (var item in detailList)
            {
                decimal? subTotal = item.Quantity * item.Price.Value - item.IrregularDiscountAmount - item.FixedDiscountAmount + item.TaxFeeAmount;

                tong_tien += subTotal;
                detailLists += "<tr>\r\n"
                + "<td class=\"text-center orderNo\">" + (index++) + "</td>\r\n"
                + "<td class=\"text-left code_product\">" + item.ProductCode + "</td>\r\n"
                + "<td class=\"text-left \">" + item.ProductName + "</td>\r\n"
                + "<td class=\"text-left \">" + item.Origin + "</td>\r\n"
                + "<td class=\"text-right\">" + item.LoCode + "</td>\r\n"
                + "<td class=\"text-right\">" + (item.ExpiryDate.HasValue ? item.ExpiryDate.Value.ToString("dd-MM-yyyy") : "") + "</td>\r\n"
                + "<td class=\"text-center\">" + item.Unit + "</td>\r\n"
                + "<td class=\"text-right orderNo\">" + Erp.BackOffice.Helpers.Common.PhanCachHangNgan(item.Quantity) + "</td>\r\n"
                + "<td class=\"text-right code_product\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.Price, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.FixedDiscountAmount, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.IrregularDiscountAmount, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(item.TaxFeeAmount, null) + "</td>\r\n"
                + "<td class=\"text-right chiet_khau\">" + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(subTotal, null) + "</td>\r\n"
                + "</tr>\r\n";
            }
            detailLists += "</tbody>\r\n";
            detailLists += "<tfoot style=\"font-weight:bold\">\r\n";
            detailLists += "<tr><td colspan=\"9\" class=\"text-right\">Tổng cộng</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.FixedDiscountAmount), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.IrregularDiscountAmount), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(detailList.Sum(x => x.TaxFeeAmount), null)
                         + "</td><td class=\"text-right\">"
                         + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong_tien, null)
                         + "</td></tr>\r\n";
            //if (model.TaxFee > 0)
            //{
            //    var vat = tong_tien * Convert.ToDecimal(model.TaxFee)/100;
            //    var tong = tong_tien + vat;
            //    detailLists += "<tr><td colspan=\"11\" class=\"text-right\">VAT (" + model.TaxFee + "%)</td><td class=\"text-right\">"
            //            + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(vat, null)
            //            + "</td></tr>\r\n";
            //    detailLists += "<tr><td colspan=\"11\" class=\"text-right\">Tổng tiền cần thanh toán</td><td class=\"text-right\">"
            //        + Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(tong, null)
            //        + "</td></tr>\r\n";
            //}


            detailLists += "<tr><td colspan=\"12\" class=\"text-right\">Đã thanh toán</td><td class=\"text-right\">"
                        + (model.PaidAmount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model.PaidAmount, null) : "0")
                        + "</td></tr>\r\n";
            detailLists += "<tr><td colspan=\"12\" class=\"text-right\">Còn lại phải thu</td><td class=\"text-right\">"
                        + (model.RemainingAmount > 0 ? Erp.BackOffice.Helpers.CommonSatic.ToCurrencyStr(model.RemainingAmount, null) : "0")
                        + "</td></tr>\r\n";
            detailLists += "</tfoot>\r\n</table>\r\n";

            return detailLists;
        }

        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int Id, string CancelReason)
        {
            var productInvoice = productInvoiceRepository.GetProductInvoiceById(Id);
            if (productInvoice != null)
            {
                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.BranchId + ",").Contains("," + productInvoice.BranchId + ",") == true))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                productInvoice.ModifiedDate = DateTime.Now;
                productInvoice.IsDeleted = true;
                productInvoice.IsArchive = false;
                productInvoice.CancelReason = CancelReason;
                productInvoice.Status = Wording.OrderStatus_deleted;
                productInvoiceRepository.UpdateProductInvoice(productInvoice);

                return RedirectToAction("Detail", new { Id = productInvoice.Id });
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Detail

        public ActionResult DetailByChart(string single, int? day, int? month, int? year, string CityId, string DistrictId, string branchId, int? quarter, int? week)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            single = single == null ? "month" : single;
            year = year == null ? DateTime.Now.Year : year;
            month = month == null ? DateTime.Now.Month : month;
            quarter = quarter == null ? 1 : quarter;
            CityId = string.IsNullOrEmpty(CityId) ? "" : CityId;
            DistrictId = string.IsNullOrEmpty(DistrictId) ? "" : DistrictId;
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            var weekdefault = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            week = week == null ? weekdefault : week;
            branchId = branchId == null ? "" : branchId.ToString();

            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);


            var q = productInvoiceRepository.GetAllvwProductInvoice().AsEnumerable().Where(x => x.IsArchive == true);
            //if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan() && string.IsNullOrEmpty(branchId))
            //{
            //    q = q.Where(x => ("," + user.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            //}
            if (!string.IsNullOrEmpty(CityId))
            {
                q = q.Where(item => !string.IsNullOrEmpty(item.CityId) && item.CityId == CityId);
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(item => !string.IsNullOrEmpty(item.DistrictId) && item.DistrictId == DistrictId);
            }
            if (!string.IsNullOrEmpty(branchId))
            {
                q = q.Where(item => !string.IsNullOrEmpty(branchId) && ("," + branchId + ",").Contains("," + item.BranchId + ",") == true);
            }
            if (year != null)
            {
                q = q.Where(n => n.Year == year);
            }
            if (month != null)
            {
                q = q.Where(n => n.Month == month);
            }
            if (day != null)
            {
                q = q.Where(n => n.Day == day);
            }

            q = q.Where(x => x.IsArchive && x.CreatedDate > StartDate && x.CreatedDate < EndDate);

            var model = q.Select(item => new ProductInvoiceViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Code = item.Code,
                CustomerCode = item.CustomerCode,
                CustomerName = item.CustomerName,
                TotalAmount = item.TotalAmount,
                //Discount = item.Discount,
                TaxFee = item.TaxFee,
                CodeInvoiceRed = item.CodeInvoiceRed,
                Status = item.Status,
                IsArchive = item.IsArchive,
                ProductOutboundId = item.ProductOutboundId,
                ProductOutboundCode = item.ProductOutboundCode,
                Note = item.Note,
                CancelReason = item.CancelReason,
                PaidAmount = item.PaidAmount,
                RemainingAmount = item.RemainingAmount,
                BranchName = item.BranchName,
                IrregularDiscount = item.IrregularDiscount,
                FixedDiscount = item.FixedDiscount
            }).OrderByDescending(m => m.CreatedDate);

            return View(model);
        }

        public ActionResult Detail(int? Id, string TransactionCode)
        {
            var productInvoice = new vwProductInvoice();

            if (Id != null && Id.Value > 0)
            {
                productInvoice = productInvoiceRepository.GetvwProductInvoiceFullById(Id.Value);
            }

            if (!string.IsNullOrEmpty(TransactionCode))
            {
                productInvoice = productInvoiceRepository.GetvwProductInvoiceByCode(TransactionCode);
            }

            if (productInvoice == null)
            {
                return RedirectToAction("Index");
            }

            var model = new ProductInvoiceViewModel();
            AutoMapper.Mapper.Map(productInvoice, model);

            model.ReceiptViewModel = new ReceiptViewModel();
            model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
            model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
            model.ReceiptViewModel.Amount = productInvoice.TotalAmount;

            //Lấy thông tin kiểm tra cho phép sửa chứng từ này hay không
            model.AllowEdit = Helpers.Common.KiemTraNgaySuaChungTu
                (model.CreatedDate.Value)
                && (Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.BranchId + ",").Contains("," + productInvoice.BranchId + ",") == true);

            //Lấy lịch sử giao dịch thanh toán
            var listTransaction = transactionLiabilitiesRepository.GetAllvwTransaction()
                        .Where(item => item.MaChungTuGoc == productInvoice.Code)
                        .OrderByDescending(item => item.CreatedDate)
                        .ToList();

            model.ListTransactionLiabilities = new List<TransactionLiabilitiesViewModel>();
            AutoMapper.Mapper.Map(listTransaction, model.ListTransactionLiabilities);

            model.Code = productInvoice.Code;
            //model.SalerId = productInvoice.SalerId;
            //model.SalerName = productInvoice.SalerFullName;
            model.CreatedUserName = Helpers.Common.CurrentUser.FullName;
            var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");

            //Lấy danh sách chi tiết đơn hàng
            model.DetailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).Select(x =>
                new ProductInvoiceDetailViewModel
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
                    Amount2 = x.Amount2
                }).OrderBy(x => x.Id).ToList();

            model.GroupProduct = model.DetailList.GroupBy(x => new { x.ProductGroup }, (key, group) => new ProductInvoiceDetailViewModel
            {
                ProductGroup = key.ProductGroup,
                ProductId = group.FirstOrDefault().ProductId,
                Id = group.FirstOrDefault().Id
            }).ToList();

            foreach (var item in model.GroupProduct)
            {
                if (!string.IsNullOrEmpty(item.ProductGroup))
                {
                    var ProductGroupName = categoryRepository.GetCategoryByCode("Categories_product").Where(x => x.Value == item.ProductGroup).FirstOrDefault();
                    item.ProductGroupName = ProductGroupName.Name;
                }
            }

            //Lấy thông tin phiếu xuất kho
            if (productInvoice.ProductOutboundId != null && productInvoice.ProductOutboundId > 0)
            {
                var productOutbound = productOutboundRepository.GetvwProductOutboundById(productInvoice.ProductOutboundId.Value);
                model.ProductOutboundViewModel = new ProductOutboundViewModel();
                AutoMapper.Mapper.Map(productOutbound, model.ProductOutboundViewModel);
            }

            ViewBag.isAdmin = Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId == 1 ? true : false;

            // model.ModifiedUserName = userRepository.GetUserById(model.ModifiedUserId.Value).FullName;

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            model.TotalAmount = 0;
            foreach (var item in model.DetailList)
            {
                model.TotalAmount += item.Amount2;
            }
            if (model.DiscountTabBillAmount.HasValue && model.DiscountTabBillAmount > 0)
            {
                model.TotalAmount -= model.DiscountTabBillAmount.Value;
            }
            if (model.DisCountAllTab.HasValue)
            {
                if (model.isDisCountAllTab == true)
                {
                    model.TotalAmount -= (model.TotalAmount * model.DisCountAllTab.Value) / 100;
                }
                else
                {
                    model.TotalAmount -= model.DisCountAllTab.Value;
                }

            }
            return View(model);
        }

        public ActionResult KiemTraKho(int Id, string TransactionCode)
        {
            #region bỏ ghi sổ ProductOutbound
            string check = "";
            var invoice_detail_list = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id).ToList();

            foreach (var item in invoice_detail_list)
            {
                var error = InventoryController.Check(item.ProductName, item.ProductId.Value, item.LoCode, item.ExpiryDate, 1, 0, item.Quantity.Value);
                check += error;
            }
            if (string.IsNullOrEmpty(check) == false)
            {
                TempData[Globals.FailedMessageKey] = "Hàng không đủ xuất " + check;
            }
            else
            {
                TempData[Globals.SuccessMessageKey] = "Hàng đủ xuất";
            }
            //productOutboundRepository.DeleteProductOutboundRs(item.Id);


            #endregion
            return RedirectToAction("Detail", new { Id = Id }); ;
        }
        #endregion

        #region - Json -
        public JsonResult GetListJsonInvoiceDetailById(int? Id)
        {
            if (Id == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id.Value);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Archive
        [HttpPost]
        public ActionResult Archive(ProductInvoiceViewModel model)
        {
            if (Request["Submit"] == "Save")
            {
                var productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);

                //Kiểm tra cho phép sửa chứng từ này hay không
                if (Helpers.Common.KiemTraNgaySuaChungTu(productInvoice.CreatedDate.Value) == false)
                {
                    return RedirectToAction("Detail", new { Id = productInvoice.Id });
                }

                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.BranchId + ",").Contains("," + productInvoice.BranchId + ",") == true))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                //Coi thử đã xuất kho chưa mới cho ghi sổ
                bool hasProductOutbound = productOutboundRepository.GetAllvwProductOutbound().Any(item => item.InvoiceId == productInvoice.Id);
                bool hasProduct = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).Any(item => item.ProductType == "product");

                if (!hasProductOutbound && hasProduct)
                {
                    TempData[Globals.FailedMessageKey] = "Chưa lập phiếu xuất kho!";
                    return RedirectToAction("Detail", new { Id = productInvoice.Id });
                }

                if (!productInvoice.IsArchive)
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {
                            //Cập nhật thông tin thanh toán cho đơn hàng
                            productInvoice.PaymentMethod = model.ReceiptViewModel.PaymentMethod;
                            productInvoice.PaidAmount = Convert.ToDecimal(model.ReceiptViewModel.Amount);
                            productInvoice.RemainingAmount = productInvoice.TotalAmount - productInvoice.PaidAmount;
                            productInvoice.NextPaymentDate = model.NextPaymentDate_Temp;

                            productInvoice.ModifiedDate = DateTime.Now;
                            productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);

                            //Lấy mã KH
                            var customer = customerRepository.GetCustomerById(productInvoice.CustomerId.Value);

                            var remain = productInvoice.TotalAmount - Convert.ToDecimal(model.ReceiptViewModel.Amount.Value);
                            if (remain > 0)
                            {

                            }
                            else
                            {
                                productInvoice.NextPaymentDate = null;
                                model.NextPaymentDate_Temp = null;
                            }

                            //Ghi Nợ TK 131 - Phải thu của khách hàng (tổng giá thanh toán)
                            Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    "Bán hàng",
                                    customer.Code,
                                    "Customer",
                                    productInvoice.TotalAmount,
                                    0,
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    null,
                                    model.NextPaymentDate_Temp,
                                    null);

                            //Khách thanh toán ngay
                            if (model.ReceiptViewModel.Amount > 0)
                            {
                                //Lập phiếu thu
                                var receipt = new Receipt();
                                AutoMapper.Mapper.Map(model.ReceiptViewModel, receipt);
                                receipt.IsDeleted = false;
                                receipt.CreatedUserId = WebSecurity.CurrentUserId;
                                receipt.ModifiedUserId = WebSecurity.CurrentUserId;
                                receipt.AssignedUserId = WebSecurity.CurrentUserId;
                                receipt.CreatedDate = DateTime.Now;
                                receipt.ModifiedDate = DateTime.Now;
                                receipt.VoucherDate = DateTime.Now;
                                receipt.CustomerId = customer.Id;
                                receipt.Payer = customer.LastName + " " + customer.FirstName;
                                receipt.PaymentMethod = productInvoice.PaymentMethod;
                                receipt.Address = customer.Address;
                                receipt.MaChungTuGoc = productInvoice.Code;
                                receipt.LoaiChungTuGoc = "ProductInvoice";
                                receipt.Note = receipt.Name;
                                //receipt.BranchId = Helpers.Common.CurrentUser.BranchId.Value;

                                if (receipt.Amount > productInvoice.TotalAmount)
                                    receipt.Amount = productInvoice.TotalAmount;

                                ReceiptRepository.InsertReceipt(receipt);

                                var prefixReceipt = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                                receipt.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixReceipt, receipt.Id);
                                ReceiptRepository.UpdateReceipt(receipt);

                                //Thêm vào quản lý chứng từ
                                TransactionController.Create(new TransactionViewModel
                                {
                                    TransactionModule = "Receipt",
                                    TransactionCode = receipt.Code,
                                    TransactionName = "Thu tiền khách hàng"
                                });

                                //Thêm chứng từ liên quan
                                TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                                {
                                    TransactionA = receipt.Code,
                                    TransactionB = productInvoice.Code
                                });

                                //Ghi Có TK 131 - Phải thu của khách hàng.
                                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    receipt.Code,
                                    "Receipt",
                                    "Thu tiền khách hàng",
                                    customer.Code,
                                    "Customer",
                                    0,
                                    Convert.ToDecimal(model.ReceiptViewModel.Amount),
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    model.ReceiptViewModel.PaymentMethod,
                                    null,
                                    null);
                            }
                            scope.Complete();
                        }
                        catch (DbUpdateException)
                        {
                            return Content("Fail");
                        }
                    }
                }

                //Cập nhật đơn hàng
                productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                productInvoice.ModifiedDate = DateTime.Now;
                productInvoice.IsArchive = true;
                productInvoice.Status = Wording.OrderStatus_complete;
                productInvoiceRepository.UpdateProductInvoice(productInvoice);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ArchiveSuccess;
                //Tạo chiết khấu cho nhân viên nếu có
                //CommisionStaffController.CreateCommission(productInvoice.Id);
                //tính điểm tích lũy hóa đơn cho khách hàng
                //Erp.BackOffice.Sale.Controllers.LogLoyaltyPointController.CreateLogLoyaltyPoint(productInvoice.CustomerId, productInvoice.TotalAmount, productInvoice.Id);
                //Cảnh báo cập nhật phiếu xuất kho
                if (hasProductOutbound)
                {
                    TempData[Globals.SuccessMessageKey] += "<br/>Đơn hàng này đã được xuất kho! Vui lòng kiểm tra lại chứng từ xuất kho để tránh sai xót dữ liệu!";
                }
            }

            return RedirectToAction("Detail", new { Id = model.Id });
        }
        #endregion

        #region UnArchive
        [HttpPost]
        public ActionResult UnArchive(int Id, bool IsPopup)
        {
            if (Request["Submit"] == "Save")
            {
                var productInvoice = productInvoiceRepository.GetProductInvoiceById(Id);

                //Kiểm tra phân quyền Chi nhánh
                if (!(Filters.SecurityFilter.IsAdmin() || ("," + Helpers.Common.CurrentUser.BranchId + ",").Contains("," + productInvoice.BranchId + ",") == true))
                {
                    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                }

                //Kiểm tra cho phép sửa chứng từ này hay không
                if (Helpers.Common.KiemTraNgaySuaChungTu(productInvoice.CreatedDate.Value) == false)
                {
                    TempData[Globals.FailedMessageKey] = "Quá hạn sửa chứng từ!";
                }
                else
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {
                            #region isDelete receipt
                            var receipt = ReceiptRepository.GetAllReceipts()
                                .Where(item => item.MaChungTuGoc == productInvoice.Code).ToList();
                            var receipt_detail = receiptDetailRepository.GetAllReceiptDetail().Where(x => x.MaChungTuGoc == productInvoice.Code).ToList();
                            if (receipt_detail.Count() > 0)
                            {
                                // isDelete chi tiết phiếu thu
                                for (int i = 0; i < receipt_detail.Count(); i++)
                                {
                                    receiptDetailRepository.DeleteReceiptDetailRs(receipt_detail[i].Id);
                                }
                            }
                            if (receipt.Count() > 0)
                            {
                                // isDelete phiếu thu
                                for (int i = 0; i < receipt.Count(); i++)
                                {
                                    ReceiptRepository.DeleteReceiptRs(receipt[i].Id);
                                }
                            }
                            #endregion

                            #region isDelete listTransaction
                            //isDelete lịch sử giao dịch
                            var listTransaction = transactionLiabilitiesRepository.GetAllTransaction()
                            .Where(item => item.MaChungTuGoc == productInvoice.Code)
                            .Select(item => item.Id)
                            .ToList();

                            foreach (var item in listTransaction)
                            {
                                transactionLiabilitiesRepository.DeleteTransactionRs(item);
                            }
                            #endregion

                            #region bỏ ghi sổ ProductOutbound
                            var productOutbound = productOutboundRepository.GetAllProductOutbound().Where(x => x.InvoiceId == productInvoice.Id).ToList();
                            foreach (var item in productOutbound)
                            {
                                //Update các lô/date đã xuất = false
                                var listWarehouseLocationItem = warehouseLocationItemRepository.GetAllWarehouseLocationItem()
                                    .Where(x => x.ProductOutboundId == item.Id).ToList();
                                foreach (var items in listWarehouseLocationItem)
                                {
                                    items.IsOut = false;
                                    items.ProductOutboundId = null;
                                    items.ProductOutboundDetailId = null;
                                    items.ModifiedUserId = WebSecurity.CurrentUserId;
                                    items.ModifiedDate = DateTime.Now;
                                    warehouseLocationItemRepository.UpdateWarehouseLocationItem(items);
                                }
                                string check = "";
                                var detail = productOutboundRepository.GetAllvwProductOutboundDetailByOutboundId(item.Id).ToList();
                                foreach (var item2 in detail)
                                {
                                    var error = InventoryController.Check(item2.ProductName, item2.ProductId.Value, item2.LoCode, item2.ExpiryDate, item.WarehouseSourceId.Value, item2.Quantity.Value, 0);
                                    check += error;
                                }
                                if (string.IsNullOrEmpty(check))
                                {
                                    //Khi đã hợp lệ thì mới update
                                    foreach (var i in detail)
                                    {
                                        InventoryController.Update(i.ProductName, i.ProductId.Value, i.LoCode, i.ExpiryDate, item.WarehouseSourceId.Value, i.Quantity.Value, 0);
                                    }

                                    item.IsArchive = false;
                                    productOutboundRepository.UpdateProductOutbound(item);
                                    TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
                                }
                                else
                                {
                                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
                                }
                                //productOutboundRepository.DeleteProductOutboundRs(item.Id);

                            }
                            #endregion

                            productInvoice.PaidAmount = 0;
                            productInvoice.RemainingAmount = productInvoice.TotalAmount;
                            productInvoice.NextPaymentDate = null;

                            productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                            productInvoice.ModifiedDate = DateTime.Now;
                            productInvoice.IsArchive = false;
                            productInvoice.Status = Wording.OrderStatus_inprogress;
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            TempData[Globals.SuccessMessageKey] = "Đã bỏ ghi sổ";
                            //cap nhat chiet khau nha thuoc
                            //Erp.BackOffice.Sale.Controllers.TotalDiscountMoneyNTController.SyncTotalDisCountMoneyNT(productInvoice, WebSecurity.CurrentUserId);
                            //cap nhat hoa hong nhan vien
                            //Erp.BackOffice.Staff.Controllers.HistoryCommissionStaffController.SyncCommissionStaff(productInvoice, WebSecurity.CurrentUserId);
                            scope.Complete();
                        }
                        catch (DbUpdateException)
                        {
                            return Content("Fail");
                        }
                    }
                }
            }

            return RedirectToAction("Detail", new { Id = Id, IsPopup = IsPopup });
        }
        #endregion



        public ActionResult SearchProductControl(string Search, int pageNumber = 1, int pageSize = 9)
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
            if (Search == null)
            { Search = ""; }
            var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "0", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = intBrandID, LoCode = "", ProductId = "", ExpiryDate = "" }).ToList();
            productList = productList.Where(x => x.IsSale != null && x.IsSale == true && (Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.ProductName).Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Search)) || Search.Trim() == "")).OrderByDescending(x => x.ModifiedDate).ToList();

            foreach (var item in productList)
            {
                item.Image_Name = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(item.Image_Pos, "product-image-folder", "product");
            }

            var pagedData = Pagination.PagedResult(productList, pageNumber, pageSize);

            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }

        string bill(int Id)
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
            var ImgLogo = "<div class=\"logo\"><img src=http://ngochuong.osales.vn/" + logo + " height=\"73\" /></div>";
            //lấy hóa đơn.
            var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id);
            //lấy thông tin khách hàng
            var customer = customerRepository.GetvwCustomerByCode(productInvoice.CustomerCode);

            List<ProductInvoiceDetailViewModel> detailList = new List<ProductInvoiceDetailViewModel>();
            if (productInvoice != null && productInvoice.IsDeleted != true)
            {
                //lấy danh sách sản phẩm xuất kho
                detailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(Id)
                        .Select(x => new ProductInvoiceDetailViewModel
                        {
                            Id = x.Id,
                            Price = x.Price,
                            ProductId = x.ProductId,
                            ProductName = x.ProductName,
                            ProductCode = x.ProductCode,
                            Quantity = x.Quantity,
                            Unit = x.Unit,
                            ProductGroup = x.ProductGroup,
                            CheckPromotion = x.CheckPromotion,
                            ProductType = x.ProductType,
                            CategoryCode = x.CategoryCode
                        }).ToList();
            }
            //lấy template phiếu xuất.
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ProductInvoice")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //truyền dữ liệu vào template.
            model.Content = template.Content;
            model.Content = model.Content.Replace("{Code}", productInvoice.Code);
            model.Content = model.Content.Replace("{CreateDate}", productInvoice.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"));
            model.Content = model.Content.Replace("{Hours}", productInvoice.CreatedDate.Value.ToString("HH:mm"));
            model.Content = model.Content.Replace("{CustomerName}", customer.LastName + " " + customer.FirstName);
            model.Content = model.Content.Replace("{CustomerPhone}", customer.Phone);
            model.Content = model.Content.Replace("{CompanyName}", customer.CompanyName);
            model.Content = model.Content.Replace("{StaffCreateName}", productInvoice.SalerFullName);
            if (!string.IsNullOrEmpty(customer.Address))
            {
                model.Content = model.Content.Replace("{Address}", customer.Address + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Address}", "");
            }
            if (!string.IsNullOrEmpty(customer.DistrictName))
            {
                model.Content = model.Content.Replace("{District}", customer.DistrictName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{District}", "");
            }
            if (!string.IsNullOrEmpty(customer.WardName))
            {
                model.Content = model.Content.Replace("{Ward}", customer.WardName + ", ");
            }
            else
            {
                model.Content = model.Content.Replace("{Ward}", "");
            }
            if (!string.IsNullOrEmpty(customer.ProvinceName))
            {
                model.Content = model.Content.Replace("{Province}", customer.ProvinceName);
            }
            else
            {
                model.Content = model.Content.Replace("{Province}", "");
            }

            model.Content = model.Content.Replace("{Note}", productInvoice.Note);
            if (!string.IsNullOrEmpty(productInvoice.CodeInvoiceRed))
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code + " - " + productInvoice.CodeInvoiceRed);
            }
            else
            {
                model.Content = model.Content.Replace("{InvoiceCode}", productInvoice.Code);
            }
            model.Content = model.Content.Replace("{PaymentMethod}", productInvoice.PaymentMethod);
            model.Content = model.Content.Replace("{MoneyText}", Erp.BackOffice.Helpers.Common.ChuyenSoThanhChu_2(Convert.ToInt32(productInvoice.TotalAmount)));

            model.Content = model.Content.Replace("{DataTable}", buildHtmlDetailList(detailList, productInvoice));
            model.Content = model.Content.Replace("{System.Logo}", ImgLogo);
            model.Content = model.Content.Replace("{System.CompanyName}", company);
            model.Content = model.Content.Replace("{System.AddressCompany}", address);
            model.Content = model.Content.Replace("{System.PhoneCompany}", phone);
            model.Content = model.Content.Replace("{System.Fax}", fax);
            model.Content = model.Content.Replace("{System.BankCodeCompany}", bankcode);
            model.Content = model.Content.Replace("{System.BankNameCompany}", bankname);
            //model.Content = model.Content.Replace("{Reminder}", NoteReminder);
            model.Content = model.Content.Replace("{PrintDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            //Tạo barcode
            Image barcode = Code128Rendering.MakeBarcodeImage(productInvoice.Code, 1, true);
            model.Content = model.Content.Replace("{BarcodeImgSource}", ImageToBase64(barcode, System.Drawing.Imaging.ImageFormat.Png));

            return model.Content;
        }


        //#region CreateFromSchedule

        //public ActionResult CreateFromSchedule()
        //{

        //    ProductInvoiceViewModel model = new ProductInvoiceViewModel();
        //    model.DetailList = new List<ProductInvoiceDetailViewModel>();
        //    var service_schedule = Request["service_schedule"];
        //    if (!string.IsNullOrEmpty(service_schedule))
        //    {
        //        string[] list = service_schedule.Split(',');
        //        List<vwServiceSchedule> scheduleList = new List<vwServiceSchedule>();
        //        int n = 0;
        //        for (int i = 0; i < list.Count(); i++)
        //        {
        //            if (list[i] != "")
        //            {
        //                var schedule = serviceScheduleRepository.GetvwServiceScheduleById(int.Parse(list[i], CultureInfo.InvariantCulture));
        //                model.CustomerId = schedule.CustomerId;
        //                model.CustomerName = schedule.CustomerName;
        //                var nhan_vien_phuc_vu = taskRepository.GetAllvwTask().Where(x => x.ParentType == "ServiceSchedule" && x.ParentId == schedule.Id).OrderByDescending(x => x.CreatedDate).ToList();
        //                var staff_user = 0;
        //                var staff_name = "";
        //                if (nhan_vien_phuc_vu.Count > 0)
        //                {
        //                    staff_user = nhan_vien_phuc_vu.FirstOrDefault().AssignedUserId.Value;
        //                    var staff = staffRepository.GetvwAllStaffs().Where(x => x.UserId.Value == staff_user).ToList();
        //                    if (staff.Count() > 0)
        //                    {
        //                        staff_user = staff.FirstOrDefault().Id;
        //                        staff_name = staff.FirstOrDefault().Name;
        //                    }

        //                }
        //                var product = ProductRepository.GetProductById(schedule.ServiceId.Value);
        //                model.DetailList.Add(new ProductInvoiceDetailViewModel
        //                {
        //                    OrderNo = n,
        //                    ProductId = product.Id,
        //                    Quantity = 1,
        //                    Unit = product.Unit,
        //                    Price = product.PriceOutbound,
        //                    ProductCode = product.Code,
        //                    ProductName = product.Name,
        //                    IsCombo = product.IsCombo,
        //                    ProductType = product.Type,
        //                    StaffId = staff_user,
        //                    SalerName = staff_name,
        //                    CategoryCode = product.CategoryCode
        //                });
        //                n++;
        //            }
        //        }
        //    }
        //    model.ReceiptViewModel = new ReceiptViewModel();
        //    model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
        //    model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
        //    model.ReceiptViewModel.Amount = 0;

        //    var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");
        //    string image_folder_product = Helpers.Common.GetSetting("product-image-folder");
        //    string image_folder_service = Helpers.Common.GetSetting("service-image-folder");

        //    var branchId = Helpers.Common.CurrentUser.BranchId.Value;
        //    var productList = inventoryRepository.GetAllvwInventoryByBranchId(branchId).Where(x => x.Quantity > 0)
        //       .Select(item => new ProductViewModel
        //       {
        //           Type = "product",
        //           Code = item.ProductCode,
        //           Name = item.ProductName,
        //           Id = item.ProductId,
        //           CategoryCode = string.IsNullOrEmpty(item.CategoryCode) ? "Sản phẩm khác" : item.CategoryCode,
        //           PriceOutbound = item.ProductPriceOutbound,
        //           Unit = item.ProductUnit,
        //           QuantityTotalInventory = item.Quantity
        //       }).OrderBy(item => item.CategoryCode).ToList();

        //    var serviceList = ProductRepository.GetAllvwProductAndService()
        //        .Where(item => item.Type == "service")
        //        .Select(item => new ProductViewModel
        //        {
        //            Type = item.Type,
        //            Code = item.Code,
        //            Name = item.Name,
        //            Id = item.Id,
        //            CategoryCode = string.IsNullOrEmpty(item.CategoryCode) ? "Dịch vụ khác" : item.CategoryCode,
        //            PriceOutbound = item.PriceOutbound,
        //            Unit = item.Unit,
        //            QuantityTotalInventory = 0,
        //            Image_Name = item.Image_Name,
        //            IsCombo = item.IsCombo
        //        }).OrderBy(item => item.CategoryCode).ToList();

        //    var unionList = productList.Union(serviceList).OrderBy(item => item.Type).ThenBy(item => item.CategoryCode).ThenBy(item => item.Code);
        //    ViewBag.productList = unionList;

        //    var jsonProductInvoiceItem = unionList.Select(item => new ProductInvoiceItemViewModel
        //    {
        //        Id = item.Id,
        //        Type = item.Type,
        //        Code = item.Code,
        //        Name = item.Name.Replace("\"", "\\\""),
        //        CategoryCode = item.CategoryCode,
        //        PriceOutbound = item.PriceOutbound,
        //        Unit = item.Unit,
        //        InventoryQuantity = item.QuantityTotalInventory.Value,
        //        Image_Name = item.Image_Name,
        //        IsCombo = item.IsCombo
        //    }).ToList();

        //    ViewBag.json = JsonConvert.SerializeObject(jsonProductInvoiceItem);

        //    List<SelectListItem> categoryList = new List<SelectListItem>();
        //    categoryList.Add(new SelectListItem { Text = "TẤT CẢ", Value = "0" });
        //    categoryList.Add(new SelectListItem { Text = "I. SẢN PHẨM", Value = "1" });
        //    foreach (var item in productList.GroupBy(i => i.CategoryCode).Select(i => new { Name = i.Key, NumberOfItem = i.Count() }))
        //    {
        //        categoryList.Add(new SelectListItem { Text = "---- " + item.Name + " (" + item.NumberOfItem + ")", Value = item.Name });
        //    }
        //    categoryList.Add(new SelectListItem { Text = "II. DỊCH VỤ", Value = "2" });
        //    foreach (var item in serviceList.GroupBy(i => i.CategoryCode).Select(i => new { Name = i.Key, NumberOfItem = i.Count() }))
        //    {
        //        categoryList.Add(new SelectListItem { Text = "---- " + item.Name + " (" + item.NumberOfItem + ")", Value = item.Name });
        //    }

        //    ViewBag.CategoryList = categoryList;

        //    model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;

        //    int taxfee = 0;
        //    int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
        //    model.TaxFee = taxfee;

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult CreateFromSchedule(ProductInvoiceViewModel model)
        //{
        //    if (ModelState.IsValid && model.DetailList.Count != 0)
        //    {

        //        ProductInvoice productInvoice = new ProductInvoice();
        //        AutoMapper.Mapper.Map(model, productInvoice);
        //        productInvoice.IsDeleted = false;
        //        productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
        //        productInvoice.CreatedDate = DateTime.Now;
        //        productInvoice.Status = Wording.OrderStatus_pending;
        //        productInvoice.BranchId = Helpers.Common.CurrentUser.BranchId.Value;
        //        productInvoice.IsArchive = false;
        //        productInvoice.IsReturn = false;
        //        //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
        //        List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
        //        foreach (var group in model.DetailList)
        //        {
        //            var product = ProductRepository.GetProductById(group.ProductId.Value);
        //            listNewCheckSameId.Add(new ProductInvoiceDetail
        //            {
        //                ProductId = product.Id,
        //                ProductType = product.Type,
        //                Quantity = group.Quantity,
        //                Unit = product.Unit,
        //                Price = group.Price,
        //                PromotionDetailId = group.PromotionDetailId,
        //                PromotionId = group.PromotionId,
        //                PromotionValue = group.PromotionValue,
        //                IsDeleted = false,
        //                CreatedUserId = WebSecurity.CurrentUserId,
        //                ModifiedUserId = WebSecurity.CurrentUserId,
        //                CreatedDate = DateTime.Now,
        //                ModifiedDate = DateTime.Now,
        //                FixedDiscount = group.FixedDiscount,
        //                IrregularDiscount = group.IrregularDiscount,
        //                CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false),
        //                IsReturn = false
        //            });
        //        }

        //        //Tính lại chiết khấu
        //        foreach (var item in listNewCheckSameId)
        //        {
        //            var thanh_tien = item.Quantity * item.Price;
        //            item.FixedDiscountAmount = Convert.ToInt32(item.FixedDiscount * thanh_tien / 100);
        //            item.IrregularDiscountAmount = Convert.ToInt32(item.IrregularDiscount * thanh_tien / 100);
        //            //tổng Point

        //        }
        //        var total = listNewCheckSameId.Sum(item => item.Quantity.Value * item.Price.Value - item.FixedDiscountAmount.Value-item.IrregularDiscountAmount.Value);
        //        productInvoice.TotalAmount = total + (total * Convert.ToInt32(Erp.BackOffice.Helpers.Common.GetSetting("VAT")));

        //        productInvoice.IsReturn = false;
        //        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
        //        productInvoice.ModifiedDate = DateTime.Now;
        //        productInvoice.PaidAmount = 0;
        //        productInvoice.RemainingAmount = productInvoice.TotalAmount;

        //        //hàm thêm mới
        //        productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

        //        //cập nhật lại mã hóa đơn
        //        string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
        //        productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
        //        productInvoiceRepository.UpdateProductInvoice(productInvoice);

        //        //Kiểm tra xem KH có mua DV nào hay không, nếu có thì tạo dữ liệu cho bảng UsingService, ServiceReminder
        //        var listService = listNewCheckSameId.Where(x => x.ProductType == "service").ToList();
        //        if (listService.Count != 0)
        //        {
        //            foreach (var item in listService)
        //            {
        //                CreateUsingLogAndReminder(item, model.DetailList, productInvoice.CreatedDate);
        //            }
        //        }

        //        //Thêm vào quản lý chứng từ
        //        TransactionController.Create(new TransactionViewModel
        //        {
        //            TransactionModule = "ProductInvoice",
        //            TransactionCode = productInvoice.Code,
        //            TransactionName = "Bán hàng"
        //        });
        //        //Tạo phiếu nhập, nếu là tự động
        //        string sale_tu_dong_tao_chung_tu = Erp.BackOffice.Helpers.Common.GetSetting("sale_tu_dong_tao_chung_tu");
        //        if (sale_tu_dong_tao_chung_tu == "true")
        //        {
        //            ProductOutboundViewModel productOutboundViewModel = new ProductOutboundViewModel();
        //            var warehouseDefault = warehouseRepository.GetAllWarehouse().Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId).FirstOrDefault();

        //            //Nếu trong đơn hàng có sản phẩm thì xuất kho
        //            if (model.DetailList.Any(item => item.ProductType == "product") && warehouseDefault != null)
        //            {
        //                productOutboundViewModel.InvoiceId = productInvoice.Id;
        //                productOutboundViewModel.InvoiceCode = productInvoice.Code;
        //                productOutboundViewModel.WarehouseSourceId = warehouseDefault.Id;
        //                productOutboundViewModel.Note = "Xuất kho cho đơn hàng " + productInvoice.Code;

        //                //Lấy dữ liệu cho chi tiết
        //                productOutboundViewModel.DetailList = new List<ProductOutboundDetailViewModel>();
        //                AutoMapper.Mapper.Map(model.DetailList, productOutboundViewModel.DetailList);

        //                var productOutbound = ProductOutboundController.CreateFromInvoice(productOutboundRepository, productOutboundViewModel, productInvoice.Code);

        //                //Ghi sổ chứng từ phiếu xuất
        //                ProductOutboundController.Archive(productOutboundRepository, warehouseLocationItemRepository, productOutbound, TempData);
        //            }

        //            //Ghi sổ chứng từ bán hàng
        //            model.Id = productInvoice.Id;
        //            Archive(model);
        //        }

        //        //Run process
        //        //var dataSource = null;
        //        //Crm.Controllers.ProcessController.Run("ProductInvoice", "Create", null, null, null, dataSource);

        //        //send email
        //        //var ii = productInvoiceRepository.GetvwProductInvoiceById(model.Id);
        //        //Erp.BackOffice.Helpers.Common.SendEmail(ii.Email, ii.BranchName, bill(ii.Id));
        //        ////lưu lịch sử email
        //        //Erp.BackOffice.Crm.Controllers.EmailLogController.SaveEmail(ii.Email, bill(ii.Id), ii.CustomerId, ii.Id, "ProductInvoice", ii.BranchName);
        //        ////send sms
        //        //var body = Erp.BackOffice.Helpers.Common.GetSetting("SMSSetting_Body") + "-" + ii.Code + "-" + ii.TotalAmount;
        //        //Erp.BackOffice.Helpers.Common.SendSMS(ii.Phone, body);
        //        ////lưu lịch sử sms
        //        //Erp.BackOffice.Crm.Controllers.SMSLogController.SaveSMS(ii.Phone, body, ii.CustomerId, ii.Id, "ProductInvoice", ii.BranchName);

        //        //var prID = productInvoiceRepository.GetvwProductInvoiceById(model.Id);
        //        // //lưu điểm vào Customer
        //        //var point = productInvoiceRepository.GetvwProductInvoiceDetailById(productInvoice.Id);
        //        //Erp.BackOffice.Account.Controllers.CustomerController.SavePoint(prID.CustomerId, item.P);
        //        return RedirectToAction("Detail", new { Id = productInvoice.Id });
        //    }

        //    return View();
        //}
        //#endregion

        #region Edit

        public ActionResult Edit(int? Id)
        {
            ProductInvoiceViewModel model = new ProductInvoiceViewModel();
            var invoice = productInvoiceRepository.GetProductInvoiceById(Id.Value);
            if (invoice != null && invoice.IsDeleted != true)
            {
                AutoMapper.Mapper.Map(invoice, model);
                var Note = NoteProductInvoiceRepository.GetAllNoteProductInvoice().Where(x => x.ProductInvoiceId == Id).OrderByDescending(x => x.CreatedDate).ToList();
                ViewBag.Note = Note;
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ProductInvoiceViewModel model)
        {
            if (Request["Submit"] == "Save")
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        var invoice = productInvoiceRepository.GetProductInvoiceById(model.Id);

                        invoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        invoice.ModifiedDate = DateTime.Now;
                        invoice.Note = model.Note;
                        invoice.CodeInvoiceRed = model.CodeInvoiceRed;

                        productInvoiceRepository.UpdateProductInvoice(invoice);

                        var Note = new NoteProductInvoice();
                        Note.ProductInvoiceId = invoice.Id;
                        Note.Note = invoice.Note;
                        Note.CreatedDate = invoice.ModifiedDate;
                        Note.CreatedUserId = invoice.ModifiedUserId;
                        NoteProductInvoiceRepository.InsertNoteProductInvoice(Note);
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion

        private bool ghiso(int IdDonhang, int? intBrandID, string Hinhthuctt)
        {
            Hinhthuctt = "";
            try
            {

                var productInvoice = productInvoiceRepository.GetProductInvoiceById(IdDonhang);
                var invoice_detail_list = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();
                ProductOutboundViewModel productOutboundViewModel = new ProductOutboundViewModel();
                var warehouseDefault = warehouseRepository.GetAllWarehouse().Where(x => x.IsSale == true).Where(x => x.BranchId == intBrandID).FirstOrDefault();
                //model.ReceiptViewModel = new ReceiptViewModel();
                //model.NextPaymentDate_Temp = DateTime.Now;
                //model.ReceiptViewModel.Amount = productInvoice.TotalAmount;
                //model.ReceiptViewModel.Name = string.Empty;
                //model.ReceiptViewModel.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;

                if (warehouseDefault == null)
                {
                    TempData[Globals.FailedMessageKey] += "<br/>Không tìm thấy kho xuất bán! Vui lòng kiểm tra lại!";
                    return false;
                }
                string check = "";
                foreach (var item in invoice_detail_list)
                {
                    var error = InventoryController.Check(item.ProductName, item.ProductId.Value, item.LoCode, item.ExpiryDate, warehouseDefault.Id, 0, item.Quantity.Value);
                    check += error;
                }
                if (!string.IsNullOrEmpty(check))
                {
                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
                    return false;
                }
                #region  phiếu xuất ok
                var product_outbound = productOutboundRepository.GetAllProductOutboundFull().Where(x => x.InvoiceId == productInvoice.Id).ToList();

                //insert mới
                if (product_outbound.Count() <= 0)
                {
                    #region  thêm mới phiếu xuất

                    //Nếu trong đơn hàng có sản phẩm thì xuất kho
                    if (warehouseDefault != null)
                    {
                        productOutboundViewModel.InvoiceId = productInvoice.Id;
                        productOutboundViewModel.InvoiceCode = productInvoice.Code;
                        productOutboundViewModel.WarehouseSourceId = warehouseDefault.Id;
                        productOutboundViewModel.Note = "Xuất kho cho đơn hàng " + productInvoice.Code;
                        var DetailList = invoice_detail_list.Select(x =>
                              new ProductInvoiceDetailViewModel
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
                                  IrregularDiscount = x.IrregularDiscount,
                                  IrregularDiscountAmount = x.IrregularDiscountAmount,
                                  CategoryCode = x.CategoryCode,
                                  ProductInvoiceCode = x.ProductInvoiceCode,
                                  ProductName = x.ProductName,
                                  ProductCode = x.ProductCode,
                                  ProductInvoiceId = x.ProductInvoiceId,
                                  ProductGroup = x.ProductGroup,
                                  CheckPromotion = x.CheckPromotion,
                                  IsReturn = x.IsReturn,
                                  //Status = x.Status
                                  LoCode = x.LoCode,
                                  ExpiryDate = x.ExpiryDate,
                                  Amount = x.Amount
                              }).ToList();
                        //Lấy dữ liệu cho chi tiết
                        productOutboundViewModel.DetailList = new List<ProductOutboundDetailViewModel>();
                        AutoMapper.Mapper.Map(DetailList, productOutboundViewModel.DetailList);

                        var productOutbound = ProductOutboundController.AutoCreateProductOutboundFromProductInvoice(productOutboundRepository, productOutboundViewModel, productInvoice.Code);
                        //ProductOutboundController.Archive_mobile(productOutbound, model.CreatedUserId.GetValueOrDefault());
                        //Ghi sổ chứng từ phiếu xuất
                        ProductOutboundController.Archive(productOutbound, TempData);
                    }
                    #endregion
                }
                else
                {
                    #region   cập nhật phiếu xuất kho
                    //xóa chi tiết phiếu xuất, insert chi tiết mới
                    //cập nhật lại tổng tiền, trạng thái phiếu xuất
                    for (int i = 0; i < product_outbound.Count(); i++)
                    {
                        var outbound_detail = productOutboundRepository.GetAllProductOutboundDetailByOutboundId(product_outbound[i].Id).ToList();
                        //xóa
                        for (int ii = 0; ii < outbound_detail.Count(); ii++)
                        {
                            productOutboundRepository.DeleteProductOutboundDetail(outbound_detail[ii].Id);
                        }
                        //insert mới
                        for (int iii = 0; iii < invoice_detail_list.Count(); iii++)
                        {
                            ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                            productOutboundDetail.Price = invoice_detail_list[iii].Price;
                            productOutboundDetail.ProductId = invoice_detail_list[iii].ProductId;
                            productOutboundDetail.Quantity = invoice_detail_list[iii].Quantity;
                            productOutboundDetail.Unit = invoice_detail_list[iii].Unit;
                            productOutboundDetail.LoCode = invoice_detail_list[iii].LoCode;
                            productOutboundDetail.ExpiryDate = invoice_detail_list[iii].ExpiryDate;
                            productOutboundDetail.IsDeleted = false;
                            productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                            productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                            productOutboundDetail.CreatedDate = DateTime.Now;
                            productOutboundDetail.ModifiedDate = DateTime.Now;
                            productOutboundDetail.ProductOutboundId = product_outbound[i].Id;
                            productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);

                        }
                        product_outbound[i].TotalAmount = invoice_detail_list.Sum(x => (x.Price * x.Quantity));

                        //Ghi sổ chứng từ phiếu xuất
                        ProductOutboundController.Archive(product_outbound[i], TempData);
                    }
                    #endregion
                }
                #endregion

                #region  xóa hết lịch sử giao dịch
                //xóa lịch sử giao dịch có liên quan đến đơn hàng, gồm: 1 dòng giao dịch bán hàng, 1 dòng thu tiền.
                var transaction_Liablities = transactionLiabilitiesRepository.GetAllTransaction().Where(x => x.MaChungTuGoc == productInvoice.Code && x.LoaiChungTuGoc == "ProductInvoice").ToList();
                if (transaction_Liablities.Count() > 0)
                {
                    for (int i = 0; i < transaction_Liablities.Count(); i++)
                    {
                        transactionLiabilitiesRepository.DeleteTransaction(transaction_Liablities[i].Id);
                    }
                }
                #endregion

                if (!productInvoice.IsArchive)
                {
                    #region  Cập nhật thông tin thanh toán cho đơn hàng
                    //Cập nhật thông tin thanh toán cho đơn hàng

                    productInvoice.PaymentMethod = "Tiền mặt";
                    if (Hinhthuctt != "" && Hinhthuctt.Length > 0)
                    {
                        if (Hinhthuctt == "qt")
                        {
                            productInvoice.PaymentMethod = "Quẹt Thẻ";
                        }
                        if (Hinhthuctt == "ck")
                        {
                            productInvoice.PaymentMethod = "Chuyển Khoản";
                        }
                        if (Hinhthuctt == "tm")
                        {
                            productInvoice.PaymentMethod = "Tiền Mặt";
                        }

                    }
                    productInvoice.PaidAmount = Convert.ToDecimal(productInvoice.TotalAmount);
                    productInvoice.RemainingAmount = productInvoice.TotalAmount - productInvoice.PaidAmount;
                    productInvoice.NextPaymentDate = null;

                    productInvoice.ModifiedDate = DateTime.Now;
                    productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                    productInvoice.BranchId = intBrandID.Value;



                    productInvoiceRepository.UpdateProductInvoice(productInvoice);

                    //Lấy mã KH
                    var customer = customerRepository.GetCustomerById(productInvoice.CustomerId.Value);
                    //neu la khach vang lai
                    if (customer == null)
                    {
                        customer = customerRepository.GetCustomerByCode("KHACHVANGLAI");
                    }

                    var remain = productInvoice.TotalAmount - Convert.ToDecimal(productInvoice.TotalAmount);
                    if (remain > 0)
                    {
                    }
                    else
                    {
                        productInvoice.NextPaymentDate = null;
                    }
                    #endregion

                    #region thêm lịch sử bán hàng
                    //Ghi Nợ TK 131 - Phải thu của khách hàng (tổng giá thanh toán)
                    if (customer != null)
                    {
                        Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                productInvoice.Code,
                                "ProductInvoice",
                                "Bán hàng",
                                customer.Code,
                                "Customer",
                                productInvoice.TotalAmount,
                                0,
                                productInvoice.Code,
                                "ProductInvoice",
                                null,
                                null,
                                null);
                    }
                    #endregion

                    #region phiếu thu
                    //Khách thanh toán ngay
                    if (productInvoice.TotalAmount > 0)
                    {
                        #region xóa phiếu thu cũ (nếu có)
                        var receipt = ReceiptRepository.GetAllReceiptFull().Where(x => x.BranchId == intBrandID || intBrandID == 0)
                            .Where(item => item.MaChungTuGoc == productInvoice.Code).ToList();
                        var receipt_detail = receiptDetailRepository.GetAllReceiptDetailFull().ToList();
                        receipt_detail = receipt_detail.Where(x => x.MaChungTuGoc == productInvoice.Code).ToList();
                        if (receipt_detail.Count() > 0)
                        {
                            // isDelete chi tiết phiếu thu
                            for (int i = 0; i < receipt_detail.Count(); i++)
                            {
                                receiptDetailRepository.DeleteReceiptDetail(receipt_detail[i].Id);
                            }
                        }
                        #endregion
                        if (receipt.Count() > 0)
                        {
                            #region cập nhật phiếu thu cũ
                            // isDelete phiếu thu
                            var receipts = receipt.FirstOrDefault();
                            receipts.IsDeleted = false;
                            receipts.Payer = customer.LastName + " " + customer.FirstName;
                            receipts.PaymentMethod = productInvoice.PaymentMethod;
                            receipts.ModifiedDate = DateTime.Now;
                            receipts.VoucherDate = DateTime.Now;
                            receipts.Amount = productInvoice.TotalAmount;
                            receipts.BranchId = intBrandID.Value;
                            if (receipts.Amount > productInvoice.TotalAmount)
                                receipts.Amount = productInvoice.TotalAmount;

                            ReceiptRepository.UpdateReceipt(receipts);

                            //Thêm vào quản lý chứng từ
                            //TransactionController.Create(new TransactionViewModel
                            //{
                            //    TransactionModule = "Receipt",
                            //    TransactionCode = receipts.Code,
                            //    TransactionName = "Thu tiền khách hàng"
                            //});

                            //Thêm chứng từ liên quan
                            //TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                            //{
                            //    TransactionA = receipts.Code,
                            //    TransactionB = productInvoice.Code
                            //});

                            //Ghi Có TK 131 - Phải thu của khách hàng.
                            Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                receipts.Code,
                                "Receipt",
                                "Thu tiền khách hàng",
                                customer.Code,
                                "Customer",
                                0,
                                Convert.ToDecimal(productInvoice.TotalAmount),
                                productInvoice.Code,
                                "ProductInvoice",
                                "Tiền mặt",
                                null,
                                null);
                            #endregion
                        }
                        else
                        {
                            #region thêm mới phiếu thu
                            //Lập phiếu thu
                            var receipt_inser = new Receipt();
                            receipt_inser.IsDeleted = false;
                            receipt_inser.CreatedUserId = WebSecurity.CurrentUserId;
                            receipt_inser.ModifiedUserId = WebSecurity.CurrentUserId;
                            receipt_inser.AssignedUserId = WebSecurity.CurrentUserId;
                            receipt_inser.CreatedDate = DateTime.Now;
                            receipt_inser.ModifiedDate = DateTime.Now;
                            receipt_inser.VoucherDate = DateTime.Now;
                            receipt_inser.CustomerId = customer.Id;
                            receipt_inser.Payer = customer.LastName + " " + customer.FirstName;
                            receipt_inser.PaymentMethod = productInvoice.PaymentMethod;
                            receipt_inser.Address = customer.Address;
                            receipt_inser.MaChungTuGoc = productInvoice.Code;
                            receipt_inser.LoaiChungTuGoc = "ProductInvoice";
                            receipt_inser.Note = receipt_inser.Name;
                            receipt_inser.BranchId = intBrandID.Value;


                            receipt_inser.Amount = productInvoice.TotalAmount;

                            ReceiptRepository.InsertReceipt(receipt_inser);

                            var prefixReceipt = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                            receipt_inser.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixReceipt, receipt_inser.Id);
                            ReceiptRepository.UpdateReceipt(receipt_inser);
                            ////Thêm vào quản lý chứng từ
                            //TransactionController.Create(new TransactionViewModel
                            //{
                            //    TransactionModule = "Receipt",
                            //    TransactionCode = receipt_inser.Code,
                            //    TransactionName = "Thu tiền khách hàng"
                            //});

                            //Thêm chứng từ liên quan
                            //TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                            //{
                            //    TransactionA = receipt_inser.Code,
                            //    TransactionB = productInvoice.Code
                            //});

                            //Ghi Có TK 131 - Phải thu của khách hàng.
                            Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                receipt_inser.Code,
                                "Receipt",
                                "Thu tiền khách hàng",
                                customer.Code,
                                "Customer",
                                0,
                                Convert.ToDecimal(productInvoice.TotalAmount),
                                productInvoice.Code,
                                "ProductInvoice",
                                "Tiền mặt",
                                null,
                                null);

                            #endregion
                        }
                    }
                    #endregion

                    #region cập nhật đơn bán hàng
                    //Cập nhật đơn hàng
                    productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                    productInvoice.ModifiedDate = DateTime.Now;
                    productInvoice.IsArchive = true;
                    productInvoice.BranchId = intBrandID.Value;
                    productInvoice.Status = Wording.OrderStatus_complete;
                    productInvoiceRepository.UpdateProductInvoice(productInvoice);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ArchiveSuccess;

                    TempData[Globals.SuccessMessageKey] += "<br/>Đơn hàng này đã được xuất kho! Vui lòng kiểm tra lại chứng từ xuất kho để tránh sai xót dữ liệu!";
                    #endregion
                    //cap nhat chiet khau nha thuoc
                    //Erp.BackOffice.Sale.Controllers.TotalDiscountMoneyNTController.SyncTotalDisCountMoneyNT(productInvoice, WebSecurity.CurrentUserId);
                    //cap nhat hoa hong nhan vien
                    //Erp.BackOffice.Staff.Controllers.HistoryCommissionStaffController.SyncCommissionStaff(productInvoice, WebSecurity.CurrentUserId);

                }
                return true;

            }
            catch (Exception ex)
            {

                return false;
            }


        }


        #region Success
        [HttpPost]
        public ActionResult Success(ProductInvoiceViewModel model)
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





            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                    var invoice_detail_list = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();
                    ProductOutboundViewModel productOutboundViewModel = new ProductOutboundViewModel();
                    var warehouseDefault = warehouseRepository.GetAllWarehouse().Where(x => x.IsSale == true).Where(x => x.BranchId == intBrandID).FirstOrDefault();
                    //model.ReceiptViewModel = new ReceiptViewModel();
                    //model.NextPaymentDate_Temp = DateTime.Now;
                    //model.ReceiptViewModel.Amount = productInvoice.TotalAmount;
                    //model.ReceiptViewModel.Name = string.Empty;
                    //model.ReceiptViewModel.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;

                    if (warehouseDefault == null)
                    {
                        TempData[Globals.FailedMessageKey] += "<br/>Không tìm thấy kho xuất bán! Vui lòng kiểm tra lại!";
                        return RedirectToAction("Detail", new { Id = model.Id });
                    }
                    string check = "";
                    foreach (var item in invoice_detail_list)
                    {
                        var error = InventoryController.Check(item.ProductName, item.ProductId.Value, item.LoCode, item.ExpiryDate, warehouseDefault.Id, 0, item.Quantity.Value);
                        check += error;
                    }
                    if (!string.IsNullOrEmpty(check))
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.ArchiveFail + check;
                        return RedirectToAction("Detail", new { Id = model.Id });
                    }
                    #region  phiếu xuất ok
                    var product_outbound = productOutboundRepository.GetAllProductOutboundFull().Where(x => x.InvoiceId == productInvoice.Id).ToList();

                    //insert mới
                    if (product_outbound.Count() <= 0)
                    {
                        #region  thêm mới phiếu xuất

                        //Nếu trong đơn hàng có sản phẩm thì xuất kho
                        if (warehouseDefault != null)
                        {
                            productOutboundViewModel.InvoiceId = productInvoice.Id;
                            productOutboundViewModel.InvoiceCode = productInvoice.Code;
                            productOutboundViewModel.WarehouseSourceId = warehouseDefault.Id;
                            productOutboundViewModel.Note = "Xuất kho cho đơn hàng " + productInvoice.Code;
                            var DetailList = invoice_detail_list.Select(x =>
                                  new ProductInvoiceDetailViewModel
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
                                      IrregularDiscount = x.IrregularDiscount,
                                      IrregularDiscountAmount = x.IrregularDiscountAmount,
                                      CategoryCode = x.CategoryCode,
                                      ProductInvoiceCode = x.ProductInvoiceCode,
                                      ProductName = x.ProductName,
                                      ProductCode = x.ProductCode,
                                      ProductInvoiceId = x.ProductInvoiceId,
                                      ProductGroup = x.ProductGroup,
                                      CheckPromotion = x.CheckPromotion,
                                      IsReturn = x.IsReturn,
                                      //Status = x.Status
                                      LoCode = x.LoCode,
                                      ExpiryDate = x.ExpiryDate,
                                      Amount = x.Amount
                                  }).ToList();
                            //Lấy dữ liệu cho chi tiết
                            productOutboundViewModel.DetailList = new List<ProductOutboundDetailViewModel>();
                            AutoMapper.Mapper.Map(DetailList, productOutboundViewModel.DetailList);

                            var productOutbound = ProductOutboundController.AutoCreateProductOutboundFromProductInvoice(productOutboundRepository, productOutboundViewModel, productInvoice.Code);
                            //ProductOutboundController.Archive_mobile(productOutbound, model.CreatedUserId.GetValueOrDefault());
                            //Ghi sổ chứng từ phiếu xuất
                            ProductOutboundController.Archive(productOutbound, TempData);
                        }
                        #endregion
                    }
                    else
                    {
                        #region   cập nhật phiếu xuất kho
                        //xóa chi tiết phiếu xuất, insert chi tiết mới
                        //cập nhật lại tổng tiền, trạng thái phiếu xuất
                        for (int i = 0; i < product_outbound.Count(); i++)
                        {
                            var outbound_detail = productOutboundRepository.GetAllProductOutboundDetailByOutboundId(product_outbound[i].Id).ToList();
                            //xóa
                            for (int ii = 0; ii < outbound_detail.Count(); ii++)
                            {
                                productOutboundRepository.DeleteProductOutboundDetail(outbound_detail[ii].Id);
                            }
                            //insert mới
                            for (int iii = 0; iii < invoice_detail_list.Count(); iii++)
                            {
                                ProductOutboundDetail productOutboundDetail = new ProductOutboundDetail();
                                productOutboundDetail.Price = invoice_detail_list[iii].Price;
                                productOutboundDetail.ProductId = invoice_detail_list[iii].ProductId;
                                productOutboundDetail.Quantity = invoice_detail_list[iii].Quantity;
                                productOutboundDetail.Unit = invoice_detail_list[iii].Unit;
                                productOutboundDetail.LoCode = invoice_detail_list[iii].LoCode;
                                productOutboundDetail.ExpiryDate = invoice_detail_list[iii].ExpiryDate;
                                productOutboundDetail.IsDeleted = false;
                                productOutboundDetail.CreatedUserId = WebSecurity.CurrentUserId;
                                productOutboundDetail.ModifiedUserId = WebSecurity.CurrentUserId;
                                productOutboundDetail.CreatedDate = DateTime.Now;
                                productOutboundDetail.ModifiedDate = DateTime.Now;
                                productOutboundDetail.ProductOutboundId = product_outbound[i].Id;
                                productOutboundRepository.InsertProductOutboundDetail(productOutboundDetail);

                            }
                            product_outbound[i].TotalAmount = invoice_detail_list.Sum(x => (x.Price * x.Quantity));

                            //Ghi sổ chứng từ phiếu xuất
                            ProductOutboundController.Archive(product_outbound[i], TempData);
                        }
                        #endregion
                    }
                    #endregion

                    #region  xóa hết lịch sử giao dịch
                    //xóa lịch sử giao dịch có liên quan đến đơn hàng, gồm: 1 dòng giao dịch bán hàng, 1 dòng thu tiền.
                    var transaction_Liablities = transactionLiabilitiesRepository.GetAllTransaction().Where(x => x.MaChungTuGoc == productInvoice.Code && x.LoaiChungTuGoc == "ProductInvoice").ToList();
                    if (transaction_Liablities.Count() > 0)
                    {
                        for (int i = 0; i < transaction_Liablities.Count(); i++)
                        {
                            transactionLiabilitiesRepository.DeleteTransaction(transaction_Liablities[i].Id);
                        }
                    }
                    #endregion

                    if (!productInvoice.IsArchive)
                    {
                        #region  Cập nhật thông tin thanh toán cho đơn hàng
                        //Cập nhật thông tin thanh toán cho đơn hàng
                        productInvoice.PaymentMethod = model.ReceiptViewModel.PaymentMethod;
                        productInvoice.PaidAmount = Convert.ToDecimal(productInvoice.TotalAmount);
                        productInvoice.RemainingAmount = productInvoice.TotalAmount - productInvoice.PaidAmount;
                        productInvoice.NextPaymentDate = model.NextPaymentDate_Temp;

                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoice.BranchId = intBrandID.Value;



                        productInvoiceRepository.UpdateProductInvoice(productInvoice);

                        //Lấy mã KH
                        var customer = customerRepository.GetCustomerById(productInvoice.CustomerId.Value);
                        //neu la khach vang lai
                        if (customer == null)
                        {
                            customer = customerRepository.GetCustomerByCode("KHACHVANGLAI");
                        }

                        var remain = productInvoice.TotalAmount - Convert.ToDecimal(productInvoice.TotalAmount);
                        if (remain > 0)
                        {
                        }
                        else
                        {
                            productInvoice.NextPaymentDate = null;
                            model.NextPaymentDate_Temp = null;
                        }
                        #endregion

                        #region thêm lịch sử bán hàng
                        //Ghi Nợ TK 131 - Phải thu của khách hàng (tổng giá thanh toán)
                        if (customer != null)
                        {
                            Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    "Bán hàng",
                                    customer.Code,
                                    "Customer",
                                    productInvoice.TotalAmount,
                                    0,
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    null,
                                    model.NextPaymentDate_Temp,
                                    null);
                        }
                        #endregion

                        #region phiếu thu
                        //Khách thanh toán ngay
                        if (productInvoice.TotalAmount > 0)
                        {
                            #region xóa phiếu thu cũ (nếu có)
                            var receipt = ReceiptRepository.GetAllReceiptFull().Where(x => x.BranchId == intBrandID || intBrandID == 0)
                                .Where(item => item.MaChungTuGoc == productInvoice.Code).ToList();
                            var receipt_detail = receiptDetailRepository.GetAllReceiptDetailFull().ToList();
                            receipt_detail = receipt_detail.Where(x => x.MaChungTuGoc == productInvoice.Code).ToList();
                            if (receipt_detail.Count() > 0)
                            {
                                // isDelete chi tiết phiếu thu
                                for (int i = 0; i < receipt_detail.Count(); i++)
                                {
                                    receiptDetailRepository.DeleteReceiptDetail(receipt_detail[i].Id);
                                }
                            }
                            #endregion
                            if (receipt.Count() > 0)
                            {
                                #region cập nhật phiếu thu cũ
                                // isDelete phiếu thu
                                var receipts = receipt.FirstOrDefault();
                                receipts.IsDeleted = false;
                                receipts.Payer = customer.LastName + " " + customer.FirstName;
                                receipts.PaymentMethod = productInvoice.PaymentMethod;
                                receipts.ModifiedDate = DateTime.Now;
                                receipts.VoucherDate = DateTime.Now;
                                receipts.Amount = productInvoice.TotalAmount;
                                receipts.BranchId = intBrandID.Value;
                                if (receipts.Amount > productInvoice.TotalAmount)
                                    receipts.Amount = productInvoice.TotalAmount;

                                ReceiptRepository.UpdateReceipt(receipts);

                                //Thêm vào quản lý chứng từ
                                //TransactionController.Create(new TransactionViewModel
                                //{
                                //    TransactionModule = "Receipt",
                                //    TransactionCode = receipts.Code,
                                //    TransactionName = "Thu tiền khách hàng"
                                //});

                                //Thêm chứng từ liên quan
                                //TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                                //{
                                //    TransactionA = receipts.Code,
                                //    TransactionB = productInvoice.Code
                                //});

                                //Ghi Có TK 131 - Phải thu của khách hàng.
                                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    receipts.Code,
                                    "Receipt",
                                    "Thu tiền khách hàng",
                                    customer.Code,
                                    "Customer",
                                    0,
                                    Convert.ToDecimal(productInvoice.TotalAmount),
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    model.ReceiptViewModel.PaymentMethod,
                                    null,
                                    null);
                                #endregion
                            }
                            else
                            {
                                #region thêm mới phiếu thu
                                //Lập phiếu thu
                                var receipt_inser = new Receipt();
                                AutoMapper.Mapper.Map(model.ReceiptViewModel, receipt_inser);
                                receipt_inser.IsDeleted = false;
                                receipt_inser.CreatedUserId = WebSecurity.CurrentUserId;
                                receipt_inser.ModifiedUserId = WebSecurity.CurrentUserId;
                                receipt_inser.AssignedUserId = WebSecurity.CurrentUserId;
                                receipt_inser.CreatedDate = DateTime.Now;
                                receipt_inser.ModifiedDate = DateTime.Now;
                                receipt_inser.VoucherDate = DateTime.Now;
                                receipt_inser.CustomerId = customer.Id;
                                receipt_inser.Payer = customer.LastName + " " + customer.FirstName;
                                receipt_inser.PaymentMethod = productInvoice.PaymentMethod;
                                receipt_inser.Address = customer.Address;
                                receipt_inser.MaChungTuGoc = productInvoice.Code;
                                receipt_inser.LoaiChungTuGoc = "ProductInvoice";
                                receipt_inser.Note = receipt_inser.Name;
                                receipt_inser.BranchId = intBrandID.Value;


                                receipt_inser.Amount = productInvoice.TotalAmount;

                                ReceiptRepository.InsertReceipt(receipt_inser);

                                var prefixReceipt = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_ReceiptCustomer");
                                receipt_inser.Code = Erp.BackOffice.Helpers.Common.GetCode(prefixReceipt, receipt_inser.Id);
                                ReceiptRepository.UpdateReceipt(receipt_inser);
                                ////Thêm vào quản lý chứng từ
                                //TransactionController.Create(new TransactionViewModel
                                //{
                                //    TransactionModule = "Receipt",
                                //    TransactionCode = receipt_inser.Code,
                                //    TransactionName = "Thu tiền khách hàng"
                                //});

                                //Thêm chứng từ liên quan
                                //TransactionController.CreateRelationship(new TransactionRelationshipViewModel
                                //{
                                //    TransactionA = receipt_inser.Code,
                                //    TransactionB = productInvoice.Code
                                //});

                                //Ghi Có TK 131 - Phải thu của khách hàng.
                                Erp.BackOffice.Account.Controllers.TransactionLiabilitiesController.Create(
                                    receipt_inser.Code,
                                    "Receipt",
                                    "Thu tiền khách hàng",
                                    customer.Code,
                                    "Customer",
                                    0,
                                    Convert.ToDecimal(productInvoice.TotalAmount),
                                    productInvoice.Code,
                                    "ProductInvoice",
                                    model.ReceiptViewModel.PaymentMethod,
                                    null,
                                    null);

                                #endregion
                            }
                        }
                        #endregion

                        #region cập nhật đơn bán hàng
                        //Cập nhật đơn hàng
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.IsArchive = true;
                        productInvoice.BranchId = intBrandID.Value;
                        productInvoice.Status = Wording.OrderStatus_complete;
                        productInvoiceRepository.UpdateProductInvoice(productInvoice);
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.ArchiveSuccess;

                        TempData[Globals.SuccessMessageKey] += "<br/>Đơn hàng này đã được xuất kho! Vui lòng kiểm tra lại chứng từ xuất kho để tránh sai xót dữ liệu!";
                        #endregion
                        //cap nhat chiet khau nha thuoc
                        //Erp.BackOffice.Sale.Controllers.TotalDiscountMoneyNTController.SyncTotalDisCountMoneyNT(productInvoice, WebSecurity.CurrentUserId);
                        //cap nhat hoa hong nhan vien
                        //Erp.BackOffice.Staff.Controllers.HistoryCommissionStaffController.SyncCommissionStaff(productInvoice, WebSecurity.CurrentUserId);

                    }
                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                    return Content("Fail");
                }
            }
            return RedirectToAction("Detail", new { Id = model.Id });
        }
        #endregion

        #region UpdateAll
        //   [HttpPost]
        //public ActionResult UpdateAll(string url)
        //{
        //     DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //     // Cộng thêm 1 tháng và trừ đi một ngày.
        //    DateTime retDateTime = aDateTime.AddDays(18);
        //    var product_invoice = productInvoiceRepository.GetAllProductInvoice().Where(x => x.IsArchive == true && x.CreatedDate >= aDateTime && x.CreatedDate <= retDateTime).ToList();
        //    foreach (var item in product_invoice)
        //    {
        //          CommisionStaffController.CreateCommission(item.Id);
        //    }   
        //    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
        //    return Redirect(url);
        //}
        #endregion


        #region CreateNT
        //latuyen.fit Comit lan 3
        public ActionResult CreateNT(int? Id, int? BranchId, bool? recallCreateNT)
        {


            ProductInvoiceViewModel model = new ProductInvoiceViewModel();
            model.recallCreateNT = false;
            if (recallCreateNT.HasValue && recallCreateNT == true)
            {
                model.recallCreateNT = true;

            }
            model.DetailList = new List<ProductInvoiceDetailViewModel>();
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            //model.BranchId = Convert.ToInt32(Erp.BackOffice.Helpers.Common.GetSetting("BranchId"));
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

            if (intBrandID == 0)
            {
                return View("Page404");
            }

            if (Id.HasValue && Id > 0)
            {
                var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(Id.Value);
                ////Kiểm tra phân quyền Chi nhánh
                //if ((Filters.SecurityFilter.IsTrinhDuocVien() || Filters.SecurityFilter.IsAdminSystemManager() || Filters.SecurityFilter.IsStaffDrugStore() || Filters.SecurityFilter.IsAdminDrugStore()) && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == false)
                //{
                //    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                //}

                AutoMapper.Mapper.Map(productInvoice, model);

                model.DetailList = productInvoiceRepository.GetAllvwInvoiceDetailsByInvoiceId(productInvoice.Id).Select(x => new ProductInvoiceDetailViewModel
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    FixedDiscountAmount = x.FixedDiscountAmount,
                    IrregularDiscountAmount = x.IrregularDiscountAmount,
                    BranchId = x.BranchId,
                    CategoryCode = x.CategoryCode,
                    ExpiryDate = x.ExpiryDate,
                    FixedDiscount = x.FixedDiscount,
                    IrregularDiscount = x.IrregularDiscount,
                    Unit = x.Unit,
                    LoCode = x.LoCode,
                    Price = x.Price,
                    ProductCode = x.ProductCode,
                    ProductGroup = x.ProductGroup,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    Origin = x.Origin,
                    TaxFee = 10,
                    TaxFeeAmount = x.TaxFeeAmount
                }).ToList();
                //AutoMapper.Mapper.Map(detailList, model.DetailList);
            }
            else
            {
                model.Id = 0;
            }
            model.ReceiptViewModel = new ReceiptViewModel();
            model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
            model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
            model.ReceiptViewModel.Amount = 0;

            //  var saleDepartmentCode = Erp.BackOffice.Helpers.Common.GetSetting("SaleDepartmentCode");
            string image_folder_product = Helpers.Common.GetSetting("product-image-folder");

            //Thêm số lượng tồn kho cho chi tiết đơn hàng đã được thêm
            if (model.DetailList != null && model.DetailList.Count > 0)
            {
                var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = intBrandID, LoCode = "", ProductId = "", ExpiryDate = "" });
                productList = productList.Where(x => x.IsSale != null && x.IsSale == true);
                ViewBag.productList = productList.ToList();
                foreach (var item in model.DetailList)
                {
                    var product = productList.Where(i => i.ProductId == item.ProductId && i.LoCode == item.LoCode && i.ExpiryDate == item.ExpiryDate).FirstOrDefault();
                    if (product != null)
                    {
                        item.QuantityInInventory = product.Quantity;
                        item.PriceTest = product.ProductPriceOutbound;
                    }
                    else
                    {
                        item.Id = 0;
                    }
                }

                model.DetailList.RemoveAll(x => x.Id == 0);

                int n = 0;
                foreach (var item in model.DetailList)
                {
                    item.OrderNo = n;
                    n++;
                }
            }
            //lấy tất cả các khách hàng
            //List<CustomerViewModel> arrcus = new List<CustomerViewModel>();
            //List<vwCustomer> arr = customerRepository.GetListAllvwCustomer();
            //foreach (var item in arr)
            //{
            //    CustomerViewModel tmp = new CustomerViewModel();
            //    tmp.Id = item.Id;
            //    tmp.CreatedUserId = item.CreatedUserId;
            //    //CreatedUserName = item.CreatedUserName,
            //    tmp.CreatedDate = item.CreatedDate;
            //    tmp.ModifiedUserId = item.ModifiedUserId;
            //    //ModifiedUserName = item.ModifiedUserName,
            //    tmp.ModifiedDate = item.ModifiedDate;
            //    tmp.Code = item.Code;
            //    tmp.CompanyName = item.CompanyName;
            //    tmp.Phone = item.Phone;
            //    tmp.Address = item.Address;
            //    tmp.Note = item.Note;
            //    tmp.CardCode = item.CardCode;
            //    tmp.SearchFullName = item.SearchFullName;
            //    tmp.Image = item.Image;
            //    tmp.Birthday = item.Birthday;
            //    tmp.Gender = item.Gender;
            //    tmp.IdCardDate = item.IdCardDate;
            //    tmp.IdCardIssued = item.IdCardIssued;
            //    tmp.IdCardNumber = item.IdCardNumber;
            //    tmp.CardIssuedName = item.CardIssuedName;
            //    tmp.FirstName = item.FirstName;
            //    tmp.LastName = item.LastName;
            //    tmp.BankAccount = item.BankAccount;
            //    tmp.BankName = item.BankName;
            //    tmp.TaxCode = item.TaxCode;
            //    tmp.ProvinceName = item.ProvinceName;
            //    tmp.DistrictName = item.DistrictName;
            //    tmp.WardName = item.WardName;
            //    tmp.DistrictId = item.DistrictId;
            //    arrcus.Add(tmp);
            //}

            //ViewBag.ListCus = arrcus.ToList();
            model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;

            int taxfee = 0;
            int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
            model.TaxFee = taxfee;
            //int? intBrandID = int.Parse(strBrandID);
            DateTime dtnow = DateTime.Now;
            List<KmHd_ViewModel> tmp = new List<KmHd_ViewModel>();
            //lấy tất cả các các chưng trình khuyên mại đang còn hiệu lực
            var commisionhh = commisionCustomerRepository.GetListAllCommisionCustomer().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow && x.status == 1 && x.TypeCus == "HH").ToList();
            if (commisionhh == null || commisionhh.Count == 0)
            {
                var commision = commisionCustomerRepository.GetListAllCommisionCustomer().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow && x.status == 1).ToList();

                //truy vấn xem sp this thuộc những chưng trình nào

                if (commision.Count() > 0)
                {
                    foreach (var item in commision)
                    {
                        if (item.Type == 4 && item.TypeCus == "HD")
                        {
                            var comApply = commisionApplyRepository.GetListCommisionApplyByIdCus(item.CommissionCusId.Value);
                            foreach (var items in comApply)
                            {

                                if (items.Type == 3)
                                {
                                    if (items.BranchId == 0 || items.BranchId == null)
                                    {
                                        KmHd_ViewModel KM = new KmHd_ViewModel();
                                        KM.id = item.Id;
                                        KM.id_cha = item.CommissionCusId.Value;

                                        KM.IsMoney = item.IsMoney;
                                        KM.CommissionValue = item.CommissionValue;
                                        KM.Minvalue = item.Minvalue;
                                        KM.TypeApply = items.Type;
                                        KM.BranchId = items.BranchId;
                                        KM.type = "ALLCN";
                                        tmp.Add(KM);

                                    }
                                    else
                                    {
                                        if (items.BranchId == intBrandID)
                                        {
                                            KmHd_ViewModel KM = new KmHd_ViewModel();
                                            KM.id = item.Id;
                                            KM.id_cha = item.CommissionCusId.Value;

                                            KM.IsMoney = item.IsMoney;
                                            KM.CommissionValue = item.CommissionValue;
                                            KM.Minvalue = item.Minvalue;
                                            KM.TypeApply = items.Type;
                                            KM.BranchId = items.BranchId;
                                            KM.type = "ALLCN";
                                            tmp.Add(KM);
                                        }
                                    }
                                }
                                else if (items.Type == 2)
                                {
                                    var logvip = logVipRepository.GetvwAllLogVip().Where(x => x.is_approved == true && x.Status == "Đã sử dụng" && x.LoyaltyPointId == items.BranchId).ToList();
                                    if (logvip != null && logvip.Count() > 0)
                                    {
                                        KmHd_ViewModel KM = new KmHd_ViewModel();
                                        KM.id = item.Id;
                                        KM.id_cha = item.CommissionCusId.Value;

                                        KM.IsMoney = item.IsMoney;
                                        KM.CommissionValue = item.CommissionValue;
                                        KM.Minvalue = item.Minvalue;
                                        KM.TypeApply = items.Type;
                                        KM.BranchId = items.BranchId;
                                        KM.type = "NV";
                                        string Idcuss = "";
                                        for (int i = 0; i < logvip.Count(); i++)
                                        {
                                            Idcuss += (logvip[i].CustomerId.Value).ToString();
                                        }
                                        KM.listIDcustomer = Idcuss;
                                        tmp.Add(KM);
                                    }

                                }
                                else
                                {
                                    KmHd_ViewModel KM = new KmHd_ViewModel();
                                    KM.id = item.Id;
                                    KM.id_cha = item.CommissionCusId.Value;

                                    KM.IsMoney = item.IsMoney;
                                    KM.CommissionValue = item.CommissionValue;
                                    KM.Minvalue = item.Minvalue;
                                    KM.TypeApply = items.Type;
                                    KM.BranchId = items.BranchId;
                                    string Idcuss = "";
                                    Idcuss = items.BranchId.ToString();
                                    KM.listIDcustomer = Idcuss;
                                    KM.type = "CUST";
                                    tmp.Add(KM);
                                }
                            }
                        }

                    }
                }
            }

            model.DetailListKMHD = tmp;
            //for(int i = 0; i < tmp.Count(); i++)
            //{
            //    decimal min = tmp[0].Minvalue;
            //    int dem = 0;
            //    for (int j = 1; j < tmp.Count(); j++)
            //    {
            //       if(tmp[j].Minvalue <= min)
            //        {
            //            dem = j;
            //            min = tmp[j].Minvalue;
            //        }

            //    }
            //    model.DetailListKMHD.Add(tmp[dem]);
            //    tmp.Remove(tmp[dem]);

            //}

            return View(model);
        }

        [HttpGet]
        [ValidateInput(false)]
        public JsonResult getImageServer(string strImage)
        {
            try
            {
                string strImage1 = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(strImage, "product-image-folder", "product");
                JsonResult jr = Json(strImage1, JsonRequestBehavior.AllowGet);
                return jr;
            }
            catch (Exception ex)
            {

                return Json("");
            }
        }




        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CreatePOS(List<ProductInvoiceDetailViewModel> listsp, string customerId, string DiscountTab, string Hinhthuctt, Decimal MoneyPayTab, decimal ATMPayTab, decimal TransferPayTab, string IsDisAll, string DiscountAll, string NoteHDtab)
        {

            try
            {

                if (listsp != null && listsp.Count > 0)
                {


                    ProductInvoiceViewModel model = new ProductInvoiceViewModel();
                    model.DetailList = new List<ProductInvoiceDetailViewModel>();

                    AutoMapper.Mapper.Map(listsp, model.DetailList);
                    model.ReceiptViewModel = new ReceiptViewModel();
                    model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
                    model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
                    model.ReceiptViewModel.Amount = 0;
                    if (customerId == "" || customerId == null)
                    {
                        customerId = "0";
                    }
                    model.CustomerId = int.Parse(customerId);

                    //begin hoapd copy code insert
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


                    var json = new JavaScriptSerializer().Serialize(model);
                    if (model.DetailList.Count != 0)
                    {
                        var customerthis = customerRepository.GetCustomerById(int.Parse(customerId));

                        ProductInvoice productInvoice = null;

                        //    string output = JsonConvert.SerializeObject(model);

                        if (model.Id > 0)
                        {
                            productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                            //Kiểm tra phân quyền Chi nhánh
                            //if ((Filters.SecurityFilter.IsTrinhDuocVien() || Filters.SecurityFilter.IsAdminSystemManager() || Filters.SecurityFilter.IsStaffDrugStore() || Filters.SecurityFilter.IsAdminDrugStore()) && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == false)
                            //{
                            //    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                            //}
                        }
                        using (var scope = new TransactionScope(TransactionScopeOption.Required))
                        {
                            try
                            {
                                if (productInvoice != null)
                                {
                                    //Nếu đã ghi sổ rồi thì không được sửa
                                    if (productInvoice.IsArchive)
                                    {
                                        return Json(0);
                                    }

                                    AutoMapper.Mapper.Map(model, productInvoice);
                                }
                                else
                                {
                                    productInvoice = new ProductInvoice();

                                    AutoMapper.Mapper.Map(model, productInvoice);
                                    if (customerId == null || customerId == "" || customerId.ToString() == "0")
                                    {
                                        var customernull = customerRepository.GetCustomerByCode("KHACHVANGLAI");
                                        productInvoice.CustomerName = customernull.FirstName;
                                        productInvoice.CustomerId = customernull.Id;
                                        productInvoice.CustomerPhone = customernull.Phone;
                                        productInvoice.TaxCode = customernull.TaxCode;
                                        productInvoice.Address = customernull.Address;
                                        productInvoice.BankName = customernull.BankName;
                                    }
                                    else
                                    {
                                        productInvoice.CustomerId = int.Parse(customerId);
                                    }

                                    if ((productInvoice.CustomerId == null) || (productInvoice.CustomerId == 0))
                                    {
                                        var customernull = customerRepository.GetCustomerByCode("KHACHVANGLAI");
                                        productInvoice.CustomerName = customernull.FirstName;
                                        productInvoice.CustomerId = customernull.Id;
                                        productInvoice.CustomerPhone = customernull.Phone;
                                        productInvoice.TaxCode = customernull.TaxCode;
                                        productInvoice.Address = customernull.Address;
                                        productInvoice.BankName = customernull.BankName;

                                    }
                                    productInvoice.TotalAmount = 0;
                                    foreach (var k in model.DetailList)
                                    {
                                        productInvoice.TotalAmount = k.Quantity.Value * k.Price.Value;
                                    }
                                    //3408 khách vãng lai
                                    if (productInvoice.CustomerId != 4)
                                    {
                                        productInvoice.CustomerId = customerthis.Id;
                                        productInvoice.CustomerName = customerthis.LastName + " " + customerthis.FirstName;
                                        productInvoice.CustomerPhone = customerthis.Phone;
                                        productInvoice.TaxCode = customerthis.TaxCode;
                                        productInvoice.Address = customerthis.Address;
                                        productInvoice.BankName = customerthis.BankName;
                                    }

                                    productInvoice.IsDeleted = false;
                                    productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
                                    productInvoice.CreatedDate = DateTime.Now;
                                    productInvoice.Status = Wording.OrderStatus_pending;
                                    productInvoice.BranchId = model.BranchId.HasValue ? model.BranchId.Value : 1;
                                    productInvoice.IsArchive = false;
                                    productInvoice.IsReturn = false;
                                    if (MoneyPayTab != null && MoneyPayTab != 0)
                                    {
                                        productInvoice.MoneyPay = MoneyPayTab;
                                    }
                                    else
                                    {
                                        productInvoice.MoneyPay = 0;
                                    }
                                    if (NoteHDtab.Length > 0 && NoteHDtab != null)
                                    {
                                        productInvoice.Note = NoteHDtab;
                                    }
                                    else
                                    {
                                        productInvoice.Note = "";
                                    }
                                    if (ATMPayTab != null && ATMPayTab != 0)
                                    {
                                        productInvoice.ATMPay = ATMPayTab;
                                    }
                                    else
                                    {
                                        productInvoice.ATMPay = 0;
                                    }
                                    if (TransferPayTab != null && TransferPayTab != 0)
                                    {
                                        productInvoice.TransferPay = TransferPayTab;
                                    }
                                    else
                                    {
                                        productInvoice.TransferPay = 0;
                                    }
                                    productInvoice.RemainingAmount = model.TotalAmount;
                                    productInvoice.PaidAmount = 0;
                                    productInvoice.BranchId = intBrandID.Value;
                                    productInvoice.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;
                                }
                                //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                                List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
                                model.DetailList.RemoveAll(x => x.Quantity <= 0);
                                foreach (var group in model.DetailList)
                                {
                                    int a = group.IrregularDiscount.Value;
                                    var product = ProductRepository.GetProductById(group.ProductId.Value);
                                    ProductInvoiceDetail tmp = new ProductInvoiceDetail();
                                    tmp.ProductId = product.Id;
                                    tmp.ProductType = product.Type;
                                    tmp.Quantity = group.Quantity;
                                    tmp.Unit = product.Unit;
                                    tmp.Price = group.Price;
                                    tmp.PromotionDetailId = group.PromotionDetailId;
                                    tmp.PromotionId = group.PromotionId;
                                    tmp.PromotionValue = group.PromotionValue;
                                    tmp.IsDeleted = false;
                                    tmp.CreatedUserId = WebSecurity.CurrentUserId;
                                    tmp.ModifiedUserId = WebSecurity.CurrentUserId;
                                    tmp.CreatedDate = DateTime.Now;
                                    tmp.ModifiedDate = DateTime.Now;
                                    tmp.FixedDiscount = group.FixedDiscount;
                                    tmp.FixedDiscountAmount = group.FixedDiscountAmount;
                                    tmp.IrregularDiscount = group.IrregularDiscount;
                                    tmp.IrregularDiscountAmount = group.IrregularDiscountAmount;
                                    tmp.QuantitySaleReturn = group.Quantity;
                                    tmp.CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false);
                                    tmp.IsReturn = false;
                                    tmp.LoCode = group.LoCode;
                                    tmp.ExpiryDate = group.ExpiryDate;
                                    tmp.Origin = product.Origin;
                                    tmp.TaxFeeAmount = group.TaxFeeAmount;
                                    tmp.TaxFee = group.TaxFee;
                                    if (DiscountTab == null || DiscountTab != "" || DiscountTab.Length == 0)
                                    {
                                        //chỉ tính KM sp chưa được KM
                                        if (tmp.IrregularDiscountAmount == 0 && tmp.IrregularDiscount == 0 || tmp.IrregularDiscountAmount == null && tmp.IrregularDiscount == 0 || tmp.IrregularDiscountAmount == 0 && tmp.IrregularDiscount == null || tmp.IrregularDiscountAmount == null && tmp.IrregularDiscount == null)
                                        {
                                            //mcuong.fit***********
                                            //begin
                                            DateTime dtnow = DateTime.Now;

                                            // var commisioncus = commissionCusRepository.GetAllCommissionCus().Where(x=>x.StartDate<= dtnow&&x.EndDate>= dtnow).ToList();
                                            //mcuong.fit******************************************************************************************************************************
                                            //Begin
                                            //lấy tất cả các các chưng trình khuyên mại đang còn hiệu lực
                                            var commision = commisionCustomerRepository.GetListAllCommisionCustomer().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow && x.status == 1).ToList();

                                            //truy vấn xem sp this thuộc những chưng trình nào
                                            foreach (var item in commision)
                                            {
                                                var comApply = commisionApplyRepository.GetListCommisionApplyByIdCus(item.CommissionCusId.Value);
                                                foreach (var items in comApply)
                                                {
                                                    //phạp vi áp dụng
                                                    //chi nhánh*****************************************************************Type apply =3
                                                    if (item.TypeCus == "HD")
                                                    {
                                                        if (items.Type == 3)
                                                        {   //ap dụng cho hóa đơn
                                                            if (items.BranchId == 0 || items.BranchId == null)
                                                            {
                                                                if (item.Type == 4)
                                                                {
                                                                    if (model.TotalAmount >= item.Minvalue)
                                                                    {
                                                                        var item_IrregularDiscount = item;
                                                                        if (item_IrregularDiscount.IsMoney == true)
                                                                        {
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                        }
                                                                        else
                                                                        {
                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                if (item.Type == 3)
                                                                {
                                                                    var item_IrregularDiscount = item;
                                                                    if (item_IrregularDiscount.IsMoney == true)
                                                                    {
                                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                    }
                                                                    else
                                                                    {
                                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                    }
                                                                    break;
                                                                }
                                                                if (item.Type == 2)
                                                                {
                                                                    // lấy thông tin sp đầy đủ
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                                                        foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                                                        {
                                                                            if (item.ProductId == int.Parse(itemss))
                                                                            {
                                                                                var item_IrregularDiscount = item;
                                                                                if (item_IrregularDiscount.IsMoney == true)
                                                                                {
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                                }
                                                                                else
                                                                                {
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                                                }
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                if (item.Type == 1)
                                                                {
                                                                    // lấy thông tin sp đầy đủ
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        if (item.ProductId == sp.Id)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (items.BranchId == intBrandID)
                                                                {
                                                                    if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
                                                                    {
                                                                        if (model.TotalAmount >= item.Minvalue)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    if (item.Type == 3)
                                                                    {
                                                                        var item_IrregularDiscount = item;
                                                                        if (item_IrregularDiscount.IsMoney == true)
                                                                        {
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                        }
                                                                        else
                                                                        {
                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                        }
                                                                        break;
                                                                    }
                                                                    if (item.Type == 2)
                                                                    {
                                                                        // lấy thông tin sp đầy đủ
                                                                        if (tmp.ProductId != 0)
                                                                        {
                                                                            var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                            string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                                                            foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                                                            {
                                                                                if (item.ProductId == int.Parse(itemss))
                                                                                {
                                                                                    var item_IrregularDiscount = item;
                                                                                    if (item_IrregularDiscount.IsMoney == true)
                                                                                    {
                                                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                                                    }
                                                                                    break;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    if (item.Type == 1)
                                                                    {
                                                                        // lấy thông tin sp đầy đủ
                                                                        if (tmp.ProductId != 0)
                                                                        {
                                                                            var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                            if (item.ProductId == sp.Id)
                                                                            {
                                                                                var item_IrregularDiscount = item;
                                                                                if (item_IrregularDiscount.IsMoney == true)
                                                                                {
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                                }
                                                                                else
                                                                                {
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                                }
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        //khuyễn mãi theo nhóm vip
                                                        if (items.Type == 2)
                                                        {

                                                            //var logvip = logVipRepository.GetvwAllLogVip().Where(x => x.is_approved == true && x.Status == "Đã sử dụng" && x.LoyaltyPointId == items.BranchId).FirstOrDefault();
                                                            var logvip = logVipRepository.GetlistvwAllLogVip();
                                                            if (logvip != null)
                                                            {
                                                                if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
                                                                {
                                                                    if (productInvoice.CustomerId == logvip[0].CustomerId)
                                                                    {
                                                                        if (model.TotalAmount >= item.Minvalue)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }

                                                                }
                                                                if (item.Type == 3)
                                                                {
                                                                    if (productInvoice.CustomerId == logvip[0].CustomerId)
                                                                    {

                                                                        var item_IrregularDiscount = item;
                                                                        if (item_IrregularDiscount.IsMoney == true)
                                                                        {
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                        }
                                                                        else
                                                                        {
                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                        }
                                                                        break;

                                                                    }

                                                                }
                                                                if (item.Type == 2)
                                                                {
                                                                    // lấy thông tin sp đầy đủ
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                                                        foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                                                        {
                                                                            if (item.ProductId == int.Parse(itemss))
                                                                            {
                                                                                var item_IrregularDiscount = item;
                                                                                if (item_IrregularDiscount.IsMoney == true)
                                                                                {
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                                }
                                                                                else
                                                                                {
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                                                }
                                                                                break;
                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                                if (item.Type == 1)
                                                                {
                                                                    // lấy thông tin sp đầy đủ
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        if (item.ProductId == sp.Id)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                }

                                                            }


                                                        }
                                                        //khách hàng cụ thể
                                                        if (items.Type == 1)
                                                        {
                                                            if (items.BranchId == productInvoice.CustomerId)
                                                            {
                                                                if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
                                                                {
                                                                    if (model.TotalAmount >= item.Minvalue)
                                                                    {
                                                                        var item_IrregularDiscount = item;
                                                                        if (item_IrregularDiscount.IsMoney == true)
                                                                        {
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                        }
                                                                        else
                                                                        {
                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                if (item.Type == 3)
                                                                {
                                                                    var item_IrregularDiscount = item;
                                                                    if (item_IrregularDiscount.IsMoney == true)
                                                                    {
                                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                    }
                                                                    else
                                                                    {
                                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                    }
                                                                    break;
                                                                }
                                                                if (item.Type == 2)
                                                                {
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                                                        foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                                                        {
                                                                            if (item.ProductId == int.Parse(itemss))
                                                                            {
                                                                                var item_IrregularDiscount = item;
                                                                                if (item_IrregularDiscount.IsMoney == true)
                                                                                {
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                                }
                                                                                else
                                                                                {
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                                                }
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                if (item.Type == 1)
                                                                {
                                                                    // lấy thông tin sp đầy đủ
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        if (item.ProductId == sp.Id)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }


                                                        }

                                                    }
                                                    if (item.TypeCus == "HH")
                                                    {
                                                        if (items.Type == 3)
                                                        {
                                                            if (items.BranchId == 0 || items.BranchId == null)
                                                            {
                                                                if (item.Type == 4)
                                                                {
                                                                    if (model.TotalAmount >= item.Minvalue)
                                                                    {
                                                                        var item_IrregularDiscount = item;
                                                                        if (item_IrregularDiscount.IsMoney == true)
                                                                        {
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                        }
                                                                        else
                                                                        {
                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (items.BranchId == intBrandID)
                                                                {
                                                                    if (item.Type == 4)
                                                                    {
                                                                        if (model.TotalAmount >= item.Minvalue)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (items.Type == 2)
                                                        {
                                                            var logvip = logVipRepository.GetvwAllLogVip().Where(x => x.is_approved == true && x.Status == "Đang sử dụng" && x.LoyaltyPointId == items.BranchId).FirstOrDefault();
                                                            if (logvip != null)
                                                            {
                                                                if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
                                                                {
                                                                    if (productInvoice.CustomerId == logvip.CustomerId)
                                                                    {
                                                                        if (model.TotalAmount >= item.Minvalue)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }

                                                                }
                                                                if (item.Type == 3)
                                                                {
                                                                    if (productInvoice.CustomerId == logvip.CustomerId)
                                                                    {

                                                                        var item_IrregularDiscount = item;
                                                                        if (item_IrregularDiscount.IsMoney == true)
                                                                        {
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                        }
                                                                        else
                                                                        {
                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                        }
                                                                        break;

                                                                    }

                                                                }
                                                                if (item.Type == 2)
                                                                {
                                                                    // lấy thông tin sp đầy đủ
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                                                        foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                                                        {
                                                                            if (item.ProductId == int.Parse(itemss))
                                                                            {
                                                                                var item_IrregularDiscount = item;
                                                                                if (item_IrregularDiscount.IsMoney == true)
                                                                                {
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                                }
                                                                                else
                                                                                {
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                                                }
                                                                                break;
                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                                if (item.Type == 1)
                                                                {
                                                                    // lấy thông tin sp đầy đủ
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        if (item.ProductId == sp.Id)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }
                                                        if (items.Type == 1)
                                                        {
                                                            if (items.BranchId == productInvoice.CustomerId)
                                                            {
                                                                if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
                                                                {
                                                                    if (model.TotalAmount >= item.Minvalue)
                                                                    {
                                                                        var item_IrregularDiscount = item;
                                                                        if (item_IrregularDiscount.IsMoney == true)
                                                                        {
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                        }
                                                                        else
                                                                        {
                                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                if (item.Type == 3)
                                                                {
                                                                    var item_IrregularDiscount = item;
                                                                    if (item_IrregularDiscount.IsMoney == true)
                                                                    {
                                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                    }
                                                                    else
                                                                    {
                                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                    }
                                                                    break;
                                                                }
                                                                if (item.Type == 2)
                                                                {
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                                                        foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                                                        {
                                                                            if (item.ProductId == int.Parse(itemss))
                                                                            {
                                                                                var item_IrregularDiscount = item;
                                                                                if (item_IrregularDiscount.IsMoney == true)
                                                                                {
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                                }
                                                                                else
                                                                                {
                                                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                                                }
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                if (item.Type == 1)
                                                                {
                                                                    // lấy thông tin sp đầy đủ
                                                                    if (tmp.ProductId != 0)
                                                                    {
                                                                        var sp = ProductRepository.GetProductById(tmp.ProductId);
                                                                        if (item.ProductId == sp.Id)
                                                                        {
                                                                            var item_IrregularDiscount = item;
                                                                            if (item_IrregularDiscount.IsMoney == true)
                                                                            {
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
                                                                            }
                                                                            else
                                                                            {
                                                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }


                                                        }

                                                    }//
                                                }

                                            }
                                            //tinh lai tong tien neu co khuyen mại 
                                            productInvoice.TotalAmount -= tmp.IrregularDiscountAmount.Value;
                                        }
                                        else
                                        {
                                            if (tmp.IrregularDiscount == 0 || tmp.IrregularDiscount == null)
                                            {
                                                tmp.IrregularDiscount = Convert.ToInt32(tmp.IrregularDiscountAmount / (tmp.Price * tmp.Quantity) * 100);
                                            }
                                            else
                                            {
                                                if (tmp.IrregularDiscountAmount == 0 || tmp.IrregularDiscountAmount == null)
                                                {
                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * tmp.IrregularDiscount / 100);
                                                }
                                            }
                                            productInvoice.TotalAmount -= tmp.IrregularDiscountAmount.Value;
                                        }
                                    }
                                    else
                                    {
                                        tmp.IrregularDiscount = 0;
                                        tmp.IrregularDiscountAmount = 0;
                                    }

                                    listNewCheckSameId.Add(tmp);
                                }

                                if (DiscountTab != null && DiscountTab != "" && DiscountTab.Length > 0 && int.Parse(DiscountTab) != 0)
                                {
                                    char[] whitespace = new char[] { ' ', '\t' };
                                    string[] arrdis = DiscountTab.Split(whitespace);
                                    if (arrdis[1] == "%")
                                    {
                                        productInvoice.DiscountTabBill = int.Parse(arrdis[0]);
                                        productInvoice.DiscountTabBillAmount = Convert.ToInt32(productInvoice.TotalAmount * (int.Parse(arrdis[0])) / 100);

                                    }
                                    else
                                    {
                                        productInvoice.DiscountTabBillAmount = decimal.Parse(arrdis[0]);
                                        productInvoice.DiscountTabBill = Convert.ToInt32(decimal.Parse(arrdis[0]) / (productInvoice.TotalAmount) * 100);
                                    }
                                    productInvoice.TotalAmount -= productInvoice.DiscountTabBillAmount.Value;
                                }
                                if (DiscountAll != null && DiscountAll != "" && DiscountAll.Length > 0 && int.Parse(DiscountAll) != 0)
                                {
                                    productInvoice.DisCountAllTab = decimal.Parse(DiscountAll);
                                    if (IsDisAll == "1")
                                    {
                                        productInvoice.TotalAmount = productInvoice.TotalAmount - ((productInvoice.TotalAmount * int.Parse(DiscountAll)) / 100);
                                        productInvoice.isDisCountAllTab = true;
                                    }
                                    else
                                    {
                                        productInvoice.TotalAmount = productInvoice.TotalAmount - int.Parse(DiscountAll);
                                        productInvoice.isDisCountAllTab = false;
                                    }
                                }
                                productInvoice.IsReturn = false;
                                productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                                productInvoice.ModifiedDate = DateTime.Now;
                                productInvoice.PaidAmount = 0;
                                productInvoice.RemainingAmount = productInvoice.TotalAmount;
                                //tính lại tổng tiền
                                //hàm edit 
                                if (model.Id > 0)
                                {
                                    productInvoiceRepository.UpdateProductInvoice(productInvoice);
                                    var listDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

                                    //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail, UsingServiceLog,UsingServiceLogDetail,ServiceReminder
                                    productInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

                                    //thêm mới toàn bộ database
                                    foreach (var item in listNewCheckSameId)
                                    {
                                        item.ProductInvoiceId = productInvoice.Id;
                                        productInvoiceRepository.InsertProductInvoiceDetail(item);
                                    }

                                    //Thêm vào quản lý chứng từ
                                    TransactionController.Create(new TransactionViewModel
                                    {
                                        TransactionModule = "ProductInvoice",
                                        TransactionCode = productInvoice.Code,
                                        TransactionName = "Bán hàng"
                                    });
                                }
                                else
                                {
                                    //hàm thêm mới
                                    productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

                                    //cập nhật lại mã hóa đơn
                                    //string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
                                    //productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
                                    //productInvoiceRepository.UpdateProductInvoice(productInvoice);

                                    //hoa sua lai 
                                    productInvoice.Code = Erp.BackOffice.Helpers.Common.GetOrderNo("ProductInvoice", model.Code);
                                    productInvoiceRepository.UpdateProductInvoice(productInvoice);
                                    Erp.BackOffice.Helpers.Common.SetOrderNo("ProductInvoice");



                                    //Thêm vào quản lý chứng từ
                                    TransactionController.Create(new TransactionViewModel
                                    {
                                        TransactionModule = "ProductInvoice",
                                        TransactionCode = productInvoice.Code,
                                        TransactionName = "Bán hàng"
                                    });
                                }

                                if (ghiso(productInvoice.Id, intBrandID, Hinhthuctt) == false)
                                {
                                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                                    return Json(0);
                                }


                                scope.Complete();

                            }
                            catch (DbUpdateException)
                            {
                                TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                                return Json(0);
                            }
                        }
                    }
                    //end hoapd copy code insert

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
        public ActionResult Checkdebtsales()
        {
            var check = _settingRepository.GetSettingByKey("chophepbanno");
            string tmp = check.Value;
            if (tmp == "true")
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreatePOSIn(List<ProductInvoiceDetailViewModel> listsp, string customerId, string DiscountTab, string Hinhthuctt, Decimal MoneyPayTab, decimal ATMPayTab, decimal TransferPayTab, string IsDisAll, string DiscountAll, string NoteHDtab, bool NotPrint)
        {

            int idHD = 0;
            try
            {

                if (listsp != null && listsp.Count > 0)
                {




                    ProductInvoiceViewModel model = new ProductInvoiceViewModel();
                    model.DetailList = new List<ProductInvoiceDetailViewModel>();

                    AutoMapper.Mapper.Map(listsp, model.DetailList);
                    model.ReceiptViewModel = new ReceiptViewModel();
                    model.NextPaymentDate_Temp = DateTime.Now.AddDays(30);
                    model.ReceiptViewModel.Name = "Bán hàng - Thu tiền mặt";
                    model.ReceiptViewModel.Amount = 0;
                    if (customerId == "")
                    {
                        customerId = "0";
                    }
                    model.CustomerId = int.Parse(customerId);

                    //begin hoapd copy code insert
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


                    var json = new JavaScriptSerializer().Serialize(model);
                    if (model.DetailList.Count != 0)
                    {
                        var customerthis = customerRepository.GetCustomerById(int.Parse(customerId));

                        ProductInvoice productInvoice = null;

                        //    string output = JsonConvert.SerializeObject(model);

                        if (model.Id > 0)
                        {
                            productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                            //Kiểm tra phân quyền Chi nhánh
                            //if ((Filters.SecurityFilter.IsTrinhDuocVien() || Filters.SecurityFilter.IsAdminSystemManager() || Filters.SecurityFilter.IsStaffDrugStore() || Filters.SecurityFilter.IsAdminDrugStore()) && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == false)
                            //{
                            //    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                            //}
                        }
                        using (var scope = new TransactionScope(TransactionScopeOption.Required))
                        {
                            try
                            {
                                if (productInvoice != null)
                                {
                                    //Nếu đã ghi sổ rồi thì không được sửa
                                    if (productInvoice.IsArchive)
                                    {
                                        return Json(0);
                                    }

                                    AutoMapper.Mapper.Map(model, productInvoice);
                                }
                                else
                                {
                                    productInvoice = new ProductInvoice();

                                    AutoMapper.Mapper.Map(model, productInvoice);
                                    if (customerId == null || customerId == "" || customerId == "0")
                                    {
                                        var customernull = customerRepository.GetCustomerByCode("KHACHVANGLAI");
                                        productInvoice.CustomerName = customernull.FirstName;
                                        productInvoice.CustomerId = customernull.Id;
                                        productInvoice.CustomerPhone = customernull.Phone;
                                        productInvoice.TaxCode = customernull.TaxCode;
                                        productInvoice.Address = customernull.Address;
                                        productInvoice.BankName = customernull.BankName;
                                    }
                                    else
                                    {
                                        productInvoice.CustomerId = int.Parse(customerId);
                                    }

                                    if ((productInvoice.CustomerId == null) || (productInvoice.CustomerId == 0))
                                    {
                                        var customernull = customerRepository.GetCustomerByCode("KHACHVANGLAI");
                                        productInvoice.CustomerName = customernull.FirstName;
                                        productInvoice.CustomerId = customernull.Id;
                                        productInvoice.CustomerPhone = customernull.Phone;
                                        productInvoice.TaxCode = customernull.TaxCode;
                                        productInvoice.Address = customernull.Address;
                                        productInvoice.BankName = customernull.BankName;

                                    }
                                    productInvoice.TotalAmount = 0;
                                    foreach (var k in model.DetailList)
                                    {
                                        productInvoice.TotalAmount += k.Quantity.Value * k.Price.Value;
                                    }
                                    //3408 khách vãng lai
                                    if (productInvoice.CustomerId != 4)
                                    {
                                        productInvoice.CustomerId = customerthis.Id;
                                        productInvoice.CustomerName = customerthis.LastName + " " + customerthis.FirstName;
                                        productInvoice.CustomerPhone = customerthis.Phone;
                                        productInvoice.TaxCode = customerthis.TaxCode;
                                        productInvoice.Address = customerthis.Address;
                                        productInvoice.BankName = customerthis.BankName;
                                    }

                                    productInvoice.IsDeleted = false;
                                    productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
                                    productInvoice.CreatedDate = DateTime.Now;
                                    productInvoice.Status = Wording.OrderStatus_pending;
                                    productInvoice.BranchId = model.BranchId.HasValue ? model.BranchId.Value : 1;
                                    productInvoice.IsArchive = false;
                                    productInvoice.IsReturn = false;
                                    if (MoneyPayTab != null && MoneyPayTab != 0)
                                    {
                                        productInvoice.MoneyPay = MoneyPayTab;
                                    }
                                    else
                                    {
                                        productInvoice.MoneyPay = 0;
                                    }
                                    if (NoteHDtab.Length > 0 && NoteHDtab != null)
                                    {
                                        productInvoice.Note = NoteHDtab;
                                    }
                                    else
                                    {
                                        productInvoice.Note = "";
                                    }
                                    if (ATMPayTab != null && ATMPayTab != 0)
                                    {
                                        productInvoice.ATMPay = ATMPayTab;
                                    }
                                    else
                                    {
                                        productInvoice.ATMPay = 0;
                                    }
                                    if (TransferPayTab != null && TransferPayTab != 0)
                                    {
                                        productInvoice.TransferPay = TransferPayTab;
                                    }
                                    else
                                    {
                                        productInvoice.TransferPay = 0;
                                    }
                                    productInvoice.RemainingAmount = model.TotalAmount;
                                    productInvoice.PaidAmount = 0;
                                    productInvoice.BranchId = intBrandID.Value;
                                    productInvoice.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;
                                }
                                //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                                List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
                                model.DetailList.RemoveAll(x => x.Quantity <= 0);
                                decimal pTongtienhoadon = 0;
                                decimal pTongtiengiamgia = 0;
                                foreach (var group in model.DetailList)
                                {
                                    var product = ProductRepository.GetProductById(group.ProductId.Value);
                                    ProductInvoiceDetail tmp = new ProductInvoiceDetail();
                                    tmp.ProductId = product.Id;
                                    tmp.ProductType = product.Type;
                                    tmp.Quantity = group.Quantity;
                                    tmp.Unit = product.Unit;
                                    tmp.Price = group.Price;
                                    tmp.PromotionDetailId = group.PromotionDetailId;
                                    tmp.PromotionId = group.PromotionId;
                                    tmp.PromotionValue = group.PromotionValue;
                                    tmp.IsMoney = group.IsMoney;
                                    tmp.IsDeleted = false;
                                    tmp.CreatedUserId = WebSecurity.CurrentUserId;
                                    tmp.ModifiedUserId = WebSecurity.CurrentUserId;
                                    tmp.CreatedDate = DateTime.Now;
                                    tmp.ModifiedDate = DateTime.Now;
                                    tmp.FixedDiscount = group.FixedDiscount;
                                    tmp.FixedDiscountAmount = group.FixedDiscountAmount;
                                    tmp.IrregularDiscount = group.IrregularDiscount;
                                    tmp.IrregularDiscountAmount = group.IrregularDiscountAmount;
                                    tmp.QuantitySaleReturn = group.Quantity;
                                    tmp.CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false);
                                    tmp.IsReturn = false;
                                    tmp.LoCode = group.LoCode;
                                    tmp.ExpiryDate = group.ExpiryDate;
                                    tmp.Origin = product.Origin;
                                    tmp.TaxFeeAmount = group.TaxFeeAmount;
                                    tmp.TaxFee = group.TaxFee;
                                    //neu chuong trinh KM la HH

                                    //chỉ tính KM sp chưa được KM
                                    if (Helpers.Common.NVL_NUM_NT_NEW(tmp.PromotionId) > 0 && Helpers.Common.NVL_NUM_NT_NEW(tmp.PromotionDetailId) > 0 && Helpers.Common.NVL_NUM_NT_NEW(tmp.PromotionValue) > 0)
                                    {
                                        if (tmp.IsMoney == true)
                                        {
                                            tmp.IrregularDiscountAmount = tmp.Quantity * tmp.PromotionValue;
                                            tmp.IrregularDiscount = null;
                                        }
                                        else
                                        {
                                            tmp.IrregularDiscountAmount = (tmp.Price * tmp.Quantity) * tmp.PromotionValue / 100;
                                        }

                                    }
                                    else
                                    {
                                        if ((tmp.IsMoney == false || tmp.IsMoney == null) && (Helpers.Common.NVL_NUM_NT_NEW(tmp.IrregularDiscount) > 0))
                                        {
                                            tmp.IrregularDiscountAmount = (tmp.Price * tmp.Quantity) * Helpers.Common.NVL_NUM_NT_NEW(tmp.IrregularDiscount) / 100;
                                        }
                                        else if ((tmp.IsMoney == true) && (Helpers.Common.NVL_NUM_NT_NEW(tmp.IrregularDiscountAmount) > 0))
                                        {
                                            tmp.IrregularDiscount = null;
                                        }
                                    }
                                    //tinh lai tong tien neu co khuyen mại 


                                    pTongtienhoadon = pTongtienhoadon + tmp.Price.Value * tmp.Quantity.Value - Helpers.Common.NVL_NUM_LONG_NEW(tmp.IrregularDiscountAmount.Value);
                                    pTongtiengiamgia = pTongtiengiamgia + Helpers.Common.NVL_NUM_LONG_NEW(tmp.IrregularDiscountAmount);
                                    listNewCheckSameId.Add(tmp);
                                }
                                productInvoice.TotalAmount = pTongtienhoadon;
                                productInvoice.IrregularDiscount = pTongtiengiamgia;

                                if (DiscountTab != null && DiscountTab != "" && DiscountTab.Length > 0)
                                {
                                    char[] whitespace = new char[] { '-', '\t' };
                                    string[] arrdis = DiscountTab.Split(whitespace);
                                    if (arrdis[1] == "%")
                                    {
                                        productInvoice.DiscountTabBill = int.Parse(arrdis[0]);
                                        productInvoice.DiscountTabBillAmount = Convert.ToInt32(productInvoice.TotalAmount * (int.Parse(arrdis[0])) / 100);
                                        productInvoice.IrregularDiscount = Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.IrregularDiscount) + productInvoice.DiscountTabBillAmount.Value;
                                        productInvoice.PromotionId = Convert.ToInt32(arrdis[3]);
                                        productInvoice.PromotionDetailId = Convert.ToInt32(arrdis[2]);
                                        productInvoice.PromotionValue = Convert.ToInt32(arrdis[0]);
                                        productInvoice.IsMoney = false;
                                        productInvoice.TotalAmount = productInvoice.TotalAmount - productInvoice.DiscountTabBillAmount.Value;

                                    }
                                    else
                                    {
                                        productInvoice.DiscountTabBillAmount = decimal.Parse(arrdis[0]);
                                        productInvoice.IrregularDiscount = Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.IrregularDiscount) + productInvoice.DiscountTabBillAmount.Value;
                                        productInvoice.PromotionId = Convert.ToInt32(arrdis[3]);
                                        productInvoice.PromotionDetailId = Convert.ToInt32(arrdis[2]);
                                        productInvoice.PromotionValue = Convert.ToInt32(arrdis[0]);
                                        productInvoice.IsMoney = true;
                                        productInvoice.TotalAmount = productInvoice.TotalAmount - productInvoice.DiscountTabBillAmount.Value;
                                    }

                                }
                                if (DiscountAll != null && DiscountAll != "" && DiscountAll.Length > 0)
                                {
                                    DiscountAll = DiscountAll.Replace(".", "");
                                    productInvoice.DisCountAllTab = decimal.Parse(DiscountAll);
                                    if (IsDisAll == "1")
                                    {
                                        decimal tiengiam3 = ((productInvoice.TotalAmount * int.Parse(DiscountAll)) / 100);
                                        productInvoice.TotalAmount = productInvoice.TotalAmount - tiengiam3;
                                        productInvoice.isDisCountAllTab = true;
                                        productInvoice.IrregularDiscount = Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.IrregularDiscount) + tiengiam3;
                                    }
                                    else
                                    {
                                        productInvoice.TotalAmount = productInvoice.TotalAmount - decimal.Parse(DiscountAll);
                                        productInvoice.IrregularDiscount = Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.IrregularDiscount) + decimal.Parse(DiscountAll);
                                        productInvoice.isDisCountAllTab = false;
                                    }
                                }
                                productInvoice.IsReturn = false;
                                productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                                productInvoice.ModifiedDate = DateTime.Now;
                                productInvoice.PaidAmount = 0;
                                productInvoice.TotalAmount = Math.Round(productInvoice.TotalAmount, 0, MidpointRounding.ToEven);
                                productInvoice.RemainingAmount = productInvoice.TotalAmount;


                                if (Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.TransferPay) == 0 && Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.ATMPay) == 0)
                                {
                                    productInvoice.MoneyPay = productInvoice.TotalAmount;
                                }


                                //tính lại tổng tiền
                                //hàm edit 
                                if (model.Id > 0)
                                {
                                    productInvoiceRepository.UpdateProductInvoice(productInvoice);
                                    var listDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

                                    //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail, UsingServiceLog,UsingServiceLogDetail,ServiceReminder
                                    productInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

                                    //thêm mới toàn bộ database
                                    foreach (var item in listNewCheckSameId)
                                    {
                                        item.ProductInvoiceId = productInvoice.Id;
                                        productInvoiceRepository.InsertProductInvoiceDetail(item);
                                    }

                                    //Thêm vào quản lý chứng từ
                                    TransactionController.Create(new TransactionViewModel
                                    {
                                        TransactionModule = "ProductInvoice",
                                        TransactionCode = productInvoice.Code,
                                        TransactionName = "Bán hàng"
                                    });
                                }
                                else
                                {
                                    //hàm thêm mới
                                    productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

                                    //cập nhật lại mã hóa đơn
                                    //string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
                                    //productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
                                    //productInvoiceRepository.UpdateProductInvoice(productInvoice);

                                    //hoa sua lai 
                                    productInvoice.Code = Erp.BackOffice.Helpers.Common.GetOrderNo("ProductInvoice", model.Code);

                                    //begin kiem tra trung ma don hang

                                    var donhangtrunga = productInvoiceRepository.GetvwProductInvoiceByCode(productInvoice.Code);
                                    if (donhangtrunga != null)
                                    {
                                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                                        return Json(0);
                                    }

                                    //end kiem tra trung ma don hang



                                    productInvoiceRepository.UpdateProductInvoice(productInvoice);
                                    Erp.BackOffice.Helpers.Common.SetOrderNo("ProductInvoice");

                                    //thêm lịch sử ghi chú - cong
                                    var Note = new NoteProductInvoice();
                                    Note.ProductInvoiceId = productInvoice.Id;
                                    Note.Note = productInvoice.Note;
                                    Note.CreatedDate = productInvoice.CreatedDate;
                                    Note.CreatedUserId = productInvoice.CreatedUserId;
                                    NoteProductInvoiceRepository.InsertNoteProductInvoice(Note);


                                    //end thêm lịch sử ghi chú

                                    //Thêm vào quản lý chứng từ
                                    TransactionController.Create(new TransactionViewModel
                                    {
                                        TransactionModule = "ProductInvoice",
                                        TransactionCode = productInvoice.Code,
                                        TransactionName = "Bán hàng"
                                    });
                                    idHD = productInvoice.Id;
                                }

                                if (ghiso(productInvoice.Id, intBrandID, Hinhthuctt) == false)
                                {
                                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                                    return Json(0);
                                }



                                if ((Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.TotalAmount) <= 0)
                                    || (Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.TotalAmount) != (Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.MoneyPay) + Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.TransferPay) + Helpers.Common.NVL_NUM_LONG_NEW(productInvoice.ATMPay)))
                                    )
                                {
                                    TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                                    return Json(0);
                                }


                                scope.Complete();

                            }
                            catch (DbUpdateException)
                            {
                                TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                                return Json(0);
                            }
                        }
                    }
                    //end hoapd copy code insert

                }
                if (NotPrint == true)
                {
                    return Json(1);
                }
                else
                {
                    return Json(idHD);
                }

            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }




        //[HttpPost]
        //public ActionResult CreateNT(ProductInvoiceViewModel model)
        //{

        //    //get cookie brachID 
        //    HttpRequestBase request = this.HttpContext.Request;
        //    string strBrandID = "0";
        //    if (request.Cookies["BRANCH_ID_CookieName"] != null)
        //    {
        //        strBrandID = request.Cookies["BRANCH_ID_CookieName"].Value;
        //        if (strBrandID == "")
        //        {
        //            strBrandID = "0";
        //        }
        //    }

        //    //get  CurrentUser.branchId

        //    if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
        //    {
        //        strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
        //    }

        //    int? intBrandID = int.Parse(strBrandID);
        //    var json = new JavaScriptSerializer().Serialize(model);
        //    if (model.DetailList.Count != 0)
        //    {
        //        ProductInvoice productInvoice = null;
        //        //    string output = JsonConvert.SerializeObject(model);

        //        if (model.Id > 0)
        //        {
        //            productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
        //            //Kiểm tra phân quyền Chi nhánh
        //            //if ((Filters.SecurityFilter.IsTrinhDuocVien() || Filters.SecurityFilter.IsAdminSystemManager() || Filters.SecurityFilter.IsStaffDrugStore() || Filters.SecurityFilter.IsAdminDrugStore()) && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == false)
        //            //{
        //            //    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
        //            //}
        //        }
        //        using (var scope = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            try
        //            {
        //                if (productInvoice != null)
        //                {
        //                    //Nếu đã ghi sổ rồi thì không được sửa
        //                    if (productInvoice.IsArchive)
        //                    {
        //                        return RedirectToAction("Detail", new { Id = productInvoice.Id });
        //                    }

        //                    AutoMapper.Mapper.Map(model, productInvoice);
        //                }
        //                else
        //                {
        //                    productInvoice = new ProductInvoice();
        //                    AutoMapper.Mapper.Map(model, productInvoice);
        //                    if (model.CustomerId2 == null && model.CustomerId3 == null)
        //                    {
        //                        productInvoice.CustomerId = model.CustomerId;
        //                    }
        //                    else if (model.CustomerId == null && model.CustomerId3 == null)
        //                    {
        //                        productInvoice.CustomerId = model.CustomerId2;
        //                    }
        //                    else
        //                    {
        //                        productInvoice.CustomerId = model.CustomerId3;
        //                    }
        //                    if (productInvoice.CustomerId == null)
        //                    {
        //                        var customernull = customerRepository.GetCustomerByCode("KHACHVANGLAI");
        //                        productInvoice.CustomerName = customernull.CompanyName;
        //                        productInvoice.CustomerId = customernull.Id;

        //                    }
        //                    productInvoice.IsDeleted = false;
        //                    productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
        //                    productInvoice.CreatedDate = DateTime.Now;
        //                    productInvoice.Status = Wording.OrderStatus_pending;
        //                    productInvoice.BranchId = model.BranchId.HasValue ? model.BranchId.Value : 1;
        //                    productInvoice.IsArchive = false;
        //                    productInvoice.IsReturn = false;
        //                    productInvoice.RemainingAmount = model.TotalAmount;
        //                    productInvoice.PaidAmount = 0;
        //                    productInvoice.BranchId = intBrandID.Value;
        //                    productInvoice.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;
        //                }
        //                //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
        //                List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
        //                model.DetailList.RemoveAll(x => x.Quantity <= 0);
        //                foreach (var group in model.DetailList)
        //                {
        //                    var product = ProductRepository.GetProductById(group.ProductId.Value);
        //                    ProductInvoiceDetail tmp = new ProductInvoiceDetail();
        //                    tmp.ProductId = product.Id;
        //                    tmp.ProductType = product.Type;
        //                    tmp.Quantity = group.Quantity;
        //                    tmp.Unit = product.Unit;
        //                    tmp.Price = group.Price;
        //                    tmp.PromotionDetailId = group.PromotionDetailId;
        //                    tmp.PromotionId = group.PromotionId;
        //                    tmp.PromotionValue = group.PromotionValue;
        //                    tmp.IsDeleted = false;
        //                    tmp.CreatedUserId = WebSecurity.CurrentUserId;
        //                    tmp.ModifiedUserId = WebSecurity.CurrentUserId;
        //                    tmp.CreatedDate = DateTime.Now;
        //                    tmp.ModifiedDate = DateTime.Now;
        //                    tmp.FixedDiscount = group.FixedDiscount;
        //                    tmp.FixedDiscountAmount = group.FixedDiscountAmount;
        //                    tmp.IrregularDiscount = group.IrregularDiscount;
        //                    tmp.IrregularDiscountAmount = Convert.ToDecimal(group.IrregularDiscountAmountstr);
        //                    tmp.QuantitySaleReturn = group.Quantity;
        //                    tmp.CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false);
        //                    tmp.IsReturn = false;
        //                    tmp.LoCode = group.LoCode;
        //                    tmp.ExpiryDate = group.ExpiryDate;
        //                    tmp.Origin = product.Origin;
        //                    tmp.TaxFeeAmount = group.TaxFeeAmount;
        //                    tmp.TaxFee = group.TaxFee;

        //                    //chỉ tính KM sp chưa được KM
        //                    if (tmp.IrregularDiscountAmount == 0)
        //                    {
        //                        //mcuong.fit***********
        //                        //begin
        //                        DateTime dtnow = DateTime.Now;

        //                        // var commisioncus = commissionCusRepository.GetAllCommissionCus().Where(x=>x.StartDate<= dtnow&&x.EndDate>= dtnow).ToList();
        //                        //mcuong.fit******************************************************************************************************************************
        //                        //Begin
        //                        //lấy tất cả các các chưng trình khuyên mại đang còn hiệu lực
        //                        var commision = commisionCustomerRepository.GetListAllCommisionCustomer().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow).ToList();

        //                        //truy vấn xem sp this thuộc những chưng trình nào
        //                        foreach (var item in commision)
        //                        {
        //                            //phạp vi áp dụng
        //                            //chi nhánh*****************************************************************Type apply =3
        //                            if (item.TypeCus == "HD")
        //                            {
        //                                if (item.TypeApply == 3)
        //                                {   //ap dụng cho hóa đơn
        //                                    if (item.BranchIdApply == 0 || item.BranchIdApply == null)
        //                                    {
        //                                        if (item.Type == 4)
        //                                        {
        //                                            if (model.TotalAmount >= item.Minvalue)
        //                                            {
        //                                                var item_IrregularDiscount = item;
        //                                                if (item_IrregularDiscount.IsMoney == true)
        //                                                {
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                }
        //                                                else
        //                                                {
        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                }
        //                                                continue;
        //                                            }
        //                                        }
        //                                        if (item.Type == 3)
        //                                        {
        //                                            var item_IrregularDiscount = item;
        //                                            if (item_IrregularDiscount.IsMoney == true)
        //                                            {
        //                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                            }
        //                                            else
        //                                            {
        //                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                            }
        //                                            continue;
        //                                        }
        //                                        if (item.Type == 2)
        //                                        {
        //                                            // lấy thông tin sp đầy đủ
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == int.Parse(sp.NHOMSANPHAM_ID_LST))
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                        }
        //                                        if (item.Type == 1)
        //                                        {
        //                                            // lấy thông tin sp đầy đủ
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == sp.Id)
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        if (item.BranchIdApply == intBrandID)
        //                                        {
        //                                            if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
        //                                            {
        //                                                if (model.TotalAmount >= item.Minvalue)
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                            if (item.Type == 3)
        //                                            {
        //                                                var item_IrregularDiscount = item;
        //                                                if (item_IrregularDiscount.IsMoney == true)
        //                                                {
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                }
        //                                                else
        //                                                {
        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                }
        //                                                continue;
        //                                            }
        //                                            if (item.Type == 2)
        //                                            {
        //                                                // lấy thông tin sp đầy đủ
        //                                                if (tmp.ProductId != 0)
        //                                                {
        //                                                    var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                    if (item.ProductId == int.Parse(sp.NHOMSANPHAM_ID_LST))
        //                                                    {
        //                                                        var item_IrregularDiscount = item;
        //                                                        if (item_IrregularDiscount.IsMoney == true)
        //                                                        {
        //                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                        }
        //                                                        else
        //                                                        {
        //                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                        }
        //                                                        continue;
        //                                                    }
        //                                                }
        //                                            }
        //                                            if (item.Type == 1)
        //                                            {
        //                                                // lấy thông tin sp đầy đủ
        //                                                if (tmp.ProductId != 0)
        //                                                {
        //                                                    var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                    if (item.ProductId == sp.Id)
        //                                                    {
        //                                                        var item_IrregularDiscount = item;
        //                                                        if (item_IrregularDiscount.IsMoney == true)
        //                                                        {
        //                                                            tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                        }
        //                                                        else
        //                                                        {
        //                                                            tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                            tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                        }
        //                                                        continue;
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }

        //                                }
        //                                //khuyễn mãi theo nhóm vip
        //                                if (item.TypeApply == 2)
        //                                {

        //                                    var logvip = logVipRepository.GetvwAllLogVip().Where(x => x.is_approved == true && x.Status == "Đang sử dụng" && x.LoyaltyPointId == item.BranchIdApply).FirstOrDefault();
        //                                    if (logvip != null)
        //                                    {
        //                                        if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
        //                                        {
        //                                            if (productInvoice.CustomerId == logvip.CustomerId)
        //                                            {
        //                                                if (model.TotalAmount >= item.Minvalue)
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }

        //                                        }
        //                                        if (item.Type == 3)
        //                                        {
        //                                            if (productInvoice.CustomerId == logvip.CustomerId)
        //                                            {

        //                                                var item_IrregularDiscount = item;
        //                                                if (item_IrregularDiscount.IsMoney == true)
        //                                                {
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                }
        //                                                else
        //                                                {
        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                }
        //                                                continue;

        //                                            }

        //                                        }
        //                                        if (item.Type == 2)
        //                                        {
        //                                            // lấy thông tin sp đầy đủ
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == int.Parse(sp.NHOMSANPHAM_ID_LST))
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }

        //                                        }
        //                                        if (item.Type == 1)
        //                                        {
        //                                            // lấy thông tin sp đầy đủ
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == sp.Id)
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                        }

        //                                    }


        //                                }
        //                                //khách hàng cụ thể
        //                                if (item.TypeApply == 1)
        //                                {
        //                                    if (item.BranchIdApply == productInvoice.CustomerId)
        //                                    {
        //                                        if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
        //                                        {
        //                                            if (model.TotalAmount >= item.Minvalue)
        //                                            {
        //                                                var item_IrregularDiscount = item;
        //                                                if (item_IrregularDiscount.IsMoney == true)
        //                                                {
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                }
        //                                                else
        //                                                {
        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                }
        //                                                continue;
        //                                            }
        //                                        }
        //                                        if (item.Type == 3)
        //                                        {
        //                                            var item_IrregularDiscount = item;
        //                                            if (item_IrregularDiscount.IsMoney == true)
        //                                            {
        //                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                            }
        //                                            else
        //                                            {
        //                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                            }
        //                                            continue;
        //                                        }
        //                                        if (item.Type == 2)
        //                                        {
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == int.Parse(sp.NHOMSANPHAM_ID_LST))
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                        }
        //                                        if (item.Type == 1)
        //                                        {
        //                                            // lấy thông tin sp đầy đủ
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == sp.Id)
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                        }
        //                                    }


        //                                }

        //                            }
        //                            if (item.TypeCus == "HH")
        //                            {
        //                                if (item.TypeApply == 3)
        //                                {
        //                                    if (item.BranchIdApply == 0 || item.BranchIdApply == null)
        //                                    {
        //                                        if (item.Type == 4)
        //                                        {
        //                                            if (model.TotalAmount >= item.Minvalue)
        //                                            {
        //                                                var item_IrregularDiscount = item;
        //                                                if (item_IrregularDiscount.IsMoney == true)
        //                                                {
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                }
        //                                                else
        //                                                {
        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                }
        //                                                continue;
        //                                            }
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        if (item.BranchIdApply == intBrandID)
        //                                        {
        //                                            if (model.TotalAmount >= item.Minvalue)
        //                                            {
        //                                                var item_IrregularDiscount = item;
        //                                                if (item_IrregularDiscount.IsMoney == true)
        //                                                {
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                }
        //                                                else
        //                                                {
        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                }
        //                                                continue;
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                if (item.TypeApply == 2)
        //                                {
        //                                    var logvip = logVipRepository.GetvwAllLogVip().Where(x => x.is_approved == true && x.Status == "Đang sử dụng" && x.LoyaltyPointId == item.BranchIdApply).FirstOrDefault();
        //                                    if (logvip != null)
        //                                    {
        //                                        if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
        //                                        {
        //                                            if (productInvoice.CustomerId == logvip.CustomerId)
        //                                            {
        //                                                if (model.TotalAmount >= item.Minvalue)
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }

        //                                        }
        //                                        if (item.Type == 3)
        //                                        {
        //                                            if (productInvoice.CustomerId == logvip.CustomerId)
        //                                            {

        //                                                var item_IrregularDiscount = item;
        //                                                if (item_IrregularDiscount.IsMoney == true)
        //                                                {
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                }
        //                                                else
        //                                                {
        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                }
        //                                                continue;

        //                                            }

        //                                        }
        //                                        if (item.Type == 2)
        //                                        {
        //                                            // lấy thông tin sp đầy đủ
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == int.Parse(sp.NHOMSANPHAM_ID_LST))
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }

        //                                        }
        //                                        if (item.Type == 1)
        //                                        {
        //                                            // lấy thông tin sp đầy đủ
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == sp.Id)
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                        }

        //                                    }
        //                                }
        //                                if (item.TypeApply == 1)
        //                                {
        //                                    if (item.BranchIdApply == productInvoice.CustomerId)
        //                                    {
        //                                        if (item.Type == 4 && item.ProductId == 0 || item.Type == 4 && item.ProductId == null)
        //                                        {
        //                                            if (model.TotalAmount >= item.Minvalue)
        //                                            {
        //                                                var item_IrregularDiscount = item;
        //                                                if (item_IrregularDiscount.IsMoney == true)
        //                                                {
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                }
        //                                                else
        //                                                {
        //                                                    tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                    tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                }
        //                                                continue;
        //                                            }
        //                                        }
        //                                        if (item.Type == 3)
        //                                        {
        //                                            var item_IrregularDiscount = item;
        //                                            if (item_IrregularDiscount.IsMoney == true)
        //                                            {
        //                                                tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                            }
        //                                            else
        //                                            {
        //                                                tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                            }
        //                                            continue;
        //                                        }
        //                                        if (item.Type == 2)
        //                                        {
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == int.Parse(sp.NHOMSANPHAM_ID_LST))
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                        }
        //                                        if (item.Type == 1)
        //                                        {
        //                                            // lấy thông tin sp đầy đủ
        //                                            if (tmp.ProductId != 0)
        //                                            {
        //                                                var sp = ProductRepository.GetProductById(tmp.ProductId);
        //                                                if (item.ProductId == sp.Id)
        //                                                {
        //                                                    var item_IrregularDiscount = item;
        //                                                    if (item_IrregularDiscount.IsMoney == true)
        //                                                    {
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (tmp.Price * tmp.Quantity) * 100);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        tmp.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
        //                                                        tmp.IrregularDiscountAmount = Convert.ToInt32((tmp.Price * tmp.Quantity) * item_IrregularDiscount.CommissionValue / 100);

        //                                                    }
        //                                                    continue;
        //                                                }
        //                                            }
        //                                        }
        //                                    }


        //                                }

        //                            }

        //                        }
        //                        //tinh lai tong tien neu co khuyen mại 
        //                        productInvoice.TotalAmount -= tmp.IrregularDiscountAmount.Value;
        //                    }

        //                    listNewCheckSameId.Add(tmp);
        //                }

        //                productInvoice.IsReturn = false;
        //                productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
        //                productInvoice.ModifiedDate = DateTime.Now;
        //                productInvoice.PaidAmount = 0;
        //                productInvoice.RemainingAmount = productInvoice.TotalAmount;
        //                //tính lại tổng tiền
        //                //hàm edit 
        //                if (model.Id > 0)
        //                {
        //                    productInvoiceRepository.UpdateProductInvoice(productInvoice);
        //                    var listDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

        //                    //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail, UsingServiceLog,UsingServiceLogDetail,ServiceReminder
        //                    productInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

        //                    //thêm mới toàn bộ database
        //                    foreach (var item in listNewCheckSameId)
        //                    {
        //                        item.ProductInvoiceId = productInvoice.Id;
        //                        productInvoiceRepository.InsertProductInvoiceDetail(item);
        //                    }

        //                    //Thêm vào quản lý chứng từ
        //                    TransactionController.Create(new TransactionViewModel
        //                    {
        //                        TransactionModule = "ProductInvoice",
        //                        TransactionCode = productInvoice.Code,
        //                        TransactionName = "Bán hàng"
        //                    });
        //                }
        //                else
        //                {
        //                    //hàm thêm mới
        //                    productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

        //                    //cập nhật lại mã hóa đơn
        //                    //string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
        //                    //productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
        //                    //productInvoiceRepository.UpdateProductInvoice(productInvoice);

        //                    //hoa sua lai 
        //                    productInvoice.Code = Erp.BackOffice.Helpers.Common.GetOrderNo("ProductInvoice", model.Code);
        //                    productInvoiceRepository.UpdateProductInvoice(productInvoice);
        //                    Erp.BackOffice.Helpers.Common.SetOrderNo("ProductInvoice");



        //                    //Thêm vào quản lý chứng từ
        //                    TransactionController.Create(new TransactionViewModel
        //                    {
        //                        TransactionModule = "ProductInvoice",
        //                        TransactionCode = productInvoice.Code,
        //                        TransactionName = "Bán hàng"
        //                    });
        //                }
        //                scope.Complete();

        //            }
        //            catch (DbUpdateException)
        //            {
        //                TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
        //                return View(model);
        //            }
        //        }
        //        bool recallCreateNT = true;
        //        return RedirectToAction("CreateNT", new { recallCreateNT = true });
        //        //return View();
        //    }

        //    return View();
        //}
        [HttpPost]
        public ActionResult CreateNT2(ProductInvoiceViewModel model)
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
            var json = new JavaScriptSerializer().Serialize(model);
            if (model.DetailList.Count != 0)
            {
                ProductInvoice productInvoice = null;
                //    string output = JsonConvert.SerializeObject(model);

                if (model.Id > 0)
                {
                    productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                    //Kiểm tra phân quyền Chi nhánh
                    //if ((Filters.SecurityFilter.IsTrinhDuocVien() || Filters.SecurityFilter.IsAdminSystemManager() || Filters.SecurityFilter.IsStaffDrugStore() || Filters.SecurityFilter.IsAdminDrugStore()) && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == false)
                    //{
                    //    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                    //}
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        if (productInvoice != null)
                        {
                            //Nếu đã ghi sổ rồi thì không được sửa
                            if (productInvoice.IsArchive)
                            {
                                return RedirectToAction("Detail", new { Id = productInvoice.Id });
                            }

                            AutoMapper.Mapper.Map(model, productInvoice);
                        }
                        else
                        {
                            productInvoice = new ProductInvoice();
                            AutoMapper.Mapper.Map(model, productInvoice);
                            if (model.CustomerId2 == null && model.CustomerId3 == null)
                            {
                                productInvoice.CustomerId = model.CustomerId;
                            }
                            else if (model.CustomerId == null && model.CustomerId3 == null)
                            {
                                productInvoice.CustomerId = model.CustomerId2;
                            }
                            else
                            {
                                productInvoice.CustomerId = model.CustomerId3;
                            }
                            if (productInvoice.CustomerId == null)
                            {
                                var customernull = customerRepository.GetCustomerByCode("KHACHVANGLAI");
                                productInvoice.CustomerName = customernull.CompanyName;
                                productInvoice.CustomerId = customernull.Id;

                            }
                            productInvoice.IsDeleted = false;
                            productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
                            productInvoice.CreatedDate = DateTime.Now;
                            productInvoice.Status = Wording.OrderStatus_pending;
                            productInvoice.BranchId = model.BranchId.HasValue ? model.BranchId.Value : 1;
                            productInvoice.IsArchive = false;
                            productInvoice.IsReturn = false;
                            productInvoice.RemainingAmount = model.TotalAmount;
                            productInvoice.PaidAmount = 0;
                            productInvoice.BranchId = intBrandID.Value;
                            productInvoice.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;
                        }
                        //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                        List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
                        model.DetailList.RemoveAll(x => x.Quantity <= 0);
                        foreach (var group in model.DetailList)
                        {
                            var product = ProductRepository.GetProductById(group.ProductId.Value);
                            listNewCheckSameId.Add(new ProductInvoiceDetail
                            {
                                ProductId = product.Id,
                                ProductType = product.Type,
                                Quantity = group.Quantity,
                                Unit = product.Unit,
                                Price = group.Price,
                                PromotionDetailId = group.PromotionDetailId,
                                PromotionId = group.PromotionId,
                                PromotionValue = group.PromotionValue,
                                IsDeleted = false,
                                CreatedUserId = WebSecurity.CurrentUserId,
                                ModifiedUserId = WebSecurity.CurrentUserId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                FixedDiscount = group.FixedDiscount,
                                FixedDiscountAmount = group.FixedDiscountAmount,
                                IrregularDiscount = group.IrregularDiscount,
                                IrregularDiscountAmount = group.IrregularDiscountAmount,
                                QuantitySaleReturn = group.Quantity,
                                CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false),
                                IsReturn = false,
                                LoCode = group.LoCode,
                                ExpiryDate = group.ExpiryDate,
                                Origin = product.Origin,
                                TaxFeeAmount = group.TaxFeeAmount,
                                TaxFee = group.TaxFee
                            });
                        }

                        productInvoice.IsReturn = false;
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.PaidAmount = 0;
                        productInvoice.RemainingAmount = productInvoice.TotalAmount;
                        //hàm edit 
                        if (model.Id > 0)
                        {
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            var listDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

                            //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail, UsingServiceLog,UsingServiceLogDetail,ServiceReminder
                            productInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

                            //thêm mới toàn bộ database
                            foreach (var item in listNewCheckSameId)
                            {
                                item.ProductInvoiceId = productInvoice.Id;
                                productInvoiceRepository.InsertProductInvoiceDetail(item);
                            }

                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }
                        else
                        {
                            //hàm thêm mới
                            productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

                            //cập nhật lại mã hóa đơn
                            //string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
                            //productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
                            //productInvoiceRepository.UpdateProductInvoice(productInvoice);

                            //hoa sua lai 
                            productInvoice.Code = Erp.BackOffice.Helpers.Common.GetOrderNo("ProductInvoice", model.Code);
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            Erp.BackOffice.Helpers.Common.SetOrderNo("ProductInvoice");



                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                        return View(model);
                    }
                }
                bool recallCreateNT = true;
                return RedirectToAction("CreateNT", new { recallCreateNT });
            }

            return View();
        }
        [HttpPost]
        public ActionResult CreateNT3(ProductInvoiceViewModel model)
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
            var json = new JavaScriptSerializer().Serialize(model);
            if (model.DetailList.Count != 0)
            {
                ProductInvoice productInvoice = null;
                //    string output = JsonConvert.SerializeObject(model);

                if (model.Id > 0)
                {
                    productInvoice = productInvoiceRepository.GetProductInvoiceById(model.Id);
                    //Kiểm tra phân quyền Chi nhánh
                    //if ((Filters.SecurityFilter.IsTrinhDuocVien() || Filters.SecurityFilter.IsAdminSystemManager() || Filters.SecurityFilter.IsStaffDrugStore() || Filters.SecurityFilter.IsAdminDrugStore()) && ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + productInvoice.BranchId + ",") == false)
                    //{
                    //    return Content("Mẫu tin không tồn tại! Không có quyền truy cập!");
                    //}
                }
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        if (productInvoice != null)
                        {
                            //Nếu đã ghi sổ rồi thì không được sửa
                            if (productInvoice.IsArchive)
                            {
                                return RedirectToAction("Detail", new { Id = productInvoice.Id });
                            }

                            AutoMapper.Mapper.Map(model, productInvoice);
                        }
                        else
                        {
                            productInvoice = new ProductInvoice();
                            AutoMapper.Mapper.Map(model, productInvoice);
                            if (model.CustomerId2 == null && model.CustomerId3 == null)
                            {
                                productInvoice.CustomerId = model.CustomerId;
                            }
                            else if (model.CustomerId == null && model.CustomerId3 == null)
                            {
                                productInvoice.CustomerId = model.CustomerId2;
                            }
                            else
                            {
                                productInvoice.CustomerId = model.CustomerId3;
                            }
                            if (productInvoice.CustomerId == null)
                            {
                                var customernull = customerRepository.GetCustomerByCode("KHACHVANGLAI");
                                productInvoice.CustomerName = customernull.CompanyName;
                                productInvoice.CustomerId = customernull.Id;

                            }
                            productInvoice.IsDeleted = false;
                            productInvoice.CreatedUserId = WebSecurity.CurrentUserId;
                            productInvoice.CreatedDate = DateTime.Now;
                            productInvoice.Status = Wording.OrderStatus_pending;
                            productInvoice.BranchId = model.BranchId.HasValue ? model.BranchId.Value : 1;
                            productInvoice.IsArchive = false;
                            productInvoice.IsReturn = false;
                            productInvoice.RemainingAmount = model.TotalAmount;
                            productInvoice.PaidAmount = 0;
                            productInvoice.BranchId = intBrandID.Value;
                            productInvoice.PaymentMethod = SelectListHelper.GetSelectList_Category("FormPayment", null, "Name", null).FirstOrDefault().Value;
                        }
                        //Duyệt qua danh sách sản phẩm mới xử lý tình huống user chọn 2 sản phầm cùng id
                        List<ProductInvoiceDetail> listNewCheckSameId = new List<ProductInvoiceDetail>();
                        model.DetailList.RemoveAll(x => x.Quantity <= 0);
                        foreach (var group in model.DetailList)
                        {
                            var product = ProductRepository.GetProductById(group.ProductId.Value);
                            listNewCheckSameId.Add(new ProductInvoiceDetail
                            {
                                ProductId = product.Id,
                                ProductType = product.Type,
                                Quantity = group.Quantity,
                                Unit = product.Unit,
                                Price = group.Price,
                                PromotionDetailId = group.PromotionDetailId,
                                PromotionId = group.PromotionId,
                                PromotionValue = group.PromotionValue,
                                IsDeleted = false,
                                CreatedUserId = WebSecurity.CurrentUserId,
                                ModifiedUserId = WebSecurity.CurrentUserId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                FixedDiscount = group.FixedDiscount,
                                FixedDiscountAmount = group.FixedDiscountAmount,
                                IrregularDiscount = group.IrregularDiscount,
                                IrregularDiscountAmount = group.IrregularDiscountAmount,
                                QuantitySaleReturn = group.Quantity,
                                CheckPromotion = (group.CheckPromotion.HasValue ? group.CheckPromotion.Value : false),
                                IsReturn = false,
                                LoCode = group.LoCode,
                                ExpiryDate = group.ExpiryDate,
                                Origin = product.Origin,
                                TaxFeeAmount = group.TaxFeeAmount,
                                TaxFee = group.TaxFee
                            });
                        }

                        productInvoice.IsReturn = false;
                        productInvoice.ModifiedUserId = WebSecurity.CurrentUserId;
                        productInvoice.ModifiedDate = DateTime.Now;
                        productInvoice.PaidAmount = 0;
                        productInvoice.RemainingAmount = productInvoice.TotalAmount;
                        //hàm edit 
                        if (model.Id > 0)
                        {
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            var listDetail = productInvoiceRepository.GetAllInvoiceDetailsByInvoiceId(productInvoice.Id).ToList();

                            //xóa danh sách dữ liệu cũ dưới database gồm product invoice, productInvoiceDetail, UsingServiceLog,UsingServiceLogDetail,ServiceReminder
                            productInvoiceRepository.DeleteProductInvoiceDetail(listDetail);

                            //thêm mới toàn bộ database
                            foreach (var item in listNewCheckSameId)
                            {
                                item.ProductInvoiceId = productInvoice.Id;
                                productInvoiceRepository.InsertProductInvoiceDetail(item);
                            }

                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }
                        else
                        {
                            //hàm thêm mới
                            productInvoiceRepository.InsertProductInvoice(productInvoice, listNewCheckSameId);

                            //cập nhật lại mã hóa đơn
                            //string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
                            //productInvoice.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, productInvoice.Id);
                            //productInvoiceRepository.UpdateProductInvoice(productInvoice);

                            //hoa sua lai 
                            productInvoice.Code = Erp.BackOffice.Helpers.Common.GetOrderNo("ProductInvoice", model.Code);
                            productInvoiceRepository.UpdateProductInvoice(productInvoice);
                            Erp.BackOffice.Helpers.Common.SetOrderNo("ProductInvoice");



                            //Thêm vào quản lý chứng từ
                            TransactionController.Create(new TransactionViewModel
                            {
                                TransactionModule = "ProductInvoice",
                                TransactionCode = productInvoice.Code,
                                TransactionName = "Bán hàng"
                            });
                        }
                        scope.Complete();
                    }
                    catch (DbUpdateException)
                    {
                        TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.Error;
                        return View(model);
                    }
                }
                bool recallCreateNT = true;
                return View(model);
            }

            return View();
        }
        #region LoadProductItemNT


        public PartialViewResult LoadProductItemNT(int? OrderNo, int? ProductId, string ProductName, string Unit, int? Quantity, decimal? Price, string ProductCode, string ProductType, int? QuantityInventory,
              string LoCode, string ExpiryDate, int? TaxFee, int? Istab, string CustomerID)
        {


           

            //get cookie brachID sdd
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
            var model = new ProductInvoiceDetailViewModel();

            if (Helpers.Common.NVL_NUM_NT_NEW(ProductId) == 0)
            {
                return PartialView(model);
            }
            var varproduct = ProductRepository.GetProductById(ProductId.Value);

            model.OrderNo = OrderNo.Value;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
            model.Unit = Unit;
            model.Quantity = Helpers.Common.NVL_NUM_NT_NEW(Quantity);
            model.Price = Helpers.Common.NVL_NUM_DECIMAL_NEW(Price);
            model.ProductCode = ProductCode;
            model.strcode = "'" + ProductCode + "'";
            if (varproduct != null)
            {
                model.Size = varproduct.Size;
                model.color = varproduct.color;

            }

            model.ProductType = ProductType;
            model.QuantityInInventory = Helpers.Common.NVL_NUM_NT_NEW(QuantityInventory);
            model.PriceTest = Helpers.Common.NVL_NUM_DECIMAL_NEW(Price);
            model.LoCode = LoCode;
            model.TaxFee = TaxFee;
            model.Istab = Helpers.Common.NVL_NUM_NT_NEW(Istab);
            model.IrregularDiscount = 0;
            model.IrregularDiscountAmount = 0;
            if (!string.IsNullOrEmpty(ExpiryDate))
                model.ExpiryDate = Convert.ToDateTime(DateTime.ParseExact(ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            //giảm giá cố định
            DateTime dtnow = DateTime.Now;

            // var commisioncus = commissionCusRepository.GetAllCommissionCus().Where(x=>x.StartDate<= dtnow&&x.EndDate>= dtnow).ToList();
            //mcuong.fit******************************************************************************************************************************
            //Begin
            //lấy tất cả các các chưng trình khuyên mại đang còn hiệu lực
            //var comCus = commissionCusRepository.GetAllCommissionCus().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow && x.status == 1).ToList();
            //var test = commisionCustomerRepository.GetListAllCommisionCustomer().ToList();
            var commision = commisionCustomerRepository.GetListAllCommisionCustomer().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow && x.status == 1 && x.TypeCus == "HH");


            //truy vấn xem sp this thuộc những chưng trình nào
            foreach (var item in commision)
            {
                var comApply = commisionApplyRepository.GetListCommisionApplyByIdCus(item.CommissionCusId.Value);
                foreach (var items in comApply)
                {
                    //phạp vi áp dụng
                    //chi nhánh*****************************************************************Type apply =3
                    if (item.TypeCus == "HH")
                    {
                        if (items.Type == 3)
                        {   //tất cả sp
                            if (item.Type == 3)
                            {
                                if (items.BranchId == 0 || items.BranchId == null)
                                {
                                    //áp dụng cho mọi sp
                                    var item_IrregularDiscount = item;
                                    if (item_IrregularDiscount.IsMoney == true)
                                    {
                                        model.IsMoney = item.IsMoney;
                                        model.PromotionId = item.CommissionCusId;
                                        model.PromotionDetailId = item.Id;
                                        model.PromotionValue = item.CommissionValue;

                                        model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                        //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue * 100 / model.Price * model.Quantity);
                                    }
                                    else
                                    {
                                        model.IsMoney = item.IsMoney;
                                        model.PromotionId = item.CommissionCusId;
                                        model.PromotionDetailId = item.Id;
                                        model.PromotionValue = item.CommissionValue;

                                        model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                        model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                    }
                                    break;
                                }//chi nhanh cụ thể
                                else
                                {
                                    //kiểm tra chi nhánh 
                                    if (items.BranchId == intBrandID)
                                    {
                                        var item_IrregularDiscount = item;
                                        if (item_IrregularDiscount.IsMoney == true)
                                        {
                                            model.IsMoney = item.IsMoney;
                                            model.PromotionId = item.CommissionCusId;
                                            model.PromotionDetailId = item.Id;
                                            model.PromotionValue = item.CommissionValue;

                                            model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                            //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                        }
                                        else
                                        {
                                            model.IsMoney = item.IsMoney;
                                            model.PromotionId = item.CommissionCusId;
                                            model.PromotionDetailId = item.Id;
                                            model.PromotionValue = item.CommissionValue;

                                            model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                            model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                        }
                                        break;
                                    }


                                }

                            }//Theo Nhóm SP
                            if (item.Type == 2)
                            {
                                if (items.BranchId == 0 || items.BranchId == null)
                                {
                                    if (ProductId.HasValue)
                                    {
                                        if (ProductId.HasValue)
                                        {

                                            //List<DM_NHOMSANPHAMViewModel> nhomSp = new List<DM_NHOMSANPHAMViewModel>().Where(x => x.NHOM_CHA == null || x.NHOM_CHA == 0).ToList();
                                            //AutoMapper.Mapper.Map(QuerynhomSp, nhomSp);

                                            var sp = ProductRepository.GetProductById(ProductId.Value);
                                            string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                            foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                            {
                                                //begin xet nhóm cha
                                                var QuerynhomSp = dM_NHOMSANPHAMRepositories.GetAllDM_NHOMSANPHAM().Where(x => x.NHOM_CHA == item.ProductId).ToList();
                                                if (QuerynhomSp.Count > 0)
                                                {
                                                    foreach (var nsps in QuerynhomSp)
                                                    {
                                                        if (nsps.NHOMSANPHAM_ID == int.Parse(itemss))
                                                        {
                                                            var item_IrregularDiscount = item;
                                                            if (item_IrregularDiscount.IsMoney == true)
                                                            {
                                                                model.IsMoney = item.IsMoney;
                                                                model.PromotionId = item.CommissionCusId;
                                                                model.PromotionDetailId = item.Id;
                                                                model.PromotionValue = item.CommissionValue;

                                                                model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                            }
                                                            else
                                                            {
                                                                model.IsMoney = item.IsMoney;
                                                                model.PromotionId = item.CommissionCusId;
                                                                model.PromotionDetailId = item.Id;
                                                                model.PromotionValue = item.CommissionValue;

                                                                model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                            }
                                                            break;
                                                        }
                                                    }

                                                }
                                                //end xet nhóm cha
                                                //begin set nhóm con
                                                if (item.ProductId == int.Parse(itemss))
                                                {
                                                    var item_IrregularDiscount = item;
                                                    if (item_IrregularDiscount.IsMoney == true)
                                                    {
                                                        model.IsMoney = item.IsMoney;
                                                        model.PromotionId = item.CommissionCusId;
                                                        model.PromotionDetailId = item.Id;
                                                        model.PromotionValue = item.CommissionValue;

                                                        model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                        //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                    }
                                                    else
                                                    {
                                                        model.IsMoney = item.IsMoney;
                                                        model.PromotionId = item.CommissionCusId;
                                                        model.PromotionDetailId = item.Id;
                                                        model.PromotionValue = item.CommissionValue;

                                                        model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                        model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                    }
                                                    break;
                                                }
                                                //end xet nhóm con
                                            }

                                        }
                                    }
                                }//chi nhanh cụ thể
                                else
                                {
                                    //kiểm tra chi nhánh 
                                    if (items.BranchId == intBrandID)
                                    {
                                        if (ProductId.HasValue)
                                        {
                                            var sp = ProductRepository.GetProductById(ProductId.Value);
                                            string[] arr = sp.NHOMSANPHAM_ID_LST.Split(',');
                                            for (int i = 0; i < arr.Length; i++)
                                            {
                                                if (item.ProductId == int.Parse(arr[i]))
                                                {
                                                    var item_IrregularDiscount = item;
                                                    if (item_IrregularDiscount.IsMoney == true)
                                                    {
                                                        model.IsMoney = item.IsMoney;
                                                        model.PromotionId = item.CommissionCusId;
                                                        model.PromotionDetailId = item.Id;
                                                        model.PromotionValue = item.CommissionValue;

                                                        model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                        //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                    }
                                                    else
                                                    {
                                                        model.IsMoney = item.IsMoney;
                                                        model.PromotionId = item.CommissionCusId;
                                                        model.PromotionDetailId = item.Id;
                                                        model.PromotionValue = item.CommissionValue;

                                                        model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                        model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                    }
                                                    break;
                                                }
                                            }

                                        }
                                    }


                                }
                                // lấy thông tin sp đầy đủ


                            }// theo sp
                            if (item.Type == 1)
                            {
                                if (items.BranchId == 0 || items.BranchId == null)
                                {
                                    if (ProductId.HasValue)
                                    {
                                        var sp = ProductRepository.GetProductById(ProductId.Value);
                                        if (item.ProductId == sp.Id)
                                        {
                                            var item_IrregularDiscount = item;
                                            if (item_IrregularDiscount.IsMoney == true)
                                            {
                                                model.IsMoney = item.IsMoney;
                                                model.PromotionId = item.CommissionCusId;
                                                model.PromotionDetailId = item.Id;
                                                model.PromotionValue = item.CommissionValue;

                                                model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                            }
                                            else
                                            {
                                                model.IsMoney = item.IsMoney;
                                                model.PromotionId = item.CommissionCusId;
                                                model.PromotionDetailId = item.Id;
                                                model.PromotionValue = item.CommissionValue;

                                                model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                            }
                                            break;
                                        }
                                    }
                                }//chi nhanh cụ thể
                                else
                                {
                                    //kiểm tra chi nhánh 
                                    if (items.BranchId == intBrandID)
                                    {
                                        if (ProductId.HasValue)
                                        {
                                            var sp = ProductRepository.GetProductById(ProductId.Value);
                                            if (item.ProductId == sp.Id)
                                            {
                                                var item_IrregularDiscount = item;
                                                if (item_IrregularDiscount.IsMoney == true)
                                                {
                                                    model.IsMoney = item.IsMoney;
                                                    model.PromotionId = item.CommissionCusId;
                                                    model.PromotionDetailId = item.Id;
                                                    model.PromotionValue = item.CommissionValue;

                                                    model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                    //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                }
                                                else
                                                {
                                                    model.IsMoney = item.IsMoney;
                                                    model.PromotionId = item.CommissionCusId;
                                                    model.PromotionDetailId = item.Id;
                                                    model.PromotionValue = item.CommissionValue;

                                                    model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                    model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                }
                                                break;
                                            }
                                        }
                                    }


                                }
                                // lấy thông tin sp đầy đủ

                            }
                        }
                        else if (items.Type == 2)
                        {
                            bool iscusdiscount = false;
                            var logvip = logVipRepository.GetlistvwAllLogVip().Where(x => x.is_approved == true && x.Status == "Đang sử dụng" && x.LoyaltyPointId == items.BranchId).ToList();
                            if (logvip != null && logvip.Count() > 0)
                            {
                                for (int i = 0; i < logvip.Count(); i++)
                                {
                                    string tmpcus = (logvip[i].CustomerId.Value).ToString();
                                    if (tmpcus == CustomerID)
                                    {
                                        iscusdiscount = true;
                                    }

                                }
                                if (iscusdiscount == true)
                                {
                                    if (item.Type == 3)
                                    {

                                        //áp dụng cho mọi sp
                                        var item_IrregularDiscount = item;
                                        if (item_IrregularDiscount.IsMoney == true)
                                        {
                                            model.IsMoney = item.IsMoney;
                                            model.PromotionId = item.CommissionCusId;
                                            model.PromotionDetailId = item.Id;
                                            model.PromotionValue = item.CommissionValue;

                                            model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                            //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue * 100 / model.Price * model.Quantity);
                                        }
                                        else
                                        {
                                            model.IsMoney = item.IsMoney;
                                            model.PromotionId = item.CommissionCusId;
                                            model.PromotionDetailId = item.Id;
                                            model.PromotionValue = item.CommissionValue;

                                            model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                            model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                        }
                                        break;

                                    }//Theo Nhóm SP
                                    if (item.Type == 2)
                                    {

                                        if (ProductId.HasValue)
                                        {
                                            if (ProductId.HasValue)
                                            {
                                                var sp = ProductRepository.GetProductById(ProductId.Value);
                                                string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                                foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                                {
                                                    //begin xet nhóm cha
                                                    var QuerynhomSp = dM_NHOMSANPHAMRepositories.GetAllDM_NHOMSANPHAM().Where(x => x.NHOM_CHA == item.ProductId).ToList();
                                                    if (QuerynhomSp.Count > 0)
                                                    {
                                                        foreach (var nsps in QuerynhomSp)
                                                        {
                                                            if (nsps.NHOMSANPHAM_ID == int.Parse(itemss))
                                                            {
                                                                var item_IrregularDiscount = item;
                                                                if (item_IrregularDiscount.IsMoney == true)
                                                                {
                                                                    model.IsMoney = item.IsMoney;
                                                                    model.PromotionId = item.CommissionCusId;
                                                                    model.PromotionDetailId = item.Id;
                                                                    model.PromotionValue = item.CommissionValue;

                                                                    model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                    //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                                }
                                                                else
                                                                {
                                                                    model.IsMoney = item.IsMoney;
                                                                    model.PromotionId = item.CommissionCusId;
                                                                    model.PromotionDetailId = item.Id;
                                                                    model.PromotionValue = item.CommissionValue;

                                                                    model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                    model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                                }
                                                                break;
                                                            }
                                                        }

                                                    }
                                                    //end xet nhóm cha
                                                    //begin set nhóm con
                                                    if (item.ProductId == int.Parse(itemss))
                                                    {
                                                        var item_IrregularDiscount = item;
                                                        if (item_IrregularDiscount.IsMoney == true)
                                                        {
                                                            model.IsMoney = item.IsMoney;
                                                            model.PromotionId = item.CommissionCusId;
                                                            model.PromotionDetailId = item.Id;
                                                            model.PromotionValue = item.CommissionValue;

                                                            model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                            //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                        }
                                                        else
                                                        {
                                                            model.IsMoney = item.IsMoney;
                                                            model.PromotionId = item.CommissionCusId;
                                                            model.PromotionDetailId = item.Id;
                                                            model.PromotionValue = item.CommissionValue;

                                                            model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                            model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                        }
                                                        break;
                                                    }
                                                    //end xet nhóm con
                                                    //if (item.ProductId == int.Parse(itemss))
                                                    //{
                                                    //    var item_IrregularDiscount = item;
                                                    //    if (item_IrregularDiscount.IsMoney == true)
                                                    //    {
                                                    //        model.IsMoney = item.IsMoney;
                                                    //        model.PromotionId = item.CommissionCusId;
                                                    //        model.PromotionDetailId = item.Id;
                                                    //        model.PromotionValue = item.CommissionValue;

                                                    //        model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                    //        //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        model.IsMoney = item.IsMoney;
                                                    //        model.PromotionId = item.CommissionCusId;
                                                    //        model.PromotionDetailId = item.Id;
                                                    //        model.PromotionValue = item.CommissionValue;

                                                    //        model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                    //        model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                    //    }
                                                    //    break;
                                                    //}
                                                }

                                            }
                                        }
                                    }// theo sp
                                    if (item.Type == 1)
                                    {
                                        if (ProductId.HasValue)
                                        {
                                            var sp = ProductRepository.GetProductById(ProductId.Value);
                                            if (item.ProductId == sp.Id)
                                            {
                                                var item_IrregularDiscount = item;
                                                if (item_IrregularDiscount.IsMoney == true)
                                                {
                                                    model.IsMoney = item.IsMoney;
                                                    model.PromotionId = item.CommissionCusId;
                                                    model.PromotionDetailId = item.Id;
                                                    model.PromotionValue = item.CommissionValue;

                                                    model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                    //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                }
                                                else
                                                {
                                                    model.IsMoney = item.IsMoney;
                                                    model.PromotionId = item.CommissionCusId;
                                                    model.PromotionDetailId = item.Id;
                                                    model.PromotionValue = item.CommissionValue;

                                                    model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                    model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            string idcusdistmp = items.BranchId.ToString();
                            if (idcusdistmp == CustomerID)
                            {
                                //tất cả chi nhánh
                                if (item.Type == 3)
                                {
                                    //áp dụng cho mọi sp
                                    var item_IrregularDiscount = item;
                                    if (item_IrregularDiscount.IsMoney == true)
                                    {
                                        model.IsMoney = item.IsMoney;
                                        model.PromotionId = item.CommissionCusId;
                                        model.PromotionDetailId = item.Id;
                                        model.PromotionValue = item.CommissionValue;

                                        model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);

                                        //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue * 100 / model.Price * model.Quantity);
                                    }
                                    else
                                    {
                                        model.IsMoney = item.IsMoney;
                                        model.PromotionId = item.CommissionCusId;
                                        model.PromotionDetailId = item.Id;
                                        model.PromotionValue = item.CommissionValue;

                                        model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                        model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                    }
                                    break;

                                }//Theo Nhóm SP
                                if (item.Type == 2)
                                {
                                    if (ProductId.HasValue)
                                    {
                                        if (ProductId.HasValue)
                                        {
                                            var sp = ProductRepository.GetProductById(ProductId.Value);
                                            string[] NHOMSANPHAM_ID_LSTDC = sp.NHOMSANPHAM_ID_LST.Split(',');
                                            foreach (var itemss in NHOMSANPHAM_ID_LSTDC)
                                            {
                                                //begin xet nhóm cha
                                                var QuerynhomSp = dM_NHOMSANPHAMRepositories.GetAllDM_NHOMSANPHAM().Where(x => x.NHOM_CHA == item.ProductId).ToList();
                                                if (QuerynhomSp.Count > 0)
                                                {
                                                    foreach (var nsps in QuerynhomSp)
                                                    {
                                                        if (nsps.NHOMSANPHAM_ID == int.Parse(itemss))
                                                        {
                                                            var item_IrregularDiscount = item;
                                                            if (item_IrregularDiscount.IsMoney == true)
                                                            {
                                                                model.IsMoney = item.IsMoney;
                                                                model.PromotionId = item.CommissionCusId;
                                                                model.PromotionDetailId = item.Id;
                                                                model.PromotionValue = item.CommissionValue;

                                                                model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                            }
                                                            else
                                                            {
                                                                model.IsMoney = item.IsMoney;
                                                                model.PromotionId = item.CommissionCusId;
                                                                model.PromotionDetailId = item.Id;
                                                                model.PromotionValue = item.CommissionValue;

                                                                model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                                model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                            }
                                                            break;
                                                        }
                                                    }

                                                }
                                                //end xet nhóm cha
                                                //begin set nhóm con
                                                if (item.ProductId == int.Parse(itemss))
                                                {
                                                    var item_IrregularDiscount = item;
                                                    if (item_IrregularDiscount.IsMoney == true)
                                                    {
                                                        model.IsMoney = item.IsMoney;
                                                        model.PromotionId = item.CommissionCusId;
                                                        model.PromotionDetailId = item.Id;
                                                        model.PromotionValue = item.CommissionValue;

                                                        model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                        //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                    }
                                                    else
                                                    {
                                                        model.IsMoney = item.IsMoney;
                                                        model.PromotionId = item.CommissionCusId;
                                                        model.PromotionDetailId = item.Id;
                                                        model.PromotionValue = item.CommissionValue;

                                                        model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                        model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                    }
                                                    break;
                                                }
                                                //end xet nhóm con
                                                //if (item.ProductId == int.Parse(itemss))
                                                //{
                                                //    var item_IrregularDiscount = item;
                                                //    if (item_IrregularDiscount.IsMoney == true)
                                                //    {
                                                //        model.IsMoney = item.IsMoney;
                                                //        model.PromotionId = item.CommissionCusId;
                                                //        model.PromotionDetailId = item.Id;
                                                //        model.PromotionValue = item.CommissionValue;

                                                //        model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                //        //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                                //    }
                                                //    else
                                                //    {
                                                //        model.IsMoney = item.IsMoney;
                                                //        model.PromotionId = item.CommissionCusId;
                                                //        model.PromotionDetailId = item.Id;
                                                //        model.PromotionValue = item.CommissionValue;

                                                //        model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                //        model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                                //    }
                                                //    break;
                                                //}
                                            }

                                        }
                                    }

                                }// theo sp
                                if (item.Type == 1)
                                {
                                    if (ProductId.HasValue)
                                    {
                                        var sp = ProductRepository.GetProductById(ProductId.Value);
                                        if (item.ProductId == sp.Id)
                                        {
                                            var item_IrregularDiscount = item;
                                            if (item_IrregularDiscount.IsMoney == true)
                                            {
                                                model.IsMoney = item.IsMoney;
                                                model.PromotionId = item.CommissionCusId;
                                                model.PromotionDetailId = item.Id;
                                                model.PromotionValue = item.CommissionValue;

                                                model.IrregularDiscountAmount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                //model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue / (model.Price * model.Quantity) * 100);
                                            }
                                            else
                                            {
                                                model.IsMoney = item.IsMoney;
                                                model.PromotionId = item.CommissionCusId;
                                                model.PromotionDetailId = item.Id;
                                                model.PromotionValue = item.CommissionValue;

                                                model.IrregularDiscount = Convert.ToInt32(item_IrregularDiscount.CommissionValue);
                                                model.IrregularDiscountAmount = Convert.ToInt32((model.Price * model.Quantity) * item_IrregularDiscount.CommissionValue / 100);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


            }
            return PartialView(model);
        }
        public JsonResult LoadCustomerInvoice(string CustomerId, string CustomerName, string CustomerPhone, string TaxCode, string BankAccount, string BankName, string Address, string Note)
        {
            vwCustomer tmp = customerRepository.GetvwCustomerById(int.Parse(CustomerId));
            var model = new CustomerViewModel();
            model.Id = tmp.Id;
            model.Phone = tmp.Mobile;
            model.Address = tmp.Address;
            model.Note = tmp.Note;
            model.BankAccount = tmp.BankAccount;
            model.BankName = tmp.BankName;
            model.TaxCode = tmp.TaxCode;
            model.FullName = tmp.LastName + " " + tmp.FirstName + "-" + tmp.Code + "(" + tmp.Mobile + ")";
            return Json(model);
        }
        public JsonResult DisCountToDay()
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
            DateTime dtnow = DateTime.Now;
            List<KmHd_ViewModel> tmp = new List<KmHd_ViewModel>();
            //lấy tất cả các các chưng trình khuyên mại đang còn hiệu lực
            var commisionhh = commisionCustomerRepository.GetListAllCommisionCustomer().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow && x.status == 1 && x.TypeCus == "HH").ToList();
            if (commisionhh == null || commisionhh.Count == 0)
            {
                var commision = commisionCustomerRepository.GetListAllCommisionCustomer().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow && x.status == 1).ToList();

                //truy vấn xem sp this thuộc những chưng trình nào

                if (commision.Count() > 0)
                {
                    foreach (var item in commision)
                    {
                        if (item.Type == 4 && item.TypeCus == "HD")
                        {
                            var comApply = commisionApplyRepository.GetListCommisionApplyByIdCus(item.CommissionCusId.Value);
                            foreach (var items in comApply)
                            {

                                if (items.Type == 3)
                                {
                                    if (items.BranchId == 0 || items.BranchId == null)
                                    {
                                        KmHd_ViewModel KM = new KmHd_ViewModel();
                                        KM.id = item.Id;
                                        KM.id_cha = item.CommissionCusId.Value;
                                        KM.IsMoney = item.IsMoney;
                                        KM.CommissionValue = item.CommissionValue;
                                        KM.Minvalue = item.Minvalue;
                                        KM.TypeApply = items.Type;
                                        KM.BranchId = items.BranchId;
                                        KM.type = "ALLCN";
                                        tmp.Add(KM);

                                    }
                                    else
                                    {
                                        if (items.BranchId == intBrandID)
                                        {
                                            KmHd_ViewModel KM = new KmHd_ViewModel();
                                            KM.id = item.Id;
                                            KM.id_cha = item.CommissionCusId.Value;
                                            KM.IsMoney = item.IsMoney;
                                            KM.CommissionValue = item.CommissionValue;
                                            KM.Minvalue = item.Minvalue;
                                            KM.TypeApply = items.Type;
                                            KM.BranchId = items.BranchId;
                                            KM.type = "ALLCN";
                                            tmp.Add(KM);
                                        }
                                    }
                                }
                                else if (items.Type == 2)
                                {
                                    var logvip = logVipRepository.GetvwAllLogVip().Where(x => x.is_approved == true && x.Status == "Đang sử dụng" && x.LoyaltyPointId == items.BranchId).ToList();
                                    if (logvip != null && logvip.Count() > 0)
                                    {
                                        KmHd_ViewModel KM = new KmHd_ViewModel();
                                        KM.id = item.Id;
                                        KM.id_cha = item.CommissionCusId.Value;

                                        KM.IsMoney = item.IsMoney;
                                        KM.CommissionValue = item.CommissionValue;
                                        KM.Minvalue = item.Minvalue;
                                        KM.TypeApply = items.Type;
                                        KM.BranchId = items.BranchId;
                                        KM.type = "NV";
                                        string Idcuss = "";
                                        for (int i = 0; i < logvip.Count(); i++)
                                        {
                                            if (i == logvip.Count() - 1)
                                            {
                                                Idcuss += (logvip[i].CustomerId.Value).ToString();
                                            }
                                            else
                                            {
                                                Idcuss += (logvip[i].CustomerId.Value).ToString() + ",";
                                            }

                                        }
                                        KM.listIDcustomer = Idcuss;
                                        tmp.Add(KM);
                                    }

                                }
                                else
                                {
                                    KmHd_ViewModel KM = new KmHd_ViewModel();
                                    KM.id = item.Id;
                                    KM.id_cha = item.CommissionCusId.Value;

                                    KM.IsMoney = item.IsMoney;
                                    KM.CommissionValue = item.CommissionValue;
                                    KM.Minvalue = item.Minvalue;
                                    KM.TypeApply = items.Type;
                                    KM.BranchId = items.BranchId;
                                    string Idcuss = "";
                                    Idcuss = items.BranchId.ToString();
                                    KM.listIDcustomer = Idcuss;
                                    KM.type = "CUST";
                                    tmp.Add(KM);
                                }
                            }
                        }

                    }
                }
            }


            return Json(tmp);
        }
        #endregion
        #endregion
        public JsonResult DisCountToDayHhOfCustomerVip()
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
            DateTime dtnow = DateTime.Now;
            List<KmHangHoaVipViewModel> tmp = new List<KmHangHoaVipViewModel>();
            //lấy tất cả các các chưng trình khuyên mại đang còn hiệu lực
            var commisionhh = commisionCustomerRepository.GetListAllCommisionCustomer().Where(x => x.StartDate <= dtnow && x.EndDate >= dtnow && x.status == 1 && x.TypeCus == "HH").ToList();
            if (commisionhh != null || commisionhh.Count > 0)
            {


                //truy vấn xem sp this thuộc những chưng trình nào

                if (commisionhh.Count() > 0)
                {
                    foreach (var item in commisionhh)
                    {
                        var comApply = commisionApplyRepository.GetListCommisionApplyByIdCus(item.CommissionCusId.Value);
                        foreach (var items in comApply)
                        {
                            //phạp vi áp dụng
                            //chi nhánh*****************************************************************Type apply =3
                            if (item.TypeCus == "HH")
                            {
                                if (items.Type == 2)
                                {
                                    bool iscusdiscount = false;
                                    var logvip = logVipRepository.GetlistvwAllLogVip().Where(x => x.is_approved == true && x.Status == "Đang sử dụng" && x.LoyaltyPointId == items.BranchId).ToList();
                                    if (logvip != null && logvip.Count() > 0)
                                    {
                                        string customerstr = "";
                                        for (int i = 0; i < logvip.Count(); i++)
                                        {
                                            string tmpcus = (logvip[i].CustomerId.Value).ToString();
                                            if (i == logvip.Count() - 1)
                                            {
                                                customerstr += tmpcus;
                                            }
                                            else
                                            {
                                                customerstr += tmpcus + ",";
                                            }

                                        }
                                        if (item.Type == 3)
                                        {

                                            KmHangHoaVipViewModel KM = new KmHangHoaVipViewModel();
                                            KM.id = item.Id;
                                            KM.id_cha = item.CommissionCusId.Value;

                                            KM.IsMoney = item.IsMoney;
                                            KM.CommissionValue = item.CommissionValue;
                                            KM.TypeApply = items.Type;
                                            KM.BranchId = items.BranchId;
                                            KM.type = "ALLSP";
                                            KM.listIDcustomer = customerstr;
                                            tmp.Add(KM);
                                            continue;

                                        }//Theo Nhóm SP
                                        if (item.Type == 2)
                                        {
                                            KmHangHoaVipViewModel KM = new KmHangHoaVipViewModel();
                                            int idnhomsp = item.ProductId.Value;
                                            KM.listIdproduct = "";
                                            var QuerynhomSp = dM_NHOMSANPHAMRepositories.GetAllDM_NHOMSANPHAM().Where(x => x.NHOM_CHA == item.ProductId).ToList();
                                            if (QuerynhomSp.Count > 0)
                                            {
                                                foreach (var nsps in QuerynhomSp)
                                                {
                                                    List<Product> listproductNC = ProductRepository.GetlistAllProduct().Where(x => x.NHOMSANPHAM_ID_LST == nsps.NHOMSANPHAM_ID.ToString()).ToList();
                                                    for (int ii = 0; ii < listproductNC.Count(); ii++)
                                                    {
                                                        if (ii == listproductNC.Count() - 1)
                                                        {
                                                            KM.listIdproduct += listproductNC[ii].Id.ToString();
                                                        }
                                                        else
                                                        {
                                                            KM.listIdproduct += listproductNC[ii].Id.ToString() + ",";
                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                List<Product> listproduct = ProductRepository.GetlistAllProduct().Where(x => x.NHOMSANPHAM_ID_LST == idnhomsp.ToString()).ToList();
                                                for (int ii = 0; ii < listproduct.Count(); ii++)
                                                {
                                                    if (ii == listproduct.Count() - 1)
                                                    {
                                                        KM.listIdproduct += listproduct[ii].Id.ToString();
                                                    }
                                                    else
                                                    {
                                                        KM.listIdproduct += listproduct[ii].Id.ToString() + ",";
                                                    }

                                                }
                                            }

                                            KM.id = item.Id;
                                            KM.id_cha = item.CommissionCusId.Value;

                                            KM.IsMoney = item.IsMoney;
                                            KM.CommissionValue = item.CommissionValue;
                                            KM.TypeApply = items.Type;
                                            KM.BranchId = items.BranchId;
                                            KM.type = "NSP";
                                            KM.listIDcustomer = customerstr;
                                            tmp.Add(KM);
                                            continue;

                                        }// theo sp
                                        if (item.Type == 1)
                                        {

                                            KmHangHoaVipViewModel KM = new KmHangHoaVipViewModel();
                                            KM.listIdproduct = item.ProductId.Value.ToString();
                                            KM.id = item.Id;
                                            KM.id_cha = item.CommissionCusId.Value;
                                            KM.IsMoney = item.IsMoney;
                                            KM.CommissionValue = item.CommissionValue;
                                            KM.TypeApply = items.Type;
                                            KM.BranchId = items.BranchId;
                                            KM.type = "ONESP";
                                            KM.listIDcustomer = customerstr;
                                            tmp.Add(KM);
                                            continue;

                                        }
                                    }

                                }
                                if (items.Type == 1)
                                {
                                    string idcusdistmp = items.BranchId.ToString();

                                    //tất cả sp
                                    if (item.Type == 3)
                                    {
                                        KmHangHoaVipViewModel KM = new KmHangHoaVipViewModel();
                                        KM.id = item.Id;
                                        KM.id_cha = item.CommissionCusId.Value;

                                        KM.IsMoney = item.IsMoney;
                                        KM.CommissionValue = item.CommissionValue;
                                        KM.TypeApply = items.Type;
                                        KM.BranchId = items.BranchId;
                                        KM.type = "ALLSP";
                                        KM.listIDcustomer = idcusdistmp;
                                        tmp.Add(KM);
                                        continue;

                                    }//Theo Nhóm SP
                                    if (item.Type == 2)
                                    {
                                        KmHangHoaVipViewModel KM = new KmHangHoaVipViewModel();
                                        int idnhomsp = item.ProductId.Value;
                                        KM.listIdproduct = "";
                                        var QuerynhomSp = dM_NHOMSANPHAMRepositories.GetAllDM_NHOMSANPHAM().Where(x => x.NHOM_CHA == item.ProductId).ToList();
                                        if (QuerynhomSp.Count > 0)
                                        {
                                            foreach (var nsps in QuerynhomSp)
                                            {
                                                List<Product> listproductNC = ProductRepository.GetlistAllProduct().Where(x => x.NHOMSANPHAM_ID_LST == nsps.NHOMSANPHAM_ID.ToString()).ToList();
                                                for (int ii = 0; ii < listproductNC.Count(); ii++)
                                                {
                                                    if (ii == listproductNC.Count() - 1)
                                                    {
                                                        KM.listIdproduct += listproductNC[ii].Id.ToString();
                                                    }
                                                    else
                                                    {
                                                        KM.listIdproduct += listproductNC[ii].Id.ToString() + ",";
                                                    }

                                                }
                                            }
                                        }
                                        else
                                        {
                                            List<Product> listproduct = ProductRepository.GetlistAllProduct().Where(x => x.NHOMSANPHAM_ID_LST == idnhomsp.ToString()).ToList();
                                            for (int ii = 0; ii < listproduct.Count(); ii++)
                                            {
                                                if (ii == listproduct.Count() - 1)
                                                {
                                                    KM.listIdproduct += listproduct[ii].Id.ToString();
                                                }
                                                else
                                                {
                                                    KM.listIdproduct += listproduct[ii].Id.ToString() + ",";
                                                }

                                            }
                                        }
                                        KM.id = item.Id;
                                        KM.id_cha = item.CommissionCusId.Value;

                                        KM.IsMoney = item.IsMoney;
                                        KM.CommissionValue = item.CommissionValue;
                                        KM.TypeApply = items.Type;
                                        KM.BranchId = items.BranchId;
                                        KM.type = "NSP";
                                        KM.listIDcustomer = idcusdistmp;
                                        tmp.Add(KM);
                                        continue;

                                    }// theo sp
                                    if (item.Type == 1)
                                    {
                                        KmHangHoaVipViewModel KM = new KmHangHoaVipViewModel();
                                        KM.listIdproduct = item.ProductId.Value.ToString();
                                        KM.id = item.Id;
                                        KM.id_cha = item.CommissionCusId.Value;
                                        KM.IsMoney = item.IsMoney;
                                        KM.CommissionValue = item.CommissionValue;
                                        KM.TypeApply = items.Type;
                                        KM.BranchId = items.BranchId;
                                        KM.type = "ONESP";
                                        KM.listIDcustomer = idcusdistmp;
                                        tmp.Add(KM);
                                        continue;
                                    }
                                }
                            }
                        }

                    }
                }
            }


            return Json(tmp);
        }
        //public static void AutoCreateProductInvoice(IProductInvoiceRepository productInvoiceRepository, Customer cus, ProductOutbound model, List<ProductOutboundDetail> detail)
        //{
        //    var insert_detail = detail.Select(x => new ProductInvoiceDetail
        //    {
        //        CreatedUserId = WebSecurity.CurrentUserId,
        //        ModifiedUserId = WebSecurity.CurrentUserId,
        //        CreatedDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        FixedDiscountAmount = 0,
        //        IrregularDiscountAmount = 0,
        //        IrregularDiscount = 0,
        //        FixedDiscount = 0,
        //        IsDeleted = false,
        //        Price = x.Price,
        //        ProductId = x.ProductId.Value,
        //        ExpiryDate = x.ExpiryDate,
        //        LoCode = x.LoCode,
        //        Quantity = x.Quantity,
        //        Unit = x.Unit
        //    }).ToList();
        //    var insert = new Domain.Sale.Entities.ProductInvoice();
        //    //AutoMapper.Mapper.Map(model, productInvoice);
        //    insert.IsDeleted = false;
        //    insert.CreatedUserId = WebSecurity.CurrentUserId;
        //    insert.CreatedDate = DateTime.Now;
        //    insert.Status = Wording.OrderStatus_pending;
        //    insert.BranchId = model.BranchId.Value;
        //    insert.IsArchive = false;
        //    insert.IsReturn = false;
        //    insert.CustomerId = cus.Id;
        //    insert.CustomerPhone = cus.Phone;
        //    insert.CustomerPhone = cus.Phone;
        //    insert.Type = "invoiceDS";
        //    int taxfee = 0;
        //    int.TryParse(Helpers.Common.GetSetting("vat"), out taxfee);
        //    insert.TaxFee = taxfee;
        //    insert.StaffCreateId = model.CreatedStaffId;
        //    insert.Status = "pending";
        //    insert.FixedDiscount = 0;
        //    insert.IrregularDiscount = 0;
        //    insert.RemainingAmount = model.TotalAmount.Value;
        //    insert.PaidAmount = 0;
        //    productInvoiceRepository.InsertProductInvoice(insert, insert_detail);
        //    //cập nhật lại mã hóa đơn
        //    string prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Invoice");
        //    insert.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, insert.Id);
        //    productInvoiceRepository.UpdateProductInvoice(insert);

        //    //Thêm vào quản lý chứng từ
        //    TransactionController.Create(new TransactionViewModel
        //    {
        //        TransactionModule = "ProductInvoice",
        //        TransactionCode = insert.Code,
        //        TransactionName = "Bán hàng"
        //    });
        //}

        public static int GetPointVIP()
        {
            try
            {
                SettingRepository settingRepository = new SettingRepository(new ErpDbContext());
                ProductInvoiceRepository productInvoiceRepository = new ProductInvoiceRepository(new ErpSaleDbContext());
                var setting = settingRepository.GetSettingByKey("setting_point").Value;
                if (!Erp.BackOffice.Filters.SecurityFilter.IsAdminDrugStore())
                    return 0;
                var branch = Erp.BackOffice.Helpers.Common.CurrentUser.BranchId;
                setting = string.IsNullOrEmpty(setting) ? "0" : setting;
                var list = productInvoiceRepository.GetAllProductInvoice().AsEnumerable().Where(x => x.BranchId != null && x.IsArchive == true && x.BranchId == Convert.ToInt32(branch)).ToList().Sum(x => x.TotalAmount);
                var rf = list / Convert.ToDecimal(setting);
                string[] arrVal = rf.ToString().Split(',');
                var value = int.Parse(arrVal[0], CultureInfo.InstalledUICulture);
                return value;
            }
            catch { }

            return 0;
        }

        //#region ApprovedDrugStore

        //[HttpPost]
        //public JsonResult ApprovedDrugStore(int? week, int? year, string branchId)
        //{
        //    using (var scope = new TransactionScope(TransactionScopeOption.Required))
        //    {
        //        try
        //        {
        //            AutoMapper.Mapper.CreateMap<vwProductInvoice, ProductInvoice>();
        //            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
        //            var invoice_list = productInvoiceRepository.GetAllvwProductInvoice().AsEnumerable().Where(x => x.Year == year && x.WeekOfYear == week && ("," + branchId + ",").Contains("," + x.BranchId + ",") == true).ToList();
        //            foreach (var item in invoice_list)
        //            {
        //                var update = new ProductInvoice();

        //                AutoMapper.Mapper.Map(item, update);
        //                update.DrugStoreUserId = WebSecurity.CurrentUserId;
        //                update.DrugStoreDate = DateTime.Now;
        //                productInvoiceRepository.UpdateProductInvoice(update);
        //            }
        //            scope.Complete();

        //            return Json(new { Result = "success", Message = App_GlobalResources.Wording.UpdateSuccess },
        //                               JsonRequestBehavior.AllowGet);
        //        }
        //        catch (DbUpdateException)
        //        {
        //            return Json(new { Result = "error", Message = App_GlobalResources.Error.UpdateUnsuccess },
        //                     JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //}
        //#endregion
        //#region Approved Kế toán

        //[HttpPost]
        //public JsonResult ApprovedAccountancy(int? week, int? year, string branchId)
        //{
        //    using (var scope = new TransactionScope(TransactionScopeOption.Required))
        //    {
        //        try
        //        {
        //            AutoMapper.Mapper.CreateMap<vwProductInvoice, ProductInvoice>();
        //            //var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
        //            var invoice_list = productInvoiceRepository.GetAllvwProductInvoice().AsEnumerable()
        //                .Where(x => x.Year == year && x.WeekOfYear == week
        //                    && ("," + branchId + ",").Contains("," + x.BranchId + ",") == true).ToList();
        //            foreach (var item in invoice_list)
        //            {
        //                var update = new ProductInvoice();

        //                AutoMapper.Mapper.Map(item, update);
        //                update.AccountancyUserId = WebSecurity.CurrentUserId;
        //                update.AccountancyDate = DateTime.Now;
        //                productInvoiceRepository.UpdateProductInvoice(update);
        //            }
        //            scope.Complete();
        //            return Json(new { Result = "success", Message = App_GlobalResources.Wording.UpdateSuccess },
        //                               JsonRequestBehavior.AllowGet);
        //        }
        //        catch (DbUpdateException)
        //        {
        //            return Json(new { Result = "error", Message = App_GlobalResources.Error.UpdateUnsuccess },
        //                            JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}
        //#endregion

        public ViewResult SearchProductInvoice(int? customerId)
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

            List<InventoryViewModel> model = new List<InventoryViewModel>();
            if (customerId != null && customerId > 0)
            {
                var cus = customerRepository.GetCustomerById(customerId.Value);
                var GroupPrice = Erp.BackOffice.Helpers.SelectListHelper.GetSelectList_Category("GroupPrice", null, null);
                var _GroupPrice = GroupPrice.Where(x => ("," + cus.GroupPrice + ",").Contains("," + x.Text + ",") == true).ToList();
                var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = intBrandID, LoCode = "", ProductId = "", ExpiryDate = "" }).ToList();
                productList = productList.Where(x => x.IsSale != null && x.IsSale == true).ToList();
                foreach (var item in productList)
                {
                    var aa = _GroupPrice.Where(x => ("," + item.GroupPrice + ",").Contains("," + x.Text + ",") == true).FirstOrDefault();
                    if (aa != null)
                    {
                        item.ProductPriceOutbound = Convert.ToDecimal(aa.Value);
                    }
                }
                model = productList;

            }

            //ViewBag.productList = list_inbound_2;
            return View(model);
        }
        public ActionResult SearchProductInvoice2()
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
            var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "1", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = intBrandID, LoCode = "", ProductId = "", ExpiryDate = "" }).ToList();
            productList = productList.Where(x => x.IsSale != null && x.IsSale == true).OrderByDescending(x => x.CreatedDate).ToList();
            var pagedData = Pagination.PagedResult(productList, 1, 9);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }
        public ViewResult SearchProductInvoice3()
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

            List<InventoryViewModel> model = new List<InventoryViewModel>();

            var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = intBrandID, LoCode = "", ProductId = "", ExpiryDate = "" }).OrderByDescending(m => m.ModifiedDate).ToList();
            productList = productList.Where(x => x.IsSale != null && x.IsSale == true).OrderByDescending(x => x.CreatedDate).ToList();
            model = productList;
            //ViewBag.productList = list_inbound_2;
            return View(model);
        }
        public ViewResult Searchbarcode3()
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
            List<InventoryViewModel> model = new List<InventoryViewModel>();

            var productList = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = "", HasQuantity = "", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = intBrandID, LoCode = "", ProductId = "", ExpiryDate = "" }).ToList();
            productList = productList.Where(x => x.IsSale != null && x.IsSale == true).ToList();
            model = productList;
            //ViewBag.productList = list_inbound_2;
            return View(model);
        }

        public ViewResult SearchCustomerInvoice2()
        {
            List<CustomerViewModel> arrcus = new List<CustomerViewModel>();
            List<vwCustomer> arr = customerRepository.GetListAllvwCustomer().ToList();
            foreach (var item in arr)
            {
                CustomerViewModel tmp = new CustomerViewModel();
                tmp.Id = item.Id;
                tmp.CreatedUserId = item.CreatedUserId;
                //CreatedUserName = item.CreatedUserName,
                tmp.CreatedDate = item.CreatedDate;
                tmp.ModifiedUserId = item.ModifiedUserId;
                //ModifiedUserName = item.ModifiedUserName,
                tmp.ModifiedDate = item.ModifiedDate;
                tmp.Code = item.Code;
                tmp.CompanyName = item.CompanyName;
                tmp.Phone = item.Phone;
                tmp.Mobile = item.Mobile;
                tmp.Address = item.Address;
                tmp.Note = item.Note;
                tmp.CardCode = item.CardCode;
                tmp.SearchFullName = item.SearchFullName;
                tmp.Image = item.Image;
                tmp.Birthday = item.Birthday;
                tmp.Gender = item.Gender;
                tmp.IdCardDate = item.IdCardDate;
                tmp.IdCardIssued = item.IdCardIssued;
                tmp.IdCardNumber = item.IdCardNumber;
                tmp.CardIssuedName = item.CardIssuedName;
                tmp.FirstName = item.FirstName;
                tmp.LastName = item.LastName;
                tmp.FullName = item.LastName + " " + item.FirstName + "-" + item.Code + "<br>" + item.Mobile;
                tmp.BankAccount = item.BankAccount;
                tmp.BankName = item.BankName;
                tmp.TaxCode = item.TaxCode;
                tmp.ProvinceName = item.ProvinceName;
                tmp.DistrictName = item.DistrictName;
                tmp.WardName = item.WardName;
                tmp.DistrictId = item.DistrictId;
                arrcus.Add(tmp);
            }

            return View(arrcus);
        }
        public ViewResult SearchCustomerInvoice3()
        {
            List<CustomerViewModel> arrcus = new List<CustomerViewModel>();
            List<vwCustomer> arr = customerRepository.GetListAllvwCustomer().ToList();
            foreach (var item in arr)
            {
                CustomerViewModel tmp = new CustomerViewModel();
                tmp.Id = item.Id;
                tmp.CreatedUserId = item.CreatedUserId;
                //CreatedUserName = item.CreatedUserName,
                tmp.CreatedDate = item.CreatedDate;
                tmp.ModifiedUserId = item.ModifiedUserId;
                //ModifiedUserName = item.ModifiedUserName,
                tmp.ModifiedDate = item.ModifiedDate;
                tmp.Code = item.Code;
                tmp.CompanyName = item.CompanyName;
                tmp.Phone = item.Phone;
                tmp.Mobile = item.Mobile;
                tmp.Address = item.Address;
                tmp.Note = item.Note;
                tmp.CardCode = item.CardCode;
                tmp.SearchFullName = item.SearchFullName;
                tmp.Image = item.Image;
                tmp.Birthday = item.Birthday;
                tmp.Gender = item.Gender;
                tmp.IdCardDate = item.IdCardDate;
                tmp.IdCardIssued = item.IdCardIssued;
                tmp.IdCardNumber = item.IdCardNumber;
                tmp.CardIssuedName = item.CardIssuedName;
                tmp.FirstName = item.FirstName;
                tmp.LastName = item.LastName;
                tmp.FullName = item.LastName + " " + item.FirstName;
                tmp.BankAccount = item.BankAccount;
                tmp.BankName = item.BankName;
                tmp.TaxCode = item.TaxCode;
                tmp.ProvinceName = item.ProvinceName;
                tmp.DistrictName = item.DistrictName;
                tmp.WardName = item.WardName;
                tmp.DistrictId = item.DistrictId;
                arrcus.Add(tmp);
            }

            return View(arrcus);
        }
        public ViewResult SearchCustomerInvoice4()
        {
            List<CustomerViewModel> arrcus = new List<CustomerViewModel>();
            List<vwCustomer> arr = customerRepository.GetListAllvwCustomer().ToList();
            foreach (var item in arr)
            {
                CustomerViewModel tmp = new CustomerViewModel();
                tmp.Id = item.Id;
                tmp.CreatedUserId = item.CreatedUserId;
                //CreatedUserName = item.CreatedUserName,
                tmp.CreatedDate = item.CreatedDate;
                tmp.ModifiedUserId = item.ModifiedUserId;
                //ModifiedUserName = item.ModifiedUserName,
                tmp.ModifiedDate = item.ModifiedDate;
                tmp.Code = item.Code;
                tmp.CompanyName = item.CompanyName;
                tmp.Phone = item.Phone;
                tmp.Mobile = item.Mobile;
                tmp.Address = item.Address;
                tmp.Note = item.Note;
                tmp.CardCode = item.CardCode;
                tmp.SearchFullName = item.SearchFullName;
                tmp.Image = item.Image;
                tmp.Birthday = item.Birthday;
                tmp.Gender = item.Gender;
                tmp.IdCardDate = item.IdCardDate;
                tmp.IdCardIssued = item.IdCardIssued;
                tmp.IdCardNumber = item.IdCardNumber;
                tmp.CardIssuedName = item.CardIssuedName;
                tmp.FirstName = item.FirstName;
                tmp.LastName = item.LastName;
                tmp.FullName = item.LastName + " " + item.FirstName;
                tmp.BankAccount = item.BankAccount;
                tmp.BankName = item.BankName;
                tmp.TaxCode = item.TaxCode;
                tmp.ProvinceName = item.ProvinceName;
                tmp.DistrictName = item.DistrictName;
                tmp.WardName = item.WardName;
                tmp.DistrictId = item.DistrictId;
                arrcus.Add(tmp);
            }

            return View(arrcus);
        }

        #region ImportDonHangGui
        //Get action method
        [System.Web.Mvc.HttpGet]
        public ActionResult ImportFile()
        {
            var resultData = new List<ImportDonHangModel>();
            return View(resultData);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult ImportFile(HttpPostedFileBase file)
        {
            var resultData = new List<ImportDonHangModel>();
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

        private List<ImportDonHangModel> ReadDataFileExcel(string filePath)
        {
            var resultData = new List<ImportDonHangModel>();
            //read file
            //resultData = ReadDataFromFileExcel(path);
            Excels.Application app = new Excels.Application();
            Excels.Workbook wb = app.Workbooks.Open(filePath);
            Excels.Worksheet ws = wb.ActiveSheet;
            Excels.Range range = ws.UsedRange;
            for (int row = 1; row <= range.Rows.Count; row++)
            {
                var donhang = new ImportDonHangModel()
                {
                    STT_DEN = ((Excels.Range)range.Cells[row, 1]).Text,
                    SO_HIEU = ((Excels.Range)range.Cells[row, 2]).Text,
                    MA_DON_HANG = ((Excels.Range)range.Cells[row, 3]).Text,
                    NGAY_KG = ((Excels.Range)range.Cells[row, 4]).Text,
                    NGUOI_GUI = ((Excels.Range)range.Cells[row, 5]).Text,
                    NGUOI_NHAN = ((Excels.Range)range.Cells[row, 6]).Text,
                    DC_NHAN = ((Excels.Range)range.Cells[row, 7]).Text,
                    DT_NHAN = ((Excels.Range)range.Cells[row, 8]).Text,
                    TEN_BC_NHAN = ((Excels.Range)range.Cells[row, 9]).Text,
                    KHOI_LUONG = ((Excels.Range)range.Cells[row, 10]).Text,
                    KHOI_LUONG_QD = ((Excels.Range)range.Cells[row, 11]).Text,
                    NOI_DUNG = ((Excels.Range)range.Cells[row, 12]).Text,
                    DV_DB = ((Excels.Range)range.Cells[row, 13]).Text,
                    TRI_GIA = ((Excels.Range)range.Cells[row, 14]).Text,
                    vung_xa = ((Excels.Range)range.Cells[row, 15]).Text,
                    CUOC_DV = ((Excels.Range)range.Cells[row, 16]).Text,
                    Cuoc_COD = ((Excels.Range)range.Cells[row, 17]).Text,
                    CUOC_DVCT = ((Excels.Range)range.Cells[row, 18]).Text,
                    TIEN_VAT = ((Excels.Range)range.Cells[row, 19]).Text,
                    TONG_CUOC = ((Excels.Range)range.Cells[row, 20]).Text,
                };
                resultData.Add(donhang);

            }
            wb.Close(false);
            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            return resultData;
        }

        private List<ImportDonHangModel> ReadDataFileExcel2(string filePath)
        {
            var resultData = new List<ImportDonHangModel>();



            //readfile 

            //FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {

                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                //...
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
                        var donhang = new ImportDonHangModel()
                        {
                            STT_DEN = excelReader.GetString(0),
                            SO_HIEU = excelReader.GetString(1),
                            MA_DON_HANG = excelReader.GetString(2),
                            NGAY_KG = excelReader.GetString(3),
                            NGUOI_GUI = excelReader.GetString(4),
                            NGUOI_NHAN = excelReader.GetString(5),
                            DC_NHAN = excelReader.GetString(6),
                            DT_NHAN = excelReader.GetString(7),
                            TEN_BC_NHAN = excelReader.GetString(8),
                            KHOI_LUONG = excelReader.GetString(9),
                            KHOI_LUONG_QD = excelReader.GetString(10),
                            NOI_DUNG = excelReader.GetString(11),
                            DV_DB = excelReader.GetString(12),
                            TRI_GIA = excelReader.GetString(13),
                            vung_xa = excelReader.GetString(14),
                            CUOC_DV = excelReader.GetString(15),
                            Cuoc_COD = excelReader.GetString(16),
                            CUOC_DVCT = excelReader.GetString(17),
                            TIEN_VAT = excelReader.GetString(18),
                            TONG_CUOC = excelReader.GetString(19),
                        };
                        resultData.Add(donhang);
                        //excelReader.GetInt32(0);
                    }

                    //6. Free resources (IExcelDataReader is IDisposable)
                    excelReader.Close();
                }
            }

            return resultData;

        }

        /// <summary>
        /// Doc du lieu tu file excel vua upload insert vao db
        /// </summary>
        /// <param name="currentFile"></param>
        /// <returns></returns>

        [System.Web.Mvc.HttpPost]
        public ActionResult SaveFileExcel(string currentFile, string ghichu, string donviGN)
        {
            var path = Path.Combine(Server.MapPath("~/fileuploads/"),
            Path.GetFileName(currentFile));
            var dataExcels = ReadDataFileExcel2(path);
            var listDonHangCodeDaco = new List<string>();
            var DonHangCha = new YHL_KIENHANG_GUI();


            // goi code insert vao db
            int i = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DonHangCha.NGUOI_NHAN = WebSecurity.CurrentUserName;
                    DonHangCha.GHI_CHU = ghichu;
                    DonHangCha.DONVI_GIAONHAN = donviGN;
                    DonHangCha.NGAY_GUI = DateTime.Now;
                    DonHangCha.IsDeleted = false;
                    KIENHANG_GUIRepository.InsertYHL_KIENHANG_GUI(DonHangCha);
                    foreach (var importExeclModel in dataExcels)
                    {
                        //goi insert tung product vao bang Sale_Product
                        if (i > 0)
                        {
                            //Kiem tra trung ma sp truoc khi insert
                            var DonHangExist = KIENHANG_GUI_CTIETRepository.GetKIENHANG_GUI_CTIETBySO_HIEU(importExeclModel.SO_HIEU);

                            //Neu no null tuc la no chua co trong bang product
                            if (DonHangExist == null)
                            {
                                var donhang = new YHL_KIENHANG_GUI_CTIET();
                                donhang.KIENHANG_GUI_ID = DonHangCha.KIENHANG_GUI_ID;
                                donhang.STT_DEN = Convert.ToInt32(importExeclModel.STT_DEN);
                                donhang.IsDeleted = false;
                                donhang.SO_HIEU = importExeclModel.SO_HIEU;
                                donhang.MA_DON_HANG = importExeclModel.MA_DON_HANG;

                                string ngaykg = importExeclModel.NGAY_KG.Substring(0, 10);
                                donhang.NGAY_KG = ngaykg;
                                donhang.NGUOI_GUI = importExeclModel.NGUOI_GUI;
                                donhang.NGUOI_NHAN = importExeclModel.NGUOI_NHAN;
                                donhang.DC_NHAN = importExeclModel.DC_NHAN;
                                donhang.DT_NHAN = importExeclModel.DT_NHAN;
                                donhang.TEN_BC_NHAN = importExeclModel.TEN_BC_NHAN;
                                donhang.KHOI_LUONG = decimal.Parse(importExeclModel.KHOI_LUONG.Replace(".", ","));
                                donhang.KHOI_LUONG_QD = Convert.ToDecimal(importExeclModel.KHOI_LUONG_QD);
                                donhang.NOI_DUNG = importExeclModel.NOI_DUNG;
                                donhang.DV_DB = importExeclModel.DV_DB;
                                donhang.TRI_GIA = Convert.ToDecimal(importExeclModel.TRI_GIA);
                                donhang.vung_xa = Convert.ToInt32(importExeclModel.vung_xa);
                                donhang.CUOC_DV = Convert.ToDecimal(importExeclModel.CUOC_DV);
                                donhang.Cuoc_COD = Convert.ToDecimal(importExeclModel.Cuoc_COD);
                                donhang.CUOC_DVCT = Convert.ToDecimal(importExeclModel.CUOC_DVCT);
                                donhang.TIEN_VAT = Convert.ToDecimal(importExeclModel.TIEN_VAT);
                                donhang.TONG_CUOC = Convert.ToDecimal(importExeclModel.TONG_CUOC);
                                KIENHANG_GUI_CTIETRepository.InsertYHL_KIENHANG_GUI_CTIET(donhang);
                            }
                            else
                            {
                                listDonHangCodeDaco.Add(importExeclModel.SO_HIEU);
                            }
                        }
                        i++;
                    }
                    if (listDonHangCodeDaco.Count > 0)
                    {

                        ViewBag.ErrorMesseage = ("Số Hiệu Đơn Hàng Đã Tồn Tại: " + string.Join(", ", listDonHangCodeDaco));
                        return View("ImportFile", dataExcels);
                        //return RedirectToAction("IndexDonHang");
                    }
                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                    return Content("Fail");
                }
            }
            return RedirectToAction("DonHang");
        }


        public ViewResult IndexDonHang(int Id, string MA_DH)
        {

            var q = KIENHANG_GUI_CTIETRepository.GetAllYHL_KIENHANG_GUI_CTIET().Where(x => x.KIENHANG_GUI_ID == Id);
            var model = q.Select(item => new YHL_KIENHANG_GUI_CTIETViewModel
            {
                KIENHANG_GUI_CTIET_ID = item.KIENHANG_GUI_CTIET_ID,
                KIENHANG_GUI_ID = item.KIENHANG_GUI_ID,
                STT_DEN = item.STT_DEN,
                SO_HIEU = item.SO_HIEU,
                MA_DON_HANG = item.MA_DON_HANG,
                NGAY_KG = item.NGAY_KG,
                NGUOI_GUI = item.NGUOI_GUI,
                NGUOI_NHAN = item.NGUOI_NHAN,
                DC_NHAN = item.DC_NHAN,
                DT_NHAN = item.DT_NHAN,
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
                TRANG_THAIDONHANG_GUI = item.TRANG_THAIDONHANG_GUI,
                GHI_CHU = item.GHI_CHU
            }).OrderBy(m => m.KIENHANG_GUI_CTIET_ID).ToList();

            if (string.IsNullOrEmpty(MA_DH) == false)
            {
                MA_DH = MA_DH == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(MA_DH);

                model = model.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.MA_DON_HANG).Contains(MA_DH)).ToList();
            }
            ViewBag.Id = Id;
            return View(model);
        }

        public ViewResult DonHang()
        {
            var don_ctiet = KIENHANG_GUI_CTIETRepository.GetAllYHL_KIENHANG_GUI_CTIET();
            var q = KIENHANG_GUIRepository.GetAllYHL_KIENHANG_GUI();
            var model = q.Select(item => new YHL_KIENHANG_GUIViewModel
            {
                KIENHANG_GUI_ID = item.KIENHANG_GUI_ID,
                NGAY_GUI = item.NGAY_GUI,
                DONVI_GIAONHAN = item.DONVI_GIAONHAN,
                NGUOI_NHAN = item.NGUOI_NHAN,
                GHI_CHU = item.GHI_CHU,


            }).OrderBy(m => m.KIENHANG_GUI_ID).ToList();

            //Lấy chi tiết đơn hàng
            /* foreach (var donhang in model)
             {
                 foreach (var chitiet in don_ctiet)
                 {
                     if (chitiet.KIENHANG_GUI_ID == donhang.KIENHANG_GUI_ID)
                     {
                         var chitiet2 = new YHL_KIENHANG_GUI_CTIETViewModel();
                         AutoMapper.Mapper.Map(chitiet, chitiet2);
                         donhang.DetailList.Add(chitiet2);
                     }
                 }
             }
             */

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteDonHangGui()
        {

            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        string idDeleteAll = Request["DeleteId-checkbox"];
                        string[] arrDeleteId = idDeleteAll.Split(',');
                        for (int i = 0; i < arrDeleteId.Count(); i++)
                        {
                            var item = KIENHANG_GUIRepository.GetKIENHANG_GUIByID(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                            if (item != null)
                            {
                                var a = KIENHANG_GUI_CTIETRepository.GetAllYHL_KIENHANG_GUI_CTIET().Where(x => x.KIENHANG_GUI_ID == item.KIENHANG_GUI_ID).ToList();

                                foreach (var j in a)
                                {

                                    KIENHANG_GUI_CTIETRepository.DeleteYHL_KIENHANG_GUI_CTIET(j.KIENHANG_GUI_CTIET_ID);
                                }
                                KIENHANG_GUIRepository.DeleteYHL_KIENHANG_GUI(item.KIENHANG_GUI_ID);
                            }
                        }
                        scope.Complete();
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                        return RedirectToAction("DonHang");

                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("DonHang");
            }

        }



        #endregion

        #region ImportDonHangTra

        [System.Web.Mvc.HttpGet]
        public ActionResult ImportFileHangTra()
        {
            var resultData = new List<ImportHangTraViewModel>();
            return View(resultData);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult ImportFileHangTra(HttpPostedFileBase file)
        {
            var resultData = new List<ImportHangTraViewModel>();
            var path = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = DateTime.Now.Ticks + file.FileName;
                path = Path.Combine(Server.MapPath("~/fileuploads/"),
                Path.GetFileName(fileName));


                file.SaveAs(path);
                ViewBag.FileName = fileName;
            }

            resultData = ReadDataFileHangTra2(path);

            return View(resultData);
        }

        private List<ImportHangTraViewModel> ReadDataFileHangTra(string filePath)
        {
            var resultData = new List<ImportHangTraViewModel>();
            //read file
            //resultData = ReadDataFromFileExcel(path);
            Excels.Application app = new Excels.Application();
            Excels.Workbook wb = app.Workbooks.Open(filePath);
            Excels.Worksheet ws = wb.ActiveSheet;
            Excels.Range range = ws.UsedRange;
            for (int row = 1; row <= range.Rows.Count; row++)
            {
                var donhang = new ImportHangTraViewModel()
                {
                    STT = ((Excels.Range)range.Cells[row, 1]).Text,
                    SO_HIEU = ((Excels.Range)range.Cells[row, 2]).Text,

                    NGAY_GUI = ((Excels.Range)range.Cells[row, 3]).Text,
                    MA_DON_HANG = ((Excels.Range)range.Cells[row, 4]).Text,
                    NGUOI_NHAN = ((Excels.Range)range.Cells[row, 5]).Text,
                    DC_NHAN = ((Excels.Range)range.Cells[row, 7]).Text,
                    DT_NHAN = ((Excels.Range)range.Cells[row, 6]).Text,
                    TRI_GIA = ((Excels.Range)range.Cells[row, 8]).Text,
                    CUOC = ((Excels.Range)range.Cells[row, 9]).Text,
                    VUN = ((Excels.Range)range.Cells[row, 10]).Text,
                    CONG_THUC = ((Excels.Range)range.Cells[row, 11]).Text,
                    CUOC_EMS_HOAN = ((Excels.Range)range.Cells[row, 12]).Text,
                    CUOC_COD_HOAN = ((Excels.Range)range.Cells[row, 13]).Text,
                    PHAI_THU = ((Excels.Range)range.Cells[row, 14]).Text,
                    LYDO_HOAN = ((Excels.Range)range.Cells[row, 15]).Text,
                    NGUOI_NHAN_CUOC_HOAN = ((Excels.Range)range.Cells[row, 16]).Text,
                    KHOI_LUONG = ((Excels.Range)range.Cells[row, 17]).Text,



                };
                resultData.Add(donhang);

            }
            wb.Close(false);
            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            return resultData;
        }

        private List<ImportHangTraViewModel> ReadDataFileHangTra2(string filePath)
        {
            var resultData = new List<ImportHangTraViewModel>();



            //readfile 

            //FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {

                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                //...
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
                        var donhang = new ImportHangTraViewModel()
                        {
                            STT = excelReader.GetString(0),
                            SO_HIEU = excelReader.GetString(1),
                            MA_DON_HANG = excelReader.GetString(3),
                            NGAY_GUI = excelReader.GetString(2),
                            NGUOI_NHAN = excelReader.GetString(4),




                            DC_NHAN = excelReader.GetString(6),
                            DT_NHAN = excelReader.GetString(5),
                            TRI_GIA = excelReader.GetString(7),
                            CUOC = excelReader.GetString(8),
                            VUN = excelReader.GetString(9),
                            CONG_THUC = excelReader.GetString(10),
                            CUOC_EMS_HOAN = excelReader.GetString(11),
                            CUOC_COD_HOAN = excelReader.GetString(12),
                            PHAI_THU = excelReader.GetString(13),
                            LYDO_HOAN = excelReader.GetString(14),
                            NGUOI_NHAN_CUOC_HOAN = excelReader.GetString(15),
                            KHOI_LUONG = excelReader.GetString(16),

                        };
                        resultData.Add(donhang);
                        //excelReader.GetInt32(0);
                    }

                    //6. Free resources (IExcelDataReader is IDisposable)
                    excelReader.Close();
                }
            }

            return resultData;

        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SaveFileExcel2(string currentFile, string ghichu)
        {
            var path = Path.Combine(Server.MapPath("~/fileuploads/"),
            Path.GetFileName(currentFile));
            var dataExcels = ReadDataFileHangTra2(path);
            var listDonHangCodeDaco = new List<string>();
            var DonHangCha = new YHL_KIENHANG_TRA();


            // goi code insert vao db
            int i = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DonHangCha.NGUOI_TRA = WebSecurity.CurrentUserName;
                    DonHangCha.GHI_CHU = ghichu;
                    DonHangCha.NGAY_TRA = DateTime.Now;
                    DonHangCha.IsDeleted = false;
                    KIENHANG_TRARepository.InsertYHL_KIENHANG_TRA(DonHangCha);
                    foreach (var importExeclModel in dataExcels)
                    {
                        //goi insert tung product vao bang Sale_Product
                        if (i > 0)
                        {
                            //Kiem tra trung ma sp truoc khi insert
                            var DonHangExist = KIENHANG_TRA_CTIETRepository.GetKIENHANG_TRA_CTIETBySO_HIEU(importExeclModel.SO_HIEU);

                            //Neu no null tuc la no chua co trong bang product
                            if (DonHangExist == null)
                            {
                                var donhang = new YHL_KIENHANG_TRA_CTIET();
                                donhang.KIENHANG_TRA_ID = DonHangCha.KIENHANG_TRA_ID;
                                donhang.STT = Convert.ToInt32(importExeclModel.STT);
                                donhang.IsDeleted = false;
                                donhang.SO_HIEU = importExeclModel.SO_HIEU;
                                donhang.MA_DON_HANG = importExeclModel.MA_DON_HANG;

                                string ngaykg = importExeclModel.NGAY_GUI.Substring(0, 10);
                                donhang.NGAY_KG = ngaykg;
                                donhang.NGUOI_NHAN = importExeclModel.NGUOI_NHAN;
                                donhang.DC_NHAN = importExeclModel.DC_NHAN;
                                donhang.DT_NHAN = importExeclModel.DT_NHAN;
                                donhang.KHOI_LUONG = decimal.Parse(importExeclModel.KHOI_LUONG.Replace(".", ","));
                                donhang.TRI_GIA = Convert.ToDecimal(importExeclModel.TRI_GIA.Replace(",", string.Empty));
                                donhang.CUOC = Convert.ToDecimal(importExeclModel.CUOC.Replace(",", string.Empty));
                                donhang.VUN = importExeclModel.VUN;
                                donhang.CONG_THUC = importExeclModel.CONG_THUC;
                                donhang.CUOC_EMS_HOAN = Convert.ToDecimal(importExeclModel.CUOC_EMS_HOAN.Replace(",", string.Empty));
                                donhang.CUOC_COD_HOAN = Convert.ToDecimal(importExeclModel.CUOC_COD_HOAN.Replace(",", string.Empty));
                                donhang.PHAI_THU = Convert.ToDecimal(importExeclModel.PHAI_THU.Replace(",", string.Empty));
                                donhang.LYDO_HOAN = importExeclModel.LYDO_HOAN;
                                donhang.NGUOI_NHAN_CUOC_HOAN = importExeclModel.NGUOI_NHAN_CUOC_HOAN;
                                KIENHANG_TRA_CTIETRepository.InsertYHL_KIENHANG_TRA_CTIET(donhang);
                            }
                            else
                            {
                                listDonHangCodeDaco.Add(importExeclModel.SO_HIEU);
                            }
                        }
                        i++;
                    }
                    if (listDonHangCodeDaco.Count > 0)
                    {

                        ViewBag.ErrorMesseage = ("Số Hiệu Đơn Hàng Đã Tồn Tại: " + string.Join(", ", listDonHangCodeDaco));
                        return View("ImportFileHangTra", dataExcels);
                        //return RedirectToAction("IndexDonHang");
                    }
                    scope.Complete();
                }
                catch (DbUpdateException)
                {
                    return Content("Fail");
                }
            }
            return RedirectToAction("DonHangTra");
        }

        public ViewResult IndexDonHangTra(int Id)
        {

            var q = KIENHANG_TRA_CTIETRepository.GetAllYHL_KIENHANG_TRA_CTIET().Where(x => x.KIENHANG_TRA_ID == Id);
            var model = q.Select(item => new YHL_KIENHANG_TRA_CTIETViewModel
            {
                KIENHANG_TRA_CTIET_ID = item.KIENHANG_TRA_CTIET_ID,
                KIENHANG_TRA_ID = item.KIENHANG_TRA_ID,
                STT = item.STT,
                SO_HIEU = item.SO_HIEU,
                MA_DON_HANG = item.MA_DON_HANG,
                NGAY_KG = item.NGAY_KG,

                NGUOI_NHAN = item.NGUOI_NHAN,
                DC_NHAN = item.DC_NHAN,
                DT_NHAN = item.DT_NHAN,
                TRI_GIA = item.TRI_GIA,
                CUOC = item.CUOC,
                CUOC_COD_HOAN = item.CUOC_COD_HOAN,
                CUOC_EMS_HOAN = item.CUOC_EMS_HOAN,
                VUN = item.VUN,
                CONG_THUC = item.CONG_THUC,
                PHAI_THU = item.PHAI_THU,
                LYDO_HOAN = item.LYDO_HOAN,
                NGUOI_NHAN_CUOC_HOAN = item.NGUOI_NHAN_CUOC_HOAN,
                KHOI_LUONG = item.KHOI_LUONG,
                GHI_CHU = item.GHI_CHU
            }).OrderBy(m => m.KIENHANG_TRA_CTIET_ID).ToList();

            return View(model);
        }

        public ViewResult DonHangTra()
        {
            var don_ctiet = KIENHANG_TRA_CTIETRepository.GetAllYHL_KIENHANG_TRA_CTIET();
            var q = KIENHANG_TRARepository.GetAllYHL_KIENHANG_TRA();
            var model = q.Select(item => new YHL_KIENHANG_TRAViewModel
            {
                KIENHANG_TRA_ID = item.KIENHANG_TRA_ID,

                GHI_CHU = item.GHI_CHU,
                NGAY_TRA = item.NGAY_TRA,
                NGUOI_TRA = item.NGUOI_TRA

            }).OrderBy(m => m.KIENHANG_TRA_ID).ToList();


            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteDonHangTra()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        string idDeleteAll = Request["DeleteId-checkbox"];
                        string[] arrDeleteId = idDeleteAll.Split(',');
                        for (int i = 0; i < arrDeleteId.Count(); i++)
                        {
                            var item = KIENHANG_TRARepository.GetKIENHANG_TRAByID(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                            if (item != null)
                            {
                                var a = KIENHANG_TRA_CTIETRepository.GetAllYHL_KIENHANG_TRA_CTIET().Where(x => x.KIENHANG_TRA_ID == item.KIENHANG_TRA_ID).ToList();

                                foreach (var j in a)
                                {
                                    //j.IsDeleted = true;
                                    KIENHANG_TRA_CTIETRepository.DeleteYHL_KIENHANG_TRA_CTIET(j.KIENHANG_TRA_CTIET_ID);
                                }
                                KIENHANG_TRARepository.DeleteYHL_KIENHANG_TRA(item.KIENHANG_TRA_ID);
                            }
                        }
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;

                        scope.Complete();
                        return RedirectToAction("DonHangTra");
                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }

            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("DonHangTra");
            }
        }


        #endregion


    }
}



