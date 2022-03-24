using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class YHL_KIENHANG_TRA_CTIETViewModel
    {
         
        public int KIENHANG_TRA_CTIET_ID { get; set; }
        public int KIENHANG_TRA_ID { get; set; }
        public string GHI_CHU { get; set; }

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public int STT { get; set; }
        public string SO_HIEU { get; set; }
        public string MA_DON_HANG { get; set; }
        public string NGAY_KG { get; set; }
        public string NGUOI_NHAN { get; set; }
        public string DC_NHAN { get; set; }
        public string DT_NHAN { get; set; }

        public decimal TRI_GIA { get; set; }
        public decimal CUOC { get; set; }
        public string VUN { get; set; }
        public string CONG_THUC { get; set; }
        public decimal CUOC_EMS_HOAN { get; set; }
        public decimal CUOC_COD_HOAN { get; set; }
        public decimal PHAI_THU { get; set; }
        public string LYDO_HOAN { get; set; }
        public string NGUOI_NHAN_CUOC_HOAN { get; set; }
        public decimal KHOI_LUONG { get; set; }
    }
}