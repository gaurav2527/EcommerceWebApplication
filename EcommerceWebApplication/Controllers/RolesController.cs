using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceWebApplication.Models.EF;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        private ECommerce db = new ECommerce();
        Customer customer = new Customer();
        public ActionResult Index()
        {
            //ViewBag.CustomerName = new SelectList(db.Customers.)
            return PartialView(db.Customers.ToList());
        }

       private List<usps_Customers_Result> Users()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_Customers().ToList();
            }
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