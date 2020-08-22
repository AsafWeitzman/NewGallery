using NewGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewGallery.Controllers
{
    public class PaintsandCommentsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: PaintsandComments
        public ActionResult Index()
        {
            List<Comment> Commentlist = db.Comments.ToList();
            List<Paint> Paintlist = db.Paints.ToList();

            var Joinlist = from c in Commentlist
                           join p in Paintlist on c.Paint.PaintID equals p.PaintID
                           select new PaintsandComments { TheComment = c, ThePaint = p };

            return View(Joinlist);
            
        }

        public ActionResult IndexUserMode()
        {
            List<Comment> Commentlist = db.Comments.ToList();
            List<Paint> Paintlist = db.Paints.ToList();

            var Joinlist = from c in Commentlist
                           join p in Paintlist on c.Paint.PaintID equals p.PaintID
                           select new PaintsandComments { TheComment = c, ThePaint = p };

            return View(Joinlist);

        }
    }
}