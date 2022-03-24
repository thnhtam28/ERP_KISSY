using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    class DM_CAMNHAN_KHANGMap : EntityTypeConfiguration<DM_CAMNHAN_KHANG>
    {
        public DM_CAMNHAN_KHANGMap()
        {
            // Primary Key
            this.HasKey(t => t.CAMNHAN_KHANG_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_CAMNHAN_KHANG");
            this.Property(t => t.CAMNHAN_KHANG_ID).HasColumnName("CAMNHAN_KHANG_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.TIEUDE).HasColumnName("TIEUDE");
            this.Property(t => t.LINK_VIDEO).HasColumnName("LINK_VIDEO");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");
        }
    }
}
