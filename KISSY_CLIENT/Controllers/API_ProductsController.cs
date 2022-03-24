using ERP_API.Models;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using ERP_API.Operation.Nomal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_API.Controllers
{
    [RoutePrefix("api/Product")]
    public class API_ProductsController : ApiController
    {
        ProductOperation productOperation;
        CommentOperation commentOperation;
        public API_ProductsController()
        {
            productOperation = new ProductOperation();
            commentOperation = new CommentOperation();
        }
        [Route("GetAllProducts")]
        public IHttpActionResult GetAllProducts()
        {
            List<Sale_Product> sale_Products = productOperation.GetSale_Products();
            return Json(sale_Products);
        }
        [Route("GetSale_ProductsWithCategory/{id}")]
        public IHttpActionResult GetProducts(int id)
        {
            List<Sale_Product> sale_Products = productOperation.GetSale_ProductsWithCategory(id);
            return Json(sale_Products);
        }



        [Route("GetSale_ProductsWithSaleOff")]
        public IHttpActionResult GetSale_ProductsWithSaleOff()
        {
            var data = productOperation.GET_vwProduct_Promotion();
            List<Product_Promotion> lstProduct = new List<Product_Promotion>();
            foreach (var item in data)
            {
                Product_Promotion tmp = new Product_Promotion();

                tmp = productOperation.GetSale_Product_Promotion2(item.ProductId);
                lstProduct.Add(tmp);
            }
            return Json(lstProduct);
        }


        [Route("GetProduct/{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            Product_Promotion sale_Products = productOperation.GetSale_Product_Promotion(id);
            return Json(sale_Products);
        }

        [HttpGet]
        [Route("SearchProduct/{categoryid}/{searchtext}")]
        public IHttpActionResult SearchProduct(int categoryid, string searchtext)
        {
            List<Sale_Product> list = productOperation.SearchProduct(categoryid, searchtext);
            return Json(list);
        }
        [HttpGet]
        [Route("GetPage_ViewProductUsers/{id}")]
        public IHttpActionResult GetPage_ViewProductUsers(string id)
        {
            List<Page_ViewProductUser> list = productOperation.Page_ViewProductUsers(id).Take(15).ToList();
            return Json(list);
        }

        [HttpPost]
        [Route("CreateComment")]
        public IHttpActionResult CreateComment(Page_Comment page_Comment)
        {
            bool result = commentOperation.InsertComment(page_Comment);

            return Json(result);
        }
        [HttpGet]
        [Route("GetComments/{userid}/{productID}")]
        public IHttpActionResult GetComments(string userid,int productID)
        {
            List<Page_Comment> page_Comments = commentOperation.GetPage_Comments(userid, productID);

            return Json(page_Comments);
        }

        [HttpGet]
        [Route("GetAllCommentsProduct/{productID}")]
        public IHttpActionResult GetAllCommentsProduct(int productID)
        {
            List<Page_Comment> page_Comments = commentOperation.GetPage_Comments(productID);

            return Json(page_Comments);
        }
        [HttpGet]
        [Route("GetProductBestSeller")]
        public IHttpActionResult GetProduct_BestSeller()
        {
            List<Product_Promotion> result = productOperation.GetProduct_Best_seller();
            return Json(result);
        }
    }
}
