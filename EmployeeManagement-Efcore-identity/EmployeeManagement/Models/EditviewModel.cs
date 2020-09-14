using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EditviewModel
    {
        public EditviewModel()
        {
            Users = new List<string>();
            
        }
        public string Id { get; set; }
        public string RoleName { get; set; }
        public List<string> Users;
        public string Username { get; set; }


    }
}
