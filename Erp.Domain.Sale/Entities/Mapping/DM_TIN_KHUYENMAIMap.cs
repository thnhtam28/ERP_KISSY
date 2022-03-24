using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Erp.Domain.Sale.Entities.Mapping
{
    public class DM_TIN_KHUYENMAIMap :EntityTypeConfiguration<DM_TIN_KHUYENMAI>  
    {
        public DM_TIN_KHUYENMAIMap() 
        {
            // Primary Key
            this.HasKey(t => t.TIN_KHUYENMAI_ID);

            // Properties

            // Table & Column Mappings
            this.ToTable("DM_TIN_KHUYENMAI");
            this.Property(t => t.TIN_KHUYENMAI_ID).HasColumnName("TIN_KHUYENMAI_ID");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            
            this.Property(t => t.TIEUDE).HasColumnName("TIEUDE");
            this.Property(t => t.TOMTAT).HasColumnName("TOMTAT");
            this.Property(t => t.NOIDUNG).HasColumnName("NOIDUNG");
            this.Property(t => t.ANH_DAIDIEN).HasColumnName("ANH_DAIDIEN");
            this.Property(t => t.STT).HasColumnName("STT");
            
            this.Property(t => t.IS_SHOW).HasColumnName("IS_SHOW");

        }
    }
}
