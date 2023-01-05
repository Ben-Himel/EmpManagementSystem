using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EmpManagementSystem.Models;
using EmpManagementSystem.ViewModels;
using EmpManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmpManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager; //sign in user
        private UserManager<User> _userManager; //crate user
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManger)
        {
            this._signInManager = signInManager;
            this._userManager = userManger;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Employee");
            }
            return View();//reutn blank login view
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure:false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Employee");
                }

            }
            ModelState.AddModelError("", "Failed to Login");
            return View();
        }

        //get
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                EmpManagementSystem.Models.User newuser = new EmpManagementSystem.Models.User();
                newuser.FirstName=registerViewModel.FirstName;
                newuser.LastName=registerViewModel.LastName;
                newuser.UserName = registerViewModel.UserName;
                newuser.Email = registerViewModel.Email;
                newuser.PhoneNumber = registerViewModel.PhoneNumber;

                //crates new user
                var result=await _userManager.CreateAsync(newuser,registerViewModel.Password);
                if(result.Succeeded)
                {
                    if(newuser.UserName=="Admin")
                    {
                        await _userManager.AddToRoleAsync(newuser, "Admin");
                        await _userManager.AddToRoleAsync(newuser, "Employee");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newuser, "Employee");
                    }
                    return RedirectToAction("Login", "Account");

                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(registerViewModel);
        }

         public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
