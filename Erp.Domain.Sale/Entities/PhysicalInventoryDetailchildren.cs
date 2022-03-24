using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class PhysicalInventoryDetailchildren
    {
        public PhysicalInventoryDetailchildren()
        {

        }

        public int Id { get; set; }
        public int? CreatedUserId { get; set; }
        public int ProductId { get; set; }
       // public string ProductCode { get; set; }
       // public string ProductName { get; set; }
        public int Qty { get; set; }
        public int PhysicalInventoryDetailId { get; set; }

        public DateTime CreatedDate { get; set; }
        public  int? NumberCheck { get; set; }
        public Nullable<bool> IsDeleted { get; set; }



    }
}
