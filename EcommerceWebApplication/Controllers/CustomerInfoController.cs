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
        // GET: CustomerInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailInfo()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var result = db.Customers.ToList();
            return View(result);
        }

        public ActionResult Customer_Read([DataSourceRequest]DataSourceRequest request)
        {

            using (ECommerce db = new ECommerce())
            {
                //IQueryable<Customer> customers = db.Customers;
                //DataSourceResult result = customers.ToDataSourceResult(request);
                //return Json(result, JsonRequestBehavior.AllowGet);
                var result = UserDetails();
                return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customer_Create([DataSourceRequest] DataSourceRequest request, Customer customer)
        {
            
            if (customer != null)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customer_Update([DataSourceRequest] DataSourceRequest request, Customer customer)
        {
            //if (ModelState.IsValid)
            if(customer!=null)
            {
                using (ECommerce db = new ECommerce())
                {
                    var entity = new Customer()
                    {
                        CustomerID = customer.CustomerID,
                        CustomerPassword = customer.CustomerPassword,
                        email = customer.email,
                        CustomerName = customer.CustomerName,
                        State = customer.State,
                        Address = customer.Address,
                        ContactNumber = customer.ContactNumber,
                        Role = customer.Role
                    };
                    db.Customers.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customer_Destroy([DataSourceRequest]DataSourceRequest request, Customer customer)
        {
            if (customer!=null)
            {
                using (var db = new ECommerce())
                {
                    // Create a new Customer entity and set its properties from the posted Customer model.
                    var entity = new Customer
                    {
                        CustomerID = customer.CustomerID,
                        CustomerName = customer.CustomerName,
                        CustomerPassword = customer.CustomerPassword,
                        Address = customer.Address,
                        State = customer.State,
                        Role = customer.Role,
                        ContactNumber = customer.ContactNumber
                    };
                    // Attach the entity.
                    db.Customers.Attach(entity);
                    // Delete the entity.
                    db.Customers.Remove(entity);
                    // Delete the entity in the database.
                    db.SaveChanges();
                }
            }
            // Return after removed Customer. Also return any validation errors.
            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }

        private List<usps_Customers_Result> UserDetails()
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_Customers().ToList();
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
        /*public ActionResult user_Read([DataSourceRequest]DataSourceRequest request)
        {
            var userlist = UserDetails();
            return Json(userlist.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }*/
    }
}