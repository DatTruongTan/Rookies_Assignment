using IdentityServer4.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServerBE.Models;
using ServerBE.Models.Auth;
using Shared.UserRole;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServerBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private IIdentityServerInteractionService _interaction;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEventService _events;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            IIdentityServerInteractionService interaction,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEventService events,
            IConfiguration configuration
            )
        {
            _interaction = interaction;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _events = events;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel/*, string button*/)
        {
            
            //if (button.Equals("cancel"))
            //{
            //    return Redirect("~/");
            //}

            //if (!ModelState.IsValid)
            //{
            //    return View("Sign Up", signUpModel);
            //}

            var user = new User()
            {
                UserName = signUpModel.Username,
                Email = signUpModel.Email,
            };

            var result = await _userManager.CreateAsync(user, signUpModel.Password);

            if (!result.Succeeded)
            {
                //return View("Register", RegisterVm);

                return Content("Signed Up Unsuccesfully!!!");

            }

            //return Unauthorized();
            return Ok(result.Succeeded);

            //await _userManager.AddToRoleAsync(user, "User");

            //await _signInManager.SignInAsync(user, false);

            //return Redirect(RegisterVm.ReturnUrl);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel/*, string button*/)
        {
            var context = await _interaction.GetAuthorizationContextAsync(signInModel.ReturnUrl);

            //if (button.Equals("cancel"))
            //{
            //    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

            //    return Redirect(context.RedirectUri);
            //}

            var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, false, false);

            if (!result.Succeeded)
            {
                return Content("Invalid Username or Password!!!");
                //ViewBag.Error = "Invalid Username or Password";
                //return View("Login", signInModel);
            }

            return Ok(result);

            //ViewBag.Error = null;

            //return Redirect(loginVm.ReturnUrl);
        }

        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] SignUpModel model)
        //{
        //    var userExists = await userManager.FindByNameAsync(model.Username);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //    User user = new User()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username
        //    };
        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //    if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await userManager.AddToRoleAsync(user, UserRoles.Admin);
        //    }

        //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //}
    }
}
