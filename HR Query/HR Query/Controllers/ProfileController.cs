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
        private List<PendingTransactionsModel> modelItems = new List<PendingTransactionsModel>();

        //
        // GET: /Profile/
        [HttpGet]
        public ActionResult Index(int profile_id = 0)
        {
            using (var db = new HR_QueryEntities())
            {
                Employee profileEmployee = (Employee)db.Employees.Select(x => x).Where(x => x.Employee_ID == profile_id).First();

                EmployeeProfileModel model = new EmployeeProfileModel();
                model.EmployeeID = profileEmployee.Employee_ID;
                model.EmployeeFirstName = profileEmployee.First_Name;
                model.EmployeeLastName = profileEmployee.Last_Name;
                model.DepartmentName = (db.Departments.Select(x => x).Where(x => x.Dept_ID == profileEmployee.Department).First()).Dept_Name;
                model.LocationName = (db.Locations.Select(x => x).Where(x => x.Location_ID == profileEmployee.Location).First()).Location_Name;
                model.PositionTypeName = (db.Position_Types.Select(x => x).Where(x => x.Position_Type_ID == profileEmployee.Position_Type).First()).Position_Type_Name;

                return View(model);
            }
        }

        //
        // POST: /Profile/Delete
        [HttpPost]
        public ActionResult Delete(EmployeeProfileModel pModel)
        {
            using (var db = new HR_QueryEntities())
            {
                List<Request> query = db.Requests.Select(x => x).Where(x => x.Request_Status == true && x.Request_Type_Index == 2).ToList();

                foreach (Request r in query)
                {
                    if (r.Employee_ID == pModel.EmployeeID)
                        return RedirectToAction("Options", "Home");
                }

                Request newRequest = new Request();
                newRequest.Requestor_Name = User.Identity.Name;
                newRequest.Request_Status = true;
                newRequest.Employee_ID = pModel.EmployeeID;
                newRequest.Request_Type_Index = 2;
                db.Requests.Add(newRequest);
                db.SaveChanges();

                return RedirectToAction("Options", "Home");
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
        // POST: /Profile/Create
        [HttpPost]
        public ActionResult Create(CreateEmployeeModel cModel)
        {
            using (var db = new HR_QueryEntities())
            {
                cModel.DepartmentsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });
                cModel.LocationsDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });
                cModel.PositionTypeDropDown.Add(new SelectListItem { Text = "Select an option...", Value = "0" });

                var list = db.Departments.ToList();

                var items = from g in list select new SelectListItem { Value = g.Dept_ID.ToString(), Text = g.Dept_Name };

                foreach (var item in items)
                {
                    cModel.DepartmentsDropDown.Add(item);
                }

                var list2 = db.Locations.ToList();

                var items2 = from g in list2 select new SelectListItem { Value = g.Location_ID.ToString(), Text = g.Location_Name };

                foreach (var item in items2)
                {
                    cModel.LocationsDropDown.Add(item);
                }

                var list3 = db.Position_Types.ToList();

                var items3 = from g in list3 select new SelectListItem { Value = g.Position_Type_ID.ToString(), Text = g.Position_Type_Name };

                foreach (var item in items3)
                {
                    cModel.PositionTypeDropDown.Add(item);
                }

                if (ModelState.IsValid)
                {
                    Employee newEmployee = new Employee();

                    newEmployee.First_Name = cModel.EmployeeFirstName;
                    newEmployee.Last_Name = cModel.EmployeeLastName;
                    newEmployee.Department = int.Parse(cModel.DepartmentsDropDownSelection);
                    newEmployee.Location = int.Parse(cModel.LocationsDropDownSelection);
                    newEmployee.Position_Type = int.Parse(cModel.PositionTypeDropDownSelection);

                    Request newRequest = new Request();
                    newRequest.Requestor_Name = User.Identity.Name;
                    newRequest.Request_Status = true;
                    newRequest.Employee_ID = (db.Employees.Select(x => x).ToList()).Last().Employee_ID + 1;
                    newRequest.Request_Type_Index = 1;

                    db.Employees.Add(newEmployee);
                    db.Requests.Add(newRequest);
                    db.SaveChanges();
                    return RedirectToAction("Options", "Home");

                }

                ModelState.AddModelError("", "Please correct errors and resubmit.");
                return View(cModel);
            }
        }
        
        //
        // GET: /Pending/
        public ActionResult Pending()
        {
            
            using (var db = new HR_QueryEntities())
            {

                List<Request> query = db.Requests.Select(x => x).Where(x => x.Request_Status == true).ToList();

                if (query.Count == 0)
                    return RedirectToAction("NullTransactionList");

                foreach (Request r in query)
                {
                    PendingTransactionsModel model = new PendingTransactionsModel();

                    model.RequestID = r.Request_ID;
                    model.TransactionType = (db.Request_Types.Select(x => x).Where(x => x.Request_Type_ID == r.Request_Type_Index).First()).Request_Type_Name;
                    model.EmployeeLastName = (db.Employees.Select(x => x).Where(x => x.Employee_ID == r.Employee_ID).First()).Last_Name;

                    modelItems.Add(model);
                }

                return View(modelItems);
            }
        }

        //
        // GET: /Review/
        public ActionResult Review(int request_id = 0)
        {
            using (var db = new HR_QueryEntities())
            {
                RequestModel model = new RequestModel();
                Request selectedRequest = db.Requests.Select(x => x).Where(x => x.Request_ID == request_id).First();
                Employee associatedEmployee = db.Employees.Select(x => x).Where(x => x.Employee_ID == selectedRequest.Employee_ID).First();

                model.RequestID = selectedRequest.Request_ID;
                model.RequestorName = selectedRequest.Requestor_Name;
                model.TransactionType = (db.Request_Types.Select(x => x).Where(x => x.Request_Type_ID == selectedRequest.Request_Type_Index).First()).Request_Type_Name;
                model.EmployeeFirstName = associatedEmployee.First_Name;
                model.EmployeeLastName = associatedEmployee.Last_Name;
                model.DepartmentName = (db.Departments.Select(x => x).Where(x => x.Dept_ID == associatedEmployee.Department).First()).Dept_Name;
                model.LocationName = (db.Locations.Select(x => x).Where(x => x.Location_ID == associatedEmployee.Location).First()).Location_Name;
                model.PositionTypeName = (db.Position_Types.Select(x => x).Where(x => x.Position_Type_ID == associatedEmployee.Position_Type).First()).Position_Type_Name;

                return View(model);

            }
        }

        //
        // POST: /Review/
        [HttpPost]
        public ActionResult Review(RequestModel rModel)
        {
            using (var db = new HR_QueryEntities())
            {
                Request selectedRequest = db.Requests.Select(x => x).Where(x => x.Request_ID == rModel.RequestID).First();
                Employee associatedEmployee = db.Employees.Select(x => x).Where(x => x.Employee_ID == selectedRequest.Employee_ID).First();

                if (selectedRequest.Request_Type_Index == 1)
                    associatedEmployee.Available = true;

                if (selectedRequest.Request_Type_Index == 2)
                    associatedEmployee.Available = false;

                selectedRequest.Request_Status = false;

                db.SaveChanges();

                return RedirectToAction("Options", "Home");
            }
        }

        //
        // GET: /Review/
        
        public ActionResult Reject(int requestID = 0)
        {
            using (var db = new HR_QueryEntities())
            {
                Request selectedRequest = db.Requests.Select(x => x).Where(x => x.Request_ID == requestID).First();

                selectedRequest.Request_Status = false;

                db.SaveChanges();

                return RedirectToAction("Options", "Home");
            }
        }
    }
}
