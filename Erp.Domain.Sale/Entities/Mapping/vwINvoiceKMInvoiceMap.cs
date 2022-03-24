using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwINvoiceKMInvoiceMap : EntityTypeConfiguration<vwINvoiceKMInvoice>
    {
        public vwINvoiceKMInvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.STT);
            // Table & Column Mappings
            this.ToTable("vwINvoiceKMInvoice");
            this.Property(t => t.IdCus).HasColumnName("IdCus");
            this.Property(t => t.STT).HasColumnName("STT");
            this.Property(t => t.IsMoney).HasColumnName("IsMoney");
            this.Property(t => t.BranchName).HasColumnName("BranchId");
            this.Property(t => t.IsMoney).HasColumnName("IsMoney");
            this.Property(t => t.DiscountTabBill).HasColumnName("DiscountTabBill");
            this.Property(t => t.DiscountTabBillAmount).HasColumnName("DiscountTabBillAmount");
            this.Property(t => t.SumAmount).HasColumnName("SumAmount");
        }
    }
}
