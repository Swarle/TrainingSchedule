using System.Runtime.CompilerServices;
using Amazon.SecurityToken.SAML;
using Microsoft.AspNetCore.Mvc;
using PLL.Services.Interfaces;

namespace PLL.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(string login, string password, string roleName)
        {
            await _authService.RegisterAsync(login, password, roleName);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(string login, string password)
        {
            await _authService.LoginAsync(login, password);

            return RedirectToAction("Index", "Home");
        }
    }
}
