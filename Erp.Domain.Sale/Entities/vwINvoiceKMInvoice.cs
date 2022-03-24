using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwINvoiceKMInvoice
    {
        public vwINvoiceKMInvoice()
        {

        }
        //public int Id { get; set; }
        public long STT { get; set; }
        public int IdCus { get; set; }
        public string BranchName { get; set; }
        public bool IsMoney { get; set; }
        public double DiscountTabBill { get; set; }
        public decimal DiscountTabBillAmount { get; set; }
        public decimal SumAmount { get; set; }

    }
}
