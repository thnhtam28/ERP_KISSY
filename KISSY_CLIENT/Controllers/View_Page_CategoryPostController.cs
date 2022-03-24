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
    public class View_Page_CategoryPostController : Controller
    {
        private ApplicationDbContext db;
        public View_Page_CategoryPostController()
        {
            db = new ApplicationDbContext();
        }
        // GET: View_Page_CategoryPost
        public ActionResult Index()
        {
            return View(db.Page_CategoryPost.ToList());
        }

        // GET: View_Page_CategoryPost/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_CategoryPost page_CategoryPost = db.Page_CategoryPost.Find(id);
            if (page_CategoryPost == null)
            {
                return HttpNotFound();
            }
            return View(page_CategoryPost);
        }

        // GET: View_Page_CategoryPost/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: View_Page_CategoryPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,STT,DateCreated,F1,F2,F3,F4,F5")] Page_CategoryPost page_CategoryPost)
        {
            if (ModelState.IsValid)
            {
                db.Page_CategoryPost.Add(page_CategoryPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(page_CategoryPost);
        }

        // GET: View_Page_CategoryPost/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_CategoryPost page_CategoryPost = db.Page_CategoryPost.Find(id);
            if (page_CategoryPost == null)
            {
                return HttpNotFound();
            }
            return View(page_CategoryPost);
        }

        // POST: View_Page_CategoryPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,STT,DateCreated,F1,F2,F3,F4,F5")] Page_CategoryPost page_CategoryPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(page_CategoryPost).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(page_CategoryPost);
        }

        // GET: View_Page_CategoryPost/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page_CategoryPost page_CategoryPost = db.Page_CategoryPost.Find(id);
            if (page_CategoryPost == null)
            {
                return HttpNotFound();
            }
            return View(page_CategoryPost);
        }

        // POST: View_Page_CategoryPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Page_CategoryPost page_CategoryPost = db.Page_CategoryPost.Find(id);
            db.Page_CategoryPost.Remove(page_CategoryPost);
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
