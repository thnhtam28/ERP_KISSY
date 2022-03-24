using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_TINTUCMap : EntityTypeConfiguration<DM_TINTUC>
    {
        public DM_TINTUCMap() 
        {
            // Primary Key
            this.HasKey(t => t.TINTUC_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_TINTUC");
            this.Property(t => t.TINTUC_ID).HasColumnName("TINTUC_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.NHOMTIN_ID).HasColumnName("NHOMTIN_ID");
            this.Property(t => t.LIST_TAGS).HasColumnName("LIST_TAGS");
            this.Property(t => t.TIEUDE).HasColumnName("TIEUDE");
            this.Property(t => t.TOMTAT).HasColumnName("TOMTAT");
            this.Property(t => t.NOIDUNG).HasColumnName("NOIDUNG");
            this.Property(t => t.ANH_DAIDIEN).HasColumnName("ANH_DAIDIEN");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.IS_NOIBAT).HasColumnName("IS_NOIBAT");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");
            this.Property(t => t.META_TITLE).HasColumnName("META_TITLE");
            this.Property(t => t.META_DESCRIPTION).HasColumnName("META_DESCRIPTION");
            this.Property(t => t.URL_SLUG).HasColumnName("URL_SLUG");
        }

    }
}
