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
    public class MyPurchaseController : Controller
    {
        private rock_paradiseEntities3 db = new rock_paradiseEntities3();

        // GET: MyPurchase
        [CheckLogin]
        public ActionResult Index()
        {
            var user_order = db.user_order.Include(u => u.account);
            int userid = (int)Session["UserID"];
            var sortedOrders = db.user_order
                    .OrderByDescending(o => o.status == null)
                    .ThenBy(o => o.status == "Agree" ? 0 : 1)
                    .ThenBy(o => o.status == "Deliver" ? 0 : 1)
                    .ThenBy(o => o.status == "Complete" ? 0 : 1)
                    .ThenBy(o => o.status == "Reject" ? 0 : 1)
                    .ToList();
            return View(sortedOrders.Where(o => o.id_user == userid).ToList());
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
