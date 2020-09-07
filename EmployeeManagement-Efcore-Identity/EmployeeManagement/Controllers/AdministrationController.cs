using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }
       
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                var newrole = new IdentityRole()
                {
                    Name = role.RoleName,
                };

                var result=await _roleManager.CreateAsync(newrole);
                if (result.Succeeded)
                {
                  return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            
            return View();
        }

        [Authorize(Roles="Admin")]
        public  IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async  Task<IActionResult> EditRole(string id)
        {

            ViewBag.Allusers = _userManager.Users.ToList();
            if (ModelState.IsValid) {
                
                var role = await _roleManager.FindByIdAsync(id);

                var editModel = new EditviewModel()
                {
                    Id = id,
                    RoleName = role.Name,


                };
                foreach (var user in _userManager.Users)
                {

                   
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        editModel.Users.Add(user.UserName);
                    }
                }
                return View(editModel);
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> EditRole(EditviewModel model)
        {

            if (ModelState.IsValid) {
                var role = await _roleManager.FindByIdAsync(model.Id);
                
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                   return RedirectToAction("ListRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddUserinRole(EditviewModel model)
        {
            var role =await _roleManager.FindByIdAsync(model.Id);

            var user = _userManager.Users.ToList().FirstOrDefault(x => x.UserName == model.Username);
            var result= await _userManager.AddToRoleAsync(user,role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("EditRole",role);
            }
            return View();
        
        }

         
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteuserFromRole(EditviewModel model)
        {

            var role = await _roleManager.FindByIdAsync(model.Id);

            var user = _userManager.Users.ToList().FirstOrDefault(x => x.UserName == model.Username);

            var result=await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("EditRole", role);
            }
            return View();
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                var result=await _roleManager.DeleteAsync(role);
                {
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        
    }
}
