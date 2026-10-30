using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _25DH110591_MyStore.Models;
using _25DH110591_MyStore.Models.ViewModel;
using System.Web.Helpers;
using System.Web.Security;

namespace _25DH110591_MyStore.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u =>
                    u.Username == model.Username &&
                    u.UserRole == "Customer");

                if (user != null &&
                    Crypto.VerifyHashedPassword(user.Password, model.Password))
                {
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;

                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("",
                    "Tên đăng nhập hoặc mật khẩu không đúng!");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users
                    .FirstOrDefault(u => u.Username == model.Username);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Username",
                        "Tên đăng nhập này đã tồn tại!");

                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    Password = Crypto.HashPassword(model.Password),
                    UserRole = "Customer"
                };

                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    CustomerEmail = model.CustomerEmail,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,
                    User = user
                };

                db.Users.Add(user);
                db.Customers.Add(customer);

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}