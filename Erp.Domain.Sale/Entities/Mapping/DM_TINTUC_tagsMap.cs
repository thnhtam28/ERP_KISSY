using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_TINTUC_tagsMap : EntityTypeConfiguration<DM_TINTUC_tags>
    {
        public DM_TINTUC_tagsMap() {
            // Primary Key
            this.HasKey(pc => new { pc.TINTUC_ID, pc.TagId });
            // Table & Column Mappings
            this.ToTable("DM_TINTUC_tags");
            this.Property(t => t.TINTUC_ID).HasColumnName("TINTUC_ID");
            this.Property(t => t.TagId).HasColumnName("TagId");
        }
    }
}
