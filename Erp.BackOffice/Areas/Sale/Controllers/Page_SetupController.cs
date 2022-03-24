using Erp.BackOffice.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.BackOffice.Areas.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class Page_SetupController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}