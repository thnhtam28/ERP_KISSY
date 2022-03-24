using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class DM_NHOMSANPHAM
    {
        public DM_NHOMSANPHAM()
        {

        }
        public int NHOMSANPHAM_ID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string TEN_NHOMSANPHAM { get; set; }
        public int CAP_NHOMSANPHAM { get; set; }
        public int STT { get; set; }
        public Nullable<int> NHOM_CHA { get; set; }
        public string BANNER { get; set; }
        public Nullable<int> IS_SHOW { get; set; }
        public string META_TITLE { get; set; }
        public string META_DESCRIPTION { get; set; }
        public string URL_SLUG { get; set; }
    }
}
