﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceWebApplication.Models;
using EcommerceWebApplication.Models.EF;
using Newtonsoft.Json;

namespace EcommerceWebApplication.Controllers
{

    public class ShoppingCartController : Controller
    {
        private ECommerce db = new ECommerce();

        public ActionResult AddToCart(int productId)
        {
            ProdutViewModel objProductDetails = new ProdutViewModel();
            if (objProductDetails != null)
            {
                using (ECommerce objEntity = new ECommerce())
                {
                    objProductDetails.ProductDetails = objEntity.ProductInfoes.Where(p => p.ProductID == productId).Include(p => p.ProductCategory).FirstOrDefault();
                }
                return View(objProductDetails);
            }
            else
                return View("Details", "ProductInfoes");
        }

       [RedirectingAction]
        public ActionResult Cart(ProdutViewModel model, FormCollection form)
        {
           
            var productId = Convert.ToInt16(form["productId"]);
            var quantity = Convert.ToInt16(form["Quantity"]);
            
            ShoppingCart cart = new ShoppingCart();
            try {
            ProductInfo objProductDetails = new ProductInfo();
            ////Customer customer = new Customer();
           
            using (ECommerce objEntity = new ECommerce())
            {
                objProductDetails = objEntity.ProductInfoes.Where(p => p.ProductID == productId).FirstOrDefault();
                if (objProductDetails != null)
                  {
                    cart.ProductName = objProductDetails.ProductName;
                    cart.Quantity = quantity;
                    decimal v = (quantity * objProductDetails.ProductPrice);
                    cart.TotalAmount = v;
                    objEntity.ShoppingCarts.Add(cart);
                    objEntity.SaveChanges();
                        //Store cart products in cookie//
                        //HttpCookie cookies = new HttpCookie("Product");
                        //cookies["Product"] = JsonConvert.SerializeObject(cart);
                  }
            }   
        }
            catch (Exception ex) { Console.WriteLine("Product cannot be added in cart at this time:",ex); }
            return View();
            //return View("~/Views/ShoppingCart/Cart.cshtml");
        }

        // public ActionResult Order(Object[] obj)
        // {
        //   return View();
        // }
        [HttpPost]
        public ActionResult Checkout(List<Checkout> obj)
        {
            List<ShoppingCart> lstCart = new List<ShoppingCart>();
            
            using (ECommerce product = new ECommerce())
            {
                using (var transaction = product.Database.BeginTransaction())
                {
                    try
                    {
                        if (obj != null)
                        {
                            foreach (Checkout item in obj)
                            {
                                ShoppingCart cart = new ShoppingCart();

                                cart.CartId = item.CartId;
                                cart.ProductName = item.ProductName;
                                cart.Quantity = item.Quantity;
                                decimal total = (item.Quantity * Convert.ToDecimal(item.Productprice));
                                cart.TotalAmount = total;

                                product.ShoppingCarts.Add(cart);
                                product.SaveChanges();
                                //lstCart.Add(cart);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                    transaction.Commit();
                }
            }
            var urlBuilder = new UrlHelper(Request.RequestContext);
            var url = urlBuilder.Action("Welcome", "Customers");
            return Json(new { status = "success", redirectUrl = url });
           //return RedirectToAction("Welcome", "Customers");
            //return View("~/Views/Customers/Welcome.cshtml");
            //return View("~/Views/ShoppingCart/Cart.cshtml");
        }

        //public ActionResult Checkout()
        //{
          //  return View("~/Views/ShoppingCart/Cart.cshtml");
        //}
    }
}