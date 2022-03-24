using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class DM_BANNER_SLIDER
    {
        public DM_BANNER_SLIDER() 
        {
        }
        public int BANNER_SLIDER_ID {get; set;}
        public string ANH_DAIDIEN {get; set;}
        public int STT { get; set; }
        public string LINK { get; set; }
        public int IS_SHOW { get; set; }
        public int IS_MOBILE { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
    }
}
