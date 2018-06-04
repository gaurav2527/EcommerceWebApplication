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
            var result = Customer(customerid);
            if (report == 1)
                return PartialView("getUserlastlogin", result);
            else
                return RedirectToAction("CustomerIndex");
            //return new EmptyResult();
        }
    

        public ActionResult CustomerIndex()
        {
            return View();
        }

       /* public ActionResult getUserlastlogin()
        {
            int customerid = Convert.ToInt32(TempData["CustomerId"]);
            var result = Customer(customerid);
            return PartialView(result);
        }*/

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