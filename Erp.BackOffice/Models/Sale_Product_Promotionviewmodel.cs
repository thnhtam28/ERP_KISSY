using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Models
{
    public class Sale_Product_Promotionviewmodel
    {
        public int ProductId { get; set; }
        public decimal CommissionValue { get; set; }
        public int? IsMoney { get; set; }
    }
}