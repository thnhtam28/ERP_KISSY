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
using System.Transactions;

namespace Erp.BackOffice.Sale.Models
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class DM_NGHESY_TINDUNGController : Controller
    {
        private readonly IDM_NGHESY_TINDUNGRepositories dm_nghesyRepository;

        public DM_NGHESY_TINDUNGController(IDM_NGHESY_TINDUNGRepositories nghesy)
        {
            dm_nghesyRepository = nghesy;
        }
        public ActionResult CheckSTTExsist(int? id, int stt)
        {
            var tintuc = dm_nghesyRepository.GetAllDM_NGHESY_TINDUNG()
                .Where(item => item.STT == stt).FirstOrDefault();
            if (tintuc != null)
            {
                if (id == null || (id != null && tintuc.NGHESY_TINDUNG_ID != id))
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
        public ViewResult Index()
        {
            var q = dm_nghesyRepository.GetAllDM_NGHESY_TINDUNG().AsEnumerable()
           .Select(item => new DM_NGHESY_TINDUNGViewModel
           {
               NGHESY_TINDUNG_ID = item.NGHESY_TINDUNG_ID,
               CreatedUserId = item.CreatedUserId,
               CreatedDate = item.CreatedDate,
               ModifiedUserId = item.ModifiedUserId,
               ModifiedDate = item.ModifiedDate,
               IsDeleted = item.IsDeleted,
               AssignedUserId = item.AssignedUserId,
               TEN_NGHESY = item.TEN_NGHESY,
               NOIDUNG = item.NOIDUNG,
               HINHANH = item.HINHANH,
               STT = item.STT,
               IS_SHOW = item.IS_SHOW


           }).OrderBy(m => m.STT).ToList();
            return View(q);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            var nghesylist = dm_nghesyRepository.GetAllDM_NGHESY_TINDUNG().AsEnumerable();
            var model = new DM_NGHESY_TINDUNGViewModel();
            nghesylist = nghesylist.ToList();
            var _nghesylist = nghesylist.Select(item => new SelectListItem
            {
                Text = item.NOIDUNG,
                Value = item.NGHESY_TINDUNG_ID.ToString()

            });
            ViewBag.nghesylist = _nghesylist;
            return View(model);
        }
        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DM_NGHESY_TINDUNGViewModel model)
        {
            if (ModelState.IsValid)
            {

                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {

                    var dm_tintuc = new Domain.Sale.Entities.DM_NGHESY_TINDUNG();
                    AutoMapper.Mapper.Map(model, dm_tintuc);
                    dm_tintuc.IsDeleted = false;
                    dm_tintuc.CreatedUserId = WebSecurity.CurrentUserId;
                    dm_tintuc.ModifiedUserId = WebSecurity.CurrentUserId;
                    dm_tintuc.CreatedDate = DateTime.Now;
                    dm_tintuc.ModifiedDate = DateTime.Now;
                    dm_tintuc.AssignedUserId = WebSecurity.CurrentUserId;

                    dm_tintuc.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);

                    dm_tintuc.STT = model.STT;
                    dm_tintuc.TEN_NGHESY = model.TEN_NGHESY;
                    dm_tintuc.NOIDUNG = model.NOIDUNG;
                    dm_tintuc.HINHANH = "";

                    dm_nghesyRepository.InsertDM_NGHESY_TINDUNG(dm_tintuc);
                    dm_tintuc = dm_nghesyRepository.GetDM_NGHESY_TINDUNGByNGHESY_TINDUNG_ID(dm_tintuc.NGHESY_TINDUNG_ID);
                    //begin up hinh anh cho backend
                    var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("nghesy-image-folder"));
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            string image_name = "nghesy_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_tintuc.NGHESY_TINDUNG_ID.ToString(), @"\s+", "_")) + "." + file.FileName.Split('.').Last();
                            bool isExists = System.IO.Directory.Exists(path);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(path);
                            file.SaveAs(path + image_name);
                            dm_tintuc.HINHANH = image_name;
                        }
                    }
                    //end up hinh anh cho backend

                    //begin up hinh anh cho client
                    path = Helpers.Common.GetSetting("nghesy-image-folder-client");
                    string prjClient = Helpers.Common.GetSetting("prj-client");
                    string filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                    DirectoryInfo di = new DirectoryInfo(filepath);
                    filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_tintuc.HINHANH);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = "nghesy_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_tintuc.NGHESY_TINDUNG_ID.ToString(), @"\s+", "_")) + "." + file.FileName.Split('.').Last();

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_tintuc.HINHANH = image_name;
                        }
                    }



                    dm_nghesyRepository.UpdateDM_NGHESY_TINDUNG(dm_tintuc);

                    scope.Complete();

                    if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                    {
                        return RedirectToAction("Edit", new { Id = model.NGHESY_TINDUNG_ID, IsPopup = Request["IsPopup"] });
                    }

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {

            var nghesyList = dm_nghesyRepository.GetAllDM_NGHESY_TINDUNG().AsEnumerable();
            var TinTuc = dm_nghesyRepository.GetDM_NGHESY_TINDUNGByNGHESY_TINDUNG_ID(id);
            if (TinTuc != null && TinTuc.IsDeleted != true)
            {
                var model = new DM_NGHESY_TINDUNGViewModel();
                AutoMapper.Mapper.Map(TinTuc, model);

                nghesyList = nghesyList.ToList();
                var _nhomtinList = nghesyList.Select(item => new SelectListItem
                {
                    Text = item.NOIDUNG,
                    Value = item.NGHESY_TINDUNG_ID.ToString()
                });
                ViewBag.nghesyList = _nhomtinList;
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(DM_NGHESY_TINDUNGViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dm_nghesy = dm_nghesyRepository.GetDM_NGHESY_TINDUNGByNGHESY_TINDUNG_ID(model.NGHESY_TINDUNG_ID);
                dm_nghesy.ModifiedUserId = WebSecurity.CurrentUserId;
                dm_nghesy.ModifiedDate = DateTime.Now;
                dm_nghesy.AssignedUserId = WebSecurity.CurrentUserId;
                dm_nghesy.STT = model.STT;
                dm_nghesy.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);
                dm_nghesy.NOIDUNG = model.NOIDUNG;
                dm_nghesy.TEN_NGHESY = model.TEN_NGHESY;

                //begin up hinh anh cho backend
                var path = Helpers.Common.GetSetting("nghesy-image-folder");
                var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                if (Request.Files["file-image"] != null)
                {
                    var file = Request.Files["file-image"];
                    if (file.ContentLength > 0)
                    {
                        FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_nghesy.HINHANH);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }

                        string image_name = "nghesy_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_nghesy.NGHESY_TINDUNG_ID.ToString(), @"\s+", "_")) + "." + file.FileName.Split('.').Last();

                        bool isExists = System.IO.Directory.Exists(filepath);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(filepath);
                        file.SaveAs(filepath + image_name);
                        dm_nghesy.HINHANH = image_name;
                    }
                }
                //end up hinh anh cho backend

                //begin up hinh anh cho client
                path = Helpers.Common.GetSetting("nghesy-image-folder-client");
                string prjClient = Helpers.Common.GetSetting("prj-client");
                filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                DirectoryInfo di = new DirectoryInfo(filepath);
                filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                if (Request.Files["file-image"] != null)
                {
                    var file = Request.Files["file-image"];
                    if (file.ContentLength > 0)
                    {
                        FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_nghesy.HINHANH);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }

                        string image_name = "nghesy_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_nghesy.NGHESY_TINDUNG_ID.ToString(), @"\s+", "_")) + "." + file.FileName.Split('.').Last();

                        bool isExists = System.IO.Directory.Exists(filepath);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(filepath);
                        file.SaveAs(filepath + image_name);
                        dm_nghesy.HINHANH = image_name;
                    }
                }
                //end up hinh anh cho client
                dm_nghesyRepository.UpdateDM_NGHESY_TINDUNG(dm_nghesy);

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
                    var item = dm_nghesyRepository.GetDM_NGHESY_TINDUNGByNGHESY_TINDUNG_ID
                        (int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        dm_nghesyRepository.DeleteDM_NGHESY_TINDUNG(item.NGHESY_TINDUNG_ID);
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
        }

        #endregion


    }
}
