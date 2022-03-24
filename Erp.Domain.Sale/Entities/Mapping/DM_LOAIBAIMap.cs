using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_LOAIBAIMap : EntityTypeConfiguration<DM_LOAIBAI>
    {
        public DM_LOAIBAIMap()
        {
            // Primary Key
            this.HasKey(t => t.LOAIBAI_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_LOAIBAI");
            this.Property(t => t.LOAIBAI_ID).HasColumnName("LOAIBAI_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.CODE_LOAIBAI).HasColumnName("CODE_LOAIBAI");
            this.Property(t => t.TEN_LOAIBAI).HasColumnName("TEN_LOAIBAI");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");
            this.Property(t => t.STT).HasColumnName("STT");
            
        }
    }
}
