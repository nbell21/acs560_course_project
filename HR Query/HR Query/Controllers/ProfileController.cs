using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR_Query.Models;

namespace HR_Query.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public ActionResult Index(int profile_id = 0)
        {
            //profile_id++;

            using (var db = new HR_QueryEntities())
            {
                Employee profileEmployee = (Employee)db.Employees.Select(x => x).Where(x => x.Employee_ID == profile_id).First();

                EmployeeProfileModel model = new EmployeeProfileModel();
                model.EmployeeID = profileEmployee.Employee_ID;
                model.EmployeeName = profileEmployee.Employee_Name;
                model.DepartmentName = (db.Departments.Select(x => x).Where(x => x.Dept_ID == profileEmployee.Department).First()).Dept_Name;
                model.LocationName = (db.Locations.Select(x => x).Where(x => x.Location_ID == profileEmployee.Location).First()).Location_Name;
                model.PositionTypeName = (db.Position_Types.Select(x => x).Where(x => x.Position_Type_ID == profileEmployee.Position_Type).First()).Position_Type_Name;

                return View(model);
            }
        }

        //
        // GET: /Profile/Create

        [HttpGet]
        public ActionResult Create()
        {
            using (var db = new HR_QueryEntities())
            {
                CreateEmployeeModel model = new CreateEmployeeModel();

                model.DepartmentsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });
                model.LocationsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });
                model.PositionTypeDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });

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
        // POST: /Profile/Create

        [HttpPost]
        public ActionResult Create(CreateEmployeeModel cModel)
        {
            using (var db = new HR_QueryEntities())
            {
                Employee newEmployee = new Employee();

                int newID = db.Employees.Count() + 1;
                newEmployee.Employee_ID = newID;
                newEmployee.Employee_Name = cModel.EmployeeName;
                newEmployee.Department = int.Parse(cModel.DepartmentsDropDownSelection);
                newEmployee.Location = int.Parse(cModel.LocationsDropDownSelection);
                newEmployee.Position_Type = int.Parse(cModel.PositionTypeDropDownSelection);

                db.Employees.Add(newEmployee);
                db.SaveChanges();

                return RedirectToAction("Options", "Home");
            }
        }
    }
}
