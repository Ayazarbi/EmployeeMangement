using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeManagement.Controllers
{
     [ApiController]
     [AllowAnonymous]
     [Route("Department")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        EmployeeDbcontext _context;
        private readonly IHubContext<Myhub> hubContext;
        private readonly UserManager<IdentityUser> userManager;

        public DepartmentController(IDepartmentRepository departmentRepository,EmployeeDbcontext context,IHubContext<Myhub> hubContext,UserManager<IdentityUser> userManager)
        {
            _departmentRepository = departmentRepository;
           _context=context;
            this.hubContext = hubContext;
            this.userManager = userManager;
        }
         
         
        [HttpPost]
        public ActionResult Create(Department department)
        {
            _context._dptlist.Add(department);
             _context.SaveChanges();
             hubContext.Clients.Group("HR").SendAsync("Receive",department.Department_name+"Department Added");
            return Ok(department);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> List(){

          return Ok(_departmentRepository.GetallDepartments());  

        }

        // [HttpGet("{id}")]
        
        // public ActionResult<Department> Getbyid(int id){

        //   return Ok(_departmentRepository.GetallDepartments().FirstOrDefault(x=>x.DepartmentId==id));  

        // }    

    [HttpPut("{id}")]
     public ActionResult<Department> Edit(int id,Department dept){

           hubContext.Clients.All.SendAsync("Receive",dept.Department_name+"Department Editted");
          return Ok(_departmentRepository.UpdateDepartment(id,dept));  

        }

        [HttpDelete("{id}")]

       
        public  async Task< ActionResult<Department>> Delete(int id){

                var dept=_context._dptlist.ToList().FirstOrDefault(x=>x.DepartmentId==id);

                foreach (var item in _context._employeelist.ToList()){
                
                    if(item.DepartmentId==id.ToString()){

                        var user=await userManager.FindByEmailAsync(item.Email);
                        if(user!=null){
                            var result=await userManager.DeleteAsync(user);
                            _context.Remove(item);
                        }
                        
                          _context.SaveChanges();
                        
                          
                      


                    }

             
                }
                        _context.Remove(dept);
                _context.SaveChanges();
                await hubContext.Clients.All.SendAsync("Receive",dept.Department_name+"Department Deleted");
                return Ok(dept);
                
                
               
                
                

        }    

    
    }
}

//         [HttpGet]
//         [AllowAnonymous]   
//         public ActionResult Getlistofdepartments()
//         {
//             var list=_departmentRepository.GetallDepartments();
//             return Ok(list);
//         }
         
//         // [HttpGet]
//         // public ActionResult GetdepartmentbyId(int id)
//         // {
//         //     var department = _departmentRepository.GetallDepartments().FirstOrDefault(x=>x.DepartmentId==id);

//         //     return Ok(department);


//         // }

//         [HttpDelete]
//         public ActionResult DeletedepartmentbyID(int id)
//         {
//             var deletedDepartment = _departmentRepository.DeleteDepartment(id);

//             return Ok(deletedDepartment);
//         }

//         [HttpPatch]
//         public ActionResult UpdatedepartmentbyId(Department dept)
//         {
//             var updatedDepartment = _departmentRepository.UpdateDepartment(dept.DepartmentId, dept);

//             return Ok(updatedDepartment);
//         }

//         [HttpPost]
//         public ActionResult Createnewdepartment(Department dept)
//         {
//            console.writeline(dept.Department_name);
//            var department= _departmentRepository.CreateDepartment(dept);
//             return Ok(department);
//         }

//         [Authorize(Roles = "Admin,HR")]
//         public IActionResult Index()
//         {
//             var listOfDepartments = _departmentRepository.GetallDepartments();
//             return View("/Views/Department/DepartmentListPage.cshtml", listOfDepartments);
//         }

//         // [HttpGet]
//         // public ActionResult GetallDepartments()
//         // {
//         //     var listOfDepartments = _departmentRepository.GetallDepartments();
//         //     return Ok(listOfDepartments);

//         // }

//         [Authorize(Roles = "Admin")]
//         // [HttpGet]
//         // public IActionResult EditDepartment(int id)
//         // {
//         //     var departmentTobeUpdated = _departmentRepository.GetallDepartments().FirstOrDefault(x => x.DepartmentId == id);

//         //     return View("/Views/Department/EditDepartment.cshtml", departmentTobeUpdated);
//         // }

//         [Authorize(Roles = "Admin")]
//         [HttpPost]
//         public IActionResult EditDepartment(int id, Department det)
//         {
//             if (ModelState.IsValid)
//             {
//                 det.DepartmentId = id;
//                 _departmentRepository.UpdateDepartment(id, det);
//                 return RedirectToAction("Index");
//             }
//             return View();
//         }

//         [Authorize(Roles = "Admin")]
//         public IActionResult DeleteDepartment(int id)
//         {
//             _departmentRepository.DeleteDepartment(id);

//             return RedirectToAction("Index");
//         }

//         // [Authorize(Roles = "Admin")]
//         // [HttpGet]
//         // public IActionResult Create()
//         // {

//         //     return View("/Views/Department/Create.cshtml");
//         // }


// //         [Authorize(Roles = "Admin")]
// //         [HttpPost]
// //         public IActionResult Create(Department dept)
// //         {
// //             if (ModelState.IsValid)
// //             {
// //                 //dept.DepartmentId = (_departmentRepository.GetallDepartments().Count()) + 1;
// //                 _departmentRepository.CreateDepartment(dept);
// //                 return RedirectToAction("Index");
// //             }
// //             return View();
// //         }
//     }
// }