using System.Globalization;
using Erp.BackOffice.sale.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.sale.Entities;
using Erp.Domain.sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;

namespace Erp.BackOffice.sale.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class saleController : Controller
    {
        private readonly IsaleRepository saleRepository;
        private readonly IUserRepository userRepository;

        public saleController(
            IsaleRepository _sale
            , IUserRepository _user
            )
        {
            saleRepository = _sale;
            userRepository = _user;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<saleViewModel> q = saleRepository.GetAllsale()
                .Select(item => new saleViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new saleViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(saleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sale = new sale();
                AutoMapper.Mapper.Map(model, sale);
                sale.IsDeleted = false;
                sale.CreatedUserId = WebSecurity.CurrentUserId;
                sale.ModifiedUserId = WebSecurity.CurrentUserId;
                sale.AssignedUserId = WebSecurity.CurrentUserId;
                sale.CreatedDate = DateTime.Now;
                sale.ModifiedDate = DateTime.Now;
                saleRepository.Insertsale(sale);

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var sale = saleRepository.GetsaleById(Id.Value);
            if (sale != null && sale.IsDeleted != true)
            {
                var model = new saleViewModel();
                AutoMapper.Mapper.Map(sale, model);
                
                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }                

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(saleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var sale = saleRepository.GetsaleById(model.Id);
                    AutoMapper.Mapper.Map(model, sale);
                    sale.ModifiedUserId = WebSecurity.CurrentUserId;
                    sale.ModifiedDate = DateTime.Now;
                    saleRepository.Updatesale(sale);

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var sale = saleRepository.GetsaleById(Id.Value);
            if (sale != null && sale.IsDeleted != true)
            {
                var model = new saleViewModel();
                AutoMapper.Mapper.Map(sale, model);
                
                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }                

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
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
                    var item = saleRepository.GetsaleById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        saleRepository.Updatesale(item);
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
