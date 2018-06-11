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
    public class CustomerInfoController : Controller
    {
        private ECommerce db = new ECommerce();
        //private Customer customer = new Customer();

        // GET: CustomerInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailInfo()
        {
           return View(db.Customers.ToList());
        }

        private List<usps_Customers_Result> UserDetails()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_Customers().ToList();
            }
        }

        public ActionResult user_Read([DataSourceRequest]DataSourceRequest request)
        {
            var userlist = UserDetails();
            return Json(userlist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}