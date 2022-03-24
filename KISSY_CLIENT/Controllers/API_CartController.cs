using ERP_API.Models;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_API.Controllers
{
    [RoutePrefix("api/Cart")]
    public class API_CartController : ApiController
    {
        CartOperation cartOperation;
        public API_CartController()
        {
            cartOperation = new CartOperation();
        }
        [Route("InsertInvoice")]
        public IHttpActionResult InsertInvoice(CartCheckOut cartCheckOut)
        {
            string madh = "";
            string thongbao = "";
            string result = cartOperation.InsertInvoice(cartCheckOut,ref madh,ref thongbao);

            return Json(result);
        }

        [Route("InsertViewProduct")]
        public IHttpActionResult InsertViewProduct(Page_ViewProductUser page_ViewProductUser)
        {
           bool result = cartOperation.InsertViewProduct(page_ViewProductUser);
            return Json(result);
        }
    }
}
