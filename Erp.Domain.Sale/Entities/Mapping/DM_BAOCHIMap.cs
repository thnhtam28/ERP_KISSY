using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_BAOCHIMap : EntityTypeConfiguration<DM_BAOCHI>
    {
         public DM_BAOCHIMap()
        {
            // Primary Key
            this.HasKey(t => t.BAOCHI_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_BAOCHI");
            this.Property(t => t.BAOCHI_ID).HasColumnName("BAOCHI_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.HINHANH).HasColumnName("HINHANH");
            this.Property(t => t.LINK_BAO).HasColumnName("LINK_BAO");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");
        }
    }
}
