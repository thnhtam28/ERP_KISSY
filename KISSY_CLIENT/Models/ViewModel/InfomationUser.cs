using ERP_API.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class InfomationUser
    {
        public CustomerInfo CustomerInfo { get; set; }
        public List<Sale_ProductInvoice_View> Sale_ProductInvoice_Views { get; set; }
        public List<Sale_Product> Page_ViewProductUsers { get; set; }
        public thongtinnguoimuaViewModel thongtinmuavm { get; set; }
        
    }
}