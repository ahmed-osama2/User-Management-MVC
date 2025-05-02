using Demo.DataAccess.models.IdentityModel;
using Demo.Presentation.Utilities;
using Demo.Presentation.ViewModels;
using Demo.Presentation.ViewModels.IdentityViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller


    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View();
        

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(model: viewModel);
            var User = new ApplicationUser()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
            };
            var Result = _userManager.CreateAsync(User ,viewModel.Password).Result;
            if (Result.Succeeded) 
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError( string.Empty, error.Description);
                }

                return View(viewModel);


            }
        }

        #endregion

        #region login


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (user is not null)
            {
                bool Flag = _userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;

                if (Flag)
                {
                    var Result = _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
                    if (!Result.IsNotAllowed) ModelState.AddModelError(string.Empty, "Your Account Is Not Allowed");
                    if (!Result.IsLockedOut) ModelState.AddModelError(string.Empty, "Your Account Is Locked Out");
                    if (Result.Succeeded) return RedirectToAction(nameof(HomeController.Index), "Home");

                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Sign In");
            }
            return View(loginViewModel);





        }

        #endregion

        #region forgetPassowrd

        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(forgetPasswordViewModel.Email).Result;

                if (user is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email, Token }, Request.Scheme);

                    var Email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset Password",
                        Body = "ResetPasswordLink" //Todo
                    };

                    EmailSettings.SendEmail(Email);
                    return RedirectToAction(nameof(CheckYourInbox));

                }
                 
            }

            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), forgetPasswordViewModel);

        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();
        #endregion

        // Login
        // Sign out
    }
}
