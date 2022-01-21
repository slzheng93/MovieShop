using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await _accountService.Validate(model.Email, model.Password);

            if(user == null)
            {
                return Unauthorized("Wrong email/password");
            }
            // JWT Authentication
            return Ok(user);
        }
    }
}
