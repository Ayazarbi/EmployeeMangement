using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{

    public delegate void Sendnotification();
    public class DepartmentRepository:IDepartmentRepository
    {
        
        EmployeeDbcontext _context;
        private readonly IHubContext<Myhub> _hubContext;
        private readonly UserManager<IdentityUser> userManager;

        public DepartmentRepository(EmployeeDbcontext context,IHubContext<Myhub> hubContext, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            this.userManager = userManager;
        }

        public void CreateDepartment(Department dpt)
        {
            _context._dptlist.Add(dpt);
            //if (_context.ChangeTracker.HasChanges())
            //{

            //    Sendnotification notify = new Sendnotification(Send);
            //    notify.Invoke();    
                
               
            //}   
            _context.SaveChanges();
            _hubContext.Clients.User("ccd63c10-d569-472c-a02f-72f9ac05d922").SendAsync("Receive", dpt.Department_name + " departmentAdded");
            _hubContext.Clients.All.SendAsync("Refresh");


        }

        private void Send()
        {
            
        }

        public async Task DeleteDepartment(int id)
        {
            var dept = _context._dptlist.Find(id);
            _context._dptlist.Remove(dept);
            foreach (var item in _context._employeelist)
            {
                if (item.DepartmentId == id)
                {
                    _context._employeelist.Remove(item);
                    var user = await userManager.FindByEmailAsync(item.Email);
                   var result= await userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        _context.SaveChanges();
                       await _hubContext.Clients.User("ccd63c10-d569-472c-a02f-72f9ac05d922").SendAsync("Receive", dept.Department_name + " department Deleted");
                       await  _hubContext.Clients.All.SendAsync("Refresh");

                    }
                }
            }
            

        }   

        public List<Department> GetallDepartments()
        {
           
            return _context._dptlist.ToList();   

                
        }

        public void UpdateDepartment(int id, Department dpt)
        {
            var dept = _context._dptlist.Attach(dpt);
            dept.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _hubContext.Clients.User("ccd63c10-d569-472c-a02f-72f9ac05d922").SendAsync("Receive", dpt.Department_name+" department Updated");
            _hubContext.Clients.All.SendAsync("Refresh");
        }

       
    }
}
