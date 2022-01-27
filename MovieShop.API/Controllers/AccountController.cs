using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await _accountService.Validate(model.Email, model.Password);

            if (user == null)
            {
                return Unauthorized("Wrong email/password");
            }
            // JWT Authentication
            // Generate the JWT Token
            var token = GenerateJWT(user);
            return Ok(  new { token = token } );
        }


        private string GenerateJWT(UserLoginResponseModel user)
        {
            // store some claimsn in the Token

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (JwtRegisteredClaimNames.GivenName , user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.DateOfBirth.ToShortDateString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("language", "english")

            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // We need to generate the Token using secret key and we need to spicify details of the token such as
            // who is the issuer of the token
            // who is the Audience of the token
            // Expiration time for the token

            // Best way to store any secret information, any configuration information in
            // Azure KeyVault

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes (  _configuration["PrivateKey"] ));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var expiration = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDetails = new SecurityTokenDescriptor 
            {
                Subject = identityClaims,
                Expires = expiration,
                SigningCredentials = credentials,
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"]
            };

            var encodedJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodedJwt);

        }
    }
}
