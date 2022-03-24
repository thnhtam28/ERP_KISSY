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

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class DM_TIN_KHUYENMAIController : Controller
    {
        private readonly IDM_TIN_KHUYENMAIRepositories dm_tinkhuyenmaiRepository;

        public DM_TIN_KHUYENMAIController(IDM_TIN_KHUYENMAIRepositories dm_tinkhuyenmai)
        {
            dm_tinkhuyenmaiRepository = dm_tinkhuyenmai;
        }

        public ActionResult CheckSTTExsist(int? id, int stt)
        {
            var tintuc = dm_tinkhuyenmaiRepository.GetAllDM_TIN_KHUYENMAI()
                .Where(item => item.STT == stt).FirstOrDefault();
            if (tintuc != null)
            {
                if (id == null || (id != null && tintuc.TIN_KHUYENMAI_ID != id))
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
            var q = dm_tinkhuyenmaiRepository.GetAllDM_TIN_KHUYENMAI().AsEnumerable()
            .Select(item => new DM_TIN_KHUYENMAIViewModel
            {
                TIN_KHUYENMAI_ID = item.TIN_KHUYENMAI_ID,
                CreatedUserId = item.CreatedUserId,
                CreatedDate = item.CreatedDate,
                ModifiedUserId = item.ModifiedUserId,
                ModifiedDate = item.ModifiedDate,
                IsDeleted = item.IsDeleted,

                TIEUDE = item.TIEUDE,
                TOMTAT = item.TOMTAT,
                NOIDUNG = item.NOIDUNG,
                ANH_DAIDIEN = item.ANH_DAIDIEN,
                STT = item.STT,
                IS_SHOW = item.IS_SHOW
            }).OrderByDescending(m => m.ModifiedDate).ToList();

            q = q.ToList();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            var khuyenmaiList = dm_tinkhuyenmaiRepository.GetAllDM_TIN_KHUYENMAI().AsEnumerable();

            var model = new DM_TIN_KHUYENMAIViewModel();
            khuyenmaiList = khuyenmaiList.ToList();
            var _nhomtinList = khuyenmaiList.Select(item => new SelectListItem
            {
                Text = item.TIEUDE,
                Value = item.TIN_KHUYENMAI_ID.ToString()
            });
            ViewBag.khuyenmaiList = _nhomtinList;
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]
        public ActionResult
            Create(DM_TIN_KHUYENMAIViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {

                    var dm_tintuc = new Domain.Sale.Entities.DM_TIN_KHUYENMAI();
                    AutoMapper.Mapper.Map(model, dm_tintuc);
                    dm_tintuc.IsDeleted = false;
                    dm_tintuc.CreatedUserId = WebSecurity.CurrentUserId;
                    dm_tintuc.ModifiedUserId = WebSecurity.CurrentUserId;
                    dm_tintuc.CreatedDate = DateTime.Now;
                    dm_tintuc.ModifiedDate = DateTime.Now;
                    dm_tintuc.AssignedUserId = WebSecurity.CurrentUserId;

                    dm_tintuc.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);
                    dm_tintuc.ANH_DAIDIEN = "";
                    dm_tinkhuyenmaiRepository.InsertDM_TIN_KHUYENMAI(dm_tintuc);

                    dm_tintuc = dm_tinkhuyenmaiRepository.GetDM_TIN_KHUYENMAIByTIN_KHUYENMAI_ID(dm_tintuc.TIN_KHUYENMAI_ID);

                    //begin up hinh anh cho backend
                    var path = System.Web.HttpContext.Current.Server.MapPath("~" + Helpers.Common.GetSetting("tinkhuyenmai-image-folder"));
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            string image_name = "khuyenmai_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_tintuc.TIN_KHUYENMAI_ID.ToString(), @"\s+", "_")) + "." + file.FileName.Split('.').Last();
                            bool isExists = System.IO.Directory.Exists(path);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(path);
                            file.SaveAs(path + image_name);
                            dm_tintuc.ANH_DAIDIEN = image_name;
                        }
                    }

                    //end up hinh anh cho backend


                    //begin up hinh anh cho client
                    path = Helpers.Common.GetSetting("tinkhuyenmai-image-folder-client");
                    string prjClient = Helpers.Common.GetSetting("prj-client");
                    string filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                    DirectoryInfo di = new DirectoryInfo(filepath);
                    filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                    if (Request.Files["file-image"] != null)
                    {
                        var file = Request.Files["file-image"];
                        if (file.ContentLength > 0)
                        {
                            FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_tintuc.ANH_DAIDIEN);
                            if (fi.Exists)
                            {
                                fi.Delete();
                            }

                            string image_name = "khuyenmai_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_tintuc.TIN_KHUYENMAI_ID.ToString(), @"\s+", "_")) + "." + file.FileName.Split('.').Last();

                            bool isExists = System.IO.Directory.Exists(filepath);
                            if (!isExists)
                                System.IO.Directory.CreateDirectory(filepath);
                            file.SaveAs(filepath + image_name);
                            dm_tintuc.ANH_DAIDIEN = image_name;
                        }
                    }
                    //end up hinh anh cho client








                    dm_tinkhuyenmaiRepository.UpdateDM_TIN_KHUYENMAI(dm_tintuc);
                    scope.Complete();

                    if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                    {
                        return RedirectToAction("Edit", new { Id = model.TIN_KHUYENMAI_ID, IsPopup = Request["IsPopup"] });
                    }

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        #endregion

        #region Edit
        public ActionResult Edit(int Id)
        {
            var nhomtinList = dm_tinkhuyenmaiRepository.GetAllDM_TIN_KHUYENMAI().AsEnumerable();
            var TinTuc = dm_tinkhuyenmaiRepository.GetDM_TIN_KHUYENMAIByTIN_KHUYENMAI_ID(Id);
            if (TinTuc != null && TinTuc.IsDeleted != true)
            {
                var model = new DM_TIN_KHUYENMAIViewModel();
                AutoMapper.Mapper.Map(TinTuc, model);

                nhomtinList = nhomtinList.ToList();
                var _nhomtinList = nhomtinList.Select(item => new SelectListItem
                {
                    Text = item.TIEUDE,
                    Value = item.TIN_KHUYENMAI_ID.ToString()
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
        public ActionResult Edit(DM_TIN_KHUYENMAIViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dm_tintuc = dm_tinkhuyenmaiRepository.GetDM_TIN_KHUYENMAIByTIN_KHUYENMAI_ID(model.TIN_KHUYENMAI_ID);
                dm_tintuc.ModifiedUserId = WebSecurity.CurrentUserId;
                dm_tintuc.ModifiedDate = DateTime.Now;
                dm_tintuc.AssignedUserId = WebSecurity.CurrentUserId;

                dm_tintuc.TIEUDE = model.TIEUDE;
                dm_tintuc.TOMTAT = model.TOMTAT;
                dm_tintuc.NOIDUNG = model.NOIDUNG;
                dm_tintuc.STT = model.STT;
                dm_tintuc.IS_SHOW = Convert.ToInt32(Request["is-show-checkbox"]);


                //begin up hinh anh cho backend
                var path = Helpers.Common.GetSetting("tinkhuyenmai-image-folder");
                var filepath = System.Web.HttpContext.Current.Server.MapPath("~" + path);
                if (Request.Files["file-image"] != null)
                {
                    var file = Request.Files["file-image"];
                    if (file.ContentLength > 0)
                    {
                        FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_tintuc.ANH_DAIDIEN);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }

                        string image_name = "khuyenmai_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_tintuc.TIN_KHUYENMAI_ID.ToString(), @"\s+", "_")) + "." + file.FileName.Split('.').Last();

                        bool isExists = System.IO.Directory.Exists(filepath);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(filepath);
                        file.SaveAs(filepath + image_name);
                        dm_tintuc.ANH_DAIDIEN = image_name;
                    }
                }
                //end up hinh anh cho backend

                //begin up hinh anh cho client
                path = Helpers.Common.GetSetting("tinkhuyenmai-image-folder-client");
                string prjClient = Helpers.Common.GetSetting("prj-client");
                filepath = System.Web.HttpContext.Current.Server.MapPath("~");
                DirectoryInfo di = new DirectoryInfo(filepath);
                filepath = di.Parent.FullName + "\\" + prjClient + path.Replace("/", @"\");
                if (Request.Files["file-image"] != null)
                {
                    var file = Request.Files["file-image"];
                    if (file.ContentLength > 0)
                    {
                        FileInfo fi = new FileInfo(Server.MapPath("~" + path) + dm_tintuc.ANH_DAIDIEN);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }

                        string image_name = "khuyenmai_" + Helpers.Common.ChuyenThanhKhongDau(Regex.Replace(dm_tintuc.TIN_KHUYENMAI_ID.ToString(), @"\s+", "_")) + "." + file.FileName.Split('.').Last();

                        bool isExists = System.IO.Directory.Exists(filepath);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(filepath);
                        file.SaveAs(filepath + image_name);
                        dm_tintuc.ANH_DAIDIEN = image_name;
                    }
                }
                //end up hinh anh cho client



                dm_tinkhuyenmaiRepository.UpdateDM_TIN_KHUYENMAI(dm_tintuc);

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
                    var item = dm_tinkhuyenmaiRepository.GetDM_TIN_KHUYENMAIByTIN_KHUYENMAI_ID(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        dm_tinkhuyenmaiRepository.DeleteDM_TIN_KHUYENMAI(item.TIN_KHUYENMAI_ID);
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
