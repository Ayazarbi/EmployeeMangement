using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
       

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
                _departmentRepository=departmentRepository;
            
        }
        public IActionResult Index()
        {
            var listOfDepartments = _departmentRepository.GetallDepartments();
            return View("/Views/Department/DepartmentListPage.cshtml",listOfDepartments);
        }

        [HttpGet]
        public IActionResult EditDepartment(int id)
        {
            var departmentTobeUpdated = _departmentRepository.GetallDepartments().FirstOrDefault(x => x.DepartmentId == id);

            return View("/Views/Department/EditDepartment.cshtml", departmentTobeUpdated);
        }
        
        [HttpPost]
        public IActionResult EditDepartment(int id,Department det)
        {
            if (ModelState.IsValid)
            {
                det.DepartmentId = id;
                _departmentRepository.UpdateDepartment(id, det);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteDepartment(int id)
        {
            _departmentRepository.DeleteDepartment(id);
           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View("/Views/Department/Create.cshtml");
        }


        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                //dept.DepartmentId = (_departmentRepository.GetallDepartments().Count()) + 1;
                _departmentRepository.CreateDepartment(dept);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
