using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwINvoiceKMDetailMap : EntityTypeConfiguration<vwINvoiceKMDetail>
    {
        public vwINvoiceKMDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.STT);
            // Table & Column Mappings
            this.ToTable("vwINvoiceKMDetail");
            this.Property(t => t.IdCus).HasColumnName("IdCus");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.BranchName).HasColumnName("BranchId");
            this.Property(t => t.IrregularDiscount).HasColumnName("IrregularDiscount");
            this.Property(t => t.IrregularDiscountAmount).HasColumnName("IrregularDiscountAmount");
            this.Property(t => t.SumAmount).HasColumnName("SumAmount");
        }
    }
}
