using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR_Query.Models;

namespace HR_Query.Controllers
{
    public class QueryController : Controller
    {
        
        public ActionResult Index(string returnUrl)
        {
            using(var db = new HR_QueryEntities())
            {            
            var model = new QueryModel();

            model.OptionsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });

            model.OptionsDropDown.Add(new SelectListItem { Text = "Query by department", Value = "1" });

            model.OptionsDropDown.Add(new SelectListItem { Text = "Query by location", Value = "2" });

            model.OptionsDropDown.Add(new SelectListItem { Text = "Query by position type", Value = "3" });

            var myList = new List<SelectListItem>();

            var list = db.Departments.ToList();

            var items = from g in list
                        select new SelectListItem
                        {
                            Value = g.Dept_ID.ToString(),
                            Text = g.Dept_Name
                        };

            model.DepartmentsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });

            foreach (var item in items)
            {
                model.DepartmentsDropDown.Add(item);
            }

            return View(model);
            }
        }
    }
}
