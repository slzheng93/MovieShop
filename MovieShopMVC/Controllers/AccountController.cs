using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        /*--------------------------------------------------
         * Login
         * Register
         * Logout
         *--------------------------------------------------
         */
        //showing the register view

        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;   
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]                        
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            // save the data in the User Table
            try
            {
                var user = await _accountService.Register(model);
                
                // redirect to login page
            }
            catch (Exception)
            {
                throw;
            }
           
           
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
           var user = await _accountService.Validate(model.Email, model.Password);
            if (user == null)
            {
                // retutn un/pw is wrong
                return View();
            }
            // if user enterred correct password then return a Cookie

            // craete claims object to store in the cookie

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("language", "english")

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create the secure cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


            return LocalRedirect("~/");
        }
    }
}
