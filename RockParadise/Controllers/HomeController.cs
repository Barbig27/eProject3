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
    public class HomeController : Controller
    {
        private rock_paradiseEntities3 db = new rock_paradiseEntities3();

        // GET: Home

        public ActionResult Index(string Category)
        {
            


            foreach (var item in db.companies.ToList())
            {
                ViewBag.Category += "<li class='category-item'>" +
                    "<a href='/Home/Index?Category=" + item.name + "' class='category-item_link'>" + item.name+"</a>" +
                    "</li>";
            }
            if (Category != null)
            {
                var products = db.products
                .Include(p => p.company)
                .Where(p => p.company.name == Category);
                return View(products.ToList());
            }
            else
            {
                var products = db.products.Include(p => p.company);
                return View(products.ToList());
            }
            
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            foreach (var item in db.companies.ToList())
            {
                ViewBag.Category += "<li class='category-item'>" +
                    "<a href='/Home/Index?Category=" + item.name + "' class='category-item_link'>" + item.name + "</a>" +
                    "</li>";
            }
            var searchString = form["text_search"];
            var products = db.products.Include(p => p.company)
                .Where(p => p.name.Contains(searchString)); 
            return View(products.ToList());
        }


        // GET: Home/Details/5
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
        public void ShowImage(int? id)
        {
            if (id == null)
            {

            }
            else
            {
                product product = db.products.Find(id);
                Response.ContentType = "image/jpeg";
                byte[] imageByteData = product.thumbnail;
                Response.BinaryWrite(imageByteData);
            }


        }
        // GET: Home/Create
        
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
