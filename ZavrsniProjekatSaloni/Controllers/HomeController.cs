using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavrsniProjekatSaloni.Models;
using ZavrsniProjekatSaloni.DTOs;


namespace ZavrsniProjekatSaloni.Controllers
{
    public class HomeController : Controller
    {
        public SaloniNamestajaEntities db = new SaloniNamestajaEntities();

        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser([Bind(Include = "UserId,UserName,Password,Name,LastName,RoleId,Address,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                Session["UserIdSS"] = user.UserId.ToString();
                Session["UserNameSS"] = user.UserName.ToString();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Login()
        //{
           
        //    var checkLogin = db.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
        //    if (checkLogin != null)
        //    {

        //        Session["UserNameSS"] = username;
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ViewBag.Notification = "Pogresno korisnicko ime ili lozinka";
        //        return View();
        //    }


        //}

        public ActionResult SingUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SingUp(User user)
        {
            
            if (db.Users.Any(x => x.UserName == user.UserName))
            {
                Session["UserNameSS"] = user.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View();
            }

        }
    }
}