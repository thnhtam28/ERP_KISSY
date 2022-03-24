using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class DanhMuc_Menu3Cap
    {
        public DM_NHOMSANPHAM MenuCap1 { get; set; }
        public List<Danh_MucMenu2Cap> MenuCap2_3 { get; set; }
        public bool Checked { get; set; }
    }
    public class Danh_MucMenu2Cap {
        public DM_NHOMSANPHAM MenuCap2 { get; set; }
        public List<DM_NHOMSANPHAM> MenuCap3 { get; set; }
        public bool Checked { get; set; }
    }
    public class Danh_MucMenu_SanPham
    {
        public DM_NHOMSANPHAM MenuCap2 { get; set; }
        public List<SanPham_Menu> MenuCap3 { get; set; }
    }
    public class SanPham_Menu {
        public DM_NHOMSANPHAM Menu { get; set; }
        public int SoLuong { get; set; }
    }
}