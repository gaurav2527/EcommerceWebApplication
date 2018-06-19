using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web.ModelBinding;
using System.Web.Mvc;
/*using System.Web.mode;

namespace EcommerceWebApplication
{
    public class DateTimeModelBinder : System.Web.ModelBinding.IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            DateTime dateTime;

            var isDate = DateTime.TryParse(value.AttemptedValue, Thread.CurrentThread.CurrentUICulture, DateTimeStyles.None, out dateTime);

            if (!isDate)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ModelValidationResources.InvalidDateTime);
                return DateTime.UtcNow;
            }
            return dateTime;
        }
    }
}*/