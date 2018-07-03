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
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace EcommerceWebApplication.Controllers
{
    public class OrderHistoryController : Controller
    {
        // GET: OrderHistory
        public ActionResult Index()
        {
            ProdutViewModel product = new ProdutViewModel();
            var customerid = Convert.ToInt32(Session["CustomerID"]);
            var result = CartHistoryDetails(customerid);
            return View(result);
        }

        private List<usps_CartHistory_Result> CartHistoryDetails(int CustomerId)
        {
            using (ECommerce db = new ECommerce())
            {
                return db.usps_CartHistory(CustomerId).ToList();
            }
        }

        public ActionResult OrderHistory_Read([DataSourceRequest]DataSourceRequest request, int customerid)
        {
            var result = CartHistoryDetails(customerid);
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
