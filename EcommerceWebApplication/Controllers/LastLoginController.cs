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

namespace EcommerceWebApplication.Controllers
{
    public class LastLoginController : Controller
    {
        // private CustomerLastLogin lastlogin = new CustomerLastLogin();
        //private Customer customer = new Customer();
        // GET: LastLogin
        public ActionResult Index(UserLogin user)
        {
            ProdutViewModel customer = new ProdutViewModel();
            /*using (ECommerce context = new ECommerce())
            {
                //customer.CustomerLastLogin = context.CustomerLastLogins.ToList();
                customer.CustomerLastLogin = customer.CustomerLastLogin.Where(a => a.CustomerID.Equals(Session["CustomerID"]));
            }*/
            int customerID = Convert.ToInt32(Session["CustomerID"]);
            var result = Customer(customerID);
            return PartialView(result);
        }
        
        private List<usps_LastUserLogin_Result> Customer(int CustomerID)
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_LastUserLogin(CustomerID).ToList();
            }
        }
    }
}