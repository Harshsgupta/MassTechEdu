using MassTechEdu.Data;
using MassTechEdu.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MassTechEdu.Controllers
{
    public class AuthController : Controller
    {
        private readonly MasstechEduContext db;
        public AuthController(MasstechEduContext db)
        {
            this.db = db;
        }

        //Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            //Method 1

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and Password are Required.";
                return View();
            }
            var user = await db.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                if (user.IsBlocked)
                {
                    ViewBag.Error = "Your Account is Blocked by the Admin.";
                    return View();
                }

               
               

                // Set user session
                HttpContext.Session.SetInt32("UserID", user.UserId);
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Urole", user.Role);



                if (user.Role == "User")
                {
                    return RedirectToAction("Dashboard", "User");
                }

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid Email or Password.";
            }
            return View();


            //Method 2

            //var data = db.Users.Where(x => x.Email.Equals(log.Email)).SingleOrDefault();
            //if (data != null)
            //{
            //    bool us = data.Email.Equals(log.Email) && data.Password.Equals(log.Password);
            //    if (us)
            //    {
            //        //step 3 passing cookies
            //        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, log.Username) }, CookieAuthenticationDefaults.AuthenticationScheme);
            //        var principal = new ClaimsPrincipal(identity);
            //        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            //        HttpContext.Session.SetString("Username", log.Username);

            //        if(data.Role=="User")
            //        {
            //            return RedirectToAction("User", "Dashboard");
            //        }
            //        if(data.Role=="Admin")
            //        {
            //            return RedirectToAction("Admin", "Dashboard");
            //        }

            //    }
            //    else
            //    {
            //        TempData["Pass"] = "Invalid Password";
            //    }
            //}
            //else
            //{
            //    TempData["us"] = "Email invalid";
            //}
            //return View();
        }

        //Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User u)
        {
            //Method 1

            //if (string.IsNullOrEmpty(model.Role))
            //{
            //    model.Role = "User";  // Default role
            //}

            //db.Users.Add(model);
            //db.SaveChanges();

            //HttpContext.Session.SetString("Username", model.Username);
            //return RedirectToAction("Login");


            //Method 2

            u.Password = u.Password;
            u.Role = "User";
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("Login");



            //return View(model);

        }

        public IActionResult Logout()
        {
            //HttpContext.SignOutAsync();
            //HttpContext.Session.Clear();
            //return RedirectToAction("Login");

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedcookies = Request.Cookies.Keys;
            foreach (var cookie in storedcookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Login");
        }
    }
}
