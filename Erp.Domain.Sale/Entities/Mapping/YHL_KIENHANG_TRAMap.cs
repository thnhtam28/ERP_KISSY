using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class YHL_KIENHANG_TRAMap : EntityTypeConfiguration<YHL_KIENHANG_TRA>
    {
        public YHL_KIENHANG_TRAMap()
        {
            // Primary Key
            this.HasKey(t => t.KIENHANG_TRA_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("YHL_KIENHANG_TRA");
            this.Property(t => t.KIENHANG_TRA_ID).HasColumnName("KIENHANG_TRA_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
          //  ///
          
            this.Property(t => t.NGAY_TRA).HasColumnName("NGAY_TRA");
            this.Property(t => t.NGUOI_TRA).HasColumnName("NGUOI_TRA");
       
            this.Property(t => t.GHI_CHU).HasColumnName("GHI_CHU");
           
        }
    }
}
