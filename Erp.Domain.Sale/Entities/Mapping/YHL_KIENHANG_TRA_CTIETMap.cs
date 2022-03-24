using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class YHL_KIENHANG_TRA_CTIETMap : EntityTypeConfiguration<YHL_KIENHANG_TRA_CTIET>
    {
        public YHL_KIENHANG_TRA_CTIETMap()
        {
            // Primary Key
            this.HasKey(t => t.KIENHANG_TRA_CTIET_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("YHL_KIENHANG_TRA_CTIET");
            this.Property(t => t.KIENHANG_TRA_CTIET_ID).HasColumnName("KIENHANG_TRA_CTIET_ID");
            this.Property(t => t.KIENHANG_TRA_ID).HasColumnName("KIENHANG_TRA_ID");
            
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
          //  ///

            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.SO_HIEU).HasColumnName("SO_HIEU");
            this.Property(t => t.MA_DON_HANG).HasColumnName("MA_DON_HANG");
            this.Property(t => t.NGAY_KG).HasColumnName("NGAY_KG");
            this.Property(t => t.NGUOI_NHAN).HasColumnName("NGUOI_NHAN");
            this.Property(t => t.DC_NHAN).HasColumnName("DC_NHAN");
            this.Property(t => t.DT_NHAN).HasColumnName("DT_NHAN");
            this.Property(t => t.TRI_GIA).HasColumnName("TRI_GIA");

            this.Property(t => t.CUOC).HasColumnName("CUOC");
            this.Property(t => t.VUN).HasColumnName("VUN");
            this.Property(t => t.CONG_THUC).HasColumnName("CONG_THUC");
            this.Property(t => t.CUOC_EMS_HOAN).HasColumnName("CUOC_EMS_HOAN");
            this.Property(t => t.CUOC_COD_HOAN).HasColumnName("CUOC_COD_HOAN");
            this.Property(t => t.PHAI_THU).HasColumnName("PHAI_THU");
            this.Property(t => t.LYDO_HOAN).HasColumnName("LYDO_HOAN");
            this.Property(t => t.NGUOI_NHAN_CUOC_HOAN).HasColumnName("NGUOI_NHAN_CUOC_HOAN");
            this.Property(t => t.KHOI_LUONG).HasColumnName("KHOI_LUONG");
            this.Property(t => t.GHI_CHU).HasColumnName("GHI_CHU");
           
        }
    }
}
