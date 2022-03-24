using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class YHL_KIENHANG_GUIViewModel
    {
        public int KIENHANG_GUI_ID { get; set; }

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        [Display(Name="Đơn Vị Giao Nhận")]
        public string DONVI_GIAONHAN { get; set; }

        [Display(Name="Ngày Gửi")]
        public System.DateTime NGAY_GUI { get; set; }

        [Display(Name="Người Nhận")]
        public string NGUOI_NHAN { get; set; }

        [Display(Name="Ghi Chú")]
        public string GHI_CHU { get; set; }

        public List<YHL_KIENHANG_GUI_CTIETViewModel> DetailList { get; set; }
    }
}