using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Query.Models
{
    public class EmployeeQueryModel 
    {
        public int EmployeeID { get; set; }
        public String EmployeeLastName { get; set; }
        public String SearchedCriteria { get; set; }
    }
}
