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
using System.Globalization;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace EcommerceWebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private ECommerce db = new ECommerce();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmpDetails()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var result = db.Employees.ToList();
            return View();
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