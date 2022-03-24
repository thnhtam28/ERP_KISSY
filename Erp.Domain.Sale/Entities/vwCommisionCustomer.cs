using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwCommisionCustomer
    {
        public vwCommisionCustomer()
        {
            
        }
        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> CommissionCusId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public decimal CommissionValue { get; set; }
        public decimal Minvalue { get; set; }
        public Nullable<bool> IsMoney { get; set; }
        public int Type { get; set; }
        public string TypeCus { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string ApplyFor { get; set; }
        public int status { get; set; }


    }
}
