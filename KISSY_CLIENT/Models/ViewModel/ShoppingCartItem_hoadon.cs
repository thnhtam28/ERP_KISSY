using ERP_API.Filters;
using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class ShoppingCartItem_hoadon
    {
        public int? PromotionId { get; set; }
        public int? PromotionDetailId { get; set; }
        public decimal? PromotionValue { get; set; }
        public bool IsMoney { get; set; }
        public decimal? TienKM { get; set; }
    }

}