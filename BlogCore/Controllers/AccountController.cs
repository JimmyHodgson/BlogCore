using BlogCore.Common;
using BlogCore.Models.ViewModels;
using BlogCore.Models.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext context, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["error"] = new ErrorViewModel();
            ViewData["ReturnUrl"] = returnUrl;
            LoginViewModel model = new LoginViewModel();
            User admin = _context.Users.FirstOrDefault();
            if (admin == null)
            {
                model.RegisterEnabled = true;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Administrator"))
                        {
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction(nameof(EmailVerificationRequired));
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            User admin = _context.Users.FirstOrDefault();
            if (admin == null)
            {
                model.RegisterEnabled = true;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            User admin = _context.Users.FirstOrDefault();
            if (admin == null)
            {
                model.RegisterEnabled = true;
                if(model.Password != model.ConfirmPassword)
                {
                    ModelState.TryAddModelError("PasswordMismatch","The password fields must match.");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }
                    return View(model);
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);

                //send email
                var variables = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(Constants.EmailValues.Name, $"{user.FirstName} {user.LastName}"),
                    new KeyValuePair<string, string>(Constants.EmailValues.ConfirmationLink, confirmationLink)
                };
                var message = TemplateReader.GetTemplate(Constants.EmailTemplates.ConfirmationLinkTemplate, variables);

                _emailService.Send(user.Email, "Confirm your email", message, true);

                await _userManager.AddToRoleAsync(user, "Visitor");
                return RedirectToAction(nameof(SuccessRegistration));


            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
                return View(nameof(ConfirmEmail));
            }

            return View("Error");
        }
        public IActionResult SuccessRegistration()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

            var variables = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Constants.EmailValues.Name, $"{user.FirstName} {user.LastName}"),
                new KeyValuePair<string, string>(Constants.EmailValues.PasswordResetLink, link)
            };
            var message = TemplateReader.GetTemplate(Constants.EmailTemplates.PasswordResetTemplate, variables);

            _emailService.Send(user.Email, "BlogCore Password Reset", message, true);

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public IActionResult ResetPassword(string token, string email)
        {
            var model = new PasswordResetViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(PasswordResetViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user==null) return RedirectToAction(nameof(ResetPasswordConfirmation));

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public IActionResult EmailVerificationRequired()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(ManageController.Index), "Manage");
            }
        }
    }
}
