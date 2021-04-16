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
    public class RolesController : Controller
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
        public RolesController()
        { 
            db = new SaloniNamestajaEntities();
        }
        #endregion

        #region HttpGet Requests
        /// <summary>
        /// Metoda koja vraca pogled sa listom svih uloga.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRoles()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }
        /// <summary>
        /// Metoda koja vraca pogled sa formom za kreiranje novih uloga.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        /// <summary>
        /// Metoda koja vraca pogled za izmenu uloge sa ispunjenom formom imena uloge.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }
        /// <summary>
        /// Metoda koja vraca pogled za brisanje selektovane role.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }
        #endregion

        #region HttpPost Requests
        /// <summary>
        /// Metoda koja dodaje novu ulogu u bazu podataka i redirektuje na prikaz liste uloga.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole([Bind(Include = "RoleId, RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("GetRoles");
            }
            return View(role);
        }
        /// <summary>
        /// Metoda koja nakon izmene imena role menja stanje entiteta u "Modified" i cuva promene u bazi podataka.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole([Bind(Include = "RoleId, RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetRoles");
            }
            return View(role);
        }
        /// <summary>
        /// Metoda koja brise selektovanu ulogu iz baze podataka i redirektuje nas na pogled sa listom uloga.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleConfirmed(int id)
        {
            Role role = db.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("GetRoles");
        }
        #endregion
    }
}