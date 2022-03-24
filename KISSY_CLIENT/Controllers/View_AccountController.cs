
using ERP_API.Models;
using ERP_API.Models.Account;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.Nomal;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ERP_API.Controllers
{
    public class View_AccountController : Controller
    {
        apiOperation apiOperation;
        ApiInvoiceOperation invoiceOperation;
        ApiPageSetupOperation apiPageSetup;
        ApiProductOperation apiProductOperation;
        ApiResetPasswordOperation apiResetPasswordOperation;
        public View_AccountController()
        {
            apiOperation = new apiOperation();
            invoiceOperation = new ApiInvoiceOperation();
            apiPageSetup = new ApiPageSetupOperation();
            apiProductOperation = new ApiProductOperation();
            apiResetPasswordOperation = new ApiResetPasswordOperation();
        }
    
        private void CEOStr(string urlWeb, string urlimage)
        {
            Page_Setup page_Setup = apiPageSetup.GetPage_Setup();
            ViewBag.CompanyName = page_Setup.CompanyName;
            ViewBag.UrlWebsite = urlWeb;
            if (string.IsNullOrEmpty(urlimage))
                ViewBag.Image = page_Setup.Logo;
            else
                ViewBag.Image = urlimage;
        }
        public ActionResult login()
        {
            CEOStr(Url.Action("Login", "View_Account"), null);
            return View();
        }
        // GET: ACcount
        [HttpPost]
        public ActionResult login([Bind(Include = "UserName,Password,RememberMe")] LoginModel loginModel)
        {
            ViewBag.Message= apiOperation.login(loginModel);
            if (Session["UserLogin"] != null)
            {
                CustomerInfo customerInfo = (CustomerInfo)Session["UserLogin"];
                FormsAuthentication.SetAuthCookie(customerInfo.Email, true);
                return RedirectToAction("Index", "Home");
            }
               
            else
                return View();
        }
        [HttpPost]
        public ActionResult register([Bind(Include = "UserName,Password,ConfirmPassword,LastName,FirstName,Email")] RegisterModel registerModel)
        {
            if (registerModel.ConfirmPassword == registerModel.Password)
            {
                apiOperation.register(registerModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Error", "View_Error");
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Code = token;
            return View(model);
        }
        [HttpPost]
        public ActionResult recoverPassword([Bind(Include = "Email")] ForgotPasswordViewModel model)
        {
            string result=apiResetPasswordOperation.ForgotPasswordToGetCode(model);
            if (string.IsNullOrEmpty(result)|| result.Trim()== "\"\"")
            {
                string mess = "Xin vui lòng vào hộp thư của bạn để đổi mật khẩu!";
                return RedirectToAction("Message",new { mess = mess });
            }
            else
            {
                return RedirectToAction("Message", new { mess = result });
            }
        }
        public ActionResult Message(string mess)
        {
            ViewBag.mess = mess;
            return View();
        }
        public ActionResult ResetPassword([Bind(Include = "Code,Email,Password,ConfirmPassword")]ResetPasswordViewModel model)
        {
            string result = apiResetPasswordOperation.ReadCodeFromEmailAndResetPassword(model);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult accountInfo()
        {
            string userID = User.Identity.GetUserId();
            CEOStr(Url.Action("accountInfo", "View_Account"), null);

            if (!string.IsNullOrEmpty(userID))
            {
                InfomationUser infomationUser = new InfomationUser();
                List<Sale_ProductInvoice_View> sale_ProductInvoice_Views = invoiceOperation.GetInvoiceByUser(User.Identity.GetUserId());
                List<Sale_Product> sale_Products = apiProductOperation.GetPage_ViewProductUsers(User.Identity.GetUserId());
                CustomerInfo customerInfo = (CustomerInfo)Session["UserLogin"];
                infomationUser.CustomerInfo = customerInfo;
                infomationUser.Sale_ProductInvoice_Views = sale_ProductInvoice_Views;
                infomationUser.Page_ViewProductUsers = sale_Products;
                return View(infomationUser);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public ActionResult LogOff()
        {
            Session["UserLogin"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LoginFacebook()
        {
            apiOperation.loginfacebook();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordBindingModel model,string token)
        {
            string result = apiOperation.ChangePassword(model, token);
            return Json(result);
        }
    }
}