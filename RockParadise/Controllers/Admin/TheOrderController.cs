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
    public class TheOrderController : Controller
    {
        private rock_paradiseEntities3 db = new rock_paradiseEntities3();

        // GET: TheOrder
        [CheckUserRole]
        public ActionResult Index()
        {
            var user_order = db.user_order.Include(u => u.account);
            var sortedOrders = db.user_order
                    .OrderByDescending(o => o.status == null)
                    .ThenBy(o => o.status == "Agree" ? 0 : 1)
                    .ThenBy(o => o.status == "Deliver" ? 0 : 1)
                    .ThenBy(o => o.status == "Complete" ? 0 : 1)
                    .ThenBy(o => o.status == "Reject" ? 0 : 1)
                    .ToList();
         
            return View(sortedOrders.ToList());
        }

        public JsonResult ChangeStatus(int idOrder, string stt)
        {
            

            user_order user_order = db.user_order.Find(idOrder);
            user_order.status = stt.Trim();
            db.Entry(user_order).State = EntityState.Modified;
            db.SaveChanges();

            //return Json(new { success = false, message = "Not logged in yet" });

            return Json(new { success = true });
            

        }

        // GET: TheOrder/Details/5
        [CheckUserRole]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_order user_order = db.user_order.Find(id);
            if (user_order == null)
            {
                return HttpNotFound();
            }
            return View(user_order);
        }

    


        // GET: TheOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_order user_order = db.user_order.Find(id);
            if (user_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_user = new SelectList(db.accounts, "id", "username", user_order.id_user);
            return View(user_order);
        }

        // POST: TheOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_user,order_time,status,OrderItemsJSON,total_price")] user_order user_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_user = new SelectList(db.accounts, "id", "username", user_order.id_user);
            return View(user_order);
        }

        // GET: TheOrder/Delete/5
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_order user_order = db.user_order.Find(id);
            db.user_order.Remove(user_order);
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
