using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceWebApplication.Models
{
    public class Checkout
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public string Productprice { get; set; }
        public string ProductName { get; set; }
    }
}