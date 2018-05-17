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
    public class OrderHistoryController : Controller
    {
        // GET: OrderHistory
        public ActionResult Index()
        {
            ProdutViewModel product = new ProdutViewModel();
            using (ECommerce context = new ECommerce())
            {
                product.ShoppingCart = context.ShoppingCarts.ToList();
            }
                return View(product);
        }
    }
}
