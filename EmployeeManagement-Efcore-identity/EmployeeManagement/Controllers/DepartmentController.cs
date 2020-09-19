﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
      

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
           
        }
        [Authorize(Roles = "Admin,HR")]
        public IActionResult Index()
        {
            var listOfDepartments = _departmentRepository.GetallDepartments();
            return View("/Views/Department/DepartmentListPage.cshtml", listOfDepartments);
        }

        [HttpGet]
        public ActionResult GetallDepartments()
        {
            var listOfDepartments = _departmentRepository.GetallDepartments();
            return Ok(listOfDepartments);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditDepartment(int id)
        {
            var departmentTobeUpdated = _departmentRepository.GetallDepartments().FirstOrDefault(x => x.DepartmentId == id);

            return View("/Views/Department/EditDepartment.cshtml", departmentTobeUpdated);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditDepartment(int id, Department det)
        {
            if (ModelState.IsValid)
            {
                det.DepartmentId = id;
                _departmentRepository.UpdateDepartment(id, det);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDepartment(int id)
        {
            _departmentRepository.DeleteDepartment(id);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {

            return View("/Views/Department/Create.cshtml");
        }


        [Authorize(Roles = "Admin")]
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