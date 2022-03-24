namespace ERP_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale_Product
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [Required]
        [StringLength(30)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public decimal? PriceInbound { get; set; }

        public decimal PriceOutBound { get; set; }

        [Required]
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

        [StringLength(50)]
        public string Size { get; set; }

        [StringLength(50)]
        public string color { get; set; }

        public int? TimeForService { get; set; }

        public int? OldDB_ID { get; set; }

        public bool? OldDB_IsProduct { get; set; }

        public int? DiscountStaff { get; set; }

        public bool? IsMoneyDiscount { get; set; }

        [StringLength(150)]
        public string Origin { get; set; }

        [StringLength(350)]
        public string GroupPrice { get; set; }

        public int? TaxFee { get; set; }

        [StringLength(150)]
        public string Product_children { get; set; }

        [StringLength(500)]
        public string Sumary { get; set; }

        public string Content { get; set; }

        public string List_Image { get; set; }

        public int NHOMSANPHAM_ID { get; set; }

        public int LOAISANPHAM_ID { get; set; }

        public bool IS_ALOW_BAN_AM { get; set; }

        [StringLength(500)]
        public string LIST_TAGS { get; set; }

        [StringLength(100)]
        public string META_TITLE { get; set; }

        [StringLength(400)]
        public string META_DESCRIPTION { get; set; }

        [StringLength(500)]
        public string URL_SLUG { get; set; }

        public bool? IS_NGUNG_KD { get; set; }

        public string HDSD { get; set; }

        public string THANH_PHAN { get; set; }

        public string THUONG_HIEU { get; set; }

        public bool? IS_NOIBAT { get; set; }

        public bool? IS_COMBO { get; set; }

        public string Description_brief { get; set; }

        [Required]
        [StringLength(150)]
        public string NHOMSANPHAM_ID_LST { get; set; }

        [StringLength(150)]
        public string LOAISANPHAM_ID_LST { get; set; }

        [StringLength(50)]
        public string id_google_product_category { get; set; }

        public bool? is_price_unknown { get; set; }

        public bool? IS_NEW { get; set; }

        public int? STT_ISNEW { get; set; }

        public bool? IS_HOT { get; set; }

        public int? STT_ISHOT { get; set; }

        public bool? is_display { get; set; }
    }
}
