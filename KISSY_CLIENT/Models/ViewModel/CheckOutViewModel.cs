using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class CheckOutViewModel
    {
        public ShoppingCartModels ShoppingCartModels { get; set; }
        public List<System_Loc_Province> System_Loc_Provinces { get; set; }
    }
}