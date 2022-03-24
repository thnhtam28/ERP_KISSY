using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class YHL_KIENHANG_GUI_CTIETMap : EntityTypeConfiguration<YHL_KIENHANG_GUI_CTIET>
    {
        public YHL_KIENHANG_GUI_CTIETMap()
        {
            // Primary Key
            this.HasKey(t => t.KIENHANG_GUI_CTIET_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("YHL_KIENHANG_GUI_CTIET");
            this.Property(t => t.KIENHANG_GUI_CTIET_ID).HasColumnName("KIENHANG_GUI_CTIET_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
          //  ///
            this.Property(t => t.STT_DEN).HasColumnName("STT_DEN");
            this.Property(t => t.SO_HIEU).HasColumnName("SO_HIEU");
            this.Property(t => t.MA_DON_HANG).HasColumnName("MA_DON_HANG");
            this.Property(t => t.NGAY_KG).HasColumnName("NGAY_KG");
            this.Property(t => t.NGUOI_GUI).HasColumnName("NGUOI_GOI");
            this.Property(t => t.NGUOI_NHAN).HasColumnName("NGUOI_NHAN");
            this.Property(t => t.DC_NHAN).HasColumnName("DC_NHAN");
            this.Property(t => t.DT_NHAN).HasColumnName("DT_NHAN");
            this.Property(t => t.TEN_BC_NHAN).HasColumnName("TEN_BC_NHAN");
            this.Property(t => t.KHOI_LUONG).HasColumnName("KHOI_LUONG");
            this.Property(t => t.KHOI_LUONG_QD).HasColumnName("KHOI_LUONG_QD");
            this.Property(t => t.NOI_DUNG).HasColumnName("NOI_DUNG");
            this.Property(t => t.DV_DB).HasColumnName("DV_DB");
            this.Property(t => t.TRI_GIA).HasColumnName("TRI_GIA");
            this.Property(t => t.vung_xa).HasColumnName("vung_xa");
            this.Property(t => t.CUOC_DV).HasColumnName("CUOC_DV");
            this.Property(t => t.Cuoc_COD).HasColumnName("Cuoc_COD");
            this.Property(t => t.CUOC_DVCT).HasColumnName("CUOC_DVCT");
            this.Property(t => t.TIEN_VAT).HasColumnName("TIEN_VAT");
            this.Property(t => t.TONG_CUOC).HasColumnName("TONG_CUOC");

            this.Property(t => t.TRANG_THAIDONHANG_GUI).HasColumnName("TRANG_THAIDONHANG_GUI");
            this.Property(t => t.GHI_CHU).HasColumnName("GHI_CHU");
            this.Property(t => t.KIENHANG_GUI_ID).HasColumnName("KIENHANG_GUI_ID");



        }
    }
}
