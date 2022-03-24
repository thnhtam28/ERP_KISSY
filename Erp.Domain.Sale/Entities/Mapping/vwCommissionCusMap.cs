using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwCommissionCusMap : EntityTypeConfiguration<vwCommissionCus>
    {
        public vwCommissionCusMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Type).HasMaxLength(50);
           // this.Property(t => t.ApplyFor).HasMaxLength(50);
            this.Property(t => t.Note).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("vwSale_CommisionCus");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.ApplyFor).HasColumnName("ApplyFor");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.status1).HasColumnName("status1");
            this.Property(t => t.TIEN_KM).HasColumnName("TIEN_KM");
            this.Property(t => t.TIEN_HANG).HasColumnName("TIEN_HANG");

        }
    }
}
