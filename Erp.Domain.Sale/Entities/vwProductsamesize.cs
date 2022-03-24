using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwProductsamesize
    {
        public vwProductsamesize()
        {

        }

        public int Id { get; set; }
        public int Product_id { get; set; }
        public int Product_idsame { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? PriceOutBound { get; set; }
        public string TEN_NHOMSANPHAM { get; set; }
        public string TEN_LOAISANPHAM { get; set; }
        public Int64 stt { get; set; }
    }
}
