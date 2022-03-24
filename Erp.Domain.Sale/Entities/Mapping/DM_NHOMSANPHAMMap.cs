using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_NHOMSANPHAMMap : EntityTypeConfiguration<DM_NHOMSANPHAM>
    {
        public DM_NHOMSANPHAMMap()
        {
            // Primary Key
            this.HasKey(t => t.NHOMSANPHAM_ID);

            // Properties
            this.Property(t => t.TEN_NHOMSANPHAM).HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("DM_NHOMSANPHAM");
            this.Property(t => t.NHOMSANPHAM_ID).HasColumnName("NHOMSANPHAM_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.TEN_NHOMSANPHAM).HasColumnName("TEN_NHOMSANPHAM");
            this.Property(t => t.CAP_NHOMSANPHAM).HasColumnName("CAP_NHOMSANPHAM");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.NHOM_CHA).HasColumnName("NHOM_CHA");
            this.Property(t => t.BANNER).HasColumnName("BANNER");
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");
            this.Property(t => t.META_TITLE).HasColumnName("META_TITLE");
            this.Property(t => t.META_DESCRIPTION).HasColumnName("META_DESCRIPTION");
            this.Property(t => t.URL_SLUG).HasColumnName("URL_SLUG");

        }
    }
}
