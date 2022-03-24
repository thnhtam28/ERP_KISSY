using ERP_API.Filters;
using ERP_API.Models;
using ERP_API.Operation.Nomal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        ApiPageSetupOperation apiPageSetup;
        public HomeController()
        {
            apiPageSetup = new ApiPageSetupOperation();
        }
        private void CEOStr(string urlWeb, string urlimage)
        {
            Page_Setup page_Setup = apiPageSetup.GetPage_Setup();
            ViewBag.CompanyName = page_Setup.CompanyName;
            ViewBag.UrlWebsite = urlWeb;
            ViewBag.Google_GTM = Common.GetSetting("Google_GTM");
            ViewBag.FacebookPixelID = Common.GetSetting("FacebookPixelID");
            ViewBag.Google_TrackingID = Common.GetSetting("Google_TrackingID");
            ViewBag.IDGoogleconversion = Common.GetSetting("IDGoogleconversion");
            ViewBag.chatbox = Common.GetSetting_Script("chatbot");

            if (string.IsNullOrEmpty(urlimage))
                ViewBag.Image = page_Setup.Logo;
            else
                ViewBag.Image = urlimage;
        }
        public ActionResult Index()
        {
            CEOStr(Url.Action("Index", "Home"), null);

            ViewBag.ecomm_prodid = "";
            ViewBag.ecomm_pagetype_FB = "Home";
            ViewBag.ecomm_pagetype_GG = "Home";
            ViewBag.ecomm_totalvalue = "0";
            ViewBag.Title = "Trang chủ";

            return View();
        }
        [AuthorByThuc]
        public ActionResult admin()
        {
            return View();
        }
    }
}
