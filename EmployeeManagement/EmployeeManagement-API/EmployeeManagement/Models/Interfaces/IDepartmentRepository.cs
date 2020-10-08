using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IDepartmentRepository
    {
         List<Department> GetallDepartments();
        Task DeleteDepartment(int id);
        Department UpdateDepartment(int id,Department dpt);

        Department CreateDepartment(Department dpt);
        
        
    }
}
