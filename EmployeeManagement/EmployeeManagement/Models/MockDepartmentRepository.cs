using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockDepartmentRepository : IDepartmentRepository
    {
        private List<Department> _listOfDepartment;
        private IEmployeeRepository _listOfEmployee;
        public MockDepartmentRepository(IEmployeeRepository listOfEmployee)
        {
            _listOfEmployee = listOfEmployee;
            _listOfDepartment = new List<Department>()
            {
                new Department{DepartmentId=1,Department_name="HR"},
                new Department{DepartmentId=2,Department_name="Engineer"},
                new Department{DepartmentId=3, Department_name="Security"}
            };
        }
        
        public void CreateDepartment(Department dpt)
        {
            _listOfDepartment.Add(dpt);
        }

        public void DeleteDepartment(int id)
        {
            var departmentTobeRemoved = _listOfDepartment.FirstOrDefault(x => x.DepartmentId == id);
            _listOfDepartment.Remove(departmentTobeRemoved);
            foreach (var item in _listOfEmployee.GetAllEmployees().ToList())
            {
                if (item.Department.DepartmentId == id)
                {
                    _listOfEmployee.GetAllEmployees().Remove(item);
                }
            }

        }

        public List<Department> GetallDepartments()
        {
            return _listOfDepartment; 
        }

        public void UpdateDepartment(int id,Department dpt)
        {
            var departmentTobeUpdated = _listOfDepartment.FirstOrDefault(x => x.DepartmentId == id);
            departmentTobeUpdated.DepartmentId = dpt.DepartmentId;
            departmentTobeUpdated.Department_name = dpt.Department_name;

            foreach(var item in _listOfEmployee.GetAllEmployees())
            {
                if (item.Department.DepartmentId == dpt.DepartmentId)
                {
                        item.Department.Department_name=dpt.Department_name;
                }
            }

        }
    }
}
