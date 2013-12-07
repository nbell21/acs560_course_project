using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace HR_Query.Models
{
    public class CreateEmployeeModel 
    {
        [Required(ErrorMessage="Employee first name is required.")]
        [StringLength(30, ErrorMessage = "Employee first name must be under 30 characters.")]
        [Display(Name = "Employee First Name")]
        public string EmployeeFirstName { get; set; }

        [Required(ErrorMessage = "Employee last name is required.")]
        [StringLength(30, ErrorMessage = "Employee last name must be under 30 characters.")]
        [Display(Name = "Employee Last Name")]
        public string EmployeeLastName { get; set; }

        public List<SelectListItem> DepartmentsDropDown { get; set; }
        [Range(1, 100, ErrorMessage="Please select a department.")]
        public String DepartmentsDropDownSelection { get; set; }
        
        public List<SelectListItem> LocationsDropDown { get; set; }
        [Range(1, 100, ErrorMessage = "Please select a location.")]
        public String LocationsDropDownSelection { get; set; }

        public List<SelectListItem> PositionTypeDropDown { get; set; }
        [Range(1, 100, ErrorMessage = "Please select a position type.")]
        public String PositionTypeDropDownSelection { get; set; }
        
        public CreateEmployeeModel()
        {
            DepartmentsDropDown = new List<SelectListItem>();
            LocationsDropDown = new List<SelectListItem>();
            PositionTypeDropDown = new List<SelectListItem>();
        }
    }
}
