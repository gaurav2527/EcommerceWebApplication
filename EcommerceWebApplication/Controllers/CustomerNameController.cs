using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceWebApplication.Models.EF;

namespace EcommerceWebApplication.Controllers
{
    public class CustomerNameController : Controller
    {
        private ECommerce customerName = new ECommerce();
        // GET: CustomerName
        public ActionResult Index()
        {
            return View(customerName.Customers.ToList());
        }
    }
}