
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class Sale_BaoCaoXuatKhoViewModelhoapd
    {
        public int KHOID { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CHUNGTU { get; set; }
        public int CHUNGTUID { get; set; }
        public string Type { get; set; }
        public int NHAP { get; set; }
        public int XUAT{ get; set; }
        public int TON { get; set; }
        public int soluongdangchuyen { get; set; }
        
    }
}