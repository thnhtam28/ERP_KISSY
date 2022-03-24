using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class ProductViewModel
    {
        public Sale_Product Sale_Product { get; set; }
        public List<Sale_Product> Sale_Product_Samegroups { get; set; }
        public List<Sale_Product_samesize> Sale_Product_Samesizes { get; set; }
    }
}