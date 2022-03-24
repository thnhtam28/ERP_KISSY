using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class Sale_Product_View
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [StringLength(30)]
        public string Code { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public decimal? PriceInbound { get; set; }

        public decimal? PriceOutBound { get; set; }

        [StringLength(20)]
        public string Type { get; set; }

        [StringLength(150)]
        public string CategoryCode { get; set; }

        public int? MinInventory { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        [StringLength(100)]
        public string Image_Name { get; set; }

        public bool? IsCombo { get; set; }

        [StringLength(20)]
        public string ProductGroup { get; set; }

        [StringLength(150)]
        public string Manufacturer { get; set; }

        [StringLength(20)]
        public string Size { get; set; }

        public int? TimeForService { get; set; }

        public int? OldDB_ID { get; set; }

        public bool? OldDB_IsProduct { get; set; }
        //public bool? is_price_unknown { get; set; }
        public int? DiscountStaff { get; set; }

        public bool? IsMoneyDiscount { get; set; }

        [StringLength(150)]
        public string Origin { get; set; }

        [StringLength(350)]
        public string GroupPrice { get; set; }

        public int? TaxFee { get; set; }
        [StringLength(500)]
        public string Sumary { get; set; }

        public string Content { get; set; }

        public string List_Image { get; set; }
        public System_Category_View System_Category { get; set; }
        public bool? is_price_unknown { get; set; }
    }
}