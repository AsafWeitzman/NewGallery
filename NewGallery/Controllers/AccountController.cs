using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using NewGallery.Models;


namespace NewGallery.Controllers
{
    public class AccountController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Account
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }
        
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //var user = db.Accounts.FirstOrDefault(u => u.Username == username && u.Password == password);
            Account user = new Account();
            user = db.Accounts.FirstOrDefault(u => u.Username == username && u.Password == password);


            if (user != null)
            {
                //
                if (user.Type.ToString() == "Customer")
                {
                    SignIn(user);
                    return RedirectToAction("IndexUserMode", "Home");

                }
                //
                SignIn(user);
                return RedirectToAction("Index", "Home");

            }

            //return View();
            return RedirectToAction("IndexUserMode", "Home");
        }

        private void SignIn(Account user)
        {
            HttpContext.Session["Type"] = user.Type.ToString();

            HttpContext.Session["d_favPaint"] = new Dictionary<string,int>();

            HttpContext.Session["d_Rate"] = new Dictionary<string, int>();




            /*
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim("FullName",user.Fullname),
                new Claim(ClaimTypes.Role,user.Type.ToString())

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieProtection.Encryption.ToString());
            await HttpContext.
            */
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password, string fname, string lname)
        {
            Account account = new Account() { Username = username, Password = password, Fullname = fname + " " + lname, Type=UserType.Customer };
            //וודויים

            /*to add an admin use this line before register*/
            //account.Type = UserType.Admin;
            /*to add an admin use this line before register*/



            db.Accounts.Add(account);
            db.SaveChanges();

            SignIn(account);

            

            if (account.Type.ToString() == "Customer")
            {
                
                return RedirectToAction("IndexUserMode", "Home");

            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Register()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,Username,Password,Fullname")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Username,Password,Fullname")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
