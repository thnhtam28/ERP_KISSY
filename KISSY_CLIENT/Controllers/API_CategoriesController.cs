using ERP_API.Operation.Nomal;
using ERP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ERP_API.Operation.API;
using ERP_API.Models.ViewModel;

namespace ERP_API.Controllers
{
  
    public class API_CategoriesController : ApiController
    {
        CategoriesOperation categoriesOperation;
        public API_CategoriesController()
        {
            categoriesOperation = new CategoriesOperation();
        }

        [Route("api/category/GetAll")]
        public IHttpActionResult GetCategories()
        {
            List<System_Category> system_Categories = categoriesOperation.GetSystem_Categories();
            return Json(system_Categories);
        }
        [Route("api/category/GetWithID/{id}")]
        public IHttpActionResult GetCategories(string id)
        {
            System_Category system_Categories = categoriesOperation.GetSystem_Category(id);
            return Json(system_Categories);
        }

        [Route("api/category/GetAllWithProduct")]
        public IHttpActionResult GetAllWithProduct()
        {
            List<System_Category_View> system_Categories = categoriesOperation.GetFullProduct();
            return Json(system_Categories);
        }
    }
}
