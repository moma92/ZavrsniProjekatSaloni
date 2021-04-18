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
    public class SalonsController : Controller
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
        public SalonsController()
        {
            db = new SaloniNamestajaEntities();
        }
        #endregion

        #region HttpGet Requests
        /// <summary>
        /// Metoda koja vraca pogled sa listom svih salona.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSalons()
        {
            var salons = db.Salons.ToList();
            return View(salons);
        }
        /// <summary>
        /// Metoda koja vraca pogled sa detaljima selektovanog salona.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SalonDetails(int? id)
        {

            Salon salon = db.Salons.Where(x => x.SalonId == id).FirstOrDefault();
            if (salon == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(salon);
        }
        /// <summary>
        /// Metoda koja vraca pogled sa formom za kreiranje novih salona.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateSalon()
        {
            return View();
        }
        /// <summary>
        /// Metoda koja vraca pogled za izmenu salona sa ispunjenom formom salonovih podataka.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditSalon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salons.Where(x => x.SalonId == id).FirstOrDefault();
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }
        /// <summary>
        /// Metoda koja vraca pogled za brisanje selektovanog salona.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteSalon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salons.Where(x => x.SalonId == id).FirstOrDefault();
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }
        #endregion

        #region HttpPost Requests
        /// <summary>
        /// Metoda koja dodaje novi salon u bazu podataka i redirektuje na prikaz liste salona.
        /// </summary>
        /// <param name="salon"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSalon([Bind(Include = "SalonId,Name,Owner,Address,PhoneNumber,Email,WebPage,TIN,BankAccountNumber")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                db.Salons.Add(salon);
                db.SaveChanges();
                return RedirectToAction("GetSalons");
            }
            return View(salon);
        }
        /// <summary>
        /// Metoda koja nakon izmene  parametara salona menja stanje entiteta u "Modified" i cuva promene u bazi podataka.
        /// </summary>
        /// <param name="salon"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSalon([Bind(Include = "SalonId,Name,Owner,Address,PhoneNumber,Email,WebPage,TIN,BankAccountNumber")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetSalons");
            }
            return View(salon);
        }
        /// <summary>
        /// Metoda koja brise selektovani salon iz baze podataka i redirektuje nas na pogled sa listom salona.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteSalon")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSalonConfirmed(int id)
        {
            Salon salon = db.Salons.Where(x => x.SalonId == id).FirstOrDefault();
            db.Salons.Remove(salon);
            db.SaveChanges();
            return RedirectToAction("GetSalons");
        }
        #endregion

        #region Validation Methods
        //public JsonResult IsSalonNameExist(string salonName, int? Id)
        //{
        //    var validateSalonName = db.Salons.FirstOrDefault
        //                        (x => x.Name == salonName && x.SalonId != Id);
        //    if (validateSalonName != null)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult IsSalonAddressExist(string address, int? Id)
        //{
        //    var validateAddress = db.Salons.FirstOrDefault
        //                        (x => x.Address == address && x.SalonId != Id);
        //    if (validateAddress != null)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult IsSalonPhoneExist(int phone, int? Id)
        //{
        //    var validatePhone = db.Salons.FirstOrDefault
        //                        (x => x.PhoneNumber == phone && x.SalonId != Id);
        //    if (validatePhone != null)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult IsSalonEmailExist(string email, int? Id)
        //{
        //    var validateEmail = db.Salons.FirstOrDefault
        //                        (x => x.Email == email && x.SalonId != Id);
        //    if (validateEmail != null)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult IsWebPageExist(string webPage, int? Id)
        //{
        //    var validateWebPage = db.Salons.FirstOrDefault
        //                        (x => x.WebPage == webPage && x.SalonId != Id);
        //    if (validateWebPage != null)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult IsTinExist(int tin, int? Id)
        //{
        //    var validateTin = db.Salons.FirstOrDefault
        //                        (x => x.TIN == tin && x.SalonId != Id);
        //    if (validateTin != null)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult IsBankAccountExist(int account, int? Id)
        //{
        //    var validateAccount = db.Salons.FirstOrDefault
        //                        (x => x.BankAccountNumber == account && x.SalonId != Id);
        //    if (validateAccount != null)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}
        #endregion
    }
}