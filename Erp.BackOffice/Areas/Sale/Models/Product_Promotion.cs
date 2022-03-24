using Erp.Domain.Sale.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Sale.Models
{
    public class Product_Promotion
    {
        public decimal? GiaKM { get; set; }
        public Product Sale_Product { get; set; }
        public CommisionCustomer Sale_Commision_Customer { get; set; }

        public CommissionCus Sale_CommissionCus { get; set; }
    }
   
}