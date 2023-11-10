using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RockParadise.Models;

namespace RockParadise.Controllers.Admin
{
    public class AccountManagementController : Controller
    {
        private rock_paradiseEntities3 db = new rock_paradiseEntities3();

        // GET: AccountManagement
        [CheckUserRole]
        public ActionResult Index()
        {
            return View(db.accounts.ToList());
        }

        // GET: AccountManagement/Details/5
        [CheckUserRole]
        public ActionResult Details(int? id)
        {
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

        // GET: AccountManagement/Create

        [CheckUserRole]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [CheckUserRole]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password")] account account)
        {
            if (ModelState.IsValid)
            {
                account.role = "member";
                account.created_at = DateTime.Now;
                db.accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: AccountManagement/Edit/5

        [CheckUserRole]
        public ActionResult Edit(int? id)
        {
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

        // POST: AccountManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CheckUserRole]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,role,created_at,fullname,email,address")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: AccountManagement/Delete/5
        [CheckUserRole]
        public ActionResult Delete(int? id)
        {
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

        // POST: AccountManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CheckUserRole]
        public ActionResult DeleteConfirmed(int id)
        {
            account account = db.accounts.Find(id);
            db.accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
