using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace EcommerceWebApplication.Cultures
{
    public class DateCultures
    {
        public static void main()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.AppDomainInitializer = SetAppDomainCultures;
            setup.AppDomainInitializerArguments = new string[] { "ru-RU" };
            AppDomain domain = AppDomain.CreateDomain("Domain2", null, setup);
        }
        public static void SetAppDomainCultures(string[] names)
        {
            //SetAppDomainCultures(names[0]);
        }
    }
}