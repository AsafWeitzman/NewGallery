using NewGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewGallery.Controllers
{
    public class ArtistandPaintsController : Controller
    {

        private MyDB db = new MyDB();

        // GET: ArtistandPaints
        public ActionResult Index()
        {
            List<Artist> Artistlist = db.Artists.ToList();
            List<Paint> Paintlist = db.Paints.ToList();

            var Joinlist = from a in Artistlist
                           join p in Paintlist on a.ArtistID equals p.ArtistID
                           select new ArtistsandPaints { TheArtist = a, ThePaint = p };

            return View(Joinlist);
        }
    }
}