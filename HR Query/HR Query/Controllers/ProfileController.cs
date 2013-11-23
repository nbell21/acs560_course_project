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

    }
}
