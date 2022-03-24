using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using System.IO;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web.Script.Serialization;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Account.Helper;
using Erp.BackOffice.Account.Models;
using System.Web;
using Excel;
using System.Data;
using Erp.BackOffice.Areas.Sale.Models;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CommissionCusController : Controller
    {
        private readonly ICommissionCusRepository CommissionCusRepository;
        private readonly IUserRepository userRepository;
        private readonly IvwINvoiceKMDetailRepository vwINvoiceKMDetailRepository;
        private readonly IvwINvoiceKMInvoiceRepository vwINvoiceKMInvoiceRepository;
        private readonly IProductOrServiceRepository productRepository;
        private readonly ICommisionCustomerRepository CommissionDetailRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IDM_DEALHOTRepositories dm_dealhotRepository;
        private readonly IDM_BANNER_SLIDERRepositories dm_bannersliderRepository;
        private readonly IObjectAttributeRepository ObjectAttributeRepository;
        private readonly ILoyaltyPointRepository LoyaltyPoint;
        private readonly ICustomerRepository customerRepository;
        private readonly IDM_NHOMSANPHAMRepositories dM_NHOMSANPHAMRepositories;
        private readonly ICommisionApplyRepository commisionApplyRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;

        public CommissionCusController(
            ICommissionCusRepository _CommissionCus
            , IUserRepository _user
            , IvwINvoiceKMDetailRepository _vwINvoiceKMDetailRepository
            , IvwINvoiceKMInvoiceRepository _vwINvoiceKMInvoiceRepository
             , IProductOrServiceRepository _Product
            , ICommisionCustomerRepository CommissionDetail
        , IBranchRepository branch
            , IDM_DEALHOTRepositories dm_dealhot
            , IDM_BANNER_SLIDERRepositories bannerslider
             , IObjectAttributeRepository _ObjectAttribute
            , ILoyaltyPointRepository _LoyaltyPoint
            , ICustomerRepository _customerRepository
            , IDM_NHOMSANPHAMRepositories _DM_NHOMSANPHAMRepositories
            , ICommisionApplyRepository _commisionApplyRepository
            , ITemplatePrintRepository templatePrint
             )
        {
            CommissionCusRepository = _CommissionCus;
            vwINvoiceKMDetailRepository = _vwINvoiceKMDetailRepository;
            vwINvoiceKMInvoiceRepository = _vwINvoiceKMInvoiceRepository;
            userRepository = _user;
            productRepository = _Product;
            CommissionDetailRepository = CommissionDetail;
            branchRepository = branch;
            dm_dealhotRepository = dm_dealhot;
            dm_bannersliderRepository = bannerslider;
            LoyaltyPoint = _LoyaltyPoint;
            customerRepository = _customerRepository;
            dM_NHOMSANPHAMRepositories = _DM_NHOMSANPHAMRepositories;
            commisionApplyRepository = _commisionApplyRepository;
            templatePrintRepository = templatePrint;
        }
        #region List Category
        //IEnumerable<CommissionCusViewModel> getCommission(string ApplyFor)
        //{
        //    IEnumerable<CommissionCusViewModel> listCategory = new List<CommissionCusViewModel>();
        //    var model = CommissionCusRepository.GetAllCommissionCus()
        //        .Where(item => item.ApplyFor == ApplyFor)
        //        .OrderByDescending(m => m.CreatedDate).ToList();

        //    listCategory = AutoMapper.Mapper.Map(model, listCategory);

        //    return listCategory;
        //}

        //public ViewResult CommissionDrugStore()
        //{
        //    string ApplyFor = "DrugStore";
        //    ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
        //    ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
        //    ViewBag.ApplyFor = ApplyFor;
        //    ViewBag.ActionName = "CommissionDrugStore";

        //    return View("Index", getCommission(ApplyFor));
        //}

        //public ViewResult CommissionCustomer()
        //{
        //    string ApplyFor = "Customer";
        //    ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
        //    ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
        //    ViewBag.ApplyFor = ApplyFor;
        //    ViewBag.ActionName = "CommissionCustomer";

        //    return View("Index", getCommission(ApplyFor));
        //}


        public ActionResult OffUser(int userId)
        {
            if (ModelState.IsValid)
            {
                var user = CommissionCusRepository.GetCommissionCusById(userId);
                user.status = 2;
                CommissionCusRepository.UpdateCommissionCus(user);
            }
            return RedirectToAction("/Index");
        }


        public ActionResult ActiveUser(int userId)
        {
            if (ModelState.IsValid)
            {
                var user = CommissionCusRepository.GetCommissionCusById(userId);
                var comCus = CommissionCusRepository.GetAllvwCommissionCus().Where(x => x.status == 1).FirstOrDefault();
                if (comCus == null)
                {

                    user.status = 1;
                    CommissionCusRepository.UpdateCommissionCus(user);
                }
                else
                {
                    if (user.Id != comCus.Id)
                    {

                        TempData[Globals.FailedMessageKey] = "Có chương trình khuyến mãi khác đang chạy. Một thời điểm chỉ được có một chương trình khuyến mãi.";
                    }
                    else
                    {
                        user.status = 1;
                        CommissionCusRepository.UpdateCommissionCus(user);
                    }
                }



            }
            return RedirectToAction("/Index");
        }
        //[HttpPost]
        //[ValidateInput(false)]
        //public JsonResult CheckActive(int? id)
        //{
        //    var comCus = CommissionCusRepository.GetAllvwCommissionCus().Where(x => x.status == 1).FirstOrDefault();

        //    var comCus2 = CommissionCusRepository.GetAllvwCommissionCus().Where(x => x.Id == id).FirstOrDefault();

        //    if(comCus2 != comCus)
        //    {
        //        return Json(1);
        //    }
        //    else
        //    {
        //        return Json(0);
        //    }
        //}
        public ViewResult Index(string txtSearch, string Type)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            IEnumerable<CommissionCusViewModel> q = CommissionCusRepository.GetAllCommissionCus()
                .Select(item => new CommissionCusViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    ApplyFor = item.ApplyFor,
                    EndDate = item.EndDate,
                    Note = item.Note,
                    StartDate = item.StartDate,
                    Type = item.Type,
                    status = item.status,
                    //status1 = item.status1,
                    //TIEN_HANG = item.TIEN_HANG,
                    //TIEN_KM = item.TIEN_KM

                }).OrderByDescending(m => m.ModifiedDate);
            if (string.IsNullOrEmpty(txtSearch) == false || string.IsNullOrEmpty(Type) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);
                Type = Type == "" ? "~" : Type.ToLower();
                //txtPhone = txtPhone == "" ? "~" : txtPhone.ToLower();
                //txtEmail = txtEmail == "" ? "~" : txtEmail.ToLower();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Type.ToLowerOrEmpty().Contains(Type));
            }

            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan())
            {
                q = q.Where(item => ("," + user.DrugStore + ",").Contains("," + item.ApplyFor + ",") == true);
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }



        public ViewResult KMCT(int? userId)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            var qq = CommissionCusRepository.GetListAllvwCommissionCus_ch().Where(x => x.Id == userId).ToList();
            IEnumerable<CommissionCusViewModel> q = CommissionCusRepository.GetAllvwCommissionCus_ch().Where(x => x.Id == userId)
                .Select(item => new CommissionCusViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    ApplyFor = item.ApplyFor,
                    EndDate = item.EndDate,
                    Note = item.Note,
                    StartDate = item.StartDate,
                    Type = item.Type,
                    status = item.status,
                    status1 = item.status1,
                    TIEN_HANG = item.TIEN_HANG,
                    TIEN_KM = item.TIEN_KM,
                    TEN_CUAHNG = item.TEN_CUAHNG,
                    TEN_CTKM = item.TEN_CTKM

                }).OrderByDescending(m => m.ModifiedDate);



            if (!Filters.SecurityFilter.IsAdmin() && !Filters.SecurityFilter.IsKeToan())
            {
                q = q.Where(item => ("," + user.DrugStore + ",").Contains("," + item.ApplyFor + ",") == true);
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        public ViewResult DisCountDetailInvoice(int? Id)
        {
            var user = userRepository.GetByvwUserName(Helpers.Common.CurrentUser.UserName);
            //List<vwINvoiceKMDetail> qq = vwINvoiceKMDetailRepository.GetAllListvwINvoiceKMDetail().ToList();
            //List<vwINvoiceKMDetailViewModel> model = new List<vwINvoiceKMDetailViewModel>();
            //AutoMapper.Mapper.Map(q, model);
            var cmcus = CommissionCusRepository.GetCommissionCusById(Id.Value);
            if (cmcus.Type == "HH")
            {
                List<CommissionCusViewModel> listtmp = new List<CommissionCusViewModel>();
                List<vwINvoiceKMDetail> q = vwINvoiceKMDetailRepository.GetAllListvwINvoiceKMDetail().Where(x => x.IdCus == Id.Value).ToList();
                if (q != null && q.Count != 0)
                {
                    CommissionCus namecus = CommissionCusRepository.GetCommissionCusById(q[0].IdCus);
                    foreach (var item in q)
                    {
                        CommissionCusViewModel tmp = new CommissionCusViewModel();
                        tmp.STT = item.STT.ToString();
                        tmp.IdCus = item.IdCus.ToString();
                        tmp.BranchName = item.BranchName.ToString();
                        if (item.IsMoney == true)
                        {
                            tmp.IrregularDiscount = item.IrregularDiscount.ToString("N0") + " đ";
                        }
                        else
                        {
                            tmp.IrregularDiscount = item.IrregularDiscount.ToString() + " %";
                        }

                        tmp.IrregularDiscountAmount = item.IrregularDiscountAmount;
                        tmp.SumAmount = item.SumAmount;
                        tmp.nameCommitionCus = namecus.Name;
                        listtmp.Add(tmp);
                    }
                    ViewBag.SuccessMessage = TempData["SuccessMessage"];
                    ViewBag.FailedMessage = TempData["FailedMessage"];
                    ViewBag.AlertMessage = TempData["AlertMessage"];
                    return View(listtmp);
                }
                else
                {
                    return View(listtmp);
                }

            }
            else
            {
                List<CommissionCusViewModel> listtmp2 = new List<CommissionCusViewModel>();
                List<vwINvoiceKMInvoice> qq = vwINvoiceKMInvoiceRepository.GetAllListvwINvoiceKMInvoice().Where(x => x.IdCus == Id.Value).ToList();
                if (qq != null && qq.Count != 0)
                {
                    CommissionCus namecus = CommissionCusRepository.GetCommissionCusById(qq[0].IdCus);
                    foreach (var item in qq)
                    {
                        CommissionCusViewModel tmp = new CommissionCusViewModel();
                        tmp.STT = item.STT.ToString();
                        tmp.IdCus = item.IdCus.ToString();
                        tmp.BranchName = item.BranchName.ToString();
                        if (item.IsMoney == true)
                        {
                            tmp.IrregularDiscount = item.DiscountTabBill.ToString("N0") + " đ";
                        }
                        else
                        {
                            tmp.IrregularDiscount = item.DiscountTabBill.ToString() + " %";
                        }
                        tmp.nameCommitionCus = namecus.Name;
                        tmp.IrregularDiscountAmount = item.DiscountTabBillAmount;
                        tmp.SumAmount = item.SumAmount;
                        listtmp2.Add(tmp);
                    }
                    ViewBag.SuccessMessage = TempData["SuccessMessage"];
                    ViewBag.FailedMessage = TempData["FailedMessage"];
                    ViewBag.AlertMessage = TempData["AlertMessage"];
                    return View(listtmp2);
                }
                else
                {
                    return View(listtmp2);
                }

            }







        }

        public ViewResult IndexSearch(int? CommissionCusId)
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            if (CommissionCusId != null && CommissionCusId > 0)
            {
                var warehouse = CommissionCusRepository.GetCommissionCusById(CommissionCusId.Value);
                List<string> listCategories = new List<string>();
                if (!string.IsNullOrEmpty(warehouse.Type))
                {
                    listCategories = warehouse.Type.Split(',').ToList();
                }
                var productList = productRepository.GetAllvwProduct()
                    .Select(item => new ProductViewModel
                    {
                        Type = item.Type,
                        Code = item.Code,
                        Barcode = item.Barcode,
                        Name = item.Name,
                        Id = item.Id,
                        CategoryCode = item.CategoryCode,
                        PriceInbound = item.PriceInbound,
                        Unit = item.Unit,
                        QuantityTotalInventory = item.QuantityTotalInventory == null ? 0 : item.QuantityTotalInventory,
                        Image_Name = item.Image_Name,
                        ProductGroup = item.ProductGroup,
                        Origin = item.Origin
                    }).ToList();
                model = productList.ToList();
            }
            //ViewBag.productList = list_inbound_2;
            return View(model);
        }


        #endregion

        #region LoadProductItem




        public PartialViewResult LoadProduct(int OrderNo, int ProductId, string ProductName, decimal? Price, string ProductCode, int CommissionValue, int CommissionCusId)
        {
            //if (ProductName == "" || ProductName == "%")
            //{
            //    var pro = productRepository.GetAllProduct().FirstOrDefault(x => x.Code == ProductCode);
            //    if (pro != null)
            //    {
            //        ProductId = pro.Id;
            //        ProductName = pro.Name;
            //        CommissionCusId = 0;
            //        Price = pro.PriceOutbound;
            //    }
            //}
            var model = new CommisionCustomerViewModel();
            model.OrderNo = OrderNo;
            model.ProductId = ProductId;
            model.ProductName = ProductName;
            model.CommissionCusId = CommissionCusId;

            // model.Type = Type;
            model.Price = Price;
            model.ProductCode = ProductCode;
            model.CommissionValue = CommissionValue;

            if (ProductName == "" || ProductName == "%")
            {
                model.OrderNo = OrderNo - 1;
                var pro = productRepository.GetAllProduct().FirstOrDefault(x => x.Code == ProductCode);
                if (pro != null)
                {
                    model.ProductId = pro.Id;
                    model.ProductName = pro.Name;
                    //CommissionCusId = 0;
                    model.Price = pro.PriceOutbound;
                    if (ProductName == "")
                    {
                        model.IsMoney = true;
                    }
                }
            }
            return PartialView(model);
            /*
            var model = new CommissionCusViewModel();
            model.DetailList = new List<CommisionCustomerViewModel>();
            model.ProductList = new List<ProductViewModel>();
            //lấy danh sách chi tiết chiết khấu sản phẩm
            if (CommissionCusId != null && CommissionCusId.Value > 0)
            {
                var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == CommissionCusId).ToList();
                model.DetailList = detail.Select(x => new CommisionCustomerViewModel
                {
                    ProductId = x.ProductId.Value,
                    Id = x.Id,
                    IsMoney = x.IsMoney,
                    Type = x.Type,
                    CommissionValue = x.CommissionValue,
                    CommissionCusId = x.CommissionCusId.Value
                }).ToList();
                foreach (var pro in detail)
                {
                    var product = productRepository.GetProductById(pro.ProductId.Value);
                    ProductViewModel product2 = new ProductViewModel();
                    AutoMapper.Mapper.Map(product, product2);
                    model.ProductList.Add(product2);
                }
            }
              */
            //lấy danh sách sản phẩm thuộc nhóm đã chọn
            // var product = productRepository.GetAllProduct();
            /*  model.ProductList = product.Select(x => new ProductViewModel
              {
                  Id = x.Id,
                  Code = x.Code,
                  Name = x.Name,
                  PriceOutbound = x.PriceOutbound
              }).ToList();
             */
        }

        #endregion

        #region Json

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CheckStartDate(string Start)
        {

            DateTime start1 = Convert.ToDateTime(Start);
            //DateTime end1 = Convert.ToDateTime(end);
            try
            {
                // lấy danh sách 
                var commission = CommissionCusRepository.GetAllvwCommissionCus().Where(n => n.IsDeleted == false).ToList();
                // lấy ra startdate và enddate
                foreach (var itemCus in commission)
                {
                    var start = itemCus.StartDate;
                    var end2 = itemCus.EndDate;
                    if (start <= start1 && start1 <= end2)
                    {
                        return Json(1);
                    }




                }


                return Json(0);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CheckEndDate(string End)
        {

            DateTime end1 = Convert.ToDateTime(End);
            //DateTime end1 = Convert.ToDateTime(end);
            try
            {
                // lấy danh sách 
                var commission = CommissionCusRepository.GetAllvwCommissionCus().Where(n => n.IsDeleted == false).ToList();
                // lấy ra startdate và enddate
                foreach (var itemCus in commission)
                {
                    var start = itemCus.StartDate;
                    var end2 = itemCus.EndDate;
                    if (start <= end1 && end1 <= end2)
                    {
                        return Json(1);
                    }





                }


                return Json(0);
            }
            catch (Exception ex)
            {

                return Json(0);
            }


        }
        #endregion

        #region Create



        public ViewResult Create(int? Id)
        {
            var QuerynhomSp = dM_NHOMSANPHAMRepositories.GetAllDM_NHOMSANPHAM().ToList();
            List<DM_NHOMSANPHAMViewModel> nhomSp = new List<DM_NHOMSANPHAMViewModel>();
            AutoMapper.Mapper.Map(QuerynhomSp, nhomSp);

            var model = new CommissionCusViewModel();
            //model.ApplyFor = "DrugStore";
            model.DetailList = new List<CommisionCustomerViewModel>();
            model.ProductList = new List<ProductViewModel>();
            var departmentList = branchRepository.GetAllBranch().Where(x => x.ParentId != null || x.ParentId > 0)
               .Select(item => new BranchViewModel
               {
                   Name = item.Name,
                   Id = item.Id,
                   ParentId = item.ParentId
               }).ToList();
            ViewBag.departmentList = departmentList;
            if (Id != null && Id > 0)
            {
                var CommissionCus = CommissionCusRepository.GetCommissionCusById(Id.Value);
                AutoMapper.Mapper.Map(CommissionCus, model);


            }

            var productList = productRepository.GetAllvwProduct().Where(x => x.Type == "product")
              .Select(item => new ProductViewModel
              {
                  Type = item.Type,
                  Code = item.Code,
                  Barcode = item.Barcode,
                  Name = item.Name,
                  Id = item.Id,
                  CategoryCode = item.CategoryCode,
                  PriceOutbound = item.PriceOutbound,
                  Unit = item.Unit,
                  // QuantityTotalInventory = item.QuantityTotalInventory == null ? 0 : item.QuantityTotalInventory,
                  Image_Name = item.Image_Name,
                  ProductGroup = item.ProductGroup,
                  Origin = item.Origin
              }).ToList();
            model.ProductList = productList;

            var loyaltypointList = LoyaltyPoint.GetAllLoyaltyPoint().Select(item => new LoyaltyPointViewModel
            {
                Id = item.Id,
                IsDeleted = item.IsDeleted,
                CreatedDate = item.CreatedDate,
                CreatedUserId = item.CreatedUserId,
                Name = item.Name,
                MinMoney = item.MinMoney,
                PlusPoint = item.PlusPoint
            }).ToList();


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

            //spGetAllCustomerByBranch trong stored hiện tại đã lọc theo tất cả chi nhanh
            var CustomerList = SqlHelper.QuerySP<CustomerViewModel>("spGetAllCustomerByBranch", new
            {
                BranchId = intBrandID.Value
            }).ToList();

            foreach (var item in CustomerList)
            {
                item.FullName = item.FirstName + " " + item.LastName;
                item.Mobile = item.Mobile;
            }


            ViewBag.productList = productList;
            ViewBag.loyaltypointList = loyaltypointList;
            ViewBag.CustomerList = CustomerList;



            //Thêm số lượng tồn kho cho chi tiết đơn hàng đã được thêm
            /*  if (model.DetailList != null && model.DetailList.Count > 0)
              {
                  foreach (var item in model.DetailList)
                  {
                      var product = productList.Where(i => i.Id == item.ProductId).FirstOrDefault();
                      if (product != null)
                      {
                          //   item.QuantityInInventory = product.;
                          item.ProductCode = product.Code;
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

              }*/
            model.DetailList = new List<CommisionCustomerViewModel>();
            //model.ProductList = new List<ProductViewModel>();
            //lấy danh sách chi tiết chiết khấu sản phẩm
            if (Id > 0)
            {
                var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == Id).ToList();
                model.DetailList = detail.Select(x => new CommisionCustomerViewModel
                {
                    ProductId = x.ProductId.Value,
                    Id = x.Id,
                    IsMoney = x.IsMoney,
                    Type = x.Type,
                    CommissionValue = x.CommissionValue,
                    CommissionCusId = x.CommissionCusId.Value
                }).ToList();

                int n = 0;
                foreach (var item in model.DetailList)
                {
                    item.OrderNo = n;
                    n++;
                }
            }
            ViewBag.nhomSp = nhomSp;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CommissionCusViewModel model)
        {
            var urlRefer = Request["UrlReferrer"];
            var LoaiKM = Request["LoaiKM"];
            var ApplyForBranch = Request["branch_hiden"];
            var ApplyForVip = Request["ApplyForVip"];
            var ApplyForCustomer = Request["ForCustomer_hiden"];
            var ChosenObject = Request["group_choice"];
            var IsGetAllProduct = Request["GetAllSp"];
            // IsApplyAllBranch =0 => chọn tất cả chi nhánh
            var IsApplyAllBranch = Request["Select_Type3"];

            if (ApplyForBranch != "")
            {
                ApplyForBranch = ApplyForBranch.Substring(1);
            }

            if (ApplyForCustomer != "")
            {
                ApplyForCustomer = ApplyForCustomer.Substring(1);
            }


            List<int> Vip = new List<int>();
            List<int> Branch = new List<int>();
            List<int> Object = new List<int>();
            List<int> DirectCustomer = new List<int>();

            int GetAllProduct = IsGetAllProduct != null ? Int32.Parse(IsGetAllProduct) : 0;

            if (ApplyForVip != null)
                Vip = ApplyForVip.Split(',').Select(Int32.Parse).ToList();
            if ((ApplyForBranch != null) && (ApplyForBranch != ""))
                Branch = ApplyForBranch.Split(',').Select(Int32.Parse).ToList();
            if (ChosenObject != null)
                Object = ChosenObject.Split(',').Select(Int32.Parse).ToList();
            if ((ApplyForCustomer != null) && (ApplyForCustomer != ""))
                DirectCustomer = ApplyForCustomer.Split(',').Select(Int32.Parse).ToList();

            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    CommissionCus commissioncus = null;
                    if (model.Id > 0)
                    {
                        commissioncus = CommissionCusRepository.GetCommissionCusById(model.Id);
                    }
                    if (commissioncus != null)
                    {
                        AutoMapper.Mapper.Map(model, commissioncus);
                        commissioncus.Type = LoaiKM;
                        commissioncus.ApplyFor = IsApplyAllBranch == "0" ? IsApplyAllBranch : ApplyForBranch;
                        commissioncus.StartDate = model.StartDate;
                        commissioncus.EndDate = model.EndDate;
                        commissioncus.ModifiedUserId = WebSecurity.CurrentUserId;
                        commissioncus.ModifiedDate = DateTime.Now;
                        commissioncus.status = 0;
                    }
                    else
                    {
                        commissioncus = new CommissionCus();
                        AutoMapper.Mapper.Map(model, commissioncus);
                        commissioncus.IsDeleted = false;
                        commissioncus.CreatedUserId = WebSecurity.CurrentUserId;
                        commissioncus.ModifiedUserId = WebSecurity.CurrentUserId;
                        commissioncus.AssignedUserId = WebSecurity.CurrentUserId;
                        commissioncus.CreatedDate = DateTime.Now;
                        commissioncus.ModifiedDate = DateTime.Now;
                        commissioncus.Type = LoaiKM;
                        commissioncus.ApplyFor = IsApplyAllBranch == "0" ? IsApplyAllBranch : ApplyForBranch;
                        commissioncus.StartDate = model.StartDate;
                        commissioncus.EndDate = model.EndDate;
                        commissioncus.status = 0;
                    }

                    if (model.DetailList != null)
                    {
                        model.DetailList.RemoveAll(x => x.CommissionValue <= 0);
                    }
                    //hàm edit 
                    if (model.Id > 0)
                    {
                        CommissionCusRepository.UpdateCommissionCus(commissioncus);
                        var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == model.Id).ToList();
                        for (int i = 0; i < detail.Count(); i++)
                        {
                            CommissionDetailRepository.DeleteCommisionCustomer(detail[i].Id);
                        }
                    }
                    else
                    {
                        // lấy danh sách 
                        var commission = CommissionCusRepository.GetAllvwCommissionCus().ToList();
                        // lấy ra startdate và enddate
                        foreach (var itemCus in commission)
                        {
                            var start = itemCus.StartDate;
                            var end = itemCus.EndDate;
                            if ((start <= model.StartDate && model.StartDate <= end) || (start <= model.EndDate && model.EndDate <= end))
                            {
                                TempData[Globals.FailedMessageKey] = "Sự kiện khuyến mãi này đã tồn tại!";
                                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                                {
                                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                                }
                                return RedirectToAction("Create", "CommissionCus");
                            }

                        }
                        CommissionCusRepository.InsertCommissionCus(commissioncus);


                    }

                    //insert commisionApply              
                    foreach (var ObjectItem in Object)
                    {
                        List<int> listId_CompatibleWith_Type = new List<int>();
                        switch (ObjectItem)
                        {
                            case 3:
                                {
                                    //IsApplyAllBranch = 0 => chọn tất cả    
                                    if (IsApplyAllBranch != "0")
                                        listId_CompatibleWith_Type = Branch;
                                    else
                                        listId_CompatibleWith_Type.Add(0);
                                    break;
                                }
                            case 2:
                                {
                                    listId_CompatibleWith_Type = Vip;
                                    break;
                                }
                            case 1:
                                {
                                    listId_CompatibleWith_Type = DirectCustomer;
                                    break;
                                }
                            default:
                                return RedirectToAction("Index", "CommissionCus");
                        }

                        foreach (var item in listId_CompatibleWith_Type)
                        {
                            var commisionApply = new CommisionApply();
                            commisionApply.IsDeleted = false;
                            commisionApply.CreatedDate = DateTime.Now;
                            commisionApply.ModifiedDate = DateTime.Now;
                            commisionApply.CreatedUserId = WebSecurity.CurrentUserId;
                            commisionApply.ModifiedUserId = WebSecurity.CurrentUserId;
                            commisionApply.CommissionCusId = commissioncus.Id;
                            commisionApply.Type = ObjectItem;
                            commisionApply.BranchId = item;
                            commisionApplyRepository.InsertCommisionApply(commisionApply);
                        }
                    }


                    // insert CommisionCustomer
                    if (model.DetailList != null && model.DetailList.Count > 0)
                    {
                        if (GetAllProduct == 0)
                        {
                            foreach (var item in model.DetailList)
                            {
                                var commision = new CommisionCustomer();
                                commision.IsDeleted = false;
                                commision.CreatedUserId = WebSecurity.CurrentUserId;
                                commision.ModifiedUserId = WebSecurity.CurrentUserId;
                                commision.CreatedDate = DateTime.Now;
                                commision.ModifiedDate = DateTime.Now;
                                commision.CommissionCusId = commissioncus.Id;
                                commision.ProductId = item.ProductId;
                                commision.CommissionValue = item.CommissionValue;
                                commision.Minvalue = item.Minvalue;
                                commision.Type = item.Type;
                                commision.IsMoney = item.IsMoney != null ? item.IsMoney : false; ;
                                CommissionDetailRepository.InsertCommisionCustomer(commision);
                            }
                            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                            scope.Complete();
                            //return RedirectToAction("Index", "CommissionCus");
                        }
                        else
                        {
                            var commision = new CommisionCustomer();
                            commision.IsDeleted = false;
                            commision.CreatedUserId = WebSecurity.CurrentUserId;
                            commision.ModifiedUserId = WebSecurity.CurrentUserId;
                            commision.CreatedDate = DateTime.Now;
                            commision.ModifiedDate = DateTime.Now;
                            commision.CommissionCusId = commissioncus.Id;
                            commision.ProductId = 0;
                            commision.CommissionValue = model.DetailList[0].CommissionValue;
                            commision.Minvalue = model.DetailList[0].Minvalue;
                            commision.Type = model.DetailList[0].Type;
                            commision.IsMoney = model.DetailList[0].IsMoney != null ? model.DetailList[0].IsMoney : false;
                            CommissionDetailRepository.InsertCommisionCustomer(commision);

                            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                            scope.Complete();
                            //return RedirectToAction("Index", "CommissionCus");
                        }
                    }
                    else
                    {
                        TempData[Globals.FailedMessageKey] = "Thêm khuyến mãi thất bại. Mặt hàng khuyến mãi chưa được chọn hoặc mức chiết khâu chưa được quy định!";
                    }
                }
            }
            if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
            {
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            return RedirectToAction("Index", "CommissionCus");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var CommissionCus = CommissionCusRepository.GetCommissionCusById(Id.Value);
            if (CommissionCus.status == 1)
            {

                TempData[Globals.FailedMessageKey] = "Bạn phải dừng chương trình khuyến mãi trước khi sửa";
                RedirectToAction("/Index");
            }
            else
            {
                if (CommissionCus != null && CommissionCus.IsDeleted != true)
                {
                    //-----------------------------------ĐỐI TƯỢNG----------------------------------------------------------------------------------
                    //chi nhánh
                    var BranchList = branchRepository.GetAllBranch().ToList();
                    //nhóm vip
                    var loyaltypointList = LoyaltyPoint.GetAllLoyaltyPoint().Select(item => new LoyaltyPointViewModel
                    {
                        Id = item.Id,
                        IsDeleted = item.IsDeleted,
                        CreatedDate = item.CreatedDate,
                        CreatedUserId = item.CreatedUserId,
                        Name = item.Name,
                        MinMoney = item.MinMoney,
                        PlusPoint = item.PlusPoint
                    }).ToList();
                    //khách hàng theo chi nhanh
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
                    //spGetAllCustomerByBranch trong stored hiện tại đã lọc theo tất cả chi nhanh
                    var CustomerList = SqlHelper.QuerySP<CustomerViewModel>("spGetAllCustomerByBranch", new
                    {
                        BranchId = intBrandID.Value
                    }).ToList();

                    foreach (var item in CustomerList)
                    {
                        item.FullName = item.FirstName + " " + item.LastName;
                    }
                    //-------------------------------ITEM--------------------------------------------------------------------------------------

                    //nhóm sản phâm
                    var AllnhomSp = dM_NHOMSANPHAMRepositories.GetAllDM_NHOMSANPHAM().ToList();
                    List<DM_NHOMSANPHAMViewModel> nhomSp = new List<DM_NHOMSANPHAMViewModel>();
                    AutoMapper.Mapper.Map(AllnhomSp, nhomSp);

                    //sản phẩm
                    var productList = productRepository.GetAllvwProduct().Where(x => x.Type == "product")
                  .Select(item => new ProductViewModel
                  {
                      Type = item.Type,
                      Code = item.Code,
                      Barcode = item.Barcode,
                      Name = item.Name,
                      Id = item.Id,
                      CategoryCode = item.CategoryCode,
                      PriceOutbound = item.PriceOutbound,
                      Unit = item.Unit,
                      Image_Name = item.Image_Name,
                      ProductGroup = item.ProductGroup,
                      Origin = item.Origin
                  }).ToList();


                    var model = new CommissionCusViewModel();
                    AutoMapper.Mapper.Map(CommissionCus, model);
                    model.DetailList = new List<CommisionCustomerViewModel>();
                    //lấy danh sách chi tiết
                    var DetailCommissionCustomer = CommissionDetailRepository.GetAllCommisionCustomerbyidcus(Id.Value).ToList();
                    if (DetailCommissionCustomer == null)
                    {
                        TempData[Globals.FailedMessageKey] = "Xem khuyến mãi thất bại. Không có mặt hàng được áp dụng!";
                        return RedirectToAction("/Index");
                    }
                    AutoMapper.Mapper.Map(DetailCommissionCustomer, model.DetailList);

                    //lấy danh sách chi tiết theo loại
                    foreach (var item in model.DetailList)
                    {
                        if (item.Type == 1)
                        {
                            var product = productRepository.GetProductById(item.ProductId);
                            item.ProductName = product.Code + "-" + product.Name;
                            item.ProductCode = product.Code;
                            item.Price = product.PriceOutbound;
                            item.ObjectName = "Sản phẩm";
                        }
                        else if (item.Type == 2)
                        {
                            var QuerynhomSp = dM_NHOMSANPHAMRepositories.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(item.ProductId);
                            item.ProductName = QuerynhomSp.TEN_NHOMSANPHAM;
                            item.ObjectName = "Nhóm sản phẩm";
                        }
                        else if (item.Type == 3)
                        {
                            item.ProductName = "Tất cả sản phẩm";
                            item.ObjectName = "Tất cả sản phẩm";
                        }
                        else
                        {
                            item.ObjectName = "Hóa đơn";
                        }

                    }

                    //lấy danh sách đối tượng được áp dụng
                    var ObjectApplyFor = Enumerable.Repeat(false, 3).ToList();
                    bool ApplyForAllBranch = false;

                    model.ApplyDetail = new List<CommisionApplyViewModel>();
                    var CommissionApply = commisionApplyRepository.GetAllCommisionApplybyIDCus(Id.Value).ToList();
                    if (CommissionApply != null)
                    {
                        AutoMapper.Mapper.Map(CommissionApply, model.ApplyDetail);
                        foreach (var item in model.ApplyDetail)
                        {
                            if (item.Type == 3)
                            {
                                ObjectApplyFor[2] = true;
                                //tất cả chi nhánh --> BranchId = 0
                                if (item.BranchId == 0)
                                {
                                    item.BranchName = "Tất cả cửa hàng";
                                    item.BranchAdress = "Tất cả cửa hàng";
                                    ApplyForAllBranch = true;
                                }
                                else
                                {
                                    var BranchQuery = branchRepository.GetBranchById(item.BranchId);
                                    item.BranchName = BranchQuery.Name;
                                    item.BranchAdress = BranchQuery.Address;

                                    //không hiển thị những item đã có trong dropdownlist
                                    BranchList.Remove(BranchQuery);
                                }
                            }
                            else if (item.Type == 2)
                            {
                                ObjectApplyFor[1] = true;
                                var loyaltypointQuery = LoyaltyPoint.GetLoyaltyPointById(item.BranchId);
                                item.LogVipName = loyaltypointQuery.Name;
                                item.MinMoney = loyaltypointQuery.MinMoney;
                                item.MaxMoney = loyaltypointQuery.MaxMoney;
                                //không hiển thị những item đã có trong dropdownlist
                                loyaltypointList.RemoveAll(x => x.Id == item.BranchId);
                            }
                            else
                            {
                                ObjectApplyFor[0] = true;
                                var customerQuery = customerRepository.GetCustomerById(item.BranchId);
                                item.CustomerName = customerQuery.FirstName + " " + customerQuery.LastName;
                                item.CustomerPhone = customerQuery.Phone;
                                //không hiển thị những item đã có trong dropdownlist
                                CustomerList.RemoveAll(x => x.Id == item.BranchId);
                            }
                        }
                    }
                    else
                    {
                        TempData[Globals.FailedMessageKey] = "Xem khuyến mãi thất bại. Không có đối tượng áp dụng!";
                        return RedirectToAction("/Index");
                    }

                    ViewBag.ObjectApplyFor = ObjectApplyFor;
                    ViewBag.ApplyForAllBranch = ApplyForAllBranch;
                    ViewBag.productList = productList;
                    ViewBag.CustomerList = CustomerList;
                    ViewBag.BranchList = BranchList;
                    ViewBag.loyaltypointList = loyaltypointList;
                    ViewBag.nhomSp = nhomSp;
                    return View(model);
                }
                if (Request.UrlReferrer != null)
                    return Redirect(Request.UrlReferrer.AbsoluteUri);
                return RedirectToAction("/Index");
            }
            return RedirectToAction("/Index");

        }

        [HttpPost]
        public ActionResult Edit(CommissionCusViewModel model)
        {
            CommissionCus commissioncus = null;
            if (model.Id > 0)
            {
                commissioncus = CommissionCusRepository.GetCommissionCusById(model.Id);
            }
            if (commissioncus == null)
            {
                TempData[Globals.FailedMessageKey] = "Khuyến mãi không tồn tại!";
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                }
                return RedirectToAction("Index", "CommissionCus");
            }

            var urlRefer = Request["UrlReferrer"];
            var LoaiKM = Request["LoaiKM"];
            var ApplyForBranch = Request["ApplyForBranch"];
            var ApplyForVip = Request["ApplyForVip"];
            var ApplyForCustomer = Request["ApplyForCustomer"];
            var ChosenObject = Request["group_choice"];
            var IsGetAllProduct = Request["GetAllSp"];
            // IsApplyAllBranch =0 => chọn tất cả chi nhánh
            var IsApplyAllBranch = Request["Select_Type3"];


            List<int> Vip = new List<int>();
            List<int> Branch = new List<int>();
            List<int> Object = new List<int>();
            List<int> DirectCustomer = new List<int>();

            int GetAllProduct = IsGetAllProduct != null ? Int32.Parse(IsGetAllProduct) : 0;

            if (ApplyForVip != null)
                Vip = ApplyForVip.Split(',').Select(Int32.Parse).ToList();
            if ((ApplyForBranch != null) && (ApplyForBranch != ""))
                Branch = ApplyForBranch.Split(',').Select(Int32.Parse).ToList();
            if (ChosenObject != null)
                Object = ChosenObject.Split(',').Select(Int32.Parse).ToList();
            if ((ApplyForCustomer != null) && (ApplyForCustomer != ""))
                DirectCustomer = ApplyForCustomer.Split(',').Select(Int32.Parse).ToList();

            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    AutoMapper.Mapper.Map(model, commissioncus);
                    commissioncus.Type = LoaiKM;
                    commissioncus.ApplyFor = IsApplyAllBranch == "0" ? IsApplyAllBranch : ApplyForBranch;
                    commissioncus.StartDate = model.StartDate;
                    commissioncus.EndDate = model.EndDate;
                    commissioncus.CreatedUserId = model.CreatedUserId;
                    commissioncus.ModifiedUserId = model.ModifiedUserId;
                    commissioncus.ModifiedDate = DateTime.Now;
                    commissioncus.status = 2;
                    commissioncus.CreatedDate = model.CreatedDate;
                    commissioncus.IsDeleted = false;

                    if (model.DetailList != null)
                    {
                        model.DetailList.RemoveAll(x => x.CommissionValue <= 0);
                    }

                    CommissionCusRepository.UpdateCommissionCus(commissioncus);
                    var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == model.Id).ToList();
                    for (int i = 0; i < detail.Count(); i++)
                    {
                        CommissionDetailRepository.DeleteCommisionCustomer(detail[i].Id);
                    }

                    //Xóa tất cả CommissionApply cũ
                    commisionApplyRepository.DeleteAllCommisionApplyByIdCus(model.Id);
                    //insert commisionApply  
                    foreach (var ObjectItem in Object)
                    {
                        List<int> listId_CompatibleWith_Type = new List<int>();
                        switch (ObjectItem)
                        {
                            case 3:
                                {
                                    //IsApplyAllBranch = 0 => chọn tất cả    
                                    if (IsApplyAllBranch != "0")
                                        listId_CompatibleWith_Type = Branch;
                                    else
                                        listId_CompatibleWith_Type.Add(0);
                                    break;
                                }
                            case 2:
                                {
                                    listId_CompatibleWith_Type = Vip;
                                    break;
                                }
                            case 1:
                                {
                                    listId_CompatibleWith_Type = DirectCustomer;
                                    break;
                                }
                            default:
                                return RedirectToAction("Index", "CommissionCus");
                        }

                        foreach (var item in listId_CompatibleWith_Type)
                        {
                            var commisionApply = new CommisionApply();
                            commisionApply.IsDeleted = false;
                            commisionApply.CreatedDate = DateTime.Now;
                            commisionApply.ModifiedDate = DateTime.Now;
                            commisionApply.CreatedUserId = WebSecurity.CurrentUserId;
                            commisionApply.ModifiedUserId = WebSecurity.CurrentUserId;
                            commisionApply.CommissionCusId = commissioncus.Id;
                            commisionApply.Type = ObjectItem;
                            commisionApply.BranchId = item;
                            commisionApplyRepository.InsertCommisionApply(commisionApply);
                        }
                    }

                    //Xóa tất cả CommissionApply cũ
                    CommissionDetailRepository.DeleteAllCommissionCustomerByIdCus(model.Id);
                    // insert CommisionCustomer
                    if (model.DetailList != null && model.DetailList.Count > 0)
                    {
                        if (GetAllProduct == 0)
                        {
                            foreach (var item in model.DetailList)
                            {
                                var commision = new CommisionCustomer();
                                commision.IsDeleted = false;
                                commision.CreatedUserId = WebSecurity.CurrentUserId;
                                commision.ModifiedUserId = WebSecurity.CurrentUserId;
                                commision.CreatedDate = DateTime.Now;
                                commision.ModifiedDate = DateTime.Now;
                                commision.CommissionCusId = commissioncus.Id;
                                commision.ProductId = item.ProductId;
                                commision.CommissionValue = item.CommissionValue;
                                commision.Minvalue = item.Minvalue;
                                commision.Type = item.Type;
                                commision.IsMoney = item.IsMoney != null ? item.IsMoney : false; ;
                                CommissionDetailRepository.InsertCommisionCustomer(commision);
                            }
                            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                            scope.Complete();
                            //return RedirectToAction("Index", "CommissionCus");
                        }
                        else
                        {
                            var commision = new CommisionCustomer();
                            commision.IsDeleted = false;
                            commision.CreatedUserId = WebSecurity.CurrentUserId;
                            commision.ModifiedUserId = WebSecurity.CurrentUserId;
                            commision.CreatedDate = DateTime.Now;
                            commision.ModifiedDate = DateTime.Now;
                            commision.CommissionCusId = commissioncus.Id;
                            commision.ProductId = 0;
                            commision.CommissionValue = model.DetailList[0].CommissionValue;
                            commision.Minvalue = model.DetailList[0].Minvalue;
                            commision.Type = model.DetailList[0].Type;
                            commision.IsMoney = model.DetailList[0].IsMoney != null ? model.DetailList[0].IsMoney : false;
                            CommissionDetailRepository.InsertCommisionCustomer(commision);

                            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                            scope.Complete();
                            //return RedirectToAction("Index", "CommissionCus");
                        }
                    }
                    else
                    {
                        TempData[Globals.FailedMessageKey] = "Thêm khuyến mãi thất bại. Mặt hàng khuyến mãi chưa được chọn hoặc mức chiết khâu chưa được quy định!";
                    }
                }
            }
            else
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Wording.UpdateFailed;
            }
            if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
            {
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }
            return RedirectToAction("Edit", "CommissionCus", new { Id = model.Id });
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var CommissionCus = CommissionCusRepository.GetCommissionCusById(Id.Value);


            if (CommissionCus != null && CommissionCus.IsDeleted != true)
            {
                var model = new CommissionCusViewModel();
                AutoMapper.Mapper.Map(CommissionCus, model);
                model.DetailList = new List<CommisionCustomerViewModel>();
                //lấy danh sách chi tiết
                var DetailCommissionCustomer = CommissionDetailRepository.GetAllCommisionCustomerbyidcus(Id.Value).ToList();
                if (DetailCommissionCustomer == null)
                {
                    TempData[Globals.FailedMessageKey] = "Xem khuyến mãi thất bại. Không có mặt hàng được áp dụng!";
                    return RedirectToAction("Index");
                }
                AutoMapper.Mapper.Map(DetailCommissionCustomer, model.DetailList);

                //lấy danh sách chi tiết theo loại
                foreach (var item in model.DetailList)
                {
                    if (item.Type == 1)
                    {
                        var product = productRepository.GetProductById(item.ProductId);
                        if (product != null)
                        {
                            item.ProductName = product.Code + "-" + product.Name;
                            item.Price = product.PriceOutbound;
                            item.ObjectName = "Sản phẩm";
                        }
                        else
                        {
                            item.ProductName = "KXD";
                            item.Price = 0;
                            item.ObjectName = "Sản phẩm";
                        }
                    }
                    else if (item.Type == 2)
                    {
                        var QuerynhomSp = dM_NHOMSANPHAMRepositories.GetDM_NHOMSANPHAMByNHOMSANPHAM_ID(item.ProductId);
                        item.ProductName = QuerynhomSp.TEN_NHOMSANPHAM;
                        item.ObjectName = "Nhóm sản phẩm";
                    }
                    else if (item.Type == 3)
                    {
                        item.ProductName = "Tất cả sản phẩm";
                        item.ObjectName = "Tất cả sản phẩm";
                    }
                    else
                    {
                        item.ObjectName = "Hóa đơn";
                    }

                }

                //lấy danh sách đối tượng được áp dụng
                var ObjectApplyFor = Enumerable.Repeat(false, 3).ToList();
                bool ApplyForAllBranch = false;

                model.ApplyDetail = new List<CommisionApplyViewModel>();
                var CommissionApply = commisionApplyRepository.GetAllCommisionApplybyIDCus(Id.Value).ToList();
                if (CommissionApply != null)
                {
                    AutoMapper.Mapper.Map(CommissionApply, model.ApplyDetail);
                    foreach (var item in model.ApplyDetail)
                    {
                        if (item.Type == 3)
                        {
                            ObjectApplyFor[2] = true;
                            //tất cả chi nhánh --> BranchId = 0
                            if (item.BranchId == 0)
                            {
                                item.BranchName = "Tất cả cửa hàng";
                                item.BranchAdress = "Tất cả cửa hàng";
                                ApplyForAllBranch = true;
                            }
                            else
                            {
                                var BranchQuery = branchRepository.GetBranchById(item.BranchId);
                                item.BranchName = BranchQuery.Name;
                                item.BranchAdress = BranchQuery.Address;
                            }
                        }
                        else if (item.Type == 2)
                        {
                            ObjectApplyFor[1] = true;
                            var loyaltypointQuery = LoyaltyPoint.GetLoyaltyPointById(item.BranchId);
                            item.LogVipName = loyaltypointQuery.Name;
                            item.MinMoney = loyaltypointQuery.MinMoney;
                            item.MaxMoney = loyaltypointQuery.MaxMoney;
                        }
                        else
                        {
                            ObjectApplyFor[0] = true;
                            var customerQuery = customerRepository.GetCustomerById(item.BranchId);
                            item.CustomerName = customerQuery.FirstName + " " + customerQuery.LastName;
                            item.CustomerPhone = customerQuery.Phone;
                        }
                    }
                }
                else
                {
                    TempData[Globals.FailedMessageKey] = "Xem khuyến mãi thất bại. Không có đối tượng áp dụng!";
                    return RedirectToAction("Index");
                }

                ViewBag.ObjectApplyFor = ObjectApplyFor;
                ViewBag.ApplyForAllBranch = ApplyForAllBranch;
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
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        for (int i = 0; i < arrDeleteId.Count(); i++)
                        {
                            var item = CommissionCusRepository.GetCommissionCusById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                            if (item != null)
                            {

                                if (item.status == 1 || item.status == 2)
                                {
                                    TempData["FailedMessage"] = "Không thể xóa chương trình đã kích hoạt hoặc đang kích hoạt";
                                    return RedirectToAction("/Index");
                                }

                                if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                                {
                                    TempData["FailedMessage"] = "NotOwner";
                                    return RedirectToAction("/Index");
                                }

                                item.IsDeleted = true;
                                CommissionCusRepository.UpdateCommissionCus(item);

                                List<CommisionApply> cusapply = commisionApplyRepository.GetAllCommisionApplybyIDCus(item.Id).ToList();
                                if (cusapply.Count() > 0)
                                {
                                    foreach (var item1 in cusapply)
                                    {
                                        item1.IsDeleted = true;
                                        commisionApplyRepository.UpdateCommisionApply(item1);
                                    }
                                }


                                List<CommisionCustomer> cuscus = CommissionDetailRepository.GetAllCommisionCustomerbyidcus(item.Id).ToList();
                                if (cuscus.Count() > 0)
                                {
                                    foreach (var item2 in cuscus)
                                    {
                                        item2.IsDeleted = true;
                                        CommissionDetailRepository.UpdateCommisionCustomer(item2);
                                    }
                                }


                            }
                        }
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        return Content("Fail");
                    }
                }
                return RedirectToAction("/Index");
            }
            catch (Exception ex)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("/Index");
            }
        }
        #endregion

        #region DM_DEALHOT
        public ViewResult DM_DealHot()
        {
            var model = new DM_DEALHOTViewModel();

            var list = dm_dealhotRepository.GetAllDM_DEALHOT().AsEnumerable()
            .Select(item => new DM_DEALHOTViewModel
            {
                DEALHOT_ID = item.DEALHOT_ID,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AssignedUserId = item.AssignedUserId,
                CommissionCus_Id = item.CommissionCus_Id,
                BANNER = item.BANNER,
                IS_SHOW = item.IS_SHOW
            }).OrderByDescending(x => x.ModifiedDate).ToList();

            var commissioncus = CommissionCusRepository.GetAllCommissionCus()
            .Select(item => new CommissionCusViewModel
            {
                Id = item.Id,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                Name = item.Name,
                ApplyFor = item.ApplyFor,
                EndDate = item.EndDate,
                Note = item.Note,
                StartDate = item.StartDate,
                Type = item.Type

            }).OrderByDescending(m => m.ModifiedDate).ToList();

            commissioncus = commissioncus.Where(x => x.Type == "IrregularDiscount").ToList();

            foreach (var t in commissioncus.ToList())
            {
                //lấy danh sách chi tiết chiết khấu sản phẩm
                var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == t.Id).ToList();
                t.DetailList = detail.Select(x => new CommisionCustomerViewModel
                {
                    ProductId = x.ProductId.Value,
                    Id = x.Id,
                    IsMoney = x.IsMoney,
                    Type = x.Type,
                    CommissionValue = x.CommissionValue,
                    CommissionCusId = x.CommissionCusId.Value
                }).ToList();
                //lấy danh sách sản phẩm thuộc nhóm đã chọn
                var product = productRepository.GetAllProduct();
                t.ProductList = product.Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    PriceOutbound = x.PriceOutbound
                }).ToList();
            }

            foreach (var item in list.ToList())
            {
                var comcus = commissioncus.Find(x => x.Id == item.CommissionCus_Id);
                if (comcus != null)
                {
                    item.CommissionCus_Name = comcus.Name;

                }
            }

            ViewBag.CommissionCus = commissioncus.ToList();
            ViewBag.DM_DEALHOT = list.ToList();
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(model);
        }

        public ActionResult _DealHot_CommissionCusDetail(int Id)
        {
            var CommissionCus = CommissionCusRepository.GetCommissionCusById(Id);
            if (CommissionCus != null && CommissionCus.IsDeleted != true)
            {
                var model = new CommissionCusViewModel();
                AutoMapper.Mapper.Map(CommissionCus, model);
                model.DetailList = new List<CommisionCustomerViewModel>();
                model.ProductList = new List<ProductViewModel>();
                //lấy danh sách chi tiết chiết khấu sản phẩm
                var detail = CommissionDetailRepository.GetAllCommisionCustomer().Where(x => x.CommissionCusId == Id).ToList();
                model.DetailList = detail.Select(x => new CommisionCustomerViewModel
                {
                    ProductId = x.ProductId.Value,
                    Id = x.Id,
                    IsMoney = x.IsMoney,
                    Type = x.Type,
                    CommissionValue = x.CommissionValue,
                    CommissionCusId = x.CommissionCusId.Value
                }).ToList();
                //lấy danh sách sản phẩm thuộc nhóm đã chọn
                var product = productRepository.GetAllProduct();
                model.ProductList = product.Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    PriceOutbound = x.PriceOutbound
                }).ToList();
                return PartialView("_DealHot_CommissionCusDetail", model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("DM_DealHot");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateDealHot(DM_DEALHOTViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.DEALHOT_ID == 0)
                {

                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {


                        #region Create
                        var dm_dealhot = new Domain.Sale.Entities.DM_DEALHOT();
                        AutoMapper.Mapper.Map(model, dm_dealhot);
                        dm_dealhot.IsDeleted = false;
                        dm_dealhot.CreatedUserId = WebSecurity.CurrentUserId;
                        dm_dealhot.ModifiedUserId = WebSecurity.CurrentUserId;
                        dm_dealhot.CreatedDate = DateTime.Now;
                        dm_dealhot.ModifiedDate = DateTime.Now;
                        dm_dealhot.AssignedUserId = WebSecurity.CurrentUserId;

                        dm_dealhot.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);//???

                        var checkComCusId = dm_dealhotRepository.GetDM_DEALHOTByCommissionCus_Id(model.CommissionCus_Id);

                        if (checkComCusId != null)
                        {
                            TempData[Globals.FailedMessageKey] = "Chương trình khuyến mãi đã tồn tại";
                            return RedirectToAction("DM_DealHot");
                        }

                        dm_dealhot.BANNER = "";
                        dm_dealhotRepository.InsertDM_DEALHOT(dm_dealhot);
                        dm_dealhot = dm_dealhotRepository.GetDM_DEALHOTByDEALHOT_ID(dm_dealhot.DEALHOT_ID);
                        //begin up hinh anh cho backend

                        var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("dealhot-image-folder"));
                        if (Request.Files["file-image"] != null)
                        {
                            var file = Request.Files["file-image"];
                            if (file.ContentLength > 0)
                            {
                                string image_name = "dealhot_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_dealhot.DEALHOT_ID.ToString(), @"\s+", "_")) + file.FileName;
                                bool isExists = System.IO.Directory.Exists(path);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(path);
                                file.SaveAs(path + image_name);
                                dm_dealhot.BANNER = image_name;
                            }
                        }

                        //end up hinh anh cho backend

                        //begin up hinh anh cho client
                        path = Helpers.Common.GetSetting("dealhot-image-folder-client");
                        string prjClient = Helpers.Common.GetSetting("prj-client");
                        string filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                        DirectoryInfo di = new DirectoryInfo(filepath);
                        filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                        if (Request.Files["file-image"] != null)
                        {
                            var file = Request.Files["file-image"];
                            if (file.ContentLength > 0)
                            {
                                FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_dealhot.BANNER);
                                if (fi.Exists)
                                {
                                    fi.Delete();
                                }

                                string image_name = "dealhot_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_dealhot.DEALHOT_ID.ToString(), @"\s+", "_")) + file.FileName;

                                bool isExists = System.IO.Directory.Exists(filepath);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(filepath);
                                file.SaveAs(filepath + image_name);
                                dm_dealhot.BANNER = image_name;
                            }
                        }
                        //end up hinh anh cho client





                        dm_dealhotRepository.UpdateDM_DEALHOT(dm_dealhot);

                        scope.Complete();

                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        return RedirectToAction("DM_DealHot");
                        #endregion
                    }
                }
                else
                {
                    #region Edit
                    var dm_dealhot = dm_dealhotRepository.GetDM_DEALHOTByDEALHOT_ID(model.DEALHOT_ID);
                    dm_dealhot.ModifiedUserId = WebSecurity.CurrentUserId;
                    dm_dealhot.ModifiedDate = DateTime.Now;
                    dm_dealhot.AssignedUserId = WebSecurity.CurrentUserId;
                    dm_dealhot.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);

                    var oldItem = dm_dealhotRepository.GetDM_DEALHOTByDEALHOT_ID(model.DEALHOT_ID);
                    if (oldItem.CommissionCus_Id != model.CommissionCus_Id)
                    {
                        var checkComCusId = dm_dealhotRepository.GetDM_DEALHOTByCommissionCus_Id(model.CommissionCus_Id);
                        if (checkComCusId != null)
                        {
                            TempData[Globals.FailedMessageKey] = "Chương trình khuyến mãi đã tồn tại";
                            return RedirectToAction("DM_DealHot");
                        }
                    }


                    //begin up hinh anh cho backend
                    var path = Helpers.Common.GetSetting("dealhot-image-folder");
                    var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_dealhot.BANNER);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = "dealhot_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_dealhot.DEALHOT_ID.ToString(), @"\s+", "_")) + file.FileName;

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_dealhot.BANNER = image_name;
                        }
                    }

                    //end up hinh anh cho backend

                    //begin up hinh anh cho client
                    path = Helpers.Common.GetSetting("dealhot-image-folder-client");
                    string prjClient = Helpers.Common.GetSetting("prj-client");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                    DirectoryInfo di = new DirectoryInfo(filepath);
                    filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_dealhot.BANNER);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = "dealhot_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_dealhot.DEALHOT_ID.ToString(), @"\s+", "_")) + file.FileName;

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_dealhot.BANNER = image_name;
                        }
                    }
                    //end up hinh anh cho client









                    dm_dealhotRepository.UpdateDM_DEALHOT(dm_dealhot);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("DM_DealHot");
                    #endregion
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteDealHot()
        {
            try
            {
                int id = int.Parse(Request["Id"], CultureInfo.InvariantCulture);
                var item = dm_dealhotRepository.GetDM_DEALHOTByDEALHOT_ID(id);
                if (item != null)
                {
                    dm_dealhotRepository.DeleteDM_DEALHOT(item.DEALHOT_ID);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("DM_DealHot");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("DM_DealHot");
            }
            return View();
        }
        #endregion

        #region DM_BannerSlider
        public ViewResult DM_BannerSlider()
        {
            var model = new DM_BANNER_SLIDERViewModel();


            var list = dm_bannersliderRepository.GetAllDM_BANNER_SLIDER().AsEnumerable()
            .Select(item => new DM_BANNER_SLIDERViewModel
            {
                BANNER_SLIDER_ID = item.BANNER_SLIDER_ID,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                AssignedUserId = item.AssignedUserId,
                LINK = item.LINK,
                STT = item.STT,
                ANH_DAIDIEN = item.ANH_DAIDIEN,
                IS_SHOW = item.IS_SHOW,
                IS_MOBILE = item.IS_MOBILE,
                IsDeleted = item.IsDeleted
            }).OrderBy(x => x.STT).ToList();
            //list = AutoMapper.Mapper.Map(model, list);


            ViewBag.DM_BANNER_SLIDER = list;
            ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
            ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateBannerSlider(DM_BANNER_SLIDERViewModel model)
        {
            if (ModelState.IsValid)
            {

                //**create**//
                if (model.BANNER_SLIDER_ID == 0)
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {


                        var dm_bannerslider = new Domain.Sale.Entities.DM_BANNER_SLIDER();
                        AutoMapper.Mapper.Map(model, dm_bannerslider);
                        dm_bannerslider.IsDeleted = false;
                        dm_bannerslider.CreatedUserId = WebSecurity.CurrentUserId;
                        dm_bannerslider.ModifiedUserId = WebSecurity.CurrentUserId;
                        dm_bannerslider.CreatedDate = DateTime.Now;
                        dm_bannerslider.ModifiedDate = DateTime.Now;
                        dm_bannerslider.AssignedUserId = WebSecurity.CurrentUserId;

                        dm_bannerslider.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);//???
                        dm_bannerslider.IS_MOBILE = Convert.ToInt32(Request["is-mobile-checkbox"]);// check show img mobile

                        var loaisanpham = dm_bannersliderRepository.GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(model.BANNER_SLIDER_ID);

                        if (loaisanpham != null)
                        {
                            TempData[Globals.FailedMessageKey] = "Banner đã tồn tại";
                            return RedirectToAction("DM_BannerSlider");
                        }

                        dm_bannerslider.ANH_DAIDIEN = "";
                        dm_bannersliderRepository.InsertDM_BANNER_SLIDER(dm_bannerslider);
                        dm_bannerslider = dm_bannersliderRepository.GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(dm_bannerslider.BANNER_SLIDER_ID);

                        //begin up hinh anh cho backend
                        var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("banner-image-folder"));
                        if (Request.Files["file-image"] != null)
                        {
                            var file = Request.Files["file-image"];
                            if (file.ContentLength > 0)
                            {
                                string image_name = "banner_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_bannerslider.BANNER_SLIDER_ID.ToString(), @"\s+", "_")) + file.FileName;
                                bool isExists = System.IO.Directory.Exists(path);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(path);
                                file.SaveAs(path + image_name);
                                dm_bannerslider.ANH_DAIDIEN = image_name;
                            }
                        }
                        //end up hinh anh cho backend

                        //begin up hinh anh cho client
                        path = Helpers.Common.GetSetting("banner-image-folder-client");
                        string prjClient = Helpers.Common.GetSetting("prj-client");
                        string filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                        DirectoryInfo di = new DirectoryInfo(filepath);
                        filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                        if (Request.Files["file-image"] != null)
                        {
                            var file = Request.Files["file-image"];
                            if (file.ContentLength > 0)
                            {
                                FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_bannerslider.ANH_DAIDIEN);
                                if (fi.Exists)
                                {
                                    fi.Delete();
                                }

                                string image_name = "banner_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_bannerslider.BANNER_SLIDER_ID.ToString(), @"\s+", "_")) + file.FileName;

                                bool isExists = System.IO.Directory.Exists(filepath);
                                if (!isExists)
                                    System.IO.Directory.CreateDirectory(filepath);
                                file.SaveAs(filepath + image_name);
                                dm_bannerslider.ANH_DAIDIEN = image_name;
                            }
                        }
                        //end up hinh anh cho client






                        dm_bannersliderRepository.UpdateDM_BANNER_SLIDER(dm_bannerslider);
                        scope.Complete();

                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        return RedirectToAction("DM_BannerSlider");
                    }
                }
                else//**edit**//
                {
                    var dm_bannerslider = dm_bannersliderRepository.GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(model.BANNER_SLIDER_ID);
                    dm_bannerslider.ModifiedUserId = WebSecurity.CurrentUserId;
                    dm_bannerslider.ModifiedDate = DateTime.Now;
                    dm_bannerslider.AssignedUserId = WebSecurity.CurrentUserId;
                    dm_bannerslider.LINK = model.LINK;

                    dm_bannerslider.STT = model.STT;
                    dm_bannerslider.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);
                    dm_bannerslider.IS_MOBILE = Convert.ToInt32(Request["is-mobile-checkbox"]);

                    var oldItem = dm_bannersliderRepository.GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(model.BANNER_SLIDER_ID);

                    //begin up hinh anh cho backend
                    var path = Helpers.Common.GetSetting("banner-image-folder");
                    var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_bannerslider.ANH_DAIDIEN);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = "banner_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_bannerslider.BANNER_SLIDER_ID.ToString(), @"\s+", "_")) + file.FileName;

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_bannerslider.ANH_DAIDIEN = image_name;
                        }
                    }

                    //end up hinh anh cho backend



                    //begin up hinh anh cho client
                    path = Helpers.Common.GetSetting("banner-image-folder-client");
                    string prjClient = Helpers.Common.GetSetting("prj-client");
                    filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                    DirectoryInfo di = new DirectoryInfo(filepath);
                    filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_bannerslider.ANH_DAIDIEN);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = "banner_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_bannerslider.BANNER_SLIDER_ID.ToString(), @"\s+", "_")) + file.FileName;

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_bannerslider.ANH_DAIDIEN = image_name;
                        }
                    }
                    //end up hinh anh cho client









                    dm_bannersliderRepository.UpdateDM_BANNER_SLIDER(dm_bannerslider);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("DM_BannerSlider");

                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteBannerSlider()
        {
            int id = int.Parse(Request["Id"], CultureInfo.InvariantCulture);
            try
            {
                var item = dm_bannersliderRepository.GetDM_BANNER_SLIDERByBANNER_SLIDER_ID(id);
                if (item != null)
                {
                    dm_bannersliderRepository.DeleteDM_BANNER_SLIDER(item.BANNER_SLIDER_ID);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("DM_BannerSlider");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("DM_BannerSlider");
            }
        }
        #endregion

        #region ImportHangHoaKM
        //[System.Web.Mvc.HttpGet]
        //public ActionResult ImportFile()
        //{
        //    var resultData = new List<ImportHangKM>();
        //    return View(resultData);
        //}

        //[System.Web.Mvc.HttpPost]
        //public JsonResult ImportFile(HttpPostedFileBase file)
        //{
        //    var resultData = new List<ImportHangKM>();
        //    var path = string.Empty;
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        var fileName = DateTime.Now.Ticks + file.FileName;
        //        path = Path.Combine(Server.MapPath("~/fileuploads/"),
        //        Path.GetFileName(fileName));


        //        file.SaveAs(path);
        //        ViewBag.FileName = fileName;
        //    }

        //    resultData = ReadDataFileExcel2(path);

        //    return Json(resultData);
        //}

        //private List<ImportHangKM> ReadDataFileExcel2(string filePath)
        //{
        //    var resultData = new List<ImportHangKM>();



        //    //readfile 

        //    //FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        //    using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
        //    {

        //        //1. Reading from a binary Excel file ('97-2003 format; *.xls)
        //        //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        //        //...
        //        //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        //        using (var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
        //        {
        //            //...

        //            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
        //            //DataSet result = excelReader.AsDataSet();
        //            //...
        //            //4. DataSet - Create column names from first row
        //            excelReader.IsFirstRowAsColumnNames = true;
        //            DataSet result = excelReader.AsDataSet();

        //            //5. Data Reader methods
        //            while (excelReader.Read())
        //            {
        //                var donhang = new ImportHangKM()
        //                {
        //                    STT = excelReader.GetString(0),
        //                    Code = excelReader.GetString(1),
        //                    Discount = excelReader.GetString(2),
        //                    Applyfor = excelReader.GetString(3),

        //                };
        //                resultData.Add(donhang);
        //                //excelReader.GetInt32(0);
        //            }

        //            //6. Free resources (IExcelDataReader is IDisposable)
        //            excelReader.Close();
        //        }
        //    }

        //    return resultData;

        //}

        //[System.Web.Mvc.HttpPost]
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


        public ActionResult PrintExample()
        {
            var model = new ImportHangKM();
            Encoding encoding = Encoding.UTF8;
            var template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("ImportKM")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            //Response.ContentEncoding = Encoding.Unicode;
            model.Content = template.Content;
            Response.AppendHeader("content-disposition", "attachment;filename=" + "MauExcel" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            Response.Charset = encoding.EncodingName;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            Response.Write(model.Content);
            Response.End();
            return View(model);
        }
        #endregion
    }
}
