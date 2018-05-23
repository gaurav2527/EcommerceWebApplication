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
    public class LastLoginController : Controller
    {

        public ActionResult Index()
        {
            //int customerID = Convert.ToInt32(Session["CustomerID"]);
           // var result = Customer(customerID).FirstOrDefault();
            //TempData["LoginTime"] = result.LoginDateTime.ToString();
            //TempData.Keep();

            return View();
        }

        private List<usps_LastUserLogin_Result> Customer(int CustomerID)
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_LastUserLogin(CustomerID).ToList();
            }
        }
    }
}