using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Sale.Models
{
    public class KmHangHoaVipViewModel
    {
        public int id { get; set; }
        public int id_cha { get; set; }
        public Nullable<bool> IsMoney { get; set; }
        public decimal CommissionValue { get; set; }
        public int TypeApply { get; set; }
        public int BranchId { get; set; }
        public string type { get; set; }
        public string typeCus { get; set; }
        public string listIDcustomer { get; set; }
        public string listIdproduct { get; set; }

    }
}