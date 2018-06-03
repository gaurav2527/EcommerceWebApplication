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
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        private ECommerce db = new ECommerce();
        public ActionResult Index()
        {
            Customer customer = new Customer();
            return View(db.Customers.ToList());
        }
    }
}