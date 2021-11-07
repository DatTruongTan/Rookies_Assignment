using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerBE.Models;
using ServerBE.Models.Auth;
using Shared.Constants;
using System.Threading.Tasks;

namespace ServerBE.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AuthenticateController : Controller
    {
        private IIdentityServerInteractionService _interaction;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEventService _events;

        public AuthenticateController(
            IIdentityServerInteractionService interaction,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEventService events
            )
        {
            _interaction = interaction;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _events = events;
        }

        [HttpGet]
        public IActionResult SignUp(string returnUrl)
        {
            return View(new SignUpModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(/*[FromBody]*/ SignUpModel signUpModel, string button)
        {
            var returnUrl = signUpModel.ReturnUrl.Split('=');
            var role = returnUrl[1].Split('&');

            if (role[0] == "client")
            {
                if (button.Equals("cancel"))
                {
                    return Redirect(ConstantUri.SERVER_SITE_URL);
                }

                if (!ModelState.IsValid)
                {
                    return View("Sign Up", signUpModel);
                }

                var user = new User()
                {
                    UserName = signUpModel.Username,
                    Email = signUpModel.Email,
                };

                var result = await _userManager.CreateAsync(user, signUpModel.Password);

                if (!result.Succeeded)
                {
                    return View("SignUp", signUpModel);
                }
                await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, false);
            }
            else
            {
                if (button.Equals("cancel"))
                {
                    return Redirect(ConstantUri.ADMIN_PAGE_URL);
                }

                if (!ModelState.IsValid)
                {
                    return View("Sign Up", signUpModel);
                }

                var admin = new User()
                {
                    UserName = signUpModel.Username,
                    Email = signUpModel.Email,
                };

                var result = await _userManager.CreateAsync(admin, signUpModel.Password);

                if (!result.Succeeded)
                {
                    return View("SignUp", signUpModel);
                }
                await _userManager.AddToRoleAsync(admin, "Admin");

                await _signInManager.SignInAsync(admin, false);
            }

            return Redirect(signUpModel.ReturnUrl);

        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
            return View(new SignInModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(/*[FromBody]*/ SignInModel signInModel, string button)
        {
            var returnUrl = signInModel.ReturnUrl.Split('=');
            var role = returnUrl[1].Split('&');

            if(role[0] == "client")
            {
                var context = await _interaction.GetAuthorizationContextAsync(signInModel.ReturnUrl);

                if (button.Equals("cancel"))
                {
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    return Redirect(ConstantUri.CUSTOMER_SITE_URL);
                }

                var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, false, false);

                if (!result.Succeeded)
                {
                    ViewBag.Error = "Invalid Username or Password";
                    return View("SignIn", signInModel);
                }

                ViewBag.Error = null;
            }
            else
            {
                var context = await _interaction.GetAuthorizationContextAsync(signInModel.ReturnUrl);

                if (button.Equals("cancel"))
                {
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    return Redirect(ConstantUri.ADMIN_PAGE_URL);
                }

                var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, false, false);

                if (!result.Succeeded)
                {
                    ViewBag.Error = "Invalid Username or Password";
                    return View("SignIn", signInModel);
                }

                ViewBag.Error = null;
            }
            return Redirect(signInModel.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interaction.GetLogoutContextAsync(logoutId);

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
