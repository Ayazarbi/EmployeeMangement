using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class DepartmentRepository:IDepartmentRepository
    {
        EmployeeDbcontext _context;
        public DepartmentRepository(EmployeeDbcontext context)
        {
            _context = context;
        }

        public void CreateDepartment(Department dpt)
        {
            _context._dptlist.Add(dpt);
            _context.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var dept = _context._dptlist.Find(id);
            _context._dptlist.Remove(dept);
            _context.SaveChanges();
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
        }
    }
}
