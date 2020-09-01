using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeDbcontext:DbContext
    {
        public EmployeeDbcontext(DbContextOptions<EmployeeDbcontext> options) : base(options)
        {

        }

        public DbSet<Employee> _employeelist { get; set; }
        public DbSet<Department> _dptlist { get; set; }
    
    }

}
