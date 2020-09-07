using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Surname cannot be more than 20 character")]
        public String Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Surname cannot be more than 20 character")]
        public String Surname { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = " cannot be more than 100 character")]
        public String Address { get; set; }


        [Required]
        [MinLength(10,ErrorMessage= "length must be 10")]
        [MaxLength(10,ErrorMessage ="length must be 10")]
        public string Contactnumber { get; set; }

        public Department Department { get; set; }

    
        public int DepartmentId { get; set; }
       }
}
