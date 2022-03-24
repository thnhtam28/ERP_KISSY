using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class PhysicalInventoryDetailchildrenmap : EntityTypeConfiguration<PhysicalInventoryDetailchildren>
    {
        public PhysicalInventoryDetailchildrenmap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            // Table & Column Mappings
            this.ToTable("Sale_PhysicalInventoryDetailchildren");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            //this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Qty).HasColumnName("Qty");
            this.Property(t => t.PhysicalInventoryDetailId).HasColumnName("PhysicalInventoryDetailId");
            this.Property(t => t.NumberCheck).HasColumnName("NumberCheck");
        }
    }
}
