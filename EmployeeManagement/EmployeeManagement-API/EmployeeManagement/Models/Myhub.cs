using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Myhub:Hub
    {

             
        
        private readonly EmployeeDbcontext _context;
        public Myhub(EmployeeDbcontext context)
        {
            this._context = context;
            
        }
        public override async Task OnConnectedAsync()
        {
                // var name=Context.User.Identity.Name;
                // await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            
            // else if (Context.User.IsInRole("HR"))
            // {
            //     await Groups.AddToGroupAsync(Context.ConnectionId, "HR");
            // }
            // else if (this.Context.User.IsInRole("Employee"))
            // {
            //     string dept = _context._employeelist.Where(e => e.Email == Context.User.Identity.Name).First().Department.Department_name;
            //     var grpName = "Employee" + dept;
            //     await this.Groups.AddToGroupAsync(Context.ConnectionId, grpName);

            // }
            // await base.OnConnectedAsync();
        
    }

    public async Task<string> Setrole(string role,string deptid)
        {   
            if(role=="Admin")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            
            }
            if(role=="HR"){
                await Groups.AddToGroupAsync(Context.ConnectionId, "HR");
           
            }
            if(role=="Employee"){

                await Groups.AddToGroupAsync(Context.ConnectionId,"Employee"+deptid);
        
            }
           
            return role;
        }
    }
}
