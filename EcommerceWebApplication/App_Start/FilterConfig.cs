using System.Web;
using System.Web.Mvc;
using EcommerceWebApplication.Controllers;


namespace EcommerceWebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ErrorHandler());
        }
    }
}
