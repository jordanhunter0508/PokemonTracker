using System.Security.Claims;
using DataDomain;
using LogicLayerInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                UserVM userVM = _userManager.LogInUser(user.Email, user.Password);

                if (userVM == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(user);
                }

                SetClaims(userVM);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "An error has occured when trying to sign in.";
                return View("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(SignupViewModel newuser)
        {
            if (!ModelState.IsValid)
            {
                return View(newuser);
            }

            try
            {
                bool wasCreated = _userManager.CreateUserAccount(newuser.GivenName,newuser.Surname,newuser.Email,newuser.Password);

                if (wasCreated)
                {
                    UserVM userVM = _userManager.LogInUser(newuser.Email, newuser.Password);
                    SetClaims(userVM);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An account with this email may have already been created.");
                    return View(newuser);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                ViewBag.DisplayError = "An error has occured when trying to sign up.";
                return View("Error", "Home");
            }
        }

        private void SetClaims(UserVM userVM)
        {
            // Sets user's id/name/ and email
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userVM.UserID.ToString()),
                new Claim(ClaimTypes.Email, userVM.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, userVM.GivenName + " " + userVM.Surname),
                new Claim(ClaimTypes.GivenName, userVM.GivenName ?? string.Empty),
                new Claim(ClaimTypes.Surname, userVM.Surname ?? string.Empty),
            };

            // Sets the user's roles
            foreach (string role in userVM.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(
               claims,
               CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false,   // clear when browser closes
                    AllowRefresh = true
                });
        }
    }
}
