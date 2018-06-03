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
    public class practiceController : Controller
    {
        // GET: practice
        private ECommerce db = new ECommerce();
        private Customer customer = new Customer();
        public ActionResult Index()
        {
                return View(db.Customers.ToList());
        }
    }
}