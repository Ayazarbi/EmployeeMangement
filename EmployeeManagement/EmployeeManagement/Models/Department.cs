using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Department
    {

        public int DepartmentId { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage="nvalid length")]
        public String Department_name { get; set; }

    }
}
