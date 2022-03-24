using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class DM_NGHESY_TINDUNG
    {
        public DM_NGHESY_TINDUNG()
        {

        }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }


        public int NGHESY_TINDUNG_ID { get; set; }
        public string TEN_NGHESY { get; set; }
        public string NOIDUNG { get; set; }
        public string HINHANH { get; set; }
        public int STT { get; set; }
        public int IS_SHOW { get; set; }
    }
}
