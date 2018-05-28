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
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpPost]
        public ActionResult UserInfo(Customer model, FormCollection form)
        {
            //var customerId = form["customerid"];
            var customerId = Convert.ToString(form["CustomerName"]);
            int report = Convert.ToInt32(form["Type"]);
            string fromdate = form["fromDate"];
            string todate = form["toDate"];
            if(report==1)
            {
                
            }
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