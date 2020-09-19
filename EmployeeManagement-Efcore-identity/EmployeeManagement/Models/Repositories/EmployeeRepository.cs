using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeRepository:IEmployeeRepository
    {
        EmployeeDbcontext _context;
        private readonly IHubContext<Myhub> _hubContext;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeRepository(EmployeeDbcontext context, IHubContext<Myhub> hubContext, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public List<Employee> CreateEmployee(Employee newEmployee)
        {
            _context._employeelist.Add(newEmployee);
            _context.SaveChanges();

            foreach (var emp in GetAllEmployees())
            {
                
                
                if (emp.DepartmentId == newEmployee.DepartmentId)
                {
                    var subemp = _userManager.FindByEmailAsync(emp.Email).Result;
                  
                    _hubContext.Clients.User(subemp.Id).SendAsync("Receive", newEmployee.Name+"Employee Added");
                }
            }

            _hubContext.Clients.All.SendAsync("Refresh");
            return _context._employeelist.ToList();

        }

        public void DeleteEmployee(int id)
        {
            var emp = _context._employeelist.Find(id);
            if (emp != null)
            {
                _context.Remove(emp);
            }
             _context.SaveChanges();
            _hubContext.Clients.Users("86938f77-c4e2-46c3-a9f0-de5f0c4f26aa", "ccd63c10-d569-472c-a02f-72f9ac05d922").SendAsync("Receive", emp.Name+" Employee deleted");
            _hubContext.Clients.All.SendAsync("Refresh");
        }

        public List<Employee> GetAllEmployees()
        {
            var emplist = _context._employeelist.ToList();
           

            return emplist;
        }

        public Employee UpdateEmployee(Employee modifiedEmployee, int id)
        {
            var employee = _context._employeelist.Attach(modifiedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _hubContext.Clients.Users("86938f77-c4e2-46c3-a9f0-de5f0c4f26aa", "ccd63c10-d569-472c-a02f-72f9ac05d922").SendAsync("Receive",  modifiedEmployee.Name+ " Employee Updated");
            _hubContext.Clients.All.SendAsync("Refresh");

            return modifiedEmployee;
        
        }
    }
}
