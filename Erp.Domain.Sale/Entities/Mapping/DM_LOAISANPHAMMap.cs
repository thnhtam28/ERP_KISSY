using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_LOAISANPHAMMap : EntityTypeConfiguration<DM_LOAISANPHAM>
    {
        public DM_LOAISANPHAMMap()
        {
            // Primary Key
            this.HasKey(t => t.LOAISANPHAM_ID);

            // Properties
            this.Property(t => t.TEN_LOAISANPHAM).HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("DM_LOAISANPHAM");
            this.Property(t => t.LOAISANPHAM_ID).HasColumnName("LOAISANPHAM_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.TEN_LOAISANPHAM).HasColumnName("TEN_LOAISANPHAM");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.IS_SHOWMAIN).HasColumnName("IS_SHOWMAIN");
            this.Property(t => t.LOAISANPHAM_IDCHA).HasColumnName("LOAISANPHAM_IDCHA");
            this.Property(t => t.ANH_DAIDIEN).HasColumnName("ANH_DAIDIEN");
            this.Property(t => t.META_TITLE).HasColumnName("META_TITLE");
            this.Property(t => t.META_DESCRIPTION).HasColumnName("META_DESCRIPTION");
            this.Property(t => t.URL_SLUG).HasColumnName("URL_SLUG");
        }

    }
}
