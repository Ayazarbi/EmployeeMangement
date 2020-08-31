using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IDepartmentRepository
    {
         List<Department> GetallDepartments();
        void DeleteDepartment(int id);
        void UpdateDepartment(int id,Department dpt);

        void CreateDepartment(Department dpt);
        
        
    }
}
