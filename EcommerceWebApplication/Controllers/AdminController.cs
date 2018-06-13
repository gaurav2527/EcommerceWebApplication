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
                var customerinfo = UserOrdersInfo(CustomerId, fromdate, todate);
                return PartialView("getUserOrderInfo", customerinfo);
            }
            else if (report == 3)
            {
                var userinfo = UserDetails();
                return RedirectToAction("Index", userinfo);
            }
            else
                return RedirectToAction("CustomerIndex");
            //return new EmptyResult();
        }
        //Getting monthly logins of User
        [HttpGet]
        public ActionResult _UserMonthInfo(string Month)
        {
            int value = Convert.ToInt32(Month);
            if(value == 1)
            {
                var result = LoginMonths();
                return PartialView("_getUserMonthlyLogins",result);
            }
            else if(value == 2)
            {
                var result = LoginWeeks();
                return PartialView("_getUserWeeklyLogin", result);
            }
            else if (value == 3)
            {
                var result = LastHourLogin();
                return PartialView("_getUserLastHourLogin", result);
            }
            else
                return RedirectToAction("CustomerIndex");
        }

        //Getting number of logins in months
        private List<usps_UserHours_Result> LoginMonths()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_UserHours().ToList();
            }
        }

        //Getting number of logins in weeks
        private List<usps_UserLoginByWeeks_Result> LoginWeeks()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_UserLoginByWeeks().ToList();
            }
        }

        //Getting number of Logins in last one hour
      private List<usps_UserLoginByHours_Result> LastHourLogin()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_UserLoginByHours().ToList();
            }
        }

        // Getting usrname, email and password
        private List<usps_Customers_Result> UserDetails()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_Customers().ToList();
            }
        }

        public ActionResult CustomerIndex()
        {
            return RedirectToAction("Index", "UserInfo");
        }

        //dropdown categories
        public ActionResult Customer_Categories()
        {
            DropDownCategory entities = new DropDownCategory();
            var result = db.DropDownCategories.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
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
        
        //Read functin for Monthly logins
        public ActionResult MonthlyLogins_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = LoginMonths();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        //Read function for Weekly logins
        public ActionResult WeeklyLogins_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = LoginWeeks();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LastHourLogins_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = LastHourLogin();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
