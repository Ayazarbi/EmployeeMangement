using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeedata;
        private IDepartmentRepository _departmentdata;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
            

        public EmployeeController(IEmployeeRepository employeedata, IDepartmentRepository departmentRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _employeedata = employeedata;
            _departmentdata = departmentRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
           
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {

       
            
            
            if ( User.IsInRole("Employee"))
            {

                var empname = User.Identity.Name;
                var departmentId = (_employeedata.GetAllEmployees().FirstOrDefault(x => x.Email == (empname))).DepartmentId;
                var emplist = _employeedata.GetAllEmployees().Where(x => x.DepartmentId == departmentId);
                var deptlist = _departmentdata.GetallDepartments();

                foreach (var emp in emplist)
                {
                    emp.Department = (deptlist.FirstOrDefault(x => x.DepartmentId == emp.DepartmentId));
                }

                return View("/Views/Employee/EmployeeListPage.cshtml", emplist);

            }

            else
            {

               

                var employeesList = _employeedata.GetAllEmployees();
                var deptlist = _departmentdata.GetallDepartments();

                foreach (var emp in employeesList)
                {
                    emp.Department = (deptlist.FirstOrDefault(x => x.DepartmentId == emp.DepartmentId));
                }

                return View("/Views/Employee/EmployeeListPage.cshtml", employeesList);
            }
        }

        [HttpGet]
        public ActionResult Getallemployees()
        {
            return Ok(_employeedata.GetAllEmployees
                ());
        }

        [Authorize(Roles = "Admin,HR")]
       
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            
           var employee= _employeedata.GetAllEmployees().FirstOrDefault(x=>x.EmployeeId==id);
            var user = await userManager.FindByEmailAsync(employee.Email);
            var result=await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _employeedata.DeleteEmployee(id);
                return RedirectToAction("Index");
                
            }
            return View("Error");
        }

        [Authorize(Roles = "Admin,HR,Employee")]
        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            ViewBag.Departments = _departmentdata.GetallDepartments();

            var employeeTobeUpdated = _employeedata.GetAllEmployees().FirstOrDefault(x => x.EmployeeId == id);

            return View("/Views/Employee/EditEmployee.cshtml", employeeTobeUpdated);
        }

        [Authorize(Roles = "Admin,HR,Employee")]
        [HttpPost]
        public async Task<IActionResult> EditEmployee(int id, Employee emp)
        {

            var user =await userManager.FindByEmailAsync(emp.Email);
            user.UserName = emp.Email;
            if (ModelState.IsValid)
            {
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded) {
                    emp.EmployeeId = id;
                    var Department = _departmentdata.GetallDepartments().Find(x => x.Department_name == emp.Department.Department_name);
                    emp.Department = Department;
                    _employeedata.UpdateEmployee(emp, id);
                    return RedirectToAction("Index");
                }



            }
            ModelState.AddModelError("", "Something Went Wrong Try again");

          
            return View();


        }

        [Authorize(Roles = "Admin,HR")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentdata.GetallDepartments().ToList();

            return View("Create");
        }

        [Authorize(Roles = "Admin,HR")]
        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            var email = emp.Name + emp.Surname + "@gmail.com";
            var user =await userManager.FindByEmailAsync(email);
            
           
                if (ModelState.IsValid)
                {
                if (user == null)
                {
                    var newuser = new IdentityUser
                    {
                        Email = email,
                        UserName = email

                    };

                    var result = await userManager.CreateAsync(newuser, "aA123456789$");
                    if (result.Succeeded)
                    {

                       await userManager.AddToRoleAsync(newuser, "Employee");
                        var Department = (_departmentdata.GetallDepartments()).Find(x => x.Department_name == emp.Department.Department_name);
                        //emp.EmployeeId = ((_employeedata.GetAllEmployees()).Count + 1);
                        emp.Department = Department;
                        emp.Email = email;

                        var resultt = _employeedata.CreateEmployee(emp);
                        return View("EmployeeListPage", resultt);

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                ModelState.AddModelError("", "User already exist with same name Try another name");

            }
            ViewBag.Departments = _departmentdata.GetallDepartments().ToList();
            return View();

        }

        
        [HttpGet]
        public ActionResult Editemail(int id)
        {
            var emp=_employeedata.GetAllEmployees().FirstOrDefault(x=>x.EmployeeId==id);
            var editemailview = new Editemailviewmodel();
            editemailview.Oldemail = emp.Email;
            return View(editemailview);

        }

        [HttpPost]
        public async Task<ActionResult> Editemail(Editemailviewmodel model)
        {
            var user =await userManager.FindByEmailAsync(model.Newemail);
            if (user == null)
            {
                var exisingUser =await userManager.FindByEmailAsync(model.Oldemail);
                exisingUser.UserName = model.Newemail;
                var token = await userManager.GenerateChangeEmailTokenAsync(exisingUser, model.Newemail);
                var result = await userManager.ChangeEmailAsync(exisingUser, model.Newemail, token);
                 if (result.Succeeded)
                {
                    await userManager.UpdateAsync(exisingUser);
                    var emp = _employeedata.GetAllEmployees().FirstOrDefault(x => x.Email == model.Oldemail);
                    emp.Email = model.Newemail;
                    _employeedata.UpdateEmployee(emp, emp.EmployeeId);
                    ViewBag.Departments = _departmentdata.GetallDepartments().ToList();

                    return RedirectToAction("Logout", "Account");

                }
            }
            ModelState.AddModelError("", "Email already exist");

            ViewBag.Departments = _departmentdata.GetallDepartments().ToList();
            return View(model);

        }


        //public ActionResult ListEmployee()
        //{
        //    if (signInManager.IsSignedIn(User))
        //    {

        //}
    }
}