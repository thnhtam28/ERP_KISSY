using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_BEST_SELLERMap : EntityTypeConfiguration<DM_BEST_SELLER>
    {
        public DM_BEST_SELLERMap()
        {
            // Primary Key
            this.HasKey(t => t.BEST_SELLER_ID);
            
            // Properties

            // Table & Column Mappings
            this.ToTable("DM_BEST_SELLER");
            this.Property(t => t.BEST_SELLER_ID).HasColumnName("BEST_SELLER_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.Product_Id).HasColumnName("Product_Id");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");

        }
    }
}
