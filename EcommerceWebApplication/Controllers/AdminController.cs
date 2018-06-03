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
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult CustomerLoginDetails(String CustomerId, String Report, String FromDate, String ToDate)
        {
            int customerid = Int32.Parse(CustomerId);
            int report = Int32.Parse(Report);
            DateTime fromdate = Convert.ToDateTime(FromDate);
            DateTime todate = DateTime.Parse(ToDate);
            var result = Customer(customerid);
            if (report == 1)
                return PartialView(result);
            else
                return RedirectToAction("CustomerIndex");
        }

        public ActionResult CustomerIndex()
        {
            return View();
        }

        private List<usps_UserLogInDetails_Result> Customer(int customerid)
        {
            using (ECommerce db = new ECommerce())
            {
                //return db.usps_UserLogInDetails(CustomerID).ToList();
                return db.usps_UserLogInDetails(customerid).ToList();
            }
        }
    }
}