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
        #region Database Context
        public SaloniNamestajaEntities db = new SaloniNamestajaEntities();
        #endregion

        #region Http Get Requests
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

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region HttpPost Requests
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser([Bind(Include = "UserId,UserName,Password,Name,LastName,RoleId,Address,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                if (user.RoleId == 2)
                {
                    Session["RoleSS"] = "admin";
                    Session["UserNameSS"] = user.UserName;
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["RoleSS"] = "kupac";
                    Session["UserNameSS"] = user.UserName;
                    return RedirectToAction("Index");
                }
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);

        }
       

        

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult LoginConfirm(UserDTO userDto)
        {
            

            var checkLogin = db.Users.Where(x => x.UserName == userDto.UserName && x.Password == userDto.Password && x.RoleId == 2).FirstOrDefault();
            
            if (checkLogin != null)
            {
                var role = "admin";
                Session["UserNameSS"] = userDto.UserName;
                Session["RoleSS"] = role;
                return RedirectToAction("Index");
            }
            else if(checkLogin == null)
            {
                var checkLogin2 = db.Users.Where(x => x.UserName == userDto.UserName && x.Password == userDto.Password && x.RoleId == 3).FirstOrDefault();

                if (checkLogin2 != null)
                {
                    var role = "kupac";
                    Session["UserNameSS"] = userDto.UserName;
                    Session["RoleSS"] = role;
                    return RedirectToAction("Index");
                }

            }
            ViewBag.Notification = "Uneli ste pogresno korisnicko ime ili lozinku";
            return View();


           


        }

        #endregion
    }
}