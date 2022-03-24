using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwProduct_PromotionNew
    {
        public vwProduct_PromotionNew()
        {

        }
        
        public int? ProductId { get; set; }
        public decimal CommissionValue { get; set; }
        public Nullable<bool> IsMoney { get; set; }
    }
}
