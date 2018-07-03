using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceWebApplication.Models.EF;

namespace EcommerceWebApplication.Models
{
    public class CustomerViewModel
    {
        public IEnumerable<Role> Role { get; set; } = new List<Role>();
        public IEnumerable<usps_Customers_Result> usps_Customers_Result { get; set; } = new List<usps_Customers_Result>();
    }
}