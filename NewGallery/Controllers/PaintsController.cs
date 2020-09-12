using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using NewGallery.Models;

namespace NewGallery.Controllers
{
    public class PaintsController : Controller
    {
        public static string paint_name;
        public static string type_s;
        public static int price_s;
        public static string sortOrder_s;



        public static int id_s;

        private MyDB db = new MyDB();

        // GET: Paints
        public ActionResult Index(string sortOrder)
        {

            sortOrder_s = sortOrder;

            string user = (string)HttpContext.Session["Type"];
            

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price" : "price_desc";
            ViewBag.reviews = db.Artists.ToList();
            var paints = from p in db.Paints
                         select p;

            if (user != "Admin")
            {
                return RedirectToAction("IndexUserMode", "Paints");

            }

            switch (sortOrder)
            {
                case "name_desc":
                    paints = paints.OrderByDescending(p => p.Artist.ArtistName);
                    break;
                case "price_desc":
                    paints = paints.OrderBy(p => p.Price);
                    break;

                default:
                    paints = paints.OrderBy(p => p.Artist.ArtistName);
                    break;
            }
            
            return View(paints.ToList());
        }

        public ActionResult IndexUserMode()
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder_s) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder_s == "Price" ? "Price" : "price_desc";

            var paints = from p in db.Paints
                         select p;

            switch (sortOrder_s)
            {
                case "name_desc":
                    paints = paints.OrderByDescending(p => p.Artist.ArtistName);
                    break;
                case "price_desc":
                    paints = paints.OrderBy(p => p.Price);
                    break;

                default:
                    paints = paints.OrderBy(p => p.Artist.ArtistName);
                    break;
            }

            return View(paints.ToList());
        }

        // GET: Paints/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                id_s = 0;
            }
            else { id_s = (int)id; }

            string user = (string)HttpContext.Session["Type"];
            if (user != "Admin")
            {
                return RedirectToAction("DetailsUserMode", "Paints");
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Paint paint = db.Paints.Include(p => p.Comments).First(p => p.PaintID == id);
           
            if (paint == null)
            {
                return HttpNotFound();
            }

            return View(paint);
        }

        public ActionResult DetailsUserMode()
        {
            var theChosenArtist = db.Paints.First(paint2 => paint2.PaintID == id_s).artistname;
            string catching = statisticsOnCustomer(theChosenArtist.ToString());
            if (catching!=null)
            {
                ViewData["chosens"] = catching;
            }

            if (id_s == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Paint paint = db.Paints.Include(p => p.Comments).First(p => p.PaintID == id_s);

            if (paint == null)
            {
                return HttpNotFound();
            }

            return View(paint);
        }

        // GET: Paints/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName");
            return View();
        }

        // POST: Paints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        

        /*
        public ActionResult Create([Bind(Include = "PaintID,Paintname,CreateDate,Size,Price,Type,ArtistID")] Paint paint, int ArtistID)
        {
            paint.Artist = db.Artists.First(p => p.ArtistID == ArtistID);
            paint.artistname = db.Artists.First(p => p.ArtistID == ArtistID).ArtistName;
            if (ModelState.IsValid)
            {
                db.Paints.Add(paint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", paint.ArtistID);
            return View(paint);
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaintID,Paintname,CreateDate,Size,Price,Type,ArtistID,artistname,ImgUrl")] Paint paint, int ArtistID)
        {
            paint.Artist = db.Artists.First(p => p.ArtistID == ArtistID);
            paint.artistname = db.Artists.First(p => p.ArtistID == ArtistID).ArtistName;
            if (ModelState.IsValid)
            {
                db.Paints.Add(paint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", paint.ArtistID);
            return View(paint);
        }

        // GET: Paints/Edit/5
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

        // POST: Paints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaintID,Paintname,CreateDate,Size,Price,Type,ArtistID")] Paint paint)
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

        // GET: Paints/Delete/5
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

        // POST: Paints/Delete/5
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

        
        public ActionResult Search(string paintname, string type, int? price)
        {

            paint_name = paintname;
            type_s = type;
            if (price == null)
            {
                price_s = 0;
            }
            else { price = (int)price_s; }

            List<Paint> Paintlist = new List<Paint>();

            string user = (string)HttpContext.Session["Type"];
            if (user != "Admin")
            {
                return RedirectToAction("SearchUserMode", "Paints");
            }


            if ((paintname == null || paintname == "") && (type == null || type == "") && price == null)
            {
                return RedirectToAction("Index");
            }


            if ((type == null || type == "") && price == null)
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Paintname.Contains(paintname))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else if ((paintname == null || paintname == "") && price == null)
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Type.Contains(type))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else if ((paintname == null || paintname == "") && (type == null || type == ""))
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Price <= price)
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else if (paintname == null || paintname == "")
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Price <= price && p.Type.Contains(type))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }

            else if (type == null || type == "")
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Paintname.Contains(paintname) && p.Price <= price)
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else if (price == null)
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Type.Contains(type) && p.Paintname.Contains(paintname))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Paintname.Contains(paintname) && p.Type.Contains(type) && p.Price <= price)
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }

        }


        private string statisticsOnCustomer(string paintName) 
        {
            int maximum_search = 5;

            /*this lines added to this function to change the rate after numbers of clicks*/
            int maximum_click = 3;
            var theChosenArtist = db.Paints.First(paint2 => paint2.PaintID == id_s).artistname;
            /*this lines added to this function to change the rate after numbers of clicks*/


            Dictionary<String, int> StatisticsOnPaintType = (Dictionary<string, int>)HttpContext.Session["d_favPaint"];

            //last edit
            Dictionary<String, int> StatisticsOnPaintType2 = (Dictionary<string, int>)HttpContext.Session["d_Rate"];
            if (StatisticsOnPaintType2 == null)
            {
                StatisticsOnPaintType2 = new Dictionary<string, int>();
            }
            if (StatisticsOnPaintType2.ContainsKey(paintName) == true)
            {
                StatisticsOnPaintType2[paintName] += 1;
                HttpContext.Session["d_favPaint"] = StatisticsOnPaintType2;
            }
            else
            {
                StatisticsOnPaintType2.Add(paintName, 1);
                HttpContext.Session["d_favPaint"] = StatisticsOnPaintType2;
            }
            //last edit

            //origin
            if (StatisticsOnPaintType==null)
            {
                StatisticsOnPaintType = new Dictionary<string, int>();
            }
            if (StatisticsOnPaintType.ContainsKey(paintName)==true)
            {
                StatisticsOnPaintType[paintName] += 1;
                HttpContext.Session["d_favPaint"] = StatisticsOnPaintType;
            }
            else
            {
                StatisticsOnPaintType.Add(paintName, 1);
                HttpContext.Session["d_favPaint"] = StatisticsOnPaintType;
            }
            //origin


            //last edit
            if (StatisticsOnPaintType2[paintName] >= maximum_click)
            {
                Artist artist = new Artist();
                artist = db.Artists.First(a => a.ArtistName == theChosenArtist);
                artist.Rate++;
                StatisticsOnPaintType2[paintName] = 0;
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (StatisticsOnPaintType[paintName] >= maximum_search)
            {
                StatisticsOnPaintType[paintName] = 0;
                return paintName;
            }
            //last edit


            /*this lines added to this function to change the rate after numbers of clicks*/
            /*<<<<< ירד לבדיקה
            if (StatisticsOnPaintType[paintName] >= maximum_click )
            {
                Artist artist = new Artist();
                artist = db.Artists.First(a => a.ArtistName == theChosenArtist);
                artist.Rate++;
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            /*this lines added to this function to change the rate after numbers of clicks*/
            /*<<<< ירד לבדיקה 
            if (StatisticsOnPaintType[paintName] >=maximum_search)
            {

                //init
                StatisticsOnPaintType[paintName] = 0;
                //
                return paintName;
            }*/


            return null;
        }


        


        public ActionResult SearchUserMode()
        {
            List<Paint> Paintlist = new List<Paint>();

            if ((paint_name == null || paint_name == "") && (type_s == null || type_s == "") && price_s == 0)
            {
                return RedirectToAction("Index");
            }

            if ((type_s == null || type_s == "") && price_s == 0)
            {

                foreach (Paint p in db.Paints)
                {
                    if (p.Paintname.Contains(paint_name))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View("IndexUserMode", Paintlist);
            }
            else if ((paint_name == null || paint_name == "") && price_s == 0)
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Type.Contains(type_s))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View("IndexUserMode", Paintlist);
            }
            else if ((paint_name == null || paint_name == "") && (type_s == null || type_s == ""))
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Price <= price_s)
                    {
                        Paintlist.Add(p);
                    }
                }
                return View("IndexUserMode", Paintlist);
            }
            else if (paint_name == null || paint_name == "")
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Price <= price_s && p.Type.Contains(type_s))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View("IndexUserMode", Paintlist);
            }

            else if (type_s == null || type_s == "")
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Paintname.Contains(paint_name) && p.Price <= price_s)
                    {
                        Paintlist.Add(p);
                    }
                }
                return View("IndexUserMode", Paintlist);
            }
            else if (price_s == 0)
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Type.Contains(type_s) && p.Paintname.Contains(paint_name))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View("IndexUserMode", Paintlist);
            }
            else
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Paintname.Contains(paint_name) && p.Type.Contains(type_s) && p.Price <= price_s)
                    {
                        Paintlist.Add(p);
                    }
                }

                return View("IndexUserMode", Paintlist);
            }

        }

       

    }
}

