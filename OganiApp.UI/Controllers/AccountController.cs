using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OganiApp.Core.Entities;
using OganiApp.Service.Models.Account;
using OganiApp.Service.Services.Interface;

namespace OganiApp.UI.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly RoleManager<AppRole> _roleManager;
        readonly IWebHostEnvironment _env;
        readonly IMessageSend _messageSend;

        public AccountController(IWebHostEnvironment env, UserManager<AppUser> userManager = null, SignInManager<AppUser> signInManager = null, RoleManager<AppRole> roleManager = null, IMessageSend messageSend = null)
        {
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _messageSend = messageSend;
        }



        #region Register
        public IActionResult Register()
        {
            return View(new UserCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserCreateDto dto)
        {
            //Validation
            if (!ModelState.IsValid) return View(dto);

            var user = new AppUser()
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var identity = await _userManager.CreateAsync(user, dto.Password);

            if (identity.Succeeded)
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = "Admin"
                });
                await _userManager.AddToRoleAsync(user, "Admin");

                var appUser = await _userManager.FindByEmailAsync(dto.Email);
                if (appUser == null) return View(dto);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action(nameof(VerifyEmail), "Account", new { userId = user.Id, token = code }, Request.Scheme, Request.Host.ToString());

                //Message Send
                _messageSend.MimeKitConfrim(appUser, url, code);

                return RedirectToAction("Login", "Account");
            }

            foreach (var error in identity.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(dto);
        }



        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user is null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("HomePage", "Home");
        }

        #endregion


        #region Login
        public IActionResult Login()
        {
            return View(new UserLoginDto());
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(dto.Username);

                var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("HomePage", "Home");
                }
                else if (result.IsLockedOut)
                {
                    var logouttime = await _userManager.GetLockoutEndDateAsync(user);
                    var minute = (logouttime.Value.UtcDateTime - DateTime.UtcNow).Minutes;

                    ModelState.AddModelError("", $"Hesabiniz {minute} deqiqeliyine muveqqeti olaraq baglanmisdir.");
                }
                else
                {
                    string message = string.Empty;

                    if (user != null)
                    {
                        var failedcount = await _userManager.GetAccessFailedCountAsync(user);
                        var count = _userManager.Options.Lockout.MaxFailedAccessAttempts - failedcount;

                        message = $"{count} defe de yalnis giris etseniz hesabiniz muveqqeti olarag baglanacaq.";
                    }

                    else if (user == null)
                    {
                        message = "Username ve password sehvdir.";
                    }

                    ModelState.AddModelError("", message);
                }
            }
            return View(dto);
        }
        #endregion


        #region LogOut
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
        #endregion


        #region AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion


        #region ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user))) return View(dto);

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new { email = dto.Email, token = code }, Request.Scheme);

            _messageSend.MimeMessageResetPassword(user, url, code);

            return RedirectToAction("ForgotPasswordConfirmation", "Account");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string Token, string Email)
        {
            if (Token == null || Email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token.");
            }
            var reset = new ResetPasswordDto { Email = Email };

            return View(reset);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            //Validation
            if (!ModelState.IsValid) return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) return RedirectToAction("Error", "Home");

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);

            if (result.Succeeded) return RedirectToAction("ResetPasswordConfirmation", "account");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        #endregion
    }
}
