using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_API.Controllers
{
    [RoutePrefix("api/data")]
    public class GetDataController : ApiController
    {
        DM_LoaiSanPhamOperation loaisanpham;
        ProductOperation productOperation;
        DealHotOperation dealHotOperation;
        DM_NGHESY_TINDUNGOperation nghesyOpertion;
        SlideOperation slideOperation;
        public GetDataController()
        {
            loaisanpham = new DM_LoaiSanPhamOperation();
            productOperation = new ProductOperation();
            dealHotOperation = new DealHotOperation();
            nghesyOpertion = new DM_NGHESY_TINDUNGOperation();
            slideOperation = new SlideOperation();
        }
        [Route("GetSanPham_LoaiSPKhongCha")]
        public IHttpActionResult GetLoaiSanPhamKhongCha()
        {
            return Json(loaisanpham.GetLoaiSanPhamNullViews());
        }
        [Route("GetSanPham_LoaiSPCoCha")]
        public IHttpActionResult GetLoaiSanPhamCoCha()
        {
            return Json(loaisanpham.GetLoaiSanPhamViews());
        }
        [Route("GetLoaiSanPham")]
        public IHttpActionResult GetLoaiSanPham()
        {
            return Json(loaisanpham.GetLoaiSanPham());
        }
        [Route("GetSanPhamBestSeller")]
        public IHttpActionResult GetSanPhamBestSeller()
        {
            return Json(productOperation.GetProduct_Best_seller());
        }
        [Route("GetSanPhamChopDeal")]
        public IHttpActionResult GetSanPhamChopDeal()
        {
            return Json(dealHotOperation.GetKM_DotXuat());
        }
        [Route("GetGioiThieuYHL")]
        public IHttpActionResult GetGioiThieuYHL()
        {
            return Json(nghesyOpertion.Get_BAIVE_YHL());
        }
        [Route("GetSlides")]
        public IHttpActionResult GetSlides()
        {
            return Json(slideOperation.GetSlides());
        }
    }
}
