using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Repositories
{
    public class vwINvoiceKMInvoiceRepository : GenericRepository<ErpSaleDbContext, vwINvoiceKMInvoice>, IvwINvoiceKMInvoiceRepository
    {
        public vwINvoiceKMInvoiceRepository(ErpSaleDbContext context)
          : base(context)
        {

        }

        public List<vwINvoiceKMInvoice> GetAllListvwINvoiceKMInvoice()
        {
            return Context.vwINvoiceKMInvoice.Where(item => (item.IdCus != null && item.IdCus > 0)).ToList();
        }
    }
}
