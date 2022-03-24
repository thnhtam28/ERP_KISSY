using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class DM_TINTUC
    {
        public DM_TINTUC() 
        {
        }
        public int TINTUC_ID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public int NHOMTIN_ID { get; set; }
        public string TIEUDE { get; set; }
        public string LIST_TAGS { get; set; }
        public string TOMTAT { get; set; }
        public string NOIDUNG { get; set; }
        public string ANH_DAIDIEN {get; set;}
        public int STT { get; set; }
        public int IS_SHOW { get; set; }
        public Nullable<int> IS_NOIBAT { get; set; }
        public string META_TITLE { get; set; }
        public string META_DESCRIPTION { get; set; }
        public string URL_SLUG { get; set; }
    }
}
