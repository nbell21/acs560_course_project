using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace HR_Query.Models
{
    public class PendingTransactionsModel
    {

        public int RequestID { get; set; }
        public String TransactionType { get; set; }
        public String EmployeeLastName { get; set; }
    }
}
