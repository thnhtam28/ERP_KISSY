using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class CartCheckOutInfo
    {
        public string Name { get; set; }
        public  bool Sex{ get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public string UserID { get; set; }
        public decimal? Total { get; set; }
        public bool Call { get; set; }
        public string Address { get; set; }
        public int? PHISHIP { get; set; }
        public string Province_id { get; set; }
        public string District_id { get; set; }
        public int? PromotionId { get; set; }
        public int? PromotionDetailId { get; set; }
        public decimal? PromotionValue { get; set; }
        public bool? IsMoney { get; set; }
    }
    public class CartCheckOut
    {
        public CartCheckOutInfo  cartCheckOutInfo {get;set;}

        public List<ShoppingCartItem> ListItem { get; set; }

    }
}