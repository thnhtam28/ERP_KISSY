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
    public class vwINvoiceKMDetailRepository : GenericRepository<ErpSaleDbContext, vwINvoiceKMDetail>, IvwINvoiceKMDetailRepository
    {
        public vwINvoiceKMDetailRepository(ErpSaleDbContext context)
          : base(context)
        {

        }
 
        public List<vwINvoiceKMDetail> GetAllListvwINvoiceKMDetail()
        {
            return Context.vwINvoiceKMDetail.Where(item =>  item.IdCus>0).ToList();
        }
    }
}
