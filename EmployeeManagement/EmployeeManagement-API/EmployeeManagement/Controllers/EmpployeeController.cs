﻿using System;
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
using Microsoft.AspNetCore.SignalR;

namespace EmployeeManagement.Controllers
{
    
     
    [ApiController]
    [AllowAnonymous]
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeedata;
        private IDepartmentRepository _departmentdata;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly EmployeeDbcontext _context;
        private readonly IHubContext<Myhub> hubContext;

        public EmployeeController(IEmployeeRepository employeedata, IDepartmentRepository departmentRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,EmployeeDbcontext context,IHubContext<Myhub> hubContext)
        {
            _employeedata = employeedata;
            _departmentdata = departmentRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._context = context;
            this.hubContext = hubContext;
        }

         
        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
               var email = employee.Email;
            var user =await userManager.FindByEmailAsync(email);
            
           
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
                          _context._employeelist.Add(employee);
                         _context.SaveChanges();
                        await hubContext.Clients.Group("Employee"+employee.DepartmentId).SendAsync("Receive",employee.Name+" Employee Added");
               
                         return Ok(employee);
                    }
                }
            
                  return BadRequest("User Already Exist with same Email please try using different email");
                

           
           
           
           
          
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> List(){

         
         
          return Ok(_employeedata.GetAllEmployees());  

        }

        [HttpGet("{id}")]
        
        public ActionResult<Department> Getbyid(int id){

          return Ok(_employeedata.GetAllEmployees().FirstOrDefault(x=>x.EmployeeId==id));  

        }    

    [HttpPut("{id}")]
     public ActionResult<Employee> Edit(int id,Employee employee){

           hubContext.Clients.Groups("Admin","HR").SendAsync("Receive",employee.Name+" Employee Edited");
          return Ok(_employeedata.UpdateEmployee(employee,id));  

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Department>> Delete(int id){

                 var employee=_context._employeelist.FirstOrDefault(x=>x.EmployeeId==id);
                 var user = await userManager.FindByEmailAsync(employee.Email);
                  if(user!=null){
                    var result= await userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                      _context.Remove(employee);  
                       _context.SaveChanges();
                        await hubContext.Clients.All.SendAsync("Receive",employee.Name+" Employee Deleted");
                        return Ok(employee);
                    }
                  }
                   
                    return BadRequest("Unable to delete");

        }    

    
    }
}


        // [HttpGet]
        // public ActionResult GetemployeebyId(int id)
        // {
        //     var employee = _employeedata.GetAllEmployees().FirstOrDefault(x => x.EmployeeId == id);

        //     return Ok(employee);
        // }

        // [HttpDelete]
        // public ActionResult DeleteemployeebyId(int id)
        // {
        //    var emp= _employeedata.DeleteEmployee(id);
        //     if (emp == null)
        //     {
        //         throw new ArgumentNullException("User not found");
        //     }
        //     return Ok(emp);
        // }


        // [HttpPatch]
        // public ActionResult  EditemployeebyId(int Id,Employee emp)
        // {
        //    var UpdatedEmp=_employeedata.UpdateEmployee(emp, Id);
        //     if (UpdatedEmp == null)
        //     {
        //         throw new ArgumentNullException("User not found");
        //     }
        //     return Ok(UpdatedEmp);


        // }

        // [HttpPost]

        // public ActionResult CreatenewEmployee(Employee emp)
        // {
        //   var emplist=  _employeedata.CreateEmployee(emp);
        //     if (emplist == null)
        //     {
        //         throw new ArgumentNullException("User not found");
        //     }
        //     return Ok(emplist);

        // }
        // [Authorize]
        // public async Task<IActionResult> Index()
        // {

       
            
            
        //     if ( User.IsInRole("Employee"))
        //     {

        //         var empname = User.Identity.Name;
        //         var departmentId = (_employeedata.GetAllEmployees().FirstOrDefault(x => x.Email == (empname))).DepartmentId;
        //         var emplist = _employeedata.GetAllEmployees().Where(x => x.DepartmentId == departmentId);
        //         var deptlist = _departmentdata.GetallDepartments();
        //         ViewBag.Department = departmentId;

        //         foreach (var emp in emplist)
        //         {
        //             emp.Department = (deptlist.FirstOrDefault(x => x.DepartmentId == emp.DepartmentId));
        //         }

        //         return View("/Views/Employee/EmployeeListPage.cshtml", emplist);

        //     }

        //     else
        //     {

               

        //         var employeesList = _employeedata.GetAllEmployees();
        //         var deptlist = _departmentdata.GetallDepartments();

        //         foreach (var emp in employeesList)
        //         {
        //             emp.Department = (deptlist.FirstOrDefault(x => x.DepartmentId == emp.DepartmentId));
        //         }

        //         return View("/Views/Employee/EmployeeListPage.cshtml", employeesList);
        //     }
        // }

        // [HttpGet]
        // public ActionResult Getallemployees()
        // {
        //     var employeesList = _employeedata.GetAllEmployees();
        //     var deptlist = _departmentdata.GetallDepartments();

        //     foreach (var emp in employeesList)
        //     {
        //         emp.Department = (deptlist.FirstOrDefault(x => x.DepartmentId == emp.DepartmentId));
        //     }

        //     return Ok(employeesList); 
        // }

        // [Authorize(Roles = "Admin,HR")]
       
        // public async Task<IActionResult> DeleteEmployee(int id)
        // {
            
        //    var employee= _employeedata.GetAllEmployees().FirstOrDefault(x=>x.EmployeeId==id);
        //     var user = await userManager.FindByEmailAsync(employee.Email);
        //     var result=await userManager.DeleteAsync(user);
        //     if (result.Succeeded)
        //     {
        //         _employeedata.DeleteEmployee(id);
        //         return RedirectToAction("Index");
                
        //     }
        //     return View("Error");
        // }

        // [Authorize(Roles = "Admin,HR,Employee")]
        // [HttpGet]
        // public IActionResult EditEmployee(int id)
        // {
        //     ViewBag.Departments = _departmentdata.GetallDepartments();

        //     var employeeTobeUpdated = _employeedata.GetAllEmployees().FirstOrDefault(x => x.EmployeeId == id);

        //     return View("/Views/Employee/EditEmployee.cshtml", employeeTobeUpdated);
        // }

        // [Authorize(Roles = "Admin,HR,Employee")]
        // [HttpPost]
        // public async Task<IActionResult> EditEmployee(int id, Employee emp)
        // {

        //     var user =await userManager.FindByEmailAsync(emp.Email);
        //     user.UserName = emp.Email;
        //     if (ModelState.IsValid)
        //     {
        //         var result = await userManager.UpdateAsync(user);
        //         if (result.Succeeded) {
        //             emp.EmployeeId = id;
        //             var Department = _departmentdata.GetallDepartments().Find(x => x.Department_name == emp.Department.Department_name);
        //             emp.Department = Department;
        //             _employeedata.UpdateEmployee(emp, id);
        //             return RedirectToAction("Index");
        //         }



        //     }
        //     ModelState.AddModelError("", "Something Went Wrong Try again");

          
        //     return View();


        // }

        // [Authorize(Roles = "Admin,HR")]
        // [HttpGet]
        // public IActionResult Create()
        // {
        //     ViewBag.Departments = _departmentdata.GetallDepartments().ToList();

        //     return View("Create");
        // }

        // [Authorize(Roles = "Admin,HR")]
        // [HttpPost]
        // public async Task<IActionResult> Create(Employee emp)
        // {
        //     var email = emp.Name + emp.Surname + "@gmail.com";
        //     var user =await userManager.FindByEmailAsync(email);
            
           
        //         if (ModelState.IsValid)
        //         {
        //         if (user == null)
        //         {
        //             var newuser = new IdentityUser
        //             {
        //                 Email = email,
        //                 UserName = email

        //             };

        //             var result = await userManager.CreateAsync(newuser, "aA123456789$");
        //             if (result.Succeeded)
        //             {

        //                await userManager.AddToRoleAsync(newuser, "Employee");
        //                 var Department = (_departmentdata.GetallDepartments()).Find(x => x.Department_name == emp.Department.Department_name);
        //                 //emp.EmployeeId = ((_employeedata.GetAllEmployees()).Count + 1);
        //                 emp.Department = Department;
        //                 emp.Email = email;

        //                 var resultt = _employeedata.CreateEmployee(emp);
        //                 return RedirectToAction("Index");

        //             }
        //             foreach (var error in result.Errors)
        //             {
        //                 ModelState.AddModelError("", error.Description);
        //             }
        //         }
        //         ModelState.AddModelError("", "User already exist with same name Try another name");

        //     }
        //     ViewBag.Departments = _departmentdata.GetallDepartments().ToList();
        //     return View();

        // }

        
        // [HttpGet]
        // public ActionResult Editemail(int id)
        // {
        //     var emp=_employeedata.GetAllEmployees().FirstOrDefault(x=>x.EmployeeId==id);
        //     var editemailview = new Editemailviewmodel();
        //     editemailview.Oldemail = emp.Email;
        //     return View(editemailview);

        // }

        // [HttpPost]
        // public async Task<ActionResult> Editemail(Editemailviewmodel model)
        // {
        //     var user =await userManager.FindByEmailAsync(model.Newemail);
        //     if (user == null)
        //     {
        //         var exisingUser =await userManager.FindByEmailAsync(model.Oldemail);
        //         exisingUser.UserName = model.Newemail;
        //         var token = await userManager.GenerateChangeEmailTokenAsync(exisingUser, model.Newemail);
        //         var result = await userManager.ChangeEmailAsync(exisingUser, model.Newemail, token);
        //          if (result.Succeeded)
        //         {
        //             await userManager.UpdateAsync(exisingUser);
        //             var emp = _employeedata.GetAllEmployees().FirstOrDefault(x => x.Email == model.Oldemail);
        //             emp.Email = model.Newemail;
        //             _employeedata.UpdateEmployee(emp, emp.EmployeeId);
        //             ViewBag.Departments = _departmentdata.GetallDepartments().ToList();

        //             return RedirectToAction("Logout", "Account");

        //         }
        //     }
        //     ModelState.AddModelError("", "Email already exist");

        //     ViewBag.Departments = _departmentdata.GetallDepartments().ToList();
        //     return View(model);

        // }


        //public ActionResult ListEmployee()
        //{
        //    if (signInManager.IsSignedIn(User))
        //    {

        //}
    