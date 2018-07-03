using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceWebApplication.Models.EF
{
    public  partial class Shopping
    {


        public int CartId { get; set; }
            public int Quantity { get; set; }
            public decimal TotalAmount { get; set; }
            public string ProductName { get; set; }
            public Nullable<int> ProductID { get; set; }
            public int OrderID { get; set; }
            public int CustomerID { get; set; }
            public Nullable<System.DateTime> OrderDate { get; set; }
    }
}