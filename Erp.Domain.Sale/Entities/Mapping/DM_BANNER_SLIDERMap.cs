using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_BANNER_SLIDERMap : EntityTypeConfiguration<DM_BANNER_SLIDER>
    {
        public DM_BANNER_SLIDERMap()
        {
            // Primary Key
            this.HasKey(t => t.BANNER_SLIDER_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_BANNER_SLIDER");
            this.Property(t => t.BANNER_SLIDER_ID).HasColumnName("BANNER_SLIDER_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.ANH_DAIDIEN).HasColumnName("ANH_DAIDIEN");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");
            this.Property(t => t.IS_MOBILE).HasColumnName("IS_MOBILE");
            this.Property(t => t.LINK).HasColumnName("LINK");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
