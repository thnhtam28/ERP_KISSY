using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwProductSKUMap: EntityTypeConfiguration<vwProductSKU>
    {
        public vwProductSKUMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            this.ToTable("vwSale_Product_Sku");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Product_idSKU).HasColumnName("Product_idSKU");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.color).HasColumnName("color");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.CodeSKU).HasColumnName("CodeSKU");
            this.Property(t => t.CodeSP).HasColumnName("CodeSP");
            this.Property(t => t.Is_display).HasColumnName("Is_display");
        }
    }
}
