using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Erp.BackOffice.Sale.Models
{
    public class YHL_KIENHANG_TRAViewModel
    {
        public int KIENHANG_TRA_ID { get; set; }

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }


        public System.DateTime NGAY_TRA { get; set; }
        public string NGUOI_TRA { get; set; }
        public string GHI_CHU { get; set; }

    }
}