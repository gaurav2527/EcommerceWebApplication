using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceWebApplication.Models
{
    public  partial class Shopping
    { 
            public int CartId { get; set; }
            public int Quantity { get; set; }
            public decimal TotalAmount { get; set; }
            public Nullable<int> ProductID { get; set; }
    }
    
}