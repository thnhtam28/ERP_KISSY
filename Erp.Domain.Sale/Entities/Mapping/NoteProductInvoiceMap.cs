using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class NoteProductInvoiceMap: EntityTypeConfiguration<NoteProductInvoice>
    {
        public NoteProductInvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.ToTable("Sale_NoteProductInvoice");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.ProductInvoiceId).HasColumnName("ProductInvoiceId");
            this.Property(t => t.Note).HasColumnName("Note");
        }
    }
}
