using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class DM_NHOMTIN
    {
        public DM_NHOMTIN() 
        {
        }
        public int NHOMTIN_ID {get; set;}
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string ANH_DAIDIEN {get; set;}
        public int STT { get; set; }
        public string TEN_LOAISANPHAM { get; set; }
        public int IS_SHOW { get; set; }
        public string META_TITLE { get; set; }
        public string META_DESCRIPTION { get; set; }
        public string URL_SLUG { get; set; }

    }
}
