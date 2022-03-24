using ERP_API.Filters;
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
    [RoutePrefix("api/Invoice")]
    public class API_InvoiceController : ApiController
    {
        InvoiceOperation invoiceOperation;
        public API_InvoiceController()
        {
            invoiceOperation = new InvoiceOperation();
        }

        [Route("GetInvoiceByUser/{userid}")]
        public IHttpActionResult GetInfomationInvoiceUser(string userid)
        {
            List<Sale_ProductInvoice_View> sale_ProductInvoice_Views = new List<Sale_ProductInvoice_View>();
            List<Sale_ProductInvoice> listInvoice = invoiceOperation.GetSale_ProductInvoices(userid);
           
            foreach (Sale_ProductInvoice item in listInvoice)
            {
                Sale_ProductInvoice_View sale_ProductInvoice_View = new Sale_ProductInvoice_View();
                sale_ProductInvoice_View = (Sale_ProductInvoice_View)UpdateDataModel.CreateData(sale_ProductInvoice_View, item);
                List<Sale_ProductInvoiceDetail> listDetail = invoiceOperation.GetSale_ProductInvoiceDetails(item.Id);
                List<Sale_ProductInvoiceDetail_View> listdetailView = new List<Sale_ProductInvoiceDetail_View>();
                foreach (Sale_ProductInvoiceDetail item2 in listDetail)
                {
                    Sale_ProductInvoiceDetail_View sale_ProductInvoiceDetail_View = new Sale_ProductInvoiceDetail_View();
                    sale_ProductInvoiceDetail_View = (Sale_ProductInvoiceDetail_View)UpdateDataModel.CreateData(sale_ProductInvoiceDetail_View, item2);
                    listdetailView.Add(sale_ProductInvoiceDetail_View);
                    
                }
                sale_ProductInvoice_View.Sale_ProductInvoiceDetail_Views= listdetailView;
                sale_ProductInvoice_Views.Add(sale_ProductInvoice_View);
            }
            return Json(sale_ProductInvoice_Views);
        }
        [Route("GetInvoiceDetail/{id}/{userid}")]
        public IHttpActionResult GetInvoiceDetails(int id,string userid)
        {
            ProductInvoice productInvoice = new ProductInvoice();
            productInvoice = invoiceOperation.GetSale_ProductInvoice(userid,id);  
            return Json(productInvoice);
        }
    }
}
