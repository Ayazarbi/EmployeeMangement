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
        public DepartmentRepository(EmployeeDbcontext context,IHubContext<Myhub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
            
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
           
        }

        private void Send()
        {
            
        }

        public void DeleteDepartment(int id)
        {
            var dept = _context._dptlist.Find(id);
            _context._dptlist.Remove(dept);
            _context._employeelist.ToList().RemoveAll(x => x.DepartmentId == id);
            _context.SaveChanges();
            _hubContext.Clients.User("ccd63c10-d569-472c-a02f-72f9ac05d922").SendAsync("Receive", dept.Department_name + " department Deleted");


        }   

        public List<Department> GetallDepartments()
        {
            var v=_hubContext.Clients.All.SendAsync("SendNotification");
            return _context._dptlist.ToList();   

                
        }

        public void UpdateDepartment(int id, Department dpt)
        {
            var dept = _context._dptlist.Attach(dpt);
            dept.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _hubContext.Clients.User("ccd63c10-d569-472c-a02f-72f9ac05d922").SendAsync("Receive", dpt.Department_name+" department Updated");
        }
    }
}
