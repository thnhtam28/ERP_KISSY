using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_BAIVE_YHLMap : EntityTypeConfiguration<DM_BAIVE_YHL>
    {
        public DM_BAIVE_YHLMap()
        {
            // Primary Key
            this.HasKey(t => t.BAIVE_YHL_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_BAIVE_YHL");
            this.Property(t => t.BAIVE_YHL_ID).HasColumnName("BAIVE_YHL_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.CODE_LOAIBAI).HasColumnName("CODE_LOAIBAI");
            this.Property(t => t.HINHANH).HasColumnName("HINHANH");
            this.Property(t => t.TIEUDE).HasColumnName("TIEUDE");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");
            this.Property(t => t.NOIDUNG).HasColumnName("NOIDUNG");
            this.Property(t => t.TOMTAT).HasColumnName("TOMTAT");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
