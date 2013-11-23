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

        private int _selectedId;
        private List<EmployeeQuery> modelItems = new List<EmployeeQuery>();

        //
        // GET: /Query/
        
        public ActionResult Index()
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

            foreach (var item in items)
            {
                model.DepartmentsDropDown.Add(item);
            }

            var list2 = db.Locations.ToList();

            var items2 = from g in list2
                        select new SelectListItem
                        {
                            Value = g.Location_ID.ToString(),
                            Text = g.Location_Name
                        };

            foreach (var item in items2)
            {
                model.LocationsDropDown.Add(item);
            }

            var list3 = db.Position_Types.ToList();

            var items3 = from g in list3
                         select new SelectListItem
                         {
                             Value = g.Position_Type_ID.ToString(),
                             Text = g.Position_Type_Name
                         };

            foreach (var item in items3)
            {
                model.PositionTypeDropDown.Add(item);
            }
         
            return View(model);
            }
        }

        //
        // POST: /Query/

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(QueryModel qModel)
        {

            _selectedId = int.Parse(qModel.DepartmentsDropDownSelection);
            return RedirectToAction("EmployeeList", new { _selectedId = _selectedId });
        }

        //
        // GET: /EmployeeList/

        public ActionResult EmployeeList(int _selectedId)
        {
            
            using (var db = new HR_QueryEntities())
            {
                List<Employee> query = db.Employees.Select(x => x).Where(x => x.Department == _selectedId).ToList();

                foreach (Employee e in query)
                {
                    EmployeeQuery model = new EmployeeQuery();
                    model.EmployeeID = e.Employee_ID;
                    model.EmployeeName = e.Employee_Name;
                    model.DepartmentName = (db.Departments.Select(x => x).Where(x => x.Dept_ID == e.Department).First()).Dept_Name;

                    modelItems.Add(model);

                    //Location loc = db.Employees.Select(x => x.Location).Where(x => x.Location_ID == e.Location);
                }

                return View(modelItems);

            }
        }
    }
}
