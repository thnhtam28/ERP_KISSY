using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class CartMessage
    {
        public string MaDH { get; set; }
        public string Message { get; set; }
        public string MessageProduct { get; set; }
        public string Icon { get; set; }
        public string Total { get; set; }
        public decimal? Total_value { get; set; }
        public string StrSKU { get; set; }
        public int CountItem { get; set; }
        public string HTTT { get; set; }
    }
}