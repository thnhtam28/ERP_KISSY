using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Entities;
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
using System.Net;
using Erp.Domain.Account.Helper;
using System.Transactions;

//
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Web;
//using Erp.Domain.Sale;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class LogVipController : Controller
    {
        private readonly ILogVipRepository LogVipRepository;
        private readonly IUserRepository userRepository;
        private readonly ILoyaltyPointRepository LoyaltyPointRepository;
        private readonly IYHL_KIENHANG_GUI_CTIETRepositories YHL_KIENHANG_GUI_CTIETRepositories;
        private readonly IProductInvoiceRepository ProductInvoiceRepository;
        private readonly ICustomerRepository CustomerRepository;

        public LogVipController(
            ILogVipRepository _LogVip
            , IUserRepository _user
            , ILoyaltyPointRepository _LoyaltyPoint
            , IYHL_KIENHANG_GUI_CTIETRepositories _gui_chitiet
            , IProductInvoiceRepository productInvoice
            , ICustomerRepository customer

            )
        {
            LogVipRepository = _LogVip;
            userRepository = _user;
            LoyaltyPointRepository = _LoyaltyPoint;
            YHL_KIENHANG_GUI_CTIETRepositories = _gui_chitiet;
            ProductInvoiceRepository = productInvoice;
            CustomerRepository = customer;
        }

        #region Index

        public ViewResult Index()
        {
            IEnumerable<LogVipViewModel> q = LogVipRepository.GetvwAllLogVip()
                .Select(item => new LogVipViewModel
                {
                    Id = item.Id,
                    CreatedDate = item.CreatedDate,
                    CreatedUserId = item.CreatedUserId,
                    CustomerId = item.CustomerId,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    ApprovedUserId = item.ApprovedUserId,
                    ApprovedUserName = item.ApprovedUserName,
                    ApprovedDate = item.ApprovedDate,
                    CustomerCode = item.CustomerCode,
                    TotalAmount = item.TotalAmount,
                    Status = item.Status,
                    Year = item.Year,
                    LoyaltyPointId = item.LoyaltyPointId,
                    CustomerName = item.CustomerName,
                    is_approved = item.is_approved,
                    LoyaltyPointName = item.LoyaltyPointName,
                    PhoneNumber = item.PhoneNumber


                }).OrderByDescending(m => m.ModifiedDate).ToList();

            //foreach (var i in q)
            //{
            //    if (i.CustomerName == null && i.CustomerCode == null)
            //    {
            //        i.CustomerCode = i.PhoneNumber;
            //        var p = YHL_KIENHANG_GUI_CTIETRepositories.GetAllYHL_KIENHANG_GUI_CTIET().Where(x => x.DT_NHAN == i.CustomerCode).ToList();
            //        foreach (var j in p)
            //        {
            //            i.CustomerName = j.NGUOI_NHAN;

            //        }
            //    }
            //}
            foreach (var i in q)
            {

                var service = ProductInvoiceRepository.GetAllvwProductInvoice().Where(x => x.CustomerId == i.CustomerId).AsEnumerable()
                        .Select(x => new ProductInvoiceViewModel
                        {
                            Id = x.Id,
                            CreatedDate = x.CreatedDate,
                        }).ToList();
                i.TongDonHang = service.Count();
                if (i.TongDonHang > 0)
                {
                    i.NgayMuaCuoi = service[i.TongDonHang - 1].CreatedDate.Value;
                }
                if (i.CustomerId != null)
                {
                    var cus = CustomerRepository.GetCustomerById(i.CustomerId.Value);
                    if (cus != null)
                    {
                        if (cus.Birthday.HasValue)
                        {
                            i.Birthday = cus.Birthday.Value;
                        }
                        if (cus.Gender == true)
                        {
                            i.Gender = "Nữ";
                        }
                        else
                        {
                            i.Gender = "Nam";
                        }
                    }
                   
                }

            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new LogVipViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LogVipViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var LogVip = new LogVip();
                    AutoMapper.Mapper.Map(model, LogVip);
                    LogVip.IsDeleted = false;
                    LogVip.CreatedUserId = WebSecurity.CurrentUserId;
                    LogVip.ModifiedUserId = WebSecurity.CurrentUserId;
                    LogVip.AssignedUserId = WebSecurity.CurrentUserId;
                    LogVip.CreatedDate = DateTime.Now;
                    LogVip.ModifiedDate = DateTime.Now;
                    LogVip.Year = DateTime.Now.Year;
                    LogVip.TotalAmount = model.TotalAmount;
                    //LogVip.CustomerName = model.CustomerName;
                    var check = Request["group_choice"];
                    LogVip.Status = check;
                    LogVip.is_approved = model.is_approved;
                    LogVip.ApprovedUserId = WebSecurity.CurrentUserId;


                    LogVipRepository.InsertLogVip(LogVip);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }

        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var LogVip = LogVipRepository.GetvwLogVipById(Id.Value);
            if (LogVip != null && LogVip.IsDeleted != true)
            {
                var model = new LogVipViewModel();
                AutoMapper.Mapper.Map(LogVip, model);
                LogVip.CustomerId = model.CustomerId;
                var check = Request["group_choice"];
                LogVip.Status = check;
                LogVip.Ratings = model.Ratings;
                //LogVip.ApprovedDate = model.ApprovedDate.ToString("dd/MM/yyyy");
                LogVip.TotalAmount = model.TotalAmount;

                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(LogVipViewModel model)
        {
            if (ModelState.IsValid)
            {
                //if (Request["Submit"] == "Save")
                //{
                var LogVip = LogVipRepository.GetLogVipById(model.Id);
                AutoMapper.Mapper.Map(model, LogVip);
                LogVip.ModifiedUserId = WebSecurity.CurrentUserId;
                LogVip.ModifiedDate = DateTime.Now;
                var check = Request["group_choice"];
                LogVip.Status = check;
                LogVip.is_approved = model.is_approved;
                LogVip.ApprovedUserId = WebSecurity.CurrentUserId;

                LogVipRepository.UpdateLogVip(LogVip);
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                return RedirectToAction("Index");
                //}

                //return RedirectToAction("Index");
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public ActionResult Detail(int Id)
        {
            var LogVip = LogVipRepository.GetLogVipById(Id);
            if (LogVip != null && LogVip.IsDeleted != true)
            {
                var model = new LogVipViewModel();
                AutoMapper.Mapper.Map(LogVip, model);

                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
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
                    var item = LogVipRepository.GetLogVipById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }
                        item.IsDeleted = true;
                        LogVipRepository.UpdateLogVip(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion


        #region ChangeStatus
        public ActionResult ChangeStatus(int Id)
        {
            var logvip = LogVipRepository.GetLogVipById(Id);
            if (logvip != null)
            {
                var year = logvip.Year;
                if (year == DateTime.Now.Year)
                    logvip.Status = "Đang sử dụng";
                else
                    logvip.Status = "Đã sử dụng";
                logvip.ModifiedUserId = WebSecurity.CurrentUserId;
                logvip.ModifiedDate = DateTime.Now;
                LogVipRepository.UpdateLogVip(logvip);

                TempData[Globals.SuccessMessageKey] = "Thay đổi thành công!";
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Rank
        public ActionResult Rank(int Id)
        {
            var logvip = LogVipRepository.GetvwLogVipById(Id);
            if (logvip != null)
            {
                var rank = logvip.TotalAmount;
                if (rank >= 1000000 && rank <= 10000000)
                    logvip.LoyaltyPointName = "Hạng bạc";
                else
                  if (rank >= 10000000 && rank <= 20000000)
                    logvip.LoyaltyPointName = "Hạng vàng";
                else
                         if (rank >= 20000000 && rank <= 40000000)
                    logvip.LoyaltyPointName = "Hạng kim cương";
                else
                            if (rank >= 40000000 && rank <= 100000000)
                    logvip.LoyaltyPointName = "Hạng Platinum";
                else
                                if (rank == 1000000)
                    logvip.LoyaltyPointName = "Hạng đồng";
                else
                                     if (rank >= 10000000 && rank <= 50000000)
                    logvip.LoyaltyPointName = "Hạng cao thủ";

                logvip.ModifiedUserId = WebSecurity.CurrentUserId;
                logvip.ModifiedDate = DateTime.Now;
                //LogVipRepository.UpdateLogVip(logvip);

                TempData[Globals.SuccessMessageKey] = "Thay đổi thành công!";
            }
            return RedirectToAction("Index");
        }
        #endregion



        #region Search
        public ActionResult Search(int? nhomVip, string Check)
        {
            int? branchId = 0;
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

            branchId = intBrandID;

            //branchId = branchId == null ? Helpers.Common.CurrentUser.BranchId : branchId;

            //year = year == null ? DateTime.Now.Year : year;

            /// bool Check = Request["Check"];
            //if (!Filters.SecurityFilter.IsAdmin() && branchId == 0)
            //{
            //    branchId = Helpers.Common.CurrentUser.BranchId;
            //}

            // DateTime StartDate = DateTime.Now;
            // DateTime EndDate = DateTime.Now;

            // ViewBag.DateRangeText = Helpers.Common.ConvertToDateRange(ref StartDate, ref EndDate, single, year.Value, month.Value, quarter.Value, ref week);

            //var d_startDate = DateTime.ParseExact(StartDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            //var d_endDate = DateTime.ParseExact(EndDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).ToString("yyyy-MM-dd HH:mm:ss");
            //var data=
            //if (Check == "on")
            //{

            //    var data = SqlHelper.QuerySP<ProductInvoiceViewModel>("spGetlenhang3", new
            //    {
            //        //StartDate = d_startDate,
            //        //EndDate = d_endDate,
            //        branchId = branchId,
            //        //CityId = CityId,
            //        //DistrictId = DistrictId
            //        //year = 0,
            //    }).ToList();

            //    foreach (var i in data)
            //    {
            //        if (i.FirstName == i.LastName)
            //        {
            //            i.LastName = null;
            //        }
            //        i.CustomerId = Convert.ToInt32(i.Phone);
            //        i.CustomerCode = i.CustomerId.ToString();
            //        i.CustomerName = i.FirstName;

            //    }
            //    return View(data);
            //}

            //else
            //{
            var data = SqlHelper.QuerySP<ProductInvoiceViewModel>("spGetlenhang", new
            {
                //StartDate = d_startDate,
                //EndDate = d_endDate,
                branchId = branchId,
                //CityId = CityId,
                //DistrictId = DistrictId
                // year = 0,
            }).ToList();
            if (branchId != null && branchId > 0)
            {
                data = data.Where(x => x.BranchId == branchId).ToList();
            }
            if (nhomVip != null && nhomVip > 0)
            {
                var Loyalty = LoyaltyPointRepository.GetLoyaltyPointById(nhomVip.Value);

                data = data.Where(x => x.tongmua >= Loyalty.MinMoney && x.tongmua <= Loyalty.MaxMoney).ToList();
            }
            foreach (var i in data)
            {
                var service = ProductInvoiceRepository.GetAllvwProductInvoice().Where(x => x.CustomerId == i.CustomerId).AsEnumerable()
                        .Select(x => new ProductInvoiceViewModel
                        {
                            Id = x.Id,
                            CreatedDate = x.CreatedDate,
                        }).ToList();
                i.TongDonHang = service.Count();
                i.NgayMuaCuoi = service[i.TongDonHang - 1].CreatedDate.Value;
            }
            if (!Filters.SecurityFilter.IsAdmin() && branchId == 0)
            {
                branchId = Helpers.Common.CurrentUser.BranchId;
            }

            return View(data);
            //}

        }
        #endregion
        #region Change
        public ActionResult Change(int CustomerId, decimal tongmua, int PlusPoint, int Idxephangcu, int TenHang, string loai, string CustomerCode, string CustomerName, string PhoneNumber)
        {

            var LogVip = new LogVip();// LogVipRepository.GetLogVipById(Id);

            if (loai == "moi")
            {
                var model = new LogVipViewModel();
                LogVip = new LogVip();
                //AutoMapper.Mapper.Map(ProductModel, LogVip);
                LogVip.IsDeleted = false;
                LogVip.CreatedUserId = WebSecurity.CurrentUserId;
                LogVip.ModifiedUserId = WebSecurity.CurrentUserId;
                LogVip.AssignedUserId = WebSecurity.CurrentUserId;
                LogVip.CreatedDate = DateTime.Now;
                LogVip.ModifiedDate = DateTime.Now;
                LogVip.Year = DateTime.Now.Year;
                LogVip.TotalAmount = tongmua;
                // LogVip.CustomerName = model.CustomerName;
                var check = Request["group_choice"];
                LogVip.Status = "Đang sử dụng";

                LogVip.CustomerId = CustomerId;

                // LogVip.Year = NAM;
                LogVip.ApprovedDate = DateTime.Now.Date;
                LogVip.ApprovedUserId = WebSecurity.CurrentUserId;
                LogVip.LoyaltyPointId = TenHang;
                LogVip.is_approved = true;
                LogVip.PhoneNumber = PhoneNumber;


                LogVipRepository.InsertLogVip(LogVip);





            }
            else
                if (loai == "lenhang")
            {

                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    LogVip = new LogVip();
                    var model = new LogVipViewModel();
                    //LogVip = LogVipRepository.GetLogVipById(CustomerId);
                    AutoMapper.Mapper.Map(model, LogVip);
                    LogVip.IsDeleted = false;
                    LogVip.CreatedUserId = WebSecurity.CurrentUserId;
                    LogVip.ModifiedUserId = WebSecurity.CurrentUserId;
                    LogVip.AssignedUserId = WebSecurity.CurrentUserId;
                    LogVip.CreatedDate = DateTime.Now;
                    LogVip.ModifiedDate = DateTime.Now;
                    LogVip.Year = DateTime.Now.Year;
                    LogVip.TotalAmount = tongmua;
                    var check = Request["group_choice"];
                    LogVip.Status = "Đang sử dụng";

                    LogVip.CustomerId = CustomerId;

                    // LogVip.Year = NAM;
                    LogVip.LoyaltyPointId = TenHang;
                    LogVip.is_approved = true;
                    LogVip.PhoneNumber = PhoneNumber;
                    LogVip.ApprovedDate = DateTime.Now.Date;

                    LogVip.ApprovedUserId = WebSecurity.CurrentUserId;

                    LogVipRepository.InsertLogVip(LogVip);
                    LogVip = LogVipRepository.GetLogVipById(Idxephangcu);
                    //LogVip.ApprovedDate = model.ApprovedDate;
                    if (LogVip != null)
                    {
                        LogVip.ApprovedDate = DateTime.Now.Date;
                        LogVip.Status = "Đã sử dụng";
                        LogVip.ModifiedUserId = WebSecurity.CurrentUserId;
                        LogVip.ModifiedDate = DateTime.Now;
                        LogVipRepository.UpdateLogVip(LogVip);


                    }
                    else
                    {
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                        return RedirectToAction("Index");
                    }


                    scope.Complete();
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                return RedirectToAction("Index");

            }

            //logvip.ModifiedUserId = WebSecurity.CurrentUserId;
            //    logvip.ModifiedDate = DateTime.Now;
            //    LogVipRepository.UpdateLogVip(logvip);

            TempData[Globals.SuccessMessageKey] = "Thay đổi thành công!";

            return RedirectToAction("Index");
        }
        #endregion

    }


}
