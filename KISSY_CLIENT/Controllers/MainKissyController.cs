using ERP_API.EnumData;
using ERP_API.Filters;
using ERP_API.Models;
using ERP_API.Models.ViewModel;
using ERP_API.Operation.API;
using ERP_API.Operation.Nomal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_API.Controllers
{
    public class MainKissyController : Controller
    {
        SlideOperation slideOperation;
        TinTucOperation tinTucOperation;
        ProductOperation productOperation;
        DM_NHOMSANPHAMOperation menu;
        ApiPageSetupOperation apiPageSetupOperation;
        FeedBackOperation feedBackOperation;
        public MainKissyController()
        {
            slideOperation = new SlideOperation();
            tinTucOperation = new TinTucOperation();
            productOperation = new ProductOperation();
            menu = new DM_NHOMSANPHAMOperation();
            apiPageSetupOperation = new ApiPageSetupOperation();
            feedBackOperation = new FeedBackOperation();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header1MainPartial()
        {
            ViewBag.hotLine = Common.GetSetting("HOTLINE");
            ViewBag.IG = Common.GetSetting("IG");
            ViewBag.FB = Common.GetSetting("FB");
            return PartialView();
        }
       
        public ActionResult Header2MainPartial()
        {
            var value = menu.GetTatCaDanhMuc();
            return PartialView(value);
        }
        public ActionResult HeaderMobileMainPartial()
        {
            var value = menu.GetTatCaDanhMuc();
            return PartialView(value);
        }
        public ActionResult SliderPartial()
        {
            List<DM_BANNER_SLIDER> List = slideOperation.GetSlides();
            return PartialView(List);

        }
        public ActionResult TinNoiBatPartial()
        {
            var data = tinTucOperation.GetDanhSachTinTucNoiBatMoiNhat();
            return PartialView(data);
        }
        public ActionResult SanPhamMainPartial()
        {
            //var data = productOperation.GetSanPhamMoi(4);
            return PartialView();
        }
        public ActionResult SanPhamMoiMainPartial()
        {
            var data = productOperation.GetSanPhamMoi(16);
            return PartialView(data);
        }
        public ActionResult SanPhamBanChayMainPartial()
        {
            var data = productOperation.GetSanPhamBanChay(16);
            return PartialView(data);
        }
        public ActionResult TinTucTuyenDungMainPartial()
        {
            var data = tinTucOperation.GetBaiViet("TUYENDUNG_YHL");
            return PartialView(data);
        }
        public ActionResult TinTucPTThanhToanMainPartial()
        {
            var data = tinTucOperation.GetBaiViet("PHUONG_THUCDATHAN");
            return PartialView(data);
        }
        public ActionResult GuiThongTinMainPartial()
        {

            return PartialView();
        }
        public ActionResult TinTucMainPartial()
        {
         
            return PartialView();
        }
        [HttpPost]
        public ActionResult InsertFeedBack(Feedback feedback)
        {
            try
            {
                if(feedback==null)
                    return Json("[101] Lỗi khi gửi thông tin");
                feedback.CreatedDate = DateTime.UtcNow.AddHours(7);
                feedBackOperation.InsertFeedBack(feedback);
                return Json("Cảm ơn bạn đã để lại feedback cho chúng tôi");
            }
            catch
            {
                return Json("Thông tin bạn gửi bị lỗi, xin vui lòng liên hệ trực tiếp cho shop");
            }

        }
        public ActionResult FooterMainPartial()
        {
            var veyhl = tinTucOperation.GetBaiViet("VEYHL");
            var tuyendung = tinTucOperation.GetBaiViet("TUYENDUNG_YHL");
            var pttt = tinTucOperation.GetBaiViet("PHUONG_THUCDATHAN");
            FooterViewModel model = new FooterViewModel();
            model.GetGioiThieu = veyhl;
            model.GetPhuongThucDatHang = pttt;
            model.GetTuyenDung = tuyendung;
            ViewBag.hotLine = Common.GetSetting("HOTLINE");
            ViewBag.IG = Common.GetSetting("IG");
            ViewBag.FB = Common.GetSetting("FB");
            return PartialView(model);
        }

      
    }
}