using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace HR_Query.Models
{
    public class RequestModel 
    {
        public int RequestID { get; set; }
        public String RequestorName { get; set; }
        public bool RequestStatus { get; set; }
        public String TransactionType { get; set; }
        public int EmployeeID { get; set; }
        public String EmployeeFirstName { get; set; }
        public String EmployeeLastName { get; set; }
        public String DepartmentName { get; set; }
        public String LocationName { get; set; }
        public String PositionTypeName { get; set; }
    }
}
