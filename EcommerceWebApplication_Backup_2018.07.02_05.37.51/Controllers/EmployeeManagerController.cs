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
    public class EmployeeManagerController : Controller
    {
        private ECommerce db = new ECommerce();
        // GET: EmployeeManager
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult EmployeeManagerList()
        {
            var items = db.Employees.ToList();
            var viewmodel = items.Select(t => new
            {
                id = t.EmpID,
                Text = t.EmployeeName,
                hasChildren = t.Manager
            });
            return Json(viewmodel, JsonRequestBehavior.AllowGet);
            //return Json(employees, JsonRequestBehavior.AllowGet);
        }
    }
}