using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewGallery.Models;
//somenote
namespace NewGallery.Controllers
{
    public class ArtistsController : Controller
    {   
        //
        public static string artist_name;
        public static string fave_style;
        public static int rate_s;
        public static int id_details_s;
        //

        private MyDB db = new MyDB();

        // GET: Artists
        public ActionResult Index()
        {
            string user = (string)HttpContext.Session["Type"];

            if (user != "Admin")
            {
                return RedirectToAction("IndexUserMode", "Artists");
            }
            
            return View(db.Artists.ToList());
        }
        public PartialViewResult All()
        {
            List<Artist> artistlist = db.Artists.ToList();
            return PartialView("PartialView", artistlist);
        }
        public PartialViewResult Top3ArtistRate()
        {
            List<Artist> artistlist = db.Artists.OrderByDescending(a=>a.Rate).Take(3).ToList();
            return PartialView("PartialView", artistlist);

        }
        public PartialViewResult Buttom3ArtistRate()
        {
            List<Artist> artistlist = db.Artists.OrderBy(a => a.Rate).Take(3).ToList();
            return PartialView("PartialView", artistlist);
        }
        // GET: Artists

        public ActionResult IndexUserMode()
        {
            return View(db.Artists.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                id_details_s = 0;
            }
            else 
            {
                id_details_s = (int)id ; 
            }


            string user = (string)HttpContext.Session["Type"];
            if (user != "Admin")
            {
                return RedirectToAction("DetailsUserMode", "Artists");
            }
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Include(a => a.Paints).First(a => a.ArtistID == id);

            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        
        public ActionResult DetailsUserMode()
        {

            /*
            var theChosenArtist = db.Paints.First(paint2 => paint2.PaintID == id_s).artistname;

            string catching = statisticsOnCustomer(theChosenArtist.ToString());
            if (catching != null)
            {
                ViewData["chosens"] = catching;
            }
            */
            if (id_details_s == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Include(a => a.Paints).First(a => a.ArtistID == id_details_s);

            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

       
        // GET: Artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistID,ArtistName,Email,FavoriteStyle,Rate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistID,ArtistName,Email,FavoriteStyle,Rate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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

        public ActionResult Search(string name, string favstyle, int? rate)
        {
            //
            artist_name = name;
            fave_style = favstyle;
            if (rate == null)
            {
                rate_s = 0;
            }
            else { rate_s = (int)rate; }
            //

            List<Artist> Artistlist = new List<Artist>();
            string user = (string)HttpContext.Session["Type"];
            if (user != "Admin")
            {
                return RedirectToAction("SearchUserMode","Artists");
            }
            

            if ((name == null || name == "") && (favstyle == null || favstyle == "")&& rate==null)
            {
                return RedirectToAction("Index");
            }


            if ((favstyle == null || favstyle == "")&&rate==null)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.ArtistName.ToLower().Contains(name.ToLower()))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("Index",Artistlist) ;
            }
            else if ((name == null || name == "")&& rate==null)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.ToLower().Contains(favstyle.ToLower()))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("Index", Artistlist);
            }
            else if ((name == null || name == "") && (favstyle == null || favstyle == ""))
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.Rate>=rate)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("Index", Artistlist);
            }
            else if (name == null || name == "")
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.Rate >= rate && a.FavoriteStyle.ToLower().Contains(favstyle.ToLower()))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("Index", Artistlist);
            }

            else if (favstyle == null || favstyle == "")
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.ArtistName.ToLower().Contains(name.ToLower())&& a.Rate >= rate)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("Index", Artistlist);
            }
            else if (rate==null)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.ToLower().Contains(favstyle)&& a.ArtistName.ToLower().Contains(name.ToLower()))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("Index", Artistlist);
            }
            else
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.ToLower().Contains(favstyle.ToLower()) && a.ArtistName.ToLower().Contains(name.ToLower()) &&a.Rate>=rate)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("Index", Artistlist);
            }

        }


        public ActionResult SearchUserMode()
        {
            List<Artist> Artistlist = new List<Artist>();

            if ((artist_name == null || artist_name == "") && (fave_style == null || fave_style == "") && rate_s == 0)
            {
                return RedirectToAction("IndexUserMode");
            }


            if ((fave_style == null || fave_style == "") && rate_s == 0)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.ArtistName.ToLower().Contains(artist_name.ToLower()))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("IndexUserMode", Artistlist);
            }
            else if ((artist_name == null || artist_name == "") && rate_s == 0)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.ToLower().Contains(fave_style.ToLower()))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("IndexUserMode", Artistlist);
            }
            else if ((artist_name == null || artist_name == "") && (fave_style == null || fave_style == ""))
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.Rate >= rate_s)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("IndexUserMode", Artistlist);
            }
            else if (artist_name == null || artist_name == "")
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.Rate >= rate_s && a.FavoriteStyle.ToLower().Contains(fave_style.ToLower()))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("IndexUserMode", Artistlist);
            }

            else if (fave_style == null || fave_style == "")
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.ArtistName.ToLower().Contains(artist_name.ToLower()) && a.Rate >= rate_s)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("IndexUserMode", Artistlist);
            }
            else if (rate_s == 0)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.ToLower().Contains(fave_style.ToLower()) && a.ArtistName.ToLower().Contains(artist_name.ToLower()))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("IndexUserMode", Artistlist);
            }
            else
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.ToLower().Contains(fave_style.ToLower()) && a.ArtistName.ToLower().Contains(artist_name.ToLower()) && a.Rate >= rate_s)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View("IndexUserMode", Artistlist);
            }

        }



    }
}

