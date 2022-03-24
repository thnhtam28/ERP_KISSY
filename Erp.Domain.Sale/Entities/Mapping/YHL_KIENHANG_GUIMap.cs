using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class YHL_KIENHANG_GUIMap : EntityTypeConfiguration<YHL_KIENHANG_GUI>

    {
        public YHL_KIENHANG_GUIMap()
        {
            // Primary Key
            this.HasKey(t => t.KIENHANG_GUI_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("YHL_KIENHANG_GUI");
            this.Property(t => t.KIENHANG_GUI_ID).HasColumnName("KIENHANG_GUI_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
          //  ///
          
            this.Property(t => t.NGAY_GUI).HasColumnName("NGAY_GUI");
            this.Property(t => t.NGUOI_NHAN).HasColumnName("NGUOI_NHAN");
            this.Property(t => t.DONVI_GIAONHAN).HasColumnName("DONVI_GIAONHAN");
            this.Property(t => t.GHI_CHU).HasColumnName("GHI_CHU");
           
        }
    }
}
