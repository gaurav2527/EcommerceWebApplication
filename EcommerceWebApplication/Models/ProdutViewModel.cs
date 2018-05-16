using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceWebApplication.Models.EF;

namespace EcommerceWebApplication.Models
{
    public class ProdutViewModel
    {

        public IEnumerable<ProductInfo> ProductInfo { get; set; } = new List<ProductInfo>();
        public IEnumerable<ProductCategory> ProductCategory { get; set; } = new List<ProductCategory>();
        public IEnumerable<Shopping> ShoppingCart { get; set; } = new List<Shopping>();
        public ProductInfo ProductDetails { get; set; } = new ProductInfo();
        public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();
        public IEnumerable<Checkout> Checkout { get; set; } = new List<Checkout>();
    }
}