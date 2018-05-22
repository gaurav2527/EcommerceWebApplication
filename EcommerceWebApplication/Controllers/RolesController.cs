using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceWebApplication.Models.EF;
using EcommerceWebApplication.Models;

namespace EcommerceWebApplication.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        private ECommerce db = new ECommerce();
        public ActionResult Index()
        {
            ProdutViewModel role = new ProdutViewModel();
            role.Role = db.Roles.ToList();
            var roleId = Session["Role"];
            string username = Convert.ToString(Session["name"]);
            return PartialView();
        }
    }
}