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
        [HttpGet]
        public ActionResult Index(String CustomerId, String Report, String FromDate, String ToDate)
        {
            //int customerID = Convert.ToInt32(Session["CustomerID"]);
            //var result = Customer(customerID).FirstOrDefault();
            //TempData["LoginTime"] = result.LoginDateTime.ToString();
            //TempData.Keep();
            int customerid = Convert.ToInt32(CustomerId);
            int report = Convert.ToInt32(Report);
            DateTime fromdate = DateTime.Parse(FromDate);
            DateTime todate = DateTime.Parse(ToDate);
            var result = Customer(customerid);
            if (report == 1)
                return PartialView("UserLoginDetails", result);
            else
            return View();
        }

        /* private List<usps_LastUserLogin_Result> Customer(int CustomerID)
         {
             using (ECommerce db = new ECommerce())
             {
                 return db.usps_LastUserLogin(CustomerID).ToList();
             }
         }*/
        [HttpGet]
        public ActionResult UserLoginDetails(String CustomerId, String Report, String FromDate, String ToDate)
        {
            int customerid = Convert.ToInt32(CustomerId);
            int report = Convert.ToInt32(Report);
            DateTime fromdate = DateTime.Parse(FromDate);
            DateTime todate = DateTime.Parse(ToDate);
            var result = Customer(customerid);
            if (report == 1)
            return PartialView("index", result);
            //return PartialView(new List<usps_UserLogInDetails_Result>());
           else
              return PartialView("OrderDetails", "Admin");
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