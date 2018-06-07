using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceWebApplication.Models.EF;

namespace EcommerceWebApplication.Controllers
{
    public class CustomersController : Controller
    {
        private ECommerce db = new ECommerce();
        private Customer customer = new Customer();
        private CustomerLastLogin lastlogin = new CustomerLastLogin();

        //public Customer Customer1 { get => Customer; set => Customer = value; }

        // GET: Customers
        public ActionResult Index()
        {
           return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
       

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerName,State,Address,ContactNumber,email,CustomerPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {

                //customer.CustomerID++; ;
               // db.ExecuteCommand("SET IDENTITY_INSERT MyTable ON");
                db.Customers.Add(customer);
                db.SaveChanges();
                //id = customer.CustomerID;
                //id++;
                return RedirectToAction("Index");
            }
            return View(customer);
        }

    public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,CustomerPassword")]UserLogin user)
        {
            if (ModelState.IsValid)
                {
                    var obj = db.Customers.Where(a => a.email.Equals(user.email) && a.CustomerPassword.Equals(user.CustomerPassword)).FirstOrDefault();
                   

                if (obj!=null)
                {
                    //Creating session after user login
                    Session["CustomerID"] = obj.CustomerID.ToString();
                    Session["email"] = obj.email.ToString();
                    Session["name"] = obj.CustomerName.ToString();
                    Session["Role"] = obj.Role;

                    //Session["LastLogin"] = lastlogin.LoginDateTime;
                    //Save last login time in session using proc
                    int customerID = Convert.ToInt32(Session["CustomerID"]);
                    var result = Customer(customerID).FirstOrDefault();
                    Session["LastLogin"] = result.LoginDateTime.ToString();

                    //Store Last login Details of customer
                   // DateTime localDate = DateTime.Now;
                    lastlogin.CustomerID = obj.CustomerID;
                    
                    //lastlogin.LoginDateTime = DateTime.Now;
                    var dbDate = DateTime.Now;
                    var udate = dbDate.ToUniversalTime();
                    lastlogin.LoginDateTime = udate;

                    db.CustomerLastLogins.Add(lastlogin);
                    db.SaveChanges();
                    return RedirectToAction("Cart", "ShoppingCart");
                }
                }
            return View("Login");
        }

        private List<usps_LastUserLogin_Result> Customer(int CustomerID)
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_LastUserLogin(CustomerID).ToList();
            }
        }

        public ActionResult Welcome()
        {
            if(Session["CustomerID"]!=null)
            {
               //return RedirectToAction("Cart","ShoppingCart");
                return View();
            }

            else
            {
                return RedirectToAction("Login");
            }
            
        }
     
        // POST: Customers/Delete/5
    

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
