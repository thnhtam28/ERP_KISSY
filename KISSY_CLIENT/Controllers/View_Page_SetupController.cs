using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_API.Filters;
using ERP_API.Models;

namespace ERP_API.Controllers
{
    [AuthorByThuc]
    public class View_Page_SetupController : Controller
    {
        private ApplicationDbContext db;
        public View_Page_SetupController()
        {
            db = new ApplicationDbContext();
        }
        // GET: View_Page_Setup
        public ActionResult Index()
        {
            return View(db.Page_Setup.ToList());
        }

        
        public ActionResult Edit()
        {
            Page_Setup page_Setup = db.Page_Setup.First();
            if (page_Setup == null)
            {
                return HttpNotFound();
            }
            return View(page_Setup);
        }

        // POST: View_Page_Setup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyName,Logo,Address,Phone1,Phone2,Phone3,Fax,Email1,Email2,Email3,Facebook,Google,MST")] Page_Setup page_Setup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(page_Setup).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(page_Setup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
