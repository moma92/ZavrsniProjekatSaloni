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
    public class CategoriesController : Controller
    {
        #region Database Context
        /// <summary>
        /// Deklarisanje objekta kontekstne klase baze podataka.
        /// </summary>
        private SaloniNamestajaEntities db;
        #endregion

        #region Constructors
        /// <summary>
        /// Konstruktor bez parametara koji instancira objekat kontekstne klase baze podataka.
        /// </summary>
        public CategoriesController()
        {
            db = new SaloniNamestajaEntities();
        }
        #endregion

        #region HttpGet Requests
        /// <summary>
        /// Metoda koja vraca pogled sa listom svih kategorija namestaja.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCategories()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }
        /// <summary>
        /// Metoda koja vraca pogled sa detaljima selektovane kategorije.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CategoryDetails(int? id)
        {

            Category category = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(category);
        }
        /// <summary>
        /// Metoda koja vraca pogled sa formom za kreiranje novih kategorija.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateCategory()
        {
            
            return View();
        }
        /// <summary>
        /// Metoda koja vraca pogled za izmenu kategorije sa ispunjenom formom sa podacima kategorije.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        /// <summary>
        /// Metoda koja vraca pogled za brisanje selektovane kategorije.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        #endregion

        #region HttpPost Requests
        /// <summary>
        /// Metoda koja dodaje novu kategoriju u bazu podataka i redirektuje na prikaz liste kategorija.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory([Bind(Include = "CategoryId,CategoryName,CategoryDescription")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("GetCategories");
            }
            return View(category);
        }
        /// <summary>
        /// Metoda koja nakon izmene parametara kategorije menja stanje entiteta u "Modified" i cuva promene u bazi podataka.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "CategoryId,CategoryName,CategoryDescription")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetCategories");
            }
            return View(category);
        }
        /// <summary>
        /// Metoda koja brise selektovanu kategoriju iz baze podataka i redirektuje nas na pogled sa listom kategorija.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            Category category = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("GetCategories");
        }
        #endregion
    }
}