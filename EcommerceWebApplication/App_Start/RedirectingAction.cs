﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EcommerceWebApplication.Controllers;

namespace EcommerceWebApplication.Controllers
{
    public class RedirectingAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        { 
            base.OnActionExecuting(context);
            if (HttpContext.Current.Session["CustomerID"] == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Customers",
                    action = "Login",
                }));
            }
        }
    }
}