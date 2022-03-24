using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class YHL_KIENHANG_TRA
    {
        public YHL_KIENHANG_TRA()
        {

        }
        public int KIENHANG_TRA_ID { get; set; }

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }


        public System.DateTime NGAY_TRA { get; set; }
        public string NGUOI_TRA { get; set; }
        public string GHI_CHU { get; set; }
    }
}
