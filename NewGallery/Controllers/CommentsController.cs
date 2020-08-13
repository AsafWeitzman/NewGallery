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
    public class CommentsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Comments
        public ActionResult Index()
        {
            
            string user = (string)HttpContext.Session["Type"];
            if (user != "Admin")
            {
                return RedirectToAction("IndexUserMode", "Comments");
            }
            
            return View(db.Comments.ToList());
        }

        public ActionResult IndexUserMode()
        {
            return View(db.Comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            string user = (string)HttpContext.Session["Type"];
            if (user != "Admin")
            {
                return RedirectToAction("CreateUserModeGet", "Comments");
            }

            ViewBag.PaintID = new SelectList(db.Paints.ToList(), "PaintID", "Paintname");
            return View();
        }

        public ActionResult CreateUserModeGet()
        {

            ViewBag.PaintID = new SelectList(db.Paints.ToList(), "PaintID", "Paintname");
            return View();
        }




        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Body,SentBy,Posted")] Comment comment, int PaintID)
        {
            /*
            string user = (string)HttpContext.Session["Type"];
            if (user != "Admin")
            {
                return RedirectToAction("CreateUserMode", "Comments");

            }
            */
            comment.Posted = DateTime.Now;
            comment.Paint = db.Paints.First(p => p.PaintID == PaintID);
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaintID = new SelectList(db.Paints, "PaintID", "Paintname");
            return View(comment);
        }


        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserMode([Bind(Include = "ID,Title,Body,SentBy,Posted")] Comment comment, int PaintID)
        {

            comment.Posted = DateTime.Now;
            comment.Paint = db.Paints.First(p => p.PaintID == PaintID);
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaintID = new SelectList(db.Paints, "PaintID", "Paintname");
            return View(comment);
        }


        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Body,SentBy,Posted")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
