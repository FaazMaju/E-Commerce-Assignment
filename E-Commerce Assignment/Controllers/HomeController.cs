using E_Commerce_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly Ecom_web_engEntities db = new Ecom_web_engEntities();
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View(db.products.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Admin()
        {
            
            if (Session["UserID"] != null || (string)Session["UserRole"] != "1")
            {
                return View(db.users);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login( user objUser)
        {
            if (ModelState.IsValid)
            {
                using (Ecom_web_engEntities db = new Ecom_web_engEntities())
                {
                    var obj = db.users.Where(a => a.user_email.Equals(objUser.user_email) && a.user_password.Equals(objUser.user_password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.user_id.ToString();
                        Session["UserName"] = obj.user_name.ToString();
                        Session["UserRole"] = obj.role.ToString();

                        if ((string)Session["UserRole"] == "1")
                        {
                            return RedirectToAction("Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}