using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class RoleModel
    {
        [Required]
        public String RoleName { get; set; }
    }
}
