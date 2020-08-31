using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeedata;
        private IDepartmentRepository _departmentdata;

        public EmployeeController(IEmployeeRepository employeedata,IDepartmentRepository departmentRepository)
        {
            _employeedata = employeedata;
            _departmentdata = departmentRepository;
        }
        public IActionResult Index()
        {
           var employeesList = _employeedata.GetAllEmployees();

            return View("/Views/Employee/EmployeeListPage.cshtml", employeesList);
        }

        public IActionResult DeleteEmployee(int id)
        {
            _employeedata.DeleteEmployee(id);
            
            return View("/Views/Employee/EmployeeListPage.cshtml", _employeedata.GetAllEmployees());
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            ViewBag.Departments = _departmentdata.GetallDepartments();

            var employeeTobeUpdated =_employeedata.GetAllEmployees().FirstOrDefault(x => x.EmployeeId == id);

            return View("/Views/Employee/EditEmployee.cshtml",employeeTobeUpdated) ;
        }
        [HttpPost]
        public IActionResult EditEmployee(int id, Employee emp)
        {
            if (ModelState.IsValid)
            {
                emp.EmployeeId = id;
                var Department = _departmentdata.GetallDepartments().Find(x => x.Department_name == emp.Department.Department_name);
                emp.Department = Department;
                _employeedata.UpdateEmployee(emp, id);

                return RedirectToAction("Index");
            }
            return View();
            

        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentdata.GetallDepartments();
           
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {

            if (ModelState.IsValid)
            {

                var Department = (_departmentdata.GetallDepartments()).Find(x => x.Department_name == emp.Department.Department_name);
                emp.EmployeeId = ((_employeedata.GetAllEmployees()).Count + 1);
                emp.Department = Department;

                var result = _employeedata.CreateEmployee(emp);
                return View("EmployeeListPage", result);
            }
            return View();
            
        }
    }
}
