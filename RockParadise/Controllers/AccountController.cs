using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RockParadise.Models;

namespace RockParadise.Controllers
{
    public class AccountController : Controller
    {
        private rock_paradiseEntities3 db = new rock_paradiseEntities3();


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home"); 
        }
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult LoginCheck(string username, string password)
        {
            account user = db.accounts.FirstOrDefault(u => u.username == username && u.password == password);
            if (user != null)
            {
                Session["UserID"] = user.id;
                Session["Username"] = user.username;
                Session["Role"] = user.role;
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Incorrect account or password!" });
            }
        }

        public JsonResult Reg(string username, string password, string confirmpassword)
        {
            account user = db.accounts.FirstOrDefault(u => u.username == username);

            if (confirmpassword != password)
            {
                return Json(new { success = false, message = "Confirmation password is incorrect!" });
            }
            else if (user != null)
            {
                return Json(new { success = false, message = "Account already exists!" });
            }
            else
            {
                var NewAccount = new account
                {
                    username = username,
                    password = password,
                    role = "member",
                    created_at = DateTime.Now

                };
                db.accounts.Add(NewAccount);
                db.SaveChanges();
                return Json(new { success = true });
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [CheckLogin]
        public ActionResult ChangePassword()
        {
            return View();
        }

        public JsonResult UpdatePassword(string password, string newpassword, string cfpassword)
        {
            account account = db.accounts.Find((int)Session["UserID"]);
            if(account.password != password)
            {
                return Json(new { success = false, message = "Incorrect password!" });
            }
            else if (newpassword != cfpassword) 
            {
                return Json(new { success = false, message = "Confirmation password is incorrect" });
            }

            else
            {
                account.password = newpassword;
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }

            

        }



        // GET: Account/Details/5


        // GET: Account/Create

        [CheckLogin]
        public ActionResult Details()
        {
            int? id = (int)Session["UserID"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }



        public JsonResult UpdateInfo(string fullname, string email, string address)
        {


            account account = db.accounts.Find((int)Session["UserID"]);
            account.fullname = fullname;
            account.email = email;
            account.address = address;
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { success = true });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "id,username,password,role,created_at,fullname,email,address")] account account)
        {
            

            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Account");
            }
            return View(account);
        }

        // GET: Account/Delete/5
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
