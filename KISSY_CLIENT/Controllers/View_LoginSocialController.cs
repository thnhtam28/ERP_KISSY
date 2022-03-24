using ERP_API.Models;
using ERP_API.Operation.Nomal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{
    public class View_LoginSocialController : Controller
    {
        ApiSocialOperation apiSocialOperation;
        public View_LoginSocialController()
        {
            apiSocialOperation = new ApiSocialOperation();
        }
        public ActionResult GetUrl()
        {
            string host = "";
            if (ConfigurationManager.AppSettings["UrlAPI"] != null)
                host = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            else
                host = "";

            return Json(host,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProvide(string provide)
        {
            var result = apiSocialOperation.GetProvide(provide);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserInfo(string token)
        {
            UserInfoViewModel result = apiSocialOperation.GetInfoUser(token);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SingupUser(string token,string provide)
        {
            var result = apiSocialOperation.SingupUser(token, provide);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult logout()
        {
            var result = apiSocialOperation.logout();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}