using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewGallery.Models;

namespace NewGallery.Controllers
{
    public class Paints1Controller : Controller
    {
        private MyDB db = new MyDB();

        // GET: Paints1
        public ActionResult Index()
        {
            var paints = db.Paints.Include(p => p.Artist);
            return View(paints.ToList());
        }

        // GET: Paints1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            return View(paint);
        }

        // GET: Paints1/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName");
            return View();
        }

        // POST: Paints1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaintID,Paintname,CreateDate,Size,Price,Type,ArtistID,artistname,ImgUrl")] Paint paint)
        {
            if (ModelState.IsValid)
            {
                db.Paints.Add(paint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", paint.ArtistID);
            return View(paint);
        }

        // GET: Paints1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", paint.ArtistID);
            return View(paint);
        }

        // POST: Paints1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaintID,Paintname,CreateDate,Size,Price,Type,ArtistID,artistname,ImgUrl")] Paint paint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", paint.ArtistID);
            return View(paint);
        }

        // GET: Paints1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            return View(paint);
        }

        // POST: Paints1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paint paint = db.Paints.Find(id);
            db.Paints.Remove(paint);
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
