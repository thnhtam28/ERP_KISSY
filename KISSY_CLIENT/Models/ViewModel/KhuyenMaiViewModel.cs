using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class KhuyenMaiViewModel
    {
        public DM_DEALHOT DM_DEALHOT { get; set; }
        public Sale_CommissionCus Sale_CommissionCus { get; set; }
        public List<Product_Commussion> Product_Commussions { get; set; }
    }

    public class Product_Commussion
    {
        public Sale_Commision_Customer Sale_Commision_Customer { get; set; }
        public Sale_Product Sale_Product { get; set; }
        public Sale_Warehouse Sale_Warehouse { get; set; }
        public Sale_Inventory Sale_Inventory { get; set; }

    }
    public class KhuyenMaiKissy { 
        public Sale_CommissionCus Sale_CommissionCus { get; set; }
        public List<Sale_Commision_Customer> Sale_Commision_Customers { get; set; }
    }
}