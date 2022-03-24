using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwProductSKU
    {
        public vwProductSKU()
        {

        }
        public int Id { get; set; }    
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int AssignedUserId { get; set; }

        public string color { get; set; }
        public string Size { get; set; }

        public Nullable<int> Product_id { get; set; }
        public Nullable<int> Product_idSKU { get; set; }

        public string CodeSKU { get; set; }

        public string CodeSP { get; set; }

        public Nullable<bool> Is_display { get; set; }

    }
}
