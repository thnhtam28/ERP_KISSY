using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwCommissionCus_ch
    {
        public vwCommissionCus_ch()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string ApplyFor { get; set; }
        public string Note { get; set; }

        public int? status { get; set; }
        public string status1 { get; set; }
        public decimal? TIEN_KM { get; set; }
        public decimal? TIEN_HANG { get; set; }
        public string TEN_CUAHNG { get; set; }
        public string TEN_CTKM { get; set; }
    }
}
