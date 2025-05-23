using SE_TRENDZZ.Data;
using SE_TRENDZZ.Helpers;
using SE_TRENDZZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE_TRENDZZ.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: /Account/Signup
        public ActionResult Signup()
        {
            ViewBag.Roles = new SelectList(new[]
            {
        new { ID = 1, Name = "Teacher" },
        new { ID = 2, Name = "Student" }
        //, new { ID = 3, Name = "Admin" }

    }, "ID", "Name");

            return View();
        }


        // POST: /Account/Signup
        [HttpPost]
        public ActionResult Signup(User model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SETrendzzDBContext())
                {
                    model.PasswordHash = PasswordHelper.HashPassword(model.PasswordHash); // hash before save
                    db.Users.Add(model);
                    db.SaveChanges();
                }

                return RedirectToAction("Login");
            }



            ViewBag.Roles = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Teacher", Value = "1" },
                new SelectListItem { Text = "Student", Value = "2" }
                //, new { ID = 3, Name = "Admin" }
            }, "Value", "Text");

            return View(model);
        }


        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            using (var db = new SETrendzzDBContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash))
                {
                    Session["UserID"] = user.UserID;
                    Session["FullName"] = user.FullName;
                    Session["RoleID"] = user.RoleID;

                    // Role-based redirection
                    if (user.RoleID == 1)
                        return RedirectToAction("Dashboard", "Teacher");
                    else if (user.RoleID == 2)
                        return RedirectToAction("Dashboard", "Student");
                    else if (user.RoleID == 3)
                        return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    ViewBag.Error = "Invalid email or password.";
                }

                ViewBag.Email = email;
                return View();

            }
        }

    }
}