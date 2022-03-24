using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class YHL_KIENHANG_GUI_CTIET
    {
        public YHL_KIENHANG_GUI_CTIET() { }

        public int KIENHANG_GUI_CTIET_ID { get; set; }
        public int KIENHANG_GUI_ID { get; set; }
        public string TRANG_THAIDONHANG_GUI { get; set; }
        public string GHI_CHU { get; set; }
   

        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public int STT_DEN { get; set; }
        public string SO_HIEU { get; set; }
        public string MA_DON_HANG { get; set; }
        public string NGAY_KG { get; set; }
        public string NGUOI_GUI { get; set; }
        public string NGUOI_NHAN { get; set; }
        public string DC_NHAN { get; set; }
        public string DT_NHAN { get; set; }
        public string TEN_BC_NHAN { get; set; }
        public decimal KHOI_LUONG { get; set; }
        public decimal KHOI_LUONG_QD { get; set; }
        public string NOI_DUNG { get; set; }
        public string DV_DB { get; set; }
        public decimal TRI_GIA { get; set; }
        public int vung_xa { get; set; }
        public decimal CUOC_DV { get; set; }
        public decimal Cuoc_COD { get; set; }
        public decimal CUOC_DVCT { get; set; }
        public decimal TIEN_VAT { get; set; }
        public decimal TONG_CUOC { get; set; }
        
    }
}
