using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwProduct
    {
        public vwProduct()
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
        public string Image_Name { get; set; }
        public int? MinInventory { get; set; }
        public string Barcode { get; set; }
        public string ProductGroup { get; set; }
        public string Manufacturer { get; set; }
        public string Size { get; set; }
        public int? QuantityTotalInventory { get; set; }
        public int? DiscountStaff { get; set; }
        public bool? IsMoneyDiscount { get; set; }
        public string Origin { get; set; }
        public string GroupPrice { get; set; }
        public int? TaxFee { get; set; }
        public Nullable<bool> IS_NEW { get; set; }

        public int NHOMSANPHAM_ID { get; set; }

        public Nullable<int> STT_ISNEW { get; set; }

        public Nullable<bool> IS_HOT { get; set; }
        public Nullable<int> STT_ISHOT { get; set; }
        public string Description_brief { get; set; }
        public string id_google_product_category { get; set; }
        public string Image_Pos { get; set; }

        public string ProductGroupName { get; set; }
       // public string[] NHOMSANPHAM_ID_LST { get; set; }
        public string color { get; set; }

        public Nullable<int> NhomCha { get; set; }

    }
}
