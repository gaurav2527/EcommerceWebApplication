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
    public class ProductInfoesController : Controller
    {
        private ECommerce db = new ECommerce();
        private ProductInfo pro = new ProductInfo();

       

        // GET: ProductInfoes
        public ActionResult Details()
        {
            
                var productInfoes = db.ProductInfoes.Include(p => p.ProductCategory);
                
            return View(productInfoes.ToList());
            /*return View(from pro in db.ProductInfoes.Take(1)
                        select pro);*/
        }


    }
}
