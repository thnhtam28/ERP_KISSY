using ERP_API.Models;
using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP_API.Controllers
{
    [RoutePrefix("api/danhmucsanpham")]
    public class Api_DM_NHOMSANPHAMController : ApiController
    {
        DM_NHOMSANPHAMOperation dM_NHOMSANPHAMOperation;
        public Api_DM_NHOMSANPHAMController()
        {
            dM_NHOMSANPHAMOperation = new DM_NHOMSANPHAMOperation();
        }
        [Route("GetNhomSanPham/{id}")]
        public IHttpActionResult GetDM_NhomSanPham(int id)
        {
            DM_NHOMSANPHAM model = dM_NHOMSANPHAMOperation.GetNHOMSANPHAM(id);
            return Json(model);
        }

        [Route("GetDSNhomSanPham")]
        public IHttpActionResult GetDM_NhomSanPhams()
        {
            List<DM_NHOMSANPHAM> result = dM_NHOMSANPHAMOperation.GetDM_NHOMSANPHAMs();
            return Json(result);
        }
        [Route("GetDSNhomSanPham/{cap}")]
        public IHttpActionResult GetCapDM_NhomSanPhams(int cap)
        {
            List<DM_NHOMSANPHAM> result = dM_NHOMSANPHAMOperation.GetCapDM_NHOMSANPHAMs(cap);
            return Json(result);
        }

        [Route("GetDMLoaiSanPhamNotIDCha")]
        public IHttpActionResult GetDM_LoaiSanPham_IDdmIsNull()
        {
            List<DM_LOAISANPHAM> result = dM_NHOMSANPHAMOperation.GetDM_LOAISANPHAMNotDMs();
            return Json(result);
        }

        [Route("GetDMLoaiSanPhamHaveIDCha")]
        public IHttpActionResult GetDM_LoaiSanPham()
        {
            List<DM_LOAISANPHAM> result = dM_NHOMSANPHAMOperation.GetDM_LOAISANPHAMs();
            return Json(result);
        }
    }
}
