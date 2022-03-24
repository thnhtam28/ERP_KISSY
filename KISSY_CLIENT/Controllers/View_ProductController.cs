using ERP_API.Models.ViewModel;
using ERP_API.Operation.Nomal;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{
    public class View_ProductController : Controller
    {
        ApiProductOperation apiProductOperation;
        public View_ProductController()
        {
            apiProductOperation = new ApiProductOperation();
        }
        // GET: View_Product
        [HttpPost]
        public ActionResult Search(Search_View model)
        {

           var list= apiProductOperation.SearchProducts(model.catelogyFilerID, model.inputSearchAuto);
            return View(list);
        }
        public ActionResult listProductModalPartial(int invoiceID)
        {
            string UserID = User.Identity.GetUserId();
            ProductInvoice product = apiProductOperation.GetInvoiceDetails(invoiceID, UserID);
            return PartialView(product);
        }
        public ActionResult GetInvoiceDetails(int invoiceID)
        {
            string UserID = User.Identity.GetUserId();
            ProductInvoice product = apiProductOperation.GetInvoiceDetails(invoiceID, UserID);
            return Json(product);
        }
    }
}