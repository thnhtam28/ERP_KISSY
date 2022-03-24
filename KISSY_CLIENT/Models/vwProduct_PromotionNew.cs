using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_API.Models
{
    public class vwProduct_PromotionNew
    {
        public vwProduct_PromotionNew()
        {

        }
        [Key]
        public int? ProductId { get; set; }
        public decimal CommissionValue { get; set; }
        public Nullable<bool> IsMoney { get; set; }
    }
}