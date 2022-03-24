using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_API.Controllers
{

    public class NgheSyTinDungController : ApiController
    {
        DM_NGHESY_TINDUNGOperation dM_NGHESY_TINDUNGOperation;
        public NgheSyTinDungController()
        {
            dM_NGHESY_TINDUNGOperation = new DM_NGHESY_TINDUNGOperation();
        }
        [Route("api/BaiVietNgheSy")]
        public IHttpActionResult GetNghesyTindung()
        {
            return Json(dM_NGHESY_TINDUNGOperation.GetBVNgheSyTinDung());
        }
        [Route("api/CamNhanKhachHang")]
        public IHttpActionResult GetCamNhanKhachHang()
        {
            return Json(dM_NGHESY_TINDUNGOperation.GetCamNhanKhachHang());
        }
    }
}
