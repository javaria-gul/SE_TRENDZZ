using SE_TRENDZZ.Data;
using SE_TRENDZZ.Helpers;
using SE_TRENDZZ.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE_TRENDZZ.Controllers
{
    public class AccountController : Controller
    {
       
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signup()
        {
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "Teacher", Value = "1" },
                new SelectListItem { Text = "Student", Value = "2" }
            };

            ViewBag.Roles = new SelectList(roles, "Value", "Text");
            return View();
        }

        
        [HttpPost]
        public ActionResult Signup(User model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SETrendzzDBContext())
                {
                    model.PasswordHash = PasswordHelper.HashPassword(model.PasswordHash); 
                    db.Users.Add(model);
                    db.SaveChanges();
                }

                return RedirectToAction("Login");
            }

            var roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "Teacher", Value = "1" },
                new SelectListItem { Text = "Student", Value = "2" }
            };

            ViewBag.Roles = new SelectList(roles, "Value", "Text");
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
                    Session["Email"] = user.Email;
                    Session["RoleID"] = user.RoleID;


                   Session["Bio"] = string.IsNullOrEmpty(user.Bio) ? "No bio added yet." : user.Bio;
                    Session["ProfilePicture"] = string.IsNullOrEmpty(user.ProfilePicture)
                        ? Url.Content("~/images/default-pfp.png")
                        : user.ProfilePicture;

                    
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
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UploadProfilePicture(HttpPostedFileBase profileImage)
        {
            try
            {
                if (profileImage != null && profileImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(profileImage.FileName);
                    string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                    string fullPath = Server.MapPath("~/Content/UploadedImages/" + uniqueName);

                    profileImage.SaveAs(fullPath);

                    string imagePath = Url.Content("~/Content/UploadedImages/" + uniqueName);

                    int userId = Convert.ToInt32(Session["UserID"]); // Correct key name

                    using (var db = new SETrendzzDBContext())
                    {
                        var user = db.Users.Find(userId);
                        if (user != null)
                        {
                            user.ProfilePicture = imagePath;
                            db.SaveChanges();
                        }
                        else
                        {
                            return Json(new { success = false, message = "User not found." });
                        }
                    }

                    Session["ProfilePicture"] = imagePath;

                    return Json(new { success = true, imageUrl = imagePath });
                }
                else
                {
                    return Json(new { success = false, message = "Please select a valid image." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        [HttpPost]
        public JsonResult UpdateBio(string bio)
        {
            int userId = (int)Session["UserID"]; // Assuming you're storing userId in session
            using (var db = new SETrendzzDBContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    user.Bio = bio;
                    db.SaveChanges();
                    Session["Bio"] = bio;
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult ChangePassword(string password)
        {
            int userId = (int)Session["UserID"];
            using (var db = new SETrendzzDBContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    // Hash password before saving
                    string hashedPassword = PasswordHelper.HashPassword(password);

                    user.PasswordHash = hashedPassword;
                    db.SaveChanges();
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
        




    }
}
