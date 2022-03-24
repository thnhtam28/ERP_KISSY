using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class Product
    {
        public Product()
        {

        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal? PriceInbound { get; set; }
        public decimal? PriceOutbound { get; set; }
        public string Type { get; set; }
        public string CategoryCode { get; set; }
        public int? MinInventory { get; set; }
        public string Barcode { get; set; }
        public string Image_Name { get; set; }
        public bool? IsCombo { get; set; }
        public string ProductGroup { get; set; }
        public string Manufacturer { get; set; }
        public string Size { get; set; }
        public string Image_Pos { get; set; }
        public string color { get; set; }
        public int TimeForService { get; set; }
        public int? DiscountStaff { get; set; }
        public bool? IsMoneyDiscount { get; set; }
        public string Origin { get; set; }
        public string GroupPrice { get; set; }
        public int? TaxFee { get; set; }

        public string Content { get; set; }
        public string Sumary { get; set; }
        public string List_Image { get; set; }
        public int NHOMSANPHAM_ID { get; set; }
        public int LOAISANPHAM_ID { get; set; }
        public bool IS_ALOW_BAN_AM { get; set; }
        public string LIST_TAGS { get; set; }
        public string META_TITLE { get; set; }
        public string META_DESCRIPTION { get; set; }
        public string URL_SLUG { get; set; }
        public bool IS_NGUNG_KD { get; set; }
        public string HDSD { get; set; }
        public string THANH_PHAN { get; set; }
        public string THUONG_HIEU { get; set; }
        public bool IS_NOIBAT { get; set; }
        public bool IS_COMBO { get; set; }
        public Nullable<bool> IS_NEW { get; set; }

        public Nullable<int> STT_ISNEW { get; set; }

        public Nullable<bool> IS_HOT { get; set; }
        public Nullable<int> STT_ISHOT { get; set; }
        public Nullable<bool> is_display { get; set; }
        public string Description_brief { get; set; }
        public string NHOMSANPHAM_ID_LST { get; set; }
        public string LOAISANPHAM_ID_LST { get; set; }

        public Nullable<bool> is_price_unknown { get; set; }

    }
}
