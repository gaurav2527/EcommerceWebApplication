using System;
using System.Web.Mvc;

namespace EcommerceWebApplication.Controllers
{
    public class ErrorHandler : IExceptionFilter
    {
        
         public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Home/Index.cshtml"
            };
        }
    }
}