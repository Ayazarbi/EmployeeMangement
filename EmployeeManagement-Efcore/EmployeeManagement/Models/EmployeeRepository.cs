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
        public EmployeeRepository(EmployeeDbcontext context)
        {
            _context = context;
        }

        public List<Employee> CreateEmployee(Employee newEmployee)
        {
            _context._employeelist.Add(newEmployee);
            _context.SaveChanges();
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
            return modifiedEmployee;
        }
    }
}
