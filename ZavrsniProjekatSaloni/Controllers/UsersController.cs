using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZavrsniProjekatSaloni.Models;

namespace ZavrsniProjekatSaloni.Controllers
{
    public class UsersController : Controller
    {
        #region Database Context
        /// <summary>
        /// Deklarisanje objekta kontekstne klase baze podataka.
        /// </summary>
        private SaloniNamestajaEntities db;
        #endregion

        #region Constructors
        /// <summary>
        /// Konstruktor bez parametara koji instancira objekat kontekstne klase baza podataka.
        /// </summary>
        public UsersController()
        {
            db = new SaloniNamestajaEntities(); 
        }
        #endregion

        #region HttpGet Requests
        /// <summary>
        /// Metoda koja vraca pogled sa listom svih korisnika.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUsers()
        {
            var users = db.Users.ToList();
            return View(users);
        }
        /// <summary>
        /// Metoda koja vraca pogled sa detaljima selektovanog korisnika.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserDetails(int? id)
        {
           
            User user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(user);    
        }
        /// <summary>
        /// Metoda koja vraca pogled sa formom za kreiranje novih korisnika.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateUser()
        {
            ViewBag.RoleId = new SelectList(db.Roles,"RoleId","RoleName");
            return View();
        }
        /// <summary>
        /// Metoda koja vraca pogled za izmenu korisnika sa ispunjenom formom korisnikovih podataka.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles,"RoleId","RoleName",user.RoleId);
            return View(user);
        }
        /// <summary>
        /// Metoda koja vraca pogled za brisanje selektovanog korisnika.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }
        #endregion

        #region HttpPost Requests
        /// <summary>
        /// Metoda koja dodaje novog korisnika u bazu podataka i redirektuje na prikaz liste korisnika.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include="UserId,UserName,Password,Name,LastName,RoleId,Address,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("GetUsers");
            }

            ViewBag.RoleId = new SelectList(db.Roles,"RoleId","RoleName", user.RoleId);
            return View(user);
        }
        /// <summary>
        /// Metoda koja nakon izmene korisnickih parametara menja stanje entiteta u "Modified" i cuva promene u bazi podataka.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "UserId,UserName,Password,Name,LastName,RoleId,Address,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetUsers");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }
        /// <summary>
        /// Metoda koja brise selektovanog korisnika iz baze podataka redirektuje nas na pogled sa listom korisnika
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost,ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(int id)
        {
            User user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("GetUsers");
        }
        #endregion
       
    }
}