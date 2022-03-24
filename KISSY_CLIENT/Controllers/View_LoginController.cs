using ERP_API.Filters;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{

    public class View_LoginController : Controller
    {
        UserOperation user;
        public View_LoginController()
        {
            user = new UserOperation();
        }
        // GET: View_Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel,string returnUrl)
        {
            var abc = user.Login(loginViewModel.UserName, loginViewModel.Password);
            if (abc != null)
            {
                Session["RoleUser"] = abc;
                return Redirect(returnUrl);
            }
              
            else
                Session["RoleUser"] = null;
            return View();
        }
    }
}