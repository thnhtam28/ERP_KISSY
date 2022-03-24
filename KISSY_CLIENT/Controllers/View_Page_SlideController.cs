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
    public class View_Page_SlideController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: View_Page_Slide
        public ActionResult Index()
        {
            return View(db.Page_Slide.ToList());
        }

        // GET: View_Page_Slide/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_Slide page_Slide = db.Page_Slide.Find(id);
            if (page_Slide == null)
            {
                return HttpNotFound();
            }
            return View(page_Slide);
        }

        // GET: View_Page_Slide/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: View_Page_Slide/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Photo,Link,STT,F1,F2,F3,F4,F5")] Page_Slide page_Slide)
        {
            if (ModelState.IsValid)
            {
                db.Page_Slide.Add(page_Slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(page_Slide);
        }

        // GET: View_Page_Slide/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_Slide page_Slide = db.Page_Slide.Find(id);
            if (page_Slide == null)
            {
                return HttpNotFound();
            }
            return View(page_Slide);
        }

        // POST: View_Page_Slide/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Photo,Link,STT,F1,F2,F3,F4,F5")] Page_Slide page_Slide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(page_Slide).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(page_Slide);
        }

        // GET: View_Page_Slide/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_Slide page_Slide = db.Page_Slide.Find(id);
            if (page_Slide == null)
            {
                return HttpNotFound();
            }
            return View(page_Slide);
        }

        // POST: View_Page_Slide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Page_Slide page_Slide = db.Page_Slide.Find(id);
            db.Page_Slide.Remove(page_Slide);
            db.SaveChanges();
            return RedirectToAction("Index");
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
