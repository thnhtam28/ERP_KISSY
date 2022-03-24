using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class ProductInvoice
    {
        public string InvoiceCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Status { get; set; }
        public decimal? Total { get; set; }
        public List<Product> Products { get; set; }
    }
    public class Product {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Total { get; set; }
    }
}