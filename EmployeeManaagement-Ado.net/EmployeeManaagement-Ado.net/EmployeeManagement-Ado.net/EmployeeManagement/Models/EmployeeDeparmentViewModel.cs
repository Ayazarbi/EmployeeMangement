using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeDeparmentViewModel
    {
        private Employee _employee;
        private List<Department> _departmentList;


        public EmployeeDeparmentViewModel(Employee employee,List<Department> departmentlist)
        {
            this._employee = employee;
            this._departmentList = departmentlist;
        }
    
    }
}
