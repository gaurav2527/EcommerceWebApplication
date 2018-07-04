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
            db.Configuration.LazyLoadingEnabled = false;
            //db.Configuration.ProxyCreationEnabled = false;
            var results = new List<Employee>();
            if (emps != null)
            {
                foreach (var emp in emps)
                {
                    var manager = db.Employees.Add(emp);
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
            db.Configuration.LazyLoadingEnabled = false;
            //db.Configuration.ProxyCreationEnabled = false;
            if (emps != null)
            {
                foreach (var emp in emps)
                {
                    //var isIn = db.Employees.Where(s => s.EmpID.Equals(emp.Manager) && s.Manager.Equals(emp.EmpID));
                    //var custEmpQuery = result.Where(c => c.Manager.Equals(emp.EmpID)); 
                    //var isIn = result.Where(c => c.Manager.Equals(emp.EmpID));
                    //bool isIn = result.Any(c => c.Manager.Equals(emp.EmpID));
                    var result = MangerList(emp.EmpID);
                    var a = emp.Manager;
                    if (!result.Any(s => s.EmpID.Equals(a)) && !emp.EmpID.Equals(emp.Manager))
                    {
                        using (ECommerce db = new ECommerce())
                        {
                            var entity = new Employee()
                            {
                                EmpID = emp.EmpID,
                                EmployeeName = emp.EmployeeName,
                                Manager = emp.Manager
                            };
                            db.Employees.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        return Json(emps.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            return RedirectToAction("Employee_Read");
        }

        public ActionResult Employee_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Employee> emps)
        {
            if (emps != null)
            {
                foreach (var emp in emps)
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


        public JsonResult Get_Employees(int? id)
        {
            var dataContext = new ECommerce();

            var employees = from e in dataContext.Employees
                            where (id.HasValue ? e.Manager == id : e.Manager == 0)
                            select new
                            {
                                id = e.EmpID,
                                Name = e.EmployeeName,
                                hasChildren = (from q in dataContext.Employees
                                               where (q.Manager == e.EmpID)
                                               select q
                                               )
                            };
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        //save employee and manager after rearrange positions in tree view.
        [HttpPost]
        public void SaveNode(int id, int? reportsTo)
        {
            var result = MangerList(id);
            var a = reportsTo;
            if (!result.Any(s => s.EmpID.Equals(a)) && !id.Equals(a))
            {
                var employee = db.Employees.First(e => e.EmpID == id);
                employee.Manager = Convert.ToInt32(reportsTo);
                db.SaveChanges();
            }
            //var response = EmployeeMangerDetails();
            //return Json(response, JsonRequestBehavior.AllowGet);
            // return RedirectToAction("Employee_Read");
        }

        //getting list of Managers
        public ActionResult Manager_Dropdown()
        {
            var result = EmployeeMangerDetails();
            var manager = from m in result
                          select (m.ManagerName);
            return Json(manager, JsonRequestBehavior.AllowGet);
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

        //getting Employee and Managers List
        /*  private List<usps_EmpManagers_Result> MangerList(int empId)
          {
              using (ECommerce db = new ECommerce())
              {
                  return db.usps_EmpManagers(empId).ToList();
              }
          }*/
        private List<usps_getManagerList_Result> MangerList(int empID)
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_getManagerList(empID).ToList();
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