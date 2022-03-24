using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_NGHESY_TINDUNGMap :EntityTypeConfiguration<DM_NGHESY_TINDUNG>
    {
         public DM_NGHESY_TINDUNGMap()
        {
            // Primary Key
            this.HasKey(t => t.NGHESY_TINDUNG_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_NGHESY_TINDUNG");
            this.Property(t => t.NGHESY_TINDUNG_ID).HasColumnName("NGHESY_TINDUNG_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.HINHANH).HasColumnName("HINHANH");
            this.Property(t => t.TEN_NGHESY).HasColumnName("TEN_NGHESY");
            this.Property(t => t.NOIDUNG).HasColumnName("NOIDUNG");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");
        }
    }
}
