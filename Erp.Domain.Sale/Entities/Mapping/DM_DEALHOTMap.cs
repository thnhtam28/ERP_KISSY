using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_DEALHOTMap : EntityTypeConfiguration<DM_DEALHOT>
    {
        public DM_DEALHOTMap()
        {
            // Primary Key
            this.HasKey(t => t.DEALHOT_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_DEALHOT");
            this.Property(t => t.DEALHOT_ID).HasColumnName("DEALHOT_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.CommissionCus_Id).HasColumnName("CommissionCus_Id");
            this.Property(t => t.BANNER).HasColumnName("BANNER");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");


        }
    }
}
