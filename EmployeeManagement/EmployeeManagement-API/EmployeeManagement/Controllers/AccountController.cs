using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.Controllers
{

    
    [ApiController]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmployeeRepository employeeRepository)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.employeeRepository = employeeRepository;
        }


       /// *****Registrattion Code**********////

        // [HttpGet]

        //  [Route("Signup")]
        // public IActionResult Signup()
        // {
        //    return View();
        // }

        // [HttpPost]
        // [Route("Signup")]
        // public async Task<IActionResult> Signup(Signupmodel user)
        // {
        //    if (ModelState.IsValid)
        //    {
        //        var newuser = new IdentityUser { UserName = user.Email, Email = user.Email };
        //        var result = await UserManager.CreateAsync(newuser, user.Password);

        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(newuser, isPersistent: false);
        //            return RedirectToAction("index", "employee");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }

        //    }
        //    return View(user);




        // }

        
        
        // [HttpGet]
        // public IActionResult Login()
        // {
        //     return View();
        // }
        [Route("Login")]
        [HttpPost]
        //  public async Task<IActionResult> Login(Loginmodel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var loggedInUser = await UserManager.FindByEmailAsync(model.Email);
        //         if (loggedInUser != null )
        //         {
        //             var result = await SignInManager.PasswordSignInAsync(loggedInUser.UserName, model.Password, model.Rememberme, false);

        //             if(result.Succeeded)
        //             {

        //             var userRoles = await UserManager.GetRolesAsync(loggedInUser);

        //             var authClaims = new List<Claim>
        //                 {
        //                      new Claim("name", loggedInUser.UserName),
        //             new Claim(ClaimTypes.Name, loggedInUser.UserName),
        //             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                 };

        //             foreach (var userRole in userRoles)
        //             {
        //                 authClaims.Add(new Claim("role", userRole));
        //                 authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //             }
        //              var key = Encoding.UTF8.GetBytes("1234567890123456");
        //             var authSigningKey = new SymmetricSecurityKey(key);

        //             var token = new JwtSecurityToken(
        //                 expires: DateTime.Now.AddHours(3),
        //                 claims: authClaims,
        //                 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //                 );

        //             return Ok(new
        //             {
                        
        //                 token = new JwtSecurityTokenHandler().WriteToken(token),
        //             });

        //         }

        //     }
        //     return BadRequest(new { message = "Username or password is incorrect." });
        // }
        //        return BadRequest(new { message = "Username or password is incorrect." });

        // }
            public async Task<IActionResult> Login(Loginmodel user)
            {

                    var key = Encoding.UTF8.GetBytes("1234567890123456");
                    var loggedInUser = await UserManager.FindByEmailAsync(user.Email);

                    if (loggedInUser != null)
                    {
                    var role=await UserManager.GetRolesAsync(loggedInUser);
                    var tokendescriptor=new SecurityTokenDescriptor{

                        Subject= new ClaimsIdentity(new Claim[]{
                            new Claim("UserId",loggedInUser.Id.ToString()),
                            new Claim("Email", loggedInUser.Email),
                                new Claim(ClaimTypes.Name, loggedInUser.UserName),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim("role", role.FirstOrDefault()),
                                    new Claim(ClaimTypes.Role, role.FirstOrDefault())
                            
                            
                            

                        }),
                        Expires=DateTime.UtcNow.AddDays(1),
                            SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenhandler=new JwtSecurityTokenHandler();
                    var Securitytoken=tokenhandler.CreateToken(tokendescriptor);
                    var token=tokenhandler.WriteToken(Securitytoken);
                    var  result = await SignInManager.PasswordSignInAsync(loggedInUser.UserName, user.Password, user.Rememberme, false); 
                    if(result.Succeeded){
                        return Ok(new {result=result,token=token});  
                        } 
                        else{
                            return Ok(new {result=result,token=""});
                                
                        }
                        // {
                        //     return Redirect(ReturnUrl);
                            // }
                            // else
                            // {    
                            //  return Ok(result);
                        // }



                    
                        
                    }
                    return Ok(new{message="User does not exist"});

                


        



            
    }

        // public async Task<IActionResult> Logout()
        // {
        //     await SignInManager.SignOutAsync();
        //     return RedirectToAction("Login");
        // }

        // [HttpGet]
        // public ActionResult ResetPassword()
        // {
        //     return View();
        // }
        [HttpPost]
         [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(Signupmodel model)
        {   

            
                var user = await UserManager.FindByEmailAsync(model.Email);
              
                if (user != null)
                {
                      var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                    var result = await UserManager.ResetPasswordAsync(user, token, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok(new{err="Success"});
                    }
                    else
                    {
                        return Ok(new{err="Fail"});
                    }
                   
                }
                return Ok(new{err="Notfound"});
        
            


           

        }

        // [HttpGet]
        // public ActionResult ResetForm()
        // {
        //     return View();
        // }

        // [HttpPost]
        // public async Task<ActionResult> ResetForm(Signupmodel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var user = await UserManager.FindByEmailAsync(model.Email);
        //         var token = await UserManager.GeneratePasswordResetTokenAsync(user);
        //         if (user != null)
        //         {

        //             var result = await UserManager.ResetPasswordAsync(user, token, model.Password);
        //             if (result.Succeeded)
        //             {
        //                 return RedirectToAction("Login");
        //             }
        //             else
        //             {
        //                 foreach (var error in result.Errors)
        //                 {
        //                     ModelState.AddModelError("", error.Description);
        //                 }
        //             }
        //         }
        //         else
        //         {
        //             ModelState.AddModelError("", "something went wrong Please try Again");
        //         }
        //     }
        //     return View();

        // }

        // public ActionResult AccessDenied()
        // {
        //     return View();
        // }




    }
}
        