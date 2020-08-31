using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
   public interface IEmployeeRepository
    {


        List<Employee> GetAllEmployees();

        List<Employee> CreateEmployee(Employee newEmployee);

        Employee UpdateEmployee(Employee modifiedEmployee,int id);
        
        void DeleteEmployee(int id);




        
    }
}
