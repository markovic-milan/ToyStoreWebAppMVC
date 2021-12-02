using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToyStoreWebAppMVC.Data;
using ToyStoreWebAppMVC.Models;

namespace ToyStoreWebAppMVC.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserAuthController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            loginModel.LoginInValid = "true";
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    loginModel.LoginInValid = "";
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt!");
                }


            }
            return PartialView("_UserLoginPartial", loginModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string url = null)
        {
            await _signInManager.SignOutAsync();

            if (url != null)
            {
                return LocalRedirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            registrationModel.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = registrationModel.Username,
                    Email = registrationModel.Email,
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName
                };

                var result = await _userManager.CreateAsync(user, registrationModel.Password);

                if (result.Succeeded)
                {
                    registrationModel.RegistrationInValid = "";
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return PartialView("_UserRegistrationPartial", registrationModel);
                }
                AddErrorToModelState(result);
            }
            return PartialView("_UserRegistrationPartial", registrationModel);
        }
        [AllowAnonymous]
        public async Task<bool> UserNameExists(string username)
        {
            bool userNameExists = await _context.Users.AnyAsync(u => u.UserName.ToUpper() == username.ToUpper());

            if (userNameExists)
            {
                return true;
            }

            return false;
        }

        private void AddErrorToModelState(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
