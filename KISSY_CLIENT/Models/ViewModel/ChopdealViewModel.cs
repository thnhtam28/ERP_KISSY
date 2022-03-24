using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class ChopdealViewModel
    {
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public int ProductID { get; set; }

        public decimal Price { get; set; }
    }
}