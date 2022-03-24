using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class YHL_KIENHANG_GUI
    {
        public YHL_KIENHANG_GUI()
        {

        }
        public int KIENHANG_GUI_ID { get; set; }

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string DONVI_GIAONHAN { get; set; }
        public System.DateTime NGAY_GUI { get; set; }
        public string NGUOI_NHAN { get; set; }
        public string GHI_CHU { get; set; }


    }
}
