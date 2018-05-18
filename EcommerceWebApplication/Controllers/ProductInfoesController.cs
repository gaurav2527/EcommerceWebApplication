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
    public class ProductInfoesController : Controller
    {
        private ECommerce db = new ECommerce(); 
        private ProductInfo pro = new ProductInfo();
       // private ShoppingCart cart = new ShoppingCart();

        // GET: ProductInfoes
        public ActionResult Details()
        {
            ProdutViewModel product = new ProdutViewModel();
            product.ProductCategory = db.ProductCategories.ToList();
            product.ProductInfo = db.ProductInfoes.ToList();
                //var productInfoes = db.ProductInfoes.Include(p => p.ProductCategory).ToList();
            return View(product);
            /*return View(from pro in db.ProductInfoes.Take(1)
                        select pro);*/
        }
    }
}
