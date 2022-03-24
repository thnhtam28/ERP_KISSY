using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwProductsamesizeMap : EntityTypeConfiguration<vwProductsamesize>
    {
        public vwProductsamesizeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("vwProductsamesize");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Product_id).HasColumnName("Product_id");
            this.Property(t => t.Product_idsame).HasColumnName("Product_idsame");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.PriceOutBound).HasColumnName("PriceOutbound");
            this.Property(t => t.TEN_LOAISANPHAM).HasColumnName("TEN_LOAISANPHAM");
            this.Property(t => t.TEN_NHOMSANPHAM).HasColumnName("TEN_NHOMSANPHAM");
            this.Property(t => t.stt).HasColumnName("stt");

        }
    }
}
