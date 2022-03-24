using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using System.Web.Script.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using Erp.Domain.Entities;

namespace Erp.BackOffice.Sale.Controllers
{

    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class DM_CAMNHAN_KHANGController : Controller
    {
        private readonly IDM_CAMNHAN_KHANGRepositories dm_camnhan_khangRepository;

        public DM_CAMNHAN_KHANGController(IDM_CAMNHAN_KHANGRepositories dm_camnhan_khang)
        {
            dm_camnhan_khangRepository = dm_camnhan_khang;
        }


        public ActionResult CheckSTTExsist(int? id, int stt)
        {
            var tintuc = dm_camnhan_khangRepository.GetAllDM_CAMNHAN_KHANG()
                .Where(item => item.STT == stt).FirstOrDefault();
            if (tintuc != null)
            {
                if (id == null || (id != null && tintuc.CAMNHAN_KHANG_ID != id))
                    return Content("Trùng số thứ tự!");
                else
                {
                    return Content("");
                }
            }
            else
            {
                return Content("");
            }
        }


        #region Index
        public ViewResult Index(string txtSearch)
        {
            var q = dm_camnhan_khangRepository.GetAllDM_CAMNHAN_KHANG().AsEnumerable()
           .Select(item => new DM_CAMNHAN_KHANGViewModel
           {
               CAMNHAN_KHANG_ID = item.CAMNHAN_KHANG_ID,
               CreatedUserId = item.CreatedUserId,
               CreatedDate = item.CreatedDate,
               ModifiedUserId = item.ModifiedUserId,
               ModifiedDate = item.ModifiedDate,
               IsDeleted = item.IsDeleted,
               AssignedUserId = item.AssignedUserId,
               TIEUDE = item.TIEUDE,
               LINK_VIDEO = item.LINK_VIDEO,
               STT = item.STT,
               IS_SHOW = item.IS_SHOW


           }).OrderBy(m => m.STT).ToList();

            if (string.IsNullOrEmpty(txtSearch) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);

                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.TIEUDE).Contains(txtSearch)).ToList();
            }
            return View(q);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            var camnhanlist = dm_camnhan_khangRepository.GetAllDM_CAMNHAN_KHANG().AsEnumerable();
            var model = new DM_CAMNHAN_KHANGViewModel();
            camnhanlist = camnhanlist.ToList();
            var _camnhanlist = camnhanlist.Select(item => new SelectListItem
                {
                    Text = item.TIEUDE,
                    Value = item.CAMNHAN_KHANG_ID.ToString()

                });
            ViewBag.camnhalist = _camnhanlist;
            return View(model);
        }
        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DM_CAMNHAN_KHANGViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dm_tintuc = new Domain.Sale.Entities.DM_CAMNHAN_KHANG();
                AutoMapper.Mapper.Map(model, dm_tintuc);
                dm_tintuc.IsDeleted = false;
                dm_tintuc.CreatedUserId = WebSecurity.CurrentUserId;
                dm_tintuc.ModifiedUserId = WebSecurity.CurrentUserId;
                dm_tintuc.CreatedDate = DateTime.Now;
                dm_tintuc.ModifiedDate = DateTime.Now;
                dm_tintuc.AssignedUserId = WebSecurity.CurrentUserId;

                dm_tintuc.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);
                dm_tintuc.LINK_VIDEO = model.LINK_VIDEO;
                dm_tintuc.TIEUDE = model.TIEUDE;
                dm_tintuc.STT = model.STT;



                dm_camnhan_khangRepository.InsertDM_CAMNHAN_KHANG(dm_tintuc);

                if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                {
                    return RedirectToAction("Edit", new { Id = model.CAMNHAN_KHANG_ID, IsPopup = Request["IsPopup"] });
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {

            var nhomtinList = dm_camnhan_khangRepository.GetAllDM_CAMNHAN_KHANG().AsEnumerable();
            var TinTuc = dm_camnhan_khangRepository.GetDM_CAMNHAN_KHANGByCAMNHAN_KHANG_ID(id);
            if (TinTuc != null && TinTuc.IsDeleted != true)
            {
                var model = new DM_CAMNHAN_KHANGViewModel();
                AutoMapper.Mapper.Map(TinTuc, model);

                nhomtinList = nhomtinList.ToList();
                var _nhomtinList = nhomtinList.Select(item => new SelectListItem
                {
                    Text = item.TIEUDE,
                    Value = item.CAMNHAN_KHANG_ID.ToString()
                });
                ViewBag.nhomtinList = _nhomtinList;
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(DM_CAMNHAN_KHANGViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dm_camnhan = dm_camnhan_khangRepository.GetDM_CAMNHAN_KHANGByCAMNHAN_KHANG_ID(model.CAMNHAN_KHANG_ID);
                dm_camnhan.ModifiedUserId = WebSecurity.CurrentUserId;
                dm_camnhan.ModifiedDate = DateTime.Now;
                dm_camnhan.AssignedUserId = WebSecurity.CurrentUserId;
                dm_camnhan.TIEUDE = model.TIEUDE;
                dm_camnhan.STT = model.STT;
                dm_camnhan.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);
                dm_camnhan.LINK_VIDEO = model.LINK_VIDEO;

                dm_camnhan_khangRepository.UpdateDM_CAMNHAN_KHANG(dm_camnhan);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                return RedirectToAction("Index");
            }

            return View(model);
        }
        #endregion

        #region Delete
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete()
        {

            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = dm_camnhan_khangRepository.GetDM_CAMNHAN_KHANGByCAMNHAN_KHANG_ID(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        dm_camnhan_khangRepository.DeleteDM_CAMNHAN_KHANG(item.CAMNHAN_KHANG_ID);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }

            /*int id = int.Parse(Request["Id"], CultureInfo.InvariantCulture);
            try
            {
                var item = dm_camnhan_khangRepository.GetDM_CAMNHAN_KHANGByCAMNHAN_KHANG_ID(id);
                if (item != null)
                {
                    dm_camnhan_khangRepository.DeleteDM_CAMNHAN_KHANG(item.CAMNHAN_KHANG_ID);
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }*/
        }
        #endregion
    }


}
