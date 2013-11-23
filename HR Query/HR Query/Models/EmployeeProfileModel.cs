using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Query.Models
{
    public class EmployeeProfileModel
    {
        public int EmployeeID { get; set; }
        public String EmployeeName { get; set; }
        public String DepartmentName { get; set; }
        public String LocationName { get; set; }
        public String PositionTypeName { get; set; }

    }
}
