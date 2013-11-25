using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace HR_Query.Models
{
    public class CreateEmployeeModel 
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        public List<SelectListItem> DepartmentsDropDown { get; set; }
        public String DepartmentsDropDownSelection { get; set; }
        public List<SelectListItem> LocationsDropDown { get; set; }
        public String LocationsDropDownSelection { get; set; }
        public List<SelectListItem> PositionTypeDropDown { get; set; }
        public String PositionTypeDropDownSelection { get; set; }
        
        public CreateEmployeeModel()
        {
            DepartmentsDropDown = new List<SelectListItem>();
            LocationsDropDown = new List<SelectListItem>();
            PositionTypeDropDown = new List<SelectListItem>();
        }
    }
}
