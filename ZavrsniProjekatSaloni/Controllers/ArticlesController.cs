using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ZavrsniProjekatSaloni.Models;

namespace ZavrsniProjekatSaloni.Controllers
{
    public class ArticlesController : Controller
    {
        #region Database Context
        /// <summary>
        /// Deklarisanje objekta kontekstne klase baze podataka.
        /// </summary>
        readonly SaloniNamestajaEntities db;
        #endregion

        #region Constructors
        /// <summary>
        /// Konstruktor bez parametara koji instancira objekat kontekstne klase baza podataka.
        /// </summary>
        public ArticlesController()
        {
            db = new SaloniNamestajaEntities();
        }
        #endregion

        #region HttpGet Requests
        /// <summary>
        /// Metoda koja vraca pogled sa listom svih artikala.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetArticles()
        {
            var articles = db.Articles.ToList();
            return View(articles);
        }
        /// <summary>
        /// Metoda koja vraca pogled sa detaljima selektovanog artikla.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ArticleDetails(int? id)
        {

            Article article = db.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            if (article == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(article);
        }
        /// <summary>
        /// Metoda koja vraca pogled sa formom za kreiranje novih artikala.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateArticle()
        {
            ViewBag.SalonId = new SelectList(db.Salons, "SalonId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        /// <summary>
        /// Metoda koja vraca pogled za izmenu artikla sa ispunjenom formom sa podacima artikla.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditArticle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalonId = new SelectList(db.Salons, "SalonId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(article);
        }
        /// <summary>
        /// Metoda koja vraca pogled za brisanje selektovanog artikla.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteArticle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalonId = new SelectList(db.Salons, "SalonId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(article);
        }
        #endregion

        #region HttpPost Requests
        /// <summary>
        /// Metoda koja dodaje novi artikal u bazu podataka i redirektuje na prikaz liste artikala.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle([Bind(Include = "ArticleId,Code,ArticleName,ProductionCountry,ProductionYear,Price,StockQuantity,SalonId,CategoryId,Image")] Article article)
        {
            
            if (ModelState.IsValid) {
                
                article.Image = new byte[article.Image.Length];

                
                    db.Articles.Add(article);
                    db.SaveChanges();
                    return RedirectToAction("GetArticles");

            }
            
            ViewBag.SalonId = new SelectList(db.Salons, "SalonId", "Name",article.SalonId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }
        /// <summary>
        /// Metoda koja nakon izmene parametara artikla menja stanje entiteta u "Modified" i cuva promene u bazi podataka.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle([Bind(Include = "ArticleId,Code,ArticleName,ProductionCountry,ProductionYear,Price,StockQuantity,SalonId,CategoryId,Image")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetArticles");
            }

            ViewBag.SalonId = new SelectList(db.Salons, "SalonId", "Name", article.SalonId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }
        /// <summary>
        /// Metoda koja brise selektovani artikal iz baze podataka i redirektuje nas na pogled sa listom artikala.
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteArticle")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteArticleConfirmed(int id)
        {
            Article article = db.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("GetArticles");
        }
        #endregion
        //[HttpGet]
        //public JsonResult GetSavedImage()
        //{
        //    var article = GetSavedImage();
        //    article.
        //}
        //public byte[] GetImage(string sBase64string)
        //{
        //    byte[] bytes = null;
        //    if (!string.IsNullOrEmpty(sBase64string))
        //    {
        //        bytes = Convert.FromBase64String(sBase64string);
        //    }
        //    return bytes;
        //}
    }
}