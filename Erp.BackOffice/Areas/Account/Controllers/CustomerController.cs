using Erp.BackOffice.Filters;
using Erp.BackOffice.Helpers;
using Erp.BackOffice.Account.Models;
using Erp.Domain.Interfaces;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using Erp.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;
using Erp.BackOffice.Sale.Controllers;
using Erp.BackOffice.Sale.Models;
using System.Drawing;
using System.IO;
using Erp.Domain.Crm.Interfaces;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using Erp.BackOffice.Staff.Models;
using Erp.BackOffice.Staff.Controllers;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Web;
using System.Transactions;

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository CustomerRepository;
        private readonly IContactRepository ContactRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IUsingServiceLogRepository usingServiceLogRepository;
        private readonly ITransactionLiabilitiesRepository transactionLiabilitiesRepository;
        private readonly IDocumentFieldRepository DocumentFieldRepository;
        private readonly IDocumentAttributeRepository DocumentAttributeRepository;
        private readonly IUsingServiceLogDetailRepository usingServiceLogdetailRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IUserTypeRepository user_typeRepository;
        private readonly ITemplatePrintRepository templatePrintRepository;
        private readonly IReceiptRepository receiptRepository;
        public CustomerController(
            ICustomerRepository _Customer
            , IContactRepository _Contact
            , IUserRepository _user
            , IProductInvoiceRepository productInvoice
            , ILocationRepository location
            , IUsingServiceLogRepository usingServiceLog
            , ITransactionLiabilitiesRepository _transactionLiabilities
             , IDocumentFieldRepository _DocumentField
            , IDocumentAttributeRepository DocumentAttribute
            , IUsingServiceLogDetailRepository usingServiceLogDetail
                        , IBranchRepository branch
                       , IUserTypeRepository user_type
            , ITemplatePrintRepository templatePrint,
            IReceiptRepository ReceiptRepository
            )
        {
            ContactRepository = _Contact;
            CustomerRepository = _Customer;
            userRepository = _user;
            productInvoiceRepository = productInvoice;
            locationRepository = location;
            usingServiceLogRepository = usingServiceLog;
            transactionLiabilitiesRepository = _transactionLiabilities;
            DocumentFieldRepository = _DocumentField;
            DocumentAttributeRepository = DocumentAttribute;
            usingServiceLogdetailRepository = usingServiceLogDetail;
            branchRepository = branch;
            user_typeRepository = user_type;
            templatePrintRepository = templatePrint;
            receiptRepository = ReceiptRepository;
        }

        #region Index

        public ViewResult Index(string CardCode, string txtCusName, string txtCode, string Phone, string ProvinceId, string DistrictId, string WardId, bool? ExportExcel,int? AmountPage)
        {
            IEnumerable<CustomerViewModel> q = CustomerRepository.GetAllvwCustomer().AsEnumerable()
                // .Where(x => x.BranchId.Value == Convert.ToInt32(Erp.BackOffice.Helpers.Common.GetSetting("BranchId")))
                .Select(item => new CustomerViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    CompanyName = item.CompanyName,
                    Phone = item.Phone,
                    Address = item.Address,
                    Note = item.Note,
                    CardCode = item.CardCode,
                    SearchFullName = item.SearchFullName,
                    Image = item.Image,
                    Birthday = item.Birthday,
                    Gender = item.Gender,
                    IdCardDate = item.IdCardDate,
                    IdCardIssued = item.IdCardIssued,
                    IdCardNumber = item.IdCardNumber,
                    CardIssuedName = item.CardIssuedName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    FullName = item.FullName,
                    BankAccount = item.BankAccount,
                    BankName = item.BankName,
                    TaxCode = item.TaxCode,
                    ProvinceName = item.ProvinceName,
                    DistrictName = item.DistrictName,
                    WardName = item.WardName,
                    DistrictId = item.DistrictId,
                    CityId = item.CityId,
                    WardId = item.WardId,
                    Mobile = item.Mobile,
                    Email = item.Email,
                }).OrderByDescending(m => m.CreatedDate).ToList();
            
            //   bool hasSearch = false;

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

            if (!string.IsNullOrEmpty(CardCode))
            {
                q = q.Where(x => x.CardCode.Contains(CardCode));
                //       hasSearch = true;
            }
            var qq = CustomerRepository.GetAllvwCustomer().ToList();
            if (!string.IsNullOrEmpty(txtCusName))
            {
                txtCusName = txtCusName.Trim();
                //var a = txtCusName.IndexOf(' ').ToString();
                txtCusName = Helpers.Common.ChuyenThanhKhongDau(txtCusName);
                //if (a.Any()) {
                //    string ten = txtCusName.Substring(0, txtCusName.IndexOf(' '));
                //    string ho = txtCusName.Substring(txtCusName.LastIndexOf(' ') + 1);
                //    q = q.Where(x => (Helpers.Common.ChuyenThanhKhongDau(x.FirstName).Contains(ten) && Helpers.Common.ChuyenThanhKhongDau(x.FirstName).Contains(ho)) || (Helpers.Common.ChuyenThanhKhongDau(x.LastName).Contains(ho) && Helpers.Common.ChuyenThanhKhongDau(x.LastName).Contains(ten)));
                //}
                q = q.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.FullName).ToLower().Contains(txtCusName));


                
                //q = q.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.FullName).ToLower().Contains(txtCusName));                         
               
                //        hasSearch = true;
            }

            if (!string.IsNullOrEmpty(txtCode))
            {
                q = q.Where(x => x.Code == txtCode);
                //       hasSearch = true;
            }
            if (!string.IsNullOrEmpty(Phone))
            {
                q = q.Where(x => x.Mobile == Phone);
                //       hasSearch = true;
            }
            if (!string.IsNullOrEmpty(ProvinceId))
            {
                q = q.Where(item => item.CityId == ProvinceId);
                //      hasSearch = true;
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(item => item.DistrictId == DistrictId);
                //        hasSearch = true;
            }
            if (!string.IsNullOrEmpty(WardId))
            {
                q = q.Where(item => item.WardId == WardId);
                //      hasSearch = true;
            }

            //if(hasSearch)
            //{
            //    q = q.OrderByDescending(m => m.CompanyName);
            ////    hasSearch = true;
            //}
            //else
            //{
            //    q = null;
            //}
            if (ExportExcel != null && ExportExcel == true)
            {
                TemplatePrint template = null;
                template = templatePrintRepository.GetAllTemplatePrint().Where(x => x.Code.Contains("Customer")).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                if (template == null)
                    return View(q);

                string output = template.Content;


                var logo = Erp.BackOffice.Helpers.Common.GetSetting("LogoCompany");
                var company = Erp.BackOffice.Helpers.Common.GetSetting("companyName");
                var address = Erp.BackOffice.Helpers.Common.GetSetting("addresscompany");
                var phone = Erp.BackOffice.Helpers.Common.GetSetting("phonecompany");
                var fax = Erp.BackOffice.Helpers.Common.GetSetting("faxcompany");
                var bankcode = Erp.BackOffice.Helpers.Common.GetSetting("bankcode");
                var bankname = Erp.BackOffice.Helpers.Common.GetSetting("bankname");
                var ImgLogo = "<div class=\"logo\"><img src=" + logo + " height=\"73\" /></div>";

                output = output.Replace("{System.Logo}", ImgLogo);
                output = output.Replace("{System.CompanyName}", company);
                output = output.Replace("{System.AddressCompany}", address);
                output = output.Replace("{System.PhoneCompany}", phone);
                output = output.Replace("{System.Fax}", fax);
                output = output.Replace("{System.BankCodeCompany}", bankcode);
                output = output.Replace("{System.BankNameCompany}", bankname);
                output = output.Replace("{DataTable}", buildHtmlDetailList(q.ToList()));

                Response.AppendHeader("content-disposition", "attachment;filename=Customer" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Write(output);
                Response.End();
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            ViewBag.Tongdong = q.Count();
            return View(q);
        }
        #endregion
        #region Print
        string buildHtmlDetailList(List<CustomerViewModel> model)
        {
            //Tạo table html chi tiết phiếu xuất
            string detailList = "<table class=\"invoice-detail\">\r\n";
            detailList += "<thead>\r\n";
            detailList += "	<tr>\r\n";
            detailList += "		<th>STT</th>\r\n";
            detailList += "		<th>Mã khách hàng</th>\r\n";
            detailList += "		<th>Tên khách hàng</th>\r\n";
            detailList += "		<th>Mã số thuế</th>\r\n";
            detailList += "		<th>Số tài khoản</th>\r\n";
            detailList += "		<th>Tên ngân hàng</th>\r\n";
            detailList += "		<th>Điện thoại</th>\r\n";
            detailList += "		<th>Địa chỉ</th>\r\n";
            detailList += "		<th>Phường/Xã</th>\r\n";
            detailList += "		<th>Quận/Huyện</th>\r\n";
            detailList += "		<th>Tỉnh/Thành phố</th>\r\n";
            detailList += "	</tr>\r\n";
            detailList += "</thead>\r\n";
            detailList += "<tbody><tbody>\r\n";
            for (int i = 0; i < model.Count(); i++)
            {
                detailList += "<tr>\r\n"
                 + "<td class=\"text-center\">" + (i + 1) + "</td>\r\n"
                 + "<td class=\"text-left\">" + model[i].Code + "</td>\r\n"
                 + "<td class=\"text-left\">" + model[i].CompanyName + "</td>\r\n"
                 + "<td class=\"text-right\">" + model[i].TaxCode + "</td>\r\n"
                 + "<td class=\"text-right\">" + model[i].BankAccount + "</td>\r\n"
                 + "<td class=\"text-left\">" + model[i].BankName + "</td>\r\n"
                 + "<td class=\"text-right\">" + model[i].Phone + "</td>\r\n"
                 + "<td class=\"text-left\">" + model[i].Address + "</td>\r\n"
                 + "<td class=\"text-left\">" + model[i].WardName + "</td>\r\n"
                  + "<td class=\"text-left\">" + model[i].DistrictName + "</td>\r\n"
                   + "<td class=\"text-left\">" + model[i].ProvinceName + "</td>\r\n"
                 + "</tr>\r\n";
            }

            detailList += "</tbody>\r\n";

            detailList += "</table>\r\n";

            return detailList;
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Customer = CustomerRepository.GetCustomerById(Id.Value);
            if (Customer != null && Customer.IsDeleted != true)
            {
                var model = new CustomerViewModel();
                AutoMapper.Mapper.Map(Customer, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}               

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var GroupPrice = Request["GroupPrice"];
                    var Customer = CustomerRepository.GetCustomerById(model.Id);
                    AutoMapper.Mapper.Map(model, Customer);
                    Customer.ModifiedUserId = WebSecurity.CurrentUserId;
                    Customer.ModifiedDate = DateTime.Now;
                    Customer.GroupPrice = GroupPrice;
                    //tạo đặc tính động cho sản phẩm nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                    //   ObjectAttributeController.CreateOrUpdateForObject(Customer.Id, model.AttributeValueList);

                    CustomerRepository.UpdateCustomer(Customer);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Detail", new { Id = Customer.Id });
                }

                return View(model);
            }
            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Create
        public ViewResult Create(string Phone)
        {
            var model = new CustomerViewModel();
            model.ProvinceList = Helpers.SelectListHelper.GetSelectList_Location("0", null, App_GlobalResources.Wording.Empty);

            if (!string.IsNullOrEmpty(Phone))
            {
                model.Phone = Phone;
            }

            return View(model);
        }
        public ViewResult Create_POS(string Phone)
        {
            var model = new CustomerViewModel();
            model.ProvinceList = Helpers.SelectListHelper.GetSelectList_Location("0", null, App_GlobalResources.Wording.Empty);

            if (!string.IsNullOrEmpty(Phone))
            {
                model.Phone = Phone;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CustomerViewModel model, bool IsPopup)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                //Kiểm tra trùng tên số điện thoại thì không cho lưu
                //var cc = CustomerRepository.GetAllvwCustomer().ToList();
                //var c = cc.Where(item => item.Code.ToLower().Trim() == model.Code.ToLower().Trim()).FirstOrDefault();

                //if (c != null)
                //{
                //    TempData[Globals.FailedMessageKey] = "Khách hàng này đã tồn tại!";
                //    return RedirectToAction("Detail", new { Id = c.Id });
                //}

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

                //  var customer = CustomerRepository.GetAllCustomer()
                //.Where(item => item.Code == model.Code).FirstOrDefault();
                //  if (customer != null)
                //  {
                //      TempData[Globals.FailedMessageKey] = "Tạo khách hàng này không thành công";

                //      ViewBag.FailedMessage01 = TempData["FailedMessage"];

                //      return View(model) /*RedirectToAction("Detail", new { Id = customer.Id })*/;
                //  }

                var Customer = new Domain.Account.Entities.Customer();

                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    var GroupPrice = Request["GroupPrice"];
                    AutoMapper.Mapper.Map(model, Customer);
                    Customer.IsDeleted = false;
                    Customer.CreatedUserId = WebSecurity.CurrentUserId;
                    Customer.ModifiedUserId = WebSecurity.CurrentUserId;
                    Customer.CreatedDate = DateTime.Now;
                    Customer.ModifiedDate = DateTime.Now;
                    Customer.CustomerType = "Customer";
                    Customer.BranchId = intBrandID;
                    Customer.GroupPrice = GroupPrice;
                    Customer.Address = model.Address;
                    Customer.Code = model.Code;
                    Customer.CompanyName = model.CompanyName;
                    Customer.Phone = model.Phone;
                    Customer.TaxCode = model.TaxCode;
                    Customer.BankAccount = model.BankAccount;
                    Customer.BankName = model.BankName;
                    Customer.CityId = model.CityId;
                    Customer.DistrictId = model.DistrictId;
                    Customer.WardId = model.WardId;
                    Customer.LastName = model.LastName;
                    Customer.FirstName = model.FirstName;
                    Customer.Gender = model.Gender;
                    Customer.Birthday = model.Birthday;
                    Customer.Mobile = model.Mobile;
                    Customer.Email = model.Email;
                    Customer.Note = model.Note;
               
                    //CustomerRepository.InsertCustomer(Customer);

                    //begin kiem tra trung ma khach hang
                    Customer.Code = Erp.BackOffice.Helpers.Common.GetOrderNo("Customer", model.Code);
                    var c = CustomerRepository.GetAllvwCustomer()
                   .Where(item => item.Mobile == Customer.Mobile).FirstOrDefault();
                    if (c != null)
                    {
                        TempData[Globals.FailedMessageKey] = "Khách hàng này đã bị trùng số điện thoại với khách hàng khác, vui lòng kiểm tra lại!";
                        ViewBag.FailedMessage01 = TempData["FailedMessage"];
                        return View(model);
                    }
                    //end kiem tra trung ma khach hang
                    CustomerRepository.InsertCustomer(Customer);
                    //CustomerRepository.UpdateCustomer(Customer);
                    Erp.BackOffice.Helpers.Common.SetOrderNo("Customer");
                    scope.Complete();
                }

                //tạo liên hệ cho khách hàng
                if (string.IsNullOrEmpty(model.FirstName) == false && string.IsNullOrEmpty(model.LastName) == false)
                {
                    var contact = new Domain.Account.Entities.Contact();
                    AutoMapper.Mapper.Map(model, contact);
                    contact.IsDeleted = false;
                    contact.CreatedUserId = WebSecurity.CurrentUserId;
                    contact.ModifiedUserId = WebSecurity.CurrentUserId;
                    contact.CreatedDate = DateTime.Now;
                    contact.ModifiedDate = DateTime.Now;
                    contact.CustomerId = Customer.Id;

                    ContactRepository.InsertContact(contact);
                }

                if (IsPopup)
                {
                    var name = Customer.LastName + " " + Customer.FirstName ;
                    var cus = CustomerRepository.GetvwCustomerById(Customer.Id);
                    var wardName = string.IsNullOrEmpty(cus.WardName) == false ? ("-" + cus.WardName) : "";
                    var districtName = string.IsNullOrEmpty(cus.DistrictName) == false ? ("-" + cus.DistrictName) : "";
                    var provinceName = string.IsNullOrEmpty(cus.ProvinceName) == false ? ("-" + cus.ProvinceName) : "";
                    var address = cus.Address + wardName + districtName + provinceName;
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage(" + Customer.Id + ",'" + name + "','" + Customer.CompanyName + "','" + Customer.TaxCode + "','" + Customer.BankName + "','" + Customer.BankAccount + "','" + address + "','" + cus.Phone + "')" });
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Detail", new { Id = Customer.Id });
            }

            return View(model);
        }




        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create_POS(CustomerViewModel model)
        {
            try
            {
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

                //Kiểm tra trùng tên số điện thoại thì không cho lưu
                //var cc = CustomerRepository.GetAllvwCustomer().ToList();
                //var c = cc.Where(item => item.Code.ToLower().Trim() == model.Code.ToLower().Trim()).FirstOrDefault();

                //if (c != null)
                //{
                //    TempData[Globals.FailedMessageKey] = "Khách hàng này đã tồn tại!";
                //    return RedirectToAction("Detail", new { Id = c.Id });
                //}
                var Customer = new Domain.Account.Entities.Customer();
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    AutoMapper.Mapper.Map(model, Customer);
                    Customer.IsDeleted = false;
                    Customer.CreatedUserId = WebSecurity.CurrentUserId;
                    Customer.ModifiedUserId = WebSecurity.CurrentUserId;
                    Customer.CreatedDate = DateTime.Now;
                    Customer.ModifiedDate = DateTime.Now;
                    Customer.CustomerType = "Customer";
                    Customer.BranchId = intBrandID;

                    Customer.Address = model.Address;
                    Customer.Code = model.Code;
                    Customer.CompanyName = model.CompanyName;
                    Customer.Phone = model.Phone;
                    Customer.TaxCode = model.TaxCode;
                    Customer.BankAccount = model.BankAccount;
                    Customer.BankName = model.BankName;
                    Customer.CityId = model.CityId;
                    Customer.DistrictId = model.DistrictId;
                    Customer.WardId = model.WardId;
                    Customer.LastName = model.LastName;
                    Customer.FirstName = model.FirstName;
                    Customer.Gender = model.Gender;
                    Customer.Birthday = model.Birthday;
                    Customer.Mobile = model.Mobile;
                    Customer.Email = model.Email;
                    Customer.Note = model.Note;
                    Customer.Code = Erp.BackOffice.Helpers.Common.GetOrderNo("Customer", model.Code);
                    var customer = CustomerRepository.GetAllCustomer()
                  .Where(item => item.Mobile == model.Mobile).FirstOrDefault();
                    if (customer != null)
                    {
                        TempData[Globals.FailedMessageKey] = "Khách hàng trùng số điện thoại ";
                        ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
                        return Json(0);
                    }


                   
                    CustomerRepository.InsertCustomer(Customer);


                    //var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Customer");
                    //Customer.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, Customer.Id);
                    // CustomerRepository.UpdateCustomer(Customer);

                    Erp.BackOffice.Helpers.Common.SetOrderNo("Customer");
                    scope.Complete();
                }

                //tạo liên hệ cho khách hàng
                if (string.IsNullOrEmpty(model.FirstName) == false && string.IsNullOrEmpty(model.LastName) == false)
                {
                    var contact = new Domain.Account.Entities.Contact();
                    AutoMapper.Mapper.Map(model, contact);
                    contact.IsDeleted = false;
                    contact.CreatedUserId = WebSecurity.CurrentUserId;
                    contact.ModifiedUserId = WebSecurity.CurrentUserId;
                    contact.CreatedDate = DateTime.Now;
                    contact.ModifiedDate = DateTime.Now;
                    contact.CustomerId = Customer.Id;

                    ContactRepository.InsertContact(contact);
                }
                return Json(Customer.Id);
            }
            catch (Exception ex)
            {

                return Json(0);
            };
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
                    var item = CustomerRepository.GetCustomerById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        //if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        //{
                        //    TempData["FailedMessage"] = "NotOwner";
                        //    return RedirectToAction("Index");
                        //}

                        item.IsDeleted = true;
                        CustomerRepository.UpdateCustomer(item);
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

        #region Detail
        public ActionResult Detail(int? Id, bool? IsNullLayOut)
        {
            var customer = CustomerRepository.GetvwCustomerById(Id.Value);
            int TagetMonth = Request["TargetMonth"] != null ? int.Parse(Request["TargetMonth"]) : -1;
            int TagetYear = Request["TargetYear"] != null ? int.Parse(Request["TargetYear"]) : -1;
            ViewBag.IsNullLayOut = IsNullLayOut;
            
            var model = new CustomerViewModel();
            if (customer != null && customer.IsDeleted != true)
            {
                AutoMapper.Mapper.Map(customer, model);
                var service = productInvoiceRepository.GetAllvwProductInvoice().Where(x => x.CustomerId == customer.Id).AsEnumerable()
                    .Select(x => new ProductInvoiceViewModel
                    {
                        Code = x.Code,
                        CodeInvoiceRed = x.CodeInvoiceRed,
                        Note = x.Note,
                        Id = x.Id,
                        TotalAmount = x.TotalAmount,
                        CreatedDate = x.CreatedDate,
                        SalerFullName = x.SalerFullName,
                        BranchName = x.BranchName
                    });
                if (TagetMonth != -1)
                {
                    service = service.Where(n => n.CreatedDate.Value.Month == TagetMonth);
                }
                if (TagetYear != -1)
                {
                    service = service.Where(n => n.CreatedDate.Value.Year == TagetYear);
                }

                ViewBag.service = service;

                //Lấy công nợ hiện tại của KH
                model.Liabilities = transactionLiabilitiesRepository.GetvwAccount_Liabilities()
                    .Where(item => item.TargetModule == "Customer" && item.TargetCode == customer.Code)
                    .Select(item => item.Remain).FirstOrDefault();

                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];

                //hai



                var mobileList = CustomerRepository.GetAllvwCustomer().Where(x => x.Id != customer.Id && x.IsDeleted != true)
                        .Select(x => new CustomerViewModel
                        {
                            Id = x.Id,
                            Mobile = x.Mobile
                        }).ToList();
                ViewBag.mobileList =  mobileList;


                return View(model);
            }
            TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin khách hàng. Vui lòng kiểm tra lại!";
            return View(model);
        }

        public ViewResult NewDetail(int? Id)
        {
            var pv = productInvoiceRepository.GetvwProductInvoiceById(Id.Value);
            var model = new CustomerViewModel();
            var customer = CustomerRepository.GetvwCustomerById(pv.CustomerId.Value);
            if (customer != null && customer.IsDeleted != true)
            {
                
                AutoMapper.Mapper.Map(customer, model);

                //if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                //{
                //    TempData["FailedMessage"] = "NotOwner";
                //    return RedirectToAction("Index");
                //}               

                return View(model);
            }

            TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin khách hàng. Vui lòng kiểm tra lại!";
            return View("ProductInvoice/Index");


        }
        #endregion
        #region Merge 
        // Gop khach 
        [HttpPost]
        public JsonResult Detail(string Id ,string searchTermId)
        {

            try
            {


                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {

                    var customer = CustomerRepository.GetCustomerById(int.Parse(Id));
                    if (customer != null && customer.IsDeleted != true)
                    {
                        var customer1 = CustomerRepository.GetCustomerById(int.Parse(searchTermId));
                        if (customer1 != null && customer1.IsDeleted == false)
                        {
                            customer1.IsDeleted = true;
                            CustomerRepository.UpdateCustomer(customer1);

                            var pi = productInvoiceRepository.GetAllProductInvoice().Where(x => x.CustomerId == customer1.Id && x.IsDeleted != true).ToList();
                            foreach (var p in pi)
                            {
                                 p.CustomerId = customer.Id;
                                 productInvoiceRepository.UpdateProductInvoice(p);
                            }
                            var re = receiptRepository.GetAllReceipts().Where(x => x.CustomerId == customer1.Id && x.IsDeleted == false).ToList();

                            if(re != null)
                            {
                                foreach (var r in re)
                                {
                                    r.CustomerId = customer.Id;
                                    receiptRepository.UpdateReceipt(r);
                                }
                            }
                                
                            

                            // TempData[Globals.SuccessMessageKey] = "Đã gộp khách hàng thành công";
                            scope.Complete();
                            return Json(1);

                        }
                        //TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin khách hàng có số điện thoại này. Vui lòng kiểm tra lại!";
                        return Json(0);

                    }



                    


                    //TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin khách hàng. Vui lòng kiểm tra lại!";

                }
            }
            catch(Exception ex)
            {
                return Json(0);

            }

            return Json(0);



        }


        [HttpPost]
        [ValidateInput(false)]
        public JsonResult GetMobile(string search)
        {
            var customer = CustomerRepository.GetAllCustomer().Where(x => x.Mobile == search && x.IsDeleted != true).FirstOrDefault();
            if (customer==null)
            {
                TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin khách hàng. Vui lòng kiểm tra lại!";
                return Json(0);
            }
            return new JsonResult { Data = customer, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region SendSMS
        [HttpPost]
        public ContentResult SendSMS(int Id)
        {
            var customer = CustomerRepository.GetvwCustomerById(Id);
            if (customer != null && !string.IsNullOrEmpty(customer.Phone))
            {
                string User = "dgthcm";
                string Password = "147a@258";
                string CPCode = "DIGITAL_HCM";
                string RequestID = "1";
                string UserID = "84987292389";
                string ReceiverID = "84987292389";
                string ServiceID = "N.HuongTest";
                string CommandCode = "bulksms";
                string sContent = "test gui tin nhan ngoc huong spa";
                string ContentType = "0";

                var service = new ServiceSMS.CcApiClient();
                var result = service.wsCpMt(User, Password, CPCode, RequestID, UserID, ReceiverID, ServiceID, CommandCode,
                    sContent, ContentType);

                return Content(result.message);
            }

            return Content("");
        }
        #endregion

        #region Client
        public ActionResult Client()
        {

            return View();
        }
        #endregion

        //#region ClientListProductInvoice
        //public ActionResult ClientListProductInvoice(string txtCode, string txtCusName)
        //{
        //    ////mở camera ở server.
        //    //Erp.BackOffice.Hubs.ErpHub.ShowOrHiddenCamera(true, "show");

        //    var q = productInvoiceRepository.GetAllvwProductInvoiceFull();
        //    var model = q.Select(item => new ProductInvoiceViewModel
        //    {
        //        Id = item.Id,
        //        IsDeleted = item.IsDeleted,
        //        CreatedUserId = item.CreatedUserId,
        //        CreatedDate = item.CreatedDate,
        //        ModifiedUserId = item.ModifiedUserId,
        //        ModifiedDate = item.ModifiedDate,
        //        Code = item.Code,
        //        CustomerCode = item.CustomerCode,
        //        CustomerName = item.CustomerName,
        //        ShipCityName = item.ShipCityName,
        //        TotalAmount = item.TotalAmount,
        //        Discount = item.Discount,
        //        TaxFee = item.TaxFee,
        //        CodeInvoiceRed = item.CodeInvoiceRed,
        //        Status = item.Status,
        //        IsArchive = item.IsArchive,
        //        ProductOutboundId = item.ProductOutboundId,
        //        ProductOutboundCode = item.ProductOutboundCode,
        //        Note = item.Note,
        //        CancelReason = item.CancelReason,
        //        CustomerId = item.CustomerId,
        //        BranchId=item.BranchId,
        //        BranchName=item.BranchName
        //    }).OrderByDescending(m => m.ModifiedDate).ToList();

        //    if (Helpers.Common.CurrentUser.BranchId != null && Helpers.Common.CurrentUser.BranchId.Value > 0)
        //    {
        //        model = model.Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(txtCode))
        //    {
        //        model = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.Code).Contains(Helpers.Common.ChuyenThanhKhongDau(txtCode))).ToList();
        //    }

        //    if (!string.IsNullOrEmpty(txtCusName))
        //    {
        //        txtCusName = Helpers.Common.ChuyenThanhKhongDau(txtCusName);
        //        model = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.CustomerName).Contains(txtCusName)).ToList();
        //    }
        //    model = model.Take(15).ToList();
        //    return View(model);
        //}
        //#endregion

        #region Camera
        public ActionResult Camera(int? Id, string ConnectionID)
        {
            if (Id != null)
            {
                var customer = CustomerRepository.GetvwCustomerById(Id.Value);
                CustomerViewModel model = null;
                if (customer != null && customer.IsDeleted != true)
                {
                    model = new CustomerViewModel();
                    AutoMapper.Mapper.Map(customer, model);
                    ViewBag.SuccessMessage = TempData["SuccessMessage"];
                    ViewBag.FailedMessage = TempData["FailedMessage"];
                    ViewBag.AlertMessage = TempData["AlertMessage"];
                    ViewBag.ConnectionID = ConnectionID;
                    return View(model);
                }
            }

            return View();
        }
        #endregion

        #region TakePhoto
        public ActionResult TakePhoto(int Id, int UserId)
        {
            var customer = CustomerRepository.GetvwCustomerById(Id);
            CustomerViewModel model = null;
            //if (Erp.BackOffice.Helpers.Common.GetSetting("status_camera_server") == "hidden")
            //{
            //    TempData[Globals.FailedMessageKey] = "Hiện tại client đang chụp hình! Nên bạn không thể chụp hình. Vui lòng tắt chụp hình ở client";
            //    return View(model);
            //}
            ViewBag.UserId = UserId;
            if (customer != null && customer.IsDeleted != true)
            {
                model = new CustomerViewModel();
                AutoMapper.Mapper.Map(customer, model);

                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];

                return View(model);
            }

            TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin khách hàng. Vui lòng kiểm tra lại!";
            return View(model);
        }

        [HttpPost]
        public ActionResult TakePhoto(CustomerViewModel model)
        {
            if (Request["Submit"] == "Save" && !string.IsNullOrEmpty(model.Image))
            {
                var Customer = CustomerRepository.GetCustomerById(model.Id);
                Customer.ModifiedUserId = WebSecurity.CurrentUserId;
                Customer.ModifiedDate = DateTime.Now;
                Customer.Image = Customer.Code + ".png";

                //Lưu hình
                string base64 = model.Image.Substring(model.Image.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                //convert to an image (or do whatever else you need to do)
                Image image;
                using (MemoryStream ms = new MemoryStream(data))
                {
                    image = Image.FromStream(ms);
                }

                var customerImagePath = Helpers.Common.GetSetting("uploads_image_path_customer");
                image.Save(Server.MapPath("~" + customerImagePath) + Customer.Image, System.Drawing.Imaging.ImageFormat.Png);

                CustomerRepository.UpdateCustomer(Customer);
                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
            }

            TempData[Globals.FailedMessageKey] = "Vui lòng chụp ảnh!";
            return RedirectToAction("TakePhoto", new { Id = model.Id });
        }
        #endregion

        #region Approval
        public ActionResult Approval()
        {
            var q = CustomerRepository.GetAllCustomer().ToList();
            for (int i = 0; i < q.Count(); i++)
            {
                if (!string.IsNullOrEmpty(q[i].CompanyName))
                {
                    q[i].SearchFullName = Helpers.Common.ChuyenThanhKhongDau(q[i].CompanyName);
                    CustomerRepository.UpdateCustomer(q[i]);
                }
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region SavePoint
        public static void SavePoint(int Id, int Point)
        {

            Erp.Domain.Account.Repositories.CustomerRepository customerRepository = new Erp.Domain.Account.Repositories.CustomerRepository(new Domain.Account.ErpAccountDbContext());
            var Customer = customerRepository.GetCustomerById(Id);
            //var customerPoint = new Domain.Account.Entities.Customer();
            Customer.Point += Point;
            customerRepository.InsertCustomer(Customer);
        }

        #endregion

        #region PhotoCustomer
        public ActionResult PhotoCustomer(int Id, int UserId)
        {
            var usinglogdetail = usingServiceLogdetailRepository.GetvwUsingServiceLogDetailById(Id);
            var customer = CustomerRepository.GetvwCustomerById(usinglogdetail.CustomerId.Value);
            UsingServiceLogDetailViewModel model = null;
            if (customer != null && customer.IsDeleted != true)
            {
                model = new UsingServiceLogDetailViewModel();
                AutoMapper.Mapper.Map(usinglogdetail, model);
                var customerModel = new CustomerViewModel();
                AutoMapper.Mapper.Map(customer, customerModel);
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];
                ViewBag.Customer = customerModel;
                ViewBag.UserId = UserId;
                return View(model);
            }

            TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin khách hàng. Vui lòng kiểm tra lại!";
            return View(model);
        }

        [HttpPost]
        public ActionResult PhotoCustomer(UsingServiceLogDetailViewModel model)
        {
            if (Request["Submit"] == "Save" && model.DetailList.Count() > 0)
            {
                var usinglogdetail = usingServiceLogdetailRepository.GetvwUsingServiceLogDetailById(model.Id);
                var Customer = CustomerRepository.GetCustomerById(usinglogdetail.CustomerId.Value);
                var DocumentField = new DocumentField();
                var type = "";
                if (usinglogdetail.Type == "usedservice")
                {
                    type = " sử dụng dịch vụ ngày " + usinglogdetail.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
                }
                else
                {
                    type = " tái khám ngày " + usinglogdetail.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
                }
                DocumentField.IsDeleted = false;
                DocumentField.CreatedUserId = WebSecurity.CurrentUserId;
                DocumentField.ModifiedUserId = WebSecurity.CurrentUserId;
                DocumentField.AssignedUserId = WebSecurity.CurrentUserId;
                DocumentField.CreatedDate = DateTime.Now;
                DocumentField.ModifiedDate = DateTime.Now;
                DocumentField.Name = Customer.CompanyName + type;
                //  DocumentField.DocumentTypeId = DocumentTypeId;
                // DocumentField.IsSearch = "";
                DocumentField.Category = "UsingServiceLogDetail";
                DocumentField.CategoryId = usinglogdetail.Id;
                DocumentFieldRepository.InsertDocumentField(DocumentField);
                var prefix = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_DocumentField");
                DocumentField.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix, DocumentField.Id);
                DocumentFieldRepository.UpdateDocumentField(DocumentField);
                int dem = 0;
                foreach (var item in model.DetailList)
                {
                    var DocumentAttribute = new DocumentAttribute();
                    DocumentAttribute.IsDeleted = false;
                    DocumentAttribute.CreatedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.ModifiedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.AssignedUserId = WebSecurity.CurrentUserId;
                    DocumentAttribute.CreatedDate = DateTime.Now;
                    DocumentAttribute.ModifiedDate = DateTime.Now;
                    DocumentAttribute.OrderNo = dem++;
                    DocumentAttribute.File = Customer.Code + "(" + DocumentField.Id + "_" + DocumentAttribute.OrderNo + ")" + ".png";
                    DocumentAttribute.Size = "";
                    DocumentAttribute.TypeFile = "png";
                    DocumentAttribute.DocumentFieldId = DocumentField.Id;
                    DocumentAttributeRepository.InsertDocumentAttribute(DocumentAttribute);
                    //Lưu hình
                    string base64 = item.File.Substring(item.File.IndexOf(',') + 1);
                    base64 = base64.Trim('\0');
                    byte[] data = Convert.FromBase64String(base64);

                    //convert to an image (or do whatever else you need to do)
                    Image image;
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        image = Image.FromStream(ms);
                    }

                    var customerImagePath = Helpers.Common.GetSetting("UsingServiceLogDetail");
                    image.Save(Server.MapPath("~" + customerImagePath) + DocumentAttribute.File, System.Drawing.Imaging.ImageFormat.Png);

                }

                return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "" });
            }

            TempData[Globals.FailedMessageKey] = "Vui lòng chụp ảnh!";
            return RedirectToAction("PhotoCustomer", new { Id = model.Id });
        }
        #endregion

        #region ProfileImage
        public ActionResult ProfileImage(int? CustomerId, int? ProductInvoiceId)
        {
            //đóng camera ở server.
            Erp.BackOffice.Hubs.ErpHub.ShowOrHiddenCamera(false, "hidden");
            if (CustomerId != null && CustomerId.Value > 0)
            {
                var customer = CustomerRepository.GetvwCustomerById(CustomerId.Value);
                if (customer != null)
                {
                    var model = new CustomerViewModel();
                    AutoMapper.Mapper.Map(customer, model);
                    if (string.IsNullOrEmpty(model.Image))//Đã có hình
                    {
                        model.Image_Path = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(model.Image, "uploads_image_path_customer", "user");
                    }
                    ViewBag.profile_image_width = Erp.BackOffice.Helpers.Common.GetSetting("profile_image_width");
                    ViewBag.profile_image_height = Erp.BackOffice.Helpers.Common.GetSetting("profile_image_height");

                    ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
                    ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
                    return View(model);
                }
                else
                {
                    ViewBag.NewPage = true;
                    ViewBag.SuccessMessage = "Chưa có thông tin";
                    return View();
                }
            }
            else
            {
                ViewBag.NewPage = true;
                ViewBag.SuccessMessage = TempData[Globals.SuccessMessageKey];
                ViewBag.FailedMessage = TempData[Globals.FailedMessageKey];
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProfileImage(CustomerViewModel model)
        {
            if (Request["Submit"] == "Save")
            {
                var ImagePath = Helpers.Common.GetSetting("uploads_image_path_customer");
                var customer = CustomerRepository.GetCustomerById(model.Id);
                var name = customer.Code + ".jpg";
                //Lưu hình từ chụp hình
                if (!string.IsNullOrEmpty(model.Image_File))
                {
                    string base64 = model.Image_File.Substring(model.Image_File.IndexOf(',') + 1);
                    base64 = base64.Trim('\0');
                    byte[] data = Convert.FromBase64String(base64);

                    //convert to an image (or do whatever else you need to do)
                    Image image;
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        image = Image.FromStream(ms);
                    }

                    image.Save(Path.Combine(Server.MapPath("~" + ImagePath), name), System.Drawing.Imaging.ImageFormat.Jpeg);
                    image.Dispose();
                    //var customer = CustomerRepository.GetCustomerById(model.Id);
                    customer.ModifiedUserId = WebSecurity.CurrentUserId;
                    customer.ModifiedDate = DateTime.Now;
                    customer.Image = name;
                    customer.ModifiedDate = DateTime.Now;
                    CustomerRepository.UpdateCustomer(customer);
                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                }
                else
                {
                    TempData[Globals.FailedMessageKey] = "Không có hình! Không thể lưu";
                    return View(model);
                }
            }

            return RedirectToAction("CameraInvoice", "Customer", new { area = "Account", ProductInvoiceId = Request["ProductInvoiceId"] });
        }

        #endregion

        #region CameraUsingService
        //public ActionResult CameraUsingService(int Id)
        //{
        //    var usinglogdetail = usingServiceLogdetailRepository.GetvwUsingServiceLogDetailById(Id);
        //    UsingServiceLogDetailViewModel model = null;
        //    // xóa file đính kèm trong session để bắt đầu thêm mới
        //    Session["file"] = null;
        //    if (usinglogdetail != null && usinglogdetail.IsDeleted != true)
        //    {
        //        model = new UsingServiceLogDetailViewModel();
        //        AutoMapper.Mapper.Map(usinglogdetail, model);
        //        ViewBag.SuccessMessage = TempData["SuccessMessage"];
        //        ViewBag.FailedMessage = TempData["FailedMessage"];
        //        ViewBag.AlertMessage = TempData["AlertMessage"];
        //        return View(model);
        //    }

        //    TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin khách hàng sử dụng dịch vụ/tái khám. Vui lòng kiểm tra lại!";
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult CameraUsingService(UsingServiceLogDetailViewModel model)
        //{
        //    if (Request["Submit"] == "Save")
        //    {
        //        var usinglogdetail = usingServiceLogdetailRepository.GetvwUsingServiceLogDetailById(model.Id);
        //        var type = "";
        //        if (usinglogdetail.Type == "usedservice")
        //        {
        //            type = " sử dụng dịch vụ ngày " + usinglogdetail.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
        //        }
        //        else
        //        {
        //            type = " tái khám ngày " + usinglogdetail.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
        //        }
        //        string FailedMessageKey = "";
        //        //lưu file
        //        DocumentFieldController.SaveUpload(usinglogdetail.CustomerName + type, "", usinglogdetail.Id, "UsingServiceLogDetail", FailedMessageKey,usinglogdetail.CustomerCode);
        //        if (!string.IsNullOrEmpty(FailedMessageKey))
        //        {
        //            TempData[Globals.FailedMessageKey] = FailedMessageKey;
        //        }
        //        //send alert
        //        var user=Erp.BackOffice.Helpers.Common.CurrentUser;
        //        var list_User=userRepository.GetUserbyUserType("Admin chi nhánh").Where(x=>x.BranchId==user.BranchId).ToList();
        //        for (int i = 0; i < list_User.Count(); i++)
        //        {
        //            string titile = "";
        //            if (usinglogdetail.Type == "usedservice")
        //            {
        //                titile="Chụp hình thành công khách hàng " + usinglogdetail.CustomerName + " sử dụng DV: " + usinglogdetail.ServiceName;
        //            }
        //            else
        //            { 
        //                titile="Chụp hình thành công khách hàng " + usinglogdetail.CustomerName + " tái khám DV: " + usinglogdetail.ServiceName;
        //            }
        //            Erp.BackOffice.Hubs.ErpHub.SendAlerts(list_User[i].Id, user.Id,titile);
        //        }

        //        return RedirectToAction("CameraUsingService", "Customer", new { area = "Account", Id =model.Id});
        //    }
        //    TempData[Globals.FailedMessageKey] = "Vui lòng chụp ảnh!";
        //    return RedirectToAction("CameraUsingService", "Customer", new { area = "Account", Id = model.Id });
        //}
        #endregion

        #region ClientListUsingService
        public ActionResult ClientListUsingService(string txtCode, string txtCusName)
        {
            var usinglogdetail = usingServiceLogdetailRepository.GetAllvwUsingServiceLogDetail();
            var model = usinglogdetail.Select(item => new UsingServiceLogDetailViewModel
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
                Status = item.Status,
                CustomerId = item.CustomerId,
                CustomerImage = item.CustomerImage,
                IsVote = item.IsVote,
                Name = item.Name,
                ProductInvoiceCode = item.ProductInvoiceCode,
                ServiceName = item.ServiceName,
                UsingServiceId = item.UsingServiceId,
                Type = item.Type,
                BranchId = item.BranchId
            }).OrderByDescending(m => m.CreatedDate).ToList();

            if (!string.IsNullOrEmpty(Helpers.Common.CurrentUser.DrugStore))
            {
                model = model.Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true).ToList();
            }
            if (!string.IsNullOrEmpty(txtCode))
            {
                model = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.Code).Contains(Helpers.Common.ChuyenThanhKhongDau(txtCode))).ToList();
            }

            if (!string.IsNullOrEmpty(txtCusName))
            {
                txtCusName = Helpers.Common.ChuyenThanhKhongDau(txtCusName);
                model = model.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.CustomerName).Contains(txtCusName)).ToList();
            }
            model = model.Take(15).ToList();
            return View(model);
        }
        #endregion

        #region CameraInvoice
        public ActionResult CameraInvoice(int ProductInvoiceId)
        {
            var productInvoice = productInvoiceRepository.GetvwProductInvoiceById(ProductInvoiceId);
            ProductInvoiceViewModel model = null;
            // xóa file đính kèm trong session để bắt đầu thêm mới
            Session["file"] = null;
            if (productInvoice != null && productInvoice.IsDeleted != true)
            {
                model = new ProductInvoiceViewModel();
                AutoMapper.Mapper.Map(productInvoice, model);
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                ViewBag.FailedMessage = TempData["FailedMessage"];
                ViewBag.AlertMessage = TempData["AlertMessage"];
                return View(model);
            }

            TempData[Globals.FailedMessageKey] = "Không tìm thấy thông tin hóa đơn. Vui lòng kiểm tra lại!";
            return View(model);
        }

        //[HttpPost]
        //public ActionResult CameraInvoice(ProductInvoiceViewModel model)
        //{
        //    if (Request["Submit"] == "Save")
        //    {
        //        string FailedMessageKey = "";
        //        //lưu file
        //        DocumentFieldController.SaveUpload(model.Code, "", model.Id, "ProductInvoice", FailedMessageKey, model.Code);
        //        if (!string.IsNullOrEmpty(FailedMessageKey))
        //        {
        //            TempData[Globals.FailedMessageKey] = FailedMessageKey;
        //        }
        //        //send alert
        //        var user = Erp.BackOffice.Helpers.Common.CurrentUser;
        //        var list_User = userRepository.GetUserbyUserType("Admin chi nhánh").Where(x => x.BranchId == user.BranchId).ToList();
        //        for (int i = 0; i < list_User.Count(); i++)
        //        {
        //            string titile = "Chụp hình thành công hóa đơn " + model.Code + " của khách " + model.CustomerName;
        //            Erp.BackOffice.Hubs.ErpHub.SendAlerts(list_User[i].Id, user.Id, titile);
        //        }
        //        TempData[Globals.SuccessMessageKey] = "Lưu hình hóa đơn thành công!";
        //        return RedirectToAction("CameraInvoice", "Customer", new { area = "Account", ProductInvoiceId = model.Id });
        //    }
        //    TempData[Globals.FailedMessageKey] = "Vui lòng chụp ảnh!";
        //    return RedirectToAction("CameraInvoice", "Customer", new { area = "Account", ProductInvoiceId = model.Id });
        //}
        #endregion

        #region ListUserDrugStore

        public ViewResult ListNT(string CardCode, string txtCusName, string txtCode, string Phone, string ProvinceId, string DistrictId, string WardId)
        {
            IEnumerable<CustomerNTViewModel> q = CustomerRepository.GetAllvwCustomer()
                 .Where(x => x.CustomerType == "DrugStore").AsEnumerable()
                //Where(x => x.BranchId == Helpers.Common.CurrentUser.BranchId)
                .Select(item => new CustomerNTViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    CompanyName = item.CompanyName,
                    Phone = item.Phone,
                    Address = item.Address,
                    Note = item.Note,
                    CardCode = item.CardCode,
                    SearchFullName = item.SearchFullName,
                    Image = item.Image,
                    Birthday = item.Birthday,
                    Gender = item.Gender,
                    IdCardDate = item.IdCardDate,
                    IdCardIssued = item.IdCardIssued,
                    IdCardNumber = item.IdCardNumber,
                    CardIssuedName = item.CardIssuedName,
                    WardName = item.WardName,
                    ProvinceName = item.ProvinceName,
                    DistrictName = item.DistrictName,
                    Mobile = item.Mobile,
                    Email = item.Email,
                    BranchId = item.BranchId,
                    Image_File = item.Image,
                    GenderName = item.GenderName,
                    UserName = item.UserName

                });

            bool hasSearch = false;
            if (!Filters.SecurityFilter.IsAdmin())
            {
                q = q.Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
                hasSearch = true;
            }
            if (!string.IsNullOrEmpty(CardCode))
            {
                q = q.Where(x => x.CardCode.Contains(CardCode));
                hasSearch = true;
            }

            if (!string.IsNullOrEmpty(txtCusName))
            {
                txtCusName = Helpers.Common.ChuyenThanhKhongDau(txtCusName);
                q = q.Where(x => Helpers.Common.ChuyenThanhKhongDau(x.FullName).Contains(txtCusName));
                hasSearch = true;
            }

            if (!string.IsNullOrEmpty(txtCode))
            {
                q = q.Where(x => x.Code.Contains(txtCode));
                hasSearch = true;
            }
            if (!string.IsNullOrEmpty(Phone))
            {
                q = q.Where(x => x.Phone.Contains(Phone));
                hasSearch = true;
            }
            if (!string.IsNullOrEmpty(ProvinceId))
            {
                q = q.Where(item => item.CityId == ProvinceId);
                hasSearch = true;
            }
            if (!string.IsNullOrEmpty(DistrictId))
            {
                q = q.Where(item => item.DistrictId == DistrictId);
                hasSearch = true;
            }
            if (!string.IsNullOrEmpty(WardId))
            {
                q = q.Where(item => item.WardId == WardId);
                hasSearch = true;
            }

            if (hasSearch)
            {
                q = q.OrderByDescending(m => m.CompanyName);
                hasSearch = true;
            }
            else
            {
                q = null;
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region CreateNT
        public ViewResult CreateNT(string Phone)
        {
            var model = new CustomerNTViewModel();
            model.CustomerType = "DrugStore";
            model.Phone = Phone;
            var departmentList = branchRepository.GetAllBranch().Where(x => x.ParentId != null || x.ParentId > 0)
                     .Select(item => new BranchViewModel
                     {
                         Name = item.Name,
                         Id = item.Id,
                         ParentId = item.ParentId
                     }).ToList();
            ViewBag.departmentList = departmentList;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNT(CustomerNTViewModel model)
        {
            if (ModelState.IsValid)
            {
                var DrugStore = Request["DrugStore"];
                var cus = new Customer();
                AutoMapper.Mapper.Map(model, cus);
                cus.IsDeleted = false;
                cus.CreatedUserId = WebSecurity.CurrentUserId;
                cus.ModifiedUserId = WebSecurity.CurrentUserId;
                cus.CreatedDate = DateTime.Now;
                cus.ModifiedDate = DateTime.Now;
                cus.CompanyName = model.LastName + " "  + model.FirstName ;
                cus.BranchId = Convert.ToInt32(DrugStore);
                //lấy mã.
                CustomerRepository.InsertCustomer(cus);
                var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_CustomerNT");
                cus.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, cus.Id);
                CustomerRepository.UpdateCustomer(cus);

                var userDN = userRepository.GetUserById(WebSecurity.CurrentUserId);
                var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("Image_CustomerNT"));
                if (Request.Files["file-image"] != null)
                {
                    var file = Request.Files["file-image"];
                    if (file.ContentLength > 0)
                    {
                        string image_name = cus.Code + "." + file.FileName.Split('.').Last();
                        bool isExists = System.IO.Directory.Exists(path);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(path);
                        file.SaveAs(path + image_name);
                        cus.Image = image_name;
                        CustomerRepository.UpdateCustomer(cus);
                    }

                }
                if (model.UserName != null)
                {
                    if (!CheckUsernameExists(model.UserName))
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password);

                        //==== Update User ===//
                        int userId = WebSecurity.GetUserId(model.UserName);
                        Erp.Domain.Entities.User user = userRepository.GetUserById(userId);
                        user.Address = model.Address;
                        user.CreatedDate = DateTime.Now;
                        user.DateOfBirth = model.Birthday;
                        user.Email = model.Email;
                        user.Mobile = model.Phone;
                        user.Sex = model.Gender;
                        user.UserName = model.UserName;
                        var user_type = user_typeRepository.GetUserTypeByCode(cus.PositionCode);
                        user.UserTypeId = user_type.Id;
                        user.FullName = cus.CompanyName;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Status = UserStatus.Active;
                        user.DrugStore = cus.BranchId.Value.ToString();
                        userRepository.UpdateUser(user);
                        TempData["AlertMessage"] = " Đã tạo User thành công.";
                        cus.UserId = user.Id;
                        CustomerRepository.UpdateCustomer(cus);
                    }
                }
                ViewBag.SuccessMessage = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                {
                    return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage(" + cus.Id + ",'" + cus.CompanyName + "')" });
                }
                return RedirectToAction("DetailBasicFull", "Customer", new { area = "Account", Id = cus.Id, IsLayout = true });

            }
            return View(model);
        }
        #endregion

        #region EditNT
        public ActionResult EditNT(int? Id)
        {
            var Staffs = CustomerRepository.GetvwCustomerById(Id.Value);
            if (Staffs != null && Staffs.IsDeleted != true)
            {
                var model = new CustomerNTViewModel();
                AutoMapper.Mapper.Map(Staffs, model);
                //model.UserList = Helpers.SelectListHelper.GetSelectList_User(model.UserId);
                var departmentList = branchRepository.GetAllBranch().Where(x => x.ParentId != null || x.ParentId > 0)
                   .Select(item => new BranchViewModel
                   {
                       Name = item.Name,
                       Id = item.Id,
                       ParentId = item.ParentId
                   }).ToList();
                ViewBag.departmentList = departmentList;
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditNT(CustomerNTViewModel model)
        {
            foreach (var modelKey in ModelState.Keys)
            {
                if (modelKey == "UserName" || modelKey == "Password")
                {
                    var index = ModelState.Keys.ToList().IndexOf(modelKey);
                    ModelState.Values.ElementAt(index).Errors.Clear();
                }
            }

            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var DrugStore = Request["DrugStore"];
                    var cus = CustomerRepository.GetCustomerById(model.Id);
                    AutoMapper.Mapper.Map(model, cus);
                    cus.BranchId = Convert.ToInt32(DrugStore);
                    cus.ModifiedUserId = WebSecurity.CurrentUserId;
                    cus.ModifiedDate = DateTime.Now;
                    cus.CompanyName = model.LastName + " " + model.FirstName;
                    var path = Helpers.Common.GetSetting("Image_CustomerNT");
                    var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + cus.Image);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = cus.Code + "." + file.FileName.Split('.').Last();

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            cus.Image = image_name;
                        }
                    }
                    CustomerRepository.UpdateCustomer(cus);

                    if (model.UserId != null && model.UserId.Value > 0)
                    {
                        User user = userRepository.GetUserById(model.UserId.Value);
                        user.Address = model.Address;
                        user.DateOfBirth = model.Birthday;
                        user.Email = model.Email;
                        user.Mobile = model.Phone;
                        user.Sex = model.Gender;
                        var user_type = user_typeRepository.GetUserTypeByCode(cus.PositionCode);
                        user.UserTypeId = user_type.Id;
                        //  user.UserName = model.UserName;
                        user.FullName = cus.CompanyName;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;

                        user.DrugStore = cus.BranchId.Value.ToString();
                        user.ProfileImage = cus.Image;
                        userRepository.UpdateUser(user);

                    }
                    if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                    {
                        return RedirectToAction("_ClosePopup", "Home", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                    }
                    return RedirectToAction("DetailBasicFull", "Customer", new { area = "Account", Id = cus.Id, IsLayout = true });
                }
                return View(model);
            }

            string errorMessage = string.Empty;
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errorMessage += error.ErrorMessage;
                }
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        private bool CheckUsernameExists(string username)
        {
            var user = userRepository.GetByUserName(username);

            return user != null;
        }

        public ActionResult DetailBasicFull(int? Id)
        {
            var student = CustomerRepository.GetvwCustomerById(Id.Value);
            if (student != null)
            {
                var model = new CustomerNTViewModel();
                AutoMapper.Mapper.Map(student, model);
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        public ActionResult CheckCodeExsist(int? id, string code)
        {
            code = code.Trim();
            var product = CustomerRepository.GetAllvwCustomer()
                .Where(item => item.Code == code).FirstOrDefault();
            if (product != null)
            {
                if (id == null || (id != null && product.Id != id))
                    return Content("Trùng mã khách hàng!");
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
    }
}
