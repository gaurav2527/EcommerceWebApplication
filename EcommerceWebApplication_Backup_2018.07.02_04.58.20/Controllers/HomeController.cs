using EcommerceWebApplication.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /* [HttpPost]
        public ActionResult Signup(FormCollection formCollection)
         {
             try
             {
                 Customer cus = new Customer();

                 using (ECommerceEntities db = new ECommerceEntities())
                 {

                     cus.CustomerName = formCollection.Customer;
                     cus.State = formCollection.state;
                     cus.Address = address;
                     cus.ContactNumber = contact;
                     db.Customers.Add(cus);
                     db.SaveChanges();
                 } ;

                 int latestID = cus.CustomerID;
                 return RedirectToAction("Index");
             }

             catch (Exception ex)
             {
                 throw ex;
             }
         }*/

    }
}