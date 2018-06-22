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
    public class EmployeeController : Controller
    {
        private ECommerce db = new ECommerce();
        private ManagerViewModel manager = new ManagerViewModel();
        private Employee emps = new Employee();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmpDetails()
         {
            db.Configuration.LazyLoadingEnabled = false;
            manager.Employee = db.Employees.ToList();
            manager.EmployeesManager = EmployeeMangerDetails();
            //var result = EmployeeMangerDetails();
            return View(manager);
        }

        public ActionResult Employee_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (ECommerce db = new ECommerce())
            {
                //IQueryable<Customer> customers = db.Customers;
                //DataSourceResult result = customers.ToDataSourceResult(request);
                //return Json(result, JsonRequestBehavior.AllowGet);
                var result = EmployeeMangerDetails();
                return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Employee_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Employee> emps)
        {
            var results = new List<Employee>();
            if (emps != null)
            {
                foreach(var emp in emps)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    results.Add(emp);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
            //return Json(new[] { results }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employee_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Employee> emps)
        {
            //if (ModelState.IsValid)
            if (emps != null)
            {
                foreach(var emp in emps)
                {
                        using (ECommerce db = new ECommerce())
                        {

                            var entity = new Employee()
                            {
                                EmpID = emp.EmpID,
                                EmployeeName = emp.EmployeeName,
                                Manager = emp.Manager,
                                //ManagerName = emp.ManagerName
                            };
                            db.Employees.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                   
                }
            }
            return Json(emps.ToDataSourceResult(request, ModelState));
            //return Json(new[] { emps }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Employee_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Employee> emps)
        {
            if(emps!=null)
            {
                foreach(var emp in emps)
                {
                    using (var db = new ECommerce())
                    {
                        // Create a new Employee entity and set its properties from the posted employee model.
                        var entity = new Employee
                        {
                            EmpID = emp.EmpID,
                            EmployeeName = emp.EmployeeName,
                            //ManagerName = emp.ManagerName,
                            Manager = emp.Manager
                        };
                        // Attach the entity.
                        db.Employees.Attach(entity);
                        // Delete the entity.
                        db.Employees.Remove(entity);
                        // Delete the entity in the database.
                        db.SaveChanges();
                    }
                }
            }
            return Json(emps.ToDataSourceResult(request, ModelState));
            //return Json(new[] { emps }.ToDataSourceResult(request, ModelState));
        }

        //getting employee relationship table
        private List<usps_Employees_Result> EmpDetail()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_Employees().ToList();
            }
        }

        private List<usps_EmployeesManager_Result> EmployeeMangerDetails()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_EmployeesManager().ToList();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}