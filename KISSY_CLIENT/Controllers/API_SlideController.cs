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
    [RoutePrefix("api/Slide")]
    public class API_SlideController : ApiController
    {
        SlideOperation slideOperation;
        public API_SlideController()
        {
            slideOperation = new SlideOperation();
        }

        [HttpGet]
        [Route("GetSlides")]
        public IHttpActionResult GetSlides()
        {
            List<DM_BANNER_SLIDER> page_Slides = slideOperation.GetSlides();
            return Json(page_Slides);

        }
        //[HttpGet]
        //[Route("GetSlides")]
        //public IHttpActionResult GetSlides()
        //{
        //    List<Page_Slide> page_Slides = slideOperation.GetPage_Slides();
        //    return Json(page_Slides);

        //}
    }
}
