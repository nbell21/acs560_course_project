using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace HR_Query.Models
{
    public class QueryModel
    {
        public List<SelectListItem> OptionsDropDown { get; set; }
        public String OptionsDropDownSelection { get; set; }
        public List<SelectListItem> DepartmentsDropDown { get; set; }
        public String DepartmentsDropDownSelection { get; set; }
        public List<SelectListItem> LocationsDropDown { get; set; }
        public String LocationsDropDownSelection { get; set; }
        public List<SelectListItem> PositionTypeDropDown { get; set; }
        public String PositionTypeDropDownSelection { get; set; }
        
        public QueryModel()
        {
            OptionsDropDown = new List<SelectListItem>();
            DepartmentsDropDown = new List<SelectListItem>();
            LocationsDropDown = new List<SelectListItem>();
            PositionTypeDropDown = new List<SelectListItem>();
        }

    }
}
