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
        private int _searchType;
        private List<EmployeeQueryModel> modelItems = new List<EmployeeQueryModel>();

        //
        // GET: /Query/

        [HttpGet]
        public ActionResult Index()
        {
            using(var db = new HR_QueryEntities())
            {            
            var model = new QueryModel();

            model.OptionsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });
            model.OptionsDropDown.Add(new SelectListItem { Text = "Query by department", Value = "1" });
            model.OptionsDropDown.Add(new SelectListItem { Text = "Query by location", Value = "2" });
            model.OptionsDropDown.Add(new SelectListItem { Text = "Query by position type", Value = "3" });

            model.DepartmentsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });
            model.LocationsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });
            model.PositionTypeDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });

            var list = db.Departments.ToList();

            var items = from g in list select new SelectListItem { Value = g.Dept_ID.ToString(), Text = g.Dept_Name };

            foreach (var item in items)
            {
                model.DepartmentsDropDown.Add(item);
            }
            
            var list2 = db.Locations.ToList();

            var items2 = from g in list2 select new SelectListItem { Value = g.Location_ID.ToString(), Text = g.Location_Name };

            foreach (var item in items2)
            {
                model.LocationsDropDown.Add(item);
            }

            var list3 = db.Position_Types.ToList();

            var items3 = from g in list3 select new SelectListItem { Value = g.Position_Type_ID.ToString(), Text = g.Position_Type_Name };

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

            int departmentSelection = int.Parse(qModel.DepartmentsDropDownSelection);
            int locationSelection = int.Parse(qModel.LocationsDropDownSelection);
            int positionTypeSelection = int.Parse(qModel.PositionTypeDropDownSelection);

            if (departmentSelection > 0)
            {
                _selectedId = departmentSelection;
                _searchType = 1;
            }
            else if (locationSelection > 0)
            {
                _selectedId = locationSelection;
                _searchType = 2;
            }
            else if (positionTypeSelection > 0)
            {
                _selectedId = positionTypeSelection;
                _searchType = 3;
            }
            else
                return RedirectToAction("NullEmployeeList");

            return RedirectToAction("FilteredEmployeeList", new { _selectedId = _selectedId, _searchType = _searchType });
        }

        //
        // GET: /FilteredEmployeeList/

        public ActionResult FilteredEmployeeList(int _selectedId, int _searchType)
        {
            
            using (var db = new HR_QueryEntities())
            {
                if (_searchType == 1)
                {
                    List<Employee> query = db.Employees.Select(x => x).Where(x => x.Department == _selectedId && x.Available == true).ToList();

                    if(query.Count == 0)
                        return RedirectToAction("NullEmployeeList");

                    ViewBag.Message = "Employee Department";

                    foreach (Employee e in query)
                    {
                        EmployeeQueryModel model = new EmployeeQueryModel();
                        model.EmployeeID = e.Employee_ID;
                        model.EmployeeLastName = e.Last_Name;
                        model.SearchedCriteria = (db.Departments.Select(x => x).Where(x => x.Dept_ID == e.Department).First()).Dept_Name;

                        modelItems.Add(model);
                    }
                }
                else if (_searchType == 2)
                {
                    List<Employee> query = db.Employees.Select(x => x).Where(x => x.Location == _selectedId && x.Available == true).ToList();

                    if (query.Count == 0)
                        return RedirectToAction("NullEmployeeList");

                    ViewBag.Message = "Employee Location";

                    foreach (Employee e in query)
                    {
                        EmployeeQueryModel model = new EmployeeQueryModel();
                        model.EmployeeID = e.Employee_ID;
                        model.EmployeeLastName = e.Last_Name;
                        model.SearchedCriteria = (db.Locations.Select(x => x).Where(x => x.Location_ID == e.Location).First()).Location_Name;

                        modelItems.Add(model);
                    }
                }
                else
                {
                    List<Employee> query = db.Employees.Select(x => x).Where(x => x.Position_Type == _selectedId && x.Available == true).ToList();

                    if (query.Count == 0)
                        return RedirectToAction("NullEmployeeList");

                    ViewBag.Message = "Employee Position Type";

                    foreach (Employee e in query)
                    {
                        EmployeeQueryModel model = new EmployeeQueryModel();
                        model.EmployeeID = e.Employee_ID;
                        model.EmployeeLastName = e.Last_Name;
                        model.SearchedCriteria = (db.Position_Types.Select(x => x).Where(x => x.Position_Type_ID == e.Position_Type).First()).Position_Type_Name;

                        modelItems.Add(model);
                    }
                }
                return View(modelItems);

            }
        }

        //
        // GET: /CompleteEmployeeList/

        public ActionResult CompleteEmployeeList()
        {

            using (var db = new HR_QueryEntities())
            {
                List<Employee> query = db.Employees.Select(x => x).Where(x => x.Available == true).ToList();

                foreach (Employee e in query)
                {
                    EmployeeQueryModel model = new EmployeeQueryModel();
                    model.EmployeeID = e.Employee_ID;
                    model.EmployeeLastName = e.Last_Name;
                    model.SearchedCriteria = (db.Departments.Select(x => x).Where(x => x.Dept_ID == e.Department).First()).Dept_Name;

                    modelItems.Add(model);
                }

                return View(modelItems);

            }
        }

         //
        // GET: /NullEmployeeList/

        public ActionResult NullEmployeeList()
        {
            return View();
        }
    }

}
