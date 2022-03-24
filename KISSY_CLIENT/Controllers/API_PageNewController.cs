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
    [RoutePrefix("api/New")]
    public class API_PageNewController : ApiController
    {
        PageNewOperation pageNewOperation;
        public API_PageNewController()
        {
            pageNewOperation = new PageNewOperation();
        }

        [HttpGet]
        [Route("GetNew/{id}")]
        public IHttpActionResult GetNew(string id)
        {
            Page_New page_New = pageNewOperation.GetPage_New(id);
            return Json(page_New);
        }

        [HttpGet]
        [Route("GetAllNews")]
        public IHttpActionResult GetAllNews()
        {
            List<Page_New> page_New = pageNewOperation.GetPage_News();
            return Json(page_New);
        }


        [HttpGet]
        [Route("GetAllCategoryNews")]
        public IHttpActionResult GetAllCategoryNews()
        {
            List<Page_CategoryPost> page_New = pageNewOperation.GetPage_CategoryPosts();
            return Json(page_New);
        }
        /// <summary>
        /// id là id của category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNewWithCategory")]
        public IHttpActionResult GetNewWithCategory(string id)
        {
            List<Page_New> page_New = pageNewOperation.GetNewWithCategory(id);
            return Json(page_New);
        }


        [HttpPost]
        [Route("CreateNew")]
        public IHttpActionResult CreateNew(Page_New page_New)
        {
            bool result = pageNewOperation.CreateNew(page_New);
            return Json(result);
        }

        [HttpPost]
        [Route("CreateCategoryNew")]
        public IHttpActionResult CreateCategoryNew(Page_CategoryPost page_New)
        {
            bool result = pageNewOperation.CreateCategoryNew(page_New);
            return Json(result);
        }

        //

        [HttpPost]
        [Route("EditNew")]
        public IHttpActionResult EditNew(Page_New page_New)
        {
            bool result = pageNewOperation.EditNew(page_New);
            return Json(result);
        }

        [HttpPost]
        [Route("EditCategoryNew")]
        public IHttpActionResult EditCategoryNew(Page_CategoryPost page_New)
        {
            bool result = pageNewOperation.EditCategoryNew(page_New);
            return Json(result);
        }

        //
        [HttpGet]
        [Route("EditNew/{id}")]
        public IHttpActionResult DeleteNew(string id)
        {
            bool result = pageNewOperation.DeleteNew(id);
            return Json(result);
        }

        [HttpGet]
        [Route("DeleteCategoryNew")]
        public IHttpActionResult DeleteCategoryNew(string id)
        {
            bool result = pageNewOperation.DeleteCategoryNew(id);
            return Json(result);
        }
    }
}
