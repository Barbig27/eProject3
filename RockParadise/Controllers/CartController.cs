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
    public class CartController : Controller
    {
        private rock_paradiseEntities3 db = new rock_paradiseEntities3();

        // GET: Cart
        [CheckLogin]
        public ActionResult Index()
        {
            var carts = db.carts.Include(c => c.account).Include(c => c.product);
            return View(carts.ToList().Where(p => p.id_user == (int)Session["UserID"]));
        }

        // GET: Cart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        

        public JsonResult AddToCart(int productId, int quantity)
        {
            var product = db.products.FirstOrDefault(p => p.id == productId);
            int? userId = (int?)Session["UserID"];
            if (userId == null)
            {
                return Json(new { success = false, message = "Not logged in yet" });
            }
            else
            {
                var cartItem = new cart
                {
                    id_user = userId,
                    id_product = productId,
                    number_of = quantity,
                    created_at = DateTime.Now
                };
                db.carts.Add(cartItem);
                db.SaveChanges();
                return Json(new { success = true });
            }

        }


        [HttpPost]
        public JsonResult BuyProduct(string products, string totalPrice)
        {
            account account = db.accounts.Find((int)Session["UserID"]);
            if (products == "[]")
            {
                return Json(new { success = false, message = "Order is empty!" });

            }
            if (account.address == null || account.email == null || account.fullname == null)
            {
                return Json(new { success = false, message = "You have not entered all the information!" });
            }
            else
            {
                user_order user_order = new user_order();
                user_order.id_user = (int)Session["UserID"];
                user_order.order_time = DateTime.Now;
                user_order.status = null;
                user_order.total_price = float.Parse(totalPrice);
                user_order.OrderItemsJSON = products;
                db.user_order.Add(user_order);
                db.SaveChanges();

                return Json(new { success = true });

            }

        }


        [CheckLogin]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_user = new SelectList(db.accounts, "id", "username", cart.id_user);
            ViewBag.id_product = new SelectList(db.products, "id", "name", cart.id_product);
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_user,id_product,number_of,created_at")] cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_user = new SelectList(db.accounts, "id", "username", cart.id_user);
            ViewBag.id_product = new SelectList(db.products, "id", "name", cart.id_product);
            return View(cart);
        }



        public JsonResult DeleteCart(int id)
        {
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return Json(new { success = false, message = "Id not found" });

            }
            else
            {
                db.carts.Remove(cart);
                db.SaveChanges();
                return Json(new { success = true });

            }


        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]


        public ActionResult DeleteConfirmed(int id)
        {
            cart cart = db.carts.Find(id);
            db.carts.Remove(cart);
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
