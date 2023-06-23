using eTrade.Data;
using eTrade.Data.VIewModel;
using eTrade.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTrade.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        var role = await _userManager.GetRolesAsync(user);

                        if (role.Contains("admin"))
                        {
                            return Redirect("/admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginViewModel);
            }

            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginViewModel);
        }


        [HttpGet("register")]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            if (User.IsInRole("admin"))
            {
                return Redirect("/admin");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
