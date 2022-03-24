using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class Sale_Product_samesizeMap : EntityTypeConfiguration<Sale_Product_samesize>
    {
        public Sale_Product_samesizeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            this.ToTable("Sale_Product_samesize");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.Product_id).HasColumnName("Product_id");
            this.Property(t => t.Product_idsame).HasColumnName("Product_idsame");
   
           
        }
    }
}
