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
            int customerId = Convert.ToInt32(form["CustomerName"]);
            TempData["customerid"] = customerId;
            int report = Convert.ToInt32(form["Type"]);
            TempData["fromdate"] = form["fromDate"];
            TempData["todate"] = form["toDate"];
            TempData.Keep();
            if (report == 1)
            {
                return RedirectToAction("lastlogin");
            }
           else
                return RedirectToAction("OrderDetails");
        }

        public ActionResult lastlogin()
        {
            int customerid = Convert.ToInt32(TempData["customerid"]);
            DateTime fromDate = Convert.ToDateTime(TempData["fromdate"]);
            DateTime todate = Convert.ToDateTime(TempData["todate"]);
            var result = Customer(customerid);
            return PartialView(result);
        }

        public ActionResult OrderDetails()
        {
            return PartialView();
        }

        private List<usps_UserLogInDetails_Result> Customer(int customerId)
        {
            using (ECommerce db = new ECommerce())
            {
                //return db.usps_UserLogInDetails(CustomerID).ToList();
               return db.usps_UserLogInDetails(customerId).ToList();
            }
        }
    }
}