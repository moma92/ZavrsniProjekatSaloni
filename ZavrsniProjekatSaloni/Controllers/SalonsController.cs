using System;
using System.Collections.Generic;
using System.Linq;
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
        private SaloniNamestajaEntities db;
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
        #endregion
    }
}