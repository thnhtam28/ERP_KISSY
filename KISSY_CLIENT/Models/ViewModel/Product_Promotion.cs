using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class Product_Promotion
    {
        public decimal? GiaKM { get; set; }
        public Sale_Product Sale_Product { get; set; }
        public Sale_Commision_Customer Sale_Commision_Customer { get; set; }

        public Sale_CommissionCus Sale_CommissionCus { get; set; }
        public List<Product_Branch> Staff_Branches { get; set; }
    }
    public class Product_Branch
    {
        public Staff_Branch Staff_Branch { get; set; }
        public int? Inventory { get; set; }
    }
    public class Product_Size {
        public int ID { get; set; }
        public string Size { get; set; }
    }
}