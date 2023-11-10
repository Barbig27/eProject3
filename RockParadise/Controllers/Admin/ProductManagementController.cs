using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RockParadise.Models;

namespace RockParadise.Controllers.Admin
{
    public class ProductManagementController : Controller
    {

        private rock_paradiseEntities3 db = new rock_paradiseEntities3();

        // GET: products
        [CheckUserRole]
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.company);
            return View(products.ToList());
        }

        // GET: products/Details/5
        [CheckUserRole]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        [CheckUserRole]
        public ActionResult Create()
        {

            ViewBag.company_id = new SelectList(db.companies, "id", "name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CheckUserRole]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,company_id,discount,info")] product product , HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {



                if (image != null && image.ContentLength > 0)
                {

                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(image.InputStream))
                    {
                        bytes = br.ReadBytes(image.ContentLength);
                    }
                    product.thumbnail = bytes;
                    product.created_at = DateTime.Now;
                    db.products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.company_id = new SelectList(db.companies, "id", "name", product.company_id);
            return View(product);
        }
        
        public void ShowImage(int? id)
        {
            if(id == null)
            {

            }
            else
            {
                try
                {
                    product product = db.products.Find(id);
                    Response.ContentType = "image/jpeg";
                    byte[] imageByteData = product.thumbnail;
                    Response.BinaryWrite(imageByteData);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }

            
        }

        [CheckUserRole]
        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.company_id = new SelectList(db.companies, "id", "name", product.company_id);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CheckUserRole]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price,company_id,thumbnail,created_at,discount,info")] product product)
        {
            if (ModelState.IsValid)
            {
                product.created_at = DateTime.Now;
                product.describe = null;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.company_id = new SelectList(db.companies, "id", "name", product.company_id);
            return View(product);
        }

        [CheckUserRole]
        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [CheckUserRole]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
