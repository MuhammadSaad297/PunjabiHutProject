using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PunjabiHutProject.Models;

namespace PunjabiHutProject.Controllers
{
    public class ExtrasController : Controller
    {
        private Model1 db = new Model1();

        // GET: Extras
        public ActionResult Index()
        {
            var extras = db.Extras.Include(e => e.Product);
            return View(extras.ToList());
        }

        // GET: Extras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extra extra = db.Extras.Find(id);
            if (extra == null)
            {
                return HttpNotFound();
            }
            return View(extra);
        }

        // GET: Extras/Create
        public ActionResult Create()
        {
            ViewBag.PRODUCT_FID = new SelectList(db.Products, "PRODUCT_ID", "PRODUCT_NAME");
            return View();
        }

        // POST: Extras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EXTRA_ID,EXTRA_NAME,EXTRA_SALEPRICE,EXTRA_PURCHASEPRICE,PRODUCT_FID")] Extra extra)
        {
            if (ModelState.IsValid)
            {
                db.Extras.Add(extra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PRODUCT_FID = new SelectList(db.Products, "PRODUCT_ID", "PRODUCT_NAME", extra.PRODUCT_FID);
            return View(extra);
        }

        // GET: Extras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extra extra = db.Extras.Find(id);
            if (extra == null)
            {
                return HttpNotFound();
            }
            ViewBag.PRODUCT_FID = new SelectList(db.Products, "PRODUCT_ID", "PRODUCT_NAME", extra.PRODUCT_FID);
            return View(extra);
        }

        // POST: Extras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EXTRA_ID,EXTRA_NAME,EXTRA_SALEPRICE,EXTRA_PURCHASEPRICE,PRODUCT_FID")] Extra extra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PRODUCT_FID = new SelectList(db.Products, "PRODUCT_ID", "PRODUCT_NAME", extra.PRODUCT_FID);
            return View(extra);
        }

        // GET: Extras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Extra extra = db.Extras.Find(id);
            if (extra == null)
            {
                return HttpNotFound();
            }
            return View(extra);
        }

        // POST: Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Extra extra = db.Extras.Find(id);
            db.Extras.Remove(extra);
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
