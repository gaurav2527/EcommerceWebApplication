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
    public class AdminController : Controller
    {
        private ECommerce db = new ECommerce();
        // GET: Admin
        [HttpGet]
        public ActionResult CustomerLoginDetails(string CustomerId, String Report, String FromDate, String ToDate)
        {
            int report = Convert.ToInt32(Report);
            TempData["CustomerId"] = CustomerId;
            DateTime fromdate = DateTime.Parse(FromDate);
            TempData["fromdate"] = fromdate;
            DateTime todate = DateTime.Parse(ToDate);
            TempData["todate"] = todate;
            if (report == 1)
            {
                var result = Customer(CustomerId, fromdate, todate);
                return PartialView("getUserlastlogin", result);
            }
           else if (report == 2)
            {
                var userinfo = UserOrdersInfo(CustomerId, fromdate, todate);
                return PartialView("getUserOrderInfo", userinfo);
            }
            else if (report == 3)
            {
                return RedirectToAction("Index");
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
        private List<usps_UserLogInDetails_Result> Customer(string CustomerId, DateTime fromdate, DateTime todate)
        {
            using (ECommerce db = new ECommerce())
            {
                //return db.usps_UserLogInDetails(CustomerID).ToList();
                return db.usps_UserLogInDetails(CustomerId, fromdate, todate).ToList();
            }
        }
        
        // getting order information of customers using stored procedures
       private List<usps_OrderDetails_Result> UserOrdersInfo(string CustomerId, DateTime fromdate, DateTime todate)
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_OrderDetails(CustomerId, fromdate, todate).ToList();
            }
        }
        //DateTime fromdate = TempData["fromdate"];
        public ActionResult Customers_Read([DataSourceRequest]DataSourceRequest request, DateTime fromDate, DateTime toDate, String CustomerId)
        {
                var customerList = Customer(CustomerId, fromDate, toDate);
                return Json(customerList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
