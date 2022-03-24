using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "ProductOrServiceName", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ProductOrServiceCode", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "Unit", ResourceType = typeof(Wording))]
        public string Unit { get; set; }

        [Display(Name = "PriceInbound", ResourceType = typeof(Wording))]
        public decimal? PriceInbound { get; set; }

        [Display(Name = "PriceOutbound", ResourceType = typeof(Wording))]
        public decimal? PriceOutbound { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "CategoryCode", ResourceType = typeof(Wording))]
        public string CategoryCode { get; set; }

        [Display(Name = "MinInventory", ResourceType = typeof(Wording))]
        public int? MinInventory { get; set; }


        [Display(Name = "IsCombo", ResourceType = typeof(Wording))]
        public bool? IsCombo { get; set; }

        [Display(Name = "Barcode", ResourceType = typeof(Wording))]
        public string Barcode { get; set; }

        //[Display(Name = "Barcode", ResourceType = typeof(Wording))]
        public string Image_Name { get; set; }
        public string imagepos { get; set; }
        public List<ObjectAttributeValueViewModel> AttributeValueList { get; set; }

        [Display(Name = "ProductGroup", ResourceType = typeof(Wording))]
        public string ProductGroup { get; set; }

        [Display(Name = "Manufacturer", ResourceType = typeof(Wording))]
        public string Manufacturer { get; set; }
        [Display(Name = "ProductCapacity", ResourceType = typeof(Wording))]
        public string Size { get; set; }
        public int? QuantityTotalInventory { get; set; }
        public int? DiscountStaff { get; set; }
        public bool? IsMoneyDiscount { get; set; }



        [Display(Name = "LoCode", ResourceType = typeof(Wording))]
        public string LoCode { get; set; }
        [Display(Name = "ExpiryDateItem", ResourceType = typeof(Wording))]
        public DateTime? ExpiryDate { get; set; }
        [Display(Name = "Origin", ResourceType = typeof(Wording))]
        public string Origin { get; set; }
        public string GroupPrice { get; set; }
        [Display(Name = "TaxFee", ResourceType = typeof(Wording))]
        public int? TaxFee { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Color_product", ResourceType = typeof(Wording))]
        public string Color_product { get; set; }

        public string[] listimg_product { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Size_product", ResourceType = typeof(Wording))]
        public string Size_product { get; set; }
        public string Content { get; set; }

        [StringLength(500)]
        public string Sumary { get; set; }

        public string List_Image { get; set; }

        [Display(Name = "NHOMSANPHAM_ID", ResourceType = typeof(Wording))]
        public int NHOMSANPHAM_ID { get; set; }

        [Display(Name = "NHOMSANPHAM_ID_LST", ResourceType = typeof(Wording))]
        public string[] NHOMSANPHAM_ID_LST { get; set; }

        [Display(Name = "LOAISANPHAM_ID", ResourceType = typeof(Wording))]
        public int LOAISANPHAM_ID { get; set; }


        [Display(Name = "LOAISANPHAM_ID", ResourceType = typeof(Wording))]
        public string[] LOAISANPHAM_ID_LST { get; set; }






        [Display(Name = "IS_ALOW_BAN_AM", ResourceType = typeof(Wording))]
        public bool IS_ALOW_BAN_AM { get; set; }
        [Display(Name = "LIST_TAGS", ResourceType = typeof(Wording))]
        public string LIST_TAGS { get; set; }
        public string META_TITLE { get; set; }
        public string META_DESCRIPTION { get; set; }
        public string URL_SLUG { get; set; }
        [Display(Name = "IS_NGUNG_KD", ResourceType = typeof(Wording))]
        public bool IS_NGUNG_KD { get; set; }
        public string HDSD { get; set; }
        public string THANH_PHAN { get; set; }
        public string THUONG_HIEU { get; set; }
        [Display(Name = "IS_NOIBAT", ResourceType = typeof(Wording))]
        public bool IS_NOIBAT { get; set; }
        [Display(Name = "IS_COMBO", ResourceType = typeof(Wording))]
        public bool IS_COMBO { get; set; }
        [Display(Name = "IS_NEW", ResourceType = typeof(Wording))]
        public Nullable<bool> IS_NEW { get; set; }
        [Display(Name = "STT_ISNEW", ResourceType = typeof(Wording))]
        public Nullable<int> STT_ISNEW { get; set; }

        [Display(Name = "IS_HOT", ResourceType = typeof(Wording))]
        public Nullable<bool> IS_HOT { get; set; }
        [Display(Name = "STT_ISHOT", ResourceType = typeof(Wording))]
        public Nullable<int> STT_ISHOT { get; set; }
        public string TEN_NHOMSANPHAM { get; set; }
        public string Description_brief { get; set; }
        [Display(Name = "Giá liên hệ")]
        public bool is_price_unknown { get; set; }
        public string Image_Pos { get; set; }

        [Display(Name = "is_display", ResourceType = typeof(Wording))]
        public Nullable<bool> is_display { get; set; }

        public string ProductGroupName { get; set; }

        public string ProductInvoieCode { get; set; }
        public string NameColor { get; set; }
        public string NameSize { get; set; }

        public string color { get; set; }

        public Nullable<int> NhomCha { get; set; }


        //sử dụng cho báo cáo doanh số
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Discount { get; set; }
        //
    }
}