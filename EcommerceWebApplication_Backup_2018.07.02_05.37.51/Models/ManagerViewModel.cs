using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceWebApplication.Models.EF;

namespace EcommerceWebApplication.Models
{
    public class ManagerViewModel
    {
        public List<Employee> Employee { get; set; } = new List<Employee>();
        public List<usps_EmployeesManager_Result> EmployeesManager { get; set; } = new List<usps_EmployeesManager_Result>();
    }
}
