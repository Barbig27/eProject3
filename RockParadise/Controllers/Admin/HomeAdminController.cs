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
    public class HomeAdminController : Controller
    {
        private rock_paradiseEntities3 db = new rock_paradiseEntities3();
        // GET: Home
        [CheckUserRole]
        public ActionResult Index()

        {
            int totalaccount = 0;
            foreach (var item in db.accounts.ToList())
            {
                totalaccount++;

            }

            int totalproduct = 0;
            foreach (var item in db.products.ToList())
            {
                totalproduct++;

            }


            int totaluser_order = 0;
            foreach (var item in db.user_order.ToList())
            {
                totaluser_order++;

            }

            ViewBag.totala = totalaccount;
            ViewBag.totalp = totalproduct;
            ViewBag.totalo = totaluser_order;
            return View();
        }

    }
}