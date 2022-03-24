using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Sale_Product");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Unit).HasColumnName("Unit");
            this.Property(t => t.PriceInbound).HasColumnName("PriceInbound");
            this.Property(t => t.PriceOutbound).HasColumnName("PriceOutbound");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.MinInventory).HasColumnName("MinInventory");
            this.Property(t => t.Barcode).HasColumnName("Barcode");
            this.Property(t => t.Image_Name).HasColumnName("Image_Name");
            this.Property(t => t.IsCombo).HasColumnName("IsCombo");
            this.Property(t => t.ProductGroup).HasColumnName("ProductGroup");
            this.Property(t => t.Manufacturer).HasColumnName("Manufacturer");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.TimeForService).HasColumnName("TimeForService");
            this.Property(t => t.DiscountStaff).HasColumnName("DiscountStaff");
            this.Property(t => t.IsMoneyDiscount).HasColumnName("IsMoneyDiscount");
            this.Property(t => t.Origin).HasColumnName("Origin");
            this.Property(t => t.GroupPrice).HasColumnName("GroupPrice");
            this.Property(t => t.TaxFee).HasColumnName("TaxFee");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Sumary).HasColumnName("Sumary");
            this.Property(t => t.List_Image).HasColumnName("List_Image");
            this.Property(t => t.NHOMSANPHAM_ID).HasColumnName("NHOMSANPHAM_ID");
            this.Property(t => t.LOAISANPHAM_ID).HasColumnName("LOAISANPHAM_ID");
            this.Property(t => t.IS_ALOW_BAN_AM).HasColumnName("IS_ALOW_BAN_AM");
            this.Property(t => t.LIST_TAGS).HasColumnName("LIST_TAGS");
            this.Property(t => t.META_TITLE).HasColumnName("META_TITLE");
            this.Property(t => t.META_DESCRIPTION).HasColumnName("META_DESCRIPTION");
            this.Property(t => t.URL_SLUG).HasColumnName("URL_SLUG");
            this.Property(t => t.IS_NGUNG_KD).HasColumnName("IS_NGUNG_KD");
            this.Property(t => t.HDSD).HasColumnName("HDSD");
            this.Property(t => t.THANH_PHAN).HasColumnName("THANH_PHAN");
            this.Property(t => t.THUONG_HIEU).HasColumnName("THUONG_HIEU");
            this.Property(t => t.IS_NOIBAT).HasColumnName("IS_NOIBAT");
            this.Property(t => t.IS_COMBO).HasColumnName("IS_COMBO");
            this.Property(t => t.IS_NEW).HasColumnName("IS_NEW");
            this.Property(t => t.STT_ISNEW).HasColumnName("STT_ISNEW");
            this.Property(t => t.IS_HOT).HasColumnName("IS_HOT");
            this.Property(t => t.STT_ISHOT).HasColumnName("STT_ISHOT");
            this.Property(t => t.Description_brief).HasColumnName("Description_brief");
            this.Property(t => t.NHOMSANPHAM_ID_LST).HasColumnName("NHOMSANPHAM_ID_LST");
            this.Property(t => t.LOAISANPHAM_ID_LST).HasColumnName("LOAISANPHAM_ID_LST");
            this.Property(t => t.is_price_unknown).HasColumnName("is_price_unknown");
            this.Property(t => t.color).HasColumnName("color");
            this.Property(t => t.Image_Pos).HasColumnName("Image_Pos");
            this.Property(t => t.is_display).HasColumnName("is_display");
        }
    }
}
