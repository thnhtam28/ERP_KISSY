using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwProduct_PromotionMap: EntityTypeConfiguration<vwProduct_PromotionNew>
    {
        public vwProduct_PromotionMap()
        {
            this.HasKey(t => t.ProductId);
            this.ToTable("vwProduct_PromotionNew");
            
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.CommissionValue).HasColumnName("CommissionValue");
            this.Property(t => t.IsMoney).HasColumnName("IsMoney");
        }
           
    }
}
