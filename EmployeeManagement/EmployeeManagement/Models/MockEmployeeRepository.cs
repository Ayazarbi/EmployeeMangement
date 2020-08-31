using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employeeList;
       
        public MockEmployeeRepository()
        {
            employeeList = new List<Employee>()
            {
               
            };
        }

        public List<Employee> CreateEmployee(Employee newEmployee)
        {
            employeeList.Add(newEmployee);
            return employeeList;
        }

        public void DeleteEmployee(int id)
        {
            var employeeTobeRemoved = employeeList.FirstOrDefault(x => x.EmployeeId == id);

            employeeList.Remove(employeeTobeRemoved);
            
        }

        public List<Employee> GetAllEmployees()
        {
            return employeeList;
        }

        
        public Employee UpdateEmployee(Employee modifiedEmployee,int id)
        {
            var employeeTobeUpdated = employeeList.FirstOrDefault(x => x.EmployeeId == modifiedEmployee.EmployeeId);
            employeeTobeUpdated.EmployeeId = modifiedEmployee.EmployeeId;
            employeeTobeUpdated.Address = modifiedEmployee.Address;
            employeeTobeUpdated.Contactnumber = modifiedEmployee.Contactnumber;
            employeeTobeUpdated.Department = modifiedEmployee.Department;
            employeeTobeUpdated.Surname = modifiedEmployee.Surname;
            employeeTobeUpdated.Name = modifiedEmployee.Name;



            return employeeTobeUpdated;
        }
    }
}
