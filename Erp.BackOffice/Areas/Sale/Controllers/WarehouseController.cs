using System.Globalization;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Staff.Interfaces;
using System.Transactions;
using System.Web;

namespace Erp.BackOffice.Sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseRepository WarehouseRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IObjectAttributeRepository ObjectAttributeRepository;
        private readonly IUserRepository userRepository;

        public WarehouseController(
            IWarehouseRepository _Warehouse
            , IBranchRepository _branch
            , IObjectAttributeRepository _ObjectAttribute
            , IUserRepository _user
            )
        {
            WarehouseRepository = _Warehouse;
            branchRepository = _branch;
            ObjectAttributeRepository = _ObjectAttribute;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch, string txtCode, string txtAddress)
        {

            HttpRequestBase request = this.HttpContext.Request;
            string strBrandID = "0";
            if (request.Cookies["BRANCH_ID_CookieName"] != null)
            {
                strBrandID = request.Cookies["BRANCH_ID_CookieName"].Value;
                if (strBrandID == "")
                {
                    strBrandID = "0";
                }
            }



            if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
            {
                strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
            }

            int? intBrandID = int.Parse(strBrandID);


            IEnumerable<WarehouseViewModel> q = WarehouseRepository.GetAllWarehouse().Where(x => x.BranchId == intBrandID || intBrandID == 0).AsEnumerable()
                .Select(item => new WarehouseViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                    Code = item.Code,
                    Address = item.Address,
                    BranchId = item.BranchId
                }).OrderByDescending(m => m.ModifiedDate);

            if (string.IsNullOrEmpty(txtSearch) == false || string.IsNullOrEmpty(txtCode) == false || string.IsNullOrEmpty(txtAddress) == false)
            {
                txtSearch = txtSearch == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtSearch);
                txtCode = txtCode == "" ? "~" : txtCode.ToLower();
                txtAddress = txtAddress == "" ? "~" : Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(txtAddress).ToLower().Trim();
                q = q.Where(x => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Name).Contains(txtSearch) || x.Code.ToLowerOrEmpty().Contains(txtCode) || Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(x.Address).ToLower().Contains(txtAddress));
            }
            //if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin())
            //{
            //    //lấy tất cả kho mà user quản lý hiện ra
            //    // admin thì hiện tất cả
            //    q = q.Where(x => ("," + Helpers.Common.CurrentUser.DrugStore + ",").Contains("," + x.BranchId + ",") == true);
            //}
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new WarehouseViewModel();
            model.IsSale = true;

            // ViewBag.branchList = branchRepository.GetAllBranch().Where(x=>x.ParentId!=null&&x.ParentId>0).AsEnumerable().Select(x => new SelectListItem { Value = x.Id + "", Text = x.Name });

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WarehouseViewModel model)
        {

            if (ModelState.IsValid)
            {

                HttpRequestBase request = this.HttpContext.Request;
                string strBrandID = "0";
                if (request.Cookies["BRANCH_ID_CookieName"] != null)
                {
                    strBrandID = request.Cookies["BRANCH_ID_CookieName"].Value;
                    if (strBrandID == "")
                    {
                        strBrandID = "0";
                    }
                }



                if ((Helpers.Common.CurrentUser.BranchId != null) && (Helpers.Common.CurrentUser.BranchId > 0))
                {
                    strBrandID = Helpers.Common.CurrentUser.BranchId.ToString();
                }

                int? intBrandID = int.Parse(strBrandID);

                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        var ListUserId = Request["ListUserId"];
                        var Warehouse = new Domain.Sale.Entities.Warehouse();
                        AutoMapper.Mapper.Map(model, Warehouse);
                        Warehouse.IsDeleted = false;
                        Warehouse.CreatedUserId = WebSecurity.CurrentUserId;
                        Warehouse.ModifiedUserId = WebSecurity.CurrentUserId;
                        Warehouse.CreatedDate = DateTime.Now;
                        Warehouse.ModifiedDate = DateTime.Now;
                        Warehouse.KeeperId = ListUserId;
                        Warehouse.Categories = Request["Categories"];
                        Warehouse.IsSale = model.IsSale;
                        Warehouse.BranchId = intBrandID;


                        var wavehouse = WarehouseRepository.GetvwAllWarehouse().Where(n => n.IsDeleted == false).ToList();

                        foreach (var item in wavehouse)
                        {

                            var CodeItem = Helpers.Common.ChuyenThanhKhongDau(item.Code.Trim());
                            var NameItem = Helpers.Common.ChuyenThanhKhongDau(item.Name.Trim());
                            if (CodeItem == Helpers.Common.ChuyenThanhKhongDau(model.Code.Trim()) || CodeItem == "")
                            {
                                TempData[Globals.FailedMessageKey] = " Mã kho rỗng hoặc đã tồn tại!";
                                //if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                                //{
                                //    return RedirectToAction("_ClosePopup", "Warehouse", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                                //}
                                return RedirectToAction("Index", "Warehouse");
                            }
                            else if (NameItem == "" || NameItem == Helpers.Common.ChuyenThanhKhongDau(model.Name.Trim()))
                            {
                                TempData[Globals.FailedMessageKey] = " Tên kho rỗng hoặc đã tồn tại!";
                                //if (Request["IsPopup"] == "true" || Request["IsPopup"] == "True")
                                //{
                                //    return RedirectToAction("_ClosePopup", "Warehouse", new { area = "", FunctionCallback = "ClosePopupAndReloadPage" });
                                //}
                                return RedirectToAction("Index", "Warehouse");
                            }
                        }

                        //tạo đặc tính động cho kho hàng nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                        ObjectAttributeController.CreateOrUpdateForObject(Warehouse.Id, model.AttributeValueList);

                        WarehouseRepository.InsertWarehouse(Warehouse);
                        scope.Complete();
                        if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                        {
                            ViewBag.closePopup = "close and append to page parent";
                            model.Id = Warehouse.Id;
                            return View(model);
                        }


                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateException)
                    {
                        return Content("Fail");
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Warehouse = WarehouseRepository.GetWarehouseById(Id.Value);
            if (Warehouse != null && Warehouse.IsDeleted != true)
            {
                var model = new WarehouseViewModel();
                AutoMapper.Mapper.Map(Warehouse, model);
                List<string> listCategories = new List<string>();
                if (!string.IsNullOrEmpty(Warehouse.Categories))
                {
                    listCategories = Warehouse.Categories.Split(',').ToList();
                }
                ViewBag.listCategories = listCategories;

                List<string> listKeeperID = new List<string>();
                if (!string.IsNullOrEmpty(Warehouse.KeeperId))
                {
                    listKeeperID = Warehouse.KeeperId.Split(',').ToList();
                }
                ViewBag.listKeeperID = listKeeperID;
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(WarehouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        try
                        {
                            var ListUserId = Request["ListUserId"];
                            var Warehouse = WarehouseRepository.GetWarehouseById(model.Id);
                            AutoMapper.Mapper.Map(model, Warehouse);
                            Warehouse.KeeperId = ListUserId;
                            Warehouse.Categories = Request["Categories"];
                            Warehouse.ModifiedUserId = WebSecurity.CurrentUserId;
                            Warehouse.ModifiedDate = DateTime.Now;
                            Warehouse.IsSale = model.IsSale;
                            Warehouse.BranchId = Warehouse.BranchId;

                            //tạo đặc tính động cho kho hàng nếu có danh sách đặc tính động truyền vào và đặc tính đó phải có giá trị
                            ObjectAttributeController.CreateOrUpdateForObject(Warehouse.Id, model.AttributeValueList);

                            WarehouseRepository.UpdateWarehouse(Warehouse);
                            scope.Complete();
                            if (string.IsNullOrEmpty(Request["IsPopup"]) == false)
                            {
                                return RedirectToAction("Edit", new { Id = model.Id, IsPopup = Request["IsPopup"] });
                            }

                            TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                            return RedirectToAction("Index");
                        }
                        catch (DbUpdateException)
                        {
                            return Content("Fail");
                        }
                    }
                }

                return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = WarehouseRepository.GetWarehouseById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));


                    if (item != null)
                    {
                        //begin hoap check xem da su dung chua
                        var listInventory = Domain.Helper.SqlHelper.QuerySP<InventoryViewModel>("spSale_Get_Inventory", new { WarehouseId = item.Id, HasQuantity = "0", ProductCode = "", ProductName = "", CategoryCode = "", ProductGroup = "", BranchId = 0, LoCode = "", ProductId = "", ExpiryDate = "" });
                        //end hoap check xem da su dung chua
                        if ((listInventory == null) || (listInventory.Count() == 0))
                        {
                            item.IsDeleted = true;
                            WarehouseRepository.UpdateWarehouse(item);
                        }
                        else
                        {
                            TempData[Globals.FailedMessageKey] = "Kho đã được sử dụng không thể xóa";
                            return RedirectToAction("Index");
                        }
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

        #region
        public JsonResult GetListUserByBranchId(int? branchId)
        {
            if (branchId == null)
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);

            var list = userRepository.GetAllUsers()
                .Select(x => new
                {
                    e = x.FullName,
                    a = x.Id,
                    b = x.UserTypeId,
                    c = x.UserName,
                    d = x.Status,
                    f = x.BranchId,
                });
            if (!Erp.BackOffice.Filters.SecurityFilter.IsAdmin())
            {
                if (branchId != null && branchId.Value > 0)
                {
                    list = list.Where(x => x.f == branchId);
                }
                else
                {
                    list = null;
                }
            }
            else
            {
                if (branchId != null && branchId.Value > 0)
                {
                    list = list.Where(x => x.f == branchId);
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region CheckJson

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CheckCode(string code)
        {

            var wavehouse = WarehouseRepository.GetvwAllWarehouse().Where(n => n.IsDeleted == false).ToList();
            try
            {
                foreach (var item in wavehouse)
                {

                    var CodeItem = Helpers.Common.ChuyenThanhKhongDau(item.Code.Trim());
                    if (CodeItem == Helpers.Common.ChuyenThanhKhongDau(code.Trim()) || CodeItem == "")
                    {
                        return Json(1);
                    }

                }
                return Json(0);
            }
            catch (Exception ex)
            {
                return Json(0);
            }

        }

        #endregion
    }
}
