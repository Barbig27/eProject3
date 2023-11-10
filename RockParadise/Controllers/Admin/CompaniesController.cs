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
    public class CompaniesController : Controller
    {
        private rock_paradiseEntities3 db = new rock_paradiseEntities3();

        // GET: Companies
        [CheckUserRole]
        public ActionResult Index()
        {
            return View(db.companies.ToList());
        }

        // GET: Companies/Details/5
        [CheckUserRole]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company company = db.companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        [CheckUserRole]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckUserRole]
        public ActionResult Create([Bind(Include = "id,name")] company company)
        {
            if (ModelState.IsValid)
            {
                company.created_at = DateTime.Now;
                db.companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        [CheckUserRole]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company company = db.companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CheckUserRole]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] company company)
        {
            if (ModelState.IsValid)
            {
                company.created_at = DateTime.Now;
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        [CheckUserRole]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company company = db.companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [CheckUserRole]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            company company = db.companies.Find(id);
            db.companies.Remove(company);
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
