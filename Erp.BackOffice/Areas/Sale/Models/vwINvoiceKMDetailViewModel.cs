using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Sale.Models
{
    public class vwINvoiceKMDetailViewModel
    {
        public int IdCus { get; set; }
        public int BranchId { get; set; }
        public int IrregularDiscount { get; set; }
        public decimal IrregularDiscountAmount { get; set; }
        public decimal SumAmount { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string NameProduct { get; set; }
    }
}