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

namespace EcommerceWebApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult CustomerLoginDetails(String CustomerId, String Report, String FromDate, String ToDate)
        {
            int customerid = Convert.ToInt32(CustomerId);
            int report = Convert.ToInt32(Report);
            DateTime fromdate = DateTime.Parse(FromDate);
            DateTime todate = DateTime.Parse(ToDate);
            //var result = Customer(customerid, fromdate, todate);
            if (report == 1)
            {
                var result = Customer(customerid, fromdate, todate);
                return PartialView("getUserlastlogin", result);
            }
            else if (report == 2)
            {
                var userinfo = UserOrdersInfo(customerid, fromdate, todate);
                return PartialView("getUserOrderInfo", userinfo);
            }
            else
                return RedirectToAction("CustomerIndex");
            //return new EmptyResult();
        }

        public ActionResult CustomerIndex()
        {
            return RedirectToAction("Index", "UserInfo");
        }

        //getting user login details using 
        private List<usps_UserLogInDetails_Result> Customer(int customerid, DateTime fromdate, DateTime todate)
        {
            using (ECommerce db = new ECommerce())
            {
                //return db.usps_UserLogInDetails(CustomerID).ToList();
                return db.usps_UserLogInDetails(customerid, fromdate, todate).ToList();
            }
        }
        
        // getting order information of customers using stored procedures
        private List<usps_OrderDetails_Result> UserOrdersInfo(int customerid, DateTime fromdate, DateTime todate)
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_OrderDetails(customerid, fromdate, todate).ToList();
            }
        }
    }
}