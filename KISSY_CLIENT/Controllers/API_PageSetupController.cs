using ERP_API.Models;
using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_API.Controllers
{
    [RoutePrefix("api/Page")]
    public class API_PageSetupController : ApiController
    {
        PageSetupOperation pageSetupOperation;
        public API_PageSetupController()
        {
            pageSetupOperation = new PageSetupOperation();
        }

        [HttpGet]
        [Route("GetPageSetup")]
        public IHttpActionResult GetPageSetup()
        {
            Page_Setup page_Setup = pageSetupOperation.GetPage_Setup();
            return Json(page_Setup);
        }
    }
}
