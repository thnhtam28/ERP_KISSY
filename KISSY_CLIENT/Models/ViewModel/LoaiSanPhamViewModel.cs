using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class LoaiSanPhamViewModel
    {
        public DM_LOAISANPHAM DM_LOAISANPHAM { get; set; }
        public List<caltalogProduct> CaltalogProducts { get; set; }
    }
    public class caltalogProduct
    {
        public DM_LOAISANPHAM DM_LOAISANPHAM { get; set; }
        public List<Sale_Product> Sale_Products { get; set; }
    }
}